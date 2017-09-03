app.controller('loginCntrl', function ($scope, $rootScope, $location, loginService) {
    sessionStorage.setItem('AUTH_TOKEN', '');
    sessionStorage.setItem('IS_AUTHENTICATE', false);
    $scope.error = false;

    $scope.signInVm = {
        username: 'admin',
        password: 'test'
    };

    $scope.regVm = {
        firstName: '',
        lastName: '',
        vendorName: '',
        email: '',
        password: ''
    };

    $scope.isRegFormValid = false;
    $scope.isSignInFormValid = false;
    $scope.message = '';

    $scope.$watch('frmRegister.$valid', function (isValid) {
        $scope.isRegFormValid = isValid;
    });

    $scope.$watch('frmSignIn.$valid', function (isValid) {
        $scope.isSignInFormValid = isValid;
    });

    $scope.signIn = function (signInVm) {
        $scope.message = '';
        if ($scope.isSignInFormValid) {
            $scope.message = 'Please Wait...';
            loginService.SignIn(signInVm).then(function (d) {
                if (d.status == 200 && d.statusText == 'OK') {
                    if (d.data.status) {                        
                        sessionStorage.setItem('USER_ID', d.data.id);
                        sessionStorage.setItem('AUTH_TOKEN', d.data.token);
                        sessionStorage.setItem('IS_AUTHENTICATE', true);
                        
                        $location.path('/Pages/dashboard');
                    }
                    else if (!d.data.status) {
                        $scope.message = d.data;
                    }
                }
            });
        }
        else {
            $scope.message = 'Please fill required fields.';
        }

    }

    $scope.register = function (regVm) {
        $scope.message = '';
        if ($scope.isRegFormValid) {
            $scope.message = 'Please Wait...';
            loginService.Register($scope.regVm).then(function (d) {
                if (d.status == 200 && d.statusText == 'OK') {
                    if (d.data) {
                        $location.path('signin');
                    }
                }
                else if (!d.data) {
                    $scope.message = 'OOPS Some thing happened...!';
                }
            });
        }
        else {
            $scope.message = 'Please fill required fields.';
        }
    }
});