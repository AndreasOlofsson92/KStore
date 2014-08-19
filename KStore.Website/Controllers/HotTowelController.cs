using System.Web.Mvc;

namespace KStore.Website.Controllers
{
    [AllowAnonymous]
    public class HotTowelController : Controller
    {
        //
        // GET: /HotTowel/

        public ActionResult Index()
        {
            return View();
        }

    }
}
