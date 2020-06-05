(function (app) {
    app.controller('content-list-controller', contentListController)
    //inject các service cần dùng
    contentListController.$inject = ['$scope', 'api-service', 'notification-service', '$ngBootbox'];

//chú ý thứ tự
    function contentListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.contentList = [];
        $scope.getListcontent = getListContent;
        $scope.keyWord = '';

        $scope.search = search;
        $scope.delContent = delContent;

        function search() {
            getListContent();
        }

        function delContent(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        ID: id,
                    }
                }
                apiService.del('/eshopcore/API-Content', config, function () {
                    notificationService.displaySuccess("Xóa thành công bản ghi");
                    getListContent();
                }, function () {

                });
                notificationService.displaySuccess("Xóa  thành công bản ghi");
                getListContent();
            });

        }

        function getListContent() {
            /*Cấu trúc config cho doget để get ra parameter chú ý các tên action*/
            var config = {
                params: {
                    keyword: $scope.keyWord,
                    action: "getAll",
                }
            }

            apiService.get('/eshopcore/API-Content', config, function (result) {
                $scope.contentList = result.data;
                if (result.data.length == 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào");
                } else {

                    notificationService.displaySuccess("Tìm thấy " + result.data.length + " bản ghi");
                }


            }, function () {
                console.log('Load content api failed.');
                notificationService.displayError("Không lấy được dữ liệu từ server");
            });

        }

        $scope.getListcontent();
    }
})(angular.module('eshop-content'));