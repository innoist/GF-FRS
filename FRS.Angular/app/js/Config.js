/**=========================================================
 * Module: config.js
 * App routes and resources configuration
 =========================================================*/


(function () {
    'use strict';

    angular
        .module('app.routes')
        .config(routesConfig);

    routesConfig.$inject = ['$stateProvider', '$locationProvider', '$urlRouterProvider', 'RouteHelpersProvider'];
    function routesConfig($stateProvider, $locationProvider, $urlRouterProvider, helper) {

        // Set the following to true to enable the HTML5 Mode
        // You may have to set <base> tag in index and a routing configuration in your server
        $locationProvider.html5Mode(false);

        // defaults to dashboard
        $urlRouterProvider.otherwise('/FRS/dashboard');

        //
        // Application Routes
        // -----------------------------------
        $stateProvider
            .state('app', {
                url: '/FRS',
                abstract: true,
                templateUrl: helper.basepath('../../../../app/views/FRS.html'),
                resolve: helper.resolveFor('fastclick', 'modernizr', 'icons', 'screenfull', 'animo', 'sparklines', 'slimscroll', 'classyloader', 'toaster', 'whirl', 'loaders.css', 'spinkit')
            })
            .state('app.dashboard', {
                url: '/dashboard',
                title: 'Dashboard',
                templateUrl: helper.basepath('../../../../app/views/dashboard.html'),
                resolve: helper.resolveFor('flot-chart', 'flot-chart-plugins', 'weather-icons')
            })
            .state('app.LoadMetaData', {
                url: '/LoadMetaData',
                title: 'Load MetaData',
                templateUrl: helper.basepath('../../../../app/views/LoadMetaData/LoadMetaData.html'),
                controller: 'LoadMetaDataController',
                controllerAs: 'mdc',
                resolve: helper.resolveFor('ui.grid', 'ui.select')
            })
            .state('app.CreateMetaData', {
                url: '/CreateMetaData',
                title: 'New MetaData',
                templateUrl: helper.basepath('../../../../app/views/CreateMetaData/CreateMetaData.html'),
                controller: 'CreateMetaDataController',
                controllerAs: 'cmdc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert')
            })
            .state('app.LogsModule', {
                url: '/Log',
                title: 'Logs',
                templateUrl: helper.basepath('../../../../app/views/Log/Log.html'),
                controller: 'LogController',
                controllerAs: 'mdc',
                resolve: helper.resolveFor('ui.grid', 'loaders.css', 'spinkit', 'ui.select')
            })
            .state('app.Load', {
                url: '/Load',
                title: 'Load',
                templateUrl: helper.basepath('../../../../app/views/Load/Load.html'),
                controller: 'LoadController',
                controllerAs: 'ldc',
                resolve: helper.resolveFor('parsley')
            })
            .state('app.Users', {
                url: '/Users',
                title: 'Users',
                templateUrl: helper.basepath('../../../../app/views/Users/Users.html'),
                controller: 'UsersController',
                controllerAs: 'uc',
                resolve: helper.resolveFor('ui.grid', 'ui.select')
            })
            //.state('app.Profile', {
            //    url: '/Profile',
            //    title: 'Profile',
            //    templateUrl: helper.basepath('../../../../app/views/Users/Profile.html'),
            //    controller: 'ProfileController',
            //    controllerAs: 'upc',
            //    resolve: helper.resolveFor('ui.select')
            //})
            .state('app.Profile', {
                url: '/Profile/:Name',
                title: 'Profile',
                templateUrl: helper.basepath('../../../../app/views/Users/Profile.html'),
                controller: 'ProfileController',
                controllerAs: 'upc',
                resolve: helper.resolveFor('ui.select')
            })
            .state('app.rightsManagement', {
                url: '/RightsManagement',
                title: 'Rights Management',
                templateUrl: helper.basepath('../../../../app/views/RightsManagement/index.html'),
                controller: 'RightsManagementController',
                controllerAs: 'rightsManagement',
                resolve: helper.resolveFor('ui.grid', 'loaders.css', 'spinkit', 'ui.select')
            })
            .state('account', {
                url: '/account',
                templateUrl: 'app/pages/page.html',
                resolve: helper.resolveFor('modernizr', 'icons', 'toaster'),
                controller: [
                    '$rootScope', function($rootScope) {
                        $rootScope.app.layout.isBoxed = false;
                    }
                ]
            })
            .state('account.login', {
                url: '/login',
                title: 'Login',
                //templateUrl: 'app/pages/login.html'
                templateUrl: 'app/Views/Users/login.html'
            })
            .state('account.register', {
                url: '/register',
                title: 'Register',
                templateUrl: 'app/Views/Users/Register.html'
            })
            .state('account.recover', {
                url: '/recover',
                title: 'Recover',
                templateUrl: 'app/Views/Users/Recover.html',
                controller: 'ForgotPasswordController',
                controllerAs: 'forgotPassword'
    })
            .state('account.lock', {
                url: '/lock',
                title: 'Lock',
                templateUrl: 'app/Views/Users/Lock.html'
            })
            .state('account.404', {
                url: '/404',
                title: 'Not Found',
                templateUrl: 'app/pages/404.html'
            })
            .state('account.ResetPassword', {
                url: '/ResetPassword',
                title: 'Reset Password',
                templateUrl: 'app/Views/Users/ResetPassword.html',
                controller: 'ResetPasswordController',
                controllerAs: 'resetPassword'
            });

        //
        // CUSTOM RESOLVES
        //   Add your own resolves properties
        //   following this object extend
        //   method
        // -----------------------------------
        // .state('app.someroute', {
        //   url: '/some_url',
        //   templateUrl: 'path_to_template.html',
        //   controller: 'someController',
        //   resolve: angular.extend(
        //     helper.resolveFor(), {
        //     // YOUR RESOLVES GO HERE
        //     }
        //   )
        // })

    } // routesConfig

})();
