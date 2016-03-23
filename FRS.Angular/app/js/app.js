/*!
 * 
 * Angle - Bootstrap Admin App + AngularJS
 * 
 * Version: 3.2.0
 * Author: @themicon_co
 * Website: http://themicon.co
 * License: https://wrapbootstrap.com/help/licenses
 * 
 */

(function () {
    'use strict';

    angular
        .module('app.bootstrapui', []);
})();
(function () {
    'use strict';

    angular
        .module('app.charts', []);
})();
(function () {
    'use strict';

    angular
        .module('app.colors', []);
})();
(function () {
    'use strict';

    angular
        .module('app.core', [
            'ngRoute',
            'ngAnimate',
            'ngStorage',
            'ngCookies',
            'pascalprecht.translate',
            'ui.bootstrap',
            'ui.router',
            'oc.lazyLoad',
            'cfp.loadingBar',
            'ngSanitize',
            'ngResource',
            'tmh.dynamicLocale',
            'ui.utils'
        ]);
})();
(function () {
    'use strict';

    angular
        .module('app.dashboard', ['$localStorage', '$rootScope']);
})();
(function () {
    'use strict';

    angular
        .module('app.elements', []);
})();
(function () {
    'use strict';

    angular
        .module('app.flatdoc', []);
})();
(function () {
    'use strict';

    angular
        .module('app.extras', []);
})();
(function () {
    'use strict';

    angular
        .module('app.forms', []);
})();
(function () {
    'use strict';

    angular
        .module('app.icons', []);
})();
(function () {
    'use strict';

    angular
        .module('app.lazyload', []);
})();
(function () {
    'use strict';

    angular
        .module('app.loadingbar', []);
})();
(function () {
    'use strict';

    angular
        .module('app.locale', []);
})();
(function () {
    'use strict';

    angular
        .module('app.mailbox', []);
})();
(function () {
    'use strict';

    angular
        .module('app.maps', []);
})();
(function () {
    'use strict';

    angular
        .module('app.navsearch', []);
})();
(function () {
    'use strict';

    angular
        .module('app.notify', []);
})();
(function () {
    'use strict';

    angular
        .module('app.pages', []);
})();
(function () {
    'use strict';

    angular
        .module('app.panels', []);
})();
(function () {
    'use strict';

    angular
        .module('app.preloader', []);
})();


(function () {
    'use strict';

    angular
        .module('app.routes', [
            'app.lazyload'
        ]);
})();
(function () {
    'use strict';

    angular
        .module('app.settings', []);
})();
(function () {
    'use strict';

    angular
        .module('app.tables', []);
})();
(function () {
    'use strict';

    angular
        .module('app.sidebar', []);
})();
(function () {
    'use strict';

    angular
        .module('app.translate', []);
})();
(function () {
    'use strict';

    angular
        .module('app.utils', [
          'app.colors'
        ]);
})();

(function () {
    'use strict';

    angular
        .module('app.bootstrapui')
        .config(bootstrapuiConfig);

    bootstrapuiConfig.$inject = ['$uibTooltipProvider'];
    function bootstrapuiConfig($uibTooltipProvider) {
        $uibTooltipProvider.options({ appendToBody: true });
    }
})();


/**=========================================================
 * Module: modals.js
 * Provides a simple way to implement bootstrap modals from templates
 =========================================================*/
(function () {
    'use strict';

    angular
        .module('app.bootstrapui')
        .controller('ModalController', ModalController);

    ModalController.$inject = ['$uibModal'];
    function ModalController($uibModal) {
        var vm = this;

        activate();

        ////////////////

        function activate() {

            vm.open = function (size) {

                var modalInstance = $uibModal.open({
                    templateUrl: '/myModalContent.html',
                    controller: ModalInstanceCtrl,
                    size: size
                });

                var state = $('#modal-state');
                modalInstance.result.then(function () {
                    state.text('Modal dismissed with OK status');
                }, function () {
                    state.text('Modal dismissed with Cancel status');
                });
            };

            // Please note that $uibModalInstance represents a modal window (instance) dependency.
            // It is not the same as the $uibModal service used above.

            ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance'];
            function ModalInstanceCtrl($scope, $uibModalInstance) {

                $scope.ok = function () {
                    $uibModalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                };
            }
        }
    }

})();

/**=========================================================
 * Module: chart.js
 * Wrapper directive for chartJS. 
 * Based on https://gist.github.com/AndreasHeiberg/9837868
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.charts')
        /* Aliases for various chart types */
        .directive('linechart', chartJS('Line'))
        .directive('barchart', chartJS('Bar'))
        .directive('radarchart', chartJS('Radar'))
        .directive('polarchart', chartJS('PolarArea'))
        .directive('piechart', chartJS('Pie'))
        .directive('doughnutchart', chartJS('Doughnut'))
        .directive('donutchart', chartJS('Doughnut'))
    ;

    function chartJS(type) {
        return function () {
            return {
                restrict: 'A',
                scope: {
                    data: '=',
                    options: '=',
                    id: '@',
                    width: '=',
                    height: '=',
                    resize: '=',
                    chart: '@',
                    segments: '@',
                    responsive: '=',
                    tooltip: '=',
                    legend: '='
                },
                link: function ($scope, $elem) {
                    var ctx = $elem[0].getContext('2d');
                    var autosize = false;

                    $scope.size = function () {
                        if ($scope.width <= 0) {
                            $elem.width($elem.parent().width());
                            ctx.canvas.width = $elem.width();
                        } else {
                            ctx.canvas.width = $scope.width || ctx.canvas.width;
                            autosize = true;
                        }

                        if ($scope.height <= 0) {
                            $elem.height($elem.parent().height());
                            ctx.canvas.height = ctx.canvas.width / 2;
                        } else {
                            ctx.canvas.height = $scope.height || ctx.canvas.height;
                            autosize = true;
                        }
                    };

                    $scope.$watch('data', function (newVal) {
                        if (chartCreated)
                            chartCreated.destroy();

                        // if data not defined, exit
                        if (!newVal) {
                            return;
                        }
                        if ($scope.chart) { type = $scope.chart; }

                        if (autosize) {
                            $scope.size();
                            chart = new Chart(ctx);
                        }

                        if ($scope.responsive || $scope.resize)
                            $scope.options.responsive = true;

                        if ($scope.responsive !== undefined)
                            $scope.options.responsive = $scope.responsive;

                        chartCreated = chart[type]($scope.data, $scope.options);
                        chartCreated.update();
                        if ($scope.legend)
                            angular.element($elem[0]).parent().after(chartCreated.generateLegend());
                    }, true);

                    $scope.$watch('tooltip', function (newVal) {
                        if (chartCreated)
                            chartCreated.draw();
                        if (newVal === undefined || !chartCreated.segments)
                            return;
                        if (!isFinite(newVal) || newVal >= chartCreated.segments.length || newVal < 0)
                            return;
                        var activeSegment = chartCreated.segments[newVal];
                        activeSegment.save();
                        activeSegment.fillColor = activeSegment.highlightColor;
                        chartCreated.showTooltip([activeSegment]);
                        activeSegment.restore();
                    }, true);

                    $scope.size();
                    var chart = new Chart(ctx);
                    var chartCreated;
                }
            };
        };
    }
})();


(function() {
    var myApp = angular.module('app.forms');

    myApp.directive('fileModel', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var model = $parse(attrs.fileModel);
                var modelSetter = model.assign;

                element.bind('change', function () {
                    scope.$apply(function () {
                        modelSetter(scope, element[0].files[0]);
                    });
                });
            }
        };
    }]);
})();


/**=========================================================
 * Module: classy-loader.js
 * Enable use of classyloader directly from data attributes
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.charts')
        .directive('classyloader', classyloader);

    classyloader.$inject = ['$timeout', 'Utils', '$window'];
    function classyloader($timeout, Utils, $window) {
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element) {
            var $scroller = $($window),
                inViewFlagClass = 'js-is-in-view'; // a classname to detect when a chart has been triggered after scroll

            // run after interpolation  
            $timeout(function () {

                var $element = $(element),
                    options = $element.data();

                // At lease we need a data-percentage attribute
                if (options) {
                    if (options.triggerInView) {

                        $scroller.scroll(function () {
                            checkLoaderInVIew($element, options);
                        });
                        // if the element starts already in view
                        checkLoaderInVIew($element, options);
                    }
                    else
                        startLoader($element, options);
                }

            }, 0);

            function checkLoaderInVIew(element, options) {
                var offset = -20;
                if (!element.hasClass(inViewFlagClass) &&
                    Utils.isInView(element, { topoffset: offset })) {
                    startLoader(element, options);
                }
            }
            function startLoader(element, options) {
                element.ClassyLoader(options).addClass(inViewFlagClass);
            }
        }
    }

})();

(function () {
    'use strict';

    angular
        .module('app.charts')
        .service('ChartData', ChartData);

    ChartData.$inject = ['$resource'];
    function ChartData($resource) {
        this.load = load;

        ////////////////

        var opts = {
            get: { method: 'GET', isArray: true }
        };
        function load(source) {
            return $resource(source, {}, opts).get();
        }
    }
})();

/**=========================================================
 * Module: flot-chart.js
 * Setup options and data for flot chart directive
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.charts')
        .controller('FlotChartController', FlotChartController);

    FlotChartController.$inject = ['$scope', 'ChartData', '$timeout'];
    function FlotChartController($scope, ChartData, $timeout) {
        var vm = this;

        activate();

        ////////////////

        function activate() {

            // BAR
            // -----------------------------------
            vm.barData = ChartData.load('server/chart/bar.js');
            vm.barOptions = {
                series: {
                    bars: {
                        align: 'center',
                        lineWidth: 0,
                        show: true,
                        barWidth: 0.6,
                        fill: 0.9
                    }
                },
                grid: {
                    borderColor: '#eee',
                    borderWidth: 1,
                    hoverable: true,
                    backgroundColor: '#fcfcfc'
                },
                tooltip: true,
                tooltipOpts: {
                    content: function (label, x, y) { return x + ' : ' + y; }
                },
                xaxis: {
                    tickColor: '#fcfcfc',
                    mode: 'categories'
                },
                yaxis: {
                    position: ($scope.app.layout.isRTL ? 'right' : 'left'),
                    tickColor: '#eee'
                },
                shadowSize: 0
            };

            // BAR STACKED
            // -----------------------------------
            vm.barStackeData = ChartData.load('server/chart/barstacked.js');
            vm.barStackedOptions = {
                series: {
                    stack: true,
                    bars: {
                        align: 'center',
                        lineWidth: 0,
                        show: true,
                        barWidth: 0.6,
                        fill: 0.9
                    }
                },
                grid: {
                    borderColor: '#eee',
                    borderWidth: 1,
                    hoverable: true,
                    backgroundColor: '#fcfcfc'
                },
                tooltip: true,
                tooltipOpts: {
                    content: function (label, x, y) { return x + ' : ' + y; }
                },
                xaxis: {
                    tickColor: '#fcfcfc',
                    mode: 'categories'
                },
                yaxis: {
                    min: 0,
                    max: 200, // optional: use it for a clear represetation
                    position: ($scope.app.layout.isRTL ? 'right' : 'left'),
                    tickColor: '#eee'
                },
                shadowSize: 0
            };

            // SPLINE
            // -----------------------------------
            vm.splineData = ChartData.load('server/chart/spline.js');
            vm.splineOptions = {
                series: {
                    lines: {
                        show: false
                    },
                    points: {
                        show: true,
                        radius: 4
                    },
                    splines: {
                        show: true,
                        tension: 0.4,
                        lineWidth: 1,
                        fill: 0.5
                    }
                },
                grid: {
                    borderColor: '#eee',
                    borderWidth: 1,
                    hoverable: true,
                    backgroundColor: '#fcfcfc'
                },
                tooltip: true,
                tooltipOpts: {
                    content: function (label, x, y) { return x + ' : ' + y; }
                },
                xaxis: {
                    tickColor: '#fcfcfc',
                    mode: 'categories'
                },
                yaxis: {
                    min: 0,
                    max: 150, // optional: use it for a clear represetation
                    tickColor: '#eee',
                    position: ($scope.app.layout.isRTL ? 'right' : 'left'),
                    tickFormatter: function (v) {
                        return v/* + ' visitors'*/;
                    }
                },
                shadowSize: 0
            };

            // AREA
            // -----------------------------------
            vm.areaData = ChartData.load('server/chart/area.js');
            vm.areaOptions = {
                series: {
                    lines: {
                        show: true,
                        fill: 0.8
                    },
                    points: {
                        show: true,
                        radius: 4
                    }
                },
                grid: {
                    borderColor: '#eee',
                    borderWidth: 1,
                    hoverable: true,
                    backgroundColor: '#fcfcfc'
                },
                tooltip: true,
                tooltipOpts: {
                    content: function (label, x, y) { return x + ' : ' + y; }
                },
                xaxis: {
                    tickColor: '#fcfcfc',
                    mode: 'categories'
                },
                yaxis: {
                    min: 0,
                    tickColor: '#eee',
                    position: ($scope.app.layout.isRTL ? 'right' : 'left'),
                    tickFormatter: function (v) {
                        return v + ' visitors';
                    }
                },
                shadowSize: 0
            };

            // LINE
            // -----------------------------------
            vm.lineData = ChartData.load('server/chart/line.js');
            vm.lineOptions = {
                series: {
                    lines: {
                        show: true,
                        fill: 0.01
                    },
                    points: {
                        show: true,
                        radius: 4
                    }
                },
                grid: {
                    borderColor: '#eee',
                    borderWidth: 1,
                    hoverable: true,
                    backgroundColor: '#fcfcfc'
                },
                tooltip: true,
                tooltipOpts: {
                    content: function (label, x, y) { return x + ' : ' + y; }
                },
                xaxis: {
                    tickColor: '#eee',
                    mode: 'categories'
                },
                yaxis: {
                    position: ($scope.app.layout.isRTL ? 'right' : 'left'),
                    tickColor: '#eee'
                },
                shadowSize: 0
            };

            // PIE
            // -----------------------------------
            vm.pieData = [{
                "label": "jQuery",
                "color": "#4acab4",
                "data": 30
            }, {
                "label": "CSS",
                "color": "#ffea88",
                "data": 40
            }, {
                "label": "LESS",
                "color": "#ff8153",
                "data": 90
            }, {
                "label": "SASS",
                "color": "#878bb6",
                "data": 75
            }, {
                "label": "Jade",
                "color": "#b2d767",
                "data": 120
            }];
            // Direct data temporarily added until fix: https://github.com/flot/flot/pull/1462
            // ChartData.load('server/chart/pie.js');

            vm.pieOptions = {
                series: {
                    pie: {
                        show: true,
                        innerRadius: 0,
                        label: {
                            show: true,
                            radius: 0.8,
                            formatter: function (label, series) {
                                return '<div class="flot-pie-label">' +
                                //label + ' : ' +
                                Math.round(series.percent) +
                                '%</div>';
                            },
                            background: {
                                opacity: 0.8,
                                color: '#222'
                            }
                        }
                    }
                }
            };

            // DONUT
            // -----------------------------------
            vm.donutData = [{
                "color": "#39C558",
                "data": 60,
                "label": "Coffee"
            },
                {
                    "color": "#00b4ff",
                    "data": 90,
                    "label": "CSS"
                },
                {
                    "color": "#FFBE41",
                    "data": 50,
                    "label": "LESS"
                },
                {
                    "color": "#ff3e43",
                    "data": 80,
                    "label": "Jade"
                },
                {
                    "color": "#937fc7",
                    "data": 116,
                    "label": "AngularJS"
                }
            ];
            // Direct data temporarily added until fix: https://github.com/flot/flot/pull/1462
            // ChartData.load('server/chart/donut.js');

            vm.donutOptions = {
                series: {
                    pie: {
                        show: true,
                        innerRadius: 0.5 // This makes the donut shape
                    }
                }
            };

            // REALTIME
            // -----------------------------------
            vm.realTimeOptions = {
                series: {
                    lines: { show: true, fill: true, fillColor: { colors: ['#a0e0f3', '#23b7e5'] } },
                    shadowSize: 0 // Drawing is faster without shadows
                },
                grid: {
                    show: false,
                    borderWidth: 0,
                    minBorderMargin: 20,
                    labelMargin: 10
                },
                xaxis: {
                    tickFormatter: function () {
                        return '';
                    }
                },
                yaxis: {
                    min: 0,
                    max: 110
                },
                legend: {
                    show: true
                },
                colors: ['#23b7e5']
            };

            // Generate random data for realtime demo
            var data = [], totalPoints = 300;

            update();

            function getRandomData() {
                if (data.length > 0)
                    data = data.slice(1);
                // Do a random walk
                while (data.length < totalPoints) {
                    var prev = data.length > 0 ? data[data.length - 1] : 50,
                      y = prev + Math.random() * 10 - 5;
                    if (y < 0) {
                        y = 0;
                    } else if (y > 100) {
                        y = 100;
                    }
                    data.push(y);
                }
                // Zip the generated y values with the x values
                var res = [];
                for (var i = 0; i < data.length; ++i) {
                    res.push([i, data[i]]);
                }
                return [res];
            }
            function update() {
                vm.realTimeData = getRandomData();
                $timeout(update, 30);
            }
            // end random data generation


            // PANEL REFRESH EVENTS
            // -----------------------------------

            $scope.$on('panel-refresh', function (event, id) {

                console.log('Simulating chart refresh during 3s on #' + id);

                // Instead of timeout you can request a chart data
                $timeout(function () {

                    // directive listen for to remove the spinner
                    // after we end up to perform own operations
                    $scope.$broadcast('removeSpinner', id);

                    console.log('Refreshed #' + id);

                }, 3000);

            });


            // PANEL DISMISS EVENTS
            // -----------------------------------

            // Before remove panel
            $scope.$on('panel-remove', function (event, id, deferred) {

                console.log('Panel #' + id + ' removing');

                // Here is obligatory to call the resolve() if we pretend to remove the panel finally
                // Not calling resolve() will NOT remove the panel
                // It's up to your app to decide if panel should be removed or not
                deferred.resolve();

            });

            // Panel removed ( only if above was resolved() )
            $scope.$on('panel-removed', function (event, id) {

                console.log('Panel #' + id + ' removed');

            });

        }
    }
})();

/**=========================================================
 * Module: flot.js
 * Initializes the Flot chart plugin and handles data refresh
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.charts')
        .directive('flot', flot);

    flot.$inject = ['$http', '$timeout'];
    function flot($http, $timeout) {

        var directive = {
            restrict: 'EA',
            template: '<div></div>',
            scope: {
                dataset: '=?',
                options: '=',
                series: '=',
                callback: '=',
                src: '='
            },
            link: link
        };
        return directive;

        function link(scope, element, attrs) {
            var height, plot, plotArea, width;
            var heightDefault = 220;

            plot = null;

            width = attrs.width || '100%';
            height = attrs.height || heightDefault;

            plotArea = $(element.children()[0]);
            plotArea.css({
                width: width,
                height: height
            });

            function init() {
                var plotObj;
                if (!scope.dataset || !scope.options) return;
                plotObj = $.plot(plotArea, scope.dataset, scope.options);
                scope.$emit('plotReady', plotObj);
                if (scope.callback) {
                    scope.callback(plotObj, scope);
                }

                return plotObj;
            }

            function onDatasetChanged(dataset) {
                if (plot) {
                    plot.setData(dataset);
                    plot.setupGrid();
                    return plot.draw();
                } else {
                    plot = init();
                    onSerieToggled(scope.series);
                    return plot;
                }
            }
            scope.$watchCollection('dataset', onDatasetChanged, true);

            function onSerieToggled(series) {
                if (!plot || !series) return;
                var someData = plot.getData();
                for (var sName in series) {
                    angular.forEach(series[sName], toggleFor(sName));
                }

                plot.setData(someData);
                plot.draw();

                function toggleFor(sName) {
                    return function (s, i) {
                        if (someData[i] && someData[i][sName])
                            someData[i][sName].show = s;
                    };
                }
            }
            scope.$watch('series', onSerieToggled, true);

            function onSrcChanged(src) {

                if (src) {

                    $http.get(src)
                      .success(function (data) {

                          $timeout(function () {
                              scope.dataset = data;
                          });

                      }).error(function () {
                          $.error('Flot chart: Bad request.');
                      });

                }
            }
            scope.$watch('src', onSrcChanged);

        }
    }


})();

/**=========================================================
 * Module: morris.js
 =========================================================*/

/**=========================================================
 * Module: morris.js
 * AngularJS Directives for Morris Charts
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.charts')
        .directive('morrisBar', morrisChart('Bar'))
        .directive('morrisDonut', morrisChart('Donut'))
        .directive('morrisLine', morrisChart('Line'))
        .directive('morrisArea', morrisChart('Area'));

    function morrisChart(type) {
        return function () {
            return {
                restrict: 'EA',
                scope: {
                    morrisData: '=',
                    morrisOptions: '='
                },
                link: function ($scope, element) {
                    // start ready to watch for changes in data
                    $scope.$watch('morrisData', function (newVal) {
                        if (newVal) {
                            $scope.morrisInstance.setData(newVal);
                            $scope.morrisInstance.redraw();
                        }
                    }, true);
                    // the element that contains the chart
                    $scope.morrisOptions.element = element;
                    // If data defined copy to options
                    if ($scope.morrisData)
                        $scope.morrisOptions.data = $scope.morrisData;
                    // Init chart
                    $scope.morrisInstance = new Morris[type]($scope.morrisOptions);

                }
            };
        };
    }

})();

/**=========================================================
 * Module: sparkline.js
 * SparkLines Mini Charts
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.charts')
        .directive('sparkline', sparkline);

    function sparkline() {
        var directive = {
            restrict: 'EA',
            scope: {
                'sparkline': '='
            },
            controller: Controller
        };
        return directive;

    }
    Controller.$inject = ['$scope', '$element', '$timeout', '$window'];
    function Controller($scope, $element, $timeout, $window) {
        var runSL = function () {
            initSparLine();
        };

        $timeout(runSL);

        function initSparLine() {
            var options = $scope.sparkline,
                data = $element.data();

            if (!options) // if no scope options, try with data attributes
                options = data;
            else
                if (data) // data attributes overrides scope options
                    options = angular.extend({}, options, data);

            options.type = options.type || 'bar'; // default chart is bar
            options.disableHiddenCheck = true;

            $element.sparkline('html', options);

            if (options.resize) {
                $($window).resize(function () {
                    $element.sparkline('html', options);
                });
            }
        }

    }


})();

(function () {
    'use strict';

    angular
        .module('app.colors')
        .constant('APP_COLORS', {
            'primary': '#5d9cec',
            'success': '#27c24c',
            'info': '#23b7e5',
            'warning': '#ff902b',
            'danger': '#f05050',
            'inverse': '#131e26',
            'green': '#37bc9b',
            'pink': '#f532e5',
            'purple': '#7266ba',
            'dark': '#3a3f51',
            'yellow': '#fad732',
            'gray-darker': '#232735',
            'gray-dark': '#3a3f51',
            'gray': '#dde6e9',
            'gray-light': '#e4eaec',
            'gray-lighter': '#edf1f2'
        })
    ;
})();
/**=========================================================
 * Module: colors.js
 * Services to retrieve global colors
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.colors')
        .service('Colors', Colors);

    Colors.$inject = ['APP_COLORS'];
    function Colors(APP_COLORS) {
        this.byName = byName;

        ////////////////

        function byName(name) {
            return (APP_COLORS[name] || '#fff');
        }
    }

})();

(function () {
    'use strict';

    angular
        .module('app.core')
        .config(coreConfig);

    coreConfig.$inject = ['$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$animateProvider', '$httpProvider'];
    function coreConfig($controllerProvider, $compileProvider, $filterProvider, $provide, $animateProvider, $httpProvider) {

        var core = angular.module('app.core');
        // registering components after bootstrap
        core.controller = $controllerProvider.register;
        core.directive = $compileProvider.directive;
        core.filter = $filterProvider.register;
        core.factory = $provide.factory;
        core.service = $provide.service;
        core.constant = $provide.constant;
        core.value = $provide.value;

        // Disables animation on items with class .ng-no-animation
        $animateProvider.classNameFilter(/^((?!(ng-no-animation)).)*$/);

        // Register http Interceptor
        core.factory('myHttpInterceptor', function ($q, $window, $rootScope) {
            $rootScope.ActiveAjaxConectionsWithouthNotifications = 0;
            var checker = function (parameters, status) {
                //YOU CAN USE parameters.url TO IGNORE SOME URL
                if (status === "request") {
                    $rootScope.ActiveAjaxConectionsWithouthNotifications += 1;
                    $("div#mainSpinner").show();
                }
                if (status === "response") {
                    $rootScope.ActiveAjaxConectionsWithouthNotifications -= 1;

                }
                if ($rootScope.ActiveAjaxConectionsWithouthNotifications <= 0) {
                    $rootScope.ActiveAjaxConectionsWithouthNotifications = 0;
                    $("div#mainSpinner").hide();

                }


            };
            return {
                'request': function (config) {
                    checker(config, "request");
                    return config;
                },
                'requestError': function (rejection) {
                    checker(rejection.config, "request");
                    return $q.reject(rejection);
                },
                'response': function (response) {
                    checker(response.config, "response");
                    return response;
                },
                'responseError': function (rejection) {
                    checker(rejection.config, "response");
                    return $q.reject(rejection);
                }
            };
        });

        $httpProvider.interceptors.push('myHttpInterceptor');
    }

})();
/**=========================================================
 * Module: constants.js
 * Define constants to inject across the application
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.core')
        .constant('APP_MEDIAQUERY', {
            'desktopLG': 1200,
            'desktop': 992,
            'tablet': 768,
            'mobile': 480
        })
    ;

})();
(function () {
    'use strict';

    angular
        .module('app.core')
        .run(appRun);

    appRun.$inject = ['$rootScope', '$state', '$stateParams', '$window', '$templateCache', 'Colors', '$http'];

    function appRun($rootScope, $state, $stateParams, $window, $templateCache, Colors, $http) {

        // Set reference to access them from any scope
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
        $rootScope.$storage = $window.localStorage;

        // Add Authorization token to every http request
        if ($window.localStorage && $window.localStorage["ngStorage-authorizationData"] && $http) {
            $http.defaults.headers.common = {
                'Authorization': 'Bearer ' + JSON.parse(localStorage["ngStorage-authorizationData"]).token
            };
        }

        // Uncomment this to disable template cache
        /*$rootScope.$on('$stateChangeStart', function(event, toState, toParams, fromState, fromParams) {
            if (typeof(toState) !== 'undefined'){
              $templateCache.remove(toState.templateUrl);
            }
        });*/

        // Allows to use branding color with interpolation
        // {{ colorByName('primary') }}
        $rootScope.colorByName = Colors.byName;

        // cancel click event easily
        $rootScope.cancel = function ($event) {
            $event.stopPropagation();
        };

        // Hooks Example
        // ----------------------------------- 

        // Hook not found
        $rootScope.$on('$stateNotFound',
          function (event, unfoundState/*, fromState, fromParams*/) {
              console.log(unfoundState.to); // "lazy.state"
              console.log(unfoundState.toParams); // {a:1, b:2}
              console.log(unfoundState.options); // {inherit:false} + default options
          });
        // Hook error
        $rootScope.$on('$stateChangeError',
          function (event, toState, toParams, fromState, fromParams, error) {
              console.log(error);
          });
        // Hook success
        $rootScope.$on('$stateChangeSuccess',
          function (/*event, toState, toParams, fromState, fromParams*/) {
              // display new view from top
              $window.scrollTo(0, 0);
              // Save the route title
              $rootScope.currTitle = $state.current.title;
          });

        // Load a title dynamically
        $rootScope.currTitle = $state.current.title;
        $rootScope.pageTitle = function () {
            var title = $rootScope.app.name + ' - ' + ($rootScope.currTitle || $rootScope.app.name);
            document.title = title;
            return title;
        };

    }

})();


(function () {
    'use strict';

    angular
        .module('app.dashboard', [])
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$http','$state', '$scope', '$rootScope', 'ChartData', '$timeout', '$localStorage'];
    function DashboardController($http,$state, $scope, $rootScope, ChartData, $timeout, $localStorage) {

        if (!$localStorage['authorizationData']) {
            $state.go('account.login');
            return;
        }


        var vm = this;

        activate();

        ////////////////

        function activate() {
            //$rootscope.user.name = $localStorage['authorizationData'].username;
            //$rootScope.user = {
            //    name: $localStorage['authorizationData'].userName,
            //    role: $localStorage['authorizationData'].UserRole,
            //    picture: '../../app/img/user/02.jpg'
            //};
            // SPLINE
            // ----------------------------------- 
            vm.splineData = ChartData.load('server/chart/spline.js');
            vm.splineOptions = {
                series: {
                    lines: {
                        show: false
                    },
                    points: {
                        show: true,
                        radius: 4
                    },
                    splines: {
                        show: true,
                        tension: 0.4,
                        lineWidth: 1,
                        fill: 0.5
                    }
                },
                grid: {
                    borderColor: '#eee',
                    borderWidth: 1,
                    hoverable: true,
                    backgroundColor: '#fcfcfc'
                },
                tooltip: true,
                tooltipOpts: {
                    content: function (label, x, y) { return x + ' : ' + y; }
                },
                xaxis: {
                    tickColor: '#fcfcfc',
                    mode: 'categories'
                },
                yaxis: {
                    min: 0,
                    max: 150, // optional: use it for a clear represetation
                    tickColor: '#eee',
                    position: ($scope.app.layout.isRTL ? 'right' : 'left'),
                    tickFormatter: function (v) {
                        return v/* + ' visitors'*/;
                    }
                },
                shadowSize: 0
            };


            // PANEL REFRESH EVENTS
            // ----------------------------------- 

            $scope.$on('panel-refresh', function (event, id) {

                console.log('Simulating chart refresh during 3s on #' + id);

                // Instead of timeout you can request a chart data
                $timeout(function () {

                    // directive listen for to remove the spinner 
                    // after we end up to perform own operations
                    $scope.$broadcast('removeSpinner', id);

                    console.log('Refreshed #' + id);

                }, 3000);

            });


            // PANEL DISMISS EVENTS
            // ----------------------------------- 

            // Before remove panel
            $scope.$on('panel-remove', function (event, id, deferred) {

                console.log('Panel #' + id + ' removing');

                // Here is obligatory to call the resolve() if we pretend to remove the panel finally
                // Not calling resolve() will NOT remove the panel
                // It's up to your app to decide if panel should be removed or not
                deferred.resolve();

            });

            // Panel removed ( only if above was resolved() )
            $scope.$on('panel-removed', function (event, id) {

                console.log('Panel #' + id + ' removed');

            });

        }

        
    }
})();

/**=========================================================
 * Module: scroll.js
 * Make a content box scrollable
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.elements')
        .directive('scrollable', scrollable);

    function scrollable() {
        var directive = {
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
            var defaultHeight = 250;
            element.slimScroll({
                height: (attrs.height || defaultHeight)
            });
        }
    }

})();

/**=========================================================
 * Module: tour.js
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.elements')
        .controller('TourCtrl', TourCtrl);

    TourCtrl.$inject = ['$scope'];
    function TourCtrl($scope) {

        activate();

        ////////////////

        function activate() {
            // BootstrapTour is not compatible with z-index based layout
            // so adding position:static for this case makes the browser
            // to ignore the property
            var section = angular.element('.wrapper > section');
            section.css({ 'position': 'static' });
            // finally restore on destroy and reuse the value declared in stylesheet
            $scope.$on('$destroy', function () {
                section.css({ 'position': '' });
            });
        }
    }
})();

/**=========================================================
 * Module: flatdoc.js
 * Creates the flatdoc markup and initializes the plugin
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.flatdoc')
        .directive('flatdoc', flatdoc);

    function flatdoc() {

        var directive = {
            template: '<div role="flatdoc"><div role="flatdoc-menu"></div><div role="flatdoc-content"></div></div>',
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
            Flatdoc.run({
                fetcher: Flatdoc.file(attrs.src)
            });

            var $root = $('html, body');
            $(document).on('flatdoc:ready', function () {
                var docMenu = $('[role="flatdoc-menu"]');
                docMenu.find('a').on('click', function (e) {
                    e.preventDefault(); e.stopPropagation();

                    var $this = $(this);

                    docMenu.find('a.active').removeClass('active');
                    $this.addClass('active');

                    $root.animate({
                        scrollTop: $(this.getAttribute('href')).offset().top - ($('.topnavbar').height() + 10)
                    }, 800);
                });

            });
        }
    }


})();


(function () {
    'use strict';

    angular
        .module('app.extras')
        .service('LoadTreeService', LoadTreeService);

    LoadTreeService.$inject = ['$resource'];
    function LoadTreeService($resource) {
        // Loads the list of files to populate the treeview
        return $resource('server/editor/filetree.js');
    }

})();

(function () {
    'use strict';

    angular
        .module('app.forms')
        .controller('ColorPickerController', ColorPickerController);

    function ColorPickerController() {
        var vm = this;

        activate();

        ////////////////

        function activate() {
            vm.hexPicker = {
                color: ''
            };

            vm.rgbPicker = {
                color: ''
            };

            vm.rgbaPicker = {
                color: ''
            };

            vm.nonInput = {
                color: ''
            };

            vm.resetColor = function () {
                vm.hexPicker = {
                    color: '#ff0000'
                };
            };

            vm.resetRBGColor = function () {
                vm.rgbPicker = {
                    color: 'rgb(255,255,255)'
                };
            };

            vm.resetRBGAColor = function () {
                vm.rgbaPicker = {
                    color: 'rgb(255,255,255, 0.25)'
                };
            };

            vm.resetNonInputColor = function () {
                vm.nonInput = {
                    color: '#ffffff'
                };
            };
        }
    }
})();
/**=========================================================
 * Module: filestyle.js
 * Initializes the fielstyle plugin
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.forms')
        .directive('filestyle', filestyle);

    function filestyle() {
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element) {
            var options = element.data();

            // old usage support
            options.classInput = element.data('classinput') || options.classInput;

            element.filestyle(options);
        }
    }

})();

/**=========================================================
 * Module: form-imgcrop.js
 * Image crop controller
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.forms')
        .controller('ImageCropController', ImageCropController);

    ImageCropController.$inject = ['$scope'];
    function ImageCropController($scope) {
        var vm = this;

        activate();

        ////////////////

        function activate() {
            vm.reset = function () {
                vm.myImage = '';
                vm.myCroppedImage = '';
                vm.imgcropType = 'square';
            };

            vm.reset();

            var handleFileSelect = function (evt) {
                var file = evt.currentTarget.files[0];
                var reader = new FileReader();
                reader.onload = function (evt) {
                    $scope.$apply(function (/*$scope*/) {
                        vm.myImage = evt.target.result;
                    });
                };
                if (file)
                    reader.readAsDataURL(file);
            };

            angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect);
        }
    }
})();

/**=========================================================
 * Module: form-wizard.js
 * Handles form wizard plugin and validation
 =========================================================*/


(function () {
    'use strict';

    angular
        .module('app.forms')
        .directive('formWizard', formWizard);

    formWizard.$inject = ['$parse'];
    function formWizard($parse) {
        var directive = {
            link: link,
            restrict: 'A',
            scope: true
        };
        return directive;

        function link(scope, element, attrs) {
            var validate = $parse(attrs.validateSteps)(scope),
                wiz = new Wizard(attrs.steps, !!validate, element);
            scope.wizard = wiz.init();
        }

        function Wizard(quantity, validate, element) {

            var self = this;
            self.quantity = parseInt(quantity, 10);
            self.validate = validate;
            self.element = element;

            self.init = function () {
                self.createsteps(self.quantity);
                self.go(1); // always start at fist step
                return self;
            };

            self.go = function (step) {

                if (angular.isDefined(self.steps[step])) {

                    if (self.validate && step !== 1) {
                        var form = $(self.element),
                            group = form.children().children('div').get(step - 2);

                        if (false === form.parsley().validate(group.id)) {
                            return false;
                        }
                    }

                    self.cleanall();
                    self.steps[step] = true;
                }
            };

            self.active = function (step) {
                return !!self.steps[step];
            };

            self.cleanall = function () {
                for (var i in self.steps) {
                    self.steps[i] = false;
                }
            };

            self.createsteps = function (q) {
                self.steps = [];
                for (var i = 1; i <= q; i++) self.steps[i] = false;
            };

        }
    }


})();

/**=========================================================
 * Module: masked,js
 * Initializes the masked inputs
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.forms')
        .directive('masked', masked);

    function masked() {
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element) {
            var $elem = $(element);
            if ($.fn.inputmask)
                $elem.inputmask();
        }
    }

})();

/**
 * AngularJS default filter with the following expression:
 * "person in people | filter: {name: $select.search, age: $select.search}"
 * performs a AND between 'name: $select.search' and 'age: $select.search'.
 * We want to perform a OR.
 */

(function () {
    'use strict';

    angular
        .module('app.forms')
        .filter('propsFilter', propsFilter);

    function propsFilter() {
        return filterFilter;

        ////////////////
        function filterFilter(items, props) {
            var out = [];

            if (angular.isArray(items)) {
                items.forEach(function (item) {
                    var itemMatches = false;

                    var keys = Object.keys(props);
                    for (var i = 0; i < keys.length; i++) {
                        var prop = keys[i];
                        var text = props[prop].toLowerCase();
                        if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                            itemMatches = true;
                            break;
                        }
                    }

                    if (itemMatches) {
                        out.push(item);
                    }
                });
            } else {
                // Let the output be the input untouched
                out = items;
            }

            return out;
        }
    }

})();
/**=========================================================
 * Module: tags-input.js
 * Initializes the tag inputs plugin
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.forms')
        .directive('tagsinput', tagsinput);

    tagsinput.$inject = ['$timeout'];
    function tagsinput($timeout) {
        var directive = {
            link: link,
            require: 'ngModel',
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs, ngModel) {
            element.on('itemAdded itemRemoved', function () {
                // check if view value is not empty and is a string
                // and update the view from string to an array of tags
                if (ngModel.$viewValue && ngModel.$viewValue.split) {
                    ngModel.$setViewValue(ngModel.$viewValue.split(','));
                    ngModel.$render();
                }
            });

            $timeout(function () {
                element.tagsinput();
            });
        }
    }

})();


/**=========================================================
 * Module: validate-form.js
 * Initializes the validation plugin Parsley
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.forms')
        .directive('validateForm', validateForm);

    function validateForm() {
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element) {
            var $elem = $(element);
            if ($.fn.parsley)
                $elem.parsley();
        }
    }

})();

/**=========================================================
 * Module: skycons.js
 * Include any animated weather icon from Skycons
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.icons')
        .directive('skycon', skycon);

    function skycon() {

        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            var skycons = new Skycons({ 'color': (attrs.color || 'white') });

            element.html('<canvas width="' + attrs.width + '" height="' + attrs.height + '"></canvas>');

            skycons.add(element.children()[0], attrs.skycon);

            skycons.play();
        }
    }

})();

(function () {
    'use strict';

    angular
        .module('app.lazyload')
        .config(lazyloadConfig);

    lazyloadConfig.$inject = ['$ocLazyLoadProvider', 'APP_REQUIRES'];
    function lazyloadConfig($ocLazyLoadProvider, APP_REQUIRES) {

        // Lazy Load modules configuration
        $ocLazyLoadProvider.config({
            debug: false,
            events: true,
            modules: APP_REQUIRES.modules
        });

    }
})();
(function () {
    'use strict';

    angular
        .module('app.lazyload')
        .constant('APP_REQUIRES', {
            // jQuery based and standalone scripts
            scripts: {
                'whirl': ['../../vendor/whirl/dist/whirl.css'],
                'classyloader': ['../../vendor/jquery-classyloader/js/jquery.classyloader.min.js'],
                'animo': ['../../vendor/animo.js/animo.js'],
                'fastclick': ['../../vendor/fastclick/lib/fastclick.js'],
                'modernizr': ['../../vendor/modernizr/modernizr.custom.js'],
                'animate': ['../../vendor/animate.css/animate.min.css'],
                'skycons': ['../../vendor/skycons/skycons.js'],
                'icons': ['../../vendor/fontawesome/css/font-awesome.min.css',
                                       '../../vendor/simple-line-icons/css/simple-line-icons.css'],
                'weather-icons': ['../../vendor/weather-icons/css/weather-icons.min.css',
                                       '../../vendor/weather-icons/css/weather-icons-wind.min.css'],
                'sparklines': ['../../vendor/sparkline/index.js'],
                'wysiwyg': ['../../vendor/bootstrap-wysiwyg/bootstrap-wysiwyg.js',
                                       '../../vendor/bootstrap-wysiwyg/external/jquery.hotkeys.js'],
                'slimscroll': ['../../vendor/slimScroll/jquery.slimscroll.min.js'],
                'screenfull': ['../../vendor/screenfull/dist/screenfull.js'],
                'vector-map': ['../../vendor/ika.jvectormap/jquery-jvectormap-1.2.2.min.js',
                                       '../../vendor/ika.jvectormap/jquery-jvectormap-1.2.2.css'],
                'vector-map-maps': ['../../vendor/ika.jvectormap/jquery-jvectormap-world-mill-en.js',
                                       '../../vendor/ika.jvectormap/jquery-jvectormap-us-mill-en.js'],
                'loadGoogleMapsJS': ['../../vendor/load-google-maps/load-google-maps.js'],
                'flot-chart': ['../../vendor/Flot/jquery.flot.js'],
                'flot-chart-plugins': ['../../vendor/flot.tooltip/js/jquery.flot.tooltip.min.js',
                                       '../../vendor/Flot/jquery.flot.resize.js',
                                       '../../vendor/Flot/jquery.flot.pie.js',
                                       '../../vendor/Flot/jquery.flot.time.js',
                                       '../../vendor/Flot/jquery.flot.categories.js',
                                       '../../vendor/flot-spline/js/jquery.flot.spline.min.js'],
                // jquery core and widgets
                'jquery-ui': ['../../vendor/jquery-ui/ui/core.js',
                                       '../../vendor/jquery-ui/ui/widget.js'],
                // loads only jquery required modules and touch support
                'jquery-ui-widgets': ['../../vendor/jquery-ui/ui/core.js',
                                       '../../vendor/jquery-ui/ui/widget.js',
                                       '../../vendor/jquery-ui/ui/mouse.js',
                                       '../../vendor/jquery-ui/ui/draggable.js',
                                       '../../vendor/jquery-ui/ui/droppable.js',
                                       '../../vendor/jquery-ui/ui/sortable.js',
                                       '../../vendor/jqueryui-touch-punch/jquery.ui.touch-punch.min.js'],
                'moment': ['../../vendor/moment/min/moment-with-locales.min.js'],
                'inputmask': ['../../vendor/jquery.inputmask/dist/jquery.inputmask.bundle.js'],
                'flatdoc': ['../../vendor/flatdoc/flatdoc.js'],
                'codemirror': ['../../vendor/codemirror/lib/codemirror.js',
                                       '../../vendor/codemirror/lib/codemirror.css'],
                // modes for common web files
                'codemirror-modes-web': ['../../vendor/codemirror/mode/javascript/javascript.js',
                                         '../../vendor/codemirror/mode/xml/xml.js',
                                         '../../vendor/codemirror/mode/htmlmixed/htmlmixed.js',
                                         '../../vendor/codemirror/mode/css/css.js'],
                'taginput': ['../../vendor/bootstrap-tagsinput/dist/bootstrap-tagsinput.css',
                                       '../../vendor/bootstrap-tagsinput/dist/bootstrap-tagsinput.min.js'],
                'filestyle': ['../../vendor/bootstrap-filestyle/src/bootstrap-filestyle.js'],
                'parsley': ['../../vendor/parsleyjs/dist/parsley.min.js'],
                'fullcalendar': ['../../vendor/fullcalendar/dist/fullcalendar.min.js',
                                       '../../vendor/fullcalendar/dist/fullcalendar.css'],
                'gcal': ['../../vendor/fullcalendar/dist/gcal.js'],
                'chartjs': ['../../vendor/Chart.js/Chart.js'],
                'morris': ['../../vendor/raphael/raphael.js',
                                       '../../vendor/morris.js/morris.js',
                                       '../../vendor/morris.js/morris.css'],
                'loaders.css': ['../../vendor/loaders.css/loaders.css'],
                'spinkit': ['../../vendor/spinkit/css/spinkit.css']
            },
            // Angular based script (use the right module name)
            modules: [
              {
                  name: 'toaster', files: ['../../vendor/angularjs-toaster/toaster.js',
                                                            '../../vendor/angularjs-toaster/toaster.css']
              },
              {
                  name: 'localytics.directives', files: ['../../vendor/chosen_v1.2.0/chosen.jquery.min.js',
                                                            '../../vendor/chosen_v1.2.0/chosen.min.css',
                                                            '../../vendor/angular-chosen-localytics/chosen.js']
              },
              {
                  name: 'ngDialog', files: ['../../vendor/ngDialog/js/ngDialog.min.js',
                                                            '../../vendor/ngDialog/css/ngDialog.min.css',
                                                            '../../vendor/ngDialog/css/ngDialog-theme-default.min.css']
              },
              { name: 'ngWig', files: ['../../vendor/ngWig/dist/ng-wig.min.js'] },
              {
                  name: 'ngTable', files: ['../../vendor/ng-table/dist/ng-table.min.js',
                                                             '../../vendor/ng-table/dist/ng-table.min.css']
              },
              { name: 'ngTableExport', files: ['../../vendor/ng-table-export/ng-table-export.js'] },
              {
                  name: 'angularBootstrapNavTree', files: ['../../vendor/angular-bootstrap-nav-tree/dist/abn_tree_directive.js',
                                                             '../../vendor/angular-bootstrap-nav-tree/dist/abn_tree.css']
              },
              {
                  name: 'htmlSortable', files: ['../../vendor/html.sortable/dist/html.sortable.js',
                                                             '../../vendor/html.sortable/dist/html.sortable.angular.js']
              },
              {
                  name: 'xeditable', files: ['../../vendor/angular-xeditable/dist/js/xeditable.js',
                                                             '../../vendor/angular-xeditable/dist/css/xeditable.css']
              },
              { name: 'angularFileUpload', files: ['../../vendor/angular-file-upload/dist/angular-file-upload.js'] },
              {
                  name: 'ngImgCrop', files: ['../../vendor/ng-img-crop/compile/unminified/ng-img-crop.js',
                                                             '../../vendor/ng-img-crop/compile/unminified/ng-img-crop.css']
              },
              {
                  name: 'ui.select', files: ['../../vendor/angular-ui-select/dist/select.js',
                                                             '../../vendor/angular-ui-select/dist/select.css']
              },
              { name: 'ui.codemirror', files: ['../../vendor/angular-ui-codemirror/ui-codemirror.js'] },
              {
                  name: 'angular-carousel', files: ['../../vendor/angular-carousel/dist/angular-carousel.css',
                                                             '../../vendor/angular-carousel/dist/angular-carousel.js']
              },
              { name: 'infinite-scroll', files: ['../../vendor/ngInfiniteScroll/build/ng-infinite-scroll.js'] },
              {
                  name: 'ui.bootstrap-slider', files: ['../../vendor/seiyria-bootstrap-slider/dist/bootstrap-slider.min.js',
                                                             '../../vendor/seiyria-bootstrap-slider/dist/css/bootstrap-slider.min.css',
                                                             '../../vendor/angular-bootstrap-slider/slider.js']
              },
              {
                  name: 'ui.grid', files: ['../../vendor/angular-ui-grid/ui-grid.min.css',
                                                             '../../vendor/angular-ui-grid/ui-grid.js']
              },
              {
                  name: 'textAngular', files: ['../../vendor/textAngular/dist/textAngular.css',
                                                             '../../vendor/textAngular/dist/textAngular-rangy.min.js',
                                                             '../../vendor/textAngular/dist/textAngular-sanitize.js',
                                                             '../../vendor/textAngular/src/globals.js',
                                                             '../../vendor/textAngular/src/factories.js',
                                                             '../../vendor/textAngular/src/DOM.js',
                                                             '../../vendor/textAngular/src/validators.js',
                                                             '../../vendor/textAngular/src/taBind.js',
                                                             '../../vendor/textAngular/src/main.js',
                                                             '../../vendor/textAngular/dist/textAngularSetup.js'
                  ], serie: true
              },
              {
                  name: 'angular-rickshaw', files: ['../../vendor/d3/d3.min.js',
                                                             '../../vendor/rickshaw/rickshaw.js',
                                                             '../../vendor/rickshaw/rickshaw.min.css',
                                                             '../../vendor/angular-rickshaw/rickshaw.js'], serie: true
              },
              {
                  name: 'angular-chartist', files: ['../../vendor/chartist/dist/chartist.min.css',
                                                             '../../vendor/chartist/dist/chartist.js',
                                                             '../../vendor/angular-chartist.js/dist/angular-chartist.js'], serie: true
              },
              { name: 'ui.map', files: ['../../vendor/angular-ui-map/ui-map.js'] },
              {
                  name: 'datatables', files: ['../../vendor/datatables/media/css/jquery.dataTables.css',
                                                             '../../vendor/datatables/media/js/jquery.dataTables.js',
                                                             '../../vendor/angular-datatables/dist/angular-datatables.js'], serie: true
              },
              {
                  name: 'angular-jqcloud', files: ['../../vendor/jqcloud2/dist/jqcloud.css',
                                                             '../../vendor/jqcloud2/dist/jqcloud.js',
                                                             '../../vendor/angular-jqcloud/angular-jqcloud.js']
              },
              {
                  name: 'angularGrid', files: ['../../vendor/ag-grid/dist/ag-grid.css',
                                                             '../../vendor/ag-grid/dist/ag-grid.js',
                                                             '../../vendor/ag-grid/dist/theme-dark.css',
                                                             '../../vendor/ag-grid/dist/theme-fresh.css']
              },
              {
                  name: 'ng-nestable', files: ['../../vendor/ng-nestable/src/angular-nestable.js',
                                                             '../../vendor/nestable/jquery.nestable.js']
              },
              { name: 'akoenig.deckgrid', files: ['../../vendor/angular-deckgrid/angular-deckgrid.js'] },
              {
                  name: 'oitozero.ngSweetAlert', files: ['../../vendor/sweetalert/dist/sweetalert.css',
                                                             '../../vendor/sweetalert/dist/sweetalert.min.js',
                                                             '../../vendor/angular-sweetalert/SweetAlert.js']
              },
              {
                  name: 'bm.bsTour', files: ['../../vendor/bootstrap-tour/build/css/bootstrap-tour.css',
                                                             '../../vendor/bootstrap-tour/build/js/bootstrap-tour-standalone.js',
                                                             '../../vendor/angular-bootstrap-tour/dist/angular-bootstrap-tour.js'], serie: true
              },
              {
                  name: 'ui.knob', files: ['../../vendor/angular-knob/src/angular-knob.js',
                                                             '../../vendor/jquery-knob/dist/jquery.knob.min.js']
              },
              { name: 'easypiechart', files: ['../../vendor/jquery.easy-pie-chart/dist/angular.easypiechart.min.js'] },
              {
                  name: 'colorpicker.module', files: ['../../vendor/angular-bootstrap-colorpicker/css/colorpicker.css',
                                                             '../../vendor/angular-bootstrap-colorpicker/js/bootstrap-colorpicker-module.js']
              },
              {
                  name: 'rightsManagement.module', files: ['/app/js/RightsManagement/rightsManagement.controller.js',
                                                             '/app/js/RightsManagement/rightsManagement.service.js']
              },
              {
                  name: 'login.module', files: ['/app/js/Users/LoginController.js']
              },
              {
                  name: 'register.module', files: ['/app/js/Users/RegisterController.js']
              },
              {
                  name: 'recover.module', files: ['/app/js/Users/ForgetPasswordController.js']
              },
              {
                  name: 'resetPassword.module', files: ['/app/js/Users/ResetPasswordController.js']
              },
              {
                  name: 'users.module', files: ['/app/js/Users/UsersController.js',
                                                '/app/js/Users/UsersService.js']
              },
              {
                  name: 'profile.module', files: ['/app/js/Users/ProfileController.js',
                                                '/app/js/Users/ProfileService.js']
              },
              {
                  name: 'loadMetaData.module', files: ['/app/js/LoadMetaData/LoadMetaDataController.js',
                                                '/app/js/LoadMetaData/LoadMetaDataService.js']
              },
              {
                  name: 'MT940Loads.module', files: ['/app/js/MT940Loads/MT940Load.controller.js',
                                                       '/app/js/MT940Loads/MT940Load.service.js',
                                                        '/app/js/MT940Loads/MT940LoadDetail.controller.js']
              },

              {
                  name: 'OracleGlLoad.module', files: ['/app/js/OracleGlLoads/OracleGlLoad.controller.js',
                                                       '/app/js/OracleGlLoads/OracleGlLoad.service.js',
                                                       '/app/js/OracleGlLoads/OracleGlLoadDetail.controller.js']
              },
              {
                  name: 'OracleGlEntry.module', files: ['/app/js/OracleGLEntries/OracleGLEntry.controller.js',
                                                        '/app/js/OracleGLEntries/OracleGLEntry.service.js']
              },
              {
                  name: 'CustomerStatements.module', files: ['/app/js/CustomerStatements/customerStatements.controller.js',
                                                             '/app/js/CustomerStatements/customerStatements.service.js',
                                                            '/app/js/CustomerStatements/customerStatementsDetail.controller.js']
              },
              {
                  name: 'CustomerStatementTransactions.module', files: ['/app/js/CustomerStatementTransactions/customerStatementTransactions.controller.js',
                                                                        '/app/js/CustomerStatementTransactions/customerStatementTransactions.service.js']
              },
              {
                  name: 'MT940Balance.module', files: ['/app/js/MT940Balance/MT940Balance.controller.js',
                                                       '/app/js/MT940Balance/MT940Balance.service.js']
              },
              {
                  name: 'createMetaData.module', files: ['/app/js/CreateMetaData/CreateMetaDataController.js',
                                                '/app/js/CreateMetaData/CreateMetaDataService.js']
              },
              {
                  name: 'load.module', files: ['/app/js/Load/LoadController.js']
              }
              ,
              {
                  name: 'changePassword.module', files: ['/app/js/Users/changePassword.controller.js',
                                                        '/app/js/Users/changePassword.service.js']
              },
              {
                  name: 'FiscalYear.module', files: ['/app/js/FiscalYear/fiscalYear.controller.js',
                                                     '/app/js/FiscalYear/fiscalYear.service.js',
                                                     '/app/js/FiscalYear/newFiscalYear.controller.js']
              },
              {
                  name: 'LoadStatus.module', files: ['/app/js/LoadStatus/loadStatus.controller.js',
                                                     '/app/js/LoadStatus/loadStatus.service.js',
                                                     '/app/js/LoadStatus/newLoadStatus.controller.js']
              },
              {
                  name: 'currency.module', files: ['/app/js/currency/Index/currency.controller.js',
                                                        '/app/js/currency/Index/currency.service.js']
              },
              {
                  name: 'newCurrency.module', files: ['/app/js/currency/Create/newCurrency.controller.js',
                                                        '/app/js/currency/Create/newCurrency.service.js']
              },
              {
                  name: 'status.module', files: ['/app/js/status/Index/status.controller.js',
                                                 '/app/js/status/Index/status.service.js']
              },
              {
                  name: 'createStatus.module', files: ['/app/js/status/Create/createStatus.controller.js',
                                                       '/app/js/status/Create/createStatus.service.js']
              },
              {
                  name: 'loadType.module', files: ['/app/js/LoadType/Index/loadType.controller.js',
                                                       '/app/js/LoadType/Index/loadType.service.js']
              },
              {
                  name: 'createLoadType.module', files: ['/app/js/LoadType/Create/CreateLoadType.controller.js',
                                                         '/app/js/LoadType/Create/CreateLoadType.service.js']
              },
              {
                  name: 'source.create.module', files: ['/app/js/Source/Create/source.create.controller.js',
                                                        '/app/js/Source/Create/source.create.service.js']
              },
              {
                  name: 'source.index.module', files: ['/app/js/Source/Index/source.index.controller.js',
                                                       '/app/js/Source/Index/source.index.service.js']
              },

              {
                  name: 'log.module', files: ['/app/js/Log/LogController.js',
                                                        '/app/js/Log/LogDataService.js']
              },
              {
                  name: 'ReconciliationMapping.module', files: ['/app/js/ReconciliationMapping/reconciliationMapping.controller.js',
                                                                '/app/js/ReconciliationMapping/reconciliationMapping.service.js']
              }
              ,

              {
                  name: 'ManualReconciliation.module', files: ['/app/js/ManualReconciliation/manualReconciliation.controller.js',
                                                               '/app/js/ManualReconciliation/manualReconciliation.service.js',
                                                               '/app/js/CustomerStatementTransactions/customerStatementTransactions.service.js',
                                                               '/app/js/CustomerStatementTransactions/customerStatementTransactions.controller.js',
                                                               '/app/js/OracleGLEntries/OracleGLEntry.service.js',
                                                               '/app/js/OracleGLEntries/OracleGLEntry.controller.js']
              }
            ]
        })
    ;

})();

(function () {
    'use strict';

    angular
        .module('app.loadingbar')
        .config(loadingbarConfig)
    ;
    loadingbarConfig.$inject = ['cfpLoadingBarProvider'];
    function loadingbarConfig(cfpLoadingBarProvider) {
        cfpLoadingBarProvider.includeBar = true;
        cfpLoadingBarProvider.includeSpinner = false;
        cfpLoadingBarProvider.latencyThreshold = 500;
        cfpLoadingBarProvider.parentSelector = '.wrapper > section';
    }
})();
(function () {
    'use strict';

    angular
        .module('app.loadingbar')
        .run(loadingbarRun)
    ;
    loadingbarRun.$inject = ['$rootScope', '$timeout', 'cfpLoadingBar'];
    function loadingbarRun($rootScope, $timeout, cfpLoadingBar) {

        // Loading bar transition
        // ----------------------------------- 
        var thBar;
        $rootScope.$on('$stateChangeStart', function () {
            if ($('.wrapper > section').length) // check if bar container exists
                thBar = $timeout(function () {
                    cfpLoadingBar.start();
                }, 0); // sets a latency Threshold
        });
        $rootScope.$on('$stateChangeSuccess', function (event) {
            event.targetScope.$watch('$viewContentLoaded', function () {
                $timeout.cancel(thBar);
                cfpLoadingBar.complete();
            });
        });

    }

})();
(function () {
    'use strict';

    angular
        .module('app.locale')
        .config(localeConfig)
    ;
    localeConfig.$inject = ['tmhDynamicLocaleProvider'];
    function localeConfig(tmhDynamicLocaleProvider) {

        tmhDynamicLocaleProvider.localeLocationPattern('../../vendor/angular-i18n/angular-locale_{{locale}}.js');
        // tmhDynamicLocaleProvider.useStorage('$cookieStore');

    }
})();


/**=========================================================
 * Module: navbar-search.js
 * Navbar search toggler * Auto dismiss on ESC key
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.navsearch')
        .directive('searchOpen', searchOpen)
        .directive('searchDismiss', searchDismiss);

    //
    // directives definition
    // 

    function searchOpen() {
        var directive = {
            controller: searchOpenController,
            restrict: 'A'
        };
        return directive;

    }

    function searchDismiss() {
        var directive = {
            controller: searchDismissController,
            restrict: 'A'
        };
        return directive;

    }

    //
    // Contrller definition
    // 

    searchOpenController.$inject = ['$scope', '$element', 'NavSearch'];
    function searchOpenController($scope, $element, NavSearch) {
        $element
          .on('click', function (e) { e.stopPropagation(); })
          .on('click', NavSearch.toggle);
    }

    searchDismissController.$inject = ['$scope', '$element', 'NavSearch'];
    function searchDismissController($scope, $element, NavSearch) {

        var inputSelector = '.navbar-form input[type="text"]';

        $(inputSelector)
          .on('click', function (e) { e.stopPropagation(); })
          .on('keyup', function (e) {
              if (e.keyCode === 27) // ESC
                  NavSearch.dismiss();
          });

        // click anywhere closes the search
        $(document).on('click', NavSearch.dismiss);
        // dismissable options
        $element
          .on('click', function (e) { e.stopPropagation(); })
          .on('click', NavSearch.dismiss);
    }

})();


/**=========================================================
 * Module: nav-search.js
 * Services to share navbar search functions
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.navsearch')
        .service('NavSearch', NavSearch);

    function NavSearch() {
        this.toggle = toggle;
        this.dismiss = dismiss;

        ////////////////

        var navbarFormSelector = 'form.navbar-form';

        function toggle() {
            var navbarForm = $(navbarFormSelector);

            navbarForm.toggleClass('open');

            var isOpen = navbarForm.hasClass('open');

            navbarForm.find('input')[isOpen ? 'focus' : 'blur']();
        }

        function dismiss() {
            $(navbarFormSelector)
              .removeClass('open') // Close control
              .find('input[type="text"]').blur() // remove focus
              .val('') // Empty input
            ;
        }
    }
})();

/**=========================================================
 * Module: notify.js
 * Directive for notify plugin
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.notify')
        .directive('notify', notify);

    notify.$inject = ['$window', 'Notify'];
    function notify($window, Notify) {

        var directive = {
            link: link,
            restrict: 'A',
            scope: {
                options: '=',
                message: '='
            }
        };
        return directive;

        function link(scope, element) {

            element.on('click', function (e) {
                e.preventDefault();
                Notify.alert(scope.message, scope.options);
            });
        }

    }

})();


/**=========================================================
 * Module: notify.js
 * Create a notifications that fade out automatically.
 * Based on Notify addon from UIKit (http://getuikit.com/docs/addons_notify.html)
 =========================================================*/

(function () {
    'use strict';
    angular
        .module('app.notify')
        .service('Notify', Notify);

    Notify.$inject = ['$timeout'];
    function Notify($timeout) {

        this.alert = notifyAlert;

        ////////////////

        function notifyAlert(msg, opts) {
            if (msg) {
                $timeout(function () {
                    $.notify(msg, opts || {});
                });
            }
        }
    }

})();

/**
 * Notify Addon definition as jQuery plugin
 * Adapted version to work with Bootstrap classes
 * More information http://getuikit.com/docs/addons_notify.html
 */
(function ($) {
    'use strict';
    var containers = {},
        messages = {},
        notify = function (options) {
            if ($.type(options) === 'string') {
                options = { message: options };
            }
            if (arguments[1]) {
                options = $.extend(options, $.type(arguments[1]) === 'string' ? { status: arguments[1] } : arguments[1]);
            }
            return (new Message(options)).show();
        },
        closeAll = function (group, instantly) {
            var id;
            if (group) {
                for (id in messages) { if (group === messages[id].group) messages[id].close(instantly); }
            } else {
                for (id in messages) { messages[id].close(instantly); }
            }
        };
    var Message = function (options) {
        // var $this = this;
        this.options = $.extend({}, Message.defaults, options);
        this.uuid = 'ID' + (new Date().getTime()) + 'RAND' + (Math.ceil(Math.random() * 100000));
        this.element = $([
            // @geedmo: alert-dismissable enables bs close icon
            '<div class="uk-notify-message alert-dismissable">',
                '<a class="close">&times;</a>',
                '<div>' + this.options.message + '</div>',
            '</div>'
        ].join('')).data('notifyMessage', this);
        // status
        if (this.options.status) {
            this.element.addClass('alert alert-' + this.options.status);
            this.currentstatus = this.options.status;
        }
        this.group = this.options.group;
        messages[this.uuid] = this;
        if (!containers[this.options.pos]) {
            containers[this.options.pos] = $('<div class="uk-notify uk-notify-' + this.options.pos + '"></div>').appendTo('body').on('click', '.uk-notify-message', function () {
                $(this).data('notifyMessage').close();
            });
        }
    };
    $.extend(Message.prototype, {
        uuid: false,
        element: false,
        timout: false,
        currentstatus: '',
        group: false,
        show: function () {
            if (this.element.is(':visible')) return;
            var $this = this;
            containers[this.options.pos].show().prepend(this.element);
            var marginbottom = parseInt(this.element.css('margin-bottom'), 10);
            this.element.css({ 'opacity': 0, 'margin-top': -1 * this.element.outerHeight(), 'margin-bottom': 0 }).animate({ 'opacity': 1, 'margin-top': 0, 'margin-bottom': marginbottom }, function () {
                if ($this.options.timeout) {
                    var closefn = function () { $this.close(); };
                    $this.timeout = setTimeout(closefn, $this.options.timeout);
                    $this.element.hover(
                        function () { clearTimeout($this.timeout); },
                        function () { $this.timeout = setTimeout(closefn, $this.options.timeout); }
                    );
                }
            });
            return this;
        },
        close: function (instantly) {
            var $this = this,
                finalize = function () {
                    $this.element.remove();
                    if (!containers[$this.options.pos].children().length) {
                        containers[$this.options.pos].hide();
                    }
                    delete messages[$this.uuid];
                };
            if (this.timeout) clearTimeout(this.timeout);
            if (instantly) {
                finalize();
            } else {
                this.element.animate({ 'opacity': 0, 'margin-top': -1 * this.element.outerHeight(), 'margin-bottom': 0 }, function () {
                    finalize();
                });
            }
        },
        content: function (html) {
            var container = this.element.find('>div');
            if (!html) {
                return container.html();
            }
            container.html(html);
            return this;
        },
        status: function (status) {
            if (!status) {
                return this.currentstatus;
            }
            this.element.removeClass('alert alert-' + this.currentstatus).addClass('alert alert-' + status);
            this.currentstatus = status;
            return this;
        }
    });
    Message.defaults = {
        message: '',
        status: 'normal',
        timeout: 5000,
        group: null,
        pos: 'top-center'
    };

    $.notify = notify;
    $.notify.message = Message;
    $.notify.closeAll = closeAll;

    return notify;
}(jQuery));





/**=========================================================
 * Collapse panels * [panel-collapse]
 =========================================================*/
(function () {
    'use strict';

    angular
        .module('app.panels')
        .directive('panelCollapse', panelCollapse);

    function panelCollapse() {
        var directive = {
            controller: Controller,
            restrict: 'A',
            scope: false
        };
        return directive;
    }

    Controller.$inject = ['$scope', '$element', '$timeout', '$localStorage'];
    function Controller($scope, $element, $timeout, $localStorage) {
        var storageKeyName = 'panelState';

        // Prepare the panel to be collapsible
        var $elem = $($element),
            parent = $elem.closest('.panel'), // find the first parent panel
            panelId = parent.attr('id');

        // Load the saved state if exists
        var currentState = loadPanelState(panelId);
        if (typeof currentState !== 'undefined') {
            $timeout(function () {
                $scope[panelId] = currentState;
            },
              10);
        }

        // bind events to switch icons
        $element.bind('click', function (e) {
            e.preventDefault();
            savePanelState(panelId, !$scope[panelId]);

        });

        // Controller helpers
        function savePanelState(id, state) {
            if (!id) return false;
            var data = angular.fromJson($localStorage[storageKeyName]);
            if (!data) { data = {}; }
            data[id] = state;
            $localStorage[storageKeyName] = angular.toJson(data);
        }
        function loadPanelState(id) {
            if (!id) return false;
            var data = angular.fromJson($localStorage[storageKeyName]);
            if (data) {
                return data[id];
            }
        }
    }

})();

/**=========================================================
 * Dismiss panels * [panel-dismiss]
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.panels')
        .directive('panelDismiss', panelDismiss);

    function panelDismiss() {

        var directive = {
            controller: Controller,
            restrict: 'A'
        };
        return directive;

    }

    Controller.$inject = ['$scope', '$element', '$q', 'Utils'];
    function Controller($scope, $element, $q, Utils) {
        var removeEvent = 'panel-remove',
            removedEvent = 'panel-removed';

        $element.on('click', function (e) {
            e.preventDefault();

            // find the first parent panel
            var parent = $(this).closest('.panel');

            removeElement();

            function removeElement() {
                var deferred = $q.defer();
                var promise = deferred.promise;

                // Communicate event destroying panel
                $scope.$emit(removeEvent, parent.attr('id'), deferred);
                promise.then(destroyMiddleware);
            }

            // Run the animation before destroy the panel
            function destroyMiddleware() {
                if (Utils.support.animation) {
                    parent.animo({ animation: 'bounceOut' }, destroyPanel);
                }
                else destroyPanel();
            }

            function destroyPanel() {

                var col = parent.parent();
                parent.remove();
                // remove the parent if it is a row and is empty and not a sortable (portlet)
                col
                  .filter(function () {
                      var el = $(this);
                      return (el.is('[class*="col-"]:not(.sortable)') && el.children('*').length === 0);
                  }).remove();

                // Communicate event destroyed panel
                $scope.$emit(removedEvent, parent.attr('id'));

            }

        });
    }
})();



/**=========================================================
 * Refresh panels
 * [panel-refresh] * [data-spinner="standard"]
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.panels')
        .directive('panelRefresh', panelRefresh);

    function panelRefresh() {
        var directive = {
            controller: Controller,
            restrict: 'A',
            scope: false
        };
        return directive;

    }

    Controller.$inject = ['$scope', '$element'];
    function Controller($scope, $element) {
        var refreshEvent = 'panel-refresh',
            whirlClass = 'whirl',
            defaultSpinner = 'standard';

        // catch clicks to toggle panel refresh
        $element.on('click', function (e) {
            e.preventDefault();

            var $this = $(this),
                panel = $this.parents('.panel').eq(0),
                spinner = $this.data('spinner') || defaultSpinner
            ;

            // start showing the spinner
            panel.addClass(whirlClass + ' ' + spinner);

            // Emit event when refresh clicked
            $scope.$emit(refreshEvent, panel.attr('id'));

        });

        // listen to remove spinner
        $scope.$on('removeSpinner', removeSpinner);

        // method to clear the spinner when done
        function removeSpinner(ev, id) {
            if (!id) return;
            var newid = id.charAt(0) === '#' ? id : ('#' + id);
            angular
              .element(newid)
              .removeClass(whirlClass);
        }
    }
})();



/**=========================================================
 * Module panel-tools.js
 * Directive tools to control panels.
 * Allows collapse, refresh and dismiss (remove)
 * Saves panel state in browser storage
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.panels')
        .directive('paneltool', paneltool);

    paneltool.$inject = ['$compile', '$timeout'];
    function paneltool($compile, $timeout) {
        var directive = {
            link: link,
            restrict: 'E',
            scope: false
        };
        return directive;

        function link(scope, element, attrs) {

            var templates = {
                /* jshint multistr: true */
                collapse: '<a href="#" panel-collapse="" uib-tooltip="Collapse Panel" ng-click="{{panelId}} = !{{panelId}}"> \
                        <em ng-show="{{panelId}}" class="fa fa-plus ng-no-animation"></em> \
                        <em ng-show="!{{panelId}}" class="fa fa-minus ng-no-animation"></em> \
                      </a>',
                dismiss: '<a href="#" panel-dismiss="" uib-tooltip="Close Panel">\
                       <em class="fa fa-times"></em>\
                     </a>',
                refresh: '<a href="#" panel-refresh="" data-spinner="{{spinner}}" uib-tooltip="Refresh Panel">\
                       <em class="fa fa-refresh"></em>\
                     </a>'
            };

            var tools = scope.panelTools || attrs;

            $timeout(function () {
                element.html(getTemplate(element, tools)).show();
                $compile(element.contents())(scope);

                element.addClass('pull-right');
            });

            function getTemplate(elem, attrs) {
                var temp = '';
                attrs = attrs || {};
                if (attrs.toolCollapse)
                    temp += templates.collapse.replace(/{{panelId}}/g, (elem.parent().parent().attr('id')));
                if (attrs.toolDismiss)
                    temp += templates.dismiss;
                if (attrs.toolRefresh)
                    temp += templates.refresh.replace(/{{spinner}}/g, attrs.toolRefresh);
                return temp;
            }
        }// link
    }

})();


/**=========================================================
 * Drag and drop any panel based on jQueryUI portlets
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.panels')
        .directive('portlet', portlet);

    portlet.$inject = ['$timeout', '$localStorage'];
    function portlet($timeout, $localStorage) {
        var storageKeyName = 'portletState';

        return {
            restrict: 'A',
            link: link
        };

        /////////////

        function link(scope, element) {

            // not compatible with jquery sortable
            if (!$.fn.sortable) return;

            element.sortable({
                connectWith: '[portlet]', // same like directive 
                items: 'div.panel',
                handle: '.portlet-handler',
                opacity: 0.7,
                placeholder: 'portlet box-placeholder',
                cancel: '.portlet-cancel',
                forcePlaceholderSize: true,
                iframeFix: false,
                tolerance: 'pointer',
                helper: 'original',
                revert: 200,
                forceHelperSize: true,
                update: savePortletOrder,
                create: loadPortletOrder
            });

        }


        function savePortletOrder(event/*, ui*/) {
            var self = event.target;
            var data = angular.fromJson($localStorage[storageKeyName]);

            if (!data) { data = {}; }

            data[self.id] = $(self).sortable('toArray');

            if (data) {
                $timeout(function () {
                    $localStorage[storageKeyName] = angular.toJson(data);
                });
            }
        }

        function loadPortletOrder(event) {
            var self = event.target;
            var data = angular.fromJson($localStorage[storageKeyName]);

            if (data) {

                var porletId = self.id,
                    panels = data[porletId];

                if (panels) {
                    var portlet = $('#' + porletId);

                    $.each(panels, function (index, value) {
                        $('#' + value).appendTo(portlet);
                    });
                }

            }
        }

    }

})();

(function () {
    'use strict';

    angular
        .module('app.preloader')
        .directive('preloader', preloader);

    preloader.$inject = ['$animate', '$timeout', '$q'];
    function preloader($animate, $timeout, $q) {

        var directive = {
            restrict: 'EAC',
            template:
              '<div class="preloader-progress">' +
                  '<div class="preloader-progress-bar" ' +
                       'ng-style="{width: loadCounter + \'%\'}"></div>' +
              '</div>'
            ,
            link: link
        };
        return directive;

        ///////

        function link(scope, el) {

            scope.loadCounter = 0;

            var counter = 0,
                timeout;

            // disables scrollbar
            angular.element('body').css('overflow', 'hidden');
            // ensure class is present for styling
            el.addClass('preloader');

            appReady().then(endCounter);

            timeout = $timeout(startCounter);

            ///////

            function startCounter() {

                var remaining = 100 - counter;
                counter = counter + (0.015 * Math.pow(1 - Math.sqrt(remaining), 2));

                scope.loadCounter = parseInt(counter, 10);

                timeout = $timeout(startCounter, 20);
            }

            function endCounter() {

                $timeout.cancel(timeout);

                scope.loadCounter = 100;

                $timeout(function () {
                    // animate preloader hiding
                    $animate.addClass(el, 'preloader-hidden');
                    // retore scrollbar
                    angular.element('body').css('overflow', '');
                }, 300);
            }

            function appReady() {
                var deferred = $q.defer();
                var viewsLoaded = 0;
                // if this doesn't sync with the real app ready
                // a custom event must be used instead
                var off = scope.$on('$viewContentLoaded', function () {
                    viewsLoaded++;
                    // we know there are at least two views to be loaded 
                    // before the app is ready (1-index.html 2-app*.html)
                    if (viewsLoaded === 2) {
                        // with resolve this fires only once
                        $timeout(function () {
                            deferred.resolve();
                        }, 3000);

                        off();
                    }

                });

                return deferred.promise;
            }

        } //link
    }

})();
/**=========================================================
 * Module: helpers.js
 * Provides helper functions for routes definition
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.routes')
        .provider('RouteHelpers', RouteHelpersProvider)
    ;

    RouteHelpersProvider.$inject = ['APP_REQUIRES'];
    function RouteHelpersProvider(APP_REQUIRES) {

        /* jshint validthis:true */
        return {
            // provider access level
            basepath: basepath,
            resolveFor: resolveFor,
            // controller access level
            $get: function () {
                return {
                    basepath: basepath,
                    resolveFor: resolveFor
                };
            }
        };

        // Set here the base of the relative path
        // for all app views
        function basepath(uri) {
            return 'app/views/' + uri;
        }

        // Generates a resolve object by passing script names
        // previously configured in constant.APP_REQUIRES
        function resolveFor() {
            var _args = arguments;
            return {
                deps: ['$ocLazyLoad', '$q', function ($ocLL, $q) {
                    // Creates a promise chain for each argument
                    var promise = $q.when(1); // empty promise
                    for (var i = 0, len = _args.length; i < len; i++) {
                        promise = andThen(_args[i]);
                    }
                    return promise;

                    // creates promise to chain dynamically
                    function andThen(_arg) {
                        // also support a function that returns a promise
                        if (typeof _arg === 'function')
                            return promise.then(_arg);
                        else
                            return promise.then(function () {
                                // if is a module, pass the name. If not, pass the array
                                var whatToLoad = getRequired(_arg);
                                // simple error check
                                if (!whatToLoad) return $.error('Route resolve: Bad resource name [' + _arg + ']');
                                // finally, return a promise
                                return $ocLL.load(whatToLoad);
                            });
                    }
                    // check and returns required data
                    // analyze module items with the form [name: '', files: []]
                    // and also simple array of script files (for not angular js)
                    function getRequired(name) {
                        if (APP_REQUIRES.modules)
                            for (var m in APP_REQUIRES.modules)
                                if (APP_REQUIRES.modules[m].name && APP_REQUIRES.modules[m].name === name)
                                    return APP_REQUIRES.modules[m];
                        return APP_REQUIRES.scripts && APP_REQUIRES.scripts[name];
                    }

                }]
            };
        } // resolveFor

    }


})();
          

(function () {
    'use strict';

    angular
        .module('app.settings')
        .run(settingsRun);

    settingsRun.$inject = ['$rootScope', '$localStorage'];

    function settingsRun($rootScope, $localStorage) {

        // Global Settings
        // -----------------------------------
        $rootScope.app = {
            name: 'FRS',
            description: 'FRS (Financial Reconcilaition System) &copy; v. 0.0.0.1 by <a href="http://innostark.com/">InnoStark Technologies</a>',
            year: ((new Date()).getFullYear()),
            layout: {
                isFixed: true,
                isCollapsed: false,
                isBoxed: false,
                isRTL: false,
                horizontal: false,
                isFloat: false,
                asideHover: false,
                theme: null,
                asideScrollbar: false
            },
            useFullLayout: false,
            hiddenFooter: false,
            offsidebarOpen: false,
            asideToggled: false,
            viewAnimation: 'ng-fadeInUp'
        };

        // Setup the layout mode
        $rootScope.app.layout.horizontal = ($rootScope.$stateParams.layout === 'app-h');

        // Restore layout settings
        if (angular.isDefined($localStorage.layout))
            $rootScope.app.layout = $localStorage.layout;
        else
            $localStorage.layout = $rootScope.app.layout;

        $rootScope.$watch('app.layout', function () {
            $localStorage.layout = $rootScope.app.layout;
        }, true);

        // Close submenu when sidebar change from collapsed to normal
        $rootScope.$watch('app.layout.isCollapsed', function (newValue) {
            if (newValue === false)
                $rootScope.$broadcast('closeSidebarMenu');
        });
    }

})();

/**=========================================================
 * Module: sidebar-menu.js
 * Handle sidebar collapsible elements
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.sidebar')
        .controller('SidebarController', SidebarController);

    SidebarController.$inject = ['$http', '$rootScope', '$scope', '$state', 'SidebarLoader', 'Utils'];
    function SidebarController($http, $rootScope, $scope, $state, SidebarLoader, Utils) {
        activate();

        ////////////////

        function activate() {
            var collapseList = [];

            // demo: when switch from collapse to hover, close all items
            $rootScope.$watch('app.layout.asideHover', function (oldVal, newVal) {
                if (newVal === false && oldVal === true) {
                    closeAllBut(-1);
                }
            });


            // Load menu from json file
            // ----------------------------------- 

            SidebarLoader.getMenu(sidebarReady);

            function sidebarReady(items) {
                $scope.menuItems = items;
            }

            // Handle sidebar and collapse items
            // ----------------------------------

            $scope.getMenuItemPropClasses = function (item) {
                return (item.heading ? 'nav-heading' : '') +
                       (isActive(item) ? ' active' : '');
            };

            $scope.addCollapse = function ($index, item) {
                collapseList[$index] = $rootScope.app.layout.asideHover ? true : !isActive(item);
            };

            $scope.isCollapse = function ($index) {
                return (collapseList[$index]);
            };

            $scope.toggleCollapse = function ($index, isParentItem) {

                // collapsed sidebar doesn't toggle drodopwn
                if (Utils.isSidebarCollapsed() || $rootScope.app.layout.asideHover) return true;

                // make sure the item index exists
                if (angular.isDefined(collapseList[$index])) {
                    if (!$scope.lastEventFromChild) {
                        collapseList[$index] = !collapseList[$index];
                        closeAllBut($index);
                    }
                }
                else if (isParentItem) {
                    closeAllBut(-1);
                }

                $scope.lastEventFromChild = isChild($index);

                return true;

            };

            // Controller helpers
            // ----------------------------------- 

            // Check item and children active state
            function isActive(item) {

                if (!item) return;

                if (!item.sref || item.sref === '#') {
                    var foundActive = false;
                    angular.forEach(item.submenu, function (value) {
                        if (isActive(value)) foundActive = true;
                    });
                    return foundActive;
                }
                else
                    return $state.is(item.sref) || $state.includes(item.sref);
            }

            function closeAllBut(index) {
                index += '';
                for (var i in collapseList) {
                    if (index < 0 || index.indexOf(i) < 0)
                        collapseList[i] = true;
                }
            }

            function isChild($index) {
                /*jshint -W018*/
                return (typeof $index === 'string') && !($index.indexOf('-') < 0);
            }

        } // activate
    }

})();

/**=========================================================
 * Module: sidebar.js
 * Wraps the sidebar and handles collapsed state
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.sidebar')
        .directive('sidebar', sidebar);

    sidebar.$inject = ['$rootScope', '$timeout', '$window', 'Utils'];
    function sidebar($rootScope, $timeout, $window, Utils) {
        var $win = angular.element($window);
        var directive = {
            // bindToController: true,
            // controller: Controller,
            // controllerAs: 'vm',
            link: link,
            restrict: 'EA',
            template: '<nav class="sidebar" ng-transclude></nav>',
            transclude: true,
            replace: true
            // scope: {}
        };
        return directive;

        function link(scope, element, attrs) {

            var currentState = $rootScope.$state.current.name;
            var $sidebar = element;

            var eventName = Utils.isTouch() ? 'click' : 'mouseenter';
            var subNav = $();

            $sidebar.on(eventName, '.nav > li', function () {

                if (Utils.isSidebarCollapsed() || $rootScope.app.layout.asideHover) {

                    subNav.trigger('mouseleave');
                    subNav = toggleMenuItem($(this), $sidebar);

                    // Used to detect click and touch events outside the sidebar          
                    sidebarAddBackdrop();

                }

            });

            scope.$on('closeSidebarMenu', function () {
                removeFloatingNav();
            });

            // Normalize state when resize to mobile
            $win.on('resize', function () {
                if (!Utils.isMobile())
                    asideToggleOff();
            });

            // Adjustment on route changes
            $rootScope.$on('$stateChangeStart', function (event, toState) {
                currentState = toState.name;
                // Hide sidebar automatically on mobile
                asideToggleOff();

                $rootScope.$broadcast('closeSidebarMenu');
            });

            // Autoclose when click outside the sidebar
            if (angular.isDefined(attrs.sidebarAnyclickClose)) {

                var wrapper = $('.wrapper');
                var sbclickEvent = 'click.sidebar';

                $rootScope.$watch('app.asideToggled', watchExternalClicks);

            }

            //////

            function watchExternalClicks(newVal) {
                // if sidebar becomes visible
                if (newVal === true) {
                    $timeout(function () { // render after current digest cycle
                        wrapper.on(sbclickEvent, function (e) {
                            // if not child of sidebar
                            if (!$(e.target).parents('.aside').length) {
                                asideToggleOff();
                            }
                        });
                    });
                }
                else {
                    // dettach event
                    wrapper.off(sbclickEvent);
                }
            }

            function asideToggleOff() {
                $rootScope.app.asideToggled = false;
                if (!scope.$$phase) scope.$apply(); // anti-pattern but sometimes necessary
            }
        }

        ///////

        function sidebarAddBackdrop() {
            var $backdrop = $('<div/>', { 'class': 'dropdown-backdrop' });
            $backdrop.insertAfter('.aside-inner').on('click mouseenter', function () {
                removeFloatingNav();
            });
        }

        // Open the collapse sidebar submenu items when on touch devices 
        // - desktop only opens on hover
        function toggleTouchItem($element) {
            $element
              .siblings('li')
              .removeClass('open')
              .end()
              .toggleClass('open');
        }

        // Handles hover to open items under collapsed menu
        // ----------------------------------- 
        function toggleMenuItem($listItem, $sidebar) {

            removeFloatingNav();

            var ul = $listItem.children('ul');

            if (!ul.length) return $();
            if ($listItem.hasClass('open')) {
                toggleTouchItem($listItem);
                return $();
            }

            var $aside = $('.aside');
            var $asideInner = $('.aside-inner'); // for top offset calculation
            // float aside uses extra padding on aside
            var mar = parseInt($asideInner.css('padding-top'), 0) + parseInt($aside.css('padding-top'), 0);
            var subNav = ul.clone().appendTo($aside);

            toggleTouchItem($listItem);

            var itemTop = ($listItem.position().top + mar) - $sidebar.scrollTop();
            var vwHeight = $win.height();

            subNav
              .addClass('nav-floating')
              .css({
                  position: $rootScope.app.layout.isFixed ? 'fixed' : 'absolute',
                  top: itemTop,
                  bottom: (subNav.outerHeight(true) + itemTop > vwHeight) ? 0 : 'auto'
              });

            subNav.on('mouseleave', function () {
                toggleTouchItem($listItem);
                subNav.remove();
            });

            return subNav;
        }

        function removeFloatingNav() {
            $('.dropdown-backdrop').remove();
            $('.sidebar-subnav.nav-floating').remove();
            $('.sidebar li.open').removeClass('open');
        }
    }


})();


(function () {
    'use strict';

    angular
        .module('app.sidebar')
        .service('SidebarLoader', SidebarLoader);

    SidebarLoader.$inject = ['$http', '$localStorage'];
    function SidebarLoader($http, $localStorage) {
        this.getMenu = getMenu;

        ////////////////

        function getMenu(onReady, onError) {
            if (!$localStorage['authorizationData']) {
                return;
            }
            var menuJson = frsApiUrl +'/api/Menu',
            //var menuJson = '../../server/sidebar-menu.js',
                menuURL = menuJson + '?v=' + (new Date().getTime()); // jumps cache

            onError = onError || function () { alert('Failure loading menu'); };

            $http
              .get(menuURL)
              .success(onReady)
              .error(onError);
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('app.sidebar')
        .controller('UserBlockController', UserBlockController);

    UserBlockController.$inject = ['$http', '$state','$rootScope', '$scope', '$localStorage'];
    function UserBlockController($http, $state, $rootScope, $scope, $localStorage) {

        activate();

        ////////////////

        function activate() {
            $rootScope.user = {
                name: $localStorage['authorizationData'].userName,
                role: $localStorage['authorizationData'].UserRole,
                picture: '../../app/img/user/02.jpg'
            };

            // Hides/show user avatar on sidebar
            $rootScope.toggleUserBlock = function () {
                $rootScope.$broadcast('toggleUserBlock');
            };

            $rootScope.userBlockVisible = true;

            var detach = $rootScope.$on('toggleUserBlock', function (/*event, args*/) {

                $rootScope.userBlockVisible = !$rootScope.userBlockVisible;

            });

            $scope.$on('$destroy', detach);
            $rootScope.logout = function () {
                delete $localStorage['authorizationData'];
                $http.post(frsApiUrl + "/api/Account/Logout")
                    .success(function () {
                        console.log("LoggedOut");
                        $state.go('account.login');
                    })
                    .error(function (err) {
                        showErrors(err);
                    });

            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('app.translate')
        .config(translateConfig)
    ;
    translateConfig.$inject = ['$translateProvider'];
    function translateConfig($translateProvider) {

        $translateProvider.useStaticFilesLoader({
            prefix: 'app/i18n/',
            suffix: '.js'
        });

        $translateProvider.preferredLanguage('en');
        $translateProvider.useLocalStorage();
        $translateProvider.usePostCompiling(true);
        $translateProvider.useSanitizeValueStrategy('sanitizeParameters');

    }
})();
(function () {
    'use strict';

    angular
        .module('app.translate')
        .run(translateRun)
    ;
    translateRun.$inject = ['$rootScope', '$translate'];

    function translateRun($rootScope, $translate) {

        // Internationalization
        // ----------------------

        $rootScope.language = {
            // Handles language dropdown
            listIsOpen: false,
            // list of available languages
            available: {
                'en': 'English',
                'es_AR': 'Español'
            },
            // display always the current ui language
            init: function () {
                var proposedLanguage = $translate.proposedLanguage() || $translate.use();
                var preferredLanguage = $translate.preferredLanguage(); // we know we have set a preferred one in app.config
                $rootScope.language.selected = $rootScope.language.available[(proposedLanguage || preferredLanguage)];
            },
            set: function (localeId) {
                // Set the new idiom
                $translate.use(localeId);
                // save a reference for the current language
                $rootScope.language.selected = $rootScope.language.available[localeId];
                // finally toggle dropdown
                $rootScope.language.listIsOpen = !$rootScope.language.listIsOpen;
            }
        };

        $rootScope.language.init();

    }
})();
/**=========================================================
 * Module: animate-enabled.js
 * Enable or disables ngAnimate for element with directive
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.utils')
        .directive('animateEnabled', animateEnabled);

    animateEnabled.$inject = ['$animate'];
    function animateEnabled($animate) {
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            scope.$watch(function () {
                return scope.$eval(attrs.animateEnabled, scope);
            }, function (newValue) {
                $animate.enabled(!!newValue, element);
            });
        }
    }

})();

/**=========================================================
 * Module: browser.js
 * Browser detection
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.utils')
        .service('Browser', Browser);

    Browser.$inject = ['$window'];
    function Browser($window) {
        return $window.jQBrowser;
    }

})();

/**=========================================================
 * Module: clear-storage.js
 * Removes a key from the browser storage via element click
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.utils')
        .directive('resetKey', resetKey);

    resetKey.$inject = ['$state', '$localStorage'];
    function resetKey($state, $localStorage) {
        var directive = {
            link: link,
            restrict: 'A',
            scope: {
                resetKey: '@'
            }
        };
        return directive;

        function link(scope, element) {
            element.on('click', function (e) {
                e.preventDefault();

                if (scope.resetKey) {
                    delete $localStorage[scope.resetKey];
                    $state.go($state.current, {}, { reload: true });
                }
                else {
                    $.error('No storage key specified for reset.');
                }
            });
        }
    }

})();

/**=========================================================
 * Module: fullscreen.js
 * Toggle the fullscreen mode on/off
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.utils')
        .directive('toggleFullscreen', toggleFullscreen);

    toggleFullscreen.$inject = ['Browser'];
    function toggleFullscreen(Browser) {
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element) {
            // Not supported under IE
            if (Browser.msie) {
                element.addClass('hide');
            }
            else {
                element.on('click', function (e) {
                    e.preventDefault();

                    if (screenfull.enabled) {

                        screenfull.toggle();

                        // Switch icon indicator
                        if (screenfull.isFullscreen)
                            $(this).children('em').removeClass('fa-expand').addClass('fa-compress');
                        else
                            $(this).children('em').removeClass('fa-compress').addClass('fa-expand');

                    } else {
                        $.error('Fullscreen not enabled');
                    }

                });
            }
        }
    }


})();

/**=========================================================
 * Module: load-css.js
 * Request and load into the current page a css file
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.utils')
        .directive('loadCss', loadCss);

    function loadCss() {
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            element.on('click', function (e) {
                if (element.is('a')) e.preventDefault();
                var uri = attrs.loadCss,
                    link;

                if (uri) {
                    link = createLink(uri);
                    if (!link) {
                        $.error('Error creating stylesheet link element.');
                    }
                }
                else {
                    $.error('No stylesheet location defined.');
                }

            });
        }

        function createLink(uri) {
            var linkId = 'autoloaded-stylesheet',
                oldLink = $('#' + linkId).attr('id', linkId + '-old');

            $('head').append($('<link/>').attr({
                'id': linkId,
                'rel': 'stylesheet',
                'href': uri
            }));

            if (oldLink.length) {
                oldLink.remove();
            }

            return $('#' + linkId);
        }
    }

})();

/**=========================================================
 * Module: now.js
 * Provides a simple way to display the current time formatted
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.utils')
        .directive('now', now);

    now.$inject = ['dateFilter', '$interval'];
    function now(dateFilter, $interval) {
        var directive = {
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
            var format = attrs.format;

            function updateTime() {
                var dt = dateFilter(new Date(), format);
                element.text(dt);
            }

            updateTime();
            var intervalPromise = $interval(updateTime, 1000);

            scope.$on('$destroy', function () {
                $interval.cancel(intervalPromise);
            });

        }
    }

})();

/**=========================================================
 * Module: table-checkall.js
 * Tables check all checkbox
 =========================================================*/
(function () {
    'use strict';

    angular
        .module('app.utils')
        .directive('checkAll', checkAll);

    function checkAll() {
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element) {
            element.on('change', function () {
                var $this = $(this),
                    index = $this.index() + 1,
                    checkbox = $this.find('input[type="checkbox"]'),
                    table = $this.parents('table');
                // Make sure to affect only the correct checkbox column
                table.find('tbody > tr > td:nth-child(' + index + ') input[type="checkbox"]')
                  .prop('checked', checkbox[0].checked);

            });
        }
    }

})();

/**=========================================================
 * Module: trigger-resize.js
 * Triggers a window resize event from any element
 =========================================================*/
(function () {
    'use strict';

    angular
        .module('app.utils')
        .directive('triggerResize', triggerResize);

    triggerResize.$inject = ['$window', '$timeout'];
    function triggerResize($window, $timeout) {
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element) {
            element.on('click', function () {
                $timeout(function () {
                    // all IE friendly dispatchEvent
                    var evt = document.createEvent('UIEvents');
                    evt.initUIEvent('resize', true, false, $window, 0);
                    $window.dispatchEvent(evt);
                    // modern dispatchEvent way
                    // $window.dispatchEvent(new Event('resize'));
                });
            });
        }
    }

})();

/**=========================================================
 * Module: utils.js
 * Utility library to use across the theme
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.utils')
        .service('Utils', Utils);

    Utils.$inject = ['$window', 'APP_MEDIAQUERY'];
    function Utils($window, APP_MEDIAQUERY) {

        var $html = angular.element('html'),
            $win = angular.element($window),
            $body = angular.element('body');

        return {
            // DETECTION
            support: {
                transition: (function () {
                    var transitionEnd = (function () {

                        var element = document.body || document.documentElement,
                            transEndEventNames = {
                                WebkitTransition: 'webkitTransitionEnd',
                                MozTransition: 'transitionend',
                                OTransition: 'oTransitionEnd otransitionend',
                                transition: 'transitionend'
                            }, name;

                        for (name in transEndEventNames) {
                            if (element.style[name] !== undefined) return transEndEventNames[name];
                        }
                    }());

                    return transitionEnd && { end: transitionEnd };
                })(),
                animation: (function () {

                    var animationEnd = (function () {

                        var element = document.body || document.documentElement,
                            animEndEventNames = {
                                WebkitAnimation: 'webkitAnimationEnd',
                                MozAnimation: 'animationend',
                                OAnimation: 'oAnimationEnd oanimationend',
                                animation: 'animationend'
                            }, name;

                        for (name in animEndEventNames) {
                            if (element.style[name] !== undefined) return animEndEventNames[name];
                        }
                    }());

                    return animationEnd && { end: animationEnd };
                })(),
                requestAnimationFrame: window.requestAnimationFrame ||
                                       window.webkitRequestAnimationFrame ||
                                       window.mozRequestAnimationFrame ||
                                       window.msRequestAnimationFrame ||
                                       window.oRequestAnimationFrame ||
                                       function (callback) { window.setTimeout(callback, 1000 / 60); },
                /*jshint -W069*/
                touch: (
                    ('ontouchstart' in window && navigator.userAgent.toLowerCase().match(/mobile|tablet/)) ||
                    (window.DocumentTouch && document instanceof window.DocumentTouch) ||
                    (window.navigator['msPointerEnabled'] && window.navigator['msMaxTouchPoints'] > 0) || //IE 10
                    (window.navigator['pointerEnabled'] && window.navigator['maxTouchPoints'] > 0) || //IE >=11
                    false
                ),
                mutationobserver: (window.MutationObserver || window.WebKitMutationObserver || window.MozMutationObserver || null)
            },
            // UTILITIES
            isInView: function (element, options) {
                /*jshint -W106*/
                var $element = $(element);

                if (!$element.is(':visible')) {
                    return false;
                }

                var window_left = $win.scrollLeft(),
                    window_top = $win.scrollTop(),
                    offset = $element.offset(),
                    left = offset.left,
                    top = offset.top;

                options = $.extend({ topoffset: 0, leftoffset: 0 }, options);

                if (top + $element.height() >= window_top && top - options.topoffset <= window_top + $win.height() &&
                    left + $element.width() >= window_left && left - options.leftoffset <= window_left + $win.width()) {
                    return true;
                } else {
                    return false;
                }
            },

            langdirection: $html.attr('dir') === 'rtl' ? 'right' : 'left',

            isTouch: function () {
                return $html.hasClass('touch');
            },

            isSidebarCollapsed: function () {
                return $body.hasClass('aside-collapsed');
            },

            isSidebarToggled: function () {
                return $body.hasClass('aside-toggled');
            },

            isMobile: function () {
                return $win.width() < APP_MEDIAQUERY.tablet;
            }

        };
    }
})();

(function () {
    'use strict';

    angular
        .module('custom', [
            // request the the entire framework
            'angle',
            // or just modules
            'app.core',
            'app.sidebar'
            /*...*/
        ]);
})();

// To run this code, edit file index.html or index.jade and change
// html data-ng-app attribute from angle to myAppName
// ----------------------------------------------------------------------

(function () {
    'use strict';

    angular
        .module('custom')
        .controller('Controller', Controller);

    Controller.$inject = ['$log'];
    function Controller($log) {
        // for controllerAs syntax
        // var vm = this;

        activate();

        ////////////////

        function activate() {
            $log.log('I\'m a line from custom.js');
        }
    }
})();

// Show Progress
function showProgress() {
    $("div#mainSpinner").show();
}

// Hide Progress
function hideProgress() {
    $("div#mainSpinner").hide();
}

function showErrors(err) {
    if (!err)
        return null;

    var errors = "";
    if (err.data) {
        for (var key in err.data.ModelState) {
            var errMsg = err.data.ModelState[key][0];
            errors += errMsg + "<br/>";
        }
    } else {
        for (var key in err.ModelState) {
            var errMsg = err.ModelState[key][0];
            errors += errMsg + "<br/>";
        }
    }
    if ($.trim(errors) !== "")
        return errors;
}
