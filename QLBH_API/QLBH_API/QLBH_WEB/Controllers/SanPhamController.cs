using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using QLBH_WEB.Models;

namespace WEB_APP.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly string _apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

        public async Task<ActionResult> Index(int maDanhMuc = 1)
        {
            IEnumerable<SanPhamViewModel> sanPhamList;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_apiBaseUrl);
                HttpResponseMessage result = await client.GetAsync($"api/SanPham/TheoDanhMuc/{maDanhMuc}");
                if (result.IsSuccessStatusCode)
                {
                    sanPhamList = await result.Content.ReadAsAsync<IEnumerable<SanPhamViewModel>>();
                }
                else
                {
                    sanPhamList = new List<SanPhamViewModel>();
                }
            }
            return View(sanPhamList);
        }

        public async Task<ActionResult> Detail(int id)
        {
            SanPhamViewModel sanPham;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_apiBaseUrl);
                HttpResponseMessage result = await client.GetAsync($"api/SanPham/{id}");
                if (result.IsSuccessStatusCode)
                {
                    sanPham = await result.Content.ReadAsAsync<SanPhamViewModel>();
                }
                else
                {
                    sanPham = null;
                }
            }
            return View(sanPham);
        }
    }
}