app.directive('userAddress', function () {
    return {
        restrict: 'E',
        replace: true,
        transclude: 'element',
        scope: {
            address: '=data',
            action: '@'
        },
        templateUrl: 'Pages/Directives/user-address.html',
        controller: ['$scope', '$uibModal', 'AddressService', '$location', '$routeParams', 'growl', function ($scope, $uibModal, AddressService, $location, $routeParams, growl) {

            var userId = sessionStorage.USER_ID;
            if (typeof userId == 'undefined')
                $location.path('/home');

            $scope.showDeliverBtn = false;
            $scope.Frm = $routeParams.frm;
            if ($scope.Frm == 'cart')
                $scope.showDeliverBtn = true;

            $scope.editAddress = function (addressId) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: 'Pages/Templates/add-address.html',
                    controller: 'addEditAddressCntrl',
                    size: 'md',
                    resolve: {
                        addressId: function () {
                            return addressId
                        }
                    }
                });

                modalInstance.result.then(function () {
                    $scope.$parent.getAddress();
                }, function () {
                    //$log.info('Modal dismissed at: ' + new Date());
                });
            };

            $scope.deleteAddress = function (addressId) {
                AddressService.deleteAddress(userId, addressId).then(function (d) {
                    if (d.status == 200 && d.statusText == 'OK') {
                        $scope.$parent.getAddress();
                        growl.success('Address Deleted Successfully!', {});
                    }
                }, function (err) { });
            };

            $scope.defaultAddress = function (addressId) {
                AddressService.defaultAddress(userId, addressId).then(function (d) {
                    if (d.status == 200 && d.statusText == 'OK') {
                        $scope.$parent.getAddress();
                        growl.success('You have changed your default address.', {});
                    }
                }, function (err) { });
            };

            $scope.deliverHere = function (addressId) {
                AddressService.defaultAddress(userId, addressId);
                $location.path('/order');
            };
        }],        
        link: function ($scope, element, attrs) {            
            $scope.isAction = attrs.action == 'true' ? true : false;
        }
    };
});