/**=========================================================
 * Module: Currency
 * Currency view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('SourceIndexController', SourceIndexController);

    SourceIndexController.$inject = ['$state', 'uiGridConstants', 'SourceIndexService'];

    function SourceIndexController($state, uiGridConstants, SourceIndexService) {

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
                    name: 'Name',
                    field: 'Name',
                    sortId: 1,
                    cellTemplate: '<div class="ui-grid-cell-contents"><a ui-sref="app.CreateSources({Id : row.entity.Value})">{{row.entity.Name}}</a> </div>',
                    filter: { placeholder: 'Search by Name' },
                    sort: {
                        direction: uiGridConstants.ASC
                    }
    },
              { name: 'Status', field: 'StatusName', sortId: 2, filter: { placeholder: 'Search by Status' } }
            ]
        };

        //vm.gridOptions.data = [];

        SourceIndexService.getCurrencies(function(response) {
            if (response) {
                vm.gridOptions.data = response;
            }
        });

    }
})();