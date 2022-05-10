using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace siges.DTO {
    public class InventarioAFDTO {
        public int ActivoFijoId {get; set;}
        public int Cantidad {get; set;}
        public string Observaciones {get; set;}
    }
}
