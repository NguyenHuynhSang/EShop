# EShop
## Requirements
  *  [Visual studio 2019](https://visualstudio.microsoft.com/)
  *  [Nodejs](https://nodejs.org/en/)
  *  [NswagStudio](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio)
  *  [SQL SERVER 2019](https://www.microsoft.com/en-us/sql-server/sql-server-2019)
  *  [.NETCore3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)

## Getting Started

### Frontend

[EShop Admin](https://eshopadmin.netlify.app) (WIP)

### Backend

* Tạo file mới tên `ConnectionString.txt` trong `EShop/EShop.Server`. Copy database connection string vào dòng đầu tiên

```
data source=VT-CNTT-NGUYENN\MSSQLSERVER01;initial catalog=Bt2;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework 
```

* Thay giá trị `data source` bằng tên sql server của máy

* Mở package Manager Console chọn default project thành EShop.Data

* Gõ update-database

* Chọn Start up project là EShop.WebApp chạy trên EShop.WebApp Không chạy
trên IIS express nếu màn hình hiện https://localhost:5001/swagger/index.html
là ok

## Database Diagram
https://drive.google.com/file/d/1xqVIjxMEDO5DzFqbf96U2llX9aTtlNV1/view?usp=sharing
## Các module chính của chương trình
  
  ### Trang quản trị
   * Trang chủ thống kê từ google analytic
   * Module quản lý sản phẩm 
        * Quản lý sản phẩm
            - Trang con: Quản lý ds sản phẩm - export excel và print
            - Trang con: Tạo mới ds sản phẩm
            - Trang con: Sửa sản phẩm
            - Trang con: Xem chí tiết sản phẩm
        * Quản lý thuộc tính sản phẩm
            - Trang con: Quản lý ds thuộc tính - export excel và print
            - Trang con: Tạo mới thuộc tính
            - Trang con: Sửa thuộc tính
        * Quản lý loại sản phẩm 
            - vd: Điện thoại-->samsung...Máy tính--->Hp...
            - Trang con: Quản lý loại sản phẩm chia tab dạng table và dạng cây phân cấp
        * Quản lý nhóm sản phẩm
           - vd: Nhóm sản phẩm hot, nhóm sản phẩm khuyết mãi...
   * Module quản lý hóa đơn	
        * Trang quản lý đơn hàng
           - Trang con: Quản lý ds đơn hàng - export excel và print
                - Chia ra các tab như tất cả hóa đơn, đơn hàng mới, đơn hàng đã xác nhận, đơn hàng đang giao,
                           đơn hàng đã thu, đơn  bị hủy
                - Xác nhận đơn
           - Trang con: Tạo mới đơn hàng (đơn bán trực tiếp tại cửa hàng)			
           - Trang con: Sửa đơn hàng
   * Module quản lý tin tức
        * Quản lý tin tức
          - Trang con: Quản lý tin tức
          - Trang con: Thêm
          - Trang con: Sửa
        * Quản lý loại tin tức
          - Trang con: Quản lý
          - Trang con: Thêm
          - Trang con: Sửa
   * Module thống kê báo cáo
       * Trang thống kê doanh thu - export excel và print
         - Tùy chọn thống kê theo ngày, tháng, năm
         - Có biểu đồ thống kê
   * Module quản lý trang
       * Quản lý slide --- có cho xóa
          - Trang con: Quản lý ds Slide
          - Trang con: Thêm
          - Trang con: Sửa
       * Quản lý footer
          - Mô tả: Chỉ có duy nhất 1 footer được active và bind ra trang chủ
          - Trang con: Quản lý ds footer
          - Trang con: Thêm
          - Trang con: Sửa
       * Quản lý menu
          - Trang con: Quản lý ds menu
          - Trang con: Thêm
          - Trang con: Sửa

## Reference
   * Repository Design Pattern
   * [Dependency Injection](https://tedu.com.vn/lap-trinh-aspnet-core/co-che-dependency-injection-trong-aspnet-core-256.html)
   * [Database factory Design Pattern ] (https://kienchu.blogspot.com/2016/06/design-patterns-trong-qua-cac-du-thuc.html)
   * [BUILDING SINGLE PAGE APPLICATIONS USING WEB API AND ANGULARJS](https://chsakell.com/2015/08/23/building-single-page-applications-using-web-api-and-angularjs-free-e-book/#architecture)
