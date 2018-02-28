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
using System.Windows.Forms;


public partial class EvaluacionGrupal : System.Web.UI.Page
{

    Recepciones recepciones = new Recepciones();
    DatosOrdenes datosOrdenes = new DatosOrdenes();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtfecha_eval.MaxDate = DateTime.Now;
    }
    protected void lnkAbreWindow_Click(object sender, EventArgs e)
    {
        lbleval.Text = "Agrega Evaluacion";
        pnlMask.Visible = true;
        windowAutorizacion.Visible = true;
        obtieneSesiones();
        int[] sesiones = new int[4];
        sesiones[0] = Convert.ToInt32(Request.QueryString["u"]);
        sesiones[1] = Convert.ToInt32(Request.QueryString["p"]);
        sesiones[2] = Convert.ToInt32(Request.QueryString["e"]);
        sesiones[3] = Convert.ToInt32(Request.QueryString["t"]);
        txt_suc.Text = Convert.ToString( sesiones[2]);
        txt_num.Text= Convert.ToString(sesiones[3]);

        

    }
    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lnkAbreEdicion.Visible = true;
        lnkImprimir.Visible = true;
       
       
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
    protected void lnkAgregaSolicitud_Click(object sender, EventArgs e)
    {

        int[] sesiones = obtieneSesiones();
        EvalGru agrega = new EvalGru();
        agrega.empresa = sesiones[2];
        agrega.sucursal = sesiones[3];
        DateTime fecha_eval = Convert.ToDateTime(txtfecha_eval.SelectedDate);
        agrega.fecha_eval = fecha_eval.ToString("yyyy/MM/dd");
        agrega.cicli_eval = Convert.ToInt32(txt_ciclo.Text);
        agrega.gerente_sucursal_eval = Convert.ToInt32( ddlgerente.SelectedValue);
        agrega.asesor_eval = Convert.ToInt32(ddlAsesor.SelectedValue) ;
        agrega.id_grupo = Convert.ToInt32( cmb_sucursal.SelectedValue);
        agrega.grupo_productivo_eval = cmb_sucursal.SelectedItem.Text;
        agrega.preg1_evalgn =Convert.ToInt32( txt1_gn.Text);
        int preg1 = Convert.ToInt32(txt1_gn.Text);
        agrega.preg2_evalgn = Convert.ToInt32(txt2_gn.Text);
        int preg2 = Convert.ToInt32(txt2_gn.Text);
        agrega.preg3_evalgn = Convert.ToInt32(txt3_gn.Text);
        int preg3 = Convert.ToInt32(txt3_gn.Text);
        agrega.preg4_evalgn = Convert.ToInt32(txt4_gn.Text);
        int preg4 = Convert.ToInt32(txt4_gn.Text);
        agrega.preg5_evalgn = Convert.ToInt32(txt5_gn.Text);
        int preg5 = Convert.ToInt32(txt5_gn.Text);
        int total = preg1 + preg2 + preg3 + preg4 + preg5;
        agrega.total_evalgn = total;
        agrega.preg1_evalgr = Convert.ToInt32(txt1_ga.Text);
        int preggr1 = Convert.ToInt32(txt1_ga.Text);
        agrega.preg2_evalgr = Convert.ToInt32(txt2_ga.Text);
        int preggr2 = Convert.ToInt32(txt2_ga.Text);
        agrega.preg3_evalgr = Convert.ToInt32(txt3_ga.Text);
        int preggr3 = Convert.ToInt32(txt3_ga.Text);
        agrega.preg4_evalgr = Convert.ToInt32(txt4_ga.Text);
        int preggr4 = Convert.ToInt32(txt4_ga.Text);
        agrega.preg5_evalgr = Convert.ToInt32(txt5_ga.Text);
        int preggr5 = Convert.ToInt32(txt5_ga.Text);
        agrega.preg6_evalgr = Convert.ToInt32(txt6_ga.Text);
        int preggr6 = Convert.ToInt32(txt6_ga.Text);
        agrega.preg7_evalgr = Convert.ToInt32(txt7_ga.Text);
        int preggr7 = Convert.ToInt32(txt7_ga.Text);
        agrega.preg8_evalgr = Convert.ToInt32(txt8_ga.Text);
        int preggr8 = Convert.ToInt32(txt8_ga.Text);
        agrega.preg9_evalgr = Convert.ToInt32(txt9_ga.Text);
        int preggr9 = Convert.ToInt32(txt9_ga.Text);
        agrega.preg10_evalgr = Convert.ToInt32(txt10_ga.Text);
        int preggr10 = Convert.ToInt32(txt10_ga.Text);
        int totalgr = preggr1 + preggr2 + preggr3 + preggr4 + preggr5 + preggr6 + preggr7 + preggr8 + preggr9 + preggr10;
        agrega.total_evalgr = totalgr;


        if (lbleval.Text == "Agrega Evaluacion")
        {
           
            agrega.agregarEval();
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
            agrega.id_grupo = Convert.ToInt32(RadGrid1.SelectedValues["id_grupo"]);
            agrega.id_evalgpo = Convert.ToInt32(RadGrid1.SelectedValues["id_evalgpo"]);
            agrega.acutulizaeval();

            if (Convert.ToBoolean(agrega.retorno[1]) == false)
            {
                
                lblErrorAgrega.Text = "Error al editar la solicitud";
            }
            else
            {
                RadGrid1.DataBind();
                borrarCampos();
                pnlMask.Visible = false;
                windowAutorizacion.Visible = false;
                lblErrorAgrega.Text = "Se esito exitosamente";
                lnkAbreEdicion.Visible = false;
            }
            

        }

    }
    protected void lnkAbreEdicion_Click(object sender, EventArgs e)
    {
        lbleval.Text = "Edita Visita";
        pnlMask.Visible = true;
        windowAutorizacion.Visible = true;
        int id_grupo = Convert.ToInt32(RadGrid1.SelectedValues["id_grupo"]);
        int id_evalgpo = Convert.ToInt32(RadGrid1.SelectedValues["id_evalgpo"]);
        int[] sesiones = obtieneSesiones();
        EvalGru agregar = new EvalGru();
        agregar.empresa = sesiones[2];
        agregar.sucursal = sesiones[3];
        agregar.id_grupo = id_grupo;
        agregar.id_evalgpo = id_evalgpo;
        agregar.obtieneEval();
        if (Convert.ToBoolean(agregar.retorno[0]))
        {
            DataSet ds = (DataSet)agregar.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txtfecha_eval.SelectedDate = Convert.ToDateTime(r[4]);
               // ddlgerente.Text = Convert.ToString(r[5]);
                cmb_sucursal.SelectedValue = Convert.ToString(r[2]);
                txt_ciclo.Text = Convert.ToString(r[6]);
                ddlgerente.SelectedValue = Convert.ToString(r[7]);
                ddlAsesor.SelectedValue = Convert.ToString(r[8]);
                txt1_gn.Text = Convert.ToString(r[9]);
                txt2_gn.Text = Convert.ToString(r[10]);
                txt3_gn.Text = Convert.ToString(r[11]);
                txt4_gn.Text = Convert.ToString(r[12]);
                txt5_gn.Text = Convert.ToString(r[13]);
                txt1_ga.Text = Convert.ToString(r[15]);
                txt2_ga.Text = Convert.ToString(r[16]);
                txt3_ga.Text = Convert.ToString(r[17]);
                txt4_ga.Text = Convert.ToString(r[18]);
                txt5_ga.Text = Convert.ToString(r[19]);
                txt6_ga.Text = Convert.ToString(r[20]);
                txt7_ga.Text = Convert.ToString(r[21]);
                txt8_ga.Text = Convert.ToString(r[22]);
                txt9_ga.Text = Convert.ToString(r[23]);
                txt10_ga.Text = Convert.ToString(r[24]);

            }
        }
    }
    public void borrarCampos()
    {
        txtfecha_eval.Clear();
        txt_ciclo.Text="";
      //  txt_gerente.Text="";
       // txt_asesor.Text="";
    }

    public static void SoloNumeros(KeyPressEventArgs pE)
    {
        if (char.IsDigit(pE.KeyChar))
        {
            pE.Handled = false;
        }
        else if (char.IsControl(pE.KeyChar))
        {
            pE.Handled = false;
        }
        else
        {
            pE.Handled = true;
        }
        
    }

    protected void lnkCerrar_Click(object sender, EventArgs e)
    {
        pnlMask.Visible = false;
        windowAutorizacion.Visible = false;
        borrarCampos();
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
        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();


        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" Evaluación Grupal ");
        documento.AddCreator("AserNegocio1");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\EvaluacionGrupal_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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



            // Creamos la imagen y le ajustamos el tamaño


            string imagepath = HttpContext.Current.Server.MapPath("img/");
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagepath + "logo_aser.png");
            //logo.WidthPercentage = 15f;


            //encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.SetWidths(new float[] { 2f, 8f });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;


            PdfPCell titulo = new PdfPCell(new Phrase("APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + " EVALUACIÓN DEL GRUPO PRODUCTIVO ", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD)));
            //PdfPCell titulo = new PdfPCell(new Phrase("Gaarve S.A de C.V. " + Environment.NewLine + Environment.NewLine + " Informe de Nomina " + Environment.NewLine + Environment.NewLine + strDatosObra, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(logo);
            tablaEncabezado.AddCell(titulo);

            documento.Add(tablaEncabezado);

            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            //fecha
            EvalGru impri = new EvalGru();
            int[] sesiones = obtieneSesiones();
            impri.sucursal = sesiones[3];
            impri.empresa = sesiones[2];
            int agregarEval = Convert.ToInt32(RadGrid1.SelectedValues["id_grupo"]);
            impri.id_grupo = agregarEval;
            int agregarEval1 = Convert.ToInt32(RadGrid1.SelectedValues["id_evalgpo"]);
            impri.id_evalgpo = agregarEval;
            impri.obtineImpri();


     

            if (Convert.ToBoolean(impri.retorno[0]))
            {
                DataSet ds = (DataSet)impri.retorno[1];
                string fechaIm = "";
                string sucurimp = "";
                string grupoproimp = "";
                string numeroimp = "";
                string cicliimp = "";
                string gerenteimp = "";
                string asesorimp = "";
                string preg1imp = "";
                string preg2imp = "";
                string preg3imp = "";
                string preg4imp = "";
                string preg5imp = "";
                string total1imp = "";
                string preg1rimp = "";
                string preg2rimp = "";
                string preg3rimp = "";
                string preg4rimp = "";
                string preg5rimp = "";
                string preg6rimp = "";
                string preg7rimp = "";
                string preg8rimp = "";
                string preg9rimp = "";
                string preg10rimp = "";
                string total2imp = "";
                foreach (DataRow evG in ds.Tables[0].Rows)
                {
                     fechaIm = evG[4].ToString();
                     sucurimp = evG[1].ToString();
                     grupoproimp = evG[5].ToString();
                     numeroimp = evG[2].ToString();
                     cicliimp = evG[6].ToString();
                     gerenteimp = evG[7].ToString();
                     asesorimp = evG[8].ToString();
                     preg1imp = evG[9].ToString();
                     preg2imp = evG[10].ToString();
                     preg3imp = evG[11].ToString();
                     preg4imp = evG[12].ToString();
                     preg5imp = evG[13].ToString();
                     total1imp = evG[14].ToString();
                     preg1rimp = evG[15].ToString();
                     preg2rimp = evG[16].ToString();
                     preg3rimp = evG[17].ToString();
                     preg4rimp = evG[18].ToString();
                     preg5rimp = evG[19].ToString();
                     preg6rimp = evG[20].ToString();
                     preg7rimp = evG[21].ToString();
                     preg8rimp = evG[22].ToString();
                     preg9rimp = evG[23].ToString();
                     preg10rimp = evG[24].ToString();
                     total2imp = evG[25].ToString();

                }



                PdfPTable fecha1 = new PdfPTable(2);
            fecha1.DefaultCell.Border = 0;
            fecha1.WidthPercentage = 30f;
            fecha1.HorizontalAlignment = Element.ALIGN_RIGHT;
            

            PdfPCell fec = new PdfPCell(new Phrase("FECHA ", fuente6));
            fec.VerticalAlignment = Element.ALIGN_MIDDLE;
                fec.HorizontalAlignment = Element.ALIGN_CENTER;
            fec.BackgroundColor = BaseColor.LIGHT_GRAY; 
            fecha1.AddCell(fec);


            PdfPCell fec2 = new PdfPCell(new Phrase("" + Convert.ToDateTime(fechaIm).ToString("dd/MM/yyyy"), fuente6));

            fec2.VerticalAlignment = Element.ALIGN_RIGHT;
            fecha1.AddCell(fec2);

                    documento.Add(fecha1);
            documento.Add(new Paragraph(" "));


            


                    //sucursal - grupo - productivo - ciclo


                    PdfPTable enca = new PdfPTable(4);

                    enca.WidthPercentage = 100f;

                  

                    PdfPCell sucur = new PdfPCell(new Phrase("SUCURSAL", fuente6));

                    sucur.HorizontalAlignment = 1;
                    sucur.BackgroundColor = BaseColor.LIGHT_GRAY;
                    enca.AddCell(sucur);
                    //documento.Add(enca); ---- final

                    PdfPCell grupro = new PdfPCell(new Phrase("GRUPO PRODUCTIVO", fuente6));

                    grupro.HorizontalAlignment = 1;
                    grupro.BackgroundColor = BaseColor.LIGHT_GRAY;
                    enca.AddCell(grupro);


                    PdfPCell nume = new PdfPCell(new Phrase("NÚMERO", fuente6));

                    nume.HorizontalAlignment = 1;
                    nume.BackgroundColor = BaseColor.LIGHT_GRAY;
                    enca.AddCell(nume);


                    PdfPCell ciclo = new PdfPCell(new Phrase("CICLO", fuente6));

                    ciclo.HorizontalAlignment = 1;
                    ciclo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    enca.AddCell(ciclo);


                    PdfPCell sucur2 = new PdfPCell(new Phrase(" "+sucurimp, fuente6));

                    sucur2.HorizontalAlignment=Element.ALIGN_CENTER;
                    enca.AddCell(sucur2);


                    PdfPCell grupro2 = new PdfPCell(new Phrase(""+grupoproimp, fuente6));

                    grupro2.HorizontalAlignment = 1;
                    enca.AddCell(grupro2);

                    PdfPCell nume2 = new PdfPCell(new Phrase(""+numeroimp, fuente6));

                    nume2.HorizontalAlignment = 1;
                    enca.AddCell(nume2);

                    PdfPCell ciclo2 = new PdfPCell(new Phrase(""+cicliimp, fuente6));

                    ciclo2.HorizontalAlignment = 1;
                    enca.AddCell(ciclo2);
                    documento.Add(enca);

                    PdfPTable gerea = new PdfPTable(2);

                    gerea.WidthPercentage = 100f;

                    PdfPCell gerente = new PdfPCell(new Phrase("GERENTE OPERATIVO", fuente6));

                    gerente.HorizontalAlignment = 1;
                    gerente.BackgroundColor = BaseColor.LIGHT_GRAY;
                    gerea.AddCell(gerente);


                    PdfPCell asesor = new PdfPCell(new Phrase("ASESOR", fuente6));

                    asesor.HorizontalAlignment = 1;
                    asesor.BackgroundColor = BaseColor.LIGHT_GRAY;
                    gerea.AddCell(asesor);


                    PdfPCell gerente2 = new PdfPCell(new Phrase(" "+gerenteimp, fuente6));

                    gerente2.HorizontalAlignment = 1;
                    gerea.AddCell(gerente2);


                    PdfPCell asesor2 = new PdfPCell(new Phrase(" "+asesorimp, fuente6));

                    asesor2.HorizontalAlignment = 1;
                    gerea.AddCell(asesor2);
                    documento.Add(gerea);
                    documento.Add(new Paragraph(" "));

                    //texto de las evalucaciones
                    PdfPTable textEv = new PdfPTable(1);

                    textEv.WidthPercentage = 90f;

                    PdfPCell txtEv = new PdfPCell(new Phrase("LA EVALUACIÓN CONSISTE EN ASIGNAR VALORES PARA CADA UNO DE LOS CRITERIOS SEÑALADOS CONSIDERANDO LA SIGUIENTE ESCALA:", fuente6));
                    txtEv.Border = 0;
                    txtEv.HorizontalAlignment = Element.ALIGN_CENTER;
                txtEv.VerticalAlignment = Element.ALIGN_MIDDLE;
                    textEv.AddCell(txtEv);
                    documento.Add(textEv);
                    //tabla de evalucación

                    PdfPTable tabEv = new PdfPTable(8);

                    tabEv.WidthPercentage = 90f;

                    PdfPCell deficiente = new PdfPCell(new Phrase("DEFICIENTE ", fuente6));

                    deficiente.HorizontalAlignment = 1;
                    deficiente.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabEv.AddCell(deficiente);

                    PdfPCell cero = new PdfPCell(new Phrase("0 ", fuente6));

                    cero.HorizontalAlignment = 1;
                    tabEv.AddCell(cero);

                    PdfPCell regular = new PdfPCell(new Phrase("REGULAR ", fuente6));

                    regular.HorizontalAlignment = 1;
                    regular.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabEv.AddCell(regular);

                    PdfPCell dos = new PdfPCell(new Phrase("2 ", fuente6));

                    dos.HorizontalAlignment = 1;
                    tabEv.AddCell(dos);

                    PdfPCell bueno = new PdfPCell(new Phrase("BUENO ", fuente6));

                    bueno.HorizontalAlignment = 1;
                    bueno.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabEv.AddCell(bueno);

                    PdfPCell tres = new PdfPCell(new Phrase("3 ", fuente6));

                    tres.HorizontalAlignment = 1;
                    tabEv.AddCell(tres);

                    PdfPCell exce = new PdfPCell(new Phrase("EXCELENTE ", fuente6));

                    exce.HorizontalAlignment = 1;
                    exce.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabEv.AddCell(exce);

                    PdfPCell cinco = new PdfPCell(new Phrase("5 ", fuente6));

                    cinco.HorizontalAlignment = 1;
                    tabEv.AddCell(cinco);
                    documento.Add(tabEv);
                    documento.Add(new Paragraph(" "));

                    //tabla evalucaciones realizadas
                    PdfPTable crit = new PdfPTable(2);
                    crit.WidthPercentage = 100f;
                    int[] critcellwidth = { 85, 15 };
                    crit.SetWidths(critcellwidth);

                    PdfPCell prod = new PdfPCell(new Phrase("CRITERIOS A CALIFICAR PARA GRUPOS PRODUCTIVOS NUEVOS", fuente6));

                    prod.HorizontalAlignment = Element.ALIGN_CENTER;
                    prod.BackgroundColor = BaseColor.LIGHT_GRAY;
                    crit.AddCell(prod);

                    PdfPCell calif = new PdfPCell(new Phrase("CALIFICACIONES", fuente6));

                    calif.HorizontalAlignment = Element.ALIGN_CENTER;
                    calif.BackgroundColor = BaseColor.LIGHT_GRAY;
                    crit.AddCell(calif);
                    documento.Add(crit);

                    // tablas de pregunta y calificación
                    PdfPTable cuest = new PdfPTable(3);
                    cuest.WidthPercentage = 100f;
                    int[] cuestcellwidth = { 2, 83, 15 };
                    cuest.SetWidths(cuestcellwidth);

                    PdfPCell one = new PdfPCell(new Phrase("1", fuente6));

                    one.HorizontalAlignment = Element.ALIGN_LEFT;
                    one.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cuest.AddCell(one);


                    PdfPCell preg1 = new PdfPCell(new Phrase("ASISTENCIA DE LOS INTEGRANTES A LAS REUNIONES DE INTEGRACIÓN GRUPAL", fuente6));

                    preg1.HorizontalAlignment = Element.ALIGN_LEFT;
                    preg1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cuest.AddCell(preg1);

                    PdfPCell cali1 = new PdfPCell(new Phrase(" "+preg1imp, fuente6));

                    cali1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cuest.AddCell(cali1);

                    PdfPCell two = new PdfPCell(new Phrase("2", fuente6));

                    two.HorizontalAlignment = Element.ALIGN_LEFT;
                    two.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cuest.AddCell(two);

                    PdfPCell preg2 = new PdfPCell(new Phrase("PUNTUALIDAD DE LOS INTEGRANTES A LAS REUNIONES DE INTEGRACIÓN GRUPAL", fuente6));

                    preg2.HorizontalAlignment = Element.ALIGN_LEFT;
                    preg2.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cuest.AddCell(preg2);

                    PdfPCell cali2 = new PdfPCell(new Phrase(" "+preg2imp, fuente6));

                    cali2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cuest.AddCell(cali2);

                    PdfPCell tree = new PdfPCell(new Phrase("3", fuente6));

                    tree.HorizontalAlignment = Element.ALIGN_LEFT;
                    tree.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cuest.AddCell(tree);

                    PdfPCell preg3 = new PdfPCell(new Phrase("CUMPLIMIENTO REQUISITOS NORMATIVOS DE LOS INTEGRANDES (SOLVENCIA MORAL, NEGOCIO PROPIO, ARRAIGO EN EL DOMICILIO, CERCANIA GEOGRÁFICA)", fuente6));

                    preg3.HorizontalAlignment = Element.ALIGN_LEFT;
                    preg3.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cuest.AddCell(preg3);

                    PdfPCell cali3 = new PdfPCell(new Phrase(" "+preg3imp, fuente6));

                    cali3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cuest.AddCell(cali3);

                    PdfPCell four = new PdfPCell(new Phrase("4", fuente6));

                    four.HorizontalAlignment = Element.ALIGN_LEFT;
                    four.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cuest.AddCell(four);
                    PdfPCell preg4 = new PdfPCell(new Phrase("DESEMPEÑO DE LA MESA DIRECTIVA (CONOCIMIENTO Y EJECUCIÓN DE FUNCIONES)", fuente6));

                    preg4.HorizontalAlignment = Element.ALIGN_LEFT;
                    preg4.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cuest.AddCell(preg4);

                    PdfPCell cali4 = new PdfPCell(new Phrase(" "+preg4imp, fuente6));

                    cali4.HorizontalAlignment = Element.ALIGN_CENTER;
                    cuest.AddCell(cali4);

                    PdfPCell five = new PdfPCell(new Phrase("5", fuente6));

                    five.HorizontalAlignment = Element.ALIGN_LEFT;
                    five.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cuest.AddCell(five);

                    PdfPCell preg5 = new PdfPCell(new Phrase("APORTACIÓN DE A GARANTÍA LÍQUIDA EN TIEMPO Y FORMA.", fuente6));

                    preg5.HorizontalAlignment = Element.ALIGN_LEFT;
                    preg5.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cuest.AddCell(preg5);

                    PdfPCell cali5 = new PdfPCell(new Phrase(" "+preg5imp, fuente6));

                    cali5.HorizontalAlignment = Element.ALIGN_CENTER;
                    cuest.AddCell(cali5);
                    documento.Add(cuest);

                    // tabla de los totales
                    PdfPTable totalC = new PdfPTable(2);
                    totalC.WidthPercentage = 100f;
                    int[] totalCcellwidth = { 85, 15 };
                    totalC.SetWidths(totalCcellwidth);

                    PdfPCell total = new PdfPCell(new Phrase("TOTAL", fuente6));

                    total.HorizontalAlignment = Element.ALIGN_CENTER;
                    total.BackgroundColor = BaseColor.LIGHT_GRAY;
                    totalC.AddCell(total);

                    PdfPCell calT = new PdfPCell(new Phrase(""+total1imp, fuente6));

                    calT.HorizontalAlignment = Element.ALIGN_CENTER;
                    totalC.AddCell(calT);
                    documento.Add(totalC);
                    documento.Add(new Paragraph(" "));

                    //calificacion optima
                    PdfPTable calO = new PdfPTable(2);
                    calO.WidthPercentage = 100f;
                    int[] calOcellwidth = { 85, 15 };
                    calO.SetWidths(calOcellwidth);

                    PdfPCell calOp = new PdfPCell(new Phrase("CALIFICACIÓN OPTIMA", fuente6));

                    calOp.HorizontalAlignment = Element.ALIGN_RIGHT;
                    calOp.BackgroundColor = BaseColor.LIGHT_GRAY;
                    calO.AddCell(calOp);

                    PdfPCell t25 = new PdfPCell(new Phrase("25 PUNTOS", fuente6));

                    t25.HorizontalAlignment = Element.ALIGN_CENTER;
                    calO.AddCell(t25);

                    PdfPCell calMin = new PdfPCell(new Phrase("CALIFICACIÓN MÍNIMA ACEPTABLE PARA AUTORIZACIÓN DEL CRÉDITO PRODUCTIVO.", fuente6));

                    calMin.HorizontalAlignment = Element.ALIGN_RIGHT;
                    calMin.BackgroundColor = BaseColor.LIGHT_GRAY;
                    calO.AddCell(calMin);

                    PdfPCell t20 = new PdfPCell(new Phrase("20 PUNTOS", fuente6));

                    t20.HorizontalAlignment = Element.ALIGN_CENTER;
                    calO.AddCell(t20);
                    documento.Add(calO);
                    documento.Add(new Paragraph(" "));

                    //ENCABEZADO DE CALIFICACIONES PARA GRUPOS PRODUCTIVOS DE RENOVACIÓN

                    PdfPTable calGPR = new PdfPTable(2);
                    calGPR.WidthPercentage = 100f;
                    int[] calGPRcellwidth = { 85, 15 };
                    calGPR.SetWidths(calGPRcellwidth);

                    PdfPCell criGPR = new PdfPCell(new Phrase("CRITERIOS A CALIFICAR PARA GRUPOS PRODUCTIVOS SUBSECUENTES", fuente6));

                    criGPR.HorizontalAlignment = Element.ALIGN_CENTER;
                    criGPR.BackgroundColor = BaseColor.LIGHT_GRAY;
                    calGPR.AddCell(criGPR);

                    PdfPCell evGPR = new PdfPCell(new Phrase("CALIFICACIÓN", fuente6));

                    evGPR.HorizontalAlignment = Element.ALIGN_MIDDLE;
                    evGPR.BackgroundColor = BaseColor.LIGHT_GRAY;
                    calGPR.AddCell(evGPR);
                    documento.Add(calGPR);

                    //LA TABLA A LLENAR
                    PdfPTable critCG = new PdfPTable(3);
                    critCG.WidthPercentage = 100f;
                    int[] critCGcellwidth = { 2, 83, 15 };
                    critCG.SetWidths(critCGcellwidth);

                    PdfPCell a1 = new PdfPCell(new Phrase("1", fuente6));

                    a1.HorizontalAlignment = Element.ALIGN_LEFT;
                    a1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(a1);

                    PdfPCell asistencia = new PdfPCell(new Phrase("ASISTENCIA DE LOS INTEGRANTES A LAS REUNIONES SEMANALES", fuente6));

                    asistencia.HorizontalAlignment = Element.ALIGN_LEFT;
                    asistencia.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(asistencia);

                    PdfPCell aCal = new PdfPCell(new Phrase(""+preg1rimp, fuente6));

                    aCal.HorizontalAlignment = Element.ALIGN_CENTER;
                    critCG.AddCell(aCal);

                    PdfPCell a2 = new PdfPCell(new Phrase("2", fuente6));

                    a2.HorizontalAlignment = Element.ALIGN_LEFT;
                    a2.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(a2);

                    PdfPCell puntInt = new PdfPCell(new Phrase("PUNTUALIDAD DE LOS INTEGRANTES A LAS REUNIONES SEMANALES", fuente6));

                    puntInt.HorizontalAlignment = Element.ALIGN_LEFT;
                    puntInt.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(puntInt);

                    PdfPCell aCal2 = new PdfPCell(new Phrase(" "+preg2rimp, fuente6));

                    aCal2.HorizontalAlignment = Element.ALIGN_CENTER;
                    critCG.AddCell(aCal2);

                    PdfPCell a3 = new PdfPCell(new Phrase("3", fuente6));

                    a3.HorizontalAlignment = Element.ALIGN_LEFT;
                    a3.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(a3);

                    PdfPCell desempeño = new PdfPCell(new Phrase("DESEMPEÑO DE LA MESA DIRECTIVA(CONOCIMIENTO Y CUMPLIMIENTO DE FUNCIONES)", fuente6));

                    desempeño.HorizontalAlignment = Element.ALIGN_LEFT;
                    desempeño.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(desempeño);

                    PdfPCell aCal3 = new PdfPCell(new Phrase(""+preg3rimp, fuente6));

                    aCal3.HorizontalAlignment = Element.ALIGN_CENTER;
                    critCG.AddCell(aCal3);

                    PdfPCell a4 = new PdfPCell(new Phrase("4", fuente6));

                    a4.HorizontalAlignment = Element.ALIGN_LEFT;
                    a4.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(a4);

                    PdfPCell aportacion = new PdfPCell(new Phrase("APORTACIÓN DE LA GARANTÍA LÍQUIDA EN TIEMPO Y FORMA", fuente6));

                    aportacion.HorizontalAlignment = Element.ALIGN_LEFT;
                    aportacion.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(aportacion);

                    PdfPCell aCal4 = new PdfPCell(new Phrase(""+preg4rimp, fuente6));

                    aCal4.HorizontalAlignment = Element.ALIGN_CENTER;
                    critCG.AddCell(aCal4);

                    PdfPCell a5 = new PdfPCell(new Phrase("5", fuente6));

                    a5.HorizontalAlignment = Element.ALIGN_LEFT;
                    a5.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(a5);

                    PdfPCell metodolog = new PdfPCell(new Phrase("APLICACIÓN DE LA METODOLOGÍA INSTITUCIONAL (CONOCIEMIENTO Y MANEJO DE CONTROLES DE PAGO).", fuente6));

                    metodolog.HorizontalAlignment = Element.ALIGN_LEFT;
                    metodolog.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(metodolog);

                    PdfPCell aCal5 = new PdfPCell(new Phrase(""+preg5rimp, fuente6));

                    aCal5.HorizontalAlignment = Element.ALIGN_CENTER;
                    critCG.AddCell(aCal5);

                    PdfPCell a6 = new PdfPCell(new Phrase("6", fuente6));

                    a6.HorizontalAlignment = Element.ALIGN_LEFT;
                    a6.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(a6);

                    PdfPCell oblig = new PdfPCell(new Phrase("CUMPLIMIENTO DE LA OBLIGACIÓN SOLIDARIA DE LOS INTEGRANTES DEL GRUPO PRODUCTIVO.", fuente6));

                    oblig.HorizontalAlignment = Element.ALIGN_LEFT;
                    oblig.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(oblig);

                    PdfPCell aCal6 = new PdfPCell(new Phrase(""+preg6rimp, fuente6));

                    aCal6.HorizontalAlignment = Element.ALIGN_CENTER;
                    critCG.AddCell(aCal6);

                    PdfPCell a7 = new PdfPCell(new Phrase("7", fuente6));

                    a7.HorizontalAlignment = Element.ALIGN_LEFT;
                    a7.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(a7);

                    PdfPCell ahorro = new PdfPCell(new Phrase("AHORRO DE LOS INTEGRANTES DEL GRUPO PRODUCTIVO", fuente6));

                    ahorro.HorizontalAlignment = Element.ALIGN_LEFT;
                    ahorro.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(ahorro);

                    PdfPCell aCal7 = new PdfPCell(new Phrase(""+preg7rimp, fuente6));

                    aCal7.HorizontalAlignment = Element.ALIGN_CENTER;
                    critCG.AddCell(aCal7);

                    PdfPCell a8 = new PdfPCell(new Phrase("8", fuente6));

                    a8.HorizontalAlignment = Element.ALIGN_LEFT;
                    a8.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(a8);

                    PdfPCell apego = new PdfPCell(new Phrase("APEGO AL REGLAMENTO INTERNO", fuente6));

                    apego.HorizontalAlignment = Element.ALIGN_LEFT;
                    apego.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(apego);

                    PdfPCell aCal8 = new PdfPCell(new Phrase(""+preg8rimp, fuente6));

                    aCal8.HorizontalAlignment = Element.ALIGN_CENTER;
                    critCG.AddCell(aCal8);

                    PdfPCell a9 = new PdfPCell(new Phrase("9", fuente6));

                    a9.HorizontalAlignment = Element.ALIGN_LEFT;
                    a9.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(a9);

                    PdfPCell creci = new PdfPCell(new Phrase("CRECIMIENTO DEL GRUPO PRODUCTIVO (INCORPORACIÓN DE NUEVOS INTEGRANTES)", fuente6));

                    creci.HorizontalAlignment = Element.ALIGN_LEFT;
                    creci.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(creci);

                    PdfPCell aCal9 = new PdfPCell(new Phrase(""+preg9rimp, fuente6));

                    aCal9.HorizontalAlignment = Element.ALIGN_CENTER;
                    critCG.AddCell(aCal9);

                    PdfPCell a10 = new PdfPCell(new Phrase("10", fuente6));

                    a10.HorizontalAlignment = Element.ALIGN_LEFT;
                    a10.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(a10);

                    PdfPCell cumNor = new PdfPCell(new Phrase("CUMPLIMIENTO REQUISITOS NORMATIVOS DE LOS NUEVOS INTEGRANTES (SOLVENCIA MORAL, NEGOCIO PROPIO, ARRAIGO EN EL DOMICILIO, CERCANIA GRAOGRÁFICA).", fuente6));

                    cumNor.HorizontalAlignment = Element.ALIGN_LEFT;
                    cumNor.BackgroundColor = BaseColor.LIGHT_GRAY;
                    critCG.AddCell(cumNor);

                    PdfPCell aCal10 = new PdfPCell(new Phrase(" "+preg10rimp, fuente6));

                    aCal10.HorizontalAlignment = Element.ALIGN_CENTER;
                    critCG.AddCell(aCal10);
                    documento.Add(critCG);

                    //pie de tabla
                    PdfPTable tota = new PdfPTable(2);
                    tota.WidthPercentage = 100f;
                    int[] totacellwidth = { 85, 15 };
                    tota.SetWidths(totacellwidth);

                    PdfPCell tota1 = new PdfPCell(new Phrase("TOTAL", fuente6));

                    tota1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tota1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tota.AddCell(tota1);

                    PdfPCell tota2 = new PdfPCell(new Phrase(""+total2imp, fuente6));

                    tota2.HorizontalAlignment = Element.ALIGN_CENTER;
                    tota.AddCell(tota2);
                    documento.Add(tota);
                    documento.Add(new Paragraph(" "));

                    //ultima tabla

                    PdfPTable optim = new PdfPTable(2);
                    optim.WidthPercentage = 100f;
                    int[] optimcellwidth = { 85, 15 };
                    optim.SetWidths(optimcellwidth);

                    PdfPCell optimCal = new PdfPCell(new Phrase("CALIFICACIÓN OPTIMA", fuente6));
                    optimCal.HorizontalAlignment = Element.ALIGN_RIGHT;
                    optimCal.BackgroundColor = BaseColor.LIGHT_GRAY;
                    optim.AddCell(optimCal);

                    PdfPCell optimCal2 = new PdfPCell(new Phrase("50 PUNTOS", fuente6));

                    optimCal2.HorizontalAlignment = Element.ALIGN_CENTER;
                    optim.AddCell(optimCal2);

                    PdfPCell minCal = new PdfPCell(new Phrase("CALIFICACIÓN MÍNIMA ACEPTABLE PARA AUTORIZACIÓN DEL CRÉDITO PRODUCTIVO.", fuente6));

                    minCal.HorizontalAlignment = Element.ALIGN_RIGHT;
                    minCal.BackgroundColor = BaseColor.LIGHT_GRAY;
                    optim.AddCell(minCal);

                    PdfPCell minCal2 = new PdfPCell(new Phrase("40 PUNTOS", fuente6));

                    minCal2.HorizontalAlignment = Element.ALIGN_CENTER;
                    optim.AddCell(minCal2);
                    documento.Add(optim);
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));

                    //firmas

                    PdfPTable firm = new PdfPTable(3);
                    firm.WidthPercentage = 100f;

                    PdfPCell guion1 = new PdfPCell(new Phrase("\n \n ______________________________________ \n \n NOMBRE Y FIRMA DEL PRESIDENTA DEL GRUPO \n \n", fuente6));
                    guion1.Border = 1;
                    guion1.BorderWidth = 1;
                    guion1.BorderWidthBottom = 1;
                    guion1.BorderWidthLeft = 1;
                    guion1.BorderWidthRight = 1;
                    guion1.BorderWidthTop = 1;
                    guion1.BorderColor = BaseColor.BLUE;
                    guion1.HorizontalAlignment = Element.ALIGN_CENTER;
                    firm.AddCell(guion1);

                    PdfPCell guion2 = new PdfPCell(new Phrase("\n \n ______________________________________ \n \n NOMBRE Y FIRMA ASESOR \n \n", fuente6));
                    guion2.Border = 1;
                guion2.BorderWidthTop = 1;
                guion2.BorderWidthRight = 1;
                guion2.BorderWidthLeft = 1;
                guion2.BorderWidthBottom = 1;
                    guion2.BorderColor = BaseColor.BLUE;
                    guion2.HorizontalAlignment = Element.ALIGN_CENTER;
                    firm.AddCell(guion2);

                    PdfPCell guion3 = new PdfPCell(new Phrase("\n \n ______________________________________ \n \n NOMBRE Y FIRMA GERENTE OPERATIVO \n \n", fuente6));
                    guion3.Border = 1;
                guion3.BorderWidthBottom = 1;
                guion3.BorderWidthLeft = 1;
                guion3.BorderWidthRight = 1;
                guion3.BorderWidthTop = 1;
                    guion3.BorderColor = BaseColor.BLUE;
                    guion3.HorizontalAlignment = Element.ALIGN_CENTER;
                    firm.AddCell(guion3);

                    
                    documento.Add(firm);

                    //documento.Add(new Paragraph(""));
                    documento.Close();


                    //
                    FileInfo filename = new FileInfo(archivo);
                    if (filename.Exists)
                    {
                        string url = "Descargas.aspx?filename=" + filename.Name;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "pdfs", "window.open('" + url + "', 'nuevo', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, titlebar=no, fullscreen=yes,resizable=yes');", true);
                    }
                
            }
        }
    }

  
    protected void txt5_gn_TextChanged(object sender, EventArgs e)
    {
        int val1, val2, val3, val4, val5;
        val1 = Convert.ToInt32(txt1_gn.Text);
        val2 = Convert.ToInt32(txt2_gn.Text);
        val3 = Convert.ToInt32(txt3_gn.Text);
        val4 = Convert.ToInt32(txt4_gn.Text);
        val5 = Convert.ToInt32(txt5_gn.Text);

        txt_total.Text = Convert.ToString (val1+val2+val3+val4+val5) ;

    }

  
    protected void cmb_sucursal_SelectedIndexChanged1(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        EvalGru agregar = new EvalGru();
        agregar.empresa = sesiones[2];
        agregar.sucursal = sesiones[3];
        int grupo = Convert.ToInt32(cmb_sucursal.SelectedValue);
        agregar.id_grupo = grupo;
        agregar.obtenerciclo();
        if (Convert.ToBoolean(agregar.retorno[0]))
        {
            DataSet ds = (DataSet)agregar.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txt_ciclo.Text = Convert.ToString(r[0]);
                if (Convert.ToInt32(txt_ciclo.Text) == 1)
                {
                    txt1_gn.Visible = true;
                    txt2_gn.Visible = true;
                    txt3_gn.Visible = true;
                    txt4_gn.Visible = true;
                    txt5_gn.Visible = true;
                    txt1_ga.Visible = false;
                    txt2_ga.Visible = false;
                    txt3_ga.Visible = false;
                    txt4_ga.Visible = false;
                    txt5_ga.Visible = false;
                    txt6_ga.Visible = false;
                    txt7_ga.Visible = false;
                    txt8_ga.Visible = false;
                    txt9_ga.Visible = false;
                    txt10_ga.Visible = false;
                    txt1_ga.Text = "0";
                    txt2_ga.Text = "0";
                    txt3_ga.Text = "0";
                    txt4_ga.Text = "0";
                    txt5_ga.Text = "0";
                    txt6_ga.Text = "0";
                    txt7_ga.Text = "0";
                    txt8_ga.Text = "0";
                    txt9_ga.Text = "0";
                    txt10_ga.Text = "0";
                }
                else {
                    txt1_gn.Visible = false;
                    txt2_gn.Visible = false;
                    txt3_gn.Visible = false;
                    txt4_gn.Visible = false;
                    txt5_gn.Visible = false;
                    txt1_ga.Visible = true;
                    txt2_ga.Visible = true;
                    txt3_ga.Visible = true;
                    txt4_ga.Visible = true;
                    txt5_ga.Visible = true;
                    txt6_ga.Visible = true;
                    txt7_ga.Visible = true;
                    txt8_ga.Visible = true;
                    txt9_ga.Visible = true;
                    txt10_ga.Visible = true;
                    txt1_gn.Text = "0";
                    txt2_gn.Text = "0";
                    txt3_gn.Text = "0";
                    txt4_gn.Text = "0";
                    txt5_gn.Text = "0";

                }
            }
        }
        }

    protected void txt10_ga_TextChanged(object sender, EventArgs e)
    {
        int val1, val2, val3, val4, val5,val6,val7,val8,val9,val10;
        val1 = Convert.ToInt32(txt1_ga.Text);
        val2 = Convert.ToInt32(txt2_ga.Text);
        val3 = Convert.ToInt32(txt3_ga.Text);
        val4 = Convert.ToInt32(txt4_ga.Text);
        val5 = Convert.ToInt32(txt5_ga.Text);
        val6 = Convert.ToInt32(txt6_ga.Text);
        val7 = Convert.ToInt32(txt7_ga.Text);
        val8 = Convert.ToInt32(txt8_ga.Text);
        val9 = Convert.ToInt32(txt9_ga.Text);
        val10 = Convert.ToInt32(txt10_ga.Text);

        txt_total2.Text = Convert.ToString(val1 + val2 + val3 + val4 + val5+val6+val7+val8+val9+val10);
    }
}