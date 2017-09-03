app.directive('remainingItems', function () {
    return {
        restrict: 'E',
        replace: true,
        scope: {
            r: '=data'
        },
        templateUrl: 'Pages/Directives/remaining-items.html',
        link: function (scope, element, attrs) {
            scope.VendorsUrl = attrs.url;
        }
    };
});