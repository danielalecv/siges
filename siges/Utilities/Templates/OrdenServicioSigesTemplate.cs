using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

using Microsoft.AspNetCore.Hosting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Utilities.Templates {
  public class OrdenServicioSigesTemplate {
    public static byte[] getOrdenServicioFormat(String assetsPath, Models.OrdenServicio os) {
      MemoryStream stream = new MemoryStream();
      PdfDocument pdf = new PdfDocument(new PdfWriter(stream));
      Document document = new Document(pdf, PageSize.A4);
      document.SetMargins(10, 15, 10, 15);

      // =============================
      pdf.GetDocumentInfo().SetTitle("Orden de servicio");
      pdf.SetTagged();
      //PdfFont symFont = PdfFontFactory.CreateFont(System.IO.Path.Combine(assetsPath, "assets/fonts/eversonmono.ttf"), PdfEncodings.IDENTITY_H, true);
      PdfFont parFont = PdfFontFactory.CreateFont(System.IO.Path.Combine(assetsPath, "assets/fonts/Raleway-Regular.ttf"), true);
      document.SetFont(parFont);
      document.SetFontSize(7);

      // =============================
      Table t = new Table(UnitValue.CreatePercentArray(new float[] { 25, 50, 25 }));
      t.SetWidth(UnitValue.CreatePercentValue(100));
      Cell c = new Cell().Add(new Image(ImageDataFactory.Create(System.IO.Path.Combine(assetsPath, "assets/img/roatechblack250.png"))).SetWidth(UnitValue.CreatePercentValue(70)));
      c.SetBackgroundColor(new DeviceCmyk(88, 38, 0, 4));
      c.SetBorder(Border.NO_BORDER);
      c.SetVerticalAlignment(VerticalAlignment.MIDDLE);
      c.SetTextAlignment(TextAlignment.CENTER);
      c.SetPaddingLeft(20);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("ORDEN DE SERVICIO").SetBold().SetFontSize(14).SetTextAlignment(TextAlignment.CENTER));
      c.SetBackgroundColor(new DeviceCmyk(88, 38, 0, 4));
      c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
      c.SetBorder(Border.NO_BORDER);
      c.SetVerticalAlignment(VerticalAlignment.MIDDLE);
      c.SetHorizontalAlignment(HorizontalAlignment.CENTER);
      t.AddCell(c);
      c = new Cell().Add(new Image(ImageDataFactory.Create(System.IO.Path.Combine(assetsPath, "assets/img/sigesnegro150px.png"))).SetWidth(UnitValue.CreatePercentValue(70)));
      c.SetBackgroundColor(new DeviceCmyk(88, 38, 0, 4));
      c.SetBorder(Border.NO_BORDER);
      c.SetVerticalAlignment(VerticalAlignment.MIDDLE);
      c.SetTextAlignment(TextAlignment.CENTER);
      c.SetPaddingLeft(20);
      t.AddCell(c);
      document.Add(t);
      ////////////////////////////////////////////////////////////////////////////////////////////////////

      t = new Table(UnitValue.CreatePercentArray(new float[]{25, 50, 25}));
      Table tAux = new Table(new float[] { 1, 1, 1, 1 });
      tAux = new Table(new float[] { 1 });
      tAux.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell().Add(new Paragraph("LÍNEA DE NEGOCIO:\t").SetFont(parFont).SetFontSize(8).Add(new Paragraph(os.LineaNegocio.Nombre).SetFontSize(8)));
      c.SetBorder(Border.NO_BORDER);
      c.SetPadding(-2);
      c.SetMargin(0);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("TIPO DE SERVICIO:\t").SetFont(parFont).SetFontSize(8).Add(new Paragraph(os.Servicio.Nombre).SetFontSize(8)));
      c.SetBorder(Border.NO_BORDER);
      c.SetPadding(-2);
      c.SetMargin(0);
      tAux.AddCell(c);
      t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));

      tAux = new Table(UnitValue.CreatePercentArray(new float[]{100}));
      tAux.AddCell(new Cell());
      t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));

      c = new Cell().Add(new Paragraph("FOLIO No.: ").SetFontSize(8).Add(new Paragraph(os.Folio).SetFont(parFont).SetFontSize(8)));
      c.SetBorder(Border.NO_BORDER);
      c.SetVerticalAlignment(VerticalAlignment.MIDDLE);
      c.SetHorizontalAlignment(HorizontalAlignment.RIGHT);
      t.AddCell(c);
      document.Add(t);
      ////////////////////////////////////////////////////////////////////////////////////////////////////

      String contactName = (os.ContactoNombre == null ? "" : os.ContactoNombre) + " " + (os.ContactoAP == null ? "" : os.ContactoAP) + " " + (os.ContactoAM == null ? "" : os.ContactoAM);
      t = new Table(new float[] { 2, 1 });
      t.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell(1, 2).Add(new Paragraph("DATOS DEL CLIENTE").SetFontSize(8));
      c.SetBackgroundColor(new DeviceCmyk(88, 38, 0, 4));
      c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);
      tAux = new Table(new float[] { 1, 1, 1, 1 });
      tAux.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell().Add(new Paragraph("NOMBRE"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell(1, 3).Add(new Paragraph(os.Cliente.RazonSocial));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("UBICACIÓN"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell(1, 3).Add(new Paragraph(os.Ubicacion.Nombre));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("DIRECCIÓN"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell(1, 3).Add(new Paragraph(os.Ubicacion.Direccion));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("TELÉFONO"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph(os.Ubicacion.ContactoTelefono != null ? os.Ubicacion.ContactoTelefono : os.ContactoTelefono));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("CONTACTO").SetFontSize(7).SetTextAlignment(TextAlignment.CENTER));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph(contactName));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));

      tAux = new Table(UnitValue.CreatePercentArray(new float[] { 30, 70 }));
      tAux.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell().Add(new Paragraph("FECHA SOLICITUD"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph(os.FechaAdministrativa.ToString("dd/MM/yyyy")));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("FECHA INICIO DE SERVICIO"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph(os.FechaInicio.Value.ToString("dd/MM/yyyy HH:MM")));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("FECHA FIN DE SERVICIO"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      //c = new Cell().Add(new Paragraph(os.FechaFin.Value.Subtract(os.FechaInicio.Value).Add(new TimeSpan(1, 0, 0, 0)).TotalDays.ToString()));
      c = new Cell().Add(new Paragraph(os.FechaFin.Value.ToString("dd/MM/yyyy HH:MM")));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("PERSONAL ASIGNADO"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph(os.Personal.Count > 0 ? os.Personal[0].Persona.Nombre + " " + os.Personal[0].Persona.Paterno + " " + os.Personal[0].Persona.Materno : ""));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));
      document.Add(t);
      ////////////////////////////////////////////////////////////////////////////////////////////////////

      t = new Table(UnitValue.CreatePercentArray(new float[] { 100 }));
      t.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell(1, 2).Add(new Paragraph("ACTIVO FIJO UTILIZADO").SetFontSize(8));
      c.SetBackgroundColor(new DeviceCmyk(88, 38, 0, 4));
      c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);
      document.Add(t);

      t = new Table(UnitValue.CreatePercentArray(new float[]{10,15,50,25}));
      t.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell().Add(new Paragraph("CLAVE").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      c = new Cell().Add(new Paragraph("MARCA").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      c = new Cell().Add(new Paragraph("DESCRIPCIÓN").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      c = new Cell().Add(new Paragraph("TIPO").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      if(os.Activos.Count > 0){
        foreach(Models.OrdenActivoFijo of in os.Activos){
          c = new Cell().Add(new Paragraph(of.ActivoFijo.Clave));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);

          c = new Cell().Add(new Paragraph(of.ActivoFijo.Marca));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);

          c = new Cell().Add(new Paragraph(of.ActivoFijo.Descripcion));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);

          c = new Cell().Add(new Paragraph(of.ActivoFijo.Tipo));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);
        }
      }
      document.Add(t);
      ////////////////////////////////////////////////////////////////////////////////////////////////////

      t = new Table(UnitValue.CreatePercentArray(new float[] { 100 }));
      t.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell(1, 2).Add(new Paragraph("INSUMOS UTILIZADOS").SetFontSize(8));
      c.SetBackgroundColor(new DeviceCmyk(88, 38, 0, 4));
      c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);
      document.Add(t);

      t = new Table(UnitValue.CreatePercentArray(new float[]{20,20,50,10}));
      t.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell().Add(new Paragraph("CLAVE").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      c = new Cell().Add(new Paragraph("MARCA").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      c = new Cell().Add(new Paragraph("DESCRIPCIÓN").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      c = new Cell().Add(new Paragraph("CANTIDAD").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      if(os.Insumos.Count > 0){
        foreach(Models.OrdenInsumo oi in os.Insumos){
          c = new Cell().Add(new Paragraph(oi.Insumo.Clave));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);

          c = new Cell().Add(new Paragraph(oi.Insumo.Marca));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);

          c = new Cell().Add(new Paragraph(oi.Insumo.Descripcion));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);

          c = new Cell().Add(new Paragraph(oi.Cantidad.ToString()));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);
        }
      }
      document.Add(t);
      ////////////////////////////////////////////////////////////////////////////////////////////////////

      t = new Table(new float[] { 1 });
      t.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell(1, 4).Add(new Paragraph("OBSERVACIONES").SetFontSize(8));
      c.SetBackgroundColor(new DeviceCmyk(88, 38, 0, 4));
      c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);
      c = new Cell(1, 4).Add(new Paragraph("Cualquier adicional que se deba considerar para realizar los trabajos se debera anotar").SetFontSize(8));
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);
      /*for (int i = 0; i < 5; i++)
        {*/
      c = new Cell().Add(new Paragraph(os.Observaciones == null ? "Sin observaciones" : os.Observaciones));
      t.AddCell(c);
      //}
      document.Add(t);
      ////////////////////////////////////////////////////////////////////////////////////////////////////

      document.Add(new Paragraph("\n\n"));
      t = new Table(UnitValue.CreatePercentArray(new float[] { 25, 10, 25, 10, 25 }));
      t.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell().Add(new Paragraph("FIRMA CLIENTE").SetTextAlignment(TextAlignment.CENTER));
      c.SetBorder(Border.NO_BORDER);
      c.SetBorderBottom(new SolidBorder(0));
      c.SetMargin(0);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("\n"));
      c.SetBorder(Border.NO_BORDER);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("FIRMA COMERCIAL").SetTextAlignment(TextAlignment.CENTER));
      c.SetBorder(Border.NO_BORDER);
      c.SetBorderBottom(new SolidBorder(0));
      c.SetMargin(0);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("\n"));
      c.SetBorder(Border.NO_BORDER);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("FIRMA TECNICO (DESPUES DEL SERVICIO)").SetTextAlignment(TextAlignment.CENTER));
      c.SetBorder(Border.NO_BORDER);
      c.SetBorderBottom(new SolidBorder(0));
      c.SetMargin(0);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("Cliente").SetTextAlignment(TextAlignment.CENTER));
      c.SetBorder(Border.NO_BORDER);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("\n"));
      c.SetBorder(Border.NO_BORDER);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("Comercial").SetTextAlignment(TextAlignment.CENTER));
      c.SetBorder(Border.NO_BORDER);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("\n"));
      c.SetBorder(Border.NO_BORDER);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("Técnico").SetTextAlignment(TextAlignment.CENTER));
      c.SetBorder(Border.NO_BORDER);
      t.AddCell(c);
      document.Add(t);
      ////////////////////////////////////////////////////////////////////////////////////////////////////

      document.Close();
      return stream.ToArray();
    }


    
    public static byte[] getOrdenServicioFormatEnglish(String assetsPath, Models.OrdenServicio os) {
      MemoryStream stream = new MemoryStream();
      PdfDocument pdf = new PdfDocument(new PdfWriter(stream));
      Document document = new Document(pdf, PageSize.A4);
      document.SetMargins(10, 15, 10, 15);

      // =============================
      pdf.GetDocumentInfo().SetTitle("Service Order");
      pdf.SetTagged();
      //PdfFont symFont = PdfFontFactory.CreateFont(System.IO.Path.Combine(assetsPath, "assets/fonts/eversonmono.ttf"), PdfEncodings.IDENTITY_H, true);
      PdfFont parFont = PdfFontFactory.CreateFont(System.IO.Path.Combine(assetsPath, "assets/fonts/Raleway-Regular.ttf"), true);
      document.SetFont(parFont);
      document.SetFontSize(7);

      // =============================
      Table t = new Table(UnitValue.CreatePercentArray(new float[] { 25, 50, 25 }));
      t.SetWidth(UnitValue.CreatePercentValue(100));
      Cell c = new Cell().Add(new Image(ImageDataFactory.Create(System.IO.Path.Combine(assetsPath, "assets/img/roatechblack250.png"))).SetWidth(UnitValue.CreatePercentValue(70)));
      c.SetBackgroundColor(new DeviceCmyk(88, 38, 0, 4));
      c.SetBorder(Border.NO_BORDER);
      c.SetVerticalAlignment(VerticalAlignment.MIDDLE);
      c.SetTextAlignment(TextAlignment.CENTER);
      c.SetPaddingLeft(20);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("SERVICE ORDER").SetBold().SetFontSize(14).SetTextAlignment(TextAlignment.CENTER));
      c.SetBackgroundColor(new DeviceCmyk(88, 38, 0, 4));
      c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
      c.SetBorder(Border.NO_BORDER);
      c.SetVerticalAlignment(VerticalAlignment.MIDDLE);
      c.SetHorizontalAlignment(HorizontalAlignment.CENTER);
      t.AddCell(c);
      c = new Cell().Add(new Image(ImageDataFactory.Create(System.IO.Path.Combine(assetsPath, "assets/img/sigesnegro150px.png"))).SetWidth(UnitValue.CreatePercentValue(70)));
      c.SetBackgroundColor(new DeviceCmyk(88, 38, 0, 4));
      c.SetBorder(Border.NO_BORDER);
      c.SetVerticalAlignment(VerticalAlignment.MIDDLE);
      c.SetTextAlignment(TextAlignment.CENTER);
      c.SetPaddingLeft(20);
      t.AddCell(c);
      document.Add(t);
      ////////////////////////////////////////////////////////////////////////////////////////////////////

      t = new Table(UnitValue.CreatePercentArray(new float[]{25, 50, 25}));
      Table tAux = new Table(new float[] { 1, 1, 1, 1 });
      tAux = new Table(new float[] { 1 });
      tAux.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell().Add(new Paragraph("BUSINESS LINE:\t").SetFont(parFont).SetFontSize(8).Add(new Paragraph(os.LineaNegocio.Nombre).SetFontSize(8)));
      c.SetBorder(Border.NO_BORDER);
      c.SetPadding(-2);
      c.SetMargin(0);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("SERVICE TYPE:\t").SetFont(parFont).SetFontSize(8).Add(new Paragraph(os.Servicio.Nombre).SetFontSize(8)));
      c.SetBorder(Border.NO_BORDER);
      c.SetPadding(-2);
      c.SetMargin(0);
      tAux.AddCell(c);
      t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));

      tAux = new Table(UnitValue.CreatePercentArray(new float[]{100}));
      tAux.AddCell(new Cell());
      t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));

      c = new Cell().Add(new Paragraph("S.O.#: ").SetFontSize(8).Add(new Paragraph(os.Folio).SetFont(parFont).SetFontSize(8)));
      c.SetBorder(Border.NO_BORDER);
      c.SetVerticalAlignment(VerticalAlignment.MIDDLE);
      c.SetHorizontalAlignment(HorizontalAlignment.RIGHT);
      t.AddCell(c);
      document.Add(t);
      ////////////////////////////////////////////////////////////////////////////////////////////////////

      String contactName = (os.ContactoNombre == null ? "" : os.ContactoNombre) + " " + (os.ContactoAP == null ? "" : os.ContactoAP) + " " + (os.ContactoAM == null ? "" : os.ContactoAM);
      t = new Table(new float[] { 2, 1 });
      t.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell(1, 2).Add(new Paragraph("CUSTOMER INFORMATION").SetFontSize(8));
      c.SetBackgroundColor(new DeviceCmyk(88, 38, 0, 4));
      c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);
      tAux = new Table(new float[] { 1, 1, 1, 1 });
      tAux.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell().Add(new Paragraph("NAME"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell(1, 3).Add(new Paragraph(os.Cliente.RazonSocial));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("LOCATION"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell(1, 3).Add(new Paragraph(os.Ubicacion.Nombre));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("ADDRESS"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell(1, 3).Add(new Paragraph(os.Ubicacion.Direccion));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("PHONE"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph(os.Ubicacion.ContactoTelefono != null ? os.Ubicacion.ContactoTelefono : os.ContactoTelefono));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("CONTACT").SetFontSize(7).SetTextAlignment(TextAlignment.CENTER));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph(contactName));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));

      tAux = new Table(UnitValue.CreatePercentArray(new float[] { 30, 70 }));
      tAux.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell().Add(new Paragraph("ORDER DATE"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph(os.FechaAdministrativa.ToString("dd/MM/yyyy")));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("STARTING DATE"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph(os.FechaInicio.Value.ToString("dd/MM/yyyy HH:MM")));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("END DATE"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      //c = new Cell().Add(new Paragraph(os.FechaFin.Value.Subtract(os.FechaInicio.Value).Add(new TimeSpan(1, 0, 0, 0)).TotalDays.ToString()));
      c = new Cell().Add(new Paragraph(os.FechaFin.Value.ToString("dd/MM/yyyy HH:MM")));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph("SERVICE PERSON"));
      c.SetBorder(Border.NO_BORDER);
      tAux.AddCell(c);
      c = new Cell().Add(new Paragraph(os.Personal.Count > 0 ? os.Personal[0].Persona.Nombre + " " + os.Personal[0].Persona.Paterno + " " + os.Personal[0].Persona.Materno : ""));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      tAux.AddCell(c);
      t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));
      document.Add(t);
      ////////////////////////////////////////////////////////////////////////////////////////////////////

      t = new Table(UnitValue.CreatePercentArray(new float[] { 100 }));
      t.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell(1, 2).Add(new Paragraph("FIXED ASSET").SetFontSize(8));
      c.SetBackgroundColor(new DeviceCmyk(88, 38, 0, 4));
      c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);
      document.Add(t);

      t = new Table(UnitValue.CreatePercentArray(new float[]{10,15,50,25}));
      t.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell().Add(new Paragraph("KEY").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      c = new Cell().Add(new Paragraph("BRAND").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      c = new Cell().Add(new Paragraph("DESCRIPTION").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      c = new Cell().Add(new Paragraph("TYPE").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      if(os.Activos.Count > 0){
        foreach(Models.OrdenActivoFijo of in os.Activos){
          c = new Cell().Add(new Paragraph(of.ActivoFijo.Clave));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);

          c = new Cell().Add(new Paragraph(of.ActivoFijo.Marca));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);

          c = new Cell().Add(new Paragraph(of.ActivoFijo.Descripcion));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);

          c = new Cell().Add(new Paragraph(of.ActivoFijo.Tipo));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);
        }
      }
      document.Add(t);
      ////////////////////////////////////////////////////////////////////////////////////////////////////

      t = new Table(UnitValue.CreatePercentArray(new float[] { 100 }));
      t.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell(1, 2).Add(new Paragraph("INPUTS").SetFontSize(8));
      c.SetBackgroundColor(new DeviceCmyk(88, 38, 0, 4));
      c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);
      document.Add(t);

      t = new Table(UnitValue.CreatePercentArray(new float[]{20,20,50,10}));
      t.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell().Add(new Paragraph("KEY").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      c = new Cell().Add(new Paragraph("BRAND").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      c = new Cell().Add(new Paragraph("DESCRIPTION").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      c = new Cell().Add(new Paragraph("QTY.").SetBold());
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);

      if(os.Insumos.Count > 0){
        foreach(Models.OrdenInsumo oi in os.Insumos){
          c = new Cell().Add(new Paragraph(oi.Insumo.Clave));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);

          c = new Cell().Add(new Paragraph(oi.Insumo.Marca));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);

          c = new Cell().Add(new Paragraph(oi.Insumo.Descripcion));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);

          c = new Cell().Add(new Paragraph(oi.Cantidad.ToString()));
          c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0));
          c.SetTextAlignment(TextAlignment.CENTER);
          t.AddCell(c);
        }
      }
      document.Add(t);
      ////////////////////////////////////////////////////////////////////////////////////////////////////

      t = new Table(new float[] { 1 });
      t.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell(1, 4).Add(new Paragraph("OBSERVATIONS").SetFontSize(8));
      c.SetBackgroundColor(new DeviceCmyk(88, 38, 0, 4));
      c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);
      c = new Cell(1, 4).Add(new Paragraph("Any additional that should be considered to carry out the work should be noted").SetFontSize(8));
      c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 10));
      c.SetTextAlignment(TextAlignment.CENTER);
      t.AddCell(c);
      /*for (int i = 0; i < 5; i++)
        {*/
      c = new Cell().Add(new Paragraph(os.Observaciones == null ? "No observations" : os.Observaciones));
      t.AddCell(c);
      //}
      document.Add(t);
      ////////////////////////////////////////////////////////////////////////////////////////////////////

      document.Add(new Paragraph("\n\n"));
      t = new Table(UnitValue.CreatePercentArray(new float[] { 25, 10, 25, 10, 25 }));
      t.SetWidth(UnitValue.CreatePercentValue(100));
      c = new Cell().Add(new Paragraph("CUSTOMER'S SIGNATURE").SetTextAlignment(TextAlignment.CENTER));
      c.SetBorder(Border.NO_BORDER);
      c.SetBorderBottom(new SolidBorder(0));
      c.SetMargin(0);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("\n"));
      c.SetBorder(Border.NO_BORDER);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("COMMERCIAL SIGNATURE").SetTextAlignment(TextAlignment.CENTER));
      c.SetBorder(Border.NO_BORDER);
      c.SetBorderBottom(new SolidBorder(0));
      c.SetMargin(0);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("\n"));
      c.SetBorder(Border.NO_BORDER);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("TECHNICIAN'S SIGNATURE (AFTER THE SERVICE)").SetTextAlignment(TextAlignment.CENTER));
      c.SetBorder(Border.NO_BORDER);
      c.SetBorderBottom(new SolidBorder(0));
      c.SetMargin(0);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("Customer").SetTextAlignment(TextAlignment.CENTER));
      c.SetBorder(Border.NO_BORDER);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("\n"));
      c.SetBorder(Border.NO_BORDER);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("Commercial").SetTextAlignment(TextAlignment.CENTER));
      c.SetBorder(Border.NO_BORDER);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("\n"));
      c.SetBorder(Border.NO_BORDER);
      t.AddCell(c);
      c = new Cell().Add(new Paragraph("Technician").SetTextAlignment(TextAlignment.CENTER));
      c.SetBorder(Border.NO_BORDER);
      t.AddCell(c);
      document.Add(t);
      ////////////////////////////////////////////////////////////////////////////////////////////////////

      document.Close();
      return stream.ToArray();
    }
  }
}
