var app = angular.module('myApp', ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'jkuri.touchspin', 'angular-growl']);

app.config(function ($routeProvider, $httpProvider, growlProvider) {
    $routeProvider
        .when('/', { templateUrl: 'Pages/Views/deals.html' })
        .when('/location', { templateUrl: 'Pages/Views/location.html' })
        .when('/home', { templateUrl: 'Pages/Views/deals.html' })
        .when('/home/:id', { templateUrl: 'Pages/Views/deals.html' })
        .when('/sign-up/:frm', { templateUrl: 'Pages/Views/sign-up.html' })
        .when('/sign-up', { templateUrl: 'Pages/Views/sign-up.html' })
        .when('/deal-info/:id', { templateUrl: 'Pages/Views/deal-info.html' })
        .when('/cart', { templateUrl: 'Pages/Views/cart.html' })
        .when('/contact', { templateUrl: 'Pages/Views/contact.html' })
        .when('/track', { templateUrl: 'Pages/Views/track.html' })
        .when('/confirm/:id', { templateUrl: 'Pages/Views/confirm.html' })
        .when('/order-info/:id', { templateUrl: 'Pages/Views/order-info.html' })
        .when('/order', { templateUrl: 'Pages/Views/order.html' })
        .when('/address', { templateUrl: 'Pages/Views/address.html' })
        .when('/address/:frm', { templateUrl: 'Pages/Views/address.html' })        
        .otherwise({ templateUrl: 'Pages/Views/404.html' })

    growlProvider.onlyUniqueMessages(false);
    growlProvider.globalTimeToLive(5000);
    growlProvider.globalDisableIcons(false);
    growlProvider.globalDisableCountDown(true);
});

app.constant('Utility', {
    ServiceUrl: 'http://192.168.0.224/users/api',
    VendorsUrl: 'http://192.168.0.224/vendors'
});

/*
app.constant('Utility', {
    ServiceUrl: '/api',
    VendorsUrl: 'http://vendors.rxshopy.com'
});
*/

app.run(function ($rootScope, $location, $controller) {
    $rootScope.$on('$routeChangeSuccess', function () {

    });
});

app.filter('DateFilter', ['$filter', function ($filter) {
    return function (input, format) {
        return $filter('date')(new Date(input), format);
    };
}]);

app.factory('mySharedService', function ($rootScope) {
    var sharedService = {};
    sharedService.path = '';

    sharedService.prepForBroadcast = function (path) {
        this.path = path;
        this.broadcastItem(path);
    };

    sharedService.broadcastItem = function (path) {
        $rootScope.$broadcast('handleBroadcast');
    };

    sharedService.LoginBroadcast = function () {
        this.broadcastLogin();
    };

    sharedService.broadcastLogin = function () {
        $rootScope.$broadcast('USERLOGGEDIN');
    };

    sharedService.LogOutBroadcast = function () {
        this.broadcastLogOut();
    };

    sharedService.broadcastLogOut = function () {
        $rootScope.$broadcast('USERLOGGEDOUT');
    };

    return sharedService;
});

app.directive('ngUnique', ['$http', 'SigninService', function ($http, SigninService) {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, cntrl) {
            elem.on('blur', function (evt) {
                scope.$apply(function () {
                    var val = elem.val();
                    SigninService.checkUserName(val).then(function (d) {
                        cntrl.$setValidity('unique', d.data.isAvail);
                        scope.errMsg = '';
                        if (!d.data.isAvail)
                            scope.errMsg = d.data.message;

                    }, function (err) {

                    });
                });
            });
        }
    };
}]);

app.directive('ngUniqueemail', ['$http', 'SigninService', function ($http, SigninService) {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, cntrl) {
            elem.on('blur', function (evt) {
                scope.$apply(function () {
                    var val = elem.val();
                    SigninService.checkEmail(val).then(function (d) {
                        cntrl.$setValidity('uniqueemail', d.data.isAvail);
                        scope.errMsg = '';
                        if (!d.data.isAvail)
                            scope.errMsg = d.data.message;

                    }, function (err) {
                    });
                });
            });
        }
    };
}]);

app.directive('rxShopyLoader', function () {
    return {
        restrict: 'E',
        replace: true,
        scope: {
            isDataLoading: '='
        },
        templateUrl: 'Pages/Templates/rx-shopy-loader.html',
        link: function (scope, elem, attrs) {
            
        }
    };
});

/*
app.directive('routeLoadingIndicator', ['$rootScope', function ($rootScope) {
    return {
        restrict: 'E',
        template: '<div class="col-lg-12" ng-if="isRouteLoading"><h1>Loading <i class="fa fa-cog fa-spin"></i></h1></div>',
        link: function (scope, elem, attrs) {
            scope.isRouteLoading = false;

            $rootScope.$on('$routeChangeStart', function () {
                scope.isRouteLoading = true;
            });

            $rootScope.$on('$routeChangeSuccess', function () {
                scope.isRouteLoading = false;
            });
        }
    };
}]);*/

