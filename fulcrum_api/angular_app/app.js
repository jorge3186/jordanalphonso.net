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
		'angularLocalStorage',
		'ngResource',
		'ui.router', 
		'ui.router.stateHelper'
	]);

	/** Constants **/
	fulcrumApp.constant('ROLES', ROLES);
	fulcrumApp.constant('context', context);

	/** Services **/
	fulcrumApp.service('Session', Session);
	fulcrumApp.factory('BaseService', BaseService);

	/** Directives **/

	/** Controllers **/
	fulcrumApp.controller('MainController', MainController);
	fulcrumApp.controller('DashboardController', DashboardController);
	fulcrumApp.controller('AboutController', AboutController);
	fulcrumApp.controller('ContactController', ContactController);

	/** Routes **/
	fulcrumApp.config(fulcrumConfig);