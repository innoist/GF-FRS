/**=========================================================
 * Module: load meta data
 * Load Meta Data view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('MT940BalanceController', MT940BalanceController);

    MT940BalanceController.$inject = ['$scope', '$state', 'uiGridConstants', 'MT940BalanceService'];

    function MT940BalanceController($scope, $state, uiGridConstants, MT940BalanceService) {

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
        vm.Currency = {};
        vm.Currencies = [
          //{ Id: 'D', Name: 'Debit'},
          //{ Id: 'C', Name: 'Credit'}
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
                CurrencyId: 0
    },

        };
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
              //{
              //    name: 'Name',
              //    field: 'Name',
              //    sortId: 1,
              //    cellTemplate: '<div class="ui-grid-cell-contents"><a ui-sref="">{{row.entity.Name}}</a> </div>',
              //    //sort: {
              //    //    direction: uiGridConstants.ASC
              //    //}
              //},
              { name: 'Id', field: 'MT940BalanceId', sortId: 0 },
              { name: 'Type', field: 'DebitOrCredit', sortId: 3 },
              { name: 'Entry', field: 'EntryDate', sortId: 5 },
              { name: 'Value', field: 'Value', sortId: 0 },
              { name: 'Actions' , cellTemplate: '<div class="ui-grid-cell-contents"><div uib-dropdown="dropdown" style="position:absolute;" class="btn-group btn-xs dropdown">' +
                  '<a type="button" class="btn btn-xs btn-info"><i class="fa fa-search"></i></a>' +
                  '<a type="button" uib-dropdown-toggle="" class="btn btn-xs dropdown-toggle btn-info" aria-haspopup="true" aria-expanded="false">' +
                  '<span class="caret"></span><span class="sr-only">info</span>' +
                  '</a>' +
                  '<ul role="menu" class="dropdown-menu">' +
                  '<li><a href="javascript:;">Action</a></li>' +
                  '<li><a href="javascript:;">Another action</a></li>' +
                  '</ul></div></div>' }
              
              
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

            MT940BalanceService.getGridData(
                function onSuccess(data) {
                    vm.gridOptions.totalItems = data.TotalCount;
                    vm.gridOptions.data = data.Data;
                    vm.Currencies = data.Currencies;
                }, null, paginationOptions);

            
        };

        $scope.resetFilter = function () {
            //vm.dt = null;
            vm.Currency.selected = null;
            paginationOptions.params.CurrencyId = 0;
            paginationOptions.params.IsAsc = true;
            paginationOptions.params.PageNo = 1;
            paginationOptions.params.sort = null;
            paginationOptions.params.SortBy = 0;
            getPage();
        }

        $scope.fiterData = function () {
            //paginationOptions.params.CreatedDate = vm.dt;
            paginationOptions.params.CurrencyId = vm.Currency.selected == null ? "" : vm.Currency.selected.Id;
            getPage();
        }

        $scope.resetFilter();


    }
})();