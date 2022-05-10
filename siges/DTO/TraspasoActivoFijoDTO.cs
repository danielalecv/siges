using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.DTO
{
    public class TraspasoActivoFijoDTO
    {
        public int Id { get; set; }
        public int UbicacionOrigen { get; set; }
        public int UbicacionDestino { get; set; }
        public string MotivoSalida { get; set; }
        public string Folio { get; set; }
        public string FechaSalida { get; set; }
        public string Paqueteria { get; set; }
        public string NumGuia { get; set; }
        public string FechaEnvio { get; set; }
        public List<ActivoFijoDTO> Detalle { get; set; }
        public bool Estatus { get; set; }
    }
}
