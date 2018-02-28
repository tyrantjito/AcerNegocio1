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

public partial class Cartera : System.Web.UI.Page
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
    protected void lnkImprimir_Click(object sender, EventArgs e)
    {
        string usuario = Request.QueryString["u"].ToString();
        CarteraCod imprimirExcel = new CarteraCod();
        imprimirExcel.usuario = usuario;
        imprimirExcel.imprimeExcel();
        string Archivo = imprimirExcel.archivo;
        if (Archivo != "")
        {
            try
            {
                FileInfo docto = new FileInfo(Archivo);
                if (docto.Exists)
                {
                    Response.Clear();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("content-disposition", "attachment;filename=" + docto.Name);
                    Response.WriteFile(Archivo);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblErrores.Text = "Error al accesar al archivo en el servidor. Detalle: " + ex.Message;
            }
        }
        else
            lblErrores.Text = "No se puedo generar el documento por favor vuelva a intentar";
    }
    /*LinkButton btnImprimir = (LinkButton)sender;

    //tipos de font a utilizar
    iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
    iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
    iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
    iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
    iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

    iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

    //variable local fechas con un objeto tipo Fechas
    Fechas fechas = new Fechas();

    //creacion de un nuevo documento
    Document documento = new Document(iTextSharp.text.PageSize.LETTER.Rotate());
    documento.AddTitle(" Cartera ");
    documento.AddCreator("Desarrollarte");
    string ruta = HttpContext.Current.Server.MapPath("~/files");
    string archivo = ruta + "\\Cartera__" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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

        PdfPTable encabezado = new PdfPTable(11);
        encabezado.DefaultCell.Border = 0;
        int[] encabezadocellwidth = { 7,13,20,3,7,7,7,14,12,5,5 };
        encabezado.SetWidths(encabezadocellwidth);
        encabezado.WidthPercentage = 100f;

        PdfPCell reg = new PdfPCell(new Phrase("Región", fuente2));
        reg.HorizontalAlignment = Element.ALIGN_CENTER;
        encabezado.AddCell(reg);

        PdfPCell nameSuc = new PdfPCell(new Phrase("Nombre Sucursal", fuente2));
        nameSuc.HorizontalAlignment = Element.ALIGN_CENTER;
        encabezado.AddCell(nameSuc);

        PdfPCell nameTeam = new PdfPCell(new Phrase("Nombre Grupo", fuente2));
        nameTeam.HorizontalAlignment = Element.ALIGN_CENTER;
        encabezado.AddCell(nameTeam);

        PdfPCell NoCiclo = new PdfPCell(new Phrase("No.Ciclo", fuente2));
        NoCiclo.HorizontalAlignment = Element.ALIGN_CENTER;
        encabezado.AddCell(NoCiclo);

        PdfPCell NoCredito = new PdfPCell(new Phrase("No. Crédito", fuente2));
        NoCredito.HorizontalAlignment = Element.ALIGN_CENTER;
        encabezado.AddCell(NoCredito);

        PdfPCell codGrupo = new PdfPCell(new Phrase("Código de Grupo", fuente2));
        codGrupo.HorizontalAlignment = Element.ALIGN_CENTER;
        encabezado.AddCell(codGrupo);

        PdfPCell fechPro = new PdfPCell(new Phrase("Fecha del Proceso", fuente2));
        fechPro.HorizontalAlignment = Element.ALIGN_CENTER;
        encabezado.AddCell(fechPro);

        PdfPCell tipCred = new PdfPCell(new Phrase("Tipo de Crédito", fuente2));
        tipCred.HorizontalAlignment = Element.ALIGN_CENTER;
        encabezado.AddCell(tipCred);

        PdfPCell noClient = new PdfPCell(new Phrase("No. de Clientes", fuente2));
        noClient.HorizontalAlignment = Element.ALIGN_CENTER;
        encabezado.AddCell(noClient);

        PdfPCell montCre = new PdfPCell(new Phrase("Monto de Crédito", fuente2));
        montCre.HorizontalAlignment = Element.ALIGN_CENTER;
        encabezado.AddCell(montCre);

        PdfPCell montDese = new PdfPCell(new Phrase("Monto de Crédito", fuente2));
        montDese.HorizontalAlignment = Element.ALIGN_CENTER;
        encabezado.AddCell(montDese);

        documento.Add(encabezado);

        documento.Close();

        }

        //
        FileInfo filename = new FileInfo(archivo);
        if (filename.Exists)
        {
            string url = "Descargas.aspx?filename=" + filename.Name;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "pdfs", "window.open('" + url + "', 'nuevo', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, titlebar=no, fullscreen=yes,resizable=yes');", true);
        }*/
}
    
