
app.controller('AddressCntrl', ['$scope', 'AddressService', '$uibModal','$location', function ($scope, AddressService, $uibModal, $location) {
    var userId = sessionStorage.USER_ID;
    if (typeof userId == 'undefined')
        $location.path('/home');

    $scope.isDataLoading = true;
    $scope.getAddress = function () {
        AddressService.getAddress(userId).then(function (d) {
            $scope.isDataLoading = false;
            $scope.addresses = d.data;            
        }, function (err) {

        });
    };

    $scope.getAddress();

    $scope.addAddress = function () {        
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'Pages/Templates/add-address.html',
            controller: 'addEditAddressCntrl',
            size: 'md',
            resolve: {
                addressId: function () {
                    return -1;
                }
            }
        });

        modalInstance.result.then(function () {
            $scope.getAddress();
        }, function () {
            //$log.info('Modal dismissed at: ' + new Date());
        });
    };   
    
}]);