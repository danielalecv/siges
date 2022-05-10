using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace siges.DTO {
    public class OperadorDTO {
        public int OrdenServicioId {get; set;}
        public int PersonaId {get; set;}
        public string NuevoEstado {get; set;}
        public string ComentarioNuevoEstado {get; set;}
        public string Hora {get; set;}
        public string Exif {get; set;}
        public List<DateTime> LastModified {get; set;}
        public List<IFormFile> ArchivosEvidencia {get; set;}
    }
}
