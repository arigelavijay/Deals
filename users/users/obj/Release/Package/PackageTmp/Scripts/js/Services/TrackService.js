app.service('TrackService', ['$http', '$q', 'Utility', function ($http, $q, Utility) {
    this.getOrders = function (userId, currentPage, limit) {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/track/orders/' + userId + '?currentPage=' + currentPage + '&limit=' + limit).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };
}]);