using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Models.Forms
{
    public class CardForm
    {
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
    }
}
