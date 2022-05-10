using Microsoft.AspNetCore.Http;

namespace  siges.DTO{
  public class TrainModelDTO{
    public int PersonaId {get;set;}
    public IFormFile Photo {get; set;}
    public string FaceId {get; set;}
    public string Nombre {get; set;}
  }
}
