using ProjectMagic_ASP.Services.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Models.Forms
{
    public class CardForm
    {
        //injection de dépendance dans le formulaire pour avoir liste déroulante avec les choix
        //IService<ColorModel, ColorForm> _colorService;

        //public CardForm(IService<ColorModel, ColorForm> cs)
        //{
        //    _colorService = cs;
        //    ListColors = _colorService.GetAll();
        //}

        public int Id { get; set; }
        public string CardName { get; set; }
        public string Cost { get; set; }
        public string PS { get; set; }
        public bool Premium { get; set; }
        public string Description { get; set; }

        public int EditionId { get; set; }
        public int RarityId { get; set; }
        public int ColorId { get; set; }
        public int TypeId { get; set; }
        public int SousTypeId { get; set; }

        //public IEnumerable<ColorModel> ListColors { get; set; }

    }
}
