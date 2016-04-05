

/**=========================================================
 * Module: load meta data
 * Load Meta Data service
 =========================================================*/
(function() {
    
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('OracleGlEntryService', OracleGlEntryService);

    OracleGlEntryService.$inject = ['$http'];

// ReSharper disable once InconsistentNaming
    function OracleGlEntryService($http) {

        this.getGridData = function (onReady, onError, paginationOptions) {

            onError = onError || function () { alert('Failed to load data'); };
            $http
                .get(window.frsApiUrl + '/api/OracleGLEntry', paginationOptions)
                .success(onReady)
                .error(onError);
        }

        this.getDetail = function (id,onReady, onError) {

            onError = onError || function () { alert('Failed to load data'); };
            $http
                .get(window.frsApiUrl + '/api/OracleGLEntry/'+id)
                .success(onReady)
                .error(onError);
        }
    }


})();
