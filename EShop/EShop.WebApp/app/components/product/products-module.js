/// <reference path="../assets/admin/libs/plugins/angular/angular.js" />

(function () {
    angular.module('eshop-Product', ['eshop-common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('Product-edit', {
            url: "/Product-edit/:id",
            templateUrl: "./app/components/Product/Product-edit-view.html",
            controller: "Product-edit-controller"
        }).state('Product-list', {
            url: "/Product-list",
            templateUrl: "./app/components/Product/Product-list-view.html",
            controller: "Product-list-controller"
        }).state('Product-create', {
            url: "/Product-create",
            templateUrl: "./app/components/Product/Product-create-view.html",
            controller: "Product-create-controller"
        });



    }

})();

