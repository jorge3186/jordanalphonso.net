/**
*
*	Main Application - jordanalphonso.net
*
*	fulcrumApp is where these are registered:
*		1. Directives (/components/directives)
*		2. Controllers (/components/controllers)
*		3. Services (components/services)
*		4. Routes (components/routes)
*		5. Constants (components/app_constants)
*
*	@author Jordan Alphonso <jordanalphonso1@yahoo.com>
*	@date 2016
*
*/

var fulcrumApp = angular.module(

	//Application Name
	'fulcrum', 

	//Dependencies
	//Register here and make sure to import the necessary scripts in index.html
	[
		'ngAnimate',
		'ngCookies', 
		'ngResource',
		'angular-storage', 
		'ui.router', 
		'ui.router.stateHelper'
	]);

	/** Constants **/
	fulcrumApp.constant('authenticated', authenticated);

	/** Controllers **/
	fulcrumApp.controller('DashboardController', DashboardController);

	/** Routes **/
	fulcrumApp.config(mainRoutes);

	/** Services **/

	/** Directives **/