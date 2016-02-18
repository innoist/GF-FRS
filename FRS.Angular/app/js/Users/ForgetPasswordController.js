(function() {
    angular
        .module('app.pages')
        .controller('ForgotPasswordController', ForgotPasswordController);

    ForgotPasswordController.$inject = ['$http', '$state', 'toaster'];
    function ForgotPasswordController($http, $state, toaster) {
        var vm = this;
        debugger;
        vm.recoverPassword = function () {

            if (vm.recoverForm.$valid) {
                var url = frsApiUrl + '/api/Account/ForgotPassword';
                var data = { Email: vm.Email };
                vm.submitButton = true;
                $http.post(url, data).success(function(response) {
                    console.log(response);
                    if (response === true) {
                        $state.go('account.ResetPassword');
                    }
                }).error(function(err) {
                    vm.submitButton = false;
                    console.log(err);
                    vm.authMsg = showErrors(err);
                    toaster.error("Invalid Request", showErrors(err));
                });
            } else {
                vm.recoverForm.email.$dirty = true;
            }

            
        }

    }
})();