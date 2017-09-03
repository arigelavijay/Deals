app.service('ReviewService', ['$http', '$q', 'Utility', function ($http, $q, Utility) {
    this.Insert = function (obj) {
        var deferred = $q.defer();
        $http.post(Utility.ServiceUrl + '/review/insert', JSON.stringify(obj)).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.reviews = function (dealId, skip, limit) {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/review/' + dealId + '?skip=' + skip + '&limit=' + limit).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };
}]);