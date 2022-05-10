using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
    public class ConfiguracionServicio
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public Contrato Contrato { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public List<DetalleConfiguracionServicio> Detalle { get; set; }
        public bool Estatus { get; set; }

        public ConfiguracionServicio()
        {
            this.Cliente = new Cliente();
            this.Contrato = new Contrato();
            this.Ubicacion = new Ubicacion();
            this.Detalle = new List<DetalleConfiguracionServicio>();
        }
    }
}
