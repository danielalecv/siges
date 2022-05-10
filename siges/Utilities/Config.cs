using System;
namespace siges.Utilities {
  public abstract class Config {
    private Config(){
      //Private Constructor will prevent the instantiation of this class directly
      throw new Exception("Config class");
    }
    // Set this to true, to show debug statements in console
    public static bool DEBUG = true;
    //	Two possible Authentication methods: 
    //	- For authentication with master user credential choose MasterUser as AuthenticationType.
    //	- For authentication with app secret choose ServicePrincipal as AuthenticationType.
    //	More details here: https://aka.ms/EmbedServicePrincipal
    public static string authenticationType = "MasterUser";
    //	Common configuration properties for both authentication types
    // Enter workspaceId / groupId
    public static string groupId = "8fdc84b1-ae11-4fdc-bc7d-a2bf65e539ca";
    // The id of the report to embed.
    public static string reportId = "43a40cf3-2c96-45a7-ac87-0497d2ab052f";
    // Enter Application Id / Client Id
    public static string clientId = "bf9e32e9-af3d-42c9-a170-6725e81304aa";
    // Enter MasterUser credentials
    public static string pbiUsername = "fernando.mdiaz@roatech.com.mx";
    public static string pbiPassword = "Siatfm2020.!";
    // Enter ServicePrincipal credentials
    public static string tenantId = "6aae21c9-d38c-4939-9710-18f93daa29b2";
    public static string appSecret = "";
    //	DO NOT CHANGE
    public static string authorityUrl = "https://login.microsoftonline.com/";
    public static string scopeUrl = "https://analysis.windows.net/powerbi/api/.default";
  }
}
