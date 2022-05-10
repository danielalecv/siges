﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
    public class TraspasoActivoFijo
    {
        public int Id { get; set; }
        public Ubicacion UbicacionOrigen { get; set; }
        public Ubicacion UbicacionDestino { get; set; }
        public string MotivoSalida { get; set; }
        public string Folio { get; set; }
        public DateTime FechaSalida { get; set; }
        public string Paqueteria { get; set; }
        public string NumGuia { get; set; }
        public DateTime FechaEnvio { get; set; }
        public List<TraspasoDetalleActivoFijo> Detalle { get; set; }
        public bool Estatus { get; set; }

        public TraspasoActivoFijo()
        {
            this.UbicacionOrigen = new Ubicacion();
            this.UbicacionDestino = new Ubicacion();
            this.Detalle = new List<TraspasoDetalleActivoFijo>();
        }
    }
}
