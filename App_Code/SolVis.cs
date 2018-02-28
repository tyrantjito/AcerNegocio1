using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de SolVis
/// </summary>
public class SolVis 
{
    Ejecuciones ejecuta = new Ejecuciones();
    //Datos Encabezado
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int idSolicitudEdita { get; set; }
    public int id_cliente { get; set; }
    public string fechasolicitud { get; set; }
    public string fechaEntrega { get; set; }
    public string grupoProductivo { get; set; }
    public int numeroGrupo { get; set; }
    public decimal montoCredito { get; set; }
    public int plazo { get; set; }
    public decimal tasa { get; set; }
    public decimal garantiaLiquida { get; set; }
    public decimal montoMaximo { get; set; }
    public decimal montoAutorizado { get; set; }
    public int plazoRC { get; set; }
    public decimal tasaRC { get; set; }
    public string formaPago { get; set; }
    public int ciclo { get; set; }
    public string observaciones { get; set; }

    
    public object[] retorno { get; set; }
    private string sql;
    public SolVis()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public void obtieneSolicitudEnc()
    {
        sql = "select * from an_solicitud_credito_encabezado where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtieneimpresionEnc()
    {
        sql = "select e.id_sucursal,e.id_grupo,e.grupo_productivo,e.plazorc,e.tasarc,d.credito_autorizado from an_solicitud_credito_encabezado e left join an_solicitud_credito_detalle d on e.id_solicitud_credito= d.id_solicitud_credito where e.id_empresa="+empresa+" and e.id_sucursal="+sucursal+" and e.id_grupo="+idSolicitudEdita+" and id_cliente="+id_cliente;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtieneimpresionDet()
    {
        sql = "select * from an_solicitud_credito_detalle where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtieneDet()
    {
        sql = "select nombre_cliente,ingreso,detino_credito,credito_anterior,credito_solicitado,garantia_liquida,credito_autorizado,telefono from an_solicitud_credito_detalle where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita+" and id_cliente="+id_cliente;
        retorno = ejecuta.dataSet(sql);
    }
}