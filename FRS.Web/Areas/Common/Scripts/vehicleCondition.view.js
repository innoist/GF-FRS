/*
    View for the Vehicle Condition. Used to keep the viewmodel clear of UI related logic
*/
define("common/vehicleCondition.view",
    ["jquery", "common/vehicleCondition.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.vehicleCondition.view = (function (specifiedViewModel) {
            var// View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#vehicleConditionDialog")[0],
                // Show the dialog
                show = function() {
                    $("#vehicleConditionDialog").modal("show");
                },
                // Hide the dialog
                hide = function() {
                    $("#vehicleConditionDialog").modal("hide");
                },
                // Get Vehicle Bitwise Condition
                getBitwiseCondition = function() {
                    var returnVal = "";
                    var l, m, n;
                    for (l = 0; l <= 2; l++) {
                        for (m = 0; m <= 2; m++) {
                            for (n = 0; n <= 2; n++) {
                                if ($("#chkVP" + l.toString() + m.toString() + n.toString())[0] != null) {
                                    if ($("#chkVP" + l.toString() + m.toString() + n.toString())[0].checked) {
                                        returnVal = returnVal + "1";
                                    }
                                    else {
                                        returnVal = returnVal + "0";
                                    }
                                }
                            }
                        }
                    }
                    return returnVal;
                },
                // Set Vehicle Pictorial Condition
                setBitwiseCondition = function(value) {
                    var i = 0, l, m, n;
                    if (value && value.length == 27) {
                        for (l = 0; l <= 2; l++) {
                            for (m = 0; m <= 2; m++) {
                                for (n = 0; n <= 2; n++) {
                                    if (value.substring(i, i + 1) == 1)
                                        $("#chkVP" + l.toString() + m.toString() + n.toString())[0].checked = true;
                                    else
                                        $("#chkVP" + l.toString() + m.toString() + n.toString())[0].checked = false;
                                    i = i + 1;
                                }
                            }
                        }
                    }
                };
            
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                show: show,
                hide: hide,
                getBitwiseCondition: getBitwiseCondition,
                setBitwiseCondition: setBitwiseCondition
            };
        })(ist.vehicleCondition.viewModel);

        // Initialize the view model
        if (ist.vehicleCondition.view.bindingRoot) {
            ist.vehicleCondition.viewModel.initialize(ist.vehicleCondition.view);
        }
    });