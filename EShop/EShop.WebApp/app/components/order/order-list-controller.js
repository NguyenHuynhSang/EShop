(function (app) {
    app.controller('order-list-controller', orderListListController)
    //inject các service cần dùng
    orderListListController.$inject = ['$scope', 'api-service', 'notification-service', '$ngBootbox'];

//chú ý thứ tự
    function orderListListController($scope, apiService, notificationService, $ngBootbox) {
        

    }


})(angular.module('eshop-product'));