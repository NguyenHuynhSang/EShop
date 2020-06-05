/// <reference path="../assets/admin/libs/plugins/angular/angular.js" />

(function () {
    angular.module('eshop-contentcategory', ['eshop-common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('contentcategory-list', {
            url: "/contentcategory-list",
            templateUrl: "./app/components/contentcategory/contentcategory-list-view.html",
            controller: "contentcategory-list-controller"
        }).state('contentcategory-create', {
            url: "/contentcategory-create",
            templateUrl: "./app/components/contentcategory/contentcategory-create-view.html",
            controller: "contentcategory-create-controller"
        });

    }

    function clearDivFiter() {
jquery('#d').val()


    }


})();

