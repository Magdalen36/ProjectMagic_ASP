using Microsoft.AspNetCore.Mvc;
using ProjectMagic_ASP.Models;
using ProjectMagic_ASP.Models.Forms;
using ProjectMagic_ASP.Services.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Controllers
{
    public class ViewerController : Controller
    {
        private readonly IService<EditionModel, EditionForm> _editionService;

        public ViewerController(IService<EditionModel, EditionForm> es)
        {
            _editionService = es;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListEdition()
        {
            IEnumerable<EditionModel> model = _editionService.GetAll();
            return View(model);
        }


    }
}
