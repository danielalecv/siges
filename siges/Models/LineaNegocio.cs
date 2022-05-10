using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
    public class LineaNegocio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estatus { get; set; }
        [DisplayName("Campo Opcional 1")]
        public string Opcional1 { get; set; }
        [DisplayName("Campo Opcional 2")]
        public string Opcional2 { get; set; }

        public LineaNegocio Copia(){
          LineaNegocio copia = new LineaNegocio();
          copia.Id = this.Id;
          copia.Nombre = this.Nombre;
          copia.Estatus = this.Estatus;
          copia.Opcional1 = this.Opcional1;
          copia.Opcional2 = this.Opcional2;
          return copia;
        }

        public override bool Equals(Object obj){
          if(obj == null || GetType() != obj.GetType())
            return false;
          LineaNegocio l = obj as LineaNegocio;
          if((Object)l == null)
            return false;
          if(!(String.Equals(this.Nombre.ToUpper(), l.Nombre.ToUpper())))
            return false;
          return true;
        }
    }
}
