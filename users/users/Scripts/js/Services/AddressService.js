app.service('AddressService', ['$http', '$q', 'Utility', function ($http, $q, Utility) {
    this.getAddress = function (userId) {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/address/' + userId).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.getStates = function () {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/address/states').then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.addAddress = function (userId, Obj) {
        var deferred = $q.defer();
        $http.post(Utility.ServiceUrl + '/address/add/' + userId, JSON.stringify(Obj)).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.getAddressById = function (userId, id) {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/address/' + userId + '/' + id).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.editAddress = function (userId, id, Obj) {
        var deferred = $q.defer();
        $http.post(Utility.ServiceUrl + '/address/edit/' + userId + '/' + id, JSON.stringify(Obj)).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.deleteAddress = function (userId, id) {
        var deferred = $q.defer();
        $http.delete(Utility.ServiceUrl + '/address/delete/' + userId + '/' + id).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.defaultAddress = function (userId, id) {
        var deferred = $q.defer();
        $http.post(Utility.ServiceUrl + '/address/defaultaddress/' + userId + '/' + id).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };
}]);