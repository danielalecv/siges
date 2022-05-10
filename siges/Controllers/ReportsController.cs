using System;
using System.Linq;
//using System.Threading.Tasks;
//using System.Collections;
//using System.Collections.Generic;

//using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
//using Microsoft.PowerBI.Api;
//using Microsoft.PowerBI.Api.Models;
using Microsoft.Rest;

using RestSharp;
//using RestSharp.Authenticators;

//using siges.Areas.Identity.Data;
//using siges.Repository;
//using siges.Models;

namespace siges.Controllers {
  [Authorize(Roles = "Supervisor, Administración, Operador")]
  public class ReportsController : Controller {

    // Application ID:  5d788436-8e15-4a54-91df-86fc7b719bb2
    // Application secret: /ZOMgMRljBMI9CIrN71VadcSlEm+S/5SJvbPLz7+UeQ=
      private static bool useEmbedToken = true;
      private static bool useRLS = true;

      private static string authorityUrl = "https://login.microsoftonline.com/organizations/";
      private static string resourceUrl = "https://analysis.windows.net/powerbi/api";
      private static string apiUrl = "https://api.powerbi.com/";

      private static string tenantId = "<ENTER TENANT ID>";
      private static Guid groupId = Guid.Parse("<ENTER GROUP/WORKSPACE ID>");

      private static Guid reportId = Guid.Parse("<ENTER REPORT ID>");
      private static Guid datasetId = Guid.Parse("<ENTER DATASET ID>");

      // **** Update the Client ID and Secret within Secrets.cs ****
      private static ClientCredential credential = null;
      private static AuthenticationResult authenticationResult = null;
      private static TokenCredentials tokenCredentials = null;

    public ReportsController(){ }

    /*public IActionResult RegDesemPorEmpleado() {
      //String payload = "client_id=bf9e32e9-af3d-42c9-a170-6725e81304aa&scope=https%3A//analysis.windows.net/powerbi/api/Report.Read.All&client_secret=Xgi5bN2x--3NMOrW9_.l5YiwqyF70yPl__&username=fernando.mdiaz@roatech.com.mx&password=Siatfm2002.&grant_type=password";
      String payload = "client_id=bf9e32e9-af3d-42c9-a170-6725e81304aa&scope=https%3A//analysis.windows.net/powerbi/api/Report.Read.All&client_secret=Xgi5bN2x--3NMOrW9_.l5YiwqyF70yPl__&username=fernando.mdiaz@roatech.com.mx&password=Siatfm2002.!&grant_type=password";
      var client = new RestClient("https://login.microsoftonline.com/6aae21c9-d38c-4939-9710-18f93daa29b2/oauth2/v2.0/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("application/x-www-form-urlencoded", payload, ParameterType.RequestBody);
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);
      //ViewData["token"] = jo.Property("access_token").Value.ToString();
      //ViewData["reportId"] = "43a40cf3-2c96-45a7-ac87-0497d2ab052f";
      //ViewData["reportId"] = "ba4046e6-f5d8-418b-a755-261f5c77f063"; //6116
      //ViewData["reportId"] = "fcbeceb7-a97d-4616-8436-2ae5b1dad949"; // Nuevo reporte para la 6116
      return View();
    }*/

    /*
     *BitácoraCliente6006C:  https://app.powerbi.com/reportEmbed?reportId=82bd4cfb-6cd8-4054-a484-d494b2208acc&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     *bitácoraCliente6006    https://app.powerbi.com/reportEmbed?reportId=155ef4d5-a38d-4a62-8652-866f850ce2c4&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * bitácoraCliente6226   https://app.powerbi.com/reportEmbed?reportId=dc3d81ba-b1f9-4d06-b92f-0f1ac4e15a04&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     * BitácoraServicio6226 :  https://app.powerbi.com/reportEmbed?reportId=adf643aa-d529-496c-bd2e-8f80fe9c37e0&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     * BitácoraServicio6226 28-Oct-2020  https://app.powerbi.com/reportEmbed?reportId=741e9671-da4a-41fc-b1ca-7fb8ac725195&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * bitácoraCliente6116   https://app.powerbi.com/reportEmbed?reportId=c83dd7cb-f728-4355-a58b-2acb31f5982d&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     * bitácoraCliente6116 : https://app.powerbi.com/reportEmbed?reportId=e389218e-30ea-4c4d-8cbe-292e2cb0e11f&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Nuevos Cambio de Imagen 29-Sep-2020
     * bitácoraCliente  https://app.powerbi.com/reportEmbed?reportId=6e38491e-1732-4452-8e1e-182cde0dcc2a&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Nueva instancia para pruebas Paco 25-Mayo-2021
     * bitácoraCliente https://app.powerbi.com/reportEmbed?reportId=13451d90-90ee-46c8-b9d6-47e616b5952b&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Nueva instancia para producción María 25-Mayo-20
     * bitácoraCliente https://app.powerbi.com/reportEmbed?reportId=a03d4205-c542-47be-8e3b-06c5f3a8a114&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     */
    //[Authorize(Roles = "Administración")]
    public IActionResult BitacoraDeServiciosPorCliente(){
            String payload = "client_id=bf9e32e9-af3d-42c9-a170-6725e81304aa&scope=https%3A//analysis.windows.net/powerbi/api/Report.Read.All&client_secret=Xgi5bN2x--3NMOrW9_.l5YiwqyF70yPl__&username=" + Utilities.Config.pbiUsername + "&password=" + Utilities.Config.pbiPassword + "&grant_type=password";
      var client = new RestClient("https://login.microsoftonline.com/6aae21c9-d38c-4939-9710-18f93daa29b2/oauth2/v2.0/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("application/x-www-form-urlencoded", payload, ParameterType.RequestBody);
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);
      ViewData["token"] = jo.Property("access_token").Value.ToString();
      //ViewData["reportId"] = "82bd4cfb-6cd8-4054-a484-d494b2208acc"; // Nuevo reporte para la 6006 covid19
      //ViewData["reportId"] = "155ef4d5-a38d-4a62-8652-866f850ce2c4"; // Nuevo reporte para la 6336 siges_covid19_new
      //ViewData["reportId"] = "741e9671-da4a-41fc-b1ca-7fb8ac725195"; // Nuevo reporte para la 6226 siges_covid19
      //ViewData["reportId"] = "6e38491e-1732-4452-8e1e-182cde0dcc2a"; // Nuevo reporte para la 6116 siges_azus
      //ViewData["reportId"] = "13451d90-90ee-46c8-b9d6-47e616b5952b"; // Nuevo reporte para la 6446 SSM4_test
      ViewData["reportId"] = "a03d4205-c542-47be-8e3b-06c5f3a8a114"; // Nuevo reporte para la 6556 SSM4_prod
      return View("~/Views/Reports/RegDesemPorEmpleado.cshtml");
    }

    /*
     * BitácoraServicios6006C: https://app.powerbi.com/reportEmbed?reportId=7e53b597-6266-4abf-8128-acc35f71ffdb&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     *bitácoraServicio6006    https://app.powerbi.com/reportEmbed?reportId=8dc626fa-a42e-4aaf-9706-3a6c82577302&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * bitácoraServicio6226   https://app.powerbi.com/reportEmbed?reportId=dc3d81ba-b1f9-4d06-b92f-0f1ac4e15a04&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     * bitácoraServicio6226 28-Oct-2020 https://app.powerbi.com/reportEmbed?reportId=78a32f8c-e152-4aa1-92f5-af60d12f3793&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * bitácoraServicio6116   https://app.powerbi.com/reportEmbed?reportId=c83dd7cb-f728-4355-a58b-2acb31f5982d&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Nuevos Cambio de Imagen 29-Sep-2020
     * BitácoraServicio https://app.powerbi.com/reportEmbed?reportId=96c99a50-34b4-4560-ac25-79b67ecd9cde&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * bitácoraServicio6446 25-Mayo-2021
     * https://app.powerbi.com/reportEmbed?reportId=5fa0b9f7-b7a6-4daa-a2f8-173b032a57a6&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * BitácoraServicio6556 25-Mayo-2021
     * https://app.powerbi.com/reportEmbed?reportId=f88105af-f2f9-4b3b-89cf-54943acee55f&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     */
    //[Authorize(Roles = "Supervisor")]
    public IActionResult BitacoraDeServicios(){
            String payload = "client_id=bf9e32e9-af3d-42c9-a170-6725e81304aa&scope=https%3A//analysis.windows.net/powerbi/api/Report.Read.All&client_secret=Xgi5bN2x--3NMOrW9_.l5YiwqyF70yPl__&username=" + Utilities.Config.pbiUsername + "&password=" + Utilities.Config.pbiPassword + "&grant_type=password";
      var client = new RestClient("https://login.microsoftonline.com/6aae21c9-d38c-4939-9710-18f93daa29b2/oauth2/v2.0/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("application/x-www-form-urlencoded", payload, ParameterType.RequestBody);
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);
      ViewData["token"] = jo.Property("access_token").Value.ToString();
      //ViewData["reportId"] = "7e53b597-6266-4abf-8128-acc35f71ffdb"; // Nuevo reporte para la 6006 covid19
      //ViewData["reportId"] = "8dc626fa-a42e-4aaf-9706-3a6c82577302"; // Nuevo reporte para la 6336 siges_covid19_new
      //ViewData["reportId"] = "78a32f8c-e152-4aa1-92f5-af60d12f3793"; // Nuevo reporte para la 6226 siges_covid19
      //ViewData["reportId"] = "96c99a50-34b4-4560-ac25-79b67ecd9cde"; // Nuevo reporte para la 6116 siges_azus
      //ViewData["reportId"] = "5fa0b9f7-b7a6-4daa-a2f8-173b032a57a6"; // Nuevo reporte para la 6446 SSM4_test
      ViewData["reportId"] = "f88105af-f2f9-4b3b-89cf-54943acee55f"; // Nuevo reporte para la 6556 SSM4_prod
      return View("~/Views/Reports/RegDesemPorEmpleado.cshtml");
    }

    public IActionResult ServiciosInsumos(){
      String payload = "client_id=bf9e32e9-af3d-42c9-a170-6725e81304aa&scope=https%3A//analysis.windows.net/powerbi/api/Report.Read.All&client_secret=Xgi5bN2x--3NMOrW9_.l5YiwqyF70yPl__&username=" + Utilities.Config.pbiUsername + "&password=" + Utilities.Config.pbiPassword + "&grant_type=password";
      var client = new RestClient("https://login.microsoftonline.com/6aae21c9-d38c-4939-9710-18f93daa29b2/oauth2/v2.0/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("application/x-www-form-urlencoded", payload, ParameterType.RequestBody);
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);
      ViewData["token"] = jo.Property("access_token").Value.ToString();
      ViewData["reportId"] = "5c59ce2c-15ce-4f05-aece-db72eab43aac"; // Nuevo reporte para la 6556 SSM4_prod
      return View("~/Views/Reports/RegDesemPorEmpleado.cshtml");
    }

    /*
     * CuentasPorPagar6006C: https://app.powerbi.com/reportEmbed?reportId=341807f6-c0bc-46d6-9e29-44aa799fd64e&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     *CuentasPorPagar6006   https://app.powerbi.com/reportEmbed?reportId=a820d3de-ce6d-4527-9799-986147139662&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     *CuentasPorPagar6226   https://app.powerbi.com/reportEmbed?reportId=a7899712-bab6-4a68-adf6-b30ade79a9f0&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     * CuentasPorPagar6226 28-Oct-2020 https://app.powerbi.com/reportEmbed?reportId=c03ab725-5777-42e9-9750-b32455927b80&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * CuentasPorPagar6116  https://app.powerbi.com/reportEmbed?reportId=53b21875-1134-403c-a303-de70cb66d266&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Nuevos Cambio de Imagen 29-Sep-2020
     * CuentasPorPagar  https://app.powerbi.com/reportEmbed?reportId=3b5bf85f-22c0-4c54-8900-f0758e4745d7&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * CuentasPorPagar6446 25-Mayo-2021
     * https://app.powerbi.com/reportEmbed?reportId=c11da3a6-9ccb-41e9-9b84-7fd440eb3ad2&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * CuentasPorPagar6556
     * https://app.powerbi.com/reportEmbed?reportId=759a0d45-c908-4474-9dab-b54bb56aac98&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     */
    //[Authorize(Roles = "Supervisor")]
    public IActionResult CuentasPorPagarExternos(){
            String payload = "client_id=bf9e32e9-af3d-42c9-a170-6725e81304aa&scope=https%3A//analysis.windows.net/powerbi/api/Report.Read.All&client_secret=Xgi5bN2x--3NMOrW9_.l5YiwqyF70yPl__&username=" + Utilities.Config.pbiUsername + "&password=" + Utilities.Config.pbiPassword + "&grant_type=password";
      var client = new RestClient("https://login.microsoftonline.com/6aae21c9-d38c-4939-9710-18f93daa29b2/oauth2/v2.0/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("application/x-www-form-urlencoded", payload, ParameterType.RequestBody);
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);
      ViewData["token"] = jo.Property("access_token").Value.ToString();
      //ViewData["reportId"] = "341807f6-c0bc-46d6-9e29-44aa799fd64e"; // Nuevo reporte para la 6006  covid19
      //ViewData["reportId"] = "a820d3de-ce6d-4527-9799-986147139662"; // Nuevo reporte para la 6336  siges_covid19_new
      //ViewData["reportId"] = "c03ab725-5777-42e9-9750-b32455927b80"; // Nuevo reporte para la 6226  siges_covid19
      //ViewData["reportId"] = "3b5bf85f-22c0-4c54-8900-f0758e4745d7"; // Nuevo reporte para la 6116  siges_azus
      //ViewData["reportId"] = "c11da3a6-9ccb-41e9-9b84-7fd440eb3ad2"; // Nuevo reporte para la 6446  SSM4_test
      ViewData["reportId"] = "759a0d45-c908-4474-9dab-b54bb56aac98"; // Nuevo reporte para la 6556  SSM4_prod
      return View("~/Views/Reports/RegDesemPorEmpleado.cshtml");
    }

    /*
     * desempeñoEmpleado6006C: https://app.powerbi.com/reportEmbed?reportId=cc8a2152-5af6-4503-8718-5f7e5803fe9c&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     *desempeñoEmpleado6006   https://app.powerbi.com/reportEmbed?reportId=90cbe748-4cba-4cc6-9b83-6d581c704e44&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     *desempeñoEmpleado6226   https://app.powerbi.com/reportEmbed?reportId=b44ba98a-a8b5-4723-ae5e-ebd0064a2e1d&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     * desempeñoEmpleado6226 28-Oct-2020 https://app.powerbi.com/reportEmbed?reportId=02e5423d-23e6-40ef-8922-558a6330c7cf&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * desempeñoEmpleado6116  https://app.powerbi.com/reportEmbed?reportId=748f6ea8-3f4d-4c82-8545-893e061b2a28&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Nuevos Cambio de Imagen 29-Sep-2020
     * DesempeñoEmpleado  https://app.powerbi.com/reportEmbed?reportId=bd336439-fbc2-4bae-a542-afe03b47d495&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * RegistroDesempeñoEmpleado 25-Mayo-2021
     * https://app.powerbi.com/reportEmbed?reportId=bc20d812-5ff3-492a-bc9f-7a2a4dc81782&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * desempeñoEmpleado6556
     * https://app.powerbi.com/reportEmbed?reportId=b8825cbe-ddaa-4a83-b665-bc212c860ab4&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     */
    //[Authorize(Roles = "Supervisor")]
    public IActionResult RegistroDesempenoPorEmpleado(){
            String payload = "client_id=bf9e32e9-af3d-42c9-a170-6725e81304aa&scope=https%3A//analysis.windows.net/powerbi/api/Report.Read.All&client_secret=Xgi5bN2x--3NMOrW9_.l5YiwqyF70yPl__&username=" + Utilities.Config.pbiUsername + "&password=" + Utilities.Config.pbiPassword + "&grant_type=password";
      var client = new RestClient("https://login.microsoftonline.com/6aae21c9-d38c-4939-9710-18f93daa29b2/oauth2/v2.0/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("application/x-www-form-urlencoded", payload, ParameterType.RequestBody);
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);
      ViewData["token"] = jo.Property("access_token").Value.ToString();
      //ViewData["reportId"] = "cc8a2152-5af6-4503-8718-5f7e5803fe9c"; // Nuevo reporte para la 6006  covid19
      //ViewData["reportId"] = "90cbe748-4cba-4cc6-9b83-6d581c704e44"; // Nuevo reporte para la 6336  siges_covid19_new
      //ViewData["reportId"] = "02e5423d-23e6-40ef-8922-558a6330c7cf"; // Nuevo reporte para la 6226  siges_covid19
      //ViewData["reportId"] = "bd336439-fbc2-4bae-a542-afe03b47d495"; // Nuevo reporte para la 6116  siges_azus
      //ViewData["reportId"] = "bc20d812-5ff3-492a-bc9f-7a2a4dc81782"; // Nuevo reporte para la 6446  SSM4_test
      ViewData["reportId"] = "b8825cbe-ddaa-4a83-b665-bc212c860ab4"; // Nuevo reporte para la 6556  SSM4_prod
      return View("~/Views/Reports/RegDesemPorEmpleado.cshtml");
    }

    /*
     * Semáforo6006C: https://app.powerbi.com/reportEmbed?reportId=7587822c-71ab-4d1e-af1e-f941a42b86af&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     *Semáforo6006    https://app.powerbi.com/reportEmbed?reportId=61f11d20-a4d8-4f9c-95e5-6512691e7d3c&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Semáforo6226   https://app.powerbi.com/reportEmbed?reportId=3063d043-1627-4177-b7f3-14da24da1e3e&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     * Semáforo6226 28-Oct-2020 https://app.powerbi.com/reportEmbed?reportId=34d81c90-b4df-4606-8c5c-b37de648367f&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     *Semáforo6116    https://app.powerbi.com/reportEmbed?reportId=6dfbe0ed-6b1f-4eaf-9cf8-8363751ed7ee&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Nuevos Cambio de Imagen 29-Sep-2020
     * Semáforo https://app.powerbi.com/reportEmbed?reportId=942f2609-bdde-4040-af57-4dc017d45eec&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Semaforo 25-Mayo-2021
     * https://app.powerbi.com/reportEmbed?reportId=5376f8f5-5678-4192-899d-91be5f4c1a47&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Semáforo6556 25-Mayo-2021
     * https://app.powerbi.com/reportEmbed?reportId=108764b5-57e2-48e6-bbb5-82744eb2b3ab&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     */
    //[Authorize(Roles = "Supervisor")]
    public IActionResult Semaforo(){
      String payload = "client_id=bf9e32e9-af3d-42c9-a170-6725e81304aa&scope=https%3A//analysis.windows.net/powerbi/api/Report.Read.All&client_secret=Xgi5bN2x--3NMOrW9_.l5YiwqyF70yPl__&username="+ Utilities.Config.pbiUsername + "&password="+ Utilities.Config.pbiPassword + "&grant_type=password";
      var client = new RestClient("https://login.microsoftonline.com/6aae21c9-d38c-4939-9710-18f93daa29b2/oauth2/v2.0/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("application/x-www-form-urlencoded", payload, ParameterType.RequestBody);
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);
      ViewData["token"] = jo.Property("access_token").Value.ToString();
      //ViewData["reportId"] = "7587822c-71ab-4d1e-af1e-f941a42b86af"; // Nuevo reporte para la 6006  covid19
      //ViewData["reportId"] = "61f11d20-a4d8-4f9c-95e5-6512691e7d3c"; // Nuevo reporte para la 6336  siges_covid19_new
      //ViewData["reportId"] = "34d81c90-b4df-4606-8c5c-b37de648367f"; // Nuevo reporte para la 6226  siges_covid19
      //ViewData["reportId"] = "942f2609-bdde-4040-af57-4dc017d45eec"; // Nuevo reporte para la 6116  siges_azus
      //ViewData["reportId"] = "5376f8f5-5678-4192-899d-91be5f4c1a47"; // Nuevo reporte para la 6446  SSM4_test
      ViewData["reportId"] = "108764b5-57e2-48e6-bbb5-82744eb2b3ab"; // Nuevo reporte para la 6556  SSM4_prod
      return View("~/Views/Reports/RegDesemPorEmpleado.cshtml");
    }

    /*
     * ServicioPorFacturar6006C: https://app.powerbi.com/reportEmbed?reportId=18d748bd-ff8a-44b6-9532-dbe333d55777&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     *ServicioPorFacturar6006   https://app.powerbi.com/reportEmbed?reportId=b706489f-eecf-450c-8538-5dd391beee0b&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * ServicioPorFacturar6226  https://app.powerbi.com/reportEmbed?reportId=cdc15c11-d705-4d3d-9236-fa4a433e8831&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     * ServicioPorFacturar6226 28-Oct-2020 https://app.powerbi.com/reportEmbed?reportId=227c9ae9-8278-4386-9f44-a44df456abfe&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * ServicioPorFactura6116   https://app.powerbi.com/reportEmbed?reportId=67fa7ef5-36b2-4384-abc2-f999c5353869&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Nuevos Cambio de Imagen 29-Sep-2020
     * Cuentas por cobrar  https://app.powerbi.com/reportEmbed?reportId=cbcb285b-118d-40e3-b335-b35c8ffbfe6b&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * ServicioPorFactura6446 25-Mayo-2021
     * https://app.powerbi.com/reportEmbed?reportId=3a5b2aaa-7243-4ed4-b9e8-8d465bc71464&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * ServicioPorFactura6556 25-Mayo-2021
     * https://app.powerbi.com/reportEmbed?reportId=8eaf2296-8a82-4d8f-b753-62c511b18fc9&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     */
    //[Authorize(Roles = "Supervisor")]
    public IActionResult CuentasPorCobrar(){
            String payload = "client_id=bf9e32e9-af3d-42c9-a170-6725e81304aa&scope=https%3A//analysis.windows.net/powerbi/api/Report.Read.All&client_secret=Xgi5bN2x--3NMOrW9_.l5YiwqyF70yPl__&username=" + Utilities.Config.pbiUsername + "&password=" + Utilities.Config.pbiPassword + "&grant_type=password";
      var client = new RestClient("https://login.microsoftonline.com/6aae21c9-d38c-4939-9710-18f93daa29b2/oauth2/v2.0/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("application/x-www-form-urlencoded", payload, ParameterType.RequestBody);
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);
      ViewData["token"] = jo.Property("access_token").Value.ToString();
      //ViewData["reportId"] = "18d748bd-ff8a-44b6-9532-dbe333d55777"; // Nuevo reporte para la 6006  covid19
      //ViewData["reportId"] = "b706489f-eecf-450c-8538-5dd391beee0b"; // Nuevo reporte para la 6336  siges_covid19_new
      //ViewData["reportId"] = "227c9ae9-8278-4386-9f44-a44df456abfe"; // Nuevo reporte para la 6226  siges_covid19
      //ViewData["reportId"] = "cbcb285b-118d-40e3-b335-b35c8ffbfe6b"; // Nuevo reporte para la 6116  siges_azus
      //ViewData["reportId"] = "3a5b2aaa-7243-4ed4-b9e8-8d465bc71464"; // Nuevo reporte para la 6446  SSM4_test
      ViewData["reportId"] = "8eaf2296-8a82-4d8f-b753-62c511b18fc9"; // Nuevo reporte para la 6556  SSM4_prod
      return View("~/Views/Reports/RegDesemPorEmpleado.cshtml");
    }

    /*
     * Asistencia6006C:  https://app.powerbi.com/reportEmbed?reportId=e68296b8-9b96-4819-a229-6de83153b18a&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * 6006 (siges_covid19_new): https://app.powerbi.com/reportEmbed?reportId=a2a5b3fb-96de-45a4-85b2-2df3f5235098&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * 6226  (siges_covid19): https://app.powerbi.com/reportEmbed?reportId=75e2db8b-2f0a-4b05-96c1-f1dda8e9bdc0&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     * Asistencia 6226 28-Oct-2020 https://app.powerbi.com/reportEmbed?reportId=568611cc-e83a-4ca6-9214-087181dea389&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     *REPORTE DE ASISTENCIA   https://app.powerbi.com/reportEmbed?reportId=897a9a89-b97a-474b-9662-2b7e5d3db8ae&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Nuevos Cambio de Imagen 29-Sep-2020
     * Asistencia https://app.powerbi.com/reportEmbed?reportId=388d7c8e-7345-4f05-a7e7-43970e34791f&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Asistencia6446 25_Mayo-2021
     * https://app.powerbi.com/reportEmbed?reportId=e2d21c20-c0e8-4886-9071-4261f1c38a24&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Asistencia6556 25-Mayo-2021
     * https://app.powerbi.com/groups/75f6a56b-cf01-46bb-9506-123073a315c5/reports/26497dca-4e3f-4973-a7be-ded70c9ec72c/ReportSection
     * */
    public IActionResult Asistencia(){
            String payload = "client_id=bf9e32e9-af3d-42c9-a170-6725e81304aa&scope=https%3A//analysis.windows.net/powerbi/api/Report.Read.All&client_secret=Xgi5bN2x--3NMOrW9_.l5YiwqyF70yPl__&username=" + Utilities.Config.pbiUsername + "&password=" + Utilities.Config.pbiPassword + "&grant_type=password";
      var client = new RestClient("https://login.microsoftonline.com/6aae21c9-d38c-4939-9710-18f93daa29b2/oauth2/v2.0/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("application/x-www-form-urlencoded", payload, ParameterType.RequestBody);
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);
      ViewData["token"] = jo.Property("access_token").Value.ToString();
      //ViewData["reportId"] = "e68296b8-9b96-4819-a229-6de83153b18a"; // Nuevo reporte para la 6006  covid19
      //ViewData["reportId"] = "a2a5b3fb-96de-45a4-85b2-2df3f5235098"; // Nuevo reporte para la 6336  siges_covid19_new
      //ViewData["reportId"] = "568611cc-e83a-4ca6-9214-087181dea389"; // Nuevo reporte para la 6226  siges_covid19
      //ViewData["reportId"] = "388d7c8e-7345-4f05-a7e7-43970e34791f"; // Nuevo reporte para la 6116  siges_azus
      //ViewData["reportId"] = "e2d21c20-c0e8-4886-9071-4261f1c38a24"; // Nuevo reporte para la 6446  SSM4_test
      ViewData["reportId"] = "26497dca-4e3f-4973-a7be-ded70c9ec72c"; // Nuevo reporte para la 6556  SSM4_prod
      return View("~/Views/Reports/RegDesemPorEmpleado.cshtml");
    }

    /*
     * Cumplimiento6006C: https://app.powerbi.com/reportEmbed?reportId=6968d0c9-4552-4450-b716-726789c42733&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * 6006 (siges_covid19_new):  https://app.powerbi.com/reportEmbed?reportId=b1fb9dcf-b12a-46c0-8645-f23c4e86393e&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * 6226  (siges_covid19): https://app.powerbi.com/reportEmbed?reportId=89fb682a-87a3-48b2-b249-7498f5566a32&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     * Cumplimiento 6226 28-Oct-2020 https://app.powerbi.com/reportEmbed?reportId=0dcbd309-2681-4778-a4b4-77afaa0b5e48&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     *REPORTE DE CUMPLIMIENTO   https://app.powerbi.com/reportEmbed?reportId=58a6405b-d5a6-4715-8e1b-8e4d51c27cb4&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * Nuevos Cambio de Imagen 29-Sep-2020
     * Cumplimiento  https://app.powerbi.com/reportEmbed?reportId=c5d9e0b7-bd8f-4157-8607-ab48cbe8f75f&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * 6446 Cumplimiento 25-Mayo-2021
     * https://app.powerbi.com/reportEmbed?reportId=c16abe08-fbba-4bd1-abfb-7577228a524a&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * 6556 Cumplimiento 25-Mayo-2021
     * https://app.powerbi.com/reportEmbed?reportId=12c5610b-ebee-4817-9f65-01bab6194247&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * */
    public IActionResult Cumplimiento(){
            String payload = "client_id=bf9e32e9-af3d-42c9-a170-6725e81304aa&scope=https%3A//analysis.windows.net/powerbi/api/Report.Read.All&client_secret=Xgi5bN2x--3NMOrW9_.l5YiwqyF70yPl__&username=" + Utilities.Config.pbiUsername + "&password=" + Utilities.Config.pbiPassword + "&grant_type=password";
      var client = new RestClient("https://login.microsoftonline.com/6aae21c9-d38c-4939-9710-18f93daa29b2/oauth2/v2.0/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("application/x-www-form-urlencoded", payload, ParameterType.RequestBody);
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);
      ViewData["token"] = jo.Property("access_token").Value.ToString();
      //ViewData["reportId"] = "6968d0c9-4552-4450-b716-726789c42733"; // Nuevo reporte para la 6006  covid19
      //ViewData["reportId"] = "b1fb9dcf-b12a-46c0-8645-f23c4e86393e"; // Nuevo reporte para la 6336  siges_covid19_new
      //ViewData["reportId"] = "0dcbd309-2681-4778-a4b4-77afaa0b5e48"; // Nuevo reporte para la 6226  siges_covid19
      //ViewData["reportId"] = "c5d9e0b7-bd8f-4157-8607-ab48cbe8f75f"; // Nuevo reporte para la 6116  siges_azus
      //ViewData["reportId"] = "c16abe08-fbba-4bd1-abfb-7577228a524a"; // Nuevo reporte para la 6446  SSM4_test
      ViewData["reportId"] = "12c5610b-ebee-4817-9f65-01bab6194247"; // Nuevo reporte para la 6556  SSM4_prod
      return View("~/Views/Reports/RegDesemPorEmpleado.cshtml");
    }

    /*
     * Marketing    https://app.powerbi.com/reportEmbed?reportId=efce103d-669b-41f8-90d7-55734d952eba&groupId=c1400d06-96d0-47bd-831e-b7d287f65069&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * https://app.powerbi.com/reportEmbed?reportId=164b163a-8b7a-4b44-a06a-904fca16314f&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     *
     * 
    public IActionResult Marketing(){
      String payload = "client_id=bf9e32e9-af3d-42c9-a170-6725e81304aa&scope=https%3A//analysis.windows.net/powerbi/api/Report.Read.All&client_secret=Xgi5bN2x--3NMOrW9_.l5YiwqyF70yPl__&username=fernando.mdiaz@roatech.com.mx&password=Siatfm2002.!&grant_type=password";
      var client = new RestClient("https://login.microsoftonline.com/6aae21c9-d38c-4939-9710-18f93daa29b2/oauth2/v2.0/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("application/x-www-form-urlencoded", payload, ParameterType.RequestBody);
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);
      ViewData["token"] = jo.Property("access_token").Value.ToString();
      //ViewData["reportId"] = ""; // Nuevo reporte para la 6006
      //ViewData["reportId"] = ""; // Nuevo reporte para la 6226
      ViewData["reportId"] = "164b163a-8b7a-4b44-a06a-904fca16314f"; // Nuevo reporte para la 6116
      return View("~/Views/Reports/RegDesemPorEmpleado.cshtml");
    }

     *https://app.powerbi.com/reportEmbed?reportId=dc0e47bc-4d9e-469b-a4fd-2b3c1904737e&groupId=c1400d06-96d0-47bd-831e-b7d287f65069&autoAuth=true&ctid=6aae21c9-d38c-4939-9710-18f93daa29b2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXBhYXMtMS1zY3VzLXJlZGlyZWN0LmFuYWx5c2lzLndpbmRvd3MubmV0LyJ9
     Este es el reporte publicado
     mediante acceso de la cuenta de Fer,
     los roles son:
     Sólo cree el de AKIN y NATURGY, los otros 2 son por "default"
     o predeterminados...
     *
     * */
    /*public IActionResult PowerBiTests(){
      //Display name : siges
      //Application (client) ID : bf9e32e9-af3d-42c9-a170-6725e81304aa
      //Directory (tenant) ID : 6aae21c9-d38c-4939-9710-18f93daa29b2
      //Object ID : 0c8c1d03-bdbb-4164-8e66-1a1afcd326c0
      //Client Secrets:  SigesPowerBi_RLS 12/31/2299 8n4K_Sd0e0v9QvXZhR2-Fov~rl1wgu-FQX

      //string payload = "[ { \"key\": \"client_id\", \"value\": \"bf9e32e9-af3d-42c9-a170-6725e81304aa\" }, { \"key\": \"username\", \"value\": \"fernando.mdiaz@roatech.com.mx\", \"description\": \"The upn of the user that wants to log in. \" }, { \"key\": \"password\", \"value\": \"Siatfm2002.!\", \"description\": \"The user's password.  Delete this as soon as the response is recieved. \" }, { \"key\": \"grant_type\", \"value\": \"password\" }, { \"key\": \"scope\", \"value\": \"openid\", \"type\": \"text\" }, { \"key\": \"resource\", \"value\": \"https://analysis.windows.net/powerbi/api\", \"type\": \"text\" } ]";
      var client = new RestClient("https://login.microsoftonline.com/common/oauth2/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      //request.AddParameter("client_id", "bf9e32e9-af3d-42c9-a170-6725e81304aa");
      request.AddParameter("client_id", "bf9e32e9-af3d-42c9-a170-6725e81304aa");
      request.AddParameter("client_secret", "8n4K_Sd0e0v9QvXZhR2-Fov~rl1wgu-FQX");
      request.AddParameter("username", "fernando.mdiaz@roatech.com.mx");
      request.AddParameter("password", "Siatfm2002.!");
      request.AddParameter("grant_type", "password");
      request.AddParameter("scope", "openid");
      request.AddParameter("resource", "https://analysis.windows.net/powerbi/api");
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);

      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("ContentType {0}\nContent {1}\nResponse {2}\n",response.ContentType, response.Content, response.ToString());
      Console.WriteLine("Token: {0}\n",jo.Property("access_token").Value.ToString());
      //Console.WriteLine(response.Content);

      // Lista los Workspaces 
      var client2 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups");
      client2.Timeout = -1;
      var request2 = new RestRequest(Method.GET);
      request2.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response2 = client2.Execute(request2);
      JObject joWorkspaces = JObject.Parse(response2.Content);
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.WriteLine("Workspaces: {0}\n", response2.Content);

      // Lista los Reports de un Workspace
      var client3 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/c1400d06-96d0-47bd-831e-b7d287f65069/reports");
      client3.Timeout = -1;
      var request3 = new RestRequest(Method.GET);
      request3.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response3 = client3.Execute(request3);
      JObject joReport = JObject.Parse(response3.Content);
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.WriteLine("Reportes de un workspace: {0} \t {1}\n", response3.Content, joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("embedUrl").Value);

      // Lista los Datasets de un Grupo
      var client6 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/c1400d06-96d0-47bd-831e-b7d287f65069/datasets");
      client6.Timeout = -1;
      var request6 = new RestRequest(Method.GET);
      request6.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response6 = client6.Execute(request6);
      Console.ForegroundColor = ConsoleColor.DarkBlue;
      Console.WriteLine("Datasets de un grupo: {0}\n", response6.Content);
      
      // Lista los Dashboards
      var client7 = new RestClient("https://api.powerbi.com/v1.0/myorg/apps/bf9e32e9-af3d-42c9-a170-6725e81304aa/dashboards");
      client7.Timeout = -1;
      var request7 = new RestRequest(Method.GET);
      request7.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response7 = client7.Execute(request7);
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("Dashboards: {0}\n",response7.Content);
      
      // Obtener el Embed Token
      var client4 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/c1400d06-96d0-47bd-831e-b7d287f65069/reports/fda924ca-3412-4a27-9083-3f20e0c6c0e9/generatetoken");
      client4.Timeout = -1;
      var request4 = new RestRequest(Method.POST);
      request4.AddHeader("Content-Type", "application/json");
      request4.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      request4.AddParameter("application/json", "{\n\t\"accessLevel\":\"View\",\n\t\"allowSaveAs\":\"false\"\n}",  ParameterType.RequestBody);
      IRestResponse response4 = client4.Execute(request4);
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Token sin identity: {0}\n", response4.Content);

      // Obtener el Embed Token con identities
      var client5 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/c1400d06-96d0-47bd-831e-b7d287f65069/reports/6c6d5487-dd83-4ae3-846f-93ec2dea7edb/generatetoken");//dc0e47bc-4d9e-469b-a4fd-2b3c1904737e
      client5.Timeout = -1;
      var request5 = new RestRequest(Method.POST);
      request5.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      request5.AddHeader("Content-Type", "application/json");
      //request5.AddParameter("application/json", "{\r\n\t\"accessLevel\": \"View\",\r\n\t\"allowSaveAs\": \"false\",\r\n\t\"identities\": [{\r\n\t\t\"username\": \"contacto@roatech.com.mx\",\r\n\t\t\"roles\": [\"invenergy\"],\r\n\t\t\"datasets\": [\"b2391da7-11a6-473f-bf98-eef1d23e8d4d\"]\r\n\t}]\r\n}",  ParameterType.RequestBody);
      request5.AddParameter("application/json", "{\r\n\t\"accessLevel\": \"View\",\r\n\t\"allowSaveAs\": \"false\",\r\n\t\"identities\": [{\r\n\t\t\"username\": \"contacto@roatech.com.mx\",\r\n\t\t\"roles\": [\"naturgy\"],\r\n\t\t\"datasets\": [\"bf70a402-d96d-4384-b213-01b6e668f110\"]\r\n\t}]\r\n}",  ParameterType.RequestBody);
      //request5.AddParameter("application/json", "{\r\n\t\"accessLevel\": \"View\",\r\n\t\"allowSaveAs\": \"false\",\r\n\t\"identities\": [{\r\n\t\t\"username\": \"contacto@roatech.com.mx\",\r\n\t\t\"roles\": [\"shellMex\"],\r\n\t\t\"datasets\": [\"b2391da7-11a6-473f-bf98-eef1d23e8d4d\"]\r\n\t}]\r\n}",  ParameterType.RequestBody);
      //request5.AddParameter("application/json", "{\r\n\t\"accessLevel\": \"View\",\r\n\t\"allowSaveAs\": \"false\",\r\n\t\"identities\": [{\r\n\t\t\"username\": \"contacto@roatech.com.mx\",\r\n\t\t\"roles\": [\"trafigura\"],\r\n\t\t\"datasets\": [\"b2391da7-11a6-473f-bf98-eef1d23e8d4d\"]\r\n\t}]\r\n}",  ParameterType.RequestBody);
      //request5.AddParameter("application/json", "{\r\n\t\"accessLevel\": \"View\",\r\n\t\"allowSaveAs\": \"false\",\r\n\t\"identities\": [{\r\n\t\t\"username\": \"contacto@roatech.com.mx\",\r\n\t\t\"roles\": [\"invenergy\",\"naturgy\",\"shellMex\",\"trafigura\"],\r\n\t\t\"datasets\": [\"b2391da7-11a6-473f-bf98-eef1d23e8d4d\"]\r\n\t}]\r\n}",  ParameterType.RequestBody);
      IRestResponse response5 = client5.Execute(request5);
      JObject joTokenIdentity = JObject.Parse(response.Content);
      Console.ForegroundColor = ConsoleColor.DarkYellow;
      Console.WriteLine("Token con identities {0}\n", joTokenIdentity.Property("access_token"));
      Console.WriteLine("Token con identities {0}\n", response.Content);

      ViewData["token"] = joTokenIdentity.Property("access_token").Value.ToString();
      ViewData["reportId"] = joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("id").Value.ToString();
      ViewData["embedUrl"] = joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("embedUrl").Value.ToString();
      return View("~/Views/Reports/PowerBi.cshtml");
    }*/

  /*
    public IActionResult Prueba060820Naturgy(){
      var client = new RestClient("https://login.microsoftonline.com/common/oauth2/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("client_id", "bf9e32e9-af3d-42c9-a170-6725e81304aa");
      request.AddParameter("client_secret", "8n4K_Sd0e0v9QvXZhR2-Fov~rl1wgu-FQX");
      request.AddParameter("username", "fernando.mdiaz@roatech.com.mx");
      request.AddParameter("password", "Siatfm2002.!");
      request.AddParameter("grant_type", "password");
      request.AddParameter("scope", "openid");
      request.AddParameter("resource", "https://analysis.windows.net/powerbi/api");
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);

      // Lista los Workspaces 
      var client2 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups");
      client2.Timeout = -1;
      var request2 = new RestRequest(Method.GET);
      request2.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response2 = client2.Execute(request2);
      JObject joWorkspaces = JObject.Parse(response2.Content);
      // Lista los Reports de un Workspace
      var client3 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/" + joWorkspaces.Property("value").Value.ElementAt(9).ToObject<JObject>().Property("id").Value.ToString() + "/reports");
      client3.Timeout = -1;
      var request3 = new RestRequest(Method.GET);
      request3.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response3 = client3.Execute(request3);
      JObject joReport = JObject.Parse(response3.Content);

      // Obtener el Embed Token con identities
      var client5 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/c1400d06-96d0-47bd-831e-b7d287f65069/reports/dc0e47bc-4d9e-469b-a4fd-2b3c1904737e/generatetoken");
      client5.Timeout = -1;
      var request5 = new RestRequest(Method.POST);
      request5.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      request5.AddHeader("Content-Type", "application/json");
      request5.AddParameter("application/json", "{\r\n\t\"accessLevel\": \"View\",\r\n\t\"allowSaveAs\": \"false\",\r\n\t\"identities\": [{\r\n\t\t\"username\": \"erick.agutierrez@roatech.com.mx\",\r\n\t\t\"roles\": [\"FUERZA Y ENERGIA\"],\r\n\t\t\"datasets\": [\"bf70a402-d96d-4384-b213-01b6e668f110\"]\r\n\t}]\r\n}",  ParameterType.RequestBody);
      IRestResponse response5 = client5.Execute(request5);
      JObject joTokenIdentity = JObject.Parse(response.Content);

      ViewData["token"] = joTokenIdentity.Property("access_token").Value.ToString();
      ViewData["reportId"] = joReport.Property("value").Value.ElementAt(1).ToObject<JObject>().Property("id").Value.ToString();
      ViewData["embedUrl"] = joReport.Property("value").Value.ElementAt(1).ToObject<JObject>().Property("embedUrl").Value.ToString();
      return View("~/Views/Reports/PowerBi.cshtml");
    }
    */

    /*
    public IActionResult Prueba060820Akin(){
      var client = new RestClient("https://login.microsoftonline.com/common/oauth2/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("client_id", "bf9e32e9-af3d-42c9-a170-6725e81304aa");
      request.AddParameter("client_secret", "8n4K_Sd0e0v9QvXZhR2-Fov~rl1wgu-FQX");
      request.AddParameter("username", "fernando.mdiaz@roatech.com.mx");
      request.AddParameter("password", "Siatfm2002.!");
      request.AddParameter("grant_type", "password");
      request.AddParameter("scope", "openid");
      request.AddParameter("resource", "https://analysis.windows.net/powerbi/api");
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);
      // Lista los Workspaces 
      var client2 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups");
      client2.Timeout = -1;
      var request2 = new RestRequest(Method.GET);
      request2.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response2 = client2.Execute(request2);
      JObject joWorkspaces = JObject.Parse(response2.Content);

      // Lista los Reports de un Workspace
      var client3 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/" + joWorkspaces.Property("value").Value.ElementAt(9).ToObject<JObject>().Property("id").Value.ToString() + "/reports");
      client3.Timeout = -1;
      var request3 = new RestRequest(Method.GET);
      request3.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response3 = client3.Execute(request3);
      JObject joReport = JObject.Parse(response3.Content);

      // Obtener el Embed Token con identities
      var client5 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/" + joWorkspaces.Property("value").Value.ElementAt(9).ToObject<JObject>().Property("id").Value.ToString() + "/reports/" + joReport.Property("value").Value.ElementAt(1).ToObject<JObject>().Property("id").Value.ToString() + "/generatetoken");
      client5.Timeout = -1;
      var request5 = new RestRequest(Method.POST);
      request5.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      request5.AddHeader("Content-Type", "application/json");
      request5.AddParameter("application/json", "{\r\n\t\"accessLevel\": \"View\",\r\n\t\"allowSaveAs\": \"false\",\r\n\t\"identities\": [{\r\n\t\t\"username\": \"contacto@roatech.com.mx\",\r\n\t\t\"roles\": [\"AKIN\"],\r\n\t\t\"datasets\": [\"bf70a402-d96d-4384-b213-01b6e668f110\"]\r\n\t}]\r\n}",  ParameterType.RequestBody);
      IRestResponse response5 = client5.Execute(request5);
      JObject joTokenIdentity = JObject.Parse(response.Content);

      ViewData["token"] = joTokenIdentity.Property("access_token").Value.ToString();
      ViewData["reportId"] = joReport.Property("value").Value.ElementAt(1).ToObject<JObject>().Property("id").Value.ToString();
      ViewData["embedUrl"] = joReport.Property("value").Value.ElementAt(1).ToObject<JObject>().Property("embedUrl").Value.ToString();
      return View("~/Views/Reports/PowerBi.cshtml");
    }
    */

    /*
    public IActionResult PowerBiTestsShellMex(){
      var client = new RestClient("https://login.microsoftonline.com/common/oauth2/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("client_id", "bf9e32e9-af3d-42c9-a170-6725e81304aa");
      request.AddParameter("client_secret", "8n4K_Sd0e0v9QvXZhR2-Fov~rl1wgu-FQX");
      request.AddParameter("username", "fernando.mdiaz@roatech.com.mx");
      request.AddParameter("password", "Siatfm2002.!");
      request.AddParameter("grant_type", "password");
      request.AddParameter("scope", "openid");
      request.AddParameter("resource", "https://analysis.windows.net/powerbi/api");
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);

      // Lista los Workspaces 
      var client2 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups");
      client2.Timeout = -1;
      var request2 = new RestRequest(Method.GET);
      request2.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response2 = client2.Execute(request2);
      JObject joWorkspaces = JObject.Parse(response2.Content);
      // Lista los Reports de un Workspace
      var client3 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/" + joWorkspaces.Property("value").Value.ElementAt(9).ToObject<JObject>().Property("id").Value.ToString() + "/reports");
      client3.Timeout = -1;
      var request3 = new RestRequest(Method.GET);
      request3.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response3 = client3.Execute(request3);
      JObject joReport = JObject.Parse(response3.Content);

      // Obtener el Embed Token con identities
      var client5 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/" + joWorkspaces.Property("value").Value.ElementAt(9).ToObject<JObject>().Property("id").Value.ToString() + "/reports/" + joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("id").Value.ToString() + "/generatetoken");
      client5.Timeout = -1;
      var request5 = new RestRequest(Method.POST);
      request5.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      request5.AddHeader("Content-Type", "application/json");
      request5.AddParameter("application/json", "{\r\n\t\"accessLevel\": \"View\",\r\n\t\"allowSaveAs\": \"false\",\r\n\t\"identities\": [{\r\n\t\t\"username\": \"contacto@roatech.com.mx\",\r\n\t\t\"roles\": [\"shellMex\"],\r\n\t\t\"datasets\": [\"b2391da7-11a6-473f-bf98-eef1d23e8d4d\"]\r\n\t}]\r\n}",  ParameterType.RequestBody);
      IRestResponse response5 = client5.Execute(request5);
      JObject joTokenIdentity = JObject.Parse(response.Content);

      ViewData["token"] = joTokenIdentity.Property("access_token").Value.ToString();
      ViewData["reportId"] = joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("id").Value.ToString();
      ViewData["embedUrl"] = joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("embedUrl").Value.ToString();
      return View("~/Views/Reports/PowerBi.cshtml");
    }
    */

    /*
    public IActionResult PowerBiTestsNaturgy(){
      var client = new RestClient("https://login.microsoftonline.com/common/oauth2/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("client_id", "bf9e32e9-af3d-42c9-a170-6725e81304aa");
      request.AddParameter("client_secret", "8n4K_Sd0e0v9QvXZhR2-Fov~rl1wgu-FQX");
      request.AddParameter("username", "fernando.mdiaz@roatech.com.mx");
      request.AddParameter("password", "Siatfm2002.!");
      request.AddParameter("grant_type", "password");
      request.AddParameter("scope", "openid");
      request.AddParameter("resource", "https://analysis.windows.net/powerbi/api");
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);

      // Lista los Workspaces 
      var client2 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups");
      client2.Timeout = -1;
      var request2 = new RestRequest(Method.GET);
      request2.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response2 = client2.Execute(request2);
      JObject joWorkspaces = JObject.Parse(response2.Content);

      // Lista los Reports de un Workspace
      var client3 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/" + joWorkspaces.Property("value").Value.ElementAt(9).ToObject<JObject>().Property("id").Value.ToString() + "/reports");
      client3.Timeout = -1;
      var request3 = new RestRequest(Method.GET);
      request3.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response3 = client3.Execute(request3);
      JObject joReport = JObject.Parse(response3.Content);

      // Obtener el Embed Token con identities
      var client5 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/" + joWorkspaces.Property("value").Value.ElementAt(9).ToObject<JObject>().Property("id").Value.ToString() + "/reports/" + joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("id").Value.ToString() + "/generatetoken");
      client5.Timeout = -1;
      var request5 = new RestRequest(Method.POST);
      request5.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      request5.AddHeader("Content-Type", "application/json");
      request5.AddParameter("application/json", "{\r\n\t\"accessLevel\": \"View\",\r\n\t\"allowSaveAs\": \"false\",\r\n\t\"identities\": [{\r\n\t\t\"username\": \"contacto@roatech.com.mx\",\r\n\t\t\"roles\": [\"naturgy\"],\r\n\t\t\"datasets\": [\"b2391da7-11a6-473f-bf98-eef1d23e8d4d\"]\r\n\t}]\r\n}",  ParameterType.RequestBody);
      IRestResponse response5 = client5.Execute(request5);
      JObject joTokenIdentity = JObject.Parse(response.Content);

      Console.ForegroundColor = ConsoleColor.DarkCyan;
      Console.WriteLine("Embed token: {0}\n", joTokenIdentity.Property("access_token").Value.ToString());
      Console.ForegroundColor = ConsoleColor.DarkRed;
      Console.WriteLine("Embed url: {0}\n", joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("embedUrl").Value.ToString());
      Console.ForegroundColor = ConsoleColor.DarkBlue;
      Console.WriteLine("Report id: {0}\n", joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("id").Value.ToString());
      ViewData["token"] = joTokenIdentity.Property("access_token").Value.ToString();
      ViewData["reportId"] = joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("id").Value.ToString();
      ViewData["embedUrl"] = joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("embedUrl").Value.ToString();
      return View("~/Views/Reports/PowerBi.cshtml");
    }
    */

    /*
    public IActionResult PowerBiTestsTrafigura(){
      var client = new RestClient("https://login.microsoftonline.com/common/oauth2/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("client_id", "bf9e32e9-af3d-42c9-a170-6725e81304aa");
      request.AddParameter("client_secret", "8n4K_Sd0e0v9QvXZhR2-Fov~rl1wgu-FQX");
      request.AddParameter("username", "fernando.mdiaz@roatech.com.mx");
      request.AddParameter("password", "Siatfm2002.!");
      request.AddParameter("grant_type", "password");
      request.AddParameter("scope", "openid");
      request.AddParameter("resource", "https://analysis.windows.net/powerbi/api");
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);

      // Lista los Workspaces 
      var client2 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups");
      client2.Timeout = -1;
      var request2 = new RestRequest(Method.GET);
      request2.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response2 = client2.Execute(request2);
      JObject joWorkspaces = JObject.Parse(response2.Content);

      // Lista los Reports de un Workspace
      var client3 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/" + joWorkspaces.Property("value").Value.ElementAt(9).ToObject<JObject>().Property("id").Value.ToString() + "/reports");
      client3.Timeout = -1;
      var request3 = new RestRequest(Method.GET);
      request3.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response3 = client3.Execute(request3);
      JObject joReport = JObject.Parse(response3.Content);

      // Obtener el Embed Token con identities
      var client5 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/" + joWorkspaces.Property("value").Value.ElementAt(9).ToObject<JObject>().Property("id").Value.ToString() + "/reports/" + joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("id").Value.ToString() + "/generatetoken");
      client5.Timeout = -1;
      var request5 = new RestRequest(Method.POST);
      request5.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      request5.AddHeader("Content-Type", "application/json");
      request5.AddParameter("application/json", "{\r\n\t\"accessLevel\": \"View\",\r\n\t\"allowSaveAs\": \"false\",\r\n\t\"identities\": [{\r\n\t\t\"username\": \"contacto@roatech.com.mx\",\r\n\t\t\"roles\": [\"trafigura\"],\r\n\t\t\"datasets\": [\"b2391da7-11a6-473f-bf98-eef1d23e8d4d\"]\r\n\t}]\r\n}",  ParameterType.RequestBody);
      IRestResponse response5 = client5.Execute(request5);
      JObject joTokenIdentity = JObject.Parse(response.Content);

      ViewData["token"] = joTokenIdentity.Property("access_token").Value.ToString();
      ViewData["reportId"] = joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("id").Value.ToString();
      ViewData["embedUrl"] = joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("embedUrl").Value.ToString();
      return View("~/Views/Reports/PowerBi.cshtml");
    }
    */

/*
    public IActionResult PowerBiTestsAll(){
      var client = new RestClient("https://login.microsoftonline.com/common/oauth2/token");
      client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var request = new RestRequest(Method.POST);
      request.AddHeader("content-type", "application/x-www-form-urlencoded");
      request.AddParameter("client_id", "bf9e32e9-af3d-42c9-a170-6725e81304aa");
      request.AddParameter("client_secret", "8n4K_Sd0e0v9QvXZhR2-Fov~rl1wgu-FQX");
      request.AddParameter("username", "fernando.mdiaz@roatech.com.mx");
      request.AddParameter("password", "Siatfm2002.!");
      request.AddParameter("grant_type", "password");
      request.AddParameter("scope", "openid");
      request.AddParameter("resource", "https://analysis.windows.net/powerbi/api");
      var response = client.Execute(request);
      JObject jo = JObject.Parse(response.Content);

      // Lista los Workspaces 
      var client2 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups");
      client2.Timeout = -1;
      var request2 = new RestRequest(Method.GET);
      request2.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response2 = client2.Execute(request2);
      JObject joWorkspaces = JObject.Parse(response2.Content);

      // Lista los Reports de un Workspace
      var client3 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/" + joWorkspaces.Property("value").Value.ElementAt(9).ToObject<JObject>().Property("id").Value.ToString() + "/reports");
      client3.Timeout = -1;
      var request3 = new RestRequest(Method.GET);
      request3.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      IRestResponse response3 = client3.Execute(request3);
      JObject joReport = JObject.Parse(response3.Content);

      // Obtener el Embed Token con identities
      var client5 = new RestClient("https://api.powerbi.com/v1.0/myorg/groups/" + joWorkspaces.Property("value").Value.ElementAt(9).ToObject<JObject>().Property("id").Value.ToString() + "/reports/" + joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("id").Value.ToString() +"/generatetoken");
      client5.Timeout = -1;
      var request5 = new RestRequest(Method.POST);
      request5.AddHeader("Authorization", "Bearer " + jo.Property("access_token").Value.ToString());
      request5.AddHeader("Content-Type", "application/json");
      request5.AddParameter("application/json", "{\r\n\t\"accessLevel\": \"View\",\r\n\t\"allowSaveAs\": \"false\",\r\n\t\"identities\": [{\r\n\t\t\"username\": \"contacto@roatech.com.mx\",\r\n\t\t\"roles\": [\"invenergy\",\"naturgy\",\"shellMex\",\"trafigura\"],\r\n\t\t\"datasets\": [\"b2391da7-11a6-473f-bf98-eef1d23e8d4d\"]\r\n\t}]\r\n}",  ParameterType.RequestBody);
      IRestResponse response5 = client5.Execute(request5);
      JObject joTokenIdentity = JObject.Parse(response.Content);

      ViewData["token"] = joTokenIdentity.Property("access_token").Value.ToString();
      ViewData["reportId"] = joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("id").Value.ToString();
      ViewData["embedUrl"] = joReport.Property("value").Value.ElementAt(4).ToObject<JObject>().Property("embedUrl").Value.ToString();
      return View("~/Views/Reports/PowerBi.cshtml");
    }
    */
  }
}
