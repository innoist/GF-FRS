(function() {
    angular
        .module('app.Users')
        .service('UsersService', UsersService);

    UsersService.$inject = ['$http'];
    function UsersService($http) {
        this.getUsers = getUsers;

        function getUsers(onSuccess, onError) {
            var url = window.frsApiUrl + '/api/Users';

            onError = onError || function () { alert('Failure loading Meta Data'); };
            //onSuccess = onSuccess || function () { alert('Loading Complete'); };


            $http
              .get(url)
              .success(onReady)
              .error(onError);
        }
    }

})();