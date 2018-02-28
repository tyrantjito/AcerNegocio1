using System;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.io;
using System.Diagnostics;
using System.IO;

/// <summary>
/// Descripción breve de ImpresionComparativo
/// </summary>
public class ImpresionComparativo
{
    Ejecuciones ejecuta = new Ejecuciones();
    string sql;
    bool resultado;
    object[] ejecutados = new object[2];

    public ImpresionComparativo()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string GenComparativo(int[] sessiones, int opcion)
    {
        int noOrden = sessiones[4];
        int idEmpresa = sessiones[2];
        int idTaller = sessiones[3];
        int idUsuario = sessiones[0];
        // Crear documento
        Document documento = new Document(iTextSharp.text.PageSize.LEGAL.Rotate());
        documento.AddTitle("ComparativoCotizacion_E" + idEmpresa.ToString() + "_T" + idTaller.ToString() + "_Orden" + noOrden.ToString());
        documento.AddCreator("MoncarWeb");

        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\" + "ComparativoCotizacion_E" + idEmpresa.ToString() + "_T" + idTaller.ToString() + "_Orden" + noOrden.ToString() + ".pdf";
        //si no existe la carpeta temporal la creamos 
        if (!(Directory.Exists(ruta)))
        {
            Directory.CreateDirectory(ruta);
        }
        if (archivo.Trim() != "")
        {
            FileStream file = new FileStream(archivo,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            FileShare.ReadWrite);
            PdfWriter.GetInstance(documento, file);
            // Abrir documento.
            documento.Open();

            string imagepath = HttpContext.Current.Server.MapPath("img/");
            iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(imagepath + "logo.png");
            gif.WidthPercentage = 15f;

            PdfPTable tablaEncabezado = new PdfPTable(3);
            tablaEncabezado.SetWidths(new float[] { 2.5f, 5f, 2.5f });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;

            string prefijoTaller = ejecuta.scalarToStringSimple("select ltrim(rtrim(identificador)) from talleres where id_taller=" + idTaller.ToString());

            PdfPCell tituPagina = new PdfPCell(new Phrase("Comparativo de Refacciones", FontFactory.GetFont("ARIAL", 16, iTextSharp.text.Font.BOLD)));
            tituPagina.HorizontalAlignment = 1;
            tituPagina.VerticalAlignment = 1;
            tituPagina.Border = 0;
            tituPagina.VerticalAlignment = Element.ALIGN_BOTTOM;

            PdfPCell titu = new PdfPCell(new Phrase("N° Orden: " + prefijoTaller + " " + noOrden, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            titu.HorizontalAlignment = 2;
            titu.Border = 0;
            titu.VerticalAlignment = Element.ALIGN_BOTTOM;
            tablaEncabezado.AddCell(gif);
            tablaEncabezado.AddCell(tituPagina);
            tablaEncabezado.AddCell(titu);

            documento.Add(tablaEncabezado);

            Mano_Obra(documento, idEmpresa, idTaller, noOrden, sessiones, opcion);
            documento.Add(new Paragraph(" "));
            documento.AddCreationDate();
            documento.Add(new Paragraph(""));
            documento.Close();
        }
        return archivo;
    }

    private void Mano_Obra(Document documento, int idEmpresa, int idTaller, int noOrden, int[] sessiones, int opcion)
    {
        // Tipo de Fuentes
        iTextSharp.text.Font estilo2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        //Obtener Datos Comparativo
        datosCotizaProv cotizacion = new datosCotizaProv();
        if (opcion!=1)
            sql = "select rank() over(order by id_cliprov),id_cliprov from Cotizacion_Detalle where id_empresa=" + sessiones[2].ToString() + " and id_taller=" + sessiones[3].ToString() + " and no_orden=" + sessiones[4].ToString() + " and id_cotizacion=" + sessiones[6].ToString() + " group by id_cliprov";
        else if (opcion == 1)
            sql = "select rank() over(order by id_cliprov),id_cliprov from Cotizacion_Detalle where id_empresa=" + sessiones[2].ToString() + " and id_taller=" + sessiones[3].ToString() + " and no_orden=" + sessiones[4].ToString() + " group by id_cliprov";
        DataSet proveedoresCotizacion = new DataSet();
        object[] proveedoresCotizantes = ejecuta.dataSet(sql);
        if (Convert.ToBoolean(proveedoresCotizantes[0]))
        {
            proveedoresCotizacion = (DataSet)proveedoresCotizantes[1];
        }

        int proveedoresTotalCotz = proveedoresCotizacion.Tables[0].Rows.Count;
        //meter proveedoresTotalCotz en la generacion de columnas para saber los proveedores a pintar
        object[] camposManObra = new object[2];
        if (opcion!=1)
            camposManObra = cotizacion.generaComparativo(sessiones);
        else if (opcion == 1)
            camposManObra = cotizacion.generaComparativoGral(sessiones);
        if (Convert.ToBoolean(camposManObra[0]))
        {
            DataSet datos = (DataSet)camposManObra[1];
            if (datos.Tables[0].Rows.Count != 0)
            {
                PdfPTable tablaEncabezadoFila = new PdfPTable(3);
                tablaEncabezadoFila.SetWidths(new float[] { 10F, 10F, (proveedoresTotalCotz*20f) });
                tablaEncabezadoFila.WidthPercentage = 100f;
                PdfPTable tablaTitFila = new PdfPTable(proveedoresTotalCotz+2);

                float[] arrAnchos = new float[proveedoresTotalCotz+2];
                arrAnchos[0] = arrAnchos[1] = 10f;
                for (int contaFor=2;contaFor<arrAnchos.Length;contaFor++)
                    arrAnchos[contaFor] = 20f;

                tablaTitFila.SetWidths(arrAnchos);
                tablaTitFila.WidthPercentage = 100f;
                PdfPCell cellTitu = new PdfPCell(new Phrase("Cantidad", FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                cellTitu.Border = 0;
                tablaEncabezadoFila.AddCell(cellTitu);
                cellTitu = new PdfPCell(new Phrase("Descripción", FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                cellTitu.Border = 0;
                tablaEncabezadoFila.AddCell(cellTitu);
                cellTitu = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                cellTitu.Border = 0;
                tablaEncabezadoFila.AddCell(cellTitu);
                documento.Add(tablaEncabezadoFila);
                int refaccionesTotales = datos.Tables[0].Rows.Count;
                int countForeach = 1;
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    PdfPCell cellCantidad = new PdfPCell(new Phrase(fila[2].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    cellCantidad.Border = 0;
                    cellCantidad.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tablaTitFila.AddCell(cellCantidad);
                    PdfPCell cellDescripcion = new PdfPCell(new Phrase(fila[3].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    cellDescripcion.Border = 0;
                    cellDescripcion.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tablaTitFila.AddCell(cellDescripcion);
                    int rs = 5, cu = 6, pd = 7, id = 8, im = 9, ex = 10, ds = 11;
                    for (int cont = 0; cont < proveedoresTotalCotz; cont++)
                    {
                        PdfPTable tablaDetalle = new PdfPTable(1);
                        tablaDetalle.DefaultCell.Border = 0;
                        PdfPTable tablaDetalleNumeros = new PdfPTable(3);
                        tablaDetalleNumeros.DefaultCell.Border = 1;
                        if (countForeach == 1)
                        {
                            PdfPCell cellRazon = new PdfPCell(new Phrase(fila[rs].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                            cellRazon.Border = 0;
                            cellRazon.BackgroundColor = BaseColor.LIGHT_GRAY;
                            cellRazon.VerticalAlignment = Element.ALIGN_MIDDLE;
                            tablaDetalle.AddCell(cellRazon);
                        }

                        if (fila[im].ToString() != "0" && fila[im].ToString() != "0.00")
                        {

                            PdfPCell cellCostoUnitario = new PdfPCell(new Phrase(fila[im].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                            cellCostoUnitario.Border = 0;
                            cellCostoUnitario.Rowspan = 5;
                            cellCostoUnitario.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellCostoUnitario.HorizontalAlignment = Element.ALIGN_MIDDLE;
                            tablaDetalleNumeros.AddCell(cellCostoUnitario);

                            PdfPCell cellCostoUnitarioTitu = new PdfPCell(new Phrase("C. U.", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                            cellCostoUnitarioTitu.Border = 0;
                            cellCostoUnitarioTitu.HorizontalAlignment = 0;
                            tablaDetalleNumeros.AddCell(cellCostoUnitarioTitu);

                            PdfPCell cellCostoUnitarioMini = new PdfPCell(new Phrase(fila[cu].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellCostoUnitarioMini.Border = 0;
                            cellCostoUnitarioMini.HorizontalAlignment = 2;
                            tablaDetalleNumeros.AddCell(cellCostoUnitarioMini);

                            PdfPCell cellPorcDecto = new PdfPCell(new Phrase("Porc. Dto.", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                            cellPorcDecto.Border = 0;
                            cellPorcDecto.HorizontalAlignment = 0;
                            tablaDetalleNumeros.AddCell(cellPorcDecto);

                            PdfPCell cellPorcDectoMini = new PdfPCell(new Phrase(fila[pd].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellPorcDectoMini.Border = 0;
                            cellPorcDectoMini.HorizontalAlignment = 2;
                            tablaDetalleNumeros.AddCell(cellPorcDectoMini);

                            PdfPCell cellDescuento = new PdfPCell(new Phrase("Dto.", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                            cellDescuento.Border = 0;
                            cellDescuento.HorizontalAlignment = 0;
                            tablaDetalleNumeros.AddCell(cellDescuento);

                            PdfPCell cellDescuentoMini = new PdfPCell(new Phrase(fila[id].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellDescuentoMini.Border = 0;
                            cellDescuentoMini.HorizontalAlignment = 2;
                            tablaDetalleNumeros.AddCell(cellDescuentoMini);

                            PdfPCell cellExistencia = new PdfPCell(new Phrase("Exist.", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                            cellExistencia.Border = 0;
                            cellExistencia.HorizontalAlignment = 0;
                            tablaDetalleNumeros.AddCell(cellExistencia);

                            string truFal = "";
                            if (fila[ex].ToString() == "True")
                                truFal = "Si";
                            else
                                truFal = "No";
                            PdfPCell cellExistenciaMini = new PdfPCell(new Phrase(truFal, FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellExistenciaMini.Border = 0;
                            cellExistenciaMini.HorizontalAlignment = 1;
                            tablaDetalleNumeros.AddCell(cellExistenciaMini);

                            PdfPCell cellDiasEntregaTitu = new PdfPCell(new Phrase("Entrega", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                            cellDiasEntregaTitu.Border = 0;
                            cellDiasEntregaTitu.HorizontalAlignment = 0;
                            tablaDetalleNumeros.AddCell(cellDiasEntregaTitu);

                            PdfPCell cellDiasEntregaMini = new PdfPCell(new Phrase(fila[ds].ToString() + " día(s)", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellDiasEntregaMini.Border = 0;
                            cellDiasEntregaMini.HorizontalAlignment = 1;
                            tablaDetalleNumeros.AddCell(cellDiasEntregaMini);

                            tablaDetalle.AddCell(tablaDetalleNumeros);
                            tablaTitFila.AddCell(tablaDetalle);
                        }
                        else
                        {
                            PdfPCell cellVacio = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellVacio.Border = 0;
                            tablaDetalle.AddCell(cellVacio);
                            tablaTitFila.AddCell(tablaDetalle);
                        }
                        rs += 9; cu += 9; pd += 9; id += 9; im += 9; ex += 9; ds += 9;
                    }

                    countForeach++;
                }
                documento.Add(tablaTitFila);
            }
        }
    }
}