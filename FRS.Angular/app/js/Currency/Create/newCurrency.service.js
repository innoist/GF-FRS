(function () {
    'use strict';
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('NewCurrencyService', NewCurrencyService);

    NewCurrencyService.$inject = ['$http'];

    function NewCurrencyService($http) {

        this.saveCurrency = function (data, onReady, onError) {

            var url = window.frsApiUrl + '/api/Currency';

            onError = onError || function () { alert('Failure saving Meta Data'); };

            $http({
                url: url,
                data: data,
                method: 'POST'
            }).then(onReady, onError);
        };

        this.loadCurrencyById = function (Id, onReady, onError) {
            var urlMetaData = window.frsApiUrl + '/api/Currency?Id=' + Id;

            onError = onError || function () { alert('Failure loading Data'); };

            $http
              .get(urlMetaData)
              .success(onReady)
              .error(onError);
        };

    }
})();