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

public partial class vistasSolicitud : System.Web.UI.Page
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
    protected void lnkObtieneEncabezado_Click(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        SolVis edita = new SolVis();
        edita.empresa = sesiones[2];
        edita.sucursal = sesiones[3];
        edita.idSolicitudEdita =Convert.ToInt32( Label15.Text);
        edita.obtieneSolicitudEnc();
        if (Convert.ToBoolean(edita.retorno[0]) == true)
        {
            DataSet ds = (DataSet)edita.retorno[1];

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txt_suc_vis.Text = r[1].ToString();
                try { txtFecha_sol_vis.SelectedDate = Convert.ToDateTime(r[3]); }
                catch (Exception) { txtFecha_sol_vis.Clear(); txt_fecha_sol.DataBind(); }
                try { txt_fecha_sol.SelectedDate = Convert.ToDateTime(r[4]); }
                catch (Exception) { txt_fecha_sol.Clear(); txt_fecha_sol.DataBind(); }
                txt_gpro_vis.Text = r[5].ToString();
                txt_numGrup_vis.Text = r[6].ToString();
                txt_moncred_vis.Text = r[7].ToString();
                txt_plazo_vis.Text = r[8].ToString();
                txt_tasa_vis.Text = r[9].ToString();
                txt_garaliq_vis.Text = r[10].ToString();
                txt_mont_max.Text = r[11].ToString();
                txt_montaut_vis.Text = r[12].ToString();
                txt_plazo2.Text = r[13].ToString();    
                txt_tasa2_vis.Text = r[14].ToString();
                txt_formapag_vis.Text = r[15].ToString();

            }

        }
      


    }



    protected void lnkImprimir_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;

        // Tipo de Font que vamos utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 3, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 1, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente11 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente9 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente20 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente21 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER.Rotate());
        documento.AddTitle(" SOLICITUDGRUPOS ");
        documento.AddCreator("AserNegocio1");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\SOLICITUDGRUPOS_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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
            logo.ScaleAbsolute(1f, 1f);
            //logo.WidthPercentage = 15f;


            //encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.SetWidths(new float[] { 2f, 8f });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 86f;


            PdfPCell titulo = new PdfPCell(new Phrase("APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + " SOLICITUD DE CRÉDITO PARA GRUPOS PRODUCTIVOS", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD)));
            //PdfPCell titulo = new PdfPCell(new Phrase("Gaarve S.A de C.V. " + Environment.NewLine + Environment.NewLine + " Informe de Nomina " + Environment.NewLine + Environment.NewLine + strDatosObra, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(logo);
            tablaEncabezado.AddCell(titulo);

            documento.Add(tablaEncabezado);

            documento.Add(new Paragraph(" "));



            SolicitudCredito infor = new SolicitudCredito();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            int idsol = Convert.ToInt32(RadGrid1.SelectedValues["id_solicitud_credito"]);
            infor.idSolicitudEdita = idsol;
            infor.optieneimpresion();


            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];

                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    //tabla 1
                    PdfPTable sucur = new PdfPTable(6);
                    sucur.WidthPercentage = 100f;
                    int[] sucurcellwidth = { 10, 15, 12, 18, 20, 20 };
                    sucur.SetWidths(sucurcellwidth);

                    string Sucursal = r[1].ToString();
                    DateTime fe = Convert.ToDateTime(r[5]);
                    string FechaSolicitud = r[4].ToString();
                    string FechaEntrega = fe.ToString("yyyy-MM-dd");
                    if (FechaEntrega == "1900-01-01")
                    {
                        FechaEntrega = "";
                    }
                    else
                    {
                        FechaEntrega = r[5].ToString();
                    }



                    PdfPCell sucurs = new PdfPCell(new Phrase("SUCURSAL", fuente6));
                    sucurs.HorizontalAlignment = Element.ALIGN_CENTER;
                    sucurs.BackgroundColor = BaseColor.LIGHT_GRAY;
                    sucur.AddCell(sucurs);

                    PdfPCell sucurs1 = new PdfPCell(new Phrase(" " + Sucursal, fuente8));
                    sucurs1.HorizontalAlignment = Element.ALIGN_CENTER;
                    sucur.AddCell(sucurs1);

                    PdfPCell fecS = new PdfPCell(new Phrase("FECHA DE SOLICITUD", fuente6));
                    fecS.HorizontalAlignment = Element.ALIGN_CENTER;
                    fecS.BackgroundColor = BaseColor.LIGHT_GRAY;
                    sucur.AddCell(fecS);

                    PdfPCell fecS1 = new PdfPCell(new Phrase(" " + Convert.ToDateTime(FechaSolicitud).ToString("dd/MM/yyyy"), fuente8));
                    fecS1.HorizontalAlignment = Element.ALIGN_CENTER;
                    sucur.AddCell(fecS1);

                    PdfPCell fecE = new PdfPCell(new Phrase("FECHA DE DESEMBOLSO", fuente6));
                    fecE.HorizontalAlignment = Element.ALIGN_CENTER;
                    fecE.BackgroundColor = BaseColor.LIGHT_GRAY;
                    sucur.AddCell(fecE);

                    PdfPCell fecE1 = new PdfPCell(new Phrase(" " + FechaEntrega, fuente8));
                    fecE1.HorizontalAlignment = Element.ALIGN_CENTER;
                    sucur.AddCell(fecE1);
                    documento.Add(sucur);

                    //TABLA DE USO PRODUCTIVO
                    PdfPTable grupProd = new PdfPTable(12);
                    grupProd.WidthPercentage = 100f;
                    int[] grupProdcellwidth = { 15, 10, 12, 20, 6, 6, 10, 18, 10, 5, 8, 5 };
                    grupProd.SetWidths(grupProdcellwidth);

                    string GrupoProductivo = r[6].ToString();
                    string NumeroGrupo = r[7].ToString();
                    string MontoCredito = r[8].ToString();
                    string Plazo = r[9].ToString();
                    string Tasa = r[10].ToString();
                    string Ciclo = r[17].ToString();

                    PdfPCell numGr = new PdfPCell(new Phrase("NÚMERO DE GRUPO", fuente6));
                    numGr.HorizontalAlignment = Element.ALIGN_CENTER;
                    numGr.BackgroundColor = BaseColor.LIGHT_GRAY;
                    grupProd.AddCell(numGr);

                    PdfPCell numGr1 = new PdfPCell(new Phrase(" " + NumeroGrupo, fuente8));
                    numGr1.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupProd.AddCell(numGr1);


                    PdfPCell gruPr = new PdfPCell(new Phrase("GRUPO PRODUCTIVO", fuente6));
                    gruPr.HorizontalAlignment = Element.ALIGN_CENTER;
                    gruPr.BackgroundColor = BaseColor.LIGHT_GRAY;
                    grupProd.AddCell(gruPr);

                    PdfPCell gruPr1 = new PdfPCell(new Phrase(" " + GrupoProductivo, fuente8));
                    gruPr1.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupProd.AddCell(gruPr1);

                    PdfPCell cliclo1 = new PdfPCell(new Phrase("CLICLO", fuente6));
                    cliclo1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cliclo1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    grupProd.AddCell(cliclo1);


                    PdfPCell cliclo = new PdfPCell(new Phrase(" " + Ciclo, fuente8));
                    cliclo.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupProd.AddCell(cliclo);


                    PdfPCell montCr = new PdfPCell(new Phrase("MONTO DEL CRÉDITO", fuente6));
                    montCr.HorizontalAlignment = Element.ALIGN_CENTER;
                    montCr.BackgroundColor = BaseColor.LIGHT_GRAY;
                    grupProd.AddCell(montCr);
                    decimal monto = 0;
                    try { monto = Convert.ToDecimal(r[8]); } catch (Exception) { monto = 0; }
                    PdfPCell montCr1 = new PdfPCell(new Phrase(monto.ToString("C2"), fuente8));
                    montCr1.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupProd.AddCell(montCr1);

                    PdfPCell plaz = new PdfPCell(new Phrase("PLAZO (SEMANAS)", fuente6));
                    plaz.HorizontalAlignment = Element.ALIGN_CENTER;
                    plaz.BackgroundColor = BaseColor.LIGHT_GRAY;
                    grupProd.AddCell(plaz);

                    PdfPCell plaz1 = new PdfPCell(new Phrase(" " + Plazo, fuente8));
                    plaz1.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupProd.AddCell(plaz1);

                    PdfPCell taza = new PdfPCell(new Phrase("TASA(%)", fuente6));
                    taza.HorizontalAlignment = Element.ALIGN_CENTER;
                    taza.BackgroundColor = BaseColor.LIGHT_GRAY;
                    grupProd.AddCell(taza);

                    PdfPCell taza1 = new PdfPCell(new Phrase(" " + Tasa, fuente8));
                    taza1.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupProd.AddCell(taza1);
                    documento.Add(grupProd);

                    //tabla invisible 
                    PdfPTable exclu = new PdfPTable(1);
                    exclu.DefaultCell.Border = 0;
                    exclu.WidthPercentage = 40f;
                    exclu.HorizontalAlignment = Element.ALIGN_RIGHT;

                    PdfPCell exc = new PdfPCell(new Phrase("PARA USO EXCLUSIVO DEL ÁREA DE RIESGO CREDITICIO", fuente1));
                    exc.HorizontalAlignment = Element.ALIGN_CENTER;
                    exc.Border = 0;
                    exclu.AddCell(exc);
                    documento.Add(exclu);

                    //TABLA DE GARANTIA
                    PdfPTable garant = new PdfPTable(10);
                    garant.WidthPercentage = 100f;
                    int[] garantcellwidth = { 10, 12, 25, 20, 14, 14, 10, 5, 10, 5 };
                    garant.SetWidths(garantcellwidth);

                    string GarantiaLiquidaEncabezado = r[11].ToString();
                    string MontoMaximo = r[12].ToString();
                    string MontoAutorizado = r[13].ToString();
                    string PlazoRC = r[14].ToString();
                    string TasaRC = r[15].ToString();


                    PdfPCell garanti = new PdfPCell(new Phrase("GARANTÍA LÍQUIDA $", fuente6));
                    garanti.HorizontalAlignment = Element.ALIGN_CENTER;
                    garanti.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garant.AddCell(garanti);
                    decimal monto1 = 0;
                    try { monto1 = Convert.ToDecimal(r[11]); } catch (Exception) { monto1 = 0; }
                    PdfPCell garanti1 = new PdfPCell(new Phrase(monto1.ToString("C2"), fuente8));
                    garanti1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garant.AddCell(garanti1);

                    PdfPCell montoMax = new PdfPCell(new Phrase("MONTO DEL CRÉDITO GRUPAL (Monto total del crédito/No. Integrantes*2)", fuente6));
                    montoMax.HorizontalAlignment = Element.ALIGN_CENTER;
                    montoMax.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garant.AddCell(montoMax);
                    decimal monto2 = 0;
                    try { monto2 = Convert.ToDecimal(r[12]); } catch (Exception) { monto2 = 0; }
                    PdfPCell montoMax1 = new PdfPCell(new Phrase(monto2.ToString("C2"), fuente8));
                    montoMax1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garant.AddCell(montoMax1);

                    PdfPCell montoAut = new PdfPCell(new Phrase("MONTO DEL CRÉDITO AUTORIZADO", fuente6));
                    montoAut.HorizontalAlignment = Element.ALIGN_CENTER;
                    montoAut.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garant.AddCell(montoAut);
                    decimal monto3 = 0;
                    try { monto3 = Convert.ToDecimal(r[13]); } catch (Exception) { monto3 = 0; }

                    PdfPCell montoAut1 = new PdfPCell(new Phrase(monto3.ToString("C2"), fuente8));
                    montoAut1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garant.AddCell(montoAut1);

                    PdfPCell plazG = new PdfPCell(new Phrase("PLAZO (Semanas)", fuente6));
                    plazG.HorizontalAlignment = Element.ALIGN_CENTER;
                    plazG.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garant.AddCell(plazG);

                    PdfPCell plazG1 = new PdfPCell(new Phrase(" " + PlazoRC, fuente8));
                    plazG1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garant.AddCell(plazG1);

                    PdfPCell tasa = new PdfPCell(new Phrase("TASA (%)", fuente6));
                    tasa.HorizontalAlignment = Element.ALIGN_CENTER;
                    tasa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garant.AddCell(tasa);

                    PdfPCell tasa1 = new PdfPCell(new Phrase(" " + TasaRC, fuente8));
                    tasa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garant.AddCell(tasa1);
                    documento.Add(garant);

                    //tabla de forma de pago
                    /* PdfPTable formP = new PdfPTable(7);
                    formP.WidthPercentage = 50f;
                    int[] formPcellwidth = { 20, 5, 10, 5, 10, 5, 10 };
                    formP.SetWidths(formPcellwidth);
                    formP.HorizontalAlignment = Element.ALIGN_LEFT;

                    string CHE = r[16].ToString();
                    string ORD = r[16].ToString();

                    if (CHE == "CHE")
                    {
                        CHE = "CHE";
                    }
                    else { CHE = " "; }

                    if (ORD == "ORD")
                    {
                        ORD = "ORD";
                    }
                    else
                    {
                        ORD = " ";
                    }

                    

                    PdfPCell formaP = new PdfPCell(new Phrase("FORMA DE PAGO DEL CRÉDITO (Marque con una X)", fuente6));
                    formaP.HorizontalAlignment = Element.ALIGN_CENTER;
                    formaP.BackgroundColor = BaseColor.LIGHT_GRAY;
                    formP.AddCell(formaP);

                    PdfPCell cheq = new PdfPCell(new Phrase("CHEQUE", fuente6));
                    cheq.HorizontalAlignment = Element.ALIGN_CENTER;
                    cheq.BackgroundColor = BaseColor.LIGHT_GRAY;
                    formP.AddCell(cheq);

                    PdfPCell formaP1 = new PdfPCell(new Phrase(" " + CHE, fuente8));
                    formaP1.HorizontalAlignment = Element.ALIGN_CENTER;
                    formP.AddCell(formaP1);

                    PdfPCell ordP = new PdfPCell(new Phrase("ORDEN DE PAGO", fuente6));
                    ordP.HorizontalAlignment = Element.ALIGN_CENTER;
                    ordP.BackgroundColor = BaseColor.LIGHT_GRAY;
                    formP.AddCell(ordP);
                    documento.Add(formP);

                    PdfPCell cheq1 = new PdfPCell(new Phrase(" " + ORD, fuente8));
                    cheq1.HorizontalAlignment = Element.ALIGN_CENTER;
                    formP.AddCell(cheq1);*/



                    //documento.Add(formP);




                    infor.optieneimpresion1();

                    decimal total = 0;
                    decimal total2 = 0;
                    decimal total3 = 0;

                    if (Convert.ToBoolean(infor.retorno[0]))
                    {
                        DataSet ds1 = (DataSet)infor.retorno[1];

                        PdfPTable clientes = new PdfPTable(14);
                        clientes.WidthPercentage = 100f;
                        int[] clientescellwidth = { 3, 5, 20, 3, 3, 3, 3, 5, 10, 5, 5, 5, 6, 24 };
                        clientes.SetWidths(clientescellwidth);
                        clientes.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell no = new PdfPCell(new Phrase("No.", fuente6));
                        no.HorizontalAlignment = Element.ALIGN_CENTER;
                        no.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(no);

                        PdfPCell noCl = new PdfPCell(new Phrase("No. DE CLIENTE", fuente6));
                        noCl.HorizontalAlignment = Element.ALIGN_CENTER;
                        noCl.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(noCl);

                        PdfPCell nomCl = new PdfPCell(new Phrase("NOMBRE DEL CLIENTE", fuente6));
                        nomCl.HorizontalAlignment = Element.ALIGN_CENTER;
                        nomCl.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(nomCl);

                        PdfPCell cicl = new PdfPCell(new Phrase("CICLO", fuente6));
                        cicl.HorizontalAlignment = Element.ALIGN_CENTER;
                        cicl.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(cicl);

                        /*PdfPCell c = new PdfPCell(new Phrase("C", fuente6));
                        c.HorizontalAlignment = Element.ALIGN_CENTER;
                        c.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(c);*/

                        PdfPCell eTab = new PdfPCell(new Phrase("E", fuente6));
                        eTab.HorizontalAlignment = Element.ALIGN_CENTER;
                        eTab.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(eTab);

                        PdfPCell cd = new PdfPCell(new Phrase("CD", fuente6));
                        cd.HorizontalAlignment = Element.ALIGN_CENTER;
                        cd.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(cd);

                        PdfPCell gn = new PdfPCell(new Phrase("GN", fuente6));
                        gn.HorizontalAlignment = Element.ALIGN_CENTER;
                        gn.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(gn);

                        PdfPCell ingre = new PdfPCell(new Phrase("INGRESO \n MENSUAL", fuente6));
                        ingre.HorizontalAlignment = Element.ALIGN_CENTER;
                        ingre.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(ingre);

                        PdfPCell dist = new PdfPCell(new Phrase("INVERSIÓN DEL CRÉDITO", fuente6));
                        dist.HorizontalAlignment = Element.ALIGN_CENTER;
                        dist.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(dist);

                        PdfPCell credA = new PdfPCell(new Phrase("CRÉDITO ANTERIOR", fuente6));
                        credA.HorizontalAlignment = Element.ALIGN_CENTER;
                        credA.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(credA);

                        PdfPCell credSo = new PdfPCell(new Phrase("CRÉDITO SOLICITADO", fuente6));
                        credSo.HorizontalAlignment = Element.ALIGN_CENTER;
                        credSo.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(credSo);

                        PdfPCell garanL = new PdfPCell(new Phrase("GARANTÍA LÍQUIDA", fuente6));
                        garanL.HorizontalAlignment = Element.ALIGN_CENTER;
                        garanL.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(garanL);

                        PdfPCell credAutor = new PdfPCell(new Phrase("CRÉDITO AUTORIZADO", fuente6));
                        credAutor.HorizontalAlignment = Element.ALIGN_CENTER;
                        credAutor.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(credAutor);

                        PdfPCell firmCl = new PdfPCell(new Phrase("FIRMA DEL CLIENTE", fuente6));
                        firmCl.HorizontalAlignment = Element.ALIGN_CENTER;
                        firmCl.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(firmCl);

                        /*PdfPCell telCl = new PdfPCell(new Phrase("TELÉFONO", fuente6));
                        telCl.HorizontalAlignment = Element.ALIGN_CENTER;
                        telCl.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes.AddCell(telCl);*/
                        int numero = 0;
                        foreach (DataRow r1 in ds1.Tables[0].Rows)
                        {
                            //tabla grande

                            numero = numero + 1;

                            string cliente = r1[5].ToString();
                            string CicloDetalle = r1[6].ToString();



                            if (numero == 1 | r1[7].ToString() == "PRE")
                            {
                                if (r1[7].ToString() == "PRE")
                                {
                                    cliente = cliente + "\n PRESIDENTA";
                                }
                            }
                            else if (numero == 2 | r1[7].ToString() == "SEC")
                            {
                                if (r1[7].ToString() == "SEC")
                                {
                                    cliente = cliente + "\n SUPERVISORA";
                                }
                            }
                            else if (numero == 3 | r1[7].ToString() == "TES")
                            {
                                if (r1[7].ToString() == "TES")
                                {
                                    cliente = cliente + "\n TESORERA";
                                }
                            }

                            else if (numero == 4 | r1[7].ToString() == "V1 ")
                            {
                                if (r1[7].ToString() == "V1 ")
                                {
                                    cliente = cliente + "\n VOCAL1";
                                }
                            }
                            else if (numero == 5 | r1[7].ToString() == "V2 ")
                            {
                                if (r1[7].ToString() == "V2 ")
                                {
                                    cliente = cliente + "\n VOCAL2";
                                }
                            }
                            else
                            {

                                if (r1[7].ToString() == "ZIN")
                                {
                                    nomCl = new PdfPCell(new Phrase(" " + cliente, fuente6));
                                    nomCl.HorizontalAlignment = Element.ALIGN_CENTER;
                                }
                            }







                            string Estatus = r1[8].ToString();
                            if (Estatus == "ANT")
                            {
                                Estatus = "A";
                            }
                            else if (Estatus == "NUE")
                            {
                                Estatus = "N";
                            }
                            else if (Estatus == "DES")
                            {
                                Estatus = "D";
                            }
                            else if (Estatus == "REI")
                            {
                                Estatus = "R";
                            }
                            else
                            {
                                Estatus = "CG";
                            }

                            string CausasDese = r1[9].ToString();
                            if (CausasDese == "MOR")
                            {
                                CausasDese = "M";
                            }
                            else if (CausasDese == "CON")
                            {
                                CausasDese = "C";
                            }
                            else if (CausasDese == "SOB")
                            {
                                CausasDese = "S";
                            }
                            else
                            {
                                CausasDese = "MF";
                            }

                            string GiroNegocio = r1[10].ToString();
                            if (GiroNegocio == "COM")
                            {
                                GiroNegocio = "C";
                            }
                            else if (GiroNegocio == "IND")
                            {
                                GiroNegocio = "I";
                            }
                            else if (GiroNegocio == "AGR")
                            {
                                GiroNegocio = "AP";
                            }
                            else if (GiroNegocio == "FOR")
                            {
                                GiroNegocio = "F";
                            }
                            else if (GiroNegocio == "PES")
                            {
                                GiroNegocio = "P";
                            }
                            else
                            {
                                GiroNegocio = "M";
                            }
                            decimal Ingreso = Convert.ToDecimal(r1[11]);

                            string DestinoCredito = r1[12].ToString();
                            decimal CreditoAnterio = Convert.ToDecimal(r1[13]);
                            decimal CreditoSolicitado = Convert.ToDecimal(r1[14]);
                            decimal GarantiaLiquida = Convert.ToDecimal(r1[15]);
                            decimal Creditoautorizado = Convert.ToDecimal(r1[16]);
                            string Telefono = r1[17].ToString();




                            no = new PdfPCell(new Phrase(" " + numero, fuente6));
                            no.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(no);


                            noCl = new PdfPCell(new Phrase("" + r1[4].ToString(), fuente6));
                            noCl.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(noCl);

                            nomCl = new PdfPCell(new Phrase(" " + cliente, fuente6));
                            nomCl.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(nomCl);

                            cicl = new PdfPCell(new Phrase(" " + CicloDetalle, fuente6));
                            cicl.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(cicl);

                            /* c = new PdfPCell(new Phrase(" " + Cargo, fuente6));
                             c.HorizontalAlignment = Element.ALIGN_CENTER;
                             clientes.AddCell(c);*/

                            eTab = new PdfPCell(new Phrase(" " + Estatus, fuente6));
                            eTab.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(eTab);

                            cd = new PdfPCell(new Phrase(" ", fuente6));
                            cd.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(cd);

                            gn = new PdfPCell(new Phrase(" " + GiroNegocio, fuente6));
                            gn.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(gn);

                            ingre = new PdfPCell(new Phrase(" " + Ingreso.ToString("C2"), fuente6));
                            ingre.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(ingre);

                            dist = new PdfPCell(new Phrase(" " + DestinoCredito, fuente6));
                            dist.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(dist);

                            credA = new PdfPCell(new Phrase(" " + CreditoAnterio.ToString("C2"), fuente6));
                            credA.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(credA);

                            credSo = new PdfPCell(new Phrase(" " + CreditoSolicitado.ToString("C2"), fuente6));
                            credSo.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(credSo);

                            garanL = new PdfPCell(new Phrase(" " + GarantiaLiquida.ToString("C2"), fuente6));
                            garanL.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(garanL);

                            credAutor = new PdfPCell(new Phrase(" " + Creditoautorizado.ToString("C2"), fuente6));
                            credAutor.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(credAutor);

                            firmCl = new PdfPCell(new Phrase(" ", fuente6));
                            firmCl.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(firmCl);

                            /*telCl = new PdfPCell(new Phrase(" " + Telefono, fuente6));
                            telCl.HorizontalAlignment = Element.ALIGN_CENTER;
                            clientes.AddCell(telCl);*/

                            total = total + Convert.ToDecimal(Creditoautorizado);
                            total2 = total2 + Convert.ToDecimal(GarantiaLiquida);
                            total3 = total3 + Convert.ToDecimal(CreditoSolicitado);

                        }
                        documento.Add(clientes);

                        PdfPTable clientes2 = new PdfPTable(5);
                        clientes2.WidthPercentage = 100f;
                        int[] clientes2cellwidth = { 60, 5, 5, 6, 24 };
                        clientes2.SetWidths(clientes2cellwidth);
                        clientes2.HorizontalAlignment = Element.ALIGN_CENTER;



                        PdfPCell garanL2 = new PdfPCell(new Phrase("Total", fuente6));
                        garanL2.HorizontalAlignment = Element.ALIGN_RIGHT;
                        garanL2.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes2.AddCell(garanL2);

                        PdfPCell credAutor3 = new PdfPCell(new Phrase("" + total3.ToString("C2"), fuente6));
                        credAutor3.HorizontalAlignment = Element.ALIGN_LEFT;
                        credAutor3.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes2.AddCell(credAutor3);

                        PdfPCell credAutor2 = new PdfPCell(new Phrase("" + total2.ToString("C2"), fuente6));
                        credAutor2.HorizontalAlignment = Element.ALIGN_LEFT;
                        credAutor2.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes2.AddCell(credAutor2);

                        PdfPCell credAutor4 = new PdfPCell(new Phrase("" + total.ToString("C2"), fuente6));
                        credAutor4.HorizontalAlignment = Element.ALIGN_LEFT;
                        credAutor4.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes2.AddCell(credAutor4);


                        PdfPCell credAutor5 = new PdfPCell(new Phrase("", fuente6));
                        credAutor5.HorizontalAlignment = Element.ALIGN_LEFT;
                        credAutor5.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clientes2.AddCell(credAutor5);

                        documento.Add(clientes2);
                        //observaciones
                        PdfPTable observ = new PdfPTable(1);
                        observ.WidthPercentage = 100f;
                        int[] observcellwidth = { 100 };
                        observ.SetWidths(observcellwidth);

                        string Observaciones = r[18].ToString();

                        PdfPCell obs = new PdfPCell(new Phrase("OBSERVACIONES: \n" + " \n \n \n  " + Observaciones + " \n \n \n ", fuente8));
                        obs.HorizontalAlignment = Element.ALIGN_CENTER;
                        obs.BorderColor = BaseColor.BLUE;
                        observ.AddCell(obs);

                        documento.Add(observ);





                        //firmas
                        PdfPTable firmas = new PdfPTable(2);
                        firmas.WidthPercentage = 100f;
                        int[] firmascellwidth = { 62, 38 };
                        firmas.SetWidths(firmascellwidth);

                        PdfPCell encafirm1 = new PdfPCell(new Phrase("PRE AUTORIZACIÓN DE CRÉDITOS EN SUCURSAL", fuente6));
                        encafirm1.HorizontalAlignment = Element.ALIGN_CENTER;
                        encafirm1.BackgroundColor = BaseColor.LIGHT_GRAY;
                        firmas.AddCell(encafirm1);

                        PdfPCell encafirm2 = new PdfPCell(new Phrase("AUTORIZACIÓN DE CRÉDITOS OFICIALES CENTRALES", fuente6));
                        encafirm2.HorizontalAlignment = Element.ALIGN_CENTER;
                        encafirm2.BackgroundColor = BaseColor.LIGHT_GRAY;
                        firmas.AddCell(encafirm2);
                        documento.Add(firmas);



                        //firmas2
                        PdfPTable firmas2 = new PdfPTable(6);
                        firmas2.WidthPercentage = 100f;
                        int[] firmas2cellwidth = { 19, 19, 19, 5, 19, 19 };
                        firmas2.SetWidths(firmas2cellwidth);

                        PdfPCell firm1 = new PdfPCell(new Phrase("\n \n \n ____________________________________ \n \n NOMBRE Y FIRMA \n \n ASESOR DE CRÉDITO \n  ", fuente6));
                        firm1.HorizontalAlignment = Element.ALIGN_CENTER;
                        firm1.BorderColor = BaseColor.BLUE;
                        firmas2.AddCell(firm1);

                        PdfPCell firm2 = new PdfPCell(new Phrase("\n \n \n ____________________________________ \n \n NOMBRE Y FIRMA \n \n GERENTE ADMINISTRATIVA \n ", fuente6));
                        firm2.HorizontalAlignment = Element.ALIGN_CENTER;
                        firm2.BorderColor = BaseColor.BLUE;
                        firmas2.AddCell(firm2);



                        PdfPCell firm4 = new PdfPCell(new Phrase("\n \n \n ____________________________________ \n \n NOMBRE Y FIRMA \n \n GERENTE OPERATIVO \n  ", fuente6));
                        firm4.HorizontalAlignment = Element.ALIGN_CENTER;
                        firm4.BorderColor = BaseColor.BLUE;
                        firmas2.AddCell(firm4);

                        PdfPCell spacio = new PdfPCell(new Phrase(" ", fuente6));
                        spacio.HorizontalAlignment = Element.ALIGN_CENTER;
                        spacio.BorderColor = BaseColor.BLUE;
                        spacio.Border = 0;
                        firmas2.AddCell(spacio);

                        PdfPCell firm3 = new PdfPCell(new Phrase("\n \n \n ____________________________________ \n \n NOMBRE Y FIRMA \n \n  ANALISTA DE RIESGO CREDITICIO \n  ", fuente6));
                        firm3.HorizontalAlignment = Element.ALIGN_CENTER;
                        firm3.BorderColor = BaseColor.BLUE;
                        firmas2.AddCell(firm3);

                        PdfPCell firm5 = new PdfPCell(new Phrase("\n \n \n ____________________________________ \n \n NOMBRE Y FIRMA \n \n GERENTE OPERATIVO \n  ", fuente6));
                        firm5.HorizontalAlignment = Element.ALIGN_CENTER;
                        firm5.BorderColor = BaseColor.BLUE;
                        firmas2.AddCell(firm5);
                        documento.Add(firmas2);

                        //final
                        PdfPTable final = new PdfPTable(1);
                        final.WidthPercentage = 100f;
                        int[] finalcellwidth = { 100 };
                        final.SetWidths(finalcellwidth);

                        PdfPCell txtfinal = new PdfPCell(new Phrase("IMPORTANTE: La solicitud de crédito deber ser llenado con tinta negra, no debe presentar alteraciones, correcciones, tachaduras o enmendaduras.", fuente20));
                        txtfinal.HorizontalAlignment = Element.ALIGN_LEFT;
                        txtfinal.Border = 0;
                        final.AddCell(txtfinal);

                        PdfPCell txtfinal1 = new PdfPCell(new Phrase("Cargo: P= Presidente; S= Secretaria; T= Tesoreria; SP= Supervisora; E=Estatus; A= Antigüo; N= Nuevo; D= Desertor; R= Reingreso; CG= Cambio de Grupo; CD= Causas de deserción; M= Morosidad; C=Conflicto; S= Sobreendeudamiento; MF= Mala fé; CD= Cambio de domicilio; CI= Cambio de institución; O= Otro.", fuente21));
                        txtfinal1.HorizontalAlignment = Element.ALIGN_LEFT;
                        txtfinal1.Border = 0;
                        final.AddCell(txtfinal1);

                        PdfPCell txtfinal2 = new PdfPCell(new Phrase("GN= Giro de Negocio; C= Comercio; I= Industria; S= Servicios; AP= Agropecuario; F= Forestal; P= Pesquero; M= Mineria.", fuente20));
                        txtfinal2.HorizontalAlignment = Element.ALIGN_LEFT;
                        txtfinal2.Border = 0;
                        final.AddCell(txtfinal2);
                        documento.Add(final);


                        documento.Close();



                    }
                }
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
}