using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Models.Forms
{
    public class DeckForm
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DeckName { get; set; }
        public int ColorId { get; set; }
    }
}
