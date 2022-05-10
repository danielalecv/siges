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

namespace siges.Utilities.Templates
{
    public class OrdenServicioTemplate
    {

        public static byte[] getOrdenServicioFormat(String assetsPath, Models.OrdenServicio os)
        {
            MemoryStream stream = new MemoryStream();
            PdfDocument pdf = new PdfDocument(new PdfWriter(stream));
            Document document = new Document(pdf, PageSize.A4);
            document.SetMargins(10, 15, 10, 15);

            // =============================

            pdf.GetDocumentInfo().SetTitle("Orden de Servicio");
            pdf.SetTagged();
            PdfFont symFont = PdfFontFactory.CreateFont(System.IO.Path.Combine(assetsPath, "assets/fonts/eversonmono.ttf"), PdfEncodings.IDENTITY_H, true);
            PdfFont parFont = PdfFontFactory.CreateFont(System.IO.Path.Combine(assetsPath, "assets/fonts/Raleway-Regular.ttf"), true);
            document.SetFont(parFont);
            document.SetFontSize(5);

            // =============================


            Table t = new Table(UnitValue.CreatePercentArray(new float[] { 100 }));
            //Table t = new Table(UnitValue.CreatePercentArray(new float[] { 25, 50, 25 }));
            t.SetWidth(UnitValue.CreatePercentValue(100));
            Cell /*c = new Cell().Add(new Image(ImageDataFactory.Create(System.IO.Path.Combine(assetsPath, "assets/img/logo.png"))).SetWidth(UnitValue.CreatePercentValue(70)));
            c.SetBorder(Border.NO_BORDER);
            c.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            c.SetTextAlignment(TextAlignment.CENTER);
            c.SetPaddingLeft(20);
            t.AddCell(c);*/
            c = new Cell().Add(new Paragraph("ORDEN DE SERVICIO").SetBold().SetFontSize(12).SetTextAlignment(TextAlignment.CENTER));
            c.SetBorder(Border.NO_BORDER);
            c.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            c.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            t.AddCell(c);
            /*c = new Cell().Add(new Paragraph("Distribuidora de Accesos y Mecanismos, S.A. DE C.V.\n" +
                    "Adolfo Prieto 610, Col. Del Valle\n" +
                    "Delegación Benito Juárez, México, D.F. \n" +
                    "Teléfono: 01 (55) 6838 4871\n").SetFontSize(5));
            c.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            c.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            c.SetTextAlignment(TextAlignment.CENTER);
            c.SetBorder(Border.NO_BORDER);
            t.AddCell(c);*/
            document.Add(t);
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            t = new Table(UnitValue.CreatePercentArray(new float[] {50, 50}));
            t.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell()
              .SetBorder(Border.NO_BORDER)
              .Add(new Paragraph("Documento ")
                  .SetFontSize(6)
                  .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                  .Add(new Paragraph(os.Contrato.Nombre + " / " + os.Contrato.Tipo)
                    .SetFontSize(8)
                    .SetBold())
                  .SetTextAlignment(TextAlignment.LEFT));
            t.AddCell(c);
            c = new Cell()
              .SetBorder(Border.NO_BORDER)
              .Add(new Paragraph("Folio N\u00BA ")
                  .SetBold()
                  .SetFontSize(6)
                  .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                  .Add(new Paragraph(os.Folio)
                    .SetFontSize(6)
                    .SetBold()
                    .SetBorder(new SolidBorder(1))
                    .SetPadding(1))
                  .SetTextAlignment(TextAlignment.RIGHT));
            t.AddCell(c);
            document.Add(t);
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //document.Add(new Paragraph("Folio N\u00BA ").SetBold().SetFontSize(6).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(os.Folio).SetFontSize(6).SetBold().SetBorder(new SolidBorder(1)).SetPadding(1)).SetTextAlignment(TextAlignment.RIGHT));
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            String contactName = (os.ContactoNombre == null ? "" : os.ContactoNombre) + " " + (os.ContactoAP == null ? "" : os.ContactoAP) + " " + (os.ContactoAM == null ? "" : os.ContactoAM);

            t = new Table(new float[] { 2, 1 });
            t.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell(1, 2).Add(new Paragraph("CLIENTE").SetFontSize(6));
            c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 100));
            c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
            c.SetTextAlignment(TextAlignment.CENTER);
            t.AddCell(c);
            Table tAux = new Table(new float[] { 1, 1, 1, 1 });
            tAux.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell().Add(new Paragraph("Nombre"));
            c.SetBorder(Border.NO_BORDER);
            tAux.AddCell(c);
            c = new Cell(1, 3).Add(new Paragraph(os.Cliente.RazonSocial));
            c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(1));
            c.SetTextAlignment(TextAlignment.CENTER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("Dirección"));
            c.SetBorder(Border.NO_BORDER);
            tAux.AddCell(c);
            c = new Cell(1, 3).Add(new Paragraph(os.Ubicacion.Direccion));
            c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(1));
            c.SetTextAlignment(TextAlignment.CENTER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("Referencias"));
            c.SetBorder(Border.NO_BORDER);
            tAux.AddCell(c);
            c = new Cell(1, 3).Add(new Paragraph(""));
            c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(1));
            c.SetTextAlignment(TextAlignment.CENTER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("Teléfono"));
            c.SetBorder(Border.NO_BORDER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph(os.ContactoTelefono == null ? "" : os.ContactoTelefono));
            c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(1));
            c.SetTextAlignment(TextAlignment.CENTER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("Persona de Contacto").SetFontSize(5).SetTextAlignment(TextAlignment.CENTER));
            c.SetBorder(Border.NO_BORDER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph(contactName));
            c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(1));
            c.SetTextAlignment(TextAlignment.CENTER);
            tAux.AddCell(c);
            t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));

            tAux = new Table(UnitValue.CreatePercentArray(new float[] { 30, 70 }));
            tAux.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell().Add(new Paragraph("Fecha Solicitud"));
            c.SetBorder(Border.NO_BORDER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph(os.FechaAdministrativa.ToString("dd/MM/yyyy")));
            c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(1));
            c.SetTextAlignment(TextAlignment.CENTER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("Fecha de Servicio"));
            c.SetBorder(Border.NO_BORDER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph(os.FechaInicio.Value.ToString("dd/MM/yyyy")));
            c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(1));
            c.SetTextAlignment(TextAlignment.CENTER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("Dias de Servicio"));
            c.SetBorder(Border.NO_BORDER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph(os.FechaFin.Value.Subtract(os.FechaInicio.Value).Add(new TimeSpan(1, 0, 0, 0)).TotalDays.ToString()));
            c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(1));
            c.SetTextAlignment(TextAlignment.CENTER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("Horario de Atención"));
            c.SetBorder(Border.NO_BORDER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph(""));
            c.SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(1));
            c.SetTextAlignment(TextAlignment.CENTER);
            tAux.AddCell(c);
            t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));
            document.Add(t);
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            t = new Table(UnitValue.CreatePercentArray(new float[] { 40, 60 }));
            t.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell(1, 2).Add(new Paragraph("REQUISITOS PARA INGRESO").SetFontSize(6));
            c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 100));
            c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
            c.SetTextAlignment(TextAlignment.CENTER);
            t.AddCell(c);

            tAux = new Table(UnitValue.CreatePercentArray(new float[] { 30, 30, 30 }));
            tAux.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("IMSS").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("EPP").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("ANTIDOPING").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("DC3").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("EXAMEN").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("OTROS").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("CURSO").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("EXAMEN MEDICO").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph(" "));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));

            tAux = new Table(UnitValue.CreatePercentArray(new float[] { 10, 90 }));
            tAux.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell().Add(new Paragraph("NOTAS: "));
            c.SetBorder(Border.NO_BORDER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\t"));
            c.SetBorder(Border.NO_BORDER);
            c.SetBorderBottom(new SolidBorder(1));
            tAux.AddCell(c);
            c = new Cell(1, 2).Add(new Paragraph("\n"));
            c.SetBorder(Border.NO_BORDER);
            c.SetBorderBottom(new SolidBorder(1));
            tAux.AddCell(c);
            t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));
            document.Add(t);
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            t = new Table(UnitValue.CreatePercentArray(new float[] { 40, 60 }));
            t.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell(1, 2).Add(new Paragraph("TIPO DE SERVICIO").SetFontSize(6));
            c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 100));
            c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
            c.SetTextAlignment(TextAlignment.CENTER);
            t.AddCell(c);

            tAux = new Table(new float[] { 1, 1 });
            tAux.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("MANTENIMIENTO PREVENTIVO").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("INSTALACION").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("MANTENIMIENTO CORRECTIVO").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("REVISION DE PRESUPUESTO").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("CAMBIO DE REFACCION").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph(""));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));

            tAux = new Table(UnitValue.CreatePercentArray(new float[] { 10, 90 }));
            tAux.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell().Add(new Paragraph("NOTAS: "));
            c.SetBorder(Border.NO_BORDER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\t"));
            c.SetBorder(Border.NO_BORDER);
            c.SetBorderBottom(new SolidBorder(1));
            tAux.AddCell(c);
            c = new Cell(1, 2).Add(new Paragraph("\n"));
            c.SetBorder(Border.NO_BORDER);
            c.SetBorderBottom(new SolidBorder(1));
            tAux.AddCell(c);
            t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));
            document.Add(t);
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            t = new Table(UnitValue.CreatePercentArray(new float[] { 40, 60 }));
            t.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell(1, 2).Add(new Paragraph("EQUIPO PARA SERVICIO").SetFontSize(6));
            c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 100));
            c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
            c.SetTextAlignment(TextAlignment.CENTER);
            t.AddCell(c);

            tAux = new Table(new float[] { 1, 1 });
            tAux.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("CORTINA ENROLLABLE").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("RAMPA HIDRAULICA").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("PUERTA SECCIONAL").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("PUERTA PEATONAL").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("RAMPA MECANICA").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("SELLO / ABRIGO").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));

            tAux = new Table(UnitValue.CreatePercentArray(new float[] { 10, 90 }));
            tAux.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("OTROS").SetFont(parFont).SetFontSize(5)));
            c.SetBorder(Border.NO_BORDER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\t"));
            c.SetBorder(Border.NO_BORDER);
            c.SetBorderBottom(new SolidBorder(1));
            tAux.AddCell(c);
            c = new Cell(1, 2).Add(new Paragraph("\n"));
            c.SetBorder(Border.NO_BORDER);
            c.SetBorderBottom(new SolidBorder(1));
            tAux.AddCell(c);
            t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));
            document.Add(t);

            t = new Table(UnitValue.CreatePercentArray(new float[] { 70, 10, 20 }));
            t.SetWidth(UnitValue.CreatePercentValue(100));
            tAux = new Table(UnitValue.CreatePercentArray(new float[] { 10, 20, 10, 20, 10, 20 }));
            tAux.SetWidth(UnitValue.CreatePercentValue(100));
            for (int i = 0; i < 6; i++)
            {
                c = new Cell().Add(new Paragraph("Cantidad").SetTextAlignment(TextAlignment.CENTER));
                c.SetBorder(Border.NO_BORDER);
                tAux.AddCell(c);
                c = new Cell().Add(new Paragraph(""));
                c.SetBorder(Border.NO_BORDER);
                c.SetBorderBottom(new SolidBorder(1));
                tAux.AddCell(c);
                c = new Cell().Add(new Paragraph("Ancho").SetTextAlignment(TextAlignment.CENTER));
                c.SetBorder(Border.NO_BORDER);
                tAux.AddCell(c);
                c = new Cell().Add(new Paragraph(""));
                c.SetBorder(Border.NO_BORDER);
                c.SetBorderBottom(new SolidBorder(1));
                tAux.AddCell(c);
                c = new Cell().Add(new Paragraph("Alto").SetTextAlignment(TextAlignment.CENTER));
                c.SetBorder(Border.NO_BORDER);
                tAux.AddCell(c);
                c = new Cell().Add(new Paragraph(""));
                c.SetBorder(Border.NO_BORDER);
                c.SetBorderBottom(new SolidBorder(1));
                tAux.AddCell(c);
            }
            t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));
            t.AddCell(new Cell().Add(new Paragraph("\t")).SetBorder(Border.NO_BORDER));

            tAux = new Table(UnitValue.CreatePercentArray(new float[] { 10, 90 }));
            tAux.SetWidth(UnitValue.CreatePercentValue(100));
            for (int i = 0; i < 6; i++)
            {
                c = new Cell().Add(new Paragraph("Operación").SetTextAlignment(TextAlignment.CENTER));
                c.SetBorder(Border.NO_BORDER);
                tAux.AddCell(c);
                c = new Cell().Add(new Paragraph(""));
                c.SetBorder(Border.NO_BORDER);
                c.SetBorderBottom(new SolidBorder(1));
                tAux.AddCell(c);
            }
            t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));
            document.Add(t);
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            t = new Table(UnitValue.CreatePercentArray(new float[] { 30, 10, 30, 30 }));
            t.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell(1, 4).Add(new Paragraph("REQUISITOS PARA INSTALACION Y / O SERVICIO").SetFontSize(6));
            c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 100));
            c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
            c.SetTextAlignment(TextAlignment.CENTER);
            t.AddCell(c);
            c = new Cell(1, 4).Add(new Paragraph("GENERAL").SetFontSize(6));
            c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 15));
            c.SetTextAlignment(TextAlignment.CENTER);
            t.AddCell(c);

            c = new Cell(1, 4).Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("AREA DESPEJADA LIBRE DE ESCOMBROS (MUEBLES, LAMPARAS, TUBERIAS, ALARMAS, ETC...)").SetFont(parFont).SetFontSize(6)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            t.AddCell(c);
            c = new Cell(1, 4).Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("ALIMENTACION ELECTRICA 110 V (NO MAYOR A 30 MTS)").SetFont(parFont).SetFontSize(6)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            t.AddCell(c);
            c = new Cell(1, 4).Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("ALIMENTACION ELECTRICA 220 V (NO MAYOR A 15 MTS)").SetFont(parFont).SetFontSize(6)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            t.AddCell(c);
            c = new Cell(1, 4).Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("AREA PARA RESGUARDO DE EQUIPOS").SetFont(parFont).SetFontSize(6)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            t.AddCell(c);
            c = new Cell(1, 4).Add(new Paragraph("CORTINAS ENROLLABLES, PUERTAS (RAPIDAS, SECCIONALES, PEATONALES)").SetFontSize(6));
            c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 15));
            c.SetTextAlignment(TextAlignment.CENTER);
            t.AddCell(c);
            tAux = new Table(new float[] { 1 });
            tAux.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("PLAFONES RETIRADOS EN CASO DE EXISTIR").SetFont(parFont).SetFontSize(6)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("ACOMETIDA PARA OPERACION DE OPERADOR").SetFont(parFont).SetFontSize(6)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            t.AddCell(new Cell().Add(tAux));

            c = new Cell().Add(new Paragraph("VANO DE").SetFontSize(6).SetRotationAngle(Math.PI / 2));
            c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 15));
            c.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            t.AddCell(c);
            tAux = new Table(UnitValue.CreatePercentArray(new float[] { 20, 80 }));
            tAux.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell(1, 2).Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("CONCRETO").SetFont(parFont).SetFontSize(6)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell(1, 2).Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("ESTRUCTURA METALICA").SetFont(parFont).SetFontSize(6)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("OTRO").SetFont(parFont).SetFontSize(6)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("").SetFontSize(8));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            c.SetBorderBottom(new SolidBorder(1));
            tAux.AddCell(c);
            t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));

            tAux = new Table(new float[] { 1 });
            tAux.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell().Add(new Paragraph("Vano con dimensiones correctas según lo suministrado por el área comercial").SetTextAlignment(TextAlignment.CENTER));
            c.SetBorder(Border.NO_BORDER);
            tAux.AddCell(c);
            c = new Cell().Add(new Paragraph("\n"));
            c.SetBorder(Border.NO_BORDER);
            c.SetBorderBottom(new SolidBorder(1));
            tAux.AddCell(c);
            t.AddCell(new Cell().Add(tAux).SetBorder(Border.NO_BORDER));

            c = new Cell(1, 4).Add(new Paragraph("RAMPAS (HIDRAULICAS / MECANICAS)").SetFontSize(6));
            c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 15));
            c.SetTextAlignment(TextAlignment.CENTER);
            t.AddCell(c);

            c = new Cell(1, 4).Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("FOSA DEBIDAMENTE ESCUADRADA Y CON LAS DIMENSIONES PROPORCIONADAS POR DAMSA").SetFont(parFont).SetFontSize(6)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            t.AddCell(c);
            c = new Cell(1, 4).Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("ANGULOS PERIMETRALES").SetFont(parFont).SetFontSize(6)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            t.AddCell(c);
            c = new Cell(1, 4).Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("TUBERIAS PARA CONEXION ELECTRICA (RAMPAS HIDRAULICAS)").SetFont(parFont).SetFontSize(6)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            t.AddCell(c);
            c = new Cell(1, 4).Add(new Paragraph("\u2610\u0020").SetFont(symFont).SetFontSize(8).Add(new Paragraph("PLACAS PARA INSTALACION DE TOPES").SetFont(parFont).SetFontSize(6)));
            c.SetBorder(Border.NO_BORDER);
            c.SetPadding(-2);
            c.SetMargin(0);
            t.AddCell(c);
            document.Add(t);
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            t = new Table(new float[] { 1 });
            t.SetWidth(UnitValue.CreatePercentValue(100));
            c = new Cell(1, 4).Add(new Paragraph("OBSERVACIONES").SetFontSize(6));
            c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 100));
            c.SetFontColor(new DeviceCmyk(0, 0, 0, 0));
            c.SetTextAlignment(TextAlignment.CENTER);
            t.AddCell(c);
            c = new Cell(1, 4).Add(new Paragraph("Cualquier adicional que se deba considerar para realizar los trabajos se debera anotar").SetFontSize(6));
            c.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 15));
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
            c.SetBorderBottom(new SolidBorder(1));
            c.SetMargin(1);
            t.AddCell(c);
            c = new Cell().Add(new Paragraph("\n"));
            c.SetBorder(Border.NO_BORDER);
            t.AddCell(c);
            c = new Cell().Add(new Paragraph("FIRMA COMERCIAL").SetTextAlignment(TextAlignment.CENTER));
            c.SetBorder(Border.NO_BORDER);
            c.SetBorderBottom(new SolidBorder(1));
            c.SetMargin(1);
            t.AddCell(c);
            c = new Cell().Add(new Paragraph("\n"));
            c.SetBorder(Border.NO_BORDER);
            t.AddCell(c);
            c = new Cell().Add(new Paragraph("FIRMA TECNICO (DESPUES DEL SERVICIO)").SetTextAlignment(TextAlignment.CENTER));
            c.SetBorder(Border.NO_BORDER);
            c.SetBorderBottom(new SolidBorder(1));
            c.SetMargin(1);
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

    }
}
