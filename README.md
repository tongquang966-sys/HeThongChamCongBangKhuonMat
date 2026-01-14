📸 HỆ THỐNG CHẤM CÔNG BẰNG NHẬN DIỆN KHUÔN MẶT
Face Recognition Attendance System
1. Giới thiệu
Trong bối cảnh chuyển đổi số hiện nay, việc ứng dụng trí tuệ nhân tạo (AI) vào công tác quản lý nhân sự giúp nâng cao hiệu quả vận hành và hạn chế sai sót do con người gây ra.
Đề tài Hệ thống chấm công bằng nhận diện khuôn mặt được xây dựng nhằm:
- Tự động hóa quá trình chấm công
- Loại bỏ gian lận (chấm công hộ, quẹt thẻ hộ)
- Giảm chi phí quản lý
- Nâng cao tính chính xác và minh bạch
Hệ thống sử dụng camera để thu hình khuôn mặt, kết hợp mô hình AI để nhận diện và ghi nhận lịch sử chấm công theo thời gian thực.
2. Mục tiêu của hệ thống
Xây dựng hệ thống chấm công không tiếp xúc
Nhận diện chính xác nhân viên thông qua khuôn mặt
Ghi nhận giờ vào – giờ ra tự động
Phân quyền người dùng:
Admin: quản lý nhân viên, lịch sử, thống kê
User: chấm công bằng khuôn mặt
Xuất báo cáo chấm công dưới dạng Excel
3. Phạm vi và đối tượng sử dụng
3.1 Phạm vi
Áp dụng cho doanh nghiệp, trường học, tổ chức vừa và nhỏ
Chấm công tại một hoặc nhiều điểm có camera
3.2 Đối tượng sử dụng
Nhân viên
Người quản trị hệ thống (Admin)
4. Kiến trúc tổng thể hệ thống
Hệ thống được xây dựng theo mô hình Client – Server – AI Service:
Trình duyệt (Camera)
        ↓
WebApp ASP.NET Core MVC
        ↓ (HTTP / JSON)
AI Server (FastAPI + TensorFlow)
        ↓
Kết quả nhận diện
        ↓
Cơ sở dữ liệu (SQL Server)

Giải thích:
WebApp: giao diện người dùng, xử lý nghiệp vụ
AI Server: xử lý nhận diện khuôn mặt
Database: lưu thông tin nhân viên và lịch sử chấm công
5. Công nghệ sử dụng
5.1 Web Application
ASP.NET Core MVC
Entity Framework Core
SQL Server
Razor View
Bootstrap 5
5.2 AI & Machine Learning
Python 3.10+
FastAPI
TensorFlow / Keras
OpenCV
Uvicorn
5.3 Frontend
HTML5 / CSS3
JavaScript
WebRTC (getUserMedia API)
6. Chức năng chính của hệ thống
6.1 Chức năng cho User
- Mở camera và quét khuôn mặt
- Tự động nhận diện nhân viên
- Chấm công vào / ra không cần thao tác
Hiển thị:
- Họ tên
- Mã nhân viên
- Trạng thái chấm công
6.2 Chức năng cho Admin
- Quản lý nhân viên (thêm / sửa / xóa)
- Xem lịch sử chấm công
Thống kê theo:
Ngày
Nhân viên
Ca làm
Xuất báo cáo chấm công ra file Excel
Dashboard tổng quan hệ thống
7. Cấu trúc thư mục hệ thống
7.1 WebApp
WebApp
├── Areas
│   ├── Admin
│   │   ├── Controllers
│   │   └── Views
│   └── User
│       ├── Controllers
│       └── Views
├── Models
├── ViewModels
├── Data
└── Program.cs

7.2 AI Server
AI_Server
├── main.py
├── face_model.h5
├── requirements.txt
└── venv

8. Quy trình chấm công bằng khuôn mặt
Người dùng truy cập trang chấm công
Trình duyệt kích hoạt camera
Hình ảnh khuôn mặt được gửi lên WebApp
WebApp gửi dữ liệu sang AI Server
AI Server nhận diện khuôn mặt
Trả kết quả về WebApp
Hệ thống lưu lịch sử chấm công vào database
Hiển thị kết quả cho người dùng

9. Hướng dẫn chạy hệ thống
9.1 Chạy AI Server
cd AI_Server
venv\Scripts\activate
uvicorn main:app --host 0.0.0.0 --port 8000 --reload

9.2 Chạy WebApp
cd WebApp
dotnet restore
dotnet run
Truy cập:
http://localhost:5169
10. Kết luận
Hệ thống Chấm công bằng nhận diện khuôn mặt đã đáp ứng được các yêu cầu cơ bản của một hệ thống quản lý chấm công hiện đại:
Tự động
Chính xác
Dễ sử dụng
Có khả năng mở rộng
Trong tương lai, hệ thống có thể được nâng cấp thêm:
Nhận diện nhiều khuôn mặt cùng lúc
Chấm công theo GPS
Thống kê nâng cao và AI Learning
11. Tác giả

Tên đề tài: Hệ thống chấm công bằng nhận diện khuôn mặt

Sinh viên thực hiện: Lâm Thuý Kiều Trinh
                     Tòng Văn Quảng
                     Nguyễn Thái Hải Triều
                     Triệu Khắc Tuấn Khoa
                     
Giảng viên hướng dẫn: Phạm Thị Tố Nga
