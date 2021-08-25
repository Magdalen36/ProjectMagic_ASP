using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectMagic.Services;
using ProjectMagic_ASP.Models;
using ProjectMagic_ASP.Models.Forms;
using ProjectMagic_ASP.Services;
using ProjectMagic_ASP.Services.Bases;
using ProjectMagic_ASP.Tools;
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
        private readonly IService<ColorModel, ColorForm> _colorService;
        private readonly IService<TypeModel, TypeForm> _typeService;
        private readonly IService<RarityModel, RarityForm> _rarityService;

        public ViewerController(IService<EditionModel, EditionForm> es, IService<CardModel, CardForm> cs, IService<ColorModel, ColorForm> co, IService<TypeModel, TypeForm> ts, IService<RarityModel, RarityForm> rs)
        {
            _editionService = es;
            _cardService = cs;
            _colorService = co;
            _typeService = ts;
            _rarityService = rs;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListEdition(string name)
        {
            IEnumerable<EditionModel> model = _editionService.GetAll();

            //Pour afficher les éditions "vides" 
            foreach (EditionModel item in model)
            {
                //Ne fonctionne pas dans le cas où la liste est justement vite
                //avec le try/catch, on passe juste au suivant en évitant l'exception renvoyée du service
                try 
                {
                    IEnumerable<CardModel> cm = (_cardService as CardService).GetByEditionId(item.Id);
                    if (cm.Count() > 0) { item.IsCard = true; } else { item.IsCard = false; }
                }
                catch(Exception)
                {

                }
            }

            //Pour la recherche
            if (name is not null)
                model = model.Where(m => m.Name.ToUpper().Contains(name.ToUpper()));
            
            return View(model);
        }

        public IActionResult ListCardByEdition([FromRoute] int id)
        {
            TempData["isLogged"] = HttpContext.Session.Get<bool>("IsLogged");
            IEnumerable<CardModel> model = (_cardService as CardService).GetByEditionId(id);
            return View(model);
        }

        public IActionResult GetCardById([FromRoute] int id)
        {
            TempData["isLogged"] = HttpContext.Session.Get<bool>("IsLogged");
            CardModel model = _cardService.GetById(id);
            return View(model);
        }

        public IActionResult ListCard(string name, string color, string type, string rarity)
        {
            TempData["isLogged"] = HttpContext.Session.Get<bool>("IsLogged");

            CardFullModel cfm = new CardFullModel();

            cfm.ListCards = (_cardService as CardService).GetRandom(30);
            cfm.ListColors = _colorService.GetAll();
            cfm.ListRaretes = _rarityService.GetAll();
            cfm.ListTypes = _typeService.GetAll();

            if (name is not null || color is not null || type is not null  || rarity is not null )
            {                     
                cfm.ListCards = _cardService.GetAll();

                if (name is not null)
                    cfm.ListCards = cfm.ListCards.Where(m => m.CardName.ToUpper().Contains(name.ToUpper()));
                if (color is not null)
                    cfm.ListCards = cfm.ListCards.Where(m => m.ColorName.ToUpper().Contains(color.ToUpper()));
                if (type is not null)
                    cfm.ListCards = cfm.ListCards.Where(m => m.TypeCardName.ToUpper().Contains(type.ToUpper()));
                if (rarity is not null)
                    cfm.ListCards = cfm.ListCards.Where(m => m.RarityName.ToUpper().Contains(rarity.ToUpper()));
                
            }
            return View(cfm);
            
        }
    }
}
