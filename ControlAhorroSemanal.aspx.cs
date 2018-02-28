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

public partial class ControlAhorroSemanal : System.Web.UI.Page
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

    protected void lnkImprimirControlAhorroSemanal_Click(object sender, EventArgs e)
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
        documento.AddTitle("CONTROL DE AHORRO SEMANAL");
        documento.AddCreator("AserNegocio");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\Cotrol_d_Ahorro_Semanal_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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


            PdfPCell titulo = new PdfPCell(new Phrase("ASESORIA Y SERVICIO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + "CONTROL DE AHORRO SEMANAL", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(logo);
            tablaEncabezado.AddCell(titulo);
            documento.Add(tablaEncabezado);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PagGrup infor = new PagGrup();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = Convert.ToInt32(Label15.Text);
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
            int num = 0;


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

                    //PRIMERA TABLA
                    PdfPTable encab = new PdfPTable(8);
            encab.SetWidths(new float[] { 10, 30, 10, 10, 10, 10, 10, 10 });
            encab.DefaultCell.Border = 0;
            encab.WidthPercentage = 100f;

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
            documento.Add(encab);

            PdfPTable encab2 = new PdfPTable(4);
            encab2.SetWidths(new float[] { 10, 15, 10, 15 });
            encab2.DefaultCell.Border = 0;
            encab2.WidthPercentage = 70f;
            encab2.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell fechIn = new PdfPCell(new Phrase("FECHA DE INICIO:", fuente6));
            fechIn.HorizontalAlignment = Element.ALIGN_CENTER;
            fechIn.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab2.AddCell(fechIn);

            PdfPCell fechIn1 = new PdfPCell(new Phrase(" " + Convert.ToDateTime(fechaini).ToString("dd/MM/yyyy"), fuente6));
            fechIn1.HorizontalAlignment = Element.ALIGN_CENTER;
            encab2.AddCell(fechIn1);

            PdfPCell fechNf = new PdfPCell(new Phrase("FECHA DE TERMINO:", fuente6));
            fechNf.HorizontalAlignment = Element.ALIGN_CENTER;
            fechNf.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab2.AddCell(fechNf);

            PdfPCell fechNf1 = new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6));
            fechNf1.HorizontalAlignment = Element.ALIGN_CENTER;
            encab2.AddCell(fechNf1);
            documento.Add(encab2);
            documento.Add(new Paragraph(" "));
           
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = sesiones[4];
            infor.obtieneInfoDetalle();

            string nombrecli = "";

          

            PdfPTable tab = new PdfPTable(12);
            tab.SetWidths(new float[] { 2, 28, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 });
            tab.DefaultCell.Border = 0;
            tab.WidthPercentage = 100f;
            tab.HorizontalAlignment = Element.ALIGN_LEFT;


          

            PdfPCell no = (new PdfPCell(new Phrase("No.", fuente6)) { Rowspan = 2 });
            no.HorizontalAlignment = Element.ALIGN_CENTER;
            no.BackgroundColor = BaseColor.LIGHT_GRAY;
            no.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(no);

            PdfPCell nomb = (new PdfPCell(new Phrase("NOMBRE DEL CLIENTE", fuente6)) { Rowspan = 2 });
            nomb.HorizontalAlignment = Element.ALIGN_CENTER;
            nomb.BackgroundColor = BaseColor.LIGHT_GRAY;
            nomb.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(nomb);

            PdfPCell ahoMin = (new PdfPCell(new Phrase("AHORRO MÍNIMO", fuente6)) { Rowspan = 2 });
            ahoMin.HorizontalAlignment = Element.ALIGN_CENTER;
            ahoMin.BackgroundColor = BaseColor.LIGHT_GRAY;
            ahoMin.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(ahoMin);

            PdfPCell sem1 = (new PdfPCell(new Phrase("Sem 1", fuente6)));
            sem1.HorizontalAlignment = Element.ALIGN_CENTER;
            sem1.BackgroundColor = BaseColor.LIGHT_GRAY;
            sem1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(sem1);

            PdfPCell sem2 = (new PdfPCell(new Phrase("Sem 2", fuente6)));
            sem2.HorizontalAlignment = Element.ALIGN_CENTER;
            sem2.BackgroundColor = BaseColor.LIGHT_GRAY;
            sem2.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(sem2);

            PdfPCell sem3 = (new PdfPCell(new Phrase("Sem 3", fuente6)));
            sem3.HorizontalAlignment = Element.ALIGN_CENTER;
            sem3.BackgroundColor = BaseColor.LIGHT_GRAY;
            sem3.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(sem3);

            PdfPCell sem4 = (new PdfPCell(new Phrase("Sem 4", fuente6)));
            sem4.HorizontalAlignment = Element.ALIGN_CENTER;
            sem4.BackgroundColor = BaseColor.LIGHT_GRAY;
            sem4.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(sem4);

            PdfPCell sem5 = (new PdfPCell(new Phrase("Sem 5", fuente6)));
            sem5.HorizontalAlignment = Element.ALIGN_CENTER;
            sem5.BackgroundColor = BaseColor.LIGHT_GRAY;
            sem5.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(sem5);

            PdfPCell sem6 = (new PdfPCell(new Phrase("Sem 6", fuente6)));
            sem6.HorizontalAlignment = Element.ALIGN_CENTER;
            sem6.BackgroundColor = BaseColor.LIGHT_GRAY;
            sem6.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(sem6);

            PdfPCell sem7 = (new PdfPCell(new Phrase("Sem 7", fuente6)));
            sem7.HorizontalAlignment = Element.ALIGN_CENTER;
            sem7.BackgroundColor = BaseColor.LIGHT_GRAY;
            sem7.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(sem7);

            PdfPCell sem8 = (new PdfPCell(new Phrase("Sem 8", fuente6)));
            sem8.HorizontalAlignment = Element.ALIGN_CENTER;
            sem8.BackgroundColor = BaseColor.LIGHT_GRAY;
            sem8.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(sem8);

            PdfPCell subT = (new PdfPCell(new Phrase("SUBTOTAL", fuente6)));
            subT.HorizontalAlignment = Element.ALIGN_CENTER;
            subT.BackgroundColor = BaseColor.LIGHT_GRAY;
            subT.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(subT);

            PdfPCell fec1 = (new PdfPCell(new Phrase(" ", fuente6)));
            fec1.HorizontalAlignment = Element.ALIGN_CENTER;
            fec1.BackgroundColor = BaseColor.LIGHT_GRAY;
            fec1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(fec1);

            PdfPCell fec2 = (new PdfPCell(new Phrase(" ", fuente6)));
            fec2.HorizontalAlignment = Element.ALIGN_CENTER;
            fec2.BackgroundColor = BaseColor.LIGHT_GRAY;
            fec2.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(fec2);

            PdfPCell fec3 = (new PdfPCell(new Phrase(" ", fuente6)));
            fec3.HorizontalAlignment = Element.ALIGN_CENTER;
            fec3.BackgroundColor = BaseColor.LIGHT_GRAY;
            fec3.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(fec3);

            PdfPCell fec4 = (new PdfPCell(new Phrase(" ", fuente6)));
            fec4.HorizontalAlignment = Element.ALIGN_CENTER;
            fec4.BackgroundColor = BaseColor.LIGHT_GRAY;
            fec4.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(fec4);

            PdfPCell fec5 = (new PdfPCell(new Phrase(" ", fuente6)));
            fec5.HorizontalAlignment = Element.ALIGN_CENTER;
            fec5.BackgroundColor = BaseColor.LIGHT_GRAY;
            fec5.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(fec5);

            PdfPCell fec6 = (new PdfPCell(new Phrase(" ", fuente6)));
            fec6.HorizontalAlignment = Element.ALIGN_CENTER;
            fec6.BackgroundColor = BaseColor.LIGHT_GRAY;
            fec6.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(fec6);

            PdfPCell fec7 = (new PdfPCell(new Phrase(" ", fuente6)));
            fec7.HorizontalAlignment = Element.ALIGN_CENTER;
            fec7.BackgroundColor = BaseColor.LIGHT_GRAY;
            fec7.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(fec7);

            PdfPCell fec8 = (new PdfPCell(new Phrase(" ", fuente6)));
            fec8.HorizontalAlignment = Element.ALIGN_CENTER;
            fec8.BackgroundColor = BaseColor.LIGHT_GRAY;
            fec8.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(fec8);

            PdfPCell subT2 = (new PdfPCell(new Phrase(" ", fuente6)));
            subT2.HorizontalAlignment = Element.ALIGN_CENTER;
            subT2.BackgroundColor = BaseColor.LIGHT_GRAY;
            subT2.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(subT2);

            documento.Add(tab);
            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];

                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    nombrecli = Convert.ToString(r[0]);
                    num++;

                    PdfPTable tabAg = new PdfPTable(12);
                    tabAg.SetWidths(new float[] { 2, 28, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 });
                    tabAg.WidthPercentage = 100f;
                    tabAg.HorizontalAlignment = Element.ALIGN_LEFT;

                    PdfPCell dat1 = (new PdfPCell(new Phrase(" "+num, fuente6)));
                    dat1.HorizontalAlignment = Element.ALIGN_CENTER;
                    dat1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(dat1);

                    PdfPCell dat2 = (new PdfPCell(new Phrase(" " + nombrecli, fuente6)));
                    dat2.HorizontalAlignment = Element.ALIGN_CENTER;
                    dat2.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(dat2);

                    PdfPCell dat3 = (new PdfPCell(new Phrase(" ", fuente6)));
                    dat3.HorizontalAlignment = Element.ALIGN_CENTER;
                    dat3.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(dat3);

                    PdfPCell dat4 = (new PdfPCell(new Phrase(" ", fuente6)));
                    dat4.HorizontalAlignment = Element.ALIGN_CENTER;
                    dat4.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(dat4);


                    PdfPCell dat5 = (new PdfPCell(new Phrase(" ", fuente6)));
                    dat5.HorizontalAlignment = Element.ALIGN_CENTER;
                    dat5.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(dat5);

                    PdfPCell dat6 = (new PdfPCell(new Phrase(" ", fuente6)));
                    dat6.HorizontalAlignment = Element.ALIGN_CENTER;
                    dat6.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(dat6);

                    PdfPCell dat7 = (new PdfPCell(new Phrase(" ", fuente6)));
                    dat7.HorizontalAlignment = Element.ALIGN_CENTER;
                    dat7.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(dat7);

                    PdfPCell dat8 = (new PdfPCell(new Phrase(" ", fuente6)));
                    dat8.HorizontalAlignment = Element.ALIGN_CENTER;
                    dat8.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(dat8);

                    PdfPCell dat9 = (new PdfPCell(new Phrase(" ", fuente6)));
                    dat9.HorizontalAlignment = Element.ALIGN_CENTER;
                    dat9.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(dat9);

                    PdfPCell dat10 = (new PdfPCell(new Phrase(" ", fuente6)));
                    dat10.HorizontalAlignment = Element.ALIGN_CENTER;
                    dat10.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(dat10);

                    PdfPCell dat11 = (new PdfPCell(new Phrase(" ", fuente6)));
                    dat11.HorizontalAlignment = Element.ALIGN_CENTER;
                    dat11.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(dat11);

                    PdfPCell dat12 = (new PdfPCell(new Phrase(" ", fuente6)));
                    dat12.HorizontalAlignment = Element.ALIGN_CENTER;
                    dat12.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg.AddCell(dat12);

                    documento.Add(tabAg);



                }
            }


            documento.Add(new Paragraph(" "));

            /*PdfPTable tabA = new PdfPTable(12);
            tabA.SetWidths(new float[] {  2, 28, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 });
            tabA.DefaultCell.Border = 0;
            tabA.WidthPercentage = 100f;
            tabA.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell Bno = (new PdfPCell(new Phrase("No.", fuente6)) { Rowspan = 2 });
            Bno.HorizontalAlignment = Element.ALIGN_CENTER;
            Bno.BackgroundColor = BaseColor.LIGHT_GRAY;
            Bno.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabA.AddCell(Bno);

            PdfPCell nomA = (new PdfPCell(new Phrase("NOMBRE DEL CLIENTE", fuente6)) { Rowspan = 2 });
            nomA.HorizontalAlignment = Element.ALIGN_CENTER;
            nomA.BackgroundColor = BaseColor.LIGHT_GRAY;
            nomA.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabA.AddCell(nomA);

            PdfPCell glA = (new PdfPCell(new Phrase("OL", fuente6)) { Rowspan = 2 });
            glA.HorizontalAlignment = Element.ALIGN_CENTER;
            glA.BackgroundColor = BaseColor.LIGHT_GRAY;
            glA.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabA.AddCell(glA);

            PdfPCell saldA = (new PdfPCell(new Phrase("SALDO TOTAL", fuente6)) { Rowspan = 2 });
            saldA.HorizontalAlignment = Element.ALIGN_CENTER;
            saldA.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(saldA);

            PdfPCell pagSemA = (new PdfPCell(new Phrase("PAGO SEMANAL", fuente6)) { Rowspan = 2 });
            pagSemA.HorizontalAlignment = Element.ALIGN_CENTER;
            pagSemA.BackgroundColor = BaseColor.LIGHT_GRAY;
            pagSemA.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabA.AddCell(pagSemA);

            PdfPCell seA1 = (new PdfPCell(new Phrase("semana uno", fuente6)));
            seA1.HorizontalAlignment = Element.ALIGN_CENTER;
            seA1.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(seA1);

            PdfPCell seA2 = (new PdfPCell(new Phrase("Semana dos", fuente6)));
            seA2.HorizontalAlignment = Element.ALIGN_CENTER;
            seA2.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(seA2);

            PdfPCell seA3 = (new PdfPCell(new Phrase("Semana tres", fuente6)));
            seA3.HorizontalAlignment = Element.ALIGN_CENTER;
            seA3.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(seA3);

            PdfPCell seA4 = (new PdfPCell(new Phrase("Semana cuatro", fuente6)));
            seA4.HorizontalAlignment = Element.ALIGN_CENTER;
            seA4.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(seA4);

            PdfPCell seA5 = (new PdfPCell(new Phrase("Semana cinco", fuente6)));
            seA5.HorizontalAlignment = Element.ALIGN_CENTER;
            seA5.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(seA5);

            PdfPCell seA6 = (new PdfPCell(new Phrase("Semana seis", fuente6)));
            seA6.HorizontalAlignment = Element.ALIGN_CENTER;
            seA6.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(seA6);

            PdfPCell seA7 = (new PdfPCell(new Phrase("Semana siete", fuente6)));
            seA7.HorizontalAlignment = Element.ALIGN_CENTER;
            seA7.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(seA7);

            PdfPCell seA8 = (new PdfPCell(new Phrase("Semana ocho", fuente6)));
            seA8.HorizontalAlignment = Element.ALIGN_CENTER;
            seA8.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(seA8);

            PdfPCell fecA1 = (new PdfPCell(new Phrase("Fecha", fuente6)));
            fecA1.HorizontalAlignment = Element.ALIGN_CENTER;
            fecA1.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(fecA1);

            PdfPCell fecA2 = (new PdfPCell(new Phrase("Fecha", fuente6)));
            fecA2.HorizontalAlignment = Element.ALIGN_CENTER;
            fecA2.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(fecA2);

            PdfPCell fecA3 = (new PdfPCell(new Phrase("Fecha", fuente6)));
            fecA3.HorizontalAlignment = Element.ALIGN_CENTER;
            fecA3.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(fecA3);

            PdfPCell fecA4 = (new PdfPCell(new Phrase("Fecha", fuente6)));
            fecA4.HorizontalAlignment = Element.ALIGN_CENTER;
            fecA4.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(fecA4);

            PdfPCell fecA5 = (new PdfPCell(new Phrase("Fecha", fuente6)));
            fecA5.HorizontalAlignment = Element.ALIGN_CENTER;
            fecA5.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(fecA5);

            PdfPCell fecA6 = (new PdfPCell(new Phrase("Fecha", fuente6)));
            fecA6.HorizontalAlignment = Element.ALIGN_CENTER;
            fecA6.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(fecA6);

            PdfPCell fecA7 = (new PdfPCell(new Phrase("Fecha", fuente6)));
            fecA7.HorizontalAlignment = Element.ALIGN_CENTER;
            fecA7.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabA.AddCell(fecA7);*/


            documento.Add(new Paragraph(" "));

            PdfPTable firmpie = new PdfPTable(4);
            firmpie.SetWidths(new float[] { 25, 25, 25, 25 });
            firmpie.WidthPercentage = 100f;
            firmpie.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell horrocrux1 = (new PdfPCell(new Phrase("\n\n\n\n\n FIRMA ", fuente8)));
            horrocrux1.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux1.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux1);

            PdfPCell horrocrux2 = (new PdfPCell(new Phrase("\n\n\n\n\n FIRMA ", fuente8)));
            horrocrux2.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux2.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux2);

            PdfPCell horrocrux3 = (new PdfPCell(new Phrase("\n\n\n\n\n FIRMA ", fuente8)));
            horrocrux3.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux3.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux3);

            PdfPCell horrocrux4 = (new PdfPCell(new Phrase("\n\n\n\n\n FIRMA ", fuente8)));
            horrocrux4.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux4.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux4);

            PdfPCell horrocrux5 = (new PdfPCell(new Phrase("PRESIDENTA", fuente8)));
            horrocrux5.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux5.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux5);

            PdfPCell horrocrux6 = (new PdfPCell(new Phrase("SECRETARIA", fuente8)));
            horrocrux6.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux6.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux6);

            PdfPCell horrocrux7 = (new PdfPCell(new Phrase("TESORERA", fuente8)));
            horrocrux7.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux7.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux7);

            PdfPCell horrocrux8 = (new PdfPCell(new Phrase("SUPERVISORA", fuente8)));
            horrocrux8.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux8.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux8);
            documento.Add(firmpie);
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