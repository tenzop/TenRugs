using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TenRugs.Controllers
{
    public class RugsController : Controller
    {
        // GET: Rug
        public ActionResult Index()
        {
            return View();
        }
    }
}