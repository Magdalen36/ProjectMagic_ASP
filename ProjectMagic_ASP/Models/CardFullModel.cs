using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMagic_ASP.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Models
{
    public class CardFullModel
    {
        public string NameR { get; set; }
 
        public IEnumerable<CardModel> ListCards { get; set; }
        public IEnumerable<ColorModel> ListColors { get; set; }
        public IEnumerable<SousTypeModel> ListSousTypes { get; set; }
        public IEnumerable<TypeModel> ListTypes { get; set; }
        public IEnumerable<RarityModel> ListRaretes { get; set; }
        

        public CardFullModel()
        {
            //ListTypes = new List<string> { "Créatures", "Artefacts", "Auras", "Enchantements", "Ephémères", "Arpenteurs", "Rituels", "Terrains" };
            //ListColors = new List<string> {"Rouge","Bleu","Vert","Blanc","Noir","Multicolore","Incolore" };
            //ListRaretes = new List<string> {"Mythiques","Rares","Peu communes","Communes"};
        }
    }
}
