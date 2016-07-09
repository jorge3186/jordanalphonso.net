/** 
*
*	Register routes for fulcrumApp in here.
*
*/

fulcrumApp.config([

	'stateHelperProvider',
	$urlRouteProvider,
	$stateProvider,
	$urlMatcherFactoryProvider,

	function(stateHelperProvider, $stateProvider, $urlRouteProvider, $urlMatcherFactoryProvider) {

		$urlRouteProvider.otherwise('/login');
		$urlMatcherFactoryProvider.strictMode(false)

		/** Register routes and subroutes below */
		$stateHelperProvider.

		//Dashboard state - Main Page
		state({
			name: "dashboard",
			url: "/",
			templateUrl: "partials/dashboard.html",
			controller: "DashboardController",
			data: { requireLogin : false }
		})

}]);