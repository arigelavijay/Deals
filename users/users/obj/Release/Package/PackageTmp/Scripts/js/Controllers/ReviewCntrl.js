app.controller('ReviewCntrl', ['$scope', '$uibModalInstance', 'dealId', 'userId', 'ReviewService', function ($scope, $uibModalInstance, dealId, userId, ReviewService) {
    /* Uib Model and Uib Rating Code Start */
    $scope.max = 5;
    $scope.isReadonly = false;
    $scope.ok = function (rating) {
        $uibModalInstance.close(rating);
    };
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
    $scope.hoveringOver = function (value) {
        $scope.overStar = value;
        $scope.percent = 100 * (value / $scope.max);
    };
    /* Uib Model and Uib Rating Code End */

    $scope.isfrmReviewValid = false;
    $scope.$watch('frmReview.$valid', function (isValid) {
        $scope.isfrmReviewValid = isValid;
    });

    $scope.review = function (r) {
        if ($scope.isfrmReviewValid) {
            r.userId = userId;
            r.dealId = dealId;
            ReviewService.Insert(r).then(function (d) {
                $scope.ok(d.data.rating);
            }, function () { });
        }
    };
}]);