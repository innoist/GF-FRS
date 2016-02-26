//Create MetaData
(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('NewCurrencyController', NewCurrencyController);

    NewCurrencyController.$inject = ['$scope', '$state', '$stateParams', 'NewCurrencyService', 'SweetAlert', 'toaster'];

    function NewCurrencyController($scope, $state, $stateParams, NewCurrencyService, SweetAlert, toaster) {
        var vm = this;
        var currencyId = 0;

        vm.submitted = false;
        vm.Currency = {};
        vm.validateInput = function (property, type) {
            if (!property || !type) {
                return false;
            }
            return (property.$dirty || vm.submitted) && property.$error[type];
        };
        //#region Post Data
        $scope.saveCurrency = function (isNew) {
            vm.submitted = true;
            if (vm.formValidate.$valid) {
                console.log('Submitted!!');
            } else {
                toaster.pop("error", "Error", "Fields are required");
                return false;
            }
            vm.Currency.Value = currencyId;
            vm.Currency.Name = $scope.Name;
            vm.Currency.Sign = $scope.Sign;

            NewCurrencyService.saveCurrency(vm.Currency, onSuccess, onError);
            function onSuccess(response) {
                if (response.data == true) {
                    toaster.pop("success", "Notification", "Currency Saved successfully");
                }
            }
            function onError(err) {
                toaster.error(err.statusText, err.data.Message);
                showErrors(err);
            }

            if (isNew) {
                //reseting form
                //vm.formValidate.$setPristine();
                $state.go('app.NewCurrency');
            }
            if (!isNew) {
                $state.go('app.Currency');
            }
            //reseting form
            vm.formValidate.$setPristine();
            vm.submitted = false;
            currencyId = 0;
            $scope.Name = "";
            $scope.Sign = "";
        }
        //#endregion

        $scope.cancelBtn = function () {
            if (!vm.formValidate.$dirty) {
                $state.go('app.Currency');
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
                        $state.go('app.Currency');
                    }
                });
            }


        }

        if ($stateParams.Id === "")
            return;

        currencyId = $stateParams.Id;
        NewCurrencyService.loadCurrencyById(currencyId, function (response) {
            if (response) {
                $scope.update = true;
                $scope.Name = response.Name;
                $scope.Sign = response.Sign;
                currencyId = response.Value;
                toaster.success("", "Currency loaded successfully.");
            } else {
                currencyId = 0;
            }

        },
        function (err) {
            toaster.error("", showErrors(err));
        });
    }
})();