using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Telerik.Web.UI;
using E_Utilities;


public partial class MesaControl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       

    }

   

    protected void RadGrid2_SelectedIndexChanged(object sender, EventArgs e)
    {
        MCon obtiene = new MCon();
        int[] sesiones = obtieneSesiones();
        obtiene.empresa = sesiones[2];
        obtiene.sucursal = sesiones[3];
        int id_cliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        obtiene.id_cliente = id_cliente;
        obtiene.existeFicha();
        if (Convert.ToBoolean(obtiene.retorno[0]))
        {
            SqlDataSource3.SelectCommand = "select * from AN_Ficha_Datos  where id_empresa=" + sesiones[2] + " and id_sucursal=" + sesiones[3] + " and id_cliente=" + id_cliente;
        }
        else
        {
            lblErrorDigital.Text = "Error al Autorizar el documento ";
            
        }
        radFichas.Visible = true;

        
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {

    }

    protected void lnkImprimirSolicitud_Click(object sender, EventArgs e)
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


    protected void lnkImprimirActaIntegracion_Click(object sender, EventArgs e)
    {

        
    }

    protected void lnkImprimirConsultaBuro_Click(object sender, EventArgs e)
    {

    }

 


    protected void RadGrid1_SelectedCellChanged(object sender, EventArgs e)
    {
        MCon obtiene = new MCon();
        int[] sesiones = obtieneSesiones();
        obtiene.empresa = sesiones[2];
        obtiene.sucursal = sesiones[3];
        int id_solicitud_credito = Convert.ToInt32(RadGrid1.SelectedValues["id_solicitud_credito"]);
        obtiene.idSolicitudEdita = id_solicitud_credito;
        obtiene.existeIntegrantesSol();
        if (Convert.ToBoolean(obtiene.retorno[0]))
        {
            int Total = Convert.ToInt32(obtiene.retorno[1]);
            obtiene.tieneAMC();
            int ConAut = Convert.ToInt32(obtiene.retorno[1]);
            
            if (Total - ConAut == 0)
            {
                LinkButton boton = FindControl("lnkAutorizaGrup") as LinkButton;
               // boton.Visible = true;
            }
        }
        lnkImprimirSolicitud.Visible = true;
    }

    protected void lnkImprimirFicha_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;

        // Tipo de Font que vamos utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();


        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" Celula del Cliente ");
        documento.AddCreator("DESARROLLARTE");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\CelulaCliente_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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
            logo.WidthPercentage = 15f;


            //encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.SetWidths(new float[] { 2f, 8f });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;


            PdfPCell titulo = new PdfPCell(new Phrase("APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + "CÉDULA DEL CLIENTE", FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD)));
            //PdfPCell titulo = new PdfPCell(new Phrase("Gaarve S.A de C.V. " + Environment.NewLine + Environment.NewLine + " Informe de Nomina " + Environment.NewLine + Environment.NewLine + strDatosObra, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(logo);
            tablaEncabezado.AddCell(titulo);

            documento.Add(tablaEncabezado);

            //documento.Add(new Paragraph(""));

            //DATOS GENERALES
            PdfPTable datGen = new PdfPTable(1);
            datGen.WidthPercentage = 100f;

            PdfPCell datG = new PdfPCell(new Phrase("DATOS GENERALES", fuente8));
            datG.BackgroundColor = BaseColor.LIGHT_GRAY;
            datG.HorizontalAlignment = Element.ALIGN_CENTER;
            datGen.AddCell(datG);
            documento.Add(datGen);

            //datos

            FDat infor = new FDat();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            int idcons = Convert.ToInt32(radFichas.SelectedValues["id_cliente"]);
            int id_fichaD = Convert.ToInt32(radFichas.SelectedValues["id_ficha"]);
            infor.id_cliente = idcons;
            infor.id_ficha = id_fichaD;
            infor.optieneimpresion();



            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    infor.id_cliente = Convert.ToInt32(r[2]);
                    infor.optieneimpresion2();
                    DataSet dn = (DataSet)infor.retorno[1];
                    string nombree, apellidop, apellidom = "";
                    foreach (DataRow rn in dn.Tables[0].Rows)
                    {

                        nombree = Convert.ToString(rn[0]);
                        apellidop = Convert.ToString(rn[1]);
                        apellidom = Convert.ToString(rn[2]);

                        PdfPTable nombre = new PdfPTable(3);
                        nombre.WidthPercentage = 100f;
                        int[] nombrecellwidth = { 30, 30, 40 };
                        nombre.SetWidths(nombrecellwidth);

                        string nom = nombree;

                        PdfPCell apeP = new PdfPCell(new Phrase(" " + apellidop.ToUpper(), fuente8));
                        apeP.HorizontalAlignment = Element.ALIGN_CENTER;
                        nombre.AddCell(apeP);

                        PdfPCell apeM = new PdfPCell(new Phrase(" " + apellidom.ToUpper(), fuente8));
                        apeM.HorizontalAlignment = Element.ALIGN_CENTER;
                        nombre.AddCell(apeM);

                        PdfPCell name = new PdfPCell(new Phrase(" " + nombree.ToUpper(), fuente8));
                        name.HorizontalAlignment = Element.ALIGN_CENTER;
                        nombre.AddCell(name);

                        PdfPCell apeP1 = new PdfPCell(new Phrase("APELLIDO PATERNO", fuente2));
                        apeP1.HorizontalAlignment = Element.ALIGN_CENTER;
                        nombre.AddCell(apeP1);

                        PdfPCell apeM1 = new PdfPCell(new Phrase("APELLIDO MATERNO", fuente2));
                        apeM1.HorizontalAlignment = Element.ALIGN_CENTER;
                        nombre.AddCell(apeM1);

                        PdfPCell name1 = new PdfPCell(new Phrase("NOMBRE (S)", fuente2));
                        name1.HorizontalAlignment = Element.ALIGN_CENTER;
                        nombre.AddCell(name1);
                        documento.Add(nombre);

                    }



                    //fecha de nacimiento
                    PdfPTable fech = new PdfPTable(10);
                    fech.WidthPercentage = 100f;
                    int[] fechcellwidth = { 15, 10, 15, 5, 5, 10, 10, 10, 10, 10 };
                    fech.SetWidths(fechcellwidth);

                    string fechanacim = r[4].ToString();
                    string entidad = r[5].ToString();


                    //SEXO
                    string H = r[6].ToString();
                    string M = r[6].ToString();

                    iTextSharp.text.Font f = new iTextSharp.text.Font();
                    iTextSharp.text.Font f2 = new iTextSharp.text.Font();

                    if (H == "H")
                    {
                        H = "X";
                        f = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        H = " ";
                        f = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (M == "M")
                    {
                        M = "X";
                        f2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else
                    {
                        M = " ";
                        f2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    //ESTADO CIVIL
                    string SOL = r[7].ToString();
                    string CAS = r[7].ToString();
                    string VIU = r[7].ToString();
                    string DIV = r[7].ToString();
                    string UL = r[7].ToString();

                    iTextSharp.text.Font j = new iTextSharp.text.Font();
                    iTextSharp.text.Font j2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font j3 = new iTextSharp.text.Font();
                    iTextSharp.text.Font j4 = new iTextSharp.text.Font();
                    iTextSharp.text.Font j5 = new iTextSharp.text.Font();

                    if (SOL == "SOL")
                    {
                        SOL = "X";
                        j = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        SOL = " ";
                        j = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (CAS == "CAS")
                    {
                        CAS = "X";
                        j2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        CAS = " ";
                        j2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (VIU == "VIU")
                    {
                        VIU = "X";
                        j3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        VIU = " ";
                        j3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (DIV == "DIV")
                    {
                        DIV = "X";
                        j4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        DIV = " ";
                        j4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (UL == "UL")
                    {
                        UL = "X";
                        j5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        UL = " ";
                        j5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }


                    PdfPCell fechN = new PdfPCell(new Phrase("FECHA DE NACIMIENTO (dd/mm/aa)", fuente2));
                    fechN.HorizontalAlignment = Element.ALIGN_CENTER;
                    fechN.BackgroundColor = BaseColor.LIGHT_GRAY;
                    fech.AddCell(fechN);

                    PdfPCell enti = new PdfPCell(new Phrase("ENTIDAD DE NACIMIENTO", fuente2));
                    enti.HorizontalAlignment = Element.ALIGN_CENTER;
                    enti.BackgroundColor = BaseColor.LIGHT_GRAY;
                    fech.AddCell(enti);

                    PdfPCell nacio = new PdfPCell(new Phrase("NACIONALIDAD", fuente2));
                    nacio.HorizontalAlignment = Element.ALIGN_CENTER;
                    nacio.BackgroundColor = BaseColor.LIGHT_GRAY;
                    fech.AddCell(nacio);

                    PdfPCell sexo = (new PdfPCell(new Phrase("SEXO (Marque con una X)", fuente2)) { Colspan = 2 });
                    sexo.HorizontalAlignment = Element.ALIGN_CENTER;
                    sexo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    fech.AddCell(sexo);

                    PdfPCell edoC = (new PdfPCell(new Phrase("ESTADO CIVIL (Marque con una X)", fuente2)) { Colspan = 5 });
                    edoC.HorizontalAlignment = Element.ALIGN_CENTER;
                    edoC.BackgroundColor = BaseColor.LIGHT_GRAY;
                    fech.AddCell(edoC);

                    PdfPCell fechN1 = new PdfPCell(new Phrase(" " + Convert.ToDateTime(fechanacim).ToString("dd/MM/yyyy"), fuente8));
                    fechN1.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(fechN1);

                    PdfPCell enti1 = new PdfPCell(new Phrase(" " + entidad.ToUpper(), fuente8));
                    enti1.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(enti1);

                    PdfPCell nacio1 = new PdfPCell(new Phrase(" ", fuente8));
                    nacio1.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(nacio1);

                    PdfPCell sexoF = new PdfPCell(new Phrase("M  \n " + H, f));
                    sexoF.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(sexoF);

                    PdfPCell sexoM = new PdfPCell(new Phrase("F  \n " + M, f2));
                    sexoM.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(sexoM);

                    PdfPCell edoCS = new PdfPCell(new Phrase("SOLTERO(A)  \n " + SOL, j));
                    edoCS.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(edoCS);

                    PdfPCell edoCC = new PdfPCell(new Phrase("CASADO(A)  \n " + CAS, j2));
                    edoCC.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(edoCC);

                    PdfPCell edoCV = new PdfPCell(new Phrase("VIUDO(A)  \n " + VIU, j3));
                    edoCV.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(edoCV);

                    PdfPCell edoCD = new PdfPCell(new Phrase("DIVORCIADO(A)  \n" + DIV, j4));
                    edoCD.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(edoCD);

                    PdfPCell edoCU = new PdfPCell(new Phrase("UNION LIBRE  \n" + UL, j5));
                    edoCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(edoCU);
                    documento.Add(fech);

                    //TABLA DE CREDENCIALES
                    PdfPTable cred = new PdfPTable(6);
                    cred.WidthPercentage = 100f;
                    int[] credcellwidth = { 15, 25, 12, 25, 5, 18 };
                    cred.SetWidths(credcellwidth);
                    string nocred = r[8].ToString();
                    string curpc = r[9].ToString();
                    string rfcc = r[10].ToString();


                    PdfPCell ine = new PdfPCell(new Phrase("No. CRED. IFE O INE", fuente2));
                    ine.HorizontalAlignment = Element.ALIGN_CENTER;
                    ine.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cred.AddCell(ine);

                    PdfPCell ine1 = new PdfPCell(new Phrase(" " + nocred, fuente8));
                    ine1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cred.AddCell(ine1);

                    PdfPCell curp = new PdfPCell(new Phrase("CURP", fuente2));
                    curp.HorizontalAlignment = Element.ALIGN_CENTER;
                    curp.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cred.AddCell(curp);

                    PdfPCell curp1 = new PdfPCell(new Phrase(" " + curpc, fuente8));
                    curp1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cred.AddCell(curp1);

                    PdfPCell rfc = new PdfPCell(new Phrase("RFC", fuente2));
                    rfc.HorizontalAlignment = Element.ALIGN_CENTER;
                    rfc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cred.AddCell(rfc);

                    PdfPCell rfc1 = new PdfPCell(new Phrase(" " + rfcc, fuente8));
                    rfc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cred.AddCell(rfc1);
                    documento.Add(cred);

                    //NIVEL DE ESCOLARIDAD
                    PdfPTable school = new PdfPTable(10);
                    school.WidthPercentage = 100f;
                    int[] schoolcellwidth = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
                    school.SetWidths(schoolcellwidth);

                    string escolaridad = r[11].ToString();
                    string escolaridad2 = r[11].ToString();
                    string escolaridad3 = r[11].ToString();
                    string escolaridad4 = r[11].ToString();
                    string escolaridad5 = r[11].ToString();
                    string escolaridad6 = r[11].ToString();

                    iTextSharp.text.Font es = new iTextSharp.text.Font();
                    iTextSharp.text.Font es2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font es3 = new iTextSharp.text.Font();
                    iTextSharp.text.Font es4 = new iTextSharp.text.Font();
                    iTextSharp.text.Font es5 = new iTextSharp.text.Font();
                    iTextSharp.text.Font es6 = new iTextSharp.text.Font();

                    if (escolaridad == "SIN")
                    {
                        escolaridad = "X";
                        es = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad = " ";
                        es = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad2 == "PRI")
                    {
                        escolaridad2 = "X";
                        es2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad2 = " ";
                        es2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad3 == "SEC")
                    {
                        escolaridad3 = "X";
                        es3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad3 = " ";
                        es3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }

                    if (escolaridad4 == "BAC")
                    {
                        escolaridad4 = "X";
                        es4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad4 = " ";
                        es4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }

                    if (escolaridad5 == "LIC")
                    {
                        escolaridad5 = "X";
                        es5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad5 = " ";
                        es5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }

                    if (escolaridad6 == "POS")
                    {
                        escolaridad6 = "X";
                        es6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad6 = " ";
                        es6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }


                    string Jefa = r[12].ToString();
                    string Pareja = r[12].ToString();
                    string Hijo = r[12].ToString();

                    iTextSharp.text.Font rol1 = new iTextSharp.text.Font();
                    iTextSharp.text.Font rol2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font rol3 = new iTextSharp.text.Font();

                    if (Jefa == "Jefa(e)")
                    {
                        Jefa = "X";
                        rol1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        Jefa = " ";
                        rol1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }

                    if (Pareja == "Pareja")
                    {
                        Pareja = "X";
                        rol2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        Pareja = " ";
                        rol2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }

                    if (Hijo == "Hijo(a)")
                    {
                        Hijo = "X";
                        rol3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        Hijo = " ";
                        rol3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }


                    PdfPCell escol = (new PdfPCell(new Phrase("NIVEL DE ESCOLARIDAD (Marque con una X) ", fuente2)) { Colspan = 6 });
                    escol.HorizontalAlignment = Element.ALIGN_CENTER;
                    escol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    school.AddCell(escol);

                    PdfPCell ocupa = (new PdfPCell(new Phrase("OCUPACIÓN", fuente2)));
                    ocupa.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocupa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    school.AddCell(ocupa);

                    PdfPCell rol = (new PdfPCell(new Phrase("ROL DEL CLIENTE EN EL HOGAR (Marque con una X)", fuente2)) { Colspan = 6 });
                    rol.HorizontalAlignment = Element.ALIGN_CENTER;
                    rol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    school.AddCell(rol);

                    PdfPCell sinIn = new PdfPCell(new Phrase("SIN INSTRUCCIÓN  \n" + escolaridad, es));
                    sinIn.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(sinIn);

                    PdfPCell prim = new PdfPCell(new Phrase("PRMARIA  \n" + escolaridad2, es2));
                    prim.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(prim);

                    PdfPCell secu = new PdfPCell(new Phrase("SECUANDARIA  \n" + escolaridad3, es3));
                    secu.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(secu);

                    PdfPCell bach = new PdfPCell(new Phrase("BACHILLERATO  \n" + escolaridad4, es4));
                    bach.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(bach);

                    PdfPCell lic = new PdfPCell(new Phrase("LICENCIATURA  \n" + escolaridad5, es5));
                    lic.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(lic);

                    PdfPCell pos = new PdfPCell(new Phrase("POSGRADO  \n" + escolaridad6, es6));
                    pos.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(pos);

                    PdfPCell ocupa1 = new PdfPCell(new Phrase(" ", es6));
                    ocupa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(ocupa1);

                    PdfPCell jef = new PdfPCell(new Phrase("JEFA  \n" + Jefa, rol1));
                    jef.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(jef);

                    PdfPCell pare = new PdfPCell(new Phrase("PAREJA  \n" + Pareja, rol2));
                    pare.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(pare);

                    PdfPCell hijo = new PdfPCell(new Phrase("HIJO(A)  \n" + Hijo, rol3));
                    hijo.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(hijo);
                    documento.Add(school);

                    //taba economica
                    PdfPTable economia = new PdfPTable(5);
                    economia.WidthPercentage = 100f;
                    int[] economiacellwidth = { 15, 15, 25, 30, 15 };
                    economia.SetWidths(economiacellwidth);
                    string hijos = r[13].ToString();
                    string depecon = r[14].ToString();
                    string telfijo = r[15].ToString();
                    string telcel = r[16].ToString();
                    string email = r[17].ToString();

                    PdfPCell noHi = new PdfPCell(new Phrase("No. DE HIJOS", fuente2));
                    noHi.HorizontalAlignment = Element.ALIGN_CENTER;
                    noHi.BackgroundColor = BaseColor.LIGHT_GRAY;
                    economia.AddCell(noHi);

                    PdfPCell depEc = new PdfPCell(new Phrase("DEP. ECONÓMICOS", fuente2));
                    depEc.HorizontalAlignment = Element.ALIGN_CENTER;
                    depEc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    economia.AddCell(depEc);

                    PdfPCell telFi = new PdfPCell(new Phrase("TELÉFONO FIJO (Incluir clave LADA)", fuente2));
                    telFi.HorizontalAlignment = Element.ALIGN_CENTER;
                    telFi.BackgroundColor = BaseColor.LIGHT_GRAY;
                    economia.AddCell(telFi);

                    PdfPCell telCel = new PdfPCell(new Phrase("TELÉFONO CELULAR", fuente2));
                    telCel.HorizontalAlignment = Element.ALIGN_CENTER;
                    telCel.BackgroundColor = BaseColor.LIGHT_GRAY;
                    economia.AddCell(telCel);

                    PdfPCell correo = new PdfPCell(new Phrase("CORREO ELECTRÓNICO", fuente2));
                    correo.HorizontalAlignment = Element.ALIGN_CENTER;
                    correo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    economia.AddCell(correo);

                    PdfPCell noHi1 = new PdfPCell(new Phrase(" " + hijos, fuente8));
                    noHi1.HorizontalAlignment = Element.ALIGN_CENTER;
                    economia.AddCell(noHi1);

                    PdfPCell depEc1 = new PdfPCell(new Phrase(" " + depecon, fuente8));
                    depEc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    economia.AddCell(depEc1);

                    PdfPCell telFi1 = new PdfPCell(new Phrase(" " + telfijo, fuente8));
                    telFi1.HorizontalAlignment = Element.ALIGN_CENTER;
                    economia.AddCell(telFi1);

                    PdfPCell telCel1 = new PdfPCell(new Phrase(" " + telcel, fuente8));
                    telCel1.HorizontalAlignment = Element.ALIGN_CENTER;
                    economia.AddCell(telCel1);

                    PdfPCell correo1 = new PdfPCell(new Phrase(" " + email, fuente8));
                    correo1.HorizontalAlignment = Element.ALIGN_CENTER;
                    economia.AddCell(correo1);
                    documento.Add(economia);

                    //

                    //domicilio
                    PdfPTable domic = new PdfPTable(1);
                    domic.WidthPercentage = 100f;
                    int[] domiccellwidth = { 100 };
                    domic.SetWidths(domiccellwidth);

                    PdfPCell domicilio = new PdfPCell(new Phrase("DOMICILIO", fuente8));
                    domicilio.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicilio.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domic.AddCell(domicilio);
                    documento.Add(domic);

                    //domicilio2.1
                    PdfPTable domici = new PdfPTable(4);
                    domici.WidthPercentage = 100f;
                    int[] domicicellwidth = { 45, 10, 10, 35 };
                    domici.SetWidths(domicicellwidth);
                    string callecl = r[18].ToString();
                    string numext = r[19].ToString();
                    string numint = r[20].ToString();
                    string colonialocal = r[21].ToString();


                    PdfPCell calle = new PdfPCell(new Phrase("CALLE, AVENIDA, ANDADOR, CERRADA, CALLEJO, ETC", fuente2));
                    calle.HorizontalAlignment = Element.ALIGN_CENTER;
                    calle.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domici.AddCell(calle);

                    PdfPCell ext = new PdfPCell(new Phrase("No. EXTERIOR", fuente2));
                    ext.HorizontalAlignment = Element.ALIGN_CENTER;
                    ext.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domici.AddCell(ext);

                    PdfPCell inte = new PdfPCell(new Phrase("No. INTERIOR", fuente2));
                    inte.HorizontalAlignment = Element.ALIGN_CENTER;
                    inte.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domici.AddCell(inte);

                    PdfPCell colo = new PdfPCell(new Phrase("COLONIA O LOCALIDAD", fuente2));
                    colo.HorizontalAlignment = Element.ALIGN_CENTER;
                    colo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domici.AddCell(colo);

                    PdfPCell calle1 = new PdfPCell(new Phrase(" " + callecl.ToUpper(), fuente8));
                    calle1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domici.AddCell(calle1);

                    PdfPCell ext1 = new PdfPCell(new Phrase(" " + numext, fuente8));
                    ext1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domici.AddCell(ext1);

                    PdfPCell inte1 = new PdfPCell(new Phrase(" " + numint, fuente8));
                    inte1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domici.AddCell(inte1);

                    PdfPCell colo1 = new PdfPCell(new Phrase(" " + colonialocal.ToUpper(), fuente8));
                    colo1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domici.AddCell(colo1);

                    documento.Add(domici);

                    //código postal
                    PdfPTable codP = new PdfPTable(6);
                    codP.WidthPercentage = 100f;
                    int[] codPcellwidth = { 10, 25, 20, 15, 15, 15 };
                    codP.SetWidths(codPcellwidth);

                    string postal = r[22].ToString();
                    string delmuni = r[23].ToString();
                    string estadoc = r[24].ToString();
                    string time = r[25].ToString();
                    string numhabita = r[26].ToString();


                    PdfPCell cp = new PdfPCell(new Phrase("CÓDIGO POSTAL", fuente2));
                    cp.HorizontalAlignment = Element.ALIGN_CENTER;
                    cp.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codP.AddCell(cp);

                    PdfPCell delg = new PdfPCell(new Phrase("DELEGACIÓN O MUNICIPIO", fuente2));
                    delg.HorizontalAlignment = Element.ALIGN_CENTER;
                    delg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codP.AddCell(delg);

                    PdfPCell esta = new PdfPCell(new Phrase("ESTADO", fuente2));
                    esta.HorizontalAlignment = Element.ALIGN_CENTER;
                    esta.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codP.AddCell(esta);

                    PdfPCell tipoR = new PdfPCell(new Phrase("TIPO DE DOMICILIO", fuente2));
                    tipoR.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipoR.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codP.AddCell(tipoR);

                    PdfPCell timeR = new PdfPCell(new Phrase("TIEMPO DE RESIDENCIA", fuente2));
                    timeR.HorizontalAlignment = Element.ALIGN_CENTER;
                    timeR.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codP.AddCell(timeR);

                    PdfPCell noHA = new PdfPCell(new Phrase("PERSONAS QUE HABITAN EN EL DOMICILIO", fuente2));
                    noHA.HorizontalAlignment = Element.ALIGN_CENTER;
                    noHA.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codP.AddCell(noHA);

                    PdfPCell cp1 = new PdfPCell(new Phrase(" " + postal, fuente8));
                    cp1.HorizontalAlignment = Element.ALIGN_CENTER;
                    codP.AddCell(cp1);

                    PdfPCell delg1 = new PdfPCell(new Phrase(" " + delmuni.ToUpper(), fuente8));
                    delg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    codP.AddCell(delg1);

                    PdfPCell esta1 = new PdfPCell(new Phrase(" " + estadoc.ToUpper(), fuente8));
                    esta1.HorizontalAlignment = Element.ALIGN_CENTER;
                    codP.AddCell(esta1);

                    PdfPCell tipoR1 = new PdfPCell(new Phrase(" ", fuente8));
                    tipoR1.HorizontalAlignment = Element.ALIGN_CENTER;
                    codP.AddCell(tipoR1);

                    PdfPCell timeR1 = new PdfPCell(new Phrase(" " + time, fuente8));
                    timeR1.HorizontalAlignment = Element.ALIGN_CENTER;
                    codP.AddCell(timeR1);

                    PdfPCell noHA1 = new PdfPCell(new Phrase(" " + numhabita, fuente8));
                    noHA1.HorizontalAlignment = Element.ALIGN_CENTER;
                    codP.AddCell(noHA1);
                    documento.Add(codP);

                    //condición de propiedad
                    PdfPTable condicionP = new PdfPTable(4);
                    condicionP.WidthPercentage = 100f;
                    int[] condicionPcellwidth = { 25, 25, 25, 25 };
                    condicionP.SetWidths(condicionPcellwidth);

                    string PRO = r[27].ToString();
                    string REN = r[27].ToString();
                    string PRE = r[27].ToString();
                    string FAM = r[27].ToString();

                    iTextSharp.text.Font ps = new iTextSharp.text.Font();
                    iTextSharp.text.Font ps2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font ps3 = new iTextSharp.text.Font();
                    iTextSharp.text.Font ps4 = new iTextSharp.text.Font();



                    if (PRO == "PRO")
                    {
                        PRO = "X";
                        ps = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        PRO = " ";
                        ps = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (REN == "REN")
                    {
                        REN = "X";
                        ps2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        REN = " ";
                        ps2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (PRE == "PRE")
                    {
                        PRE = "X";
                        ps3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        PRE = " ";
                        ps3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (FAM == "FAM")
                    {
                        FAM = "X";
                        ps4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        FAM = " ";
                        ps4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    PdfPCell condPro = (new PdfPCell(new Phrase("CONDICIÓN DE PROPIEDAD DEL INMUEBLE (Marque con una X)", fuente2)) { Colspan = 4 });
                    condPro.HorizontalAlignment = Element.ALIGN_CENTER;
                    condPro.BackgroundColor = BaseColor.LIGHT_GRAY;
                    condicionP.AddCell(condPro);

                    PdfPCell propio = new PdfPCell(new Phrase("PROPIO  \n" + PRO, ps));
                    propio.HorizontalAlignment = Element.ALIGN_CENTER;
                    condicionP.AddCell(propio);

                    PdfPCell rentado = new PdfPCell(new Phrase("RENTADO  \n" + REN, ps2));
                    rentado.HorizontalAlignment = Element.ALIGN_CENTER;
                    condicionP.AddCell(rentado);

                    PdfPCell prestado = new PdfPCell(new Phrase("PRESTADO  \n" + PRE, ps3));
                    prestado.HorizontalAlignment = Element.ALIGN_CENTER;
                    condicionP.AddCell(prestado);

                    PdfPCell famili = new PdfPCell(new Phrase("FAMILIAR  \n" + FAM, ps4));
                    famili.HorizontalAlignment = Element.ALIGN_CENTER;
                    condicionP.AddCell(famili);
                    //documento.Add(condicionP);

                    //datos generales
                    PdfPTable datosGen = new PdfPTable(3);
                    datosGen.WidthPercentage = 100f;
                    int[] datosGencellwidth = { 30, 30, 40 };
                    datosGen.SetWidths(datosGencellwidth);
                    string apellidopc = r[28].ToString();
                    string apellidomc = r[29].ToString();
                    string nombresc = r[30].ToString();


                    PdfPCell datosGeneral = (new PdfPCell(new Phrase("DATOS GENENRALES DEL ESPOSO(A) ó CONCUBINO(A)", fuente2)) { Colspan = 3 });
                    datosGeneral.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosGeneral.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datosGen.AddCell(datosGeneral);

                    PdfPCell apelliME = new PdfPCell(new Phrase(" " + apellidopc.ToUpper(), fuente8));
                    apelliME.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosGen.AddCell(apelliME);

                    PdfPCell apelliPE = new PdfPCell(new Phrase(" " + apellidomc.ToUpper(), fuente8));
                    apelliPE.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosGen.AddCell(apelliPE);

                    PdfPCell nameE = new PdfPCell(new Phrase(" " + nombresc.ToUpper(), fuente8));
                    nameE.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosGen.AddCell(nameE);

                    PdfPCell apelliPE1 = new PdfPCell(new Phrase("APELLIDO PATERNO", fuente2));
                    apelliPE1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosGen.AddCell(apelliPE1);

                    PdfPCell apelliME1 = new PdfPCell(new Phrase("APELLIDO MATERNO", fuente2));
                    apelliME1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosGen.AddCell(apelliME1);

                    PdfPCell nameE1 = new PdfPCell(new Phrase("NOMBRE(S)", fuente2));
                    nameE1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosGen.AddCell(nameE1);
                    documento.Add(datosGen);

                    //ocupación
                    PdfPTable ocup = new PdfPTable(4);
                    ocup.WidthPercentage = 100f;
                    int[] ocupcellwidth = { 40, 20, 20, 20 };
                    ocup.SetWidths(ocupcellwidth);
                    string ocpacionc = r[32].ToString();
                    string teltrac = r[33].ToString();
                    string telcasc = r[34].ToString();
                    string telcelc = r[35].ToString();

                    PdfPCell ocupaci = new PdfPCell(new Phrase("OCUPACIÓN", fuente2));
                    ocupaci.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocupaci.BackgroundColor = BaseColor.LIGHT_GRAY;
                    ocup.AddCell(ocupaci);

                    PdfPCell telTra = new PdfPCell(new Phrase("TELÉFONO TRABAJO", fuente2));
                    telTra.HorizontalAlignment = Element.ALIGN_CENTER;
                    telTra.BackgroundColor = BaseColor.LIGHT_GRAY;
                    ocup.AddCell(telTra);

                    PdfPCell telTra1 = new PdfPCell(new Phrase("TELÉFONO CASA", fuente2));
                    telTra1.HorizontalAlignment = Element.ALIGN_CENTER;
                    telTra1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    ocup.AddCell(telTra1);

                    PdfPCell telTra2 = new PdfPCell(new Phrase("TELÉFONO CELULAR", fuente2));
                    telTra2.HorizontalAlignment = Element.ALIGN_CENTER;
                    telTra2.BackgroundColor = BaseColor.LIGHT_GRAY;
                    ocup.AddCell(telTra2);

                    PdfPCell ocupaci1 = new PdfPCell(new Phrase(" " + ocpacionc.ToUpper(), fuente8));
                    ocupaci1.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocup.AddCell(ocupaci1);

                    PdfPCell telTra11 = new PdfPCell(new Phrase(" " + teltrac, fuente8));
                    telTra11.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocup.AddCell(telTra11);

                    PdfPCell telTra12 = new PdfPCell(new Phrase(" " + telcasc, fuente8));
                    telTra12.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocup.AddCell(telTra12);

                    PdfPCell telTra21 = new PdfPCell(new Phrase(" " + telcelc, fuente8));
                    telTra21.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocup.AddCell(telTra21);
                    documento.Add(ocup);

                    //info neg.
                    PdfPTable info = new PdfPTable(1);
                    info.WidthPercentage = 100f;
                    int[] infocellwidth = { 100 };
                    info.SetWidths(infocellwidth);

                    PdfPCell infonego = new PdfPCell(new Phrase("INFORMACIÓN DEL NEGOCIO", fuente8));
                    infonego.HorizontalAlignment = Element.ALIGN_CENTER;
                    infonego.BackgroundColor = BaseColor.LIGHT_GRAY;
                    info.AddCell(infonego);
                    documento.Add(info);

                    //NEGOCIO
                    PdfPTable domicinEG = new PdfPTable(4);
                    domicinEG.WidthPercentage = 100f;
                    int[] domicinEGcellwidth = { 45, 10, 10, 35 };
                    domicinEG.SetWidths(domicinEGcellwidth);
                    string callenc = r[36].ToString();
                    string numextcl = r[37].ToString();
                    string numintecl = r[38].ToString();
                    string coloniac = r[39].ToString();

                    PdfPCell calleNeg = new PdfPCell(new Phrase("CALLE, AVENIDA, ANDADOR, CERRADA, CALLEJO, ETC", fuente2));
                    calleNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    calleNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicinEG.AddCell(calleNeg);

                    PdfPCell extNeg = new PdfPCell(new Phrase("No. EXTERIOR", fuente2));
                    extNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    extNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicinEG.AddCell(extNeg);

                    PdfPCell inteNeg = new PdfPCell(new Phrase("No. EXTERIOR", fuente2));
                    inteNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    inteNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicinEG.AddCell(inteNeg);

                    PdfPCell coloNeg = new PdfPCell(new Phrase("COLONIA O LOCALIDAD", fuente2));
                    coloNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    coloNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicinEG.AddCell(coloNeg);

                    PdfPCell calle1Neg = new PdfPCell(new Phrase(" " + callenc.ToUpper(), fuente8));
                    calle1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicinEG.AddCell(calle1Neg);

                    PdfPCell ext1Neg = new PdfPCell(new Phrase(" " + numextcl, fuente8));
                    ext1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicinEG.AddCell(ext1Neg);

                    PdfPCell inte1Neg = new PdfPCell(new Phrase(" " + numintecl, fuente8));
                    inte1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicinEG.AddCell(inte1Neg);

                    PdfPCell colo1Neg = new PdfPCell(new Phrase(" " + coloniac.ToUpper(), fuente8));
                    colo1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicinEG.AddCell(colo1Neg);
                    documento.Add(domicinEG);

                    //cod postal nego
                    //código postal
                    PdfPTable codPNeg = new PdfPTable(5);
                    codPNeg.WidthPercentage = 100f;
                    int[] codPNegcellwidth = { 15, 30, 20, 20, 15 };
                    codPNeg.SetWidths(codPNegcellwidth);
                    string codpostal = r[40].ToString();
                    string delegacimuni = r[41].ToString();
                    string estadoclie = r[42].ToString();
                    string telfijocli = r[43].ToString();
                    string antiguedadcli = r[44].ToString();

                    PdfPCell cpNeg = new PdfPCell(new Phrase("CÓDIGO POSTAL", fuente2));
                    cpNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    cpNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codPNeg.AddCell(cpNeg);

                    PdfPCell delgNeg = new PdfPCell(new Phrase("DELEGACIÓN O MUNICIPIO", fuente2));
                    delgNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    delgNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codPNeg.AddCell(delgNeg);

                    PdfPCell estaNeg = new PdfPCell(new Phrase("ESTADO", fuente2));
                    estaNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    estaNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codPNeg.AddCell(estaNeg);

                    PdfPCell timeRNeg = new PdfPCell(new Phrase("TELÉFONO FIJO", fuente2));
                    timeRNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    timeRNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codPNeg.AddCell(timeRNeg);

                    PdfPCell noHANeg = new PdfPCell(new Phrase("ANTIGÜEDAD", fuente2));
                    noHANeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    noHANeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codPNeg.AddCell(noHANeg);

                    PdfPCell cp1Neg = new PdfPCell(new Phrase(" " + codpostal, fuente8));
                    cp1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    codPNeg.AddCell(cp1Neg);

                    PdfPCell delg1Neg = new PdfPCell(new Phrase(" " + delegacimuni.ToUpper(), fuente8));
                    delg1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    codPNeg.AddCell(delg1Neg);

                    PdfPCell esta1Neg = new PdfPCell(new Phrase(" " + estadoclie.ToUpper(), fuente8));
                    esta1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    codPNeg.AddCell(esta1Neg);

                    PdfPCell timeR1Neg = new PdfPCell(new Phrase(" " + telfijocli, fuente8));
                    timeR1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    codPNeg.AddCell(timeR1Neg);

                    PdfPCell noHA1Neg = new PdfPCell(new Phrase(" " + antiguedadcli, fuente8));
                    noHA1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    codPNeg.AddCell(noHA1Neg);
                    documento.Add(codPNeg);

                    //razón social
                    PdfPTable razS = new PdfPTable(8);
                    razS.WidthPercentage = 100f;
                    int[] razScellwidth = { 35, 10, 10, 15, 10, 5, 10, 5 };
                    razS.SetWidths(razScellwidth);
                    string razonsocial = r[45].ToString();

                    string Fijo = r[46].ToString();
                    string Semifijo = r[46].ToString();
                    string Ambulante = r[46].ToString();

                    iTextSharp.text.Font pt = new iTextSharp.text.Font();
                    iTextSharp.text.Font pt2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font pt3 = new iTextSharp.text.Font();



                    if (Fijo == "Fijo")
                    {
                        Fijo = "X";
                        pt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        Fijo = " ";
                        pt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (Semifijo == "Semifijo")
                    {
                        Semifijo = "X";
                        pt2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        Semifijo = " ";
                        pt2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (Ambulante == "Ambulante")
                    {
                        Ambulante = "X";
                        pt3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        Ambulante = " ";
                        pt3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    string numempleP = r[47].ToString();
                    string numempleE = r[48].ToString();

                    PdfPCell nameRaz = new PdfPCell(new Phrase("NOMBRE O RAZÓN SOCIAL DEL NEGOCIO", fuente2));
                    nameRaz.HorizontalAlignment = Element.ALIGN_CENTER;
                    nameRaz.BackgroundColor = BaseColor.LIGHT_GRAY;
                    razS.AddCell(nameRaz);

                    PdfPCell tipEst = (new PdfPCell(new Phrase("TIPO DE ESTABLECIMIENTO (Marque con una X)", fuente2)) { Colspan = 3 });
                    tipEst.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipEst.BackgroundColor = BaseColor.LIGHT_GRAY;
                    razS.AddCell(tipEst);

                    PdfPCell NoEmp = (new PdfPCell(new Phrase("No. DE EMPLEOS", fuente2)) { Colspan = 4 });
                    NoEmp.HorizontalAlignment = Element.ALIGN_CENTER;
                    NoEmp.BackgroundColor = BaseColor.LIGHT_GRAY;
                    razS.AddCell(NoEmp);

                    PdfPCell nomRazS1 = new PdfPCell(new Phrase(" " + razonsocial.ToUpper(), fuente8));
                    nomRazS1.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(nomRazS1);

                    PdfPCell fijo = new PdfPCell(new Phrase("FIJO  \n" + Fijo, pt));
                    fijo.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(fijo);

                    PdfPCell semi = new PdfPCell(new Phrase("SEMIFIJO  \n" + Semifijo, pt2));
                    semi.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(semi);

                    PdfPCell ambu = new PdfPCell(new Phrase("AMBULANTE  \n" + Ambulante, pt3));
                    ambu.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(ambu);

                    PdfPCell perma = new PdfPCell(new Phrase("PERMANENTE", fuente2));
                    perma.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(perma);

                    PdfPCell perma1 = new PdfPCell(new Phrase(" " + numempleP, fuente8));
                    perma1.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(perma1);

                    PdfPCell even = new PdfPCell(new Phrase("EVENTUALES", fuente2));
                    even.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(even);

                    PdfPCell even1 = new PdfPCell(new Phrase(" " + numempleE, fuente8));
                    even1.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(even1);
                    documento.Add(razS);

                    //grupo principa
                    PdfPTable grupP = new PdfPTable(4);
                    grupP.WidthPercentage = 100f;
                    int[] grupPcellwidth = { 15, 35, 15, 35 };
                    grupP.SetWidths(grupPcellwidth);

                    string giroprinci = r[49].ToString();
                    string ingrem = r[50].ToString();
                    string otraact = r[51].ToString();
                    string ingrems = r[52].ToString();

                    PdfPCell gruPrin = new PdfPCell(new Phrase("GIRO PRINCIPAL", fuente2));
                    gruPrin.HorizontalAlignment = Element.ALIGN_CENTER;
                    gruPrin.BackgroundColor = BaseColor.LIGHT_GRAY;
                    grupP.AddCell(gruPrin);

                    PdfPCell gruPrin1 = new PdfPCell(new Phrase(" " + giroprinci.ToUpper(), fuente8));
                    gruPrin1.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupP.AddCell(gruPrin1);

                    PdfPCell ingM = new PdfPCell(new Phrase("INGRESO MENSUAL", fuente2));
                    ingM.HorizontalAlignment = Element.ALIGN_CENTER;
                    ingM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    grupP.AddCell(ingM);

                    PdfPCell ingM1 = new PdfPCell(new Phrase(" " + ingrem, fuente8));
                    ingM1.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupP.AddCell(ingM1);

                    PdfPCell otrasA = new PdfPCell(new Phrase("OTRAS ACTIVIDADES", fuente2));
                    otrasA.HorizontalAlignment = Element.ALIGN_CENTER;
                    otrasA.BackgroundColor = BaseColor.LIGHT_GRAY;
                    grupP.AddCell(otrasA);

                    PdfPCell otrasA1 = new PdfPCell(new Phrase(" " + otraact.ToUpper(), fuente8));
                    otrasA1.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupP.AddCell(otrasA1);

                    PdfPCell ingMO = new PdfPCell(new Phrase("INGRESO MENSUAL", fuente2));
                    ingMO.HorizontalAlignment = Element.ALIGN_CENTER;
                    ingMO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    grupP.AddCell(ingMO);

                    PdfPCell ingMO1 = new PdfPCell(new Phrase(" " + ingrems, fuente8));
                    ingMO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupP.AddCell(ingMO1);
                    documento.Add(grupP);
                    documento.Add(new Paragraph(" "));

                    //titulo del croquis
                    PdfPTable cro = new PdfPTable(2);
                    cro.WidthPercentage = 100f;
                    int[] crocellwidth = { 50, 50 };
                    cro.SetWidths(crocellwidth);

                    PdfPCell croDC = new PdfPCell(new Phrase("CROQUIS DE UBICACIÓN DEL DOMICILIO DEL CLIENTE", fuente2));
                    croDC.HorizontalAlignment = Element.ALIGN_CENTER;
                    croDC.Border = 0;
                    cro.AddCell(croDC);

                    PdfPCell croNC = new PdfPCell(new Phrase("CROQUIS DE UBICACIÓN DEL DOMICILIO DEL CLIENTE", fuente2));
                    croNC.HorizontalAlignment = Element.ALIGN_CENTER;
                    croNC.Border = 0;
                    cro.AddCell(croNC);
                    documento.Add(cro);

                    //imagen de croquis
                    PdfPTable croquisImg = new PdfPTable(1);
                    croquisImg.WidthPercentage = 100f;

                    string imagepath1 = HttpContext.Current.Server.MapPath("img/");
                    iTextSharp.text.Image croquis = iTextSharp.text.Image.GetInstance(imagepath1 + "croquis.png");

                    croquisImg.AddCell(croquis);
                    croquis.Border = 0;
                    documento.Add(croquisImg);

                    //pie de croquis
                    PdfPTable regisName = new PdfPTable(1);
                    regisName.WidthPercentage = 80f;
                    regisName.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell reName = new PdfPCell(new Phrase("REGISTRE EL NOMBRE DE LAS PRINCIPALES CALLES, AVENIDAS, ANDADORES, CERRADAS QUE COLINDAN CON LA VIVIENDA Y EL NEGOCIO DEL CLIENTE", fuente2));
                    reName.HorizontalAlignment = Element.ALIGN_CENTER;
                    reName.Border = 0;
                    regisName.AddCell(reName);
                    documento.Add(regisName);

                    //referencias pincipales
                    PdfPTable ubiRe = new PdfPTable(2);
                    ubiRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    ubiRe.WidthPercentage = 100f;

                    PdfPCell refUbi = (new PdfPCell(new Phrase("PRINCIPALES REFERENCIAS DE UBICACIÓN", fuente2)));
                    refUbi.HorizontalAlignment = Element.ALIGN_CENTER;
                    ubiRe.AddCell(refUbi);

                    PdfPCell refUbi1 = (new PdfPCell(new Phrase("PRINCIPALES REFERENCIAS DE UBICACIÓN", fuente2)));
                    refUbi1.HorizontalAlignment = Element.ALIGN_CENTER;
                    ubiRe.AddCell(refUbi1);

                    PdfPCell refUbi2 = (new PdfPCell(new Phrase(" \n \n \n", fuente6)) { Rowspan = 5 });
                    refUbi2.HorizontalAlignment = Element.ALIGN_CENTER;
                    ubiRe.AddCell(refUbi2);

                    PdfPCell refUbi3 = (new PdfPCell(new Phrase(" \n \n \n ", fuente6)) { Rowspan = 5 });
                    refUbi3.HorizontalAlignment = Element.ALIGN_CENTER;
                    ubiRe.AddCell(refUbi3);
                    documento.Add(ubiRe);






                    //referencias personales (llenado de datos
                    PdfPTable refeCli = new PdfPTable(7);
                    refeCli.WidthPercentage = 100f;
                    int[] refeClicellwidth = { 3, 27, 15, 15, 10, 10, 20 };
                    refeCli.SetWidths(refeClicellwidth);
                    string nombreref = r[53].ToString();
                    string telfijoclrf = r[54].ToString();
                    string telceluref = r[55].ToString();
                    string parentesco = r[56].ToString();
                    string timeconocerl = r[57].ToString();

                    PdfPCell encaRefe = (new PdfPCell(new Phrase("REFERENCIAS DEL CLIENTE (PERSONALES Y FAMILIARES", fuente8)) { Colspan = 7 });
                    encaRefe.HorizontalAlignment = Element.ALIGN_CENTER;
                    encaRefe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    refeCli.AddCell(encaRefe);


                    PdfPCell NoRefe = new PdfPCell(new Phrase("No.", fuente6));
                    NoRefe.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(NoRefe);

                    PdfPCell NameRef = new PdfPCell(new Phrase("NOMBRE COMPLETO", fuente2));
                    NameRef.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(NameRef);

                    PdfPCell telRef = new PdfPCell(new Phrase("TELÉFONO FIJO (Incluir clave LADA)", fuente2));
                    telRef.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(telRef);

                    PdfPCell celRef = new PdfPCell(new Phrase("TELÉFONO CELULAR", fuente2));
                    celRef.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(celRef);

                    PdfPCell pareRef = new PdfPCell(new Phrase("PARENTESCO O RELACIÓN", fuente2));
                    pareRef.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(pareRef);

                    PdfPCell timeRef = new PdfPCell(new Phrase("TIEMPO DE CONOCERLO", fuente2));
                    timeRef.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(timeRef);

                    PdfPCell correoRef = new PdfPCell(new Phrase("CORREO ELECTRÓNICO", fuente2));
                    correoRef.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(correoRef);

                    PdfPCell NoRefe1 = new PdfPCell(new Phrase("1", fuente6));
                    NoRefe1.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(NoRefe1);

                    PdfPCell NameRef1 = new PdfPCell(new Phrase(" " + nombreref.ToUpper(), fuente8));
                    NameRef1.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(NameRef1);

                    PdfPCell telRef1 = new PdfPCell(new Phrase(" " + telfijoclrf, fuente8));
                    telRef1.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(telRef1);

                    PdfPCell celRef1 = new PdfPCell(new Phrase(" " + telceluref, fuente8));
                    celRef1.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(celRef1);

                    PdfPCell pareRef1 = new PdfPCell(new Phrase(" " + parentesco.ToUpper(), fuente8));
                    pareRef1.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(pareRef1);

                    PdfPCell timeRef1 = new PdfPCell(new Phrase(" " + timeconocerl, fuente8));
                    timeRef1.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(timeRef1);

                    PdfPCell NoRefe2 = new PdfPCell(new Phrase("2", fuente6));
                    NoRefe2.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(NoRefe2);

                    PdfPCell NameRef2 = new PdfPCell(new Phrase(" ", fuente6));
                    NameRef2.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(NameRef2);

                    PdfPCell telRef2 = new PdfPCell(new Phrase(" ", fuente6));
                    telRef2.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(telRef2);

                    PdfPCell celRef2 = new PdfPCell(new Phrase(" ", fuente6));
                    celRef2.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(celRef2);

                    PdfPCell pareRef2 = new PdfPCell(new Phrase(" ", fuente6));
                    pareRef2.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(pareRef2);

                    PdfPCell timeRef2 = new PdfPCell(new Phrase(" ", fuente6));
                    timeRef2.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(timeRef2);
                    documento.Add(refeCli);

                    PdfPTable benSeg = new PdfPTable(3);
                    benSeg.WidthPercentage = 100f;
                    int[] benSegcellwidth = { 30, 30, 40 };
                    benSeg.SetWidths(benSegcellwidth);

                    PdfPCell benefSeg = (new PdfPCell(new Phrase("BENEFICIARIO DEL SEGURO", fuente3)) { Colspan = 3 });
                    benefSeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    benefSeg.VerticalAlignment = Element.ALIGN_MIDDLE;
                    benefSeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    benSeg.AddCell(benefSeg);

                    PdfPCell apelliPSeg = new PdfPCell(new Phrase(" \n ", fuente6));
                    apelliPSeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    benSeg.AddCell(apelliPSeg);

                    PdfPCell apelliMSeg = new PdfPCell(new Phrase(" \n ", fuente6));
                    apelliMSeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    benSeg.AddCell(apelliMSeg);

                    PdfPCell nomSeg = new PdfPCell(new Phrase(" \n ", fuente6));
                    nomSeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    benSeg.AddCell(nomSeg);


                    PdfPCell apelliPSeg1 = new PdfPCell(new Phrase("APELLIDO PATERNO", fuente3));
                    apelliPSeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    benSeg.AddCell(apelliPSeg1);

                    PdfPCell apelliMSeg1 = new PdfPCell(new Phrase("APELLIDO MATERNO", fuente3));
                    apelliMSeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    benSeg.AddCell(apelliMSeg1);

                    PdfPCell nomSeg1 = new PdfPCell(new Phrase("NOMBRE(S)", fuente3));
                    nomSeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    benSeg.AddCell(nomSeg1);
                    documento.Add(benSeg);


                    PdfPTable datBen = new PdfPTable(5);
                    datBen.WidthPercentage = 100f;
                    int[] datBencellwidth = { 30, 10, 10, 35, 15 };
                    datBen.SetWidths(datBencellwidth);

                    PdfPCell datosBenf = (new PdfPCell(new Phrase("CALLE, AVENIDA, ANDADOR, CERRADA, CALLEJON, ETC.", fuente3)));
                    datosBenf.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosBenf.VerticalAlignment = Element.ALIGN_MIDDLE;
                    datosBenf.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datBen.AddCell(datosBenf);

                    PdfPCell extBen = (new PdfPCell(new Phrase("No. EXTERIOR", fuente3)));
                    extBen.HorizontalAlignment = Element.ALIGN_CENTER;
                    extBen.VerticalAlignment = Element.ALIGN_MIDDLE;
                    extBen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datBen.AddCell(extBen);

                    PdfPCell intBen = (new PdfPCell(new Phrase("No. INTERIOR", fuente3)));
                    intBen.HorizontalAlignment = Element.ALIGN_CENTER;
                    intBen.VerticalAlignment = Element.ALIGN_MIDDLE;
                    intBen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datBen.AddCell(intBen);

                    PdfPCell colBen = (new PdfPCell(new Phrase("COLONIA O LOCALIDAD", fuente3)));
                    colBen.HorizontalAlignment = Element.ALIGN_CENTER;
                    colBen.VerticalAlignment = Element.ALIGN_MIDDLE;
                    colBen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datBen.AddCell(colBen);

                    PdfPCell telBen = (new PdfPCell(new Phrase("TELÉFONO", fuente3)));
                    telBen.HorizontalAlignment = Element.ALIGN_CENTER;
                    telBen.VerticalAlignment = Element.ALIGN_MIDDLE;
                    telBen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datBen.AddCell(telBen);

                    PdfPCell datosBenf1 = (new PdfPCell(new Phrase(" ", fuente3)));
                    datosBenf1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosBenf1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    datBen.AddCell(datosBenf1);

                    PdfPCell extBen1 = (new PdfPCell(new Phrase(" ", fuente3)));
                    extBen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    extBen1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    datBen.AddCell(extBen1);

                    PdfPCell intBen1 = (new PdfPCell(new Phrase(" ", fuente3)));
                    intBen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    intBen1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    datBen.AddCell(intBen1);

                    PdfPCell colBen1 = (new PdfPCell(new Phrase(" ", fuente3)));
                    colBen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    colBen1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    datBen.AddCell(colBen1);

                    PdfPCell telBen1 = (new PdfPCell(new Phrase(" ", fuente3)));
                    telBen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    telBen1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    datBen.AddCell(telBen1);
                    documento.Add(datBen);



                    documento.NewPage();

                    //tabla 2° hoja
                    PdfPTable SegHo = new PdfPTable(5);
                    SegHo.WidthPercentage = 100f;
                    int[] SegHocellwidth = { 5, 5, 35, 35, 20 };
                    SegHo.SetWidths(SegHocellwidth);


                    // string sicli = r[61].ToString();
                    // string nocli = r[61].ToString();
                    string cargo = r[59].ToString();
                    string dependencia = r[60].ToString();
                    string periodo = r[61].ToString();


                    PdfPCell ocupado = (new PdfPCell(new Phrase("¿USTED HA OCUPADO CARGOS PÚBLICOS DESTACADOS EN LOS ÚLTIMOS DOCE MESES?", fuente6)) { Colspan = 5 });
                    ocupado.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocupado.BackgroundColor = BaseColor.LIGHT_GRAY;
                    SegHo.AddCell(ocupado);

                    PdfPCell persPol = (new PdfPCell(new Phrase("(Pesona políticamente expuesta, entre otros: Jefe de estado, de Gobierno, Líder Politico, Senador, Diputado, Presidente Municipal, miembro del Partido político, Judicial o Militar de cualquier gerarquía", fuente10)) { Colspan = 5, });
                    persPol.HorizontalAlignment = Element.ALIGN_CENTER;
                    SegHo.AddCell(persPol);

                    PdfPCell no = new PdfPCell(new Phrase("NO", fuente2));
                    no.HorizontalAlignment = Element.ALIGN_CENTER;
                    no.BackgroundColor = BaseColor.LIGHT_GRAY;
                    SegHo.AddCell(no);

                    PdfPCell si = new PdfPCell(new Phrase("SI", fuente2));
                    si.HorizontalAlignment = Element.ALIGN_CENTER;
                    si.BackgroundColor = BaseColor.LIGHT_GRAY;
                    SegHo.AddCell(si);

                    PdfPCell carDes = new PdfPCell(new Phrase("CARGO DESEMPEÑADO", fuente2));
                    carDes.HorizontalAlignment = Element.ALIGN_CENTER;
                    carDes.BackgroundColor = BaseColor.LIGHT_GRAY;
                    SegHo.AddCell(carDes);

                    PdfPCell depen = new PdfPCell(new Phrase("DEPENDENCIA", fuente2));
                    depen.HorizontalAlignment = Element.ALIGN_CENTER;
                    depen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    SegHo.AddCell(depen);

                    PdfPCell perio = new PdfPCell(new Phrase("PERIODO", fuente2));
                    perio.HorizontalAlignment = Element.ALIGN_CENTER;
                    perio.BackgroundColor = BaseColor.LIGHT_GRAY;
                    SegHo.AddCell(perio);

                    PdfPCell no1 = new PdfPCell(new Phrase(" ", fuente6));
                    no1.HorizontalAlignment = Element.ALIGN_CENTER;
                    SegHo.AddCell(no1);

                    PdfPCell si1 = new PdfPCell(new Phrase(" ", fuente6));
                    si1.HorizontalAlignment = Element.ALIGN_CENTER;
                    SegHo.AddCell(si1);

                    PdfPCell carDes1 = new PdfPCell(new Phrase(" " + cargo.ToUpper(), fuente8));
                    carDes1.HorizontalAlignment = Element.ALIGN_CENTER;
                    SegHo.AddCell(carDes1);

                    PdfPCell depen1 = new PdfPCell(new Phrase(" " + dependencia, fuente8));
                    depen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    SegHo.AddCell(depen1);

                    PdfPCell perio1 = new PdfPCell(new Phrase(" " + periodo, fuente8));
                    perio1.HorizontalAlignment = Element.ALIGN_CENTER;
                    SegHo.AddCell(perio1);

                    PdfPCell sobri1 = (new PdfPCell(new Phrase("ESPOSO(A), CONCUBINO(A), MADRE, PADRE, ABUELO(A), HIJA(O), NIETO, HERMANO, SOBRINO, CUÑADO", fuente2)) { Colspan = 5 });
                    sobri1.HorizontalAlignment = Element.ALIGN_LEFT;
                    SegHo.AddCell(sobri1);
                    documento.Add(SegHo);

                    //FAMILIAR
                    PdfPTable family = new PdfPTable(4);
                    family.WidthPercentage = 100f;
                    int[] familycellwidth = { 5, 5, 50, 50 };
                    family.SetWidths(familycellwidth);

                    string nomfami = r[63].ToString();
                    string paren = r[64].ToString();

                    PdfPCell noFa = new PdfPCell(new Phrase("NO", fuente2));
                    noFa.HorizontalAlignment = Element.ALIGN_CENTER;
                    noFa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    family.AddCell(noFa);

                    PdfPCell siFa = new PdfPCell(new Phrase("SI", fuente2));
                    siFa.HorizontalAlignment = Element.ALIGN_CENTER;
                    siFa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    family.AddCell(siFa);

                    PdfPCell nameFa = new PdfPCell(new Phrase("NOMBRE FAMILIAR", fuente2));
                    nameFa.HorizontalAlignment = Element.ALIGN_CENTER;
                    nameFa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    family.AddCell(nameFa);

                    PdfPCell pareFa = new PdfPCell(new Phrase("PARENTESCO", fuente2));
                    pareFa.HorizontalAlignment = Element.ALIGN_CENTER;
                    pareFa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    family.AddCell(pareFa);

                    PdfPCell noFa1 = new PdfPCell(new Phrase(" ", fuente6));
                    noFa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    family.AddCell(noFa1);

                    PdfPCell siFa1 = new PdfPCell(new Phrase(" ", fuente6));
                    siFa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    family.AddCell(siFa1);

                    PdfPCell nameFa1 = new PdfPCell(new Phrase(" " + nomfami.ToUpper(), fuente8));
                    nameFa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    family.AddCell(nameFa1);

                    PdfPCell pareFa1 = new PdfPCell(new Phrase(" " + paren.ToUpper(), fuente8));
                    pareFa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    family.AddCell(pareFa1);
                    documento.Add(family);

                    //cargo desempeñado
                    PdfPTable carDe = new PdfPTable(3);
                    carDe.WidthPercentage = 100f;
                    int[] carDecellwidth = { 40, 40, 20 };
                    carDe.SetWidths(carDecellwidth);

                    string cargodes = r[65].ToString();
                    string dep = r[66].ToString();
                    string per = r[67].ToString();

                    PdfPCell cargoDesem = new PdfPCell(new Phrase("CARGO DEPEMPEÑADO", fuente2));
                    cargoDesem.HorizontalAlignment = Element.ALIGN_CENTER;
                    cargoDesem.BackgroundColor = BaseColor.LIGHT_GRAY;
                    carDe.AddCell(cargoDesem);

                    PdfPCell depenDen = new PdfPCell(new Phrase("DEPENDENCIA", fuente2));
                    depenDen.HorizontalAlignment = Element.ALIGN_CENTER;
                    depenDen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    carDe.AddCell(depenDen);

                    PdfPCell periodoDe = new PdfPCell(new Phrase("PERIODO", fuente2));
                    periodoDe.HorizontalAlignment = Element.ALIGN_CENTER;
                    periodoDe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    carDe.AddCell(periodoDe);

                    PdfPCell cargoDesem1 = new PdfPCell(new Phrase(" ", fuente2));
                    cargoDesem1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cargoDesem1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    carDe.AddCell(cargoDesem1);

                    PdfPCell depenDen1 = new PdfPCell(new Phrase(" ", fuente2));
                    depenDen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    depenDen1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    carDe.AddCell(depenDen1);

                    PdfPCell periodoDe1 = new PdfPCell(new Phrase(" ", fuente2));
                    periodoDe1.HorizontalAlignment = Element.ALIGN_CENTER;
                    periodoDe1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    carDe.AddCell(periodoDe1);
                    documento.Add(carDe);



                    //INFORMACIÓN REFERENTE AL PROPIETARIO
                    PdfPTable infoRefP = new PdfPTable(3);
                    infoRefP.WidthPercentage = 100f;
                    int[] infoRefPcellwidth = { 20, 20, 60 };
                    infoRefP.SetWidths(infoRefPcellwidth);

                    string apellipa = r[68].ToString();
                    string apellima = r[69].ToString();
                    string nombrs = r[70].ToString();

                    PdfPCell encaRefP = (new PdfPCell(new Phrase("INFORMACIÓN REFERENTE AL PROPIETARIO REAL", fuente6)) { Colspan = 3 });
                    encaRefP.HorizontalAlignment = Element.ALIGN_CENTER;
                    encaRefP.BackgroundColor = BaseColor.LIGHT_GRAY;
                    infoRefP.AddCell(encaRefP);

                    PdfPCell apelliParR = new PdfPCell(new Phrase(" " + apellipa.ToUpper(), fuente8));
                    apelliParR.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRefP.AddCell(apelliParR);

                    PdfPCell apelliMarR = new PdfPCell(new Phrase(" " + apellima.ToUpper(), fuente8));
                    apelliMarR.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRefP.AddCell(apelliMarR);

                    PdfPCell nameRePr = new PdfPCell(new Phrase(" " + nombrs.ToUpper(), fuente8));
                    nameRePr.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRefP.AddCell(nameRePr);

                    PdfPCell apelliParR1 = new PdfPCell(new Phrase("APELLIDO PATERNO", fuente2));
                    apelliParR1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRefP.AddCell(apelliParR1);

                    PdfPCell apelliMarR1 = new PdfPCell(new Phrase("APELLIDO MATERNO", fuente2));
                    apelliMarR1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRefP.AddCell(apelliMarR1);

                    PdfPCell nameRePr1 = new PdfPCell(new Phrase("NOMBRE(S)", fuente2));
                    nameRePr1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRefP.AddCell(nameRePr1);
                    documento.Add(infoRefP);

                    //datos generales
                    PdfPTable datGeRe = new PdfPTable(10);
                    datGeRe.WidthPercentage = 100f;
                    int[] datGeRecellwidth = { 20, 18, 13, 2, 2, 9, 9, 9, 9, 9 };
                    datGeRe.SetWidths(datGeRecellwidth);

                    string fechanac = r[72].ToString();
                    string entidnacimi = r[73].ToString();
                    string nacionali = r[74].ToString();

                    string H1 = r[75].ToString();
                    string M1 = r[75].ToString();

                    iTextSharp.text.Font xb = new iTextSharp.text.Font();
                    iTextSharp.text.Font xb2 = new iTextSharp.text.Font();

                    if (H1 == "H")
                    {
                        H1 = "X";
                        xb = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        H1 = " ";
                        xb = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (M1 == "M")
                    {
                        M1 = "X";
                        xb2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else
                    {
                        M1 = " ";
                        xb2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    string SOL1 = r[76].ToString();
                    string CAS1 = r[76].ToString();
                    string VIU1 = r[76].ToString();
                    string DIV1 = r[76].ToString();
                    string UL1 = r[76].ToString();

                    iTextSharp.text.Font PUT = new iTextSharp.text.Font();
                    iTextSharp.text.Font PUT2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font PUT3 = new iTextSharp.text.Font();
                    iTextSharp.text.Font PUT4 = new iTextSharp.text.Font();
                    iTextSharp.text.Font PUT5 = new iTextSharp.text.Font();


                    if (SOL1 == "SOL")
                    {
                        SOL1 = "X";
                        PUT = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        SOL1 = " ";
                        PUT = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (CAS1 == "CAS")
                    {
                        CAS1 = "X";
                        PUT2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        CAS1 = " ";
                        PUT2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (VIU1 == "VIU")
                    {
                        VIU1 = "X";
                        PUT3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        VIU1 = " ";
                        PUT3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (DIV1 == "DIV")
                    {
                        DIV1 = "X";
                        PUT4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        DIV1 = " ";
                        PUT4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (UL1 == "UL")
                    {
                        UL1 = "X";
                        PUT5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        UL1 = " ";
                        PUT5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }




                    PdfPCell fechNaR = new PdfPCell(new Phrase("FECHA DE NACIMIENTO(dd/mm/aa)", fuente2));
                    fechNaR.HorizontalAlignment = Element.ALIGN_CENTER;
                    fechNaR.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGeRe.AddCell(fechNaR);

                    PdfPCell entiNaR = new PdfPCell(new Phrase("ENTIDAD DE NACIMIENTO", fuente2));
                    entiNaR.HorizontalAlignment = Element.ALIGN_CENTER;
                    entiNaR.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGeRe.AddCell(entiNaR);

                    PdfPCell naciRe = new PdfPCell(new Phrase("NACIONALIDAD", fuente2));
                    naciRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    naciRe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGeRe.AddCell(naciRe);

                    PdfPCell sexRe = (new PdfPCell(new Phrase("SEXO", fuente2)) { Colspan = 2 });
                    sexRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    sexRe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGeRe.AddCell(sexRe);

                    PdfPCell estCiRe = (new PdfPCell(new Phrase("ESTADO CIVIL (Marque con una X)", fuente2)) { Colspan = 5 });
                    estCiRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    estCiRe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGeRe.AddCell(estCiRe);

                    PdfPCell fechNaR1 = new PdfPCell(new Phrase(" " + Convert.ToDateTime(fechanac).ToString("dd/MM/yyyy"), fuente8));
                    fechNaR1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(fechNaR1);

                    PdfPCell entiNaR1 = new PdfPCell(new Phrase(" " + entidnacimi.ToUpper(), fuente8));
                    entiNaR1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(entiNaR1);

                    PdfPCell naciRe1 = new PdfPCell(new Phrase(" " + nacionali.ToUpper(), fuente8));
                    naciRe1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(naciRe1);

                    PdfPCell sexRe1 = new PdfPCell(new Phrase("M" + M1, xb2));
                    sexRe1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(sexRe1);

                    PdfPCell sexRe2 = new PdfPCell(new Phrase("H" + H1, xb));
                    sexRe2.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(sexRe2);

                    PdfPCell estCiRe1 = new PdfPCell(new Phrase("SOLTERO(A)  \n" + SOL1, PUT));
                    estCiRe1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(estCiRe1);

                    PdfPCell estCiRe2 = new PdfPCell(new Phrase("CASADO(A)  \n" + CAS1, PUT2));
                    estCiRe2.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(estCiRe2);

                    PdfPCell estCiRe3 = new PdfPCell(new Phrase("VIUDO(A)  \n" + VIU1, PUT3));
                    estCiRe3.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(estCiRe3);

                    PdfPCell estCiRe4 = new PdfPCell(new Phrase("DIVORCIADO(A)  \n" + DIV1, PUT4));
                    estCiRe4.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(estCiRe4);

                    PdfPCell estCiRe5 = new PdfPCell(new Phrase("UNION LIBRE  \n" + UL1, PUT5));
                    estCiRe5.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(estCiRe5);
                    documento.Add(datGeRe);

                    //DATOS GENERALES VOL 2
                    PdfPTable curpRe = new PdfPTable(6);
                    curpRe.WidthPercentage = 100f;
                    int[] curpRecellwidth = { 15, 20, 10, 25, 10, 20 };
                    curpRe.SetWidths(curpRecellwidth);

                    string ifeine = r[77].ToString();
                    string curprop = r[78].ToString();
                    string rfcpro = r[79].ToString();

                    PdfPCell ineRe = new PdfPCell(new Phrase("No. CRED. IFE o INE", fuente2));
                    ineRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    ineRe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    curpRe.AddCell(ineRe);

                    PdfPCell ineRe1 = new PdfPCell(new Phrase(" " + ifeine, fuente8));
                    ineRe1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpRe.AddCell(ineRe1);

                    PdfPCell curpReal = new PdfPCell(new Phrase("CURP", fuente2));
                    curpReal.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpReal.BackgroundColor = BaseColor.LIGHT_GRAY;
                    curpRe.AddCell(curpReal);

                    PdfPCell curpReal1 = new PdfPCell(new Phrase(" " + curprop, fuente8));
                    curpReal1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpRe.AddCell(curpReal1);

                    PdfPCell rfcReal = new PdfPCell(new Phrase("RFC", fuente2));
                    rfcReal.HorizontalAlignment = Element.ALIGN_CENTER;
                    rfcReal.BackgroundColor = BaseColor.LIGHT_GRAY;
                    curpRe.AddCell(rfcReal);

                    PdfPCell rfcReal1 = new PdfPCell(new Phrase(" " + rfcpro, fuente8));
                    rfcReal1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpRe.AddCell(rfcReal1);
                    documento.Add(curpRe);

                    //escolaridad real
                    PdfPTable schoolRe = new PdfPTable(10);
                    schoolRe.WidthPercentage = 100f;
                    int[] schoolRecellwidth = { 10, 10, 10, 10, 10, 10, 10, 10, 7, 13 };
                    schoolRe.SetWidths(schoolRecellwidth);


                    string rolhogar1 = r[81].ToString();
                    string rolhogar2 = r[81].ToString();
                    string rolhogar3 = r[81].ToString();

                    iTextSharp.text.Font roll1 = new iTextSharp.text.Font();
                    iTextSharp.text.Font roll2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font roll3 = new iTextSharp.text.Font();

                    if (rolhogar1 == "Jefa(e)")
                    {
                        rolhogar1 = "X";
                        roll1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        rolhogar1 = " ";
                        roll1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (rolhogar2 == "Pareja")
                    {
                        rolhogar2 = "X";
                        roll2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        rolhogar2 = " ";
                        roll2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (rolhogar3 == "Hijo(a)")
                    {
                        rolhogar3 = "X";
                        roll3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        rolhogar3 = " ";
                        roll3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    string nohijos = r[82].ToString();
                    string noindepn = r[83].ToString();

                    string escolaridad1 = r[80].ToString();
                    string escolaridad12 = r[80].ToString();
                    string escolaridad13 = r[80].ToString();
                    string escolaridad14 = r[80].ToString();
                    string escolaridad15 = r[80].ToString();
                    string escolaridad16 = r[80].ToString();

                    iTextSharp.text.Font EST = new iTextSharp.text.Font();
                    iTextSharp.text.Font EST2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font EST3 = new iTextSharp.text.Font();
                    iTextSharp.text.Font EST4 = new iTextSharp.text.Font();
                    iTextSharp.text.Font EST5 = new iTextSharp.text.Font();
                    iTextSharp.text.Font EST6 = new iTextSharp.text.Font();

                    if (escolaridad1 == "SIN")
                    {
                        escolaridad1 = "X";
                        EST = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad1 = " ";
                        EST = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad12 == "PRI")
                    {
                        escolaridad12 = "X";
                        EST2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad12 = " ";
                        EST2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad13 == "SEC")
                    {
                        escolaridad13 = "X";
                        EST3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad13 = " ";
                        EST3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad14 == "BAC")
                    {
                        escolaridad14 = "X";
                        EST4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad14 = " ";
                        EST4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad15 == "LIC")
                    {
                        escolaridad15 = "X";
                        EST5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad15 = " ";
                        EST5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad16 == "POS")
                    {
                        escolaridad16 = "X";
                        EST6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad16 = " ";
                        EST6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }




                    PdfPCell niVe = (new PdfPCell(new Phrase("NIVEL DE ESCOLARIDAD (Marque con una X)", fuente2)) { Colspan = 5 });
                    niVe.HorizontalAlignment = Element.ALIGN_CENTER;
                    niVe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolRe.AddCell(niVe);

                    PdfPCell rolRe = (new PdfPCell(new Phrase("ROL EN EL HOGAR (Marque con una X)", fuente2)) { Colspan = 3 });
                    rolRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    rolRe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolRe.AddCell(rolRe);

                    PdfPCell hijosRe = new PdfPCell(new Phrase("No. HIJOS", fuente2));
                    hijosRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    hijosRe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolRe.AddCell(hijosRe);

                    PdfPCell depenRe = new PdfPCell(new Phrase("No. DE DEPEDIENTES", fuente2));
                    depenRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    depenRe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolRe.AddCell(depenRe);

                    PdfPCell nini = new PdfPCell(new Phrase("SIN INSTRUCCIÓN  \n " + escolaridad1, EST));
                    nini.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(nini);

                    PdfPCell prima = new PdfPCell(new Phrase("PRIMARIA  \n " + escolaridad12, EST2));
                    prima.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(prima);

                    PdfPCell secundaria = new PdfPCell(new Phrase("SECUNDARIA  \n " + escolaridad13, EST3));
                    secundaria.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(secundaria);

                    PdfPCell bachi = new PdfPCell(new Phrase("BACHILLERATO  \n " + escolaridad14, EST4));
                    bachi.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(bachi);

                    PdfPCell licen = new PdfPCell(new Phrase("LICENCIATURA  \n " + escolaridad15, EST5));
                    licen.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(licen);

                    PdfPCell POST = new PdfPCell(new Phrase("POSGRADO  \n " + escolaridad16, EST6));
                    POST.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(POST);

                    PdfPCell jefeFa = new PdfPCell(new Phrase("JEFE(A) \n" + rolhogar1, roll1));
                    jefeFa.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(jefeFa);

                    PdfPCell parej = new PdfPCell(new Phrase("PAREJA \n" + rolhogar2, roll2));
                    parej.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(parej);

                    PdfPCell hij = new PdfPCell(new Phrase("HIJO(A) \n" + rolhogar3, roll3));
                    hij.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(hij);

                    PdfPCell numHij = new PdfPCell(new Phrase(" " + nohijos, fuente8));
                    numHij.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(numHij);

                    PdfPCell numD = new PdfPCell(new Phrase(" " + noindepn, fuente8));
                    numD.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(numD);
                    documento.Add(schoolRe);
                    documento.Add(new Paragraph(" "));

                    //mas tablas 
                    PdfPTable tablaDos = new PdfPTable(4);
                    tablaDos.WidthPercentage = 100f;
                    int[] tablaDoscellwidth = { 35, 15, 25, 25 };
                    tablaDos.SetWidths(tablaDoscellwidth);

                    string ocuparef = r[84].ToString();
                    string tfijoref = r[85].ToString();
                    string tcelre = r[86].ToString();
                    string emailref = r[87].ToString();


                    PdfPCell ocupDos = new PdfPCell(new Phrase("OCUPACIÓN", fuente2));
                    ocupDos.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocupDos.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDos.AddCell(ocupDos);

                    PdfPCell telFiDos = new PdfPCell(new Phrase("TELÉFONO FIJO", fuente2));
                    telFiDos.HorizontalAlignment = Element.ALIGN_CENTER;
                    telFiDos.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDos.AddCell(telFiDos);

                    PdfPCell telCelDos = new PdfPCell(new Phrase("TELÉFONO CELULAR", fuente2));
                    telCelDos.HorizontalAlignment = Element.ALIGN_CENTER;
                    telCelDos.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDos.AddCell(telCelDos);

                    PdfPCell correoDos = new PdfPCell(new Phrase("CORREO ELECTRÓNICO", fuente2));
                    correoDos.HorizontalAlignment = Element.ALIGN_CENTER;
                    correoDos.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDos.AddCell(correoDos);

                    PdfPCell ocupDos1 = new PdfPCell(new Phrase(" " + ocuparef, fuente8));
                    ocupDos1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDos.AddCell(ocupDos1);

                    PdfPCell telFiDos2 = new PdfPCell(new Phrase(" " + tfijoref, fuente8));
                    telFiDos2.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDos.AddCell(telFiDos2);

                    PdfPCell telCelDos1 = new PdfPCell(new Phrase(" " + tcelre, fuente8));
                    telCelDos1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDos.AddCell(telCelDos1);

                    PdfPCell correoDos1 = new PdfPCell(new Phrase(" " + emailref, fuente8));
                    correoDos1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDos.AddCell(correoDos1);
                    documento.Add(tablaDos);

                    //domicilio 3.0
                    PdfPTable domiTre = new PdfPTable(4);
                    domiTre.WidthPercentage = 100f;
                    int[] domiTrecellwidth = { 40, 10, 10, 40 };
                    domiTre.SetWidths(domiTrecellwidth);
                    string domic5 = r[88].ToString();
                    string numext5 = r[89].ToString();
                    string numint5 = r[90].ToString();
                    string colonia5 = r[91].ToString();

                    PdfPCell domiTr3s = (new PdfPCell(new Phrase("DOMICILIO", fuente6)) { Colspan = 4 });
                    domiTr3s.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTr3s.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTre.AddCell(domiTr3s);

                    PdfPCell aveni = new PdfPCell(new Phrase("CALLE, AVENIDA ANDADOR, CERRADA, CALLEJON, MANZANA, LOTE", fuente2));
                    aveni.HorizontalAlignment = Element.ALIGN_CENTER;
                    aveni.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTre.AddCell(aveni);

                    PdfPCell exter = new PdfPCell(new Phrase("No. EXTERIOR", fuente2));
                    exter.HorizontalAlignment = Element.ALIGN_CENTER;
                    exter.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTre.AddCell(exter);

                    PdfPCell inter = new PdfPCell(new Phrase("No. INTERIOR", fuente2));
                    inter.HorizontalAlignment = Element.ALIGN_CENTER;
                    inter.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTre.AddCell(inter);

                    PdfPCell coloniaD = new PdfPCell(new Phrase("COLONIA O LOCALIDAD", fuente2));
                    coloniaD.HorizontalAlignment = Element.ALIGN_CENTER;
                    coloniaD.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTre.AddCell(coloniaD);

                    PdfPCell aveni1 = new PdfPCell(new Phrase(" " + domic5.ToUpper(), fuente8));
                    aveni1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTre.AddCell(aveni1);

                    PdfPCell exter1 = new PdfPCell(new Phrase(" " + numext5, fuente8));
                    exter1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTre.AddCell(exter1);

                    PdfPCell inter1 = new PdfPCell(new Phrase(" " + numint5, fuente8));
                    inter1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTre.AddCell(inter1);

                    PdfPCell coloniaD1 = new PdfPCell(new Phrase(" " + colonia5.ToUpper(), fuente8));
                    coloniaD1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTre.AddCell(coloniaD1);
                    documento.Add(domiTre);

                    //mas datos generales
                    PdfPTable datGenra = new PdfPTable(5);
                    datGenra.WidthPercentage = 100f;
                    int[] datGenracellwidth = { 15, 30, 20, 15, 20 };
                    datGenra.SetWidths(datGenracellwidth);

                    string cp5 = r[92].ToString();
                    string del5 = r[93].ToString();
                    string estado5 = r[94].ToString();
                    string tiempo5 = r[95].ToString();
                    string numhabit5 = r[96].ToString();

                    PdfPCell codiPost = new PdfPCell(new Phrase("CÓDIGO POSTAL", fuente2));
                    codiPost.HorizontalAlignment = Element.ALIGN_CENTER;
                    codiPost.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenra.AddCell(codiPost);

                    PdfPCell munici = new PdfPCell(new Phrase("DELEGACIÓN MUNICIPIO", fuente2));
                    munici.HorizontalAlignment = Element.ALIGN_CENTER;
                    munici.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenra.AddCell(munici);

                    PdfPCell stado = new PdfPCell(new Phrase("ESTADO", fuente2));
                    stado.HorizontalAlignment = Element.ALIGN_CENTER;
                    stado.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenra.AddCell(stado);

                    PdfPCell timeResiden = new PdfPCell(new Phrase("TIEMPO DE RESIDENCIA", fuente2));
                    timeResiden.HorizontalAlignment = Element.ALIGN_CENTER;
                    timeResiden.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenra.AddCell(timeResiden);

                    PdfPCell habitant = new PdfPCell(new Phrase("No. DE HABITANTES", fuente2));
                    habitant.HorizontalAlignment = Element.ALIGN_CENTER;
                    habitant.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenra.AddCell(habitant);

                    PdfPCell codiPost1 = new PdfPCell(new Phrase(" " + cp5.ToUpper(), fuente8));
                    codiPost1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenra.AddCell(codiPost1);

                    PdfPCell munici1 = new PdfPCell(new Phrase(" " + del5.ToUpper(), fuente8));
                    munici1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenra.AddCell(munici1);

                    PdfPCell stado1 = new PdfPCell(new Phrase(" " + estado5.ToUpper(), fuente8));
                    stado1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenra.AddCell(stado1);

                    PdfPCell timeResiden1 = new PdfPCell(new Phrase(" " + tiempo5, fuente8));
                    timeResiden1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenra.AddCell(timeResiden1);

                    PdfPCell habitant1 = new PdfPCell(new Phrase(" " + numhabit5, fuente8));
                    habitant1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenra.AddCell(habitant1);
                    documento.Add(datGenra);
                    documento.Add(new Paragraph(" "));

                    //INFORMACION DEL PROVEEDOR DE RECURSOS
                    PdfPTable infoPRO = new PdfPTable(3);
                    infoPRO.WidthPercentage = 100f;
                    int[] infoPROcellwidth = { 20, 20, 60 };
                    infoPRO.SetWidths(infoPROcellwidth);

                    string apellido5 = r[97].ToString();
                    string materno5 = r[98].ToString();
                    string names5 = r[99].ToString();


                    PdfPCell encaRefPRO = (new PdfPCell(new Phrase("INFORMACIÓN REFERENTE PROVEEDOR DE RECURSOS", fuente6)) { Colspan = 3 });
                    encaRefPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    encaRefPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    infoPRO.AddCell(encaRefPRO);

                    PdfPCell apelliParPRO = new PdfPCell(new Phrase(" " + apellido5.ToUpper(), fuente8));
                    apelliParPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoPRO.AddCell(apelliParPRO);

                    PdfPCell apelliMarPRO = new PdfPCell(new Phrase(" " + materno5.ToUpper(), fuente8));
                    apelliMarPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoPRO.AddCell(apelliMarPRO);

                    PdfPCell namePROPr = new PdfPCell(new Phrase(" " + names5.ToUpper(), fuente8));
                    namePROPr.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoPRO.AddCell(namePROPr);

                    PdfPCell apelliParPRO1 = new PdfPCell(new Phrase("APELLIDO PATERNO", fuente2));
                    apelliParPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoPRO.AddCell(apelliParPRO1);

                    PdfPCell apelliMarPRO1 = new PdfPCell(new Phrase("APELLIDO MATERNO", fuente2));
                    apelliMarPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoPRO.AddCell(apelliMarPRO1);

                    PdfPCell namePROPr1 = new PdfPCell(new Phrase("NOMBRE(S)", fuente2));
                    namePROPr1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoPRO.AddCell(namePROPr1);
                    documento.Add(infoPRO);

                    //datos generales
                    PdfPTable datGePRO = new PdfPTable(10);
                    datGePRO.WidthPercentage = 100f;
                    int[] datGePROcellwidth = { 20, 18, 13, 2, 2, 9, 9, 9, 9, 9 };
                    datGePRO.SetWidths(datGePROcellwidth);

                    string fechanam5 = r[101].ToString();
                    string entid5 = r[102].ToString();
                    string nacional5 = r[103].ToString();

                    string H12 = r[104].ToString();
                    string M13 = r[104].ToString();

                    iTextSharp.text.Font SEX = new iTextSharp.text.Font();
                    iTextSharp.text.Font SEX2 = new iTextSharp.text.Font();


                    if (H12 == "H")
                    {
                        H12 = "X";
                        SEX = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        H12 = " ";
                        SEX = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (M13 == "M")
                    {
                        M13 = "X";
                        SEX2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else
                    {
                        M13 = " ";
                        SEX2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }


                    string SOL2 = r[105].ToString();
                    string CAS3 = r[105].ToString();
                    string VIU4 = r[105].ToString();
                    string DIV5 = r[76].ToString();
                    string UL6 = r[105].ToString();

                    iTextSharp.text.Font EC = new iTextSharp.text.Font();
                    iTextSharp.text.Font EC2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font EC3 = new iTextSharp.text.Font();
                    iTextSharp.text.Font EC4 = new iTextSharp.text.Font();
                    iTextSharp.text.Font EC5 = new iTextSharp.text.Font();



                    if (SOL2 == "SOL")
                    {
                        SOL2 = "X";
                        EC = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        SOL2 = " ";
                        EC = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (CAS3 == "CAS")
                    {
                        CAS3 = "X";
                        EC2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        CAS3 = " ";
                        EC2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (VIU4 == "VIU")
                    {
                        VIU4 = "X";
                        EC3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        VIU4 = " ";
                        EC3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (DIV5 == "DIV")
                    {
                        DIV5 = "X";
                        EC4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        DIV5 = " ";
                        EC4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (UL6 == "UL")
                    {
                        UL6 = "X";
                        EC5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        UL6 = " ";
                        EC5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }



                    PdfPCell fechNaPRO = new PdfPCell(new Phrase("FECHA DE NACIMIENTO(dd/mm/aa)", fuente2));
                    fechNaPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    fechNaPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGePRO.AddCell(fechNaPRO);

                    PdfPCell entiNaPRO = new PdfPCell(new Phrase("ENTIDAD DE NACIMIENTO", fuente2));
                    entiNaPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    entiNaPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGePRO.AddCell(entiNaPRO);

                    PdfPCell naciPRO = new PdfPCell(new Phrase("NACIONAIDAD", fuente2));
                    naciPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    naciPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGePRO.AddCell(naciPRO);

                    PdfPCell sexPRO = (new PdfPCell(new Phrase("SEXO", fuente2)) { Colspan = 2 });
                    sexPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    sexPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGePRO.AddCell(sexPRO);

                    PdfPCell estCiPRO = (new PdfPCell(new Phrase("ESTADO CIVIL (Marque con una X)", fuente2)) { Colspan = 5 });
                    estCiPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    estCiPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGePRO.AddCell(estCiPRO);

                    PdfPCell fechNaPRO1 = new PdfPCell(new Phrase(" " + Convert.ToDateTime(fechanam5).ToString("dd/MM/yyyy"), fuente8));
                    fechNaPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(fechNaPRO1);

                    PdfPCell entiNaPRO1 = new PdfPCell(new Phrase(" " + entid5.ToUpper(), fuente8));
                    entiNaPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(entiNaPRO1);

                    PdfPCell naciPRO1 = new PdfPCell(new Phrase(" " + nacional5.ToUpper(), fuente8));
                    naciPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(naciPRO1);

                    PdfPCell sexPRO1 = new PdfPCell(new Phrase("M  \n" + M13, SEX2));
                    sexPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(sexPRO1);

                    PdfPCell sexPRO2 = new PdfPCell(new Phrase("H  \n" + H12, SEX));
                    sexPRO2.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(sexPRO2);

                    PdfPCell estCiPRO1 = new PdfPCell(new Phrase("SOLTERO(A)  \n" + SOL2, EC));
                    estCiPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(estCiPRO1);

                    PdfPCell estCiPRO2 = new PdfPCell(new Phrase("CASADO(A) \n" + CAS3, EC2));
                    estCiPRO2.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(estCiPRO2);

                    PdfPCell estCiPRO3 = new PdfPCell(new Phrase("VIUDO(A)  \n" + VIU4, EC3));
                    estCiPRO3.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(estCiPRO3);

                    PdfPCell estCiPRO4 = new PdfPCell(new Phrase("DIVORCIADO(A)  \n" + DIV5, EC4));
                    estCiPRO4.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(estCiPRO4);

                    PdfPCell estCiPRO5 = new PdfPCell(new Phrase("UNIÓN LIBRE  \n" + UL6, EC5));
                    estCiPRO5.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(estCiPRO5);
                    documento.Add(datGePRO);

                    //DATOS GENERALES VOL 2
                    PdfPTable curpPRO = new PdfPTable(6);
                    curpPRO.WidthPercentage = 100f;
                    int[] curpPROcellwidth = { 15, 20, 10, 25, 10, 20 };
                    curpPRO.SetWidths(curpPROcellwidth);

                    string ine5 = r[106].ToString();
                    string curp5 = r[107].ToString();
                    string rfc5 = r[108].ToString();

                    PdfPCell inePRO = new PdfPCell(new Phrase("No. CRED. IFE o INE", fuente2));
                    inePRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    inePRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    curpPRO.AddCell(inePRO);

                    PdfPCell inePRO1 = new PdfPCell(new Phrase(" " + ine5, fuente8));
                    inePRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpPRO.AddCell(inePRO1);

                    PdfPCell curpPRO1 = new PdfPCell(new Phrase("CURP", fuente2));
                    curpPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpPRO1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    curpPRO.AddCell(curpPRO1);

                    PdfPCell curpPROO1 = new PdfPCell(new Phrase(" " + curp5, fuente8));
                    curpPROO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpPRO.AddCell(curpPROO1);

                    PdfPCell rfcPRO = new PdfPCell(new Phrase("RFC", fuente2));
                    rfcPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    rfcPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    curpPRO.AddCell(rfcPRO);

                    PdfPCell rfcPRO1 = new PdfPCell(new Phrase(" " + rfc5, fuente8));
                    rfcPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpPRO.AddCell(rfcPRO1);
                    documento.Add(curpPRO);

                    //escolaridad real
                    PdfPTable schoolPRO = new PdfPTable(10);
                    schoolPRO.WidthPercentage = 100f;
                    int[] schoolPROcellwidth = { 10, 10, 10, 10, 10, 10, 10, 10, 7, 13 };
                    schoolPRO.SetWidths(schoolPROcellwidth);

                    string numhijos5 = r[111].ToString();
                    string numdepe5 = r[112].ToString();

                    string escolaridad01 = r[109].ToString();
                    string escolaridad02 = r[109].ToString();
                    string escolaridad03 = r[109].ToString();
                    string escolaridad04 = r[109].ToString();
                    string escolaridad05 = r[109].ToString();
                    string escolaridad06 = r[109].ToString();

                    iTextSharp.text.Font Ees = new iTextSharp.text.Font();
                    iTextSharp.text.Font Ees2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font Ees3 = new iTextSharp.text.Font();
                    iTextSharp.text.Font Ees4 = new iTextSharp.text.Font();
                    iTextSharp.text.Font Ees5 = new iTextSharp.text.Font();
                    iTextSharp.text.Font Ees6 = new iTextSharp.text.Font();

                    if (escolaridad01 == "SIN")
                    {
                        escolaridad01 = "X";
                        Ees = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad01 = " ";
                        Ees = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad02 == "PRI")
                    {
                        escolaridad02 = "X";
                        Ees2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad02 = " ";
                        Ees2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad03 == "SEC")
                    {
                        escolaridad03 = "X";
                        Ees3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad03 = " ";
                        Ees3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad04 == "BAC")
                    {
                        escolaridad04 = "X";
                        Ees4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad04 = " ";
                        Ees4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad05 == "LIC")
                    {
                        escolaridad05 = "X";
                        Ees5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad05 = " ";
                        Ees5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad06 == "POS")
                    {
                        escolaridad06 = "X";
                        Ees6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad06 = " ";
                        Ees6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }


                    string rolhogar01 = r[110].ToString();
                    string rolhogar02 = r[110].ToString();
                    string rolhogar03 = r[110].ToString();

                    iTextSharp.text.Font RROLL = new iTextSharp.text.Font();
                    iTextSharp.text.Font RROLL2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font RROLL3 = new iTextSharp.text.Font();

                    if (rolhogar01 == "Jefa(e)")
                    {
                        rolhogar01 = "X";
                        RROLL = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        rolhogar01 = " ";
                        RROLL = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (rolhogar02 == "Pareja")
                    {
                        rolhogar02 = "X";
                        RROLL2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        rolhogar02 = " ";
                        RROLL2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (rolhogar03 == "Hijo(a)")
                    {
                        rolhogar03 = "X";
                        RROLL3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        rolhogar03 = " ";
                        RROLL3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }


                    PdfPCell niVePRO = (new PdfPCell(new Phrase("NIVEL DE ESCOLARIDAD (Marque con una X)", fuente2)) { Colspan = 5 });
                    niVePRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    niVePRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolPRO.AddCell(niVePRO);

                    PdfPCell rolPRO = (new PdfPCell(new Phrase("ROL EN EL HOGAR (Marque con una X)", fuente2)) { Colspan = 3 });
                    rolPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    rolPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolPRO.AddCell(rolPRO);

                    PdfPCell hijosPRO = new PdfPCell(new Phrase("No. HIJOS", fuente2));
                    hijosPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    hijosPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolPRO.AddCell(hijosPRO);

                    PdfPCell depenPRO = new PdfPCell(new Phrase("No. DE DEPEDIENTES", fuente2));
                    depenPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    depenPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolPRO.AddCell(depenPRO);

                    PdfPCell niniPRO = new PdfPCell(new Phrase("SIN INSTRUCCIÓN  \n" + escolaridad01, Ees));
                    niniPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(niniPRO);

                    PdfPCell primaPRO = new PdfPCell(new Phrase("PRIMARIA  \n" + escolaridad02, Ees2));
                    primaPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(primaPRO);

                    PdfPCell secundariaPRO = new PdfPCell(new Phrase("SECUNDARIA  \n" + escolaridad03, Ees3));
                    secundariaPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(secundariaPRO);

                    PdfPCell bachiPRO = new PdfPCell(new Phrase("BACHILLERATO  \n" + escolaridad04, Ees4));
                    bachiPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(bachiPRO);

                    PdfPCell licenPRO = new PdfPCell(new Phrase("LICENCIATURA  \n" + escolaridad05, Ees5));
                    licenPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(licenPRO);

                    PdfPCell jefeFaPRO = new PdfPCell(new Phrase("JEFE(A)  \n" + rolhogar01, RROLL));
                    jefeFaPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(jefeFaPRO);

                    PdfPCell parejPRO = new PdfPCell(new Phrase("PAREJA  \n" + rolhogar02, RROLL2));
                    parejPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(parejPRO);

                    PdfPCell hijPRO = new PdfPCell(new Phrase("HIJO(A)  \n" + rolhogar03, RROLL3));
                    hijPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(hijPRO);

                    PdfPCell numHijPRO = new PdfPCell(new Phrase(" " + numhijos5, fuente8));
                    numHijPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(numHijPRO);

                    PdfPCell numDPRO = new PdfPCell(new Phrase(" " + numdepe5, fuente8));
                    numDPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(numDPRO);
                    documento.Add(schoolPRO);

                    //mas tablas 
                    PdfPTable tablaDosPRO = new PdfPTable(4);
                    tablaDosPRO.WidthPercentage = 100f;
                    int[] tablaDosPROcellwidth = { 35, 15, 25, 25 };
                    tablaDosPRO.SetWidths(tablaDosPROcellwidth);

                    string ocupacion6 = r[113].ToString();
                    string fijo6 = r[114].ToString();
                    string celular5 = r[115].ToString();
                    string email6 = r[116].ToString();

                    PdfPCell ocupDosPRO = new PdfPCell(new Phrase("OCUPACIÓN", fuente2));
                    ocupDosPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocupDosPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDosPRO.AddCell(ocupDosPRO);

                    PdfPCell telFiDosPRO = new PdfPCell(new Phrase("TELÉFONO FIJO", fuente2));
                    telFiDosPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    telFiDosPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDosPRO.AddCell(telFiDosPRO);

                    PdfPCell telCelDosPRO = new PdfPCell(new Phrase("TELÉFONO CELULAR", fuente2));
                    telCelDosPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    telCelDosPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDosPRO.AddCell(telCelDosPRO);

                    PdfPCell correoDosPRO = new PdfPCell(new Phrase("CORREO ELECTRÓNICO", fuente2));
                    correoDosPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    correoDosPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDosPRO.AddCell(correoDosPRO);

                    PdfPCell ocupDos1PRO = new PdfPCell(new Phrase(" " + ocupacion6, fuente8));
                    ocupDos1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDosPRO.AddCell(ocupDos1PRO);

                    PdfPCell telFiDos2PRO = new PdfPCell(new Phrase(" " + fijo6, fuente8));
                    telFiDos2PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDosPRO.AddCell(telFiDos2PRO);

                    PdfPCell telCelDos1PRO = new PdfPCell(new Phrase(" " + celular5, fuente8));
                    telCelDos1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDosPRO.AddCell(telCelDos1PRO);

                    PdfPCell correoDos1PRO = new PdfPCell(new Phrase(" " + email6, fuente8));
                    correoDos1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDosPRO.AddCell(correoDos1PRO);
                    documento.Add(tablaDosPRO);

                    //domicilio 3.0
                    PdfPTable domiTrePRO = new PdfPTable(4);
                    domiTrePRO.WidthPercentage = 100f;
                    int[] domiTrePROcellwidth = { 40, 10, 10, 40 };
                    domiTrePRO.SetWidths(domiTrePROcellwidth);

                    string calle7 = r[117].ToString();
                    string numext7 = r[118].ToString();
                    string numint7 = r[119].ToString();
                    string coloni7 = r[120].ToString();

                    PdfPCell domiTr3sPRO = (new PdfPCell(new Phrase("DOMICILIO", fuente6)) { Colspan = 4 });
                    domiTr3sPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTr3sPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTrePRO.AddCell(domiTr3sPRO);

                    PdfPCell aveniPRO = new PdfPCell(new Phrase("CALLE, AVENIDA ANDADOR, CERRADA, CALLEJON, MANZANA, LOTE", fuente2));
                    aveniPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    aveniPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTrePRO.AddCell(aveniPRO);

                    PdfPCell exterPRO = new PdfPCell(new Phrase("No. EXTERIOR", fuente2));
                    exterPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    exterPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTrePRO.AddCell(exterPRO);

                    PdfPCell interPRO = new PdfPCell(new Phrase("No. INTERIOR", fuente2));
                    interPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    interPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTrePRO.AddCell(interPRO);

                    PdfPCell coloniaDPRO = new PdfPCell(new Phrase("COLONIA O LOCALIDAD", fuente2));
                    coloniaDPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    coloniaDPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTrePRO.AddCell(coloniaDPRO);

                    PdfPCell aveni1PRO = new PdfPCell(new Phrase(" " + calle7.ToUpper(), fuente8));
                    aveni1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTrePRO.AddCell(aveni1PRO);

                    PdfPCell exter1PRO = new PdfPCell(new Phrase(" " + numext7, fuente8));
                    exter1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTrePRO.AddCell(exter1PRO);

                    PdfPCell inter1PRO = new PdfPCell(new Phrase(" " + numint7, fuente8));
                    inter1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTrePRO.AddCell(inter1PRO);

                    PdfPCell coloniaD1PRO = new PdfPCell(new Phrase(" " + coloni7.ToUpper(), fuente8));
                    coloniaD1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTrePRO.AddCell(coloniaD1PRO);
                    documento.Add(domiTrePRO);

                    //mas datos generales
                    PdfPTable datGenraPRO = new PdfPTable(5);
                    datGenraPRO.WidthPercentage = 100f;
                    int[] datGenraPROcellwidth = { 15, 30, 20, 15, 20 };
                    datGenraPRO.SetWidths(datGenraPROcellwidth);

                    string cp7 = r[121].ToString();
                    string delega7 = r[122].ToString();
                    string estado7 = r[123].ToString();
                    string tiemporesi7 = r[124].ToString();
                    string numhabi7 = r[125].ToString();

                    PdfPCell codiPostPRO = new PdfPCell(new Phrase("CÓDIGO POSTAL", fuente2));
                    codiPostPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    codiPostPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenraPRO.AddCell(codiPostPRO);

                    PdfPCell municiPRO = new PdfPCell(new Phrase("DELEGACIÓN MUNICIPIO", fuente2));
                    municiPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    municiPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenraPRO.AddCell(munici);

                    PdfPCell stadoPRO = new PdfPCell(new Phrase("ESTADO", fuente2));
                    stadoPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    stadoPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenraPRO.AddCell(stadoPRO);

                    PdfPCell timeResidenPRO = new PdfPCell(new Phrase("TIEMPO DE RESIDENCIA", fuente2));
                    timeResidenPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    timeResidenPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenraPRO.AddCell(timeResidenPRO);

                    PdfPCell habitantPRO = new PdfPCell(new Phrase("No. DE HABITANTES", fuente2));
                    habitantPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    habitantPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenraPRO.AddCell(habitantPRO);

                    PdfPCell codiPost1PRO = new PdfPCell(new Phrase(" " + cp7, fuente8));
                    codiPost1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenraPRO.AddCell(codiPost1PRO);

                    PdfPCell munici1PRO = new PdfPCell(new Phrase(" " + delega7.ToUpper(), fuente8));
                    munici1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenraPRO.AddCell(munici1PRO);

                    PdfPCell stado1PRO = new PdfPCell(new Phrase(" " + estado7.ToUpper(), fuente8));
                    stado1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenraPRO.AddCell(stado1PRO);

                    PdfPCell timeResiden1PRO = new PdfPCell(new Phrase(" " + tiemporesi7, fuente8));
                    timeResiden1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenraPRO.AddCell(timeResiden1PRO);

                    PdfPCell habitant1PRO = new PdfPCell(new Phrase(" " + numhabi7, fuente8));
                    habitant1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenraPRO.AddCell(habitant1PRO);
                    documento.Add(datGenraPRO);

                    PdfPTable firmElec = new PdfPTable(2);
                    firmElec.WidthPercentage = 65f;
                    firmElec.HorizontalAlignment = Element.ALIGN_LEFT;
                    int[] firmEleccellwidth = { 60, 40 };
                    firmElec.SetWidths(firmEleccellwidth);

                    PdfPCell razSoc = new PdfPCell(new Phrase("DENOMINACIÓN O RAZÓN SOCIAL", fuente8));
                    razSoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    razSoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    firmElec.AddCell(razSoc);

                    PdfPCell firmaE = new PdfPCell(new Phrase("FIRMA ELECTRÓNICA", fuente8));
                    firmaE.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmaE.BackgroundColor = BaseColor.LIGHT_GRAY;
                    firmElec.AddCell(firmaE);

                    PdfPCell razSoc1 = new PdfPCell(new Phrase(" ", fuente8));
                    razSoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmElec.AddCell(razSoc1);

                    PdfPCell firmaE1 = new PdfPCell(new Phrase(" ", fuente8));
                    firmaE1.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmElec.AddCell(firmaE1);
                    documento.Add(firmElec);

                    PdfPTable infoRef = new PdfPTable(4);
                    infoRef.WidthPercentage = 100f;
                    infoRef.HorizontalAlignment = Element.ALIGN_LEFT;
                    int[] infoRefcellwidth = { 45, 25, 15, 15 };
                    infoRef.SetWidths(infoRefcellwidth);

                    PdfPCell persMoral = (new PdfPCell(new Phrase("INFORMACIÓN REFERENTE AL PROPIETARIO REAL (PERSONA MORAL)", fuente6)) { Colspan = 4 });
                    persMoral.HorizontalAlignment = Element.ALIGN_CENTER;
                    persMoral.BackgroundColor = BaseColor.LIGHT_GRAY;
                    infoRef.AddCell(persMoral);

                    PdfPCell denoRaz = (new PdfPCell(new Phrase("DENOMINACIÓN O RAZON SOCIAL", fuente2)));
                    denoRaz.HorizontalAlignment = Element.ALIGN_CENTER;
                    denoRaz.BackgroundColor = BaseColor.LIGHT_GRAY;
                    infoRef.AddCell(denoRaz);

                    PdfPCell nacioRaz = (new PdfPCell(new Phrase("NACIONALIDAD", fuente2)));
                    nacioRaz.HorizontalAlignment = Element.ALIGN_CENTER;
                    nacioRaz.BackgroundColor = BaseColor.LIGHT_GRAY;
                    infoRef.AddCell(nacioRaz);

                    PdfPCell objeRaz = (new PdfPCell(new Phrase("OBJETO SOCIAL", fuente2)));
                    objeRaz.HorizontalAlignment = Element.ALIGN_CENTER;
                    objeRaz.BackgroundColor = BaseColor.LIGHT_GRAY;
                    infoRef.AddCell(objeRaz);

                    PdfPCell capSoRaz = (new PdfPCell(new Phrase("CAPITAL SOCIAL", fuente2)));
                    capSoRaz.HorizontalAlignment = Element.ALIGN_CENTER;
                    capSoRaz.BackgroundColor = BaseColor.LIGHT_GRAY;
                    infoRef.AddCell(capSoRaz);

                    PdfPCell denoRaz1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    denoRaz1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRef.AddCell(denoRaz1);

                    PdfPCell nacioRaz1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    nacioRaz1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRef.AddCell(nacioRaz1);

                    PdfPCell objeRaz1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    objeRaz1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRef.AddCell(objeRaz1);

                    PdfPCell capSoRaz1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    capSoRaz1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRef.AddCell(capSoRaz1);

                    documento.Add(infoRef);

                    PdfPTable domicRaz = new PdfPTable(7);
                    domicRaz.WidthPercentage = 100f;
                    domicRaz.HorizontalAlignment = Element.ALIGN_LEFT;
                    int[] domicRazcellwidth = { 25, 10, 10, 20, 10, 15, 10 };
                    domicRaz.SetWidths(domicRazcellwidth);

                    PdfPCell domicilioPM = (new PdfPCell(new Phrase("DOMICILIO", fuente6)) { Colspan = 7 });
                    domicilioPM.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicilioPM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(domicilioPM);

                    PdfPCell callePM = (new PdfPCell(new Phrase("CALLE, AVENIDA, CERRADA, CALLEJÓN, ETC.", fuente2)));
                    callePM.HorizontalAlignment = Element.ALIGN_CENTER;
                    callePM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(callePM);

                    PdfPCell noEtxPM = (new PdfPCell(new Phrase("No. EXT", fuente2)));
                    noEtxPM.HorizontalAlignment = Element.ALIGN_CENTER;
                    noEtxPM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(noEtxPM);

                    PdfPCell noIntPM = (new PdfPCell(new Phrase("No. INT", fuente2)));
                    noIntPM.HorizontalAlignment = Element.ALIGN_CENTER;
                    noIntPM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(noIntPM);

                    PdfPCell colPM = (new PdfPCell(new Phrase("COLONIA O LOCALIDAD", fuente2)));
                    colPM.HorizontalAlignment = Element.ALIGN_CENTER;
                    colPM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(colPM);

                    PdfPCell codPsPM = (new PdfPCell(new Phrase("CÓDIGO POSTAL", fuente2)));
                    codPsPM.HorizontalAlignment = Element.ALIGN_CENTER;
                    codPsPM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(codPsPM);

                    PdfPCell delegPM = (new PdfPCell(new Phrase("DELEGACIÓN O MUNICIPIO", fuente2)));
                    delegPM.HorizontalAlignment = Element.ALIGN_CENTER;
                    delegPM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(delegPM);

                    PdfPCell edoPM = (new PdfPCell(new Phrase("ESTADO", fuente2)));
                    edoPM.HorizontalAlignment = Element.ALIGN_CENTER;
                    edoPM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(edoPM);

                    PdfPCell callePM1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    callePM1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicRaz.AddCell(callePM1);

                    PdfPCell noEtxPM1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    noEtxPM1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicRaz.AddCell(noEtxPM1);

                    PdfPCell noIntPM1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    noIntPM1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicRaz.AddCell(noIntPM1);

                    PdfPCell colPM1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    colPM1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicRaz.AddCell(colPM1);

                    PdfPCell codPsPM1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    codPsPM1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicRaz.AddCell(codPsPM1);

                    PdfPCell delegPM1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    delegPM1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicRaz.AddCell(delegPM1);

                    PdfPCell edoPM1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    edoPM1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicRaz.AddCell(edoPM1);
                    documento.Add(domicRaz);
                    documento.Add(new Paragraph(" "));


                    PdfPTable accionistas = new PdfPTable(1);
                    accionistas.WidthPercentage = 60f;
                    accionistas.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell space = (new PdfPCell(new Phrase("ACCIONISTAS", fuente2)));
                    space.HorizontalAlignment = Element.ALIGN_CENTER;
                    space.BackgroundColor = BaseColor.LIGHT_GRAY;
                    accionistas.AddCell(space);

                    PdfPCell space1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    space1.HorizontalAlignment = Element.ALIGN_CENTER;
                    accionistas.AddCell(space1);

                    PdfPCell space2 = (new PdfPCell(new Phrase(" ", fuente2)));
                    space2.HorizontalAlignment = Element.ALIGN_CENTER;
                    accionistas.AddCell(space2);

                    documento.Add(accionistas);


                    //firma de enmedio
                    PdfPTable tabFirm = new PdfPTable(3);
                    tabFirm.WidthPercentage = 100f;
                    int[] tabFirmcellwidth = { 33, 33, 34 };
                    tabFirm.SetWidths(tabFirmcellwidth);

                    PdfPCell aviso = (new PdfPCell(new Phrase(" \n \n Declaro bajo protesta de decir la verdad que la información y los documentos proporcionados para esta solicitud son verdaderos.", fuente10)) { Colspan = 3 });
                    aviso.HorizontalAlignment = Element.ALIGN_CENTER;
                    aviso.VerticalAlignment = Element.ALIGN_MIDDLE;
                    aviso.Border = 0;
                    tabFirm.AddCell(aviso);

                    PdfPCell firmaTa = new PdfPCell(new Phrase("\n \n _______________________________________________ \n \n NOMBRE Y FIRMA DEL CLIENTE \n", fuente2));
                    firmaTa.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmaTa.BorderColor = BaseColor.BLUE;
                    tabFirm.AddCell(firmaTa);

                    PdfPCell firmaTa1 = new PdfPCell(new Phrase("\n \n _______________________________________________ \n \n NOMBRE Y FIRMA DEL ASESOR \n", fuente2));
                    firmaTa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmaTa1.BorderColor = BaseColor.BLUE;
                    tabFirm.AddCell(firmaTa1);

                    PdfPCell firmaTa2 = new PdfPCell(new Phrase("\n \n _______________________________________________ \n \n NOMBRE Y FIRMA GERENTE OPERATIVO \n", fuente2));
                    firmaTa2.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmaTa2.BorderColor = BaseColor.BLUE;
                    tabFirm.AddCell(firmaTa2);
                    documento.Add(tabFirm);

                    documento.Add(new Paragraph(" "));


                    //TABLA FINAL DE POLITICAS
                    PdfPTable FINAL = new PdfPTable(1);
                    FINAL.WidthPercentage = 100f;
                    int[] FINALcellwidth = { 100 };
                    FINAL.SetWidths(FINALcellwidth);

                    PdfPCell final1 = new PdfPCell(new Phrase("Estoy enterado del contenido del aviso de privacidad de APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR y sus declaraciones legales con fundamento en lo dispuesto por la Ley Federal de Protección de Datos Personales en posesión de los particulares y su reglamento, para lo cual otorgo de manera voluntaria el más amplio consentimiento y facultad a la empresa APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR a utilizar mis datos personales. APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR se reserva el derecho de cambiar, modificar, complementar y/o alterar el presente aviso, en cualquier momento, en cuyo casi se hará de su conocimiento a través de los medios que establezca la legislación en materia", fuente10));
                    final1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    final1.Border = 0;
                    FINAL.AddCell(final1);
                    documento.Add(FINAL);
                    documento.Close();
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

    protected void radFichas_SelectedIndexChanged(object sender, EventArgs e)
    {
        MCon obtiene = new MCon();
        int[] sesiones = obtieneSesiones();
        obtiene.empresa = sesiones[2];
        obtiene.sucursal = sesiones[3];
        int id_ficha = Convert.ToInt32(radFichas.SelectedValues["id_ficha"]);
        obtiene.id_ficha = id_ficha;
        obtiene.existeAdjuntosFicha();
        SqlDataSource4.SelectCommand = "select * from AN_Adjunto_FIcha_datos  where id_empresa=" + sesiones[2] + " and id_sucursal=" + sesiones[3] + " and id_ficha=" + id_ficha;

        RadGrid3.Visible = true;
        lnkImprimirFicha.Visible = true;
    }

    protected void lnkArchivoDescarga_Click(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        string ruta = Server.MapPath("~/TMP");
        int fichas = Convert.ToInt32(RadGrid3.SelectedValues["id_ficha"]);
        int adjuntos = Convert.ToInt32(RadGrid3.SelectedValues["Id_Ficha_Adjunto"]);
        int renglonDocto = Convert.ToInt32(adjuntos);
        MCon docto = new MCon();
        docto.empresa = sesiones[2];
        docto.sucursal = sesiones[3];
        docto.id_ficha = fichas;
        docto.id_adjunto = renglonDocto;
        docto.obtieneImagen();
        object[] retorno = docto.retorno;
        if (Convert.ToBoolean(retorno[0]))
        {
            DataSet docuemntos = (DataSet)retorno[1];

            if (docuemntos.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in docuemntos.Tables[0].Rows)
                {
                    string nombreFoto = r[0].ToString();
                    string extension = r[1].ToString().Trim();
                    byte[] imagenBuffer = (byte[])r[2];

                    string rutaArchivo = ruta + "\\" + nombreFoto.Trim() + "." + extension.ToLower().Trim();
                    FileInfo archivo = new FileInfo(rutaArchivo);
                    if (archivo.Exists)
                        archivo.Delete();

                    switch (extension.ToLower())
                    {
                        case "jpeg":
                            System.IO.MemoryStream st = new System.IO.MemoryStream(imagenBuffer);
                            System.Drawing.Image foto = System.Drawing.Image.FromStream(st);
                            System.Drawing.Imaging.ImageFormat formato = obtieneFormato(extension.ToLower());
                            foto.Save(rutaArchivo, formato);
                            break;
                        case "jpg":
                            st = new System.IO.MemoryStream(imagenBuffer);
                            foto = System.Drawing.Image.FromStream(st);
                            formato = obtieneFormato(extension.ToLower());
                            foto.Save(rutaArchivo, formato); break;
                        case "png":
                            st = new System.IO.MemoryStream(imagenBuffer);
                            foto = System.Drawing.Image.FromStream(st);
                            formato = obtieneFormato(extension.ToLower());
                            foto.Save(rutaArchivo, formato); break;
                        case "gif":
                            st = new System.IO.MemoryStream(imagenBuffer);
                            foto = System.Drawing.Image.FromStream(st);
                            formato = obtieneFormato(extension.ToLower());
                            foto.Save(rutaArchivo, formato); break;
                        case "bmp":
                            st = new System.IO.MemoryStream(imagenBuffer);
                            foto = System.Drawing.Image.FromStream(st);
                            formato = obtieneFormato(extension.ToLower());
                            foto.Save(rutaArchivo, formato); break;
                        case "tiff":
                            st = new System.IO.MemoryStream(imagenBuffer);
                            foto = System.Drawing.Image.FromStream(st);
                            formato = obtieneFormato(extension.ToLower());
                            foto.Save(rutaArchivo, formato);
                            break;
                        default:
                            File.WriteAllBytes(rutaArchivo, imagenBuffer);
                            break;
                    }

                    descargaArchivo(archivo, extension, ruta);
                }
            }
            else
            {

            }
              
        }

    }

    private ImageFormat obtieneFormato(string extension)
    {
        System.Drawing.Imaging.ImageFormat formato;
        switch (extension.ToLower())
        {
            case "jpg":
                formato = System.Drawing.Imaging.ImageFormat.Jpeg;
                break;
            case "png":
                formato = System.Drawing.Imaging.ImageFormat.Png;
                break;
            case "jpeg":
                formato = System.Drawing.Imaging.ImageFormat.Jpeg;
                break;
            case "gif":
                formato = System.Drawing.Imaging.ImageFormat.Gif;
                break;
            case "bmp":
                formato = System.Drawing.Imaging.ImageFormat.Bmp;
                break;
            case "tiff":
                formato = System.Drawing.Imaging.ImageFormat.Tiff;
                break;
            default:
                formato = System.Drawing.Imaging.ImageFormat.Jpeg;
                break;
        }
        return formato;
    }

    private void descargaArchivo(FileInfo archivo, string extension, string ruta)
    {
        Response.Clear();
        FileInfo doc = new FileInfo(archivo.FullName);

        Response.AddHeader("content-disposition", "attachment;filename=" + doc.Name);
        Response.WriteFile(ruta + "\\" + doc.Name);
        Response.End();
    }

    protected void RadGrid3_SelectedIndexChanged1(object sender, EventArgs e)
    {
        lnkArchivoDescarga.Visible = true;
    }
    private int[] obtieneSesiones()
    {
        int[] sesiones = new int[5];
        sesiones[0] = Convert.ToInt32(Request.QueryString["u"]);
        sesiones[1] = Convert.ToInt32(Request.QueryString["p"]);
        sesiones[2] = Convert.ToInt32(Request.QueryString["e"]);
        sesiones[3] = Convert.ToInt32(Request.QueryString["t"]);
        return sesiones;
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        
        MCon obtiene = new MCon();
        int[] sesiones = obtieneSesiones();
        obtiene.empresa = sesiones[2];
        obtiene.sucursal = sesiones[3];
        //int cliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        LinkButton aut = (LinkButton)sender;
        string[] arg = aut.CommandArgument.ToString().Split(new char[] { ';' });
        int solicitud = Convert.ToInt16(arg[1]);
        int cliente = Convert.ToInt16(arg[0]);
        obtiene.id_cliente = cliente;
        obtiene.tieneokAMC();
        if (Convert.ToBoolean(obtiene.retorno[0]))
        {
            lblErrorDigital.Text = "Documento Autorizado";
            SqlDataSource1.SelectCommand = "select * from AN_Solicitud_Credito_Detalle  where id_empresa="+sesiones[2]+" and id_sucursal="+sesiones[3]+" and id_solicitud_credito="+solicitud;
            SqlDataSource1.DataBind();
            RadGrid2.DataBind();

        }
        else
        {
            lblErrorDigital.Text = "Error al Autorizar el documento ";
        }
        

    }
    protected void CheckBox2a_CheckedChanged(object sender, EventArgs e)
    {

        MCon obtiene = new MCon();
        int[] sesiones = obtieneSesiones();
        obtiene.empresa = sesiones[2];
        obtiene.sucursal = sesiones[3];
        //int cliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        LinkButton aut = (LinkButton)sender;
        string[] arg = aut.CommandArgument.ToString().Split(new char[] { ';' });
        int solicitud = Convert.ToInt16(arg[1]);
        int cliente = Convert.ToInt16(arg[0]);
        obtiene.id_cliente = cliente;
        obtiene.notieneokAMC();
        if (Convert.ToBoolean(obtiene.retorno[0]))
        {
            lblErrorDigital.Text = "Documento Autorizado";
            SqlDataSource1.SelectCommand = "select * from AN_Solicitud_Credito_Detalle  where id_empresa=" + sesiones[2] + " and id_sucursal=" + sesiones[3] + " and id_solicitud_credito=" + solicitud;
            SqlDataSource1.DataBind();
            RadGrid2.DataBind();

        }
        else
        {
            lblErrorDigital.Text = "Error al Autorizar el documento ";
        }



    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

       
    }

    protected void headerChkbox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox headerCheckBox = (sender as CheckBox);
        foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
        {
            (dataItem.FindControl("CheckBox1") as CheckBox).Checked = headerCheckBox.Checked;
            dataItem.Selected = headerCheckBox.Checked;
        }

    }
   

    protected void lnkAutorizaGrup_Click(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        MCon obtiene = new MCon();
        obtiene.empresa = sesiones[2];
        obtiene.sucursal = sesiones[3];
        LinkButton aut = (LinkButton)sender;
        string[] arg = aut.CommandArgument.ToString().Split(new char[] { ';' });
        int solicitud = Convert.ToInt16(arg[0]);
        obtiene.idSolicitudEdita = solicitud;
        obtiene.tieneokAMCs();
        Credit genera = new Credit();
        genera.id_empresa = sesiones[2];
        genera.id_sucursal = sesiones[3];
        LinkButton auta = (LinkButton)sender;
        string[] arg2 = aut.CommandArgument.ToString().Split(new char[] { ';' });
        int solicitudcre = Convert.ToInt16(arg[0]);
        int id_solicitud_credito = Convert.ToInt32(RadGrid1.SelectedValues["id_solicitud_credito"]);
        genera.id_solicitud_credito = id_solicitud_credito;
        genera.obtieneInfoSolicitud();
        float tasa = 0;

        if (Convert.ToBoolean(genera.retorno[0]))
        {
            Fechas fec = new Fechas();
            DataSet ds1 = (DataSet)genera.retorno[1];
            foreach (DataRow r in ds1.Tables[0].Rows)
            {

                DateTime fecha = fec.obtieneFechaLocal();
                tasa =Convert.ToUInt32( r[15]);

                int conteo =0;
                int dias = 0;
                int plazo = Convert.ToInt32(r[14]);
                int plazo2 = plazo;
                tasa = tasa / 100 + 1;

                for (int i=1;i<= plazo;i++)
                {

                   

                   
                    int id_cliente = 0;
                    string nombre = "";
                    decimal monto_autorizado = 0;
                    DateTime fechapag;
                    string pagooo = "yyyy/MM/dd";
                    decimal pagoosem = 0;
                   // string aplicacion = "yyyy/MM/dd";

                    genera.obtieneDetalle();


                if (Convert.ToBoolean(genera.retorno[0]))
                {

                    DataSet ds2 = (DataSet)genera.retorno[1];
                        foreach (DataRow r2 in ds2.Tables[0].Rows)
                        {
                            dias = conteo;
                            id_cliente = Convert.ToInt32(r2[0]);
                            nombre = r2[1].ToString();
                            monto_autorizado = Convert.ToDecimal(r2[2]);
                            pagoosem = monto_autorizado * Convert.ToDecimal( tasa);
                            pagoosem = pagoosem / plazo2;
                            fechapag = Convert.ToDateTime(r2[3]);
                            fechapag = fechapag.AddDays(dias);
                            string fechapago = fechapag.ToString("yyyy/MM/dd");
                            pagooo = fechapago;
                            //aplicacion = "0000/00/00";
                            genera.id_cliente = id_cliente;
                            genera.nombre = nombre.ToString();
                            genera.monto_autorizado = monto_autorizado;
                            genera.npago = i;
                            genera.fecha_pago = pagooo.ToString();
                            genera.fecha_aplicacion = pagooo.ToString();
                            genera.pagosem = pagoosem;
                            genera.insertarClienteCredito();

                           

                    }
                        
                    }

                     conteo = dias;
                    conteo = conteo + 7; 
                   
                }
     


                genera.obtieneDetalle();


                if (Convert.ToBoolean(genera.retorno[0]))
                {

                    DataSet ds2 = (DataSet)genera.retorno[1];
                    foreach (DataRow r2 in ds1.Tables[0].Rows)
                    {

                    }
                }

                genera.validarCredito();

                if (Convert.ToBoolean(genera.retorno[0]))
                {

                
                    
                }
                else
                {
                    lblErrorDigital.Text = "Credito ya aceptado ";

                    genera.generaCredito();
                    RadGrid1.DataBind();
                }
                
             
            }
        }
        ValCre obt = new ValCre();
        obt.obtienenvalidacion();
        int tValidacion = 0;
        if (Convert.ToBoolean(obt.retorno[0]))
        {
            DataSet ds1 = (DataSet)obt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                tValidacion = Convert.ToInt32(r1[0]);

            }
        }
        int totalval = tValidacion;

        ValCre insert = new ValCre();
        insert.credito = id_solicitud_credito;
        insert.obtieneintegranrtesid();
        int idcliente = 0;
        if (Convert.ToBoolean(insert.retorno[0]))
        {
            DataSet ds2 = (DataSet)insert.retorno[1];
            foreach (DataRow r2 in ds2.Tables[0].Rows)
            {
                idcliente = Convert.ToInt32(r2[0]);
                for (int i = 1; i <= totalval; i++)
                {
                    insert.empresa = sesiones[2];
                    insert.sucursal = sesiones[3];
                    insert.credito = id_solicitud_credito;
                    int cliente1 = idcliente;
                    insert.cliente = cliente1;
                    insert.id_Val = i;
                    insert.agregarValidacion();
                }
            }
        }

        PolGrup obt1 = new PolGrup();
        obt1.obtienepOLITICAS();
        int tPoliticas = 0;
        if (Convert.ToBoolean(obt1.retorno[0]))
        {
            DataSet ds4 = (DataSet)obt1.retorno[1];

            foreach (DataRow r4 in ds4.Tables[0].Rows)
            {
                tPoliticas = Convert.ToInt32(r4[0]);

            }
        }
        int totalpol = tPoliticas;

        PolGrup inserta = new PolGrup();
        inserta.credito = id_solicitud_credito;
        inserta.obtieneintegranrtesid();
        int idcliente2 = 0;
        if (Convert.ToBoolean(inserta.retorno[0]))
        {
            DataSet ds5 = (DataSet)inserta.retorno[1];
            foreach (DataRow r5 in ds5.Tables[0].Rows)
            {
                idcliente2 = Convert.ToInt32(r5[0]);
                for (int i = 1; i <= tPoliticas; i++)
                {
                    inserta.empresa = sesiones[2];
                    inserta.sucursal = sesiones[3];
                    inserta.credito = id_solicitud_credito;
                    int cliente1 = idcliente2;
                    inserta.cliente = cliente1;
                    inserta.id_POL = i;
                    inserta.agregarPolitica();
                }
            }
        }
        RadGrid1.DataBind();
    }

    protected void lnkNegaGrup_Click(object sender, EventArgs e)
    {

    }




    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            DataRowView filas = (DataRowView)e.Item.DataItem;
            DataRow r = filas.Row;
            //string fase = DataBinder.Eval(e.DataItem, "fase_orden").ToString();
            //var img = e.Row.Cells[10].Controls[1].FindControl("imgFase") as System.Web.UI.WebControls.Image;

            var lnkAutorizaGrup = r[0].ToString();
            var status = r[1].ToString();
            //img.ImageUrl = "img/fase_" + fase + ".png";
            try
            {
                if (status == "AUTORIZADO")
                {
                    var btn1 = e.Item.Cells[0].Controls[0].FindControl("lnkAutorizaGrup") as LinkButton;
                    var btn2 = e.Item.Cells[0].Controls[0].FindControl("lnkNegaGrup") as LinkButton;
                    btn1.Visible = false;
                    btn2.Visible = false;

                }
               else
                {
                    var btn3 = e.Item.Cells[0].Controls[0].FindControl("lnkAutorizaGrup") as LinkButton;
                    var btn4 = e.Item.Cells[0].Controls[0].FindControl("lnkNegaGrup") as LinkButton;
                    btn3.Visible = true;
                    btn4.Visible = true;
                }


            }
            catch (Exception ex)
            {

            }
        }
        
    }
}
