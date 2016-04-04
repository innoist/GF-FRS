/**=========================================================
 * Module: load Log
 * Load Log service
 =========================================================*/


(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('ServiceLogController', ServiceLogController);

    ServiceLogController.$inject = ['$scope', '$state', 'uiGridConstants', 'ServiceLogService'];

    function ServiceLogController($scope, $state, uiGridConstants, ServiceLogService) {

        var vm = this;

        //datepicker
        vm.today = function () {
            vm.dt = new Date();
        };
        //vm.today();

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


        var paginationOptions = {
            'params': {
                SortBy: 0,
                SearchString: '',
                IsAsc: true,
                PageNo: 1,
                PageSize: 10,
                sort: null,
                Url : '',
                Message : '',
                Logged : null

            },

        };

        vm.gridOptions = {
            paginationPageSizes: [10, 25, 50, 100, 500],
            paginationPageSize: 10,
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
                    field: 'ServiceLogID',
                    sortId: 0,
                    width: '15%',
                    sort: {
                        direction: uiGridConstants.ASC
                    }
                },
                { name: 'Machine Name', field: 'MachineName', sortId: 1 },
                { name: 'Application', field: 'Application', sortId: 2 },
                { displayName: 'URL', field: 'Url', sortId: 3 },
                { displayName: 'Log Date', field: 'Logged', sortId: 5 },
                { name: 'Message', field: 'Message', sortId: 4, cellTemplate: '<div class="ui-grid-cell-contents" title="{{row.entity.Message}}">{{row.entity.Message}}</div>' },
                {
                    name: 'Actions',
                    cellTemplate: '<div class="ui-grid-cell-contents"><div class="btn btn-xs">' +
                        '<a ui-sref="app.ServiceLogDetail({Id : row.entity.ServiceLogID})" class="btn btn-xs btn-info"><i class="fa fa-search"></i></a>' +
                        '</div></div>',
                    enableSorting: false
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

            ServiceLogService.getServiceLogs(paginationOptions, function(data) {
                vm.gridOptions.totalItems = data.TotalCount;
                vm.gridOptions.data = data.Data;
            });

        };

        $scope.resetFilter = function () {
            vm.dt = null;
            vm.Url = '';
            vm.Message = '';
            vm.Logged = '';

            paginationOptions.params.Url = '',
            paginationOptions.params.Message= '',
            paginationOptions.params.Logged = '',

            paginationOptions.params.IsAsc = true;
            paginationOptions.params.PageNo = 1;
            paginationOptions.params.sort = null;
            paginationOptions.params.SortBy = 0;
            getPage();
        }

        $scope.fiterData = function () {

            paginationOptions.params.Logged = vm.dt;
            paginationOptions.params.Url = vm.Url;
            paginationOptions.params.Message = vm.Message;

            getPage();
        }

        getPage();

    }
})();
