app.controller('addEditAddressCntrl', ['$scope', '$uibModalInstance', 'AddressService', 'addressId', function ($scope, $uibModalInstance, AddressService, addressId) {
    var userId = sessionStorage.USER_ID;
    $scope.ok = function () {
        $uibModalInstance.close();
    };

    $scope.title = addressId == -1 ? 'Add' : 'Edit';
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.getStates = function () {
        AddressService.getStates().then(function (d) {
            $scope.states = d.data;
        }, function (err) { });
    };

    $scope.getStates();

    if (addressId != -1) {
        AddressService.getAddressById(userId, addressId).then(function (d) {
            if (d.status == 200 && d.statusText == 'OK')
                $scope.address = d.data;
        }, function (err) { });
    }

    $scope.isfrmAddValid = false;
    $scope.$watch('frmAdd.$valid', function (isValid) {
        $scope.isfrmAddValid = isValid;
    });

    $scope.add = function (add) {
        if ($scope.isfrmAddValid && addressId == -1) {
            AddressService.addAddress(userId, add).then(function (d) {
                $scope.ok();
            }, function (err) {

            });
        }
        else if ($scope.isfrmAddValid && addressId != -1) {
            AddressService.editAddress(userId, addressId, add).then(function (d) {
                $scope.ok();
            }, function (err) {

            });
        }
    };
}]);