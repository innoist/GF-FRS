
//(function () {
//    'use strict';

//    angular
//        .module('app.LogsModule', []);
//})();

/**=========================================================
 * Module: load Log
 * Load Log service
 =========================================================*/


(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('LogsController', LogsController);

    LogsController.$inject = ['$http', '$scope', '$state', 'uiGridConstants'];

    function LogsController($http, $scope, $state, uiGridConstants) {

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


        var paginationOptions = {
            'params': {
                SortBy: 0,
                SearchString: '',
                IsAsc: true,
                PageNo: 1,
                PageSize: 10,
                sort: null,
                Timestamp: '',
                Severity: '',
                Message: ''
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
              { name: 'Severity', field: 'Severity', sortId: 0, width: '15%' },
              { name: 'Timestamp', field: 'Timestamp', sortId: 1, width: '20%' },
              { name: 'Message', field: 'Message', sortId: 2 }
             
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

            $http.get(window.frsApiUrl + '/api/Log', paginationOptions)
            .success(function (data) {
                vm.gridOptions.totalItems = data.TotalCount;
                //var firstRow = (paginationOptions.pageNumber - 1) * paginationOptions.pageSize;
                vm.gridOptions.data = data.LogDatas; //.slice(firstRow, firstRow + paginationOptions.pageSize);
            }).error(function () {
            });
        };

        $scope.resetFilter = function () {
            vm.dt = null;
            vm.message = '';
            vm.severity = '';

            paginationOptions.params.Severity = '';
            paginationOptions.params.IsAsc = true;
            paginationOptions.params.PageNo = 1;
            paginationOptions.params.sort = null;
            paginationOptions.params.SortBy = 0;
            paginationOptions.params.Timestamp = '';
            paginationOptions.params.Message = '';
            getPage();
        }

        $scope.fiterData = function () {
            paginationOptions.params.Message = vm.message;
            paginationOptions.params.Severity = vm.severity;
            paginationOptions.params.Timestamp = vm.dt;
            getPage();
        }

        getPage();

    }
})();
