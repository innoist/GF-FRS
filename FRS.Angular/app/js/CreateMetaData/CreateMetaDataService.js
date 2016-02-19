(function () {
    'use strict';
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.service('CreateMetaDataService', CreateMetaDataService);

    CreateMetaDataService.$inject = ['$http', '$state', '$localStorage'];
    function CreateMetaDataService($http, $state, $localStorage) {
        this.getLoadMetaData = getLoadMetaData;
        this.saveLoadMetaDataDetail = saveLoadMetaDataDetail;
        this.loadMetaDataById = function (Id, onReady, onError) {
            var urlMetaData = window.frsApiUrl + '/api/LoadMetaData/' + Id;

            onError = onError || function () { alert('Failure loading Meta Data'); };

            $http
              .get(urlMetaData)
              .success(onReady)
              .error(onError);
        };

        function getLoadMetaData(onReady, onError) {
            var urlMetaData = window.frsApiUrl + '/api/LoadMetaDataBase';

            onError = onError || function () { alert('Failure loading Meta Data'); };

            $http
              .get(urlMetaData)
              .success(onReady)
              .error(onError);
        }

        function saveLoadMetaDataDetail(metaData, onReady, onError) {

            var urlMetaData = window.frsApiUrl + '/api/LoadMetaData';

            onError = onError || function () { alert('Failure saving Meta Data'); };

            $http(
                {
                    method: 'POST',
                    url: urlMetaData,
                    headers: {
                        //'Content-Type': 'application/x-www-form-urlencoded',
                        'Authorization': 'Bearer ' + $localStorage['authorizationData'].token
                    },
                    data: JSON.stringify(metaData),
                }
              )
              .then(onReady, onError);
        }


    }
})();