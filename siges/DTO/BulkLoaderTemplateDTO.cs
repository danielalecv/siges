using Microsoft.AspNetCore.Http;

namespace siges.DTO{
  public class BulkLoaderTemplateDTO{
    public string Tipo {get;set;}
    public string Version {get;set;}
    public IFormFile Archivo {get;set;}
  }
}
