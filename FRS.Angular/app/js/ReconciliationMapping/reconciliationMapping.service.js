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

        this.getManual = function (onReady, onError) {
            var url = frsApiUrl + "/api/ReconciledMapping";
            onError = onError || function () { alert('Failed to load data'); };
            $http
              .get(url)
              .success(onReady)
              .error(onError);
        }

    }
})();
