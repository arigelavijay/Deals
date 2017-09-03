app.service('HistoryService', ['$http', '$q', 'Utility', function ($http, $q, Utility) {    
    this.userHistory = function (userId, dealId) {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/history/' + userId + '/' + dealId).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };
}]);