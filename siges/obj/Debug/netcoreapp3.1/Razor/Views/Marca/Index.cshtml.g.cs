#pragma checksum "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Marca\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d780c5c1a86e7bc47b41b275686a643089eb8a4c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Marca_Index), @"mvc.1.0.view", @"/Views/Marca/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d780c5c1a86e7bc47b41b275686a643089eb8a4c", @"/Views/Marca/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f34ac2a518186cd6e1fad2ab593126982fe1167", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Marca_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<siges.Models.Marca>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Marca\Index.cshtml"
  
    ViewData["Title"] = "Marcas";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container"">
    <div class=""row"">
        <div class=""col"">
            <h2>Marcas</h2>
        </div>
        <div class=""col align-self-center text-right"">
            <a onclick=""btnAdd()"" class=""btn btn-success marg-Float float"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Agregar"" data-original-title=""Tooltip on bottom""><i class=""fas fa-plus""></i></a>
        </div>
    </div>
    <div class=""row"">
        <div class=""col margBott4em"">
            <table id=""tableMarca"" data-toggle=""table"" data-search=""true"" data-show-columns=""false"" data-pagination=""true"" data-locale=""es-MX"" data-classes=""table table-hover"" data-page-size=""5""  data-page-list=""[5,10,25]"" data-show-footer=""true"" data-sort-name=""name"">
                <thead>
                    <tr>
                        <th data-visible=""false"" data-field=""id""></th>
                        <th data-sortable=""true"" data-field=""name"">Marca</th>
                        <th class=""mobileHidden"">Acciones</th>
         ");
            WriteLiteral("           </tr>\r\n                </thead>\r\n                <tbody>\r\n");
#nullable restore
#line 27 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Marca\Index.cshtml"
                     foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 29 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Marca\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 30 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Marca\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Descripcion));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
                            <td class=""mobileHidden"">
                                <a class=""btn btn-success float_Table"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Acciones"" data-original-title=""Tooltip on bottom""><i class=""fab fa-elementor""></i></a>
                            </td>
                        </tr>
");
#nullable restore
#line 35 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Marca\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
    <script>
        const btnAdd = () => {
            const token = $(""[name='__RequestVerificationToken']"").val();
            Swal.fire({
                title: ""Agregar Marca"",
                confirmButtonColor: ""#67E7CF"",
                confirmButtonText:""Guardar"",
                cancelButtonText:""Cancelar"",
                reverseButtons: true,
                showCancelButton: true,
                allowOutsideClick: false,
                allowEscapeKey: false,
                showCloseButton: true,
                closeButtonHtml: '<i class=""fas fa-times""></i>',
                html:`
                    <div class=""form-group text-left"">
                        <label class=""control-label"">Marca *</label>
                        <input id=""txtMarca"" type=""text"" class=""form-control"" />
                    </div>
                    <div class=""form-group text-left"">
                        <label class=""control-label"">Campo Opcional</label>
                        <input id");
                WriteLiteral(@"=""txtOp1"" type=""text"" class=""form-control"" />
                    </div>
                    <div class=""form-group text-left"">
                        <label class=""control-label"">Campo Opcional</label>
                        <input id=""txtOp2"" type=""text"" class=""form-control"" />
                    </div>
                    <div class=""form-group text-right"">
                        <small>* Campos obligatorios</small>
                    </div>
                `,
                onOpen: () => {
                    $(""input[type='text']"").attr(""onkeyup"",'this.value = this.value.toUpperCase();');
                },
                preConfirm: () =>{
                    if($(""#txtMarca"").val().length === 0){
                        $(""#txtMarca"").attr(""style"", ""border-color: #f27474!important;box-shadow: 0 0 2px #f27474!important;"");
                    }
                    else{
                        $(""#txtMarca"").removeAttr(""style"");
                    }
                    if($(""");
                WriteLiteral(@"#txtMarca"").val().length === 0){
                        Swal.showValidationMessage(""Algún campo obligatorio está vacío"");
                    }
                }
            }).then(result => {
                if(result.value){
                    $.ajax({
                        url: '");
#nullable restore
#line 92 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Marca\Index.cshtml"
                         Write(Url.Action("Create","Marca"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                        method: ""POST"",
                        data: {
                            __RequestVerificationToken: token,
                            mDTO:{
                                Descripcion: $(""#txtMarca"").val(),
                                Opcional1: $(""#txtOp1"").val(),
                                Opcional2: $(""#txtOp2"").val()
                            }
                        },
                        async: true,
                        dataType: ""json"",
                        success: res => {
                            if(res.success){
                                Swal.fire({
                                    icon: 'success',
                                    title: '¡Éxito!',
                                    confirmButtonColor: ""#67E7CF"",
                                    confirmButtonText:""Aceptar"",
                                    text: `La marca se registró con éxito`,
                                    onClose: ()=>{
     ");
                WriteLiteral(@"                                   location.reload(true);
                                    }
                                }).then((result) => {
                                    if (result.value) {
                                        location.reload(true);
                                    }
                                });
                            }
                            else{
                                Swal.fire({
                                    icon: 'error',
                                    title: '¡Error!',
                                    confirmButtonColor: ""#67E7CF"",
                                    confirmButtonText:""Aceptar"",
                                    text: `La marca ya existe`
                                });
                            }
                        }
                    });
                }
                else{
                    Swal.close();
                }
            });
        }

        cons");
                WriteLiteral("t btnDetails = element => {\r\n            $.ajax({\r\n                url: \'");
#nullable restore
#line 141 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Marca\Index.cshtml"
                 Write(Url.Action("Details","Marca"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                method: ""GET"",
                data: {
                    id: element.id
                },
                async: true,
                dataType: ""json"",
                success: data => {
                    if(data.success){
                        Swal.fire({
                            title:""Detalles"",
                            confirmButtonColor: ""#67E7CF"",
                            confirmButtonText:""Aceptar"",
                            html:`
                                <div class=""form-group text-left"">
                                    <label class=""control-label"">Marca</label>
                                    <input type=""text"" class=""form-control"" value=""${(data.data.descripcion===null)?"""":data.data.descripcion}"" readonly/>
                                </div>
                                <div class=""form-group text-left"">
                                    <label class=""control-label"">Campo Opcional</label>
                             ");
                WriteLiteral(@"       <input type=""text"" class=""form-control"" value=""${(data.data.opcional1 === null)?"""":data.data.opcional1}"" readonly/>
                                </div>
                                <div class=""form-group text-left"">
                                    <label class=""control-label"">Campo Opcional</label>
                                    <input type=""text"" class=""form-control"" value=""${(data.data.opcional2 === null)?"""":data.data.opcional2}"" readonly/>
                                </div>
                            `
                        });
                    }
                }
            });
        }

        const btnEdit = element => {
            const token = $(""[name='__RequestVerificationToken']"").val();
            $.ajax({
                url: '");
#nullable restore
#line 177 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Marca\Index.cshtml"
                 Write(Url.Action("Details","Marca"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                method: ""GET"",
                data: {
                    id: element.id
                },
                async: true,
                dataType: ""json"",
                success: data => {
                    if(data.success){
                        Swal.fire({
                            title: ""Editar Marca"",
                            confirmButtonColor: ""#67E7CF"",
                            confirmButtonText:""Guardar"",
                            cancelButtonText:""Cancelar"",
                            reverseButtons: true,
                            showCancelButton: true,
                            allowOutsideClick: false,
                            allowEscapeKey: false,
                            showCloseButton: true,
                            closeButtonHtml: '<i class=""fas fa-times""></i>',
                            html:`
                                <div class=""form-group text-left"">
                                    <label class=""control");
                WriteLiteral(@"-label"">Marca *</label>
                                    <input id=""txtMarca"" type=""text"" class=""form-control"" value=""${(data.data.descripcion===null)?"""":data.data.descripcion}"" />
                                </div>
                                <div class=""form-group text-left"">
                                    <label class=""control-label"">Campo Opcional</label>
                                    <input id=""txtOp1"" type=""text"" class=""form-control"" value=""${(data.data.opcional1 === null)?"""":data.data.opcional1}"" />
                                </div>
                                <div class=""form-group text-left"">
                                    <label class=""control-label"">Campo Opcional</label>
                                    <input id=""txtOp2"" type=""text"" class=""form-control"" value=""${(data.data.opcional2 === null)?"""":data.data.opcional2}"" />
                                </div>
                                <div class=""form-group text-right"">
                     ");
                WriteLiteral(@"               <small>* Campos obligatorios</small>
                                </div>
                            `,
                            onOpen: ()=>{
                                $(""input[type='text']"").attr(""onkeyup"",'this.value = this.value.toUpperCase();');
                            },
                            preConfirm: () =>{
                                if($(""#txtMarca"").val().length === 0){
                                    $(""#txtMarca"").attr(""style"", ""border-color: #f27474!important;box-shadow: 0 0 2px #f27474!important;"");
                                }
                                else{
                                    $(""#txtMarca"").removeAttr(""style"");
                                }
                                if($(""#txtMarca"").val().length === 0){
                                    Swal.showValidationMessage(""Algún campo obligatorio está vacío"");
                                }
                            }
                        }");
                WriteLiteral(").then(result =>{\r\n                            if(result.value){\r\n                                $.ajax({\r\n                                    url: \'");
#nullable restore
#line 231 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Marca\Index.cshtml"
                                     Write(Url.Action("Edit","Marca"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                                    method: ""POST"",
                                    data: {
                                        __RequestVerificationToken: token,
                                        id: element.id,
                                        mDTO:{
                                            Id:element.id,
                                            Descripcion: $(""#txtMarca"").val(),
                                            Opcional1: $(""#txtOp1"").val(),
                                            Opcional2: $(""#txtOp2"").val()
                                        }
                                    },
                                    async: true,
                                    dataType: ""json"",
                                    success: res => {
                                        if(res.success){
                                            Swal.fire({
                                                icon: 'success',
                          ");
                WriteLiteral(@"                      title: '¡Éxito!',
                                                confirmButtonColor: ""#67E7CF"",
                                                confirmButtonText:""Aceptar"",
                                                text: `La marca se modificó con éxito`,
                                                onClose: ()=>{
                                                    location.reload(true);
                                                }
                                            }).then((result) => {
                                                if (result.value) {
                                                    location.reload(true);
                                                }
                                            });
                                        }
                                    }
                                });
                            }
                            else{
                                Swal.close();
 ");
                WriteLiteral(@"                           }
                        });
                    }
                }
            });
        }

        const btnDelete = element => { 
            const token = $(""[name='__RequestVerificationToken']"").val();
            Swal.fire({
                icon: 'error',
                title: `La marca ${element.name} se eliminará`,
                text: `¿Seguro que deseas eliminarlo?`,
                reverseButtons: true,
                showCancelButton: true,
                confirmButtonText:""Si, Eliminar"",
                cancelButtonText:""No, Cancelar"",
                confirmButtonColor:""#E74C3C"",
                cancelButtonColor: ""#67E7CF"",
                allowOutsideClick: false,
                allowEscapeKey: false,
                showCloseButton: true,
                closeButtonHtml: '<i class=""fas fa-times""></i>'
            }).then(result => {
                if(result.value){
                    $.ajax({
                        url: '");
#nullable restore
#line 293 "C:\Users\HP EliteBook 2570p\source\repos\siges\siges\Views\Marca\Index.cshtml"
                         Write(Url.Action("Delete","Marca"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                        method: ""POST"",
                        data: {
                            __RequestVerificationToken: token,
                            id: element.id
                        },
                        async: true,
                        dataType: ""json"",
                        success: res => {
                            if(res.success){
                                Swal.fire({
                                    icon: 'success',
                                    title: '¡Éxito!',
                                    confirmButtonColor: ""#67E7CF"",
                                    confirmButtonText:""Aceptar"",
                                    text: `La marca ${element.name} ha sido eliminada`,
                                    onClose: ()=>{
                                        location.reload(true);
                                    }
                                }).then(result => {
                                    if (result.value) {");
                WriteLiteral(@"
                                        location.reload(true);
                                    }
                                });
                            }
                        }
                    });
                }
                else{
                    Swal.close();
                }
            });
        }

        $('#tableMarca').on('click-row.bs.table', (row, element, field) => {
            Swal.fire({
                title: `${element.name}`,
                showConfirmButton:false,
                allowOutsideClick: false,
                allowEscapeKey: false,
                showCloseButton: true,
                closeButtonHtml: '<i class=""fas fa-times""></i>',
                html:`
                    <div class=""form-row margTop40"">
                        <div class=""form-group col"">
                            <a onclick=""btnDetails({'id':'${element.id}'})"" class=""btn btn-success float"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Detal");
                WriteLiteral(@"les"" data-original-title=""Tooltip on bottom""><i class=""fas fa-search""></i></a>
                            <label class=""d-block margTop5px"">Detalles</label>
                        </div>
                        <div class=""form-group col"">
                            <a onclick=""btnEdit({'id':'${element.id}', 'name':'${element.name}'})"" class=""btn btn-success float"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Editar"" data-original-title=""Tooltip on bottom""><i class=""fas fa-edit""></i></a>
                            <label class=""d-block margTop5px"">Editar</label>
                        </div>
                        <div class=""form-group col"">
                            <a onclick=""btnDelete({'id':'${element.id}', 'name':'${element.name}'})"" class=""btn btn-success float"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Eliminar"" data-original-title=""Tooltip on bottom""><i class=""fas fa-trash""></i></a>
                            <label class=""d-block margTop5px"">Eliminar</label>
   ");
                WriteLiteral("                     </div>\r\n                    </div>\r\n                `\r\n            });\r\n        });\r\n    </script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<siges.Models.Marca>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
