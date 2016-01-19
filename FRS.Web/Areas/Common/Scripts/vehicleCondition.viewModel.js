/*
    Module with the view model for vehicleCondition
*/
define("common/vehicleCondition.viewModel",
    ["jquery", "amplify", "ko"], function ($, amplify, ko) {
        var ist = window.ist || {};
        ist.vehicleCondition = {
            viewModel: (function () {
                var// The view 
                    view,
                    // True if we are loading data
                    isLoading = ko.observable(false),
                    // True if Editable
                    isEditable = ko.observable(true),
                    // On Proceed
                    afterProceed = ko.observable(),
                    // On Cancel
                    afterCancel = ko.observable(),
                    // Description
                    description = ko.observable(),
                    // Proceed with the request
                    proceed = function () {
                        if (typeof afterProceed() === "function") {
                            afterProceed()({ Condition: view.getBitwiseCondition(), Description: description() });
                        }
                        hide();
                    },
                    // Reset Dialog
                    resetDialog = function () {
                        afterCancel(undefined);
                        afterProceed(undefined);
                    },
                    // Show the dialog
                    show = function (bitwiseCondition, conditionDescription, editable) {
                        isLoading(true);
                        isEditable(editable !== null && editable !== undefined ? editable : true);
                        view.show();
                        view.setBitwiseCondition(bitwiseCondition || "000000000000000000000000000");
                        description(conditionDescription || undefined);
                    },
                    // Hide the dialog
                    hide = function () {
                        // Reset Call Backs
                        resetDialog();
                        view.hide();
                    },
                    // Cancel 
                    cancel = function () {
                        if (typeof afterCancel() === "function") {
                            afterCancel()();
                        }
                        hide();
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                    };

                return {
                    isLoading: isLoading,
                    initialize: initialize,
                    show: show,
                    cancel: cancel,
                    proceed: proceed,
                    afterProceed: afterProceed,
                    afterCancel: afterCancel,
                    resetDialog: resetDialog,
                    hide: hide,
                    description: description,
                    isEditable: isEditable
                };
            })()
        };

        return ist.vehicleCondition.viewModel;
    });

