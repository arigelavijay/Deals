app.controller('appCntrl', function ($scope, $location) {
    $scope.isSpecificPage = function () {
        
        var path = $location.path();        
        if (path == '/Pages/signin' || path == '/Pages/register') {
            console.log(path);
            console.log(true);
            return true;
        }
        else {
            console.log(path);
            console.log(false);
            return false;
        }
    };
});