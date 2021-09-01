using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class DeckController : Controller
    {
        private readonly IService<DeckModel, DeckForm> _deckService;
        private readonly IService<CardInDeckModel, CardInDeckForm> _cardInDeckService;
        private readonly IService<CardModel, CardForm> _cardService;
        private readonly IService<ColorModel, ColorForm> _colorService;
        private readonly IService<RarityModel, RarityForm> _rarityService;
        private readonly IService<TypeModel, TypeForm> _typeService;
        private readonly IService<SousTypeModel, SousTypeForm> _sousTypeService;

        public DeckController(IService<DeckModel, DeckForm> ds, IService<CardInDeckModel, CardInDeckForm> cs, IService<CardModel, CardForm> cards, IService<ColorModel, ColorForm> colors, IService<RarityModel, RarityForm> rs, IService<TypeModel, TypeForm> ts, IService<SousTypeModel, SousTypeForm> sts)
        {
            _deckService = ds;
            _cardInDeckService = cs;
            _cardService = cards;
            _colorService = colors;
            _rarityService = rs;
            _typeService = ts;
            _sousTypeService = sts;
        }

        public IActionResult Index()
        {
            IEnumerable<DeckModel> model = (_deckService as DeckService).GetAllById(HttpContext.Session.Get<int>("UserId"));

            foreach (DeckModel item in model)
            {
                item.NbCard = (_cardInDeckService as CardInDeckService).GetAllByDeck(item.Id).Count();
                //item.ColorName = (_cardInDeckService as CardInDeckService).GetAllByDeck(item.Id).Select(x => x.ColorName).FirstOrDefault();
            }
            return View(model);
        }

        public IActionResult ListCardByDeck([FromRoute] int id)
        {
            IEnumerable<CardInDeckModel> model = (_cardInDeckService as CardInDeckService).GetAllByDeck(id);
            HttpContext.Session.Set<int>("ColorId", _deckService.GetById(id).ColorId);
            HttpContext.Session.Set<int>("DeckId", id);
            return View(model);
        }

        //DECK
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DeckForm form)
        {
            if (ModelState.IsValid)
            {
                form.UserId = HttpContext.Session.Get<int>("UserId");
                _deckService.Insert(form);
                TempData["success"] = "Insertion effectuée";
                return RedirectToAction("Index");
            }
            else
            {
                return View(form);
            }
        }

        //DECK
        public IActionResult Delete([FromRoute] int id)
        {
            if (_deckService.Delete(id))
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

        //DECK
        public IActionResult Update([FromRoute] int id)
        {
            DeckModel model = _deckService.GetById(id);
            DeckForm form = new DeckForm { Id = model.Id, UserId = model.UserId, DeckName = model.DeckName };

            if (form == null) return NotFound();

            return View(form);
        }

        [HttpPost]
        public IActionResult Update(DeckForm form)
        {
            if (ModelState.IsValid)
            {
                DeckModel model = _deckService.GetById(form.Id);
                form.ColorId = model.ColorId;
                form.UserId = HttpContext.Session.Get<int>("UserId");
                _deckService.Update(form);
                TempData["success"] = "Modification effectuée";
                return RedirectToAction("Index");
            }
            else
            {
                return View(form);
            }
        }

        public IActionResult ListCardToAdd(string name, string sousType, string type, string rarity)
        {
            TempData["isLogged"] = HttpContext.Session.Get<bool>("IsLogged");

            CardFullModel cfm = new CardFullModel();

            cfm.ListCards = _cardService.GetAll().Where(c => c.ColorId == HttpContext.Session.Get<int>("ColorId") || c.ColorId == 6); 
            cfm.ListSousTypes = _sousTypeService.GetAll();
            cfm.ListRaretes = _rarityService.GetAll();
            cfm.ListTypes = _typeService.GetAll();

            if (name is not null || sousType is not null || type is not null || rarity is not null)
            {

                if (name is not null)
                    cfm.ListCards = cfm.ListCards.Where(m => m.CardName.ToUpper().Contains(name.ToUpper()));
                if (sousType is not null)
                    cfm.ListCards = cfm.ListCards.Where(m => m.SousTypeCardName.ToUpper().Contains(sousType.ToUpper()));
                if (type is not null)
                    cfm.ListCards = cfm.ListCards.Where(m => m.TypeCardName.ToUpper().Contains(type.ToUpper()));
                if (rarity is not null)
                    cfm.ListCards = cfm.ListCards.Where(m => m.RarityName.ToUpper().Contains(rarity.ToUpper()));

            }
            return View(cfm);
        }

        //CARD
        public IActionResult InsertCard([FromRoute] int id, string fromController, string fromAction, string? fromId, string? fromMotiv)
        {

            //Gestion du nombre de cartes dans un deck 
            //1.Vérifier que la carte n'est pas déjà dans le deck. Si oui, gestion du nombre
            //2.Vérifier le nombre de cartes que l'on peut mettre selon la rareté 

            IEnumerable<CardInDeckModel> cid = _cardInDeckService.GetAll().Where(x => x.DeckId == HttpContext.Session.Get<int>("DeckId"));
            CardInDeckModel model = cid.Where(x => x.CardId == id).FirstOrDefault();
            if (model != null || fromMotiv == "plus" || fromMotiv == "moins")
            {
                int temp = model.NbCard;
                if (fromMotiv == "plus")  temp++; 
                else if (fromMotiv == "moins")  temp--; 
                else if (model != null) temp++;
                if (temp == 0) { /*delete*/ }

                //update du nombre si existe
                CardInDeckForm form = new CardInDeckForm { Id = model.Id, CardId = model.CardId, DeckId = model.DeckId, NbCard = temp };
                try
                {
                    _cardInDeckService.Update(form);
                    TempData["success"] = "Modification du nombre";
                }
                catch (Exception)
                {
                    TempData["error"] = "Erreur";
                }
            }
            else
            {
                if ((_cardInDeckService as CardInDeckService).Insert(id, HttpContext.Session.Get<int>("DeckId")))
                {
                    TempData["success"] = "Ajout au deck";
                }
                else
                {
                    TempData["error"] = "Erreur";
                }
            }
                
            if (fromId is null)
                return RedirectToAction(fromAction, fromController);
            else
                return RedirectToAction(fromAction, new { Controller = fromController, id = fromId });
        }
    }
}
