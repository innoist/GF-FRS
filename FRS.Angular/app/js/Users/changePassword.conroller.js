﻿/**=========================================================
 * Module: change password
 * change password Controller
 =========================================================*/

(function () {
	'use strict';

	var core = angular.module('app.core');
	// ReSharper disable FunctionsUsedBeforeDeclared
	core.lazy.controller('changePasswordController', changePasswordController);

	changePasswordController.$inject = ['$scope','changePasswordService', 'toaster'];
	function changePasswordController($scope, changePasswordService, toaster) {
		var vm = this;
		vm.resetPassword = function () {
			if (vm.changePasswordForm.$valid) {
				var data = {
				    OldPassword : $scope.OldPassword,
				    NewPassword: $scope.NewPassword,
				    ConfirmPassword: $scope.ConfirmPassword,
				};
			    debugger;
			    changePasswordService.changePassword(data, function(response) {
			            if (response) {

			                toaster.success("Success", "Password Changed Successfully.");
			                vm.changePasswordForm.$setPristine();
			            }
			        },
			        function(err) {
			        	toaster.error("Error", showErrors(err));
			        	vm.changePasswordForm.$setPristine();
			        });
			} else {
			    debugger;
				vm.changePasswordForm.OldPassword.$dirty = true;
				vm.changePasswordForm.NewPassword.$dirty = true;
				vm.changePasswordForm.ConfirmPassword.$dirty = true;
			}
			
		}


	}


}
)();