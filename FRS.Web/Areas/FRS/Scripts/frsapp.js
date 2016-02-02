//(function () {
//    'use strict';

//    angular
//        .module('asdf', []);
//})();

//(function () {
//    'use strict';

//    angular
//        .module('FRS.sidebar', []);
//})();

//(function () {
//    'use strict';

//    angular
//        .module('FRS.sidebar')
//        .controller('SidebarController', SidebarController);

//    SidebarController.$inject = ['$rootScope', '$scope', '$state', 'SidebarLoader', 'Utils'];
//    function SidebarController($rootScope, $scope, $state, SidebarLoader, Utils) {

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

//        } // activate
//    }

//})();

//(function () {
//    'use strict';

//    angular
//        .module('FRS.sidebar')
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
//        .module('FRS.utils')
//        .service('Utils', Utils);

//    Utils.$inject = ['$window', 'APP_MEDIAQUERY'];
//    function Utils($window, APP_MEDIAQUERY) {

//        var $html = angular.element('html'),
//            $win = angular.element($window),
//            $body = angular.element('body');

//        return {
//            // DETECTION
//            support: {
//                transition: (function () {
//                    var transitionEnd = (function () {

//                        var element = document.body || document.documentElement,
//                            transEndEventNames = {
//                                WebkitTransition: 'webkitTransitionEnd',
//                                MozTransition: 'transitionend',
//                                OTransition: 'oTransitionEnd otransitionend',
//                                transition: 'transitionend'
//                            }, name;

//                        for (name in transEndEventNames) {
//                            if (element.style[name] !== undefined) return transEndEventNames[name];
//                        }
//                    }());

//                    return transitionEnd && { end: transitionEnd };
//                })(),
//                animation: (function () {

//                    var animationEnd = (function () {

//                        var element = document.body || document.documentElement,
//                            animEndEventNames = {
//                                WebkitAnimation: 'webkitAnimationEnd',
//                                MozAnimation: 'animationend',
//                                OAnimation: 'oAnimationEnd oanimationend',
//                                animation: 'animationend'
//                            }, name;

//                        for (name in animEndEventNames) {
//                            if (element.style[name] !== undefined) return animEndEventNames[name];
//                        }
//                    }());

//                    return animationEnd && { end: animationEnd };
//                })(),
//                requestAnimationFrame: window.requestAnimationFrame ||
//                                       window.webkitRequestAnimationFrame ||
//                                       window.mozRequestAnimationFrame ||
//                                       window.msRequestAnimationFrame ||
//                                       window.oRequestAnimationFrame ||
//                                       function (callback) { window.setTimeout(callback, 1000 / 60); },
//                /*jshint -W069*/
//                touch: (
//                    ('ontouchstart' in window && navigator.userAgent.toLowerCase().match(/mobile|tablet/)) ||
//                    (window.DocumentTouch && document instanceof window.DocumentTouch) ||
//                    (window.navigator['msPointerEnabled'] && window.navigator['msMaxTouchPoints'] > 0) || //IE 10
//                    (window.navigator['pointerEnabled'] && window.navigator['maxTouchPoints'] > 0) || //IE >=11
//                    false
//                ),
//                mutationobserver: (window.MutationObserver || window.WebKitMutationObserver || window.MozMutationObserver || null)
//            },
//            // UTILITIES
//            isInView: function (element, options) {
//                /*jshint -W106*/
//                var $element = $(element);

//                if (!$element.is(':visible')) {
//                    return false;
//                }

//                var window_left = $win.scrollLeft(),
//                    window_top = $win.scrollTop(),
//                    offset = $element.offset(),
//                    left = offset.left,
//                    top = offset.top;

//                options = $.extend({ topoffset: 0, leftoffset: 0 }, options);

//                if (top + $element.height() >= window_top && top - options.topoffset <= window_top + $win.height() &&
//                    left + $element.width() >= window_left && left - options.leftoffset <= window_left + $win.width()) {
//                    return true;
//                } else {
//                    return false;
//                }
//            },

//            langdirection: $html.attr('dir') === 'rtl' ? 'right' : 'left',

//            isTouch: function () {
//                return $html.hasClass('touch');
//            },

//            isSidebarCollapsed: function () {
//                return $body.hasClass('aside-collapsed');
//            },

//            isSidebarToggled: function () {
//                return $body.hasClass('aside-toggled');
//            },

//            isMobile: function () {
//                return $win.width() < APP_MEDIAQUERY.tablet;
//            }

//        };
//    }
//})();

//(function () {
//    'use strict';

//    angular
//        .module('FRS.core')
//        .constant('APP_MEDIAQUERY', {
//            'desktopLG': 1200,
//            'desktop': 992,
//            'tablet': 768,
//            'mobile': 480
//        })
//    ;

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