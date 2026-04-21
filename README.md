# ĐỀ TÀI: XÂY DỰNG WEBSITE BÁN LAPTOP

## Thông tin sinh viên

- **Họ và tên:** Lê Đức Mạnh
- **Mã sinh viên:** 170124483
- **Lớp:** DK24TTC5
- **Ngành:** Công nghệ thông tin
- **Email:** ducmanh1681997@gmail.com
- **Số điện thoại:** 0941627687
- **Giảng viên hướng dẫn:** TS. Đoàn Phước Miền

---

## Giới thiệu đề tài

LaptopStore là website bán laptop được xây dựng bằng **ASP.NET Core MVC** kết hợp **SQL Server** để quản lý dữ liệu.  
Hệ thống cho phép khách hàng xem sản phẩm, tìm kiếm, lọc theo danh mục, thêm vào giỏ hàng, thêm vào danh sách yêu thích, đặt hàng và theo dõi đơn hàng.  
Ngoài ra, hệ thống còn có trang quản trị dành cho **Admin** để quản lý sản phẩm, đơn hàng và khách hàng.

---

## Công nghệ sử dụng

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap
- HTML, CSS, JavaScript
- Session

---

## Chức năng chính

### 1. Khách hàng

- Đăng ký tài khoản
- Đăng nhập, đăng xuất
- Xem danh sách sản phẩm
- Xem chi tiết sản phẩm
- Tìm kiếm sản phẩm theo tên
- Lọc sản phẩm theo danh mục
- Phân trang sản phẩm
- Thêm sản phẩm vào giỏ hàng
- Quản lý giỏ hàng theo tài khoản
- Thêm / xóa sản phẩm yêu thích
- Đặt hàng
- Xem danh sách đơn hàng
- Xem chi tiết đơn hàng

### 2. Quản trị viên

- Đăng nhập tài khoản Admin
- Quản lý sản phẩm
  - Thêm sản phẩm
  - Sửa sản phẩm
  - Xóa sản phẩm
- Quản lý đơn hàng
  - Xem danh sách đơn hàng
  - Xem chi tiết đơn hàng
  - Cập nhật trạng thái đơn hàng
- Quản lý khách hàng
  - Xem danh sách khách hàng
  - Xóa khách hàng thường
  - Không cho phép xóa tài khoản Admin

---

## Cấu trúc dữ liệu chính

### Product

Lưu thông tin sản phẩm laptop:

- Tên sản phẩm
- Giá
- Hình ảnh
- Mô tả
- CPU
- RAM
- Ổ cứng
- Số lượng tồn
- Hãng
- Danh mục

### Customer

Lưu thông tin tài khoản khách hàng:

- Họ tên
- Email
- Số điện thoại
- Mật khẩu
- Địa chỉ
- Quyền tài khoản (`Customer`, `Admin`)

### CartItem

Lưu thông tin giỏ hàng theo từng khách hàng:

- CustomerId
- ProductId
- Quantity

### WishlistItem

Lưu danh sách sản phẩm yêu thích theo từng khách hàng:

- CustomerId
- ProductId

### Order

Lưu thông tin đơn hàng:

- Mã đơn hàng
- Ngày đặt
- Tổng tiền
- Trạng thái
- Người nhận
- Số điện thoại nhận hàng
- Địa chỉ giao hàng
- CustomerId

### OrderDetail

Lưu chi tiết sản phẩm trong đơn hàng:

- OrderId
- ProductId
- Quantity
- UnitPrice

---

## Cấu trúc thư mục repository

```text
ASP.NET-DK24TTC5-leducmanh-LaptopStore/
├── README.md
├── .gitignore
├── setup/
│   ├── huong-dan-cai-dat.md
│   └── database/
├── src/
│   ├── LaptopStore.sln
│   └── LaptopStore/
├── progress-report/
├── thesis/
│   ├── doc/
│   ├── pdf/
│   ├── html/
│   ├── abs/
│   └── refs/
```
