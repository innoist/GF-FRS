/**=========================================================
 * Module: Currency
 * Currency view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('StatusController', StatusController);

    StatusController.$inject = ['$state', 'uiGridConstants', 'StatusService'];

    function StatusController($state, uiGridConstants, StatusService) {

        var vm = this;

        //ui-grid
        vm.gridOptions = {
            paginationPageSizes: [10, 25, 50, 100, 500],
            paginationPageSize: 10,
            enableSorting: true,
            enableFiltering: true,
            flatEntityAccess: true,
            enableColumnMenus: false,
            columnDefs: [
                // name is for display on the table header, field is for mapping as in 
                //sortId is kept locally it is not the property of ui.grid
                {
                    name: 'Name',
                    field: 'Name',
                    sortId: 1,
                    cellTemplate: '<div class="ui-grid-cell-contents"><a ui-sref="app.CreateStatus({Id : row.entity.Value})">{{row.entity.Name}}</a> </div>',
                    filter: { placeholder: 'Search by Name' },
                    sort: {
                        direction: uiGridConstants.ASC
                    }
                }
            ]
        };

        StatusService.getCurrencies(function (response) {
            if (response) {
                vm.gridOptions.data = response;
            }
        });

    }
})();