
/*
* Các api service chương trình cung cấp
* */
(function (app) {
    app.factory('api-service', apiService);

    apiService.$inject = ['$http','notification-service'];
    function apiService($http,notificationService)
    {
        return {
            get:get,
            post:post,
            put:put,
            del:del

        }

        function get(url, params, success, failed) {
            $http.get(url, params).then(function (result) {
                success(result);
            },(function (error) {
                failed(error);
            }));

        }

        function post(url, data, success, failed) {
            $http.post(url, data).then(function (result) {

                success(result);
            },(function (error) {
                if (error.status==='401'){

                    notificationService.displayError('Yêu cầu đăng nhập');
                }
                notificationService.displayError(error);
                failed(error);
            }));

        }

        function del(url, data, success, failed) {
            $http.delete(url, data).then(function (result) {
                success(result);
            },(function (error) {
                if (error.status===401){

                    notificationService.displayError('Yêu cầu đăng nhập');
                }
                /// Server java không hỗ trợ delete???
             //   notificationService.displayError(error);
         //       failed(error);
            }));

        }

        function put(url, data, success, failed) {
            $http.put(url, data).then(function (result) {

                success(result);
            },(function (error) {
                if (error.status===401){

                    notificationService.displayError('Yêu cầu đăng nhập');
                }
                notificationService.displayError(error);
                failed(error);
            }));

        }

        f
    }

})(angular.module('eshop-common'));