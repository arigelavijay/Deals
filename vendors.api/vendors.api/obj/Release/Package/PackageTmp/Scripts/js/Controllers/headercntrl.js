app.controller('headerCntrl', function ($scope, $location) {
    $scope.isHome = false;
    $scope.isDeal = false;
    $scope.isMyDeals = false;

    var location = $location.url();
    if (location == '/Pages/dashboard' || location == '/Pages/signin' || location == '/Pages/register')
        $scope.isHome = true;
    else if (location == '/Pages/deals')
        $scope.isDeal = true;
    else if (location == '/Pages/mydeals')
        $scope.isMyDeals = true;

    $scope.navFocus = function (option) {
        $scope.isHome = false;
        $scope.isDeal = false;
        $scope.isMyDeals = false;

        if (option == 'home')
            $scope.isHome = true;
        else if (option == 'deals')
            $scope.isDeal = true;
        else if (option == 'mydeals')
            $scope.isMyDeals = true;
    };
});