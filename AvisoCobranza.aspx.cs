using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using E_Utilities;
using System.IO;

public partial class AvisoCobranza : System.Web.UI.Page
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
    protected void lnkImprimir_Click(object sender, EventArgs e)
    
    {
        LinkButton btnImprimir = (LinkButton)sender;
        // Tipo de Font que vamos utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" PRIMER AVISO ");
        documento.AddCreator("DESARROLLARTE");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\PRIMER_AVISO" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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
            //logo.WidthPercentage = 15f;


            //encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.SetWidths(new float[] { 2f, 8f });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;


            PdfPCell titulo = new PdfPCell(new Phrase("APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + " PRIMER AVISO DE COBRANZA ", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            //PdfPCell titulo = new PdfPCell(new Phrase("Gaarve S.A de C.V. " + Environment.NewLine + Environment.NewLine + " Informe de Nomina " + Environment.NewLine + Environment.NewLine + strDatosObra, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(logo);
            tablaEncabezado.AddCell(titulo);

            documento.Add(tablaEncabezado);
            documento.Add(new Paragraph(" "));

            PdfPTable titulo1 = new PdfPTable(1);
            titulo1.DefaultCell.Border = 0;
            titulo1.WidthPercentage = 100f;

            string fechahoy = "";
            fechahoy = fechas.obtieneFechaLocal().ToString("yyyy/MM/dd");

            PdfPCell aviso1 = new PdfPCell(new Phrase("PRIMER AVISO DE COBRANZA", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            aviso1.HorizontalAlignment = Element.ALIGN_CENTER;
            aviso1.Border = 0;
            aviso1.VerticalAlignment = Element.ALIGN_MIDDLE;
            titulo1.AddCell(aviso1);

            PdfPCell fecha1 = new PdfPCell(new Phrase("FECHA: "+fechahoy, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            fecha1.HorizontalAlignment = Element.ALIGN_RIGHT;
            fecha1.Border = 0;
            fecha1.VerticalAlignment = Element.ALIGN_MIDDLE;
            titulo1.AddCell(fecha1);

            documento.Add(titulo1);

            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPTable encabe = new PdfPTable(1);
            encabe.DefaultCell.Border = 0;
            encabe.WidthPercentage = 100f;
            encabe.HorizontalAlignment = Element.ALIGN_LEFT;

            AvCob infor = new AvCob();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = sesiones[4];
            infor.obtieneInfoEnca();

            string gerente2="";
            string grupo="";
            int idgrupo=0;
            decimal montaut = 0;

            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    gerente2 = Convert.ToString(r[0]);
                    grupo = Convert.ToString(r[1]);
                    idgrupo = Convert.ToInt32(r[2]);
                    montaut = Convert.ToDecimal(r[3]);

                }
            }

            PdfPCell gerente = new PdfPCell(new Phrase("CLIENTE: "+gerente2.ToUpper(), FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            gerente.HorizontalAlignment = Element.ALIGN_LEFT;
            gerente.Border = 0;
            gerente.VerticalAlignment = Element.ALIGN_MIDDLE;
            encabe.AddCell(gerente);

            PdfPCell grupPro = new PdfPCell(new Phrase("GRUPO PRODUCTIVO: "+grupo.ToUpper(), FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            grupPro.HorizontalAlignment = Element.ALIGN_LEFT;
            grupPro.Border = 0;
            grupPro.VerticalAlignment = Element.ALIGN_MIDDLE;
            encabe.AddCell(grupPro);


            PdfPCell numero = new PdfPCell(new Phrase("NÚMERO: "+idgrupo, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            numero.HorizontalAlignment = Element.ALIGN_LEFT;
            numero.Border = 0;
            numero.VerticalAlignment = Element.ALIGN_MIDDLE;
            encabe.AddCell(numero);

            documento.Add(encabe);

            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPTable txt1 = new PdfPTable(1);
            txt1.DefaultCell.Border = 0;
            txt1.WidthPercentage = 100F;
            Convertidores numText = new Convertidores();
            numText._importe = montaut.ToString();


            PdfPCell texto1 = new PdfPCell(new Phrase("Sirva la presente para comunicarle que a esta fecha el crédito que le fue autorizado por nuestra institución por la cantidad de  " + montaut.ToString("C2")  + "  "+numText.convierteMontoEnLetras().ToUpper().Trim() + " , registra un saldo pendiente de pago por la cantidad de_____________ \n \n \n En virtud de lo anterior lo exhortamos para que en un plazo no mayor a 5 (cinco) días natural, contados a partir de la recepción de este aviso, acuda a la oficina más cercana a su domicilio a regularizar su situación crediticia. \n \n \n Esperando una respuesta satisfactoria, quedo de usted. \n \n \n \n \n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            texto1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            texto1.Border = 0;
            texto1.VerticalAlignment = Element.ALIGN_MIDDLE;
            txt1.AddCell(texto1);

            PdfPCell atte = new PdfPCell(new Phrase("ATENTAMENTE", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            atte.HorizontalAlignment = Element.ALIGN_CENTER;
            atte.Border = 0;
            atte.VerticalAlignment = Element.ALIGN_MIDDLE;
            txt1.AddCell(atte);

            documento.Add(txt1);


            //documento.Add(new Paragraph(""));
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

    protected void lnkImprimir2_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;
        // Tipo de Font que vamos utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" SEGUNDO AVISO ");
        documento.AddCreator("DESARROLLARTE");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\SEGUNDO_AVISO" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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
            //logo.WidthPercentage = 15f;


            //encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.SetWidths(new float[] { 2f, 8f });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;


            PdfPCell titulo = new PdfPCell(new Phrase("APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + " SEGUNDO AVISO DE COBRANZA ", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            //PdfPCell titulo = new PdfPCell(new Phrase("Gaarve S.A de C.V. " + Environment.NewLine + Environment.NewLine + " Informe de Nomina " + Environment.NewLine + Environment.NewLine + strDatosObra, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(logo);
            tablaEncabezado.AddCell(titulo);

            documento.Add(tablaEncabezado);
            documento.Add(new Paragraph(" "));

            PdfPTable titulo1 = new PdfPTable(1);
            titulo1.DefaultCell.Border = 0;
            titulo1.WidthPercentage = 100f;

            string fechahoy = "";
            fechahoy = fechas.obtieneFechaLocal().ToString("yyyy/MM/dd");

            PdfPCell aviso1 = new PdfPCell(new Phrase("SEGUNDO AVISO DE COBRANZA", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            aviso1.HorizontalAlignment = Element.ALIGN_CENTER;
            aviso1.Border = 0;
            aviso1.VerticalAlignment = Element.ALIGN_MIDDLE;
            titulo1.AddCell(aviso1);

            PdfPCell fecha1 = new PdfPCell(new Phrase("FECHA: " + fechahoy, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            fecha1.HorizontalAlignment = Element.ALIGN_RIGHT;
            fecha1.Border = 0;
            fecha1.VerticalAlignment = Element.ALIGN_MIDDLE;
            titulo1.AddCell(fecha1);

            documento.Add(titulo1);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));



            PdfPTable encabe = new PdfPTable(1);
            encabe.DefaultCell.Border = 0;
            encabe.WidthPercentage = 100f;
            encabe.HorizontalAlignment = Element.ALIGN_LEFT;

            AvCob infor = new AvCob();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = sesiones[4];
            infor.obtieneInfoEnca();

            string gerente2 = "";
            string grupo = "";
            int idgrupo = 0;

            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    gerente2 = Convert.ToString(r[0]);
                    grupo = Convert.ToString(r[1]);
                    idgrupo = Convert.ToInt32(r[2]);

                }
            }

            PdfPCell gerente = new PdfPCell(new Phrase("CLIENTE: "+gerente2.ToUpper(), FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            gerente.HorizontalAlignment = Element.ALIGN_LEFT;
            gerente.Border = 0;
            gerente.VerticalAlignment = Element.ALIGN_MIDDLE;
            encabe.AddCell(gerente);

            PdfPCell grupPro = new PdfPCell(new Phrase("GRUPO PRODUCTIVO: "+grupo.ToUpper(), FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            grupPro.HorizontalAlignment = Element.ALIGN_LEFT;
            grupPro.Border = 0;
            grupPro.VerticalAlignment = Element.ALIGN_MIDDLE;
            encabe.AddCell(grupPro);


            PdfPCell numero = new PdfPCell(new Phrase("NÚMERO: "+idgrupo, FontFactory.GetFont("ARIAL",12, iTextSharp.text.Font.BOLD)));
            numero.HorizontalAlignment = Element.ALIGN_LEFT;
            numero.Border = 0;
            numero.VerticalAlignment = Element.ALIGN_MIDDLE;
            encabe.AddCell(numero);

            documento.Add(encabe);

            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPTable txt1 = new PdfPTable(1);
            txt1.DefaultCell.Border = 0;
            txt1.WidthPercentage = 100F;

            PdfPCell texto1 = new PdfPCell(new Phrase("Considerando que no hubo respuesta de su parte al primer aviso de cobranza, aprovechamos este medio para reiterarle el requerimiento de pago del saldo vencido que mantiene con esta institucion por la cantidad de ________, otorgándole una prórroga de 3 (tres) días naturales, contados a partir de la recepción del presente aviso. \n \n \n En caso de hacer caso omiso a este segundo aviso de cobranza su expediente de crédito será turnado inmediatamente al Departamentos de Cobranza Extrajudicial, con lo que su historial en las Sociedades de Información Crediticia (Buró o Círculo de Crédito) se verá afectado seriamente limitado a la posibilidad de que usted pueda acceder a nuevos financiamientos. \n \n Agradezco de antemano la atención que sirva presentar a la presente. \n \n \n \n \n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            texto1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            texto1.Border = 0;
            texto1.VerticalAlignment = Element.ALIGN_MIDDLE;
            txt1.AddCell(texto1);

            PdfPCell atte = new PdfPCell(new Phrase("ATENTAMENTE", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            atte.HorizontalAlignment = Element.ALIGN_CENTER;
            atte.Border = 0;
            atte.VerticalAlignment = Element.ALIGN_MIDDLE;
            txt1.AddCell(atte);

            documento.Add(txt1);

            //documento.Add(new Paragraph(""));
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

    protected void lnkImprimir3_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;
        // Tipo de Font que vamos utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" TERCER AVISO ");
        documento.AddCreator("DESARROLLARTE");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\TERCER_AVISO" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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
            //logo.WidthPercentage = 15f;


            //encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.SetWidths(new float[] { 2f, 8f });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;


            PdfPCell titulo = new PdfPCell(new Phrase("APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + " TERCER AVISO DE COBRANZA ", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            //PdfPCell titulo = new PdfPCell(new Phrase("Gaarve S.A de C.V. " + Environment.NewLine + Environment.NewLine + " Informe de Nomina " + Environment.NewLine + Environment.NewLine + strDatosObra, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(logo);
            tablaEncabezado.AddCell(titulo);

            documento.Add(tablaEncabezado);

            PdfPTable titulo1 = new PdfPTable(1);
            titulo1.DefaultCell.Border = 0;
            titulo1.WidthPercentage = 100f;

            string fechahoy = "";
            fechahoy = fechas.obtieneFechaLocal().ToString("yyyy/MM/dd");

            PdfPCell aviso1 = new PdfPCell(new Phrase("TERCER AVISO DE COBRANZA", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            aviso1.HorizontalAlignment = Element.ALIGN_CENTER;
            aviso1.Border = 0;
            aviso1.VerticalAlignment = Element.ALIGN_MIDDLE;
            titulo1.AddCell(aviso1);

            PdfPCell fecha1 = new PdfPCell(new Phrase("FECHA: " + fechahoy, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            fecha1.HorizontalAlignment = Element.ALIGN_RIGHT;
            fecha1.Border = 0;
            fecha1.VerticalAlignment = Element.ALIGN_MIDDLE;
            titulo1.AddCell(fecha1);

            documento.Add(titulo1);

            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            PdfPTable encabe = new PdfPTable(1);
            encabe.DefaultCell.Border = 0;
            encabe.WidthPercentage = 100f;
            encabe.HorizontalAlignment = Element.ALIGN_LEFT;

            AvCob infor = new AvCob();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = sesiones[4];
            infor.obtieneInfoEnca();

            string gerente2 = "";
            string grupo = "";
            int idgrupo = 0;

            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    gerente2 = Convert.ToString(r[0]);
                    grupo = Convert.ToString(r[1]);
                    idgrupo = Convert.ToInt32(r[2]);

                }
            }

            PdfPCell gerente = new PdfPCell(new Phrase("CLIENTE: "+gerente2.ToUpper(), FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            gerente.HorizontalAlignment = Element.ALIGN_LEFT;
            gerente.Border = 0;
            gerente.VerticalAlignment = Element.ALIGN_MIDDLE;
            encabe.AddCell(gerente);

            PdfPCell grupPro = new PdfPCell(new Phrase("GRUPO PRODUCTIVO: "+grupo.ToUpper(), FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            grupPro.HorizontalAlignment = Element.ALIGN_LEFT;
            grupPro.Border = 0;
            grupPro.VerticalAlignment = Element.ALIGN_MIDDLE;
            encabe.AddCell(grupPro);


            PdfPCell numero = new PdfPCell(new Phrase("NÚMERO: "+idgrupo, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            numero.HorizontalAlignment = Element.ALIGN_LEFT;
            numero.Border = 0;
            numero.VerticalAlignment = Element.ALIGN_MIDDLE;
            encabe.AddCell(numero);

            documento.Add(encabe);

            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPTable txt1 = new PdfPTable(1);
            txt1.DefaultCell.Border = 0;
            txt1.WidthPercentage = 100F;

            PdfPCell texto1 = new PdfPCell(new Phrase("Como hasta la fecha no hemos recibido respuesta al requerimiento de pago realizado mediante primer y segundo aviso de cobranza, a través de la presente se le exhoda una vez más para que en un plazo de 24 horas, contadas a partir de la recepción de la presente, acuda a la Sucursal que le corresponda para que liquide la deuda que tiene pendiente con esta institución. \n \n \n En caso de no presentarse a la sucursal o no tener respuesta de su parte, se dará por entendido que existe negativa para entablar cualquier tipo de negociación para normalizar su situación financiera. Bajo esta circunstancia, la institución se verá obligada a realizar las gestiones legales y judiciales correspondientes para la recuperación de su acuerdo. \n \n \n Sin otro particular y para cualquiero duda o aclaración al respecto, quedo de usted \n \n \n \n \n", FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD)));
            texto1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            texto1.Border = 0;
            texto1.VerticalAlignment = Element.ALIGN_MIDDLE;
            txt1.AddCell(texto1);

            PdfPCell atte = new PdfPCell(new Phrase("ATENTAMENTE", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            atte.HorizontalAlignment = Element.ALIGN_CENTER;
            atte.Border = 0;
            atte.VerticalAlignment = Element.ALIGN_MIDDLE;
            txt1.AddCell(atte);

            documento.Add(txt1);

            //documento.Add(new Paragraph(""));
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

