using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Controllers
{
    public class CollectionController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
