using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Web.Mvc;
using QLBH_WEB.Models;

namespace WEB_APP.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult DanhMucMenu()
        {
            IEnumerable<DanhMucViewModel> danhMucList;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_apiBaseUrl);
                var responseTask = client.GetAsync("api/DanhMuc");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<DanhMucViewModel>>();
                    readTask.Wait();
                    danhMucList = readTask.Result;
                }
                else
                {
                    danhMucList = new List<DanhMucViewModel>();
                }
            }
            return PartialView("_DanhMucMenu", danhMucList);
        }
    }
}