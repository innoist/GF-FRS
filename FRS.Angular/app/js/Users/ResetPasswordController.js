(function () {
    angular
        .module('app.pages')
        .controller('ResetPasswordController', ResetPasswordController);

    ResetPasswordController.$inject = ['$http', '$state'];
    function ResetPasswordController($http, $state) {
        var vm = this;

        vm.resetPassword = function () {
            if (vm.resetPasswordForm.$valid) {
                //var url = frsApiUrl + '/api/Account/ForgotPassword';
                //var data = { Email: vm.Email };
                //$http.post(url, data).success(function (response) {
                //    console.log(response);
                //    if (response === true) {
                //        $state.go('account.ResetPassword');
                //    }
                //}).error(function (err) {
                //    console.log(err);
                //    vm.authMsg = err.Message;
                //    toaster.error(showErrors(err));
                //});
            } else {
                vm.resetPasswordForm.email.$dirty = true;
                vm.resetPasswordForm.account_password.$dirty = true;
                vm.resetPasswordForm.account_password_confirm.$dirty = true;
            }
            
        }

    }
})();