app.service('OrderService', ['$http', '$q', 'Utility', function ($http, $q, Utility) {
    this.applyPromo = function (userId, promoCode) {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/order/applypromo/' + userId + '/' + promoCode).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err)
        });
        return deferred.promise;
    };

    this.confirm = function (userId, promoCode) {
        var deferred = $q.defer();
        $http.post(Utility.ServiceUrl + '/order/confirm/' + userId + '/' + promoCode).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };
}]);