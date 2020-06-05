(function (app) {
    app.controller('contentcategory-list-controller', contentcategoryListController)
    //inject các service cần dùng
    contentcategoryListController.$inject = ['$scope', 'api-service', 'notification-service', '$ngBootbox'];

//chú ý thứ tự
    function contentcategoryListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.contentcategoryList = [];
        $scope.getListcontentcategory = getListContentCategory;
        $scope.keyWord = '';

        $scope.search = search;
        $scope.delContentCategory = delContentCategory;

        $scope.childrencategory=[];
        $scope.parentcategory=[];
        function search() {
            getListContentCategory();
        }

        function delContentCategory(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        ID: id,
                    }
                }
                apiService.del('/api/Content/DeleteContent', config, function () {
                    notificationService.displaySuccess("Xóa thành công bản ghi");
                    getListContentCategory();
                }, function () {

                });
                notificationService.displaySuccess("Xóa  thành công bản ghi");
                getListContentCategory();
            });

        }

        function getListContentCategory() {
            /*Cấu trúc config cho doget để get ra parameter chú ý các tên action*/
            var config = {
                params: {
                    keyword: $scope.keyWord,
                }
            }

            apiService.get('​/api​/Content​/GetContentForView', config, function (result) {
                $scope.contentcategoryList = result.data;
                if (result.data.length == 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào");
                } else {

                    notificationService.displaySuccess("Tìm thấy " + result.data.length + " bản ghi");
                }


            }, function () {
                console.log('Load content category api failed.');
                notificationService.displayError("Không lấy được dữ liệu từ server");
            });

        }

        $scope.getListcontentcategory();
    }
})(angular.module('eshop-contentcategory'));