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
    public class CardController : Controller
    {

        private readonly IService<CardModel, CardForm> _cardService;

        public CardController(IService<CardModel, CardForm> cs)
        {
            _cardService = cs;
        }

        public IActionResult Index()
        {
            IEnumerable<CardModel> model = _cardService.GetAll();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CardForm form)
        {
            if (ModelState.IsValid)
            {
                _cardService.Insert(form);
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
            if (_cardService.Delete(id))
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
            CardModel model = _cardService.GetById(id);
            CardForm form = new CardForm { CardName = model.CardName, Cost = model.Cost, PS = model.PS, Description = model.Description, ColorId = model.ColorId, EditionId = model.EditionId, RarityId = model.RarityId, TypeId = model.TypeCardId, SousTypeId = model.SousTypeCardId };

            if (form == null) return NotFound();

            return View(form);
        }
        //Traitement
        [HttpPost]
        public IActionResult Update(CardForm form)
        {
            if (ModelState.IsValid)
            {
                _cardService.Update(form);
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
