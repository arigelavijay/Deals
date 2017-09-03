app.controller('OrderCntrl', ['$scope', 'CartService', 'OrderService', 'Utility', '$location', 'growl', function ($scope, CartService, OrderService, Utility, $location, growl) {
    var userId = sessionStorage.USER_ID;
    $scope.VendorsUrl = Utility.VendorsUrl;

    var isGuest = false;
    if (typeof (userId) == 'undefined') {
        userId = localStorage.getItem('GUEST_ID');
        isGuest = true;
    }

    $scope.isDataLoading = true;
    $scope.GetCartItems = function () {
        CartService.GetItems(userId, isGuest).then(function (d) {
            if (d.status == 200 && d.statusText == 'OK') {
                $scope.isDataLoading = false;
                $scope.CartVm = d.data.CartItems;                
            }
        }, function (err) {

        });
    };

    $scope.GetCartItems();
    
    $scope.isPromoFormValid = false;
    $scope.$watch('frmPromo.$valid', function (isValid) {
        $scope.isPromoFormValid = isValid;
    });

    $scope.hasPromo = false;
    $scope.PromoMsg = '';
    $scope.makeDisable = false;
    $scope.btnText = 'Apply';
    $scope.applyPromo = function (promoCode) {        
        if ($scope.isPromoFormValid) {
            if ($scope.btnText == 'Apply') {
                OrderService.applyPromo(userId, promoCode).then(function (d) {
                    if (d.status == 200 && d.statusText == 'OK' && d.data.status == 'Success') {
                        $scope.hasPromo = true;
                        $scope.CartVm = d.data.cartData;
                        $scope.promoAmount = d.data.pAmount;
                        $scope.PromoMsg = '';
                        $scope.makeDisable = true;
                        $scope.btnText = 'Remove';

                        growl.success(d.data.msg, {});
                    }
                    else if (d.data.status == 'Failed') {
                        $scope.PromoMsg = d.data.msg;
                        $scope.hasPromo = false;
                        $scope.CartVm = d.data.cartData;

                        growl.warning(d.data.msg, {});
                    }


                }, function () {

                });
            }
            else if ($scope.btnText == 'Remove') {
                $scope.GetCartItems();
                $scope.hasPromo = false;
                $scope.makeDisable = false;
                $scope.promoCode = '';
                $scope.btnText = 'Apply';
            }
        }
    };

    $scope.orderMsg = '';
    $scope.confirm = function () {        
        OrderService.confirm(userId, $scope.promoCode).then(function (d) {
            if (d.status == 200 && d.statusText == 'OK' && d.data.status != 'Failed')
                $location.path('/confirm/' + d.data.orderId);
            else if (d.data.status == 'Failed')
                $scope.orderMsg = d.data.msg;
        }, function (err) {

        });
    };
}]);