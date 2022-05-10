using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
    public class Servicio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [DisplayName("Linea de Negocio")]
        public int LineaNegocioId { get; set; }
        public virtual LineaNegocio LineaNegocio { get; set; }

        public bool Estatus { get; set; }
        [DisplayName("Precio")]
        public string Opcional1 { get; set; }
        [DisplayName("Costo proveedor externo")]
        public string Opcional2 { get; set; }

        public Servicio()
        {
            LineaNegocio = new LineaNegocio();
        }

        public Servicio Copia(){
          Servicio copia = new Servicio();
          copia.Id = this.Id;
          copia.Nombre = this.Nombre;
          copia.Descripcion = this.Descripcion;
          copia.LineaNegocio = this.LineaNegocio.Copia();
          copia.Estatus = this.Estatus;
          copia.Opcional1 = this.Opcional1;
          copia.Opcional2 = this.Opcional2;
          return copia;
        }

        public override bool Equals(Object obj){
          if(obj == null || GetType() != obj.GetType())
            return false;
          Servicio s = obj as Servicio;
          if((Object)s == null)
            return false;
          if(this.Id != s.Id)
            return false;
          if(!(String.Equals(this.Nombre, s.Nombre)))
            return false;
          if(!(String.Equals(this.Descripcion, s.Descripcion)))
            return false;
          if(!this.LineaNegocio.Equals(s.LineaNegocio))
            return false;
          if(this.Estatus != s.Estatus)
            return false;
          if(!(String.Equals(this.Opcional1, s.Opcional1)))
            return false;
          if(!(String.Equals(this.Opcional2, s.Opcional2)))
            return false;
          return true;
        }
    }
}
