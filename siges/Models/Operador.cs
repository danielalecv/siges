using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace siges.Models {
    public class Operador {
        public int Id {get; set;}
        public bool Estatus {get; set;}
        public OrdenServicio OrdenServicio {get; set;}
        public Persona Persona {get; set;}
        public DateTime Hora {get; set;}
        public List<Operador.Estado> EstadoN {get; set;}
        public DateTime FechaAdministrativa {get; set;}

        public Operador(){
            this.EstadoN = new List<Operador.Estado>();
            this.Hora = new DateTime();
            this.FechaAdministrativa = new DateTime();
        }
        public class Estado{
            public int Id {get; set;}
            public string NuevoEstado {get; set;}
            public string ComentarioNuevoEstado {get; set;}
            public List<Operador.Estado.Archivo> ArchivosEvidencia {get; set;}

            public Estado(){
              ArchivosEvidencia = new List<Operador.Estado.Archivo>();
            }
            public class Archivo{
                public int Id {get; set;}
                public string Path {get; set;}
                public string Type {get; set;}
                public string Name {get; set;}
                public string Exif {get; set;}
                public string ExifBi {get; set;}
                public long Size {get; set;}
                public byte[] File {get; set;}
                public DateTime LastModified {get; set;}
                public EstructuraExifBi EstructuraExif {get; set;}
                public string FaceApiFinalResponse {get; set;}
                public string CustomVisionResult {get;set;}
                public List<EstructuraCustomVision> EstructuraCustomVisionResult {get;set;}

                public Archivo(){
                  EstructuraExif = new EstructuraExifBi();
                  EstructuraCustomVisionResult = new List<EstructuraCustomVision>();
                }
            }
        }
    }
}
