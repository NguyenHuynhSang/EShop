(function (app) {
    app.controller('Product-list-controller', ProductListController)
    ProductListController.$inject = ['$scope', 'api-service', 'notification-service'];

    function ProductListController($scope, apiService, notificationService) {
        $scope.ProductList = [];
        $scope.getListProduct = getListProduct;
        $scope.keyWord = '';

        $scope.search = search;

        $scope.delProduct = delProduct;

        $scope.Product = {};



        function delProduct(item) {
            $scope.Product = item;
            apiService.del('/api/Product/DeleteProduct', item, function (result) {
                notificationService.displaySuccess("Xóa bản ghi thành công");
                $state.go('Product-list');
            }, function () {
                    console.log('delete api failed.');
                    notificationService.displayError("Xóa bản ghi KHÔNG thành công");
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
                $scope.ProductList = result.data;
                if (result.data.length == 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào");
                } else {

                    notificationService.displaySuccess("Tìm thấy " + result.data.length + " bản ghi");
                }


            }, function () {
                console.log('Load Product api failed.');
                notificationService.displayError("Không lấy được dữ liệu từ server");
            });

        }

        $scope.getListProduct();
    }
})(angular.module('eshop-Product'));
