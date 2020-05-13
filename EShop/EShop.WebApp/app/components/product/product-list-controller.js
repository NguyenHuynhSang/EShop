(function (app) {
    app.controller('product-list-controller', productListController)
    //inject các service cần dùng
    productListController.$inject = ['$scope', 'api-service', 'notification-service', '$ngBootbox'];

//chú ý thứ tự
    function productListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.productList = [];
        $scope.getListProduct = getListProduct;
        $scope.keyWord = '';

        $scope.search = search;
        $scope.delProduct = delProduct;


        function search() {
            getListProduct();
        }

        function delProduct(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        ID: id,
                    }
                }
                apiService.del('/api/Product/GetAll', config, function () {
                    notificationService.displaySuccess("Xóa  thành công bản ghi");
                    getListProduct();
                }, function () {

                });
                notificationService.displaySuccess("Xóa  thành công bản ghi");
                getListProduct();
            });

        }

        function getListProduct() {
            /*Cấu trúc config cho doget để get ra parameter chú ý các tên action*/
           

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