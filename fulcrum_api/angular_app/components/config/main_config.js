/** 
*
*	Register routes for fulcrumApp in here.
*
**/

var fulcrumConfig = [

	'$stateProvider',
	'$urlRouterProvider',
	'$httpProvider',
	'$urlMatcherFactoryProvider',
	'stateHelperProvider',

	function($stateProvider, 
			 $urlRouterProvider, 
			 $httpProvider,
			 $urlMatcherFactoryProvider, 
			 stateHelperProvider) {

		$httpProvider.defaults.withCredentials = true;
		
		$urlRouterProvider.otherwise('/');
		$urlMatcherFactoryProvider.strictMode(false)

		/** Register routes and subroutes below */
		stateHelperProvider.

		//Dashboard state - Main Page and generic info
		state(
			{
				name: "dashboard",
				url: "/",
				templateUrl: "angular_app/partials/dashboard.html",
				controller: "DashboardController",
			})
		//Contact state - for contact info page
		.state(
			{
				name: "contact",
				url : "/contact",
				templateUrl: "angular_app/partials/contact.html",
				controller: "ContactController",
			})
		//About state - for info and desc page
		.state(
			{
				name: "about",
				url : "/about",
				templateUrl: "angular_app/partials/about.html",
				controller: "AboutController",
			})
		//Portfolio state - for demo material and examples
		.state(
			{
				name: "portfolio",
				url : "/portfolio",
				templateUrl: "angular_app/partials/portfolio.html",
				controller: "PortfolioController",
			})
}];