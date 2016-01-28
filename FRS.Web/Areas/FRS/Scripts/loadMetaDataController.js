mainApp.controller("LoadMetaDataController", ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
    //#region Web Model properties
    $scope.LoadMetaDataId;
    $scope.LoadTypeId;
    $scope.SourceId;
    $scope.Header;
    $scope.Footer;
    $scope.Name;
    $scope.CurrencyId;
    $scope.Description;
    $scope.StatusId;
    //#endregion

    //#region DropDowns
    $scope.LoadTypes = [];
    $scope.Sources = [];
    $scope.Currencies = [];
    $scope.Statuses = [];
    //#endregion
    $scope.IsShowEdit = false;
    $scope.Currency = '';
    $scope.IsReadOnly = false;

    //#region Get Data from DB
    $scope.getLoadMetaDataList = function () {
        $http.get(ist.siteUrl + '/api/LoadMetaData')
            .success(function (data, status, headers, config) {
                $scope.loadMetaDataList = data.LoadMetaDatas;
                $scope.LoadTypes = data.LoadTypes;
                $scope.Sources = data.Sources;
                $scope.Currencies = data.Currencies;
                $scope.Statuses = data.Statuses;
            });
    }
    //#endregion

    //#region Post Data
    $scope.saveLoadMetaDataDetail = function () {
        var loadMetaData = {
            LoadMetaDataId: $scope.LoadMetaDataId,
            LoadTypeId: $scope.LoadTypeId,
            SourceId: $scope.SourceId,
            Header: $scope.Header,
            Footer: $scope.Footer,
            Name: $scope.Name,
            CurrencyId: $scope.CurrencyId,
            Description: $scope.Description,
            StatusId: $scope.StatusId
        };
        $http.post(ist.siteUrl + '/api/LoadMetaData', loadMetaData)
            .success(function (data, status, headers, config) {
                if (data != null) {
                    toastr.success("Success");
                }

                console.log(data);
            }).error(function (data, status, headers, config) {
                if (data != null) {
                    toastr.success("Error");
                }
                console.log(data);
            });
    }
    //#endregion

    //#region Edit LoadMetaData
    $scope.editLoadMetaData = function(loadMetaDataId) {
        var metaData = $filter('filter')($scope.loadMetaDataList, { LoadMetaDataId: loadMetaDataId });
        $scope.LoadMetaDataId = metaData[0].LoadMetaDataId;
        $scope.LoadTypeId = metaData[0].LoadTypeId;
        $scope.SourceId = metaData[0].SourceId;
        $scope.Header = metaData[0].Header;
        $scope.Footer = metaData[0].Footer;
        $scope.Name = metaData[0].Name;
        $scope.CurrencyId = metaData[0].CurrencyId;
        $scope.Description = metaData[0].Description;
        $scope.StatusId = metaData[0].StatusId;
        $scope.Currency = metaData[0].Currency;
        $scope.IsReadOnly = false;
        $scope.IsShowEdit = true;
    }
    //#endregion

    //#region Delete Data
    $scope.deleteLoadMetaData = function (loadMetaDataId) {
        $http.delete(ist.siteUrl + '/api/LoadMetaData', { params: { loadMetaDataId: loadMetaDataId } })
                    .success(function (data, status, headers, config) {
                        if (data != false) {
                            $scope.getLoadMetaDataList();
                        }
                    });
    }
    //#endregion

    //#region Functions
    $scope.defaultModel = function () {
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
    }

    $scope.showEdit = function () {
        $scope.IsReadOnly = false;
        $scope.IsShowEdit = true;
    }

    $scope.onEditCancel = function () {
        $scope.defaultModel();
        $scope.IsShowEdit = false;
    }

    // get data on page load
    $scope.getLoadMetaDataList();
    //#endregion

}]);