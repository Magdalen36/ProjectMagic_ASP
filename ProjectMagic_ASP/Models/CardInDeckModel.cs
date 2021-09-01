using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Models
{
    public class CardInDeckModel
    {
        public int Id { get; set; }
        public int DeckId { get; set; }
        public int CardId { get; set; }
        public int NbCard { get; set; }

        public int UserId { get; set; }
        public int ColorId { get; set; }
        public int RarityId { get; set; }
        public int TypeId { get; set; }
        public int SousTypeId { get; set; }

        public string DeckName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string CardName { get; set; }
        public string Cost { get; set; }
        public string Description { get; set; }
        public string PS { get; set; }

        public string ColorName { get; set; }
        public string RarityName { get; set; }
        public string TypeName { get; set; }
        public string SousTypeName { get; set; }
    }
}
