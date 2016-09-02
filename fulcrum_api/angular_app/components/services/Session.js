var Session = 

function() {

	this.create = function(userId, userName, roles, company) {
		this.userId = userId;
		this.user = userName;
		this.userRoles = roles;
		this.company = company;
	}

	this.destroy = function() {
		this.userId = null;
		this.user = null;
		this.userRoles = null;
		this.company = null;
	}
}