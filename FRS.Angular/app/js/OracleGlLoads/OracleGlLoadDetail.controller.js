///**=========================================================
// * Module: load meta data
// * Load Meta Data view Controller
// =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('OracleGlLoadDetailController', OracleGlLoadDetailController);

    OracleGlLoadDetailController.$inject = ['$scope', '$state', '$stateParams', 'uiGridConstants', 'OracleGlLoadService', 'toaster'];

    function OracleGlLoadDetailController($scope, $state, $stateParams, uiGridConstants, OracleGlLoadService, toaster) {

        var vm = this;
        $scope.toProcess = true;

        $scope.processLoad = function () {
            if (vm.OracleGlLoad.LoadStatus == 'Created') {
                var LoadId = vm.load.LoadId;
                OracleGlLoadService.processOracleGlLoad(LoadId, function (response) {
                    toaster.info("Message", response);
                },
                function (err) {
                    toaster.error("Message", "Processing failed for load" + "Id: " + vm.load);
                });
            }
        }

        if ($stateParams.Id != "") {
            OracleGlLoadService.getOracleGLLoadDetail($stateParams.Id, function (response) {
                vm.OracleGlLoad = response.OracleGlLoad;
                vm.load = response.Load;

                $scope.toProcess = vm.OracleGlLoad.LoadStatus != 'Created';
            });
        }


        //ui-grid
        var paginationOptions = {
            'params': {
                SortBy: 0,
                SearchString: '',
                IsAsc: true,
                PageNo: 1,
                PageSize: 10,
                sort: null,
                OracleGLLoadId: $stateParams.Id == "" ? 0 : $stateParams.Id
            },

        };
        vm.gridOptions = {
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
              { name: 'Unique Ref. Key', field: 'UniqueReferenceKey', sortId: 5 },
              { name: 'A/C#', field: 'AccountNumber', sortId: 3 },
              { name: 'Period', field: 'Period', sortId: 3 },
              { name: 'Year', field: 'Year', sortId: 4, headerCellClass: 'text-right' },
              { displayName: 'JE Created', field: 'JECreationDate', sortId: 4 },
              { displayName: 'JE Updated', field: 'JELastUpdateDate', sortId: 4 },
              {
                  name: 'Actions', cellTemplate: '<div class="ui-grid-cell-contents text-center"><div class="btn btn-xs">' +
                    '<a ui-sref="app.OracleGlEntryDetail({Id : row.entity.OracleGLEntryId})" class="btn btn-xs btn-info"><i class="fa fa-search"></i></a>' +
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

            OracleGlLoadService.getOracleGLEntries(function (data) {
                vm.gridOptions.totalItems = data.TotalCount;
                vm.gridOptions.data = data.OracleGlEntries;
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
            getPage();
        }

        $scope.fiterData = function () {
            //paginationOptions.params.Name = vm.name;
            //paginationOptions.params.CreatedDate = vm.dt;
            //paginationOptions.params.StatusId = vm.Statuses.selected == null ? 0 : vm.Status.selected.Id;
            getPage();
        }

        $scope.resetFilter();
        //Set Id 
    }
})();