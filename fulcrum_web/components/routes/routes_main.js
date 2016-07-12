/** 
*
*	Register routes for fulcrumApp in here.
*
*/

var mainRoutes = [

	'$stateProvider',
	'$urlRouterProvider',
	'$urlMatcherFactoryProvider',
	'stateHelperProvider',

	function($stateProvider, 
			 $urlRouterProvider, 
			 $urlMatcherFactoryProvider, 
			 stateHelperProvider) {

		$urlRouterProvider.otherwise('/');
		$urlMatcherFactoryProvider.strictMode(false)

		/** Register routes and subroutes below */
		stateHelperProvider.

		//Dashboard state - Main Page
		state(
			{	
				name: "dashboard",
				url: "/",
				templateUrl: "partials/dashboard.html",
				controller: "DashboardController",
				data: { requireLogin : false }
			}
		});

}];