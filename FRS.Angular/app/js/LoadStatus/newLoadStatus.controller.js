//Create MetaData
(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('NewLoadStatusController', NewLoadStatusController);

    NewLoadStatusController.$inject = ['$scope', '$state', '$stateParams', 'LoadStatusService', 'SweetAlert', 'toaster'];

    function NewLoadStatusController($scope, $state, $stateParams, LoadStatusService, SweetAlert, toaster) {
        var vm = this;
        var loadStatusId = 0;

        vm.submitted = false;
        vm.FiscalYear = {};
        vm.validateInput = function (property, type) {
            if (!property || !type) {
                return false;
            }
            return (property.$dirty || vm.submitted) && property.$error[type];
        };
        //#region Post Data
        $scope.save = function (isNew) {
            vm.submitted = true;
            if (vm.formValidate.$valid) {
                console.log('Submitted!!');
            } else {
                toaster.pop("error", "Error", "Fields are required");
                return false;
            }
            vm.FiscalYear.Name = $scope.Name;
            vm.FiscalYear.Value = $scope.Value;
            vm.FiscalYear.StatusId = vm.Status.selected.Value;

            LoadStatusService.save(vm.FiscalYear, onSuccess, onError);
            function onSuccess(response) {
                if (response.data == true) {
                    toaster.pop("success", "Notification", "Load Status Saved successfully");
                }
            }
            function onError(err) {
                toaster.error(err.statusText, err.data.Message);
                showErrors(err);
            }

            if (isNew) {
                //reseting form
                //vm.formValidate.$setPristine();
                $state.go('app.NewLoadStatus');
            }
            if (!isNew) {
                $state.go('app.LoadStatus');
            }
            //reseting form
            vm.formValidate.$setPristine();
            vm.submitted = false;
            loadStatusId = 0;
            $scope.Name = "";
            $scope.Value = "";
        }
        //#endregion
        $scope.clear = function ($event) {
            $event.stopPropagation();
            vm.Status.selected = null;
            vm.formValidate.$dirty = true;
        };
        $scope.cancelBtn = function () {
            if (!vm.formValidate.$dirty) {
                $state.go('app.LoadStatus');
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
                        $state.go('app.LoadStatus');
                    }
                });
            }


        }

        loadStatusId = $stateParams.Id;
        LoadStatusService.loadLoadStatusById(loadStatusId, function (response) {
            if (response) {
                vm.Status = response.Statuses;
                
                if (response.LoadStatus) {
                    $scope.update = true;
                    $scope.Name = response.LoadStatus.Name;
                    $scope.Value = response.LoadStatus.Value;
                    loadStatusId = response.LoadStatus.Value;
                    
                    var selectedStatus = $(vm.Status).filter(function (index, item) {
                        return item.Value === response.LoadStatus.StatusId;
                    });
                    if (selectedStatus.length > 0) {
                        vm.Status.selected = selectedStatus[0];
                    }

                    toaster.success("", "Load Status loaded successfully.");
                }
            } else {
                loadStatusId = 0;
            }

        },
        function (err) {
            toaster.error("", showErrors(err));
        });
    }
})();