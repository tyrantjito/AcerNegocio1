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

public partial class Validaciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
    protected void RadGrid2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Vali obtiene = new Vali();
        int[] sesiones = obtieneSesiones();
        obtiene.empresa = sesiones[2];
        obtiene.sucursal = sesiones[3];
        int id_cliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        obtiene.id_cliente = id_cliente;
        obtiene.obtieneFicha();
        SqlDataSource1.SelectCommand = "select f.id_ficha,f.id_cliente,c.nombre_completo from an_ficha_datos f inner join an_clientes c on f.id_cliente= c.id_cliente where f.id_empresa="+sesiones[2]+" and f.id_sucursal="+sesiones[3]+" and f.id_cliente="+id_cliente;

        SqlDataSource4.SelectCommand = "select a.id_visita,b.nombre_completo,a.id_cliente,convert(char(10),a.fecha_visita,120) as fecha_visita,a.gerente_sucursal_visita from AN_visita_ocular a inner join an_clientes b on b.id_cliente = a.id_cliente where a.id_sucursal = "+sesiones[2]+" and a.id_empresa = "+sesiones[3]+" and a.id_cliente ="+id_cliente+" order by a.id_visita desc";

        SqlDataSource5.SelectCommand = "select id_apago,id_cliente,grupo_productivo_apago,nombre_cliente_apago from an_analisis_pago where id_sucursal="+sesiones[3]+" and id_empresa="+sesiones[2]+" and id_cliente="+id_cliente+" order by id_apago desc";
        lnkFicha.Visible = true;
        lnkImprimir.Visible = true;
        RadGridFD.Visible = true;
        RadGridVO.Visible = true;
        RadGridCP.Visible = true;
        lnkVisita.Visible = true;
        lnkCapacidadPago.Visible = true;
    }

    protected void lnkImprimir_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;

        //tipos de font a utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de un nuevo documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" Consulta Crédito ");
        documento.AddCreator("AserNegocio1");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\ConsultaBuro_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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
            //el encabezado
            PdfPTable tablaEncabezado = new PdfPTable(1);
            tablaEncabezado.SetWidths(new float[] { 10f });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;


            PdfPCell titulo = new PdfPCell(new Phrase("Autorización para solicitar reportes de crédito" + Environment.NewLine + " Persona Física / Persona Moral ", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL)));

            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(titulo);
            documento.Add(tablaEncabezado);
            documento.Add(new Paragraph(" "));

            //texto 
            PdfPTable cuerpotext = new PdfPTable(1);
            cuerpotext.DefaultCell.Border = 0;
            cuerpotext.WidthPercentage = 100f;

            PdfPCell txt = new PdfPCell(new Phrase("Por este conduto autoriza expresamente a ASESORIA Y SERVICIO RURAL Y URBANO, S.A. de C.V. SOFOM ENR, para que por conducto de sus funcionarios facultados lleve a cabo investigaciones sobre mi comportamiento crediticio o el de la empresa que represento en CIRCULO DE CREDITO, S.A. de C.V.", fuente8));
            txt.Border = 0;
            txt.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cuerpotext.AddCell(txt);
            documento.Add(cuerpotext);
            documento.Add(new Paragraph(""));

            PdfPTable cuerpotext2 = new PdfPTable(1);
            cuerpotext2.DefaultCell.Border = 0;
            cuerpotext2.WidthPercentage = 100f;

            PdfPCell txt2 = new PdfPCell(new Phrase("Así mismo, declaro que conozco la naturaleza y alcance de las Sociedades de Información contenida en los reportes de crédito y reporte de crédito especial, declaro que conozco la naturaleza y alcance de la información que se solicitará, del uso que ASESORIA Y SERVICIO RURAL Y URBANO, S.A. de C.V. SOFOM ENR hará de la infomación, de que ésta podra realizar consultas periódicas sobre mi historial o el de la empresa que represento, consintiendo que esta autorización se encuentre vigente por un periodo de tres años contados a partir de su expedición y en todo caso durante el tiempo que se mantenga la relación juridica.", fuente8));
            txt2.Border = 0;
            txt2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cuerpotext2.AddCell(txt2);
            documento.Add(cuerpotext2);
            documento.Add(new Paragraph(""));

            PdfPTable cuerpotext3 = new PdfPTable(1);
            cuerpotext3.DefaultCell.Border = 0;
            cuerpotext3.WidthPercentage = 100f;

            PdfPCell txt3 = new PdfPCell(new Phrase("En caso de que el solicitante sea una Persona Moral, declaro bajo portesta de decir la verdad ser Representante Legal de la Empresa mencionada en esta autorización; manifestando que a la fecha de firma de la presente autorización de los poderes no me han sido revocados, ni modificados en forma alguna.", fuente8));
            txt3.Border = 0;
            txt3.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cuerpotext3.AddCell(txt3);
            documento.Add(cuerpotext3);
            documento.Add(new Paragraph(" "));

            //autorización encabezado.

            ConsBuro info = new ConsBuro();
            int[] sesiones = obtieneSesiones();
            info.empresa = sesiones[2];
            info.sucursal = sesiones[3];
            int idConsulta = Convert.ToInt32(RadGrid2.SelectedValues["id_consulta"]);
            int idCliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
            info.idConsultaEdita = idConsulta;
            info.idClienteEdita = idCliente;
            info.optieneimoresion();

            if (Convert.ToBoolean(info.retorno[0]))
            {
                DataSet ds = (DataSet)info.retorno[1];

                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    PdfPTable AutoEnca = new PdfPTable(1);
                    AutoEnca.DefaultCell.Border = 0;
                    AutoEnca.WidthPercentage = 100f;
                    string personaFIS = r[2].ToString();
                    string personaFISC = r[2].ToString();
                    string personaMOR = r[2].ToString();
                    if (personaFIS == "FIS")
                    {
                        personaFIS = "X";
                    }
                    else
                    {
                        personaFIS = "";
                    }
                    if (personaFISC == "MOR")
                    {
                        personaFISC = "X";
                    }
                    else
                    {
                        personaFISC = "";
                    }
                    if (personaMOR == "FAE")
                    {
                        personaMOR = "X";
                    }
                    else
                    {
                        personaMOR = "";
                    }


                    PdfPCell encaAuto = new PdfPCell(new Phrase("Autorización para : ", fuente10));
                    encaAuto.Border = 0;
                    encaAuto.HorizontalAlignment = 1;
                    encaAuto.VerticalAlignment = Element.ALIGN_MIDDLE;
                    AutoEnca.AddCell(encaAuto);
                    documento.Add(AutoEnca);

                    //personas
                    PdfPTable person = new PdfPTable(3);
                    person.DefaultCell.Border = 0;
                    person.WidthPercentage = 100f;

                    PdfPCell per1 = new PdfPCell(new Phrase("Persona Fisica" + " " + personaFIS, fuente10));
                    per1.Border = 0;
                    per1.HorizontalAlignment = 1;
                    per1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    person.AddCell(per1);


                    PdfPCell per2 = new PdfPCell(new Phrase("Persona física con actividad empresarial" + " " + personaFISC, fuente10));
                    per2.Border = 0;
                    per2.HorizontalAlignment = 1;
                    per2.VerticalAlignment = Element.ALIGN_MIDDLE;
                    person.AddCell(per2);

                    PdfPCell per3 = new PdfPCell(new Phrase("Persona Moral" + " " + personaMOR, fuente10));
                    per3.Border = 0;
                    per3.HorizontalAlignment = 1;
                    per3.VerticalAlignment = Element.ALIGN_MIDDLE;
                    person.AddCell(per3);
                    documento.Add(person);
                    documento.Add(new Paragraph(" "));

                    //campos a llenar con bases de datos

                    //nombre del solicitante
                    PdfPTable nameSol = new PdfPTable(1);
                    nameSol.DefaultCell.Border = 0;
                    nameSol.WidthPercentage = 100f;
                    string nombre = r[0].ToString();

                    PdfPCell name = new PdfPCell(new Phrase("Nombre de solicitante (Persona físca o razón social de la presona moral :" + " " + nombre, fuente8));
                    name.Border = 0;
                    name.HorizontalAlignment = 1;
                    name.HorizontalAlignment = Element.ALIGN_LEFT;
                    nameSol.AddCell(name);
                    documento.Add(nameSol);

                    //nombre del representante legal
                    PdfPTable nameRe = new PdfPTable(1);
                    nameRe.DefaultCell.Border = 0;
                    nameRe.WidthPercentage = 100f;
                    string nombrerep = r[3].ToString();

                    PdfPCell nameRep = new PdfPCell(new Phrase("Para el caso de Persona Moral, nombre de Representante Legal :" + " " + nombrerep, fuente8));
                    nameRep.Border = 0;
                    nameRep.HorizontalAlignment = 1;
                    nameRep.HorizontalAlignment = Element.ALIGN_LEFT;
                    nameRe.AddCell(nameRep);
                    documento.Add(nameRe);

                    //RFC/CURP

                    PdfPTable curp = new PdfPTable(1);
                    curp.DefaultCell.Border = 0;
                    curp.WidthPercentage = 100f;
                    string Rfcurp = r[1].ToString();

                    PdfPCell rfc = new PdfPCell(new Phrase("RFC/CURP :" + " " + Rfcurp, fuente8));
                    rfc.Border = 0;
                    rfc.HorizontalAlignment = 1;
                    rfc.HorizontalAlignment = Element.ALIGN_LEFT;
                    curp.AddCell(rfc);
                    documento.Add(curp);

                    //Domicilio

                    PdfPTable dom = new PdfPTable(1);
                    dom.DefaultCell.Border = 0;
                    dom.WidthPercentage = 100f;
                    string domicilio = r[4].ToString();

                    PdfPCell direc = new PdfPCell(new Phrase("Domicilio Calle y Número : " + " " + domicilio, fuente8));
                    direc.Border = 0;
                    direc.HorizontalAlignment = 1;
                    direc.HorizontalAlignment = Element.ALIGN_LEFT;
                    dom.AddCell(direc);
                    documento.Add(dom);

                    //Colonia / municipio / estado
                    PdfPTable reside = new PdfPTable(3);
                    reside.DefaultCell.Border = 0;
                    reside.WidthPercentage = 100f;
                    string colon = r[6].ToString();
                    string municip = r[7].ToString();
                    string estad = r[8].ToString();

                    PdfPCell col = new PdfPCell(new Phrase("Colonia : " + " " + colon, fuente8));
                    col.Border = 0;
                    col.HorizontalAlignment = 1;
                    col.HorizontalAlignment = Element.ALIGN_LEFT;
                    reside.AddCell(col);
                    PdfPCell muni = new PdfPCell(new Phrase("Municipio :" + " " + municip, fuente8));
                    muni.Border = 0;
                    muni.HorizontalAlignment = 1;
                    muni.HorizontalAlignment = Element.ALIGN_LEFT;
                    reside.AddCell(muni);
                    PdfPCell est = new PdfPCell(new Phrase("Estado : " + " " + estad, fuente8));
                    est.Border = 0;
                    est.HorizontalAlignment = 1;
                    est.HorizontalAlignment = Element.ALIGN_LEFT;
                    reside.AddCell(est);
                    documento.Add(reside);



                    //Codigo postal / telefonos
                    PdfPTable codp = new PdfPTable(2);
                    codp.DefaultCell.Border = 0;
                    codp.WidthPercentage = 100f;
                    string codipos = r[9].ToString();
                    string telefon = r[10].ToString();

                    PdfPCell cp = new PdfPCell(new Phrase("Código Postal : " + " " + codipos, fuente8));
                    cp.Border = 0;
                    cp.HorizontalAlignment = 1;
                    cp.HorizontalAlignment = Element.ALIGN_LEFT;
                    codp.AddCell(cp);
                    documento.Add(codp);

                    PdfPCell tel = new PdfPCell(new Phrase("Teléfono(s): " + " " + telefon, fuente8));
                    tel.Border = 0;
                    tel.HorizontalAlignment = 1;
                    tel.HorizontalAlignment = Element.ALIGN_LEFT;
                    codp.AddCell(tel);
                    documento.Add(codp);

                    //lugar y fecha de firma autorizada
                    PdfPTable firau = new PdfPTable(1);
                    firau.DefaultCell.Border = 0;
                    firau.WidthPercentage = 100f;
                    string lugaryfecha = r[11].ToString();
                    string fechaAuFu = r[12].ToString();

                    PdfPCell lug = new PdfPCell(new Phrase("Lugar y fecha en que se firma a autorización : " + " " + lugaryfecha + " " + fechaAuFu, fuente8));
                    lug.Border = 0;
                    lug.HorizontalAlignment = 1;
                    lug.HorizontalAlignment = Element.ALIGN_LEFT;
                    firau.AddCell(lug);
                    documento.Add(firau);

                    //Nombre del funcionario
                    PdfPTable funci = new PdfPTable(1);
                    funci.DefaultCell.Border = 0;
                    funci.WidthPercentage = 100f;
                    string nomfunci = r[13].ToString();

                    PdfPCell nomfu = new PdfPCell(new Phrase("Nombre del funcionario que recaba la autorización : " + " " + nomfunci, fuente8));
                    nomfu.Border = 0;
                    nomfu.HorizontalAlignment = 1;
                    nomfu.HorizontalAlignment = Element.ALIGN_LEFT;
                    funci.AddCell(nomfu);
                    documento.Add(funci);
                    documento.Add(new Paragraph(" "));

                    //texto  faltante
                    PdfPTable textoSeg = new PdfPTable(1);
                    textoSeg.DefaultCell.Border = 0;
                    textoSeg.WidthPercentage = 100f;

                    PdfPCell segun = new PdfPCell(new Phrase("Estoy consciente y acepto que este documnto quede bajo custodia de ASESORIA Y SERVICIO RURAL Y URBANOS S.A. de C.V. SOFOM ENR y/o la Sociedad de Información Crediticia consultada para efectos de control y cumplimiento del artículo 28 de la Ley para Regular las Sociedades de Información Crediticia; mismo que señala que las Sciedades solo podrán preporcionar información a un usuario cuando este cuente con la autorización expresa del Cliente mediate su firma autógrafa", fuente8));
                    segun.Border = 0;
                    segun.HorizontalAlignment = 1;
                    segun.HorizontalAlignment = Element.ALIGN_LEFT;
                    textoSeg.AddCell(segun);
                    documento.Add(textoSeg);
                    documento.Add(new Paragraph(" "));

                    //encabezado de la firma
                    PdfPTable pfae = new PdfPTable(1);
                    pfae.DefaultCell.Border = 0;
                    pfae.WidthPercentage = 100f;

                    PdfPCell firmapf = new PdfPCell(new Phrase("Nombre y firma del PF, PFAE o Representante legal de la Empresa ", fuente10));
                    firmapf.Border = 0;
                    firmapf.HorizontalAlignment = 1;
                    firmapf.VerticalAlignment = Element.ALIGN_MIDDLE;
                    pfae.AddCell(firmapf);
                    documento.Add(pfae);
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));

                    //guion bajo para la firma
                    PdfPTable guion = new PdfPTable(1);
                    guion.DefaultCell.Border = 0;
                    guion.WidthPercentage = 100f;

                    PdfPCell bajo = new PdfPCell(new Phrase("_____________________________________________________________ ", fuente8));
                    bajo.Border = 0;
                    bajo.HorizontalAlignment = 1;
                    bajo.VerticalAlignment = Element.ALIGN_MIDDLE;
                    guion.AddCell(bajo);
                    documento.Add(guion);
                    //texto abajo de la firma
                    PdfPTable txtfir = new PdfPTable(1);
                    txtfir.DefaultCell.Border = 0;
                    txtfir.WidthPercentage = 100f;

                    PdfPCell firtxt = new PdfPCell(new Phrase("Para uso exclusivo de la Empresa que efectúa la consulta, ", fuente10));
                    firtxt.Border = 0;
                    firtxt.HorizontalAlignment = 1;
                    firtxt.VerticalAlignment = Element.ALIGN_MIDDLE;
                    txtfir.AddCell(firtxt);
                    documento.Add(txtfir);

                    PdfPTable txtfir2 = new PdfPTable(1);
                    txtfir2.DefaultCell.Border = 0;
                    txtfir2.WidthPercentage = 100f;

                    PdfPCell firtxt2 = new PdfPCell(new Phrase("ASESORIA Y SERVICIO RURAL S.A. de C.V. SOFOM ENR ", fuente10));
                    firtxt2.Border = 0;
                    firtxt2.HorizontalAlignment = 1;
                    firtxt2.VerticalAlignment = Element.ALIGN_MIDDLE;
                    txtfir2.AddCell(firtxt2);
                    documento.Add(txtfir2);
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));

                    // FECHA DE CONSUKTA Y FOLIO DE LA CONSULTA
                    PdfPTable fecfol = new PdfPTable(2);
                    fecfol.DefaultCell.Border = 0;
                    fecfol.WidthPercentage = 100f;
                    string fechaconsul = r[14].ToString();
                    string folioconsul = r[15].ToString();


                    PdfPCell fechaC = new PdfPCell(new Phrase("Fecha de consulta CC ):" + " " + fechaconsul, fuente8));
                    fechaC.Border = 0;
                    fechaC.HorizontalAlignment = 1;
                    fechaC.HorizontalAlignment = Element.ALIGN_LEFT;
                    fecfol.AddCell(fechaC);
                    documento.Add(fecfol);

                    PdfPCell folioC = new PdfPCell(new Phrase("Folio de consulta CC):" + " " + folioconsul, fuente8));
                    folioC.Border = 0;
                    folioC.HorizontalAlignment = 1;
                    folioC.HorizontalAlignment = Element.ALIGN_CENTER;
                    fecfol.AddCell(folioC);
                    documento.Add(fecfol);
                    documento.Add(new Paragraph(" "));
                    //Aviso importante
                    PdfPTable aviso = new PdfPTable(1);
                    aviso.DefaultCell.Border = 0;
                    aviso.WidthPercentage = 100f;



                    PdfPCell txtA = new PdfPCell(new Phrase("IMPORTANTE. Este formato debe ser llenado de forma individual para una sola persona física o para una sola empresa. En caso de requerir el historial crediticio del representante legal, favor de llenar un formato adicional. ", fuente8));
                    txtA.Border = 0;
                    txtA.HorizontalAlignment = 1;
                    txtA.HorizontalAlignment = Element.ALIGN_LEFT;
                    aviso.AddCell(txtA);
                    documento.Add(aviso);
                    //para abrirlo en navegador el documento

                    //documento.Add(new Paragraph(""));
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
    protected void lnkImprimirFicha_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;

        // Tipo de Font que vamos utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();


        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" FichaDeDatos ");
        documento.AddCreator("AserNegocio1");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\FichaDeDatos_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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


            PdfPCell titulo = new PdfPCell(new Phrase("ASESORIA Y SERVICIO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + " FICHA DE DATOS ", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD)));
            //PdfPCell titulo = new PdfPCell(new Phrase("Gaarve S.A de C.V. " + Environment.NewLine + Environment.NewLine + " Informe de Nomina " + Environment.NewLine + Environment.NewLine + strDatosObra, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(logo);
            tablaEncabezado.AddCell(titulo);

            documento.Add(tablaEncabezado);

            documento.Add(new Paragraph(" "));
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
            int idcons = Convert.ToInt32(RadGridFD.SelectedValues["id_cliente"]);
            int id_fichaD = Convert.ToInt32(RadGridFD.SelectedValues["id_ficha"]);
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
                    PdfPTable fech = new PdfPTable(9);
                    fech.WidthPercentage = 100f;
                    int[] fechcellwidth = { 20, 20, 5, 5, 8, 8, 8, 8, 8 };
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

                    PdfPCell sexoF = new PdfPCell(new Phrase("H  \n " + H, f));
                    sexoF.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(sexoF);

                    PdfPCell sexoM = new PdfPCell(new Phrase("M  \n " + M, f2));
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
                    int[] credcellwidth = { 15, 25, 15, 25, 5, 15 };
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
                    PdfPTable school = new PdfPTable(9);
                    school.WidthPercentage = 100f;
                    int[] schoolcellwidth = { 12, 11, 11, 11, 11, 11, 11, 11, 11 };
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

                    PdfPCell rol = (new PdfPCell(new Phrase("ROL DEL CLIENTE EN EL HOGAR (Marque con una X)", fuente2)) { Colspan = 6 });
                    rol.HorizontalAlignment = Element.ALIGN_CENTER;
                    rol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    school.AddCell(rol);

                    PdfPCell sinIn = new PdfPCell(new Phrase("SIN INSTRUCCION  \n" + escolaridad, es));
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

                    PdfPCell depEc = new PdfPCell(new Phrase("DEP. ECONOMICOS", fuente2));
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
                    documento.Add(new Paragraph(" "));

                    //

                    //domicilio
                    PdfPTable domic = new PdfPTable(1);
                    domic.WidthPercentage = 100f;
                    int[] domiccellwidth = { 100 };
                    domic.SetWidths(domiccellwidth);

                    PdfPCell domicilio = new PdfPCell(new Phrase("DOMICILIO", fuente2));
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
                    PdfPTable codP = new PdfPTable(5);
                    codP.WidthPercentage = 100f;
                    int[] codPcellwidth = { 15, 30, 20, 20, 15 };
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

                    PdfPCell timeR = new PdfPCell(new Phrase("TIEMPO DE RESIDENCIA", fuente2));
                    timeR.HorizontalAlignment = Element.ALIGN_CENTER;
                    timeR.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codP.AddCell(timeR);

                    PdfPCell noHA = new PdfPCell(new Phrase("No. DE HABITANTES", fuente2));
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
                    documento.Add(condicionP);

                    //datos generales
                    PdfPTable datosGen = new PdfPTable(3);
                    datosGen.WidthPercentage = 100f;
                    int[] datosGencellwidth = { 30, 30, 40 };
                    datosGen.SetWidths(datosGencellwidth);
                    string apellidopc = r[28].ToString();
                    string apellidomc = r[29].ToString();
                    string nombresc = r[30].ToString();

                    documento.Add(new Paragraph(" "));

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
                    documento.Add(new Paragraph(" "));

                    //info neg.
                    PdfPTable info = new PdfPTable(1);
                    info.WidthPercentage = 100f;
                    int[] infocellwidth = { 100 };
                    info.SetWidths(infocellwidth);

                    PdfPCell infonego = new PdfPCell(new Phrase("INFORMACIÓN DEL NEGOCIO", fuente2));
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

                    PdfPCell timeRNeg = new PdfPCell(new Phrase("TIEMPO DE RESIDENCIA", fuente2));
                    timeRNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    timeRNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codPNeg.AddCell(timeRNeg);

                    PdfPCell noHANeg = new PdfPCell(new Phrase("No. DE HABITANTES", fuente2));
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

                    PdfPCell tipEst = (new PdfPCell(new Phrase("TIPO DE ESTABLECIMIENTO", fuente2)) { Colspan = 3 });
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

                    PdfPCell croNC = new PdfPCell(new Phrase("COQUIS DE UBICACIÓN DEL DOMICILIO DEL CLIENTE", fuente2));
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

                    PdfPCell refUbi = (new PdfPCell(new Phrase("PRINCIPALES REFERENCIAS DE UBICACIÓN", fuente6)) { Rowspan = 5 });
                    refUbi.HorizontalAlignment = Element.ALIGN_CENTER;
                    ubiRe.AddCell(refUbi);

                    PdfPCell refUbi1 = (new PdfPCell(new Phrase("PRINCIPALES REFERENCIAS DE UBICACIÓN", fuente6)) { Rowspan = 5 });
                    refUbi1.HorizontalAlignment = Element.ALIGN_CENTER;
                    ubiRe.AddCell(refUbi1);
                    documento.Add(ubiRe);

                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));


                    //referencias personales (llenado de datos
                    PdfPTable refeCli = new PdfPTable(6);
                    refeCli.WidthPercentage = 100f;
                    int[] refeClicellwidth = { 2, 28, 20, 30, 10, 10 };
                    refeCli.SetWidths(refeClicellwidth);
                    string nombreref = r[53].ToString();
                    string telfijoclrf = r[54].ToString();
                    string telceluref = r[55].ToString();
                    string parentesco = r[56].ToString();
                    string timeconocerl = r[57].ToString();

                    PdfPCell encaRefe = (new PdfPCell(new Phrase("REFERENCIAS DEL CLIENTE (PERSONAL Y FAMILIAR, QUE NO VIVAN EN EL MISMO DOMICILIO", fuente8)) { Colspan = 6 });
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
                    documento.Add(new Paragraph(" "));

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


                    PdfPCell ocupado = (new PdfPCell(new Phrase("¿USTED HA OCUPADO CARGOS PÚBLICOS DESTACADOS EN LOS ÚLTIMOS DOCE MESES?", fuente2)) { Colspan = 5 });
                    ocupado.HorizontalAlignment = Element.ALIGN_LEFT;
                    SegHo.AddCell(ocupado);

                    PdfPCell persPol = (new PdfPCell(new Phrase("(Pesona politicamente expuesta, entre otros: Jefe de estado, de Gobierno, Líder Politico, Senador, Diputado, Presidente Municipal, miembro del Partido político, Judicial o Militar de cualquier gerarquía", fuente10)) { Colspan = 5, });
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

                    PdfPCell cargoDesem1 = new PdfPCell(new Phrase(" " + cargodes.ToUpper(), fuente8));
                    cargoDesem1.HorizontalAlignment = Element.ALIGN_CENTER;
                    carDe.AddCell(cargoDesem1);

                    PdfPCell depenDen1 = new PdfPCell(new Phrase("DEPENDENCIA", fuente2));
                    depenDen1.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell periodoDe1 = new PdfPCell(new Phrase("PERIODO", fuente2));
                    periodoDe1.HorizontalAlignment = Element.ALIGN_CENTER;
                    carDe.AddCell(periodoDe1);
                    documento.Add(carDe);
                    documento.Add(new Paragraph(" "));

                    //INFORMACIÓN REFERENTE AL PROPIETARIO
                    PdfPTable infoRefP = new PdfPTable(3);
                    infoRefP.WidthPercentage = 100f;
                    int[] infoRefPcellwidth = { 20, 20, 60 };
                    infoRefP.SetWidths(infoRefPcellwidth);

                    string apellipa = r[68].ToString();
                    string apellima = r[69].ToString();
                    string nombrs = r[70].ToString();

                    PdfPCell encaRefP = (new PdfPCell(new Phrase("INFORMACIÓN PREFERENCIA AL PROPIETARIO REAL", fuente2)) { Colspan = 3 });
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

                    PdfPCell naciRe = new PdfPCell(new Phrase("NACIONAIDAD", fuente2));
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

                    PdfPCell nini = new PdfPCell(new Phrase("SIN INSTRUCCION  \n " + escolaridad1, EST));
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


                    PdfPCell ocupDos = new PdfPCell(new Phrase("OCUPACION", fuente2));
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

                    PdfPCell domiTr3s = (new PdfPCell(new Phrase("DOMICILIO", fuente2)) { Colspan = 4 });
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

                    PdfPCell codiPost = new PdfPCell(new Phrase("CODIGO POSTAL", fuente2));
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


                    PdfPCell encaRefPRO = (new PdfPCell(new Phrase("INFORMACION REFERENTEAL PROVEEDOR DE RECURSO HUMANOS", fuente2)) { Colspan = 3 });
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

                    PdfPCell estCiPRO5 = new PdfPCell(new Phrase("UNION LIBRE  \n" + UL6, EC5));
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

                    PdfPCell niniPRO = new PdfPCell(new Phrase("SIN INSTRUCCION  \n" + escolaridad01, Ees));
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
                    documento.Add(new Paragraph(" "));

                    //mas tablas 
                    PdfPTable tablaDosPRO = new PdfPTable(4);
                    tablaDosPRO.WidthPercentage = 100f;
                    int[] tablaDosPROcellwidth = { 35, 15, 25, 25 };
                    tablaDosPRO.SetWidths(tablaDosPROcellwidth);

                    string ocupacion6 = r[113].ToString();
                    string fijo6 = r[114].ToString();
                    string celular5 = r[115].ToString();
                    string email6 = r[116].ToString();

                    PdfPCell ocupDosPRO = new PdfPCell(new Phrase("OCUPACION", fuente2));
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

                    PdfPCell domiTr3sPRO = (new PdfPCell(new Phrase("DOMICILIO", fuente2)) { Colspan = 4 });
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

                    PdfPCell codiPostPRO = new PdfPCell(new Phrase("CODIGO POSTAL", fuente2));
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

                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));
                    //firma de enmedio
                    PdfPTable tabFirm = new PdfPTable(1);
                    tabFirm.WidthPercentage = 30f;
                    int[] tabFirmcellwidth = { 100 };
                    tabFirm.SetWidths(tabFirmcellwidth);

                    PdfPCell firmaTa = new PdfPCell(new Phrase("_______________________________________________", fuente6));
                    firmaTa.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmaTa.Border = 0;
                    tabFirm.AddCell(firmaTa);

                    PdfPCell firmaTa1 = new PdfPCell(new Phrase("NOMBBRE Y FIRMA DEL CLIENTE", fuente10));
                    firmaTa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmaTa1.Border = 0;
                    tabFirm.AddCell(firmaTa1);
                    documento.Add(tabFirm);
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));

                    //firmas restantes
                    PdfPTable tabFirm2 = new PdfPTable(2);
                    tabFirm2.WidthPercentage = 100f;
                    int[] tabFirm2cellwidth = { 50, 50 };
                    tabFirm2.SetWidths(tabFirm2cellwidth);

                    PdfPCell rayon = new PdfPCell(new Phrase("________________________________________________________", fuente6));
                    rayon.HorizontalAlignment = Element.ALIGN_CENTER;
                    rayon.Border = 0;
                    tabFirm2.AddCell(rayon);

                    PdfPCell rayon2 = new PdfPCell(new Phrase("________________________________________________________", fuente6));
                    rayon2.HorizontalAlignment = Element.ALIGN_CENTER;
                    rayon2.Border = 0;
                    tabFirm2.AddCell(rayon2);

                    PdfPCell rayon3 = new PdfPCell(new Phrase("NOMBRE Y PUESTO DE QUIEN ENTREVISTO", fuente10));
                    rayon3.HorizontalAlignment = Element.ALIGN_CENTER;
                    rayon3.Border = 0;
                    tabFirm2.AddCell(rayon3);

                    PdfPCell rayon4 = new PdfPCell(new Phrase("NOMBRE Y FIRMA GERENTE SUCURSAL", fuente10));
                    rayon4.HorizontalAlignment = Element.ALIGN_CENTER;
                    rayon4.Border = 0;
                    tabFirm2.AddCell(rayon4);

                    PdfPCell rayon5 = new PdfPCell(new Phrase("PUESTO_________________________________", fuente10));
                    rayon5.HorizontalAlignment = Element.ALIGN_CENTER;
                    rayon5.Border = 0;
                    tabFirm2.AddCell(rayon5);

                    PdfPCell rayon6 = new PdfPCell(new Phrase(" ", fuente10));
                    rayon6.HorizontalAlignment = Element.ALIGN_CENTER;
                    rayon6.Border = 0;
                    tabFirm2.AddCell(rayon6);
                    documento.Add(tabFirm2);
                    documento.Add(new Paragraph(" "));
                    //TABLA FINAL DE POLITICAS
                    PdfPTable FINAL = new PdfPTable(1);
                    FINAL.WidthPercentage = 100f;
                    int[] FINALcellwidth = { 100 };
                    FINAL.SetWidths(FINALcellwidth);

                    PdfPCell final1 = new PdfPCell(new Phrase("Declaro bajo protesta de decir la verdad que la información asentada y los documentos proporcionados para esta solicitud sol verdaderos y correctos, así mismo me encuentro voluntariamente enterado del contenido del aviso de privacidad de ASESORIA Y SERVICIO RURAL Y URBANO S.A. de C.V. SOFOM ENR y sus alcances legales con fundamento en lo dispuesto por la Ley Federal de Protección de Datos Personales en posesión de los particulares y su reglamento para lo cual otorgo de manera voluntaria el más amplio consentimiento y facutad a la empresa ASESORIA Y SERVICIO RURAL Y URBANO S.A. de C.V. SOFOM ENR a utilizar mis datos personales. ASESORIA Y SERVICIO RURAL Y URBANO S.A. de C.V. SOFOM ENR se reserva el derecho de cambiar, modificar, complementar y/o alterarel presente aviso, en cualquier momento, en cuyo caso se hará de su conocimiento a través de los medios que establezala legislación en la materia", fuente10));
                    final1.HorizontalAlignment = Element.ALIGN_CENTER;
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
    protected void lnkVisita_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;
        // Tipo de Font que vamos utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" Visita Ocular ");
        documento.AddCreator("AserNegocio1");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\VisitaOcular_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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
            //logo.WidthPercentage = 15f;


            //encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.SetWidths(new float[] { 2f, 8f });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;


            PdfPCell titulo = new PdfPCell(new Phrase("ASESORIA Y SERVICIO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + " VISITA OCULAR ", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD)));
            //PdfPCell titulo = new PdfPCell(new Phrase("Gaarve S.A de C.V. " + Environment.NewLine + Environment.NewLine + " Informe de Nomina " + Environment.NewLine + Environment.NewLine + strDatosObra, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(logo);
            tablaEncabezado.AddCell(titulo);

            documento.Add(tablaEncabezado);

            documento.Add(new Paragraph(" "));


            VOcu infor = new VOcu();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            int id_cliente = Convert.ToInt32(RadGridVO.SelectedValues["id_cliente"]);
            infor.id_cliente = id_cliente;
            infor.optieneimpresion();


            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];
                string fechavisita = "";
                string Sucursal = "";
                string GrupoProductivo = "";
                string Numero = "";
                string TipoCredito = "";
                string AsesorCredito = ""; ;
                string entrelascalles1 = "";
                string Gerente = "";
                string edadclien = "";
                string clientdes = "";
                string Pro = "";
                string Ren = "";
                string Pre = "";
                string Otr = "";
                string Luz = "";
                string Agua = "";
                string Drenaje = "";
                string Tel = "";
                string Internet = "";
                string Gas = "";
                string Tv = "";
                string Tab = "";
                string Mad = "";
                string Otr1 = "";

                if (Tab == "Tab")
                { Tab = "X"; }
                else { Tab = " "; }

                if (Mad == "Mad")
                { Mad = ""; }
                else
                { Mad = " "; }

                if (Otr1 == "Otr1")
                { Otr1 = "X"; }
                else
                { Otr1 = " "; }

                string Sala = "";
                string Comedor = "";
                string Estufa = "";
                string Refrigerador = "";
                string Lavadora = "";
                string Television = "";
                string Compu = "";
                string Auto = "";
                string Marca = "";
                string Modelo = "";
                string Placas = "";
                string entrelascalles2 = "";
                string tiempneg = "";
                string principrov = "";
                string Pro1 = "";
                string Ren1 = "";
                string Fij1 = "";
                string Sem1 = "";

                if (Pro1 == "Pro")
                { Pro1 = "X"; }
                else { Pro1 = " "; }

                if (Ren1 == "Ren")
                { Ren1 = ""; }
                else
                { Ren1 = " "; }

                if (Fij1 == "Fij")
                { Fij1 = "X"; }
                else
                { Fij1 = " "; }

                if (Sem1 == "Sem")
                { Sem1 = "X"; }
                else
                { Sem1 = " "; }


                string OS = "";
                string aval2 = "";

                if (OS == "O.S")
                { OS = "OBLIGADO SOLIDARIO"; }
                else { OS = " "; }

                if (aval2 == "Ava")
                { aval2 = "AVAL"; }
                else
                { aval2 = " "; }

                string nombre3 = "";
                string apep = "";
                string apem = "";
                string fenac = "";
                string edad3 = "";
                string sexo3 = "";
                string sexo4 = "";

                if (sexo3 == "H")
                { sexo3 = "X"; }
                else { sexo3 = " "; }

                if (sexo4 == "M")
                { sexo4 = "X"; }
                else
                { sexo4 = " "; }

                string ocupacion = "";
                string cbinm = "";
                string cbinm1 = "";

                if (cbinm == "S")
                { cbinm = "X"; }
                else { cbinm = " "; }

                if (cbinm1 == "N")
                { cbinm1 = "X"; }
                else
                { cbinm1 = " "; }

                string valorest = "";
                string callegp = "";
                string numext = "";
                string numint = "";
                string colonia = "";
                string copostal = "";
                string munici = "";
                string estadogp = "";
                string telobli = "";

                foreach (DataRow r in ds.Tables[0].Rows)

                {
                    fechavisita = r[5].ToString();
                    Sucursal = r[1].ToString();
                    GrupoProductivo = r[6].ToString();
                    Numero = r[4].ToString();
                    TipoCredito = r[7].ToString();
                    AsesorCredito = r[9].ToString();
                    entrelascalles1 = r[12].ToString();
                    Gerente = r[8].ToString();
                    edadclien = r[10].ToString();
                    clientdes = r[11].ToString();
                    Pro = r[13].ToString();
                    Ren = r[13].ToString();
                    Pre = r[13].ToString();
                    Otr = r[13].ToString();

                    if (Pro == "Pro")
                    { Pro = "X"; }
                    else { Pro = " "; }

                    if (Ren == "Ren")
                    { Ren = ""; }
                    else
                    { Ren = " "; }

                    if (Pre == "Pre")
                    { Pre = "X"; }
                    else
                    { Pre = " "; }

                    if (Otr == "Otr")
                    { Otr = "X"; }
                    else
                    { Otr = " "; }
                    Luz = r[14].ToString();
                    Agua = r[15].ToString();
                    Drenaje = r[16].ToString();
                    Tel = r[17].ToString();
                    Internet = r[18].ToString();
                    Gas = r[19].ToString();
                    Tv = r[20].ToString();
                    Tab = r[21].ToString();
                    Mad = r[21].ToString();
                    Otr1 = r[21].ToString();

                    if (Tab == "Tab")
                    { Tab = "X"; }
                    else { Tab = " "; }

                    if (Mad == "Mad")
                    { Mad = ""; }
                    else
                    { Mad = " "; }

                    if (Otr1 == "Otr1")
                    { Otr1 = "X"; }
                    else
                    { Otr1 = " "; }

                    Sala = r[22].ToString();
                    Comedor = r[23].ToString();
                    Estufa = r[24].ToString();
                    Refrigerador = r[25].ToString();
                    Lavadora = r[26].ToString();
                    Television = r[27].ToString();
                    Compu = r[28].ToString();
                    Auto = r[29].ToString();
                    Marca = r[30].ToString();
                    Modelo = r[31].ToString();
                    Placas = r[32].ToString();
                    entrelascalles2 = r[33].ToString();
                    tiempneg = r[35].ToString();
                    principrov = r[36].ToString();
                    Pro1 = r[34].ToString();
                    Ren1 = r[34].ToString();
                    Fij1 = r[34].ToString();
                    Sem1 = r[34].ToString();

                    if (Pro1 == "Pro")
                    { Pro1 = "X"; }
                    else { Pro1 = " "; }

                    if (Ren1 == "Ren")
                    { Ren1 = ""; }
                    else
                    { Ren1 = " "; }

                    if (Fij1 == "Fij")
                    { Fij1 = "X"; }
                    else
                    { Fij1 = " "; }

                    if (Sem1 == "Sem")
                    { Sem1 = "X"; }
                    else
                    { Sem1 = " "; }

                    OS = r[37].ToString();
                    aval2 = r[37].ToString();

                    if (OS == "O.S")
                    { OS = "OBLIGADO SOLIDARIO"; }
                    else { OS = " "; }

                    if (aval2 == "Ava")
                    { aval2 = "AVAL"; }
                    else
                    { aval2 = " "; }

                    nombre3 = r[38].ToString();
                    apep = r[39].ToString();
                    apem = r[40].ToString();
                    fenac = r[41].ToString();
                    edad3 = r[42].ToString();
                    sexo3 = r[43].ToString();
                    sexo4 = r[43].ToString();

                    if (sexo3 == "H")
                    { sexo3 = "X"; }
                    else { sexo3 = " "; }

                    if (sexo4 == "M")
                    { sexo4 = "X"; }
                    else
                    { sexo4 = " "; }

                    ocupacion = r[44].ToString();
                    cbinm = r[45].ToString();
                    cbinm1 = r[45].ToString();

                    if (cbinm == "S")
                    { cbinm = "X"; }
                    else { cbinm = " "; }

                    if (cbinm1 == "N")
                    { cbinm1 = "X"; }
                    else
                    { cbinm1 = " "; }
                    valorest = r[46].ToString();
                    callegp = r[47].ToString();
                    numext = r[48].ToString();
                    numint = r[49].ToString();
                    colonia = r[50].ToString();
                    copostal = r[51].ToString();
                    munici = r[52].ToString();
                    estadogp = r[53].ToString();
                    telobli = r[55].ToString();



                }
                //FECHA
                PdfPTable fecha1 = new PdfPTable(2);
                fecha1.SetWidths(new float[] { 15, 25 });
                fecha1.DefaultCell.Border = 0;
                fecha1.WidthPercentage = 40f;
                fecha1.HorizontalAlignment = Element.ALIGN_LEFT;



                PdfPCell fec = new PdfPCell(new Phrase("FECHA DE VISITA ", fuente6));
                fec.VerticalAlignment = Element.ALIGN_CENTER;
                fec.BackgroundColor = BaseColor.LIGHT_GRAY;
                fecha1.AddCell(fec);

                PdfPCell fec1 = new PdfPCell(new Phrase("  " + Convert.ToDateTime(fechavisita).ToString("dd/MM/yyyy"), fuente8));
                fec1.HorizontalAlignment = Element.ALIGN_CENTER;
                fecha1.AddCell(fec1);
                documento.Add(fecha1);

                //datos 
                PdfPTable dat1 = new PdfPTable(4);
                dat1.SetWidths(new float[] { 15, 45, 20, 20 });
                dat1.DefaultCell.Border = 0;
                dat1.WidthPercentage = 100f;



                PdfPCell sucursal = new PdfPCell(new Phrase("SUCURSAL", fuente6));
                sucursal.VerticalAlignment = Element.ALIGN_CENTER;
                sucursal.BackgroundColor = BaseColor.LIGHT_GRAY;
                dat1.AddCell(sucursal);

                PdfPCell grupP = new PdfPCell(new Phrase("GRUPO PRODUCTIVO", fuente6));
                grupP.VerticalAlignment = Element.ALIGN_CENTER;
                grupP.BackgroundColor = BaseColor.LIGHT_GRAY;
                dat1.AddCell(grupP);

                PdfPCell nume = new PdfPCell(new Phrase("NÚMERO", fuente6));
                nume.VerticalAlignment = Element.ALIGN_CENTER;
                nume.BackgroundColor = BaseColor.LIGHT_GRAY;
                dat1.AddCell(nume);

                PdfPCell tipCre = new PdfPCell(new Phrase("TIPO DE CRÉDITO", fuente6));
                tipCre.VerticalAlignment = Element.ALIGN_CENTER;
                tipCre.BackgroundColor = BaseColor.LIGHT_GRAY;
                dat1.AddCell(tipCre);

                PdfPCell sucursal1 = new PdfPCell(new Phrase(" " + Sucursal, fuente8));
                sucursal1.VerticalAlignment = Element.ALIGN_CENTER;
                dat1.AddCell(sucursal1);

                PdfPCell grupP1 = new PdfPCell(new Phrase(" " + GrupoProductivo, fuente8));
                grupP1.VerticalAlignment = Element.ALIGN_CENTER;
                dat1.AddCell(grupP1);

                PdfPCell nume1 = new PdfPCell(new Phrase(" " + Numero, fuente8));
                nume1.VerticalAlignment = Element.ALIGN_CENTER;
                dat1.AddCell(nume1);

                PdfPCell tipCre1 = new PdfPCell(new Phrase(" " + TipoCredito, fuente8));
                tipCre1.VerticalAlignment = Element.ALIGN_CENTER;
                dat1.AddCell(tipCre1);
                documento.Add(dat1);

                VOcu infor1 = new VOcu();
                int[] sesiones1 = obtieneSesiones();
                infor1.empresa = sesiones[2];
                infor1.sucursal = sesiones[3];
                int id_cliente1 = Convert.ToInt32(RadGridVO.SelectedValues["id_cliente"]);
                infor1.id_cliente = id_cliente;
                infor1.optieneimpresion1();

                if (Convert.ToBoolean(infor1.retorno[0]))
                {
                    DataSet ds1 = (DataSet)infor1.retorno[1];

                    string nombres = "";
                    string app = "";
                    string apm = "";
                    string fechanacim = "";
                    string generooo = "";
                    string personasdep = "";
                    string callee = "";
                    string numexte = "";
                    string numinte = "";
                    string coloniaa = "";
                    string cpos = "";
                    string municipio = "";
                    string estadoooo = "";
                    string telefonofij = "";
                    string calle_nego = "";
                    string num_ext_neg = "";
                    string num_int_neg = "";
                    string colo_neg = "";
                    string cpos_neg = "";
                    string muni_nego = "";
                    string estado_neg = "";
                    string tel_fijo_neg = "";
                    string razon_soci_neg = "";
                    string gri_prin_neg = "";

                    foreach (DataRow r1 in ds1.Tables[0].Rows)
                    {

                        nombres = r1[0].ToString();
                        app = r1[1].ToString();
                        apm = r1[2].ToString();
                        fechanacim = r1[3].ToString();
                        generooo = r1[4].ToString();
                        if (generooo == "H")
                        {
                            generooo = "H";
                        }

                        else
                        {
                            generooo = "M";
                        }
                        personasdep = r1[5].ToString();
                        callee = r1[6].ToString();
                        numexte = r1[7].ToString();
                        numinte = r1[8].ToString();
                        coloniaa = r1[9].ToString();
                        cpos = r1[10].ToString();
                        municipio = r1[11].ToString();
                        estadoooo = r1[12].ToString();
                        telefonofij = r1[13].ToString();
                        calle_nego = r1[14].ToString();
                        num_ext_neg = r1[15].ToString();
                        num_int_neg = r1[16].ToString();
                        colo_neg = r1[17].ToString();
                        cpos_neg = r1[18].ToString();
                        muni_nego = r1[19].ToString();
                        estado_neg = r1[20].ToString();
                        tel_fijo_neg = r1[21].ToString();
                        razon_soci_neg = r1[22].ToString();
                        gri_prin_neg = r1[23].ToString();


                    }


                    // TABLA A GERENTE DE SUCURSAL
                    PdfPTable tabSA = new PdfPTable(6);
                    tabSA.SetWidths(new float[] { 15, 5, 10, 5, 10, 5 });
                    tabSA.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabSA.WidthPercentage = 50f;


                    PdfPCell gerenS = (new PdfPCell(new Phrase("GERENTE DE SUCURSAL", fuente6)) { Colspan = 6 });
                    gerenS.HorizontalAlignment = Element.ALIGN_CENTER;
                    gerenS.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(gerenS);

                    PdfPCell gerenS1 = (new PdfPCell(new Phrase(" " + Gerente, fuente8)) { Colspan = 6 });
                    gerenS1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabSA.AddCell(gerenS1);

                    PdfPCell datCli = (new PdfPCell(new Phrase("DATOS DEL CLIENTE", fuente6)) { Colspan = 6 });
                    datCli.HorizontalAlignment = Element.ALIGN_CENTER;
                    datCli.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(datCli);

                    PdfPCell nomb = new PdfPCell(new Phrase("NOMBRE(S)", fuente6));
                    nomb.HorizontalAlignment = Element.ALIGN_LEFT;
                    nomb.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(nomb);

                    PdfPCell nomb1 = (new PdfPCell(new Phrase(" " + nombres, fuente8)) { Colspan = 5 });
                    nomb1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabSA.AddCell(nomb1);

                    PdfPCell apeP = new PdfPCell(new Phrase("APELLIDO PATERNO", fuente6));
                    apeP.HorizontalAlignment = Element.ALIGN_LEFT;
                    apeP.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(apeP);

                    PdfPCell apeP1 = (new PdfPCell(new Phrase(" " + app, fuente8)) { Colspan = 5 });
                    apeP1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabSA.AddCell(apeP1);

                    PdfPCell apeM = new PdfPCell(new Phrase("APELLIDO MATERNO", fuente6));
                    apeM.HorizontalAlignment = Element.ALIGN_LEFT;
                    apeM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(apeM);

                    PdfPCell apeM1 = (new PdfPCell(new Phrase(" " + apm, fuente8)) { Colspan = 5 });
                    apeM1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabSA.AddCell(apeM1);

                    PdfPCell feNa = (new PdfPCell(new Phrase("FECHA DE NACIMIENTO", fuente6)) { Colspan = 2 });
                    feNa.HorizontalAlignment = Element.ALIGN_LEFT;
                    feNa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(feNa);

                    PdfPCell feNa1 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(fechanacim).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 4 });
                    feNa1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabSA.AddCell(feNa1);

                    PdfPCell edad = new PdfPCell(new Phrase("EDAD", fuente6));
                    edad.HorizontalAlignment = Element.ALIGN_CENTER;
                    edad.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(edad);

                    PdfPCell edad1 = (new PdfPCell(new Phrase(" " + edadclien, fuente8)) { Colspan = 5 });
                    edad1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabSA.AddCell(edad1);

                    PdfPCell sexo = (new PdfPCell(new Phrase("SEXO", fuente6)));
                    sexo.HorizontalAlignment = Element.ALIGN_CENTER;
                    sexo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(sexo);

                    PdfPCell sexo1 = (new PdfPCell(new Phrase(" ", fuente6)));
                    sexo1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabSA.AddCell(sexo1);

                    PdfPCell fame = (new PdfPCell(new Phrase("F", fuente6)));
                    fame.HorizontalAlignment = Element.ALIGN_CENTER;
                    fame.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(fame);

                    PdfPCell fame1 = (new PdfPCell(new Phrase(" " + generooo, fuente6)));
                    fame1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabSA.AddCell(fame1);

                    PdfPCell masc = (new PdfPCell(new Phrase("M", fuente6)));
                    masc.HorizontalAlignment = Element.ALIGN_CENTER;
                    masc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(masc);

                    PdfPCell masc1 = (new PdfPCell(new Phrase(" " + generooo, fuente6)));
                    masc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabSA.AddCell(masc1);

                    PdfPCell depen = (new PdfPCell(new Phrase("PERSONAS QUE DEPENDEN DE USTED", fuente6)) { Colspan = 4 });
                    depen.HorizontalAlignment = Element.ALIGN_LEFT;
                    depen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(depen);

                    PdfPCell depen1 = (new PdfPCell(new Phrase(" " + personasdep, fuente6)) { Colspan = 2 });
                    depen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabSA.AddCell(depen1);

                    PdfPCell clieDe = new PdfPCell(new Phrase("CLIENTE DESDE", fuente6));
                    clieDe.HorizontalAlignment = Element.ALIGN_LEFT;
                    clieDe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(clieDe);

                    PdfPCell clieDe1 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(clientdes).ToString("dd/MM/yyyy"), fuente8)) { Colspan = 5 });
                    clieDe1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabSA.AddCell(clieDe1);


                    //TABLA ASESOR DE CREDITO B
                    PdfPTable tabAC = new PdfPTable(5);
                    tabAC.SetWidths(new float[] { 10, 10, 5, 10, 15 });
                    tabAC.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabAC.WidthPercentage = 50f;



                    PdfPCell asesor = (new PdfPCell(new Phrase("ASESOR DE CREDITO", fuente6)) { Colspan = 5 });
                    asesor.HorizontalAlignment = Element.ALIGN_CENTER;
                    asesor.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(asesor);

                    PdfPCell asesor1 = (new PdfPCell(new Phrase(" " + AsesorCredito, fuente8)) { Colspan = 5 });
                    asesor1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabAC.AddCell(asesor1);

                    PdfPCell domiCliente = (new PdfPCell(new Phrase("DOMICILIO DEL CLIENTE", fuente6)) { Colspan = 5 });
                    domiCliente.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiCliente.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(domiCliente);

                    PdfPCell calle = new PdfPCell(new Phrase("CALLE", fuente6));
                    calle.HorizontalAlignment = Element.ALIGN_LEFT;
                    calle.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(calle);

                    PdfPCell calle1 = (new PdfPCell(new Phrase(" " + callee, fuente6)) { Colspan = 4 });
                    calle1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(calle1);

                    PdfPCell numExt = new PdfPCell(new Phrase("NUM EXT", fuente6));
                    numExt.HorizontalAlignment = Element.ALIGN_LEFT;
                    numExt.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(numExt);

                    PdfPCell numExt1 = (new PdfPCell(new Phrase(" " + numexte, fuente6)) { Colspan = 2 });
                    numExt1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(numExt1);

                    PdfPCell numInt = new PdfPCell(new Phrase("NUM INT", fuente6));
                    numInt.HorizontalAlignment = Element.ALIGN_LEFT;
                    numInt.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(numInt);

                    PdfPCell numInt1 = (new PdfPCell(new Phrase(" " + numinte, fuente6)));
                    numInt1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(numInt1);

                    PdfPCell colo = new PdfPCell(new Phrase("COLONIA", fuente6));
                    colo.HorizontalAlignment = Element.ALIGN_LEFT;
                    colo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(colo);

                    PdfPCell colo1 = (new PdfPCell(new Phrase(" " + coloniaa, fuente6)) { Colspan = 2 });
                    colo1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(colo1);

                    PdfPCell codPo = new PdfPCell(new Phrase("COD POSTAL", fuente6));
                    codPo.HorizontalAlignment = Element.ALIGN_LEFT;
                    codPo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(codPo);

                    PdfPCell codPo1 = (new PdfPCell(new Phrase(" " + cpos, fuente6)));
                    codPo1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(codPo1);

                    PdfPCell muni = (new PdfPCell(new Phrase("MUNICIPIO O DELEG.", fuente6)) { Colspan = 2 });
                    muni.HorizontalAlignment = Element.ALIGN_LEFT;
                    muni.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(muni);

                    PdfPCell muni1 = (new PdfPCell(new Phrase(" " + municipio, fuente6)) { Colspan = 3 });
                    muni1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(muni1);

                    PdfPCell estado = new PdfPCell(new Phrase("ESTADO", fuente6));
                    estado.HorizontalAlignment = Element.ALIGN_LEFT;
                    estado.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(estado);

                    PdfPCell estado1 = (new PdfPCell(new Phrase(" " + estadoooo, fuente6)) { Colspan = 4 });
                    estado1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(estado1);

                    PdfPCell entrCa = (new PdfPCell(new Phrase("ENTRE LAS CALLES", fuente6)) { Colspan = 2 });
                    entrCa.HorizontalAlignment = Element.ALIGN_LEFT;
                    entrCa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(entrCa);

                    PdfPCell entrCa1 = (new PdfPCell(new Phrase(" " + entrelascalles1, fuente8)) { Colspan = 3 });
                    entrCa1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(entrCa1);

                    PdfPCell entrCa2 = (new PdfPCell(new Phrase(" ", fuente6)) { Colspan = 5 });
                    entrCa2.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabAC.AddCell(entrCa2);

                    PdfPCell telefo = (new PdfPCell(new Phrase("TELÉFONO (OBLIGATORIO)", fuente6)) { Colspan = 2 });
                    telefo.HorizontalAlignment = Element.ALIGN_LEFT;
                    telefo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(telefo);

                    PdfPCell telefo1 = (new PdfPCell(new Phrase(" " + telefonofij, fuente6)) { Colspan = 3 });
                    telefo1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(telefo1);

                    //TABLA DE GERENTE Y ASESOR (UNION DE TABBLAS)
                    PdfPTable tabACSV = new PdfPTable(2);
                    tabACSV.SetWidths(new float[] { 50, 50 });
                    tabACSV.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabACSV.WidthPercentage = 100f;

                    PdfPCell SV = (new PdfPCell(tabSA));
                    SV.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabACSV.AddCell(SV);

                    PdfPCell AC = (new PdfPCell(tabAC));
                    AC.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabACSV.AddCell(AC);
                    documento.Add(tabACSV);
                    documento.Add(new Paragraph(" "));

                    //datos socioeconomicos del cliente
                    //TIPOS DE VIVIENDA
                    PdfPTable tipsVi = new PdfPTable(2);
                    tipsVi.SetWidths(new float[] { 9, 9 });
                    tipsVi.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tipsVi.WidthPercentage = 18f;




                    PdfPCell tipV = (new PdfPCell(new Phrase("TIPO DE VIVIENDO", fuente6)) { Colspan = 2 });
                    tipV.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipV.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipsVi.AddCell(tipV);

                    PdfPCell propia = (new PdfPCell(new Phrase("PROPIA", fuente6)));
                    propia.HorizontalAlignment = Element.ALIGN_CENTER;
                    propia.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipsVi.AddCell(propia);

                    PdfPCell propia1 = (new PdfPCell(new Phrase(" " + Pro, fuente8)));
                    propia1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipsVi.AddCell(propia1);

                    PdfPCell renta = (new PdfPCell(new Phrase("RENTADA", fuente6)));
                    renta.HorizontalAlignment = Element.ALIGN_CENTER;
                    renta.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipsVi.AddCell(renta);

                    PdfPCell renta1 = (new PdfPCell(new Phrase(" " + Ren, fuente8)));
                    renta1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipsVi.AddCell(renta1);

                    PdfPCell prest = (new PdfPCell(new Phrase("PRESTADA", fuente6)));
                    prest.HorizontalAlignment = Element.ALIGN_CENTER;
                    prest.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipsVi.AddCell(prest);

                    PdfPCell prest1 = (new PdfPCell(new Phrase(" " + Pre, fuente8)));
                    prest1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipsVi.AddCell(prest1);

                    PdfPCell otroV = (new PdfPCell(new Phrase("OTRO", fuente6)));
                    otroV.HorizontalAlignment = Element.ALIGN_CENTER;
                    otroV.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipsVi.AddCell(otroV);

                    PdfPCell otroV1 = (new PdfPCell(new Phrase(" " + Otr, fuente8)));
                    otroV1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipsVi.AddCell(otroV1);

                    //SERVICIOS
                    PdfPTable servi = new PdfPTable(2);
                    servi.SetWidths(new float[] { 9, 9 });
                    servi.HorizontalAlignment = Element.ALIGN_RIGHT;
                    servi.WidthPercentage = 18f;




                    PdfPCell servi1 = (new PdfPCell(new Phrase("SERVICIOS", fuente6)) { Colspan = 2 });
                    servi1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(servi1);

                    PdfPCell luz = (new PdfPCell(new Phrase("LUZ", fuente6)));
                    luz.HorizontalAlignment = Element.ALIGN_CENTER;
                    luz.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(luz);

                    PdfPCell luz1 = (new PdfPCell(new Phrase(" " + Luz, fuente8)));
                    luz1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi.AddCell(luz1);

                    PdfPCell agua = (new PdfPCell(new Phrase("AGUA", fuente6)));
                    agua.HorizontalAlignment = Element.ALIGN_CENTER;
                    agua.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(agua);

                    PdfPCell agua1 = (new PdfPCell(new Phrase(" " + Agua, fuente8)));
                    agua1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi.AddCell(agua1);

                    PdfPCell drena = (new PdfPCell(new Phrase("DRENAJE", fuente6)));
                    drena.HorizontalAlignment = Element.ALIGN_CENTER;
                    drena.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(drena);

                    PdfPCell drena1 = (new PdfPCell(new Phrase(" " + Drenaje, fuente8)));
                    drena1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi.AddCell(drena1);

                    PdfPCell callTel = (new PdfPCell(new Phrase("TELÉFONO", fuente6)));
                    callTel.HorizontalAlignment = Element.ALIGN_CENTER;
                    callTel.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(callTel);

                    PdfPCell callTel1 = (new PdfPCell(new Phrase(" " + Tel, fuente8)));
                    callTel1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi.AddCell(callTel1);

                    PdfPCell internet = (new PdfPCell(new Phrase("INTERNET", fuente6)));
                    internet.HorizontalAlignment = Element.ALIGN_CENTER;
                    internet.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(internet);

                    PdfPCell internet1 = (new PdfPCell(new Phrase(" " + Internet, fuente8)));
                    internet1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi.AddCell(internet1);

                    PdfPCell gas = (new PdfPCell(new Phrase("GAS", fuente6)));
                    gas.HorizontalAlignment = Element.ALIGN_CENTER;
                    gas.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(gas);

                    PdfPCell gas1 = (new PdfPCell(new Phrase(" " + Gas, fuente8)));
                    gas1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi.AddCell(gas1);

                    PdfPCell ppv = (new PdfPCell(new Phrase("TV DE PAGA", fuente6)));
                    ppv.HorizontalAlignment = Element.ALIGN_CENTER;
                    ppv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(gas);

                    PdfPCell ppv1 = (new PdfPCell(new Phrase(" " + Tv, fuente8)));
                    ppv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi.AddCell(ppv1);

                    //TIPO DE CONSTRUCCIONES
                    PdfPTable tipCo = new PdfPTable(2);
                    tipCo.SetWidths(new float[] { 9, 9 });
                    tipCo.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tipCo.WidthPercentage = 18f;



                    PdfPCell tipoCo = (new PdfPCell(new Phrase("TIPO DE CONSTRUCCIÓN", fuente6)) { Colspan = 2 });
                    tipoCo.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipoCo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipCo.AddCell(tipoCo);

                    PdfPCell tabique = (new PdfPCell(new Phrase("TABIQUE", fuente6)));
                    tabique.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabique.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipCo.AddCell(tabique);

                    PdfPCell tabique1 = (new PdfPCell(new Phrase(" " + Tab, fuente8)));
                    tabique1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipCo.AddCell(tabique1);

                    PdfPCell madera = (new PdfPCell(new Phrase("MADERA", fuente6)));
                    madera.HorizontalAlignment = Element.ALIGN_CENTER;
                    madera.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipCo.AddCell(madera);

                    PdfPCell madera1 = (new PdfPCell(new Phrase(" " + Mad, fuente8)));
                    madera1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipCo.AddCell(madera1);

                    PdfPCell otroCon = (new PdfPCell(new Phrase("OTRO", fuente6)));
                    otroCon.HorizontalAlignment = Element.ALIGN_CENTER;
                    otroCon.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipCo.AddCell(otroCon);

                    PdfPCell otroCon1 = (new PdfPCell(new Phrase(" " + Otr1, fuente8)));
                    otroCon1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipCo.AddCell(otroCon1);

                    //APARATOS ELECTRODOMESTICOS
                    PdfPTable aparaEl = new PdfPTable(2);
                    aparaEl.SetWidths(new float[] { 20, 8 });
                    aparaEl.HorizontalAlignment = Element.ALIGN_RIGHT;
                    aparaEl.WidthPercentage = 28f;




                    PdfPCell aemv = (new PdfPCell(new Phrase("APARATOS ELECTRODOMESTICOS Y MUEBLES EN LA VIVIENDA", fuente6)) { Colspan = 2 });
                    aemv.HorizontalAlignment = Element.ALIGN_CENTER;
                    aemv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(aemv);

                    PdfPCell sala = (new PdfPCell(new Phrase("SALA", fuente6)));
                    sala.HorizontalAlignment = Element.ALIGN_CENTER;
                    sala.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(sala);

                    PdfPCell sala1 = (new PdfPCell(new Phrase(" " + Sala, fuente8)));
                    sala1.HorizontalAlignment = Element.ALIGN_CENTER;
                    aparaEl.AddCell(sala1);

                    PdfPCell comedor = (new PdfPCell(new Phrase("COMEDOR", fuente6)));
                    comedor.HorizontalAlignment = Element.ALIGN_CENTER;
                    comedor.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(comedor);

                    PdfPCell comedor1 = (new PdfPCell(new Phrase(" " + Comedor, fuente8)));
                    comedor1.HorizontalAlignment = Element.ALIGN_CENTER;
                    aparaEl.AddCell(comedor1);

                    PdfPCell estufa = (new PdfPCell(new Phrase("ESTUFA", fuente6)));
                    estufa.HorizontalAlignment = Element.ALIGN_CENTER;
                    estufa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(estufa);

                    PdfPCell estufa1 = (new PdfPCell(new Phrase(" " + Estufa, fuente8)));
                    estufa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    aparaEl.AddCell(estufa1);

                    PdfPCell refri = (new PdfPCell(new Phrase("REFRIGERADOR", fuente6)));
                    refri.HorizontalAlignment = Element.ALIGN_CENTER;
                    refri.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(refri);

                    PdfPCell refri1 = (new PdfPCell(new Phrase(" " + Refrigerador, fuente8)));
                    refri1.HorizontalAlignment = Element.ALIGN_CENTER;
                    aparaEl.AddCell(refri1);

                    PdfPCell lavad = (new PdfPCell(new Phrase("LAVADORA", fuente6)));
                    lavad.HorizontalAlignment = Element.ALIGN_CENTER;
                    lavad.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(lavad);

                    PdfPCell lavad1 = (new PdfPCell(new Phrase(" " + Lavadora, fuente8)));
                    lavad1.HorizontalAlignment = Element.ALIGN_CENTER;
                    aparaEl.AddCell(lavad1);

                    PdfPCell telev = (new PdfPCell(new Phrase("TELEVISIÓN", fuente6)));
                    telev.HorizontalAlignment = Element.ALIGN_CENTER;
                    telev.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(telev);

                    PdfPCell telev1 = (new PdfPCell(new Phrase(" " + Television, fuente8)));
                    telev1.HorizontalAlignment = Element.ALIGN_CENTER;
                    aparaEl.AddCell(telev1);

                    PdfPCell comput = (new PdfPCell(new Phrase("COMPUTADORA", fuente6)));
                    comput.HorizontalAlignment = Element.ALIGN_CENTER;
                    comput.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(comput);

                    PdfPCell comput1 = (new PdfPCell(new Phrase(" " + Compu, fuente8)));
                    comput1.HorizontalAlignment = Element.ALIGN_CENTER;
                    aparaEl.AddCell(comput1);

                    //OTROS BIENES
                    PdfPTable obie = new PdfPTable(2);
                    obie.SetWidths(new float[] { 9, 9 });
                    obie.HorizontalAlignment = Element.ALIGN_RIGHT;
                    obie.WidthPercentage = 18f;



                    PdfPCell otherBie = (new PdfPCell(new Phrase("OTROS BIENES", fuente6)) { Colspan = 2 });
                    otherBie.HorizontalAlignment = Element.ALIGN_CENTER;
                    otherBie.BackgroundColor = BaseColor.LIGHT_GRAY;
                    obie.AddCell(otherBie);

                    PdfPCell carro = (new PdfPCell(new Phrase("AUTO", fuente6)));
                    carro.HorizontalAlignment = Element.ALIGN_CENTER;
                    carro.BackgroundColor = BaseColor.LIGHT_GRAY;
                    obie.AddCell(carro);

                    PdfPCell carro1 = (new PdfPCell(new Phrase(" " + Auto, fuente8)));
                    carro1.HorizontalAlignment = Element.ALIGN_CENTER;
                    obie.AddCell(carro1);

                    PdfPCell marca = (new PdfPCell(new Phrase("MARCA", fuente6)));
                    marca.HorizontalAlignment = Element.ALIGN_CENTER;
                    marca.BackgroundColor = BaseColor.LIGHT_GRAY;
                    obie.AddCell(marca);

                    PdfPCell marca1 = (new PdfPCell(new Phrase(" " + Marca, fuente8)));
                    marca1.HorizontalAlignment = Element.ALIGN_CENTER;
                    obie.AddCell(marca1);

                    PdfPCell modelo = (new PdfPCell(new Phrase("MODELO", fuente6)));
                    modelo.HorizontalAlignment = Element.ALIGN_CENTER;
                    modelo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    obie.AddCell(modelo);

                    PdfPCell modelo1 = (new PdfPCell(new Phrase(" " + Modelo, fuente8)));
                    modelo1.HorizontalAlignment = Element.ALIGN_CENTER;
                    obie.AddCell(modelo1);

                    PdfPCell placas = (new PdfPCell(new Phrase("PLACAS", fuente6)));
                    placas.HorizontalAlignment = Element.ALIGN_CENTER;
                    placas.BackgroundColor = BaseColor.LIGHT_GRAY;
                    obie.AddCell(placas);

                    PdfPCell placas1 = (new PdfPCell(new Phrase(" " + Placas, fuente8)));
                    placas1.HorizontalAlignment = Element.ALIGN_CENTER;
                    obie.AddCell(placas1);

                    //tabla principal une tablas de tip de vivienda, serviccios, tipo de const, aparatos elect, otros bieness
                    PdfPTable tipDaEco = new PdfPTable(5);
                    tipDaEco.SetWidths(new float[] { 18, 18, 18, 28, 18 });
                    tipDaEco.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipDaEco.DefaultCell.Border = 0;
                    tipDaEco.WidthPercentage = 100f;

                    PdfPCell ecoVivi = (new PdfPCell(tipsVi));
                    ecoVivi.HorizontalAlignment = Element.ALIGN_LEFT;
                    tipDaEco.AddCell(ecoVivi);

                    PdfPCell ecoServi = (new PdfPCell(servi));
                    ecoServi.HorizontalAlignment = Element.ALIGN_LEFT;
                    tipDaEco.AddCell(ecoServi);

                    PdfPCell ecoConst = (new PdfPCell(tipCo));
                    ecoConst.HorizontalAlignment = Element.ALIGN_LEFT;
                    tipDaEco.AddCell(ecoConst);

                    PdfPCell ecoApara = (new PdfPCell(aparaEl));
                    ecoApara.HorizontalAlignment = Element.ALIGN_LEFT;
                    tipDaEco.AddCell(ecoApara);

                    PdfPCell ecoBienes = (new PdfPCell(obie));
                    ecoBienes.HorizontalAlignment = Element.ALIGN_LEFT;
                    tipDaEco.AddCell(ecoBienes);
                    documento.Add(tipDaEco);
                    documento.Add(new Paragraph(" "));

                    //domicilio del negocio

                    PdfPTable domiNeg = new PdfPTable(4);
                    domiNeg.SetWidths(new float[] { 10, 20, 10, 10 });
                    domiNeg.HorizontalAlignment = Element.ALIGN_RIGHT;
                    domiNeg.WidthPercentage = 50f;



                    PdfPCell doNego = (new PdfPCell(new Phrase("DOMICILIO DEL NEGOCIO", fuente6)) { Colspan = 4 });
                    doNego.HorizontalAlignment = Element.ALIGN_CENTER;
                    doNego.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(doNego);

                    PdfPCell calleNeg = (new PdfPCell(new Phrase("CALLE", fuente6)));
                    calleNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    calleNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(calleNeg);

                    PdfPCell calleNeg1 = (new PdfPCell(new Phrase(" " + calle_nego, fuente6)) { Colspan = 3 });
                    calleNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(calleNeg1);

                    PdfPCell nExtN = (new PdfPCell(new Phrase("NUM. EXT", fuente6)));
                    nExtN.HorizontalAlignment = Element.ALIGN_CENTER;
                    nExtN.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(nExtN);

                    PdfPCell nExtN1 = (new PdfPCell(new Phrase(" " + num_ext_neg, fuente6)));
                    nExtN1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(nExtN1);

                    PdfPCell nIntN = (new PdfPCell(new Phrase("NUM. INT", fuente6)));
                    nIntN.HorizontalAlignment = Element.ALIGN_CENTER;
                    nIntN.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(nExtN);

                    PdfPCell nIntN1 = (new PdfPCell(new Phrase(" " + num_int_neg, fuente6)));
                    nIntN1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(nIntN1);

                    PdfPCell colNeg = (new PdfPCell(new Phrase("COLONIA", fuente6)));
                    colNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    colNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(colNeg);

                    PdfPCell colNeg1 = (new PdfPCell(new Phrase(" " + colo_neg, fuente6)));
                    colNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(colNeg1);

                    PdfPCell cpNeg = (new PdfPCell(new Phrase("C.P.", fuente6)));
                    cpNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    cpNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(cpNeg);

                    PdfPCell cpNeg1 = (new PdfPCell(new Phrase(" " + cpos_neg, fuente6)));
                    cpNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(cpNeg1);

                    PdfPCell delNeg = (new PdfPCell(new Phrase("MUNICIPIO O DELEGACIÓN", fuente6)) { Colspan = 2 });
                    delNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    delNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(delNeg);

                    PdfPCell delNeg1 = (new PdfPCell(new Phrase(" " + muni_nego, fuente6)) { Colspan = 2 });
                    delNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(delNeg1);

                    PdfPCell estaNeg = (new PdfPCell(new Phrase("ESTADO", fuente6)));
                    estaNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    estaNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(estaNeg);

                    PdfPCell estaNeg1 = (new PdfPCell(new Phrase(" " + estado_neg, fuente6)) { Colspan = 3 });
                    estaNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(estaNeg1);

                    PdfPCell entreNeg = (new PdfPCell(new Phrase("ENTRE LAS CALLES", fuente6)));
                    entreNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    entreNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(entreNeg);

                    PdfPCell entreNeg1 = (new PdfPCell(new Phrase(" " + entrelascalles2, fuente8)) { Colspan = 3 });
                    entreNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(entreNeg1);

                    PdfPCell entreNeg2 = (new PdfPCell(new Phrase(" ", fuente8)) { Colspan = 4 });
                    entreNeg2.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(entreNeg2);

                    PdfPCell telNeg = (new PdfPCell(new Phrase("TEL (OBLIGATORIO)", fuente6)));
                    telNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    telNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(telNeg);

                    PdfPCell telNeg1 = (new PdfPCell(new Phrase(" " + tel_fijo_neg, fuente6)) { Colspan = 3 });
                    telNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(telNeg1);

                    //CARACTERISTICAS DEL LOCAL
                    PdfPTable CarLoc = new PdfPTable(4);
                    CarLoc.SetWidths(new float[] { 20, 5, 20, 5 });
                    CarLoc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    CarLoc.WidthPercentage = 50f;



                    PdfPCell carLo = (new PdfPCell(new Phrase("CARACTERISTICAS DEL LOCAL", fuente6)) { Colspan = 4 });
                    carLo.HorizontalAlignment = Element.ALIGN_CENTER;
                    carLo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(doNego);

                    PdfPCell propioLoc = (new PdfPCell(new Phrase("PROPIO", fuente6)));
                    propioLoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    propioLoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(propioLoc);

                    PdfPCell propioLoc1 = (new PdfPCell(new Phrase(" " + Pro1, fuente8)));
                    propioLoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(propioLoc1);

                    PdfPCell rentLoc = (new PdfPCell(new Phrase("RENTADO", fuente6)));
                    rentLoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    rentLoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(rentLoc);

                    PdfPCell rentLoc1 = (new PdfPCell(new Phrase(" " + Ren1, fuente8)));
                    rentLoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(rentLoc1);

                    PdfPCell fijoLoc = (new PdfPCell(new Phrase("FIJO", fuente6)));
                    fijoLoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    fijoLoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(fijoLoc);

                    PdfPCell fijoLoc1 = (new PdfPCell(new Phrase(" " + Fij1, fuente8)));
                    fijoLoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(fijoLoc1);

                    PdfPCell semiLoc = (new PdfPCell(new Phrase("SEMIFIJO", fuente6)));
                    semiLoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    semiLoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(semiLoc);

                    PdfPCell semiLoc1 = (new PdfPCell(new Phrase(" " + Sem1, fuente8)));
                    semiLoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(semiLoc1);

                    PdfPCell timeNeg = (new PdfPCell(new Phrase("TIEMPO CON EL NEGOCIO", fuente6)) { Colspan = 2 });
                    timeNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    timeNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(timeNeg);

                    PdfPCell timeNeg1 = (new PdfPCell(new Phrase(" " + tiempneg, fuente8)) { Colspan = 2 });
                    timeNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(timeNeg1);

                    PdfPCell razLoc = (new PdfPCell(new Phrase("RAZON SOCIAL", fuente6)) { Colspan = 2 });
                    razLoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    razLoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(razLoc);

                    PdfPCell razLoc1 = (new PdfPCell(new Phrase(" " + razon_soci_neg, fuente6)) { Colspan = 2 });
                    razLoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(razLoc1);

                    PdfPCell giroLoc = (new PdfPCell(new Phrase("GIRO DEL NEGOCIO", fuente6)) { Colspan = 2 });
                    giroLoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    giroLoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(giroLoc);

                    PdfPCell giroLoc1 = (new PdfPCell(new Phrase(" " + gri_prin_neg, fuente6)) { Colspan = 2 });
                    giroLoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(giroLoc1);

                    PdfPCell prinLoc = (new PdfPCell(new Phrase("PRINCIPALES PROVEEDORES", fuente6)) { Colspan = 2 });
                    prinLoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    prinLoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(prinLoc);

                    PdfPCell prinLoc1 = (new PdfPCell(new Phrase(" " + principrov, fuente8)) { Colspan = 2 });
                    prinLoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(prinLoc1);

                    PdfPCell prinLoc2 = (new PdfPCell(new Phrase(" ", fuente6)) { Colspan = 4 });
                    prinLoc2.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(prinLoc2);

                    //TERCERA TABLA PARA UNIR TABLAS
                    PdfPTable terTable = new PdfPTable(2);
                    terTable.SetWidths(new float[] { 50, 50 });
                    terTable.HorizontalAlignment = Element.ALIGN_RIGHT;
                    terTable.WidthPercentage = 100f;

                    PdfPCell NegoDom = (new PdfPCell(domiNeg));
                    NegoDom.HorizontalAlignment = Element.ALIGN_LEFT;
                    terTable.AddCell(NegoDom);

                    PdfPCell NegoCara = (new PdfPCell(CarLoc));
                    NegoCara.HorizontalAlignment = Element.ALIGN_RIGHT;
                    terTable.AddCell(NegoCara);
                    documento.Add(terTable);
                    documento.Add(new Paragraph(" "));

                    //CROQUIS DE LOCALIZACIÓN
                    PdfPTable croLoc = new PdfPTable(2);
                    croLoc.SetWidths(new float[] { 50, 50 });
                    croLoc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    croLoc.WidthPercentage = 100f;

                    PdfPCell croquis = (new PdfPCell(new Phrase("CROQUIS DE LOCALIZACIÓN", fuente6)) { Colspan = 2 });
                    croquis.HorizontalAlignment = Element.ALIGN_CENTER;
                    croquis.BackgroundColor = BaseColor.LIGHT_GRAY;
                    croLoc.AddCell(croquis);

                    PdfPCell casaCro = (new PdfPCell(new Phrase("CASA", fuente6)));
                    casaCro.HorizontalAlignment = Element.ALIGN_CENTER;
                    casaCro.BackgroundColor = BaseColor.LIGHT_GRAY;
                    croLoc.AddCell(casaCro);

                    PdfPCell negCro = (new PdfPCell(new Phrase("NEGOCIO", fuente6)));
                    negCro.HorizontalAlignment = Element.ALIGN_CENTER;
                    negCro.BackgroundColor = BaseColor.LIGHT_GRAY;
                    croLoc.AddCell(negCro);
                    documento.Add(croLoc);

                    //imagen de croquis
                    PdfPTable croquisImg = new PdfPTable(1);
                    croquisImg.WidthPercentage = 100f;

                    string imagepath2 = HttpContext.Current.Server.MapPath("img/");
                    iTextSharp.text.Image croquisIm = iTextSharp.text.Image.GetInstance(imagepath2 + "cro_visita.png");

                    croquisImg.AddCell(croquisIm);
                    documento.Add(croquisImg);
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));

                    //obligado solidario
                    PdfPTable garan1 = new PdfPTable(6);
                    garan1.SetWidths(new float[] { 15, 5, 5, 10, 5, 10 });
                    garan1.HorizontalAlignment = Element.ALIGN_RIGHT;
                    garan1.WidthPercentage = 50f;

                    PdfPCell obliSol = (new PdfPCell(new Phrase("OBLIGADO SOLIDARIO", fuente6)) { Colspan = 2 });
                    obliSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    obliSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(obliSol);




                    PdfPCell obliSol1 = (new PdfPCell(new Phrase(" " + OS, fuente8)) { Colspan = 4 });
                    obliSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(obliSol1);

                    PdfPCell nameSol = (new PdfPCell(new Phrase("NOMBRE(S)", fuente6)) { Colspan = 2 });
                    nameSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    nameSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(nameSol);

                    PdfPCell nameSol1 = (new PdfPCell(new Phrase(" " + nombre3, fuente8)) { Colspan = 4 });
                    nameSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(nameSol1);

                    PdfPCell apePSol = (new PdfPCell(new Phrase("APELLIDO PATERNO", fuente6)) { Colspan = 2 });
                    apePSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    apePSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(apePSol);

                    PdfPCell apePSol1 = (new PdfPCell(new Phrase(" " + apep, fuente8)) { Colspan = 4 });
                    apePSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(apePSol1);

                    PdfPCell apeMSol = (new PdfPCell(new Phrase("APELLIDO MATERNO", fuente6)) { Colspan = 2 });
                    apeMSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    apeMSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(apeMSol);

                    PdfPCell apeMSol1 = (new PdfPCell(new Phrase(" " + apem, fuente8)) { Colspan = 4 });
                    apeMSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(apeMSol1);

                    PdfPCell fechSol = (new PdfPCell(new Phrase("FECHA DE NACIMIENTO", fuente6)) { Colspan = 2 });
                    fechSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    fechSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(fechSol);

                    PdfPCell fechSol1 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(fenac).ToString("dd/MM/yyyy"), fuente8)) { Colspan = 4 });
                    fechSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(fechSol1);

                    PdfPCell edadSol = (new PdfPCell(new Phrase("EDAD", fuente6)));
                    edadSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    edadSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(edadSol);

                    PdfPCell edadSol1 = (new PdfPCell(new Phrase(" " + edad3, fuente8)) { Colspan = 5 });
                    edadSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(edadSol1);

                    PdfPCell sexoSol = (new PdfPCell(new Phrase("SEXO", fuente6)));
                    sexoSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    sexoSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(sexoSol);

                    PdfPCell sexoSol1 = (new PdfPCell(new Phrase(" ", fuente8)));
                    sexoSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(sexoSol1);

                    PdfPCell fameSol = (new PdfPCell(new Phrase("F", fuente6)));
                    fameSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    fameSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(fameSol);

                    PdfPCell fameSol1 = (new PdfPCell(new Phrase(" " + sexo4, fuente8)));
                    fameSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(fameSol1);

                    PdfPCell maleSol = (new PdfPCell(new Phrase("M", fuente6)));
                    maleSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    maleSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(maleSol);

                    PdfPCell maleSol1 = (new PdfPCell(new Phrase(" " + sexo3, fuente8)));
                    maleSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(maleSol1);

                    PdfPCell ocupSol = (new PdfPCell(new Phrase("OCUPACIÓN", fuente6)));
                    ocupSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocupSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(ocupSol);

                    PdfPCell ocupSol1 = (new PdfPCell(new Phrase(" " + ocupacion, fuente8)) { Colspan = 5 });
                    ocupSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(ocupSol1);

                    PdfPCell cuentaSol = (new PdfPCell(new Phrase("CUENTA CON BIENES INMUEBLES", fuente6)));
                    cuentaSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    cuentaSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(cuentaSol);

                    PdfPCell cuentaSol1 = (new PdfPCell(new Phrase(" ", fuente8)));
                    cuentaSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(cuentaSol1);

                    PdfPCell siSol = (new PdfPCell(new Phrase("SI", fuente6)));
                    siSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    siSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(siSol);

                    PdfPCell siSol1 = (new PdfPCell(new Phrase(" " + cbinm, fuente8)));
                    siSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(siSol1);

                    PdfPCell noSol = (new PdfPCell(new Phrase("NO", fuente6)));
                    noSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    noSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(noSol);

                    PdfPCell noSol1 = (new PdfPCell(new Phrase(" " + cbinm1, fuente8)));
                    noSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(noSol1);

                    PdfPCell valoSol = (new PdfPCell(new Phrase("VALOR ESTIMADO EN BIENES", fuente6)) { Colspan = 4 });
                    valoSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    valoSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(valoSol);
                    PdfPCell valoSol1 = (new PdfPCell(new Phrase("" + valorest, fuente8)) { Colspan = 2 });
                    valoSol1.HorizontalAlignment = Element.ALIGN_LEFT;
                    garan1.AddCell(valoSol1);

                    //tabla aval
                    PdfPTable garan2 = new PdfPTable(5);
                    garan2.SetWidths(new float[] { 10, 10, 10, 10, 10 });
                    garan2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    garan2.WidthPercentage = 50f;




                    PdfPCell aval = (new PdfPCell(new Phrase("AVAL", fuente6)));
                    aval.HorizontalAlignment = Element.ALIGN_CENTER;
                    aval.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(aval);

                    PdfPCell aval1 = (new PdfPCell(new Phrase(" " + aval2, fuente8)) { Colspan = 4 });
                    aval1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(aval1);

                    PdfPCell calleAv = (new PdfPCell(new Phrase("CALLE", fuente6)));
                    calleAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    calleAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(calleAv);

                    PdfPCell calleAv1 = (new PdfPCell(new Phrase(" " + callegp, fuente8)) { Colspan = 4 });
                    calleAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(calleAv1);

                    PdfPCell numExtAv = (new PdfPCell(new Phrase("NUM. EXT", fuente6)));
                    numExtAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    numExtAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(numExtAv);

                    PdfPCell numExtAv1 = (new PdfPCell(new Phrase(" " + numext, fuente8)));
                    numExtAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(numExtAv1);

                    PdfPCell numIntAv = (new PdfPCell(new Phrase("NUM. INT", fuente6)));
                    numIntAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    numIntAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(numIntAv);

                    PdfPCell numIntAv1 = (new PdfPCell(new Phrase(" " + numint, fuente8)) { Colspan = 2 });
                    numIntAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(numIntAv1);

                    PdfPCell colAv = (new PdfPCell(new Phrase("COLONIA", fuente6)));
                    colAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    colAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(colAv);

                    PdfPCell colAv1 = (new PdfPCell(new Phrase(" " + colonia, fuente8)) { Colspan = 2 });
                    colAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(colAv1);

                    PdfPCell cpAv = (new PdfPCell(new Phrase("C.P.", fuente6)));
                    cpAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    cpAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(cpAv);

                    PdfPCell cpAv1 = (new PdfPCell(new Phrase(" " + copostal, fuente8)));
                    cpAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(cpAv1);

                    PdfPCell munAv = (new PdfPCell(new Phrase("MUNICIPIO O DELEGACIÓN", fuente6)) { Colspan = 2 });
                    munAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    munAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(munAv);

                    PdfPCell munAv1 = (new PdfPCell(new Phrase(" " + munici, fuente8)) { Colspan = 3 });
                    munAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(munAv1);

                    PdfPCell estaAv = (new PdfPCell(new Phrase("ESTADO", fuente6)));
                    estaAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    estaAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(estaAv);

                    PdfPCell estaAv1 = (new PdfPCell(new Phrase(" " + estadogp, fuente8)) { Colspan = 4 });
                    estaAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(estaAv1);

                    PdfPCell entreAv = (new PdfPCell(new Phrase("ENTRE CALLES", fuente6)) { Colspan = 2 });
                    entreAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    entreAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(entreAv);

                    PdfPCell entreAv1 = (new PdfPCell(new Phrase(" ", fuente8)) { Colspan = 3 });
                    entreAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(entreAv1);

                    PdfPCell entreAv2 = (new PdfPCell(new Phrase(" ", fuente8)) { Colspan = 5 });
                    entreAv2.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(entreAv2);

                    PdfPCell telAv = (new PdfPCell(new Phrase("TEL (OBLIGATORIO)", fuente6)) { Colspan = 2 });
                    telAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    telAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(telAv);

                    PdfPCell telAv1 = (new PdfPCell(new Phrase(" " + telobli, fuente8)) { Colspan = 3 });
                    telAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(telAv1);



                    //UNION DE TABLAS (TRES)
                    PdfPTable tabCuat = new PdfPTable(2);
                    tabCuat.SetWidths(new float[] { 50, 50 });
                    tabCuat.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabCuat.WidthPercentage = 100f;

                    PdfPCell encaCuat = (new PdfPCell(new Phrase("GARANTIAS PERSONALES")) { Colspan = 2 });
                    encaCuat.HorizontalAlignment = Element.ALIGN_CENTER;
                    encaCuat.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabCuat.AddCell(encaCuat);

                    PdfPCell tabCuat1 = (new PdfPCell(garan1));
                    tabCuat1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabCuat.AddCell(tabCuat1);

                    PdfPCell tabCuat2 = (new PdfPCell(garan2));
                    tabCuat2.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabCuat.AddCell(tabCuat2);
                    documento.Add(tabCuat);
                    documento.Add(new Paragraph(" "));

                    //TABLA DE LA FOTO
                    PdfPTable tabfoto = new PdfPTable(1);
                    tabfoto.SetWidths(new float[] { 100 });
                    tabfoto.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabfoto.WidthPercentage = 80f;

                    PdfPCell fotoT = new PdfPCell(new Phrase(" "));
                    fotoT.HorizontalAlignment = Element.ALIGN_CENTER;
                    fotoT.FixedHeight = 200f;
                    tabfoto.AddCell(fotoT);
                    documento.Add(tabfoto);

                    //PIE DE FOTO
                    PdfPTable pieFoto = new PdfPTable(2);
                    pieFoto.SetWidths(new float[] { 5, 75 });
                    pieFoto.HorizontalAlignment = Element.ALIGN_CENTER;
                    pieFoto.WidthPercentage = 80f;

                    PdfPCell anexFo = (new PdfPCell(new Phrase("ANEXAR FOTOGRAFIAS DE CASA Y NEGOCIO")) { Colspan = 2 });
                    anexFo.HorizontalAlignment = Element.ALIGN_CENTER;
                    anexFo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    pieFoto.AddCell(anexFo);

                    PdfPCell nota1 = (new PdfPCell(new Phrase("Nota 1", fuente4)));
                    nota1.HorizontalAlignment = Element.ALIGN_CENTER;
                    nota1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    pieFoto.AddCell(nota1);

                    PdfPCell notaA = (new PdfPCell(new Phrase("DOMICILIO: a) Se debe visualizar la fachada completa del domicilio; b) El cliente debe de esstar dentro del domicilio", fuente4)));
                    notaA.HorizontalAlignment = Element.ALIGN_CENTER;
                    pieFoto.AddCell(notaA);

                    PdfPCell nota2 = (new PdfPCell(new Phrase("Nota 1", fuente4)));
                    nota2.HorizontalAlignment = Element.ALIGN_CENTER;
                    nota2.BackgroundColor = BaseColor.LIGHT_GRAY;
                    pieFoto.AddCell(nota2);

                    PdfPCell notaB = (new PdfPCell(new Phrase("DOMICILIO: a) Se debe visualizar la fecha del domicilio; b) Que se visualicen todas las viviendas y su independecia.", fuente4)));
                    notaB.HorizontalAlignment = Element.ALIGN_CENTER;
                    pieFoto.AddCell(notaB);

                    PdfPCell nota3 = (new PdfPCell(new Phrase("Nota 3", fuente4)));
                    nota3.HorizontalAlignment = Element.ALIGN_CENTER;
                    nota3.BackgroundColor = BaseColor.LIGHT_GRAY;
                    pieFoto.AddCell(nota3);

                    PdfPCell notaC = (new PdfPCell(new Phrase("NEGOCIO: Se debe visualizar toda la fachada del negocio. El negocio debe de estar operando. Se debe de observar toda la mercancia.", fuente4)));
                    notaC.HorizontalAlignment = Element.ALIGN_CENTER;
                    pieFoto.AddCell(notaC);
                    documento.Add(pieFoto);
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));

                    //Firmas
                    PdfPTable firmas1 = new PdfPTable(2);
                    firmas1.SetWidths(new float[] { 35, 35 });
                    firmas1.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmas1.DefaultCell.Border = 0;
                    firmas1.WidthPercentage = 70f;

                    PdfPCell raya1 = (new PdfPCell(new Phrase("____________________________________________________________", fuente4)));
                    raya1.HorizontalAlignment = Element.ALIGN_CENTER;
                    raya1.Border = 0;
                    firmas1.AddCell(raya1);

                    PdfPCell raya2 = (new PdfPCell(new Phrase("____________________________________________________________", fuente4)));
                    raya2.HorizontalAlignment = Element.ALIGN_CENTER;
                    raya2.Border = 0;
                    firmas1.AddCell(raya2);

                    PdfPCell name1 = (new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL GERENTE DE SUCURSAL", fuente6)));
                    name1.HorizontalAlignment = Element.ALIGN_CENTER;
                    name1.Border = 0;
                    firmas1.AddCell(name1);

                    PdfPCell name2 = (new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL CLIENTE", fuente6)));
                    name2.HorizontalAlignment = Element.ALIGN_CENTER;
                    name2.Border = 0;
                    firmas1.AddCell(name2);
                    documento.Add(firmas1);



                    //documento.Add(new Paragraph(""));
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

    protected void lnkCapacidadPago_Click(object sender, EventArgs e)
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


            PdfPCell titulo = new PdfPCell(new Phrase("ASESORIA Y SERVICIO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + "ANÁLISIS DE CAPACIDAD DE PAGO", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

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

            PdfPTable enca = new PdfPTable(3);

            enca.WidthPercentage = 100f;


            AnaPag infor = new AnaPag();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            int id_cliente = Convert.ToInt32(RadGridCP.SelectedValues["id_cliente"]);

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
            decimal dispp = 0;
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
                    sucursal = Convert.ToString(r[1]);
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

                PdfPCell gere = new PdfPCell(new Phrase("GERENTE", fuente6));

                gere.HorizontalAlignment = Element.ALIGN_CENTER;
                gere.BackgroundColor = BaseColor.LIGHT_GRAY;
                enca.AddCell(gere);

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

                PdfPCell numG = new PdfPCell(new Phrase("NUMERO DE GRUPO", fuente6));

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



                PdfPCell ventaMen = (new PdfPCell(new Phrase("VENTAS MENSUALES (A)", fuente6)) { Colspan = 2 });
                ventaMen.HorizontalAlignment = Element.ALIGN_CENTER;
                ventaMen.BackgroundColor = BaseColor.LIGHT_GRAY;
                venM.AddCell(ventaMen);

                PdfPCell dia = new PdfPCell(new Phrase("DIA", fuente6));
                dia.HorizontalAlignment = Element.ALIGN_CENTER;
                dia.BackgroundColor = BaseColor.LIGHT_GRAY;
                venM.AddCell(dia);

                PdfPCell monto = new PdfPCell(new Phrase("MONTO($)", fuente6));
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

                PdfPCell mierco = new PdfPCell(new Phrase("MIERCOLES", fuente6));
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

                PdfPCell toMe = new PdfPCell(new Phrase("TOTAL MENSUAL", fuente6));
                toMe.HorizontalAlignment = Element.ALIGN_CENTER;
                venM.AddCell(toMe);

                PdfPCell toMe2 = new PdfPCell(new Phrase(" " + totalmr.ToString("C2"), fuente6));
                toMe2.HorizontalAlignment = Element.ALIGN_CENTER;
                venM.AddCell(toMe2);
                //venM.AddCell(toMe2);

                PdfPCell relleno = new PdfPCell(new Phrase(" ", fuente6));
                relleno.HorizontalAlignment = Element.ALIGN_CENTER;
                venM.AddCell(relleno);


                // tabla 2
                PdfPTable venM1 = new PdfPTable(2);
                venM1.WidthPercentage = 30f;
                venM1.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell ventaMen1 = (new PdfPCell(new Phrase("COSTOS Y GASTOS MENSUALES DEL NEGOCIO", fuente6)) { Colspan = 2 });
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





                //prueba 2
                PdfPTable venM2 = new PdfPTable(2);
                venM2.WidthPercentage = 30f;
                venM2.HorizontalAlignment = Element.ALIGN_RIGHT;

                PdfPCell ventaMen2 = (new PdfPCell(new Phrase("VENTAS MENSUALES (A)", fuente6)) { Colspan = 2 });
                ventaMen2.HorizontalAlignment = Element.ALIGN_CENTER;
                ventaMen2.BackgroundColor = BaseColor.LIGHT_GRAY;
                venM2.AddCell(ventaMen2);

                PdfPCell dia2 = new PdfPCell(new Phrase("DIA", fuente6));
                dia2.HorizontalAlignment = Element.ALIGN_CENTER;
                dia2.BackgroundColor = BaseColor.LIGHT_GRAY;
                venM2.AddCell(dia2);

                PdfPCell monto2 = new PdfPCell(new Phrase("MONTO(A)", fuente6));
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

                PdfPCell totalGasC = new PdfPCell(new Phrase("TOTAL(C)", fuente6));
                totalGasC.HorizontalAlignment = Element.ALIGN_CENTER;
                venM2.AddCell(totalGasC);

                PdfPCell totalGasC1 = new PdfPCell(new Phrase(" " + totalc.ToString("C2"), fuente6));
                totalGasC1.HorizontalAlignment = Element.ALIGN_CENTER;
                venM2.AddCell(totalGasC1);


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


                PdfPCell ventaA = new PdfPCell(venM);
                ventaMensual.AddCell(ventaA);
                PdfPCell ventaB = new PdfPCell(venM1);
                ventaMensual.AddCell(ventaB);
                PdfPCell ventaC = new PdfPCell(venM2);
                ventaMensual.AddCell(ventaC);
                documento.Add(ventaMensual);
                documento.Add(new Paragraph(" "));

                //TABLA DE DISPONIBILIDAD DE NEGOCIO
                PdfPTable nego = new PdfPTable(6);
                nego.WidthPercentage = 100f;
                int[] negocellwidth = { 35, 15, 13, 10, 11, 11 };
                nego.SetWidths(negocellwidth);




                PdfPCell dispo = (new PdfPCell(new Phrase("DISPONIBILIDAD SEMANAL DEL NEGOCIO", fuente6)) { Colspan = 2, Rowspan = 2 });
                dispo.VerticalAlignment = Element.ALIGN_MIDDLE;
                dispo.HorizontalAlignment = Element.ALIGN_CENTER;
                dispo.BackgroundColor = BaseColor.LIGHT_GRAY;
                nego.AddCell(dispo);

                PdfPCell solv = (new PdfPCell(new Phrase("SOLVENCIA PARA EL PAGO DE CRÉDITO", fuente6)) { Colspan = 4 });
                solv.HorizontalAlignment = Element.ALIGN_CENTER;
                solv.BackgroundColor = BaseColor.LIGHT_GRAY;
                nego.AddCell(solv);

                PdfPCell montCre = new PdfPCell(new Phrase("MONTO DEL CRÉDITO", fuente6));
                montCre.HorizontalAlignment = Element.ALIGN_CENTER;
                montCre.BackgroundColor = BaseColor.LIGHT_GRAY;
                nego.AddCell(montCre);

                PdfPCell plazSe = new PdfPCell(new Phrase("PLAZO (SEM)", fuente6));
                plazSe.HorizontalAlignment = Element.ALIGN_CENTER;
                plazSe.BackgroundColor = BaseColor.LIGHT_GRAY;
                nego.AddCell(plazSe);

                PdfPCell pagSema = new PdfPCell(new Phrase("PAGO SEMANAL = (F)", fuente6));
                pagSema.HorizontalAlignment = Element.ALIGN_CENTER;
                pagSema.BackgroundColor = BaseColor.LIGHT_GRAY;
                nego.AddCell(pagSema);

                PdfPCell solven = new PdfPCell(new Phrase("SOLVENCIA (G)=(E)-(F)", fuente6));
                solven.HorizontalAlignment = Element.ALIGN_CENTER;
                solven.BackgroundColor = BaseColor.LIGHT_GRAY;
                nego.AddCell(solven);

                PdfPCell unid = new PdfPCell(new Phrase("UTILIDAD (D)= (A)-(B)-(C)", fuente6));
                unid.HorizontalAlignment = Element.ALIGN_CENTER;
                nego.AddCell(unid);

                PdfPCell unid1 = new PdfPCell(new Phrase(" " + utitli.ToString("C2"), fuente6));
                unid1.HorizontalAlignment = Element.ALIGN_CENTER;
                nego.AddCell(unid1);

                PdfPCell montCre1 = (new PdfPCell(new Phrase(" " + moncre.ToString("C2"), fuente6)) { Rowspan = 2 });
                montCre1.HorizontalAlignment = Element.ALIGN_CENTER;
                nego.AddCell(montCre1);

                PdfPCell plazSe1 = (new PdfPCell(new Phrase(" " + plazoo, fuente6)) { Rowspan = 2 });
                plazSe1.HorizontalAlignment = Element.ALIGN_CENTER;
                nego.AddCell(plazSe1);

                PdfPCell pagoSema1 = (new PdfPCell(new Phrase(" " + pagsem.ToString("C2"), fuente6)) { Rowspan = 2 });
                pagoSema1.HorizontalAlignment = Element.ALIGN_CENTER;
                nego.AddCell(pagoSema1);

                PdfPCell solven1 = (new PdfPCell(new Phrase(" " + solvenc.ToString("C2"), fuente6)) { Rowspan = 2 });
                solven1.HorizontalAlignment = Element.ALIGN_CENTER;
                nego.AddCell(solven1);

                PdfPCell dispSem = new PdfPCell(new Phrase("DISPONIBLE SEMANAL (E)=(E/4*70%)", fuente6));
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
                documento.Add(procedente);
                documento.Add(new Paragraph(" "));
                documento.Add(new Paragraph(" "));

                //comentarios adicionales
                PdfPTable coment = new PdfPTable(1);
                coment.WidthPercentage = 100f;
                coment.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell comenta = (new PdfPCell(new Phrase("COMENTARIOS ADICIONALES", fuente6)) { Rowspan = 5 });
                comenta.HorizontalAlignment = Element.ALIGN_CENTER;
                coment.AddCell(comenta);
                documento.Add(coment);
                documento.Add(new Paragraph(" "));
                documento.Add(new Paragraph(" "));
                documento.Add(new Paragraph(" "));
                documento.Add(new Paragraph(" "));




                //tablla de firmas
                PdfPTable firmas = new PdfPTable(2);
                firmas.WidthPercentage = 80f;
                firmas.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell firma1B = new PdfPCell(new Phrase(" ", fuente6));
                firma1B.Border = 0;
                firma1B.HorizontalAlignment = Element.ALIGN_CENTER;
                firmas.AddCell(firma1B);


                PdfPCell firma2B = new PdfPCell(new Phrase(" ", fuente6));
                firma2B.Border = 0;
                firma2B.HorizontalAlignment = Element.ALIGN_CENTER;
                firmas.AddCell(firma2B);

                PdfPCell firmaSub1 = new PdfPCell(new Phrase("_________________________________________________", fuente6));
                firmaSub1.Border = 0;
                firmaSub1.HorizontalAlignment = Element.ALIGN_CENTER;
                firmas.AddCell(firmaSub1);

                PdfPCell firmaSub2 = new PdfPCell(new Phrase("_________________________________________________", fuente6));
                firmaSub2.HorizontalAlignment = Element.ALIGN_CENTER;
                firmaSub2.Border = 0;
                firmas.AddCell(firmaSub2);

                PdfPCell firmCl = (new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL CLIENTE", fuente6)) { Rowspan = 2 });
                firmCl.HorizontalAlignment = Element.ALIGN_CENTER;
                firmCl.Border = 0;
                firmas.AddCell(firmCl);

                PdfPCell firmRes = new PdfPCell(new Phrase("NOMBRE Y FIRMA DEL RESPONSABLE DE ELABORACIÓN", fuente6));
                firmRes.HorizontalAlignment = Element.ALIGN_CENTER;
                firmRes.Border = 0;
                firmas.AddCell(firmRes);

                PdfPCell puesto = new PdfPCell(new Phrase("PUESTO_________________________", fuente6));
                puesto.HorizontalAlignment = Element.ALIGN_CENTER;
                puesto.Border = 0;
                firmas.AddCell(puesto);
                documento.Add(firmas);
                documento.Add(new Paragraph(" "));

                //politicas
                PdfPTable politicas = new PdfPTable(1);
                politicas.WidthPercentage = 80f;
                politicas.HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL;

                PdfPCell texto = new PdfPCell(new Phrase("Declaro bajo protesta de decir verdad que la información asentada y los documentos proporcionados para esta solicitud son verdaderos y correctos, así mismo me encuentro voluntariamente enterado del contenido del aviso de privasidad de ASESORIA Y SERVICIO RURAL Y URBANO S.A. de C.V. SOFOM ENR y sus alcances legales con fundamento en lo dispuesto por la Ley Federal de Proteccion de Datos Personale en posesión de los particulares y su reglamento, para lo cual otorgo de manera voluntaria el más amplio consentimiento y facultad a la empresa ASESORIA Y SERVICIO RURAL Y URBANO S.A. de C.V. SOFOM ENR a utilizar mis datos personale. ASESORIA Y SERVICIO RURAL Y URBANO S.A. de C.V. SOFOM ENR se reserva el derecho de cambiar, modificar, complementar y/o alterar el presente aviso, en cualquier momento, cuyo caso se hará de su conocimiento a través de los medios que establezca la legislación en la materia.", fuente2));
                texto.Border = 0;
                texto.HorizontalAlignment = Element.ALIGN_CENTER;
                politicas.AddCell(texto);

                documento.Add(politicas);









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
}