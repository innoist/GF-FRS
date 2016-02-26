/**=========================================================
 * Module: Currency
 * Currency view Service
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('StatusService', StatusService);

    StatusService.$inject = ['$http'];

    function StatusService($http) {

        this.getCurrencies = function (onReady, onError) {
            var url = frsApiUrl + "/api/Status";
            onError = onError || function () { alert('Failed to load data.'); };
            $http
              .get(url)
              .success(onReady)
              .error(onError);
        }

    }
})();