/**=========================================================
 * Module: Currency
 * Currency view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('ReconciliationMappingController', ReconciliationMappingController);

    ReconciliationMappingController.$inject = ['$scope', '$state', '$stateParams', 'uiGridConstants', 'ReconciliationMappingService'];

    function ReconciliationMappingController($scope, $state, $stateParams, uiGridConstants, ReconciliationMappingService) {


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
          { Id: 'D', Name: 'Debit' },
          { Id: 'C', Name: 'Credit' }
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
                TransactDate: null,
                Amount:"",
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
                { displayName: 'ID', field: 'Identifier', sortId: 0 },
                {
                    displayName: 'Accounting Entry', field: 'OracleGLEntryId', sortId: 0,
                    cellTemplate: '<div class="ui-grid-cell-contents">' +
                    '<a ui-sref="app.OracleGlEntryDetail({Id : row.entity.OracleGLEntryId})">{{row.entity.OracleGLEntryId}}</a>' +
                    '</div>'
                },
                { name: 'Transactions', field: 'TransactionsCount', sortId: 2, headerCellClass: 'grid-align-right' },
                {
                    name: 'Debit/Credit', field: 'DebitOrCredit', sortId: 4, headerCellClass: 'grid-align-right',
                    cellTemplate: "<div class='ui-grid-cell-contents text-right'><label title='{{row.entity.DebitOrCredit}}' class='label' ng-class=" + '"' + "{'bg-green-light':row.entity.DebitOrCredit == 'Credit', 'bg-primary-light' : row.entity.DebitOrCredit == 'Debit'}" + '"' + ">{{row.entity.DebitOrCredit}}</label></div>"
                },
                { name: 'Amount', field: 'Amount', sortId: 3, headerCellClass: 'grid-align-right', cellFilter: 'number : 2' },
                {
                    name: 'Reconciliation Status', field: 'DebitOrCredit', sortId: 4, headerCellClass: 'grid-align-right',
                    cellTemplate: "<div class='ui-grid-cell-contents text-right'><label title='{{row.entity.DebitOrCredit}}' class='label' ng-class=" + '"' + "{'bg-green-light':row.entity.DebitOrCredit == 'Credit', 'bg-primary-light' : row.entity.DebitOrCredit == 'Debit'}" + '"' + ">{{row.entity.DebitOrCredit}}</label></div>"
                },
                {
                    name: 'Actions', cellTemplate: '<div class="ui-grid-cell-contents text-center"><div class="btn btn-xs">' +
                      '<a ui-sref="app.ReconciledMappingDetail({Id : row.entity.ReconciledMappingId})" class="btn btn-xs btn-info"><i class="fa fa-search"></i></a>' +
                      '</div></div>',
                    headerCellClass: 'text-center'
                }
            ],
            onRegisterApi: function (gridApi) {
                vm.gridApi = gridApi;
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

            ReconciliationMappingService.getManual(
                function onSuccess(data) {
                    vm.gridOptions.totalItems = data.TotalCount;
                    vm.gridOptions.data = data.ReconciledMappingModels;
                }, null, paginationOptions);


        };

        $scope.resetFilter = function () {
            //vm.dt = null;
            //vm.DebitOrCredit.selected = null;
            vm.Amount = "";
            vm.tansctiondt = null;
            paginationOptions.params.TransactDate = null;
            paginationOptions.params.Amount = "";
            paginationOptions.params.DebitOrCredit = null;
            paginationOptions.params.IsAsc = true;
            paginationOptions.params.PageNo = 1;
            paginationOptions.params.sort = null;
            paginationOptions.params.SortBy = 0;
            getPage();
        }

        $scope.fiterData = function () {
            
            paginationOptions.params.TransactDate = vm.tansctiondt;
            paginationOptions.params.Amount = vm.Amount;
           // paginationOptions.params.TransactDate = vm.tansctiondt;

            //paginationOptions.params.DebitOrCredit = vm.DebitOrCredit.selected == null ? "" : vm.DebitOrCredit.selected.Id;
            getPage();
        }

        $scope.resetFilter();

    }
})();
