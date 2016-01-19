/*
    View for the RA Report. Used to keep the viewmodel clear of UI related logic
*/
define("common/raReport.view",
    ["jquery", "common/raReport.viewModel"], function ($, raReportViewModel) {

        var ist = window.ist || {};

        // View 
        ist.raReport.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#rentalAgreementSearchBinding")[0],
                // Initialize
                initialize = function () {
                    
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
            };
        })(raReportViewModel);

        // Initialize the view model
        if (ist.raReport.view.bindingRoot) {
            raReportViewModel.initialize(ist.raReport.view);
        }
        return ist.raReport.view;
    });