//(function () {
//    'use strict';

//    angular.module('app.Users', []);
//})();


(function() {
    //angular
    //    .module('app.Users', [])
    //    .controller('UsersController', UsersController);
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('UsersController', UsersController);

    UsersController.$inject = ['$scope', '$http', 'uiGridConstants', 'UsersService'];

    function UsersController($scope, $http, uiGridConstants, UsersService) {
        var vm = this;
        var actionBtnTemplate = '<div uib-dropdown="dropdown" class="btn-group">' +
            '<button type="button" class="btn btn-inverse">Action</button>' +
            '<button type="button" uib-dropdown-toggle="" class="btn dropdown-toggle btn-inverse">' + 
            '</button>' +
            '<ul role="menu" class="dropdown-menu">' +
            '</li><li>' +
            '<a href="#">Another action</a>' +
            '</li><li><a href="#">Something else here</a>' +
            '</li>' +
            '<li class="divider"></li><li><a href="#">Separated link</a></li></ul></div>';
        var paginationOptions = {
            'params': {
                SortBy: 0,
                SearchString: '',
                IsAsc: true,
                PageNo: 1,
                PageSize: 10,
                sort: null,
                Name: null,
                Role: null,
                PhoneNumber: null
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
              { name: 'Id', field: 'Id', sortId: 0, width: '8%', enableSorting: true, cellTemplate: '<a class="ui-grid-cell-contents" ui-sref="app.Profile({Name : row.entity.UserName})">{{row.entity.Id}} </a>' },
              { name: 'First Name', field: 'FirstName', sortId: 1, enableSorting: false },
              { name: 'Last Name', field: 'LastName', sortId: 2, enableSorting: false },
              { name: 'User Name', field: 'UserName', sortId: 3, enableSorting: false },
              { name: 'Email', field: 'Email', sortId: 4, enableSorting: false },
              { name: 'Address', field: 'Address', sortId: 5, enableSorting: false },
              { name: 'Phone No.', field: 'Telephone', sortId: 6, enableSorting: false },
              { name: 'Company', field: 'CompanyName', sortId: 7, enableSorting: false },
              { name: 'Role', field: 'Role', sortId: 8, enableSorting: false },
              //{ name: 'Action', field: '', sortId: 8, enableSorting: false, cellTemplate: actionBtnTemplate }
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
                        angular.forEach(vm.UsersGridView.columnDefs, function (value, key) {
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
                    break;
            }

            UsersService.paginationOpts = paginationOptions;
            UsersService.getUsers(function (data) {
                vm.UsersGridView.totalItems = data.TotalCount || 0;
                vm.UsersGridView.data = data.Data || [];
            },
            function (err) {
                alert("Error loading data");
                console.log(err);
            });
        };

        //ui-select
        vm.disabled = undefined;
        vm.Role = {};
        vm.Roles = [
          //{ Id: '1', Name: 'a'}
        ];

        UsersService.getBaseData(function(response) {
            vm.Roles = response;
        });

        vm.resetFilter = function () {
            vm.phone = '';
            vm.name = '';
            vm.Roles.selected = null;

            paginationOptions.params.CreatedDate = '';
            paginationOptions.params.IsAsc = true;
            paginationOptions.params.PageNo = 1;
            paginationOptions.params.sort = null;
            paginationOptions.params.SortBy = 0;

            //Custom Filter parameters
            paginationOptions.params.Name = null;
            paginationOptions.params.Role = null;
            paginationOptions.params.PhoneNumber = null;
            getPage();
        }

        vm.fiterData = function () {
            paginationOptions.params.Name = vm.name;
            paginationOptions.params.CreatedDate = vm.dt;
            paginationOptions.params.Role = vm.Roles.selected == null ? '' : vm.Roles.selected.Id;
            getPage();
        }

        getPage();
    }
})();
