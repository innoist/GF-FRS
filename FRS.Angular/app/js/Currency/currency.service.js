/**=========================================================
 * Module: Currency
 * Currency view Service
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('CurrencyService', CurrencyService);

    CurrencyService.$inject = ['$http'];

    function CurrencyService($http) {

        this.getCurrencies = function (onReady, onError) {
            var url = frsApiUrl + "/api/Currency";
           onError = onError || function () { alert('Failed to load rights'); };
           $http
             .get(url)
             .success(onReady)
             .error(onError);
       }

    }
})();