var mainApp = angular.module("FRSApp", []);

mainApp.controller("FRSAppController", ['$scope', '$http', '$filter', 'dataService', function ($scope, $http, $filter, dataService) {

    //#region Variables
    $scope.LoadMetaDataId = '';
    $scope.Header = '';
    $scope.Footer = '';
    $scope.Name = '';
    $scope.CurrencyId = '';
    $scope.Description = '';
    $scope.StatusId = '';

    //#endregion

    //#region Get Data from DB
    $scope.getMetaDataList = function () {
        $http.get(ist.siteUrl + '/api/LoadMetaData')
            .success(function (data, status, headers, config) {
                $scope.getMetaDataList = data.LoadMetaDatas;
            });
    }
    //#endregion

    //#region Save Data to DB
    $scope.saveEmployee = function () {
        var employee = {
            EmployeeId: $scope.EmployeeId,
            EmployeeName: $scope.EmployeeName,
            Designation: $scope.Designation
        };
        $http.post(ist.siteUrl + '/api/Employee', employee)
                    .success(function (data, status, headers, config) {
                        if (data != false) {
                            $scope.defaultModel();
                            $scope.getMetaDataList();
                            $scope.IsShowEdit = false;
                        }
                    });
    }
    //#endregion

    //#region Delete Data from DB
    $scope.deleteEmployee = function (employeeId) {
        $http.delete(ist.siteUrl + '/api/Employee', { params: { employeeId: employeeId } })
                    .success(function (data, status, headers, config) {
                        if (data != false) {
                            $scope.getMetaDataList();
                        }
                    });
    }
    //#endregion

    //#region Edit Employee
    $scope.editEmployee = function (employeeId) {
        var emp = $filter('filter')($scope.employeeList, { EmployeeId: employeeId });
        $scope.EmployeeId = emp[0].EmployeeId;
        $scope.EmployeeName = emp[0].EmployeeName;
        $scope.Designation = emp[0].Designation;
        $scope.IsShowEdit = true;
    }
    //#endregion

    //#region Functions
    $scope.showEdit = function () {
        $scope.defaultModel();
        $scope.IsShowEdit = true;
    }

    $scope.onCancelSave = function () {
        $scope.EmployeeName = '';
        $scope.Designation = '';
        $scope.IsShowEdit = false;
    }

    $scope.defaultModel = function () {
        $scope.EmployeeId = 0;
        $scope.EmployeeName = '';
        $scope.Designation = '';
    }
    $scope.getMetaDataList();
    //#endregion
}]);

mainApp.controller("FRSLoadController", ['$scope', '$http', '$filter', 'dataService', 'akFileUploaderService', function ($scope, $http, $filter, dataService, akFileUploaderService) {
    $scope.LoadId;
    $scope.LoadTypeId;
    $scope.MetaDataId;
    $scope.MT940DetailId;
    $scope.Attachment;
    $scope.IsShowEdit = false;
    $scope.LoadMetadataDropDown = [];
    $scope.LoadMetadataId = 0;

    //#region Get Data from DB
    $scope.getLoadList = function () {
        $http.get(ist.siteUrl + '/api/MT940Load')
            .success(function (data, status, headers, config) {
                $scope.loadList = data.Loads;
                $scope.LoadMetadataDropDown = data.LoadMetadataDropDown;
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
        akFileUploaderService.saveModel(load, '/Api/MT940Load');
        //$http.post(ist.siteUrl + '/api/MT940Load', load)
        //    .success(function (data, status, headers, config) {

        //    });
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
        $scope.defaultModel();
        $scope.IsShowEdit = true;
    }

    $scope.onEditCancel = function () {
        $scope.IsShowEdit = false;
    }

    // get data on page load
    $scope.getLoadList();
    //#endregion

}])
.factory('FileUploadService', ['$http', '$q', function ($http, $q) {
    var fac = {};
    fac.UploadFile = function (file, description) {
        var formData = new FormData();
        formData.append("file", file);
        formData.append("description", description);

        var defer = $q.defer;
        $http.post("URL", formData, {
            withCredentials: true,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        })
        .success(function (d) {
            defer.resolve(d);
        })
        .error(function () {
            defer.reject("File Upload Failed!");
        });
        return defer.promise();
    }
}]);