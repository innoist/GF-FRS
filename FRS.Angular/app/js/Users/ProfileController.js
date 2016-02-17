(function() {
    'use strict';

    angular.module('app.Profile',[]);


})();

(function() {
    'use strict';

    angular
        .module('app.Profile', [])
        .controller('ProfileController', ProfileController);

    ProfileController.$inject = ['$scope', 'ProfileService'];

    function ProfileController($scope, ProfileService) {
        var vm = this;

        vm.saveProfile = function () {
            ProfileService.profile = vm.profile;
            ProfileService.saveProfile(function(response) {
                
            }, function(err) {
                
            });
        }


    }
})();