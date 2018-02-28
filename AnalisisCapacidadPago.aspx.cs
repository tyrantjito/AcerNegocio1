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

public partial class AnalisisCapacidadPago : System.Web.UI.Page
{
    Recepciones recepciones = new Recepciones();
    DatosOrdenes datosOrdenes = new DatosOrdenes();
    protected void Page_Load(object sender, EventArgs e)
    {
        txt_fecha_ela.MaxDate = DateTime.Now;
    }
   
    protected void lnkCerrar_Click(object sender, EventArgs e)
    {
        pnlMask.Visible = false;
        windowAutorizacion.Visible = false;
       // borrarCampos();
    }
    private int[] obtieneSesiones()
    {
        int[] sesiones = new int[4];
        sesiones[0] = Convert.ToInt32(Request.QueryString["u"]);
        sesiones[1] = Convert.ToInt32(Request.QueryString["p"]);
        sesiones[2] = Convert.ToInt32(Request.QueryString["e"]);
        sesiones[3] = Convert.ToInt32(Request.QueryString["t"]);
        return sesiones;
    }
    protected void agregaAnalisisP(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        AnaPag agrega = new AnaPag();
        agrega.empresa = sesiones[2];
        agrega.sucursal = sesiones[3];
        agrega.id_cliente = Convert.ToInt32(cmb_nombre.SelectedValue);
        DateTime fechaEla = Convert.ToDateTime(txt_fecha_ela.SelectedDate);
        agrega.fecha_pago_apago = fechaEla.ToString("yyyy/MM/dd");
        agrega.gerente_apago = Convert.ToString(ddlgerente.SelectedItem);
        agrega.id_grupo = Convert.ToInt32(cmb_sucursal.SelectedValue);
        agrega.grupo_productivo_apago = cmb_sucursal.SelectedItem.Text;
        string[] nombrecompleto = cmb_nombre.SelectedItem.Text.ToString().Split(new char[] { ' ' });
        string nombre = nombrecompleto[0];
        string apaterno = nombrecompleto[1];
        string amaterno = nombrecompleto[2];
        agrega.nombre_cliente_apago = nombre;
        agrega.girio_neg = txt_giro.Text;
        agrega.Lunes = Convert.ToDecimal( txt_l.Text);
        decimal val1= Convert.ToDecimal(txt_l.Text);
        agrega.Martes = Convert.ToDecimal(txt_martes.Text);
        decimal val2 = Convert.ToDecimal(txt_martes.Text);
        agrega.Miercoles = Convert.ToDecimal(txt_miercoles.Text);
        decimal val3= Convert.ToDecimal(txt_miercoles.Text);
        agrega.Jueves = Convert.ToDecimal(txt_jueves.Text);
        decimal val4= Convert.ToDecimal(txt_jueves.Text);
        agrega.Viernes = Convert.ToDecimal(txt_viernes.Text);
        decimal val5 = Convert.ToDecimal(txt_viernes.Text);
        agrega.Sabado = Convert.ToDecimal(txt_sabado.Text);
        decimal val6 = Convert.ToDecimal(txt_sabado.Text);
        agrega.Domingo = Convert.ToDecimal(txt_domingo.Text);
        decimal val7= Convert.ToDecimal(txt_domingo.Text);
        decimal totalSem = val1 + val2 + val3 + val4 + val5 + val6 + val7;
        agrega.total_semanal_ap = totalSem;
        decimal totalMen = totalSem * 4;
        agrega.total_mensual_ap = totalMen;
        agrega.materias_primas = Convert.ToDecimal(txt_matp.Text);
        decimal oval1 = Convert.ToDecimal(txt_matp.Text);
        agrega.mercancias = Convert.ToDecimal(txt_mercancias.Text);
        decimal oval2 = Convert.ToDecimal(txt_mercancias.Text);
        agrega.renta = Convert.ToDecimal(txt_renta.Text);
        decimal oval3 = Convert.ToDecimal(txt_renta.Text);
        agrega.luz = Convert.ToDecimal(txt_luz.Text);
        decimal oval4 = Convert.ToDecimal(txt_luz.Text);
        agrega.agua = Convert.ToDecimal(txt_agua.Text);
        decimal oval5 = Convert.ToDecimal(txt_agua.Text);
        agrega.gas = Convert.ToDecimal(txt_gas.Text);
        decimal oval6 = Convert.ToDecimal(txt_gas.Text);
        agrega.art_papeleria = Convert.ToDecimal(txt_artpape.Text);
        decimal oval7 = Convert.ToDecimal(txt_artpape.Text);
        agrega.telefono = Convert.ToDecimal(txt_telefono.Text);
        decimal oval8 = Convert.ToDecimal(txt_telefono.Text);
        agrega.sueldos_sal = Convert.ToDecimal(txt_sueldos.Text);
        decimal oval9 = Convert.ToDecimal(txt_sueldos.Text);
        agrega.transporte = Convert.ToDecimal(txt_tranfle.Text);
        decimal oval10 = Convert.ToDecimal(txt_tranfle.Text);
        agrega.mantenimiento = Convert.ToDecimal(txt_mantenimiento.Text);
        decimal oval11 = Convert.ToDecimal(txt_mantenimiento.Text);
        agrega.pago_impuestos = Convert.ToDecimal(txt_pagimp.Text);
        decimal oval12 = Convert.ToDecimal(txt_pagimp.Text);
        agrega.pago_finiancieros = Convert.ToDecimal(txt_pagfin.Text);
        decimal oval13 = Convert.ToDecimal(txt_pagfin.Text);
        agrega.otras_deudas = Convert.ToDecimal(txt_odeu.Text);
        decimal oval14 = Convert.ToDecimal(txt_odeu.Text);
        decimal ototalB = oval1 + oval2 + oval3 + oval4 + oval5 + oval6 + oval7 + oval8 + oval9 + oval10 + oval11 + oval12 + oval13 + oval14;
        agrega.total_b = ototalB;
        agrega.renta_cli = Convert.ToDecimal(txt_ren_cli.Text);
        decimal nval1= Convert.ToDecimal(txt_ren_cli.Text);
        agrega.luz_cli = Convert.ToDecimal(txt_luz_cli.Text);
        decimal nval2 = Convert.ToDecimal(txt_luz_cli.Text);
        agrega.agua_cli = Convert.ToDecimal(txt_agua_cli.Text);
        decimal nval3 = Convert.ToDecimal(txt_agua_cli.Text);
        agrega.telefono_cli = Convert.ToDecimal(txt_tel_cli.Text);
        decimal nval4 = Convert.ToDecimal(txt_tel_cli.Text);
        agrega.alimentacion_cli = Convert.ToDecimal(txt_alimentacion.Text);
        decimal nval5 = Convert.ToDecimal(txt_alimentacion.Text);
        agrega.vestido_cli = Convert.ToDecimal(txt_vestido.Text);
        decimal nval6 = Convert.ToDecimal(txt_vestido.Text);
        agrega.gastos_escolares_cli = Convert.ToDecimal(txt_gastos_esc.Text);
        decimal nval7 = Convert.ToDecimal(txt_gastos_esc.Text);
        agrega.gastos_medicos_cli = Convert.ToDecimal(txt_gastos_med.Text);
        decimal nval8 = Convert.ToDecimal(txt_gastos_med.Text);
        agrega.transporte_cli = Convert.ToDecimal(txt_trasnporte_cli.Text);
        decimal nval9 = Convert.ToDecimal(txt_trasnporte_cli.Text);
        agrega.deudas_cli = Convert.ToDecimal(txt_deudas.Text);
        decimal nval10 = Convert.ToDecimal(txt_deudas.Text);
        agrega.mantenimiento_cli = Convert.ToDecimal(txt_mante.Text);
        decimal nval11 = Convert.ToDecimal(txt_mante.Text);
        agrega.pago_impuestos_cli = Convert.ToDecimal(txt_pag_imp.Text);
        decimal nval12 = Convert.ToDecimal(txt_pag_imp.Text);
        agrega.otros_gastos_cli = Convert.ToDecimal(txt_otrosg.Text);
        decimal nval13 = Convert.ToDecimal(txt_otrosg.Text);
        decimal ntotalc = nval1 + nval2 + nval3 + nval4 + nval5 + nval6 + nval7 + nval8 + nval9 + nval10 + nval11 + nval12 + nval13;
        agrega.total_c = ntotalc;
        decimal utilidad = totalMen - ototalB - ntotalc;
        agrega.utilidad = utilidad;
        decimal dispSem = utilidad / 4 * Convert.ToDecimal( 0.70);
        agrega.disponibilidad_semanal = dispSem;
        agrega.monto_credito = Convert.ToDecimal(txt_mon_cred.Text);
        agrega.plazo = Convert.ToDecimal(txt_plazo.Text);
        agrega.pago_semanal = Convert.ToDecimal(txt_pagsem.Text);
        decimal pagSem= Convert.ToDecimal(txt_pagsem.Text);
        decimal solvenciap = dispSem - pagSem;
        agrega.solvencia = solvenciap;
        agrega.asesor_apago = Convert.ToString(ddlAsesor.SelectedValue);

        agrega.dictamen_final = Convert.ToString(cmb_dic_fin.SelectedValue);
        if (lblAnalisis.Text == "Agrega Analisis de Pago")
        {
            agrega.agregarAnaPago();
             if (Convert.ToBoolean(agrega.retorno[1]) == false)
              {
                  lblErrorAgrega.Visible = true;
                  lblErrorAgrega.Text = "Error al agregar la solicitud";
              }
              else
              {
                  RadGrid1.DataBind();
                  borrarCampos();
                  pnlMask.Visible = false;
                  windowAutorizacion.Visible = false;
                  lblErrorAgrega.Text = "Se agrego exitosamente";
              }
          }
          else
          {
            agrega.id_apago = Convert.ToInt32(RadGrid1.SelectedValues["id_apago"]);
            agrega.id_cliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
            agrega.acutulizaAnalisis();

              if (Convert.ToBoolean(agrega.retorno[1]) == false)
              {
                  lblErrorAgrega.Visible = true;
                  lblErrorAgrega.Text = "Error al editar la solicitud";
              }
              else
              {
                  RadGrid1.DataBind();
                  borrarCampos();
                  pnlMask.Visible = false;
                  windowAutorizacion.Visible = false;
                  lblErrorAgrega.Text = "Se edito exitosamente";
              }
          }


    } 
    protected void cmbPersonaSelected(object sender, EventArgs e)
    {
        
    }

    protected void lnkAbreWindow_Click(object sender, EventArgs e)
    {
        lblAnalisis.Text = "Agrega Analisis de Pago";
        pnlMask.Visible = true;
        windowAutorizacion.Visible = true;
        obtieneSesiones();
        int[] sesiones = new int[4];
        sesiones[0] = Convert.ToInt32(Request.QueryString["u"]);
        sesiones[1] = Convert.ToInt32(Request.QueryString["p"]);
        sesiones[2] = Convert.ToInt32(Request.QueryString["e"]);
        sesiones[3] = Convert.ToInt32(Request.QueryString["t"]);
        txt_sucursal.Text = Convert.ToString(sesiones[2]);
        

 
    }
    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lnkAbreEdicion.Visible = true;
        lnkImprimir.Visible = true;
    }
   protected void lnkAbreEdicion_Click(object sender, EventArgs e)
    {
        lblAnalisis.Text = "Edita Visita";
        pnlMask.Visible = true;
        windowAutorizacion.Visible = true;
        int id_cliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
        int id_apago = Convert.ToInt32(RadGrid1.SelectedValues["id_apago"]);

        int[] sesiones = obtieneSesiones();
        AnaPag agregar = new AnaPag();
        agregar.empresa = sesiones[2];
        agregar.sucursal = sesiones[3];
        agregar.id_cliente = id_cliente;
        agregar.id_apago = id_apago;
        agregar.edicionAnalisis();
        if (Convert.ToBoolean(agregar.retorno[0]))
        {
            DataSet ds = (DataSet)agregar.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
              //  ddlGerente.Text = Convert.ToString(r[6]);
                txt_fecha_ela.SelectedDate = Convert.ToDateTime(r[7]);
                txt_l.Text = Convert.ToString(r[11]);
                txt_martes.Text = Convert.ToString(r[12]);
                txt_miercoles.Text = Convert.ToString(r[13]);
                txt_jueves.Text = Convert.ToString(r[14]);
                txt_viernes.Text = Convert.ToString(r[15]);
                txt_sabado.Text = Convert.ToString(r[16]);
                txt_domingo.Text = Convert.ToString(r[17]);
                txt_totals.Text = Convert.ToString(r[18]);
                txt_totalm.Text = Convert.ToString(r[19]);
                txt_matp.Text = Convert.ToString(r[20]);
                txt_mercancias.Text = Convert.ToString(r[21]);
                txt_renta.Text = Convert.ToString(r[22]);
                txt_luz.Text = Convert.ToString(r[23]);
                txt_agua.Text = Convert.ToString(r[24]);
                txt_gas.Text = Convert.ToString(r[25]);
                txt_artpape.Text = Convert.ToString(r[26]);
                txt_telefono.Text = Convert.ToString(r[27]);
                txt_sueldos.Text = Convert.ToString(r[28]);
                txt_tranfle.Text = Convert.ToString(r[29]);
                txt_mantenimiento.Text = Convert.ToString(r[30]);
                txt_pagimp.Text = Convert.ToString(r[31]);
                txt_pagfin.Text = Convert.ToString(r[32]);
                txt_odeu.Text = Convert.ToString(r[33]);
                txt_totalb.Text = Convert.ToString(r[34]);
                txt_ren_cli.Text = Convert.ToString(r[35]);
                txt_luz_cli.Text = Convert.ToString(r[36]);
                txt_agua_cli.Text = Convert.ToString(r[37]);
                txt_tel_cli.Text = Convert.ToString(r[38]);
                txt_alimentacion.Text = Convert.ToString(r[39]);
                txt_vestido.Text = Convert.ToString(r[40]);
                txt_gastos_esc.Text = Convert.ToString(r[41]);
                txt_gastos_med.Text = Convert.ToString(r[42]);
                txt_trasnporte_cli.Text = Convert.ToString(r[43]);
                txt_deudas.Text = Convert.ToString(r[44]);
                txt_mante.Text = Convert.ToString(r[45]);
                txt_pag_imp.Text = Convert.ToString(r[46]);
                txt_otrosg.Text = Convert.ToString(r[47]);
                txt_totalc.Text = Convert.ToString(r[48]);
                txt_utilidad.Text = Convert.ToString(r[49]);
                txt_dissem.Text = Convert.ToString(r[50]);
                txt_mon_cred.Text = Convert.ToString(r[51]);
                txt_plazo.Text = Convert.ToString(r[52]);
                txt_pagsem.Text = Convert.ToString(r[53]);
                txt_solvencia.Text = Convert.ToString(r[54]);
                cmb_dic_fin.SelectedValue = Convert.ToString(r[55]);
            }
        }

    }
    public void borrarCampos()
    {
       /* txt_gerente.Text = "";
        txt_fecha_ela.Clear();
        txt_l.Text="";
        txt_martes.Text="";
        txt_miercoles.Text="";
        txt_jueves.Text="";
        txt_viernes.Text="";
        txt_sabado.Text="";
        txt_domingo.Text="";
        txt_matp.Text="";
        txt_mercancias.Text="";
        txt_renta.Text="";
        txt_luz.Text="";
        txt_agua.Text="";
        txt_gas.Text="";
        txt_artpape.Text="";
        txt_telefono.Text="";
        txt_sueldos.Text="";
        txt_tranfle.Text="";
        txt_mantenimiento.Text="";
        txt_pagimp.Text="";
        txt_pagfin.Text="";
        txt_odeu.Text="";
        txt_totalb.Text="";
        txt_ren_cli.Text="";
        txt_luz_cli.Text="";
        txt_agua_cli.Text="";
        txt_tel_cli.Text="";
        txt_alimentacion.Text="";
        txt_vestido.Text="";
        txt_gastos_esc.Text="";
        txt_gastos_med.Text="";
        txt_trasnporte_cli.Text="";
        txt_deudas.Text="";
        txt_mante.Text="";
        txt_pag_imp.Text="";
        txt_otrosg.Text="";
        txt_mon_cred.Text="";
        txt_plazo.Text="";
        txt_pagsem.Text="";*/
    }
    protected void lnkImprimir_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;

        // Tipo de Font que vamos utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();


        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" Analisis Capacidad de Pago ");
        documento.AddCreator("AserNegocio1");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\AnalisisCapasidadDePago_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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


            PdfPCell titulo = new PdfPCell(new Phrase("APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + "ANÁLISIS DE CAPACIDAD DE PAGO", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(logo);
            tablaEncabezado.AddCell(titulo);
            documento.Add(tablaEncabezado);

            //PRIMERA TABLA

            PdfPTable enca = new PdfPTable(4);
            enca.SetWidths(new float[] { 25, 25, 25, 25 });
            enca.WidthPercentage = 100f;


            AnaPag infor = new AnaPag();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            int id_cliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);

            infor.id_cliente = id_cliente;
            infor.optieneimpresion();
            string sucursal = "";
            string gerente = "";
            string fechaela = "";
            string grupo = "";
            string ngrupo = "";
            string nombre = "";
            string giron = "";
            decimal lunesr = 0;
            decimal martesr = 0;
            decimal miercolesr = 0;
            decimal juevesr = 0;
            decimal viernesr = 0;
            decimal sabador = 0;
            decimal domingor = 0;
            decimal totalsr = 0;
            decimal totalmr = 0;
            decimal matpri = 0;
            decimal mercancia = 0;
            decimal renta = 0;
            decimal luz = 0;
            decimal agua = 0;
            decimal gass = 0;
            decimal artpape = 0;
            decimal telefono = 0;
            decimal sueldoysal = 0;
            decimal transportefle = 0;
            decimal mantenimiento = 0;
            decimal pagoimpu = 0;
            decimal pagofin = 0;
            decimal otrasdeu = 0;
            decimal totalb = 0;
            decimal rentaa = 0;
            decimal luzz = 0;
            decimal aguaa = 0;
            decimal telefonoo = 0;
            decimal alimentacionn = 0;
            decimal vestidoo = 0;
            decimal gastosescoo = 0;
            decimal gastosmedii = 0;
            decimal trasnportee = 0;
            decimal deudass = 0;
            decimal mantenimientoo = 0;
            decimal pagoimpuu = 0;
            decimal otragas = 0;
            decimal totalc = 0;
            decimal dispp =0;
            decimal utitli = 0;
            decimal moncre = 0;
            decimal plazoo = 0;
            decimal pagsem = 0;
            decimal solvenc = 0;
            string Pro = "";
            string Imp = "";
            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                     sucursal = Convert.ToString( r[1]);
                     gerente = Convert.ToString(r[6]);
                     fechaela = Convert.ToString(r[7]);
                     grupo = r[8].ToString();
                     ngrupo = r[4].ToString();
                     nombre = r[9].ToString();
                     giron = r[10].ToString();
                     lunesr = Convert.ToDecimal(r[11]);
                     martesr = Convert.ToDecimal(r[12]);
                     miercolesr = Convert.ToDecimal(r[13]);
                     juevesr = Convert.ToDecimal(r[14]);
                     viernesr = Convert.ToDecimal(r[15]);
                     sabador = Convert.ToDecimal(r[16]);
                     domingor = Convert.ToDecimal(r[17]);
                     totalsr = Convert.ToDecimal(r[18]);
                     totalmr = Convert.ToDecimal(r[19]);
                     matpri = Convert.ToDecimal(r[20]);
                     mercancia = Convert.ToDecimal(r[21]);
                     renta = Convert.ToDecimal(r[22]);
                     luz = Convert.ToDecimal(r[23]);
                     agua = Convert.ToDecimal(r[24]);
                     gass = Convert.ToDecimal(r[25]);
                     artpape = Convert.ToDecimal(r[26]);
                     telefono = Convert.ToDecimal(r[27]);
                     sueldoysal = Convert.ToDecimal(r[28]);
                     transportefle = Convert.ToDecimal(r[29]);
                     mantenimiento = Convert.ToDecimal(r[30]);
                     pagoimpu = Convert.ToDecimal(r[31]);
                     pagofin = Convert.ToDecimal(r[32]);
                     otrasdeu = Convert.ToDecimal(r[33]);
                     totalb = Convert.ToDecimal(r[34]);
                     rentaa = Convert.ToDecimal(r[35]);
                     luzz = Convert.ToDecimal(r[36]);
                     aguaa = Convert.ToDecimal(r[37]);
                     telefonoo = Convert.ToDecimal(r[38]);
                     alimentacionn = Convert.ToDecimal(r[39]);
                     vestidoo = Convert.ToDecimal(r[40]);
                     gastosescoo = Convert.ToDecimal(r[41]);
                     gastosmedii = Convert.ToDecimal(r[42]);
                     trasnportee = Convert.ToDecimal(r[43]);
                     deudass = Convert.ToDecimal(r[44]);
                     mantenimientoo = Convert.ToDecimal(r[45]);
                     pagoimpuu = Convert.ToDecimal(r[46]);
                     otragas = Convert.ToDecimal(r[47]);
                     totalc = Convert.ToDecimal(r[48]);
                     dispp = Convert.ToDecimal(r[50]);
                     utitli = Convert.ToDecimal(r[49]);
                     moncre = Convert.ToDecimal(r[51]);
                     plazoo = Convert.ToDecimal(r[52]);
                     pagsem = Convert.ToDecimal(r[53]);
                     solvenc = Convert.ToDecimal(r[54]);
                    Pro = r[55].ToString();
                     Imp = r[55].ToString();
                }

                PdfPCell sucur = new PdfPCell(new Phrase("SUCURSAL", fuente6));

                    sucur.HorizontalAlignment = Element.ALIGN_CENTER;
                    sucur.BackgroundColor = BaseColor.LIGHT_GRAY;
                    enca.AddCell(sucur);

                    PdfPCell gere = new PdfPCell(new Phrase("GERENTE OPERATIVO", fuente6));

                    gere.HorizontalAlignment = Element.ALIGN_CENTER;
                    gere.BackgroundColor = BaseColor.LIGHT_GRAY;
                    enca.AddCell(gere);

                PdfPCell ciclo = new PdfPCell(new Phrase("CICLO", fuente6));

                ciclo.HorizontalAlignment = Element.ALIGN_CENTER;
                ciclo.BackgroundColor = BaseColor.LIGHT_GRAY;
                enca.AddCell(ciclo);

                PdfPCell feEl = new PdfPCell(new Phrase("FECHA DE ELABORACIÓN", fuente6));

                    feEl.HorizontalAlignment = Element.ALIGN_CENTER;
                    feEl.BackgroundColor = BaseColor.LIGHT_GRAY;
                    enca.AddCell(feEl);

                    PdfPCell sucur2 = new PdfPCell(new Phrase(" " + sucursal, fuente6));

                    sucur2.HorizontalAlignment = Element.ALIGN_CENTER;
                    enca.AddCell(sucur2);

                    PdfPCell gere2 = new PdfPCell(new Phrase(" " + gerente, fuente6));

                    gere2.HorizontalAlignment = Element.ALIGN_CENTER;
                    enca.AddCell(gere2);

                PdfPCell ciclo1 = new PdfPCell(new Phrase(" " , fuente6));

                ciclo1.HorizontalAlignment = Element.ALIGN_CENTER;
                enca.AddCell(ciclo1);

                PdfPCell feEl2 = new PdfPCell(new Phrase(" " + Convert.ToDateTime(fechaela).ToString("dd/MM/yyyy"), fuente6));

                    feEl2.HorizontalAlignment = Element.ALIGN_CENTER;
                    enca.AddCell(feEl2);
                    documento.Add(enca);

                    //SEGUNDA TABLA
                    PdfPTable enca2 = new PdfPTable(3);
                    enca2.WidthPercentage = 100f;
                    int[] enca2cellwidth = { 35, 20, 35 };
                    enca2.SetWidths(enca2cellwidth);


                    PdfPCell gruP = new PdfPCell(new Phrase("GRUPO PRODUCTIVO", fuente6));

                    gruP.HorizontalAlignment = Element.ALIGN_CENTER;
                    gruP.BackgroundColor = BaseColor.LIGHT_GRAY;
                    enca2.AddCell(gruP);

                    PdfPCell numG = new PdfPCell(new Phrase("No. DE GRUPO", fuente6));

                    numG.HorizontalAlignment = Element.ALIGN_CENTER;
                    numG.BackgroundColor = BaseColor.LIGHT_GRAY;
                    enca2.AddCell(numG);

                    PdfPCell nomC = new PdfPCell(new Phrase("NOMBRE DEL CLIENTE", fuente6));

                    nomC.HorizontalAlignment = Element.ALIGN_CENTER;
                    nomC.BackgroundColor = BaseColor.LIGHT_GRAY;
                    enca2.AddCell(nomC);

                    PdfPCell gruP2 = new PdfPCell(new Phrase(" " + grupo, fuente6));

                    gruP2.HorizontalAlignment = Element.ALIGN_CENTER;
                    enca2.AddCell(gruP2);

                    PdfPCell numG2 = new PdfPCell(new Phrase(" " + ngrupo, fuente6));

                    numG2.HorizontalAlignment = Element.ALIGN_CENTER;
                    enca2.AddCell(numG2);

                    PdfPCell nomC2 = new PdfPCell(new Phrase(" " + nombre, fuente6));

                    nomC2.HorizontalAlignment = Element.ALIGN_CENTER;
                    enca2.AddCell(nomC2);
                    documento.Add(enca2);

                    //tabla grupo de negocio
                    PdfPTable tabN = new PdfPTable(2);
                    tabN.WidthPercentage = 100f;
                    int[] tabNcellwidth = { 20, 80 };
                    tabN.SetWidths(tabNcellwidth);
                   

                    PdfPCell grupN = new PdfPCell(new Phrase("GIRO DEL NEGOCIO", fuente6));

                    grupN.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupN.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabN.AddCell(grupN);

                    PdfPCell grupN2 = new PdfPCell(new Phrase(" " + giron, fuente6));

                    grupN2.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabN.AddCell(grupN2);
                    documento.Add(tabN);
                    documento.Add(new Paragraph(" "));


                    //ventas mensuales
                    PdfPTable venM = new PdfPTable(2);
                    venM.WidthPercentage = 30f;
                    venM.HorizontalAlignment = Element.ALIGN_LEFT;
                  


                    PdfPCell ventaMen = (new PdfPCell(new Phrase("VENTAS MENSUALES (C)", fuente6)) { Colspan = 2 });
                    ventaMen.HorizontalAlignment = Element.ALIGN_CENTER;
                    ventaMen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    venM.AddCell(ventaMen);

                    PdfPCell dia = new PdfPCell(new Phrase("DÍA", fuente6));
                    dia.HorizontalAlignment = Element.ALIGN_CENTER;
                    dia.BackgroundColor = BaseColor.LIGHT_GRAY;
                    venM.AddCell(dia);

                    PdfPCell monto = new PdfPCell(new Phrase("MONTO ($)", fuente6));
                    monto.HorizontalAlignment = Element.ALIGN_CENTER;
                    monto.BackgroundColor = BaseColor.LIGHT_GRAY;
                    venM.AddCell(monto);

                    PdfPCell lunes = new PdfPCell(new Phrase("LUNES", fuente6));
                    lunes.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(lunes);

                    PdfPCell lunes2 = new PdfPCell(new Phrase(" " + lunesr.ToString("C2"), fuente6));
                    lunes2.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(lunes2);

                    PdfPCell martes = new PdfPCell(new Phrase("MARTES", fuente6));
                    martes.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(martes);

                    PdfPCell martes2 = new PdfPCell(new Phrase(" " + martesr.ToString("C2"), fuente6));
                    martes2.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(martes2);

                    PdfPCell mierco = new PdfPCell(new Phrase("MIÉRCOLES", fuente6));
                    mierco.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(mierco);

                    PdfPCell mierco2 = new PdfPCell(new Phrase(" " + miercolesr.ToString("C2"), fuente6));
                    mierco2.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(mierco2);

                    PdfPCell jueves = new PdfPCell(new Phrase("JUEVES", fuente6));
                    jueves.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(jueves);

                    PdfPCell jueves2 = new PdfPCell(new Phrase(" " + juevesr.ToString("C2"), fuente6));
                    jueves2.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(jueves2);

                    PdfPCell viernes = new PdfPCell(new Phrase("VIERNES", fuente6));
                    viernes.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(viernes);

                    PdfPCell viernes2 = new PdfPCell(new Phrase(" " + viernesr.ToString("C2"), fuente6));
                    viernes2.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(viernes2);

                    PdfPCell sabado = new PdfPCell(new Phrase("SÁBADO", fuente6));
                    sabado.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(sabado);

                    PdfPCell sabado2 = new PdfPCell(new Phrase(" " + sabador.ToString("C2"), fuente6));
                    sabado2.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(sabado2);

                    PdfPCell domingo = new PdfPCell(new Phrase("DOMINGO", fuente6));
                    domingo.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(domingo);

                    PdfPCell domingo2 = new PdfPCell(new Phrase(" " + domingor.ToString("C2"), fuente6));
                    domingo2.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(domingo2);

                    PdfPCell toSe = new PdfPCell(new Phrase("TOTAL SEMANAL", fuente6));
                    toSe.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(toSe);

                    PdfPCell toSe2 = new PdfPCell(new Phrase(" " + totalsr.ToString("C2"), fuente6));
                    toSe2.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(toSe2);

                    PdfPCell toMe = new PdfPCell(new Phrase("TOTAL MENSUAL(C)", fuente6));
                    toMe.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(toMe);

                    PdfPCell toMe2 = new PdfPCell(new Phrase(" " + totalmr.ToString("C2"), fuente6));
                    toMe2.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM.AddCell(toMe2);

                    PdfPCell relleno = new PdfPCell(new Phrase(" ", fuente6));
                    relleno.HorizontalAlignment = Element.ALIGN_CENTER;
                relleno.BorderWidth = 0;
                relleno.BorderWidthRight = 0;
                relleno.BorderWidthLeft = 0;
                relleno.BorderWidthBottom = 0;
                relleno.BorderWidthTop = 0;
                    venM.AddCell(relleno);

                PdfPCell relleno1 = new PdfPCell(new Phrase(" ", fuente6));
                relleno1.HorizontalAlignment = Element.ALIGN_CENTER;
                relleno1.BorderWidth = 0;
                venM.AddCell(relleno1);

                PdfPCell otIng = (new PdfPCell(new Phrase("OTROS INGRESOS (D)", fuente6)) { Colspan=2 });
                otIng.HorizontalAlignment = Element.ALIGN_CENTER;
                otIng.BackgroundColor = BaseColor.LIGHT_GRAY;
                venM.AddCell(otIng);

                PdfPCell descOI = (new PdfPCell(new Phrase("DESCRIPCIÓN", fuente6)));
                descOI.HorizontalAlignment = Element.ALIGN_CENTER;
                descOI.BackgroundColor = BaseColor.LIGHT_GRAY;
                venM.AddCell(descOI);

                PdfPCell montOI = (new PdfPCell(new Phrase("MONTO($)", fuente6)));
                montOI.HorizontalAlignment = Element.ALIGN_CENTER;
                montOI.BackgroundColor = BaseColor.LIGHT_GRAY;
                venM.AddCell(montOI);

                PdfPCell renOI = (new PdfPCell(new Phrase("RENTA", fuente6)));
                renOI.HorizontalAlignment = Element.ALIGN_CENTER;
                venM.AddCell(renOI);

                PdfPCell renOI1 = (new PdfPCell(new Phrase(" ", fuente6)));
                renOI1.HorizontalAlignment = Element.ALIGN_CENTER;
                venM.AddCell(renOI1);

                PdfPCell remeOI = (new PdfPCell(new Phrase("REMESAS", fuente6)));
                remeOI.HorizontalAlignment = Element.ALIGN_CENTER;
                venM.AddCell(remeOI);

                PdfPCell remeOI1 = (new PdfPCell(new Phrase(" ", fuente6)));
                remeOI1.HorizontalAlignment = Element.ALIGN_CENTER;
                venM.AddCell(remeOI1);

                PdfPCell aportOI = (new PdfPCell(new Phrase("APORTACIONES FAMILIARES", fuente6)));
                aportOI.HorizontalAlignment = Element.ALIGN_CENTER;
                venM.AddCell(aportOI);

                PdfPCell aportOI1 = (new PdfPCell(new Phrase(" ", fuente6)));
                aportOI1.HorizontalAlignment = Element.ALIGN_CENTER;
                venM.AddCell(aportOI1);

                PdfPCell totalOI = (new PdfPCell(new Phrase("TOTAL(D)", fuente6)));
                totalOI.HorizontalAlignment = Element.ALIGN_CENTER;
                venM.AddCell(totalOI);

                PdfPCell totalOI1 = (new PdfPCell(new Phrase(" ", fuente6)));
                totalOI1.HorizontalAlignment = Element.ALIGN_CENTER;
                venM.AddCell(totalOI1);



                // tabla 2
                PdfPTable venM1 = new PdfPTable(2);
                    venM1.WidthPercentage = 30f;
                    venM1.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell ventaMen1 = (new PdfPCell(new Phrase("COSTOS Y GASTOS MENSUALES DEL NEGOCIO (B)", fuente6)) { Colspan = 2 });
                    ventaMen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    ventaMen1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    venM1.AddCell(ventaMen1);

                    PdfPCell concep = new PdfPCell(new Phrase("CONCEPTO", fuente6));
                    concep.HorizontalAlignment = Element.ALIGN_CENTER;
                    concep.BackgroundColor = BaseColor.LIGHT_GRAY;
                    venM1.AddCell(concep);

                    PdfPCell monto1 = new PdfPCell(new Phrase("MONTO($)", fuente6));
                    monto1.HorizontalAlignment = Element.ALIGN_CENTER;
                    monto1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    venM1.AddCell(monto1);

                

                    PdfPCell mateP = new PdfPCell(new Phrase("MATERIAS PRIMAS", fuente6));
                    mateP.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(mateP);

                    PdfPCell mateP1 = new PdfPCell(new Phrase(" " + matpri.ToString("C2"), fuente6));
                    mateP1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(mateP1);

                    PdfPCell merca = new PdfPCell(new Phrase("MERCANCIAS", fuente6));
                    merca.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(merca);

                    PdfPCell merca1 = new PdfPCell(new Phrase(" " + mercancia.ToString("C2"), fuente6));
                    merca1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(merca1);

                    PdfPCell rentaC = new PdfPCell(new Phrase("RENTA", fuente6));
                    rentaC.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(rentaC);

                    PdfPCell rentaC1 = new PdfPCell(new Phrase(" " + renta.ToString("C2"), fuente6));
                    rentaC1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(rentaC1);

                    PdfPCell luzC = new PdfPCell(new Phrase("LUZ", fuente6));
                    luzC.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(luzC);

                    PdfPCell luzC1 = new PdfPCell(new Phrase(" " + luz.ToString("C2"), fuente6));
                    luzC1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(luzC1);

                    PdfPCell aguaC = new PdfPCell(new Phrase("AGUA", fuente6));
                    aguaC.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(aguaC);

                    PdfPCell aguaC1 = new PdfPCell(new Phrase(" " + agua.ToString("C2"), fuente6));
                    aguaC1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(aguaC1);

                    PdfPCell gas = new PdfPCell(new Phrase("GAS", fuente6));
                    gas.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(gas);

                    PdfPCell gas1 = new PdfPCell(new Phrase(" " + gass.ToString("C2"), fuente6));
                    gas1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(gas1);

                    PdfPCell pap = new PdfPCell(new Phrase("ART. DE PAPELERIA", fuente6));
                    pap.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(pap);

                    PdfPCell pap1 = new PdfPCell(new Phrase(" " + artpape.ToString("C2"), fuente6));
                    pap1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(pap1);

                    PdfPCell telC = new PdfPCell(new Phrase("TELÉFONO", fuente6));
                    telC.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(telC);

                    PdfPCell telC1 = new PdfPCell(new Phrase(" " + telefono.ToString("C2"), fuente6));
                    telC1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(telC1);

                    PdfPCell sueld = new PdfPCell(new Phrase("SUELDO Y SALARIOS", fuente6));
                    sueld.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(sueld);

                    PdfPCell sueld1 = new PdfPCell(new Phrase(" " + sueldoysal.ToString("C2"), fuente6));
                    sueld1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(sueld1);

                    PdfPCell flet = new PdfPCell(new Phrase("TRANSPORTE/FLETES", fuente6));
                    flet.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(flet);

                    PdfPCell flet1 = new PdfPCell(new Phrase(" " + transportefle.ToString("C2"), fuente6));
                    flet1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(flet1);

                    PdfPCell mantC = new PdfPCell(new Phrase("MANTENIMIENTO", fuente6));
                    mantC.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(mantC);

                    PdfPCell mantC1 = new PdfPCell(new Phrase(" " + mantenimiento.ToString("C2"), fuente6));
                    mantC1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(mantC1);

                    PdfPCell pagIC = new PdfPCell(new Phrase("PAGO IMPUESTOS", fuente6));
                    pagIC.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(pagIC);

                    PdfPCell pagIC1 = new PdfPCell(new Phrase(" " + pagoimpu.ToString("C2"), fuente6));
                    pagIC1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(pagIC1);

                    PdfPCell pagFC = new PdfPCell(new Phrase("PAGO FINANCIERAS", fuente6));
                    pagFC.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(pagFC);

                    PdfPCell pagFC1 = new PdfPCell(new Phrase(" " + pagofin.ToString("C2"), fuente6));
                    pagFC1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(pagFC1);

                    PdfPCell otherC = new PdfPCell(new Phrase("OTRAS DEUDAS", fuente6));
                    otherC.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(otherC);

                    PdfPCell otherC1 = new PdfPCell(new Phrase(" " + otrasdeu.ToString("C2"), fuente6));
                    otherC1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(otherC1);

                    PdfPCell totalC = new PdfPCell(new Phrase("TOTAL(B)", fuente6));
                    totalC.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(totalC);

                    PdfPCell totalC1 = new PdfPCell(new Phrase(" " + totalb.ToString("C2"), fuente6));
                    totalC1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM1.AddCell(totalC1);

                PdfPCell spacer = new PdfPCell(new Phrase(" ", fuente6));
                spacer.HorizontalAlignment = Element.ALIGN_CENTER;
                spacer.BorderWidth = 0;
                venM1.AddCell(spacer);

                PdfPCell spacer1 = new PdfPCell(new Phrase(" ", fuente6));
                spacer1.HorizontalAlignment = Element.ALIGN_CENTER;
                spacer1.BorderWidth = 0;
                venM1.AddCell(spacer1);




                //prueba 2
                PdfPTable venM2 = new PdfPTable(2);
                    venM2.WidthPercentage = 30f;
                    venM2.HorizontalAlignment = Element.ALIGN_RIGHT;

                    PdfPCell ventaMen2 = (new PdfPCell(new Phrase("GASTOS MENSUALES DEL HOGAR (RECURSOS APORTADOS DIRECTAMENTE POR EL CLIENTE)(A)", fuente6)) { Colspan = 2 });
                    ventaMen2.HorizontalAlignment = Element.ALIGN_CENTER;
                    ventaMen2.BackgroundColor = BaseColor.LIGHT_GRAY;
                    venM2.AddCell(ventaMen2);

                    PdfPCell dia2 = new PdfPCell(new Phrase("CONCEPTO", fuente6));
                    dia2.HorizontalAlignment = Element.ALIGN_CENTER;
                    dia2.BackgroundColor = BaseColor.LIGHT_GRAY;
                    venM2.AddCell(dia2);

                    PdfPCell monto2 = new PdfPCell(new Phrase("MONTO($)", fuente6));
                    monto2.HorizontalAlignment = Element.ALIGN_CENTER;
                    monto2.BackgroundColor = BaseColor.LIGHT_GRAY;
                    venM2.AddCell(monto2);

              

                    PdfPCell rentaG = new PdfPCell(new Phrase("RENTA", fuente6));
                    rentaG.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(rentaG);

                    PdfPCell rentaG1 = new PdfPCell(new Phrase(" " + rentaa.ToString("C2"), fuente6));
                    rentaG1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(rentaG1);

                    PdfPCell luzG = new PdfPCell(new Phrase("LUZ", fuente6));
                    luzG.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(luzG);

                    PdfPCell luzG1 = new PdfPCell(new Phrase(" " + luzz.ToString("C2"), fuente6));
                    luzG1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(luzG1);

                    PdfPCell aguaG = new PdfPCell(new Phrase("AGUA", fuente6));
                    aguaG.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(aguaG);

                    PdfPCell aguaG1 = new PdfPCell(new Phrase(" " + aguaa.ToString("C2"), fuente6));
                    aguaG1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(aguaG1);

                    PdfPCell teleG = new PdfPCell(new Phrase("TELÉFONO", fuente6));
                    teleG.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(teleG);

                    PdfPCell teleG1 = new PdfPCell(new Phrase(" " + telefonoo.ToString("C2"), fuente6));
                    teleG1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(teleG1);

                    PdfPCell alimG = new PdfPCell(new Phrase("ALIMENTACIÓN", fuente6));
                    alimG.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(alimG);

                    PdfPCell alimG1 = new PdfPCell(new Phrase(" " + alimentacionn.ToString("C2"), fuente6));
                    alimG1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(alimG1);

                    PdfPCell vestido = new PdfPCell(new Phrase("VESTIDO", fuente6));
                    vestido.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(vestido);

                    PdfPCell vestido1 = new PdfPCell(new Phrase(" " + vestidoo.ToString("C2"), fuente6));
                    vestido1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(vestido1);

                    PdfPCell gastE = new PdfPCell(new Phrase("GASTOS ESCOLARES", fuente6));
                    gastE.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(gastE);

                    PdfPCell gastE1 = new PdfPCell(new Phrase(" " + gastosescoo.ToString("C2"), fuente6));
                    gastE1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(gastE1);

                    PdfPCell gastM = new PdfPCell(new Phrase("GASTOS MÉDICOS", fuente6));
                    gastM.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(gastM);

                    PdfPCell gastM1 = new PdfPCell(new Phrase(" " + gastosmedii.ToString("C2"), fuente6));
                    gastM1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(gastM1);

                    PdfPCell trasn = new PdfPCell(new Phrase("TRANSPORTE", fuente6));
                    trasn.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(trasn);

                    PdfPCell trasn1 = new PdfPCell(new Phrase(" " + trasnportee.ToString("C2"), fuente6));
                    trasn1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(trasn1);

                    PdfPCell deudasG = new PdfPCell(new Phrase("DEUDAS", fuente6));
                    deudasG.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(deudasG);

                    PdfPCell deudasG1 = new PdfPCell(new Phrase(" " + deudass.ToString("C2"), fuente6));
                    deudasG1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(deudasG1);

                    PdfPCell mantG = new PdfPCell(new Phrase("MANTENIMIENTO", fuente6));
                    mantG.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(mantG);

                    PdfPCell mantG1 = new PdfPCell(new Phrase(" " + mantenimientoo.ToString("C2"), fuente6));
                    mantG1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(mantG1);

                    PdfPCell pagIG = new PdfPCell(new Phrase("PAGOS IMPUESTOS", fuente6));
                    pagIG.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(pagIG);

                    PdfPCell pagIG1 = new PdfPCell(new Phrase(" " + pagoimpuu.ToString("C2"), fuente6));
                    pagIG1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(pagIG1);

                    PdfPCell otherGG = new PdfPCell(new Phrase("OTROS GASTOS", fuente6));
                    otherGG.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(otherGG);

                    PdfPCell otherGG1 = new PdfPCell(new Phrase(" " + otragas.ToString("C2"), fuente6));
                    otherGG1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(otherGG1);

                    PdfPCell totalGasC = new PdfPCell(new Phrase("TOTAL(A)", fuente6));
                    totalGasC.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(totalGasC);

                    PdfPCell totalGasC1 = new PdfPCell(new Phrase(" " + totalc.ToString("C2"), fuente6));
                    totalGasC1.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(totalGasC1);

                PdfPCell space = new PdfPCell(new Phrase(" ", fuente6));
                space.HorizontalAlignment = Element.ALIGN_CENTER;
                space.BorderWidth = 0;
                venM2.AddCell(space);

                PdfPCell space1 = new PdfPCell(new Phrase(" ", fuente6));
                space1.HorizontalAlignment = Element.ALIGN_CENTER;
                space1.BorderWidth = 0;
                venM2.AddCell(space1);


                //celda de relleno
                PdfPCell rellen = new PdfPCell(new Phrase(" ", fuente6));
                    rellen.HorizontalAlignment = Element.ALIGN_CENTER;
                    venM2.AddCell(rellen);

                    documento.Add(new Paragraph(" "));
                    // tabla principal

                    PdfPTable ventaMensual = new PdfPTable(3);
                    ventaMensual.WidthPercentage = 100f;
                    int[] ventaMensualcellwidth = { 30, 30, 30 };
                    ventaMensual.SetWidths(ventaMensualcellwidth);

                PdfPCell ventaC = new PdfPCell(venM2);
                ventaC.BorderWidth = 0;
                    ventaMensual.AddCell(ventaC);

                PdfPCell ventaB = new PdfPCell(venM1);
                ventaB.BorderWidth = 0;
                ventaMensual.AddCell(ventaB);

                PdfPCell ventaA = new PdfPCell(venM);
                ventaA.BorderWidth = 0;
                    ventaMensual.AddCell(ventaA);

                
                    
                    documento.Add(ventaMensual);
                    documento.Add(new Paragraph(" "));

                    //TABLA DE DISPONIBILIDAD DE NEGOCIO
                    PdfPTable nego = new PdfPTable(7);
                    nego.WidthPercentage = 100f;
                    int[] negocellwidth = { 35, 10, 10,10, 8, 11, 11 };
                    nego.SetWidths(negocellwidth);

                   


                    PdfPCell dispo = (new PdfPCell(new Phrase("DISPONIBILIDAD SEMANAL DEL NEGOCIO", fuente6)) { Colspan = 2, Rowspan = 2 });
                    dispo.VerticalAlignment = Element.ALIGN_MIDDLE;
                    dispo.HorizontalAlignment = Element.ALIGN_CENTER;
                    dispo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    nego.AddCell(dispo);

                    PdfPCell solv = (new PdfPCell(new Phrase("SOLVENCIA PARA EL PAGO DE CRÉDITO", fuente6)) { Colspan = 5 });
                    solv.HorizontalAlignment = Element.ALIGN_CENTER;
                    solv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    nego.AddCell(solv);

                    PdfPCell montCre = new PdfPCell(new Phrase("MONTO DEL CRÉDITO", fuente6));
                    montCre.HorizontalAlignment = Element.ALIGN_CENTER;
                    montCre.BackgroundColor = BaseColor.LIGHT_GRAY;
                    nego.AddCell(montCre);

                PdfPCell tasa = new PdfPCell(new Phrase("TASA", fuente6));
                tasa.HorizontalAlignment = Element.ALIGN_CENTER;
                tasa.BackgroundColor = BaseColor.LIGHT_GRAY;
                nego.AddCell(tasa);

                PdfPCell plazSe = new PdfPCell(new Phrase("PLAZO (SEM)", fuente6));
                    plazSe.HorizontalAlignment = Element.ALIGN_CENTER;
                    plazSe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    nego.AddCell(plazSe);

                    PdfPCell pagSema = new PdfPCell(new Phrase("PAGO SEMANAL = (G)", fuente6));
                    pagSema.HorizontalAlignment = Element.ALIGN_CENTER;
                    pagSema.BackgroundColor = BaseColor.LIGHT_GRAY;
                    nego.AddCell(pagSema);

                    PdfPCell solven = new PdfPCell(new Phrase("SOLVENCIA (H) = (G)-(F)", fuente6));
                    solven.HorizontalAlignment = Element.ALIGN_CENTER;
                    solven.BackgroundColor = BaseColor.LIGHT_GRAY;
                    nego.AddCell(solven);

                    PdfPCell unid = new PdfPCell(new Phrase("UTILIDAD (E)= (C+D)-(A)-(B)", fuente6));
                    unid.HorizontalAlignment = Element.ALIGN_CENTER;
                    nego.AddCell(unid);

                    PdfPCell unid1 = new PdfPCell(new Phrase(" " + utitli.ToString("C2"), fuente6));
                    unid1.HorizontalAlignment = Element.ALIGN_CENTER;
                    nego.AddCell(unid1);

                    PdfPCell montCre1 = (new PdfPCell(new Phrase(" " + moncre.ToString("C2"), fuente6)) { Rowspan = 2 });
                    montCre1.HorizontalAlignment = Element.ALIGN_CENTER;
                    nego.AddCell(montCre1);

                PdfPCell tasa1 = (new PdfPCell(new Phrase(" ", fuente6)) { Rowspan = 2 });
                tasa1.HorizontalAlignment = Element.ALIGN_CENTER;
                nego.AddCell(tasa1);

                PdfPCell plazSe1 = (new PdfPCell(new Phrase(" " + plazoo, fuente6)) { Rowspan = 2 });
                    plazSe1.HorizontalAlignment = Element.ALIGN_CENTER;
                    nego.AddCell(plazSe1);

                    PdfPCell pagoSema1 = (new PdfPCell(new Phrase(" " + pagsem.ToString("C2"), fuente6)) { Rowspan = 2 });
                    pagoSema1.HorizontalAlignment = Element.ALIGN_CENTER;
                    nego.AddCell(pagoSema1);

                    PdfPCell solven1 = (new PdfPCell(new Phrase(" " + solvenc.ToString("C2"), fuente6)) { Rowspan = 2 });
                    solven1.HorizontalAlignment = Element.ALIGN_CENTER;
                    nego.AddCell(solven1);

                    PdfPCell dispSem = new PdfPCell(new Phrase("DISPONIBILIDAD SEMANAL (F)=(E/4*.70)", fuente6));
                    dispSem.HorizontalAlignment = Element.ALIGN_CENTER;
                    nego.AddCell(dispSem);

                    PdfPCell dispSem1 = new PdfPCell(new Phrase(" " + dispp.ToString("C2"), fuente6));
                    dispSem1.HorizontalAlignment = Element.ALIGN_CENTER;
                    nego.AddCell(dispSem1);
                    documento.Add(nego);
                    documento.Add(new Paragraph(" "));

                    // las tres tablas finales
                    PdfPTable dict = new PdfPTable(1);
                    dict.WidthPercentage = 20f;
                    dict.HorizontalAlignment = Element.ALIGN_LEFT;

                    PdfPCell dicta = new PdfPCell(new Phrase("DICTAMEN FINAL (Marque con una X)", fuente6));
                    dicta.HorizontalAlignment = Element.ALIGN_CENTER;
                    dicta.BackgroundColor = BaseColor.LIGHT_GRAY;
                    dict.AddCell(dicta);
                   

                    if (Pro == "Pro")
                    {
                        Pro = "X";
                    }
                    else { Pro = " "; }

                    if (Imp == "Imp")
                    {
                        Imp = "X";
                    }
                    else
                    {
                        Imp = " ";
                    }

                    //segunda tabla

                    PdfPTable proce = new PdfPTable(2);
                    proce.WidthPercentage = 20f;
                    int[] procecellwidth = { 11, 9 };
                    proce.SetWidths(procecellwidth);
                    proce.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell procede = new PdfPCell(new Phrase("PROCEDENTE", fuente6));
                    procede.HorizontalAlignment = Element.ALIGN_CENTER;
                    procede.BackgroundColor = BaseColor.LIGHT_GRAY;
                    proce.AddCell(procede);

                    PdfPCell procede1 = new PdfPCell(new Phrase(" " + Pro, fuente6));
                    procede1.HorizontalAlignment = Element.ALIGN_CENTER;
                    proce.AddCell(procede1);


                    //TERCERA TABLA
                    PdfPTable improce = new PdfPTable(2);
                    improce.WidthPercentage = 20f;
                    int[] improcecellwidth = { 13, 7 };
                    improce.SetWidths(improcecellwidth);
                    improce.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell improcede = new PdfPCell(new Phrase("IMPROCEDENTE", fuente6));
                    improcede.HorizontalAlignment = Element.ALIGN_CENTER;
                    improcede.BackgroundColor = BaseColor.LIGHT_GRAY;
                    improce.AddCell(improcede);

                    PdfPCell improcede1 = new PdfPCell(new Phrase("  " + Imp, fuente6));
                    improcede1.HorizontalAlignment = Element.ALIGN_CENTER;
                    improce.AddCell(improcede1);



                    //tabla principal 2
                    PdfPTable procedente = new PdfPTable(3);
                    procedente.WidthPercentage = 100f;
                    int[] procedentecellwidth = { 20, 20, 20 };
                    procedente.SetWidths(procedentecellwidth);
                    procedente.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell dicA = new PdfPCell(dict);
                    procedente.AddCell(dicA);
                    PdfPCell procedA = new PdfPCell(proce);
                    procedente.AddCell(procedA);
                    PdfPCell improA = new PdfPCell(improce);
                    procedente.AddCell(improA);
                    //documento.Add(procedente);
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));

                    //comentarios adicionales
                    PdfPTable coment = new PdfPTable(1);
                    coment.WidthPercentage = 100f;
                    coment.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell comenta = (new PdfPCell(new Phrase(" \n \n OBSERVACIONES \n \n \n \n \n \n \n ", fuente6)) { Rowspan = 5 });
                    comenta.HorizontalAlignment = Element.ALIGN_CENTER;
                comenta.BorderColor = BaseColor.BLUE;
                    coment.AddCell(comenta);
                    documento.Add(coment);

                documento.Add(new Paragraph(" "));

                documento.Add(new Paragraph(" "));

                documento.Add(new Paragraph(" "));

                documento.Add(new Paragraph(" "));




                //tablla de firmas
                PdfPTable firmas = new PdfPTable(2);
                    firmas.WidthPercentage = 90f;
                    firmas.HorizontalAlignment = Element.ALIGN_CENTER;

                    

                    PdfPCell firmaSub1 = new PdfPCell(new Phrase("\n \n \n ______________________________________________ \n \n NOMBRE Y FIRMA DEL CLIENTE", fuente6));
                    firmaSub1.BorderColor = BaseColor.BLUE;
                    firmaSub1.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmas.AddCell(firmaSub1);

                    PdfPCell firmaSub2 = new PdfPCell(new Phrase("\n \n \n ______________________________________________ \n \n NOMBRE Y FIRMA DEL ", fuente6));
                    firmaSub2.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmaSub2.BorderColor = BaseColor.BLUE;
                    firmas.AddCell(firmaSub2);

                    
                    documento.Add(firmas);

                    //politicas
                    /*PdfPTable politicas = new PdfPTable(1);
                    politicas.WidthPercentage = 80f;
                    politicas.HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL;

                    PdfPCell texto = new PdfPCell(new Phrase("Declaro bajo protesta de decir verdad que la información asentada y los documentos proporcionados para esta solicitud son verdaderos y correctos, así mismo me encuentro voluntariamente enterado del contenido del aviso de privasidad de ASESORIA Y SERVICIO RURAL Y URBANO S.A. de C.V. SOFOM ENR y sus alcances legales con fundamento en lo dispuesto por la Ley Federal de Proteccion de Datos Personale en posesión de los particulares y su reglamento, para lo cual otorgo de manera voluntaria el más amplio consentimiento y facultad a la empresa ASESORIA Y SERVICIO RURAL Y URBANO S.A. de C.V. SOFOM ENR a utilizar mis datos personale. ASESORIA Y SERVICIO RURAL Y URBANO S.A. de C.V. SOFOM ENR se reserva el derecho de cambiar, modificar, complementar y/o alterar el presente aviso, en cualquier momento, cuyo caso se hará de su conocimiento a través de los medios que establezca la legislación en la materia.", fuente2));
                    texto.Border = 0;
                    texto.HorizontalAlignment = Element.ALIGN_CENTER;
                    politicas.AddCell(texto);

                    documento.Add(politicas);*/









                    documento.Close();

             
            }

            FileInfo filename = new FileInfo(archivo);
            if (filename.Exists)
            {
                string url = "Descargas.aspx?filename=" + filename.Name;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "pdfs", "window.open('" + url + "', 'nuevo', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, titlebar=no, fullscreen=yes,resizable=yes');", true);
            }
        }
    }

 

    protected void cmb_nombre_SelectedIndexChanged(object sender, EventArgs e)
    {

        AnaPag agregar = new AnaPag();
        int[] sesiones = obtieneSesiones();
        agregar.empresa = sesiones[2];
        agregar.sucursal = sesiones[3];
        int id_cliente = Convert.ToInt32(cmb_nombre.SelectedValue);

        agregar.id_cliente = id_cliente;

        agregar.obtienepagosem();
        if (Convert.ToBoolean(agregar.retorno[0]))
        {
            DataSet ds = (DataSet)agregar.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txt_mon_cred.Text = Convert.ToString(r[0]);
                txt_plazo.Text = Convert.ToString(r[1]);
                decimal pago = 0;
                decimal monto = 0;
                decimal plazo = 0;

                monto = Convert.ToDecimal(txt_mon_cred.Text);
                plazo = Convert.ToDecimal(txt_plazo.Text);

                pago = monto / plazo * Convert.ToDecimal(1.16);
                txt_pagsem.Text = Convert.ToString(pago);
            }
        }
    }

    protected void cmb_sucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        int grupo = Convert.ToInt32(cmb_sucursal.SelectedValue);
        cmbNombre.SelectCommand = "select 0 as id_cliente,'Selecione Integrante'as nombre_completo union all  select c.id_cliente,c.nombre_completo from AN_Clientes c inner join AN_Acta_Integraciondetalle d on c.id_cliente = d.id_cliente where d.id_sucursal=" + sucursal+" and d.id_empresa="+empresa+" and d.id_acta="+grupo+" and d.id_cliente  not in (select id_cliente from AN_Analisis_Pago where id_empresa="+empresa+" and id_sucursal="+sucursal+")";
        cmbNombre.DataBind();
        txt_numero.Text = Convert.ToString(grupo);
    }

    protected void txt_domingo_TextChanged(object sender, EventArgs e)
    {
        int lunes = Convert.ToInt32(txt_l.Text);
        int martes = Convert.ToInt32(txt_martes.Text);
        int miercoles = Convert.ToInt32(txt_miercoles.Text);
        int jueves = Convert.ToInt32(txt_jueves.Text);
        int viernes = Convert.ToInt32(txt_viernes.Text);
        int sabado = Convert.ToInt32(txt_sabado.Text);
        int domingo = Convert.ToInt32(txt_domingo.Text);
        int semanal = lunes + martes + miercoles + jueves + viernes + sabado + domingo;
        txt_totals.Text = semanal.ToString();
        int mensual = semanal * 4;
        txt_totalm.Text = mensual.ToString(); 

    }

    protected void txt_odeu_TextChanged(object sender, EventArgs e)
    {
        int mat = Convert.ToInt32(txt_matp.Text);
        int merca = Convert.ToInt32(txt_mercancias.Text);
        int renta = Convert.ToInt32(txt_renta.Text);
        int luz = Convert.ToInt32(txt_luz.Text);
        int agua = Convert.ToInt32(txt_agua.Text);
        int gas = Convert.ToInt32(txt_gas.Text);
        int pape = Convert.ToInt32(txt_artpape.Text);
        int telefono = Convert.ToInt32(txt_telefono.Text);
        int sueldos = Convert.ToInt32(txt_sueldos.Text);
        int trans = Convert.ToInt32(txt_tranfle.Text);
        int mante = Convert.ToInt32(txt_mantenimiento.Text);
        int impuestos = Convert.ToInt32(txt_pagimp.Text);
        int fin = Convert.ToInt32(txt_pagfin.Text);
        int odeu = Convert.ToInt32(txt_odeu.Text);

        int total = mat + merca + renta + luz + agua + gas + pape + telefono + sueldos + trans + mante + impuestos + fin + odeu;

        txt_totalb.Text = total.ToString();

    }

    protected void txt_otrosg_TextChanged(object sender, EventArgs e)
    {
        int renta = Convert.ToInt32(txt_ren_cli.Text);
        int luz = Convert.ToInt32(txt_luz_cli.Text);
        int agua = Convert.ToInt32(txt_agua_cli.Text);
        int tel = Convert.ToInt32(txt_tel_cli.Text);
        int ali = Convert.ToInt32(txt_alimentacion.Text);
        int vestido = Convert.ToInt32(txt_vestido.Text);
        int escuela = Convert.ToInt32(txt_gastos_esc.Text);
        int medico = Convert.ToInt32(txt_gastos_med.Text);
        int transporte = Convert.ToInt32(txt_trasnporte_cli.Text);
        int deudas = Convert.ToInt32(txt_deudas.Text);
        int mante = Convert.ToInt32(txt_mante.Text);
        int impuestos = Convert.ToInt32(txt_pag_imp.Text);
        int odeu = Convert.ToInt32(txt_otrosg.Text);

        int total = renta + luz + agua + tel + ali + vestido + escuela + medico + transporte + deudas + mante + impuestos + odeu;
        txt_totalc.Text = total.ToString();


        int a = Convert.ToInt32( txt_totalm.Text);
        int b = Convert.ToInt32(txt_totalb.Text);
        int c = Convert.ToInt32(txt_totalc.Text);

        int utilidad = a - b - c;

        txt_utilidad.Text = utilidad.ToString();

        decimal disp = utilidad / 4 * Convert.ToDecimal(.70);
        txt_dissem.Text = disp.ToString();
        decimal solvencia = disp - Convert.ToDecimal( txt_pagsem.Text);
        txt_solvencia.Text = solvencia.ToString();


    }
} 