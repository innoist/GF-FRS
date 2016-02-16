(function () {
    'use strict';

    angular
        .module('app.CreateMetaData', []);
})();

//Create MetaData
(function () {

    angular
        .module('app.CreateMetaData', [])
        .controller('CreateMetaDataController', CreateMetaDataController);

    CreateMetaDataController.$inject = ['$http', '$scope', '$state', 'CreateMetaDataService', 'SweetAlert', 'toaster'];

    function CreateMetaDataController($http, $scope, $state, CreateMetaDataService, SweetAlert, toaster) {
        var vm = this;
        function activate() {
            // Load base data
            $scope.GetBaseData();
        }

        //#region Web Model properties
        vm.LoadMetaData = {

        }
        //#endregion
        vm.submitted = false;
        //vm.submitForm = function () {

        //};
        //#region DropDowns
        $scope.LoadTypes = [];
        $scope.Sources = [];
        $scope.Currencies = [];
        $scope.Statuses = [];
        //#endregion
        //$scope.IsShowEdit = false;
        $scope.Currency = '';
        $scope.IsReadOnly = false;

        //#region Get Data from DB
        $scope.GetBaseData = function () {
            CreateMetaDataService.getLoadMetaData(onSuccess);
            function onSuccess(data) {
                //vm.gridOptions.data = data.LoadMetaDatas;
                $scope.LoadTypes = data.LoadTypes;
                $scope.Sources = data.Sources;
                $scope.Currencies = data.Currencies;
                $scope.Statuses = data.Statuses;
            }
        }
        //#endregion
        vm.validateInput = function (property, type) {
            if (!property || !type) {
                return false;
            }
            return (property.$dirty || vm.submitted) && property.$error[type];
        };
        //#region Post Data
        $scope.saveLoadMetaDataDetail = function (isNew) {
            vm.submitted = true;
            if (vm.formValidate.$valid) {
                console.log('Submitted!!');
            } else {
                toaster.pop("error", "Fields are required", "Notification");
                return false;
            }
            vm.LoadMetaData.Header = $scope.Header;
            vm.LoadMetaData.Footer = $scope.Footer;
            vm.LoadMetaData.Name = $scope.Name;
            vm.LoadMetaData.Description = $scope.Description;

            CreateMetaDataService.saveLoadMetaDataDetail(vm.LoadMetaData, onSuccess, onError);
            function onSuccess(response) {
                debugger;
                if (response.data == true) {
                    toaster.pop("success", "Metadata Saved successfully", "Notification");
                    //$scope.GetBaseData();
                    //(function () {
                    //    SweetAlert.swal({
                    //        title: 'Done !',
                    //        text: 'Record has been Saved Successfully.',
                    //        type: 'success',
                    //        //showCancelButton: true,
                    //        //confirmButtonColor: '#DD6B55',
                    //        //confirmButtonText: 'Yes, delete it!',
                    //        //closeOnConfirm: false
                    //    //}, function () {
                    //    //    SweetAlert.swal('Booyah!');
                    //    });
                    //})();

                }
            }
            function onError(err) {
                toaster.error(err.statusText, err.data.Message);
                showErrors(err);
                //(function () {
                //    SweetAlert.swal({
                //        title: 'Alas !',
                //        text: 'Something went wrong.',
                //        type: 'error',
                //        //showCancelButton: true,
                //        //confirmButtonColor: '#DD6B55',
                //        //confirmButtonText: 'Yes, delete it!',
                //        //closeOnConfirm: false
                //        //}, function () {
                //        //    SweetAlert.swal('Booyah!');
                //    });
                //})();
            }

            if (isNew) {

                $state.go('app.CreateMetaData');
            }
            if (!isNew) {
                $state.go('app.LoadMetaData');
            }
            defaultModel();
        }
        //#endregion

        //#region Edit LoadMetaData
        $scope.editLoadMetaData = function (loadMetaDataId) {
            $http.get(window.frsApiUrl + '/api/LoadMetaData/' + loadMetaDataId)
                .success(function (data) {
                    if (data != null) {
                        vm.LoadMetaData = data;
                        $("#load-type-sel").text(data.LoadType).append('&nbsp;<b class="caret"></b>');
                        $("#source-sel").text(data.Source).append('&nbsp;<b class="caret"></b>');
                        $("#currency-sel").text(data.Currency).append('&nbsp;<b class="caret"></b>');
                        $("#status-sel").text(data.Status).append('&nbsp;<b class="caret"></b>');
                    }
                });
        }
        //#endregion

        //#region Delete Data
        $scope.deleteLoadMetaData = function (loadMetaDataId) {
            $http.delete('/api/LoadMetaData', { params: { loadMetaDataId: loadMetaDataId } })
                        .success(function (data, status, headers, config) {
                            if (data != false) {
                                $scope.getLoadMetaDataList();
                            }
                        });
        }
        //#endregion

        //#region Functions
        var defaultModel = function () {
            $scope.LoadMetaDataId = 0;
            $scope.LoadTypeId = 0;
            $scope.SourceId = 0;
            $scope.Header = '';
            $scope.Footer = '';
            $scope.Name = '';
            $scope.CurrencyId = 0;
            $scope.Description = '';
            $scope.StatusId = 0;
            $scope.Currency = '';
            vm.submitted = false;
        }

        $scope.showEdit = function () {
            $scope.IsReadOnly = false;
            //$scope.IsShowEdit = true;
            $scope.editLoadMetaData(5);
        }

        $scope.LoadTypeChange = function (load) {
            vm.LoadMetaData.LoadTypeId = load.Id;
            $("#load-type-sel").text(load.Name).append('&nbsp;<b class="caret"></b>');
        }

        $scope.SourceChange = function (source) {
            vm.LoadMetaData.SourceId = source.Id;
            $("#source-sel").text(source.Name).append('&nbsp;<b class="caret"></b>');
        }

        $scope.CurrencyChange = function (currency) {
            vm.LoadMetaData.CurrencyId = currency.Id;
            $("#currency-sel").text(currency.Name).append('&nbsp;<b class="caret"></b>');
        }

        $scope.StatusChange = function (status) {
            vm.LoadMetaData.StatusId = status.Id;
            $("#status-sel").text(status.Name).append('&nbsp;<b class="caret"></b>');
        }

        activate();
    }


})();