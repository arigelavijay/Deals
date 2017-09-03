app.service('SearchService', ['$http', '$q', 'Utility', function ($http, $q, Utility) {
    this.getResults = function (text) {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/search/' + text).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };
}]);