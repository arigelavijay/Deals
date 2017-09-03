app.service('OrderInfoService', ['$http', '$q', 'Utility', function ($http, $q, Utility) {
    this.getOrderInfo = function (userId, orderId) {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/OrderInfo/' + userId + '/' + orderId).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };
}]);