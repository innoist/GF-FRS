/**=========================================================
 * Module: load
 * Load view
 =========================================================*/
(function () {
    'use strict';

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('LoadController', LoadController);

    LoadController.$inject = ['$http', '$scope', 'SweetAlert'];

    function LoadController($http, $scope, SweetAlert) {
        var vm = this;

        activate();

        function activate() {

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
                        vm.LoadMetaDatas = data.LoadMetadataDropDown;
                        $scope.MetaDataWithFileTypes = data.MetaDataWithFileTypes;
                    });
            }

            $scope.finalize = function () {
                debugger;
                //var file = $('#loadFile').get(0).files[0];
                var file = vm.loadFile;
                var fd = new FormData();
                fd.append('loadFile', file);

                var load = {
                    LoadMetadataId: $scope.LoadMetadataId,
                    File: fd
                };

                $http({
                    url: window.frsApiUrl + '/api/Load',
                    data: load,
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' }
                })
                    .then(function (data) {
                        SweetAlert.swal({
                            title: 'Success',
                            text: 'Load Created Successfully.',
                            type: 'success'
                        });
                        console.log(data);
                    }, function(err) {
                        showErrors(err);
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
                }else{
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

            //#region Post Data
            
            //#endregion

            //#region Functions

            // get data on page load
            $scope.getLoadList();
            //#endregion
            $scope.readPhotoURL = function (input) {
                debugger;
                if (input.files && input.files[0]) {
                    $scope.FileName = input.files[0].name;
                    $scope.FileExtension = input.files[0].type.split('/')[1];
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var img = new Image;
                        img.onload = function () {
                            if (img.height > 250 || img.width > 250) {
                                //   toastr.error("Image Max. width 1280 and height 1024px; please resize the image and try again");
                            } else {
                                $('#vehicleImage')
                                    .attr('src', e.target.result)
                                    .width(120)
                                    .height(120);
                            }
                        };
                        img.src = reader.result;
                        $scope.Attachment = img.src;
                    };
                    reader.readAsDataURL(input.files[0]);
                }
            }
            

        }
    }
})();