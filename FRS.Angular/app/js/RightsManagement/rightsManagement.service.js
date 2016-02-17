/**=========================================================
 * Module: Rights Management
 * Rights Management service
 =========================================================*/


(function () {
    'use strict';

    // Setup Service
    angular
        .module('app.rightsManagement', [])
    // ReSharper disable FunctionsUsedBeforeDeclared
        .service('RightsManagementService', RightsManagementService);


    RightsManagementService.$inject = ['$http'];
    // ReSharper restore FunctionsUsedBeforeDeclared

    // ReSharper disable InconsistentNaming
    function RightsManagementService($http) {
        // ReSharper restore InconsistentNaming

        this.rightsUri = window.frsApiUrl + '/api/RightsManagement';

        this.get = function (onReady, onError) {
            onError = onError || function () { alert('Failed to load rights'); };
            $http
              .get(this.rightsUri)
              .success(onReady)
              .error(onError);
        }

        this.save = function (data, onReady, onError) {
            onError = onError || function () { alert('Failed to save rights'); };
            $http(
                {
                    method: 'POST',
                    url: this.rightsUri,
                    data: JSON.stringify(data)
                }
              )
              .then(onReady, onError);
        }
    }

})();