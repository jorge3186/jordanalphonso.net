/** Login Controller **/

var LoginController = [

	'$scope',
	'$state', 

	function($scope,
			 $state) 
	{

	$scope.message = "Welcome to the Login Page";

	$scope.loggedUser = {'username':'','password':''};

	$scope.login =  function(loggedUser) {
		console.log(loggedUser.username+": "+loggedUser.password);
		$state.go('dashboard');
	}

	}
];	