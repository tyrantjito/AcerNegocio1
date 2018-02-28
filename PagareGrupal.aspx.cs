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


public partial class PagareGrupal : System.Web.UI.Page
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
        documento.AddTitle(" PagareGrupal ");
        documento.AddCreator("AserNegocio");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\Pagare_Grupal_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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



            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            //encabezado
            PdfPTable tablaEncabezado = new PdfPTable(1);
            tablaEncabezado.SetWidths(new float[] { 100 });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;
            tablaEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;


            PdfPCell titulo = new PdfPCell(new Phrase("PAGARE GRUPAL", fuente1));
            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_CENTER;
            tablaEncabezado.AddCell(titulo);
            documento.Add(tablaEncabezado);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PagGrup infor = new PagGrup();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = Convert.ToInt32( Label15.Text);
            infor.obtieneInfoEncabezado();

            decimal montoin = 0;
            int taza = 0;
            string fecha = "";

            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    montoin = Convert.ToDecimal(r[12]);
                    taza = Convert.ToInt32(r[9]);
                    fecha = Convert.ToString(r[4]);

                }
                }

                PdfPTable monto = new PdfPTable(1);
                monto.SetWidths(new float[] { 100 });
                monto.DefaultCell.Border = 0;
                monto.WidthPercentage = 50f;
                monto.HorizontalAlignment = Element.ALIGN_LEFT;

                Convertidores montoText = new Convertidores();
                montoText._importe = montoin.ToString();

                PdfPCell monto1 = new PdfPCell(new Phrase("Monto: " + montoin.ToString("C2") +"  " +montoText.convierteMontoEnLetras().ToUpper().Trim(), fuente8));

                monto1.HorizontalAlignment = Element.ALIGN_LEFT;
                monto1.Border = 0;
                monto1.VerticalAlignment = Element.ALIGN_CENTER;
                monto.AddCell(monto1);

                documento.Add(monto);
                documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            // texto del pagaré grupal
            PdfPTable texto1 = new PdfPTable(1);
                texto1.SetWidths(new float[] { 100 });
                texto1.DefaultCell.Border = 0;
                texto1.WidthPercentage = 100f;
                texto1.HorizontalAlignment = Element.ALIGN_LEFT;

                PagGrup datEmpre = new PagGrup();
                datEmpre.empresa = sesiones[2];
                datEmpre.sucursal = sesiones[3];
                datEmpre.obtenerEmpresa();

                string nameCort = "";
                string nameEmp = "";
                string direEmp = "";
                string telEmpresa = "";
                string correoEmp = "";
                string pagWeb = "";
                string rfcEmp = "";
                string represen = "";

                if (Convert.ToBoolean(datEmpre.retorno[0]))
                {
                    DataSet para = (DataSet)datEmpre.retorno[1];


                    foreach (DataRow rt in para.Tables[0].Rows)
                    {
                        nameCort = rt[2].ToString();
                        nameEmp = rt[3].ToString();
                        direEmp = rt[4].ToString();
                        correoEmp = rt[5].ToString();
                        telEmpresa = rt[6].ToString();
                        rfcEmp = rt[7].ToString();
                        pagWeb = rt[8].ToString();
                        represen = rt[9].ToString();
                    }
                }

                PdfPCell txt1 = new PdfPCell(new Phrase("POR ADEUDO RECONOCIDO. Los suscriptores de este Título de Crédito, PROMETEMOS PAGAR INCONDICIONALMENTE a la orden de ''"+nameEmp+"'' en el domicilio ubicado en "+direEmp+", la sumaprincipal consiste en la canditadad de " + montoin.ToString("C2") + montoText.convierteMontoEnLetras().ToUpper().Trim() + "más los intereses ordinarios que se generen a razón de una tasa equivalente al " + taza + "% calculado sobre el total del crédito otorgado. Cualquier parte del monto principal o intereses que no sea pegado a su vencimiento, generará intereses moratorios a razón de una rasa de interés moratorio igual a la tasa de interés ordinaria muultiplicada por dos por cada dia transcurrido. El caso de que la fecha de pago no sea un día hábil, el pago deberá efectuarse el día hábil inmediato anterior. \n \n Este pagaré se interpretará y se regirá bajo las leyes de los Estados Unidos Mexicanos. Para todo lo relacionado con el presente pagaré, los suscriptores se someten a la jurisdicción de los tribunales competentes en la Ciudad de México, renunciando expresa e irrevocablemente a cualquier otra jurisdicción que por razón de sus domicilios pudiera corresponderle en lo futuro. Este pagaré está compuesto por una hoja útil, escrita por el frente y en su caso por ambas caras, se extiende y firma en la Ciudad de México el día " + Convert.ToDateTime(fecha).ToString("dd/MM/yyyy"), fuente6));
                txt1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                txt1.Border = 0;
                txt1.VerticalAlignment = Element.ALIGN_JUSTIFIED;
                texto1.AddCell(txt1);

                documento.Add(texto1);
                documento.Add(new Paragraph(" "));

                documento.Add(new Paragraph(" "));

                //tabla de pagare grupal
                PdfPTable tab1 = new PdfPTable(3);
                tab1.SetWidths(new float[] { 40, 30, 30 });
                tab1.DefaultCell.Border = 0;
                tab1.WidthPercentage = 100f;
                tab1.HorizontalAlignment = Element.ALIGN_LEFT;


           
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = Convert.ToInt32(Label15.Text);
            infor.obtieneInfoDetalle();

         
            PdfPCell nomTab = new PdfPCell(new Phrase("NOMBRE DEL SUSCRIPTOR", fuente6));
            nomTab.HorizontalAlignment = Element.ALIGN_CENTER;
            nomTab.BackgroundColor = BaseColor.LIGHT_GRAY;
            nomTab.VerticalAlignment = Element.ALIGN_CENTER;
            tab1.AddCell(nomTab);

            PdfPCell curpTab = new PdfPCell(new Phrase("CURP", fuente6));
            curpTab.HorizontalAlignment = Element.ALIGN_CENTER;
            curpTab.BackgroundColor = BaseColor.LIGHT_GRAY;
            curpTab.VerticalAlignment = Element.ALIGN_CENTER;
            tab1.AddCell(curpTab);

            PdfPCell firmaTab = new PdfPCell(new Phrase("FIRMA", fuente6));
            firmaTab.HorizontalAlignment = Element.ALIGN_CENTER;
            firmaTab.BackgroundColor = BaseColor.LIGHT_GRAY;
            firmaTab.VerticalAlignment = Element.ALIGN_CENTER;
            tab1.AddCell(firmaTab);


            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    string nomcli = Convert.ToString(r[0]);
                    string curpcli = Convert.ToString(r[1]);


                    PdfPCell nomTab1 = new PdfPCell(new Phrase(" " + nomcli + "\n \n", fuente6));
                    nomTab1.HorizontalAlignment = Element.ALIGN_CENTER;
                    nomTab1.VerticalAlignment = Element.ALIGN_CENTER;
                    tab1.AddCell(nomTab1);

                    PdfPCell curpTab1 = new PdfPCell(new Phrase(" " + curpcli + "\n \n", fuente6));
                    curpTab1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpTab1.VerticalAlignment = Element.ALIGN_CENTER;
                    tab1.AddCell(curpTab1);

                    PdfPCell firmaTab1 = new PdfPCell(new Phrase("\n \n", fuente6));
                    firmaTab1.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmaTab1.VerticalAlignment = Element.ALIGN_CENTER;
                    tab1.AddCell(firmaTab1);
                  

                }
            }



            documento.Add(tab1);

            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPTable tablaEncabezado2 = new PdfPTable(1);
            tablaEncabezado2.SetWidths(new float[] { 100 });
            tablaEncabezado2.DefaultCell.Border = 0;
            tablaEncabezado2.WidthPercentage = 100f;
            tablaEncabezado2.HorizontalAlignment = Element.ALIGN_CENTER;


            PdfPCell titulo2 = new PdfPCell(new Phrase("ANEXO 2.TABLA DE AMORTIZACIÓN", fuente1));
            titulo2.HorizontalAlignment = 1;
            titulo2.Border = 0;
            titulo2.VerticalAlignment = Element.ALIGN_CENTER;
            tablaEncabezado2.AddCell(titulo2);
            documento.Add(tablaEncabezado2);
            documento.Add(new Paragraph(" "));

            PdfPTable sucur = new PdfPTable(5);
            sucur.WidthPercentage = 100f;
            int[] sucurcellwidth = { 15, 15, 15, 15, 15 };
            sucur.SetWidths(sucurcellwidth);







            PdfPCell sucurs = (new PdfPCell(new Phrase("NÚMERO DE PAGO", fuente8)) { Colspan = 1, Rowspan = 3 });
            sucurs.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(sucurs);

            PdfPCell sucurs1 = (new PdfPCell(new Phrase("FECHA", fuente8)) { Colspan = 1, Rowspan = 3 });
            sucurs1.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(sucurs1);

            PdfPCell fecS = (new PdfPCell(new Phrase("PAGO DE CAPITAL", fuente8)) { Colspan = 1, Rowspan = 3 });
            fecS.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(fecS);

            PdfPCell fecS1 = (new PdfPCell(new Phrase("PAGO DE INTERES ORDINARIO", fuente8)) { Colspan = 1, Rowspan = 3 });
            fecS1.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(fecS1);

            PdfPCell fecE = (new PdfPCell(new Phrase("PAGO TOTAL", fuente8)) { Colspan = 1, Rowspan = 3 });
            fecE.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(fecE);

          


            infor.tablaAmor();
            string fecha2 ="";
            decimal creditoAut = 0;
            int tasaAmor = 0;
            decimal amortizacion = 0;
            int cont = 1;
            int plazo = 0;
            DateTime fechapag;
            decimal tasaanual = 0;
            decimal tasasema = 0;
            decimal inter2 = 0;
            decimal interes = 0;
            decimal mpagoo = 0;

            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    creditoAut = Convert.ToDecimal(r[1]);
                    plazo = Convert.ToInt32(r[2]);
                    tasaAmor = Convert.ToInt32(r[3]);
                    fecha = Convert.ToString(r[0]);
                    amortizacion = creditoAut / plazo;
                    fechapag = Convert.ToDateTime(fecha);
                    fechapag = fechapag.AddDays(7);
                    tasaanual = (((tasaAmor / Convert.ToDecimal(1.16)) * 360) / 28);
                    tasasema = tasaanual / (360) * 7 * Convert.ToDecimal(1.16);
                    inter2 = creditoAut * plazo * Convert.ToDecimal(tasasema) / 100;
                    interes = inter2 / plazo;
                    mpagoo = amortizacion + interes;
                    for (int i = 1; i <= (plazo); i++)
                    {

                        PdfPCell nume1 = new PdfPCell(new Phrase(""+cont, fuente8));
                        nume1.HorizontalAlignment = Element.ALIGN_CENTER;
                        sucur.AddCell(nume1);

                        PdfPCell nume2 = new PdfPCell(new Phrase(""+ Convert.ToDateTime(fechapag).ToString("dd/MM/yyyy"), fuente8));
                        nume2.HorizontalAlignment = Element.ALIGN_CENTER;
                        sucur.AddCell(nume2);

                        PdfPCell nume3 = new PdfPCell(new Phrase(""+amortizacion.ToString("C2"), fuente8));
                        nume3.HorizontalAlignment = Element.ALIGN_CENTER;
                        sucur.AddCell(nume3);

                        PdfPCell nume4 = new PdfPCell(new Phrase(""+interes.ToString("C2"), fuente8));
                        nume4.HorizontalAlignment = Element.ALIGN_CENTER;
                        sucur.AddCell(nume4);

                        PdfPCell nume5 = new PdfPCell(new Phrase(""+mpagoo.ToString("C2"), fuente8));
                        nume5.HorizontalAlignment = Element.ALIGN_CENTER;
                        sucur.AddCell(nume5);

                        cont++;
                        fechapag = fechapag.AddDays(7);
                    }
                }

            }



            documento.Add(sucur);
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