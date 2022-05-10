using System;

using siges.Areas.Identity.Data;

namespace siges.Models {
  public class BulkUploadTemplate{
    public int Id {get;set;}
    public string Tipo {get;set;}
    public string Version {get;set;}
    public byte[] Archivo {get;set;}
    public long TamanoArchivo {get;set;}
    public DateTime FechaAdministrativa {get;set;}
    public RoatechIdentityUser CreadoPor {get;set;}

    public BulkUploadTemplate(){
      CreadoPor = new RoatechIdentityUser();
    }
  }
}
