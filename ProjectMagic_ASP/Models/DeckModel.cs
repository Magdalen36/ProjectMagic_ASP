using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Models
{
    public class DeckModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DeckName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int NbCard { get; set; }
        public string ColorName { get; set; }
        public int ColorId { get; set; }

        public IEnumerable<CardInDeckModel> cid { get; set; }
        public int NbArpenteurs { get; set; }
        public int NbCreatures { get; set; }
        public int NbSorts { get; set; }
        public int NbArtefacts { get; set; }
        public int NbTerrains { get; set; }
    }
}
