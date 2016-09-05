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
				templateUrl: "partials/dashboard.html",
				controller: "DashboardController",
			})
		//Contact state - for contact info page
		.state(
			{
				name: "contact",
				url : "/contact",
				templateUrl: "partials/contact.html",
				controller: "ContactController",
			})
		//About state - for info and desc page
		.state(
			{
				name: "about",
				url : "/about",
				templateUrl: "partials/about.html",
				controller: "AboutController",
			})
		//Portfolio state - for demo material and examples
		.state(
			{
				name: "portfolio",
				url : "/portfolio",
				templateUrl: "partials/portfolio.html",
				controller: "PortfolioController",
			})
}];