/// <reference path="../assets/admin/libs/plugins/angular/angular.js" />

(function () {
    angular.module('eshop-content', ['eshop-common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('content-list', {
            url: "/content-list",
            templateUrl: "./app/components/content/content-list-view.html",
            controller: "content-list-controller"
        });

    }

    function clearDivFiter() {
jquery('#d').val()


    }


})();

