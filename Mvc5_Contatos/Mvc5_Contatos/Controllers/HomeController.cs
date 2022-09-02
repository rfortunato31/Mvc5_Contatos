using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5_Contatos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Aplicação para cadastro e movimentação de Fundos.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Rafaela Fortunato";

            return View();
        }
    }
}