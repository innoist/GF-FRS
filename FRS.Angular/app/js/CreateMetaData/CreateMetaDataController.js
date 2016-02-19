﻿//Create MetaData
(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('CreateMetaDataController', CreateMetaDataController);

    CreateMetaDataController.$inject = ['$http', '$scope', '$state', '$stateParams', 'CreateMetaDataService', 'SweetAlert', 'toaster'];

    function CreateMetaDataController($http, $scope, $state, $stateParams, CreateMetaDataService, SweetAlert, toaster) {
        var vm = this;
        var metaDataId = 0;

        //#region Get Data from DB
        CreateMetaDataService.getLoadMetaData(function (data) {
            vm.LoadTypes = data.LoadTypes;
            vm.Sources = data.Sources;
            vm.Currencies = data.Currencies;
            vm.Status = data.Statuses;
        });
        
        //#endregion

        //#region Web Model properties
        vm.LoadMetaData = {

        }
        //#endregion
        vm.submitted = false;
        $scope.Currency = '';
        $scope.IsReadOnly = false;

        vm.validateInput = function (property, type) {
            if (!property || !type) {
                return false;
            }
            return (property.$dirty || vm.submitted) && property.$error[type];
        };
        //#region Post Data
        $scope.saveLoadMetaDataDetail = function (isNew) {
            vm.submitted = true;
            if (vm.formValidate.$valid) {
                console.log('Submitted!!');
            } else {
                toaster.pop("error", "Fields are required", "Notification");
                return false;
            }
            vm.LoadMetaData.LoadMetaDataId = metaDataId;
            vm.LoadMetaData.Header = $scope.Header;
            vm.LoadMetaData.Footer = $scope.Footer;
            vm.LoadMetaData.Name = $scope.Name;
            vm.LoadMetaData.Description = $scope.Description;
            vm.LoadMetaData.LoadTypeId = vm.LoadTypes.selected.Id;
            vm.LoadMetaData.SourceId = vm.Sources.selected.Id;
            vm.LoadMetaData.CurrencyId = vm.Currencies.selected.Id;
            vm.LoadMetaData.StatusId = vm.Status.selected.Id;

            CreateMetaDataService.saveLoadMetaDataDetail(vm.LoadMetaData, onSuccess, onError);
            function onSuccess(response) {
                if (response.data == true) {
                    toaster.pop("success", "Metadata Saved successfully", "Notification");
                }
            }
            function onError(err) {
                toaster.error(err.statusText, err.data.Message);
                showErrors(err);
            }

            if (isNew) {

                $state.go('app.CreateMetaData');
            }
            if (!isNew) {
                $state.go('app.LoadMetaData');
            }
            defaultModel();
        }
        //#endregion

        $scope.cancelBtn = function () {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'All data you entered in form will be lost!',
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, cancel saving form.!',
                cancelButtonText: 'No, stay on this page!',
                closeOnConfirm: true,
                closeOnCancel: true,
            }, function (isConfirm) {
                if (isConfirm) {
                    //SweetAlert.swal('Deleted!', 'Your imaginary file has been deleted.', 'success');
                    $state.go('app.LoadMetaData');
                } else {
                    //SweetAlert.swal('Cancelled', 'Stay on this page', 'error');
                }
            });

        }


        //#region Delete Data
        $scope.deleteLoadMetaData = function (loadMetaDataId) {
            $http.delete('/api/LoadMetaData', { params: { loadMetaDataId: loadMetaDataId } })
                        .success(function (data, status, headers, config) {
                            if (data != false) {
                                $scope.getLoadMetaDataList();
                            }
                        });
        }
        //#endregion

        //#region Functions
        var defaultModel = function () {
            $scope.LoadMetaDataId = 0;
            $scope.LoadTypeId = 0;
            $scope.SourceId = 0;
            $scope.Header = '';
            $scope.Footer = '';
            $scope.Name = '';
            $scope.CurrencyId = 0;
            $scope.Description = '';
            $scope.StatusId = 0;
            $scope.Currency = '';
            vm.submitted = false;
            vm.LoadTypes.selected = null;
            vm.Sources.selected = null;
            vm.Currencies.selected = null;
            vm.Status.selected = null;
        }

        if ($stateParams.Id !== "") {
            metaDataId = $stateParams.Id;
            CreateMetaDataService.loadMetaDataById(metaDataId, function (response) {
                $scope.Header = response.Header;
                $scope.Footer = response.Footer;
                $scope.Name = response.Name;
                $scope.Description = response.Description;
                var selectedLoadType = $(vm.LoadTypes).filter(function (index, item) {
                    return item.Id === response.LoadTypeId;
                });
                if (selectedLoadType.length > 0) {
                    vm.LoadTypes.selected = selectedLoadType[0];
                }

                var selectedSource = $(vm.Sources).filter(function (index, item) {
                    return item.Id === response.SourceId;
                });
                if (selectedSource.length > 0) {
                    vm.Sources.selected = selectedSource[0];
                }

                var selectedCurrency = $(vm.Currencies).filter(function (index, item) {
                    return item.Id === response.CurrencyId;
                });
                if (selectedCurrency.length > 0) {
                    vm.Currencies.selected = selectedCurrency[0];
                }

                var selectedStatus = $(vm.Status).filter(function (index, item) {
                    return item.Id === response.StatusId;
                });
                if (selectedStatus.length > 0) {
                    vm.Status.selected = selectedStatus[0];
                }
                toaster.success("", "Metadata loaded successfully.");
            },
                function (err) {
                    toaster.error("", showErrors(err));
                });
        } else {
            metaDataId = 0;
        }
    }


})();