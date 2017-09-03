app.controller('homeCntrl', ['$scope', 'HomeService', 'Utility', '$location', 'mySharedService', function ($scope, HomeService, Utility, $location, mySharedService) {
    
    if (localStorage.GUEST_ID == null && sessionStorage.USER_ID == null) {        
        HomeService.getGuestId().then(function (d) {
            console.log(d.status);
            console.log(d.statusText);
            if (d.status == 200 && d.statusText == 'OK') {
                localStorage.setItem('GUEST_ID', d.data.guest_id);
            }
        }, function (err) {

        });
    }
    else if (sessionStorage.USER_ID != null) {
        localStorage.removeItem('GUEST_ID');
    }

    /* to retain state when page refresh and back navigations when user not logged in */
    var isUserLoggedIn = (sessionStorage.USER_ID != null && sessionStorage.USER_ID != '') ? true : false;
    if(!isUserLoggedIn)
        mySharedService.prepForBroadcast($location.path());

    $scope.isDataLoading = true;
    HomeService.getDeals(7).then(function (d) {        
        $scope.VendorsUrl = Utility.VendorsUrl;        
        if (d.status == 200 && d.statusText == 'OK') {           
            $scope.isDataLoading = false;
            if (d.data.main == null) {
                var obj = {
                    bannerId: 1,
                    dealId: -1,
                    image: 'offer-img.jpg',
                    name: '',
                    remaining: 0,
                    vendorId: -1
                };
                
                d.data.main = obj;
            }
            
            if (d.data.other.length < 4) {
                var OtherArray = [2, 3, 4, 5];
                for (var i = 0; i < d.data.other.length; i++) {
                    for (var j = 0; j < OtherArray.length; j++) {
                        if (d.data.other[i].bannerId == OtherArray[j]) {
                            OtherArray.splice(j, 1);
                            break;
                        }
                    }                    
                }
                for (var i = 0; i < OtherArray.length; i++) {
                    var obj = {
                        bannerId: OtherArray[i],
                        dealId: -1,
                        image: 'slider.jpg',
                        name: '',
                        remaining: 0,
                        vendorId: -1
                    };

                    d.data.other.push(obj);
                }
            }
            $scope.main = d.data.main;
            $scope.other = d.data.other;
            $scope.remaining = d.data.remaining;
            
        }
    }, function (err) {

    });

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

    // $scope.ratingStates = [
   
    //{ stateOn: 'fa fa-star', stateOff: 'fa fa-star-o' }
   
    //];
}]);