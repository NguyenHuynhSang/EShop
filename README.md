# EShop
## Requirements
  *  [Visual studio 2019](https://visualstudio.microsoft.com/)
  *  [Nodejs](https://nodejs.org/en/)
  *  [NswagStudio](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio)
   *  [SQL SERVER 2019](https://www.microsoft.com/en-us/sql-server/sql-server-2019)
  ## Hướng dẫn config
  * Mở file appsettings.json trong project ESHop.WebApp
	sửa data source=LAPTOP-6KVMDIF8 bằng tên server sql của máy
  * Mở tiếp EShop.Data/DataCore/DbFactory
	sửa data source=LAPTOP-6KVMDIF8 bằng tên server sql của máy
	* Cài csdl
	Mở package Manager Console chọn default project thành EShop.Data
	gõ update-database
	* Chọn Start up project là EShop.WebApp chạy trên EShop.WebApp Không chạy
	trên IIS express nếu màn hình hiện https://localhost:5001/swagger/index.html
	là ok
	* Chạy Angular
		cd vào \EShopGUI gõ npm install sau đó npm start
		vào http://localhost:4200/ để xem kq
	
	
  
  
