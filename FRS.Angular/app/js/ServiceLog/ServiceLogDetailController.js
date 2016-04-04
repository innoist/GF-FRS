/**=========================================================
 * Module: load Log
 * Load Log service
 =========================================================*/


(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('ServiceLogDetailController', ServiceLogDetailController);

    ServiceLogDetailController.$inject = ['$scope', '$state', '$stateParams', 'uiGridConstants', 'ServiceLogService'];

    function ServiceLogDetailController($scope, $state, $stateParams, uiGridConstants, ServiceLogService) {

        var vm = this;
        var id = 0;
        if ($stateParams.Id !== "") {
            id = $stateParams.Id;
            ServiceLogService.getServiceLogData(id, function (response) {
                vm.Log = response;
            });
        }

    }
})();
