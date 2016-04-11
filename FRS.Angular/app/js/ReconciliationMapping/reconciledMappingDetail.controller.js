/**=========================================================
 * Module: Currency
 * Currency view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('ReconciledMappingDetailController', ReconciledMappingDetailController);

    ReconciledMappingDetailController.$inject = ['$scope', '$state', '$stateParams', 'uiGridConstants', 'ReconciliationMappingService', 'toaster'];

    function ReconciledMappingDetailController($scope, $state, $stateParams, uiGridConstants, ReconciliationMappingService, toaster) {

        if ($stateParams.Id === "") {
            history.back();
        }

        var vm = this;

        //ui-grid
        vm.gridOptions = {
            paginationPageSizes: [10, 25, 50, 100, 500],
            paginationPageSize: 10,
            enableSorting: true,
            multiSelect: false,
            flatEntityAccess: true,
            enableGridMenu: true,
            enableColumnMenus: false,
            columnDefs: [
              { name: 'Id', displayName: 'ID', field: 'MT940CustomerStatementId', sortId: 0 },
              { name: 'Sequence', field: 'Sequence', sortId: 0 },
              { name: 'Reference', field: 'Reference', sortId: 4 },
              { name: 'Type', field: 'TransactionType', sortId: 1 },
                {
                    name: 'Debit/Credit', field: 'DebitOrCredit', sortId: 1,
                    cellTemplate: "<div class='ui-grid-cell-contents'><label title='{{row.entity.DebitOrCredit}}' class='label' ng-class=" + '"' + "{'bg-green-light':row.entity.DebitOrCredit == 'Credit', 'bg-primary-light' : row.entity.DebitOrCredit == 'Debit'}" + '"' + ">{{row.entity.DebitOrCredit}}</label></div>"
                },
              { name: 'Value', field: 'Value', sortId: 1 },
              { name: 'ValueDate', field: 'ValueDate', sortId: 1 },
              { name: 'Amount', field: 'Amount', sortId: 2, cellFilter: 'number' },
              { name: 'Entry', field: 'EntryDate', sortId: 2 }
            ]
        };

        ReconciliationMappingService.getMappingDetail($stateParams.Id, function (response) {
            vm.OracleGlEntry = response.OracleGlEntry;
            vm.gridOptions.data = vm.Transactions = response.Transactions;
            vm.ReconciledMapping = response.ReconciledMapping;

            toaster.success("Success", "Reconciliation set detail loaded successfully.");
        });
    }
})();
