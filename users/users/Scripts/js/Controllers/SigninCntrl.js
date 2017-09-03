app.controller('SigninCntrl', ['$scope', '$route', '$location', 'mySharedService', 'SigninService', '$routeParams', 'growl', function ($scope, $route, $location, mySharedService, SigninService, $routeParams, growl) {
    $scope.errMsg = '';
    $scope.isRegFormValid = false;
    $scope.isSignFormValid = false;
    $scope.isforgetFormValid = false;
    mySharedService.prepForBroadcast($location.path());

    $scope.Frm = $routeParams.frm;

    $scope.tabs = [
        { title: 'Sign In', content: 'Pages/Templates/sign-in.html', active: true, disabled: false },
        { title: 'Sign Up', content: 'Pages/Templates/sign-up.html', active: false, disabled: false },
        { title: 'Forget Password', content: 'Pages/Templates/forget-password.html', active: false, disabled: false }
    ];

    $scope.reg = {
        firstName: '',
        lastName: '',
        userName: '',
        email: '',
        password: ''
    };

    /*
    $scope.log = {
        userName: 'admin',
        password: 'test'
    };*/

    $scope.$watch('sign.frmRegister.$valid', function (isValid) {
        $scope.isRegFormValid = isValid;
    });

    $scope.$watch('sign.frmSigin.$valid', function (isValid) {
        $scope.isSignFormValid = isValid;
    });

    $scope.$watch('sign.frmfrgPassword.$valid', function (isValid) {
        $scope.isforgetFormValid = isValid;
    });

    $scope.register = function (reg) {
        if ($scope.isRegFormValid) {
            SigninService.register(reg).then(function (d) {
                if (d.status == 200 && d.statusText == 'OK' && d.data.success)
                    $scope.tabs[0].active = true;
                else
                    $scope.errMsg2 = d.data;

            }, function (err) {

            });
        }
        else {
            $scope.errMsg2 = 'Please fill all fields.'
        }
    };

    $scope.login = function (log) {
        if ($scope.isSignFormValid) {
            var guest_id = localStorage.getItem('GUEST_ID');
            SigninService.login(log, guest_id).then(function (d) {
                if (d.status == 200 && d.statusText == 'OK' && d.data.success) {
                    sessionStorage.USER_ID = d.data.id;
                    sessionStorage.USER_NAME = d.data.userName;
                    mySharedService.LoginBroadcast();
                    if ($scope.Frm != null)
                        $location.path('/' + $scope.Frm);
                    else
                        $location.path('/home');
                    //$route.reload();

                    growl.info('Welcome <b>' + sessionStorage.USER_NAME + '</b>', {});
                }
                else
                    $scope.errMsg = d.data.message;
            }, function (err) {

            });
        }
        else {
            $scope.errMsg = 'Please fill all fields.'
        }
    };

    $scope.forget = function (username) {
        if ($scope.isforgetFormValid) {
            SigninService.forget(username).then(function (d) {
                if (d.status == 200 && d.statusText == 'OK' && d.data.status == 'Success') {
                    $scope.errMsg3 = d.data.msg;
                    growl.success(d.data.msg, {});
                }
                else {
                    $scope.errMsg3 = d.data.msg;
                    growl.warning(d.data.msg, {});
                }
            }, function (err) {
                console.log(JSON.stringify(err));
            });
        }
        else {
            $scope.errMsg3 = 'Please enter username'
        }
    };

}]);