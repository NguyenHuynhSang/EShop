(function (app) {
    app.controller('catalog-list-controller', catalogListController)
    //inject các service cần dùng
    catalogListController.$inject = ['$scope', 'api-service', 'notification-service', '$ngBootbox'];

    //chú ý thứ tự
    function catalogListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.catalogList = [];
        $scope.catalogTree = [];


        $scope.getListCatalog = getListCatalog;
        $scope.getCatalogTree = getCatalogTree;

        $scope.keyWord = '';

        $scope.search = search;
        $scope.delProduct = delProduct;

        $scope.treeOptions = {
            accept: function (sourceNodeScope, destNodesScope, destIndex) {


                console.log("Des");
                console.log(destNodesScope.$modelValue);
                if (sourceNodeScope.$modelValue.hasOwnProperty("parent")==true) {
                    if (sourceNodeScope.$modelValue.parent.parentID == null) {
                        return false;
                    }
                }

                if (destNodesScope.$modelValue.lengh==0) {
                    return false;
                }

                return true;

            }
        };
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

        function getListCatalog() {
            /*Cấu trúc config cho doget để get ra parameter chú ý các tên action*/


            apiService.get('/api/Catalog/GetAll', null, function (result) {
                $scope.catalogList = result.data;
                if (result.data.length == 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào");
                } else {

                    notificationService.displaySuccess("Tìm thấy " + result.data.length + " bản ghi");
                }
            }, function () {
                notificationService.displayError("Không lấy được dữ liệu từ server");
            });

        }
        function getCatalogTree() {

            apiService.get('/api/Catalog/GetTree', null, function (result) {
                $scope.catalogTree = result.data;
                if (result.data.length == 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào");
                } else {

                    notificationService.displaySuccess("Tìm thấy " + result.data.length + " bản ghi");
                }
            }, function () {
                notificationService.displayError("Không lấy được dữ liệu từ server");
            });
        }


        $scope.getListCatalog();
        $scope.getCatalogTree();
    }
})(angular.module('eshop-catalog'));