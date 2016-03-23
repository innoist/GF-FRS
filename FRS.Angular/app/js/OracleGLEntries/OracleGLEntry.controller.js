﻿/**=========================================================
 * Module: load meta data
 * Load Meta Data view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('OracleGlEntryController', OracleGlEntryController);

    OracleGlEntryController.$inject = ['$scope', '$state', 'uiGridConstants', 'OracleGlEntryService'];

// ReSharper disable once InconsistentNaming
    function OracleGlEntryController($scope, $state, uiGridConstants, OracleGlEntryService) {
        window.OracleEntry = undefined;
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
        vm.Status = {};
        vm.Statuses = [
          { Id: '1', Name: 'Inactive'},
          { Id: '2', Name: 'Active'},
          { Id: '5', Name: 'Pending'}
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
                  field: 'OracleGLLoadId', sortId: 2,
                  //cellTemplate: '<div class="ui-grid-cell-contents"><a ui-sref="app.OracleGLLoadDetail({OracleGLLoadId : row.entity.OracleGLLoadId})">{{ row.entity.OracleGLLoadId}}</a></div>'
              },
              { name: 'Load', field: 'Name', sortId: 2 },
              { name: 'Unique Ref. Key', field: 'ModifiedOnString', sortId: 5 },
              { name: 'A/C#', field: 'FileName', sortId: 3 },
              { name: 'Period', field: 'OracleGLEntryCount', sortId: 3 },
              { name: 'Year', field: 'Status', sortId: 4 },
              { name: 'Created On', field: 'Status', sortId: 4 },
              { name: 'Modified On', field: 'Status', sortId: 4 },
              //{
              //    name: 'Actions', cellTemplate: '<div class="ui-grid-cell-contents"><div class="btn btn-xs">' +
              //      '<a href="javascript:;" class="btn btn-xs btn-info"><i class="fa fa-search"></i></a>' +
              //      '</div></div>'
              //}
              
              //{ name: 'Action', width: '10%', cellTemplate: '<div class="ui-grid-cell-contents"><a ui-sref="app.CustomerStatements({Id : row.entity.MT940LoadId})" class="btn btn-xs btn-green">Details</a></div>' }
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
                gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                    window.OracleEntry = row.entity;
                    console.log(row.entity);
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

            OracleGlEntryService.getGridData(
                function onSuccess(data) {
                    vm.gridOptions.totalItems = data.TotalCount;
                    vm.gridOptions.data = data.OracleGlEntries;
            }, null, paginationOptions);

            
        };

        $scope.resetFilter = function () {
            vm.dt = null;
            //vm.name = '';
            vm.Status.selected = null;

            paginationOptions.params.IsAsc = true;
            paginationOptions.params.PageNo = 1;
            paginationOptions.params.sort = null;
            paginationOptions.params.SortBy = 0;
            getPage();
        }

        $scope.fiterData = function () {
            //paginationOptions.params.Name = vm.name;
            //paginationOptions.params.CreatedDate = vm.dt;
            paginationOptions.params.StatusId = vm.Statuses.selected == null ? 0 : vm.Status.selected.Id;
            getPage();
        }

        $scope.resetFilter();


    }
})();