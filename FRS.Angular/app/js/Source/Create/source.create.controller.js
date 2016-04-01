//Create MetaData
(function () {

    var core = angular.module('app.core');
    // ReSharper disable FunctionsUsedBeforeDeclared
    core.lazy.controller('SourceCreateController', SourceCreateController);

    SourceCreateController.$inject = ['$scope', '$state', '$stateParams', 'CreateSourceService', 'SweetAlert', 'toaster'];

    function SourceCreateController($scope, $state, $stateParams, CreateSourceService, SweetAlert, toaster) {
        var vm = this;
        var SourceId = 0;

        vm.submitted = false;
        vm.Source = {};
        vm.validateInput = function (property, type) {
            if (!property || !type) {
                return false;
            }
            return (property.$dirty || vm.submitted) && property.$error[type];
        };

        $scope.clear = function ($event) {
            $event.stopPropagation();
            vm.Status.selected = null;
            vm.formValidate.$dirty = true;
        };
        //#region Post Data
        $scope.saveSource = function (isNew) {
            vm.submitted = true;
            if (vm.formValidate.$valid) {
                console.log('Submitted!!');
            } else {
                toaster.pop("error", "Error", "Fields are required");
                return false;
            }
            vm.Source.Value = SourceId;
            vm.Source.Name = $scope.Name;
            vm.Source.StatusId = vm.Status.selected.Value;

            CreateSourceService.saveSource(vm.Source, onSuccess, onError);
            function onSuccess(response) {
                if (response.data == true) {
                    toaster.pop("success", "Notification", "Source Saved successfully");
                    if (isNew) {
                        //reseting form
                        //vm.formValidate.$setPristine();
                        $state.go('app.CreateSources');
                    }
                    if (!isNew) {
                        $state.go('app.Sources');
                    }
                }
            }
            function onError(err) {
                toaster.error(err.statusText, err.data.Message);
                showErrors(err);
            }

            
            //reseting form
            vm.formValidate.$setPristine();
            vm.submitted = false;
            SourceId = 0;
            $scope.Name = "";
            vm.Status.selected = null;
        }
        //#endregion

        $scope.cancelBtn = function () {
            if (!vm.formValidate.$dirty) {
                $state.go('app.Sources');
            } else {
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
                        $state.go('app.Sources');
                    }
                });
            }


        }

        SourceId = $stateParams.Id;
        CreateSourceService.loadSourceById(SourceId, function(response) {
            vm.Status = response.Statuses;

            if (response.Source) {
                $scope.update = true;
                $scope.Name = response.Source.Name;
                SourceId = response.Source.Value;

                var selectedStatus = $(vm.Status).filter(function (index, item) {
                    return item.Value === response.Source.StatusId;
                });
                if (selectedStatus.length > 0) {
                    vm.Status.selected = selectedStatus[0];
                }
                toaster.success("", "Source loaded successfully.");
            } else {
                SourceId = 0;
            }

        },
        function (err) {
            toaster.error("", showErrors(err));
        });
        }
})();