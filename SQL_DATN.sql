-- Tạo bảng tài khoản
CREATE TABLE TaiKhoan (
   MaTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(255) NOT NULL,
    TenDangNhap VARCHAR(100) UNIQUE NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    SoDienThoai VARCHAR(20) UNIQUE NOT NULL,
    LoaiTaiKhoan INT NOT NULL,
    TrangThai BIT DEFAULT 1,
    NgayCapNhat DATE DEFAULT GETDATE()
);

-- tạo trigger check password
--CREATE TRIGGER Check_Password_Rules
--ON TaiKhoan
--AFTER INSERT, UPDATE
--AS
--BEGIN
--    IF EXISTS (
--        SELECT 1
--        FROM inserted
--        WHERE MatKhau NOT LIKE '%[A-Z]%'  -- Ít nhất một chữ hoa
--           OR MatKhau NOT LIKE '%[a-z]%'  -- Ít nhất một chữ thường
--           OR MatKhau NOT LIKE '%[0-9]%'  -- Ít nhất một số
--           OR LEN(MatKhau) < 8            -- Ít nhất 8 ký tự
--    )
--    BEGIN
--        RAISERROR ('Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường và số.', 16, 1);
--        ROLLBACK TRANSACTION;
--    END
--END;


-- tạo bảng địa chỉ
CREATE TABLE DiaChi (
    MaDiaChi INT IDENTITY(1,1) PRIMARY KEY,
    MaTaiKhoan INT NOT NULL,
    Nuoc NVARCHAR(100) NOT NULL,
    ThanhPho NVARCHAR(100) NOT NULL,
    Huyen NVARCHAR(100) NOT NULL,
    Xa NVARCHAR(100) NOT NULL,
    ChiTiet NVARCHAR(255) NOT NULL,
    HoTenNguoiNhan NVARCHAR(255) NOT NULL,
    SoDienThoaiNguoiNhan VARCHAR(15) NOT NULL CHECK (SoDienThoaiNguoiNhan LIKE '[0-9]%'),
    NgayCapNhat DATE DEFAULT GETDATE(),
    
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan) ON DELETE CASCADE
);

-- tạo bảng mã giảm giá
CREATE TABLE MaGiamGia (
    MaGiamGia INT IDENTITY(1,1) PRIMARY KEY,
    MaHienThi VARCHAR(50) NOT NULL UNIQUE, -- Mã giảm giá duy nhất
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    LoaiGiamGia NVARCHAR(50) NOT NULL CHECK (LoaiGiamGia IN (N'Giảm theo %', N'Giảm tiền mặt')),
    GiaTri FLOAT NOT NULL CHECK (GiaTri > 0),
    TrangThai BIT NOT NULL DEFAULT 1,
    NgayCapNhat DATE DEFAULT GETDATE(),
    
    -- Ràng buộc CHECK ở cấp bảng để đảm bảo NgayKetThuc >= NgayBatDau
    CONSTRAINT CHK_NgayKetThuc CHECK (NgayKetThuc >= NgayBatDau)
);
-- tạo bảng kích thước
CREATE TABLE KichThuoc (
    MaKichThuoc INT IDENTITY(1,1) PRIMARY KEY,
    TenKichThuoc NVARCHAR(50) NOT NULL UNIQUE, -- Kích thước duy nhất
    MoTa NVARCHAR(255) NULL,
    TrangThai BIT NOT NULL DEFAULT 1, -- 1: Hoạt động, 0: Không hoạt động
    NgayCapNhat DATE DEFAULT GETDATE()
);

-- tạo bảng thương hiệu 
CREATE TABLE ThuongHieu (
    MaThuongHieu INT IDENTITY(1,1) PRIMARY KEY,
    TenThuongHieu NVARCHAR(100) NOT NULL UNIQUE, -- Tên thương hiệu không trùng
    QuocGia NVARCHAR(50) NOT NULL, -- Quốc gia thương hiệu
    MoTa NVARCHAR(255) NULL, -- Mô tả thương hiệu (nếu có)
    NgayCapNhat DATE DEFAULT GETDATE(), -- Ngày cập nhật mặc định là ngày hiện tại
    TrangThai BIT NOT NULL DEFAULT 1 -- 1: Hoạt động, 0: Ngừng hoạt động
);

-- tạo bảng nhóm hương
CREATE TABLE NhomHuong (
    MaNhomHuong INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính tự động tăng
    TenNhomHuong NVARCHAR(100) NOT NULL UNIQUE, -- Tên nhóm hương, không trùng
    MoTa NVARCHAR(255) NULL, -- Mô tả nhóm hương (có thể bỏ trống)
    NgayCapNhat DATE DEFAULT GETDATE(), -- Ngày cập nhật, mặc định là ngày hiện tại
    TrangThai BIT NOT NULL DEFAULT 1 -- 1: Hoạt động, 0: Ngừng hoạt động
);

-- tạo bảng nồng độ
CREATE TABLE NongDo (
    MaNongDo INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính tự động tăng
    TenNongDo NVARCHAR(100) NOT NULL UNIQUE, -- Tên nồng độ, không trùng lặp
    MoTa NVARCHAR(255) NULL, -- Mô tả nồng độ (có thể bỏ trống)
    NgayCapNhat DATE DEFAULT GETDATE(), -- Ngày cập nhật, mặc định là ngày hiện tại
    TrangThai BIT NOT NULL DEFAULT 1 -- 1: Hoạt động, 0: Ngừng hoạt động
);

-- tạo bảng phương thức thanh toán
CREATE TABLE PhuongThucThanhToan (
    MaPhuongThuc INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính tự động tăng
    TenPhuongThuc NVARCHAR(100) NOT NULL UNIQUE, -- Tên phương thức thanh toán (Ví dụ: VNPAY, Momo, Tiền mặt)
    MoTa NVARCHAR(255) NULL, -- Mô tả chi tiết phương thức thanh toán
    NgayCapNhat DATE DEFAULT GETDATE(), -- Ngày cập nhật, mặc định là ngày hiện tại
    TrangThai BIT NOT NULL DEFAULT 1 -- 1: Hoạt động, 0: Không hoạt động
);

-- tạo bảng ảnh
CREATE TABLE Anh (
    MaAnh INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính tự động tăng
    TenAnh NVARCHAR(255) NOT NULL, -- Tên ảnh (Ví dụ: sanpham_1.jpg)
    MoTa NVARCHAR(500) NULL, -- Mô tả ảnh (Có thể bỏ trống)
    NgayCapNhat DATE DEFAULT GETDATE(), -- Ngày cập nhật, mặc định là ngày hiện tại
    URL VARCHAR(500) NOT NULL UNIQUE -- Đường dẫn ảnh (Lưu link ảnh)
);


-- tạo bảng sản phẩm
CREATE TABLE SanPham (
    MaSanPham INT IDENTITY(1,1) PRIMARY KEY, -- Mã sản phẩm tự động tăng
    MaThuongHieu INT NOT NULL, -- Mã thương hiệu (FK)
    MaNhomHuong INT NOT NULL, -- Mã nhóm hương (FK)
    MaNongDo INT NOT NULL, -- Mã nồng độ (FK)
    TenSanPham NVARCHAR(255) NOT NULL, -- Tên sản phẩm
    GioiTinh NVARCHAR(50) NOT NULL, -- Giới tính sử dụng (Nam/Nữ/Unisex)
    TrangThai BIT DEFAULT 1, -- Trạng thái hoạt động (1: còn bán, 0: ngừng bán)
    MoTa NVARCHAR(1000) NULL, -- Mô tả sản phẩm
    NgayCapNhat DATE DEFAULT GETDATE(), -- Ngày cập nhật, mặc định lấy ngày hiện tại

    -- Khóa ngoại
    CONSTRAINT FK_SanPham_ThuongHieu FOREIGN KEY (MaThuongHieu) REFERENCES ThuongHieu(MaThuongHieu),
    CONSTRAINT FK_SanPham_NhomHuong FOREIGN KEY (MaNhomHuong) REFERENCES NhomHuong(MaNhomHuong),
    CONSTRAINT FK_SanPham_NongDo FOREIGN KEY (MaNongDo) REFERENCES NongDo(MaNongDo)
);

-- tạo bảng giỏ hàng
CREATE TABLE GioHang (
    MaGioHang INT IDENTITY(1,1) PRIMARY KEY,
    MaTaiKhoan INT NOT NULL,
    NgayCapNhat DATE DEFAULT GETDATE(),

    -- Khóa ngoại
    CONSTRAINT FK_GioHang_TaiKhoan FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan)
);


-- tạo bảng chi tiết sp
CREATE TABLE ChiTietSanPham (
    MaChiTietSP INT IDENTITY(1,1) PRIMARY KEY,
    MaSanPham INT NOT NULL,
    MaKichThuoc INT NOT NULL,
    MaAnh INT NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong >= 0),
    Gia DECIMAL(18,2) NOT NULL CHECK (Gia >= 0),
    NgayCapNhat DATE DEFAULT GETDATE(),
    TrangThai BIT NOT NULL DEFAULT 1,

    -- Khóa ngoại
    CONSTRAINT FK_ChiTietSP_SanPham FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham),
    CONSTRAINT FK_ChiTietSP_KichThuoc FOREIGN KEY (MaKichThuoc) REFERENCES KichThuoc(MaKichThuoc),
    CONSTRAINT FK_ChiTietSP_Anh FOREIGN KEY (MaAnh) REFERENCES Anh(MaAnh)
);

-- tạo bảng đánh giá
CREATE TABLE DanhGia (
    MaDanhGia INT IDENTITY(1,1) PRIMARY KEY,
    MaTaiKhoan INT NOT NULL,
    MaCTSanPham INT NOT NULL,
    SoDiem INT NOT NULL CHECK (SoDiem BETWEEN 1 AND 5),
    MoTa NVARCHAR(MAX),
    NgayCapNhat DATE DEFAULT GETDATE(),

    -- Khóa ngoại
    CONSTRAINT FK_DanhGia_TaiKhoan FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
    CONSTRAINT FK_DanhGia_ChiTietSP FOREIGN KEY (MaCTSanPham) REFERENCES ChiTietSanPham(MaChiTietSP)
);
-- tạo bảng yêu thích
CREATE TABLE YeuThich (
    MaYeuThich INT IDENTITY(1,1) PRIMARY KEY,
    MaTaiKhoan INT NOT NULL,
    MaSanPham INT NOT NULL,
    NgayCapNhat DATE DEFAULT GETDATE(),

    -- Khóa ngoại
    CONSTRAINT FK_YeuThich_TaiKhoan FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
    CONSTRAINT FK_YeuThich_SanPham FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham)
);



--tạo bảng ảnh sản phẩm
CREATE TABLE AnhSanPham (
    MaAnhSP INT IDENTITY(1,1) PRIMARY KEY,
    MaSanPham INT NOT NULL,
    MaAnh INT NOT NULL,
    NgayCapNhat DATE DEFAULT GETDATE(),

    -- Khóa ngoại
    CONSTRAINT FK_AnhSanPham_SanPham FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham),
    CONSTRAINT FK_AnhSanPham_Anh FOREIGN KEY (MaAnh) REFERENCES Anh(MaAnh)
);

-- tạo bảng đơn hàng
CREATE TABLE DonHang (
    MaDonHang INT IDENTITY(1,1) PRIMARY KEY,
    MaTaiKhoan INT NOT NULL,
    NgayTao DATE DEFAULT GETDATE(),
    NgayCapNhat DATE DEFAULT GETDATE(),
    TrangThaiVanChuyen NVARCHAR(50) NOT NULL CHECK (TrangThaiVanChuyen IN ('Chờ xác nhận', 'Đang vận chuyển', 'Giao hàng thành công', 'Đã hủy')),
    MaGiamGia INT NULL,
    TongTien DECIMAL(18,2) NOT NULL,
    MaPhuongThucThanhToan INT NOT NULL,
    TrangThaiThanhToan BIT NOT NULL,
    MaDiaChi INT NOT NULL,

    -- Khóa ngoại
    CONSTRAINT FK_DonHang_TaiKhoan FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
    CONSTRAINT FK_DonHang_GiamGia FOREIGN KEY (MaGiamGia) REFERENCES MaGiamGia(MaGiamGia),
    CONSTRAINT FK_DonHang_PhuongThucThanhToan FOREIGN KEY (MaPhuongThucThanhToan) REFERENCES PhuongThucThanhToan(MaPhuongThuc),
    CONSTRAINT FK_DonHang_DiaChi FOREIGN KEY (MaDiaChi) REFERENCES DiaChi(MaDiaChi)
);

-- tạo bảng chi tiết đơn hàng
CREATE TABLE ChiTietDonHang (
    MaChiTietDonHang INT IDENTITY(1,1) PRIMARY KEY,  -- Khóa chính tự động tăng
    MaDonHang INT NOT NULL,                           -- FK đến bảng DonHang
    MaChiTietSP INT NOT NULL,                         -- FK đến bảng ChiTietSP
    SoLuong INT NOT NULL CHECK (SoLuong > 0),        -- Số lượng phải lớn hơn 0
    TongTien FLOAT NOT NULL,                         -- Tổng tiền = Giá * Số lượng
    
    -- Khóa ngoại
    CONSTRAINT FK_ChiTietDonHang_DonHang FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietDonHang_ChiTietSP FOREIGN KEY (MaChiTietSP) REFERENCES ChiTietSanPham(MaChiTietSP) ON DELETE CASCADE
);
-- tạo bảng chi tiết giỏ hàng
CREATE TABLE ChiTietGioHang (
    MaChiTietGio INT IDENTITY(1,1) PRIMARY KEY,  -- Khóa chính tự động tăng
    MaGioHang INT NOT NULL,                      -- FK đến bảng GioHang
    MaChiTietSanPham INT NOT NULL,               -- FK đến bảng ChiTietSanPham
    SoLuong INT NOT NULL CHECK (SoLuong > 0),   -- Số lượng phải lớn hơn 0
    TongTien FLOAT NOT NULL,                     -- Tổng tiền = Giá * Số lượng
    NgayCapNhat DATE DEFAULT GETDATE(),          -- Ngày cập nhật mặc định là hiện tại

    -- Khóa ngoại
    CONSTRAINT FK_ChiTietGioHang_GioHang FOREIGN KEY (MaGioHang) REFERENCES GioHang(MaGioHang) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietGioHang_ChiTietSP FOREIGN KEY (MaChiTietSanPham) REFERENCES ChiTietSanPham(MaChiTietSP) ON DELETE CASCADE
);

-- tạo bảng thanh toán (lưu thông tin thanh toán)
CREATE TABLE ThanhToan (
    MaThanhToan INT IDENTITY(1,1) PRIMARY KEY,  -- Mã thanh toán tự động tăng
    MaTaiKhoan INT NOT NULL,                    -- FK đến bảng Tài Khoản
    MaDonHang INT NOT NULL,                     -- FK đến bảng Đơn Hàng
    MaLoi INT NULL,                             -- FK đến bảng Lỗi (nếu có)
    MaGiaoDichNoiBo NVARCHAR(50) UNIQUE NOT NULL, -- Mã giao dịch nội bộ (duy nhất)
    MaGiaoDich NVARCHAR(50) NOT NULL,           -- Mã giao dịch từ ngân hàng
    MoTaGiaoDich NVARCHAR(255) NULL,            -- Mô tả giao dịch
    SoTienGiaoDich FLOAT NOT NULL CHECK (SoTienGiaoDich > 0), -- Số tiền phải lớn hơn 0
    ThoiGian DATETIME DEFAULT GETDATE(),        -- Thời gian thực hiện thanh toán
    SoTaiKhoan VARCHAR(50) NOT NULL,            -- Số tài khoản nhận thanh toán
    TenNganHang NVARCHAR(100) NOT NULL,         -- Tên ngân hàng nhận
    MaNganHang VARCHAR(10) NOT NULL,            -- Mã ngân hàng nhận
    TenNguoiGui NVARCHAR(100) NOT NULL,         -- Tên người gửi tiền
    STKGui VARCHAR(50) NOT NULL,                -- Số tài khoản người gửi
    MaNganHangGui VARCHAR(10) NOT NULL,         -- Mã ngân hàng người gửi
    TenNganHangGui NVARCHAR(100) NOT NULL,      -- Tên ngân hàng người gửi

    -- Khóa ngoại
    CONSTRAINT FK_ThanhToan_TaiKhoan FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan) ON DELETE CASCADE,
    CONSTRAINT FK_ThanhToan_DonHang FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang) ON DELETE CASCADE
);
