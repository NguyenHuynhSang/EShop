

///$scope chứa dữ liệu của model giúp 
// truyền dữ liệu view và controller tương tự viewbag...
var myApp = angular.module('myModule', []);
myApp.controller("baseController", baseController);

myApp.service('validationService  ', validationService);
myApp.directive('appDirective', appDirective);
    
//$scope chỉ có phạm vi trong thẻ html khai báo
function baseController($scope) {

    $scope.message = "Hello world base!!!"
  //  Validation.showMessage();
}


function validationService ($window) {
    return {
        showMessage, showMessage
    }
    function showMessage() {
       // $window.alern('Hello ');

    }
}


function appDirective() {
    return {
        template:"<h2>abc</h2>"
    }


}