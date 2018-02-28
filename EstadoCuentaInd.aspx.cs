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

public partial class EstadoCuentaInd : System.Web.UI.Page
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
        documento.AddTitle(" EstadoCuentaInd ");
        documento.AddCreator("Desarrollarte");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\Estado_Cuenta_Individ_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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
            tablaEncabezado.SetWidths(new float[] { 8f, 2f,});
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;
            tablaEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell titulo = new PdfPCell(new Phrase("Estado de Cuenta Individual", fuente6));
            titulo.HorizontalAlignment = Element.ALIGN_CENTER;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(titulo);
            tablaEncabezado.AddCell(logo);
           
            documento.Add(tablaEncabezado);
            documento.Add(new Paragraph(" "));

            EdoCuent parame = new EdoCuent();

            int[] sesiones = obtieneSesiones();
            parame.empresa = sesiones[2];
            parame.sucursal = sesiones[3];
            parame.obtenEmpresa();

            string nameCort = "";
            string nameEmp = "";
            string direEmp = "";
            string telEmpresa = "";
            string correoEmp = "";

            PdfPTable encabeza = new PdfPTable(1);
            encabeza.DefaultCell.Border = 0;
            encabeza.WidthPercentage = 100f;
            encabeza.HorizontalAlignment = Element.ALIGN_CENTER;


            PdfPTable piePag = new PdfPTable(2);
            piePag.SetWidths(new float[] { 85, 15 });
            piePag.WidthPercentage = 100f;
            piePag.HorizontalAlignment = Element.ALIGN_CENTER;

            if (Convert.ToBoolean(parame.retorno[0]))
            {
                DataSet para = (DataSet)parame.retorno[1];


                foreach (DataRow rt in para.Tables[0].Rows)
                {
                    nameCort=rt[2].ToString();
                    nameEmp = rt[3].ToString();
                    direEmp=rt[4].ToString();
                    correoEmp = rt[5].ToString();
                    telEmpresa = rt[6].ToString();

                    

                    PdfPCell empresa = new PdfPCell(new Phrase(nameCort.ToUpper(), fuente4));
                    empresa.HorizontalAlignment = Element.ALIGN_LEFT;
                    empresa.Border = 0;
                    empresa.VerticalAlignment = Element.ALIGN_MIDDLE;
                    encabeza.AddCell(empresa);

                    PdfPCell direc = new PdfPCell(new Phrase(direEmp, fuente4));
                    direc.HorizontalAlignment = Element.ALIGN_LEFT;
                    direc.Border = 0;
                    direc.VerticalAlignment = Element.ALIGN_MIDDLE;
                    encabeza.AddCell(direc);

                    PdfPCell telEmp = new PdfPCell(new Phrase("Teléfono: "+telEmpresa, fuente4));
                    telEmp.HorizontalAlignment = Element.ALIGN_LEFT;
                    telEmp.Border = 0;
                    telEmp.VerticalAlignment = Element.ALIGN_MIDDLE;
                    encabeza.AddCell(telEmp);


                    PdfPCell aclara = new PdfPCell(new Phrase("Para solicitudes y aclaraciones favor de comunicarse a la Unidad Especializada al: ____________________ ó Correo:"+correoEmp, fuente4));
                    aclara.HorizontalAlignment = Element.ALIGN_LEFT;
                    aclara.VerticalAlignment = Element.ALIGN_MIDDLE;
                    aclara.Border = 0;
                    piePag.AddCell(aclara);

                    PdfPCell impreso = new PdfPCell(new Phrase("Impreso por: ___________", fuente4));
                    impreso.HorizontalAlignment = Element.ALIGN_LEFT;
                    impreso.VerticalAlignment = Element.ALIGN_MIDDLE;
                    impreso.Border = 0;
                    piePag.AddCell(impreso);

                    PdfPCell CAT = new PdfPCell(new Phrase("Centro de Atención Telefónica CONDUCEF: 55530999  www.conducef.gob.mx", fuente4));
                    CAT.HorizontalAlignment = Element.ALIGN_LEFT;
                    CAT.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CAT.Border = 0;
                    piePag.AddCell(CAT);

                    PdfPCell pagina = new PdfPCell(new Phrase("Página 1", fuente4));
                    pagina.Border = 0;
                    pagina.HorizontalAlignment = Element.ALIGN_CENTER;
                    piePag.AddCell(pagina);

                    PdfPCell catel = (new PdfPCell(new Phrase("CAT (Costo Anual Total)              141.82%", fuente4)) { Colspan = 2 });
                    catel.HorizontalAlignment = Element.ALIGN_LEFT;
                    catel.VerticalAlignment = Element.ALIGN_MIDDLE;
                    catel.Border = 0;
                    piePag.AddCell(catel);

                    PdfPCell tasaFij = new PdfPCell(new Phrase("Costo Anual Total Crédito a Tasa Fija, para fines informativos y de comparación exclusivamente", fuente4));
                    tasaFij.Border = 0;
                    tasaFij.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tasaFij.HorizontalAlignment = Element.ALIGN_LEFT;
                    piePag.AddCell(tasaFij);


                    PdfPCell fechaa = new PdfPCell(new Phrase("Fecha: " + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd"), fuente4));
                    fechaa.Border = 0;
                    fechaa.HorizontalAlignment = Element.ALIGN_CENTER;
                    piePag.AddCell(fechaa);
                }
            }
            documento.Add(encabeza);
            documento.Add(new Paragraph(" "));

            PdfPTable detalleEnca = new PdfPTable(6);
            detalleEnca.SetWidths(new float[] { 10, 20, 5, 20, 10, 20 });
            detalleEnca.DefaultCell.Border = 0;
            detalleEnca.WidthPercentage = 80f;
            detalleEnca.HorizontalAlignment = Element.ALIGN_LEFT;

            EdoCuent encabez = new EdoCuent();
            encabez.empresa = sesiones[2];
            encabez.sucursal = sesiones[3];
            encabez.grupo = sesiones[4];
            int idcliente1 = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
            encabez.idcliente = idcliente1;
            encabez.obtenEncabezados();

            string grupoSol = "";
            string nombClient = "";
            string numInt = "";
            string calleCli = "";
            string numExt = "";
            string gereRegio = "";
            string colCli = "";
            string sucurClien = "";
            string municiCli = "";
            string asesorCli = "";
            string localCli = "";
            string cpCli = "";

            if (Convert.ToBoolean(encabez.retorno[0]))
            {
                DataSet Ed = (DataSet)encabez.retorno[1];


                foreach (DataRow r1 in Ed.Tables[0].Rows)
                {
                    nombClient = r1[0].ToString();
                    grupoSol = r1[1].ToString();
                    numInt = r1[2].ToString();
                    calleCli = r1[3].ToString();
                    numExt = r1[4].ToString();
                    cpCli = r1[5].ToString();
                    sucurClien = r1[6].ToString();
                    municiCli = r1[7].ToString();
                    localCli = r1[8].ToString();

                    EdoCuent cpost = new EdoCuent();
                    cpost.CodPost =Convert.ToInt32(r1[5]);
                    cpost.obtenCodigoPostal();

                    if (Convert.ToBoolean(cpost.retorno[0]))
                    {
                        DataSet cptl = (DataSet)cpost.retorno[1];


                        foreach (DataRow cp1 in cptl.Tables[0].Rows)
                        {
                            colCli = cp1[0].ToString();
                        }
                    }

                    EdoCuent anali = new EdoCuent();
                    anali.grupo=sesiones[4];
                    anali.obtenanalista();

                    if (Convert.ToBoolean(anali.retorno[0]))
                    {
                        DataSet analis = (DataSet)anali.retorno[1];


                        foreach (DataRow an1 in analis.Tables[0].Rows)
                        {
                            asesorCli = an1[2].ToString();
                        }
                    }


                }
            }

                    PdfPCell grupSol = new PdfPCell(new Phrase("Cliente:", fuente4));
                    grupSol.HorizontalAlignment = Element.ALIGN_LEFT;
                    grupSol.Border = 0;
                    grupSol.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(grupSol);

                    PdfPCell grupSol1 = new PdfPCell(new Phrase(nombClient+" \n   Grupo: "+ grupoSol, fuente4));
                    grupSol1.HorizontalAlignment = Element.ALIGN_LEFT;
                    grupSol1.Border = 0;
                    grupSol1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(grupSol1);

                    PdfPCell noInt = new PdfPCell(new Phrase("No Int:", fuente4));
                    noInt.HorizontalAlignment = Element.ALIGN_LEFT;
                    noInt.Border = 0;
                    noInt.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(noInt);

                    PdfPCell noInt1 = new PdfPCell(new Phrase(numInt, fuente4));
                    noInt1.HorizontalAlignment = Element.ALIGN_LEFT;
                    noInt1.Border = 0;
                    noInt1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(noInt1);

                    PdfPCell calle = new PdfPCell(new Phrase("Calle:", fuente4));
                    calle.HorizontalAlignment = Element.ALIGN_LEFT;
                    calle.Border = 0;
                    calle.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(calle);

                    PdfPCell calle1 = new PdfPCell(new Phrase(calleCli+"    No. Ext: "+numExt, fuente4));
                    calle1.HorizontalAlignment = Element.ALIGN_LEFT;
                    calle1.Border = 0;
                    calle1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(calle1);

                    PdfPCell gereReg = new PdfPCell(new Phrase("Gerecia Regional:", fuente4));
                    gereReg.HorizontalAlignment = Element.ALIGN_LEFT;
                    gereReg.Border = 0;
                    gereReg.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(gereReg);

                    PdfPCell gereReg1 = new PdfPCell(new Phrase(gereRegio, fuente4));
                    gereReg1.HorizontalAlignment = Element.ALIGN_LEFT;
                    gereReg1.Border = 0;
                    gereReg1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(gereReg1);

                    PdfPCell spac = new PdfPCell(new Phrase(" ", fuente4));
                    spac.HorizontalAlignment = Element.ALIGN_LEFT;
                    spac.Border = 0;
                    spac.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(spac);

                    PdfPCell spac1 = new PdfPCell(new Phrase(" ", fuente4));
                    spac1.HorizontalAlignment = Element.ALIGN_LEFT;
                    spac1.Border = 0;
                    spac1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(spac1);

                    PdfPCell colo = new PdfPCell(new Phrase("Colonia:", fuente4));
                    colo.HorizontalAlignment = Element.ALIGN_LEFT;
                    colo.Border = 0;
                    colo.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(colo);

                    PdfPCell colo1 = new PdfPCell(new Phrase(colCli, fuente4));
                    colo1.HorizontalAlignment = Element.ALIGN_LEFT;
                    colo1.Border = 0;
                    colo1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(colo1);

                    PdfPCell suc = new PdfPCell(new Phrase("Sucursal:", fuente4));
                    suc.HorizontalAlignment = Element.ALIGN_LEFT;
                    suc.Border = 0;
                    suc.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(suc);

                    PdfPCell suc1 = new PdfPCell(new Phrase(sucurClien, fuente4));
                    suc1.HorizontalAlignment = Element.ALIGN_LEFT;
                    suc1.Border = 0;
                    suc1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(suc1);

                    PdfPCell space = new PdfPCell(new Phrase(" ", fuente4));
                    space.HorizontalAlignment = Element.ALIGN_LEFT;
                    space.Border = 0;
                    space.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(space);

                    PdfPCell space1 = new PdfPCell(new Phrase(" ", fuente4));
                    space1.HorizontalAlignment = Element.ALIGN_LEFT;
                    space1.Border = 0;
                    space1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(space1);

                    PdfPCell muni = new PdfPCell(new Phrase("Municipio:", fuente4));
                    muni.HorizontalAlignment = Element.ALIGN_LEFT;
                    muni.Border = 0;
                    muni.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(muni);

                    PdfPCell muni1 = new PdfPCell(new Phrase(municiCli, fuente4));
                    muni1.HorizontalAlignment = Element.ALIGN_LEFT;
                    muni1.Border = 0;
                    muni1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(muni1);

                    PdfPCell asesor = new PdfPCell(new Phrase("Asesor:", fuente4));
                    asesor.HorizontalAlignment = Element.ALIGN_LEFT;
                    asesor.Border = 0;
                    asesor.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(asesor);

                    PdfPCell asesor1 = (new PdfPCell(new Phrase(asesorCli, fuente4)) { Colspan = 3 });
                    asesor1.HorizontalAlignment = Element.ALIGN_LEFT;
                    asesor1.Border = 0;
                    asesor1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(asesor1);



                    PdfPCell local = new PdfPCell(new Phrase("Localidad:", fuente4));
                    local.HorizontalAlignment = Element.ALIGN_LEFT;
                    local.Border = 0;
                    local.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(local);

                    PdfPCell local1 = new PdfPCell(new Phrase(localCli, fuente4));
                    local1.HorizontalAlignment = Element.ALIGN_LEFT;
                    local1.Border = 0;
                    local1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    detalleEnca.AddCell(local1);
                    documento.Add(detalleEnca);

            

            PdfPTable tabCiclo = new PdfPTable(6);
            tabCiclo.SetWidths(new float[] { 10, 20, 10, 20, 10, 20 });
            tabCiclo.WidthPercentage = 60f;
            tabCiclo.HorizontalAlignment = Element.ALIGN_LEFT;

            EdoCuent deta = new EdoCuent();
            deta.empresa = sesiones[2];
            deta.sucursal = sesiones[3];
            deta.grupo = sesiones[4];
            int idclienteee = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
            deta.idcliente = idclienteee;
            deta.obtenerDetaCredito();
            
            decimal cicloCli = 0;
            string credito = "";
            string fecha = "";
            decimal plazoCli=0;
            decimal tasa = 0;
            string interesCli = "";
            string pagSemCli = "";
            string totPagar = "";
            string creAutCli = "";
            string  requeClient = "";
            string fechaFin = "";
            string periodoCli = "";
            string devolucion = "";
            string saldoCli="";
            string devCliTo = "";
            decimal cero = 0;

            if (Convert.ToBoolean(deta.retorno[0]))
            {
                DataSet ediCre = (DataSet)deta.retorno[1];


                foreach (DataRow cre in ediCre.Tables[0].Rows)
                {
                    cicloCli = Convert.ToDecimal(cre[0]);
                    credito = cre[1].ToString();
                    fecha = Convert.ToDateTime(cre[2]).ToString("yyyy/MM/dd");
                    plazoCli = Convert.ToDecimal(cre[3]);
                    tasa = Convert.ToDecimal(cre[4]);
                    pagSemCli = Convert.ToDecimal(cre[5]).ToString("C2");
                    creAutCli = Convert.ToDecimal(cre[6]).ToString("C2");
                    requeClient = Convert.ToDecimal( cre[7]).ToString("C2");
                    interesCli = Convert.ToDecimal(cre[8]).ToString("C2");
                    totPagar = Convert.ToDecimal(cre[9]).ToString("C2");
                    fechaFin = Convert.ToDateTime(cre[10]).ToString("yyyy/MM/dd");
                    periodoCli = cre[11].ToString();
                    devCliTo = Convert.ToDecimal( cre[12]).ToString("C2");                    
                }
            }
            EdoCuent devo = new EdoCuent();
            devo.empresa = sesiones[2];
            devo.sucursal = sesiones[3];
            devo.grupo = sesiones[4];
            int idclientee = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
            devo.idcliente = idclientee;
            devo.periodoCliTo =Convert.ToInt32( periodoCli);
            devo.obtenerDev();

            if (Convert.ToBoolean(devo.retorno[0]))
            {
                DataSet ediDev = (DataSet)devo.retorno[1];


                foreach (DataRow dvt in ediDev.Tables[0].Rows)
                {
                    try
                    {
                        devolucion = Convert.ToDecimal(dvt[0]).ToString("C2");
                    }
                    catch
                    {
                        devolucion = cero.ToString("C2");
                    }
                    try
                    {
                        saldoCli = Convert.ToDecimal(dvt[1]).ToString("C2");
                    }
                    catch
                    {
                        saldoCli = cero.ToString("C2");
                    }
                    
                }
            }


            PdfPCell clicloEtc = (new PdfPCell(new Phrase("Ciclo:  " + cicloCli + "     Crédito:  " + credito + "      Inicio:  " + fecha + "       Fin:   " + fechaFin + "        Periodos Transcurridos:       "+periodoCli+"        ", fuente4)) { Colspan = 6 });
            clicloEtc.HorizontalAlignment = Element.ALIGN_CENTER;
            clicloEtc.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(clicloEtc);

            PdfPCell dura = new PdfPCell(new Phrase("Duración", fuente4));
            dura.HorizontalAlignment = Element.ALIGN_LEFT;
            dura.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(dura);

            PdfPCell tasaAn = new PdfPCell(new Phrase("Tasa Anual", fuente4));
            tasaAn.HorizontalAlignment = Element.ALIGN_LEFT;
            tasaAn.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(tasaAn);

            PdfPCell interes = new PdfPCell(new Phrase("Interes", fuente4));
            interes.HorizontalAlignment = Element.ALIGN_LEFT;
            interes.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(interes);

            PdfPCell Pagsem = new PdfPCell(new Phrase("Pago Semanal", fuente4));
            Pagsem.HorizontalAlignment = Element.ALIGN_LEFT;
            Pagsem.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(Pagsem);

            PdfPCell totapag = new PdfPCell(new Phrase("Total Pagar", fuente4));
            totapag.HorizontalAlignment = Element.ALIGN_LEFT;
            totapag.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(totapag);

            PdfPCell etc = (new PdfPCell(new Phrase(" Autorizado: " + creAutCli + " \n\n Devolución: " + devCliTo + " \n\n Cartera Vencida: ", fuente4)) { Rowspan = 4 });
            etc.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            etc.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(etc);

            PdfPCell dura1 = new PdfPCell(new Phrase("\n \n "+plazoCli, fuente4));
            dura1.HorizontalAlignment = Element.ALIGN_CENTER;
            dura1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(dura1);

            PdfPCell tasaAn1 = new PdfPCell(new Phrase("\n \n " + tasa+" %", fuente4));
            tasaAn1.HorizontalAlignment = Element.ALIGN_CENTER;
            tasaAn1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(tasaAn1);

            PdfPCell interes1 = new PdfPCell(new Phrase("\n \n "+ interesCli, fuente4));
            interes1.HorizontalAlignment = Element.ALIGN_CENTER;
            interes1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(interes1);

            PdfPCell Pagsem1 = new PdfPCell(new Phrase("\n \n " + pagSemCli, fuente4));
            Pagsem1.HorizontalAlignment = Element.ALIGN_CENTER;
            Pagsem1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(Pagsem1);

            PdfPCell totapag1 = new PdfPCell(new Phrase("\n \n "+ totPagar, fuente4));
            totapag1.HorizontalAlignment = Element.ALIGN_CENTER;
            totapag1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(totapag1);

            PdfPCell garaLi = (new PdfPCell(new Phrase("Garantía Líquida", fuente4)) { Rowspan=2 });
            garaLi.HorizontalAlignment = Element.ALIGN_CENTER;
            garaLi.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(garaLi);

            PdfPCell reque = new PdfPCell(new Phrase("Requerida", fuente4));
            reque.HorizontalAlignment = Element.ALIGN_LEFT;
            reque.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(reque);

            PdfPCell abonos = new PdfPCell(new Phrase("Depósito", fuente4));
            abonos.HorizontalAlignment = Element.ALIGN_LEFT;
            abonos.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(abonos);

            PdfPCell cargos = new PdfPCell(new Phrase("Devlución", fuente4));
            cargos.HorizontalAlignment = Element.ALIGN_LEFT;
            cargos.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(cargos);

            PdfPCell Saldos = new PdfPCell(new Phrase("Saldos", fuente4));
            Saldos.HorizontalAlignment = Element.ALIGN_LEFT;
            Saldos.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(Saldos);

            PdfPCell reque1 = new PdfPCell(new Phrase(requeClient, fuente4));
            reque1.HorizontalAlignment = Element.ALIGN_CENTER;
            reque1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(reque1);

            PdfPCell abonos1 = new PdfPCell(new Phrase(" ", fuente4));
            abonos1.HorizontalAlignment = Element.ALIGN_CENTER;
            abonos1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(abonos1);

            PdfPCell cargos1 = new PdfPCell(new Phrase(devolucion, fuente4));
            cargos1.HorizontalAlignment = Element.ALIGN_CENTER;
            cargos1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(cargos1);

            PdfPCell Saldos1 = new PdfPCell(new Phrase(" ", fuente4));
            Saldos1.HorizontalAlignment = Element.ALIGN_CENTER;
            Saldos1.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabCiclo.AddCell(Saldos1);


            PdfPTable resum = new PdfPTable(2);
            resum.SetWidths(new float[] { 50, 50 });
            resum.WidthPercentage = 20f;
            resum.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell resumenCre = (new PdfPCell(new Phrase("\n Resumen \n", fuente4)) { Colspan = 2 });
            resumenCre.HorizontalAlignment = Element.ALIGN_CENTER;
            resumenCre.VerticalAlignment = Element.ALIGN_MIDDLE;
            resumenCre.Border = 0;
            resum.AddCell(resumenCre);

            PdfPCell inteResum = new PdfPCell(new Phrase("  Interes:", fuente4));
            inteResum.HorizontalAlignment = Element.ALIGN_LEFT;
            inteResum.VerticalAlignment = Element.ALIGN_MIDDLE;
            inteResum.Border = 0;
            resum.AddCell(inteResum);

            PdfPCell inteResum1 = new PdfPCell(new Phrase("", fuente4));
            inteResum1.HorizontalAlignment = Element.ALIGN_LEFT;
            inteResum1.VerticalAlignment = Element.ALIGN_MIDDLE;
            inteResum1.Border = 0;
            resum.AddCell(inteResum1);

            PdfPCell abonito = new PdfPCell(new Phrase("  Abono:", fuente4));
            abonito.HorizontalAlignment = Element.ALIGN_LEFT;
            abonito.VerticalAlignment = Element.ALIGN_MIDDLE;
            abonito.Border = 0;
            resum.AddCell(abonito);

            PdfPCell abonito1 = new PdfPCell(new Phrase(" ", fuente4));
            abonito1.HorizontalAlignment = Element.ALIGN_LEFT;
            abonito1.VerticalAlignment = Element.ALIGN_MIDDLE;
            abonito1.Border = 0;
            resum.AddCell(abonito1);

            PdfPCell capiPago = new PdfPCell(new Phrase("  Capital Pag:", fuente4));
            capiPago.HorizontalAlignment = Element.ALIGN_LEFT;
            capiPago.VerticalAlignment = Element.ALIGN_MIDDLE;
            capiPago.Border = 0;
            resum.AddCell(capiPago);

            PdfPCell capiPago1 = new PdfPCell(new Phrase(" ", fuente4));
            capiPago1.HorizontalAlignment = Element.ALIGN_LEFT;
            capiPago1.VerticalAlignment = Element.ALIGN_MIDDLE;
            capiPago1.Border = 0;
            resum.AddCell(capiPago1);

            PdfPCell intPagado = new PdfPCell(new Phrase("  Interés Pagado:", fuente4));
            intPagado.HorizontalAlignment = Element.ALIGN_LEFT;
            intPagado.VerticalAlignment = Element.ALIGN_MIDDLE;
            intPagado.Border = 0;
            resum.AddCell(intPagado);

            PdfPCell intPagado1 = new PdfPCell(new Phrase(" ", fuente4));
            intPagado1.HorizontalAlignment = Element.ALIGN_LEFT;
            intPagado1.VerticalAlignment = Element.ALIGN_MIDDLE;
            intPagado1.Border = 0;
            resum.AddCell(intPagado1);

            PdfPCell situacion = new PdfPCell(new Phrase("  Situación:", fuente4));
            situacion.HorizontalAlignment = Element.ALIGN_LEFT;
            situacion.VerticalAlignment = Element.ALIGN_MIDDLE;
            situacion.Border = 0;
            resum.AddCell(situacion);

            PdfPCell situacion1 = new PdfPCell(new Phrase(" ", fuente4));
            situacion1.HorizontalAlignment = Element.ALIGN_LEFT;
            situacion1.VerticalAlignment = Element.ALIGN_MIDDLE;
            situacion1.Border = 0;
            resum.AddCell(situacion1);

            PdfPTable saldoTab = new PdfPTable(2);
            saldoTab.SetWidths(new float[] { 50, 50 });
            saldoTab.WidthPercentage = 20f;
            saldoTab.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell saldito = (new PdfPCell(new Phrase("\n Saldo", fuente4)) { Colspan=2 });
            saldito.HorizontalAlignment = Element.ALIGN_CENTER;
            saldito.VerticalAlignment = Element.ALIGN_MIDDLE;
            saldito.Border = 0;
            saldoTab.AddCell(saldito);

            PdfPCell dayAtra = new PdfPCell(new Phrase("Días Atraso: ", fuente4));
            dayAtra.HorizontalAlignment = Element.ALIGN_LEFT;
            dayAtra.VerticalAlignment = Element.ALIGN_MIDDLE;
            dayAtra.Border = 0;
            saldoTab.AddCell(dayAtra);

            PdfPCell dayAtra1 = new PdfPCell(new Phrase(" ", fuente4));
            dayAtra1.HorizontalAlignment = Element.ALIGN_LEFT;
            dayAtra1.VerticalAlignment = Element.ALIGN_MIDDLE;
            dayAtra1.Border = 0;
            saldoTab.AddCell(dayAtra1);

            PdfPCell capita = new PdfPCell(new Phrase("Capital: ", fuente4));
            capita.HorizontalAlignment = Element.ALIGN_LEFT;
            capita.VerticalAlignment = Element.ALIGN_MIDDLE;
            capita.Border = 0;
            saldoTab.AddCell(capita);

            PdfPCell capita1 = new PdfPCell(new Phrase(" ", fuente4));
            capita1.HorizontalAlignment = Element.ALIGN_LEFT;
            capita1.VerticalAlignment = Element.ALIGN_MIDDLE;
            capita1.Border = 0;
            saldoTab.AddCell(capita1);

            PdfPCell IntSal = new PdfPCell(new Phrase("Intereses: ", fuente4));
            IntSal.HorizontalAlignment = Element.ALIGN_LEFT;
            IntSal.VerticalAlignment = Element.ALIGN_MIDDLE;
            IntSal.Border = 0;
            saldoTab.AddCell(IntSal);

            PdfPCell IntSal1 = new PdfPCell(new Phrase(" ", fuente4));
            IntSal1.HorizontalAlignment = Element.ALIGN_LEFT;
            IntSal1.VerticalAlignment = Element.ALIGN_MIDDLE;
            IntSal1.Border = 0;
            saldoTab.AddCell(IntSal1);

            PdfPCell intMoSal = new PdfPCell(new Phrase("Interes Moral: ", fuente4));
            intMoSal.HorizontalAlignment = Element.ALIGN_LEFT;
            intMoSal.VerticalAlignment = Element.ALIGN_MIDDLE;
            intMoSal.Border = 0;
            saldoTab.AddCell(intMoSal);

            PdfPCell intMoSal1 = new PdfPCell(new Phrase(" ", fuente4));
            intMoSal1.HorizontalAlignment = Element.ALIGN_LEFT;
            intMoSal1.VerticalAlignment = Element.ALIGN_MIDDLE;
            intMoSal1.Border = 0;
            saldoTab.AddCell(intMoSal1);

            PdfPCell cicloSal = new PdfPCell(new Phrase("Ciclo: \n \n", fuente4));
            cicloSal.HorizontalAlignment = Element.ALIGN_LEFT;
            cicloSal.VerticalAlignment = Element.ALIGN_MIDDLE;
            cicloSal.Border = 0;
            saldoTab.AddCell(cicloSal);

            PdfPCell cicloSal1 = new PdfPCell(new Phrase(" ", fuente4));
            cicloSal1.HorizontalAlignment = Element.ALIGN_LEFT;
            cicloSal1.VerticalAlignment = Element.ALIGN_MIDDLE;
            cicloSal1.Border = 0;
            saldoTab.AddCell(cicloSal1);

            PdfPTable union = new PdfPTable(3);
            union.SetWidths(new float[] { 60, 20, 20 });
            union.WidthPercentage = 100f;
            union.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell tab1 = new PdfPCell(tabCiclo);
            union.AddCell(tab1);

            PdfPCell tab2 = new PdfPCell(resum);
            union.AddCell(tab2);

            PdfPCell tab3 = new PdfPCell(saldoTab);
            union.AddCell(tab3);
            documento.Add(union);


            documento.Add(new Paragraph(" "));

            
                
            
            PdfPTable tabamort = new PdfPTable(12);
            tabamort.SetWidths(new float[] { 2, 8, 12, 18, 5, 5, 5, 5, 5, 10, 5, 10 });
            tabamort.WidthPercentage = 100f;
            tabamort.HorizontalAlignment = Element.ALIGN_CENTER;

            

            PdfPCell no = new PdfPCell(new Phrase("N°", fuente4));
            no.HorizontalAlignment = Element.ALIGN_LEFT;
            no.VerticalAlignment = Element.ALIGN_MIDDLE;
            no.Border = 0;
            tabamort.AddCell(no);

            PdfPCell fec = new PdfPCell(new Phrase("Fecha", fuente4));
            fec.HorizontalAlignment = Element.ALIGN_LEFT;
            fec.VerticalAlignment = Element.ALIGN_MIDDLE;
            fec.Border = 0;
            tabamort.AddCell(fec);

            PdfPCell fecApl = new PdfPCell(new Phrase("Fecha Aplicación", fuente4));
            fecApl.HorizontalAlignment = Element.ALIGN_LEFT;
            fecApl.VerticalAlignment = Element.ALIGN_MIDDLE;
            fecApl.Border = 0;
            tabamort.AddCell(fecApl);

            PdfPCell mov = new PdfPCell(new Phrase("Movimiento", fuente4));
            mov.HorizontalAlignment = Element.ALIGN_LEFT;
            mov.VerticalAlignment = Element.ALIGN_MIDDLE;
            mov.Border = 0;
            tabamort.AddCell(mov);

            PdfPCell abono = new PdfPCell(new Phrase("Abono", fuente4));
            abono.HorizontalAlignment = Element.ALIGN_LEFT;
            abono.VerticalAlignment = Element.ALIGN_MIDDLE;
            abono.Border = 0;
            tabamort.AddCell(abono);

            PdfPCell cap = new PdfPCell(new Phrase("Capital", fuente4));
            cap.HorizontalAlignment = Element.ALIGN_LEFT;
            cap.VerticalAlignment = Element.ALIGN_MIDDLE;
            cap.Border = 0;
            tabamort.AddCell(cap);

            PdfPCell inte = new PdfPCell(new Phrase("Intereses", fuente4));
            inte.HorizontalAlignment = Element.ALIGN_LEFT;
            inte.VerticalAlignment = Element.ALIGN_MIDDLE;
            inte.Border = 0;
            tabamort.AddCell(inte);

            PdfPCell iva = new PdfPCell(new Phrase("IVA", fuente4));
            iva.HorizontalAlignment = Element.ALIGN_LEFT;
            iva.VerticalAlignment = Element.ALIGN_MIDDLE;
            iva.Border = 0;
            tabamort.AddCell(iva);

            PdfPCell IntMo = new PdfPCell(new Phrase("Int Moratorios", fuente4));
            IntMo.HorizontalAlignment = Element.ALIGN_LEFT;
            IntMo.VerticalAlignment = Element.ALIGN_MIDDLE;
            IntMo.Border = 0;
            tabamort.AddCell(IntMo);

            PdfPCell salCap = new PdfPCell(new Phrase("Saldo Capital", fuente4));
            salCap.HorizontalAlignment = Element.ALIGN_LEFT;
            salCap.VerticalAlignment = Element.ALIGN_MIDDLE;
            salCap.Border = 0;
            tabamort.AddCell(salCap);

            PdfPCell salIva = new PdfPCell(new Phrase("Interes por devengar", fuente4));
            salIva.HorizontalAlignment = Element.ALIGN_LEFT;
            salIva.VerticalAlignment = Element.ALIGN_MIDDLE;
            salIva.Border = 0;
            tabamort.AddCell(salIva);

            PdfPCell salMora = new PdfPCell(new Phrase("Saldo Moratorios", fuente4));
            salMora.HorizontalAlignment = Element.ALIGN_LEFT;
            salMora.VerticalAlignment = Element.ALIGN_MIDDLE;
            salMora.Border = 0;
            tabamort.AddCell(salMora);

            EdoCuent enca = new EdoCuent();
            enca.empresa = sesiones[2];
            enca.sucursal = sesiones[3];
            enca.grupo = sesiones[4];
            int idclienteTab = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
            enca.idcliente = idclienteTab;
            enca.obtenGrid();

            decimal noPago = 0;
            string fechaPago = "";
            string fechaApl = "";
            string referen = "";
            decimal abonoCli = 0;
            decimal capital = 0;
            decimal intereses = 0;
            decimal ivaCli = 0;
            decimal intMorato = 0;
            decimal SaldCap = 0;
            decimal saldoIva = 0;
            decimal salMoratorio = 0;
            if (Convert.ToBoolean(enca.retorno[0]))
            {
                DataSet ds = (DataSet)enca.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    noPago = Convert.ToUInt32(r[6]);
                    fechaPago = Convert.ToDateTime(r[7]).ToString("yyyy/MM/dd");
                    fechaApl = Convert.ToDateTime(r[8]).ToString("yyyy/MM/dd");
                    referen = Convert.ToString(r[9]);
                    abonoCli = Convert.ToDecimal(r[10]);
                    capital = Convert.ToDecimal(r[11]);
                    SaldCap = Convert.ToDecimal(r[12]);

                    PdfPCell no1 = new PdfPCell(new Phrase(noPago.ToString(), fuente4));
                    no1.HorizontalAlignment = Element.ALIGN_LEFT;
                    no1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    no1.Border = 0;
                    tabamort.AddCell(no1);

                    PdfPCell fec1 = new PdfPCell(new Phrase(fechaPago, fuente4));
                    fec1.HorizontalAlignment = Element.ALIGN_LEFT;
                    fec1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    fec1.Border = 0;
                    tabamort.AddCell(fec1);

                    PdfPCell fecApl1 = new PdfPCell(new Phrase(fechaApl, fuente4));
                    fecApl1.HorizontalAlignment = Element.ALIGN_LEFT;
                    fecApl1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    fecApl1.Border = 0;
                    tabamort.AddCell(fecApl1);

                    PdfPCell mov1 = new PdfPCell(new Phrase(referen, fuente4));
                    mov1.HorizontalAlignment = Element.ALIGN_LEFT;
                    mov1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    mov1.Border = 0;
                    tabamort.AddCell(mov1);

                    PdfPCell abono1 = new PdfPCell(new Phrase(abonoCli.ToString("C2"), fuente4));
                    abono1.HorizontalAlignment = Element.ALIGN_LEFT;
                    abono1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    abono1.Border = 0;
                    tabamort.AddCell(abono1);

                    PdfPCell cap1 = new PdfPCell(new Phrase(capital.ToString("C2"), fuente4));
                    cap1.HorizontalAlignment = Element.ALIGN_LEFT;
                    cap1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cap1.Border = 0;
                    tabamort.AddCell(cap1);

                    PdfPCell inte1 = new PdfPCell(new Phrase(intereses.ToString("C2"), fuente4));
                    inte1.HorizontalAlignment = Element.ALIGN_LEFT;
                    inte1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    inte1.Border = 0;
                    tabamort.AddCell(inte1);

                    PdfPCell iva1 = new PdfPCell(new Phrase(ivaCli.ToString("C2"), fuente4));
                    iva1.HorizontalAlignment = Element.ALIGN_LEFT;
                    iva1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    iva1.Border = 0;
                    tabamort.AddCell(iva1);

                    PdfPCell IntMo1 = new PdfPCell(new Phrase(intMorato.ToString("C2"), fuente4));
                    IntMo1.HorizontalAlignment = Element.ALIGN_LEFT;
                    IntMo1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    IntMo1.Border = 0;
                    tabamort.AddCell(IntMo1);

                    PdfPCell salCap1 = new PdfPCell(new Phrase(SaldCap.ToString("C2"), fuente4));
                    salCap1.HorizontalAlignment = Element.ALIGN_LEFT;
                    salCap1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    salCap1.Border = 0;
                    tabamort.AddCell(salCap1);

                    PdfPCell salIva1 = new PdfPCell(new Phrase(saldoIva.ToString("C2"), fuente4));
                    salIva1.HorizontalAlignment = Element.ALIGN_LEFT;
                    salIva1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    salIva1.Border = 0;
                    tabamort.AddCell(salIva1);

                    PdfPCell salMora1 = new PdfPCell(new Phrase(salMoratorio.ToString("C2"), fuente4));
                    salMora1.HorizontalAlignment = Element.ALIGN_LEFT;
                    salMora1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    salMora1.Border = 0;
                    tabamort.AddCell(salMora1);
                }
            }

            documento.Add(tabamort);


            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));


            
            documento.Add(piePag);            
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
}