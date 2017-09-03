app.directive('otherItems', function () {
    return {
        restrict: 'E',
        replace: true,
        scope: {
            ot: '=data'
        },
        templateUrl: 'Pages/Directives/other-items.html',
        link: function (scope, element, attrs) {            
            scope.VendorsUrl = attrs.url;
        },
        controller: ['$scope', function ($scope) {
            $scope.TimeLeft = function (hours) {
                if (typeof hours == 'undefined')
                    return;

                var seconds = hours * 60 * 60;

                var x = seconds
                var d = moment.duration(x, 'seconds');
                var hours = Math.floor(d.asHours());
                var mins = Math.floor(d.asMinutes()) - hours * 60;

                return pad(hours) + ':' + pad(mins);
            };

            function pad(d) {
                return (d < 10) ? '0' + d.toString() : d.toString();
            }
        }]
    };
});