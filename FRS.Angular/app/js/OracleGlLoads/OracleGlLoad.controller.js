/**=========================================================
 * Module: load meta data
 * Load Meta Data view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('OracleGlLoadController', OracleGlLoadController);

    OracleGlLoadController.$inject = ['$scope', '$state', 'uiGridConstants', 'OracleGlLoadService'];

    function OracleGlLoadController($scope, $state, uiGridConstants, OracleGlLoadService) {

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
                  displayName: 'ID',
                  field: 'OracleGLLoadId', sortId: 0,
                  //cellTemplate: '<div class="ui-grid-cell-contents"><a ui-sref="app.OracleGlLoadDetail({Id : row.entity.OracleGLLoadId})">{{ row.entity.OracleGLLoadId}}</a></div>'
              },
              { name: 'Load Name', field: 'Name', sortId: 5, enableSorting: false },
              { name: 'Start', field: 'Start', sortId: 1 },
              { name: 'Finish', field: 'Finish', enableSorting: false },

                //{
                //    name: 'In Progress', field: 'Progress', sortId: 6,
                //    cellTemplate: "<div class='ui-grid-cell-contents'><label class='label' ng-class=" + '"' + "{'bg-green-light':row.entity.Progress, 'bg-primary-light' : !row.entity.Progress}" + '"' + ">{{row.entity.ProgressTitle}}</label></div>"

                //},
                {
                    name: 'Load Status', field: 'LoadStatus', sortId: 2,headerCellClass: 'grid-align-right',
                    cellTemplate: "<div class='ui-grid-cell-contents text-center'><label class='label' ng-class=" + '"' + "{'bg-warning-light' : row.entity.LoadStatus == 'Created' || row.entity.LoadStatus == 'Submitted', 'bg-green-light' : row.entity.LoadStatus == 'Parsing' || row.entity.LoadStatus == 'Transforming' || row.entity.LoadStatus == 'Importing', 'bg-success' : row.entity.LoadStatus == 'Completed', 'bg-danger' : row.entity.LoadStatus == 'Failed'}" + '"' + ">{{row.entity.LoadStatus}}</label></div>"
                },
              { name: 'Modified On', field: 'ModifiedOnString', sortId: 3 },
              { name: 'File', field: 'FileName', enableSorting: false },
              { name: 'Entry Count', field: 'OracleGLEntryCount', sortId: 4,  headerCellClass: 'grid-align-right' },
              {
                  name: 'Actions', cellTemplate: '<div class="ui-grid-cell-contents text-center"><div class="btn btn-xs">' +
                    '<a ui-sref="app.OracleGlLoadDetail({Id : row.entity.OracleGLLoadId})" class="btn btn-xs btn-info"><i class="fa fa-search"></i></a>' +
                    '</div></div>',
                  headerCellClass: 'grid-align-right',
                  enableSorting: false
              }
              
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

            OracleGlLoadService.getGridData(
                function onSuccess(data) {
                    vm.gridOptions.totalItems = data.TotalCount;
                    vm.gridOptions.data = data.OracleGlLoads;
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