using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Models
{
    public class CardFullModel
    {
        public string NameR { get; set; }
        public List<string> ListTypes { get; set; }
        public List<string> ListColors { get; set; }
        public List<string> ListRaretes { get; set; }
        public IEnumerable<CardModel> ListCards { get; set; }

        //Le plus propre serait de faire des modèles pour les types, colors, .. 
        //Et de les traiter comme CardModel

        public CardFullModel()
        {
            ListTypes = new List<string> { "Créatures", "Artefacts", "Auras", "Enchantements", "Ephémères", "Arpenteurs", "Rituels", "Terrains" };
            ListColors = new List<string> {"Rouge","Bleu","Vert","Blanc","Noir","Multicolore","Incolore" };
            ListRaretes = new List<string> {"Mythiques","Rares","Peu communes","Communes"};
        }
    }
}
