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
    }
}
