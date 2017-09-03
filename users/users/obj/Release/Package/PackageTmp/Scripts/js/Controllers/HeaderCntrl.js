app.controller('HeaderCntrl', ['$scope', '$location', '$routeParams', 'mySharedService', 'HomeService', '$route', 'growl', 'SearchService', 'Utility', 'limitToFilter','$http', function ($scope, $location, $routeParams, mySharedService, HomeService, $route, growl, SearchService, Utility, limitToFilter, $http) {
    /* start */
    $scope.data = {};
    $scope.data.case = 0;

    
    $scope.VendorsUrl = Utility.VendorsUrl;
    /* to retain the login-header when page refresh and back navigations */
    var isUserLoggedIn = (sessionStorage.USER_ID != null && sessionStorage.USER_ID != '') ? true : false;
    if (isUserLoggedIn) {
        $scope.data.case = 1;
    }
    else if (localStorage.GUEST_ID == null) {
        $location.path('/home');
    }

    /* handling broadcast for first time user logs in and changes the header to login-header */
    $scope.$on('USERLOGGEDIN', function () {
        $scope.data.case = 1;
        $scope.userName = sessionStorage.USER_NAME;
    });

    $scope.$on('USERLOGGEDOUT', function () {        
        $scope.data.case = 0;
        $scope.doHide = false;
    });
    /* end */


    /* handling the broadcast from same controller and other controller */
    $scope.$on('handleBroadcast', function () {        
        if (mySharedService.path == '/home') {
            $scope.doHide = false;
        }
        else if (mySharedService.path == '/sign-up') {
            $scope.doHide = true;
        }
    });

    /* When user clicks on sign-in to hide header  */
    $scope.hide = function (val) {
        $scope.doHide = val;
    };

    /* when user clicks on sign-in and refresh the page to hide the header */
    var isUserLoggedIn = (sessionStorage.USER_ID != null && sessionStorage.USER_ID != '') ? true : false;
    if (!isUserLoggedIn)
        mySharedService.prepForBroadcast($location.path());
    else
        $scope.userName = sessionStorage.USER_NAME;

    /* logout */
    $scope.logOut = function () {
        for (var item in sessionStorage) {
            sessionStorage.removeItem(item);
        }
        mySharedService.LogOutBroadcast();

        HomeService.getGuestId().then(function (d) {
            if (d.status == 200 && d.statusText == 'OK') {
                localStorage.setItem('GUEST_ID', d.data.guest_id);
                $route.reload();
            }
        }, function (err) {

        });
        growl.info('Logged out successfully...!', {});
    };
    /* logout */    

    $scope.results = function (text) {
        //return function() {
        //    SearchService.getResults(text).then(function (d) {
        //        if (d.status == 200 && d.statusText == 'OK') {
        //            //$scope.results = d.data;
                
        //            return limitToFilter(d.data, 15);
        //        }
        //    }, function (err) { })

        //};
         

        //$http.get(Utility.ServiceUrl + '/search/' + text).then(function (res) {
        //    deferred.resolve(res);
        //}, function (err) {
        //    deferred.reject(err);
        //});

        return $http.get(Utility.ServiceUrl + '/search/' + '/7/' + text).then(function (response) {
            return limitToFilter(response.data, 15);
        });
    };
    /*

     $scope.cities = function (cityName) {
        return $http.jsonp("http://gd.geobytes.com/AutoCompleteCity?callback=JSON_CALLBACK &filter=US&q=" + cityName).then(function (response) {
            debugger;
            return limitToFilter(response.data, 15);
        });
    };

    $scope.getDeals = function (text) {
        SearchService.getResults(text).then(function (d) {
            if (d.status == 200 && d.statusText == 'OK') {
                $scope.results = d.data;
            }
        }, function (err) { });
    };

    $scope.$watch(function () {
        return $scope.selecteddeal;
    }, function (newVal, oldVal) {
        console.log(newVal);
        console.log(oldVal);
        if (typeof newVal != 'undefined' && newVal != '')
            $scope.getDeals(newVal);
    });*/
}]);