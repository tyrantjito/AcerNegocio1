using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Telerik.Web.UI;
using E_Utilities;

public partial class CarteraMora : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lnkImprimir_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;

        //tipos de font a utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente11 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de un nuevo documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER.Rotate());
        documento.AddTitle(" Cartera Mora ");
        documento.AddCreator("Desarrollarte");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\Cartera_Mora_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

        //si no existe la carpeta temporal la creamos 
        if (!(Directory.Exists(ruta)))
            Directory.CreateDirectory(ruta);

        FileInfo docto = new FileInfo(archivo);
        if (docto.Exists)
            docto.Delete();
        if (archivo.Trim() != "")
        {

            FileStream file = new FileStream(archivo,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            FileShare.ReadWrite);
            PdfWriter.GetInstance(documento, file);

            // Abrir documento.
            documento.Open();

            PdfPTable encabezado = new PdfPTable(9);
            encabezado.DefaultCell.Border = 0;
            int[] encabezadocellwidth = { 11,11,11,11,11,11,11,11,12 };
            encabezado.SetWidths(encabezadocellwidth);
            encabezado.WidthPercentage = 100F;
            encabezado.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell tittle = (new PdfPCell(new Phrase("MORA SEGMENTADA", fuente11)) { Colspan = 9 });
            tittle.HorizontalAlignment = Element.ALIGN_CENTER;
            tittle.Border = 0;
            tittle.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(tittle);

            PdfPCell sucursal = (new PdfPCell(new Phrase("SUCURSAL", fuente6)));
            sucursal.HorizontalAlignment = Element.ALIGN_CENTER;
            sucursal.Border = 0;
            sucursal.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(sucursal);

            PdfPCell oneSev = (new PdfPCell(new Phrase("1 - 7", fuente6)));
            oneSev.HorizontalAlignment = Element.ALIGN_CENTER;
            oneSev.Border = 0;
            oneSev.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(oneSev);

            PdfPCell ochQui = (new PdfPCell(new Phrase("8 - 15", fuente6)));
            ochQui.HorizontalAlignment = Element.ALIGN_CENTER;
            ochQui.Border = 0;
            ochQui.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(ochQui);

            PdfPCell diescTre = (new PdfPCell(new Phrase("16 - 30", fuente6)));
            diescTre.HorizontalAlignment = Element.ALIGN_CENTER;
            diescTre.Border = 0;
            diescTre.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(diescTre);

            PdfPCell tre1Ses = (new PdfPCell(new Phrase("31 - 60", fuente6)));
            tre1Ses.HorizontalAlignment = Element.ALIGN_CENTER;
            tre1Ses.Border = 0;
            tre1Ses.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(tre1Ses);

            PdfPCell Ses1nov = (new PdfPCell(new Phrase("61 - 90", fuente6)));
            Ses1nov.HorizontalAlignment = Element.ALIGN_CENTER;
            Ses1nov.Border = 0;
            Ses1nov.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(Ses1nov);

            PdfPCell nov1cien20 = (new PdfPCell(new Phrase("91 - 120", fuente6)));
            nov1cien20.HorizontalAlignment = Element.ALIGN_CENTER;
            nov1cien20.Border = 0;
            nov1cien20.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(nov1cien20);

            PdfPCell cien20more = (new PdfPCell(new Phrase("> 120", fuente6)));
            cien20more.HorizontalAlignment = Element.ALIGN_CENTER;
            cien20more.Border = 0;
            cien20more.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(cien20more);

            PdfPCell totGral = (new PdfPCell(new Phrase("TOTAL GENERAL", fuente6)));
            totGral.HorizontalAlignment = Element.ALIGN_CENTER;
            totGral.Border = 0;
            totGral.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(totGral);

            PdfPTable bordegr = new PdfPTable(1);
            bordegr.DefaultCell.Border = 1;
            bordegr.WidthPercentage = 100F;
            bordegr.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell oneCell = (new PdfPCell((encabezado)));
            oneCell.HorizontalAlignment = Element.ALIGN_CENTER;
            oneCell.Border=1;
            bordegr.AddCell(oneCell);

            documento.Add(bordegr);
            /*
            PdfPTable datos = new PdfPTable(9);
            datos.DefaultCell.Border = 0;
            int[] datoscellwidth = { 11, 11, 11, 11, 11, 11, 11, 11, 12 };
            datos.SetWidths(datoscellwidth);
            datos.WidthPercentage = 100F;
            datos.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell suc1 = (new PdfPCell(new Phrase("LERMA", fuente6)));
            suc1.HorizontalAlignment = Element.ALIGN_CENTER;
            suc1.Border = 0;
            suc1.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            datos.AddCell(suc1);

            PdfPCell dat1 = (new PdfPCell(new Phrase("8,500", fuente6)));
            dat1.HorizontalAlignment = Element.ALIGN_CENTER;
            dat1.Border = 0;
            datos.AddCell(dat1);

            PdfPCell dat2 = (new PdfPCell(new Phrase("10,850", fuente6)));
            dat2.HorizontalAlignment = Element.ALIGN_CENTER;
            dat2.Border = 0;
            datos.AddCell(dat2);

            PdfPCell dat3 = (new PdfPCell(new Phrase("12,850", fuente6)));
            dat3.HorizontalAlignment = Element.ALIGN_CENTER;
            dat3.Border = 0;
            datos.AddCell(dat3);

            PdfPCell dat4 = (new PdfPCell(new Phrase("21,350", fuente6)));
            dat4.HorizontalAlignment = Element.ALIGN_CENTER;
            dat4.Border = 0;
            datos.AddCell(dat4);

            PdfPCell dat5 = (new PdfPCell(new Phrase("30,850", fuente6)));
            dat5.HorizontalAlignment = Element.ALIGN_CENTER;
            dat5.Border = 0;
            datos.AddCell(dat5);

            PdfPCell dat6 = (new PdfPCell(new Phrase(" ", fuente6)));
            dat6.HorizontalAlignment = Element.ALIGN_CENTER;
            dat6.Border = 0;
            datos.AddCell(dat6);

            PdfPCell dat7 = (new PdfPCell(new Phrase(" ", fuente6)));
            dat7.HorizontalAlignment = Element.ALIGN_CENTER;
            dat7.Border = 0;
            datos.AddCell(dat7);

            PdfPCell datTot = (new PdfPCell(new Phrase("$ 84,400", fuente6)));
            datTot.HorizontalAlignment = Element.ALIGN_CENTER;
            datTot.Border = 0;
            datTot.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            datos.AddCell(datTot);

            documento.Add(datos);
            

            PdfPTable totales = new PdfPTable(9);
            totales.DefaultCell.Border = 0;
            int[] totalescellwidth = { 11, 11, 11, 11, 11, 11, 11, 11, 12 };
            totales.SetWidths(totalescellwidth);
            totales.WidthPercentage = 100F;
            totales.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell total = (new PdfPCell(new Phrase("TOTAL", fuente3)));
            total.HorizontalAlignment = Element.ALIGN_CENTER;
            total.Border = 0;
            total.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            totales.AddCell(total);

            PdfPCell tot1 = (new PdfPCell(new Phrase("8,500", fuente3)));
            tot1.HorizontalAlignment = Element.ALIGN_CENTER;
            tot1.Border = 0;
            tot1.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            totales.AddCell(tot1);

            PdfPCell tot2 = (new PdfPCell(new Phrase("10,850", fuente3)));
            tot2.HorizontalAlignment = Element.ALIGN_CENTER;
            tot2.Border = 0;
            tot2.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            totales.AddCell(tot2);

            PdfPCell tot3 = (new PdfPCell(new Phrase("12,850", fuente3)));
            tot3.HorizontalAlignment = Element.ALIGN_CENTER;
            tot3.Border = 0;
            tot3.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            totales.AddCell(tot3);

            PdfPCell tot4 = (new PdfPCell(new Phrase("21,350", fuente3)));
            tot4.HorizontalAlignment = Element.ALIGN_CENTER;
            tot4.Border = 0;
            tot4.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            totales.AddCell(tot4);

            PdfPCell tot5 = (new PdfPCell(new Phrase("30,850", fuente3)));
            tot5.HorizontalAlignment = Element.ALIGN_CENTER;
            tot5.Border = 0;
            tot5.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            totales.AddCell(tot5);

            PdfPCell tot6 = (new PdfPCell(new Phrase(" ", fuente3)));
            tot6.HorizontalAlignment = Element.ALIGN_CENTER;
            tot6.Border = 0;
            tot6.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            totales.AddCell(tot6);

            PdfPCell tot7 = (new PdfPCell(new Phrase(" ", fuente3)));
            tot7.HorizontalAlignment = Element.ALIGN_CENTER;
            tot7.Border = 0;
            tot7.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            totales.AddCell(tot7);

            PdfPCell totfin = (new PdfPCell(new Phrase("$ 84,400", fuente3)));
            totfin.HorizontalAlignment = Element.ALIGN_CENTER;
            totfin.Border = 0;
            totfin.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            totales.AddCell(totfin);

            documento.Add(totales);
            */

            documento.Close();

        }

        //
        FileInfo filename = new FileInfo(archivo);
        if (filename.Exists)
        {
            string url = "Descargas.aspx?filename=" + filename.Name;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "pdfs", "window.open('" + url + "', 'nuevo', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, titlebar=no, fullscreen=yes,resizable=yes');", true);
        }
    }
}