﻿/**=========================================================
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
              resolve: helper.resolveFor('fastclick', 'modernizr', 'icons', 'screenfull', 'animo', 'sparklines', 'slimscroll', 'classyloader', 'toaster', 'whirl')
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
                resolve: helper.resolveFor('ui.grid', 'loaders.css', 'spinkit', 'ui.select')
            })
            .state('app.CreateMetaData', {
                url: '/CreateMetaData',
                title: 'New MetaData',
                templateUrl: helper.basepath('../../../../app/views/CreateMetaData/CreateMetaData.html'),
                controller: 'CreateMetaDataController',
                controllerAs: 'cmdc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert')
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
                resolve: helper.resolveFor('ui.grid')
            })
        .state('page', {
            url: '/page',
            templateUrl: 'app/pages/page.html',
            resolve: helper.resolveFor('modernizr', 'icons'),
            controller: ['$rootScope', function ($rootScope) {
                $rootScope.app.layout.isBoxed = false;
            }]
        })
          .state('page.login', {
              url: '/login',
              title: 'Login',
              templateUrl: 'app/pages/login.html'
          })
          .state('page.register', {
              url: '/register',
              title: 'Register',
              templateUrl: 'app/pages/register.html'
          })
          .state('page.recover', {
              url: '/recover',
              title: 'Recover',
              templateUrl: 'app/pages/recover.html'
          })
          .state('page.lock', {
              url: '/lock',
              title: 'Lock',
              templateUrl: 'app/pages/lock.html'
          })
          .state('page.404', {
              url: '/404',
              title: 'Not Found',
              templateUrl: 'app/pages/404.html'
          })

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
        ;

    } // routesConfig

})();