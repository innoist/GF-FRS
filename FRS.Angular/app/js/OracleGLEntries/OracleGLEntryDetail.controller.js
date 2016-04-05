/**=========================================================
 * Module: load meta data
 * Load Meta Data view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('OracleGLEntryDetailController', OracleGLEntryDetailController);

    OracleGLEntryDetailController.$inject = ['$stateParams', '$scope', '$state', 'uiGridConstants', 'OracleGlEntryService', 'toaster'];

// ReSharper disable once InconsistentNaming
    function OracleGLEntryDetailController($stateParams, $scope, $state, uiGridConstants, OracleGlEntryService, toaster) {
        //window.OracleEntry = undefined;
        var vm = this;
        var id = $stateParams.Id;
        if (id !== "") {
            OracleGlEntryService.getDetail(id, function(response) {
                vm.OracleGLEntry = response;
                toaster.success("Notification", "Oracle Entry Detail Loaded Successfully.");
            });
        } else {
            toaster.error("Notification", "Oracle Entry Detail Loading Failed.");
        }
        


    }
})();