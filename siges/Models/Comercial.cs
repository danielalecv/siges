using System;

namespace siges.Models {
    public class Comercial {
        public int Id {get; set;}
        public bool Estatus {get; set;}
        public int OrdenServicioId {get; set;}
        public int PersonaId {get; set;}
        public DateTime FechaAdministrativa {get; set;}
    }
}
