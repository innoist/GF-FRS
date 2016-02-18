(function() {
    angular
        .module('app.pages')
        .controller('ForgotPasswordController', ForgotPasswordController);

    ForgotPasswordController.$inject = ['$http'];
    function ForgotPasswordController($http) {
        var vm = this;

        vm.recoverPassword = function() {
            var url = frsApiUrl + '/api/Account/ForgotPassword';
            var data = { Email : vm.Email };
            $http.post(url, data).success(function(response) {
                console.log(response);
            }).error(function(err) {
                console.log(err);
            });
        }

    }
})();