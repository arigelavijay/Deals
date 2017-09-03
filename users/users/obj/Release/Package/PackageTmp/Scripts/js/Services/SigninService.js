app.service('SigninService', ['$http', '$q', 'Utility', function ($http, $q, Utility) {
    this.register = function (reg) {
        var deferred = $q.defer();
        $http.post(Utility.ServiceUrl + '/signin/register', JSON.stringify(reg)).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.login = function (log, guest_id) {
        var deferred = $q.defer();
        $http.post(Utility.ServiceUrl + '/signin/login/' + guest_id, JSON.stringify(log)).then(function (res) {
            deferred.resolve(res)
        }, function (err) {
            deferred.reject(err)
        });
        return deferred.promise;
    };

    this.checkUserName = function (val) {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/signin/checkusername/' + val).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.checkEmail = function (val) {
        var deferred = $q.defer();
        $http.post(Utility.ServiceUrl + '/signin/checkemail', { email: val }).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.forget = function (val) {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/signin/forgetpassword/' + val).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };
}]);