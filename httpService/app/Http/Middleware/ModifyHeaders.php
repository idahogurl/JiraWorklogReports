<?php
namespace App\Http\Middleware;

use Closure;

class ModifyHeaders
{

    /**
     * Handle an incoming request.
     *
     * @param  \Illuminate\Http\Request $request
     * @param  \Closure $next
     * @return mixed
     */
    public function handle($request, Closure $next) {
       header('Content-Type: application/json');

       return $next($request);
    }

}
