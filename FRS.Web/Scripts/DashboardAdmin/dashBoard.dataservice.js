/*
    Data service module with ajax calls to the server
*/
define("dashBoard/dashBoard.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function() {
                if (!isInitialized) {


                    // Define request to get Regions 
                    amplify.request.define('getSiteData', 'ajax', {
                        url: ist.siteUrl + '/Api/Dashboard',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });
                   
                    // Define request to add/update Regio
                    amplify.request.define('saveSiteData', 'ajax', {
                        url: ist.siteUrl + '/Api/Dashboard',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });


                    isInitialized = true;
                }
            },
            // Get Site Data
            getSiteData = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: "getSiteData",
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
           // Save Site Data
            saveSiteData = function(params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'saveSiteData',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };
      
        return {
            saveSiteData: saveSiteData,
            getSiteData: getSiteData,

        };
    })();
    return dataService;
});