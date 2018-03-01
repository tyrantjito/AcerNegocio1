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

public partial class PagosPactados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private int[] obtieneSesiones()
    {
        int[] sesiones = new int[5];
        sesiones[0] = Convert.ToInt32(Request.QueryString["u"]);
        sesiones[1] = Convert.ToInt32(Request.QueryString["p"]);
        sesiones[2] = Convert.ToInt32(Request.QueryString["e"]);
        sesiones[3] = Convert.ToInt32(Request.QueryString["t"]);
        return sesiones;
    }
    protected void lnkImprimir_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;

        //tipos de font a utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de un nuevo documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER.Rotate());
        documento.AddTitle(" Pagos Pactados ");
        documento.AddCreator("Desarrollarte");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\PagosPactados_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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

            PdfPTable encabezado = new PdfPTable(7);
            encabezado.DefaultCell.Border = 0;
            int[] encabezadocellwidth = { 15,15,10,10,10,10,10 };
            encabezado.SetWidths(encabezadocellwidth);
            encabezado.WidthPercentage = 80f;
            encabezado.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell fechaToday = (new PdfPCell(new Phrase(fechas.obtieneFechaLocal().ToString("yyyy-MM-dd"), fuente6)) { Colspan= 7 });
            fechaToday.HorizontalAlignment = Element.ALIGN_CENTER;
            fechaToday.BorderColor = new iTextSharp.text.BaseColor(144, 221, 238);
            fechaToday.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(fechaToday);

            PdfPCell reg = new PdfPCell(new Phrase("Región", fuente6));
            reg.HorizontalAlignment = Element.ALIGN_CENTER;
            reg.BorderColor = new iTextSharp.text.BaseColor(144, 221, 238);
            reg.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(reg);

            PdfPCell nameSuc = new PdfPCell(new Phrase("Sucursal", fuente6));
            nameSuc.HorizontalAlignment = Element.ALIGN_CENTER;
            nameSuc.BorderColor = new iTextSharp.text.BaseColor(144, 221, 238);
            nameSuc.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(nameSuc);

            PdfPCell nameTeam = new PdfPCell(new Phrase("# Pactadas", fuente6));
            nameTeam.HorizontalAlignment = Element.ALIGN_CENTER;
            nameTeam.BorderColor = new iTextSharp.text.BaseColor(144, 221, 238);
            nameTeam.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238); 
            encabezado.AddCell(nameTeam);

            PdfPCell montPag = new PdfPCell(new Phrase("Monto Pactadas", fuente6));
            montPag.HorizontalAlignment = Element.ALIGN_CENTER;
            montPag.BorderColor = new iTextSharp.text.BaseColor(144, 221, 238);
            montPag.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(montPag);

            PdfPCell montRec = new PdfPCell(new Phrase("Monto Recuperado", fuente6));
            montRec.HorizontalAlignment = Element.ALIGN_CENTER;
            montRec.BorderColor = new iTextSharp.text.BaseColor(144, 221, 238);
            montRec.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(montRec);

            PdfPCell difer = new PdfPCell(new Phrase("Diferencia", fuente6));
            difer.HorizontalAlignment = Element.ALIGN_CENTER;
            difer.BorderColor = new iTextSharp.text.BaseColor(144, 221, 238);
            difer.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(difer);

            PdfPCell porcen = new PdfPCell(new Phrase("Porcentaje", fuente6));
            porcen.HorizontalAlignment = Element.ALIGN_CENTER;
            porcen.BorderColor = new iTextSharp.text.BaseColor(144, 221, 238);
            porcen.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado.AddCell(porcen);

            PdfPCell reg1 = new PdfPCell(new Phrase(" ", fuente6));
            reg1.HorizontalAlignment = Element.ALIGN_CENTER;
            reg1.Border = 0;
            encabezado.AddCell(reg1);

            PdfPCell nameSuc1 = new PdfPCell(new Phrase(" ", fuente6));
            nameSuc1.HorizontalAlignment = Element.ALIGN_CENTER;
            nameSuc1.Border = 0;
            encabezado.AddCell(nameSuc1);

            PdfPCell nameTeam1 = new PdfPCell(new Phrase(" ", fuente6));
            nameTeam1.HorizontalAlignment = Element.ALIGN_CENTER;
            nameTeam1.Border = 0;
            encabezado.AddCell(nameTeam1);

            PdfPCell montPag1 = new PdfPCell(new Phrase(" ", fuente6));
            montPag1.HorizontalAlignment = Element.ALIGN_CENTER;
            montPag1.Border = 0;
            encabezado.AddCell(montPag1);

            PdfPCell montRec1 = new PdfPCell(new Phrase(" ", fuente6));
            montRec1.HorizontalAlignment = Element.ALIGN_CENTER;
            montRec1.Border = 0;
            encabezado.AddCell(montRec1);

            PdfPCell difer1 = new PdfPCell(new Phrase(" ", fuente6));
            difer1.HorizontalAlignment = Element.ALIGN_CENTER;
            difer1.Border = 0;
            encabezado.AddCell(difer1);

            PdfPCell porcen1 = new PdfPCell(new Phrase(" ", fuente6));
            porcen1.HorizontalAlignment = Element.ALIGN_CENTER;
            porcen1.Border = 0;
            encabezado.AddCell(porcen1);


            PdfPTable encabezado2 = new PdfPTable(2);
            encabezado2.DefaultCell.Border = 0;
            int[] encabezado2cellwidth = { 15,5 };
            encabezado2.SetWidths(encabezado2cellwidth);
            encabezado2.WidthPercentage = 20f;
            encabezado2.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell Sucur = new PdfPCell(new Phrase("Sucursal".ToUpper(), fuente6));
            Sucur.HorizontalAlignment = Element.ALIGN_CENTER;
            Sucur.BorderColor = new iTextSharp.text.BaseColor(144, 221, 238);
            Sucur.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado2.AddCell(Sucur);
            
            PdfPCell Porcen2 = new PdfPCell(new Phrase("Porcentaje".ToUpper(), fuente6));
            Porcen2.HorizontalAlignment = Element.ALIGN_CENTER;
            Porcen2.BorderColor = new iTextSharp.text.BaseColor(144, 221, 238);
            Porcen2.BackgroundColor = new iTextSharp.text.BaseColor(144, 221, 238);
            encabezado2.AddCell(Porcen2);
            /*
            PdfPCell suc1 = new PdfPCell(new Phrase("APATZINGAN", fuente6));
            suc1.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc1);

            PdfPCell suc11 = new PdfPCell(new Phrase(" ", fuente6));
            suc11.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc11);

            PdfPCell suc2 = new PdfPCell(new Phrase("CIUDAD DEL CARMEN", fuente6));
            suc2.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc2);

            PdfPCell suc22 = new PdfPCell(new Phrase(" ", fuente6));
            suc22.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc22);

            PdfPCell suc3 = new PdfPCell(new Phrase("LERMA", fuente6));
            suc3.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc3);

            PdfPCell suc33 = new PdfPCell(new Phrase(" ", fuente6));
            suc33.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc33);

            PdfPCell suc4 = new PdfPCell(new Phrase("MINATITLAN", fuente6));
            suc4.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc4);

            PdfPCell suc44 = new PdfPCell(new Phrase(" ", fuente6));
            suc44.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc44);

            PdfPCell suc5 = new PdfPCell(new Phrase("CIUDAD DEL CARMEN SUC2", fuente6));
            suc5.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc5);

            PdfPCell suc55 = new PdfPCell(new Phrase(" ", fuente6));
            suc55.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc55);

            PdfPCell suc6 = new PdfPCell(new Phrase("TENANGO", fuente6));
            suc6.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc6);

            PdfPCell suc66 = new PdfPCell(new Phrase(" ", fuente6));
            suc66.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc66);

            PdfPCell suc7 = new PdfPCell(new Phrase("JALPA DE MENDEZ", fuente6));
            suc7.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc7);

            PdfPCell suc77 = new PdfPCell(new Phrase(" ", fuente6));
            suc77.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc77);

            PdfPCell suc8 = new PdfPCell(new Phrase("CHOLULA", fuente6));
            suc8.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc8);

            PdfPCell suc88 = new PdfPCell(new Phrase(" ", fuente6));
            suc88.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc88);

            PdfPCell suc9 = new PdfPCell(new Phrase("CAMPECHE", fuente6));
            suc9.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc9);

            PdfPCell suc99 = new PdfPCell(new Phrase(" ", fuente6));
            suc99.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc99);

            PdfPCell suc10 = new PdfPCell(new Phrase("TAMPICO", fuente6));
            suc10.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc10);

            PdfPCell suc1010 = new PdfPCell(new Phrase(" ", fuente6));
            suc1010.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc1010);

            PdfPCell suc11A = new PdfPCell(new Phrase("TENANCINGO", fuente6));
            suc11A.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc11A);

            PdfPCell suc1111 = new PdfPCell(new Phrase(" ", fuente6));
            suc1111.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc1111);

            PdfPCell suc12 = new PdfPCell(new Phrase("CENTRAL", fuente6));
            suc12.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc12);

            PdfPCell suc1212 = new PdfPCell(new Phrase(" ", fuente6));
            suc1212.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc1212);

            PdfPCell suc13 = new PdfPCell(new Phrase("LAZARO CARDENAS", fuente6));
            suc13.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc13);

            PdfPCell suc1313 = new PdfPCell(new Phrase(" ", fuente6));
            suc1313.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc1313);

            PdfPCell suc14 = new PdfPCell(new Phrase("PAPANTLA", fuente6));
            suc14.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc14);

            PdfPCell suc1414 = new PdfPCell(new Phrase(" ", fuente6));
            suc1414.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc1414);

            PdfPCell suc15 = new PdfPCell(new Phrase("TUXPAN", fuente6));
            suc15.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc15);

            PdfPCell suc1515 = new PdfPCell(new Phrase(" ", fuente6));
            suc1515.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc1515);

            PdfPCell suc16 = new PdfPCell(new Phrase("TEOTIHUACAN", fuente6));
            suc16.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc16);

            PdfPCell suc1616 = new PdfPCell(new Phrase(" ", fuente6));
            suc1616.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc1616);

            PdfPCell suc17 = new PdfPCell(new Phrase("POZA RICA", fuente6));
            suc17.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc17);

            PdfPCell suc1717 = new PdfPCell(new Phrase(" ", fuente6));
            suc1717.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc1717);

            PdfPCell suc18 = new PdfPCell(new Phrase("CANCÚN", fuente6));
            suc18.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc18);

            PdfPCell suc1818 = new PdfPCell(new Phrase(" ", fuente6));
            suc1818.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc1818);

            PdfPCell suc19 = new PdfPCell(new Phrase("NARANJOS", fuente6));
            suc19.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc19);

            PdfPCell suc1919 = new PdfPCell(new Phrase(" ", fuente6));
            suc1919.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc1919);

            PdfPCell suc20 = new PdfPCell(new Phrase("URUAPAN", fuente6));
            suc20.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc20);

            PdfPCell suc2020 = new PdfPCell(new Phrase(" ", fuente6));
            suc2020.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc2020);

            PdfPCell suc21 = new PdfPCell(new Phrase("TEMOAYA", fuente6));
            suc21.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc21);

            PdfPCell suc2121 = new PdfPCell(new Phrase(" ", fuente6));
            suc2121.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc2121);

            PdfPCell suc22A = new PdfPCell(new Phrase("PLAYA DEL CARMEN", fuente6));
            suc22A.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc22A);

            PdfPCell suc22AA = new PdfPCell(new Phrase(" ", fuente6));
            suc22AA.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc22AA);

            PdfPCell suc23 = new PdfPCell(new Phrase("VALLE DE BRAVO", fuente6));
            suc23.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc23);

            PdfPCell suc2323 = new PdfPCell(new Phrase(" ", fuente6));
            suc2323.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc2323);

            PdfPCell suc24 = new PdfPCell(new Phrase("ACAYUCAN", fuente6));
            suc24.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc24);

            PdfPCell suc2424 = new PdfPCell(new Phrase(" ", fuente6));
            suc2424.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc2424);

            PdfPCell suc25 = new PdfPCell(new Phrase("SAN MARTIN", fuente6));
            suc25.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc25);

            PdfPCell suc2525 = new PdfPCell(new Phrase(" ", fuente6));
            suc2525.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc2525);

            PdfPCell suc26 = new PdfPCell(new Phrase("CARRILLO PUERTO", fuente6));
            suc26.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc26);

            PdfPCell suc2626 = new PdfPCell(new Phrase(" ", fuente6));
            suc2626.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc2626);

            PdfPCell suc27 = new PdfPCell(new Phrase("ACAPULCO", fuente6));
            suc27.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc27);

            PdfPCell suc2727 = new PdfPCell(new Phrase(" ", fuente6));
            suc2727.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc2727);

            PdfPCell suc28 = new PdfPCell(new Phrase("TANTOYUCA", fuente6));
            suc28.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc28);

            PdfPCell suc2828 = new PdfPCell(new Phrase(" ", fuente6));
            suc2828.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc2828);

            PdfPCell suc29 = new PdfPCell(new Phrase("COZUMEL", fuente6));
            suc29.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc29);

            PdfPCell suc2929 = new PdfPCell(new Phrase(" ", fuente6));
            suc2929.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc2929);

            PdfPCell suc30 = new PdfPCell(new Phrase("TOLUCA", fuente6));
            suc30.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc30);

            PdfPCell suc3030 = new PdfPCell(new Phrase(" ", fuente6));
            suc3030.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc3030);

            PdfPCell suc31 = new PdfPCell(new Phrase("IXTLAHUACA", fuente6));
            suc31.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc31);

            PdfPCell suc3131 = new PdfPCell(new Phrase(" ", fuente6));
            suc3131.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc3131);

            PdfPCell suc32 = new PdfPCell(new Phrase("VERACRUZ", fuente6));
            suc32.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc32);

            PdfPCell suc3232 = new PdfPCell(new Phrase(" ", fuente6));
            suc3232.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc3232);

            PdfPCell suc33A = new PdfPCell(new Phrase("ZIHUATANEJO", fuente6));
            suc33A.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc33A);

            PdfPCell suc33AA = new PdfPCell(new Phrase(" ", fuente6));
            suc33AA.HorizontalAlignment = Element.ALIGN_CENTER;
            encabezado2.AddCell(suc33AA);
            */



            
            PdfPTable union = new PdfPTable(2);
            union.DefaultCell.Border = 0;
            int[] unioncellwidth = {80,20};
            union.SetWidths(unioncellwidth);
            union.WidthPercentage = 100f;
            union.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell tab1 = new PdfPCell(encabezado);
            tab1.HorizontalAlignment = Element.ALIGN_CENTER;
            tab1.Border = 0;
            union.AddCell(tab1);

            PdfPCell tab2 = new PdfPCell(encabezado2);
            tab2.HorizontalAlignment = Element.ALIGN_CENTER;
            tab2.Border = 0;
            union.AddCell(tab2);
            

            documento.Add(union);
            

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