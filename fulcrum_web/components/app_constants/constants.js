/** 
*
* Register common constants here.
*
**/

var authenticated = function($scope) {

	$rootScope.loggedUser = {

	}
	return false;
};

var USER_ROLES = function() 
{
	return 
	{
		admin : 'A',
		developer : 'D',
		viewer : 'V'
	}
}