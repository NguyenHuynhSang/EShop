(function (app) {
    app.controller('product-create-controller', productCreateController)
    productCreateController.$inject = ['api-service', '$scope', 'notification-service', '$state'];

    function productCreateController(apiService, $scope, notificationService, $state) {


        $scope.listItem = [
            {
                id: 1,
            },
        ]




        $scope.product = {};
        $scope.productVertion = {};


        $scope.selectedItem = {};
        $scope.jsonEntity = {};
        $scope.CreateProduct = CreateProduct;
        $scope.counter = 0;
        $scope.addDirective;
        $scope.listAttribute = [];
        $scope.listAttributeValue = [];
        $scope.selectedAttributeValue = {};

        $scope.IncreateCounter = function () {
            $scope.counter++;
        }

        $scope.removeItem = function (selectedItem, id) {
            $('#object-' + id).empty();
        }

        $scope.addMoreAttribute = function () {
            maxID++;
            $scope.listItem.push({ id: maxID });
            $scope.listItem.forEach(element => console.log($scope.selectedItem[element.id]));
        }


        $scope.removeAtribute = function (index, id) {

            var config = {
                params: {
                    atributeId: index,
                    action: "getAll",
                }
            }
            apiService.get('/api/AttributeValue/GetAll', config, function (result) {
                $scope.listAttributeValue[id] = result.data;
                if (result.data.length == 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào");
                } else {

                    notificationService.displaySuccess("Tìm thấy " + result.data.length + " bản ghi");
                }
            }, function () {
                notificationService.displayError("Không lấy được dữ liệu từ server");
            });

            //for (var i = 0; i < $scope.listAttribute.length; i++) {
            //    if ($scope.listAttribute[i].id == index) {
            //        $scope.listAttribute.splice(i, 1);
            //    }
            //}

        }


        function GetListAttribute() {

            apiService.get('/api/Attribute/GetAll', null, function (result) {
                $scope.listAttribute = result.data;
                if (result.data.length == 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào");
                } else {

                    notificationService.displaySuccess("Tìm thấy " + result.data.length + " bản ghi");
                }
            }, function () {
                notificationService.displayError("Không lấy được dữ liệu từ server");
            });

        }

        function GetListAttributeValue() {
            apiService.get('/api/AttributeValue/GetAll', null, function (result) {
                $scope.listAttributeValue = result.data;
                if (result.data.length == 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào");
                } else {

                    notificationService.displaySuccess("Tìm thấy " + result.data.length + " bản ghi");
                }
            }, function () {
                notificationService.displayError("Không lấy được dữ liệu từ server");
            });




        }

        var maxID = $scope.listItem.length;
        function CreateProduct() {
            apiService.post('/eshopcore_war/api/json', JSON.stringify($scope.jsonEntity), function (result) {
                notificationService.displaySuccess("Thêm mới bản ghi thành công");
                $state.go('product-list');
            }, function () {
                console.log('Load product api failed.');
                notificationService.displayError("Thêm mới bản ghi KHÔNG thành công");
            });
        }


        GetListAttribute();
      //  GetListAttributeValue();
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

