

(function() {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('ProfileController', ProfileController);

    ProfileController.$inject = ['$scope', '$stateParams', '$state', '$localStorage', 'ProfileService', 'toaster'];

    function ProfileController($scope, $stateParams, $state, $localStorage, ProfileService, toaster) {
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
                if (!response)
                    return;
                vm.user = response;
                $scope.update = true;
                if ($localStorage['authorizationData'].UserRole !== "SystemAdministrator")
                    vm.disabled = true;

                var selectedRole = $(vm.Roles).filter(function (index, item) {
                    return item.Name === response.Role;
                });
                if (selectedRole.length > 0) {
                    vm.Roles.selected = selectedRole[0];
                }
            });
        }
        vm.validateInput = function (property, type) {
            if (!property || !type) {
                return false;
            }
            return (property.$dirty || vm.submitted) && property.$error[type];
        };
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