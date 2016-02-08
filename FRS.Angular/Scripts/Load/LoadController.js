/**=========================================================
 * Module: Loads.js
 * Controller: LoadController
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.Loads')
        .controller('UIGridController', UIGridController);

    UIGridController.$inject = ['uiGridConstants', '$http'];
    function UIGridController(uiGridConstants, $http) {
        var vm = this;

        activate();

        ////////////////

        function activate() {

            vm.gridOptions = {
                paginationPageSizes: [25, 50, 75],
                paginationPageSize: 25,
                columnDefs: [
                  { name: 'name' },
                  { name: 'gender' },
                  { name: 'company' }
                ]
            };

            $http.get('server/uigrid-100.js')
            .success(function (data) {
                vm.gridOptions1.data = data;
            });

        }
    }
})();