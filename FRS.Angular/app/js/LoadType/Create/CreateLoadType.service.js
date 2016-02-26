(function () {
    'use strict';
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('CreateLoadTypeService', CreateLoadTypeService);

    CreateLoadTypeService.$inject = ['$http'];

    function CreateLoadTypeService($http) {

        this.saveLoadType = function (data, onReady, onError) {

            var url = window.frsApiUrl + '/api/LoadType';

            onError = onError || function () { alert('Failure saving Meta Data'); };

            $http({
                url: url,
                data: data,
                method: 'POST'
            }).then(onReady, onError);
        };

        this.loadLoadTypeById = function (Id, onReady, onError) {
            var urlMetaData = window.frsApiUrl + '/api/LoadType?Id=' + Id;

            onError = onError || function () { alert('Failure loading Data'); };

            $http
              .get(urlMetaData)
              .success(onReady)
              .error(onError);
        };

    }
})();