app.controller('OrderInfoCntrl', ['$scope', 'OrderInfoService', 'Utility', '$routeParams', '$anchorScroll', '$location', function ($scope, OrderInfoService, Utility, $routeParams, $anchorScroll, $location) {
    var userId = sessionStorage.USER_ID;
    if (typeof userId == 'undefined')
        $location.path('/home');

    var orderId = $routeParams.id;        
    $scope.VendorsUrl = Utility.VendorsUrl;
    $scope.isPromoUsed = false;
    $scope.isDataLoading = true;
    OrderInfoService.getOrderInfo(userId, orderId).then(function (d) {        
        if (d.status == 200 && d.statusText == 'OK') {
            $scope.isDataLoading = false;
            $scope.OrderInfo = d.data.orders;
            $scope.address = d.data.address;
            if (d.data.promoCode != null)
                $scope.isPromoUsed = true;
            $anchorScroll('bodyId');
        }
    }, function (err) {

    });
}]);