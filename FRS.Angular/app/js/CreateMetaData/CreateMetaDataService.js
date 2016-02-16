(function () {
    'use strict';

    angular
        .module('app.CreateMetaData')
        .service('CreateMetaDataService', CreateMetaDataService);

    CreateMetaDataService.$inject = ['$http', '$state', '$localStorage'];
    function CreateMetaDataService($http, $state, $localStorage) {
        this.getLoadMetaData = getLoadMetaData;
        this.saveLoadMetaDataDetail = saveLoadMetaDataDetail;

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