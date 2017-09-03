app.service('LocationService', ['$http', '$q', 'Utility', function ($http, $q, Utility) {
    this.getParentLocations = function () {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/location/parents').then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };
}]);