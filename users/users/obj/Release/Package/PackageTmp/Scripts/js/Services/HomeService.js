app.service('HomeService', ['$http', '$q', 'Utility', function ($http, $q, Utility) {
    this.getDeals = function (locationId) {        
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/home/deals/' + locationId).then(function (res) {
            deferred.resolve(res);
        }, function (err) {            
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.getGuestId = function () {
        var deferred = $q.defer();
        $http.get(Utility.ServiceUrl + '/home/guestid').then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };
}]);