(function (app) {
    app.controller('product-list-controller', productListController)
    productListController.$inject = ['$scope', 'api-service', 'notification-service'];

    function productListController($scope, apiService, notificationService) {
        $scope.productList = [];
        $scope.getListProduct = getListProduct;
        $scope.keyWord = '';

        $scope.search = search;

        $scope.delProduct = delProduct;

        $scope.product = {};



        function delProduct(any item) {
            $scope.product = item;
            apiService.del('/api/Product/delete', $scope.product, function (result) {
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

        function search() {
            getListProduct();
        }

        function getListProduct() {
            var config = {
                params: {
                    keyword: $scope.keyWord,
                }
            }

            apiService.get('/api/Product/GetAll', config, function (result) {
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