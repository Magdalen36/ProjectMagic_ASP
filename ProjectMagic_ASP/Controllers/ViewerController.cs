using Microsoft.AspNetCore.Mvc;
using ProjectMagic.Services;
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
                catch(Exception e)
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
            IEnumerable<CardModel> model = (_cardService as CardService).GetByEditionId(id);
            return View(model);
        }

        public IActionResult GetCardById([FromRoute] int id)
        {
            CardModel model = _cardService.GetById(id);
            return View(model);
        }

        public IActionResult ListCard(string name, string color, string type, string rarity)
        {
            IEnumerable<CardModel> modelRandom = (_cardService as CardService).GetRandom(30);

            if (name is not null || color is not null || type is not null  || rarity is not null )
            {              
                IEnumerable<CardModel> model = _cardService.GetAll();

                if (name is not null)
                    model = model.Where(m => m.CardName.ToUpper().Contains(name.ToUpper()));
                if (color is not null)
                    model = model.Where(m => m.ColorName.ToUpper().Contains(color.ToUpper()));
                if (type is not null)
                    model = model.Where(m => m.TypeCardName.ToUpper().Contains(type.ToUpper()));
                if (rarity is not null)
                    model = model.Where(m => m.RarityName.ToUpper().Contains(rarity.ToUpper()));
                return View(model);
            }
            else
            {
                return View(modelRandom);
            }
            
        }

        //public IActionResult SearchByEditionName() //affichage du form avec le nom 
        //{
        //    return View();
        //}
        //public IActionResult SearchByEditionName(string name) //affichage de la liste des éditions avec réception du nom
        //{
        //    IEnumerable<EditionModel> model = (_editionService as EditionService).GetByName(name);
        //    return View(model);
        //}
    }
}
