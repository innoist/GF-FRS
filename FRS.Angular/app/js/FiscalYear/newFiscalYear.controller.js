//Create MetaData
(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('NewFiscalYearController', NewFiscalYearController);

    NewFiscalYearController.$inject = ['$scope', '$state', '$stateParams', 'FiscalYearService', 'SweetAlert', 'toaster'];

    function NewFiscalYearController($scope, $state, $stateParams, FiscalYearService, SweetAlert, toaster) {
        var vm = this;
        var fiscalYearId = 0;

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

            FiscalYearService.save(vm.FiscalYear, onSuccess, onError);
            function onSuccess(response) {
                if (response.data == true) {
                    toaster.pop("success", "Notification", "Fiscal Year Saved successfully");
                    if (isNew) {
                        //reseting form
                        //vm.formValidate.$setPristine();
                        $state.go('app.NewFiscalYear');
                    }
                    if (!isNew) {
                        $state.go('app.FiscalYear');
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
            fiscalYearId = 0;
            $scope.Name = "";
            $scope.Value = "";
        }
        //#endregion

        $scope.cancelBtn = function () {
            if (!vm.formValidate.$dirty) {
                $state.go('app.FiscalYear');
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
                        $state.go('app.FiscalYear');
                    }
                });
            }


        }

        if ($stateParams.Id === "")
            return;

        fiscalYearId = $stateParams.Id;
        FiscalYearService.loadYearById(fiscalYearId, function (response) {
            if (response) {
                $scope.update = true;
                $scope.Name = response.Name;
                $scope.Value = response.Value;
                fiscalYearId = response.Value;
                toaster.success("", "Fiscal Year loaded successfully.");
            } else {
                fiscalYearId = 0;
            }

        },
        function (err) {
            toaster.error("", showErrors(err));
        });
    }
})();