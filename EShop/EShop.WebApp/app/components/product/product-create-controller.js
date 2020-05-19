(function (app) {
    app.controller('product-create-controller', productCreateController)
    productCreateController.$inject = ['api-service', '$scope', 'notification-service', '$state'];

    function productCreateController(apiService, $scope, notificationService, $state) {

        ///**********************
        /// 1 product có nhiều phiên bản
        // mỗi phiên bản có nhiều attributevalue khác nhau



        //ds attributevalue của từng attribute của từng version
        $scope.attributeValueListPerVersion = [{ atributeValue: [] }]

        $scope.product = {
            name: '', description: '', version: [{ attribute: [{}]}]
        };




        ///Thêm  phiên bản cho sản phẩm
        $scope.addNewVersion = function() {
            $scope.product.version.push({ attribute: [] });
            $scope.attributeValueListPerVersion.push({ atributeValue: [] });
        }
        

        // Thêm  attributevalue dựa vào phiên bản
        $scope.addNewAttributeSet = function (verIndex) {
            $scope.product.version[verIndex].attribute.push({});
        
        };

        // loại bỏ 1 attribute value
        $scope.removeAttributeValue = function (verIndex,element) {
            $scope.product.version[verIndex].attribute.splice(element, 1);
        };


        
        //submit form để test
        $scope.submit = function () {
            var temp = $scope.product;
            var selected = $scope.attributeValueListPerVersion;
        }


        $scope.CreateProduct = CreateProduct;
        // danh sách các tên attribute 
        $scope.listAttribute = [];
      

        // lấy lên các attributevalue dựa vào attribute name đã chọn
        $scope.selectAttributeName = function (versionIndex,atributeIndex,selectedAtribute) {

            var config = {
                params: {
                    atributeId: selectedAtribute,
                    action: "getAll",
                }
            }
            apiService.get('/api/AttributeValue/GetAll', config, function (result) {
                $scope.attributeValueListPerVersion[versionIndex].atributeValue[atributeIndex] = result.data;
                if (result.data.length == 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào");
                } else {

                    notificationService.displaySuccess("Tìm thấy " + result.data.length + " bản ghi");
                }
            }, function () {
                notificationService.displayError("Không lấy được dữ liệu từ server");
            });


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




        /// Chưa dùng đến 
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







    //app.directive('addDivDirective', function () {
    //    var temp = '<div class="form-group">'
    //        + '<label>{{counter}}</label>'
    //        + '<input type="text" class="form-control" placeholder="Nhập tên sản phẩm" required>'
    //        + '<span class="form-text text-muted">hello</span > '
    //        + '</div>';
    //    return {
    //        restrict: 'A',
    //        scope: true,
    //        template: '<button id="addDiv" class="btn btn-default" ng-click="click()">Add</button>',
    //        controller: function ($scope, $element, $compile) {
    //            $scope.clicked = 0;
    //            $scope.click = function () {
    //                $('#addMoreVersion').append($compile(temp)($scope));
    //            }
    //        }
    //    }
    //});

    //app.directive('addAttributeDirective', function () {
    //    var temp = ' <div class="form-group row">'
    //        + '  <div class="col-lg-3">'
    //        + '    <label>Thuộc tính </label>'
    //        + '    <div class="input-group">'
    //        + '        <div class="input-group-prepend"><span class="input-group-text" id="basic-addon2"><i>VNĐ</i></span></div>'
    //        + '         <select class="form-control" ng-model="selectedItem" ng-options="x for x in list">'
    //        + '         </select >'
    //        + '   </div>'
    //        + '   </div>'
    //        + '  <div class="col-lg-6">'
    //        + '        <label>Giá trị thuộc tính</label>'
    //        + '        <div class="input-group">'
    //        + '            <input type="text" class="form-control" placeholder="Nhập barcode" required>'
    //        + '                           </div>'
    //        + '         </div>'
    //        + '       <div class="col-lg-3">'
    //        + '          <label>Tùy chọn</label>'
    //        + '          <div class="input-group align-content-center center-block">'
    //        + '              <button style="height:100%"><i class="la flaticon-delete"></i></button>'
    //        + '           </div>'
    //        + '      </div>'
    //        + '   </div>';
    //    return {
    //        restrict: 'A',
    //        scope: true,
    //        template: '<button id="addAtributeBtn" class="btn btn-default" ng-click="click()">Add</button>',
    //        controller: function ($scope, $element, $compile) {
    //            $scope.clicked = 0;
    //            $scope.list = ['option A', 'option B', 'option C']
    //            $scope.selectedItem;
    //            $scope.click = function () {
    //                $('#attributeDiv').append($compile(temp)($scope));

    //            }
    //        }
    //    }
    //});



})(angular.module('eshop-product'));

