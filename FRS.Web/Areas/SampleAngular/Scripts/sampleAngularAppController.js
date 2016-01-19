var mainApp = angular.module("SampleAngularApp", []);

mainApp.controller("SampleAngularAppController", ['$scope', '$http', '$filter', 'dataService', function ($scope, $http, $filter, dataService) {

    //#region Variables
    $scope.EmployeeId = '';
    $scope.EmployeeName = '';
    $scope.Designation = '';
    $scope.IsShowEdit = false;

    //#endregion

    //#region Get Data from DB
    $scope.getEmployeeList = function () {
        $http.get(ist.siteUrl + '/api/Employee')
            .success(function (data, status, headers, config) {
                $scope.employeeList = data.Employees;
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
                            $scope.getEmployeeList();
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
                            $scope.getEmployeeList();
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
    $scope.getEmployeeList();
    //#endregion
}]);