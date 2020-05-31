(function (app) {
    app.controller('product-create-controller', productCreateController)
    productCreateController.$inject = ['api-service', '$scope', 'notification-service', '$state'];
    function productCreateController(apiService, $scope, notificationService, $state) {

        ///**********************
        /// 1 product có nhiều phiên bản
        // mỗi phiên bản có nhiều attributevalue khác nhau



        //ds attributevalue của từng attribute của từng version
        $scope.attributeValueListPerVersion = [{ atributeValue: [] }]

        $scope.productInput = {
            name: '', description: '', version: [{ attribute: []}]
        };


        //list catalog
        $scope.childCagalogList = [];
        $scope.getListChildCatalog = getListChildCatalog();





        ///Thêm  phiên bản cho sản phẩm
        $scope.addNewVersion = function() {
            $scope.productInput.version.push({ attribute: [] });
            $scope.attributeValueListPerVersion.push({ atributeValue: [] });
        }
        

        // Thêm  attributevalue dựa vào phiên bản
        $scope.addNewAttributeSet = function (verIndex) {
            $scope.productInput.version[verIndex].attribute.push({});
        
        
        };

        // loại bỏ 1 attribute value
        $scope.removeAttributeValue = function (verIndex,element) {
            $scope.productInput.version[verIndex].attribute.splice(element, 1);
            $scope.attributeValueListPerVersion[verIndex].atributeValue.splice(element,1);
        };


        
        //submit form để test
        $scope.submit = function () {
            var temp = $scope.productInput;
            var selected = $scope.attributeValueListPerVersion;
            CreateProduct();
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



        function getListChildCatalog() {
            apiService.get('/api/Catalog/GetChild', null, function (result) {
                $scope.childCagalogList = result.data;
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
            apiService.post('/api/Product/CreateProductByProductInput', $scope.productInput, function (result) {
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








})(angular.module('eshop-product'));

