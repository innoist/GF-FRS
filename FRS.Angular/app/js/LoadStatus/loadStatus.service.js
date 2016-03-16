/**=========================================================
 * Module: Currency
 * Currency view Service
 =========================================================*/

(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('LoadStatusService', LoadStatusService);

    LoadStatusService.$inject = ['$http'];

    function LoadStatusService($http) {

        this.getStatuses = function (onReady, onError) {
            var url = frsApiUrl + "/api/LoadStatus";
           onError = onError || function () { alert('Failed to load data'); };
           $http
             .get(url)
             .success(onReady)
             .error(onError);
        }

        this.save = function (data, onReady, onError) {

            var url = window.frsApiUrl + '/api/LoadStatus';

            onError = onError || function () { alert('Failure saving Data'); };

            $http({
                url: url,
                data: data,
                method: 'POST'
            }).then(onReady, onError);
        };

        this.loadLoadStatusById = function (Id, onReady, onError) {
            var urlMetaData = window.frsApiUrl + '/api/LoadStatus?Id=' + Id;

            onError = onError || function () { alert('Failure loading Data'); };

            $http
              .get(urlMetaData)
              .success(onReady)
              .error(onError);
        };

    }
})();