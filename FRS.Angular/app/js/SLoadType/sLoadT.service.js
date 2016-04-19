(function () {
    'use strict';
    var core = angular.module('app.core');
    core.lazy.service('sLoadTservice', sLoadTservice);
    sLoadTservice.$inject = ['$http'];

    function sLoadTservice($http) {
        this.getLoadTypes = function (onReady, onError) {
            var url = frsApiUrl + "/api/LoadType";
            onError = onError || function () { alert('Failed to load data'); };
            $http
                .get(url)
                .success(onReady)
                .error(onError);
        }
    }
})();