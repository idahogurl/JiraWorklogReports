<?php
namespace App\Http\Controllers;

use Illuminate\Routing\Controller;
use App\TimeEntry;
use Illuminate\Support\Facades\Request;
use Illuminate\Support\Facades\Log;

class EditEntriesController extends Controller {

    function __construct() {
     $this->middleware('modHeaders');
    }

	public function index($dateTime) {
        return TimeEntry::all($dateTime);
    }

    public function find($dateTime, $rowIndex) {
        $jsonString = str_replace("\\", "", json_encode($this->findByRowIndex($dateTime, $rowIndex), JSON_UNESCAPED_SLASHES));
        return substr($jsonString, 1, -1);
    }

    public function save($dateTime, $rowIndex) {
        Log::error($dateTime);
        Log::error("Row index".$rowIndex);
        Log::error("Row".Request::get("RowIndex"));
        $timeEntry = $this->find($dateTime, $rowIndex);
        $timeEntry->Description = Request::get("Description");
    }

    private function findByRowIndex($dateTime, $rowIndex) {
        $timeEntries = TimeEntry::all($dateTime);
        if(isset($timeEntries[$rowIndex])) {
            return json_encode($timeEntries[$rowIndex]);

        } else {
            return json_encode(new TimeEntry());
        }
    }
}
