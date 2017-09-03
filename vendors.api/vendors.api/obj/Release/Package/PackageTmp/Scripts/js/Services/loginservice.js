app.factory('loginService', function ($http, utility) {
    return {
        SignIn: function (obj) {
            return $http.post(utility.serviceUrl + 'login/login', JSON.stringify(obj));
        },

        Register: function (obj) {
            debugger;
            return $http.post(utility.serviceUrl + 'login/register', JSON.stringify(obj)).then(function (res) {
                return res;
            }, function (err) {
                return err;
            });
        }
    }
});