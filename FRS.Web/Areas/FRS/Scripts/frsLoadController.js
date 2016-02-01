mainApp.controller("FRSLoadController", ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
    // Load Model properties
    $scope.LoadId;
    $scope.LoadMetadataId;
    $scope.MT940LoadId;
    $scope.Attachment;
    $scope.FileName;
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
        $http.get(ist.siteUrl + '/api/MT940Load')
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
            FileName: $scope.FileName
        };
        $http.post(ist.siteUrl + '/api/MT940Load', load)
            .success(function (data, status, headers, config) {
                console.log(data);
            });
    }
    //#endregion

    //#region Delete Data
    $scope.deleteLoad = function (loadId) {
        $http.delete(ist.siteUrl + '/api/MT940Load', { params: { loadId: loadId } })
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

    $scope.showHideFileUploader = function (loadMetadataId) {
        if (loadMetadataId != null) {
            var url = ist.siteUrl + '/api/MT940Load?metaDataId=' + loadMetadataId.Id;
            $http.get(url)
                .success(function(data, status, headers, config) {
                    if (data != null) {
                        $scope.IsLoadTypeMT940 = data.IsLoadTypeMT940;
                        $scope.LoadTypeName = data.LoadType;
                        $scope.SourceName = data.SourceName;
                        $scope.LastModified = data.LastModified;
                    } else {
                        $scope.IsLoadTypeMT940 = false;
                        $scope.LoadTypeName = '';
                        $scope.SourceName = '';
                        $scope.LastModified = '';
                    }
                });
        } else {
            $scope.IsLoadTypeMT940 = false;
            $scope.LoadTypeName = '';
            $scope.SourceName = '';
            $scope.LastModified = '';
        }
    }

    // get data on page load
    $scope.getLoadList();
    //#endregion
    $scope.readPhotoURL = function (input) {
        if (input.files && input.files[0]) {
            $scope.FileName = input.files[0].name;
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
}]);