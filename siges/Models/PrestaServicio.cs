using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace siges.Models {
  public class PrestaServicio {
    public int Id {get; set;}
    public bool Estatus {get; set;}
    public OrdenPersona OrdenPersona { get; set; }
    public DateTime Hora {get; set;}
    public List<PrestaServicio.Estado> EstadoN {get; set;}
    public DateTime FechaAdministrativa {get; set;}

    public PrestaServicio(){
      this.EstadoN = new List<PrestaServicio.Estado>();
      this.Hora = new DateTime();
      this.FechaAdministrativa = new DateTime();
    }
    public class Estado{
      public int Id {get; set;}
      public string NuevoEstado {get; set;}
      public string ComentarioNuevoEstado {get; set;}
      public List<PrestaServicio.Estado.Archivo> ArchivosEvidencia {get; set;}

      public Estado(){
        ArchivosEvidencia = new List<PrestaServicio.Estado.Archivo>();
      }
      public class Archivo{
        public int Id {get; set;}
        public string Path {get; set;}
        public string Type {get; set;}
        public string Name {get; set;}
        public long Size {get; set;}
        public byte[] File {get; set;}
        public DateTime LastModified {get; set;}
      }
    }
  }
}
