/**=========================================================
 * Module: Currency
 * Currency view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('FiscalYearController', FiscalYearController);

    FiscalYearController.$inject = ['$state', 'uiGridConstants', 'FiscalYearService'];

    function FiscalYearController($state, uiGridConstants, FiscalYearService) {

        var vm = this;

        //ui-grid
        vm.gridOptions = {
            paginationPageSizes: [10, 25, 50, 100, 500],
            paginationPageSize: 10,
            enableSorting: true,
            enableFiltering: true,
            //suppressRemoveSort: true,
            //useExternalPagination: true,
            //useExternalSorting: true,
            //enableFiltering: true,
            flatEntityAccess: true,
            //fastWatch: true,
            //enableGridMenu: true,
            enableColumnMenus: false,
            //useExternalFiltering: true,
            columnDefs: [
                // name is for display on the table header, field is for mapping as in 
                //sortId is kept locally it is not the property of ui.grid
                {
                    displayName: 'ID',
                    field: 'Value',
                    sortId: 1,
                    cellTemplate: '<div class="ui-grid-cell-contents"><a ui-sref="app.NewFiscalYear({Id : row.entity.Value})">{{row.entity.Value}}</a> </div>',
                    filter: { placeholder: 'Search by Id' },
                    sort: {
                        direction: uiGridConstants.ASC
                    }
    },
              { name: 'Name', field: 'Name', sortId: 2, filter: { placeholder: 'Search by Name' } }
            ]
        };

        //vm.gridOptions.data = [];

        FiscalYearService.getYears(function(response) {
            if (response) {
                vm.gridOptions.data = response;
            }
        });

    }
})();