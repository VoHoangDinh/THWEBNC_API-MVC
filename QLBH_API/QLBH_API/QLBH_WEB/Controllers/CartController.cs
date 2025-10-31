using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using QLBH_WEB.Models; // Đảm bảo namespace này là đúng

namespace QLBH_WEB.Controllers
{
    public class CartController : Controller
    {
        private readonly string _apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        private const string CartSession = "CartSession"; // Tên của key lưu trong Session

        private HttpClient GetHttpClient()
        {
            var client = new HttpClient { BaseAddress = new System.Uri(_apiBaseUrl) };
            return client;
        }

        // GET: /Cart/Index
        // Hiển thị trang giỏ hàng từ dữ liệu trong Session
        public ActionResult Index()
        {
            var cart = Session[CartSession] as Cart;
            if (cart == null)
            {
                cart = new Cart(); // Nếu chưa có giỏ hàng, tạo mới
            }
            return View(cart);
        }

        // POST: /Cart/AddToCart
        // Thêm sản phẩm vào giỏ hàng và lưu vào Session
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToCart(int id, int soLuong = 1)
        {
            SanPhamViewModel sanPham;

            // Bước 1: Luôn lấy thông tin sản phẩm mới nhất từ API để đảm bảo đúng giá
            using (var client = GetHttpClient())
            {
                HttpResponseMessage result = await client.GetAsync($"api/SanPham/{id}");
                if (result.IsSuccessStatusCode)
                {
                    sanPham = await result.Content.ReadAsAsync<SanPhamViewModel>();
                }
                else
                {
                    TempData["CartError"] = "Không tìm thấy sản phẩm để thêm vào giỏ.";
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }

            // Bước 2: Lấy giỏ hàng từ Session hoặc tạo mới
            var cart = Session[CartSession] as Cart ?? new Cart();

            // Bước 3: Thêm sản phẩm vào đối tượng giỏ hàng trong bộ nhớ
            cart.AddItem(sanPham, soLuong);

            // Bước 4: Lưu đối tượng giỏ hàng đã cập nhật trở lại vào Session
            Session[CartSession] = cart;

            // Bước 5: Chuyển hướng đến trang giỏ hàng
            return RedirectToAction("Index");
        }

        // POST: /Cart/UpdateCart
        // Cập nhật số lượng các sản phẩm trong giỏ hàng trong Session
        [HttpPost]
        public ActionResult UpdateCart(FormCollection form)
        {
            var cart = Session[CartSession] as Cart;
            if (cart != null)
            {
                foreach (var key in form.AllKeys)
                {
                    if (key.StartsWith("quantity_"))
                    {
                        int maSP = int.Parse(key.Substring(9));
                        int soLuong = int.Parse(form[key]);
                        cart.UpdateItem(maSP, soLuong);
                    }
                }
                Session[CartSession] = cart; // Lưu lại thay đổi vào Session
            }
            return RedirectToAction("Index");
        }

        // GET: /Cart/RemoveFromCart/5
        // Xóa một sản phẩm khỏi giỏ hàng trong Session
        public ActionResult RemoveFromCart(int id)
        {
            var cart = Session[CartSession] as Cart;
            if (cart != null)
            {
                cart.RemoveItem(id);
                Session[CartSession] = cart; // Lưu lại thay đổi vào Session
            }
            return RedirectToAction("Index");
        }

        // Child Action để hiển thị widget giỏ hàng trên mọi trang
        [ChildActionOnly]
        public ActionResult ShoppingCartWidget()
        {
            var cart = Session[CartSession] as Cart ?? new Cart();
            return PartialView("_ShoppingCartWidget", cart);
        }
    }
}