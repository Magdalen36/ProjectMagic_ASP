using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Models
{
    public class CardModel
    {
        public int Id { get; set; }
        public string CardName { get; set; }
        public string Cost { get; set; }
        public string PS { get; set; }
        public bool Premium { get; set; }
        public string Description { get; set; }

        public int EditionId { get; set; }
        public string EditionName { get; set; }
        public int RarityId { get; set; }
        public string RarityName { get; set; }
        public int TypeCardId { get; set; }
        public string TypeCardName { get; set; }
        public int SousTypeCardId { get; set; }
        public string SousTypeCardName { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
    }
}
