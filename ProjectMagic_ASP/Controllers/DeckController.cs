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
            TempData["isLogged"] = HttpContext.Session.Get<bool>("IsLogged");

            IEnumerable<DeckModel> model = (_deckService as DeckService).GetAllById(HttpContext.Session.Get<int>("UserId"));

            foreach (DeckModel item in model)
            {                
                IEnumerable<CardInDeckModel> cid = (_cardInDeckService as CardInDeckService).GetAllByDeck(item.Id);
                int count = 0;
                foreach(CardInDeckModel card in cid)
                {
                    count += card.NbCard;
                }
                item.NbCard = count;
            }
            return View(model);
        }

        public IActionResult ListCardByDeck([FromRoute] int id)
        {
            TempData["isLogged"] = HttpContext.Session.Get<bool>("IsLogged");

            DeckModel dm = _deckService.GetById(id);
            dm.cid = (_cardInDeckService as CardInDeckService).GetAllByDeck(id);
            int nbArpenteurs = 0; int nbCreatures = 0; int nbSorts = 0; int nbArtefacts = 0; int nbTerrains =0; int nbCard = 0;
            
            foreach (CardInDeckModel item in dm.cid)
            {
                nbCard += item.NbCard;
                switch (item.TypeId)
                {
                    case 1: nbArpenteurs+=item.NbCard; break;
                    case 2: nbArtefacts+=item.NbCard; break;
                    case 3: case 4: case 7: case 8: case 9: case 10: nbSorts+=item.NbCard; break;
                    case 5: case 6: nbCreatures+=item.NbCard; break;
                    case 11: nbTerrains += item.NbCard; break;
                }
            }
            dm.NbArpenteurs = nbArpenteurs; dm.NbArtefacts = nbArtefacts; dm.NbCreatures = nbCreatures; dm.NbSorts = nbSorts; dm.NbTerrains = nbTerrains;
            dm.NbCard = nbCard;

            HttpContext.Session.Set<int>("ColorId", _deckService.GetById(id).ColorId);
            HttpContext.Session.Set<int>("DeckId", id);
            return View(dm);
        }

        //DECK
        public IActionResult Create()
        {
            TempData["isLogged"] = HttpContext.Session.Get<bool>("IsLogged");
            return View();
        }
        [HttpPost]
        public IActionResult Create(DeckForm form)
        {
            TempData["isLogged"] = HttpContext.Session.Get<bool>("IsLogged");
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
            TempData["isLogged"] = HttpContext.Session.Get<bool>("IsLogged");
            DeckModel model = _deckService.GetById(id);
            DeckForm form = new DeckForm { Id = model.Id, UserId = model.UserId, DeckName = model.DeckName };

            if (form == null) return NotFound();

            return View(form);
        }

        [HttpPost]
        public IActionResult Update(DeckForm form)
        {
            TempData["isLogged"] = HttpContext.Session.Get<bool>("IsLogged");
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
            TempData["isLogged"] = HttpContext.Session.Get<bool>("IsLogged");
            //Gestion du nombre de cartes dans un deck 
            //Vérifier que la carte n'est pas déjà dans le deck. Si oui, gestion du nombre => update

            IEnumerable<CardInDeckModel> cid = _cardInDeckService.GetAll().Where(x => x.DeckId == HttpContext.Session.Get<int>("DeckId"));
            CardInDeckModel model = cid.Where(x => x.CardId == id).FirstOrDefault();
            if (model != null || fromMotiv == "plus" || fromMotiv == "moins")
            {
                int temp = model.NbCard;
                if (fromMotiv == "plus")  temp++; 
                else if (fromMotiv == "moins")  temp--; 
                else if (model != null) temp++;
                if (temp == 0) { /*delete*/ }
                if (temp > 4) temp = 4; //pas plus de 4x une carte de même nom

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

        //CARTE
        public IActionResult DeleteCard([FromRoute] int id)
        {

            if (_cardInDeckService.Delete(id))
            {
                TempData["success"] = "Suppression OK";
            }
            else
            {
                TempData["error"] = "Erreur de suppression";
            }
            //pas de vue, on redirige direct
            return RedirectToAction("ListCardByDeck", new { id = HttpContext.Session.Get<int>("DeckId") });
        }

        public IActionResult AdapterTerrain([FromRoute] int id) //id n'est pas un id, mais le nombre de cartes
        {
            TempData["isLogged"] = HttpContext.Session.Get<bool>("IsLogged");
            //Vérifier si les terrains ont déjà été calculés une fois 
            CardInDeckModel cm = _cardInDeckService.GetAll().Where(c => c.TypeId == 11).FirstOrDefault();
            if(cm!= null)
            {
                //enlever les terrains
                id -= cm.NbCard;
                _cardInDeckService.Delete(cm.Id);
            }

            //Lorsque le deck contient au moins 40 cartes, on peut rajouter des terrains
            if(id >= 40)
            {
                int nbTerrain = id / 2 ;

                int cardId = _cardService.GetAll().Where(c => c.TypeCardId == 11 && c.ColorId == HttpContext.Session.Get<int>("ColorId")).Select(c => c.Id).FirstOrDefault();

                CardInDeckForm cf = new CardInDeckForm { CardId = cardId, DeckId = HttpContext.Session.Get<int>("DeckId"), NbCard = nbTerrain };
                _cardInDeckService.Insert(cf);
            }
            return RedirectToAction("ListCardByDeck", new { id = HttpContext.Session.Get<int>("DeckId") });
        }
    }
}
