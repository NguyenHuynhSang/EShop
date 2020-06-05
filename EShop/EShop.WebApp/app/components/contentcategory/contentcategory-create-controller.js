(function (app) {
    app.controller('contentcategory-create-controller', contentcategoryCreateController)
    contentcategoryCreateController.$inject = ['api-service','$scope', 'notification-service','$state'];

    function contentcategoryCreateController(apiService,$scope,notificationService,$state) {
        $scope.contentcategoryEntity = {};
        $scope.CreateContentCategory = CreateContentCategory;
        $scope.contentcategoryList =[];
        $scope.getListcontentcategory = getListContentCategory;
        $scope.keyWord = '';

        function CreateContentCategory() {
            apiService.post('/api/Content/CreateContent', JSON.stringify($scope.contentcategoryEntity)  , function (result) {
                notificationService.displaySuccess("Thêm mới bản ghi thành công");
                $state.go('contentcategory-list');
            }, function () {
                console.log('Load content category api failed.');
                notificationService.displayError("Thêm mới bản ghi KHÔNG thành công");
            });
        }

        function getListContentCategory() {
            var config = {
                params: {
                    keyword: $scope.keyWord,
                }
            }
            apiService.get('/api​/Content​/GetContentForView', config, function (result) {
                $scope.contentcategoryList = result.data;   
            }, function () {
                console.log('Load content category api failed.');
                notificationService.displayError("Không lấy được dữ liệu từ server");
            });
    
        }

        $scope.getListcontentcategory();  
    }
})(angular.module('eshop-contentcategory'));