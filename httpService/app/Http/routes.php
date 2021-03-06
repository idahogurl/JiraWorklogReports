<?php

/*
|--------------------------------------------------------------------------
| Application Routes
|--------------------------------------------------------------------------
|
| Here is where you can register all of the routes for an application.
| It's a breeze. Simply tell Laravel the URIs it should respond to
| and give it the controller to call when that URI is requested.
|
*/
Route::get('/{dateTime}', 'EditEntriesController@index');
Route::get('/all',  'EditEntriesController@all');
Route::get('/{dateTime}/{index}', 'EditEntriesController@find');
Route::post('/', 'EditEntriesController@save');

