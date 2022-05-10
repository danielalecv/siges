namespace siges.DTO {
  public class SettingsDTO {
    public SettingsDTO(){ }
    public string FolioPrefix {get; set;}
    public string EmailHost {get; set;}
    public string EmailPort {get; set;}
    public string EmailUser {get; set;}
    public string EmailPass {get; set;}
    public bool EmailEnableSSL {get; set;}
    public int MaxCaractersFields {get; set;}
  }
}
