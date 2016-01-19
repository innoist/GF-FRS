/*
    View for the dashBoard Used to keep the viewmodel clear of UI related logic
*/
define("dashBoard/dashBoard.view",
    ["jquery", "dashBoard/dashBoard.viewModel"], function ($, regionViewModel) {
        var ist = window.ist || {};
        // View 
        ist.dashBoard.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#RegionBinding")[0],
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                    // Handle Sorting
                    handleSorting("RegionTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getRegions);
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
            };
        })(regionViewModel);
        // Initialize the view model
        if (ist.dashBoard.view.bindingRoot) {
            regionViewModel.initialize(ist.dashBoard.view);
        }
        return ist.dashBoard.view;
    });

function readURLlogo(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = new Image;
            img.onload = function () {
                if (img.height > 75 || img.width > 215) {
                    toastr.warning("Image should have Max. width 75px and height 215px!");
                } else {
                    $('#dashboardLogo')
                    .attr('src', e.target.result)
                    .width(120)
                    .height(120);
                }
            };
            img.src = reader.result;
            ist.dashBoard.viewModel.dashboard().logoUrl(img.src);
        };
        reader.readAsDataURL(input.files[0]);
    }
}
function readURLBanner(input, element) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = new Image;
            img.onload = function () {
                if (img.height < 260 || img.width < 1024) {
                    toastr.warning("Image should have Max. width 1024px and height 260px!");
                } else {
                    $(this)
                    .attr('src', e.target.result)
                    .width(120)
                    .height(120);
                }
            };
            img.src = reader.result;
            if(element==1)
                ist.dashBoard.viewModel.dashboard().banner1Url(img.src);
            else if (element == 2)
                ist.dashBoard.viewModel.dashboard().banner2Url(img.src);
            else ist.dashBoard.viewModel.dashboard().banner3Url(img.src);
        };
        reader.readAsDataURL(input.files[0]);
    }
}