<?php
namespace App\Http\Controllers;

use Illuminate\Routing\Controller;
use App\TimeEntry;

class EditEntriesController extends Controller {

    function __construct() {
     $this->middleware('modHeaders');
    }

	public function index($dateTime) {
        return TimeEntry::all($dateTime);
    }
}
