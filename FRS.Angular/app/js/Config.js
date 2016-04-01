/// <reference path="../views/CustomerStatements/CustomerStatementsDetail.html" />
/**=========================================================
 * Module: config.js
 * App routes and resources configuration
 =========================================================*/


(function () {
    'use strict';

    angular
        .module('app.routes')
        .config(routesConfig);

    routesConfig.$inject = ['$stateProvider', '$locationProvider', '$urlRouterProvider', 'RouteHelpersProvider',
        '$controllerProvider', '$provide'];
    function routesConfig($stateProvider, $locationProvider, $urlRouterProvider, helper, $controllerProvider, $provide) {

        // Set the following to true to enable the HTML5 Mode
        // You may have to set <base> tag in index and a routing configuration in your server
        $locationProvider.html5Mode(false);

        // defaults to dashboard
        $urlRouterProvider.otherwise('/account/login');

        var core = angular.module('app.core');
        // Lazy loading
        core.lazy = {
            controller: $controllerProvider.register,
            factory: $provide.factory,
            service: $provide.service
        }

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
                resolve: helper.resolveFor('ui.grid', 'ui.select', 'loadMetaData.module')
            })
            .state('app.OracleGlEntry', {
                url: '/OracleGlEntry',
                title: 'Oracle GL Entries',
                templateUrl: helper.basepath('../../../../app/views/OracleGLEntries/OracleGLEntries.html'),
                controller: 'OracleGlEntryController',
                controllerAs: 'oglec',
                resolve: helper.resolveFor('ui.grid', 'ui.select', 'OracleGlEntry.module')
            })
            .state('app.OracleGlLoad', {
                url: '/OracleGlLoad',
                title: 'Oracle GL Loads',
                templateUrl: helper.basepath('../../../../app/views/OracleGlLoad/OracleGlLoad.html'),
                controller: 'OracleGlLoadController',
                controllerAs: 'ogllc',
                resolve: helper.resolveFor('ui.grid', 'ui.select', 'OracleGlLoad.module')
            })
            .state('app.OracleGlLoadDetail', {
                url: '/OracleGlLoadDetail/:Id',
                title: 'Oracle GL Load Detail',
                templateUrl: helper.basepath('../../../../app/views/OracleGlLoad/OracleGlLoadDetail.html'),
                controller: 'OracleGlLoadDetailController',
                controllerAs: 'oglldc',
                resolve: helper.resolveFor('ui.grid', 'ui.select', 'OracleGlLoad.module')
            })
            .state('app.MT940Loads', {
                url: '/MT940Loads',
                title: 'MT940 Loads',
                templateUrl: helper.basepath('../../../../app/views/MT940Loads/MT940Loads.html'),
                controller: 'MT940LoadsController',
                controllerAs: 'mt940lc',
                resolve: helper.resolveFor('ui.grid', 'ui.select', 'MT940Loads.module')
            })
            .state('app.MT940LoadDetail', {
                url: '/MT940LoadDetail/:MT940LoadId',
                title: 'MT940 Load Detail',
                templateUrl: helper.basepath('../../../../app/views/MT940Loads/MT940LoadDetail.html'),
                controller: 'MT940LoadDetailController',
                controllerAs: 'mt940ldc',
                resolve: helper.resolveFor('ui.grid', 'ui.select', 'MT940Loads.module')
            })
            .state('app.CustomerStatements', {
                url: '/CustomerStatements/:Id',
                title: 'Customer Statements',
                templateUrl: helper.basepath('../../../../app/views/CustomerStatements/CustomerStatements.html'),
                controller: 'CustomerStatementsController',
                controllerAs: 'custsc',
                resolve: helper.resolveFor('ui.grid', 'ui.select', 'CustomerStatements.module')
            })
            
             .state('app.CustomerStatementsDetail', {
                 url: '/CustomerStatementDetail/:MT940CustomerStatementId',
                 title: 'MT940 Customer Statment Detail',

                 templateUrl: helper.basepath('../../../../app/views/CustomerStatements/CustomerStatementsDetail.html'),
                 controller: 'CustomerStatementsDetailController',
                 controllerAs: 'mt940CustStatDetail',
                 resolve: helper.resolveFor('ui.grid', 'ui.select', 'CustomerStatements.module')
             })
            .state('app.CustomerStatementTransactions', {
                url: '/CustomerStatementTransactions/:Id?/:MT940LoadId?',
                title: 'Customer Statement Transactions',
                templateUrl: helper.basepath('../../../../app/views/CustomerStatementTransactions/CustomerStatementTransactions.html'),
                controller: 'CustomerStatementTransactionsController',
                controllerAs: 'custstc',
                resolve: helper.resolveFor('ui.grid', 'ui.select', 'CustomerStatementTransactions.module')
            })
            .state('app.MT940Balance', {
                url: '/MT940Balance',
                title: 'MT940 Balance',
                templateUrl: helper.basepath('../../../../app/views/MT940Balance/MT940Balance.html'),
                controller: 'MT940BalanceController',
                controllerAs: 'mt940bal',
                resolve: helper.resolveFor('ui.grid', 'ui.select', 'MT940Balance.module')
            })
            .state('app.CreateMetaData', {
                url: '/CreateMetaData/:Id',
                title: 'New MetaData',
                templateUrl: helper.basepath('../../../../app/views/CreateMetaData/CreateMetaData.html'),
                controller: 'CreateMetaDataController',
                controllerAs: 'cmdc',
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'createMetaData.module', 'ui.select')
            })
            .state('app.LogsModule', {
                url: '/Log',
                title: 'Logs',
                templateUrl: helper.basepath('../../../../app/views/Log/LogLV.html'),
                controller: 'LogsController',
                controllerAs: 'lc',
                resolve: helper.resolveFor('ui.grid', 'ui.select', 'log.module')
            })
            .state('app.Load', {
                url: '/Load/:Type',
                title: 'Load',
                templateUrl: helper.basepath('../../../../app/views/Load/Load.html'),
                controller: 'LoadController',
                controllerAs: 'ldc',
                resolve: helper.resolveFor('parsley', 'load.module', 'ui.select', 'oitozero.ngSweetAlert', 'filestyle')
            })
            .state('app.Users', {
                url: '/Users',
                title: 'Users',
                templateUrl: helper.basepath('../../../../app/views/Users/Users.html'),
                controller: 'UsersController',
                controllerAs: 'uc',
                resolve: helper.resolveFor('ui.grid', 'ui.select', 'users.module')
            })
            .state('app.Profile', {
                url: '/Profile/:Name',
                title: 'Profile',
                templateUrl: helper.basepath('../../../../app/views/Users/Profile.html'),
                controller: 'ProfileController',
                controllerAs: 'upc',
                resolve: helper.resolveFor('ui.select', 'profile.module')
            })
            .state('app.changePassword', {
                url: '/changePassword',
                title: 'Change Password',
                templateUrl: helper.basepath('../../../../app/views/Users/ChangePassword.html'),
                controller: 'changePasswordController',
                controllerAs: 'changePassword',
                resolve: helper.resolveFor('changePassword.module')
            })
            .state('app.rightsManagement', {
                url: '/RightsManagement',
                title: 'Rights Management',
                templateUrl: helper.basepath('../../../../app/views/RightsManagement/index.html'),
                controller: 'RightsManagementController',
                controllerAs: 'rightsManagement',
                resolve: helper.resolveFor('rightsManagement.module', 'ui.grid', 'loaders.css', 'spinkit', 'ui.select')
            })
            .state('app.LoadStatus', {
                url: '/LoadStatus',
                title: 'Load Status',
                templateUrl: helper.basepath('../../../../app/views/LoadStatus/LoadStatus.html'),
                controller: 'LoadStatusController',
                controllerAs: 'lsc',
                resolve: helper.resolveFor('LoadStatus.module', 'ui.grid')
            })
            .state('app.NewLoadStatus', {
                url: '/NewLoadStatus/:Id',
                title: 'New Load Status',
                templateUrl: helper.basepath('../../../../app/views/LoadStatus/NewLoadStatus.html'),
                controller: 'NewLoadStatusController',
                controllerAs: 'nlsc',
                resolve: helper.resolveFor('LoadStatus.module', 'ui.select', 'oitozero.ngSweetAlert')
            })
            .state('app.FiscalYear', {
                url: '/FiscalYear',
                title: 'FiscalYear',
                templateUrl: helper.basepath('../../../../app/views/FiscalYear/FiscalYear.html'),
                controller: 'FiscalYearController',
                controllerAs: 'fyc',
                resolve: helper.resolveFor('FiscalYear.module', 'ui.grid')
            })
            .state('app.NewFiscalYear', {
                url: '/NewFiscalYear/:Id',
                title: 'New Fiscal Year',
                templateUrl: helper.basepath('../../../../app/views/FiscalYear/NewFiscalYear.html'),
                controller: 'NewFiscalYearController',
                controllerAs: 'nfyc',
                resolve: helper.resolveFor('FiscalYear.module', 'oitozero.ngSweetAlert')
            })
            .state('app.Currency', {
                url: '/Currency',
                title: 'Currency',
                templateUrl: helper.basepath('../../../../app/views/Currency/Currency.html'),
                controller: 'CurrencyController',
                controllerAs: 'cc',
                resolve: helper.resolveFor('currency.module', 'ui.grid')
            })
            .state('app.NewCurrency', {
                url: '/NewCurrency/:Id',
                title: 'New Currency',
                templateUrl: helper.basepath('../../../../app/views/Currency/NewCurrency.html'),
                controller: 'NewCurrencyController',
                controllerAs: 'ncc',
                resolve: helper.resolveFor('newCurrency.module', 'oitozero.ngSweetAlert')
            })
            .state('app.CreateStatus', {
                url: '/CreateStatus/:Id',
                title: 'Create Status',
                templateUrl: helper.basepath('../../../../app/views/Status/CreateStatus.html'),
                controller: 'CreateStatusController',
                controllerAs: 'csc',
                resolve: helper.resolveFor('createStatus.module', 'oitozero.ngSweetAlert')
            })
            .state('app.Status', {
                url: '/Status',
                title: 'Status',
                templateUrl: helper.basepath('../../../../app/views/Status/Status.html'),
                controller: 'StatusController',
                controllerAs: 'sc',
                resolve: helper.resolveFor('status.module', 'ui.grid')
            })
            .state('app.LoadType', {
                url: '/LoadType',
                title: 'Load Type',
                templateUrl: helper.basepath('../../../../app/views/LoadType/LoadType.html'),
                controller: 'LoadTypeController',
                controllerAs: 'ltc',
                resolve: helper.resolveFor('loadType.module', 'ui.grid')
            })
            .state('app.CreateLoadType', {
                url: '/CreateLoadType/:Id',
                title: 'Create Load Type',
                templateUrl: helper.basepath('../../../../app/views/LoadType/CreateLoadType.html'),
                controller: 'CreateLoadTypeController',
                controllerAs: 'cltc',
                resolve: helper.resolveFor('createLoadType.module', 'oitozero.ngSweetAlert', 'ui.select')
            })
            .state('app.Sources', {
                url: '/Sources',
                title: 'Sources - Reference Data',
                templateUrl: helper.basepath('../../../../app/views/Source/SourceIndex.html'),
                controller: 'SourceIndexController',
                controllerAs: 'sic',
                resolve: helper.resolveFor('source.index.module', 'ui.grid')
            })
            .state('app.CreateSources', {
                url: '/CreateSource/:Id',
                title: 'Create Sources - Reference Data',
                templateUrl: helper.basepath('../../../../app/views/Source/SourceCreate.html'),
                controller: 'SourceCreateController',
                controllerAs: 'scc',
                resolve: helper.resolveFor('source.create.module', 'oitozero.ngSweetAlert', 'ui.select')
            })
            .state('app.ManualReconciliation', {
                url: '/ManualReconciliation',
                title: 'Manual Reconciliation',
                templateUrl: helper.basepath('../../../../app/views/ManualReconciliation/ManualReconciliation.html'),
                controller: 'ManualReconciliationController',
                controllerAs: 'mrc',
                resolve: helper.resolveFor('ManualReconciliation.module', 'ui.grid', 'ui.select', 'pageguide')
            })
              .state('app.ReconciliationMapping', {
                  url: '/ReconciliationMapping',
                  title: 'Reconciliation Mappings',
                  templateUrl: helper.basepath('../../../../app/views/ManualReconciliation/ReconciliationList.html'),
                  controller: 'ReconciliationMappingController',
                  controllerAs: 'rm',
                  resolve: helper.resolveFor('ReconciliationMapping.module', 'ui.grid', 'ui.select')
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
                templateUrl: 'app/Views/Users/login.html',
                resolve: helper.resolveFor('login.module'),
            })
            .state('account.register', {
                url: '/register',
                title: 'Register',
                templateUrl: 'app/Views/Users/Register.html',
                resolve: helper.resolveFor('register.module'),
            })
            .state('account.recover', {
                url: '/recover',
                title: 'Recover',
                templateUrl: 'app/Views/Users/Recover.html',
                controller: 'ForgotPasswordController',
                controllerAs: 'forgotPassword',
                resolve: helper.resolveFor('recover.module'),
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
                controllerAs: 'resetPassword',
                resolve: helper.resolveFor('resetPassword.module'),
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
