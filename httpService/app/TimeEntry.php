<?php namespace App;
use DateTime;
use Storage;

class TimeEntry {
    var $Duration;
    var $Started;
    var $Ended;
    var $Description;
    var $StartedString;
    var $EndedString;
    var $DurationDisplay;
    var $IssueKey;

    public function stop() {
        $this->Ended = new DateTime();
    }

    public static function all($dateTime) {
        if ($dateTime == null) {
           $now = new DateTime();
           $dateTime = $now->format("Y-m-d");
        }

        $dataFileName = $dateTime . ".txt";

        if(Storage::exists($dataFileName)) {

            $data = json_decode(Storage::get($dataFileName));

            $timeEntries = [];
            foreach ($data as $entry) {
                $timeEntries[] = TimeEntry::create($entry);
            }

            return $timeEntries;
        }

        Storage::put($dataFileName, "");
        return null;
    }

    public static function find() {
    }

    public static function create($json) {
       $timeEntry = new TimeEntry();
        foreach ($json as $key => $value) {
            $timeEntry->{$key} = $value;
        }
        return $timeEntry;
    }
}
