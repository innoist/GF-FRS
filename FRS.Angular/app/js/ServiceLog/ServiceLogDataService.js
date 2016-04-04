/**=========================================================
 * Module: Service Log
 =========================================================*/


(function () {
    'use strict';

    // Setup Service
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('ServiceLogService', ServiceLogService);


    ServiceLogService.$inject = ['$http'];
    // ReSharper restore FunctionsUsedBeforeDeclared

    // ReSharper disable InconsistentNaming
    function ServiceLogService($http) {
        // ReSharper restore InconsistentNaming

        this.getServiceLogs = function (data, onReady, onError) {
            onError = onError || function () { alert('Failed to load Data'); };
            $http
              .get(window.frsApiUrl + '/api/ServiceLog', data)
              .success(onReady)
              .error(onError);
        }

        this.getServiceLogData = function (id, onReady, onError) {
            onError = onError || function () { alert('Failed to load Data'); };
            $http
              .get(window.frsApiUrl + '/api/ServiceLog/' + id)
              .success(onReady)
              .error(onError);
        }
    }

})();