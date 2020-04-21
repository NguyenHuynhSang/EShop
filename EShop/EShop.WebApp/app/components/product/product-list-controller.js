(function (app) {
    app.controller('product-list-controller', productListController)
    productListController.$inject = ['$scope','api-service']; 
    function productListController($scope,apiService) {
        $scope.productList = [];
        $scope.getListProduct = getListProduct;

        function getListProduct() {
            apiService.get('/api/Product/GetAll', null, function (result) {
                $scope.productList = result.data;
            }, function () {
                    console.log('Load product api failed.');
            });

        }
        $scope.getListProduct();
    }
})(angular.module('eshop-product'));