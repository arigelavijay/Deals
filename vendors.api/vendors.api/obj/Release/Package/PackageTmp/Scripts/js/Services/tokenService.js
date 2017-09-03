app.factory('token_auth', function ($q, $location) {
    return {
        checkToken: function () {
            if (sessionStorage.getItem('token') != null && sessionStorage.getItem('token') != '')
                return;
            else
                $location.path('/Pages/signin');
        }
    }
});