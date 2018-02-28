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


public partial class ControlPagos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        int[] sesiones = obtieneSesiones();
        CtrlPag obt = new CtrlPag();
        obt.empresa = sesiones[2];
        obt.sucursal = sesiones[3];
        obt.credito = sesiones[4];
        obt.obtenerSaldoActual();

        decimal montoAut = 0;
        float tasa = 0;
        float inte = 0;
        decimal saldoA = 0;
        decimal saldoB = 0;
        int cliente = 0;

        if (Convert.ToBoolean(obt.retorno[0]))
        {
            DataSet ds = (DataSet)obt.retorno[1];


            foreach (DataRow r in ds.Tables[0].Rows)
            {
                cliente = Convert.ToInt32(r[0]);
                montoAut = Convert.ToDecimal(r[2]);
                tasa = Convert.ToInt32(r[4]);
                inte = tasa / 100;
                saldoA = montoAut * Convert.ToDecimal( inte);
                saldoB = montoAut + saldoA;

                obt.idcliente = cliente;
                obt.saldo_actual = Convert.ToInt32( saldoB);
                obt.actualizaSaldo();
              
            }
        }



        obt.obtieneInfoEnca();
        int plazo = 0;
        if (Convert.ToBoolean(obt.retorno[0]))
        {
            DataSet ds = (DataSet)obt.retorno[1];


            foreach (DataRow r in ds.Tables[0].Rows)
            {

                plazo = Convert.ToInt32( r[0]);
                txtplazo.Text = plazo.ToString();
            }
        }
        if (plazo == 16)
        {
            cmbSemanas.Visible = true;
            cmbsemanas32.Visible = false;
            cmbSemanas20.Visible = false;
            cmbsemanas64.Visible = false;
            txt_sem.Text = cmbSemanas.SelectedValue;
        }
        if (plazo == 20)
        {
            cmbSemanas.Visible = false;
            cmbsemanas32.Visible = false;
            cmbSemanas20.Visible = true;
            cmbsemanas64.Visible = false;
            txt_sem.Text = cmbSemanas20.SelectedValue;
        }
        else if (plazo == 32)
        {
            cmbSemanas.Visible = false;
            cmbsemanas32.Visible = true;
            cmbSemanas20.Visible = false;
            cmbsemanas64.Visible = false;
            txt_sem.Text = cmbsemanas32.SelectedValue;
        }
        else
        {
            cmbSemanas.Visible = false;
            cmbsemanas32.Visible = false;
            cmbSemanas20.Visible = false;
            cmbsemanas64.Visible = true;
            txt_sem.Text = cmbsemanas64.SelectedValue;
        }
    }
    protected void RadGrid2_ItemUpdated(object sender, GridUpdatedEventArgs e)
    {
       
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

    protected void cmbSemanas_TextChanged(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        CtrlPag obt = new CtrlPag();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        int credito = sesiones[4];
        int pago = Convert.ToInt32(cmbSemanas.SelectedValue);
        int pagonew = pago - 1;
        obt.empresa = empresa;
        obt.sucursal = sucursal;
        obt.credito = credito;
        obt.pago = pagonew;
        obt.obtenerSaldos();

        int cliente = 0;
        decimal ahorro;
        decimal saldo_actual;
        decimal pago2;
        decimal newsaldo = 0;
        decimal ap = 0;
        decimal dev = 0;


        if (Convert.ToBoolean(obt.retorno[0]))
        {
            DataSet ds = (DataSet)obt.retorno[1];


            foreach (DataRow r in ds.Tables[0].Rows)
            {
                cliente = Convert.ToInt32(r[0]);
                saldo_actual = Convert.ToDecimal(r[1]);
                pago2 = Convert.ToDecimal(r[2]);
                ahorro = Convert.ToDecimal(r[3]);
                ap = Convert.ToDecimal(r[4]);


                newsaldo = saldo_actual - (pago2 + ahorro);


                obt.idcliente = cliente;
                obt.saldo_actual = Convert.ToInt32(newsaldo);
                obt.pago = pago;
                obt.actualizaSaldo2();

            }
        }

           
        RadGrid2.DataBind();
        cmbSemanas.DataBind();
                txt_sem.Text = pago.ToString();

        SqlDataSource2.SelectCommand = "select  d.id_cliente,d.nombre_cliente,d.credito_autorizado*.10 as Gl,d.credito_autorizado,o.pagosemanal,o.saldo_actual,o.fecha_pago,o.fecha_aplicacion,o.no_pago,o.monto_Pago,o.monto_Ahorro,o.ap,o.dev  from AN_Solicitud_Credito_Detalle d left join AN_Operacion_Credito o on d.id_cliente = o.id_cliente where o.id_empresa =" + empresa+" AND o.id_sucursal ="+sucursal+" and o.id_grupo="+credito+" and no_pago="+pago;
        SqlDataSource2.DataBind();
    }

    protected void lnkImprimirControlPagoSemanal_Click(object sender, EventArgs e)
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
        documento.AddTitle("CONTROL DE PAGOS");
        documento.AddCreator("DESARROLLARTE");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\Cotrol_Pagos_Por_Semana" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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


            PdfPCell titulo = new PdfPCell(new Phrase("APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + "CONTROL DE PAGO Y AHORRO SEMANAL", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

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
            encab.SetWidths(new float[] { 10, 20, 9, 9, 9, 9, 9, 9, 8, 8 });
            encab.DefaultCell.Border = 0;
            encab.WidthPercentage = 100f;

            ConPS infor = new ConPS();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = sesiones[4];
            infor.obtieneInfoEncabezado();

            string grupopro = "";
            int numGrupo = 0;
            int numeCre = 0;
            int ciclog = 0;
            string fechaini = "";
            DateTime fechafin;
            string ofec = "";
            int plazo = 0;
            int plazosum = 0;
            DateTime sumafecha;

            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    grupopro = Convert.ToString(r[6]);
                    numGrupo = Convert.ToInt32(r[7]);
                    numeCre = Convert.ToInt32(r[2]);
                    ciclog = Convert.ToInt32(r[17]);
                    fechaini = Convert.ToString(r[5]);
                    plazo = Convert.ToInt32(r[14]);
                    plazosum = plazo * 7;
                    fechafin = Convert.ToDateTime(fechaini);
                    fechafin = fechafin.AddDays(plazo);
                    ofec = Convert.ToString(fechafin);
                }
            }

            PdfPCell gruPro = new PdfPCell(new Phrase("GRUPO PRODUCTIVO:", fuente6));
            gruPro.HorizontalAlignment = Element.ALIGN_CENTER;
            gruPro.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab.AddCell(gruPro);

            PdfPCell gruPro1 = new PdfPCell(new Phrase(" " + grupopro, fuente6));
            gruPro1.HorizontalAlignment = Element.ALIGN_CENTER;
            encab.AddCell(gruPro1);

            PdfPCell noGru = new PdfPCell(new Phrase("NUM. DE GRUPO:", fuente6));
            noGru.HorizontalAlignment = Element.ALIGN_CENTER;
            noGru.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab.AddCell(noGru);

            PdfPCell noGru1 = new PdfPCell(new Phrase(" " + numGrupo, fuente6));
            noGru1.HorizontalAlignment = Element.ALIGN_CENTER;
            encab.AddCell(noGru1);

            PdfPCell numCre = new PdfPCell(new Phrase("NUM. DE CRÉDITO:", fuente6));
            numCre.HorizontalAlignment = Element.ALIGN_CENTER;
            numCre.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab.AddCell(numCre);

            PdfPCell numCre1 = new PdfPCell(new Phrase(" " + numeCre, fuente6));
            numCre1.HorizontalAlignment = Element.ALIGN_CENTER;
            encab.AddCell(numCre1);

            PdfPCell ciclo = new PdfPCell(new Phrase("CICLO:", fuente6));
            ciclo.HorizontalAlignment = Element.ALIGN_CENTER;
            ciclo.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab.AddCell(ciclo);

            PdfPCell ciclo1 = new PdfPCell(new Phrase(" " + ciclog, fuente6));
            ciclo1.HorizontalAlignment = Element.ALIGN_CENTER;
            encab.AddCell(ciclo1);
            documento.Add(encab);

            PdfPCell sucur = new PdfPCell(new Phrase("CICLO:", fuente6));
            sucur.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab.AddCell(sucur);

            PdfPCell sucur1 = new PdfPCell(new Phrase(" " + ciclog, fuente6));
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

            PdfPTable tab = new PdfPTable(13);
            tab.SetWidths(new float[] { 2, 25, 5, 5, 7, 7, 7, 7, 7, 7, 7, 7, 7 });
            tab.DefaultCell.Border = 0;
            tab.WidthPercentage = 100f;
            tab.HorizontalAlignment = Element.ALIGN_LEFT;


            PdfPCell no = (new PdfPCell(new Phrase("No.", fuente6)) { Rowspan = 3 });
            no.HorizontalAlignment = Element.ALIGN_CENTER;
            no.BackgroundColor = BaseColor.LIGHT_GRAY;
            no.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(no);

            PdfPCell nom = (new PdfPCell(new Phrase("NOMBRE DEL CLIENTE", fuente6)) { Rowspan = 3 });
            nom.HorizontalAlignment = Element.ALIGN_CENTER;
            nom.BackgroundColor = BaseColor.LIGHT_GRAY;
            nom.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(nom);

            PdfPCell gl = (new PdfPCell(new Phrase("GL", fuente6)) { Rowspan = 3 });
            gl.HorizontalAlignment = Element.ALIGN_CENTER;
            gl.BackgroundColor = BaseColor.LIGHT_GRAY;
            gl.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(gl);

            PdfPCell sald = (new PdfPCell(new Phrase("SALDO TOTAL", fuente6)) { Rowspan = 3 });
            sald.HorizontalAlignment = Element.ALIGN_CENTER;
            sald.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(sald);

            PdfPCell pagSem = (new PdfPCell(new Phrase("PAGO SEMANAL", fuente6)) { Rowspan = 3 });
            pagSem.HorizontalAlignment = Element.ALIGN_CENTER;
            pagSem.BackgroundColor = BaseColor.LIGHT_GRAY;
            pagSem.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab.AddCell(pagSem);

            PdfPCell se1 = (new PdfPCell(new Phrase("Semana 1", fuente6)));
            se1.HorizontalAlignment = Element.ALIGN_CENTER;
            se1.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(se1);

            PdfPCell se2 = (new PdfPCell(new Phrase("Semana 2", fuente6)));
            se2.HorizontalAlignment = Element.ALIGN_CENTER;
            se2.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(se2);

            PdfPCell se3 = (new PdfPCell(new Phrase("Semana 3", fuente6)));
            se3.HorizontalAlignment = Element.ALIGN_CENTER;
            se3.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(se3);

            PdfPCell se4 = (new PdfPCell(new Phrase("Semana 4", fuente6)));
            se4.HorizontalAlignment = Element.ALIGN_CENTER;
            se4.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(se4);

            PdfPCell se5 = (new PdfPCell(new Phrase("Semana 5", fuente6)));
            se5.HorizontalAlignment = Element.ALIGN_CENTER;
            se5.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(se5);

            PdfPCell se6 = (new PdfPCell(new Phrase("Semana 6", fuente6)));
            se6.HorizontalAlignment = Element.ALIGN_CENTER;
            se6.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(se6);

            PdfPCell se7 = (new PdfPCell(new Phrase("Semana 7", fuente6)));
            se7.HorizontalAlignment = Element.ALIGN_CENTER;
            se7.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(se7);

            PdfPCell se8 = (new PdfPCell(new Phrase("Semana 8", fuente6)));
            se8.HorizontalAlignment = Element.ALIGN_CENTER;
            se8.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(se8);

            PdfPCell pag1 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag1.HorizontalAlignment = Element.ALIGN_CENTER;
            pag1.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(pag1);

            PdfPCell pag2 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag2.HorizontalAlignment = Element.ALIGN_CENTER;
            pag2.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(pag2);

            PdfPCell pag3 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag3.HorizontalAlignment = Element.ALIGN_CENTER;
            pag3.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(pag3);

            PdfPCell pag4 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag4.HorizontalAlignment = Element.ALIGN_CENTER;
            pag4.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(pag4);

            PdfPCell pag5 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag5.HorizontalAlignment = Element.ALIGN_CENTER;
            pag5.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(pag5);

            PdfPCell pag6 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag6.HorizontalAlignment = Element.ALIGN_CENTER;
            pag6.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(pag6);

            PdfPCell pag7 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag7.HorizontalAlignment = Element.ALIGN_CENTER;
            pag7.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(pag7);

            PdfPCell pag8 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag8.HorizontalAlignment = Element.ALIGN_CENTER;
            pag8.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(pag8);

            PdfPCell ahorro1 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro1.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro1.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(ahorro1);

            PdfPCell ahorro2 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro2.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro2.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(ahorro2);

            PdfPCell ahorro3 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro3.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro3.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(ahorro3);

            PdfPCell ahorro4 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro4.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro4.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(ahorro4);

            PdfPCell ahorro5 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro5.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro5.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(ahorro5);

            PdfPCell ahorro6 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro6.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro6.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(ahorro6);

            PdfPCell ahorro7 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro7.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro7.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(ahorro7);

            PdfPCell ahorro8 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro8.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro8.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab.AddCell(ahorro8);





            documento.Add(tab);

            PdfPTable tabAg = new PdfPTable(13);
            tabAg.SetWidths(new float[] { 2, 25, 5, 5, 7, 7, 7, 7, 7, 7, 7, 7, 7 });
            tabAg.WidthPercentage = 100f;
            tabAg.HorizontalAlignment = Element.ALIGN_CENTER;
            decimal totalGL = 0;
            decimal totalsal = 0;
            decimal totalpagSem = 0;

            for (int i = 1; i <= 8; i++)
            {
                infor.npago = i;

                infor.obtieneInfoDetalle();

                string nombrecli = "";
                int cont = 1;
                decimal gliq = 0;
                decimal salTotal = 0;
                decimal pagSemanal = 0;
                decimal ahorroSem = 0;
              
                decimal numSemana = 0;
                decimal pagoSem1 = 0;
                decimal pagoSem2 = 0;
                decimal pagoSem3 = 0;
                decimal pagoSem4 = 0;
                decimal pagoSem5 = 0;
                decimal pagoSem6 = 0;
                decimal pagoSem7 = 0;
                decimal pagoSem8 = 0;
                decimal ahor1 = 0;
                decimal ahor2 = 0;
                decimal ahor3 = 0;
                decimal ahor4 = 0;
                decimal ahor5 = 0;
                decimal ahor6 = 0;
                decimal ahor7 = 0;
                decimal ahor8 = 0;
                decimal pagoSem9 = 0;
                decimal pagoSem10 = 0;
                decimal pagoSem11 = 0;
                decimal pagoSem12 = 0;
                decimal pagoSem13 = 0;
                decimal pagoSem14 = 0;
                decimal pagoSem15 = 0;
                decimal pagoSem16 = 0;

                if (Convert.ToBoolean(infor.retorno[0]))
                {
                    DataSet ds = (DataSet)infor.retorno[1];


                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        nombrecli = Convert.ToString(r[0]);
                        gliq = Convert.ToDecimal(r[1]);
                        salTotal = Convert.ToDecimal(r[2]);
                        pagSemanal = Convert.ToDecimal(r[3]);
                        try
                        {
                            numSemana = Convert.ToDecimal(r[4]);
                        }
                        catch
                        {
                            numSemana = 0;
                        }


                        PdfPCell DAT1 = (new PdfPCell(new Phrase(cont.ToString(), fuente6)) { Rowspan = 2 });
                        DAT1.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT1);

                        PdfPCell DAT2 = (new PdfPCell(new Phrase(nombrecli, fuente6)) { Rowspan = 2 });
                        DAT2.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT2.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT2);

                        PdfPCell DAT3 = (new PdfPCell(new Phrase(gliq.ToString("C2"), fuente6)) { Rowspan = 2 });
                        DAT3.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT3.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT3);

                        PdfPCell DAT4 = (new PdfPCell(new Phrase(salTotal.ToString("C2"), fuente6)) { Rowspan = 2 });
                        DAT4.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT4.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT4);

                        PdfPCell DAT5 = (new PdfPCell(new Phrase(pagSemanal.ToString("C2"), fuente6)) { Rowspan = 2 });
                        DAT5.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT5.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT5);

                        //pago por semanas
                        if (numSemana == 1)
                        {
                            pagoSem1 = Convert.ToDecimal(r[5]);
                        }

                        if (numSemana == 2)
                        {
                            pagoSem2 = Convert.ToDecimal(r[5]);
                        }
                        else
                        {
                            pagoSem2 = 0;
                        }
                        if (numSemana == 3)
                        {
                            pagoSem3 = Convert.ToDecimal(r[5]);
                        }
                        else
                        {
                            pagoSem3 = 0;
                        }
                        if (numSemana == 4)
                        {
                            pagoSem4 = Convert.ToDecimal(r[5]);
                        }
                        else
                        {
                            pagoSem4 = 0;
                        }
                        if (numSemana == 5)
                        {
                            pagoSem5 = Convert.ToDecimal(r[5]);
                        }
                        else
                        {
                            pagoSem5 = 0;
                        }
                        if (numSemana == 6)
                        {
                            pagoSem6 = Convert.ToDecimal(r[5]);
                        }
                        else
                        {
                            pagoSem6 = 0;
                        }
                        if (numSemana == 7)
                        {
                            pagoSem7 = Convert.ToDecimal(r[5]);
                        }
                        else
                        {
                            pagoSem7 = 0;
                        }
                        if (numSemana == 8)
                        {
                            pagoSem8 = Convert.ToDecimal(r[5]);
                        }
                        else
                        {
                            pagoSem8 = 0;
                        }

                        //pago por semanas

                        PdfPCell DAT6 = (new PdfPCell(new Phrase(pagoSem1.ToString("C2"), fuente6)));
                        DAT6.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT6.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT6);

                        PdfPCell DAT7 = (new PdfPCell(new Phrase(pagoSem2.ToString("C2"), fuente6)));
                        DAT7.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT7.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT7);

                        PdfPCell DAT8 = (new PdfPCell(new Phrase(pagoSem3.ToString("C2"), fuente6)));
                        DAT8.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT8.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT8);

                        PdfPCell DAT9 = (new PdfPCell(new Phrase(pagoSem4.ToString("C2"), fuente6)));
                        DAT9.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT9.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT9);

                        PdfPCell DAT10 = (new PdfPCell(new Phrase(pagoSem5.ToString("C2"), fuente6)));
                        DAT10.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT10.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT10);

                        PdfPCell DAT11 = (new PdfPCell(new Phrase(pagoSem6.ToString("C2"), fuente6)));
                        DAT11.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT11.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT11);

                        PdfPCell DAT12 = (new PdfPCell(new Phrase(pagoSem7.ToString("C2"), fuente6)));
                        DAT12.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT12.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT12);

                        PdfPCell DAT13 = (new PdfPCell(new Phrase(pagoSem8.ToString("C2"), fuente6)));
                        DAT13.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT13.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT13);

                        //ahorro semanas
                        if (numSemana == 1)
                        {
                            ahor1 = Convert.ToDecimal(r[6]);
                        }

                        if (numSemana == 2)
                        {
                            ahor2 = Convert.ToDecimal(r[6]);
                        }
                        else
                        {
                            ahor2 = 0;
                        }
                        if (numSemana == 3)
                        {
                            ahor3 = Convert.ToDecimal(r[6]);
                        }
                        else
                        {
                            ahor3 = 0;
                        }
                        if (numSemana == 4)
                        {
                            pagoSem4 = Convert.ToDecimal(r[6]);
                        }
                        else
                        {
                            pagoSem4 = 0;
                        }
                        if (numSemana == 5)
                        {
                            pagoSem5 = Convert.ToDecimal(r[6]);
                        }
                        else
                        {
                            pagoSem5 = 0;
                        }
                        if (numSemana == 6)
                        {
                            pagoSem6 = Convert.ToDecimal(r[6]);
                        }
                        else
                        {
                            pagoSem6 = 0;
                        }
                        if (numSemana == 7)
                        {
                            pagoSem7 = Convert.ToDecimal(r[6]);
                        }
                        else
                        {
                            pagoSem7 = 0;
                        }
                        if (numSemana == 8)
                        {
                            pagoSem8 = Convert.ToDecimal(r[6]);
                        }
                        else
                        {
                            pagoSem8 = 0;
                        }

                        PdfPCell DA14 = (new PdfPCell(new Phrase(ahor1.ToString("C2"), fuente6)));
                        DA14.HorizontalAlignment = Element.ALIGN_CENTER;
                        DA14.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DA14);

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

                        PdfPCell DAT19 = (new PdfPCell(new Phrase(" ", fuente6)));
                        DAT19.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT19.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT19);

                        PdfPCell DAT20 = (new PdfPCell(new Phrase(" ", fuente6)));
                        DAT20.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT20.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT20);

                        PdfPCell DAT21 = (new PdfPCell(new Phrase(" ", fuente6)));
                        DAT21.HorizontalAlignment = Element.ALIGN_CENTER;
                        DAT21.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabAg.AddCell(DAT21);

                        cont++;

                        totalGL = totalGL + gliq;
                        totalsal = totalsal + salTotal;
                        totalpagSem = totalpagSem + pagSemanal;

                    }

                }
            }

            documento.Add(tabAg);

            PdfPTable total = new PdfPTable(12);
            total.SetWidths(new float[] { 27, 5, 5, 7, 7, 7, 7, 7, 7, 7, 7, 7 });
            total.WidthPercentage = 100f;
            total.HorizontalAlignment = Element.ALIGN_CENTER;




            PdfPCell total1 = (new PdfPCell(new Phrase(" TOTAL ", fuente6)) { Rowspan = 2 });
            total1.HorizontalAlignment = Element.ALIGN_CENTER;
            total1.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(total1);

            PdfPCell total2 = (new PdfPCell(new Phrase(totalGL.ToString("C2"), fuente6)) { Rowspan = 2 });
            total2.HorizontalAlignment = Element.ALIGN_CENTER;
            total2.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(total2);

            PdfPCell total3 = (new PdfPCell(new Phrase(totalsal.ToString("C2"), fuente6)) { Rowspan = 2 });
            total3.HorizontalAlignment = Element.ALIGN_CENTER;
            total3.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(total3);

            PdfPCell total4 = (new PdfPCell(new Phrase(totalpagSem.ToString("C2"), fuente6)) { Rowspan = 2 });
            total4.HorizontalAlignment = Element.ALIGN_CENTER;
            total4.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(total4);

            PdfPCell total5 = (new PdfPCell(new Phrase(" ", fuente6)));
            total5.HorizontalAlignment = Element.ALIGN_CENTER;
            total5.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(total5);

            PdfPCell total6 = (new PdfPCell(new Phrase(" ", fuente6)));
            total6.HorizontalAlignment = Element.ALIGN_CENTER;
            total6.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(total6);

            PdfPCell total7 = (new PdfPCell(new Phrase(" ", fuente6)));
            total7.HorizontalAlignment = Element.ALIGN_CENTER;
            total7.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(total7);

            PdfPCell total8 = (new PdfPCell(new Phrase(" ", fuente6)));
            total8.HorizontalAlignment = Element.ALIGN_CENTER;
            total8.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(total8);

            PdfPCell total9 = (new PdfPCell(new Phrase(" ", fuente6)));
            total9.HorizontalAlignment = Element.ALIGN_CENTER;
            total9.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(total9);

            PdfPCell total12 = (new PdfPCell(new Phrase(" ", fuente6)));
            total12.HorizontalAlignment = Element.ALIGN_CENTER;
            total12.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(total12);

            PdfPCell total10 = (new PdfPCell(new Phrase(" ", fuente6)));
            total10.HorizontalAlignment = Element.ALIGN_CENTER;
            total10.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(total10);

            PdfPCell total11 = (new PdfPCell(new Phrase(" ", fuente6)));
            total11.HorizontalAlignment = Element.ALIGN_CENTER;
            total11.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(total11);

            PdfPCell tota20 = (new PdfPCell(new Phrase(" ", fuente6)));
            tota20.HorizontalAlignment = Element.ALIGN_CENTER;
            tota20.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(tota20);

            PdfPCell tota21 = (new PdfPCell(new Phrase(" ", fuente6)));
            tota21.HorizontalAlignment = Element.ALIGN_CENTER;
            tota21.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(tota21);

            PdfPCell tota22 = (new PdfPCell(new Phrase(" ", fuente6)));
            tota22.HorizontalAlignment = Element.ALIGN_CENTER;
            tota22.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(tota22);

            PdfPCell tota23 = (new PdfPCell(new Phrase(" ", fuente6)));
            tota23.HorizontalAlignment = Element.ALIGN_CENTER;
            tota23.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(tota23);

            PdfPCell tota24 = (new PdfPCell(new Phrase(" ", fuente6)));
            tota24.HorizontalAlignment = Element.ALIGN_CENTER;
            tota24.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(tota24);

            PdfPCell tota25 = (new PdfPCell(new Phrase(" ", fuente6)));
            tota25.HorizontalAlignment = Element.ALIGN_CENTER;
            tota25.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(tota25);

            PdfPCell tota26 = (new PdfPCell(new Phrase(" ", fuente6)));
            tota26.HorizontalAlignment = Element.ALIGN_CENTER;
            tota26.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(tota26);

            PdfPCell tota27 = (new PdfPCell(new Phrase(" ", fuente6)));
            tota27.HorizontalAlignment = Element.ALIGN_CENTER;
            tota27.VerticalAlignment = Element.ALIGN_MIDDLE;
            total.AddCell(tota27);

            documento.Add(total);


            documento.Add(new Paragraph(" "));


            documento.Add(new Paragraph(" "));

            PdfPTable firmpie = new PdfPTable(4);
            firmpie.SetWidths(new float[] { 25, 25, 25, 25 });
            firmpie.WidthPercentage = 100f;
            firmpie.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell horrocrux1 = (new PdfPCell(new Phrase("\n\n\n\n\n  ", fuente8)));
            horrocrux1.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux1.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux1);

            PdfPCell horrocrux2 = (new PdfPCell(new Phrase("\n\n\n\n\n  ", fuente8)));
            horrocrux2.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux2.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux2);

            PdfPCell horrocrux3 = (new PdfPCell(new Phrase("\n\n\n\n\n  ", fuente8)));
            horrocrux3.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux3.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux3);

            PdfPCell horrocrux4 = (new PdfPCell(new Phrase("\n\n\n\n\n  ", fuente8)));
            horrocrux4.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux4.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux4);

            PdfPCell horrocrux5 = (new PdfPCell(new Phrase("PRESIDENTA", fuente8)));
            horrocrux5.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux5.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux5);

            PdfPCell horrocrux7 = (new PdfPCell(new Phrase("TESORERA", fuente8)));
            horrocrux7.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux7.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux7);

            PdfPCell horrocrux6 = (new PdfPCell(new Phrase("SECRETARIA", fuente8)));
            horrocrux6.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux6.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux6);



            PdfPCell horrocrux8 = (new PdfPCell(new Phrase("SUPERVISORA", fuente8)));
            horrocrux8.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux8.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie.AddCell(horrocrux8);
            documento.Add(firmpie);


            documento.NewPage();
    

            PdfPTable tablaEncabezado2 = new PdfPTable(2);
            tablaEncabezado2.SetWidths(new float[] { 2f, 8f });
            tablaEncabezado2.DefaultCell.Border = 0;
            tablaEncabezado2.WidthPercentage = 100f;


            PdfPCell titulo2 = new PdfPCell(new Phrase("APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + "CONTROL DE PAGO Y AHORRO SEMANAL", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

            titulo2.HorizontalAlignment = 1;
            titulo2.Border = 0;
            titulo2.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado2.AddCell(logo);
            tablaEncabezado2.AddCell(titulo);
            documento.Add(tablaEncabezado2);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            //PRIMERA TABLA
            PdfPTable encab22 = new PdfPTable(10);
            encab22.SetWidths(new float[] { 10, 20, 9, 9, 9, 9, 9, 9, 8, 8 });
            encab22.DefaultCell.Border = 0;
            encab22.WidthPercentage = 100f;



            PdfPCell gruPro22 = new PdfPCell(new Phrase("GRUPO PRODUCTIVO:", fuente6));
            gruPro22.HorizontalAlignment = Element.ALIGN_CENTER;
            gruPro22.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab22.AddCell(gruPro22);

            PdfPCell gruPro12 = new PdfPCell(new Phrase(" " + grupopro, fuente6));
            gruPro12.HorizontalAlignment = Element.ALIGN_CENTER;
            encab22.AddCell(gruPro12);

            PdfPCell noGru2 = new PdfPCell(new Phrase("NUM. DE GRUPO:", fuente6));
            noGru2.HorizontalAlignment = Element.ALIGN_CENTER;
            noGru2.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab22.AddCell(noGru2);

            PdfPCell noGru12 = new PdfPCell(new Phrase(" " + numGrupo, fuente6));
            noGru12.HorizontalAlignment = Element.ALIGN_CENTER;
            encab22.AddCell(noGru12);

            PdfPCell numCre2 = new PdfPCell(new Phrase("NUM. DE CRÉDITO:", fuente6));
            numCre2.HorizontalAlignment = Element.ALIGN_CENTER;
            numCre2.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab22.AddCell(numCre2);

            PdfPCell numCre12 = new PdfPCell(new Phrase(" " + numeCre, fuente6));
            numCre12.HorizontalAlignment = Element.ALIGN_CENTER;
            encab22.AddCell(numCre12);

            PdfPCell ciclo2 = new PdfPCell(new Phrase("CICLO:", fuente6));
            ciclo2.HorizontalAlignment = Element.ALIGN_CENTER;
            ciclo2.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab22.AddCell(ciclo2);

            PdfPCell ciclo12 = new PdfPCell(new Phrase(" " + ciclog, fuente6));
            ciclo12.HorizontalAlignment = Element.ALIGN_CENTER;
            encab22.AddCell(ciclo12);


            PdfPCell sucur2 = new PdfPCell(new Phrase("SUCURSAL:", fuente6));
            sucur2.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.BackgroundColor = BaseColor.LIGHT_GRAY;
            encab.AddCell(sucur2);

            PdfPCell sucur12 = new PdfPCell(new Phrase(" " + ciclog, fuente6));
            sucur12.HorizontalAlignment = Element.ALIGN_CENTER;
            encab.AddCell(sucur12);
            documento.Add(encab22);

            PdfPTable encabSeg = new PdfPTable(4);
            encabSeg.SetWidths(new float[] { 10, 15, 10, 15 });
            encabSeg.DefaultCell.Border = 0;
            encabSeg.WidthPercentage = 70f;
            encabSeg.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell fechIn2 = new PdfPCell(new Phrase("FECHA DE INICIO:", fuente6));
            fechIn2.HorizontalAlignment = Element.ALIGN_CENTER;
            fechIn2.BackgroundColor = BaseColor.LIGHT_GRAY;
            encabSeg.AddCell(fechIn2);

            PdfPCell fechIn12 = new PdfPCell(new Phrase(" " + Convert.ToDateTime(fechaini).ToString("dd/MM/yyyy"), fuente6));
            fechIn12.HorizontalAlignment = Element.ALIGN_CENTER;
            encabSeg.AddCell(fechIn12);

            PdfPCell fechNf2 = new PdfPCell(new Phrase("FECHA DE TERMINO:", fuente6));
            fechNf2.HorizontalAlignment = Element.ALIGN_CENTER;
            fechNf2.BackgroundColor = BaseColor.LIGHT_GRAY;
            encabSeg.AddCell(fechNf2);

            PdfPCell fechNf12 = new PdfPCell(new Phrase(" " + Convert.ToDateTime(ofec).ToString("dd/MM/yyyy"), fuente6));
            fechNf12.HorizontalAlignment = Element.ALIGN_CENTER;
            encabSeg.AddCell(fechNf12);
            documento.Add(encabSeg);
            documento.Add(new Paragraph(" "));

            PdfPTable tab2 = new PdfPTable(13);
            tab2.SetWidths(new float[] { 2, 27, 5, 5, 5, 7, 7, 7, 7, 7, 7, 7, 7 });
            tab2.DefaultCell.Border = 0;
            tab2.WidthPercentage = 100f;
            tab2.HorizontalAlignment = Element.ALIGN_LEFT;


            PdfPCell no2 = (new PdfPCell(new Phrase("No.", fuente6)) { Rowspan = 3 });
            no2.HorizontalAlignment = Element.ALIGN_CENTER;
            no2.BackgroundColor = BaseColor.LIGHT_GRAY;
            no2.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab2.AddCell(no2);

            PdfPCell nom2 = (new PdfPCell(new Phrase("NOMBRE DEL CLIENTE", fuente6)) { Rowspan = 3 });
            nom2.HorizontalAlignment = Element.ALIGN_CENTER;
            nom2.BackgroundColor = BaseColor.LIGHT_GRAY;
            nom2.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab2.AddCell(nom2);

            PdfPCell gl2 = (new PdfPCell(new Phrase("GL", fuente6)) { Rowspan = 3 });
            gl2.HorizontalAlignment = Element.ALIGN_CENTER;
            gl2.BackgroundColor = BaseColor.LIGHT_GRAY;
            gl2.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab2.AddCell(gl2);

            PdfPCell sald2 = (new PdfPCell(new Phrase("SALDO TOTAL", fuente6)) { Rowspan = 3 });
            sald2.HorizontalAlignment = Element.ALIGN_CENTER;
            sald2.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(sald2);

            PdfPCell pagSem2 = (new PdfPCell(new Phrase("PAGO SEMANAL", fuente6)) { Rowspan = 3 });
            pagSem2.HorizontalAlignment = Element.ALIGN_CENTER;
            pagSem2.BackgroundColor = BaseColor.LIGHT_GRAY;
            pagSem2.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab2.AddCell(pagSem2);

            PdfPCell se12 = (new PdfPCell(new Phrase("Semana 9", fuente6)));
            se12.HorizontalAlignment = Element.ALIGN_CENTER;
            se12.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(se12);

            PdfPCell se22 = (new PdfPCell(new Phrase("Semana 10", fuente6)));
            se22.HorizontalAlignment = Element.ALIGN_CENTER;
            se22.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(se22);

            PdfPCell se32 = (new PdfPCell(new Phrase("Semana 11", fuente6)));
            se32.HorizontalAlignment = Element.ALIGN_CENTER;
            se32.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(se32);

            PdfPCell se42 = (new PdfPCell(new Phrase("Semana 12", fuente6)));
            se42.HorizontalAlignment = Element.ALIGN_CENTER;
            se42.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(se42);

            PdfPCell se52 = (new PdfPCell(new Phrase("Semana 13", fuente6)));
            se52.HorizontalAlignment = Element.ALIGN_CENTER;
            se52.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(se52);

            PdfPCell se62 = (new PdfPCell(new Phrase("Semana 14", fuente6)));
            se62.HorizontalAlignment = Element.ALIGN_CENTER;
            se62.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(se62);

            PdfPCell se72 = (new PdfPCell(new Phrase("Semana 15", fuente6)));
            se72.HorizontalAlignment = Element.ALIGN_CENTER;
            se72.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(se72);

            PdfPCell se82 = (new PdfPCell(new Phrase("Semana 16", fuente6)));
            se82.HorizontalAlignment = Element.ALIGN_CENTER;
            se82.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(se82);

            PdfPCell pag12 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag12.HorizontalAlignment = Element.ALIGN_CENTER;
            pag12.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(pag12);

            PdfPCell pag22 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag22.HorizontalAlignment = Element.ALIGN_CENTER;
            pag22.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(pag22);

            PdfPCell pag32 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag32.HorizontalAlignment = Element.ALIGN_CENTER;
            pag32.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(pag32);

            PdfPCell pag42 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag42.HorizontalAlignment = Element.ALIGN_CENTER;
            pag42.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(pag42);

            PdfPCell pag52 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag52.HorizontalAlignment = Element.ALIGN_CENTER;
            pag52.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(pag52);

            PdfPCell pag62 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag62.HorizontalAlignment = Element.ALIGN_CENTER;
            pag62.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(pag62);

            PdfPCell pag72 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag72.HorizontalAlignment = Element.ALIGN_CENTER;
            pag72.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(pag72);

            PdfPCell pag82 = (new PdfPCell(new Phrase("PAGO", fuente6)));
            pag82.HorizontalAlignment = Element.ALIGN_CENTER;
            pag82.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(pag82);

            PdfPCell ahorro12 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro12.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro12.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(ahorro12);

            PdfPCell ahorro22 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro22.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro22.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(ahorro22);

            PdfPCell ahorro32 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro32.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro32.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(ahorro32);

            PdfPCell ahorro42 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro42.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro42.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(ahorro42);

            PdfPCell ahorro52 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro52.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro52.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(ahorro52);

            PdfPCell ahorro62 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro62.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro62.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(ahorro62);

            PdfPCell ahorro72 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro72.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro72.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(ahorro72);

            PdfPCell ahorro82 = (new PdfPCell(new Phrase("AHORRO", fuente6)));
            ahorro82.HorizontalAlignment = Element.ALIGN_CENTER;
            ahorro82.BackgroundColor = BaseColor.LIGHT_GRAY;
            tab2.AddCell(ahorro82);




            documento.Add(tab2);

            PdfPTable tabAg2 = new PdfPTable(13);
            tabAg2.SetWidths(new float[] { 2, 27, 5, 5, 5, 7, 7, 7, 7, 7, 7, 7, 7 });
            tabAg2.WidthPercentage = 100f;
            tabAg2.HorizontalAlignment = Element.ALIGN_CENTER;




       /*     if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    nombrecli = Convert.ToString(r[0]);

                    PdfPCell DAT1 = (new PdfPCell(new Phrase(cont.ToString(), fuente6)) { Rowspan = 2 });
                    DAT1.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT1);

                    PdfPCell DAT2 = (new PdfPCell(new Phrase(nombrecli, fuente6)) { Rowspan = 2 });
                    DAT2.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT2.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT2);

                    PdfPCell DAT3 = (new PdfPCell(new Phrase(" ", fuente6)) { Rowspan = 2 });
                    DAT3.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT3.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT3);

                    PdfPCell DAT4 = (new PdfPCell(new Phrase(" ", fuente6)) { Rowspan = 2 });
                    DAT4.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT4.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT4);

                    PdfPCell DAT5 = (new PdfPCell(new Phrase(" ", fuente6)) { Rowspan = 2 });
                    DAT5.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT5.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT5);

                    PdfPCell DAT6 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT6.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT6.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT6);

                    PdfPCell DAT7 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT7.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT7.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT7);

                    PdfPCell DAT8 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT8.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT8.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT8);

                    PdfPCell DAT9 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT9.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT9.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT9);

                    PdfPCell DAT10 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT10.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT10.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT10);

                    PdfPCell DAT11 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT11.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT11.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT11);

                    PdfPCell DAT12 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT12.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT12.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT12);

                    PdfPCell DAT13 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT13.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT13.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT13);

                    PdfPCell DA14 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DA14.HorizontalAlignment = Element.ALIGN_CENTER;
                    DA14.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DA14);

                    PdfPCell DAT15 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT15.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT15.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT15);

                    PdfPCell DAT16 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT16.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT16.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT16);

                    PdfPCell DAT17 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT17.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT17.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT17);

                    PdfPCell DAT18 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT18.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT18.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT18);

                    PdfPCell DAT19 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT19.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT19.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT19);

                    PdfPCell DAT20 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT20.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT20.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT20);

                    PdfPCell DAT21 = (new PdfPCell(new Phrase(" ", fuente6)));
                    DAT21.HorizontalAlignment = Element.ALIGN_CENTER;
                    DAT21.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tabAg2.AddCell(DAT21);

                    cont++;
                }
            }

            documento.Add(tabAg2);

            PdfPTable totales2 = new PdfPTable(12);
            totales2.SetWidths(new float[] { 29, 5, 5, 5, 7, 7, 7, 7, 7, 7, 7, 7 });
            totales2.WidthPercentage = 100f;
            totales2.HorizontalAlignment = Element.ALIGN_CENTER;



            PdfPCell tabTota = (new PdfPCell(new Phrase(" TOTAL ", fuente6)) { Rowspan = 2 });
            tabTota.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota);

            PdfPCell tabTota1 = (new PdfPCell(new Phrase(" ", fuente6)) { Rowspan = 2 });
            tabTota1.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota1.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota1);

            PdfPCell tabTota2 = (new PdfPCell(new Phrase(" ", fuente6)) { Rowspan = 2 });
            tabTota2.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota2.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota2);

            PdfPCell tabTota3 = (new PdfPCell(new Phrase(" ", fuente6)) { Rowspan = 2 });
            tabTota3.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota3.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota3);

            PdfPCell tabTota4 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota4.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota4.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota4);

            PdfPCell tabTota5 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota5.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota5.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota5);

            PdfPCell tabTota6 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota6.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota6.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota6);

            PdfPCell tabTota7 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota7.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota7.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota7);

            PdfPCell tabTota8 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota8.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota8.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota8);

            PdfPCell tabTota9 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota9.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota9.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota9);

            PdfPCell tabTota10 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota10.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota10.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota10);

            PdfPCell tabTota11 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota11.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota11.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota11);

            PdfPCell tabTota12 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota12.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota12.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota12);

            PdfPCell tabTota13 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota13.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota13.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota13);

            PdfPCell tabTota14 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota14.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota14.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota14);

            PdfPCell tabTota15 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota15.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota15.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota15);

            PdfPCell tabTota16 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota16.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota16.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota16);

            PdfPCell tabTota17 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota17.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota17.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota17);

            PdfPCell tabTota18 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota18.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota18.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota18);

            PdfPCell tabTota19 = (new PdfPCell(new Phrase(" ", fuente6)));
            tabTota19.HorizontalAlignment = Element.ALIGN_CENTER;
            tabTota19.VerticalAlignment = Element.ALIGN_MIDDLE;
            totales2.AddCell(tabTota19);

            documento.Add(totales2);

            documento.Add(new Paragraph(" "));


            documento.Add(new Paragraph(" "));

            PdfPTable firmpie2 = new PdfPTable(4);
            firmpie2.SetWidths(new float[] { 25, 25, 25, 25 });
            firmpie2.WidthPercentage = 100f;
            firmpie2.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell horrocrux12 = (new PdfPCell(new Phrase("\n\n\n\n\n  ", fuente8)));
            horrocrux12.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux12.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie2.AddCell(horrocrux12);

            PdfPCell horrocrux13 = (new PdfPCell(new Phrase("\n\n\n\n\n  ", fuente8)));
            horrocrux13.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux13.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie2.AddCell(horrocrux13);

            PdfPCell horrocrux14 = (new PdfPCell(new Phrase("\n\n\n\n\n  ", fuente8)));
            horrocrux14.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux14.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie2.AddCell(horrocrux14);

            PdfPCell horrocrux15 = (new PdfPCell(new Phrase("\n\n\n\n\n  ", fuente8)));
            horrocrux15.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux15.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie2.AddCell(horrocrux15);

            PdfPCell horrocrux16 = (new PdfPCell(new Phrase("PRESIDENTA", fuente8)));
            horrocrux16.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux16.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie2.AddCell(horrocrux16);

            PdfPCell horrocrux17 = (new PdfPCell(new Phrase("TESORERA", fuente8)));
            horrocrux17.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux17.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie2.AddCell(horrocrux17);

            PdfPCell horrocrux18 = (new PdfPCell(new Phrase("SECRETARIA", fuente8)));
            horrocrux18.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux18.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie2.AddCell(horrocrux18);



            PdfPCell horrocrux19 = (new PdfPCell(new Phrase("SUPERVISORA", fuente8)));
            horrocrux19.HorizontalAlignment = Element.ALIGN_CENTER;
            horrocrux19.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmpie2.AddCell(horrocrux19);
            documento.Add(firmpie2);
            */


            documento.Close();




            FileInfo filename = new FileInfo(archivo);
            if (filename.Exists)
            {
                string url = "Descargas.aspx?filename=" + filename.Name;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "pdfs", "window.open('" + url + "', 'nuevo', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, titlebar=no, fullscreen=yes,resizable=yes');", true);
            }

        }
    
}

    protected void RadGrid1_ItemUpdated(object sender, GridUpdatedEventArgs e)
    {
        string sql = SqlDataSource2.UpdateCommand;
        cmbSemanas.DataBind();
        CtrlPag obt2 = new CtrlPag();
        int[] sesiones = obtieneSesiones();
        int pagoobt = 0;
        int cliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        int credito = sesiones[4];

        if (txt_sem.Text == "16")
        {
            pagoobt = Convert.ToInt32(cmbSemanas.SelectedValue);
            obt2.empresa = empresa;
            obt2.sucursal = sucursal;
            obt2.idcliente = cliente;
            obt2.pago = pagoobt;
            obt2.obtenerSaldos2();

        }
        else if (txtplazo.Text == "20")
        {
            pagoobt = Convert.ToInt32(cmbSemanas20.SelectedValue);
            obt2.empresa = empresa;
            obt2.sucursal = sucursal;
            obt2.idcliente = cliente;
            obt2.pago = pagoobt;
            obt2.obtenerSaldos2();
        }
        else if (txtplazo.Text == "32")
        {
            pagoobt = Convert.ToInt32(cmbsemanas32.SelectedValue);
            obt2.empresa = empresa;
            obt2.sucursal = sucursal;
            obt2.idcliente = cliente;
            obt2.pago = pagoobt;
            obt2.obtenerSaldos2();
        }
        else
        {
            pagoobt = Convert.ToInt32(cmbsemanas64.SelectedValue);
            obt2.empresa = empresa;
            obt2.sucursal = sucursal;
            obt2.idcliente = cliente;
            obt2.pago = pagoobt;
            obt2.obtenerSaldos2();
        }

        /*      int[] sesiones = obtieneSesiones();
              CtrlPag obt2 = new CtrlPag();

              int pago = Convert.ToInt32(cmbSemanas.SelectedValue);
              int empresa = sesiones[2];
              int sucursal = sesiones[3];
              int credito = sesiones[4];
              int pagonew2 = pago + 1;
              obt2.empresa = empresa;
              obt2.sucursal = sucursal;
              obt2.credito = credito;
              obt2.pago = pagoobt;
              obt2.obtenerSaldos2();
              int cliente2 = 0;
              decimal ap = 0;
              decimal dev = 0;


           if (Convert.ToBoolean(obt2.retorno[0]))
              {
                  DataSet ds2 = (DataSet)obt2.retorno[1];


                  foreach (DataRow r2 in ds2.Tables[0].Rows)
                  {


                      cliente2 = Convert.ToInt32(r2[0]);
                      ap = Convert.ToDecimal(r2[1]);
                      dev = Convert.ToDecimal(r2[2]);




                      decimal apnew = ap - dev;
                      dev = dev - dev;
                      decimal devnew = dev;


                      obt2.idcliente = cliente2;
                      obt2.ap = apnew;
                      obt2.apt = 0;
                     // obt2.devt = devnew;
                      obt2.pago = pagoobt + 1;
                      obt2.actualizaAPDEV();

                        CtrlPag obt4 = new CtrlPag();
                        obt4.empresa = empresa;
                        obt4.sucursal = sucursal;
                        obt4.credito = credito;
                        obt4.idcliente = cliente2;
                        obt4.ap = apnew;
                        obt.apt = apnew;
                        obt4.pago = pagoobt + 1;
                        obt4.actualizaAPDEVsig();*/


        SqlDataSource2.SelectCommand = "select d.id_cliente,d.nombre_cliente,d.credito_autorizado*.10 as Gl,d.credito_autorizado,o.pagosemanal,o.saldo_actual,o.fecha_pago,o.fecha_aplicacion,o.no_pago,o.monto_Pago,o.monto_Ahorro,o.ap,o.dev  from AN_Solicitud_Credito_Detalle d left join AN_Operacion_Credito o on d.id_cliente = o.id_cliente where no_pago="+pagoobt+" and id_grupo="+credito ;
                SqlDataSource2.DataBind();


            

        
    }





    protected void cmbSemanas20_TextChanged(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        CtrlPag obt = new CtrlPag();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        int credito = sesiones[4];
        int pago = Convert.ToInt32(cmbSemanas20.SelectedValue);
        int pagonew = pago - 1;
        obt.empresa = empresa;
        obt.sucursal = sucursal;
        obt.credito = credito;
        obt.pago = pagonew;
        obt.obtenerSaldos();

        int cliente = 0;
        decimal ahorro;
        decimal saldo_actual;
        decimal pago2;
        decimal newsaldo = 0;
        decimal ap = 0;
        decimal dev = 0;


        if (Convert.ToBoolean(obt.retorno[0]))
        {
            DataSet ds = (DataSet)obt.retorno[1];


            foreach (DataRow r in ds.Tables[0].Rows)
            {
                cliente = Convert.ToInt32(r[0]);
                saldo_actual = Convert.ToDecimal(r[1]);
                pago2 = Convert.ToDecimal(r[2]);
                ahorro = Convert.ToDecimal(r[3]);
                ap = Convert.ToDecimal(r[4]);


                newsaldo = saldo_actual - (pago2 + ahorro);


                obt.idcliente = cliente;
                obt.saldo_actual = Convert.ToInt32(newsaldo);
                obt.pago = pago;
                obt.actualizaSaldo2();

            }
        }


        RadGrid2.DataBind();
        cmbSemanas.DataBind();
        txt_sem.Text = pago.ToString();

        SqlDataSource2.SelectCommand = "select  d.id_cliente,d.nombre_cliente,d.credito_autorizado*.10 as Gl,d.credito_autorizado,o.pagosemanal,o.saldo_actual,o.fecha_pago,o.fecha_aplicacion,o.no_pago,o.monto_Pago,o.monto_Ahorro,o.ap,o.dev  from AN_Solicitud_Credito_Detalle d left join AN_Operacion_Credito o on d.id_cliente = o.id_cliente where o.id_empresa =" + empresa + " AND o.id_sucursal =" + sucursal + " and o.id_grupo=" + credito + " and no_pago=" + pago;
        SqlDataSource2.DataBind();
    }
}