(function (app) {
    app.controller('catalog-create-controller', catalogCreateController)
    catalogCreateController.$inject = ['api-service', '$scope', 'notification-service', '$state'];
    function catalogCreateController(apiService, $scope, notificationService, $state) {

        $scope.catalogInput = {};
        $scope.listParentCatalog = [];
        $scope.getParentCatalog = getParentCatalog();

        $scope.createCatalog = createCatalog;


        function getParentCatalog() {
            apiService.get('/api/Catalog/GetParent', null, function (result) {
                $scope.listParentCatalog = result.data;
                if (result.data.length == 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào");
                } else {

                    notificationService.displaySuccess("Tìm thấy " + result.data.length + " bản ghi");
                }
            }, function () {
                notificationService.displayError("Không lấy được dữ liệu từ server");
            });


        }

        function createCatalog() {
            apiService.post('/api/Catalog/Create', $scope.catalogInput, function (result) {
                notificationService.displaySuccess("Thêm mới bản ghi thành công");
                $state.go('catalog-list');
            }, function () {
                console.log('Load product api failed.');
                notificationService.displayError("Thêm mới bản ghi KHÔNG thành công");
            });
        }







    }








})(angular.module('eshop-catalog'));

