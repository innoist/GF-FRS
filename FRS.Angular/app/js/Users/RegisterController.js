/**=========================================================
 * Module: access-register.js
 * Demo for register account api
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.pages')
        .controller('RegisterFormController', RegisterFormController);

    RegisterFormController.$inject = ['$http', '$state', 'toaster'];
    function RegisterFormController($http, $state, toaster) {
        var vm = this;

        activate();

        ////////////////

        function activate() {
            // bind here all data from the form
            vm.register = {};
            // place the message if something goes wrong
            vm.authMsg = '';
            vm.register = function () {
                vm.authMsg = '';

                if (vm.registerForm.$valid) {
                    $http
                      .post(frsApiUrl + '/api/Account/Register', {
                          Email: vm.register.email,
                          Password: vm.register.password,
                          ConfirmPassword: vm.register.account_password_confirm,
                          FirstName: vm.register.FirstName,
                          LastName: vm.register.LastName,
                      })
                      .then(function (response) {
                          // assumes if ok, response is an object with some data, if not, a string with error
                          // customize according to your api
                          if (!response.account) {
                              vm.authMsg = response;
                          } else {
                              $state.go('app.dashboard');
                          }
                      }, function (err) {
                          vm.authMsg = 'Server Request Error';
                          toaster.error("Error", showErrors(err));
                      });
                }
                else {
                    // set as dirty if the user click directly to login so we show the validation messages
                    /*jshint -W106*/
                    vm.registerForm.account_email.$dirty = true;
                    vm.registerForm.account_password.$dirty = true;
                    vm.registerForm.account_agreed.$dirty = true;
                    vm.registerForm.account_FirstName.$dirty = true;
                    vm.registerForm.account_LastName.$dirty = true;
                    vm.registerForm.account_password_confirm.$dirty = true;

                }
            };
        }
    }
})();