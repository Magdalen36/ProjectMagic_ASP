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
    public class CollectionController : Controller
    {
        private readonly IService<CollectionModel, CollectionForm> _collectionService;

        public CollectionController(IService<CollectionModel, CollectionForm> cs)
        {
            _collectionService = cs;
        }

        public IActionResult Index()
        {
            IEnumerable<CollectionModel> model = (_collectionService as CollectionService).GetAllById(HttpContext.Session.Get<int>("UserId"));
            return View(model);
        }

        public IActionResult Insert([FromRoute] int id, string fromController, string fromAction, string? fromId, string? fromMotiv)
        {
            //Gestion du nombre de cartes 
            // -> rechercher l'utilisateur
            // -> rechercher si il a déjà la carte en collection 
            IEnumerable<CollectionModel> collections = _collectionService.GetAll().Where(x => x.UserId == HttpContext.Session.Get<int>("UserId"));
            CollectionModel model = collections.Where(x => x.CardId == id).FirstOrDefault();
            if (model != null || fromMotiv == "plus" || fromMotiv == "moins")
            {
                int temp = model.NbCard;
                if (fromMotiv == "plus") { temp++; }
                if (fromMotiv == "moins") { temp--; }
                    if(temp == 0) { /*delete*/ }
                    
                //Si la carte existe : faire un update du nombre
                CollectionForm form = new CollectionForm { Id = model.Id, UserId = model.UserId, CardId = model.CardId, NbCard = temp};
                try
                {
                    _collectionService.Update(form);
                    TempData["success"] = "Modification du nombre";
                }
                catch (Exception)
                {
                    TempData["error"] = "Erreur";
                }
            }
            else
            {
                if ((_collectionService as CollectionService).Insert(id, HttpContext.Session.Get<int>("UserId")))
                {
                    TempData["success"] = "Ajout à votre collection";
                }
                else
                {
                    TempData["error"] = "Erreur";
                }
            }

            //Interet : pouvoir rester sur la même page, quelque soit l'endroit d'où on appelle cette action
            if(fromId is null)
            return RedirectToAction(fromAction, fromController);
            else
            return RedirectToAction(fromAction, new { Controller = fromController, id = fromId });
        }

        public IActionResult Delete([FromRoute] int id)
        {
            if (_collectionService.Delete(id))
            {
                TempData["success"] = "Suppression OK";
            }
            else
            {
                TempData["error"] = "Erreur de suppression";
            }        
            return RedirectToAction("Index");
        }
    }
}
