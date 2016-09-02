/** Main Controller **/

var MainController = 

[
	'$scope',
    '$state',
	'storage',
	'Session',
	'BaseService',

	function($scope, $state, storage, Session, BaseService) {

	    $scope.$state = $state;

		$scope.setCurrentUser = function(user) {
			storage.set('loggedUser', user);
		}

		$scope.getCurrentUser = function() {
			return storage.get('loggedUser');
		}

		$scope.menuItems = BaseService.menu($scope.getCurrentUser());

		$scope.credentials = 
		{
			username : '',
			password : ''
		}

		$scope.reloadMenu = function() {
			$scope.menuItems = BaseService.menu($scope.getCurrentUser());
		}

		$scope.login = function (credentials) {
			BaseService.login(credentials).then(function(user) {
				$scope.setCurrentUser(user);
				$scope.reloadMenu();
			})
		}

		$scope.logout = function() {
			BaseService.logout();
			storage.remove('loggedUser');
			$scope.reloadMenu();
		}
	}
];