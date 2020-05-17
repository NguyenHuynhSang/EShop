/// <reference path="../assets/admin/libs/plugins/angular/angular.js" />

(function () {
    angular.module('eshop-catalog', ['eshop-common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('catalog-list', {
            url: "/catalog-list",
            templateUrl: "./app/components/catalog/catalog-list-view.html",
            controller: "catalog-list-controller"
        }).state('catalog-create', {
            url: "/catalog-create",
            templateUrl: "./app/components/catalog/catalog-create-view.html",
            controller: "catalog-create-controller"
        });

    }

})();

