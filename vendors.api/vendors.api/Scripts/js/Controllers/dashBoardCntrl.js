app.controller('dashBoardCntrl', function ($q, $scope, $location, $window, dealsService, ngTableParams, errorService) {
    $scope.vendorId = sessionStorage.getItem('USER_ID');

    var obj = {};
    $scope.tblParam = new ngTableParams(
        {
            page: 0,
            count: 10,
            sorting: {
                categoryName: 'DESC'
            }
        },
        {
            counts: [10, 20, 30],
            getData: function ($defer, params) {
                obj.offset = params.$params.page == 0 ? 0 : (params.$params.count * (params.$params.page - 1));
                obj.sortColumn = Object.keys(params.$params.sorting)[0];
                obj.sortType = params.$params.sorting[obj.sortColumn].toUpperCase();
                obj.limit = params.$params.count;

                dealsService.getPagedData(sessionStorage.getItem('USER_ID'), obj).then(function (d) {
                    //$scope.deals = d.data.data;
                    params.total(d.data.total);
                    $defer.resolve(d.data.data);
                }, errorService.handleErr);
            }
        }
    );

    

    $scope.checkStatus = function () {
        dealsService.checkStatus().then(function (d) {
            debugger;
        }, errorService.handleErr);
    };
    $scope.viewDeal = function (dealId) {
        dealsService.getDeal(sessionStorage.getItem('USER_ID'), dealId).then(function (d) {
            $scope.$parent.deal = d.data;
        }, errorService.handleErr);
    };

    $scope.imgSize = function (dealId, bannerId, bannerType) {
        $scope.$root.$broadcast('editDealBroadcast', {
            dealId: dealId,
            bannerId: bannerId,
            bannerType: bannerType
        });
    };
});