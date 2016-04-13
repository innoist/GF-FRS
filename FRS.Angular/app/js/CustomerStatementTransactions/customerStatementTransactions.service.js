

/**=========================================================
 * Module: load meta data
 * Load Meta Data service
 =========================================================*/
(function() {
    
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('CustomerStatementTransactionService', CustomerStatementTransactionService);

    CustomerStatementTransactionService.$inject = ['$http'];

    function CustomerStatementTransactionService($http) {

        this.getGridData = function (onReady, onError, paginationOptions) {

            onError = onError || function () { alert('Failed to load rights'); };
            $http
                .get(window.frsApiUrl + '/api/MT940CustomerStatementTransaction', paginationOptions)
                .success(onReady)
                .error(onError);

            



            
        }
        this.getDetail = function(id, onReady, onError) {

            onError = onError || function() { alert('Failed to load rights'); };
            $http
                .get(window.frsApiUrl + '/api/MT940CustomerStatementTransaction/' + id)
                .success(onReady)
                .error(onError);
        }
    }


    })();
