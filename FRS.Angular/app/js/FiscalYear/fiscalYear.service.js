/**=========================================================
 * Module: Currency
 * Currency view Service
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('FiscalYearService', FiscalYearService);

    FiscalYearService.$inject = ['$http'];

    function FiscalYearService($http) {

        this.getYears = function (onReady, onError) {
            var url = frsApiUrl + "/api/FiscalYear";
           onError = onError || function () { alert('Failed to load data'); };
           $http
             .get(url)
             .success(onReady)
             .error(onError);
        }

        this.save = function (data, onReady, onError) {

            var url = window.frsApiUrl + '/api/FiscalYear';

            onError = onError || function () { alert('Failure saving Data'); };

            $http({
                url: url,
                data: data,
                method: 'POST'
            }).then(onReady, onError);
        };

        this.loadYearById = function (Id, onReady, onError) {
            var urlMetaData = window.frsApiUrl + '/api/FiscalYear?Id=' + Id;

            onError = onError || function () { alert('Failure loading Data'); };

            $http
              .get(urlMetaData)
              .success(onReady)
              .error(onError);
        };

    }
})();