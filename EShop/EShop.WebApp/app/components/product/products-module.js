/// <reference path="../assets/admin/libs/plugins/angular/angular.js" />

(function () {
    angular.module('eshop-product', ['eshop-common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('product-edit', {
            url: "/admin/product-edit",
            templateUrl: "/app/components/product/product-edit-view.html",
            controller: "product-edit-controller"
        });
    }

})();

