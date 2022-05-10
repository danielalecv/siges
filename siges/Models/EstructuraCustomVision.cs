namespace siges.Models{
  public class EstructuraCustomVision{
    public int Id {get;set;}
    public float BoundingBoxHeight {get;set;}
    public float BoundingBoxLeft {get;set;}
    public float BoundingBoxTop {get;set;}
    public float BoundingBoxWidth {get;set;}
    public float Probability {get;set;}
    public string TagId {get;set;}
    public string TagName {get;set;}
  }
}
