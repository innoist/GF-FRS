

/**=========================================================
 * Module: load meta data
 * Load Meta Data service
 =========================================================*/
(function() {
    
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('CustomerStatementsService', CustomerStatementsService);

    CustomerStatementsService.$inject = ['$http'];

    function CustomerStatementsService($http) {

        this.getGridData = function (onReady, onError, paginationOptions) {

            onError = onError || function () { alert('Failed to load rights'); };
            $http
                .get(window.frsApiUrl + '/api/CustomerStatement', paginationOptions)
                .success(onReady)
                .error(onError);

            
        }
        this.CustomerStatementDetail = function (Id, onReady, onError) {
            onError = onError || function () { alert('Failed to load data'); };
            $http
                .get(window.frsApiUrl + '/api/CustomerStatement/' + Id)
                .success(onReady)
                .error(onError);
        }
    }


})();
