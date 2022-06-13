#pragma checksum "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2b1a84ca9fc9ba84f5a5c8ac785349b5172ae4d1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_SemaphoreParams), @"mvc.1.0.view", @"/Views/Admin/SemaphoreParams.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\_ViewImports.cshtml"
using siges;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\_ViewImports.cshtml"
using siges.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\_ViewImports.cshtml"
using siges.Areas.Identity.Data;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2b1a84ca9fc9ba84f5a5c8ac785349b5172ae4d1", @"/Views/Admin/SemaphoreParams.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f34ac2a518186cd6e1fad2ab593126982fe1167", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Admin_SemaphoreParams : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<siges.Models.SemaphoreParams>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
  
    ViewData["Title"] = "Parámetros Semáforo";
 

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 7 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container"">
    <div class=""row"">
        <div class=""col"">
            <h2>Configuración Par&aacute;metros Sem&aacute;foro</h2>
        </div>
    </div>
    <div class=""row margTop40"">
        <div class=""col"">
            <table id=""tableLlegada"" data-toggle=""table"" data-search=""false"" data-show-columns=""false"" data-pagination=""false"" data-locale=""es-MX"" data-classes=""table table-hover"" data-page-size=""5"" data-show-footer=""true"">
                <thead>
                    <tr>
                        <th data-visible=""false""></th>
                        <th>Hora de Llegada</th>
                        <th>Minutos</th>
                        <th></th>
                        <th class=""mobileHidden"">Acciones</th>
                </thead>
                <tbody>
");
#nullable restore
#line 26 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
                     foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                        <td>llegadaVerde</td>\r\n                        <td>Tolerancia Verde</td>\r\n                        <td>");
#nullable restore
#line 30 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
                       Write(Html.DisplayFor(modelItem => item.LlegadaVerde));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" min</td>
                        <td><i class=""fas fa-circle"" style=""color: green;""></i></td>
                        <td><a class=""btn btn-success float_Table"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Editar"" data-original-title=""Tooltip on bottom""><i class=""fas fa-edit""></i></a></td>
                        </tr>
                        <tr>
                        <td>llegadaAmarillo</td>
                        <td>Tolerancia Amarillo</td>
                        <td>");
#nullable restore
#line 37 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
                       Write(Html.DisplayFor(modelItem => item.LlegadaAmarillo));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" min</td>
                        <td><i class=""fas fa-circle"" style=""color: yellow;""></i></td>
                        <td><a class=""btn btn-success float_Table"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Editar"" data-original-title=""Tooltip on bottom""><i class=""fas fa-edit""></i></a></td>
                        </tr>
                        <tr>
                        <td>llegadaRojo</td>
                        <td>Tolerancia Rojo</td>
                        <td>");
#nullable restore
#line 44 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
                       Write(Html.DisplayFor(modelItem => item.LlegadaRojo));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" min</td>
                        <td><i class=""fas fa-circle"" style=""color: red;""></i></td>
                        <td class=""mobileHidden""><a class=""btn btn-success float_Table"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Editar"" data-original-title=""Tooltip on bottom""><i class=""fas fa-edit""></i></a></td>
                        </tr>
");
#nullable restore
#line 48 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                </tbody>
            </table>
        </div>
        <div class=""col"">
            <table id=""tableSalida"" data-toggle=""table"" data-search=""false"" data-show-columns=""false"" data-pagination=""false"" data-locale=""es-MX"" data-classes=""table table-hover"" data-page-size=""5"" data-show-footer=""true"">
                <thead>
                    <tr>
                        <th data-visible=""false""></th>
                        <th>Hora de Salida</th>
                        <th>Porcentaje</th>
                        <th></th>
                        <th class=""mobileHidden"">Acciones</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 64 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
                     foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                        <td>salidaVerde</td>\r\n                        <td>Tolerancia Verde</td>\r\n                        <td>");
#nullable restore
#line 68 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
                       Write(Html.DisplayFor(modelItem => item.SalidaVerde));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"%</td>
                        <td><i class=""fas fa-circle"" style=""color: green;""></i></td>
                        <td><a class=""btn btn-success float_Table"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Editar"" data-original-title=""Tooltip on bottom""><i class=""fas fa-edit""></i></a></td>
                        </tr>
                        <tr>
                        <td>salidaAmarillo</td>
                        <td>Tolerancia Amarillo</td>
                        <td>");
#nullable restore
#line 75 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
                       Write(Html.DisplayFor(modelItem => item.SalidaAmarillo));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"%</td>
                        <td><i class=""fas fa-circle"" style=""color: yellow;""></i></td>
                        <td><a class=""btn btn-success float_Table"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Editar"" data-original-title=""Tooltip on bottom""><i class=""fas fa-edit""></i></a></td>
                        </tr>
                        <tr>
                        <td>salidaRojo</td>
                        <td>Tolerancia Rojo</td>
                        <td>");
#nullable restore
#line 82 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
                       Write(Html.DisplayFor(modelItem => item.SalidaRojo));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"%</td>
                        <td><i class=""fas fa-circle"" style=""color: red;""></i></td>
                        <td class=""mobileHidden""><a class=""btn btn-success float_Table"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Editar"" data-original-title=""Tooltip on bottom""><i class=""fas fa-edit""></i></a></td>
                        </tr>
");
#nullable restore
#line 86 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    <script>\r\n        const Model = ");
#nullable restore
#line 95 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
                 Write(Html.Raw(Json.Serialize(Model)));

#line default
#line hidden
#nullable disable
                WriteLiteral(@";
        const token = $(""[name='__RequestVerificationToken']"").val();
        $('#tableLlegada').on('click-row.bs.table', async (row, $element, field) => {
            let num = null;
            let col = null;
            switch ($element[0]) {
                case 'llegadaVerde':
                    num = Model[0].llegadaVerde;
                    col = 'llegadaVerde';
                break
                case 'llegadaAmarillo':
                    num = Model[0].llegadaAmarillo;
                    col = 'llegadaAmarillo';
                break
                case 'llegadaRojo':
                    num = Model[0].llegadaRojo;
                    col = 'llegadaRojo';
                break
            }
            const { value } = await Swal.fire({
                title: ""Hora Llegada "" + $element[1],
                confirmButtonColor: ""#67E7CF"",
                confirmButtonText:""Guardar"",
                cancelButtonText:""Cancelar"",
                input: 'text',
          ");
                WriteLiteral(@"      inputValue: num,
                reverseButtons: true,
                showCancelButton: true,
                allowOutsideClick: false,
                allowEscapeKey: false,
                showCloseButton: true,
                closeButtonHtml: '<i class=""fas fa-times""></i>',
                inputValidator: (value) => {
                  if (!value) {
                    return 'Se necesita escribir una cantidad'
                  }
                  else if(!value.match(/^([0-9][0-9])$/)){
                    return 'Algún caracter no es aceptado'
                  }
                }
            });
            if(value){
                $.ajax({
                    url: '");
#nullable restore
#line 138 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
                     Write(Url.Action("Admin","SemaphoreParams"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                    method: ""POST"",
                    data: {
                        __RequestVerificationToken: token,
                        ""ID"":""1"",
                        ""key"":`${col}`,
                        ""value"":`${value}`
                    },
                    async: true,
                    dataType: ""json"",
                    success: function (res){
                        Swal.fire({
                            icon: 'success',
                            title: '¡Éxito!',
                            confirmButtonColor: ""#67E7CF"",
                            confirmButtonText:""Aceptar"",
                            text: `Se ha sido modificado correctamente`,
                            onClose: ()=>{
                                location.reload(true);
                            }
                        }).then((result) => {
                            if (result.value) {
                                location.reload(true);
                          ");
                WriteLiteral(@"  }
                        });
                    }
                });
            }
        });

        $('#tableSalida').on('click-row.bs.table', async (row, $element, field) => {
            let num = null;
            let col = null;
            switch ($element[0]) {
                case 'salidaVerde':
                    num = Model[0].salidaVerde;
                    col = 'salidaVerde';
                break
                case 'salidaAmarillo':
                    num = Model[0].salidaAmarillo;
                    col = 'salidaAmarillo';
                break
                case 'salidaRojo':
                    num = Model[0].salidaRojo;
                    col = 'salidaRojo';
                break
            }
            const { value } = await Swal.fire({
                title: ""Hora Salida "" + $element[1],
                confirmButtonColor: ""#67E7CF"",
                confirmButtonText:""Guardar"",
                cancelButtonText:""Cancelar"",
                inp");
                WriteLiteral(@"ut: 'text',
                inputValue: num,
                reverseButtons: true,
                showCancelButton: true,
                allowOutsideClick: false,
                allowEscapeKey: false,
                showCloseButton: true,
                closeButtonHtml: '<i class=""fas fa-times""></i>',
                inputValidator: (value) => {
                  if (!value) {
                    return 'Se necesita escribir una cantidad'
                  }
                  else if(!value.match(/^([0-9][0-9])$/)){
                    return 'Algún caracter no es aceptado'
                  }
                }
            });
            if(value){
                $.ajax({
                    url: '");
#nullable restore
#line 209 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Admin\SemaphoreParams.cshtml"
                     Write(Url.Action("Admin","SemaphoreParams"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                    method: ""POST"",
                    data: {
                        __RequestVerificationToken: token,
                        ""ID"":""1"",
                        ""key"":`${col}`,
                        ""value"":`${value}`
                    },
                    async: true,
                    dataType: ""json"",
                    success: function (res){
                        Swal.fire({
                            icon: 'success',
                            title: '¡Éxito!',
                            confirmButtonColor: ""#67E7CF"",
                            confirmButtonText:""Aceptar"",
                            text: `Se ha sido modificado correctamente`,
                            onClose: ()=>{
                                location.reload(true);
                            }
                        }).then((result) => {
                            if (result.value) {
                                location.reload(true);
                          ");
                WriteLiteral("  }\r\n                        });\r\n                    }\r\n                });\r\n            }\r\n        });\r\n    </script>\r\n");
            }
            );
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<siges.Models.SemaphoreParams>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
