namespace siges.Utilities{
  public interface IEmailConfiguration{
    string Host {get; set;}
    int Port {get; set;}
    bool EnableSSL {get; set;}
    string UserName {get; set;}
    string Password {get; set;}
  }
}
