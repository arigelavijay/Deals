angular.module('jkuri.touchspin', [])

.directive('ngTouchSpin', ['$timeout', '$interval', function($timeout, $interval) {
	'use strict';

	var setScopeValues = function (scope, attrs) {
		scope.min = attrs.min || 0;
		scope.max = attrs.max || 100;
		scope.step = attrs.step || 1;
		scope.prefix = attrs.prefix || undefined;
		scope.postfix = attrs.postfix || undefined;
		scope.decimals = attrs.decimals || 0;
		scope.stepInterval = attrs.stepInterval || 100000;
		scope.stepIntervalDelay = attrs.stepIntervalDelay || 100000;
		scope.initval = attrs.initval || '';
		scope.val = attrs.value || scope.initval;
	};

	return {
		restrict: 'EA',
		require: '?ngModel',
		scope: true,
		replace: true,
		link: function (scope, element, attrs, ngModel) {
			setScopeValues(scope, attrs);

			var timeout, timer, helper = true, oldval = scope.val, clickStart;

			ngModel.$setViewValue(scope.val);

			scope.decrement = function () {
				oldval = scope.val;
				var value = parseFloat(parseFloat(Number(scope.val)) - parseFloat(scope.step)).toFixed(scope.decimals);

				if (value < scope.min) {
					value = parseFloat(scope.min).toFixed(scope.decimals);
					scope.val = value;
					ngModel.$setViewValue(value);
					return;
				}

				scope.val = value;
				ngModel.$setViewValue(value);
			};

			scope.increment = function () {
				oldval = scope.val;
				var value = parseFloat(parseFloat(Number(scope.val)) + parseFloat(scope.step)).toFixed(scope.decimals);

				if (parseInt(value) > parseInt(scope.max)) return;

				scope.val = value;
				ngModel.$setViewValue(value);
			};

			scope.startSpinUp = function () {
				scope.checkValue();
				scope.increment();

				clickStart = Date.now();
				scope.stopSpin();

				$timeout(function() {
					timer = $interval(function() {
						scope.increment();
					}, scope.stepInterval);
				}, scope.stepIntervalDelay);
			};

			scope.startSpinDown = function () {
				scope.checkValue();
				scope.decrement();

				clickStart = Date.now();

				var timeout = $timeout(function() {
					timer = $interval(function() {
						scope.decrement();
					}, scope.stepInterval);
				}, scope.stepIntervalDelay);
			};

			scope.stopSpin = function () {
				if (Date.now() - clickStart > scope.stepIntervalDelay) {
					$timeout.cancel(timeout);
					$interval.cancel(timer);
				} else {
					$timeout(function() {
						$timeout.cancel(timeout);
						$interval.cancel(timer);
					}, scope.stepIntervalDelay);
				}
			};

			scope.checkValue = function () {
				var val;

				if (scope.val !== '' && !scope.val.match(/^-?(?:\d+|\d*\.\d+)$/i)) {
					val = oldval !== '' ? parseFloat(oldval).toFixed(scope.decimals) : parseFloat(scope.min).toFixed(scope.decimals);
					scope.val = val;
					ngModel.$setViewValue(val);
				}
			};

		},
		template: 
		'<div class="input-group">' +
		'  <span class="input-group-btn" ng-show="!verticalButtons">' +
		'    <button class="btn btn-default" ng-mousedown="startSpinDown()" ng-mouseup="stopSpin()"><i class="fa fa-minus"></i></button>' +
		'  </span>' +
		'  <span class="input-group-addon" ng-show="prefix" ng-bind="prefix"></span>' +
		'  <input type="text" ng-model="val" ng-disabled="true" class="form-control" ng-blur="checkValue()">' +
		'  <span class="input-group-addon" ng-show="postfix" ng-bind="postfix"></span>' +
		'  <span class="input-group-btn" ng-show="!verticalButtons">' +
		'    <button class="btn btn-default" ng-mousedown="startSpinUp()" ng-mouseup="stopSpin()"><i class="fa fa-plus"></i></button>' +
		'  </span>' +
		'</div>'
	};

}]);