///**
// * Created by rvest on 6/8/2015.
// */
var timeEntriesApp = angular.module('timeEntriesApp', ['ngTouch', 'ui.grid', 'ui.grid.edit', 'ui.grid.cellNav', 'ui.bootstrap', 'ngResource']);

timeEntriesApp.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
}
]);

timeEntriesApp.factory("timeEntrySrv", function ($resource, $filter) {
    return {
        selectedDate: new Date(),
        all: function (date) {
            return $resource("http://localhost:8181/:date", {date: "@date"}).query({date: $filter('date')(date, "yyyy_MM_dd")});
        },
        formatDate: function (date) {
            return $filter("date")(date, "M/dd/yyyy h:mm a");
        },
        getDurationDisplay: function (startedString, endedString) {
            if (isEmpty(startedString) || isEmpty(endedString)) {
                return null;
            }
            var duration = this.getDuration(new Date(startedString), new Date(endedString));

            if (Math.floor(duration.asMinutes()) === 0) {
                return null;
            }
            if (Math.floor(duration.asHours()) === 0) {
                return Math.floor(duration.asMinutes()) + "m";
            }
            return Math.floor(duration.asHours()) + "h " + (Math.floor(duration.asMinutes()) - (Math.floor(duration.asHours()) * 60)) + "m";
        },
        getDuration: function (startDateTime, endDateTime) {
            return moment.duration(moment(endDateTime).diff(startDateTime));
        },
        first: function () {
            //return users[0];
        },
        isDirty: function(timeEntry, rowEntity) {
            if(timeEntry.Description !== rowEntity.Description) {
                return true;
            }

            if (timeEntry.StartedString !== rowEntity.StartedString) {
                return true;
            }

            if (timeEntry.EndedString !== rowEntity.EndedString) {
                return true;
            }

            return false;
        }
    };
});

timeEntriesApp.controller("datePickerCtrl", function ($scope, timeEntrySrv) {
    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();

    $scope.clear = function () {
        $scope.dt = null;
    };

    // Disable weekend selection
    $scope.disabled = function (date, mode) {
        return ( mode === "day" && ( date.getDay() === 0 || date.getDay() === 6 ) );
    };

    $scope.toggleMin = function () {
        $scope.minDate = $scope.minDate ? null : new Date();
    };
    $scope.toggleMin();

    $scope.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened = true;
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1
    };

    $scope.formats = ["dd-MMMM-yyyy", "yyyy/MM/dd", "dd.MM.yyyy", "shortDate"];
    $scope.format = $scope.formats[0];

    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    var afterTomorrow = new Date();
    afterTomorrow.setDate(tomorrow.getDate() + 2);
    $scope.events =
        [
            {
                date: tomorrow,
                status: 'full'
            },
            {
                date: afterTomorrow,
                status: 'partially'
            }
        ];

    $scope.getDayClass = function (date, mode) {
        if (mode === "day") {
            var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                if (dayToCheck === currentDay) {
                    return $scope.events[i].status;
                }
            }
        }

        return "";
    };

    $scope.dateChanged = function (dt) {
        timeEntrySrv.selectedDate = dt;
        $scope.gridLoaded = false;
        $scope.loadTimeEntries();
    }
});

function isEmpty(value) {
    if(value === null || value === "") {
        return true;
    }
    return false;
}
timeEntriesApp.controller("timeEntryCtrl", function ($scope, $resource, $filter, timeEntrySrv) {
    $scope.hasData = false;
    
    $scope.gridOptions = {
        enableCellSelection: true,
        enableRowSelection: false,
        enableCellEditOnFocus: true,
        columnDefs: [
            {field: "Description", displayName: "Description", enableCellEdit: true},
            {field: "StartedString", displayName: "Started", enableCellEdit: true},
            {field: "EndedString", displayName: "Ended", enableCellEdit: true},
            {field: "DurationDisplay", displayName: "Duration", enableCellEdit: true}
        ]
    };

    $scope.loadTimeEntries = function () {
        timeEntrySrv.all(timeEntrySrv.selectedDate).$promise.then(
        function (timeEntries) {
            $scope.gridOptions.data = timeEntries;
            if (timeEntries.length === 0) {
               $scope.hasData = false;
            } else {
               $scope.hasData = true;
            }
        }, function (statusCode) {
            console.log(statusCode);
        });
    }

    $scope.addData = function () {
        $scope.stopPreviousEntry();

        $scope.gridOptions.data.push({
            "Description": null,
            "Started": null,
            "StartedString": null,
            "Ended": null,
            "EndedString": null,
            "DurationDisplay": null
        });


        $scope.hasData = true;
    };

    $scope.stopPreviousEntry = function () {
        //stop the previous entry

        if ($scope.gridOptions.data.length !== 0) {
            var lastEntry = $scope.gridOptions.data[$scope.gridOptions.data.length - 1];
            if (isEmpty(lastEntry.EndedString)) {

                lastEntry.EndedString =
                    timeEntrySrv.formatDate(new Date());

                //update duration
                lastEntry.DurationDisplay = timeEntrySrv.getDurationDisplay(lastEntry.StartedString, lastEntry.EndedString);
                $scope.save($scope.gridOptions.data.length - 1, lastEntry);
            }
        }
    };

    $scope.startStop = function () {
        var currentEntry = $scope.gridOptions.data[$scope.gridOptions.data.length - 1];

        if (isEmpty(currentEntry.StartedString)) {
            currentEntry.StartedString = timeEntrySrv.formatDate(timeEntrySrv.selectedDate);
        } else {
            currentEntry.EndedString = timeEntrySrv.formatDate(new Date());

            //update duration
            currentEntry.DurationDisplay = timeEntrySrv.getDurationDisplay(currentEntry.StartedString, currentEntry.EndedString);
        }
        $scope.save($scope.gridOptions.data.length - 1, currentEntry);
    }

    $scope.gridOptions.onRegisterApi = function (gridApi) {
        //set gridApi on scope
        $scope.gridApi = gridApi;
        gridApi.edit.on.afterCellEdit($scope, function (rowEntity, colDef, newValue, oldValue) {
            $scope.save($scope.findIndex(rowEntity), rowEntity);
        });
    };

    $scope.findIndex = function (row) {
        // find real row by comparing $$hashKey with entity in row
        var rowIndex = -1;
        var hash = row.$$hashKey;
        var data = $scope.gridOptions.data;     // original rows of data
        for (var ndx = 0; ndx < data.length; ndx++) {
            if (data[ndx].$$hashKey === hash) {
                rowIndex = ndx;
                break;
            }
        }
        return rowIndex;
    };

    $scope.save = function (rowIndex, rowEntity) {


        var timeEntryRes = $resource("http://localhost:8181/:date/:rowIndex", { date: "@date", rowIndex: "@rowIndex" },
            {
                save: {
                    method: "POST"
                }
            }
        );


        timeEntryRes.get({
            date: $filter("date")(timeEntrySrv.selectedDate, "yyyy_MM_dd"),
            rowIndex: rowIndex
        }).$promise.then(
            function (timeEntry) {
                //did it really change
                if (timeEntrySrv.isDirty(timeEntry, rowEntity)) {
                    timeEntry.RowIndex = rowIndex;
                    timeEntry.Date = $filter("date")(timeEntrySrv.selectedDate, "yyyy_MM_dd");
                    timeEntry.Description = rowEntity.Description;
                    timeEntry.StartedString = rowEntity.StartedString;
                    timeEntry.EndedString = rowEntity.EndedString;

                    timeEntry.DurationDisplay = timeEntrySrv.getDurationDisplay(timeEntry.StartedString, timeEntry.EndedString);
                    timeEntry.$save().then(
                        function () {
                            //scope.isRunning =
                        });
                }

            },
            function (statusCode) {
                console.log(statusCode);
            }
        );
        //$scope.$apply();
    }

    $scope.isRunning = function() {

        if ($scope.gridOptions.data == undefined || $scope.gridOptions.data.length == 0) {
            return false;
        }

        var currentEntry = $scope.gridOptions.data[$scope.gridOptions.data.length - 1];
        return (!isEmpty(currentEntry.StartedString) && isEmpty(currentEntry.EndedString));
    }




});

