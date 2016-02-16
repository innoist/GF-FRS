

/**=========================================================
 * Module: load meta data
 * Load Meta Data service
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.LoadMetaData')
        .service('LoadMetaDataService', LoadMetaDataService);

    LoadMetaDataService.$inject = ['$http', '$state', '$localStorage'];
    function LoadMetaDataService($http, $state, $localStorage) {
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