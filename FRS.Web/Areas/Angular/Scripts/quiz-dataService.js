angular.module('sessionService', [])
.service('SessionService', function() {
    // Define request to get Cities 
    amplify.request.define('getCities', 'ajax', {
            url: ist.siteUrl + '/Api/TestAngular',
            dataType: 'json',
            decoder: amplify.request.decoders.istStatusDecoder,
            type: 'GET'
        }),
        // Define request to add/update City
        amplify.request.define('saveCity', 'ajax', {
            url: ist.siteUrl + '/Api/TestAngular',
            dataType: 'json',
            decoder: amplify.request.decoders.istStatusDecoder,
            type: 'POST'
        }),
    //add-update City.
        $scope.saveCity = function (params, callbacks) {
            return amplify.request({
                resourceId: 'saveCity',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        },
        //Get Cities
        $scope.getCitiesDataBase = function(params, callbacks) {
            //initialize();
            return amplify.request({
                resourceId: 'getCities',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        };
});