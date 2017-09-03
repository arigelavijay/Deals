app.service('CartService', ['$http', '$q', 'Utility', function ($http, $q, Utility) {
    this.GetItems = function (userId, isGuest) {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/cart/' + userId + '/' + isGuest).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.remove = function (userId, isGuest, dealId) {
        var deferred = $q.defer();

        $http.delete(Utility.ServiceUrl + '/cart/delete?userId=' + userId + '&isGuest=' + isGuest + '&dealId=' + dealId).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.updateCart = function (obj, userId) {
        var deferred = $q.defer();
        $http.post(Utility.ServiceUrl + '/cart/update/' + userId, JSON.stringify(obj)).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });        
        return deferred.promise;        
    };
}]);