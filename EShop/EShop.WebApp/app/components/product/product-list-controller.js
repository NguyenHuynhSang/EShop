(function (app) {
    app.controller('product-list-controller', productListController)
    productListController.$inject = ['$scope', 'api-service', 'notification-service'];

    function productListController($scope, apiService, notificationService) {
        $scope.productList = [];
        $scope.getListProduct = getListProduct;
        $scope.keyWord = '';

        $scope.search = search;


        function search() {
            getListProduct();
        }

        function getListProduct() {
            //var config = {
            //    params: {
            //        keyword: $scope.keyWord,
            //        action: "getAll",
            //    }
            //}

            apiService.get('/api/Product/GetAll', null, function (result) {
                $scope.productList = result.data;
                if (result.data.length == 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào");
                } else {

                    notificationService.displaySuccess("Tìm thấy " + result.data.length + " bản ghi");
                }


            }, function () {
                console.log('Load product api failed.');
                notificationService.displayError("Không lấy được dữ liệu từ server");
            });

        }

        $scope.getListProduct();
    }
})(angular.module('eshop-product'));