(function (app) {
    app.factory('api-service', apiService);

    apiService.$inject = ['$http'];
    function apiService($http)
    {
        return {
            get:get
        }

        function get(url, params, success, failed) {
            $http.get(url, params).then(function (result) {
                success(result);
            },(function (error) {
                failed(error);
            }));

        }

    }

})(angular.module('eshop-common'));