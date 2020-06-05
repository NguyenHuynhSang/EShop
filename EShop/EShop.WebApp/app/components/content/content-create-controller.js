(function (app) {
    app.controller('content-create-controller', contentCreateController)
    contentCreateController.$inject = ['api-service','$scope', 'notification-service','$state'];

    function contentCreateController(apiService,$scope,notificationService,$state) {
        $scope.contentEntity = {};
        $scope.CreateContent = CreateContent;
        $scope.contentList =[];
        $scope.getListcontentcategory = getListContentCategory;
        $scope.keyWord = '';

        function CreateContent() {
            apiService.post('/eshopcore/API-Content', JSON.stringify($scope.contentEntity)  , function (result) {
                notificationService.displaySuccess("Thêm mới bản ghi thành công");
                $state.go('content-list');
            }, function () {
                console.log('Load content  api failed.');
                notificationService.displayError("Thêm mới bản ghi KHÔNG thành công");
            });
        }

        function getListContentCategory() {
            var config = {
                params: {
                    keyword: $scope.keyWord,
                    action: "getAll",
                }
            }
            apiService.get('/eshopcore/API-ContentCategory', config, function (result) {
                $scope.contentList = result.data;   
            }, function () {
                console.log('Load content  api failed.');
                notificationService.displayError("Không lấy được dữ liệu từ server");
            });
    
        }

        $scope.getListcontent();  
    }
})(angular.module('eshop-content'));