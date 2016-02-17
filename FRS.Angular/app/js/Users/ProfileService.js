(function() {
    'use strict';

    angular.module('app.Profile')
        .service('ProfileService', ProfileService);

    ProfileService.$inject = ['$http'];

    function ProfileService($http) {
        this.loadProfile = loadProfile;
        this.saveProfile = saveProfile;
        this.profile = {};

        function saveProfile(onSuccess, onError) {
            var url = window.frsApiUrl + '/api/Profile';

            onError = onError || function () { alert('Failure loading Data'); };

            $http
                .get(url)
                .success(onSuccess)
                .error(onError);
        }
    }


    function loadProfile(onSuccess, onError) {
        var url = window.frsApiUrl + '/api/Profile';

        onError = onError || function () { alert('Failure saving Data'); };
        onSuccess = onSuccess || function () { alert('Save Complete'); };

        $http
            .post(url, this.profile)
            .success(onSuccess)
            .error(onError);
    }
})();