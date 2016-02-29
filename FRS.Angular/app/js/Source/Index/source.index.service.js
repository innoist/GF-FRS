/**=========================================================
 * Module: Currency
 * Currency view Service
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('SourceIndexService', SourceIndexService);

    SourceIndexService.$inject = ['$http'];

    function SourceIndexService($http) {

        this.getCurrencies = function (onReady, onError) {
            var url = frsApiUrl + "/api/Source";
           onError = onError || function () { alert('Failed to load rights'); };
           $http
             .get(url)
             .success(onReady)
             .error(onError);
       }

    }
})();