using System.Web.Mvc;

namespace JustBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "I would try to update u with the latest technology posts";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "My blog description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "My contact page.";

            return View();
        }
    }
}
