﻿/// <reference path="../assets/admin/libs/plugins/angular/angular.js" />

(function () {
    angular.module('eshop', ['eshop-product', 'eshop-common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: "/admin",
            templateUrl: "/app/components/home/home-view.html",
            controller: "home-controller"
        });

        //bắt lỗi 404 điều hướng tại đây
        $urlRouterProvider.otherwise('/admin');
    }

})();