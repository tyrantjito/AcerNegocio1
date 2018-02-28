using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using E_Utilities;

public partial class ControlAportacionesSolidarias : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SolicitudCredito grp = new SolicitudCredito();
        int[] sesiones = obtieneSesiones();
        grp.grupo = sesiones[4];
        grp.recuperagrupo();
        if (Convert.ToBoolean(grp.retorno[0]))
        {
            DataSet ds = (DataSet)grp.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                Label15.Text = r[0].ToString();
            }
        }
    }
    private int[] obtieneSesiones()
    {
        int[] sesiones = new int[5];
        sesiones[0] = Convert.ToInt32(Request.QueryString["u"]);
        sesiones[1] = Convert.ToInt32(Request.QueryString["p"]);
        sesiones[2] = Convert.ToInt32(Request.QueryString["e"]);
        sesiones[3] = Convert.ToInt32(Request.QueryString["t"]);
        sesiones[4] = Convert.ToInt32(Request.QueryString["c"]);
        return sesiones;

    }

    protected void lnkImprimirControlAportacionesSolidarias_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;

        // Tipo de Font que vamos utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();


        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER.Rotate());
        documento.AddTitle("CONTROL DE APORTACIONES SOLIDARIAS");
        documento.AddCreator("DESARROLLARTE");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\Cotrol_deee_Aportaciones_Solidarias_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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

            //Insertar logo o imagen  

            string imagepath = HttpContext.Current.Server.MapPath("img/");
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagepath + "logo_aser.png");
            logo.WidthPercentage = 15f;

            //encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.SetWidths(new float[] { 2f, 8f });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;


            PdfPCell titulo = new PdfPCell(new Phrase("APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + "CONTROL DE APORTACIONES SOLIDARIAS", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(logo);
            tablaEncabezado.AddCell(titulo);
            documento.Add(tablaEncabezado);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            //PRIMERA TABLA
            PdfPTable encab = new PdfPTable(10);
            encab.SetWidths(new float[] { 10, 20, 10, 8, 10, 8, 10, 8,10,6 });
            encab.DefaultCell.Border = 0;
            encab.WidthPercentage = 100f;

            ConApsol infor = new ConApsol();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = sesiones[4];
            infor.obtieneInfoEncabezado();

            string grupo = "";
            int numeroGrupo = 0;
            int numeroCred = 0;
            int cicloGru = 0;
            string fechaini = "";
            DateTime fechafin;
            int plazo = 0;
            string ofec = "";
            int plazosum = 0;
            DateTime sumafecha;


            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    grupo = Convert.ToString(r[6]);
                    numeroGrupo = Convert.ToInt32(r[7]);
                    numeroCred = Convert.ToInt32(r[2]);
                    cicloGru = Convert.ToInt32(r[17]);
                    fechaini = Convert.ToString(r[5]);
                    plazo = Convert.ToInt32(r[14]);
                    plazosum = plazo * 7;
                    fechafin = Convert.ToDateTime(fechaini);
                    fechafin = fechafin.AddDays(plazosum);
                    ofec = Convert.ToString(fechafin);


                }
            }

            PdfPCell gruPro = new PdfPCell(new Phrase("GRUPO PRODUCTIVO:", fuente6));
            gruPro.HorizontalAlignment = Element.ALIGN_CENTER;
            gruPro.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab.AddCell(gruPro);

            PdfPCell gruPro1 = new PdfPCell(new Phrase(" "+grupo, fuente6));
            gruPro1.HorizontalAlignment = Element.ALIGN_CENTER;
            encab.AddCell(gruPro1);

            PdfPCell noGru = new PdfPCell(new Phrase("NUM. DE GRUPO:", fuente6));
            noGru.HorizontalAlignment = Element.ALIGN_CENTER;
            noGru.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab.AddCell(noGru);

            PdfPCell noGru1 = new PdfPCell(new Phrase(" "+numeroGrupo, fuente6));
            noGru1.HorizontalAlignment = Element.ALIGN_CENTER;
            encab.AddCell(noGru1);

            PdfPCell numCre = new PdfPCell(new Phrase("NUM. DE CRÉDITO:", fuente6));
            numCre.HorizontalAlignment = Element.ALIGN_CENTER;
            numCre.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab.AddCell(numCre);

            PdfPCell numCre1 = new PdfPCell(new Phrase(" "+numeroCred, fuente6));
            numCre1.HorizontalAlignment = Element.ALIGN_CENTER;
            encab.AddCell(numCre1);

            PdfPCell ciclo = new PdfPCell(new Phrase("CICLO:", fuente6));
            ciclo.HorizontalAlignment = Element.ALIGN_CENTER;
            ciclo.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab.AddCell(ciclo);

            PdfPCell ciclo1 = new PdfPCell(new Phrase(" "+cicloGru, fuente6));
            ciclo1.HorizontalAlignment = Element.ALIGN_CENTER;
            encab.AddCell(ciclo1);

            PdfPCell sucur = new PdfPCell(new Phrase("SUCURSAL:", fuente6));
            sucur.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab.AddCell(sucur);

            PdfPCell sucur1 = new PdfPCell(new Phrase(" ", fuente6));
            sucur1.HorizontalAlignment = Element.ALIGN_CENTER;
            encab.AddCell(sucur1);

            documento.Add(encab);

            PdfPTable encab2 = new PdfPTable(4);
            encab2.SetWidths(new float[] { 10, 15, 10, 15 });
            encab2.DefaultCell.Border = 0;
            encab2.WidthPercentage = 70f;
            encab2.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell fechIn = new PdfPCell(new Phrase("FECHA DE INICIO:", fuente6));
            fechIn.HorizontalAlignment = Element.ALIGN_CENTER;
            fechIn.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab2.AddCell(fechIn);

            PdfPCell fechIn1 = new PdfPCell(new Phrase(" "+ Convert.ToDateTime(fechaini).ToString("dd/MM/yyyy"), fuente6));
            fechIn1.HorizontalAlignment = Element.ALIGN_CENTER;
            encab2.AddCell(fechIn1);

            PdfPCell fechNf = new PdfPCell(new Phrase("FECHA DE TERMINO:", fuente6));
            fechNf.HorizontalAlignment = Element.ALIGN_CENTER;
            fechNf.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab2.AddCell(fechNf);

            PdfPCell fechNf1 = new PdfPCell(new Phrase(" "+ Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6));
            fechNf1.HorizontalAlignment = Element.ALIGN_CENTER;
            encab2.AddCell(fechNf1);
            documento.Add(encab2);



            documento.Add(new Paragraph(" "));


            //tabla grande
            PdfPTable tabG = new PdfPTable(18);
            tabG.SetWidths(new float[] { 2, 18, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 });
            tabG.WidthPercentage = 100f;
            tabG.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell harry1 = (new PdfPCell(new Phrase("N°", fuente6)) { Rowspan = 3 });
            harry1.HorizontalAlignment = Element.ALIGN_CENTER;
            harry1.BackgroundColor = BaseColor.LIGHT_GRAY;
            harry1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(harry1);

            PdfPCell potter1 = (new PdfPCell(new Phrase("Nombre del Cliente", fuente8)) { Rowspan = 3 });
            potter1.HorizontalAlignment = Element.ALIGN_CENTER;
            potter1.BackgroundColor = BaseColor.LIGHT_GRAY;
            potter1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(potter1);

            PdfPCell Ase1 = (new PdfPCell(new Phrase("Semana 1", fuente6)) { Colspan = 2 });
            Ase1.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase1.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Ase1);

            PdfPCell Ase2 = (new PdfPCell(new Phrase("Semana 2", fuente6)) { Colspan = 2 });
            Ase2.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase2.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase2.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Ase2);

            PdfPCell Ase3 = (new PdfPCell(new Phrase("Semana 3", fuente6)) { Colspan = 2 });
            Ase3.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase3.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase3.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Ase3);

            PdfPCell Ase4 = (new PdfPCell(new Phrase("Semana 4", fuente6)) { Colspan = 2 });
            Ase4.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase4.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase4.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Ase4);

            PdfPCell Ase5 = (new PdfPCell(new Phrase("Semana 5", fuente6)) { Colspan = 2 });
            Ase5.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase5.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase5.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Ase5);

            PdfPCell Ase6 = (new PdfPCell(new Phrase("Semana 6", fuente6)) { Colspan = 2 });
            Ase6.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase6.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase6.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Ase6);

            PdfPCell Ase7 = (new PdfPCell(new Phrase("Semana 7", fuente6)) { Colspan = 2 });
            Ase7.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase7.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase7.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Ase7);

            PdfPCell Ase8 = (new PdfPCell(new Phrase("Semana 8", fuente6)) { Colspan = 2 });
            Ase8.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase8.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase8.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Ase8);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec1 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec1.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec1.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Afec1);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec2 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec2.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec2.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec2.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Afec2);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec3 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec3.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec3.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec3.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Afec3);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec4 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec4.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec4.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec4.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Afec4);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec5 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec5.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec5.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec5.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Afec5);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec6 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec6.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec6.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec6.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Afec6);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec7 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec7.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec7.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec7.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Afec7);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec8 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec8.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec8.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec8.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Afec8);

            PdfPCell Aap1 = (new PdfPCell(new Phrase(" AP", fuente6)));
            Aap1.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap1.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Aap1);

            PdfPCell Adev1 = (new PdfPCell(new Phrase(" DEV", fuente6)));
            Adev1.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev1.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Adev1);

            PdfPCell Aap2 = (new PdfPCell(new Phrase("AP ", fuente6)));
            Aap2.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap2.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap2.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Aap2);

            PdfPCell Adev2 = (new PdfPCell(new Phrase("DEV ", fuente6)));
            Adev2.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev2.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev2.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Adev2);

            PdfPCell Aap3 = (new PdfPCell(new Phrase(" AP", fuente6)));
            Aap3.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap3.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap3.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Aap3);

            PdfPCell Adev3 = (new PdfPCell(new Phrase("DEV ", fuente6)));
            Adev3.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev3.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev3.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Adev3);

            PdfPCell Aap4 = (new PdfPCell(new Phrase(" AP", fuente6)));
            Aap4.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap4.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap4.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Aap4);

            PdfPCell Adev4 = (new PdfPCell(new Phrase("DEV ", fuente6)));
            Adev4.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev4.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev4.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Adev4);

            PdfPCell Aap5 = (new PdfPCell(new Phrase("AP ", fuente6)));
            Aap5.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap5.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap5.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Aap5);

            PdfPCell Adev5 = (new PdfPCell(new Phrase("DEV ", fuente6)));
            Adev5.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev5.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev5.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Adev5);

            PdfPCell Aap6 = (new PdfPCell(new Phrase("AP ", fuente6)));
            Aap6.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap6.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap6.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Aap6);

            PdfPCell Adev6 = (new PdfPCell(new Phrase("DEV ", fuente6)));
            Adev6.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev6.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev6.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Adev6);

            PdfPCell Aap7 = (new PdfPCell(new Phrase(" AP", fuente6)));
            Aap7.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap7.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap7.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Aap7);

            PdfPCell Adev7 = (new PdfPCell(new Phrase(" DEV", fuente6)));
            Adev7.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev7.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev7.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Adev7);

            PdfPCell Aap8 = (new PdfPCell(new Phrase(" AP", fuente6)));
            Aap8.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap8.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap8.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Aap8);

            PdfPCell Adev8 = (new PdfPCell(new Phrase("DEV ", fuente6)));
            Adev8.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev8.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev8.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG.AddCell(Adev8);
            documento.Add(tabG);

            PdfPTable tabAg = new PdfPTable(18);
            tabAg.SetWidths(new float[] { 2, 18, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 });
            tabAg.WidthPercentage = 100f;
            tabAg.HorizontalAlignment = Element.ALIGN_LEFT;

            infor.obtieneInfoDetalle();
            string nombrecli = "";
            int num = 1;

            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];

                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    nombrecli = Convert.ToString(r[0]);


                    PdfPCell DAT1 = (new PdfPCell(new Phrase(" " + num, fuente6)));
                    DAT1.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT1);

                    PdfPCell DAT2 = (new PdfPCell(new Phrase(" "+nombrecli, fuente6)));
                    DAT2.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT2.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT2);

                    PdfPCell DAT3 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT3.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT3.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT3);

                    PdfPCell DAT4 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT4.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT4.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT4);

                    PdfPCell DAT5 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT5.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT5.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT5);

                    PdfPCell DAT6 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT6.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT6.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT6);

                    PdfPCell DAT7 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT7.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT7.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT7);

                    PdfPCell DAT8 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT8.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT8.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT8);

                    PdfPCell DAT9 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT9.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT9.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT9);

                    PdfPCell DAT10 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT10.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT10.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT10);

                    PdfPCell DAT11 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT11.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT11.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT11);

                    PdfPCell DAT12 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT12.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT12.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT12);

                    PdfPCell DAT13 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT13.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT13.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT13);

                    PdfPCell DAT14 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT14.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT14.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT14);

                    PdfPCell DAT15 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT15.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT15.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT15);

                    PdfPCell DAT16 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT16.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT16.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT16);

                    PdfPCell DAT17 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT17.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT17.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT17);

                    PdfPCell DAT18 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT18.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT18.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(DAT18);

                    num++;

                }
            }

            PdfPTable firmpie = new PdfPTable(4);
            firmpie.SetWidths(new float[] { 25, 25, 25, 25 });
            firmpie.WidthPercentage = 100f;
            firmpie.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell horrocrux1 = (new PdfPCell(new Phrase("\n\n\n\n\n ___________________________ \n \n PRESIDENTA \n \n ", fuente8)));
            horrocrux1.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux1.VerticalAlignment = Element.ALIGN_MIDDLE;
            horrocrux1.BorderColor = BaseColor.BLUE;
            firmpie.AddCell(horrocrux1);

            PdfPCell horrocrux2 = (new PdfPCell(new Phrase("\n\n\n\n\n  ___________________________ \n \n TESORERA \n \n ", fuente8)));
            horrocrux2.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux2.VerticalAlignment = Element.ALIGN_MIDDLE;
            horrocrux2.BorderColor = BaseColor.BLUE;
            firmpie.AddCell(horrocrux2);

            PdfPCell horrocrux3 = (new PdfPCell(new Phrase("\n\n\n\n\n  ___________________________ \n \n SECRETARIA \n \n ", fuente8)));
            horrocrux3.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux3.VerticalAlignment = Element.ALIGN_MIDDLE;
            horrocrux3.BorderColor = BaseColor.BLUE;
            firmpie.AddCell(horrocrux3);

            PdfPCell horrocrux4 = (new PdfPCell(new Phrase("\n\n\n\n\n  ___________________________ \n \n SUPERVISORA \n \n ", fuente8)));
            horrocrux4.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux4.VerticalAlignment = Element.ALIGN_MIDDLE;
            horrocrux4.BorderColor = BaseColor.BLUE;
            firmpie.AddCell(horrocrux4);




            documento.Add(tabAg);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));



            documento.Add(firmpie);
           documento.Add(new Paragraph(" "));
           documento.NewPage();
           documento.Add(tablaEncabezado);
           documento.Add(new Paragraph(" "));
           documento.Add(encab);
           documento.Add(encab2);
           documento.Add(new Paragraph(" "));

            PdfPTable tabG1 = new PdfPTable(18);
            tabG1.SetWidths(new float[] { 2, 18, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 });
            tabG1.WidthPercentage = 100f;
            tabG1.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell harry12 = (new PdfPCell(new Phrase("N°", fuente6)) { Rowspan = 3 });
            harry12.HorizontalAlignment = Element.ALIGN_CENTER;
            harry12.BackgroundColor = BaseColor.LIGHT_GRAY;
            harry12.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(harry12);

            PdfPCell potter12 = (new PdfPCell(new Phrase("Nombre del Cliente", fuente8)) { Rowspan = 3 });
            potter12.HorizontalAlignment = Element.ALIGN_CENTER;
            potter12.BackgroundColor = BaseColor.LIGHT_GRAY;
            potter12.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(potter12);

            PdfPCell Ase12 = (new PdfPCell(new Phrase("Semana 9", fuente6)) { Colspan = 2 });
            Ase12.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase12.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase12.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Ase12);

            PdfPCell Ase22 = (new PdfPCell(new Phrase("Semana 10", fuente6)) { Colspan = 2 });
            Ase22.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase22.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase22.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Ase22);

            PdfPCell Ase32 = (new PdfPCell(new Phrase("Semana 11", fuente6)) { Colspan = 2 });
            Ase32.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase32.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase32.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Ase32);

            PdfPCell Ase42 = (new PdfPCell(new Phrase("Semana 12", fuente6)) { Colspan = 2 });
            Ase42.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase42.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase42.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Ase42);

            PdfPCell Ase52 = (new PdfPCell(new Phrase("Semana 13", fuente6)) { Colspan = 2 });
            Ase52.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase52.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase52.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Ase52);

            PdfPCell Ase62 = (new PdfPCell(new Phrase("Semana 14", fuente6)) { Colspan = 2 });
            Ase62.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase62.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase62.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Ase62);

            PdfPCell Ase72 = (new PdfPCell(new Phrase("Semana 15", fuente6)) { Colspan = 2 });
            Ase72.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase72.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase72.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Ase72);

            PdfPCell Ase82 = (new PdfPCell(new Phrase("Semana 16", fuente6)) { Colspan = 2 });
            Ase82.HorizontalAlignment = Element.ALIGN_CENTER;
            Ase82.BackgroundColor = BaseColor.LIGHT_GRAY;
            Ase82.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Ase82);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec22 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec22.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec22.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec22.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Afec22);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec21 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec21.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec21.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec21.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Afec21);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec32 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec32.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec32.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec32.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Afec32);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec42 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec42.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec42.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec42.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Afec42);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec52 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec52.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec52.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec52.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Afec52);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec62 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec62.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec62.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec62.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Afec62);

            sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec72 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec72.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec72.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec72.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Afec72);

             sumafecha = Convert.ToDateTime(ofec);
            sumafecha = sumafecha.AddDays(7);
            ofec = Convert.ToString(sumafecha);

            PdfPCell Afec82 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 2 });
            Afec82.HorizontalAlignment = Element.ALIGN_CENTER;
            Afec82.BackgroundColor = BaseColor.LIGHT_GRAY;
            Afec82.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Afec82);

            PdfPCell Aap12 = (new PdfPCell(new Phrase(" AP", fuente6)));
            Aap12.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap12.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap12.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Aap12);

            PdfPCell Adev12 = (new PdfPCell(new Phrase(" DEV", fuente6)));
            Adev12.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev12.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev12.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Adev12);

            PdfPCell Aap22 = (new PdfPCell(new Phrase("AP ", fuente6)));
            Aap22.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap22.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap22.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Aap22);

            PdfPCell Adev22 = (new PdfPCell(new Phrase("DEV ", fuente6)));
            Adev22.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev22.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev22.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Adev22);

            PdfPCell Aap32 = (new PdfPCell(new Phrase(" AP", fuente6)));
            Aap32.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap32.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap32.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Aap32);

            PdfPCell Adev32 = (new PdfPCell(new Phrase("DEV ", fuente6)));
            Adev32.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev32.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev32.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Adev32);

            PdfPCell Aap42 = (new PdfPCell(new Phrase(" AP", fuente6)));
            Aap42.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap42.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap42.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Aap42);

            PdfPCell Adev42 = (new PdfPCell(new Phrase("DEV ", fuente6)));
            Adev42.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev42.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev42.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Adev42);

            PdfPCell Aap52 = (new PdfPCell(new Phrase("AP ", fuente6)));
            Aap52.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap52.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap52.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Aap52);

            PdfPCell Adev52 = (new PdfPCell(new Phrase("DEV ", fuente6)));
            Adev52.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev52.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev52.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Adev52);

            PdfPCell Aap62 = (new PdfPCell(new Phrase("AP ", fuente6)));
            Aap62.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap62.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap62.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Aap62);

            PdfPCell Adev62 = (new PdfPCell(new Phrase("DEV ", fuente6)));
            Adev62.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev62.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev62.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Adev62);

            PdfPCell Aap72 = (new PdfPCell(new Phrase(" AP", fuente6)));
            Aap72.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap72.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap72.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Aap72);

            PdfPCell Adev72 = (new PdfPCell(new Phrase(" DEV", fuente6)));
            Adev72.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev72.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev72.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Adev72);

            PdfPCell Aap82 = (new PdfPCell(new Phrase(" AP", fuente6)));
            Aap82.HorizontalAlignment = Element.ALIGN_CENTER;
            Aap82.BackgroundColor = BaseColor.LIGHT_GRAY;
            Aap82.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Aap82);

            PdfPCell Adev82 = (new PdfPCell(new Phrase("DEV ", fuente6)));
            Adev82.HorizontalAlignment = Element.ALIGN_CENTER;
            Adev82.BackgroundColor = BaseColor.LIGHT_GRAY;
            Adev82.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabG1.AddCell(Adev82);
            documento.Add(tabG1);

            PdfPTable tabAg1 = new PdfPTable(10);
            tabAg1.SetWidths(new float[] { 2, 18, 8, 8, 8, 8, 8, 8, 8, 8 });
            tabAg1.WidthPercentage = 100f;
            tabAg1.HorizontalAlignment = Element.ALIGN_LEFT;

            infor.obtieneInfoDetalle();
            string nombrecli2 = "";
            int num2 = 1;

            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];

                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    nombrecli2 = Convert.ToString(r[0]);

                    PdfPCell ADAT1 = (new PdfPCell(new Phrase(" "+num2, fuente6)));
                    ADAT1.HorizontalAlignment = Element.ALIGN_CENTER;
                    ADAT1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg1.AddCell(ADAT1);

                    PdfPCell ADAT2 = (new PdfPCell(new Phrase(" "+nombrecli2, fuente6)));
                    ADAT2.HorizontalAlignment = Element.ALIGN_CENTER;
                    ADAT2.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg1.AddCell(ADAT2);

                    PdfPCell ADAT3 = (new PdfPCell(new Phrase(" ", fuente6)));
                    ADAT3.HorizontalAlignment = Element.ALIGN_CENTER;
                    ADAT3.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg1.AddCell(ADAT3);

                    PdfPCell ADAT4 = (new PdfPCell(new Phrase(" ", fuente6)));
                    ADAT4.HorizontalAlignment = Element.ALIGN_CENTER;
                    ADAT4.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg1.AddCell(ADAT4);

                    PdfPCell ADAT5 = (new PdfPCell(new Phrase(" ", fuente6)));
                    ADAT5.HorizontalAlignment = Element.ALIGN_CENTER;
                    ADAT5.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg1.AddCell(ADAT5);

                    PdfPCell ADAT6 = (new PdfPCell(new Phrase(" ", fuente6)));
                    ADAT6.HorizontalAlignment = Element.ALIGN_CENTER;
                    ADAT6.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg1.AddCell(ADAT6);

                    PdfPCell ADAT7 = (new PdfPCell(new Phrase(" ", fuente6)));
                    ADAT7.HorizontalAlignment = Element.ALIGN_CENTER;
                    ADAT7.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg1.AddCell(ADAT7);

                    PdfPCell ADAT8 = (new PdfPCell(new Phrase(" ", fuente6)));
                    ADAT8.HorizontalAlignment = Element.ALIGN_CENTER;
                    ADAT8.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg1.AddCell(ADAT8);

                    PdfPCell ADAT9 = (new PdfPCell(new Phrase(" ", fuente6)));
                    ADAT9.HorizontalAlignment = Element.ALIGN_CENTER;
                    ADAT9.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg1.AddCell(ADAT9);

                    PdfPCell ADAT10 = (new PdfPCell(new Phrase(" ", fuente6)));
                    ADAT10.HorizontalAlignment = Element.ALIGN_CENTER;
                    ADAT10.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg1.AddCell(ADAT10);
                    num2++;
                }
            }

            documento.Add(tabAg1);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(firmpie);

            documento.Add(new Paragraph(" "));

           
            documento.Close();




            FileInfo filename = new FileInfo(archivo);
            if (filename.Exists)
            {
                string url = "Descargas.aspx?filename=" + filename.Name;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "pdfs", "window.open('" + url + "', 'nuevo', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, titlebar=no, fullscreen=yes,resizable=yes');", true);
            }

        }
    }
}