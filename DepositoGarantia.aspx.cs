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

public partial class DepositoGarantia : System.Web.UI.Page
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
        sesiones[4] = Convert.ToInt32(Request.QueryString["c"]);
        return sesiones;

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
        iTextSharp.text.Font fuente22 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" Depósito de Garantia ");
        documento.AddCreator("DESARROLLARTE");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\deposito_garantia__lq" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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
            tablaEncabezado.WidthPercentage = 86f;


            PdfPCell titulo = new PdfPCell(new Phrase("APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. DE C.V. SOFOM ENR" + Environment.NewLine + Environment.NewLine + "  ", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD)));
            //PdfPCell titulo = new PdfPCell(new Phrase("Gaarve S.A de C.V. " + Environment.NewLine + Environment.NewLine + " Informe de Nomina " + Environment.NewLine + Environment.NewLine + strDatosObra, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(logo);
            tablaEncabezado.AddCell(titulo);

            documento.Add(tablaEncabezado);
            documento.Add(new Paragraph(" "));

            DepoGar infor = new DepoGar();

            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            infor.idSolicitudEdita = sesiones[4];
            infor.obtieneimpresionEnc();

            int id_empresa = 0;
            int id_sucursal = 0;
            int id_consulta = 0;
            int id_cliente = 0;
            int ciclo = 0;

            int numcreditoo = 0;
            string grup = "";
            int suc = 0;
            decimal mont = 0;
            decimal montTot = 0;
            decimal montGL = 0;
            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    numcreditoo = Convert.ToInt16(r[1]);
                    grup = r[2].ToString();
                    suc = Convert.ToInt16(r[0]);
                    try
                    {
                        mont = Convert.ToDecimal(r[5]);
                    }
                    catch
                    {
                        mont = 0;
                    }
                    
                    ciclo = Convert.ToInt32(r[6]);
                    montTot = montTot + mont;

                    montGL = (montTot * 10) / 100;
                }

            }

            PdfPTable refBan = new PdfPTable(1);
            refBan.WidthPercentage = 80f;

            PdfPCell pagoLiq = new PdfPCell(new Phrase("REFERENCIA PAGO GARANTIA LIQUIDA",fuente22));
            pagoLiq.BackgroundColor = BaseColor.LIGHT_GRAY;
            pagoLiq.HorizontalAlignment = Element.ALIGN_CENTER;
            pagoLiq.VerticalAlignment = Element.ALIGN_MIDDLE;
            refBan.AddCell(pagoLiq);

            documento.Add(refBan);
            documento.Add(new Paragraph(" "));



            PdfPTable sucur = new PdfPTable(3);
            sucur.WidthPercentage = 60f;
            int[] sucurcellwidth = { 30, 30, 40 };
            sucur.SetWidths(sucurcellwidth);
           







            PdfPCell sucurs = (new PdfPCell(new Phrase("BANCO", fuente22)) );
            sucurs.HorizontalAlignment = Element.ALIGN_CENTER;
            sucurs.BackgroundColor = BaseColor.LIGHT_GRAY;
            sucur.AddCell(sucurs);

            PdfPCell banco = new PdfPCell(new Phrase("REFENCIA", fuente22));
            banco.HorizontalAlignment = Element.ALIGN_CENTER;
            banco.BackgroundColor = BaseColor.LIGHT_GRAY;
            sucur.AddCell(banco);

            PdfPCell refe = new PdfPCell(new Phrase("CONVENIO", fuente22));
            refe.HorizontalAlignment = Element.ALIGN_CENTER;
            refe.BackgroundColor = BaseColor.LIGHT_GRAY;
            sucur.AddCell(refe);

            PdfPCell sucurs1 = (new PdfPCell(new Phrase("SCOTIABANK", fuente8)) );
            sucurs1.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(sucurs1);


            PdfPCell nume6 = new PdfPCell(new Phrase("3" + "00" + suc + "00000" + numcreditoo + "0" + ciclo + "001", fuente8));
            nume6.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(nume6);

            PdfPCell nume1 = new PdfPCell(new Phrase("1777", fuente8));
            nume1.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(nume1);


            PdfPCell fecS = (new PdfPCell(new Phrase("BANORTE", fuente8)));
            fecS.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(fecS);

            PdfPCell nume7 = new PdfPCell(new Phrase("3" + "00" + suc + "00000" + numcreditoo + "0" + ciclo + "001", fuente8));
            nume7.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(nume7);

            PdfPCell nume2 = new PdfPCell(new Phrase("64861", fuente8));
            nume2.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(nume2);            

            PdfPCell fecS1 = (new PdfPCell(new Phrase("BANCOMER", fuente8)));
            fecS1.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(fecS1);

            PdfPCell nume8 = new PdfPCell(new Phrase("3" + "00" + suc + "00000" + numcreditoo + "0" + ciclo + "001", fuente8));
            nume8.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(nume8);

            PdfPCell nume3 = new PdfPCell(new Phrase("904449", fuente8));
            nume3.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(nume3);
            

            PdfPCell fecE = (new PdfPCell(new Phrase("BANSEFI", fuente8)) );
            fecE.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(fecE);

            PdfPCell nume9 = new PdfPCell(new Phrase("3" + "00" + suc + "00000" + numcreditoo + "0" + ciclo + "001", fuente8));
            nume9.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(nume9);

            PdfPCell nume4 = new PdfPCell(new Phrase("75308023", fuente8));
            nume4.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(nume4);            

            PdfPCell fecE1 = (new PdfPCell(new Phrase("BANCOPEL", fuente8)) );
            fecE1.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(fecE1);

            PdfPCell nume10 = new PdfPCell(new Phrase("3" + "00" + suc + "00000" + numcreditoo + "0" + ciclo + "001", fuente8));
            nume10.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(nume10);

            PdfPCell nume5 = new PdfPCell(new Phrase("12000000734", fuente8));
            nume5.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur.AddCell(nume5);

            
            documento.Add(sucur);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPTable sucurDep = new PdfPTable(2);
            sucurDep.WidthPercentage = 60f;
            sucurDep.HorizontalAlignment = Element.ALIGN_CENTER;
            int[] sucurDepcellwidth = { 30, 70 };
            sucurDep.SetWidths(sucurDepcellwidth);

            PdfPCell NomAc = new PdfPCell(new Phrase("Nombre crédito productivo:".ToUpper(), fuente8));
            NomAc.HorizontalAlignment = Element.ALIGN_LEFT;
            NomAc.BackgroundColor = BaseColor.LIGHT_GRAY;
            sucurDep.AddCell(NomAc);

            PdfPCell NomAc2 = new PdfPCell(new Phrase(" " + grup.ToUpper(), fuente8));
            NomAc2.HorizontalAlignment = Element.ALIGN_CENTER;
            sucurDep.AddCell(NomAc2);

            PdfPCell Ncred = new PdfPCell(new Phrase("no. del grupo:".ToUpper(), fuente8));
            Ncred.HorizontalAlignment = Element.ALIGN_LEFT;
            Ncred.BackgroundColor = BaseColor.LIGHT_GRAY;
            sucurDep.AddCell(Ncred);

            PdfPCell Ncred2 = new PdfPCell(new Phrase(" "+numcreditoo.ToString().ToUpper(), fuente8));
            Ncred2.HorizontalAlignment = Element.ALIGN_CENTER;
            sucurDep.AddCell(Ncred2);
            

            PdfPCell Sucursall = new PdfPCell(new Phrase("Sucursal:".ToUpper(), fuente8));
            Sucursall.HorizontalAlignment = Element.ALIGN_LEFT;
            Sucursall.BackgroundColor = BaseColor.LIGHT_GRAY;
            sucurDep.AddCell(Sucursall);

            PdfPCell Sucursall2 = new PdfPCell(new Phrase(" "+suc.ToString().ToUpper(), fuente8));
            Sucursall2.HorizontalAlignment = Element.ALIGN_CENTER;
            sucurDep.AddCell(Sucursall2);


            PdfPCell MonCo = new PdfPCell(new Phrase("garantía liquida a cubrir".ToUpper(), fuente8));
            MonCo.HorizontalAlignment = Element.ALIGN_LEFT;
            MonCo.BackgroundColor = BaseColor.LIGHT_GRAY;
            sucurDep.AddCell(MonCo);

            PdfPCell MonCo2 = new PdfPCell(new Phrase(" " + montGL.ToString("C2").ToUpper(), fuente8));
            MonCo2.HorizontalAlignment = Element.ALIGN_CENTER;
            sucurDep.AddCell(MonCo2);

            PdfPTable linea = new PdfPTable(1);
            linea.WidthPercentage = 100f;


            PdfPCell div = (new PdfPCell(new Phrase("\n \n \n \n \n \n \n______________________________________________________________________________________________________________________", fuente8)) { Colspan = 2 });
            div.HorizontalAlignment = Element.ALIGN_CENTER;
            div.Border = 0;
            linea.AddCell(div);

            documento.Add(sucurDep);
            documento.Add(linea);

            documento.Add(new Paragraph(" "));

            //encabezado
            PdfPTable tablaEncabezado2 = new PdfPTable(2);
            tablaEncabezado2.SetWidths(new float[] { 2f, 8f });
            tablaEncabezado2.DefaultCell.Border = 0;
            tablaEncabezado2.WidthPercentage = 86f;


            PdfPCell titulo2 = new PdfPCell(new Phrase("ASESORIA Y SERVICIO RURAL Y URBANO S.A. DE C.V. SOFOM ENR" + Environment.NewLine + Environment.NewLine + "  ", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD)));
            //PdfPCell titulo = new PdfPCell(new Phrase("Gaarve S.A de C.V. " + Environment.NewLine + Environment.NewLine + " Informe de Nomina " + Environment.NewLine + Environment.NewLine + strDatosObra, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            titulo2.HorizontalAlignment = 1;
            titulo2.Border = 0;
            titulo2.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado2.AddCell(logo);
            tablaEncabezado2.AddCell(titulo2);

            documento.Add(tablaEncabezado2);
            documento.Add(new Paragraph(" "));

            PdfPTable pagSeg = new PdfPTable(1);
            pagSeg.WidthPercentage = 80f;

            PdfPCell refSeg = new PdfPCell(new Phrase ("REFERENCIA PAGO SEGURO",fuente22));
            refSeg.HorizontalAlignment = Element.ALIGN_CENTER;
            refSeg.VerticalAlignment = Element.ALIGN_MIDDLE;
            refSeg.BackgroundColor = BaseColor.LIGHT_GRAY;
            pagSeg.AddCell(refSeg);
            documento.Add(pagSeg);
            documento.Add(new Paragraph(" "));


            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];


                foreach (DataRow r in ds.Tables[0].Rows)
                {


                }

            }

            PdfPTable sucur2 = new PdfPTable(3);
            sucur2.WidthPercentage = 60f;
            int[] sucur2cellwidth = { 30, 30, 40 };
            sucur2.SetWidths(sucur2cellwidth);

            
            PdfPCell sucurs2 = (new PdfPCell(new Phrase("Banco".ToUpper(), fuente8))  );
            sucurs2.HorizontalAlignment = Element.ALIGN_CENTER;
            sucurs2.BackgroundColor = BaseColor.LIGHT_GRAY;
            sucur2.AddCell(sucurs2);

            PdfPCell refe2 = new PdfPCell(new Phrase("Referencia".ToUpper(), fuente8));
            refe2.HorizontalAlignment = Element.ALIGN_CENTER;
            refe2.BackgroundColor = BaseColor.LIGHT_GRAY;
            sucur2.AddCell(refe2);

            PdfPCell banco2 = new PdfPCell(new Phrase("Convenio".ToUpper(), fuente8));
            banco2.HorizontalAlignment = Element.ALIGN_CENTER;
            banco2.BackgroundColor = BaseColor.LIGHT_GRAY;
            sucur2.AddCell(banco2);
            

            PdfPCell sucursa = (new PdfPCell(new Phrase("SCOTIABANK", fuente8)) );
            sucursa.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(sucursa);

            PdfPCell nume62 = new PdfPCell(new Phrase("5" + "00" + suc + "00000" + numcreditoo + "0" + ciclo + "001", fuente8));
            nume62.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(nume62);

            PdfPCell numer = new PdfPCell(new Phrase("1777", fuente8));
            numer.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(numer);

            

            PdfPCell fecS2 = (new PdfPCell(new Phrase("BANORTE", fuente8))  );
            fecS2.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(fecS2);

            PdfPCell nume72 = new PdfPCell(new Phrase("5" + "00" + suc + "00000" + numcreditoo + "0" + ciclo + "001", fuente8));
            nume72.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(nume72);

            PdfPCell numer2 = new PdfPCell(new Phrase("64861", fuente8));
            numer2.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(numer2);

            

            PdfPCell fecS3 = (new PdfPCell(new Phrase("BANCOMER", fuente8))  );
            fecS3.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(fecS3);

            PdfPCell nume82 = new PdfPCell(new Phrase("5" + "00" + suc + "00000" + numcreditoo + "0" + ciclo + "001", fuente8));
            nume82.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(nume82);

            PdfPCell numer3 = new PdfPCell(new Phrase("904449", fuente8));
            numer3.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(numer3);

            

            PdfPCell fecE2 = (new PdfPCell(new Phrase("BANSEFI", fuente8))  );
            fecE2.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(fecE2);


            PdfPCell nume92 = new PdfPCell(new Phrase("5" + "00" + suc + "00000" + numcreditoo + "0" + ciclo + "001", fuente8));
            nume92.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(nume92);

            PdfPCell numer4 = new PdfPCell(new Phrase("75308023", fuente8));
            numer4.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(numer4);


            PdfPCell fecE3 = (new PdfPCell(new Phrase("BANCOPEL", fuente8))  );
            fecE3.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(fecE3);

            PdfPCell nume102 = new PdfPCell(new Phrase("5" + "00" + suc + "00000" + numcreditoo + "0" + ciclo + "001", fuente8));
            nume102.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(nume102);

            PdfPCell numer5 = new PdfPCell(new Phrase("12000000734", fuente8));
            numer5.HorizontalAlignment = Element.ALIGN_CENTER;
            sucur2.AddCell(numer5);

            
            documento.Add(sucur2);
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph(" "));

            PdfPTable sucurDep2 = new PdfPTable(2);
            sucurDep2.WidthPercentage = 60f;
            sucurDep2.HorizontalAlignment = Element.ALIGN_CENTER;
            int[] sucurDep2cellwidth = { 30, 70 };
            sucurDep.SetWidths(sucurDepcellwidth);

            PdfPCell NomAc22 = new PdfPCell(new Phrase("Nombre del crédito productivo:".ToUpper(), fuente8));
            NomAc22.HorizontalAlignment = Element.ALIGN_LEFT;
            NomAc22.BackgroundColor = BaseColor.LIGHT_GRAY;
            sucurDep2.AddCell(NomAc22);

            PdfPCell NomAc222 = new PdfPCell(new Phrase(" " + grup, fuente8));
            NomAc222.HorizontalAlignment = Element.ALIGN_CENTER;
            sucurDep2.AddCell(NomAc222);

            PdfPCell Ncred21 = new PdfPCell(new Phrase("No. de grupo:".ToUpper(), fuente8));
            Ncred21.HorizontalAlignment = Element.ALIGN_LEFT;
            Ncred21.BackgroundColor = BaseColor.LIGHT_GRAY;
            sucurDep2.AddCell(Ncred21);

            PdfPCell Ncred22 = new PdfPCell(new Phrase(" " + numcreditoo.ToString().ToUpper(), fuente8));
            Ncred22.HorizontalAlignment = Element.ALIGN_CENTER;
            sucurDep2.AddCell(Ncred22);

            

            PdfPCell Sucursall21 = new PdfPCell(new Phrase("Sucursal:".ToUpper(), fuente8));
            Sucursall21.HorizontalAlignment = Element.ALIGN_LEFT;
            Sucursall21.BackgroundColor = BaseColor.LIGHT_GRAY;
            sucurDep2.AddCell(Sucursall21);

            PdfPCell Sucursall211 = new PdfPCell(new Phrase(" " + suc, fuente8));
            Sucursall211.HorizontalAlignment = Element.ALIGN_CENTER;
            sucurDep2.AddCell(Sucursall211);


            PdfPCell MonCo21 = new PdfPCell(new Phrase("Monto seguro a cubrir".ToUpper(), fuente8));
            MonCo21.HorizontalAlignment = Element.ALIGN_LEFT;
            MonCo21.BackgroundColor = BaseColor.LIGHT_GRAY;
            sucurDep2.AddCell(MonCo21);

            PdfPCell MonCo22 = new PdfPCell(new Phrase(" " + montTot.ToString("C2"), fuente8));
            MonCo22.HorizontalAlignment = Element.ALIGN_CENTER;
            sucurDep2.AddCell(MonCo22);





            documento.Add(sucurDep2);

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
