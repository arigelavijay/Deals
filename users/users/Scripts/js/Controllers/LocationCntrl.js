app.controller('LocationCntrl', ['$scope', '$uibModalInstance', 'LocationService', function ($scope, $uibModalInstance, LocationService) {
    $scope.getParentLocations = function () {
        LocationService.getParentLocations().then(function (d) {
            $scope.parents = d.data;
        }, function (err) {

        });
    };

    $scope.getParentLocations();

    $scope.parentChanged = function (parent) {
        angular.forEach($scope.parents, function (item, i) {
            if (item.id == parent) {
                $scope.locations = item.locations;
            }
        });
    };

    $scope.locationChanged = function (location) {
        var lObj = {};
        angular.forEach($scope.locations, function (item, i) {
            if (item.id == location) {                
                lObj.id = location;
                lObj.name = item.name;
            }
        });
        localStorage.RxShopyLocation = JSON.stringify(lObj);
        $uibModalInstance.close();
    };
}]);