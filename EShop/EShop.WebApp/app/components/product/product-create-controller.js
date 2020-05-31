(function (app) {
    app.controller('Product-create-controller', ProductCreateController)
    ProductCreateController.$inject = ['api-service','$scope', 'notification-service','$state'];

    function ProductCreateController(apiService,$scope,notificationService,$state) {
        $scope.newProduct = {};
        $scope.CreateProduct = CreateProduct;
        function CreateProduct() {
            apiService.post('/api/Product/CreateProduct', $scope.newProduct, function (result) {
                notificationService.displaySuccess("Thêm mới bản ghi thành công");
                $state.go('Product-list');
            }, function () {
                console.log('Load Product api failed.');
                notificationService.displayError("Thêm mới bản ghi KHÔNG thành công");
            });
        }

    }
})(angular.module('eshop-Product'));
