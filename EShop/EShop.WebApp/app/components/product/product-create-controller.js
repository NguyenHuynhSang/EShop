(function (app) {
    app.controller('product-create-controller', productCreateController)
    productCreateController.$inject = ['api-service', '$scope', 'notification-service', '$state'];

    function productCreateController(apiService, $scope, notificationService, $state) {

        var temp2 = ' <div class="form-group row">'
            + '  <div class="col-lg-3">'
            + '    <label>Thuộc tính </label>'
            + '    <div class="input-group">'
            + '        <div class="input-group-prepend"><span class="input-group-text" id="basic-addon2"><i>VNĐ</i></span></div>'
            + '         <select class="form-control" ng-model="selectedItem" ng-options="x for x in listItem.id">'
            + '         </select >'
            + '   </div>'
            + '   </div>'
            + '  <div class="col-lg-6">'
            + '        <label>Giá trị thuộc tính</label>'
            + '        <div class="input-group">'
            + '            <input type="text" class="form-control" placeholder="Nhập barcode" required>'
            + '                           </div>'
            + '         </div>'
            + '       <div class="col-lg-3">'
            + '          <label>Tùy chọn</label>'
            + '          <div class="input-group align-content-center center-block">'
            + '              <button style="height:100%"><i class="la flaticon-delete"></i></button>'
            + '           </div>'
            + '      </div>'
            + '   </div>';



        $scope.listItem = [
            {
                id: 1,
            },



        ]
        var maxID = $scope.listItem.length;
        $scope.selectedItem = {};
        $scope.jsonEntity = {};
        $scope.CreateProduct = CreateProduct;
        $scope.counter = 0;
        $scope.addDirective;
        $scope.IncreateCounter = function () {
            $scope.counter++;
        }

        $scope.removeItem = function (id) {

            $('#object-' + id).empty();
        }

        $scope.addMoreAttribute = function () {
            maxID++;
            $scope.listItem.push({ id: maxID });
        
        }
        function CreateProduct() {
            apiService.post('/eshopcore_war/api/json', JSON.stringify($scope.jsonEntity), function (result) {
                notificationService.displaySuccess("Thêm mới bản ghi thành công");
                $state.go('product-list');
            }, function () {
                console.log('Load product api failed.');
                notificationService.displayError("Thêm mới bản ghi KHÔNG thành công");
            });
        }

    }







    app.directive('addDivDirective', function () {
        var temp = '<div class="form-group">'
            + '<label>{{counter}}</label>'
            + '<input type="text" class="form-control" placeholder="Nhập tên sản phẩm" required>'
            + '<span class="form-text text-muted">hello</span > '
            + '</div>';
        return {
            restrict: 'A',
            scope: true,
            template: '<button id="addDiv" class="btn btn-default" ng-click="click()">Add</button>',
            controller: function ($scope, $element, $compile) {
                $scope.clicked = 0;
                $scope.click = function () {
                    $('#addMoreVersion').append($compile(temp)($scope));
                }
            }
        }
    });

    app.directive('addAttributeDirective', function () {
        var temp = ' <div class="form-group row">'
            + '  <div class="col-lg-3">'
            + '    <label>Thuộc tính </label>'
            + '    <div class="input-group">'
            + '        <div class="input-group-prepend"><span class="input-group-text" id="basic-addon2"><i>VNĐ</i></span></div>'
            + '         <select class="form-control" ng-model="selectedItem" ng-options="x for x in list">'
            + '         </select >'
            + '   </div>'
            +'   </div>'
            + '  <div class="col-lg-6">'
            + '        <label>Giá trị thuộc tính</label>'
            + '        <div class="input-group">'
            + '            <input type="text" class="form-control" placeholder="Nhập barcode" required>'
            + '                           </div>'
            + '         </div>'
            + '       <div class="col-lg-3">'
            + '          <label>Tùy chọn</label>'
            + '          <div class="input-group align-content-center center-block">'
            + '              <button style="height:100%"><i class="la flaticon-delete"></i></button>'
            + '           </div>'
            + '      </div>'
            +'   </div>';
        return {
            restrict: 'A',
            scope: true,
            template: '<button id="addAtributeBtn" class="btn btn-default" ng-click="click()">Add</button>',
            controller: function ($scope, $element, $compile) {
                $scope.clicked = 0;
                $scope.list = ['option A', 'option B', 'option C']
                $scope.selectedItem;
                $scope.click = function () {
                    $('#attributeDiv').append($compile(temp)($scope));
          
                }
            }
        }
    });


    
})(angular.module('eshop-product'));

