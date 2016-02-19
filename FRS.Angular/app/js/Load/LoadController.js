//(function () {
//    'use strict';

//    angular
//        .module('app.Load', []);
//})();

/**=========================================================
 * Module: load
 * Load view
 =========================================================*/
(function () {
    'use strict';
    //angular
    //    .module('app.Load')
    //    .controller('LoadController', LoadController);
    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('LoadController', LoadController);

    LoadController.$inject = ['$http', '$scope'];

    function LoadController($http, $scope) {
        var vm = this;

        activate();

        function activate() {

            //functionality starts here
            $scope.LoadId;
            $scope.LoadMetadataId;
            $scope.MT940LoadId;
            $scope.Attachment;
            $scope.FileName;
            $scope.FileExtension;
            $scope.IsShowEdit = false;
            $scope.LoadMetadataDropDown = [];
            $scope.MetaDataWithFileTypes = [];
            // readonly properties
            $scope.LoadTypeName;
            $scope.SourceName;
            $scope.LastModified;


            // show/hide file uploading facility
            $scope.IsLoadTypeMT940 = false;

            //#region Get Data from DB
            $scope.getLoadList = function () {
                $http.get(window.frsApiUrl + '/api/Load')
                    .success(function (data, status, headers, config) {
                        $scope.loadList = data.Loads;
                        $scope.LoadMetadataDropDown = data.LoadMetadataDropDown;
                        $scope.MetaDataWithFileTypes = data.MetaDataWithFileTypes;
                    });
            }
            //#endregion

            //#region Post Data
            $scope.saveMT940Detail = function () {
                var load = {
                    LoadId: $scope.LoadId,
                    LoadMetadataId: $scope.LoadMetadataId.Id,
                    Attachment: $scope.Attachment,
                    FileName: $scope.FileName,
                    FileExtension: $scope.FileExtension,
                };
                $http.post(window.frsApiUrl + '/api/Load', load)
                    .success(function (data, status, headers, config) {
                        console.log(data);
                    });
            }
            //#endregion

            //#region Delete Data
            $scope.deleteLoad = function (loadId) {
                $http.delete(window.frsApiUrl + '/api/Load', { params: { loadId: loadId } })
                            .success(function (data, status, headers, config) {
                                if (data != false) {
                                    $scope.getLoadList();
                                }
                            });
            }
            //#endregion

            //#region Functions
            $scope.defaultModel = function () {
                $scope.LoadId = 0;
                $scope.LoadMetadataId = 0;
                $scope.MT940LoadId = 0;
                $scope.Attachment = '';
                $scope.FileName = '';
                $scope.FileExtension = '';
                $scope.IsLoadTypeMT940 = false;
                $scope.LoadTypeName = '';
                $scope.SourceName = '';
                $scope.LastModified = '';
            }

            $scope.showEdit = function () {
                $scope.IsShowEdit = true;
            }

            $scope.onEditCancel = function () {
                $scope.defaultModel();
                $scope.IsShowEdit = false;
            }

            $scope.showHideFileUploader = function (loadMetadata) {
                if (loadMetadata.Id != null) {
                    var url = window.frsApiUrl + '/api/Load?metaDataId=' + loadMetadata.Id;
                    $("#source-sel").text(loadMetadata.Name).append('&nbsp;<b class="caret"></b>');
                    $http.get(url)
                        .success(function (data, status, headers, config) {
                            if (data != null) {
                                $scope.IsLoadTypeMT940 = data.IsLoadTypeMT940;
                                $scope.LoadTypeName = data.LoadType;
                                $scope.SourceName = data.SourceName;
                                $scope.Trailer = data.Trailer;
                                $scope.Header = data.Header;
                                $scope.Currency = data.Currency;

                            } else {
                                $scope.IsLoadTypeMT940 = false;
                                $scope.Trailer = '';
                                $scope.Header = '';
                                $scope.Currency = '';
                            }
                        });
                } else {
                    $scope.IsLoadTypeMT940 = false;
                    $scope.LoadTypeName = '';
                    $scope.SourceName = '';
                    $scope.Trailer = '';
                    $scope.Header = '';
                    $scope.Currency = '';
                }
            }

            // get data on page load
            $scope.getLoadList();
            //#endregion
            $scope.readPhotoURL = function (input) {
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