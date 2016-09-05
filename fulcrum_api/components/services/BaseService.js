var BaseService =

[
	'$http',
	'Session',
	'context',
	'ROLES',

	function($http, Session, context, ROLES) {
		var baseService = {};

		baseService.login = function(credentials) {
			return $http
				.post(context+'/login', credentials)
				.then(function(response){
					Session.create(response.data.id, response.data.user,
						response.data.roles, response.data.company);
					return response.data;
				})
		}

		baseService.logout = function() {
		    Session.destroy();
		    return $http.get(context + '/logout');
		}

		baseService.authenticated = function(user) {
			return (user !== undefined && user !== null);
		}

		baseService.authorized = function(authorizedRole, user) {
    		return (baseService.authenticated(user) &&
      			user.roles.indexOf(authorizedRole) !== -1);
		}

		baseService.menu = function(user) {
			menuItems = 
			[
				{name:'H', link:'dashboard'},
				{name:'A', link:'about'},
				{name:'C', link:'contact'},
				{name:'P', link:'portfolio'}
			]

			if (baseService.authorized(ROLES.Owner, user)) {
				menuItems.push({name:'MESSAGES', link:'messageBoard'});
			}
			return menuItems;
		}

		return baseService;
	}
]