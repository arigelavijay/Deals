app.service('DealInfoService', ['$http', '$q', 'Utility', function ($http, $q, Utility) {
    this.getDealInfo = function (id, userid, isGuest) {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/dealinfo/' + id + '/' + userid + '/' + isGuest).then(function (res) {
            deferred.resolve(res)
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.buyNow = function (obj, dealId, isGuest) {
        var deferred = $q.defer();
        $http.post(Utility.ServiceUrl + '/dealinfo/buy/' + dealId + '/' + isGuest, JSON.stringify(obj)).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };
}]);