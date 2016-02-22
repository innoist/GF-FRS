/**=========================================================
 * Module: access-login.js
 * Demo for login api
 =========================================================*/

(function () {
    'use strict';

    //angular
    //    .module('app.pages')
    //    .controller('LoginFormController', LoginFormController);

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('LoginFormController', LoginFormController);

    LoginFormController.$inject = ['$http', '$state', '$localStorage'];
    function LoginFormController($http, $state, $localStorage) {

        if ($localStorage['authorizationData']) {
            $state.go('app.dashboard');
        }

        var vm = this;

        activate();

        ////////////////

        function activate() {
            // bind here all data from the form
            vm.account = {};
            // place the message if something goes wrong
            vm.authMsg = '';

            vm.login = function () {
                vm.authMsg = '';

                if (vm.loginForm.$valid) {
                    var data = "grant_type=password&username=" + vm.account.email + "&password=" + vm.account.password + "&userTimeZone=" + getCookie("_timeZoneOffset");
                    vm.submitButton = true;
                    $http
                        .post(window.frsApiUrl + '/token', data, {
                            headers: {
                                'Content-Type': 'application/x-www-form-urlencoded'
                            }
                        })
                        .success(function (response) {
                            $localStorage['authorizationData'] = { token: response.access_token, userName: response.userName, UserRole: response.UserRole };

                            // Add Authorization token to every http request
                            $http.defaults.headers.common = {
                                'Authorization': 'Bearer ' + $localStorage['authorizationData'].token
                            };

                            // assumes if ok, response is an object with some data, if not, a string with error
                            // customize according to your api
                            $state.go('app.dashboard');

                        })
                        .error(function (err, status) {
                            vm.submitButton = false;
                            if (status === 400) {
                                vm.authMsg = 'Incorrect credentials.';
                            }
                            if (status === 500) {
                                vm.authMsg = 'Server Request Error';
                            }
                        });
                }
                else {
                    // set as dirty if the user click directly to login so we show the validation messages
                    /*jshint -W106*/
                    vm.loginForm.account_email.$dirty = true;
                    vm.loginForm.account_password.$dirty = true;
                }
            };

        }
    }
})();