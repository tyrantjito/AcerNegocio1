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

public partial class Pagare : System.Web.UI.Page
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
        documento.AddTitle(" Pagare ");
        documento.AddCreator("AserNegocio");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\Pagare_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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
            PdfPTable tablaEncabezado = new PdfPTable(1);
            tablaEncabezado.SetWidths(new float[] { 100 });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;
            tablaEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;


            PdfPCell titulo = new PdfPCell(new Phrase("PAGARE", fuente1));

            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_CENTER;
            tablaEncabezado.AddCell(titulo);
            documento.Add(tablaEncabezado);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            Paga infor = new Paga();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = sesiones[4];
            int idcliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
            infor.idcliente = idcliente;
            infor.obtieneInfoDeta();

            decimal mont = 0;
            string fecha1 = "";
            string fecha2 = "";
            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    mont = Convert.ToDecimal(r[0]);
                    fecha1 = Convert.ToString(r[1]);
                    fecha2 = Convert.ToString(r[2]);

                }
            }
            PdfPTable monto = new PdfPTable(1);
            monto.SetWidths(new float[] { 100 });
            monto.DefaultCell.Border = 0;
            monto.WidthPercentage = 50f;
            monto.HorizontalAlignment = Element.ALIGN_LEFT;

            Convertidores numText = new Convertidores();
            numText._importe = mont.ToString();

            PdfPCell monto1 = new PdfPCell(new Phrase("Monto: " + mont.ToString("C2") + " " + numText.convierteMontoEnLetras().ToUpper().Trim(), fuente6));

            monto1.HorizontalAlignment = Element.ALIGN_LEFT;
            monto1.Border = 0;
            monto1.VerticalAlignment = Element.ALIGN_CENTER;
            monto.AddCell(monto1);

            documento.Add(monto);

            PdfPTable pagare = new PdfPTable(1);
            pagare.SetWidths(new float[] { 100 });
            pagare.DefaultCell.Border = 0;
            pagare.WidthPercentage = 100f;
            pagare.HorizontalAlignment = Element.ALIGN_CENTER;

            Paga empre = new Paga();
            empre.empresa = sesiones[2];
            empre.sucursal = sesiones[3];
            empre.obtenerEmpresa();

            string nameCort = "";
            string nameEmp = "";
            string direEmp = "";
            string telEmpresa = "";
            string correoEmp = "";
            string pagWeb = "";
            string rfcEmp = "";
            string represen = "";

            if (Convert.ToBoolean(empre.retorno[0]))
            {
                DataSet para = (DataSet)empre.retorno[1];


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



            PdfPCell pagare1 = new PdfPCell(new Phrase("POR ADEUDO RENOCONOCIDO. El presente pagaré es suscrito de conformidad por lo dispuesto en los artículos 3, 4, 12, 159 en relación al 174, 170 y demás relativos y aplicables de la Ley de Títulos y Operaciones de Créditos, por este PAGARE el SUSCRIPTOR PROMETE PAGAR a la orde de ''" + nameEmp + "'', en las oficinas ubicadas en: "+direEmp+", la suma principal, en lo sucesivo el ''MONTO PRINCIPAL'' por la cantidad de  " + mont.ToString("C2") + " " + numText.convierteMontoEnLetras().ToUpper().Trim() + " más los intereses ordinarios que se generen, conforme al calendario de amortizaciones que se describe más adelante, siendo la última fecha de pago en 14 de junio de 2017, día fijo en que el SUSCRIPTOR deberá efectuar el pago total de los saldos insolutos pendientes. El monto principal de este pagaré causará un interes total anual sin considerar el Impuesto al Valor Agregado (IVA) equivalente al 60% caculado sobre el total del crédito otorgado. Cualquier parte del monto principal o intereses que no sea pagado en las fechas establecidas, generará interes moratorios a razón  de una rasa igual de interés ordinario multiplicada por dos. \n \n", fuente6));
            pagare1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            pagare1.Border = 0;
            pagare1.VerticalAlignment = Element.ALIGN_CENTER;
            pagare.AddCell(pagare1);


            PdfPCell pagare2 = new PdfPCell(new Phrase("EL ''MONTO PRINCIPAL'' de este pagaré, los intereses ordinarios y moratorios serán pagaderos en moneda de curso legal en los Estados Unidos Mexicanos, libre de y sin deducción alguna por cualesquiera cargas, gravémenes, retenciones y accesorios con respecto a las mismas, EL SUSCRIPTOR de este pagaré deberá pagar la ''"+nameEmp+"'' el monto que por amortización a capital más intereses ordinarios corresponda conforme al siguiente calendario.", fuente6));
            pagare2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            pagare2.Border = 0;
            pagare2.VerticalAlignment = Element.ALIGN_CENTER;
            pagare.AddCell(pagare2);
            documento.Add(pagare);
            documento.Add(new Paragraph(" "));


            //tabla de monto de amortizacion (la capital mas interes ordinarios)

            PdfPTable amorti = new PdfPTable(3);
            amorti.SetWidths(new float[] { 30, 40, 30 });
            amorti.WidthPercentage = 50f;
            amorti.HorizontalAlignment = Element.ALIGN_LEFT;

            infor.tablaAmorti();

            decimal creditoAut = 0;
            int tasaAmor = 0;
            string fecha = "";
            decimal amortizacion = 0;
            int cont = 1;
            int plazo = 0;
            DateTime fechapag;
            decimal tasaanual = 0;
            decimal tasasema = 0;
            decimal inter2 = 0;
            decimal interes = 0;
            decimal mpagoo = 0;
            

            PdfPCell nuAmor = new PdfPCell(new Phrase("Número de Amortización", fuente6));
            nuAmor.HorizontalAlignment = Element.ALIGN_CENTER;
            nuAmor.VerticalAlignment = Element.ALIGN_MIDDLE;
            amorti.AddCell(nuAmor);

            PdfPCell MontoAmor = new PdfPCell(new Phrase("Monto de Amortización (capital más interes ordinarios)", fuente6));
            MontoAmor.HorizontalAlignment = Element.ALIGN_CENTER;
            MontoAmor.VerticalAlignment = Element.ALIGN_MIDDLE;
            amorti.AddCell(MontoAmor);

            PdfPCell fechaMor = new PdfPCell(new Phrase("Fecha", fuente6));
            fechaMor.HorizontalAlignment = Element.ALIGN_CENTER;
            fechaMor.VerticalAlignment = Element.ALIGN_MIDDLE;
            amorti.AddCell(fechaMor);

            PdfPTable amorti1 = new PdfPTable(3);
            amorti1.SetWidths(new float[] { 30, 40, 30 });
            amorti1.WidthPercentage = 50f;
            amorti1.HorizontalAlignment = Element.ALIGN_LEFT;


            PdfPCell nuAmor1 = new PdfPCell(new Phrase("Número de Amortización", fuente6));
            nuAmor1.HorizontalAlignment = Element.ALIGN_CENTER;
            nuAmor1.VerticalAlignment = Element.ALIGN_MIDDLE;
            amorti1.AddCell(nuAmor1);

            PdfPCell MontoAmor1 = new PdfPCell(new Phrase("Monto de Amortización (capital más interes ordinarios)", fuente6));
            MontoAmor1.HorizontalAlignment = Element.ALIGN_CENTER;
            MontoAmor1.VerticalAlignment = Element.ALIGN_MIDDLE;
            amorti1.AddCell(MontoAmor1);

            PdfPCell fechaMor1 = new PdfPCell(new Phrase("Fecha", fuente6));
            fechaMor1.HorizontalAlignment = Element.ALIGN_CENTER;
            fechaMor1.VerticalAlignment = Element.ALIGN_MIDDLE;
            amorti1.AddCell(fechaMor1);

            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


              
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                 
                    creditoAut = Convert.ToDecimal(r[3]);
                    plazo = Convert.ToInt32(r[0]);
                    tasaAmor = Convert.ToInt32(r[1]);
                    fecha = Convert.ToString(r[2]);
                    amortizacion = creditoAut / plazo;
                    fechapag = Convert.ToDateTime(fecha);
                    fechapag = fechapag.AddDays(7);
                    tasaanual =(((tasaAmor / Convert.ToDecimal(1.16) ) * 360) / 28);
                    tasasema = tasaanual / (360) * 7 * Convert.ToDecimal(1.16);
                    inter2 = creditoAut * plazo * Convert.ToDecimal(tasasema)/100;
                    interes = inter2 / plazo;
                    mpagoo = amortizacion + interes;
                    

                    for (int i = 1; i <= (plazo/2); i++)
                    {

                        PdfPCell nuAmorr = new PdfPCell(new Phrase(""+cont, fuente6));
                        nuAmorr.HorizontalAlignment = Element.ALIGN_CENTER;
                        nuAmorr.VerticalAlignment = Element.ALIGN_CENTER;
                        amorti.AddCell(nuAmorr);

                        PdfPCell MontoAmorr = new PdfPCell(new Phrase("" + mpagoo.ToString("C2"), fuente6));
                        MontoAmorr.HorizontalAlignment = Element.ALIGN_CENTER;
                        MontoAmorr.VerticalAlignment = Element.ALIGN_CENTER;
                        amorti.AddCell(MontoAmorr);

                        PdfPCell fechaMorr = new PdfPCell(new Phrase("" + Convert.ToDateTime(fechapag).ToString("dd/MM/yyyy"), fuente6));
                        fechaMorr.HorizontalAlignment = Element.ALIGN_CENTER;
                        fechaMorr.VerticalAlignment = Element.ALIGN_CENTER;
                        amorti.AddCell(fechaMorr);

                        cont++;
                        fechapag = fechapag.AddDays(7);
                    }
                  
                    for (int y = 1; y <= (plazo / 2); y++)
                    {
                        PdfPCell nuAmor2 = new PdfPCell(new Phrase(" "+cont, fuente6));
                        nuAmor2.HorizontalAlignment = Element.ALIGN_CENTER;
                        nuAmor2.VerticalAlignment = Element.ALIGN_CENTER;
                        amorti1.AddCell(nuAmor2);

                        PdfPCell MontoAmor2 = new PdfPCell(new Phrase(" " + mpagoo.ToString("C2"), fuente6));
                        MontoAmor2.HorizontalAlignment = Element.ALIGN_CENTER;
                        MontoAmor2.VerticalAlignment = Element.ALIGN_CENTER;
                        amorti1.AddCell(MontoAmor2);

                        PdfPCell fechaMor2 = new PdfPCell(new Phrase(" " + Convert.ToDateTime(fechapag).ToString("dd/MM/yyyy"), fuente6));
                        fechaMor2.HorizontalAlignment = Element.ALIGN_CENTER;
                        fechaMor2.VerticalAlignment = Element.ALIGN_CENTER;
                        amorti1.AddCell(fechaMor2);


                        cont++;
                        fechapag = fechapag.AddDays(7);
                    }

                   
                }
            }


           



            //tabla continuacion de monto amortización



            //union de tablas
            PdfPTable unitab = new PdfPTable(2);
            unitab.SetWidths(new float[] { 50, 50 });
            unitab.WidthPercentage = 100f;
            unitab.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell tab1 = new PdfPCell(amorti);
            tab1.HorizontalAlignment = Element.ALIGN_CENTER;
            tab1.VerticalAlignment = Element.ALIGN_CENTER;
            unitab.AddCell(tab1);

            PdfPCell tab2 = new PdfPCell(amorti1);
            tab2.HorizontalAlignment = Element.ALIGN_CENTER;
            tab2.VerticalAlignment = Element.ALIGN_CENTER;
            unitab.AddCell(tab2);
            documento.Add(unitab);
            documento.Add(new Paragraph(" "));

            //segundo texto
            PdfPTable text2 = new PdfPTable(1);
            text2.SetWidths(new float[] { 100 });
            text2.WidthPercentage = 100f;
            text2.HorizontalAlignment = Element.ALIGN_LEFT;
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPCell texto2 = new PdfPCell(new Phrase("En caso de que la ficha de pago no sea un día hábil, el pago deberá efectuarse el día hábil siguiente. Día hábil significará cualquier día, distinto a un sábado o domingo, en el que los bancos se encuentren abiertos para llevar a cabo sus operaciones. El presente título de crédito documenta una obligación del SUSCRIPTOR frente a ''"+nameEmp+"'' cuyo cumplimiento fue pactado a plazos, por lo que desde ahora los suscriptores manifiestan que en que caso de falta de pago total o parcial de cualquier amortización consignada en este pagaré, genera el derecho de ser exigible el monto total del saldo del adeudo reconocido en este pagaré, a partir de la fecha en que se incurra en moratoria de pago; en cuyo caso, a partir del incumplimiento, causará moratorios a tasa pactada. Para los efectos de los artículos 128, 160, 165 de la General de Títulos y Operaciones de Crédito, los plazos de caducidad y prescripción de las acciones cambiarias de este pagaré por falta de presentación para su cobro, se prorrogarán irrevocablmente hasta 5 (cinco) años contados a partir de la decha de este pagaré, en el entendido de que dicha extensión no impedirá la presentación de este pagaré con anterioridad a dicha fecha. Este pagaré se interpretará y se regirá bajos las leyes de los Estados Unidos Mexicanos. Para todo lo relacioado con el presente pagaré, los SUSCRIPTORES se someten a la jurisdicción que po razón de sus domicilios pudiere corresponderle en lo futuro. Este pagaré está compuesto por una hoja útil, escritas por ambas y se extiende y firma en Ciudad de México el dia " + Convert.ToDateTime(fecha).ToString("dd/MM/yyyy") + ".", fuente6));
            texto2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            texto2.VerticalAlignment = Element.ALIGN_CENTER;
            texto2.Border = 0;
            text2.AddCell(texto2);
            documento.Add(text2);
            documento.Add(new Paragraph(" "));

            documento.Add(new Paragraph(" ")); documento.Add(new Paragraph(" "));
            //tabla del suscriptor
            PdfPTable suscri = new PdfPTable(4);
            suscri.SetWidths(new float[] { 30, 30, 10, 30 });
            suscri.WidthPercentage = 100f;
            suscri.HorizontalAlignment = Element.ALIGN_CENTER;

            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = sesiones[4];
            int idcliente2 = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
            infor.idcliente = idcliente2;
            infor.obtieneInfoDetalle();

            string nomcli = "";
            string curpcli = "";
            decimal montoCli = 0;
            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    nomcli = Convert.ToString(r[0]);
                    curpcli = Convert.ToString(r[1]);
                    montoCli = Convert.ToDecimal(r[2]);
                }
            }

            PdfPCell nomSus = new PdfPCell(new Phrase ("NOMBRE DEL SUSCRIPTOR",fuente6));
            nomSus.HorizontalAlignment = Element.ALIGN_CENTER;
            nomSus.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(nomSus);

            PdfPCell curpSus = new PdfPCell(new Phrase("CURP", fuente6));
            curpSus.HorizontalAlignment = Element.ALIGN_CENTER;
            curpSus.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(curpSus);

            PdfPCell impoSus = new PdfPCell(new Phrase("IMPORTE", fuente6));
            impoSus.HorizontalAlignment = Element.ALIGN_CENTER;
            impoSus.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(impoSus);

            PdfPCell firmaSus = new PdfPCell(new Phrase("FIRMA", fuente6));
            firmaSus.HorizontalAlignment = Element.ALIGN_CENTER;
            firmaSus.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(firmaSus);

            PdfPCell nomSus1 = new PdfPCell(new Phrase(" "+nomcli, fuente6));
            nomSus1.HorizontalAlignment = Element.ALIGN_CENTER;
            nomSus1.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(nomSus1);

            PdfPCell curpSus1 = new PdfPCell(new Phrase(" "+curpcli, fuente6));
            curpSus1.HorizontalAlignment = Element.ALIGN_CENTER;
            curpSus1.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(curpSus1);

            PdfPCell impoSus1 = new PdfPCell(new Phrase(" "+montoCli.ToString("C2"), fuente6));
            impoSus1.HorizontalAlignment = Element.ALIGN_CENTER;
            impoSus1.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(impoSus1);

            PdfPCell firmaSus1 = new PdfPCell(new Phrase(" ", fuente6));
            firmaSus1.HorizontalAlignment = Element.ALIGN_CENTER;
            firmaSus1.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(firmaSus1);
            documento.Add(suscri);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            // firmas "avales"
            PdfPTable firAva = new PdfPTable(2);
            firAva.SetWidths(new float[] { 50, 50 });
            firAva.DefaultCell.Border = 0;
            firAva.WidthPercentage = 100f;
            firAva.HorizontalAlignment = Element.ALIGN_CENTER;



            PdfPCell encaFir = (new PdfPCell(new Phrase("LOS AVALES", fuente6)) { Colspan = 2, Rowspan = 2 });
            encaFir.HorizontalAlignment = Element.ALIGN_CENTER;
            encaFir.VerticalAlignment = Element.ALIGN_CENTER;
            encaFir.Border = 0;
            firAva.AddCell(encaFir);

            PdfPCell encaFir1 = (new PdfPCell(new Phrase(" ", fuente6)) { Colspan = 2, Rowspan = 2 });
            encaFir1.HorizontalAlignment = Element.ALIGN_CENTER;
            encaFir1.VerticalAlignment = Element.ALIGN_CENTER;
            encaFir1.Border = 0;
            firAva.AddCell(encaFir1);

            PdfPCell encaFir2 = (new PdfPCell(new Phrase(" ", fuente6)) { Colspan = 2, Rowspan = 2 });
            encaFir2.HorizontalAlignment = Element.ALIGN_CENTER;
            encaFir2.VerticalAlignment = Element.ALIGN_CENTER;
            encaFir2.Border = 0;
            firAva.AddCell(encaFir2);

            PdfPCell encaFir3 = (new PdfPCell(new Phrase(" ", fuente6)) { Colspan = 2, Rowspan = 2 });
            encaFir3.HorizontalAlignment = Element.ALIGN_CENTER;
            encaFir3.VerticalAlignment = Element.ALIGN_CENTER;
            encaFir3.Border = 0;
            firAva.AddCell(encaFir3);



            PdfPCell raya1 = new PdfPCell(new Phrase("_________________________________________", fuente6));
            raya1.HorizontalAlignment = Element.ALIGN_CENTER;
            raya1.VerticalAlignment = Element.ALIGN_CENTER;
            raya1.Border = 0;
            firAva.AddCell(raya1);


            PdfPCell raya2 = new PdfPCell(new Phrase("_________________________________________", fuente6));
            raya2.HorizontalAlignment = Element.ALIGN_CENTER;
            raya2.VerticalAlignment = Element.ALIGN_CENTER;
            raya2.Border = 0;
            firAva.AddCell(raya2);

            PdfPCell firmi1 = new PdfPCell(new Phrase("primer nombre aqui", fuente6));
            firmi1.HorizontalAlignment = Element.ALIGN_CENTER;
            firmi1.VerticalAlignment = Element.ALIGN_CENTER;
            firmi1.Border = 0;
            firAva.AddCell(firmi1);

            PdfPCell firmi2 = new PdfPCell(new Phrase("primer nombre aqui", fuente6));
            firmi2.HorizontalAlignment = Element.ALIGN_CENTER;
            firmi2.VerticalAlignment = Element.ALIGN_CENTER;
            firmi2.Border = 0;
            firAva.AddCell(firmi2);

            PdfPCell direc1 = new PdfPCell(new Phrase("primera dirección", fuente6));
            direc1.HorizontalAlignment = Element.ALIGN_CENTER;
            direc1.VerticalAlignment = Element.ALIGN_CENTER;
            direc1.Border = 0;
            firAva.AddCell(direc1);

            PdfPCell direc2 = new PdfPCell(new Phrase("primera dirección", fuente6));
            direc2.HorizontalAlignment = Element.ALIGN_CENTER;
            direc2.VerticalAlignment = Element.ALIGN_CENTER;
            direc2.Border = 0;
            firAva.AddCell(direc2);
            documento.Add(firAva);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPTable firAva2 = new PdfPTable(2);
            firAva2.SetWidths(new float[] { 50, 50 });
            firAva2.DefaultCell.Border = 0;
            firAva2.WidthPercentage = 100f;
            firAva2.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell raya3 = new PdfPCell(new Phrase("_________________________________________", fuente6));
            raya3.HorizontalAlignment = Element.ALIGN_CENTER;
            raya3.VerticalAlignment = Element.ALIGN_CENTER;
            raya3.Border = 0;
            firAva2.AddCell(raya3);


            PdfPCell raya4 = new PdfPCell(new Phrase("_________________________________________", fuente6));
            raya4.HorizontalAlignment = Element.ALIGN_CENTER;
            raya4.VerticalAlignment = Element.ALIGN_CENTER;
            raya4.Border = 0;
            firAva2.AddCell(raya4);

            PdfPCell firmi3 = new PdfPCell(new Phrase("primer nombre aqui", fuente6));
            firmi3.HorizontalAlignment = Element.ALIGN_CENTER;
            firmi3.VerticalAlignment = Element.ALIGN_CENTER;
            firmi3.Border = 0;
            firAva2.AddCell(firmi3);

            PdfPCell firmi4 = new PdfPCell(new Phrase("primer nombre aqui", fuente6));
            firmi4.HorizontalAlignment = Element.ALIGN_CENTER;
            firmi4.VerticalAlignment = Element.ALIGN_CENTER;
            firmi4.Border = 0;
            firAva2.AddCell(firmi4);

            PdfPCell direc3 = new PdfPCell(new Phrase("primera dirección", fuente6));
            direc3.HorizontalAlignment = Element.ALIGN_CENTER;
            direc3.VerticalAlignment = Element.ALIGN_CENTER;
            direc3.Border = 0;
            firAva2.AddCell(direc3);

            PdfPCell direc4 = new PdfPCell(new Phrase("primera dirección", fuente6));
            direc4.HorizontalAlignment = Element.ALIGN_CENTER;
            direc4.VerticalAlignment = Element.ALIGN_CENTER;
            direc4.Border = 0;
            firAva2.AddCell(direc4);
            documento.Add(firAva2);

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

    protected void lnkImprimirTodo_Click(object sender, EventArgs e)
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
        documento.AddTitle(" Pagare ");
        documento.AddCreator("AserNegocio");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\Pagare_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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
            PdfPTable tablaEncabezado = new PdfPTable(1);
            tablaEncabezado.SetWidths(new float[] { 100 });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;
            tablaEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;


            PdfPCell titulo = new PdfPCell(new Phrase("PAGARE", fuente1));

            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_CENTER;
            tablaEncabezado.AddCell(titulo);
            documento.Add(tablaEncabezado);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            int[] sesiones = obtieneSesiones();
            int cliente=0;
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
                

                    Paga infor = new Paga();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = sesiones[4];
                    int idcliente = cliente;
            infor.idcliente = idcliente;
            infor.obtieneInfoDeta();

            decimal mont = 0;
            string fecha1 = "";
            string fecha2 = "";
            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    mont = Convert.ToDecimal(r[0]);
                    fecha1 = Convert.ToString(r[1]);
                    fecha2 = Convert.ToString(r[2]);

                }
            }
            PdfPTable monto = new PdfPTable(1);
            monto.SetWidths(new float[] { 100 });
            monto.DefaultCell.Border = 0;
            monto.WidthPercentage = 50f;
            monto.HorizontalAlignment = Element.ALIGN_LEFT;

            Convertidores numText = new Convertidores();
            numText._importe = mont.ToString();

            PdfPCell monto1 = new PdfPCell(new Phrase("Monto: " + mont.ToString("C2") + " " + numText.convierteMontoEnLetras().ToUpper().Trim(), fuente6));

            monto1.HorizontalAlignment = Element.ALIGN_LEFT;
            monto1.Border = 0;
            monto1.VerticalAlignment = Element.ALIGN_CENTER;
            monto.AddCell(monto1);

            documento.Add(monto);

            PdfPTable pagare = new PdfPTable(1);
            pagare.SetWidths(new float[] { 100 });
            pagare.DefaultCell.Border = 0;
            pagare.WidthPercentage = 100f;
            pagare.HorizontalAlignment = Element.ALIGN_CENTER;

            Paga empre = new Paga();
            empre.empresa = sesiones[2];
            empre.sucursal = sesiones[3];
            empre.obtenerEmpresa();

            string nameCort = "";
            string nameEmp = "";
            string direEmp = "";
            string telEmpresa = "";
            string correoEmp = "";
            string pagWeb = "";
            string rfcEmp = "";
            string represen = "";

            if (Convert.ToBoolean(empre.retorno[0]))
            {
                DataSet para = (DataSet)empre.retorno[1];


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



            PdfPCell pagare1 = new PdfPCell(new Phrase("POR ADEUDO RENOCONOCIDO. El presente pagaré es suscrito de conformidad por lo dispuesto en los artículos 3, 4, 12, 159 en relación al 174, 170 y demás relativos y aplicables de la Ley de Títulos y Operaciones de Créditos, por este PAGARE el SUSCRIPTOR PROMETE PAGAR a la orde de ''" + nameEmp + "'', en las oficinas ubicadas en: " + direEmp + ", la suma principal, en lo sucesivo el ''MONTO PRINCIPAL'' por la cantidad de  " + mont.ToString("C2") + " " + numText.convierteMontoEnLetras().ToUpper().Trim() + " más los intereses ordinarios que se generen, conforme al calendario de amortizaciones que se describe más adelante, siendo la última fecha de pago en 14 de junio de 2017, día fijo en que el SUSCRIPTOR deberá efectuar el pago total de los saldos insolutos pendientes. El monto principal de este pagaré causará un interes total anual sin considerar el Impuesto al Valor Agregado (IVA) equivalente al 60% caculado sobre el total del crédito otorgado. Cualquier parte del monto principal o intereses que no sea pagado en las fechas establecidas, generará interes moratorios a razón  de una rasa igual de interés ordinario multiplicada por dos. \n \n", fuente6));
            pagare1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            pagare1.Border = 0;
            pagare1.VerticalAlignment = Element.ALIGN_CENTER;
            pagare.AddCell(pagare1);


            PdfPCell pagare2 = new PdfPCell(new Phrase("EL ''MONTO PRINCIPAL'' de este pagaré, los intereses ordinarios y moratorios serán pagaderos en moneda de curso legal en los Estados Unidos Mexicanos, libre de y sin deducción alguna por cualesquiera cargas, gravémenes, retenciones y accesorios con respecto a las mismas, EL SUSCRIPTOR de este pagaré deberá pagar la ''" + nameEmp + "'' el monto que por amortización a capital más intereses ordinarios corresponda conforme al siguiente calendario.", fuente6));
            pagare2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            pagare2.Border = 0;
            pagare2.VerticalAlignment = Element.ALIGN_CENTER;
            pagare.AddCell(pagare2);
            documento.Add(pagare);
            documento.Add(new Paragraph(" "));


            //tabla de monto de amortizacion (la capital mas interes ordinarios)

            PdfPTable amorti = new PdfPTable(3);
            amorti.SetWidths(new float[] { 30, 40, 30 });
            amorti.WidthPercentage = 50f;
            amorti.HorizontalAlignment = Element.ALIGN_LEFT;

            infor.tablaAmorti();

            decimal creditoAut = 0;
            int tasaAmor = 0;
            string fecha = "";
            decimal amortizacion = 0;
            int cont = 1;
            int plazo = 0;
            DateTime fechapag;
            decimal tasaanual = 0;
            decimal tasasema = 0;
            decimal inter2 = 0;
            decimal interes = 0;
            decimal mpagoo = 0;


            PdfPCell nuAmor = new PdfPCell(new Phrase("Número de Amortización", fuente6));
            nuAmor.HorizontalAlignment = Element.ALIGN_CENTER;
            nuAmor.VerticalAlignment = Element.ALIGN_MIDDLE;
            amorti.AddCell(nuAmor);

            PdfPCell MontoAmor = new PdfPCell(new Phrase("Monto de Amortización (capital más interes ordinarios)", fuente6));
            MontoAmor.HorizontalAlignment = Element.ALIGN_CENTER;
            MontoAmor.VerticalAlignment = Element.ALIGN_MIDDLE;
            amorti.AddCell(MontoAmor);

            PdfPCell fechaMor = new PdfPCell(new Phrase("Fecha", fuente6));
            fechaMor.HorizontalAlignment = Element.ALIGN_CENTER;
            fechaMor.VerticalAlignment = Element.ALIGN_MIDDLE;
            amorti.AddCell(fechaMor);

            PdfPTable amorti1 = new PdfPTable(3);
            amorti1.SetWidths(new float[] { 30, 40, 30 });
            amorti1.WidthPercentage = 50f;
            amorti1.HorizontalAlignment = Element.ALIGN_LEFT;


            PdfPCell nuAmor1 = new PdfPCell(new Phrase("Número de Amortización", fuente6));
            nuAmor1.HorizontalAlignment = Element.ALIGN_CENTER;
            nuAmor1.VerticalAlignment = Element.ALIGN_MIDDLE;
            amorti1.AddCell(nuAmor1);

            PdfPCell MontoAmor1 = new PdfPCell(new Phrase("Monto de Amortización (capital más interes ordinarios)", fuente6));
            MontoAmor1.HorizontalAlignment = Element.ALIGN_CENTER;
            MontoAmor1.VerticalAlignment = Element.ALIGN_MIDDLE;
            amorti1.AddCell(MontoAmor1);

            PdfPCell fechaMor1 = new PdfPCell(new Phrase("Fecha", fuente6));
            fechaMor1.HorizontalAlignment = Element.ALIGN_CENTER;
            fechaMor1.VerticalAlignment = Element.ALIGN_MIDDLE;
            amorti1.AddCell(fechaMor1);

            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];



                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    creditoAut = Convert.ToDecimal(r[3]);
                    plazo = Convert.ToInt32(r[0]);
                    tasaAmor = Convert.ToInt32(r[1]);
                    fecha = Convert.ToString(r[2]);
                    amortizacion = creditoAut / plazo;
                    fechapag = Convert.ToDateTime(fecha);
                    fechapag = fechapag.AddDays(7);
                    tasaanual = (((tasaAmor / Convert.ToDecimal(1.16)) * 360) / 28);
                    tasasema = tasaanual / (360) * 7 * Convert.ToDecimal(1.16);
                    inter2 = creditoAut * plazo * Convert.ToDecimal(tasasema) / 100;
                    interes = inter2 / plazo;
                    mpagoo = amortizacion + interes;


                    for (int i = 1; i <= (plazo / 2); i++)
                    {

                        PdfPCell nuAmorr = new PdfPCell(new Phrase("" + cont, fuente6));
                        nuAmorr.HorizontalAlignment = Element.ALIGN_CENTER;
                        nuAmorr.VerticalAlignment = Element.ALIGN_CENTER;
                        amorti.AddCell(nuAmorr);

                        PdfPCell MontoAmorr = new PdfPCell(new Phrase("" + mpagoo.ToString("C2"), fuente6));
                        MontoAmorr.HorizontalAlignment = Element.ALIGN_CENTER;
                        MontoAmorr.VerticalAlignment = Element.ALIGN_CENTER;
                        amorti.AddCell(MontoAmorr);

                        PdfPCell fechaMorr = new PdfPCell(new Phrase("" + Convert.ToDateTime(fechapag).ToString("dd/MM/yyyy"), fuente6));
                        fechaMorr.HorizontalAlignment = Element.ALIGN_CENTER;
                        fechaMorr.VerticalAlignment = Element.ALIGN_CENTER;
                        amorti.AddCell(fechaMorr);

                        cont++;
                        fechapag = fechapag.AddDays(7);
                    }

                    for (int y = 1; y <= (plazo / 2); y++)
                    {
                        PdfPCell nuAmor2 = new PdfPCell(new Phrase(" " + cont, fuente6));
                        nuAmor2.HorizontalAlignment = Element.ALIGN_CENTER;
                        nuAmor2.VerticalAlignment = Element.ALIGN_CENTER;
                        amorti1.AddCell(nuAmor2);

                        PdfPCell MontoAmor2 = new PdfPCell(new Phrase(" " + mpagoo.ToString("C2"), fuente6));
                        MontoAmor2.HorizontalAlignment = Element.ALIGN_CENTER;
                        MontoAmor2.VerticalAlignment = Element.ALIGN_CENTER;
                        amorti1.AddCell(MontoAmor2);

                        PdfPCell fechaMor2 = new PdfPCell(new Phrase(" " + Convert.ToDateTime(fechapag).ToString("dd/MM/yyyy"), fuente6));
                        fechaMor2.HorizontalAlignment = Element.ALIGN_CENTER;
                        fechaMor2.VerticalAlignment = Element.ALIGN_CENTER;
                        amorti1.AddCell(fechaMor2);


                        cont++;
                        fechapag = fechapag.AddDays(7);
                    }


                }
            }






            //tabla continuacion de monto amortización



            //union de tablas
            PdfPTable unitab = new PdfPTable(2);
            unitab.SetWidths(new float[] { 50, 50 });
            unitab.WidthPercentage = 100f;
            unitab.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell tab1 = new PdfPCell(amorti);
            tab1.HorizontalAlignment = Element.ALIGN_CENTER;
            tab1.VerticalAlignment = Element.ALIGN_CENTER;
            unitab.AddCell(tab1);

            PdfPCell tab2 = new PdfPCell(amorti1);
            tab2.HorizontalAlignment = Element.ALIGN_CENTER;
            tab2.VerticalAlignment = Element.ALIGN_CENTER;
            unitab.AddCell(tab2);
            documento.Add(unitab);
            documento.Add(new Paragraph(" "));

            //segundo texto
            PdfPTable text2 = new PdfPTable(1);
            text2.SetWidths(new float[] { 100 });
            text2.WidthPercentage = 100f;
            text2.HorizontalAlignment = Element.ALIGN_LEFT;
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPCell texto2 = new PdfPCell(new Phrase("En caso de que la ficha de pago no sea un día hábil, el pago deberá efectuarse el día hábil siguiente. Día hábil significará cualquier día, distinto a un sábado o domingo, en el que los bancos se encuentren abiertos para llevar a cabo sus operaciones. El presente título de crédito documenta una obligación del SUSCRIPTOR frente a ''" + nameEmp + "'' cuyo cumplimiento fue pactado a plazos, por lo que desde ahora los suscriptores manifiestan que en que caso de falta de pago total o parcial de cualquier amortización consignada en este pagaré, genera el derecho de ser exigible el monto total del saldo del adeudo reconocido en este pagaré, a partir de la fecha en que se incurra en moratoria de pago; en cuyo caso, a partir del incumplimiento, causará moratorios a tasa pactada. Para los efectos de los artículos 128, 160, 165 de la General de Títulos y Operaciones de Crédito, los plazos de caducidad y prescripción de las acciones cambiarias de este pagaré por falta de presentación para su cobro, se prorrogarán irrevocablmente hasta 5 (cinco) años contados a partir de la decha de este pagaré, en el entendido de que dicha extensión no impedirá la presentación de este pagaré con anterioridad a dicha fecha. Este pagaré se interpretará y se regirá bajos las leyes de los Estados Unidos Mexicanos. Para todo lo relacioado con el presente pagaré, los SUSCRIPTORES se someten a la jurisdicción que po razón de sus domicilios pudiere corresponderle en lo futuro. Este pagaré está compuesto por una hoja útil, escritas por ambas y se extiende y firma en Ciudad de México el dia " + Convert.ToDateTime(fecha).ToString("dd/MM/yyyy") + ".", fuente6));
            texto2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            texto2.VerticalAlignment = Element.ALIGN_CENTER;
            texto2.Border = 0;
            text2.AddCell(texto2);
            documento.Add(text2);
            documento.Add(new Paragraph(" "));

            documento.Add(new Paragraph(" ")); documento.Add(new Paragraph(" "));
            //tabla del suscriptor
            PdfPTable suscri = new PdfPTable(4);
            suscri.SetWidths(new float[] { 30, 30, 10, 30 });
            suscri.WidthPercentage = 100f;
            suscri.HorizontalAlignment = Element.ALIGN_CENTER;

            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = sesiones[4];
                    int idcliente2 = cliente;
            infor.idcliente = idcliente2;
            infor.obtieneInfoDetalle();

            string nomcli = "";
            string curpcli = "";
            decimal montoCli = 0;
            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    nomcli = Convert.ToString(r[0]);
                    curpcli = Convert.ToString(r[1]);
                    montoCli = Convert.ToDecimal(r[2]);
                }
            }

            PdfPCell nomSus = new PdfPCell(new Phrase("NOMBRE DEL SUSCRIPTOR", fuente6));
            nomSus.HorizontalAlignment = Element.ALIGN_CENTER;
            nomSus.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(nomSus);

            PdfPCell curpSus = new PdfPCell(new Phrase("CURP", fuente6));
            curpSus.HorizontalAlignment = Element.ALIGN_CENTER;
            curpSus.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(curpSus);

            PdfPCell impoSus = new PdfPCell(new Phrase("IMPORTE", fuente6));
            impoSus.HorizontalAlignment = Element.ALIGN_CENTER;
            impoSus.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(impoSus);

            PdfPCell firmaSus = new PdfPCell(new Phrase("FIRMA", fuente6));
            firmaSus.HorizontalAlignment = Element.ALIGN_CENTER;
            firmaSus.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(firmaSus);

            PdfPCell nomSus1 = new PdfPCell(new Phrase(" " + nomcli, fuente6));
            nomSus1.HorizontalAlignment = Element.ALIGN_CENTER;
            nomSus1.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(nomSus1);

            PdfPCell curpSus1 = new PdfPCell(new Phrase(" " + curpcli, fuente6));
            curpSus1.HorizontalAlignment = Element.ALIGN_CENTER;
            curpSus1.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(curpSus1);

            PdfPCell impoSus1 = new PdfPCell(new Phrase(" " + montoCli.ToString("C2"), fuente6));
            impoSus1.HorizontalAlignment = Element.ALIGN_CENTER;
            impoSus1.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(impoSus1);

            PdfPCell firmaSus1 = new PdfPCell(new Phrase(" ", fuente6));
            firmaSus1.HorizontalAlignment = Element.ALIGN_CENTER;
            firmaSus1.VerticalAlignment = Element.ALIGN_CENTER;
            suscri.AddCell(firmaSus1);
            documento.Add(suscri);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));

                    // firmas "avales"
                    PdfPTable firAva = new PdfPTable(2);
            firAva.SetWidths(new float[] { 50, 50 });
            firAva.DefaultCell.Border = 0;
            firAva.WidthPercentage = 100f;
            firAva.HorizontalAlignment = Element.ALIGN_CENTER;



            PdfPCell encaFir = (new PdfPCell(new Phrase("LOS AVALES", fuente6)) { Colspan = 2, Rowspan = 2 });
            encaFir.HorizontalAlignment = Element.ALIGN_CENTER;
            encaFir.VerticalAlignment = Element.ALIGN_CENTER;
            encaFir.Border = 0;
            firAva.AddCell(encaFir);

            PdfPCell encaFir1 = (new PdfPCell(new Phrase(" ", fuente6)) { Colspan = 2, Rowspan = 2 });
            encaFir1.HorizontalAlignment = Element.ALIGN_CENTER;
            encaFir1.VerticalAlignment = Element.ALIGN_CENTER;
            encaFir1.Border = 0;
            firAva.AddCell(encaFir1);

            PdfPCell encaFir2 = (new PdfPCell(new Phrase(" ", fuente6)) { Colspan = 2, Rowspan = 2 });
            encaFir2.HorizontalAlignment = Element.ALIGN_CENTER;
            encaFir2.VerticalAlignment = Element.ALIGN_CENTER;
            encaFir2.Border = 0;
            firAva.AddCell(encaFir2);

            PdfPCell encaFir3 = (new PdfPCell(new Phrase(" ", fuente6)) { Colspan = 2, Rowspan = 2 });
            encaFir3.HorizontalAlignment = Element.ALIGN_CENTER;
            encaFir3.VerticalAlignment = Element.ALIGN_CENTER;
            encaFir3.Border = 0;
            firAva.AddCell(encaFir3);



            PdfPCell raya1 = new PdfPCell(new Phrase("_________________________________________", fuente6));
            raya1.HorizontalAlignment = Element.ALIGN_CENTER;
            raya1.VerticalAlignment = Element.ALIGN_CENTER;
            raya1.Border = 0;
            firAva.AddCell(raya1);


            PdfPCell raya2 = new PdfPCell(new Phrase("_________________________________________", fuente6));
            raya2.HorizontalAlignment = Element.ALIGN_CENTER;
            raya2.VerticalAlignment = Element.ALIGN_CENTER;
            raya2.Border = 0;
            firAva.AddCell(raya2);

            PdfPCell firmi1 = new PdfPCell(new Phrase("primer nombre aqui", fuente6));
            firmi1.HorizontalAlignment = Element.ALIGN_CENTER;
            firmi1.VerticalAlignment = Element.ALIGN_CENTER;
            firmi1.Border = 0;
            firAva.AddCell(firmi1);

            PdfPCell firmi2 = new PdfPCell(new Phrase("primer nombre aqui", fuente6));
            firmi2.HorizontalAlignment = Element.ALIGN_CENTER;
            firmi2.VerticalAlignment = Element.ALIGN_CENTER;
            firmi2.Border = 0;
            firAva.AddCell(firmi2);

            PdfPCell direc1 = new PdfPCell(new Phrase("primera dirección", fuente6));
            direc1.HorizontalAlignment = Element.ALIGN_CENTER;
            direc1.VerticalAlignment = Element.ALIGN_CENTER;
            direc1.Border = 0;
            firAva.AddCell(direc1);

            PdfPCell direc2 = new PdfPCell(new Phrase("primera dirección", fuente6));
            direc2.HorizontalAlignment = Element.ALIGN_CENTER;
            direc2.VerticalAlignment = Element.ALIGN_CENTER;
            direc2.Border = 0;
            firAva.AddCell(direc2);
            documento.Add(firAva);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPTable firAva2 = new PdfPTable(2);
            firAva2.SetWidths(new float[] { 50, 50 });
            firAva2.DefaultCell.Border = 0;
            firAva2.WidthPercentage = 100f;
            firAva2.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell raya3 = new PdfPCell(new Phrase("_________________________________________", fuente6));
            raya3.HorizontalAlignment = Element.ALIGN_CENTER;
            raya3.VerticalAlignment = Element.ALIGN_CENTER;
            raya3.Border = 0;
            firAva2.AddCell(raya3);


            PdfPCell raya4 = new PdfPCell(new Phrase("_________________________________________", fuente6));
            raya4.HorizontalAlignment = Element.ALIGN_CENTER;
            raya4.VerticalAlignment = Element.ALIGN_CENTER;
            raya4.Border = 0;
            firAva2.AddCell(raya4);

            PdfPCell firmi3 = new PdfPCell(new Phrase("primer nombre aqui", fuente6));
            firmi3.HorizontalAlignment = Element.ALIGN_CENTER;
            firmi3.VerticalAlignment = Element.ALIGN_CENTER;
            firmi3.Border = 0;
            firAva2.AddCell(firmi3);

            PdfPCell firmi4 = new PdfPCell(new Phrase("primer nombre aqui", fuente6));
            firmi4.HorizontalAlignment = Element.ALIGN_CENTER;
            firmi4.VerticalAlignment = Element.ALIGN_CENTER;
            firmi4.Border = 0;
            firAva2.AddCell(firmi4);

            PdfPCell direc3 = new PdfPCell(new Phrase("primera dirección", fuente6));
            direc3.HorizontalAlignment = Element.ALIGN_CENTER;
            direc3.VerticalAlignment = Element.ALIGN_CENTER;
            direc3.Border = 0;
            firAva2.AddCell(direc3);

            PdfPCell direc4 = new PdfPCell(new Phrase("primera dirección", fuente6));
            direc4.HorizontalAlignment = Element.ALIGN_CENTER;
            direc4.VerticalAlignment = Element.ALIGN_CENTER;
            direc4.Border = 0;
            firAva2.AddCell(direc4);
            documento.Add(firAva2);
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
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));

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