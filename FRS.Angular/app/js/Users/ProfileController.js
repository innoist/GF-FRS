

(function() {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('ProfileController', ProfileController);

    ProfileController.$inject = ['$scope','$stateParams', '$state', 'ProfileService', 'toaster'];

    function ProfileController($scope,$stateParams, $state, ProfileService, toaster) {
        var vm = this;
        //ui-select
        
        ProfileService.getBaseData(function (response) {
            vm.Roles = response;
        },
        function (err) {
            toaster.error(showErrors(err));
        });
        
        if ($stateParams.Name !== "") {
            ProfileService.loadProfile($stateParams.Name, function (response) {
                vm.user = response;
                var selectedRole = $(vm.Roles).filter(function (index, item) {
                    return item.Name === response.Role;
                });
                if (selectedRole.length > 0) {
                    vm.Roles.selected = selectedRole[0];
                }
            });
        }
        
        vm.saveProfile = function() {
            if (vm.userForm.$valid) {
                vm.user.RoleId = vm.Roles.selected.Id;
                ProfileService.saveProfile(vm.user, function(response) {
                    if (response) {
                        toaster.success("Profile Saved.");
                        $state.go('app.Users');
                    }

                }, function(err) {
                    toaster.pop("error", "Alert", showErrors(err));
                });
            } else {
            vm.userForm.FirstName.$dirty = true;
            vm.userForm.LastName.$dirty = true;
            vm.userForm.Address.$dirty = true;
            vm.userForm.Phone.$dirty = true;
            vm.userForm.Email.$dirty = true;
            vm.userForm.Role.$dirty = true;
        }
        }


    }
})();