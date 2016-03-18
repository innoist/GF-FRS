/**=========================================================
 * Module: load meta data
 * Load Meta Data view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('LoadMetaDataController', LoadMetaDataController);

    LoadMetaDataController.$inject = ['$http', '$rootScope', '$scope', '$state', 'uiGridConstants'];

    function LoadMetaDataController($http, $rootScope, $scope, $state, uiGridConstants) {

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
        vm.LoadType = {};
        vm.LoadTypes = [
          //{ Id: '1', Name: 'a'}
        ];

        $http.get(window.frsApiUrl + '/api/LoadMetaDataBase').success(function (response) {
            vm.LoadTypes = response.LoadTypes;
        });



        //ui-grid
        var paginationOptions = {
            'params': {
                SortBy: 0,
                SearchString: '',
                IsAsc: true,
                PageNo: 1,
                PageSize: 10,
                sort: null,
                Name: '',
                LoadTypeId: 0,
                CreatedDate: ''
            },

        };
        vm.gridOptions = {
            paginationPageSizes: [10, 25, 50, 100, 500],
            paginationPageSize: 10,
            enableSorting: true,
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
                  name: 'ID', field: 'LoadMetaDataId', sortId: 1,
                  //cellTemplate: '<div class="ui-grid-cell-contents"><a ui-sref="app.CreateMetaData({Id : row.entity.LoadMetaDataId})">{{row.entity.Name}}</a> </div>',
              },
              { name: 'Name',field: 'Name'},
              { name: 'Type', field: 'LoadType', sortId: 2 },
              { name: 'Source', field: 'Source', sortId: 3 },
              { name: 'Currency', field: 'Currency', sortId: 4 },
              { name: 'Created On', field: 'CreatedOnString', sortId: 5 },
              { name: 'Modified On', field: 'ModifiedOnString' },
              { name: 'Status', field: 'Status' },
              {
                  name: 'Actions', cellTemplate: '<div class="ui-grid-cell-contents"><div class="btn btn-xs">' +
                    '<a ui-sref="app.CreateMetaData({Id : row.entity.LoadMetaDataId})" class="btn btn-xs btn-info"><i class="fa fa-search"></i></a>' +
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
            $scope.loading = true;
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

            $http.get(window.frsApiUrl + '/api/LoadMetaData', paginationOptions)
            .success(function (data) {
                vm.gridOptions.totalItems = data.TotalCount;
                //var firstRow = (paginationOptions.pageNumber - 1) * paginationOptions.pageSize;
                vm.gridOptions.data = data.LoadMetaDatas; //.slice(firstRow, firstRow + paginationOptions.pageSize);
                $scope.loading = false;
            }).error(function () {
                $scope.loading = false;
            });
        };

        $scope.resetFilter = function () {
            vm.dt = null;
            vm.name = '';
            vm.LoadType.selected = null;

            paginationOptions.params.CreatedDate = '';
            paginationOptions.params.IsAsc = true;
            paginationOptions.params.PageNo = 1;
            paginationOptions.params.sort = null;
            paginationOptions.params.SortBy = 0;
            paginationOptions.params.Name = '';
            paginationOptions.params.LoadTypeId = 0;
            getPage();
        }

        $scope.fiterData = function () {
            paginationOptions.params.Name = vm.name;
            paginationOptions.params.CreatedDate = vm.dt;
            paginationOptions.params.LoadTypeId = vm.LoadType.selected == null ? 0 : vm.LoadType.selected.Id;
            getPage();
        }

        $scope.resetFilter();


    }
})();