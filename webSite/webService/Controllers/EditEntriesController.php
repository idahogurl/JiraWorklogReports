<?php namespace App\Http\Controllers;

use App\Http\Requests;

use App\TimeEntry;
use Illuminate\Http\Request;

class EditEntriesController extends Controller {

	public function index($dateTime) {
        return TimeEntry::all($dateTime);
    }
}
