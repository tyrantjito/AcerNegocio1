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


public partial class Contrato : System.Web.UI.Page
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
    protected void lnkImprimirContrato_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;
        // Tipo de Font que vamos utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9 , iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER_LANDSCAPE);
        documento.AddTitle(" Contrato ");
        documento.AddCreator("AserNegocio");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\Contrato_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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


            //encabezado
            PdfPTable tablaEncabezado = new PdfPTable(1);
            tablaEncabezado.SetWidths(new float[] { 100 });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;
            tablaEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;


            PdfPCell titulo = new PdfPCell(new Phrase("CARATULA INTEGRANTE DEL CONTRATO DE CREDITO SIMPLE", fuente1));
            titulo.HorizontalAlignment = Element.ALIGN_CENTER;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_CENTER;
            tablaEncabezado.AddCell(titulo);
            documento.Add(tablaEncabezado);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            Cntra infor = new Cntra();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = Convert.ToInt32(Label15.Text);
            infor.obtieneInfoEncabezado();

            string NGrupo = "";
            decimal tasa = 0;
            decimal monto = 0;
            int plazo = 0;
            string fecha = "";
            DateTime fechat;
            string ofec = "";
            int plazosum = 0;
            

            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    NGrupo = Convert.ToString(r[6]);
                    tasa = Convert.ToDecimal(r[10]);
                    monto = Convert.ToDecimal(r[8]);
                    plazo = Convert.ToInt32(r[9]);
                    plazosum = plazo * 7;
                    fecha = Convert.ToString(r[4]);
                    fechat = Convert.ToDateTime(fecha);
                    fechat = fechat.AddDays(plazosum);
                    ofec = Convert.ToString( fechat);
                }
            }

            PdfPTable tab1 = new PdfPTable(4);
            tab1.SetWidths(new float[] { 30, 20, 25, 25 });
            tab1.DefaultCell.Border = 0;
            tab1.WidthPercentage = 100f;
            tab1.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell nombreCli = (new PdfPCell(new Phrase("Nombre del Cliente: "+NGrupo, fuente6)) { Colspan = 4 });
            nombreCli.HorizontalAlignment = Element.ALIGN_LEFT;
            nombreCli.VerticalAlignment = Element.ALIGN_CENTER;
            nombreCli.FixedHeight = 30f;
            tab1.AddCell(nombreCli);

            PdfPCell nomProd = (new PdfPCell(new Phrase("Nombre del Producto: ", fuente6)) { Colspan = 2 });
            nomProd.HorizontalAlignment = Element.ALIGN_LEFT;
            nomProd.VerticalAlignment = Element.ALIGN_CENTER;
            nomProd.FixedHeight = 30f;
            tab1.AddCell(nomProd);

            PdfPCell tipCre = (new PdfPCell(new Phrase("Tipo de Crédito: ", fuente6)) { Colspan = 2 });
            tipCre.HorizontalAlignment = Element.ALIGN_LEFT;
            tipCre.VerticalAlignment = Element.ALIGN_CENTER;
            tipCre.FixedHeight = 30f;
            tab1.AddCell(tipCre);

            PdfPCell cat = (new PdfPCell(new Phrase("CAT (Costo Anual Total)", fuente6)));
            cat.HorizontalAlignment = Element.ALIGN_CENTER;
            cat.VerticalAlignment = Element.ALIGN_MIDDLE;
            cat.FixedHeight = 70f;
            tab1.AddCell(cat);

            PdfPCell cat1 = (new PdfPCell(new Phrase("Tasa de Interés \n Anual Ordinaria y \n Monetaria \n"+tasa+"%", fuente6)));
            cat1.HorizontalAlignment = Element.ALIGN_CENTER;
            cat1.VerticalAlignment = Element.ALIGN_MIDDLE;
            cat1.FixedHeight = 70f;
            tab1.AddCell(cat1);

            PdfPCell cat2 = (new PdfPCell(new Phrase("Monto del Crédito \n"+monto.ToString("C2"), fuente6)));
            cat2.HorizontalAlignment = Element.ALIGN_CENTER;
            cat2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cat2.FixedHeight = 70f;
            tab1.AddCell(cat2);

            PdfPCell cat3 = (new PdfPCell(new Phrase("Monto Total A Pagar \n", fuente6)));
            cat3.HorizontalAlignment = Element.ALIGN_CENTER;
            cat3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cat3.FixedHeight = 70f;
            tab1.AddCell(cat3);
            documento.Add(tab1);

            PdfPTable tab2 = new PdfPTable(3);
            tab2.SetWidths(new float[] { 30, 35, 35 });
            tab2.DefaultCell.Border = 0;
            tab2.WidthPercentage = 100f;
            tab2.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell plaCre = (new PdfPCell(new Phrase("Plazo del Crédito \n "+plazo+" semanas", fuente6)));
            plaCre.HorizontalAlignment = Element.ALIGN_CENTER;
            plaCre.VerticalAlignment = Element.ALIGN_MIDDLE;
            plaCre.FixedHeight = 35f;
            tab2.AddCell(plaCre);

            PdfPCell fechInCre = (new PdfPCell(new Phrase("Fecha de Inicio del Crédito \n "+ Convert.ToDateTime(fecha).ToString("dd/MM/yyyy"), fuente6)));
            fechInCre.HorizontalAlignment = Element.ALIGN_CENTER;
            fechInCre.VerticalAlignment = Element.ALIGN_MIDDLE;
            fechInCre.FixedHeight = 35f;
            tab2.AddCell(fechInCre);

            PdfPCell fechFnCre = (new PdfPCell(new Phrase("Fecha de término del Crédito \n "+ Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6)));
            fechFnCre.HorizontalAlignment = Element.ALIGN_CENTER;
            fechFnCre.VerticalAlignment = Element.ALIGN_MIDDLE;
            fechFnCre.FixedHeight = 35f;
            tab2.AddCell(fechFnCre);

            PdfPCell comiRe = (new PdfPCell(new Phrase("COMICIONES RELEVANTES \n \n $100 (Cien Pesos 00/100 M.N.) por gastos de cobranza por visita a los ''" + NGrupo + "'' que incumplan con los datos programados.", fuente6)) { Colspan = 3 });
            comiRe.HorizontalAlignment = Element.ALIGN_CENTER;
            comiRe.VerticalAlignment = Element.ALIGN_MIDDLE;
            comiRe.FixedHeight = 45f;
            tab2.AddCell(comiRe);

            PdfPCell advert = (new PdfPCell(new Phrase("ADVERTENCIA \n \n Incumplir tus obligaciones te puede generar comisiones e intereses moratorios. \n \n Contratar créditos que excedan de tu capacidad de pago afecta tu historial crediticio \n \n El aval, obligado solidario o coacreditado responderá como obligado principal por el total del pago frente a la Institución Financiera", fuente6)) { Colspan = 3 });
            advert.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            advert.VerticalAlignment = Element.ALIGN_MIDDLE;
            advert.FixedHeight = 90f;
            tab2.AddCell(advert);

            PdfPCell seguros = (new PdfPCell(new Phrase("SEGUROS", fuente6)) { Colspan = 3 });
            seguros.HorizontalAlignment = Element.ALIGN_CENTER;
            seguros.VerticalAlignment = Element.ALIGN_MIDDLE;
            seguros.FixedHeight = 20f;
            tab2.AddCell(seguros);

            PdfPCell seguro1 = (new PdfPCell(new Phrase("Seguro:", fuente6)));
            seguro1.HorizontalAlignment = Element.ALIGN_LEFT;
            seguro1.VerticalAlignment = Element.ALIGN_MIDDLE;
            seguro1.FixedHeight = 20f;
            tab2.AddCell(seguro1);

            PdfPCell Asegur = (new PdfPCell(new Phrase("Aseguradora:", fuente6)));
            Asegur.HorizontalAlignment = Element.ALIGN_LEFT;
            Asegur.VerticalAlignment = Element.ALIGN_MIDDLE;
            Asegur.FixedHeight = 20f;
            tab2.AddCell(Asegur);

            PdfPCell claus = (new PdfPCell(new Phrase("Clausula:", fuente6)));
            claus.HorizontalAlignment = Element.ALIGN_LEFT;
            claus.VerticalAlignment = Element.ALIGN_MIDDLE;
            claus.FixedHeight = 20f;
            tab2.AddCell(claus);

            PdfPCell edoCue = (new PdfPCell(new Phrase("ESTADO DE CUENTA: \n Enviar a:   Domicilio:                           Consulta via internet:                           Consulta en la oficina de servicios:", fuente6)) { Colspan = 3 });
            edoCue.HorizontalAlignment = Element.ALIGN_LEFT;
            edoCue.VerticalAlignment = Element.ALIGN_MIDDLE;
            edoCue.FixedHeight = 40f;
            tab2.AddCell(edoCue);

            Cntra datEmpre = new Cntra();
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


            PdfPCell aclaraRe = (new PdfPCell(new Phrase("Aclaraciones y reclamos: \n \n Unidad Especializada de Atención a Usuarios: \n \n Domicilio: "+direEmp+" \n \n Télefono: "+telEmpresa+"  Correo Electrónico: "+correoEmp+" \n \n Página de internet: "+pagWeb, fuente6)) { Colspan = 3 });
            aclaraRe.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            aclaraRe.VerticalAlignment = Element.ALIGN_MIDDLE;
            aclaraRe.FixedHeight = 70f;
            tab2.AddCell(aclaraRe);

            PdfPCell regis = (new PdfPCell(new Phrase("Registro de contrato de Adhesión Número \n \n Comisión Nacional para la Protección y Defensa de los Usuarios de Servicios Financieros (CONDUSEF) \n \n Teléfono: 01-800-998-080 y 5340 0999. Página de internet: https://www.condusef.com", fuente6)) { Colspan = 3 });
            regis.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            regis.VerticalAlignment = Element.ALIGN_MIDDLE;
            regis.FixedHeight = 70f;
            tab2.AddCell(regis);



            documento.Add(tab2);

            documento.Add(new Paragraph(" "));

            documento.NewPage();
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPTable tablaEncabezado1 = new PdfPTable(1);
            tablaEncabezado1.SetWidths(new float[] { 100 });
            tablaEncabezado1.DefaultCell.Border = 0;
            tablaEncabezado1.WidthPercentage = 100f;
            tablaEncabezado1.HorizontalAlignment = Element.ALIGN_CENTER;

           
          
            infor.obtieneNombres();
            string nombres = "";
            string nombrescom = "";

            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    nombres = Convert.ToString(r[0]);
                    nombrescom = nombrescom + ", " + nombres;
                   
                }
            }

            PdfPCell titulo43 = new PdfPCell(new Phrase("CONTRATO DE APERTURA DE CRÉDITO SIMPLE EN SU MODALIDAD DE CRÉDITO COMUNITARIO QUE CELEBRAN POR PARTE DE UNA " + nameEmp + " PRESENTADA EN ESTE ACTO POR EL C. " + represen +", A QUIEN EN LO SUCESIVO SE LE DENOMINARA COMO ''" + nameCort + "'' Y POR LA OTRA PARTE LAS CC. " + nombrescom + ", A QUIEN EN LO SUCESIVO SE LES DENOMINARA COMO LOS ''" + NGrupo + "'', QUIENES DE CONFORMIDAD HAN MANIFETADO SU PLENA VOLUNTAD DE SUJETARSE AL TENOR DE LAS SIGUIENTES ACLARACIONES Y CLAUSULAS:", fuente10));
            titulo43.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            titulo43.Border = 0;
            titulo43.VerticalAlignment = Element.ALIGN_CENTER;
            tablaEncabezado1.AddCell(titulo43);
            documento.Add(tablaEncabezado1);

            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            //tablas que se dividen
            PdfPTable Decl = new PdfPTable(2);
            Decl.SetWidths(new float[] { 5, 95 });
            Decl.DefaultCell.Border = 0;
            Decl.WidthPercentage = 50f;
            Decl.DefaultCell.Border = 0;
            Decl.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell deCont = (new PdfPCell(new Phrase("DECLARACIONES\n", fuente8)) { Colspan = 2 });
            deCont.HorizontalAlignment = Element.ALIGN_CENTER;
            deCont.Border = 0;
            deCont.VerticalAlignment = Element.ALIGN_CENTER;
            deCont.FixedHeight = 30f;
            Decl.AddCell(deCont);

            PdfPCell de1 = (new PdfPCell(new Phrase("I. Declara ''"+nameEmp+"''  por conducto de su representante, que: \n \n", fuente8)) { Colspan = 2 });
            de1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            de1.Border = 0;
            de1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(de1);

            PdfPCell deA = (new PdfPCell(new Phrase("a)", fuente6)));
            deA.HorizontalAlignment = Element.ALIGN_CENTER;
            deA.Border = 0;
            deA.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deA);

            PdfPCell deA1 = (new PdfPCell(new Phrase("Es una Sociedad de Objetivo Múltiple, Entidad No Regulada contituida y en operación  de conformidad con las leyes de los Estados Unidos Mexicanos\n \n", fuente6)));
            deA1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deA1.Border = 0;
            deA1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deA1);

            PdfPCell deB = (new PdfPCell(new Phrase("b)", fuente6)));
            deB.HorizontalAlignment = Element.ALIGN_CENTER;
            deB.Border = 0;
            deB.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deB);

            PdfPCell deB1 = (new PdfPCell(new Phrase("En términos de la Ley general de Organizaciones y Actividades Auxiliares de Crédito, su representada no requiere autorización de la Secretaria de Hacienda y Crédito Público para su constitución y operación y que no se encuentra sujeta a la supervisión de la Comisión Nacional Bancaria y de Valores, sin embargo, en las operaciones de crédito, factoraje y arrendamiento financiero se encuentra bajo supervisión y vigilancia de la Comisión Nacional para la Prtección y Defensa del Usuario de Servicios Financieros (CONDUSEF), por lo que se encuentra inscrita en el Sistema de Registro de Prestadores de Servicios Financieros (SIPRES).\n \n", fuente6)));
            deB1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deB1.Border = 0;
            deB1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deB1);

            PdfPCell deC = (new PdfPCell(new Phrase("c)", fuente6)));
            deC.HorizontalAlignment = Element.ALIGN_CENTER;
            deC.Border = 0;
            deC.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deC);

            PdfPCell deC1 = (new PdfPCell(new Phrase("Está inscrita en el Registro Federal de Constituyentes con la clave:  "+rfcEmp+"\n \n", fuente6)));
            deC1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deC1.Border = 0;
            deC1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deC1);

            PdfPCell deD = (new PdfPCell(new Phrase("d)", fuente6)));
            deD.HorizontalAlignment = Element.ALIGN_CENTER;
            deD.Border = 0;
            deD.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deD);

            PdfPCell deD1 = (new PdfPCell(new Phrase("Su domicilio para efectos del presente contrato es el ubicado en "+ direEmp, fuente6)));
            deD1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deD1.Border = 0;
            deD1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deD1);

            PdfPCell deE = (new PdfPCell(new Phrase("e)", fuente6)));
            deE.HorizontalAlignment = Element.ALIGN_CENTER;
            deE.Border = 0;
            deE.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deE);

            PdfPCell deE1 = (new PdfPCell(new Phrase("Su Representante cuenta con las facultades necesarias para acudir en su nombre y representacion a la celebración y ejecución del presente contrato mismas no le han sido modificadas, revocadas, ni limitadas en forma alguna.\n \n", fuente6)));
            deE1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deE1.Border = 0;
            deE1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deE1);

            PdfPCell deF = (new PdfPCell(new Phrase("f)", fuente6)));
            deF.HorizontalAlignment = Element.ALIGN_CENTER;
            deF.Border = 0;
            deF.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deF);

            PdfPCell deF1 = (new PdfPCell(new Phrase("Considerando las declaraciones de los ''" + NGrupo + "'' está dispuesto a otorgarles el crédito objeto del presente contrato.\n \n", fuente6)));
            deF1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deF1.Border = 0;
            deF1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deF1);

            PdfPCell de2 = (new PdfPCell(new Phrase("II. Declaran los ''" + NGrupo + "'' en lo individual y en conjunto , que: \n \n", fuente8)) { Colspan = 2 });
            de2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            de2.Border = 0;
            de2.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(de2);

            PdfPCell deG = (new PdfPCell(new Phrase("g)", fuente6)));
            deG.HorizontalAlignment = Element.ALIGN_CENTER;
            deG.Border = 0;
            deG.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deG);

            PdfPCell deG1 = (new PdfPCell(new Phrase("Son personas físicas de nacionadad mexicana, con pleno goce y ejercicio de sus facultades para la celebración del presente contrato.\n \n", fuente6)));
            deG1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deG1.Border = 0;
            deG1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deG1);

            PdfPCell deH = (new PdfPCell(new Phrase("h)", fuente6)));
            deH.HorizontalAlignment = Element.ALIGN_CENTER;
            deH.Border = 0;
            deH.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deH);

            PdfPCell deH1 = (new PdfPCell(new Phrase("Sus datos generales son los que han quedado asentados los formatos ficha de datos y solicitud de crédito en la que formalizan su decisión de obtener el crédito onjeto del presente instumento, en términos ahi disputados.\n \n", fuente6)));
            deH1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deH1.Border = 0;
            deH1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl.AddCell(deH1);


            PdfPTable Decl1 = new PdfPTable(2);
            Decl1.SetWidths(new float[] { 7, 93 });
            Decl1.DefaultCell.Border = 0;
            Decl1.WidthPercentage = 50f;
            Decl1.DefaultCell.Border = 0;
            Decl1.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell deI = (new PdfPCell(new Phrase("i)", fuente6)));
            deI.HorizontalAlignment = Element.ALIGN_CENTER;
            deI.Border = 0;
            deI.VerticalAlignment = Element.ALIGN_CENTER;
            Decl1.AddCell(deI);

            PdfPCell deI1 = (new PdfPCell(new Phrase("Previo a la celebración del presente contrato, ''"+nameCort+"'' dio a conocer a los ''" + NGrupo + "'' el Costo Anual Total (CAT) de conformidad con la metodología del Banco de México, mismo que se indica en la caratula del presente instrumento, el monto de los pagos parciales, la forma y periodicidad para liquidarlos, los accesorios, el monto y detalle de los cargos por incumplimiento, así como el derecho que se tiene que liquidar anticipadamente la operación y las condiciones para tal efecto, así como los intereses ordinariosy moratorios, la forma de calcular los mismos y el tipo de tasa aplicable. \n \n", fuente6)));
            deI1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deI1.Border = 0;
            deI1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl1.AddCell(deI1);

            PdfPCell deJ = (new PdfPCell(new Phrase("j)", fuente6)));
            deJ.HorizontalAlignment = Element.ALIGN_CENTER;
            deJ.Border = 0;
            deJ.VerticalAlignment = Element.ALIGN_CENTER;
            Decl1.AddCell(deJ);

            PdfPCell deJ1 = (new PdfPCell(new Phrase("Libremente han constituido un Grupo de personas que por cuenta propia desarrollan una actividad productivalícita, y que el crédito solicitado será destinado a dichas actividades, mismas que se indican en la solicitud de crédito comunitario y han convenidoen designar al Grupo como " + NGrupo+" \n \n", fuente6)));
            deJ1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deJ1.Border = 0;
            deJ1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl1.AddCell(deJ1);

            PdfPCell deK = (new PdfPCell(new Phrase("k)", fuente6)));
            deK.HorizontalAlignment = Element.ALIGN_CENTER;
            deK.Border = 0;
            deK.VerticalAlignment = Element.ALIGN_CENTER;
            Decl1.AddCell(deK);

            PdfPCell deK1 = (new PdfPCell(new Phrase("Los ''" + NGrupo + "'' cuentan con un Comité de Administración integrado por Presidenta: SANCHEZ HERNANDEZ BERTHA ALICIA; Secretaria: ANGELES MALDONADOOLGA; Tesorera: HERNANDEZ MARTINEZ GRICELDA y Supervisora: HUATO BLANCO MA. DEL ROSARIO, la cual tendrá la representación de todos los demás ''" + NGrupo + "'' frente a ''"+nameCort+"'', en relación con las obligaciones de pago asumidas con motivo de la suscripción del presente contrato.\n \n", fuente6)));
            deK1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deK1.Border = 0;
            deK1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl1.AddCell(deK1);

            PdfPCell deL = (new PdfPCell(new Phrase("l)", fuente6)));
            deL.HorizontalAlignment = Element.ALIGN_CENTER;
            deL.Border = 0;
            deL.VerticalAlignment = Element.ALIGN_CENTER;
            Decl1.AddCell(deL);

            PdfPCell deL1 = (new PdfPCell(new Phrase("Todos y cada uno de los ''" + NGrupo + "'' han solicitado individualmente a ''"+nameCort+"'', el otorgamiento de un crédito y han acordado como GRUPO, en constituirse cada unos de ellos en ''OBLIGADOS SOLIDARIOS'' de cada uno de los demás ''"+NGrupo+"'' frente a ''"+nameCort+"'', en relación con las obligacionesde pago asumidas con motivo de la suscripción del presente contrato. \n \n", fuente6)));
            deL1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deL1.Border = 0;
            deL1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl1.AddCell(deL1);

            PdfPCell deM = (new PdfPCell(new Phrase("m)", fuente6)));
            deM.HorizontalAlignment = Element.ALIGN_CENTER;
            deM.Border = 0;
            deM.VerticalAlignment = Element.ALIGN_CENTER;
            Decl1.AddCell(deM);

            PdfPCell deM1 = (new PdfPCell(new Phrase("Cada uno de los ''" + NGrupo + "'' conocen las sanciones en que incurren las personas que declaran con falsedad o haciendo creer a algien una capacidad de pago que no se tiene,  con el objeto de obtener un beneficio o un lucro indebido, así como las sanciones de carácter penal por la realizacion de dichas conductas. \n \n", fuente6)));
            deM1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deM1.Border = 0;
            deM1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl1.AddCell(deM1);

            PdfPCell deN = (new PdfPCell(new Phrase("n)", fuente6)));
            deN.HorizontalAlignment = Element.ALIGN_CENTER;
            deN.Border = 0;
            deN.VerticalAlignment = Element.ALIGN_CENTER;
            Decl1.AddCell(deN);

            PdfPCell deN1 = (new PdfPCell(new Phrase("Tienen conocimiento y otorgan su consentimiento a ''"+nameCort+"'' para que actúe como responsabe de sus datos personales y de sus datos patrimoniales y financieros que, de acuerdo con lo estipulado en el Aviso de Privacidad Integral para clientes les han sido solicitados o les sean solicitados en el futuro por ''"+nameCort+"''. De igual manera manifiestan que conocen las finalidades para las que ''DESAROLLARTE'' recaba sus datos. \n \n", fuente6)));
            deN1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deN1.Border = 0;
            deN1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl1.AddCell(deN1);

            PdfPTable de1yde2 = new PdfPTable(2);
            de1yde2.SetWidths(new float[] { 50, 50 });
            de1yde2.DefaultCell.Border = 0;
            de1yde2.WidthPercentage = 100f;
            de1yde2.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell tablita1 = (new PdfPCell(Decl));
            tablita1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            tablita1.Border = 0;
            tablita1.VerticalAlignment = Element.ALIGN_CENTER;
            de1yde2.AddCell(tablita1);

            PdfPCell tablita2 = (new PdfPCell(Decl1));
            tablita2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            tablita2.Border = 0;
            tablita2.VerticalAlignment = Element.ALIGN_CENTER;
            de1yde2.AddCell(tablita2);
            documento.Add(de1yde2);

            documento.NewPage();
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPTable Decl2 = new PdfPTable(2);
            Decl2.SetWidths(new float[] { 7, 93 });
            Decl2.DefaultCell.Border = 0;
            Decl2.WidthPercentage = 50f;
            Decl2.DefaultCell.Border = 0;
            Decl2.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell deO = (new PdfPCell(new Phrase("o)", fuente6)));
            deO.HorizontalAlignment = Element.ALIGN_CENTER;
            deO.Border = 0;
            deO.VerticalAlignment = Element.ALIGN_CENTER;
            Decl2.AddCell(deO);


            PdfPCell deO1 = (new PdfPCell(new Phrase("Conocen que el presente crédito podrá ser fondeado con recursos prevenientes de ''"+nameCort+"'' y/o de cualquier Institución financiera del país o del extranjero Banca de Desarrollo, Banca Comercial o cualquiero otra fuente de fondeo. \n \n", fuente6)));
            deO1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deO1.Border = 0;
            deO1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl2.AddCell(deO1);

            PdfPCell deP = (new PdfPCell(new Phrase("p)", fuente6)));
            deP.HorizontalAlignment = Element.ALIGN_CENTER;
            deP.Border = 0;
            deP.VerticalAlignment = Element.ALIGN_CENTER;
            Decl2.AddCell(deP);

            PdfPCell deP1 = (new PdfPCell(new Phrase("Manifiestan que ''"+nameCort+"'' ha hecho de su conocimiento que sus datos personales generales y personales, patrimoniales y financieros serán manejados de forma confidencial  y será protegidos  a través de medidas de seguridad tecnológicas, físicas y administrativas. \n \n", fuente6)));
            deP1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deP1.Border = 0;
            deP1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl2.AddCell(deP1);

            PdfPCell deQ = (new PdfPCell(new Phrase("q)", fuente6)));
            deQ.HorizontalAlignment = Element.ALIGN_CENTER;
            deQ.Border = 0;
            deQ.VerticalAlignment = Element.ALIGN_CENTER;
            Decl2.AddCell(deQ);

            PdfPCell deQ1 = (new PdfPCell(new Phrase("Que cuentan con la capacidad juridica suficiente para la celebración del presente contrato, asií como la solvencia económica y moral para cumplir con las obligaciones establecidas en el mismo. \n \n", fuente6)));
            deQ1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deQ1.Border = 0;
            deQ1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl2.AddCell(deQ1);

            PdfPCell deFin = (new PdfPCell(new Phrase("Una vez declarado lo anterior, las partes se obligan de conformidad con las siguientes clausulas: \n \n", fuente6)) { Colspan = 2 });
            deFin.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            deFin.Border = 0;
            deFin.VerticalAlignment = Element.ALIGN_CENTER;
            Decl2.AddCell(deFin);

            PdfPCell clausul = (new PdfPCell(new Phrase("CLAUSULAS \n \n \n CAPITULO PRIMERO \n DEL CONTRATO Y DEL CREDITO \n \n ", fuente8)) { Colspan = 2 });
            clausul.HorizontalAlignment = Element.ALIGN_CENTER;
            clausul.Border = 0;
            clausul.VerticalAlignment = Element.ALIGN_CENTER;
            Decl2.AddCell(clausul);

            Convertidores numText = new Convertidores();
            numText._importe = monto.ToString();

            PdfPCell PriClausul = (new PdfPCell(new Phrase("PRIMERA. OBJETO DEL CONTRATO. En términos de lo dispuesto por el artículo 29 de la ley General de Títulos y Operaciones de Crédito, ''"+nameEmp+"'' otorga en favor de los ''" + NGrupo + "'' un crédito simple bajo la modalidad de CREDITO COMUNITARIO por un importe total de " + monto.ToString("C2") + " "+numText.convierteMontoEnLetras().ToUpper().Trim()+", en lo sucesivo el ''CREDITO''. Los ''" + NGrupo + "'' esta´n de acuardo con los montos señalados en el ''ANEXO1'' del presente instrumento y se obligan cumplir en la forma, términos y condiciones concevidas en este contrato obligándose a restituir a ''"+nameCort+"'' el monto del crédito, más los impuestos, comisiones, intereses y gastos que se estipulen o generen hasta el día de la liquidación total del crédito. \n \n", fuente6)) { Colspan = 2 });
            PriClausul.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            PriClausul.Border = 0;
            PriClausul.VerticalAlignment = Element.ALIGN_JUSTIFIED;
            Decl2.AddCell(PriClausul);

            Cntra pagSem = new Cntra();
            pagSem.empresa = sesiones[2];
            pagSem.sucursal = sesiones[3];
            pagSem.idSolicitudEdita = sesiones[4];
            pagSem.obtenpagSemanal();

            decimal paSemanal = 0;
            decimal pagoSemTo = 0;

            if (Convert.ToBoolean(pagSem.retorno[0]))
            {
                DataSet ps = (DataSet)pagSem.retorno[1];


                foreach (DataRow sxp in ps.Tables[0].Rows)
                {
                    paSemanal = Convert.ToDecimal( sxp[0]);
                    pagoSemTo = pagoSemTo + paSemanal;
                }
            }
            Convertidores montoText = new Convertidores();
            montoText._importe = pagoSemTo.ToString();


            PdfPCell SegClausul = (new PdfPCell(new Phrase("SEGUNDA FORMA DE PAGO. De conformidad con la tabla de amortización que se indica en el ''ANEXO1'', los ''" + NGrupo + "'' se obligan a realizar pagos parciales semanales por la cantidad de " + pagoSemTo.ToString("C2") + " " + montoText.convierteMontoEnLetras().ToUpper().Trim() + " cada uno de ellos, y solidariamente a pagar el importe total del crédito más los intereses ordinarios a ''" + nameCort + "''mediante DECISEIS pagos con vencimiento en las fechas indicadas en el ''ANEXO 1'', mediante depósitos en la cuenta bancaria que ''" + nameCort + "'' le indique a los ''" + NGrupo + "'' a través de los instrumentos de cobranza que para el efecto implemente. \n \n", fuente6)) { Colspan = 2 });
            SegClausul.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            SegClausul.Border = 0;
            SegClausul.VerticalAlignment = Element.ALIGN_CENTER;
            Decl2.AddCell(SegClausul);

            PdfPCell SegClausul1 = (new PdfPCell(new Phrase("En caso de que por cualquier causa las instituciones financieras se negaran a recibir el pago en la fecha acordada entre las partes.Los ''" + NGrupo + "'' se obligan a realizar el pago en el domicilio de ''"+nameCort+"'' o en las sucursales que les correspondan, debiendo ''"+nameCort+"'' las circunstancia por la cual la institución de crédito se haya negado a recibir el pago. \n \n", fuente6)) { Colspan = 2 });
            SegClausul1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            SegClausul1.Border = 0;
            SegClausul1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl2.AddCell(SegClausul1);

            PdfPCell TerClausul = (new PdfPCell(new Phrase("TERCERA: DESTINO. Los ''" + NGrupo + "'' destinaran el importe del crédito para un negocio licito.", fuente6)) { Colspan = 2 });
            TerClausul.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            TerClausul.Border = 0;
            TerClausul.VerticalAlignment = Element.ALIGN_CENTER;
            Decl2.AddCell(TerClausul);

            PdfPTable Decl3 = new PdfPTable(2);
            Decl3.SetWidths(new float[] { 7, 93 });
            Decl3.DefaultCell.Border = 0;
            Decl3.WidthPercentage = 50f;
            Decl3.DefaultCell.Border = 0;
            Decl3.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell CuaClausul = (new PdfPCell(new Phrase("CUARTA. DISPOSICIÓN DEL CRÉDITO. ''"+nameCort+"'' pondrá el ''CRÉDITO'' a disposición de los ''" + NGrupo + "'' conforme a las cantidades descritas en el ANEXO 1 del presente instrumento mediante la entrega de cheques nominativos no negociables, órdenes de pago referenciadas o cualquier otro medio que condidere conveniente ''"+nameCort+"'' a favor de los ''" + NGrupo + "'', en una sola exhibición, siendo el presente  contrato el recibo más amplio que en derecho proceda respecto a la integra recepción del crédito. \n \n \n ''"+nameCort+"'' estará facultada para entregar el monto del crédito en efectivo, o en su caso, a través de depósitos o transferencias en alguna de las cuentas que los ''" + NGrupo + "'' tengan abiertas en algunas institución de crédito. \n \n", fuente6)) { Colspan = 2 });
            CuaClausul.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            CuaClausul.Border = 0;
            CuaClausul.VerticalAlignment = Element.ALIGN_CENTER;
            Decl3.AddCell(CuaClausul);

            PdfPCell llenadoCla = (new PdfPCell(new Phrase("Considerando que las CC. " + nombrescom + ", no dipusieron del crédito, el importe total final del crédito simple que ''"+nameCort+"'' otorga a los ''" + NGrupo + "'' es por $_______(___________________________________________________________________________________________________________________) \n \n Los ''" + NGrupo + "'' pordrán cancelar el presente contrato sin responsabilidad algun y sin cobro de comisiones en un periodo de diez dias hábiles posteriores a su firma; lo anterior conforme a lo dispuesto en el artículo 11 Bis de la Ley para la Transparencia u Ordenamiento de los Servicios Financieros, transcurrido el término indicado los ''" + NGrupo + "'' se verám obligados a cumplir con las obligaciones que se estípulan en el presente contratom incluyendo el pago integro del crédito otorgado y los accesorios que se pudieran generar. \n \n Cuando los ''" + NGrupo + "'' determinen cancela el crédito, conforme a los dispuesto en el párrafo anterior, deberán presentar por escrito una solicitud de cancelación en la sucursal de ''"+nameCort+"'' que les corresponda.", fuente6)) { Colspan = 2 });
            llenadoCla.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            llenadoCla.Border = 0;
            llenadoCla.VerticalAlignment = Element.ALIGN_CENTER;
            Decl3.AddCell(llenadoCla);

            PdfPCell QuiClausul = (new PdfPCell(new Phrase("QUINTA-DURACIÓN DEL CONTRATO. El presente contrato tendrá una vigencia de 112 días contados a partir de la fecha de su contratación, este plazo será improrrogable. \n \n", fuente6)) { Colspan = 2 });
            QuiClausul.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            QuiClausul.Border = 0;
            QuiClausul.VerticalAlignment = Element.ALIGN_CENTER;
            Decl3.AddCell(QuiClausul);

            PdfPCell cap2 = (new PdfPCell(new Phrase("CAPITULO SEGUNDO \n \n DEL PAGO DEL CÉDITO, INTERESES COMISIONES Y ESTADOS DE CUENTA \n \n", fuente8)) { Colspan = 2 });
            cap2.HorizontalAlignment = Element.ALIGN_CENTER;
            cap2.Border = 0;
            cap2.VerticalAlignment = Element.ALIGN_CENTER;
            Decl3.AddCell(cap2);

            PdfPCell SexClausul = (new PdfPCell(new Phrase("SEXTA. DEL PAGO DEL CREDITO. Los ''" + NGrupo + "'' se obligan a pagar a ''"+nameCort+"'' el monto del crédito mediante las amortizaciones que correspondan al plazo del crédito autorizado, mismas que corresponderán al abono del capital y los intereses ordinarios sobre saldos insolutos a razón de una rasa fija anual.\n \n Los ''" + NGrupo + "''  pagaran a ''"+nameCort+"'' intereses ordinarios con la periodicidad señalada en la tabla de amortización incluidaen el ''ANEXO 1'' del presente contrato, a razón de una Tasa fija del 60% anual más el impuesto al Valor Agregado (IVA).", fuente6)) { Colspan = 2 });
            SexClausul.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            SexClausul.Border = 0;
            SexClausul.VerticalAlignment = Element.ALIGN_CENTER;
            Decl3.AddCell(SexClausul);


            PdfPTable tabUni2 = new PdfPTable(2);
            tabUni2.SetWidths(new float[] { 50, 50 });
            tabUni2.DefaultCell.Border = 0;
            tabUni2.WidthPercentage = 100f;
            tabUni2.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell harry1 = (new PdfPCell(Decl2));
            harry1.Border = 0;
            tabUni2.AddCell(harry1);

            PdfPCell harry2 = (new PdfPCell(Decl3));
            harry2.Border = 0;
            tabUni2.AddCell(harry2);
            documento.Add(tabUni2);

            

            PdfPTable Decl4 = new PdfPTable(1);
            Decl4.SetWidths(new float[] { 100 });
            Decl4.DefaultCell.Border = 0;
            Decl4.WidthPercentage = 50f;
            Decl4.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell SexClausul2 = (new PdfPCell(new Phrase("La tasa de interés se aplicará a los días efectivamente tranascurridos en el periodo de cómputo de intereses, y serán causados sobre la base establesida en el párrafo anterior, y su pago no podrá ser exigido por adelantado sino únicamente por periodos vencidos. \n Método de Calculo. \n (capital) x (Tasa Anual) / 100 = Intereses Ordinarios Anuales / (360) x (Días de atraso) + (IVA 16%x X (2) = Intereses Monetarios. \n \n  En caso de atrazo en los pagos que deberan realizarse ''" + nameCort + "'' cobrará a los ''" + NGrupo + "'', la cantidad de $100.00 (Cien pesos 00/100 MN) más IVA, por visita realizada a cada uno de los ''" + NGrupo + "'' por concepto de saldos vencidos. ''" + nameCort + "'' tendrpa derecho a cobrar a los ''" + NGrupo + "'' las cantidades que se establecen en este inciso cada vez que estos incurran en mora en cualquiera de los pagos que deban realizarse de conformidad a lo establecido en el presente instrumento. \n \n Durante la vigencia de este contrato ''" + nameCort + "'' no modificara las tasas de interés del crédito, ni las comisiones o gastis estipulados en el mismo. \n \nTodos los pagos que realizen los ''" + NGrupo + "'' serán en efectivo, mediante depósito en cuenta bancaria para lo cual deberán apegarse a lo indicado en la en la carta de instrucción de pagos que se adiciona al presente contrato como ''ANEXO 2''\n \n", fuente6)));
            SexClausul2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            SexClausul2.Border = 0;
            SexClausul2.VerticalAlignment = Element.ALIGN_CENTER;
            Decl4.AddCell(SexClausul2);

            PdfPCell SepClausul = (new PdfPCell(new Phrase("SEPTIMA. ESTADOS DE CUENTA Y CONSUTAS DE SALDOS, MOVIMIENTOS Y TRANSACCIONES. \n \n ''" + nameCort + "'' generará el estado de cuenta mensual al día hábil siguiente de la fecha de corte, mismo que estará a disposición de cliente en la oficina de ''" + nameCort + "'' que le corresponda a partir del tercer día hábil de la fecha de corte. Para que el estado de cuenta pueda ser entregado al cliente, este deberá presentar su credencial para votar expedida por el Instituto Nacional Electoral. Dicho estado de cuenta contendrá los pagos aplicados y conciliados al momento de la genreacion del reporrrte, debiendo considerar que los pagos de las amortizaciones podrán tardar hasta dos hasta tres días naturales para aplicarse, esto dependiendo del lugar donde los ''" + NGrupo + "'' haya efectuado el pago, La impresión o reimpresión del estado de cuenta será entregado si costo a los ''" + NGrupo + "'' por conducto de cualquier funcionario de la oficina de ''" + nameCort + "'' que les corresponda.\n \n", fuente6)));
            SepClausul.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            SepClausul.Border = 0;
            SepClausul.VerticalAlignment = Element.ALIGN_CENTER;
            Decl4.AddCell(SepClausul);

            PdfPCell OctClausul = (new PdfPCell(new Phrase("OCTAVA: PRELACIÓN DE PAGOS. Los pagos que realicen los ''" + NGrupo + "'' por orden de vencimiento se aplicarán de la siguiente manera: \n \na) Gastos de cobranza extrajudicial y judicial, seguros u otros conceptos contabilizados si los hay \nb) Impuesto al Valor Agregado sobre los intereses moratorios si los hay \nc) Intereses moratorios si los hay \nd)Impuestos al Valor Agregado sobre los intereses ordinarios\ne) Intereses ordinarios \nf) Capital\n \n", fuente6)));
            OctClausul.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            OctClausul.Border = 0;
            OctClausul.VerticalAlignment = Element.ALIGN_CENTER;
            Decl4.AddCell(OctClausul);

            PdfPTable Decl5 = new PdfPTable(1);
            Decl5.SetWidths(new float[] { 100 });
            Decl5.DefaultCell.Border = 0;
            Decl5.WidthPercentage = 50f;
            Decl5.HorizontalAlignment = Element.ALIGN_RIGHT;

            PdfPCell NovClausul = (new PdfPCell(new Phrase("NOVENTA PAGOS ANTICIPADOS. Los ''" + NGrupo + "'' podrán hacer pagos parciales o totales de forma anticipada, sin penalización o bonificaciones, siempre y cuando a) lo soliciten a ''" + nameCort + "'' por escrito, b) se encuentren al corriente en los pagos anexos  y c) el importedel pago anticipado sea por una cantidad igual o mayor al pago que deba realizarse en el periodo correspondiente. \n \nEn caso de que el pago anticipado  sea total, esto traerá como consecuencia la terminacion anticipada del contrato crédito el cual se apegara al procedimiento institucional correspondiente \n \n Si los ''" + NGrupo + "'' quieren realizar pagos anticipados parciales deberán presentar una solicitud por escrito en las oficinas de ''" + nameCort + "'' que correspoonda, con al menos tres días hábiles previo a su siguiente exibilidad.\n \nEl importe del pago anticipado parcial podrá ser por cualquier cantidad y será aplicado al saldo insoluto del principal. En este caso el pago será aplicado al saldo insoluto del principal, dependiendo el monto del pago calcular los intereses a devengar con base en el nuevo saldo insoluto. Una vez recibido el pago anticipado parcial ''" + nameCort + "'' entregará al cliente la tabla del amortizaciones actualizada.\n \n", fuente6)));
            NovClausul.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            NovClausul.Border = 0;
            NovClausul.VerticalAlignment = Element.ALIGN_CENTER;
            Decl5.AddCell(NovClausul);

            PdfPCell cap3 = (new PdfPCell(new Phrase("CAPITULO TERCERO\nDE LAS DISPOSICIONES GENERALES\n \n", fuente8)) { Colspan = 2 });
            cap3.HorizontalAlignment = Element.ALIGN_CENTER;
            cap3.Border = 0;
            cap3.VerticalAlignment = Element.ALIGN_CENTER;
            Decl5.AddCell(cap3);

            PdfPCell DecClausul = (new PdfPCell(new Phrase("DECIMA. RESTRICCIONES. Por ninguna causa o motivo los ''" + NGrupo + "'' deberán entregar dinero en efectivo, cheques, pagarés, bienes, valores al persona de ''" + nameCort + "''. En caso de que los ''" + NGrupo + "'' incumplan con esta restricción serán directamente responsables, por lo que ''ASER'' NO se hará responsable de la devolución o pago del dinero a los ''" + NGrupo + "''. \n \n DECIMA PRIMERA. CESION O TRASMISION DEL CRÉDITO. El crédito objero del presente contrato se condicionará al estricto cumplimiento de su aplicación por parte de los ''" + NGrupo + "'', en caso de que alguno de ellos en lo individual o en grupo desvien este favor de un tercero, sea este integrante o no del grupo, se tipificara como una conductora ilegal de prestanombres, o bien cuando el destino del crédito se aplique a un fin distinto al originamente manifestado, se tendrá po rescindido el contrato, configurandose a la causa para reclamar el pago total del crédito y denunciándose por tal la causa como delito de fraude ante el Ministerio Público con fundamento en I fracción III del artículo 230 del Código Penal para la Ciudad de México. \n \nAdemás, se hará la aplicación inmediata de la Garantía Líquida para amortizar como pago parcial a la deuda reclamada.\n \n ''" + nameCort + "'' queda facultado para ceder o negociar, aun antes del vencimiento ed este contrato, los derechos derivados del mismo ante un tercero. De igual forma, queda facultado para dndosar en los mismos términos los pagarés individuales y entregar cualquier documentación relativa al crédito que se haya suscrito con los ''" + NGrupo + "''.", fuente6)));
            DecClausul.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            DecClausul.Border = 0;
            DecClausul.VerticalAlignment = Element.ALIGN_CENTER;
            Decl5.AddCell(DecClausul);

            documento.NewPage();
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));


            PdfPTable tabUni3 = new PdfPTable(2);
            tabUni3.SetWidths(new float[] { 50, 50 });
            tabUni3.DefaultCell.Border = 0;
            tabUni3.WidthPercentage = 100f;
            tabUni3.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell potter1 = (new PdfPCell(Decl4));
            potter1.Border = 0;
            tabUni3.AddCell(potter1);

            PdfPCell potter2 = (new PdfPCell(Decl5));
            potter2.Border = 0;
            tabUni3.AddCell(potter2);
            documento.Add(tabUni3);

           

            PdfPTable Decl6 = new PdfPTable(2);
            Decl6.SetWidths(new float[] { 7, 93 });
            Decl6.DefaultCell.Border = 0;
            Decl6.WidthPercentage = 50f;
            Decl6.HorizontalAlignment = Element.ALIGN_RIGHT;

            PdfPCell DecClausul1 = (new PdfPCell(new Phrase("Los ''" + NGrupo + "'' autorizan a ''" + nameCort + "''  para que, en cualquiero caso, previsto en esta cláusula, aún prewvio a la transmisión o gravenmente correspondiente, pueda transferir toda la información de datos personales y patrimoniales-financieros manifestados por los ''" + NGrupo + "'' en el expediente de solicitud de crédito al posible acreedor o cesionario, a solicitud expresa de este último.\n \n", fuente6)) { Colspan = 2 });
            DecClausul1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            DecClausul1.Border = 0;
            DecClausul1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(DecClausul1);

            PdfPCell DecClausul2 = (new PdfPCell(new Phrase("DECIMA SEGUNDA. TERMINACIÓN ANTICIPADA. Los ''" + NGrupo + "'' y ''" + nameCort + "'' podrán dar por terminado anticipadamente el presente contrato por las causales siguientes:\n \n1.   Los ''" + NGrupo + "'' decidan liquidar anticipadamente el total del crédito. Para tal efecto deberán solicitar la terminación anticipada del contrato presentando una solicitud por escrito en las oficinas de ''" + nameCort + "'' que les corresponda con al menos dos días de anticipación a la siguiente exigibilidad.\n''" + nameCort + "'' dará por terminado en contrato al tercer hábil siguiente a aquiel en que se reciba la solicitud de terminación anticipada, siempore y cuando no existan adeudo, entregando al cliente como comprobante de terminación, estado de cuenta o documento constancia de la relación contractual y el pagaré individual debidamente cancelado. ambos documentos serán prueba suficiente de la terminación y de la inexistencia de adeudos. \n \nEn caso de que los ''" + NGrupo + "'' tengan adeudos pendientes ''" + nameCort + "'' les comunicará el importe deudor en los siguientes tres días hábilesa la recepción de la solicitud determinación anticipada para que los ''" + NGrupo + "'' realicen el pago  correspondiente. Una vez que el pago quede aplicado aplicado se dará por terminado el contrato. ''" + nameCort + "'' dispondrá por diez días hábiles para entregar a los ''" + NGrupo + "'' los documentos sealados en el párrafo anterior. \n \nSi llegara a existir un saldo a favor de los ''" + NGrupo + "'' y se dé la terminación anticipada, ''" + nameCort + "'' informará a los mismos, a través de sus promotores de crédito para que acudan a la sucursal que les corresponda para tramitar su devolución.\n \n2.  Sin necesidad de declaración judicial previa''" + nameCort + "'' dará por terminado anticipadamente el plazo de crédito y sus accesorios, cuando se presente alguno de los siguientes casos: \n \n ", fuente6)) { Colspan = 2 });
            DecClausul2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            DecClausul2.Border = 0;
            DecClausul2.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(DecClausul2);

            

            PdfPCell decla = (new PdfPCell(new Phrase("a)", fuente6)));
            decla.HorizontalAlignment = Element.ALIGN_CENTER;
            decla.Border = 0;
            decla.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(decla);

            PdfPCell decla1 = (new PdfPCell(new Phrase("Si los ''" + NGrupo + "'' dejan de cubrir puntualmente cualquier obligación de pago derivadas del presente contrato", fuente6)));
            decla1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            decla1.Border = 0;
            decla1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(decla1);

            PdfPCell declb = (new PdfPCell(new Phrase("b)", fuente6)));
            declb.HorizontalAlignment = Element.ALIGN_CENTER;
            declb.Border = 0;
            declb.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(declb);

            PdfPCell declb1 = (new PdfPCell(new Phrase("Si los ''" + NGrupo + "'' incumplen cualquiera de las obligaciones contenidas en este instrumento cualquiera de las obligaciones contenidas en este instrumento .", fuente6)));
            declb1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            declb1.Border = 0;
            declb1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(declb1);

            PdfPCell declc = (new PdfPCell(new Phrase("c)", fuente6)));
            declc.HorizontalAlignment = Element.ALIGN_CENTER;
            declc.Border = 0;
            declc.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(declc);

            PdfPCell declc1 = (new PdfPCell(new Phrase("Si los ''" + NGrupo + "'' transmiten en cualquier forma sus derechos y obligaciones.", fuente6)));
            declc1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            declc1.Border = 0;
            declc1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(declc1);

            PdfPCell decld = (new PdfPCell(new Phrase("d)", fuente6)));
            decld.HorizontalAlignment = Element.ALIGN_CENTER;
            decld.Border = 0;
            decld.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(decld);

            PdfPCell decld1 = (new PdfPCell(new Phrase("Si se comprobará falsedad en la información proporcionada por los ''" + NGrupo + "''  en la solicitud de crédito o si al entregarle el monto del crédito, estese otorga o utiliza por algún tercero ajeno a la relación crediticia o se destina a un fin distinto al establecido en la solicitud de crédito.", fuente6)));
            decld1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            decld1.Border = 0;
            decld1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(decld1);

            PdfPCell decle = (new PdfPCell(new Phrase("e)", fuente6)));
            decle.HorizontalAlignment = Element.ALIGN_CENTER;
            decle.Border = 0;
            decle.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(decle);

            PdfPCell decle1 = (new PdfPCell(new Phrase("En caso de cualquera de los ''" + NGrupo + "'' contrate créditos o cualquier tipo de adeudos con posterioridad a la firma de presente instrumento.", fuente6)));
            decle1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            decle1.Border = 0;
            decle1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(decle1);

            PdfPCell declf = (new PdfPCell(new Phrase("f)", fuente6)));
            declf.HorizontalAlignment = Element.ALIGN_CENTER;
            declf.Border = 0;
            declf.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(declf);

            PdfPCell declf1 = (new PdfPCell(new Phrase("Si existe jucio en contra de cualquiera de los ''" + NGrupo + "'', en donde se ordene judicialmente embargo en su contra y que de manera directae indirecta afecte el buen funcionamiento de su actividad económica.", fuente6)));
            declf1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            declf1.Border = 0;
            declf1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(declf1);

            PdfPCell declg = (new PdfPCell(new Phrase("g)", fuente6)));
            declg.HorizontalAlignment = Element.ALIGN_CENTER;
            declg.Border = 0;
            declg.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(declg);

            PdfPCell declg1 = (new PdfPCell(new Phrase("Si ''" + NGrupo + "'' no cumplieran con el pago de sus obligaciones con el físico, menos de que se tenga causa legitima para ello.", fuente6)));
            declg1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            declg1.Border = 0;
            declg1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl6.AddCell(declg1);

            PdfPTable Decl8 = new PdfPTable(1);
            Decl8.SetWidths(new float[] { 100 });
            Decl8.DefaultCell.Border = 0;
            Decl8.WidthPercentage = 50f;
            Decl8.HorizontalAlignment = Element.ALIGN_RIGHT;

            PdfPCell DecClausul3 = (new PdfPCell(new Phrase("DECIMA TERCERA. PAGARE. los ''" + NGrupo + "'' suscribirán un pagaré a la orden de ''" + nameCort + "'' que documente la disposición del crédito mismo que inclúirauna tabla de amortización que contendrá los montos y las dechas de vencimieto de cada uno de los pagos parciales consecutivos que les correspondan a los ''" + NGrupo + "''. Este título de crédito se considerá pagadero a la vistay en caso de cumplimiento puntual por parte de los ''" + NGrupo + "'' será cancelado. Una vez liquidado en su totalidad los impuestos, comisiones, intereses sobre saldos onsolutos, gastos y principal del monto del crédito, ''" + nameCort + "'' devolverá el pagaré deberpa ser suscrito por los obligados solidarios en su carácter del avales. \n \nEn caso de que los ''" + NGrupo + "'' u obligados solidarios se negaran a suscribir el título de crédito señalado, ''ASER'' estará facultado para negarles el crédito.\n \n", fuente6)));
            DecClausul3.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            DecClausul3.Border = 0;
            DecClausul3.VerticalAlignment = Element.ALIGN_CENTER;
            Decl8.AddCell(DecClausul3);

            PdfPCell DecClausul4 = (new PdfPCell(new Phrase("DECIMA CUARTA. OBLIGACIÓN SOLIDARIA. Sin perjuicio de lo estipulado en la cláusula tercera y cuarta del contrato, cada uno de los ''" + NGrupo + "'' se constituye en obligadosolidario de los demás ''" + NGrupo + "'' que celebran este contrato, obligándose tanto para el cumplimiento exacto, completo y en timepo de los compromisos señalados en el presente instrumento, así comopara el pago puntual del principaldel crédito, intereses, comisiones y de cualquier otra cantidad que deba pagarse a ''" + nameCort + "'' incluyendo accesorios y en caso de juicio, los gastos y costes que determine la autoridad jurisdiccional.", fuente6)));
            DecClausul4.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            DecClausul4.Border = 0;
            DecClausul4.VerticalAlignment = Element.ALIGN_CENTER;
            Decl8.AddCell(DecClausul4);

            PdfPCell DecClausul5 = (new PdfPCell(new Phrase("DECIMA QUINTA. SUPLENCIA EN CASO DE NO SABER LEER Y/O ESCRIBIR. En caso de que alguno(s) de ''" + NGrupo + "'' no sepa leer y/o escribir, deberá firmar otra persona a su ruego este contrato y los pagarés que se deriven de la disposición de crédito agregando la leyenda ''a ruego y encargo de'' señalando el nombre del acreditado seguido por el nombre y la firma de la otra persona \nIndependientemente de lo anterior el ''" + NGrupo + "'' que no sepa leer y/o escribir deberá estampar su huella digital. \n \nEn este caso, los ''" + NGrupo + "'' aceptan y acuerdan que la firma hecha por encargo y la impresión  de la hueya digital del acreditado, sufrirá los mismos efectos legales que si este hubiese estampadola firma personalmente, y por lo tanto, estos asumen la responsabilidad de enterar y explicar a Los ''" + NGrupo + "'' que no sepan leer y/o escribir respecto de todos y cada uno de los derechos y obligaciones que se contraes con motivo del presente contrato.", fuente6)));
            DecClausul5.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            DecClausul5.Border = 0;
            DecClausul5.VerticalAlignment = Element.ALIGN_CENTER;
            Decl8.AddCell(DecClausul5);

            PdfPCell DecClausul6 = (new PdfPCell(new Phrase("DECIMA SEXTA. AUTORIZACIONES. Los ''" + NGrupo + "'' autorizan a ''" + nameCort + "'' para que:\n \na)  Proporcione la información que se estime necesario a quien presente los servicios operativos y trámite de crédito.\n \nb)  Suministra y recabe información sobre operaciones crediticias y ptras de naturaleza análogas que los ''" + NGrupo + "'' hayan celebrado con ''" + nameCort + "'' y con información fincanciera debidamente autoriazas.\n \nc)  ''" + nameCort + "'' y/o cualquier institución o persona que esta designe realice visitas a su domicilio o lugar donde se encuentra ubicado su negocioa efecto de verificar y validar la actividad económica registrada en la solicitud crédito.\n \nd)  Llame o envíe mensajes a su domicilio o teléfono celula, o de cualquier forma y en cualquier lugar contactarlo para informale de los servicios y productos que ''" + nameCort + "'' ofrece.", fuente6)));
            DecClausul6.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            DecClausul6.Border = 0;
            DecClausul6.VerticalAlignment = Element.ALIGN_CENTER;
            Decl8.AddCell(DecClausul6);



            documento.NewPage();
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPTable tabUni4 = new PdfPTable(2);
            tabUni4.SetWidths(new float[] { 50, 50 });
            tabUni4.DefaultCell.Border = 0;
            tabUni4.WidthPercentage = 100f;
            tabUni4.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell vans1 = (new PdfPCell(Decl6));
            vans1.Border = 0;
            tabUni4.AddCell(vans1);

            PdfPCell vans2 = (new PdfPCell(Decl8));
            vans2.Border = 0;
            tabUni4.AddCell(vans2);
            documento.Add(tabUni4);

            documento.NewPage();
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPTable Decl9 = new PdfPTable(1);
            Decl9.SetWidths(new float[] { 100 });
            Decl9.DefaultCell.Border = 0;
            Decl9.WidthPercentage = 50f;
            Decl9.HorizontalAlignment = Element.ALIGN_RIGHT;

            PdfPCell DecClausul7 = (new PdfPCell(new Phrase("DECIMA SEPTA. GARANTIA LIQUIDA. En el acto de la firma del presente contrato, cada uno de los ''" + NGrupo + "'' constituyen Garantia Líquida en la cuenta que para tal efecto sañel ''" + nameCort + "'', cuyo monto no podrá ser menor al 10% del crédito otorgado. \n \nLa Garantia está devuelta a los ''" + NGrupo + "'' cuando estos hayan cumplido con las obligaciones estipuladas en el presente contrato. En caso de incumplimiento, ''" + nameCort + "'' estará facultada para que al término del plazo del crédito aplique las garantias a las obligaciones pecuniarias no cumplidas en tiempo por parte de los ''" + NGrupo + "''.\n \n", fuente6)));
            DecClausul7.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            DecClausul7.Border = 0;
            DecClausul7.VerticalAlignment = Element.ALIGN_CENTER;
            Decl9.AddCell(DecClausul7);

            PdfPCell DecClausul8 = (new PdfPCell(new Phrase("DECIMA OCTAVA. LIBERACION DE OBLIGACIONES EN CASO DE FALLECIMIENTO. En caso de que alguno de los ''" + NGrupo + "'' fallezca antes del plazo de vencimiento del crédito, ''" + nameCort + "'' liberará al ''" + NGrupo + "'' fallecido y a los ''" + NGrupo + "'' en su carácter del finado, siempre y cuando se encuentren al corriente en todas las obligaciones contenidas en este contrato y acrediten fehacientemente el fallecimeinto con la documentación que para tal efecto sea requerida por ''" + nameCort + "''.\n \n", fuente6)));
            DecClausul8.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            DecClausul8.Border = 0;
            DecClausul8.VerticalAlignment = Element.ALIGN_CENTER;
            Decl9.AddCell(DecClausul8);

            PdfPCell DecClausul9 = (new PdfPCell(new Phrase("DECIMA NOVENA. TITULO EJECUTIVO. El presente contrato junto con la certificación del contador de ''" + nameCort + "'' respecto del estado que guarde la cuenta, será título ejecutico, de conformidad con el artículo 68 de la Ley de Institucipnes de Crédito, por lo que ''" + nameCort + "'' estará facultado en caso de incumplimiento o vencimiento anticipado a demandar en la vía ejecutica mercantil o por la vía judicial que más le convenga.\n \n", fuente6)));
            DecClausul9.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            DecClausul9.Border = 0;
            DecClausul9.VerticalAlignment = Element.ALIGN_CENTER;
            Decl9.AddCell(DecClausul9);

            PdfPCell VigClausul = (new PdfPCell(new Phrase("VIESIMA. GASTOS Y COSTES. En el supuesto de que ''" + nameCort + "'' demandará a los ''" + NGrupo + "'' por incumplimiento los gastos y costes del procedimiento legal correrán a cargo de los mismos. \n \n", fuente6)));
            VigClausul.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            VigClausul.Border = 0;
            VigClausul.VerticalAlignment = Element.ALIGN_CENTER;
            Decl9.AddCell(VigClausul);

            PdfPCell VigClausul1 = (new PdfPCell(new Phrase("VIGESIMA PRIMERA. MODIFICACIÓN DEL CONTRATO. ''" + nameCort + "'' avisará a cliente de cualquier modificación al presente contrato por medio de su promotor de crédito y mediante la disposición pública en sus sucursales con treinta días naturales de anticipación a su modificación. Los ''" + NGrupo + "'' podrán dar pr terminada la relación contractual en caso de no estar de acuerdo con las modificaciones efectuada con treinta días naturales posteriores al aviso de notificación siempre y cuando los ''" + NGrupo + "'' hayan liquidado totalmente el crédito, intereses, comisiones y demás gastos que se generen en virtud del presente contrato, sin generarle algún gasto adicional.\n \n", fuente6)));
            VigClausul1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            VigClausul1.Border = 0;
            VigClausul1.VerticalAlignment = Element.ALIGN_CENTER;
            Decl9.AddCell(VigClausul1);

            PdfPCell VigClausul2 = (new PdfPCell(new Phrase("VIGESIMA SEGUNDA. UNIDAD ESPECIALIZADA. En caso de que los ''" + NGrupo + "'' deseen realizar alguna solicitu, consulta, aclaración, inconformidad y quejas relacionadas con el servicio y operación que instrumenta a través del presente contrato, tendrá expendio su derecho para acudir directamente a la Unidad Especializada de Atención a Usuarios de ''" + nameCort + "'', la cual será la sucursal de ''" + nameCort + "'' que sirva de enlace con el cliente; vi teléfonica al "+telEmpresa+", por correspondencia o correo electrónico a la siguiente dirección "+correoEmp+", o bien directamente ante la CONDUSEF en el centro de atención a usuarios de la entidad o en los teléfonos 53400999 y lada sin costo 01-800-999 8080 o en el correo electrónico opinión@condusef.gob.mx o en sitio https://www.condusef.gob.mx \n \nEl procedimiento de atención a una solicitud será el siguiente:\n \nEl horario de atención de la Unidad Especializada es de 90:00 a 14:00 horas lo días lunes a viernes de cada semana, excepto los días señalados como inhabiles por la CONDUSEF mediante os acuerdos que publique en el Diario Oficial de la Federación (DOF)\n \nLa unidad deberá responder por escrito al acreditado que haya presentado la aclaración, inconfomidad o queja ha sido rechazada, quedando expendido el derecho de los ''" + NGrupo + "'' para interponer los procedimientos precistos en la Ley de Protección y Defensa del Usuario de Servicios financieros.\n \n", fuente6)));
            VigClausul2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            VigClausul2.Border = 0;
            VigClausul2.VerticalAlignment = Element.ALIGN_CENTER;
            Decl9.AddCell(VigClausul2);

            PdfPTable Decl10 = new PdfPTable(1);
            Decl10.SetWidths(new float[] { 100 });
            Decl10.DefaultCell.Border = 0;
            Decl10.WidthPercentage = 50f;
            Decl10.HorizontalAlignment = Element.ALIGN_RIGHT;

            PdfPCell VigClausul22 = (new PdfPCell(new Phrase("La presentación de reclamaciones ante la Unidad Especializadas suspenderá la prescripción de las acciones administrativas a que pudiera dar lugar, no así respecto de las acciones de naturaleza civil o mercantil. \n \nLas reclamaciones que resulten procedentes se rectificaran a la brevedad. Notificando dicha circunstancia a los ''" + NGrupo + "'' quienes podrán verificar la aclaración en el estado de cuenta inmediato posterior.\n \nLa Unidad Especializada servirá  de enlace para atender inconformidades, aclaraciones y quejas que se presenten ante la CONDUSEF, en los términos establecidos en la Ley de Protección y Defensa del Usuario de Servicios Finacieros, la cual se puede consuktar en la página web: https://www.condusef.gob.mx \n \n", fuente6)));
            VigClausul22.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            VigClausul22.Border = 0;
            VigClausul22.VerticalAlignment = Element.ALIGN_CENTER;
            Decl10.AddCell(VigClausul22);

            PdfPCell VigClausul3 = (new PdfPCell(new Phrase("VIGESIMA TERCERA. DOMICILIOS. Las partes señalan como sus domicilios para oír y recibir todo tipo de notificaciones, documentos y valores relacionados con este contrato los que se indicanen el apartado de declaraciones. En caso de que cualquiera de las partes cambie de domicilio avisar por escrito a la otra parte, de lo contrario las notificaciones judiciales y extrajudiciales y extrajudiciales continuaran surtiendo sus efectos y serán legalmente válidas si se realizan en las direcciones declaradas. \n \n", fuente6)));
            VigClausul3.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            VigClausul3.Border = 0;
            VigClausul3.VerticalAlignment = Element.ALIGN_CENTER;
            Decl10.AddCell(VigClausul3);

            PdfPCell VigClausul4 = (new PdfPCell(new Phrase("VIGESIMA CUARTA. INTEGRIDAD DEL CONTRATO. Este contrato, su caratula y sus anexos constituyen el acuerdo íntegro entre las partes y dejan sin efecto cualquier acuedo oral o escrito anterior, y los enunciados en cada clausula son únicamente referencia. En caso de que una clausula o parte de la misma sea objeto de anulación por un órgano juridiccional, el resto del contrato permanecerá con plen efecto y fuerza legal.\n \n", fuente6)));
            VigClausul4.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            VigClausul4.Border = 0;
            VigClausul4.VerticalAlignment = Element.ALIGN_CENTER;
            Decl10.AddCell(VigClausul4);

            PdfPCell VigClausul5 = (new PdfPCell(new Phrase("VIGESIMA QUNTA. VIGILANCIA. Cualquier ebtudad nacional e internacional que haya intervenido en el financiamiento tendrá la facultad durante todo el tiempo de vigencia del crédito de designar a un supervisor que cuide el exacto cumplimiento de las obligaciones de los ''" + NGrupo + "'' principalmente en lo que se refiere a la vigilancia de la inversión de fondos, de debido funcionamiento de ''" + nameCort + "'' y del cuidado y conservación de las garantías otorgadas. El suelo y los gastos que ''" + nameCort + "'' autorice al supervisor serán cubiertos por los ''ACEDITADOS'', para lo cual este expresa su consetimiento.\n \n", fuente6)));
            VigClausul5.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            VigClausul5.Border = 0;
            VigClausul5.VerticalAlignment = Element.ALIGN_CENTER;
            Decl10.AddCell(VigClausul5);

            PdfPCell VigClausul6 = (new PdfPCell(new Phrase("VIGESIMA SEXTA. JURISDICCIÓN Y COMPETENCIA. Para el cumplimiento e interpretación del presente contrato, las partes se someten expresamente a la legislación mercantil vigente de la República Mexicana y a la elección de ''" + nameCort + "'' en a jurisdicción de los tribunales competentes de la Ciudad de México, o de la Ciudad donde se firme el presente contrato renunciando expresamente a cualquier fuero que por razón de sus domicilios presentes o futuros pudiera corresponderles.\n \n", fuente6)));
            VigClausul6.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            VigClausul6.Border = 0;
            VigClausul6.VerticalAlignment = Element.ALIGN_CENTER;
            Decl10.AddCell(VigClausul6);

            PdfPCell VigClausul7 = (new PdfPCell(new Phrase("VIGESIMA SEPTIMA. El procedimiento de modificación del aviso de privacidad se manejará y notificará a los ''" + NGrupo + "'' a través del portal de internet https://www.asernegocios.com Con apego a lo previsto por la Ley de Federal de Protección de Datos Personales en Posesión de Particulares y su reglamento. Asimismo, se hace del conocimiento a los ''" + NGrupo + "'' que los datos personales no serán transferidos a terceros para fines distintos a los necesarios en el procedimiento de otorgamiento del crédio, promoción y pfrecimiento del otroso produtos financieros. \n \nPor lo anterior  los ''" + NGrupo + "'' manifiestan que quedan enterados voluntariamente del contenido de este aciso, de sus alcances legales y con fundamento en lo dispuesto por la Ley de Federal de Protección de Datos Personales en Poseción de Particulares y su reglamento; de igual forma por esste medio otorgan de manera voluntaria el más amplio consetimiento consentimiento y facultad a '' " + nameCort + "'' para que utilice los datos personales de cada uno de los ''" + NGrupo + "''.", fuente6)));
            VigClausul7.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            VigClausul7.Border = 0;
            VigClausul7.VerticalAlignment = Element.ALIGN_CENTER;
            Decl10.AddCell(VigClausul7);

            PdfPTable tabUni5 = new PdfPTable(2);
            tabUni5.SetWidths(new float[] { 50, 50 });
            tabUni5.DefaultCell.Border = 0;
            tabUni5.WidthPercentage = 100f;
            tabUni5.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell sam1 = (new PdfPCell(Decl9));
            sam1.Border = 0;
            tabUni5.AddCell(sam1);

            PdfPCell sam2 = (new PdfPCell(Decl10));
            sam2.Border = 0;
            tabUni5.AddCell(sam2);
            documento.Add(tabUni5);


            documento.NewPage();
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPTable final1 = new PdfPTable(1);
            final1.SetWidths(new float[] { 100 });
            final1.DefaultCell.Border = 0;
            final1.WidthPercentage = 100f;
            final1.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell firmaPri = (new PdfPCell(new Phrase("POR ''"+nameEmp+"'' \n \n PRESENTADA POR:\n \n \n \n \n \n \n \n \n \n ____________________________________________\n  "+represen+" \n \n \n \n POR LOS ''" + NGrupo + "''", fuente8)));
            firmaPri.HorizontalAlignment = Element.ALIGN_CENTER;
            firmaPri.Border = 0;
            firmaPri.VerticalAlignment = Element.ALIGN_CENTER;
            final1.AddCell(firmaPri);

            documento.Add(final1);

            PdfPTable final2 = new PdfPTable(2);
            final2.SetWidths(new float[] { 50, 50 });
            final2.DefaultCell.Border = 0;
            final2.WidthPercentage = 100f;
            final2.HorizontalAlignment = Element.ALIGN_CENTER;

            infor.datosClientes();
            string nombresFri = "";
            string nacionalidad = "";
            string estadoCivil = "";
            string giro = "";
            string calle = "";
            string numero = "";
            string colonia = "";
            string cp = "";
            string delegacion = "";
            string estado = "";
            string ife = "";

            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    nombresFri = Convert.ToString(r[0]);
                    nacionalidad = Convert.ToString(r[1]);
                    estadoCivil = Convert.ToString(r[2]);
                    giro = Convert.ToString(r[3]);
                    calle = Convert.ToString(r[4]);
                    numero = Convert.ToString(r[5]);
                    colonia = Convert.ToString(r[6]);
                    cp = Convert.ToString(r[7]);
                    delegacion = Convert.ToString(r[8]);
                    estado = Convert.ToString(r[9]);
                    ife = Convert.ToString(r[10]);

                    PdfPCell firmAcr = (new PdfPCell(new Phrase("\n \n \n ________________________________________\n " + nombresFri + " \nDeclara ser " + nacionalidad + "(A), mayor de edad, estado civil:"+estadoCivil+ " con actividad economica: "+giro+" y tener su domicilio particular: "+calle+" "+numero+" "+colonia+" C.P. "+cp+" "+delegacion+", "+estado+". Identificandose con Credencial de Elector (INE): "+ife+", la cual se anexa copia fotostática a este contrato", fuente8)));
                    firmAcr.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmAcr.Border = 0;
                    firmAcr.VerticalAlignment = Element.ALIGN_CENTER;
                    final2.AddCell(firmAcr);
                }
            }

           
            

            documento.Add(final2);




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