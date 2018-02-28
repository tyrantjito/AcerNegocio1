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

                hoja.Cell(filas, 1).SetValue("Región").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 2).SetValue("Nombre Sucursal").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 3).SetValue("Nombre Grupo").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 4).SetValue("No. Ciclo").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 5).SetValue("Número de Crédito").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 6).SetValue("Código de Grupo").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 7).SetValue("Fecha de Proceso").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 8).SetValue("Tipo de Crédito").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 9).SetValue("Número de Clientes").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 10).SetValue("Monto del Crédito").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 11).SetValue("Monto del Desembolsado").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 12).SetValue("Saldo Capital").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 13).SetValue("Monto Interés").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 14).SetValue("Monto I.V.A. Interés Moratorio").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 15).SetValue("Total a Pagar").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 16).SetValue("Saldo Interés").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 17).SetValue("Saldo Total").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 18).SetValue("Monto Cuota").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 19).SetValue("Monto de Capital Pagado").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 20).SetValue("Monto de Interés Pagado").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 21).SetValue("Monto de I.V.A. de interés Pagado").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 22).SetValue("Monto de Interes Acumulado").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 23).SetValue("Monto de Interés en Suspenso").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 24).SetValue("Monto de Interés Anticipado").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 25).SetValue("I.V.A. por interés Corriente").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 26).SetValue("Plazo del Crédito").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 27).SetValue("Fecha apertura del Crédito").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 28).SetValue("Fecha de Vencimiento del Crédito").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 29).SetValue("Fecha del último deposito").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 30).SetValue("Estado del Crédito").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 31).SetValue("Estado Contable").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 32).SetValue("Tasa de Interés corriente").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 33).SetValue("Tasa de Interés Moratorio").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 34).SetValue("Días de Intrerés Moratorio").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 35).SetValue("Días de Atrazo").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 36).SetValue("Mora Capital").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 37).SetValue("Mora Interés").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 38).SetValue("Mora Total").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 39).SetValue("Porcentaje Mora").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 40).SetValue("Monto de Interés Moratorio").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 41).SetValue("Monto Cargos interés Moratorios").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 42).SetValue("Monto Interés Moratorio Acumulado").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 43).SetValue("I.V.A. por Interés Moratorio").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 44).SetValue("Monto Cargos I.V.A. por Interés Moratorio").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 45).SetValue("I.V.A. por interés moratorio acumulado").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 46).SetValue("Cod. Asesor").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 47).SetValue("Asesor").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 48).SetValue("Pagos Comprometidos").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 49).SetValue("Pagos Reales").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 50).SetValue("Pagos Reales Acumulado").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 51).SetValue("Tipo Cartera Cobranza").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 52).SetValue("Saldo Garantía").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 53).SetValue("Pago No Exigible Capital").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 54).SetValue("Pago No Exigible Intereses").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 55).SetValue("Saldo Principal Pagado por Anticipado").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 56).SetValue("Saldo Intereses Pagado por Anticipado").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 57).SetValue("Saldo I.V.A. Pagado por Anticipado").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 58).SetValue("Clasificación por Atraso").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 59).SetValue("Monto último Pago").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 60).SetValue("Día Reunión").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 61).SetValue("Hora Reunión").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 62).SetValue("Cuenta Referencia").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 63).SetValue("Cuenta Referencia Garantía").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 64).SetValue("pagos_comprometidos_fec").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);
                hoja.Cell(filas, 65).SetValue("sal_pagos_comprometidos_fec").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Font.SetFontSize(10).Font.SetBold(true).Font.SetFontColor(XLColor.Black).Fill.SetBackgroundColor(XLColor.LightBlue);

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