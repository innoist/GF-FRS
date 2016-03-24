/**=========================================================
 * Module: load meta data
 * Load Meta Data view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('CustomerStatementTransactionsController', CustomerStatementTransactionsController);

    CustomerStatementTransactionsController.$inject = ['$rootScope', '$scope', '$state', '$stateParams', 'uiGridConstants', 'CustomerStatementTransactionService'];

    function CustomerStatementTransactionsController($rootScope, $scope, $state, $stateParams, uiGridConstants, CustomerStatementTransactionService) {
        //window.Transactions = [];
        
        $rootScope.app.Transactions = [];
        $rootScope.app.CustomerTransactions = [];
        var vm = this;

        //datepicker
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
        vm.DebitOrCredit = {};
        vm.DebitsCredits = [
          { Id: 'D', Name: 'Debit'},
          { Id: 'C', Name: 'Credit'}
        ];

        //$http.get(window.frsApiUrl + '/api/LoadMetaDataBase').success(function (response) {
        //    vm.Statuses = response.Statuses;
        //});



        //ui-grid
        var paginationOptions = {
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
        vm.gridOptions = {
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
              { name: 'Id', displayName: 'ID', field: 'MT940CustomerStatementId', sortId: 0 },
              { name: 'Sequence', field: 'Sequence', sortId: 0 },
              { name: 'Reference', field: 'Reference', sortId: 4 },
              { name: 'Type', field: 'TransactionType', sortId: 1 },
              { name: 'Value', field: 'Value', sortId: 1, cellTemplate: '<div class="ui-grid-cell-contents"><span title="{{row.entity.Value}}">{{row.entity.Value}}</span></div>' },
              { name: 'ValueDate', field: 'ValueDate', sortId: 1 },
              { name: 'Amount', field: 'Amount', sortId: 2 },
              { name: 'Entry', field: 'EntryDate', sortId: 2 }
            ],
            onRegisterApi: function (gridApi) {
                vm.gridApi = gridApi;
                gridApi.core.notifyDataChange(uiGridConstants.dataChange.OPTIONS);
                vm.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                    if (sortColumns.length == 0) {
                        paginationOptions.params.sort = null;
                        paginationOptions.params.SortBy = 0;
                    } else {
                        paginationOptions.params.sort = sortColumns[0].sort.direction;
                        var temp = -1;
                        angular.forEach(vm.gridOptions.columnDefs, function (value, key) {
                            if (temp == -1)
                                if (value.field == sortColumns[0].field) {
                                    paginationOptions.params.SortBy = value.sortId;
                                    temp = 0;
                                }
                        });
                    }
                    getPage();
                });
                gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                    paginationOptions.params.PageNo = newPage;
                    paginationOptions.params.PageSize = pageSize;
                    getPage();
                });
                gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                    if (row.isSelected === true) {
                        // window.Transactions.push(row.entity);
                        $rootScope.app.CustomerTransactions.push(row.entity);
                        
                        var index=vm.gridOptions.data.indexOf(row.entity);
                        vm.gridOptions.data.splice(index, 1);
                        //vm.gridOptions.data.pop(row.entity);
                    }else if (row.isSelected === false) {
                        //window.Transactions.pop(row.entity);
                        $rootScope.app.CustomerTransactions.pop(row.entity);
                    }
                    console.log(row.entity);
                 
                });

                gridApi.selection.on.rowSelectionChangedBatch($scope, function (rows) {
                    
                    angular.forEach(rows, function(key, value) {
                        
                    });
                    var msg = 'rows changed ' + rows.length;
                    console.log(msg);
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

            CustomerStatementTransactionService.getGridData(
                function onSuccess(data) {
                    vm.gridOptions.totalItems = data.TotalCount;
                    $rootScope.app.Transactions = data.Data;
                    vm.gridOptions.data = $rootScope.app.Transactions;
            }, null, paginationOptions);

            
        };

        $scope.resetFilter = function () {
            //vm.dt = null;
            vm.DebitOrCredit.selected = null;
            paginationOptions.params.DebitOrCredit = null;
            paginationOptions.params.IsAsc = true;
            paginationOptions.params.PageNo = 1;
            paginationOptions.params.sort = null;
            paginationOptions.params.SortBy = 0;
            getPage();
        }

        $scope.fiterData = function () {
            //paginationOptions.params.CreatedDate = vm.dt;
            paginationOptions.params.DebitOrCredit = vm.DebitOrCredit.selected == null ? "" : vm.DebitOrCredit.selected.Id;
            getPage();
        }

        $scope.resetFilter();


    }
})();