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
        getDurationDisplay: function (startDateTime, endDateTime) {
            var duration = this.getDuration(startDateTime, endDateTime);

            if (Math.floor(duration.asMinutes()) === 0) {
                return null;
            }
            if (Math.floor(duration.asHours()) === 0) {
                return Math.floor(duration.asMinutes()) + "m";
            }
            return Math.floor(duration.asHours()) + "h " + Math.floor(duration.asMinutes()) + "m";
        },
        getDuration: function (startDateTime, endDateTime) {
            return moment.duration(moment(endDateTime).diff(startDateTime));
        },
        first: function () {
            //return users[0];
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

timeEntriesApp.controller("timeEntryCtrl", function ($scope, $resource, $filter, timeEntrySrv) {
    $scope.isRunning = false;
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
                $scope.isRunning = false;
                $scope.hasData = false;
            } else {
                $scope.isRunning = timeEntries[timeEntries.length - 1].Ended == null;
                $scope.hasData = true;
            }
        }, function (statusCode) {
            console.log(statusCode);
        });
    }

        $scope.addData = function () {
            $scope.stopPreviousEntry();

            $scope.gridOptions.data.push({
            "Description": " ",
            "Started": null,
            "StartedString": " ",
            "Ended": null,
            "EndedString": " ",
            "DurationDisplay": ""
        });

            $scope.hasData = true;
        };

    $scope.stopPreviousEntry = function () {
        //stop the previous entry
        if ($scope.gridOptions.data.length !== 0) {
            var currentEntry = $scope.gridOptions.data[$scope.gridOptions.data.length - 1];
            currentEntry.Ended = new Date();
            currentEntry.EndedString =
                timeEntrySrv.formatDate(currentEntry.Ended);

            //update duration
            currentEntry.DurationDisplay = timeEntrySrv.getDurationDisplay(currentEntry.Started, currentEntry.Ended);
        }
    };

    $scope.startStop = function () {
        var currentEntry = $scope.gridOptions.data[$scope.gridOptions.data.length - 1];

        if (currentEntry.Started == null) {
            currentEntry.Started = timeEntrySrv.selectedDate;
            currentEntry.StartedString = timeEntrySrv.formatDate(currentEntry.Started);
        } else {
            $scope.stopPreviousEntry();
        }
    }

    $scope.gridOptions.onRegisterApi = function (gridApi) {
        //set gridApi on scope
        $scope.gridApi = gridApi;
        gridApi.edit.on.afterCellEdit($scope, function (rowEntity, colDef, newValue, oldValue) {

            var rowIndex = $scope.findIndex(rowEntity);
            $scope.save(timeEntrySrv.selectedDate, rowIndex, rowEntity);
            $scope.$apply();
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

    $scope.save = function (date, rowIndex, rowEntity) {
        var timeEntryRes = $resource("http://localhost:8181/:date/:rowIndex", {date: "@date", rowIndex: "@rowIndex"},
            {
                save: {
                    method: "POST"
                }
            }
        );

        timeEntryRes.get({
            date: $filter("date")(date, "yyyy_MM_dd"),
            rowIndex: rowIndex
        }).$promise.then(
            function (timeEntry) {
                timeEntry.RowIndex = rowIndex;
                timeEntry.Date = $filter("date")(date, "yyyy_MM_dd");
                timeEntry.Description = rowEntity.Description;
                timeEntry.Started = rowEntity.Started;
                timeEntry.StartedString = rowEntity.StartedString;
                timeEntry.Ended = rowEntity.Ended;
                timeEntry.EndedString = rowEntity.EndedString;
                timeEntry.DurationDisplay = timeEntrySrv.getDurationDisplay(time.Started, $scope.gridOptions[rowIndex].Ended);
                timeEntry.$save();

                $scope.gridOptions[rowIndex].DurationDisplay = timeEntry.DurationDisplay;
            },
            function (statusCode) {
                console.log(statusCode);
            }
        );
    };
});

