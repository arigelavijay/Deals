app.service('RatingService', ['$http', '$q', 'Utility', function ($http, $q, Utility) {
    this.rateDeal = function (dealId, userId, rating) {
        var deferred = $q.defer();
        $http.post(Utility.ServiceUrl + '/rating/' + userId + '/' + dealId + '/' + rating).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };
}]);