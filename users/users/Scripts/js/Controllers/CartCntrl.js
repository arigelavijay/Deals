app.controller('CartCntrl', ['$scope', 'CartService', 'Utility', '$location', 'growl', function ($scope, CartService, Utility, $location, growl) {
    
    var userId = sessionStorage.USER_ID;
    $scope.VendorsUrl = Utility.VendorsUrl;

    var isGuest = false;
    if (typeof (userId) == 'undefined') {
        userId = localStorage.getItem('GUEST_ID');
        isGuest = true;
    }    

    $scope.hasShipping = false;
    $scope.isDataLoading = true;
    $scope.GetCartItems = function () {
        CartService.GetItems(userId, isGuest).then(function (d) {
            if (d.status == 200 && d.statusText == 'OK') {
                $scope.isDataLoading = false;
                $scope.CartVm = d.data.CartItems;
                $scope.hasShipping = d.data.hasShipping;                
            }
        }, function (err) {

        });
    };
    
    $scope.GetCartItems();    

    $scope.remove = function (dealId) {
        CartService.remove(userId, isGuest, dealId).then(function (d) {
            if (d.status == 200 && d.statusText == 'OK') {
                $scope.GetCartItems();
            }
        }, function (err) {
            debugger;
        });
    };

    $scope.isChanged = false;
    $scope.GrandTotal = function () {        
        if (typeof $scope.CartVm == 'undefined')
            return;
        
        var grandTotal = 0;        
        $scope.CartVm.GrandTotal = $scope.CartVm.SubTotal + $scope.CartVm.Tax;
        grandTotal = $scope.CartVm.GrandTotal;

        if ($scope.isLoading) {
            //debugger;
        }
        return grandTotal;
    };

    $scope.SubTotal = function () {
        if (typeof $scope.CartVm == 'undefined')
            return;

        var subtotal = 0;
        angular.forEach($scope.CartVm.ItemVm, function (item) {
            subtotal += item.quantity * item.sellingPrice
        });
        $scope.CartVm.SubTotal = subtotal;
        return subtotal;
    };

    $scope.updateCart = function () {
        var updateArr = new Array();
        angular.forEach($scope.CartVm.ItemVm, function (item) {
            var Obj = {
                bannerId: item.bannerId,
                dealId: item.dealId,
                locationId: item.locationId,
                quantity: parseInt(item.quantity),
                vendorId: item.vendorId
            };
            updateArr.push(Obj);
        });

        CartService.updateCart(updateArr, userId).then(function (d) {
            $scope.isChanged = false;
        }, function () {
            debugger;
        });
    };

    
    $scope.$watch(function (scope) {
        if (typeof scope.CartVm == 'undefined')
            return;
        return scope.CartVm.GrandTotal;
    }, function (newVal, oldVal) {
        if (typeof newVal == 'undefined' || typeof oldVal == 'undefined')
            return;
        

        if ($scope.CartVm.ItemVm.length > 0) {
            if (newVal != oldVal)
                $scope.isChanged = true;
            else if (newVal == oldVal)
                $scope.isChanged = false;
        }
        else {
            $scope.isChanged = false;
        }
    });

    $scope.checkOut = function () {
        if ($scope.CartVm.ItemVm.length < 1) {
            growl.info('No items in the cart to checkout.', {});
            return;
        }

        var isUserLoggedIn = (sessionStorage.USER_ID != null && sessionStorage.USER_ID != '') ? true : false;
        if (!isUserLoggedIn) {
            $location.path('/sign-up/cart');
        }
        else {            
            if($scope.hasShipping)
                $location.path('/address/cart');
            else 
                $location.path('/order');
        }
    };    
}]);