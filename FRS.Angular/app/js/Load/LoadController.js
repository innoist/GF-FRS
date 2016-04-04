/**=========================================================
 * Module: load
 * Load view
 =========================================================*/
(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('LoadController', LoadController);

    LoadController.$inject = ['$http', '$scope', '$stateParams', '$state', 'SweetAlert'];

    function LoadController($http, $scope, $stateParams, $state, SweetAlert) {
        var vm = this;

        activate();

        function activate() {
            var loadType = $stateParams.Type;
            if (loadType == "OracleGL")
                loadType = "CSV";

            vm.validateInput = function (property, type) {
                if (!property || !type) {
                    return false;
                }
                return (property.$dirty || vm.submitted) && property.$error[type];
            };

            $scope.clear = function ($event) {
                $event.stopPropagation();
                vm.LoadMetaDatas.selected = null;
            };
            // show/hide file uploading facility
            $scope.IsLoadTypeMT940 = false;

            //#region Get Data from DB
            $scope.getLoadList = function () {
                $http.get(window.frsApiUrl + '/api/Load')
                    .success(function (data) {
                        $scope.loadList = data.Loads;
                        if (loadType != "") {
                            vm.LoadMetaDatas = data.LoadMetadataDropDown.filter(function (item) {
                                return item.Type === loadType;
                            });
                        } else {
                            vm.LoadMetaDatas = data.LoadMetadataDropDown;
                        }
                        
                        $scope.MetaDataWithFileTypes = data.MetaDataWithFileTypes;
                    });
            }

            $scope.finalize = function () {

                var load = {
                    LoadMetadataId: $scope.LoadMetadataId,
                    FileBase64Content: $scope.Attachment,
                    FileName: $scope.FileName,
                    FileExtension: $scope.FileExtension,
                    Name: vm.LoadName,
                    LoadType: vm.LoadMetaDatas.selected.Type

                };
                $http.post(window.frsApiUrl + '/api/Load', load)
                    .then(function (data) {
                        SweetAlert.swal({
                            title: 'Success',
                            text: 'Load Created Successfully.',
                            type: 'success'
                        });
                        
                        if ($stateParams.Type == "MT940")
                            $state.go('app.MT940Loads');
                        else
                            $state.go('app.OracleGlLoad');

                    }, function (err) {
                        SweetAlert.swal({
                            title: 'Error',
                            text: 'Load Creation Failed.',
                            type: 'error'
                        });
                        window.location.reload();
                    });

            }
            // Wire Source Change
            $scope.$watch("ldc.LoadMetaDatas.selected", function (newValue, oldValue) {
                if (newValue) {
                    var url = window.frsApiUrl + '/api/Load?metaDataId=' + newValue.Id;
                    $http.get(url)
                        .success(function (data) {
                            if (data != null) {
                                $scope.IsLoadTypeMT940 = data.IsLoadTypeMT940;
                                $scope.LoadTypeName = data.LoadType;
                                $scope.SourceName = data.SourceName;
                                $scope.Trailer = data.Trailer;
                                $scope.Header = data.Header;
                                $scope.Currency = data.Currency;
                                $scope.LoadMetadataId = newValue.Id;
                            }
                        });
                } else {
                    $scope.IsLoadTypeMT940 = false;
                    $scope.Trailer = '';
                    $scope.Header = '';
                    $scope.Currency = '';
                    $scope.LoadTypeName = '';
                    $scope.SourceName = '';
                    $scope.LoadMetadataId = 0;
                }
            });
            //#endregion

            // get data on page load
            $scope.getLoadList();
            //#endregion
            $scope.readFile = function (input) {
                if (input.files && input.files[0]) {
                    $scope.FileName = input.files[0].name.split('.')[0];
                    $scope.FileExtension = input.files[0].name.split('.')[1];
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $scope.Attachment = reader.result;
                    };
                    reader.readAsDataURL(input.files[0]);
                }
            }


        }
    }
})();