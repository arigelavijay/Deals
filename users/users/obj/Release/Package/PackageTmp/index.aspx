<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="users.index" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8">
    <title>Scoopon</title>
    <link href="Images/favicon.ico" rel="icon" type="image/x-icon" />
    <link rel="stylesheet" href="Css/new.css">
    <link rel="stylesheet" href="Css/style.css">
    <link rel="stylesheet" href="Css/custom.css">
    <link rel="stylesheet" href="Css/layout.css">
    <link rel="stylesheet" href="Css/font-awesome-4.3.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="Css/bootstrap-3.3.4-dist/css/bootstrap.min.css">
    <link href="Css/angular-growl.css" rel="stylesheet" />
    <link href="Css/autocomplete.css" rel="stylesheet" />
    <!--image slider-->   
    
    <link rel="stylesheet" href="Css/default.css">
    <script src="Scripts/angular.js"></script>
    <!--slider JS-->
    <!--<script src="Scripts/ideal-image-slider.js"></script>-->
    <style type="text/css">
        .submitted .ng-invalid {
            border: 1px solid red;
        }
    </style>
    <style>
        .ord-btn {
            border-radius: 0px;
        }

        .order .cart-header-title {
            float: none;
        }

        .makeHide {
            display: none;
        }
    </style>
    <style>
        .ord-btn {
            border-radius: 0px;
        }

        .order .cart-header-title {
            float: none;
        }

        .order-status .dl-horizontal dd {
            margin-left: auto;
            padding: 8px 0px;
            border-bottom: 1px solid #eee;
        }

        .order-status .dl-horizontal dt {
            width: auto;
            min-width: 100px;
            text-align: left;
            padding: 8px 0px;
        }

        .order-add {
            border-left: 1px dotted #d3cbb8;
            margin: 20px 0;
            min-height: 159px;
            padding: 15px 19px;
            font-size: 13px;
        }

            .order-add b {
                font-size: 18px;
            }

            .order-add address {
                max-width: 320px;
            }

        .pdt-total_amt hr {
            margin: 8px auto;
        }

        .pdt-total_amt .panel-heading {
            text-align: center;
        }

        .pdt-total_amt dl {
            border-left: 1px solid #eee;
            padding-left: 20px;
        }

        html {
            overflow-y: scroll;
        }

        /*body {
            padding: 20px;
        }*/

        .fadein.ng-enter {
            webkit-animation: 0.35s fade-up-enter;
  -moz-animation: 0.35s fade-up-enter;
  -o-animation: 0.35s fade-up-enter;
  animation: 0.35s fade-up-enter;
            transition: 1s;
            opacity: 0;
        }

            .fadein.ng-enter.ng-enter-active {
                opacity: 1;
            }

        .inline-fadein.ng-hide-remove {
            -webkit-transition: 500ms ease all;
            -moz-transition: 500ms ease all;
            -o-transition: 500ms ease all;
            transition: 500ms ease all;
            display: inline-block !important;
            opacity: 0;
        }

            .inline-fadein.ng-hide-remove.ng-hide-remove-active {
                opacity: 1;
            }

        @-webkit-keyframes webkit-spin {
            from {
                -webkit-transform: scale(1) rorate(0deg);
            }

            to {
                -webkit-transform: scale(1) rotate(359deg);
            }
        }

        @keyframes spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }

        .loading-icon {
            -ms-animation: spin .7s infinite linear;
            -moz-animation: spin .7s infinite linear;
            -webkit-animation: webkit-spin .7s infinite linear;
            top: 2px;
            margin-left: 7px;
        }

        .alert {
            margin-top: 15px;
        }
        [ng\:cloak], [ng-cloak], [data-ng-cloak], [x-ng-cloak], .ng-cloak, .x-ng-cloak {
          display: none !important;
        }
        [ng-enter]{
            display:none;
        }
    </style>

</head>
<body class="main-container" ng-app="myApp" id="bodyId">
    <div growl limit-messages="3"></div>
    <div ng-controller="AppCntrl">
        <header ng-include="'Pages/header.html'"></header>
        <div class="ng-cloak"><div ng-view class="fadein"></div></div>
        <div class="clear"></div>
    </div>
    <!--footer-->
    <footer class="footer" ng-include="'Pages/footer.html'"></footer>
    
    <script src="Scripts/ui-bootstrap-tpls-0.14.3.js"></script>
    <script src="Scripts/angular-route.js"></script>
    <script src="Scripts/angular-animate.js"></script>
    <script src="Scripts/moment.js"></script>
    <script src="Scripts/ngTouchSpin.js"></script>
    <script src="Scripts/angular-growl.js"></script>
    <script src="Scripts/app.js"></script>

    <script src="Scripts/js/Controllers/HomeCntrl.js"></script>
    <script src="Scripts/js/Controllers/AppCntrl.js"></script>
    <script src="Scripts/js/Controllers/HeaderCntrl.js"></script>
    <script src="Scripts/js/Controllers/SigninCntrl.js"></script>
    <script src="Scripts/js/Controllers/DealInfoCntrl.js"></script>
    <script src="Scripts/js/Controllers/CartCntrl.js"></script>
    <script src="Scripts/js/Controllers/TrackCntrl.js"></script>
    <script src="Scripts/js/Controllers/ConfirmCntrl.js"></script>
    <script src="Scripts/js/Controllers/OrderInfoCntrl.js"></script>
    <script src="Scripts/js/Controllers/OrderCntrl.js"></script>
    <script src="Scripts/js/Controllers/AddressCntrl.js"></script>
    <script src="Scripts/js/Controllers/addEditAddressCntrl.js"></script>
    <script src="Scripts/js/Controllers/ReviewCntrl.js"></script>

    <script src="Scripts/js/Services/HomeService.js"></script>
    <script src="Scripts/js/Services/SigninService.js"></script>
    <script src="Scripts/js/Services/DealInfoService.js"></script>
    <script src="Scripts/js/Services/CartService.js"></script>
    <script src="Scripts/js/Services/TrackService.js"></script>
    <script src="Scripts/js/Services/OrderInfoService.js"></script>
    <script src="Scripts/js/Services/OrderService.js"></script>
    <script src="Scripts/js/Services/AddressService.js"></script>
    <!-- <script src="Scripts/js/Services/RatingService.js"></script> -->
    <script src="Scripts/js/Services/HistoryService.js"></script>
    <script src="Scripts/js/Services/SearchService.js"></script>
    <script src="Scripts/js/Services/ReviewService.js"></script>

    <script src="Scripts/js/Directives/userAddress.js"></script>
    <script src="Scripts/js/Directives/otherItems.js"></script>
    <script src="Scripts/js/Directives/remainingItems.js"></script>
    <script src="Scripts/js/Directives/spin.js"></script>
</body>
</html>
