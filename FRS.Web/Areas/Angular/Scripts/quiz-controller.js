angular.module('QuizApp', [])//'sessionService'

    .controller('QuizCtrl', function ($scope, $http) {
        $scope.city = {},
        $scope.cities = [{ CityName: "test1", CountryName: "test1" }],

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
        $scope.getCitiesDataBase = function (params, callbacks) {
            //initialize();
            return amplify.request({
                resourceId: 'getCities',
                success: callbacks.success,
                error: callbacks.error,
                data: params
            });
        },


        //Updating Checkbox
        $scope.counter = 0,
        $scope.change = function () {
            $scope.counter++;
        };

        //Updating checkbox
        $scope.names = ['pizza', 'unicorns', 'robots'];
        $scope.my = { favorite: 'unicorns' };

        $scope.name = 'Whirled';

        $scope.error = function (name) {
            var s = $scope.form[name];
            return s.$invalid && s.$dirty ? "error" : "";
        };


        //Get Cities
        $scope.getCities = function () {
            $scope.getCitiesDataBase(
            {},
            {
                success: function (data) {
                    $scope.cities = data.Cities;
                    $scope.$apply();
                },
                error: function (error) {
                }
            });
        };

        $scope.data = {
            repeatSelect: null,
            availableOptions: [
              { id: '1', name: 'Option A' },
              { id: '2', name: 'Option B' },
              { id: '3', name: 'Option C' }
            ],
        };

        //Submit
        $scope.city = { CityName: 'Lahore', CityCode: 'LHR' }

        $scope.isEdittable = false,
        $scope.cityCopy = false,

            $scope.submit = function () {
                $scope.saveCity($scope.city, {
                    success: function (dataFromServer) {
                        toastr.success(ist.resourceText.CitySaveSuccessMessage);
                    },
                    error: function (exceptionMessage, exceptionType) {
                        debugger;
                        if (exceptionType === ist.exceptionType.CaresGeneralException)
                            toastr.error(exceptionMessage);
                        else
                            toastr.error(ist.resourceText.CitySaveFailError);
                    }
                });
            },
        $scope.editForm = function() {
            $scope.isEdittable = true;
            $scope.cityCopy = angular.copy($scope.city);
        },
        $scope.cancelEditForm = function () {
            $scope.isEdittable = false;
            $scope.city = $scope.cityCopy;
        }
        
    });