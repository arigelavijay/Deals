var app = angular.module('dealsApp', ['ngRoute', 'ui.bootstrap', 'ngTable']);

app.config(function ($routeProvider, $httpProvider) {
    $routeProvider
        .when('/', { redirectTo: 'Pages/signin' })
        .when('/Pages/signin', { templateUrl: 'Pages/Views/signin.html' })
        .when('/Pages/register', { templateUrl: 'Pages/Views/register.html' })
        .when('/Pages/dashboard', { templateUrl: 'Pages/Views/dashboard.html' })
        .when('/Pages/deals', { templateUrl: 'Pages/Views/adddeal.html' })
        .when('/Pages/mydeals', { templateUrl: 'Pages/Views/mydeals.html' })
        .otherwise({ template: '<b>could\'nt find route.</b>' })

    $httpProvider.interceptors.push('HttpRequestInterceptor');
});

app.run(function ($rootScope, $location) {
    $rootScope.$on('$routeChangeSuccess', function () {
        var AUTH_TOKEN = sessionStorage.getItem('AUTH_TOKEN');
        var IS_AUTHENTICATE = sessionStorage.getItem('IS_AUTHENTICATE') == 'true' ? true : false;
        if (IS_AUTHENTICATE) {
            if (AUTH_TOKEN != null && AUTH_TOKEN != '')
                return;
            else
                $location.path('/Pages/signin');
        }
        else {
            var path = $location.path();
            if (path == '/Pages/signin' || path == '/Pages/register')
                return;
            else
                $location.path('/Pages/signin');;
        }
    });
});

app.constant('utility', {
    serviceUrl: 'api/'
});

app.factory('HttpRequestInterceptor', ['$rootScope', function ($rootScope) {
    return {
        request: function ($config) {
            var AUTH_TOKEN = sessionStorage.getItem('AUTH_TOKEN');
            var USER_ID = sessionStorage.getItem('USER_ID');
            var IS_AUTHENTICATE = sessionStorage.getItem('IS_AUTHENTICATE') == 'true' ? true : false;
            if (IS_AUTHENTICATE) {
                $config.headers['AUTH_TOKEN'] = AUTH_TOKEN;
                $config.headers['USER_ID'] = USER_ID;
            }
            return $config;
        }
    }
}]);


/*
app.directive('bsDatePicker', function () {
    return {
        restrict: 'E',
        scope: {
            id: '@',
            name: '@',
            'class': '@',
            ngModel: '=',
            maxDate: '=',
            minDate: '=',
            disabledDates: '@'
        },
        require: 'ngModel',
        replace: true,
        template: '<input type="text" class="form-control" />',
        link: function (scope, el, attrs, ngModel) {
            var arrDates = JSON.parse(attrs.disabledDates);
            var dDates = new Array();
            for (var i = 0; i < arrDates.length; i++) {
                var dt = moment(arrDates[i]);
                dDates.push(dt);
            }

            angular.element(el).datetimepicker({
                //useCurrent: true,
                format: 'MM/DD/YYYY',
                icons: {
                    time: "fa fa-clock-o",
                    date: "fa fa-calendar",
                    up: "fa fa-arrow-up",
                    down: "fa fa-arrow-down",
                    previous: 'fa fa-arrow-left',
                    next: 'fa fa-arrow-right',
                    clear: 'fa fa-trash',
                    close: 'fa fa-times'
                },
                minDate: moment(scope.minDate),
                maxDate: moment(scope.maxDate),
                disabledDates: dDates
            }).on('dp.change', function (e) {                
                var date = el.val();
                if (el[0].id == 'startsOn') {
                    $('#endsOn').data("DateTimePicker").minDate(e.date);
                }
                else if (el[0].id == 'endsOn') {
                    $('#startsOn').data("DateTimePicker").maxDate(e.date);
                }
                scope.$apply(function () {
                    ngModel.$setViewValue(date);
                });
            });
            //    .on('dp.change', function (e) {
            //    var date = el.val();
            //    if (el[0].id == 'startsOn') {
            //        $('#endsOn').data("DateTimePicker").minDate(e.date);
            //        $('#endsOn').data("DateTimePicker").maxDate(moment(date).add(15, 'days').format("MM/DD/YYYY"));
            //        var t = scope.maxDate;
            //        debugger;
            //    }
            //    else if (el[0].id == 'endsOn') {
            //        $('#startsOn').data("DateTimePicker").minDate(moment(date).add(-15, 'days').format("MM/DD/YYYY"));
            //        $('#startsOn').data("DateTimePicker").maxDate(e.date);
            //    }
            //    //scope.$apply(function () {
            //    //    ngModel.$setViewValue(date);
            //    //});
            //});
        }
    }
});
*/
app.directive('validFile', function () {
    return {
        require: 'ngModel',
        link: function (scope, el, attrs, ngModel) {
            el.bind('change', function () {
                scope.$apply(function () {
                    ngModel.$setViewValue(el.val());
                    ngModel.$render();
                });
            });
        }
    }
});

Date.prototype.toMSJSON = function () {
    var date = '/Date(' + this.getTime() + ')/'; //CHANGED LINE
    return date;
};

app.directive('bsTimePicker', function () {
    return {
        restrict: 'E',
        scope: {
            id: '@',
            name: '@',
            'class': '@',
            value: '@',
            ngModel: '='
        },
        replace: true,
        template: '<input type="text" class="form-control" />',
        link: function ($scope, el, attrs) {            
            angular.element(el).datetimepicker({
                useCurrent: true,
                format: 'HH:mm:ss',
                icons: {
                    time: "fa fa-clock-o",
                    date: "fa fa-calendar",
                    up: "fa fa-arrow-up",
                    down: "fa fa-arrow-down",
                    previous: 'fa fa-arrow-left',
                    next: 'fa fa-arrow-right',
                    clear: 'fa fa-trash',
                    close: 'fa fa-times'
                }
            });
        }
    }
});

app.directive("akFileModel", ["$parse", function ($parse) {
    return {
        restrict: "A",
        link: function (scope, element, attrs) {
            var model = $parse(attrs.akFileModel);
            var modelSetter = model.assign;
            element.bind("change", function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

app.service('errorService', function ($location) {
    this.handleErr = function (err) {
        if (err.status == 401 && err.statusText == 'Unauthorized') {
            $location.path('/Pages/signin');
        }
    };
});

app.filter('DateFilter', ['$filter', function ($filter) {
    return function (input, format) {
        return $filter('date')(new Date(input), format);
    };
}]);

app.filter('chopper', ['$filter', function ($filter) {
    return function (input, length) {
        var iLength = parseInt(length);
        return input.length > iLength ? (input.substring(0, iLength) + '..') : input;;
    };
}]);
