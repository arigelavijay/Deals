app.controller('dealsCntrl', function ($scope, $route, $location, $compile, dealsService, errorService) {
    $scope.isfrmDealValid = false;
    $scope.fileName = null;    
    $scope.uploadedImgData = null;
    $scope.imgHt = -1;
    $scope.imgWt = -1;    
    $scope.fileName = '';
    $scope.isLocationSel = true;
    $scope.ParentfrmDeal = {};
    $scope.isPreview = false;

    $scope.isEditDeal = false;
    $scope.isFreeDeal = false;

    /*
    $scope.minDate = moment().format("MM/DD/YYYY");
    $scope.maxDate = moment().add(3, 'months').format("MM/DD/YYYY");*/

    $scope.minDate = moment();
    $scope.maxDate = moment().add(1, 'months');   
    

    $scope.disabled = function (val) {       
        return false;
    };

    $scope.$on('editDealBroadcast', function (event, args) {        
        dealsService.getDeal(sessionStorage.getItem('USER_ID'), args.dealId).then(function (d) {
            $scope.isLocationSel = true;
            $scope.isEditDeal = true;           
            debugger;
            $scope.deal = {
                name: d.data.name,
                description: d.data.description,
                startsOn: d.data.startsOn,
                endsOn: d.data.endsOn,
                count: d.data.count,
                attachment: d.data.fileName,                
                location: d.data.locationId,
                category: d.data.categoryId,
                unitPrice: d.data.unitPrice,
                discount: d.data.discount,
                sellingPrice: d.data.sellingPrice,
                dealId: d.data.id,
                dataX: '',
                dataY: '',
                dataHeight: '',
                dataWidth: ''
            };

            $scope.imgSize(args.bannerId, args.bannerType);

            
        }, errorService.handleErr);        
    });

    dealsService.getLocationsCategories().then(function (d) {        
        $scope.deal = {
            name: '',
            description: '',
            startsOn: '',
            endsOn: '',
            count: '',
            attachment: '',
            locations: d.data.locations,
            categories: d.data.categories,
            location: '',
            category: '',
            dataX: '',
            dataY: '',
            dataHeight: '',
            dataWidth: '',
            bannerId: '',
            unitPrice: '',
            discount: '',
            sellingPrice: '',
            hasShipping: '',
        };
    });

    $scope.fileChanged = function (ufile) {
        var file = ufile[0];

        $scope.fileName = file.name;
        $scope.isPreview = true;
        
        var url = URL.createObjectURL(file);
        $scope.uploadedImgData = url;
        $scope.$digest();
        if (!flag) {
            imgCropper();
            myCropper();
        }
        else {
            var _files = ufile;
            var _file;

            if (cropper && _files && _files.length) {
                _file = _files[0];

                if (/^image\/\w+/.test(_file.type)) {
                    blobURL = URL.createObjectURL(_file);
                    cropper.reset().replace(blobURL);                    
                }
                else {
                    window.alert('Please choose an image file.');
                }
            }
        }        
    };

    $scope.imgSize = function (bannerId, bannerType) {
        $('#imgfile1').wrap('<form>').closest('form').get(0).reset();
        $('#imgfile1').unwrap();

        $scope.deal.bannerId = bannerId;
        
        if (!$scope.isEditDeal) {
            $scope.deal.attachment = null;
            $scope.deal.location = null;

            $scope.deal.startsOn = null;
            $scope.deal.endsOn = null;
        }

        
        $scope.isLocationSel = true;
        $scope.isPreview = false;
        $('.img-preview').html('');
        flag = false;
        if (cropper && !flag) {
            cropper.destroy();
            cropper = null;
        }

        if (bannerType == '1') {
            angular.element('#uploadedImg1').attr({
                height: 200,
                width: 400
            });

            angular.element('#imgDivId').css({
                height: 200,
                width: 400
            });

            $scope.imgHt = 300;
            $scope.imgWt = 400;

            if ($scope.isEditDeal) {
                $scope.uploadedImgData = 'UploadedFiles/' + sessionStorage.getItem('USER_ID') + '/' + $scope.deal.attachment;
            }
            else
                $scope.uploadedImgData = 'images/admin-images/slider.jpg';
            ratio = 16 / 11;
        }
        else if (bannerType == '2') {
            angular.element('#uploadedImg1').attr({
                height: 150,
                width: 200
            });

            angular.element('#imgDivId').css({
                height: 150,
                width: 200
            });

            $scope.imgHt = 150;
            $scope.imgWt = 200;

            if ($scope.isEditDeal) {
                $scope.uploadedImgData = 'UploadedFiles/' + sessionStorage.getItem('USER_ID') + '/' + $scope.deal.attachment;
            }
            else
                $scope.uploadedImgData = 'images/admin-images/offer-img.jpg';
            
            ratio = 16 / 15;
        }        
    };

    /*
    $scope.$watch(function (scope) {
        return (scope.deal.unitPrice != null && scope.deal.unitPrice != '') && (scope.deal.discount != null && scope.deal.discount != '')
    }, function (val) {
        debugger;
    });
    */    

    $scope.SaveDeal = function (deal) {
        debugger;        
        if (!$scope.isEditDeal) {
            if ($scope.ParentfrmDeal.frmDeal.$valid) {
                debugger;
                var obj = {};

                //obj.attachment = $scope.uploadedImgData.replace(/^[^,]*,/, '');
                obj.count = deal.count;
                obj.dataHeight = deal.dataHeight;
                obj.dataWidth = deal.dataWidth;
                obj.dataX = deal.dataX;
                obj.dataY = deal.dataY;
                obj.description = deal.description;
                obj.endsOn = deal.endsOn;
                obj.fileName = $scope.fileName;
                obj.location = deal.location;
                obj.name = deal.name;
                obj.startsOn = deal.startsOn;
                obj.bannerId = deal.bannerId;
                obj.category = deal.category;
                obj.unitPrice = deal.unitPrice;
                obj.discount = deal.discount;
                obj.sellingPrice = deal.sellingPrice;
                obj.hasShipping = (typeof deal.hasShipping == 'string' ? false : deal.hasShipping);

                var file = document.getElementById('imgfile1').files[0];

                
                dealsService.add(obj, sessionStorage.getItem('USER_ID'), file).then(function (d) {
                    if (d == 'Success')
                        $route.reload();
                }, errorService.handleErr);
                
            }
        }
        else {
            if ($scope.ParentfrmDeal.frmDeal.$valid) {
                var attachment = $scope.deal.attachment;
                var obj;
                var file;
                var reg = new RegExp(/\\/g);
                if (reg.test(attachment)) {
                    obj = {
                        //attachment: $scope.uploadedImgData.replace(/^[^,]*,/, ''),
                        fileName: $scope.fileName,
                        description: deal.description,
                        name: deal.name,
                        dataHeight: deal.dataHeight,
                        dataWidth: deal.dataWidth,
                        dataX: deal.dataX,
                        dataY: deal.dataY
                    };

                    file = document.getElementById('imgfile1').files[0];
                }
                else {
                    obj = {
                        //attachment: '',
                        fileName: deal.attachment,
                        description: deal.description,
                        name: deal.name,
                        dataHeight: '',
                        dataWidth: '',
                        dataX: '',
                        dataY: ''
                    };
                }

                dealsService.updateDeal(
                    sessionStorage.getItem('USER_ID'),
                    $scope.deal.location,
                    $scope.deal.bannerId,
                    $scope.deal.dealId, file, obj).then(function (d) {
                        if (d = 'Success') {
                            $route.reload();
                        }
                    }, errorService.handleErr);
            }
        }
    };

    var options = {};
    var flag = false;
    var cropper;
    var image;
    var $previews;
    var Cropper;

    var uploadedImg;
    var imgPreview;
    var ratio;
    function imgCropper() {
        $previews = $('.img-preview');
        Cropper = window.Cropper;
        options = {
            aspectRatio: 16 / 9,
            preview: '.img-preview',
            build: function (e) {
                var $clone = $(this).clone();

                $clone.css({
                    display: 'block',
                    width: '100%',
                    minWidth: 0,
                    minHeight: 0,
                    maxWidth: 'none',
                    maxHeight: 'none'
                });

                $previews.css({
                    width: '100%',
                    overflow: 'hidden'
                }).html($clone);
            },
            built: function () {
                console.log('built');
            },
            cropstart: function (data) {
                console.log('cropstart', data.action);
            },
            cropmove: function (data) {
                console.log('cropmove', data.action);
            },
            cropend: function (data) {
                console.log('cropend', data.action);
            },
            crop: function (e) {
                angular.element('#dataX').val(Math.round(e.x));
                angular.element('#dataY').val(Math.round(e.Y));
                angular.element('#dataHeight').val(Math.round(e.height));
                angular.element('#dataWidth').val(Math.round(e.width));
                $scope.deal.dataX = Math.round(e.x);
                $scope.deal.dataY = Math.round(e.y);
                $scope.deal.dataHeight = Math.round(e.height);
                $scope.deal.dataWidth = Math.round(e.width);                  
            },
            zoom: function (data) {
                console.log('zoom', data.ratio);
            }
        };
    }

    function myCropper() {
        flag = true;
        image = document.getElementById('uploadedImg1');
        cropper = new Cropper(image, options);
        cropper.setAspectRatio(ratio);
    }

    $scope.locationChanged = function () {
        $scope.isLocationSel = false;      
        var bannerId = $scope.deal.bannerId;

        if (bannerId == 6) {
            $scope.disabledDates = [];
        }
        else {
            dealsService.getAvailDatesForLocation($scope.deal.location, bannerId).then(function (d) {
                if (d.status == 200 && d.statusText == 'OK') {
                    var arr = new Array();
                    for (var i = 0; i < d.data.length; i++) {
                        var item = d.data[i];
                        arr.push(item);
                    }
                    $scope.disabledDates = arr;
                }
            }, errorService.handleErr);
        }  
        
    };

    $scope.addFreeDeal = function (bannerId, bannerType, freeDealFlag) {
        $scope.isFreeDeal = freeDealFlag;
        $scope.minDate = moment().format("MM/DD/YYYY");
        $scope.maxDate = moment().add(3, 'months').format("MM/DD/YYYY");
        $scope.imgSize(bannerId, bannerType);        
    };

    $scope.addPayedDeal = function (bannerId, bannerType, freeDealFlag) {
        $scope.isFreeDeal = freeDealFlag;
        $scope.imgSize(bannerId, bannerType);
    };

    $scope.endsOnDisabled = function (isLocationSel, isFreeDeal) {
        if (isLocationSel)
            return isFreeDeal;        
        else
            return true;
    };
    $scope.disabledDates = [];
    /* -ui-datepicker */
    $scope.disabled = function (date, mode) {
        var dtStr = moment(date).format('MM/D/YYYY');
        var flag = false;
        var arr = $scope.disabledDates;
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] == dtStr) {
                flag = true;
                break;
            }

        }
        return flag;
    };
    $scope.open = function ($event, opened) {
        $scope.status[opened] = true;
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1
    };

    $scope.status = {
        opened: false
    };

    $scope.selectStartDate = function (dt, isFreeDeal) {
        if (isFreeDeal) {
            var strDt = moment(dt).format('MM/DD/YYYY');
            $scope.minDate = moment(dt);
            $scope.maxDate = moment(dt).add(15, 'days');
        }
    };

    $scope.selectEndDate = function (dt, isFreeDeal) {
        if (isFreeDeal) {
            var currentStr = moment().format('MM/DD/YYYY');
            var Dtstr = moment(dt).format('MM/DD/YYYY');

            var cur = moment();
            var sel = moment(dt);

            var diff = sel.diff(cur, 'days');

            var x;
            if (diff > 15)
                x = 15;
            else
                x = diff;

            $scope.minDate = moment(dt).add(-x, 'days');
            $scope.maxDate = moment(dt);
        }
    };

    $scope.clear = function () {
        debugger;
    };
    /* -ui-datepicker */
});



