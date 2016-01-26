mainApp.controller("LoadMetaDataController", ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
    $scope.LoadMetaDataId;
    $scope.LoadTypeId;
    $scope.Header;
    $scope.Name;
    $scope.CurrencyId;
    $scope.Description;
    $scope.IsShowEdit = false;
    $scope.Currency = '';
    $scope.IsReadOnly = false;

    //#region Get Data from DB
    $scope.getLoadMetaDataList = function () {
        $http.get(ist.siteUrl + '/api/LoadMetaData')
            .success(function (data, status, headers, config) {
                $scope.loadMetaDataList = data.LoadMetaDatas;
            });
    }
    //#endregion

    //#region Post Data
    $scope.saveLoadMetaDataDetail = function () {
        var loadMetaData = {
            LoadMetaDataId: $scope.LoadMetaDataId,
            LoadTypeId: $scope.LoadTypeId,
            Header: $scope.Header,
            Name: $scope.Name,
            CurrencyId: $scope.CurrencyId,
            Description: $scope.Description
        };
        $http.post(ist.siteUrl + '/api/LoadMetaData', loadMetaData)
            .success(function (data, status, headers, config) {
                console.log(data);
            });
    }
    //#endregion

    //#region Edit LoadMetaData
    $scope.editLoadMetaData = function(loadMetaDataId) {
        var metaData = $filter('filter')($scope.loadMetaDataList, { LoadMetaDataId: loadMetaDataId });
        $scope.Header = metaData[0].Header;
        $scope.Name = metaData[0].Name;
        $scope.Description = metaData[0].Description;
        $scope.IsReadOnly = true;
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
        $scope.Header = '';
        $scope.Name = '';
        $scope.CurrencyId = 0;
        $scope.Description = '';
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