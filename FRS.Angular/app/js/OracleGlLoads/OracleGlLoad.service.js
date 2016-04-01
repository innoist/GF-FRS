

/**=========================================================
 * Module: load meta data
 * Load Meta Data service
 =========================================================*/
(function() {
    
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('OracleGlLoadService', OracleGlLoadService);

    OracleGlLoadService.$inject = ['$http'];

    function OracleGlLoadService($http) {

        this.getGridData = function (onReady, onError, paginationOptions) {

            onError = onError || function () { alert('Failed to load data'); };
            $http
                .get(window.frsApiUrl + '/api/OracleGLLoad', paginationOptions)
                .success(onReady)
                .error(onError);
        }

        this.getOracleGLEntries = function (onReady, onError, paginationOptions) {

            onError = onError || function () { alert('Failed to load data'); };
            $http
                .get(window.frsApiUrl + '/api/OracleGLEntry', paginationOptions)
                .success(onReady)
                .error(onError);
        }

        this.getOracleGLLoadDetail = function (Id, onReady, onError) {
            onError = onError || function () { alert('Failed to load data'); };
            $http
                .get(window.frsApiUrl + '/api/OracleGLLoad/' + Id)
                .success(onReady)
                .error(onError);
        }

        this.processOracleGlLoad = function (Id, onReady, onError) {
            onError = onError || function () { alert('Failed to load data'); };
            $http
                .post(window.frsApiUrl + '/api/OracleGLLoad/?LoadId=' + Id)
                .success(onReady)
                .error(onError);
        }
    }


})();
