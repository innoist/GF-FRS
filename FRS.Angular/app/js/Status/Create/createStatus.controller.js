//Create MetaData
(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('CreateStatusController', CreateStatusController);

    CreateStatusController.$inject = ['$scope', '$state', '$stateParams', 'CreateStatusService', 'SweetAlert', 'toaster'];

    function CreateStatusController($scope, $state, $stateParams, CreateStatusService, SweetAlert, toaster) {
        var vm = this;
        var statusId = 0;

        vm.submitted = false;
        vm.Status = {};
        vm.validateInput = function (property, type) {
            if (!property || !type) {
                return false;
            }
            return (property.$dirty || vm.submitted) && property.$error[type];
        };
        //#region Post Data
        $scope.saveStatus = function (isNew) {
            vm.submitted = true;
            if (vm.formValidate.$valid) {
                console.log('Submitted!!');
            } else {
                toaster.pop("error", "Error", "Fields are required");
                return false;
            }
            vm.Status.Value = statusId;
            vm.Status.Name = $scope.Name;

            CreateStatusService.saveStatus(vm.Status, onSuccess, onError);
            function onSuccess(response) {
                if (response.data == true) {
                    toaster.pop("success", "Notification", "Currency Saved successfully");
                    if (isNew) {
                        //reseting form
                        //vm.formValidate.$setPristine();
                        $state.go('app.CreateStatus');
                    }
                    if (!isNew) {
                        $state.go('app.Status');
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
            statusId = 0;
            $scope.Name = "";
        }
        //#endregion

        $scope.cancelBtn = function () {
            if (!vm.formValidate.$dirty) {
                $state.go('app.Status');
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
                        $state.go('app.Status');
                    }
                });
            }


        }

        if ($stateParams.Id === "")
            return;

        statusId = $stateParams.Id;
        CreateStatusService.loadStatusById(statusId, function (response) {
            if (response) {
                $scope.update = true;
                $scope.Name = response.Name;
                statusId = response.Value;
                toaster.success("", "Status loaded successfully.");
            } else {
                statusId = 0;
            }

        },
        function (err) {
            toaster.error("", showErrors(err));
        });
    }
})();