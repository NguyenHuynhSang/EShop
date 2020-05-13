(function (app) {
    app.controller('product-edit-controller', productEditController)
    productEditController.$inject = ['api-service','$scope', 'notification-service','$state','$stateParams'];

    function productEditController(apiService,$scope,notificationService,$state,$stateParams) {
        $scope.jsonEntity = {};
        $scope.EditProduct = EditProduct;

        function LoadProductByID() {

            var config = {
                params: {
                    keyword: "",
                    ID:$stateParams.id,
                    action: "getByID",
                }
            }

            apiService.get('/eshopcore_war/api/json', config, function (result) {
                $scope.jsonEntity = result.data;
                if (result.data.length == 0) {
                    //notificationService.displayWarning("Không tìm thấy bản ghi nào");
                } else {

                //    notificationService.displaySuccess("Tìm thấy " + result.data.length + "bản ghi");
                }


            }, function () {
                console.log('Load product api failed.');
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