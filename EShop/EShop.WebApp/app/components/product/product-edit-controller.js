(function (app) {
    app.controller('product-edit-controller', productEditController)
    productEditController.$inject = ['api-service','$scope', 'notification-service','$state','$stateParams'];

    function productEditController(apiService,$scope,notificationService,$state,$stateParams) {
        $scope.newProduct = {};
        $scope.EditProduct = EditProduct;

        function LoadProductByID() {
            var config = {
                params: {
                    id: $stateParams.id,
                }
            }
            apiService.get('/api/Product/GetById', config, function (result) {
                $scope.newProduct = result.data;
                console.log($scope.newProduct);
            }, function () {
                notificationService.displayError("Không lấy được dữ liệu từ server");
            });


        }


        function EditProduct() {
            apiService.put('/eshopcore_war/api/json',  JSON.stringify($scope.jsonEntity)  , function (result) {
                notificationService.displaySuccess("Sửa bản ghi thành công");
                $state.go('product-list');
            }, function () {
                console.log('Load product api failed.');
                notificationService.displayError("Thêm mới bản ghi KHÔNG thành công");
            });
        }
        LoadProductByID();
    }
})(angular.module('eshop-product'));