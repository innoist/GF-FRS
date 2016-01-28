mainApp.controller("FRSLoadController", ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
    $scope.LoadId;
    $scope.LoadTypeId;
    $scope.MetaDataId;
    $scope.MT940DetailId;
    $scope.Attachment;
    $scope.IsShowEdit = false;
    $scope.LoadMetadataDropDown = [];
    $scope.MetaDataWithFileTypes = [];
    $scope.LoadMetadataId = 0;
    // show/hide file uploading facility
    $scope.IsSourceTypeFile = false;

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
            LoadTypeId: $scope.LoadTypeId,
            MetaDataId: $scope.MetaDataId,
            MT940DetailId: $scope.MT940DetailId,
            Attachment: $scope.Attachment,
            LoadMetadataId: $scope.LoadMetadataId
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
        $scope.LoadTypeId = 0;
        $scope.MetaDataId = 0;
        $scope.MT940DetailId = 0;
    }

    $scope.showEdit = function () {
        $scope.IsShowEdit = true;
    }

    $scope.onEditCancel = function () {
        $scope.defaultModel();
        $scope.IsShowEdit = false;
    }

    $scope.showHideFileUploader = function (loadMetadataId) {
        var url = ist.siteUrl + '/api/MT940Load?metaDataId=' + loadMetadataId.Id;
        $http.get(url)
            .success(function (data, status, headers, config) {
                if (data == true) {
                    $scope.IsSourceTypeFile = true;
                } else {
                    $scope.IsSourceTypeFile = false;
                }
            });
    }

    // get data on page load
    $scope.getLoadList();
    //#endregion
    $scope.readPhotoURL = function (input) {
        if (input.files && input.files[0]) {
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