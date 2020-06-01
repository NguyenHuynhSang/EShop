(function (app) {
    app.controller('catalog-edit-controller', catalogEditController)
    catalogEditController.$inject = ['api-service', '$scope', 'notification-service', '$state'];
    function catalogEditController(apiService, $scope, notificationService, $state) {

        $scope.catalogInput = {};
        $scope.listParentCatalog = [];
        $scope.getParentCatalog = getParentCatalog();

        $scope.editCatalog = editCatalog;


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

        function editCatalog() {
            apiService.post('/api/Catalog/Edit', $scope.catalogInput, function (result) {
                notificationService.displaySuccess("Thêm mới bản ghi thành công");
                $state.go('catalog-list');
            }, function () {
                console.log('Load product api failed.');
                notificationService.displayError("Thêm mới bản ghi KHÔNG thành công");
            });
        }







    }








})(angular.module('eshop-catalog'));

