using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.DTO
{
    public class OrdenInsumoDTO
    {
        public int Id { get; set; }
        public int InsumoId { get; set; }
        //public int LoteId {get; set;}
        public string Lote {get; set;}
        public string Caducidad {get; set;}
        public int Cantidad { get; set; }
    }
}
