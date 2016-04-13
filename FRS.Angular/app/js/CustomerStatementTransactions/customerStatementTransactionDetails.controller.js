/**=========================================================
 * Module: load meta data
 * Load Meta Data view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('TransactionDetailControllor', TransactionDetailControllor);

    TransactionDetailControllor.$inject = ['$stateParams', '$scope', 'CustomerStatementTransactionService', 'toaster'];

    // ReSharper disable once InconsistentNaming
    function TransactionDetailControllor($stateParams, $scope, CustomerStatementTransactionService, toaster) {
        //window.OracleEntry = undefined;
        var vm = this;
        var id = $stateParams.Id;
        if (id !== "") {
            CustomerStatementTransactionService.getDetail(id, function (response) {
                vm.Transaction = response;
                toaster.success("Notification", "Oracle Entry Detail Loaded Successfully.");
            });
        } else {
            toaster.error("Notification", "Oracle Entry Detail Loading Failed.");
        }



    }
})();