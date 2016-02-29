(function () {
    'use strict';
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('CreateSourceService', CreateSourceService);

    CreateSourceService.$inject = ['$http'];

    function CreateSourceService($http) {

        this.saveSource = function (data, onReady, onError) {

            var url = window.frsApiUrl + '/api/Source';

            onError = onError || function () { alert('Failure saving Meta Data'); };

            $http({
                url: url,
                data: data,
                method: 'POST'
            }).then(onReady, onError);
        };

        this.loadSourceById = function (Id, onReady, onError) {
            var urlMetaData = window.frsApiUrl + '/api/Source?Id=' + Id;

            onError = onError || function () { alert('Failure loading Data'); };

            $http
              .get(urlMetaData)
              .success(onReady)
              .error(onError);
        };

    }
})();