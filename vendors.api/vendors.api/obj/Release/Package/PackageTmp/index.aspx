﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="vendors.api.index" EnableViewState="false" %>



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Deals</title>
    <link href="Css/bootstrap.css" rel="stylesheet" />
    <link href="Css/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="Css/font-awesome-4.2.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="Css/custom.css" rel="stylesheet" />
    <link href="Css/cropper.css" rel="stylesheet" />
    <link href="Css/main.css" rel="stylesheet" />
    <style type="text/css">
        /*html {
            overflow-y: scroll;
        }*/        
        .table > thead > tr > th {
            background-color: #F29E16;
            color:white;
            cursor:pointer;
            font-weight:bold;
        }
        .submitted .ng-invalid {
            border: 1px solid red;
        }

        body {
            padding-top: 20px;
            padding-bottom: 20px;
        }

        .navbar {
            margin-bottom: 20px;
        }

        .ngViewContainer {
            position: absolute;
            width: 100%;
            display: block;
        }

        .ngViewContainer.ng-enter,
        .ngViewContainer.ng-leave {
            -webkit-transition: 100ms linear all;
            transition: 100ms linear all;
        }

        .ngViewContainer.ng-enter {
            transform: translateX(500px);
        }

        .ngViewContainer.ng-enter-active {
            transform: translateX(0px);
        }

        .ngViewContainer.ng-leave {
            transform: translateX(0px);
        }

        .ngViewContainer.ng-leave-active {
            transform: translateX(-1000px);
        }

        .type1 {
            height: 300px;
            width: 400px
        }

        .type2 {
            height: 150px;
            width: 200px
        }

        .panel > .panel-heading {
		    background-image: none;
		    background-color: #f29e16;
		    color: white;
	    }

	    .panel {
		    background-color: #edf0f5;
	    }
        .day.disabled{
	        background-color:#F8F7F7  !important;            
	    }
        .wapper-control {
            position: relative;
        }
        .modal.fade:not(.in) .modal-dialog {
            -webkit-transform: translate3d(25%, 0, 0);
	        transform: translate3d(25%, 0, 0);
        }
        /*.btn[disabled] {
          opacity: 0.90;
          filter: alpha(opacity=90);
          background-color: #690000;
          color: #777;
        }*/
    </style>
</head>
<body ng-app="dealsApp">
    <div class="container" ng-controller="appCntrl">
        <div class="wapper-control">
            <div ng-hide="isSpecificPage()">
                <div ng-include="'Pages/header.html'"></div>
            </div>
            <ng-view class="ngViewContainer"></ng-view>
        </div>
    </div>
    
    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/cropper.js"></script>
    <script src="Scripts/moment.js"></script>
    <script src="Scripts/angular.js"></script>    
    
    <script src="Scripts/ui-bootstrap-tpls-0.14.3.js"></script>
    <script src="Scripts/angular-route.js"></script>   
    <script src="Scripts/ngTable.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>    

    <script src="Scripts/app.js"></script>
    <script src="Scripts/js/Controllers/appCntrl.js"></script>
    <script src="Scripts/js/Controllers/headercntrl.js"></script>
    <script src="Scripts/js/Controllers/logincntrl.js"></script>
    <script src="Scripts/js/Controllers/dashBoardCntrl.js"></script>
    <script src="Scripts/js/Controllers/dealscntrl.js"></script>

    <script src="Scripts/js/Services/loginservice.js"></script>
    <script src="Scripts/js/Services/dealsService.js"></script>
    <script src="Scripts/js/Services/tokenService.js"></script>
    <script src="Scripts/js/Services/akFileUploaderService.js"></script>
    
</body>
</html>