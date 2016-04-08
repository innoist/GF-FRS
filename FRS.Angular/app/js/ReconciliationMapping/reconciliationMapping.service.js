/**=========================================================
 * Module: Currency
 * Currency view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('ReconciliationMappingService', ReconciliationMappingService);

    ReconciliationMappingService.$inject = ['$http'];

    function ReconciliationMappingService($http) {

        this.getManual = function (onReady, onError, paginationOptions) {
            var url = frsApiUrl + "/api/ReconciledMapping";
            onError = onError || function () { alert('Failed to load data'); };
            $http
              .get(url, paginationOptions)
              .success(onReady)
              .error(onError);
        }

        this.getMappingDetail = function (id, onReady, onError) {
            var url = frsApiUrl + "/api/ReconciledMapping/"+id;
            onError = onError || function () { alert('Failed to load data'); };
            $http
              .get(url)
              .success(onReady)
              .error(onError);
        }

    }
})();
