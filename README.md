# EShop
## Requirements
  *  [Visual studio 2019](https://visualstudio.microsoft.com/)
  *  [SQL SERVER 2019](https://www.microsoft.com/en-us/sql-server/sql-server-2019)
  *  [.NETCore3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)

## Getting Started
### Backend
[EShop Server] (https://eshopserverlinux.azurewebsites.net/swagger/index.html)
### Frontend

[EShop Admin](https://eshopadmin.netlify.app) (WIP)

### Backend

* Tạo file mới tên `ConnectionString.txt` trong `EShop/EShop.Server`. Copy database connection string vào dòng đầu tiên

### dynamic Filter param
<table style="width: 464px;">
<tbody>
<tr style="height: 23px;">
<td style="width: 107px; height: 23px;">Params</td>
<td style="width: 121px; height: 23px;">Kiểu&nbsp;</td>
<td style="width: 234px; height: 23px;">diễn giải</td>
</tr>
<tr style="height: 23px;">
<td style="width: 107px; height: 23px;">filterProperty</td>
<td style="width: 121px; height: 23px;">string</td>
<td style="width: 234px; height: 23px;">
<p>* vd:&nbsp; để filter id =&gt; truyền filterProperty= id</p>
<p>* để filter catalog name truyền filterProperty= calalog.name</p>
<p>* với mỗi list con trong object, muốn filter phải truyền đ&uacute;ng đường dẫn như vd về catalog name ở tr&ecirc;n</p>
<pre class=" microlight"> "id": 4,
    "catalog": {
      "id": 5,
      "name": "&Aacute;o sơ mi"
    }</pre>
</td>
</tr>
<tr style="height: 23px;">
<td style="width: 107px; height: 23px;">filterOperator</td>
<td style="width: 121px; height: 23px;">enum string</td>
<td style="width: 234px; height: 23px;">dựa v&agrave;o kiểu của filterProperty đầu v&agrave;o chọn đ&uacute;ng gi&aacute; trị vd: id(int)=&gt; chọn num_equals...</td>
</tr>
<tr style="height: 23px;">
<td style="width: 107px; height: 23px;">filterType</td>
<td style="width: 121px; height: 23px;">enum string</td>
<td style="width: 234px; height: 23px;">loại filter vd: num,text,date...</td>
</tr>
<tr style="height: 23px;">
<td style="width: 107px; height: 23px;">filterValue</td>
<td style="width: 121px; height: 23px;">string</td>
<td style="width: 234px; height: 23px;">gi&aacute; trị filter, c&oacute; thể l&agrave; data, int...</td>
</tr>
<tr style="height: 23px;">
<td style="width: 107px; height: 23px;">filterValue1</td>
<td style="width: 121px; height: 23px;">string</td>
<td style="width: 234px; height: 23px;">gi&aacute; trị filter 2, d&ugrave;ng cho trường hợp range, nếu k phải range bỏ trống</td>
</tr>
</tbody>
</table>

HIỆN TẠI CHƯA HỖ TRỢ FILTER  AND hoặc OR

### dynamic sorting param

<table style="width: 439px;">
<tbody>
<tr>
<td style="width: 123px;">param</td>
<td style="width: 210px;">kiểu</td>
<td style="width: 105px;">diễn giải</td>
</tr>
<tr>
<td style="width: 123px;">sortBy</td>
<td style="width: 210px;">string</td>
<td style="width: 105px;">
<pre class=" microlight">"id": 14,
    "catalog": {
      "id": 10,
      "name": "Quần short"
    },</pre>
<p>&nbsp;vd: để&nbsp;sort&nbsp;theo id truyền&nbsp;</p>
<p>sortBy= id</p>
<p>để sort theo catalog name truyền</p>
<p>sortBy=catalog.name</p>
<p>&nbsp;Kh&ocirc;ng hỗ trợ sort c&aacute;c phần tử của 1 list con trong object ban đầu(nếu c&oacute; vd: List&lt;ProductVersion&gt;) chỉ hỗ trợ nếu l&agrave; object con như catalog ở tr&ecirc;n</p>
</td>
</tr>
<tr>
<td style="width: 123px;">sort</td>
<td style="width: 210px;">enum string&nbsp;</td>
<td style="width: 105px;">c&oacute; 2 kiểu : esc v&agrave; desc, mặc định l&agrave; desc</td>
</tr>
</tbody>
</table>

### Paging
<table style="width: 462px;">
<tbody>
<tr style="height: 23px;">
<td style="width: 87px; height: 23px;">param</td>
<td style="width: 99px; height: 23px;">kiểu</td>
<td style="width: 123px; height: 23px;">mặc định</td>
<td style="width: 152px; height: 23px;">diễn giải</td>
</tr>
<tr style="height: 23px;">
<td style="width: 87px; height: 23px;">page</td>
<td style="width: 99px; height: 23px;">int</td>
<td style="width: 123px; height: 23px;">1</td>
<td style="width: 152px; height: 23px;">trang hiện tại</td>
</tr>
<tr style="height: 23px;">
<td style="width: 87px; height: 23px;">perPage</td>
<td style="width: 99px; height: 23px;">int&nbsp;</td>
<td style="width: 123px; height: 23px;">50</td>
<td style="width: 152px; height: 23px;">số item tối đa trong 1 trang</td>
</tr>
</tbody>
</table>
<p><br />Respose format</p>
<p>&nbsp;</p>
<table style="width: 452px;">
<tbody>
<tr>
<td style="width: 155px;">biến</td>
<td style="width: 126px;">kiểu</td>
<td style="width: 170px;">diễn giải</td>
</tr>
<tr>
<td style="width: 155px;">source</td>
<td style="width: 126px;">list model&nbsp;</td>
<td style="width: 170px;">trả về danh s&aacute;ch model tr&ecirc;n trang hiện tại</td>
</tr>
<tr>
<td style="width: 155px;">
<pre class="example microlight">currentPage</pre>
</td>
<td style="width: 126px;">int</td>
<td style="width: 170px;">trang hiện tại</td>
</tr>
<tr>
<td style="width: 155px;">
<pre class="example microlight">totalPages</pre>
</td>
<td style="width: 126px;">int</td>
<td style="width: 170px;">Tổng số trang đ&atilde; ph&acirc;n ra</td>
</tr>
<tr>
<td style="width: 155px;">
<pre class="example microlight">pageSize</pre>
</td>
<td style="width: 126px;">int</td>
<td style="width: 170px;">tổng số item tr&ecirc;n 1 trang</td>
</tr>
<tr>
<td style="width: 155px;">
<pre class="example microlight">totalCount</pre>
</td>
<td style="width: 126px;">int</td>
<td style="width: 170px;">tổng số item tr&ecirc;n tất cả trang</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>



## Config

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
