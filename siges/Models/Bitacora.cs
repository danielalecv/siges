using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
    public class Bitacora
    {
        public int ID { get; set; }
        public String UserId { get; set; }
        public DateTime EventDate { get; set; }
        public String Event { get; set; }
        public String Description { get; set; }
        public String Section { get; set; }
    }
}
