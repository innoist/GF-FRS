/**=========================================================
 * Module: Currency
 * Currency view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('ManualReconciliationController', ManualReconciliationController);

    ManualReconciliationController.$inject = ['$timeout','$rootScope', '$scope', '$state', 'uiGridConstants', 'ReconciliationSerice', 'toaster'];

    function ManualReconciliationController($timeout ,$rootScope, $scope, $state, uiGridConstants, ReconciliationSerice, toaster) {

        var vm = this;
        $scope.toReconcile = true;
        vm.gridOptions = {
            paginationPageSizes: [10, 25, 50, 100, 500],
            paginationPageSize: 10,
            enableSorting: true,
            //enableFiltering: true,
            flatEntityAccess: true,
            //fastWatch: true,
            enableGridMenu: true,
            enableColumnMenus: false,
            //useExternalFiltering: true,
            columnDefs: [
                // name is for display on the table header, field is for mapping as in 
                //sortId is kept locally it is not the property of ui.grid
              { name: 'Id', displayName: 'ID', field: 'MT940CustomerStatementId', sortId: 0 },
              { name: 'Sequence', field: 'Sequence', sortId: 0 },
              { name: 'Reference', field: 'Reference', sortId: 4 },
              { name: 'Type', field: 'TransactionType', sortId: 1 },
              { name: 'Value', field: 'Value', sortId: 1, cellTemplate: '<div class="ui-grid-cell-contents"><span title="{{row.entity.Value}}">{{row.entity.Value}}</span></div>' },
              { name: 'ValueDate', field: 'ValueDate', sortId: 1 },
              { name: 'Amount', field: 'Amount', sortId: 2 },
              { name: 'Entry', field: 'EntryDate', sortId: 2 }
            ],
            onRegisterApi: function(gridApi) {
                vm.gridApi = gridApi;
                gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                   
                    if (row.isSelected === false) {
                        //window.Transactions.pop(row.entity);
                        $rootScope.app.Transactions.pop(row.entity);
                    }
                });
            }
        };

        $rootScope.app.Transactions = []
        vm.gridOptions.data = $rootScope.app.Transactions;
        
        $timeout(function () {
           
            vm.gridOptions.data = $rootScope.app.Transactions;
        
        }, 3000);
        
        $scope.done = function () {
            //if (window.OracleEntry && window.Transactions.length > 0) {
        
            if (window.OracleEntry && $rootScope.app.Transactions.length > 0) {
                vm.OracleEntry = window.OracleEntry;
                //vm.gridOptions.data = window.Transactions;
                //vm.gridOptions.data = $rootScope.app.Transactions;
                toaster.info("Info", "Records are ready to reconcile and can be viewed in grid below. Press Reconcile Button to save changes.");
                $scope.toReconcile = false;
            } else {
                $scope.toReconcile = true;
                toaster.warning("Warning", "Please select records from grid to reconcile.");
            }
        }

        $scope.reconcile = function () {

            var data = {
                OracleGlEntryId: window.OracleEntry.OracleGLEntryId,
                //TransactionIds: window.Transactions.map(function (value) {
                TransactionIds: $rootScope.app.Transactions.map(function (value) {
                    return value.MT940CustomerStatementTransactionId;
                })
            }
            ReconciliationSerice.saveReconciledRecords(data, function(response) {
                if (response) {
                    toaster.success("Success", "Reconcilition was successful.");
                    $state.go("app.ReconciliationMapping");
                }
            });
        }

    }
})();
