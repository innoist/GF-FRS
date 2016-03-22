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

       // this.getLoadTypes = function (onReady, onError) {
       //     var url = frsApiUrl + "/api/LoadType";
       //    onError = onError || function () { alert('Failed to load data'); };
       //    $http
       //      .get(url)
       //      .success(onReady)
       //      .error(onError);
       //}

    }
})();