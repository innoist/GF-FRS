//Create MetaData
(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('CreateLoadTypeController', CreateLoadTypeController);

    CreateLoadTypeController.$inject = ['$scope', '$state', '$stateParams', 'CreateLoadTypeService', 'SweetAlert', 'toaster'];

    function CreateLoadTypeController($scope, $state, $stateParams, CreateLoadTypeService, SweetAlert, toaster) {
        var vm = this;
        var loadTypeId = 0;

        vm.submitted = false;
        vm.LoadType = {};
        $scope.clear = function ($event) {
            $event.stopPropagation();
            vm.Status.selected = null;
            vm.formValidate.$dirty = true;
        };
        vm.validateInput = function (property, type) {
            if (!property || !type) {
                return false;
            }
            return (property.$dirty || vm.submitted) && property.$error[type];
        };
        //#region Post Data
        $scope.saveLoadType = function (isNew) {
            vm.submitted = true;
            if (vm.formValidate.$valid) {
                console.log('Submitted!!');
            } else {
                toaster.pop("error", "Error", "Fields are required");
                return false;
            }
            vm.LoadType.Value = loadTypeId;
            vm.LoadType.Name = $scope.Name;
            vm.LoadType.StatusId = vm.Status.selected.Value;

            CreateLoadTypeService.saveLoadType(vm.LoadType, onSuccess, onError);
            function onSuccess(response) {
                if (response.data == true) {
                    toaster.pop("success", "Notification", "Load Type Saved successfully");
                    if (isNew) {
                        //reseting form
                        //vm.formValidate.$setPristine();
                        $state.go('app.CreateLoadType');
                    }
                    if (!isNew) {
                        $state.go('app.LoadType');
                    }
                }
            }
            function onError(err) {
                toaster.error(err.statusText, err.data.Message);
                showErrors(err);
            }

            
            //reseting form
            vm.formValidate.$setPristine();
            vm.submitted = false;
            loadTypeId = 0;
            $scope.Name = "";
            vm.Status.selected = null;
        }
        //#endregion

        $scope.cancelBtn = function () {
            if (!vm.formValidate.$dirty) {
                $state.go('app.LoadType');
            } else {
                SweetAlert.swal({
                    title: 'Are you sure?',
                    text: 'All data you entered in form will be lost!',
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#DD6B55',
                    confirmButtonText: 'Yes, cancel saving form.!',
                    cancelButtonText: 'No, stay on this page!',
                    closeOnConfirm: true,
                    closeOnCancel: true,
                }, function (isConfirm) {
                    if (isConfirm) {
                        $state.go('app.LoadType');
                    }
                });
            }


        }

        loadTypeId = $stateParams.Id;
        CreateLoadTypeService.loadLoadTypeById(loadTypeId, function (response) {
            vm.Status = response.Statuses;

            if (response.LoadType) {
                $scope.update = true;
                $scope.Name = response.LoadType.Name;
                loadTypeId = response.LoadType.Value;

                var selectedStatus = $(vm.Status).filter(function (index, item) {
                    return item.Value === response.LoadType.StatusId;
                });
                if (selectedStatus.length > 0) {
                    vm.Status.selected = selectedStatus[0];
                }
                toaster.success("", "Load Type loaded successfully.");
            } else {
                loadTypeId = 0;
            }

        },
        function (err) {
            toaster.error("", showErrors(err));
        });
    }
})();