using Microsoft.AspNetCore.Mvc;
using ProjectMagic_ASP.Models;
using ProjectMagic_ASP.Models.Forms;
using ProjectMagic_ASP.Services.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EditionController : Controller
    {
        private readonly IService<EditionModel, EditionForm> _editionService;

        public EditionController(IService<EditionModel, EditionForm> es)
        {
            _editionService = es;
        }

        public IActionResult Index()
        {
            IEnumerable<EditionModel> model = _editionService.GetAll();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EditionForm form)
        {
            if (ModelState.IsValid)
            {
                _editionService.Insert(form);
                TempData["success"] = "Insertion effectuée";
                return RedirectToAction("Index");
            }
            else
            {
                return View(form);
            }
        }

        public IActionResult Delete([FromRoute] int id)
        {
            if(_editionService.Delete(id))
            {
                TempData["success"] = "Suppression OK";
            }
            else
            {
                TempData["error"] = "Erreur de suppression";
            }
            //pas de vue, on redirige direct
            return RedirectToAction("Index");
        }

        //affichage
        public IActionResult Update([FromRoute] int id)
        {
            EditionModel model = _editionService.GetById(id);
            EditionForm form = new EditionForm { Name = model.Name, NbMax = model.NbMax };

            if (form == null) return NotFound();

            return View(form);
        }
        //Traitement
        [HttpPost]
        public IActionResult Update(EditionForm form)
        {
            if (ModelState.IsValid)
            {
                _editionService.Update(form);
                TempData["success"] = "Modification effectuée";
                return RedirectToAction("Index");
            }
            else
            {
                return View(form);
            }
        }

    }
}
