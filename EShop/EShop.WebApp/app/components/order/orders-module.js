/// <reference path="../assets/admin/libs/plugins/angular/angular.js" />

(function () {
    angular.module('eshop-order', ['eshop-common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('order-list', {
            url: "/order-list",
            templateUrl: "./app/components/order/order-list-view.html",
            controller: "order-list-controller"
        });

    }

    function clearDivFiter() {
jquery('#d').val()


    }


})();

