/**=========================================================
 * Module: Currency
 * Currency view Controller
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('ReconciliationSerice', ReconciliationSerice);

    ReconciliationSerice.$inject = ['$http'];

    function ReconciliationSerice($http) {

        this.getManual = function (onReady, onError, paginationOptions) {
            var url = frsApiUrl + "/api/ReconciledMapping";
            onError = onError || function () { alert('Failed to load data'); };
            $http
              .get(url, paginationOptions)
              .success(onReady)
              .error(onError);
        }

    }
})();
