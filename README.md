# EShop
## Requirements
  *  [Visual studio 2019](https://visualstudio.microsoft.com/)
  *  [Nodejs](https://nodejs.org/en/)
  *  [NswagStudio](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio)
  *  [SQL SERVER 2019](https://www.microsoft.com/en-us/sql-server/sql-server-2019)
  *  [.NETCore3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)

## Getting Started

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

### Angular- Cập nhật sau

## Reference
   * Repository Design Pattern
   * [Dependency Injection](https://tedu.com.vn/lap-trinh-aspnet-core/co-che-dependency-injection-trong-aspnet-core-256.html)
   * [Database factory Design Pattern ] (https://kienchu.blogspot.com/2016/06/design-patterns-trong-qua-cac-du-thuc.html)
   * [BUILDING SINGLE PAGE APPLICATIONS USING WEB API AND ANGULARJS](https://chsakell.com/2015/08/23/building-single-page-applications-using-web-api-and-angularjs-free-e-book/#architecture)
