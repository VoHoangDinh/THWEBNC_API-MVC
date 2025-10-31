using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using QLBH_ADMIN.Models; // <-- ĐÃ THAY ĐỔI

namespace QLBH_ADMIN.Controllers // <-- ĐÃ THAY ĐỔI
{
    public class QuanLySanPhamController : Controller
    {
        private readonly string _apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        private HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new System.Uri(_apiBaseUrl);
            return client;
        }

        // GET: Hiển thị danh sách sản phẩm
        public async Task<ActionResult> Index()
        {
            IEnumerable<SanPhamViewModel> sanPhamList;
            using (var client = GetHttpClient())
            {
                HttpResponseMessage result = await client.GetAsync("api/SanPham");
                if (result.IsSuccessStatusCode)
                {
                    sanPhamList = await result.Content.ReadAsAsync<IEnumerable<SanPhamViewModel>>();
                }
                else
                {
                    sanPhamList = new List<SanPhamViewModel>();
                    ModelState.AddModelError(string.Empty, "Lỗi khi lấy danh sách sản phẩm từ API.");
                }
            }
            return View(sanPhamList);
        }

        // GET: Hiển thị chi tiết sản phẩm
        public async Task<ActionResult> Details(int id)
        {
            SanPhamViewModel sanPham = null; // Khởi tạo rõ ràng là null
            using (var client = GetHttpClient())
            {
                HttpResponseMessage result = await client.GetAsync($"api/SanPham/{id}");
                if (result.IsSuccessStatusCode)
                {
                    sanPham = await result.Content.ReadAsAsync<SanPhamViewModel>();
                }
                // Không cần 'else' ở đây, chúng ta sẽ kiểm tra 'sanPham' ở bên ngoài
            }

            // --- PHẦN SỬA LỖI QUAN TRỌNG ---
            // Luôn kiểm tra 'sanPham' có bị null hay không TRƯỚC KHI trả về View.
            // Nếu API thất bại (result.IsSuccessStatusCode là false) 
            // HOẶC API thành công nhưng không đọc được dữ liệu (ReadAsAsync trả về null)
            // thì 'sanPham' đều sẽ là null.
            if (sanPham == null)
            {
                return HttpNotFound(); // Trả về lỗi 404 Not Found
            }
            // --- HẾT PHẦN SỬA LỖI ---

            // Nếu code chạy đến đây, 'sanPham' chắc chắn có giá trị
            return View(sanPham);
        }

        // HÀM TIỆN ÍCH: Tải danh sách danh mục (dùng cho Create và Edit)
        private async Task LoadDanhMucList(object selectedValue = null)
        {
            using (var client = GetHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("api/DanhMuc");
                if (response.IsSuccessStatusCode)
                {
                    var danhMucs = await response.Content.ReadAsAsync<IEnumerable<DanhMucViewModel>>();
                    ViewBag.DanhMucList = new SelectList(danhMucs, "MADANHMUC", "TENDANHMUC", selectedValue);
                }
                else
                {
                    ViewBag.DanhMucList = new SelectList(new List<DanhMucViewModel>(), "MADANHMUC", "TENDANHMUC");
                    ModelState.AddModelError(string.Empty, "Không thể tải danh sách danh mục từ API.");
                }
            }
        }

        // GET: Hiển thị form tạo mới VỚI DANH SÁCH DANH MỤC
        public async Task<ActionResult> Create()
        {
            await LoadDanhMucList(); // Gọi hàm tiện ích
            return View();
        }

        // POST: Xử lý tạo mới sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SanPhamViewModel sanPham, HttpPostedFileBase hinhAnhFile)
        {
            // Nếu model hợp lệ (không có lỗi validation)
            if (ModelState.IsValid)
            {
                // Xử lý lưu file ảnh
                if (hinhAnhFile != null && hinhAnhFile.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(hinhAnhFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);

                    // Đảm bảo thư mục tồn tại
                    Directory.CreateDirectory(Server.MapPath("~/Content/images"));

                    hinhAnhFile.SaveAs(path);
                    sanPham.HINHANH = fileName;
                }

                // Gọi API để tạo mới
                using (var client = GetHttpClient())
                {
                    HttpResponseMessage result = await client.PostAsJsonAsync("api/SanPham", sanPham);
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Đã thêm sản phẩm thành công!";
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Lỗi khi tạo sản phẩm qua API.");
            }

            // Nếu ModelState không hợp lệ, tải lại danh sách danh mục
            await LoadDanhMucList(sanPham.MADANHMUC);

            // Trả về view với model hiện tại để người dùng sửa lỗi
            return View(sanPham);
        }



        // GET: Hiển thị form chỉnh sửa
        public async Task<ActionResult> Edit(int id)
        {
            SanPhamViewModel sanPham;
            using (var client = GetHttpClient())
            {
                HttpResponseMessage result = await client.GetAsync($"api/SanPham/{id}");
                if (result.IsSuccessStatusCode)
                {
                    sanPham = await result.Content.ReadAsAsync<SanPhamViewModel>();
                }
                else
                {
                    return HttpNotFound();
                }
            }

            // Tải danh sách danh mục và chọn mục hiện tại
            await LoadDanhMucList(sanPham.MADANHMUC);

            return View(sanPham);
        }

        //PUT: Xử lý chỉnh sửa sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SanPhamViewModel sanPham, HttpPostedFileBase hinhAnhFile)
        {
            if (ModelState.IsValid)
            {
                // BƯỚC 1: XỬ LÝ LƯU FILE ẢNH (NẾU CÓ)
                if (hinhAnhFile != null && hinhAnhFile.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(hinhAnhFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);

                    // Đảm bảo thư mục tồn tại
                    Directory.CreateDirectory(Server.MapPath("~/Content/images"));

                    hinhAnhFile.SaveAs(path);
                    sanPham.HINHANH = fileName;
                }
                // Nếu không có file mới, `sanPham.HINHANH` sẽ giữ nguyên giá trị cũ (từ input hidden)

                // BƯỚC 2: GỬI DỮ LIỆU ĐÃ CẬP NHẬT LÊN API
                using (var client = GetHttpClient())
                {
                    HttpResponseMessage result = await client.PutAsJsonAsync($"api/SanPham/{id}", sanPham);

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Lỗi khi cập nhật sản phẩm qua API.");
            }

            // Nếu không hợp lệ, tải lại danh sách danh mục
            await LoadDanhMucList(sanPham.MADANHMUC);

            return View(sanPham);
        }

        // POST: Xử lý xóa sản phẩm (Giữ nguyên)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = GetHttpClient())
            {
                HttpResponseMessage result = await client.DeleteAsync($"api/SanPham/{id}");
                if (!result.IsSuccessStatusCode)
                {
                    // Thêm thông báo lỗi nếu muốn
                    TempData["ErrorMessage"] = "Lỗi khi xóa sản phẩm.";
                }
            }
            return RedirectToAction("Index");
        }
    }
}