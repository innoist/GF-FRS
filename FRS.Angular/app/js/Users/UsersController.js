(function () {
    'use strict';

    angular.module('app.Users', []);
})();


(function() {
    angular
        .module('app.Users', [])
        .controller('UsersController', UsersController);

    UsersController.$inject = ['uiGridConstants', 'UsersService'];

    function UsersController(uiGridConstants, UsersService) {
        var vm = this;

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
        vm.UsersGridView = {
            paginationPageSizes: [10, 25, 50, 100, 500],
            paginationPageSize: 10,
            useExternalPagination: true,
            useExternalSorting: true,
            //enableFiltering: true,
            flatEntityAccess: true,
            //fastWatch: true,
            enableGridMenu: true,
            //useExternalFiltering: true,
            columnDefs: [
                // name is for display on the table header, field is for mapping as in 
                //sortId is kept locally it is not the property of ui.grid
              { name: 'Id', field: 'UserId', sortId: 0, width: '8%', enableSorting: false },
              { name: 'First Name', field: 'FirstName', sortId: 1 },
              { name: 'Last Name', field: 'LastName', sortId: 2 },
              { name: 'User Role', field: 'Role', sortId: 3 },
              { name: 'Email', field: 'Email', sortId: 4 },
              { name: 'Address', field: 'Address', sortId: 5 },
              { name: 'Telephone', field: 'Telephone', sortId: 6 },
              { name: 'Company', field: 'Company', sortId: 7 }
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
                    break;
            }

            $http.get(window.frsApiUrl + '/api/Users', paginationOptions)
            .success(function (data) {
                vm.gridOptions.totalItems = data.TotalCount;
                //var firstRow = (paginationOptions.pageNumber - 1) * paginationOptions.pageSize;
                vm.gridOptions.data = data.LoadMetaDatas; //.slice(firstRow, firstRow + paginationOptions.pageSize);
            }).error(function () {
            });
        };

        getPage();
    }
})();