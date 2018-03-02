using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Data;
using E_Utilities;
using System.Drawing;
using ClosedXML.Excel;

/// <summary>
/// Descripción breve de CarteraCod
/// </summary>
public class CarteraCod
{
    Fechas fechas = new Fechas();
    BaseDatos data = new BaseDatos();
    public string archivo { get; set; }
    public string usuario { get; set; }
    string sql;
    public CarteraCod()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public DataSet llenaCatProd()
    {
        //query para llenar los datos del excel
        DataSet datas = new DataSet();

        this.sql = "select p.idProducto,isnull(p.descripcion,'') as descripcionProducto,isnull(p.idMedida,'') as idMedida, isnull(u.descripcion,'') as descripcionMedida,isnull(p.idFamilia,'') as idFamilia,isnull(f.descripcionFamilia,'') as descripcionFamilia,isnull(p.idLinea,'') as idLinea,isnull(l.descripcionLinea,'') as descripcionLinea,p.detalles,p.observaciones,p.estatus,cat.descripcion_categoria,cat.id_categoria as id_categorias,isnull(p.claveProductoSAT,'') as claveProductoSAT, isnull(p.claveUnidadSAT,'') as claveUnidadSAT from catproductos p left join catunidmedida u on u.idMedida=p.idMedida left join catcategorias cat on cat.id_categoria=p.id_categoria left join catfamilias f on f.idFamilia=p.idFamilia left join catlineas l on l.idLinea=p.idLinea where p.estatus='A'";
        object[] ejecutado = data.scalarData(sql);
        if ((bool)ejecutado[0])
            datas = (DataSet)ejecutado[1];
        else
            datas = null;
        return datas;
    }
    public void imprimeExcel()
    {
        List<string> encabezado = new List<string>{"RegionPrueba","Nombre Sucursal", "Nombre Grupo", "No. Ciclo", "Número de Crédito", "Código de Grupo", "Fecha de Proceso", "Tipo de Crédito", "Número de Clientes", "Monto del Crédito", "Monto del Desembolsado", "Saldo Capital", "Monto Interés", "Monto I.V.A. Interés Moratorio", "Total a Pagar", "Saldo Interés", "Saldo Total", "Monto Cuota", "Monto de Capital Pagado", "Monto de Interés Pagado", "Monto de I.V.A. de interés Pagado", "Monto de Interes Acumulado", "Monto de Interés en Suspenso", "Monto de Interés Anticipado", "I.V.A. por interés Corriente", "Plazo del Crédito", "Fecha apertura del Crédito", "Fecha de Vencimiento del Crédito", "Fecha del último deposito", "Estado del Crédito", "Estado Contable", "Tasa de Interés corriente", "Tasa de Interés Moratorio", "Días de Intrerés Moratorio", "Días de Atrazo", "Mora Capital", "Mora Interés", "Mora Total", "Porcentaje Mora", "Monto de Interés Moratorio", "Monto Cargos interés Moratorios", "Monto Interés Moratorio Acumulado", "I.V.A. por Interés Moratorio", "Monto Cargos I.V.A. por Interés Moratorio", "I.V.A. por interés moratorio acumulado", "Cod. Asesor", "Asesor", "Pagos Comprometidos", "Pagos Reales", "Pagos Reales Acumulado", "Tipo Cartera Cobranza", "Saldo Garantía", "Pago No Exigible Capital", "Pago No Exigible Intereses", "Saldo Principal Pagado por Anticipado", "Saldo Intereses Pagado por Anticipado", "Saldo I.V.A. Pagado por Anticipado", "Clasificación por Atraso", "Monto último Pago", "Día Reunión", "Hora Reunión", "Cuenta Referencia", "Cuenta Referencia Garantía", "pagos_comprometidos_fec", "sal_pagos_comprometidos_fec" };
        try
        {
            //CarteraCod datosProducto = new CarteraCod();
            //DataSet data = new DataSet();
            //data = datosProducto.llenaCatProd();
            //if (data.Tables[0].Rows.Count > 0)
            //{
            var libroExcel = new XLWorkbook();
            var hoja = libroExcel.Worksheets.Add("CARTERA");

            int filas = 1;
            int A = 1;

            foreach (string enc in encabezado)
                hoja.Cell(filas, A++).SetValue(enc).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
            

            /*foreach (DataRow r in data.Tables[0].Rows)
              {
                  hoja.Cell(filas, 1).SetValue(r[0].ToString()).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(false).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.Transparent);
                  hoja.Cell(filas, 2).SetValue(r[1].ToString()).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(false).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.Transparent);
                  hoja.Cell(filas, 3).SetValue(r[3].ToString()).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(false).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.Transparent);
                  hoja.Cell(filas, 4).SetValue(r[11].ToString()).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(false).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.Transparent);
                  hoja.Cell(filas, 5).SetValue(r[5].ToString()).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(false).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.Transparent);
                  hoja.Cell(filas, 6).SetValue(r[7].ToString()).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(false).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.Transparent);
                  hoja.Cell(filas, 7).SetValue(r[8].ToString()).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(false).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.Transparent);
                  hoja.Cell(filas, 8).SetValue(r[9].ToString()).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(false).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.Transparent);
                  hoja.Cell(filas, 9).SetValue(r[14].ToString()).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(false).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.Transparent);
                  hoja.Cell(filas, 10).SetValue(r[13].ToString()).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(false).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.Transparent);


                  filas++;
              }*/
            string ruta = HttpContext.Current.Server.MapPath("~/TMP");
                string archivoGuardado = ruta + "\\CARTERA_DESARROLLARTE_"+usuario+"_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".xlsx";

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                FileInfo docto = new FileInfo(archivoGuardado);
                if (docto.Exists)
                    docto.Delete();

                libroExcel.SaveAs(archivoGuardado);
                archivo = archivoGuardado;
            //}
            //else
                //archivo = "";
        }
        catch (Exception ex) { archivo = ex.Message; }
    }
}