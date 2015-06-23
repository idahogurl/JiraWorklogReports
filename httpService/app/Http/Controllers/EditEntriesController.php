<?php
namespace App\Http\Controllers;

use App\TimeEntry;
use Illuminate\Routing\Controller;
use Illuminate\Support\Facades\Log;
use Illuminate\Support\Facades\Response;
use Illuminate\Support\Facades\Request;

class EditEntriesController extends Controller
{

    function __construct()
    {
        $this->middleware('modHeaders');
    }

    public function index($dateTime)
    {
        return TimeEntry::all($dateTime);
    }

    public function find($dateTime, $rowIndex)
    {
        $timeEntry = $this->findByRowIndex($dateTime, $rowIndex);
        return Response::json($timeEntry);
    }

    public function save()
    {
        $request = Request::json()->all();

        $timeEntries = TimeEntry::all($request["Date"]);

        $timeEntry = $this->find($request["Date"], $request["RowIndex"]);

        $timeEntry->StartedString = $request["StartedString"];
        $timeEntry->EndedString = $request["EndedString"];
        $timeEntry->Description = $request["Description"];
		$timeEntry->DurationDisplay = $request["DurationDisplay"];

        if (str_contains($request["Description"], ":")) {
            $timeEntry->IssueKey = preg_split(":", $request["Description"]);
        }

        $timeEntries[$request["RowIndex"]] = $timeEntry;

        TimeEntry::save($request["Date"], $timeEntries);
    }

	private function findByRowIndex($dateTime, $rowIndex) {
        $timeEntries = TimeEntry::all($dateTime);
        if (isset($timeEntries[$rowIndex])) {
            return $timeEntries[$rowIndex];
        } else {
            return new TimeEntry();
        }
    }

    function var_error_log( $object=null ){
        ob_start();                    // start buffer capture
        var_dump( $object );           // dump the values
        $contents = ob_get_contents(); // put the buffer into a variable
        ob_end_clean();                // end capture
        error_log( $contents );        // log contents of the result of var_dump( $object )
    }
}
