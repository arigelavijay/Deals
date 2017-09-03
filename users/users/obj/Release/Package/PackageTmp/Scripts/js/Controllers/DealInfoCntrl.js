app.controller('DealInfoCntrl', ['$scope',
                                 'mySharedService',
                                 '$routeParams',
                                 '$location',
                                 'DealInfoService',
                                 'Utility',
                                 '$anchorScroll',
                                 'growl',
                                 'HistoryService',
                                 'ReviewService',
                                 '$uibModal',
   function ($scope, mySharedService, $routeParams, $location, DealInfoService, Utility, $anchorScroll, growl, HistoryService, ReviewService, $uibModal) {

    var isUserLoggedIn = (sessionStorage.USER_ID != null && sessionStorage.USER_ID != '') ? true : false;
    if (!isUserLoggedIn)
        mySharedService.prepForBroadcast($location.path());

    var userid = sessionStorage.USER_ID;
    var isGuest = false;
    if (typeof (userid) == 'undefined') {
        userid = localStorage.getItem('GUEST_ID');
        isGuest = true;
    }

    var dealId = $routeParams.id;
    $scope.VendorsUrl = Utility.VendorsUrl;
    
    $scope.isDataLoading = true;
    DealInfoService.getDealInfo(dealId, userid, isGuest).then(function (d) {
        if (d.status == 200 && d.statusText == 'OK') {            
            $scope.isDataLoading = false;
            $scope.dealInfo = d.data;
            
            /* $scope.isReadonly = isUserLoggedIn ? d.data.isRated : true;*/
            $scope.dealInfo.isReviewed = isUserLoggedIn ? d.data.isReviewed : true;
            $anchorScroll();
        }
    }, function (err) {

    });

    
    if (!isGuest) {
        HistoryService.userHistory(userid, dealId).then(function (d) {
            if (d.status == 200 && d.statusText == 'OK') {
                $scope.historyVm = d.data;
            }
        }, function (err) {

        });
    }

    $scope.buyNow = function () {
        var dealInfo = $scope.dealInfo;       

        var obj = {
            dealId: dealInfo.dealId,
            vendorId: dealInfo.vendorId,
            bannerId: dealInfo.bannerId,
            locationId: dealInfo.locationId
        };
        DealInfoService.buyNow(obj, userid, isGuest).then(function (d) {            
            $location.path('/cart');
        }, function () {

        });
    };

    $scope.TimeLeft = function (hours) {
        if (typeof hours == 'undefined')
            return;

        var seconds = hours * 60 * 60;

        var x = seconds
        var d = moment.duration(x, 'seconds');
        var hours = Math.floor(d.asHours());
        var mins = Math.floor(d.asMinutes()) - hours * 60;

        return pad(hours) + ':' + pad(mins);
    };

    function pad(d) {
        return (d < 10) ? '0' + d.toString() : d.toString();
    }

    $scope.max = 5;
    $scope.hoveringOver = function (value) {
        $scope.overStar = value;
        $scope.percent = 100 * (value / $scope.max);
    };

    /*
    $scope.$watch(function () {
        if (typeof $scope.dealInfo == 'undefined')
            return;

        return $scope.dealInfo.rating;
    }, function (newVal, oldVal) {        
        if (newVal == oldVal || typeof newVal == 'undefined' || typeof oldVal == 'undefined' || isGuest)
            return;

        RatingService.rateDeal(dealId, userid, newVal).then(function (d) {
            if (d.status == 200 && d.statusText == 'OK') {
                
                $scope.isReadonly = true;
                $scope.dealInfo.rating = d.data.avg;
                growl.info('Thanks for rating...', {});
            }
        }, function () {

        });
    });
    */

    $scope.currentPage = 1;
    $scope.limit = 5;
    $scope.userReviews = function () {
        var skip = $scope.currentPage == 1 ? 0 : ($scope.limit * ($scope.currentPage - 1));
        ReviewService.reviews(dealId, skip, $scope.limit).then(function (d) {           
            $scope.reviews = d.data.reviews;
            $scope.totalItems = d.data.total;
        }, function () { });
    };

    $scope.pageChanged = function () {
        $scope.userReviews();
    };

    $scope.review = function () {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'Pages/Templates/add-review.html',
            controller: 'ReviewCntrl',
            size: 'md',
            resolve: {
                dealId: function () {
                    return parseInt(dealId);
                },
                userId: function () {
                    return parseInt(userid);
                }
            }
        });

        modalInstance.result.then(function (rating) {
            $scope.dealInfo.rating = rating;
            $scope.dealInfo.isReviewed = true;
            $scope.userReviews();
        }, function () {

        });
    };    

    $scope.userReviews();
}]);