(function () {
    'use strict';
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('CreateStatusService', CreateStatusService);

    CreateStatusService.$inject = ['$http'];

    function CreateStatusService($http) {

        this.saveStatus = function (data, onReady, onError) {

            var url = window.frsApiUrl + '/api/Status';

            onError = onError || function () { alert('Failure saving Data'); };

            $http({
                url: url,
                data: data,
                method: 'POST'
            }).then(onReady, onError);
        };

        this.loadStatusById = function (Id, onReady, onError) {
            var urlMetaData = window.frsApiUrl + '/api/Status?Id=' + Id;

            onError = onError || function () { alert('Failure loading Data'); };

            $http
              .get(urlMetaData)
              .success(onReady)
              .error(onError);
        };

    }
})();