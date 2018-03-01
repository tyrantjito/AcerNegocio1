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

public partial class ComparativoMora : System.Web.UI.Page
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
        iTextSharp.text.Font fuente7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
        iTextSharp.text.Font fuente5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.RED);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de un nuevo documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER.Rotate());
        documento.AddTitle(" Comparativo Mora ");
        documento.AddCreator("Desarrollarte");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\Comparativo_Mora_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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

            PdfPTable encabezado = new PdfPTable(10);
            encabezado.DefaultCell.Border = 0;
            int[] encabezadocellwidth = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            encabezado.SetWidths(encabezadocellwidth);
            encabezado.WidthPercentage = 100f;
            encabezado.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell fechaToday = (new PdfPCell(new Phrase("COMPARATIVO DE MORA " + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd"), fuente7)) { Colspan = 10 });
            fechaToday.HorizontalAlignment = Element.ALIGN_CENTER;
            fechaToday.Border = 3;
            fechaToday.BackgroundColor = new iTextSharp.text.BaseColor(10, 90, 160);
            encabezado.AddCell(fechaToday);

            PdfPCell reg = new PdfPCell(new Phrase("REGIÓN", fuente7));
            reg.HorizontalAlignment = Element.ALIGN_CENTER;
            reg.Border = 3;
            reg.BackgroundColor = new iTextSharp.text.BaseColor(10, 90, 160);
            encabezado.AddCell(reg);

            PdfPCell nameSuc = new PdfPCell(new Phrase("SUCURSAL", fuente7));
            nameSuc.HorizontalAlignment = Element.ALIGN_CENTER;
            nameSuc.Border = 3;
            nameSuc.BackgroundColor = new iTextSharp.text.BaseColor(10, 90, 160);
            encabezado.AddCell(nameSuc);

            PdfPCell nameTeam = new PdfPCell(new Phrase("GRUPOS", fuente7));
            nameTeam.HorizontalAlignment = Element.ALIGN_CENTER;
            nameTeam.Border = 3;
            nameTeam.BackgroundColor = new iTextSharp.text.BaseColor(10, 90, 160);
            encabezado.AddCell(nameTeam);

            PdfPCell client = new PdfPCell(new Phrase("CLIENTES", fuente7));
            client.HorizontalAlignment = Element.ALIGN_CENTER;
            client.Border = 3;
            client.BackgroundColor = new iTextSharp.text.BaseColor(10, 90, 160);
            encabezado.AddCell(client);

            PdfPCell saldTot = new PdfPCell(new Phrase("SALDO TOTAL", fuente7));
            saldTot.HorizontalAlignment = Element.ALIGN_CENTER;
            saldTot.Border = 3;
            saldTot.BackgroundColor = new iTextSharp.text.BaseColor(10, 90, 160);
            encabezado.AddCell(saldTot);

            PdfPCell moraInMe = new PdfPCell(new Phrase("MORA INICIO DE MES", fuente7));
            moraInMe.HorizontalAlignment = Element.ALIGN_CENTER;
            moraInMe.Border = 3;
            moraInMe.BackgroundColor = new iTextSharp.text.BaseColor(10, 90, 160);
            encabezado.AddCell(moraInMe);

            PdfPCell morAct = new PdfPCell(new Phrase("MORA ACTUAL", fuente7));
            morAct.HorizontalAlignment = Element.ALIGN_CENTER;
            morAct.Border = 3;
            morAct.BackgroundColor = new iTextSharp.text.BaseColor(10, 90, 160);
            encabezado.AddCell(morAct);

            PdfPCell difer = new PdfPCell(new Phrase("DIFERENCIA", fuente7));
            difer.HorizontalAlignment = Element.ALIGN_CENTER;
            difer.Border = 3;
            difer.BackgroundColor = new iTextSharp.text.BaseColor(10, 90, 160);
            //difer.BackgroundColor = new iTextSharp.text.BaseColor(223, 165, 165);
            encabezado.AddCell(difer);

            PdfPCell morGrup = new PdfPCell(new Phrase("% MORA GRAL", fuente7));
            morGrup.HorizontalAlignment = Element.ALIGN_CENTER;
            morGrup.Border = 3;
            morGrup.BackgroundColor = new iTextSharp.text.BaseColor(10, 90, 160);
            encabezado.AddCell(morGrup);

            PdfPCell morSuc = new PdfPCell(new Phrase("% MORA SUCURSAL", fuente7));
            morSuc.HorizontalAlignment = Element.ALIGN_CENTER;
            morSuc.Border = 3;
            morSuc.BackgroundColor = new iTextSharp.text.BaseColor(10, 90, 160);
            encabezado.AddCell(morSuc);

            documento.Add(encabezado);

            /*
            PdfPTable llenaDat = new PdfPTable(10);
            llenaDat.DefaultCell.Border = 0;
            int[] llenaDatcellwidth = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            llenaDat.SetWidths(llenaDatcellwidth);
            llenaDat.WidthPercentage = 100f;
            llenaDat.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell reg1 = (new PdfPCell(new Phrase("ESTADO DE MÉXICO", fuente6)) { Rowspan = 2 });
            reg1.HorizontalAlignment = Element.ALIGN_CENTER;
            reg1.Border = 3;
            reg1.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            llenaDat.AddCell(reg1);

            PdfPCell nameSuc1 = new PdfPCell(new Phrase("LERMA", fuente6));
            nameSuc1.HorizontalAlignment = Element.ALIGN_CENTER;
            nameSuc1.Border = 3;
            nameSuc1.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            llenaDat.AddCell(nameSuc1);

            PdfPCell nameTeam1 = new PdfPCell(new Phrase("13", fuente6));
            nameTeam1.HorizontalAlignment = Element.ALIGN_CENTER;
            nameTeam1.Border = 3;
            llenaDat.AddCell(nameTeam1);

            PdfPCell client1 = new PdfPCell(new Phrase("98", fuente6));
            client1.HorizontalAlignment = Element.ALIGN_CENTER;
            client1.Border = 3;
            llenaDat.AddCell(client1);

            PdfPCell saldTot1 = new PdfPCell(new Phrase("$ 1,450,800.00", fuente6));
            saldTot1.HorizontalAlignment = Element.ALIGN_CENTER;
            saldTot1.Border = 3;
            llenaDat.AddCell(saldTot1);

            PdfPCell moraInMe1 = new PdfPCell(new Phrase("$ 57,800.00", fuente6));
            moraInMe1.HorizontalAlignment = Element.ALIGN_CENTER;
            moraInMe1.Border = 3;
            llenaDat.AddCell(moraInMe1);

            PdfPCell morAct1 = new PdfPCell(new Phrase("$ 245,850.00", fuente6));
            morAct1.HorizontalAlignment = Element.ALIGN_CENTER;
            morAct1.Border = 3;
            llenaDat.AddCell(morAct1);

            PdfPCell difer1 = new PdfPCell(new Phrase("$ 188,050.00", fuente5));
            difer1.HorizontalAlignment = Element.ALIGN_CENTER;
            difer1.BackgroundColor = new iTextSharp.text.BaseColor(223, 165, 165);
            difer1.Border = 3;

            llenaDat.AddCell(difer1);

            PdfPCell morGrup1 = new PdfPCell(new Phrase("54.90%", fuente6));
            morGrup1.HorizontalAlignment = Element.ALIGN_CENTER;
            morGrup1.Border = 3;
            llenaDat.AddCell(morGrup1);

            PdfPCell morSuc1 = new PdfPCell(new Phrase("16.95%", fuente6));
            morSuc1.HorizontalAlignment = Element.ALIGN_CENTER;
            morSuc1.Border = 3;
            llenaDat.AddCell(morSuc1);


            PdfPCell nameSuc2 = new PdfPCell(new Phrase("NAUCALPAN", fuente6));
            nameSuc2.HorizontalAlignment = Element.ALIGN_CENTER;
            nameSuc2.Border = 3;
            nameSuc2.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            llenaDat.AddCell(nameSuc2);

            PdfPCell nameTeam2 = new PdfPCell(new Phrase("7", fuente6));
            nameTeam2.HorizontalAlignment = Element.ALIGN_CENTER;
            nameTeam2.Border = 3;
            llenaDat.AddCell(nameTeam2);

            PdfPCell client2 = new PdfPCell(new Phrase("48", fuente6));
            client2.HorizontalAlignment = Element.ALIGN_CENTER;
            client2.Border = 3;
            llenaDat.AddCell(client2);

            PdfPCell saldTot2 = new PdfPCell(new Phrase("$ 865,850.00", fuente6));
            saldTot2.HorizontalAlignment = Element.ALIGN_CENTER;
            saldTot2.Border = 3;
            llenaDat.AddCell(saldTot2);

            PdfPCell moraInMe2 = new PdfPCell(new Phrase("$ 43,500.00", fuente6));
            moraInMe2.HorizontalAlignment = Element.ALIGN_CENTER;
            moraInMe2.Border = 3;
            llenaDat.AddCell(moraInMe2);

            PdfPCell morAct2 = new PdfPCell(new Phrase("$ 189,450.00", fuente6));
            morAct2.HorizontalAlignment = Element.ALIGN_CENTER;
            morAct2.Border = 3;
            llenaDat.AddCell(morAct2);

            PdfPCell difer2 = new PdfPCell(new Phrase("$ 145,950.00", fuente5));
            difer2.HorizontalAlignment = Element.ALIGN_CENTER;
            difer2.Border = 3;
            difer2.BackgroundColor = new iTextSharp.text.BaseColor(223, 165, 165);
            llenaDat.AddCell(difer2);

            PdfPCell morGrup2 = new PdfPCell(new Phrase("42.31%", fuente6));
            morGrup2.HorizontalAlignment = Element.ALIGN_CENTER;
            morGrup2.Border = 3;
            llenaDat.AddCell(morGrup2);

            PdfPCell morSuc2 = new PdfPCell(new Phrase("21.88%", fuente6));
            morSuc2.HorizontalAlignment = Element.ALIGN_CENTER;
            morSuc2.Border = 3;
            llenaDat.AddCell(morSuc2);

            documento.Add(llenaDat);


            PdfPTable subtotal = new PdfPTable(9);
            subtotal.DefaultCell.Border = 0;
            int[] subtotalcellwidth = { 20, 10, 10, 10, 10, 10, 10, 10, 10 };
            subtotal.SetWidths(subtotalcellwidth);
            subtotal.WidthPercentage = 100f;
            subtotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell subText = (new PdfPCell(new Phrase("SUBTOTAL", fuente6)));
            subText.HorizontalAlignment = Element.ALIGN_CENTER;
            subText.Border = 3;
            subText.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal.AddCell(subText);

            PdfPCell sub1 = new PdfPCell(new Phrase("20", fuente6));
            sub1.HorizontalAlignment = Element.ALIGN_CENTER;
            sub1.Border = 3;
            sub1.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal.AddCell(sub1);

            PdfPCell sub2 = new PdfPCell(new Phrase("146", fuente6));
            sub2.HorizontalAlignment = Element.ALIGN_CENTER;
            sub2.Border = 3;
            sub2.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal.AddCell(sub2);

            PdfPCell sub3 = new PdfPCell(new Phrase("$ 2,316,650.00", fuente6));
            sub3.HorizontalAlignment = Element.ALIGN_CENTER;
            sub3.Border = 3;
            sub3.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal.AddCell(sub3);

            PdfPCell sub4 = new PdfPCell(new Phrase("$ 101,300.00", fuente6));
            sub4.HorizontalAlignment = Element.ALIGN_CENTER;
            sub4.Border = 3;
            sub4.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal.AddCell(sub4);

            PdfPCell sub5 = new PdfPCell(new Phrase("$ 435,300.00", fuente6));
            sub5.HorizontalAlignment = Element.ALIGN_CENTER;
            sub5.Border = 3;
            sub5.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal.AddCell(sub5);

            PdfPCell sub6 = new PdfPCell(new Phrase("$ 334,000.00", fuente6));
            sub6.HorizontalAlignment = Element.ALIGN_CENTER;
            sub6.Border = 3;
            sub6.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal.AddCell(sub6);

            PdfPCell submoragral = new PdfPCell(new Phrase("97.21%", fuente6));
            submoragral.HorizontalAlignment = Element.ALIGN_CENTER;
            submoragral.BackgroundColor = new iTextSharp.text.BaseColor(223, 165, 165);
            submoragral.Border = 3;
            submoragral.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal.AddCell(submoragral);

            PdfPCell submoraSuc = new PdfPCell(new Phrase("54.90%", fuente6));
            submoraSuc.HorizontalAlignment = Element.ALIGN_CENTER;
            submoraSuc.Border = 3;
            submoraSuc.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal.AddCell(submoraSuc);

            documento.Add(subtotal);

            PdfPTable llenaDat1 = new PdfPTable(10);
            llenaDat1.DefaultCell.Border = 0;
            int[] llenaDat1cellwidth = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            llenaDat1.SetWidths(llenaDat1cellwidth);
            llenaDat1.WidthPercentage = 100f;
            llenaDat1.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell reg5 = (new PdfPCell(new Phrase("PACÍFICO", fuente6)));
            reg5.HorizontalAlignment = Element.ALIGN_CENTER;
            reg5.Border = 3;
            reg5.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            llenaDat1.AddCell(reg5);

            PdfPCell nameSuc5 = new PdfPCell(new Phrase("URUAPAN", fuente6));
            nameSuc5.HorizontalAlignment = Element.ALIGN_CENTER;
            nameSuc5.Border = 3;
            nameSuc5.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            llenaDat1.AddCell(nameSuc5);

            PdfPCell nameTeam5 = new PdfPCell(new Phrase("17", fuente6));
            nameTeam5.HorizontalAlignment = Element.ALIGN_CENTER;
            nameTeam5.Border = 3;
            llenaDat1.AddCell(nameTeam5);

            PdfPCell client5 = new PdfPCell(new Phrase("153", fuente6));
            client5.HorizontalAlignment = Element.ALIGN_CENTER;
            client5.Border = 3;
            llenaDat1.AddCell(client5);

            PdfPCell saldTot5 = new PdfPCell(new Phrase("$ 2.985,500.00", fuente6));
            saldTot5.HorizontalAlignment = Element.ALIGN_CENTER;
            saldTot5.Border = 3;
            llenaDat1.AddCell(saldTot5);

            PdfPCell moraInMe5 = new PdfPCell(new Phrase("$ 6,500.00", fuente6));
            moraInMe5.HorizontalAlignment = Element.ALIGN_CENTER;
            moraInMe5.Border = 3;
            llenaDat1.AddCell(moraInMe5);

            PdfPCell morAct5 = new PdfPCell(new Phrase("$ 12,500.00", fuente6));
            morAct5.HorizontalAlignment = Element.ALIGN_CENTER;
            morAct5.Border = 3;
            llenaDat1.AddCell(morAct5);

            PdfPCell difer5 = new PdfPCell(new Phrase("$ 6,000.00", fuente6));
            difer5.HorizontalAlignment = Element.ALIGN_CENTER;
            difer5.Border = 3;

            llenaDat1.AddCell(difer5);

            PdfPCell morGrup5 = new PdfPCell(new Phrase("2.79%", fuente6));
            morGrup5.HorizontalAlignment = Element.ALIGN_CENTER;
            morGrup5.Border = 3;
            llenaDat1.AddCell(morGrup5);

            PdfPCell morSuc5 = new PdfPCell(new Phrase(" ", fuente6));
            morSuc5.HorizontalAlignment = Element.ALIGN_CENTER;
            morSuc5.Border = 3;
            llenaDat1.AddCell(morSuc5);
            documento.Add(llenaDat1);



            PdfPTable subtotal2 = new PdfPTable(9);
            subtotal2.DefaultCell.Border = 0;
            int[] subtotal2cellwidth = { 20, 10, 10, 10, 10, 10, 10, 10, 10 };
            subtotal2.SetWidths(subtotal2cellwidth);
            subtotal2.WidthPercentage = 100f;
            subtotal2.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell subText2 = (new PdfPCell(new Phrase("SUBTOTAL", fuente6)));
            subText2.HorizontalAlignment = Element.ALIGN_CENTER;
            subText2.Border = 3;
            subText2.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal2.AddCell(subText2);

            PdfPCell sub12 = new PdfPCell(new Phrase("17", fuente6));
            sub12.HorizontalAlignment = Element.ALIGN_CENTER;
            sub12.Border = 3;
            sub12.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal2.AddCell(sub12);

            PdfPCell sub22 = new PdfPCell(new Phrase("153", fuente6));
            sub22.HorizontalAlignment = Element.ALIGN_CENTER;
            sub22.Border = 3;
            sub22.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal2.AddCell(sub22);

            PdfPCell sub32 = new PdfPCell(new Phrase("$ 2,965,500.00", fuente6));
            sub32.HorizontalAlignment = Element.ALIGN_CENTER;
            sub32.Border = 3;
            sub32.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal2.AddCell(sub32);

            PdfPCell sub42 = new PdfPCell(new Phrase("$ 6,500.00", fuente6));
            sub42.HorizontalAlignment = Element.ALIGN_CENTER;
            sub42.Border = 3;
            sub42.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal2.AddCell(sub42);

            PdfPCell sub52 = new PdfPCell(new Phrase("$ 12,500.00", fuente6));
            sub52.HorizontalAlignment = Element.ALIGN_CENTER;
            sub52.Border = 3;
            sub52.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal2.AddCell(sub52);

            PdfPCell sub62 = new PdfPCell(new Phrase("$ 6,000.00", fuente6));
            sub62.HorizontalAlignment = Element.ALIGN_CENTER;
            sub62.Border = 3;
            sub62.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal2.AddCell(sub62);

            PdfPCell submoragral2 = new PdfPCell(new Phrase("2.79%", fuente6));
            submoragral2.HorizontalAlignment = Element.ALIGN_CENTER;
            submoragral2.BackgroundColor = new iTextSharp.text.BaseColor(223, 165, 165);
            submoragral2.Border = 3;
            submoragral2.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal2.AddCell(submoragral2);

            PdfPCell submoraSuc2 = new PdfPCell(new Phrase(" ", fuente6));
            submoraSuc2.HorizontalAlignment = Element.ALIGN_CENTER;
            submoraSuc2.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            subtotal2.AddCell(submoraSuc2);

            documento.Add(subtotal2);

            documento.Add(new Paragraph(" "));

            PdfPTable totalfi = new PdfPTable(9);
            totalfi.DefaultCell.Border = 0;
            int[] totalficellwidth = { 20, 10, 10, 10, 10, 10, 10, 10, 10 };
            totalfi.SetWidths(totalficellwidth);
            totalfi.WidthPercentage = 100f;
            totalfi.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell totText = (new PdfPCell(new Phrase("TOTAL", fuente6)));
            totText.HorizontalAlignment = Element.ALIGN_CENTER;
            totText.Border = 3;
            totText.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            totalfi.AddCell(totText);

            PdfPCell tot1 = new PdfPCell(new Phrase("37", fuente6));
            tot1.HorizontalAlignment = Element.ALIGN_CENTER;
            tot1.Border = 3;
            tot1.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            totalfi.AddCell(tot1);

            PdfPCell tot2 = new PdfPCell(new Phrase("299", fuente6));
            tot2.HorizontalAlignment = Element.ALIGN_CENTER;
            tot2.Border = 3;
            tot2.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            totalfi.AddCell(tot2);

            PdfPCell tot3 = new PdfPCell(new Phrase("$ 5,302,150.00", fuente6));
            tot3.HorizontalAlignment = Element.ALIGN_CENTER;
            tot3.Border = 3;
            tot3.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            totalfi.AddCell(tot3);

            PdfPCell tot4 = new PdfPCell(new Phrase("$ 107,800.00", fuente6));
            tot4.HorizontalAlignment = Element.ALIGN_CENTER;
            tot4.Border = 3;
            tot4.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            totalfi.AddCell(tot4);

            PdfPCell tot5 = new PdfPCell(new Phrase("$ 447,800.00", fuente6));
            tot5.HorizontalAlignment = Element.ALIGN_CENTER;
            tot5.Border = 3;
            tot5.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            totalfi.AddCell(tot5);

            PdfPCell tot6 = new PdfPCell(new Phrase("$ 340,000.00", fuente6));
            tot6.HorizontalAlignment = Element.ALIGN_CENTER;
            tot6.Border = 3;
            tot6.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            totalfi.AddCell(tot6);

            PdfPCell totmoragral = new PdfPCell(new Phrase("100%", fuente6));
            totmoragral.HorizontalAlignment = Element.ALIGN_CENTER;
            totmoragral.BackgroundColor = new iTextSharp.text.BaseColor(223, 165, 165);
            totmoragral.Border = 3;
            totmoragral.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            totalfi.AddCell(totmoragral);

            PdfPCell totmoraSuc2 = new PdfPCell(new Phrase("8.45%", fuente6));
            totmoraSuc2.HorizontalAlignment = Element.ALIGN_CENTER;
            totmoraSuc2.BackgroundColor = new iTextSharp.text.BaseColor(146, 163, 209);
            totalfi.AddCell(totmoraSuc2);

            documento.Add(totalfi);*/


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