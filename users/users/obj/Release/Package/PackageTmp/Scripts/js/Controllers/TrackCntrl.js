app.controller('TrackCntrl', ['$scope', 'TrackService', 'Utility', '$location', '$anchorScroll', function ($scope, TrackService, Utility, $location, $anchorScroll) {
    var userId = sessionStorage.USER_ID;
    if (typeof userId == 'undefined')
        $location.path('/home');

    $scope.VendorsUrl = Utility.VendorsUrl;
    $scope.currentPage = 1;
    $scope.limit = 5;

    $scope.isDataLoading = true;
    $scope.getData = function () {        
        var skip = $scope.currentPage == 1 ? 0 : ($scope.limit * ($scope.currentPage - 1));
        TrackService.getOrders(userId, skip, $scope.limit).then(function (d) {
            if (d.status == 200 && d.statusText == 'OK') {
                $scope.isDataLoading = false;
                $scope.orders = d.data.data;
                $scope.totalItems = d.data.total;
            }
            //$anchorScroll('bodyId');
        }, function () {

        });
    };

    $scope.getData();    

    $scope.viewDetails = function (id) {        
        $location.path('/order-info/' + id);
    };

    $scope.pageChanged = function () {        
        $scope.getData();
    };
}]);