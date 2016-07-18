using PluginImplementation.Utils;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PluginImplementation.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult _PluginMenuPartial()
        {
            string folder = Server.MapPath("/Plugins/");
            List<IPlugin> list = Utility.GetPlugins<IPlugin>(folder);

            return PartialView(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}