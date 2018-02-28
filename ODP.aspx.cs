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


public partial class ODP : System.Web.UI.Page
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
            iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font fuente4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            //variable local fechas con un objeto tipo Fechas
            Fechas fechas = new Fechas();

            //creacion de documento
            Document documento = new Document(iTextSharp.text.PageSize.LETTER);
            documento.AddTitle(" ODP ");
            documento.AddCreator("Desarrollarte");
            string ruta = HttpContext.Current.Server.MapPath("~/files");
            string archivo = ruta + "\\ODPDesarrollar_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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


            logo.ScaleAbsolute(3000, 770);
            // Se indica que la imagen debe almacenarse como fondo
            logo.Alignment = iTextSharp.text.Image.UNDERLYING;
            // Coloca la imagen en una posición absoluta
            logo.SetAbsolutePosition(7, 69);


            PdfPTable suscri = new PdfPTable(2);
            suscri.SetWidths(new float[] { 20, 80 });
            suscri.WidthPercentage = 50f;
            suscri.HorizontalAlignment = Element.ALIGN_LEFT;
            suscri.DefaultCell.Border = 0;






            CodODP hary = new CodODP();
            int[] sesiones = obtieneSesiones();
            hary.empresa = sesiones[2];
            hary.sucursal = sesiones[3];
            hary.grupo = sesiones[4];
            int idcliente2 = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
            hary.idcliente = idcliente2;
            hary.obtieneInfoDetalle();

            string nameCliente = "";
            decimal convenio = 0;
            decimal referencia = 0;
            decimal montoPagar = 0;
            string fechahoy = "";
            if (Convert.ToBoolean(hary.retorno[0]))
            {
                DataSet ds = (DataSet)hary.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    nameCliente = Convert.ToString(r[1]);
                    convenio = Convert.ToDecimal(r[5]);
                    referencia = Convert.ToDecimal(r[6]);
                    montoPagar = Convert.ToDecimal(r[7]);
                    fechahoy = fechas.obtieneFechaLocal().ToString("yyyy/MM/dd");

                }
            }

            
            suscri.AddCell(logo);

            PdfPCell nomSus = (new PdfPCell(new Phrase("\n \n"+nameCliente+"\n \n ", fuente6)) { Rowspan = 5 });
            nomSus.HorizontalAlignment = Element.ALIGN_CENTER;
            nomSus.VerticalAlignment = Element.ALIGN_CENTER;
            nomSus.Border = 0;
            suscri.AddCell(nomSus);

            PdfPCell fechatoday = (new PdfPCell(new Phrase("  FECHA: " + fechahoy, fuente6)) { Colspan=2 });
            fechatoday.HorizontalAlignment = Element.ALIGN_LEFT;
            fechatoday.VerticalAlignment = Element.ALIGN_CENTER;
            fechatoday.Border = 0;
            suscri.AddCell(fechatoday);

            PdfPCell convenioodp = (new PdfPCell(new Phrase("  CONVENIO: " + convenio, fuente6)) { Colspan=2 });
            convenioodp.HorizontalAlignment = Element.ALIGN_LEFT;
            convenioodp.VerticalAlignment = Element.ALIGN_CENTER;
            convenioodp.Border = 0;
            suscri.AddCell(convenioodp);

            PdfPCell refeODP = (new PdfPCell(new Phrase("  REFERENCIA: " + referencia, fuente6)) { Colspan=2 });
            refeODP.HorizontalAlignment = Element.ALIGN_LEFT;
            refeODP.VerticalAlignment = Element.ALIGN_CENTER;
            refeODP.Border = 0;
            suscri.AddCell(refeODP);

            PdfPCell montEntr = (new PdfPCell(new Phrase("  MONTO A ENTREGAR: " + montoPagar.ToString("C2") + " \n ", fuente6)) { Colspan=2 });
            montEntr.HorizontalAlignment = Element.ALIGN_LEFT;
            montEntr.VerticalAlignment = Element.ALIGN_CENTER;
            montEntr.Border = 0;
            suscri.AddCell(montEntr);

           
        


    PdfPTable twoTable = new PdfPTable(2);
            twoTable.SetWidths(new float[] { 50, 50 });
            twoTable.WidthPercentage = 100f;
            twoTable.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell tabone = (new PdfPCell(suscri));
            tabone.HorizontalAlignment = Element.ALIGN_LEFT;
            tabone.VerticalAlignment = Element.ALIGN_CENTER;
            
            tabone.Border = 1;
            tabone.BorderWidth = 1;
            tabone.BorderWidthBottom = 1;
            tabone.BorderWidthLeft = 1;
            tabone.BorderWidthRight = 1;
            twoTable.AddCell(tabone);
            

            PdfPCell tabtwo = (new PdfPCell(suscri));
            tabtwo.HorizontalAlignment = Element.ALIGN_LEFT;
            tabtwo.VerticalAlignment = Element.ALIGN_CENTER;
            tabtwo.Border = 1;
            tabtwo.BorderWidthBottom = 1;
            tabtwo.BorderWidthRight = 1;
            twoTable.AddCell(tabtwo);

            documento.Add(twoTable);




            documento.Close();



                FileInfo filename = new FileInfo(archivo);
                if (filename.Exists)
                {
                    string url = "Descargas.aspx?filename=" + filename.Name;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "pdfs", "window.open('" + url + "', 'nuevo', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, titlebar=no, fullscreen=yes,resizable=yes');", true);


                }

            }
        }
        protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lnkImprimir.Visible = true;
        }

    protected void lnkimprimirTodo_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;
        // Tipo de Font que vamos utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" ODP ");
        documento.AddCreator("Desarrollarte");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\ODPDesarrollar_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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


            logo.ScaleAbsolute(3000, 770);
            // Se indica que la imagen debe almacenarse como fondo
            logo.Alignment = iTextSharp.text.Image.UNDERLYING;
            // Coloca la imagen en una posición absoluta
            logo.SetAbsolutePosition(7, 69);


            PdfPTable suscri = new PdfPTable(2);
            suscri.SetWidths(new float[] { 20, 80 });
            suscri.WidthPercentage = 50f;
            suscri.HorizontalAlignment = Element.ALIGN_LEFT;
            suscri.DefaultCell.Border = 0;


            int[] sesiones = obtieneSesiones();
            int cliente = 0;
            Paga inf = new Paga();
            inf.empresa = sesiones[2];
            inf.sucursal = sesiones[3];
            inf.idSolicitudEdita = sesiones[4];
            inf.obtieneclientes();

            if (Convert.ToBoolean(inf.retorno[0]))
            {
                DataSet ds1 = (DataSet)inf.retorno[1];


                foreach (DataRow r1 in ds1.Tables[0].Rows)
                {
                    cliente = Convert.ToInt32(r1[0]);




                    CodODP hary = new CodODP();
                    int[] sesiones = obtieneSesiones();
                    hary.empresa = sesiones[2];
                    hary.sucursal = sesiones[3];
                    hary.grupo = sesiones[4];
                    int idcliente2 = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
                    hary.idcliente = idcliente2;
                    hary.obtieneInfoDetalle();

                    string nameCliente = "";
                    decimal convenio = 0;
                    decimal referencia = 0;
                    decimal montoPagar = 0;
                    string fechahoy = "";
                    if (Convert.ToBoolean(hary.retorno[0]))
                    {
                        DataSet ds = (DataSet)hary.retorno[1];


                        foreach (DataRow r in ds.Tables[0].Rows)
                        {

                            nameCliente = Convert.ToString(r[1]);
                            convenio = Convert.ToDecimal(r[5]);
                            referencia = Convert.ToDecimal(r[6]);
                            montoPagar = Convert.ToDecimal(r[7]);
                            fechahoy = fechas.obtieneFechaLocal().ToString("yyyy/MM/dd");

                        }
                    }


                    suscri.AddCell(logo);

                    PdfPCell nomSus = (new PdfPCell(new Phrase("\n \n" + nameCliente + "\n \n ", fuente6)) { Rowspan = 5 });
                    nomSus.HorizontalAlignment = Element.ALIGN_CENTER;
                    nomSus.VerticalAlignment = Element.ALIGN_CENTER;
                    nomSus.Border = 0;
                    suscri.AddCell(nomSus);

                    PdfPCell fechatoday = (new PdfPCell(new Phrase("  FECHA: " + fechahoy, fuente6)) { Colspan = 2 });
                    fechatoday.HorizontalAlignment = Element.ALIGN_LEFT;
                    fechatoday.VerticalAlignment = Element.ALIGN_CENTER;
                    fechatoday.Border = 0;
                    suscri.AddCell(fechatoday);

                    PdfPCell convenioodp = (new PdfPCell(new Phrase("  CONVENIO: " + convenio, fuente6)) { Colspan = 2 });
                    convenioodp.HorizontalAlignment = Element.ALIGN_LEFT;
                    convenioodp.VerticalAlignment = Element.ALIGN_CENTER;
                    convenioodp.Border = 0;
                    suscri.AddCell(convenioodp);

                    PdfPCell refeODP = (new PdfPCell(new Phrase("  REFERENCIA: " + referencia, fuente6)) { Colspan = 2 });
                    refeODP.HorizontalAlignment = Element.ALIGN_LEFT;
                    refeODP.VerticalAlignment = Element.ALIGN_CENTER;
                    refeODP.Border = 0;
                    suscri.AddCell(refeODP);

                    PdfPCell montEntr = (new PdfPCell(new Phrase("  MONTO A ENTREGAR: " + montoPagar.ToString("C2") + " \n ", fuente6)) { Colspan = 2 });
                    montEntr.HorizontalAlignment = Element.ALIGN_LEFT;
                    montEntr.VerticalAlignment = Element.ALIGN_CENTER;
                    montEntr.Border = 0;
                    suscri.AddCell(montEntr);





                    PdfPTable twoTable = new PdfPTable(2);
                    twoTable.SetWidths(new float[] { 50, 50 });
                    twoTable.WidthPercentage = 100f;
                    twoTable.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell tabone = (new PdfPCell(suscri));
                    tabone.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabone.VerticalAlignment = Element.ALIGN_CENTER;

                    tabone.Border = 1;
                    tabone.BorderWidth = 1;
                    tabone.BorderWidthBottom = 1;
                    tabone.BorderWidthLeft = 1;
                    tabone.BorderWidthRight = 1;
                    twoTable.AddCell(tabone);


                    PdfPCell tabtwo = (new PdfPCell(suscri));
                    tabtwo.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabtwo.VerticalAlignment = Element.ALIGN_CENTER;
                    tabtwo.Border = 1;
                    tabtwo.BorderWidthBottom = 1;
                    tabtwo.BorderWidthRight = 1;
                    twoTable.AddCell(tabtwo);

                    documento.Add(twoTable);
                    documento.NewPage();
                }
            }


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