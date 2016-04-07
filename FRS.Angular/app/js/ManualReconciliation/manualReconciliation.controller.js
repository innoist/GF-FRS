/**=========================================================
 * Module: Currency
 * Currency view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('ManualReconciliationController', ManualReconciliationController);

    ManualReconciliationController.$inject = ['$timeout', '$rootScope', '$scope', '$state', 'uiGridConstants', 'ReconciliationSerice', 'toaster', '$stateParams'];
  
    function ManualReconciliationController($timeout, $rootScope, $scope, $state, uiGridConstants, ReconciliationSerice, toaster, $stateParams) {
        
        var vm = this;
        $scope.toReconcile = false;

        $scope.ReconiliationTransactions = [];
        $scope.CustomerTransactions = [];
        vm.OracleEntry = undefined;

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
                   
                    if (row.isSelected === true) {
                        
                        //$scope.CustomerTransactions.pop(row.entity);
                        vm.gridOptions.data.pop(row.entity);
                        vm.gridTransactionOptions.data.pop(row.entity);
                        //$rootScope.ReconiliationTransactions.push(row.entity);

                    }
                });
            }
        };

        //****** ORACLE GL GRID ******
        vm.today = function () {
            vm.dt = new Date();
        };
        vm.today();

        vm.clear = function () {
            vm.dt = null;
        };

        // Disable weekend selection
        vm.disabled = function (date, mode) {
            return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
        };
        vm.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.opened = true;
        };

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        vm.initDate = new Date();
        vm.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        vm.format = vm.formats[0];


        //ui-select
        vm.disabled = undefined;
        vm.Status = {};
        vm.Statuses = [
          { Id: '1', Name: 'Inactive' },
          { Id: '2', Name: 'Active' },
          { Id: '5', Name: 'Pending' }
        ];


        //ui-grid
        var paginationOptions = {
            'params': {
                SortBy: 0,
                SearchString: '',
                IsAsc: true,
                PageNo: 1,
                PageSize: 10,
                sort: null,
            },

        };
        vm.gridOracleOptions = {
            paginationPageSizes: [10, 25, 50, 100, 500],
            paginationPageSize: 10,
            enableSorting: false,
            //suppressRemoveSort: true,
            multiSelect: false,
            modifierKeysToMultiSelect: false,
            noUnselect: true,
            useExternalPagination: true,
            useExternalSorting: true,
            //enableFiltering: true,
            flatEntityAccess: true,
            //fastWatch: true,
            enableGridMenu: true,
            enableColumnMenus: false,

            //useExternalFiltering: true,
            columnDefs: [
                // name is for display on the table header, field is for mapping as in 
                //sortId is kept locally it is not the property of ui.grid
              {
                  displayName: 'ID',
                  field: 'OracleGLEntryId', sortId: 2,
              },
                {
                    displayName: 'OracleGL Load Id',
                    field: 'OracleGLLoadId', sortId: 2,
                    cellTemplate: '<div class="ui-grid-cell-contents">' +
                    '<a target="_blank" ui-sref="app.OracleGlLoadDetail({Id : row.entity.OracleGLLoadId})">{{row.entity.OracleGLLoadId}}</a>' +
                    '</div>'
                },
              
              { name: 'A/C#', field: 'AccountNumber', sortId: 3 },
              { name: 'Period', field: 'Period', sortId: 3 },
             
              { name: 'Currency', field: 'Currency', sortId: 4 },
                {
                    name: 'Type', field: 'Type', sortId: 4,
                    cellTemplate: "<div class='ui-grid-cell-contents'><label class='label' ng-class=" + '"' + "{'bg-green-light':row.entity.Type == 'Credit', 'bg-primary-light' : row.entity.Type == 'Debit'}" + '"' + ">{{row.entity.Type}}</label></div>"
                },
              { name: 'Amount', field: 'Amount', sortId: 4 },
              { name: 'Created On', field: 'CreatedOn', sortId: 4 }
              //,{ name: 'Modified On', field: 'ModifiedOn', sortId: 4 }
              //,{
              //    name: 'Actions', cellTemplate: '<div class="ui-grid-cell-contents"><div class="btn btn-xs">' +
              //      '<a target="_blank" ui-sref="app.OracleGlEntryDetail({Id : row.entity.OracleGLEntryId})" class="btn btn-xs btn-info"><i class="fa fa-search"></i></a>' +
              //      '</div></div>'
              //}
            ],
            onRegisterApi: function (gridOracleApi) {
                vm.gridOracleApi = gridOracleApi;
                vm.gridOracleApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                    if (sortColumns.length == 0) {
                        paginationOptions.params.sort = null;
                        paginationOptions.params.SortBy = 0;
                    } else {
                        paginationOptions.params.sort = sortColumns[0].sort.direction;
                        var temp = -1;
                        angular.forEach(vm.gridOracleOptions.columnDefs, function (value, key) {
                            if (temp == -1)
                                if (value.field == sortColumns[0].field) {
                                    paginationOptions.params.SortBy = value.sortId;
                                    temp = 0;
                                }
                        });

                    }
                    getPage();
                });
                gridOracleApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                    paginationOptions.params.PageNo = newPage;
                    paginationOptions.params.PageSize = pageSize;
                    getPage();
                });
                
                gridOracleApi.selection.on.rowSelectionChanged($scope, function (row) {
                    //$rootScope.app.OracleEntry = row.entity;
                    vm.OracleEntry = row.entity;
                    //console.log(row.entity);
                    });
            }
        };

        var getPage = function () {
            switch (paginationOptions.params.sort) {
                case uiGridConstants.ASC:
                    paginationOptions.params.IsAsc = true;
                    break;
                case uiGridConstants.DESC:
                    paginationOptions.params.IsAsc = false;
                    break;
                default:
                    //url = '/data/100.json';
                    break;
            }

            ReconciliationSerice.getOracleGridData(function (data) {
                vm.gridOracleOptions.totalItems = data.TotalCount;
                vm.gridOracleOptions.data = data.OracleGlEntries;
            }, null, paginationOptions);


        };
        getPage();//calling oracle gl page
        //******* END ORACLE GL GRID ******


        //************* Bank MT940 Transaction ***********************//

        var paginationTransactionOptions = {
            'params': {
                SortBy: 0,
                SearchString: '',
                IsAsc: true,
                PageNo: 1,
                PageSize: 10,
                sort: null,
                DebitOrCredit: null,
                MT940CustomerStatementId: $stateParams.Id == null || $stateParams.Id == "" ? 0 : $stateParams.Id
            },

        };

        $scope.specificDetail = $stateParams.Id == null || $stateParams.Id == "" ? false : true;
        $scope.MT940CustomerStatementId = $stateParams.Id;
        $scope.MT940LoadId = $stateParams.MT940LoadId;
        vm.gridTransactionOptions = {
            paginationPageSizes: [10, 25, 50, 100, 500],
            paginationPageSize: 10,
            enableSorting: false,
            //modifierKeysToMultiSelect: true,
            //enableFullRowSelection: true,
            //enableRowHeaderSelection: true,
            //suppressRemoveSort: true,
            useExternalPagination: true,
            useExternalSorting: true,
            //enableFiltering: true,
            flatEntityAccess: true,
            //fastWatch: true,
            enableGridMenu: true,
            enableColumnMenus: false,
            //useExternalFiltering: true,
            columnDefs: [
                // name is for display on the table header, field is for mapping as in 
                //sortId is kept locally it is not the property of ui.grid
              { displayName: 'ID', field: 'MT940CustomerStatementTransactionId', sortId: 0 },
              {
                  displayName: 'Statement ID', field: 'MT940CustomerStatementId', sortId: 0,
                  cellTemplate: '<div class="ui-grid-cell-contents"><a ui-sref="app.CustomerStatementsDetail({MT940CustomerStatementId : row.entity.MT940CustomerStatementId})">{{row.entity.MT940CustomerStatementId}}</span></div>'
              },
              
              { name: 'Reference', field: 'Reference', sortId: 4 },
              { name: 'Type', field: 'TransactionType', sortId: 1 },
              { name: 'Value', field: 'Value', sortId: 1, cellTemplate: '<div class="ui-grid-cell-contents"><span title="{{row.entity.Value}}">{{row.entity.Value}}</span></div>' },
              { name: 'ValueDate', field: 'ValueDate', sortId: 1 },
              { name: 'Amount', field: 'Amount', sortId: 2 },
              { name: 'Entry', field: 'EntryDate', sortId: 2 }
            ],
            onRegisterApi: function (gridTransactionApi) {
                vm.gridTransactionApi = gridTransactionApi;
                gridTransactionApi.core.notifyDataChange(uiGridConstants.dataChange.OPTIONS);
                vm.gridTransactionApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                    if (sortColumns.length == 0) {
                        paginationTransactionOptions.params.sort = null;
                        paginationTransactionOptions.params.SortBy = 0;
                    } else {
                        paginationTransactionOptions.params.sort = sortColumns[0].sort.direction;
                        var temp = -1;
                        angular.forEach(vm.gridTransactionOptions.columnDefs, function (value, key) {
                            if (temp == -1)
                                if (value.field == sortColumns[0].field) {
                                    paginationTransactionOptions.params.SortBy = value.sortId;
                                    temp = 0;
                                }
                        });
                    }
                    getTransactionPage();
                });
                gridTransactionApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                    paginationTransactionOptions.params.PageNo = newPage;
                    paginationTransactionOptions.params.PageSize = pageSize;
                    getTransactionPage();
                });
                gridTransactionApi.selection.on.rowSelectionChanged($scope, function (row) {
                    if (row.isSelected === true) {
                        // window.Transactions.push(row.entity);
                      //  $scope.CustomerTransactions.push(row.entity);
                        vm.gridOptions.data.push(row.entity);
                        var index = vm.gridTransactionOptions.data.indexOf(row.entity);
                        vm.gridTransactionOptions.data.splice(index, 1);
                        //vm.gridTransactionOptions.data.pop(row.entity);
                    } else if (row.isSelected === false) {
                        //window.Transactions.pop(row.entity);
                        // $scope.CustomerTransactions.pop(row.entity);
                        vm.gridOptions.data.pop(row.entity);
                    }
                    console.log(row.entity);

                });

                gridTransactionApi.selection.on.rowSelectionChangedBatch($scope, function (rows) {

                    angular.forEach(rows, function (key, value) {

                    });
                    var msg = 'rows changed ' + rows.length;
                    console.log(msg);
                });
            }
        };
        var getTransactionPage = function () {
            switch (paginationTransactionOptions.params.sort) {
                case uiGridConstants.ASC:
                    paginationTransactionOptions.params.IsAsc = true;
                    break;
                case uiGridConstants.DESC:
                    paginationTransactionOptions.params.IsAsc = false;
                    break;
                default:
                    //url = '/data/100.json';
                    break;
            }

            ReconciliationSerice.getCustomerStatementGridData(
                function onSuccess(data) {
                    vm.gridTransactionOptions.totalItems = data.TotalCount;
                    //$rootScope.app.Transactions = data.Data;
                    //$scope.ReconiliationTransactions = data.Data;
                    vm.gridTransactionOptions.data = data.Data;
                }, null, paginationTransactionOptions);


        };
        getTransactionPage();//calling mt 940 transaction page
        //************* End Bank MT940 Transaction ***********************//


     

        $scope.$on('$destroy', function () {
            $('#tlyPageGuideWrapper').hide();

        });
        angular.element(document).ready(function () {
          
            $('#tlyPageGuideWrapper').show();
            
        });
        
        //vm.gridOracleOptions.data = $rootScope.app.CustomerTransactions;
        
        //$timeout(function () {
        //    vm.gridOracleOptions.data = $rootScope.app.CustomerTransactions;
        //    tl.pg.init();
        //}, 3000);
        //vm.OracleEntry = $rootScope.app.OracleEntry;
        $scope.reconcile = function () {
            //if ($rootScope.app.OracleEntry && $rootScope.app.CustomerTransactions.length > 0) {
            if (vm.OracleEntry) {
                var data = {
                    OracleGlEntryId: vm.OracleEntry.OracleGLEntryId,
                    
                    TransactionIds: vm.gridOptions.data.map(function (value) {
                        return value.MT940CustomerStatementTransactionId;
                    })
                }
                ReconciliationSerice.saveReconciledRecords(data, function (response) {
                    if (response) {
                        toaster.success("Success", "Reconcilition was successful.");
                        $state.go("app.ReconciliationMapping");
                    }
                });
                //toaster.info("Info", "Records are ready to reconcile and can be viewed in grid below. Press Reconcile Button to save changes.");
                //$scope.toReconcile = false;
            } else {
                //$scope.toReconcile = true;
                toaster.warning("Warning", "Please select records from grid to reconcile.");
            }
        }

    }

})();
