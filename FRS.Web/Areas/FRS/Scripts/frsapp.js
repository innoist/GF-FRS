//(function () {
//    'use strict';

//    angular.module('myModule', []);
//})();

//(function () {
//    'use strict';

//    angular.module('myModule').controller('SidebarController', SidebarController);

//    SidebarController.$inject = ['$rootScope', '$scope', 'SidebarLoader'];

//    function SidebarController($rootScope, $scope, SidebarLoader) {
//        $scope.FirstName = "Ammar";
//        activate();

//        ////////////////

//        function activate() {
//            var collapseList = [];

//            // demo: when switch from collapse to hover, close all items
//            $rootScope.$watch('app.layout.asideHover', function (oldVal, newVal) {
//                if (newVal === false && oldVal === true) {
//                    closeAllBut(-1);
//                }
//            });


//            // Load menu from json file
//            // ----------------------------------- 

//            SidebarLoader.getMenu(sidebarReady);

//            function sidebarReady(items) {
//                $scope.menuItems = items;
//            }

//            // Handle sidebar and collapse items
//            // ----------------------------------

//            $scope.getMenuItemPropClasses = function (item) {
//                return (item.heading ? 'nav-heading' : '') +
//                       (isActive(item) ? ' active' : '');
//            };

//            $scope.addCollapse = function ($index, item) {
//                collapseList[$index] = $rootScope.app.layout.asideHover ? true : !isActive(item);
//            };

//            $scope.isCollapse = function ($index) {
//                return (collapseList[$index]);
//            };

//            $scope.toggleCollapse = function ($index, isParentItem) {

//                // collapsed sidebar doesn't toggle drodopwn
//                if (Utils.isSidebarCollapsed() || $rootScope.app.layout.asideHover) return true;

//                // make sure the item index exists
//                if (angular.isDefined(collapseList[$index])) {
//                    if (!$scope.lastEventFromChild) {
//                        collapseList[$index] = !collapseList[$index];
//                        closeAllBut($index);
//                    }
//                }
//                else if (isParentItem) {
//                    closeAllBut(-1);
//                }

//                $scope.lastEventFromChild = isChild($index);

//                return true;

//            };

//            // Controller helpers
//            // ----------------------------------- 

//            // Check item and children active state
//            function isActive(item) {

//                if (!item) return;

//                if (!item.sref || item.sref === '#') {
//                    var foundActive = false;
//                    angular.forEach(item.submenu, function (value) {
//                        if (isActive(value)) foundActive = true;
//                    });
//                    return foundActive;
//                }
//                else
//                    return $state.is(item.sref) || $state.includes(item.sref);
//            }

//            function closeAllBut(index) {
//                index += '';
//                for (var i in collapseList) {
//                    if (index < 0 || index.indexOf(i) < 0)
//                        collapseList[i] = true;
//                }
//            }

//            function isChild($index) {
//                /*jshint -W018*/
//                return (typeof $index === 'string') && !($index.indexOf('-') < 0);
//            }

//        }
//    }
//})();

//(function () {
//    'use strict';

//    angular
//        .module('myModule')
//        .service('SidebarLoader', SidebarLoader);

//    SidebarLoader.$inject = ['$http'];
//    function SidebarLoader($http) {
//        this.getMenu = getMenu;

//        ////////////////

//        function getMenu(onReady, onError) {
//            var menuJson = '../../server/sidebar-menu.js',
//                menuURL = menuJson + '?v=' + (new Date().getTime()); // jumps cache

//            onError = onError || function () { alert('Failure loading menu'); };

//            $http
//              .get(menuURL)
//              .success(onReady)
//              .error(onError);
//        }
//    }
//})();

//(function () {
//    'use strict';

//    angular
//        .module('app.dashboard')
//        .controller('DashboardV2Controller', DashboardV2Controller);

//    DashboardV2Controller.$inject = ['$rootScope', '$scope', '$state'];
//    function DashboardV2Controller($rootScope, $scope, $state) {
//        var vm = this;

//        activate();

//        ////////////////

//        function activate() {

//            // Change layout mode
//            if ($state.includes('app-h')) {
//                // Setup layout horizontal for demo
//                $rootScope.app.layout.horizontal = true;
//                $scope.$on('$destroy', function () {
//                    $rootScope.app.layout.horizontal = false;
//                });
//            }
//            else {
//                $rootScope.app.layout.isCollapsed = true;
//            }

//            // BAR STACKED
//            // ----------------------------------- 
//            vm.barStackedOptions = {
//                series: {
//                    stack: true,
//                    bars: {
//                        align: 'center',
//                        lineWidth: 0,
//                        show: true,
//                        barWidth: 0.6,
//                        fill: 0.9
//                    }
//                },
//                grid: {
//                    borderColor: '#eee',
//                    borderWidth: 1,
//                    hoverable: true,
//                    backgroundColor: '#fcfcfc'
//                },
//                tooltip: true,
//                tooltipOpts: {
//                    content: function (label, x, y) { return x + ' : ' + y; }
//                },
//                xaxis: {
//                    tickColor: '#fcfcfc',
//                    mode: 'categories'
//                },
//                yaxis: {
//                    min: 0,
//                    max: 200, // optional: use it for a clear represetation
//                    position: ($rootScope.app.layout.isRTL ? 'right' : 'left'),
//                    tickColor: '#eee'
//                },
//                shadowSize: 0
//            };

//            // SPLINE
//            // ----------------------------------- 

//            vm.splineOptions = {
//                series: {
//                    lines: {
//                        show: false
//                    },
//                    points: {
//                        show: true,
//                        radius: 4
//                    },
//                    splines: {
//                        show: true,
//                        tension: 0.4,
//                        lineWidth: 1,
//                        fill: 0.5
//                    }
//                },
//                grid: {
//                    borderColor: '#eee',
//                    borderWidth: 1,
//                    hoverable: true,
//                    backgroundColor: '#fcfcfc'
//                },
//                tooltip: true,
//                tooltipOpts: {
//                    content: function (label, x, y) { return x + ' : ' + y; }
//                },
//                xaxis: {
//                    tickColor: '#fcfcfc',
//                    mode: 'categories'
//                },
//                yaxis: {
//                    min: 0,
//                    max: 150, // optional: use it for a clear represetation
//                    tickColor: '#eee',
//                    position: ($rootScope.app.layout.isRTL ? 'right' : 'left'),
//                    tickFormatter: function (v) {
//                        return v/* + ' visitors'*/;
//                    }
//                },
//                shadowSize: 0
//            };
//        }
//    }
//})();

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