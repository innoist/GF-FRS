/*
    Module with the view model for the dashBoard
*/
define("dashBoard/dashBoard.viewModel",
    ["jquery", "amplify", "ko", "dashBoard/dashBoard.dataservice", "dashBoard/dashBoard.model",
    "common/confirmation.viewModel", "common/pagination"],
    function($, amplify, ko, dataservice, model, confirmation, pagination) {
        var ist = window.ist || {};
        ist.dashBoard = {
            viewModel: (function() { 
                var view,
                  
                     // Editor View Model
                    editorViewModel = new ist.ViewModel(model.dashBoard),
                    // Selected dashboard
                    dashboard = editorViewModel.itemForEditing,
                    //save button handler
                    onSavebtn = function() {
                    if (dobeforesave())
                        saveSiteData(dashboard());
                },
               // Save Site Data
                    saveSiteData = function (item) {
                        var serverObj = item.convertToServerData();
                        // contents from CKEditors 
                        serverObj.ServiceContents = CKEDITOR.instances['ServiceContentEng'].getData();
                        serverObj.ServiceContentsAr = CKEDITOR.instances['ServiceContentAr'].getData();
                        serverObj.AboutusContents = CKEDITOR.instances['AboutUsContentEng'].getData();
                        serverObj.AboutusContentsAr = CKEDITOR.instances['AboutUsContentAr'].getData();
                        
                        dataservice.saveSiteData(serverObj, {
                        success: function(dataFromServer) {
                          
                            toastr.success("Data saved successfully!");
                        },
                        error: function(exceptionMessage, exceptionType) {
                            if (exceptionType === ist.exceptionType.CaresGeneralException)
                                toastr.error(exceptionMessage);
                            else
                                toastr.error('Data failed to save!');
                        }
                    });
                    },
                   
                //validation check 
                    dobeforesave = function() {
                        if (!dashboard().isValid()) {
                            dashboard().errors.showAllMessages();
                        return false;
                    }
                    return true;
                },
                //cancel button handler
                    onCancelbtn = function() {
                    editorViewModel.revertItem();
                },
               
               
                    //get Regions list from Dataservice
                    getSiteContents= function() {
                        dataservice.getSiteData(
                    {
                        success: function (data) {
                            if (data) {
                               var onj= model.dashBoardServertoClinetMapper(data);
                               dashboard(onj);
                            }
                            

                        },
                        error: function() {
                            isLoadingFleetPools(false);
                            toastr.error(ist.resourceText.RegionLoadFailError);
                        }
                    });
                    },
                    
                // Initialize the view model
                    initialize = function(specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        getSiteContents();
                    };
                return {
                    initialize: initialize,
                    onCancelbtn: onCancelbtn,
                    onSavebtn: onSavebtn,
                    dashboard:dashboard,
                    getSiteContents: getSiteContents
                };
            })()
        };
        return ist.dashBoard.viewModel;
    });
