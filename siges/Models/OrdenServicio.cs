using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Models
{
  public class OrdenServicio : IComparable<OrdenServicio>
  {
    public int Id { get; set; }
    public string Folio { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public Cliente Cliente { get; set; }
    public Contrato Contrato { get; set; }
    public Ubicacion Ubicacion { get; set; }
    public LineaNegocio LineaNegocio { get; set; }
    public Servicio Servicio { get; set; }
    public string Tipo { get; set; }
    public string EstatusServicio { get; set; }
    public string Observaciones { get; set; }
    public string ContactoNombre { get; set; }
    public string ContactoAP { get; set; }
    public string ContactoAM { get; set; }
    public string ContactoEmail { get; set; }
    public string ContactoTelefono { get; set; }
    public string NombreCompletoCC1 { get; set; }
    public string EmailCC1 { get; set; }
    public string NombreCompletoCC2 { get; set; }
    public string EmailCC2 { get; set; }
    public string Opcional1 { get; set; }
    public string Opcional2 { get; set; }
    public string Opcional3 { get; set; }
    public string Opcional4 { get; set; }
    public bool Estatus { get; set; }
    [JsonIgnore]
    public List<OrdenInsumo> Insumos { get; set; }
    public List<OrdenPersona> Personal { get; set; }
    [JsonIgnore]
    public List<OrdenActivoFijo> Activos { get; set; }
    public Persona PersonaComercial {get; set;}
    public Persona PersonaValida {get; set;}
    public DateTime FechaAdministrativa {get; set;}

    public OrdenServicio()
    {
      this.Cliente = new Cliente();
      this.Contrato = new Contrato();
      this.Ubicacion = new Ubicacion();
      this.LineaNegocio = new LineaNegocio();
      this.Servicio = new Servicio();
      this.Insumos = new List<OrdenInsumo>();
      this.Personal = new List<OrdenPersona>();
      this.Activos = new List<OrdenActivoFijo>();
      this.PersonaComercial = new Persona();
      this.PersonaValida = new Persona();
    }

    public int CompareTo(OrdenServicio os){
      if(os == null)
        return 1;
      else
        return os.Id.CompareTo(this.Id);
    }

    public OrdenServicio Copia(){
      OrdenServicio copia = new OrdenServicio();
      copia.Id = this.Id;
      copia.Folio = this.Folio;
      copia.FechaInicio = this.FechaInicio;
      copia.FechaFin = this.FechaFin;
      copia.Cliente = this.Cliente.Copia();
      copia.Contrato = this.Contrato.Copia();
      copia.Ubicacion = this.Ubicacion.Copia();
      copia.LineaNegocio = this.LineaNegocio.Copia();
      copia.Servicio = this.Servicio.Copia();
      copia.Tipo = this.Tipo;
      copia.EstatusServicio = this.EstatusServicio;
      copia.Observaciones = this.Observaciones;
      copia.ContactoNombre = this.ContactoNombre;
      copia.ContactoAP = this.ContactoAP;
      copia.ContactoAM = this.ContactoAM;
      copia.ContactoEmail = this.ContactoEmail;
      copia.ContactoTelefono = this.ContactoTelefono;
      copia.NombreCompletoCC1 = this.NombreCompletoCC1;
      copia.EmailCC1 = this.EmailCC1;
      copia.NombreCompletoCC2 = this.NombreCompletoCC2;
      copia.EmailCC2 = this.EmailCC2;
      copia.Opcional1 = this.Opcional1;
      copia.Opcional2 = this.Opcional2;
      copia.Opcional3 = this.Opcional3;
      copia.Opcional4 = this.Opcional4;
      copia.Estatus = this.Estatus;
      if(this.Insumos != null){
        copia.Insumos = new List<OrdenInsumo>();
        foreach(OrdenInsumo oi in this.Insumos){
          copia.Insumos.Add(new OrdenInsumo(){Id = oi.Id, Cantidad = oi.Cantidad, Estatus = oi.Estatus, Insumo = oi.Insumo.Copia()});
        }
      } else {
        copia.Insumos = this.Insumos;
      }
      copia.Personal = this.Personal;
      copia.Activos = this.Activos;
      copia.PersonaComercial = this.PersonaComercial;
      copia.PersonaValida = this.PersonaValida;
      copia.FechaAdministrativa = this.FechaAdministrativa;
      return copia;
    }

    public override bool Equals(Object obj){
      if(obj == null || GetType() != obj.GetType())
        return false;
      OrdenServicio copia = obj as OrdenServicio;
      if((Object) copia == null)
        return false;
      //if(this.Id != copia.Id)
        //return false;
      if(!(String.Equals(this.Folio, copia.Folio)))
        return false;
      //if(!DateTime.Equals((DateTime)this.FechaInicio, (DateTime)copia.FechaInicio))
        //return false;
      //if(!DateTime.Equals((DateTime)this.FechaFin, (DateTime)copia.FechaFin))
        //return false;
      //if(!this.Cliente.Equals(copia.Cliente))
        //return false;
      //if(!this.Contrato.Equals(copia.Contrato))
        //return false;
      //if(!this.Ubicacion.Equals(copia.Ubicacion))
        //return false;
      //if(!this.LineaNegocio.Equals(copia.LineaNegocio))
        //return false;
      //if(!this.Servicio.Equals(copia.Servicio))
        //return false;
      //if(!(String.Equals(this.Tipo, copia.Tipo)))
        //return false;
      //if(!(String.Equals(this.EstatusServicio, copia.EstatusServicio)))
        //return false;
      //if(!(String.Equals(this.Observaciones, copia.Observaciones)))
        //return false;
      //if(!(String.Equals(this.ContactoNombre, copia.ContactoNombre)))
        //return false;
      //if(!(String.Equals(this.ContactoAP, copia.ContactoAP)))
        //return false;
      //if(!(String.Equals(this.ContactoAM, copia.ContactoAM)))
        //return false;
      //if(!(String.Equals(this.ContactoEmail, copia.ContactoEmail)))
        //return false;
      //if(!(String.Equals(this.ContactoTelefono, copia.ContactoTelefono)))
        //return false;
      //if(!(String.Equals(this.NombreCompletoCC1, copia.NombreCompletoCC1)))
        //return false;
      //if(!(String.Equals(this.EmailCC1, copia.EmailCC1)))
        //return false;
      //if(!(String.Equals(this.NombreCompletoCC2, copia.NombreCompletoCC2)))
        //return false;
      //if(!(String.Equals(this.EmailCC2, copia.EmailCC2)))
        //return false;
      //if(!(String.Equals(this.Opcional1, copia.Opcional1)))
        //return false;
      //if(!(String.Equals(this.Opcional2, copia.Opcional2)))
        //return false;
      //if(this.Opcional3 != copia.Opcional3)
        //return false;
      //if(this.Opcional4 != copia.Opcional4)
        //return false;
      //if(this.Estatus != copia.Estatus)
        //return false;
      //if(((this.Insumos != null) == (copia.Insumos != null))){
        //if(this.Insumos.Count > 0 && copia.Insumos.Count > 0){
          //Console.WriteLine("Insumos en las listas {0} {1}", this.Insumos.Count, copia.Insumos.Count);
          //if(this.Insumos.Count != copia.Insumos.Count)
            //return false;
          //Console.WriteLine("Las listas tiene la misma cantidad de elementos");
          //int count = 0;
          //do{
            //if(!this.Insumos[count].Equals(copia.Insumos[count]))
              //return false;
            //count++;
          //}while(this.Insumos.Count > count);
        //}
      //}
      //if(this.Personal.Count > 0 && copia.Personal.Count > 0){
        //if(this.Personal.Count != copia.Personal.Count)
          //return false;
        //int count = 0;
        //do{
          //if(!this.Personal[count].Equals(copia.Personal[count]))
            //return false;
          //count++;
        //}while(this.Personal.Count > count);
      //}
      //if(this.Activos.Count > 0 && copia.Activos.Count > 0){
        //if(this.Activos.Count != copia.Activos.Count)
          //return false;
        //int count = 0;
        //do{
          //if(!this.Activos[count].Equals(copia.Activos[count]))
            //return false;
          //count++;
        //}while(this.Activos.Count > count);
      //}
      //if(!this.PersonaComercial.Equals(copia.PersonaComercial))
        //return false;
      //if(!this.PersonaValida.Equals(copia.PersonaValida))
        //return false;
      //if(!this.FechaAdministrativa.Equals(copia.FechaAdministrativa))
        //return false;
      return true;
    }
  }
}
