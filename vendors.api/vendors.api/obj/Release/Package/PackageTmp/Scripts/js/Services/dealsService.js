app.service('dealsService', function ($http, $q, utility) {
    
    this.getLocationsCategories = function () {
        var deferred = $q.defer();
        $http.get(utility.serviceUrl + 'deals/locationscategories').then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.add = function (obj, vendorId, file) {
        var deferred = $q.defer();

        var data = new FormData();
        data.append('file', file)
        data.append('obj', JSON.stringify(obj));

        var objXhr = new XMLHttpRequest();
        objXhr.onreadystatechange = function () {
            if (objXhr.readyState == 4) {
                deferred.resolve('Success');
            }
        };

        objXhr.onerror = function () {
            deferred.reject('Error');
        };
        
        objXhr.open('POST', utility.serviceUrl + 'deals/add/' + sessionStorage.getItem('USER_ID'));
        objXhr.setRequestHeader('AUTH_TOKEN', sessionStorage.getItem('AUTH_TOKEN'));
        objXhr.setRequestHeader('USER_ID', sessionStorage.getItem('USER_ID'));
        
        objXhr.send(data);
        return deferred.promise;
    };

    this.checkStatus = function () {
        var deferred = $q.defer();
        $http.get(utility.serviceUrl + 'deals/status').then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.getAvailDatesForLocation = function (locationId, bannerId) {
        var deferred = $q.defer();
        $http.get(utility.serviceUrl + 'deals/dates/' + locationId + '/' + bannerId).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.getDeals = function (vendorId) {
        var deferred = $q.defer();
        $http.get(utility.serviceUrl + 'deals/' + vendorId).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.getPagedData = function (vendorId, obj) {
        var deferred = $q.defer();
        $http.post(utility.serviceUrl + 'deals/pageddata/' + vendorId, JSON.stringify(obj)).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.getDeal = function (vendorId, dealId) {
        var deferred = $q.defer();
        $http.get(utility.serviceUrl + 'deals/' + vendorId + '/' + dealId).then(function (res) {
            deferred.resolve(res);
        }, function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    this.updateDeal = function (vendorId, locationId, bannerId, dealId, file, obj) {
        var deferred = $q.defer();        

        var data = new FormData();
        data.append('file', file)
        data.append('obj', JSON.stringify(obj));

        var objXhr = new XMLHttpRequest();
        objXhr.onreadystatechange = function () {
            if (objXhr.readyState == 4) {
                deferred.resolve('Success');
            }
        };

        objXhr.onerror = function () {
            deferred.reject('Error');
        };

        objXhr.open('POST', utility.serviceUrl + 'deals/update/' + vendorId + '/' + locationId + '/' + bannerId + '/' + dealId);
        objXhr.setRequestHeader('AUTH_TOKEN', sessionStorage.getItem('AUTH_TOKEN'));
        objXhr.setRequestHeader('USER_ID', sessionStorage.getItem('USER_ID'));

        objXhr.send(data);
        return deferred.promise;
    };
    
});