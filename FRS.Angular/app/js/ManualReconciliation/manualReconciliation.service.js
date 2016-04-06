/**=========================================================
 * Module: Currency
 * Currency view Service
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('ReconciliationSerice', ReconciliationSerice);

    ReconciliationSerice.$inject = ['$http'];

    function ReconciliationSerice($http) {

        this.saveReconciledRecords = function (data, onReady, onError) {
            var url = frsApiUrl + "/api/ReconciledMapping";
           onError = onError || function () { alert('Failed to save'); };
           $http
             .post(url, data)
             .success(onReady)
             .error(onError);
        }

        this.getOracleGridData = function (onReady, onError, paginationOptions) {

            onError = onError || function () { alert('Failed to load data'); };
            $http
                .get(window.frsApiUrl + '/api/OracleGLEntry', paginationOptions)
                .success(onReady)
                .error(onError);
        }

        this.getCustomerStatementGridData = function (onReady, onError, paginationOptions) {

            onError = onError || function () { alert('Failed to load rights'); };
            $http
                .get(window.frsApiUrl + '/api/MT940CustomerStatementTransaction', paginationOptions)
                .success(onReady)
                .error(onError);


        }

    }
})();