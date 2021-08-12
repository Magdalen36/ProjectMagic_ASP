using Microsoft.AspNetCore.Mvc;
using ProjectMagic_ASP.Models;
using ProjectMagic_ASP.Models.Forms;
using ProjectMagic_ASP.Services;
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
        private readonly IService<CardModel, CardForm> _cardService;

        public ViewerController(IService<EditionModel, EditionForm> es, IService<CardModel, CardForm> cs)
        {
            _editionService = es;
            _cardService = cs;
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

        public IActionResult ListCardByEdition([FromRoute] int id)
        {
            IEnumerable<CardModel> model = (_cardService as CardService).GetByEditionId(id);
            return View(model);
        }
    }
}
