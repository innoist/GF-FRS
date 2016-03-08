

/**=========================================================
 * Module: load meta data
 * Load Meta Data service
 =========================================================*/
(function() {
    
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('MT940BalanceService', MT940BalanceService);

    MT940BalanceService.$inject = ['$http'];

    function MT940BalanceService($http) {

        this.getGridData = function (onReady, onError, paginationOptions) {

            onError = onError || function () { alert('Failed to load rights'); };
            $http
                .get(window.frsApiUrl + '/api/MT940Balance', paginationOptions)
                .success(onReady)
                .error(onError);

            
        }
    }


})();
