/**=========================================================
 * Module: load meta data
 * Load Meta Data view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('MT940LoadDetailController', MT940LoadDetailController);
    
    MT940LoadDetailController.$inject = ['$scope','$controller' , '$state', '$stateParams', 'uiGridConstants', 'MT940Service'];

    function MT940LoadDetailController($scope, $controller, $state, $stateParams, uiGridConstants, MT940Service) {
        
        var vm = this;
        $scope.toProcess = true;
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
                MT940LoadId: 0
            },
        };

        if ($stateParams.MT940LoadId != "") {
            
            paginationOptions.params.MT940LoadId = $stateParams.MT940LoadId;
            MT940Service.getMT940Detail($stateParams.MT940LoadId, function (response) {
                var load = response.Load;
                var mt940LoadModel = response.Mt940LoadModel;
                var loadMetadata = response.LoadMetaData;

                vm.load = load;
                vm.mt940Load = mt940LoadModel;
                vm.loadMetadata = loadMetadata;
                $scope.toProcess = vm.mt940Load.LoadStatus != 'Created';
            });
        }

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
                // name is for display on the table header, field is for mapping as in 
                //sortId is kept locally it is not the property of ui.grid
              {
                  displayName: 'ID', field: 'MT940CustomerStatementId', sortId: 1,
                  sort: {
                      direction: uiGridConstants.ASC
                  },
                  //cellTemplate: '<div class="ui-grid-cell-contents"><a ui-sref="app.CustomerStatementsDetail({MT940CustomerStatementId : row.entity.MT940CustomerStatementId})">{{row.entity.MT940CustomerStatementId}}</a></div>'
              },
              { name: 'Sequence', field: 'Sequence', sortId: 2 },
              { name: 'A/C#', field: 'AccountNumber', sortId: 2 },
              { name: 'Statement No.', field: 'StatementNumber', sortId: 2 },
              { name: 'Transaction Reference', field: 'TransactionReference', sortId: 5 },
              { name: 'Transactions', field: 'TransactionCount', sortId: 2 },
              {
                  name: 'Actions', cellTemplate: '<div class="ui-grid-cell-contents"><div class="btn btn-xs">' +
                    '<a ui-sref="app.CustomerStatementsDetail({MT940CustomerStatementId : row.entity.MT940CustomerStatementId})" class="btn btn-xs btn-info"><i class="fa fa-search"></i></a>' +
                    '</div></div>'
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

            MT940Service.getCustomerStatements(
                function onSuccess(data) {
                    vm.gridOptions.totalItems = data.TotalCount;
                    vm.gridOptions.data = data.Data;
                }, null, paginationOptions);


        };

        $scope.resetFilter = function () {
            //vm.dt = null;
            //vm.name = '';
            //vm.Status.selected = null;

            paginationOptions.params.IsAsc = true;
            paginationOptions.params.PageNo = 1;
            paginationOptions.params.sort = null;
            paginationOptions.params.SortBy = 0;
            paginationOptions.params.accountNumber = vm.accountNumber = '';
            getPage();
        }

        $scope.fiterData = function () {
            paginationOptions.params.accountNumber = vm.accountNumber;
            //paginationOptions.params.CreatedDate = vm.dt;
            //paginationOptions.params.LoadTypeId = vm.Statuses.selected == null ? 0 : vm.Status.selected.Id;
            getPage();
        }

        $scope.resetFilter();
    }
})();