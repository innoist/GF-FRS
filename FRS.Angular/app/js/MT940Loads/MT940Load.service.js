

/**=========================================================
 * Module: load meta data
 * Load Meta Data service
 =========================================================*/
(function() {
    
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('MT940Service', MT940Service);
    
    MT940Service.$inject = ['$http'];

    function MT940Service($http) {

        this.getGridData = function (onReady, onError, paginationOptions) {

            onError = onError || function () { alert('Failed to load data'); };
            $http
                .get(window.frsApiUrl + '/api/MT940Load', paginationOptions)
                .success(onReady)
                .error(onError);
        }

        this.getCustomerStatements = function (onReady, onError, paginationOptions) {

            onError = onError || function () { alert('Failed to load data'); };
            $http
                .get(window.frsApiUrl + '/api/CustomerStatement', paginationOptions)
                .success(onReady)
                .error(onError);
        }

        this.getMT940Detail = function (Id, onReady, onError) {
            onError = onError || function () { alert('Failed to load data'); };
            $http
                .get(window.frsApiUrl + '/api/MT940Load/'+ Id)
                .success(onReady)
                .error(onError);
        }
    }


})();
