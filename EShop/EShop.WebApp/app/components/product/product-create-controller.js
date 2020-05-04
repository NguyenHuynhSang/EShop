(function (app) {
    app.controller('product-create-controller', productCreateController)
    productCreateController.$inject = ['api-service','$scope', 'notification-service','$state'];

    function productCreateController(apiService,$scope,notificationService,$state) {
        $scope.newProduct = {};
        $scope.CreateProduct = CreateProduct;
        function CreateProduct() {
            apiService.post('/api/Product/CreateProduct', $scope.newProduct, function (result) {
                notificationService.displaySuccess("Thêm mới bản ghi thành công");
                $state.go('product-list');
            }, function () {
                console.log('Load product api failed.');
                notificationService.displayError("Thêm mới bản ghi KHÔNG thành công");
            });
        }

    }
})(angular.module('eshop-product'));