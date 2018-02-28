using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de MCon
/// </summary>
public class MCon
{
    Ejecuciones ejecuta = new Ejecuciones();
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int idSolicitudEdita { get; set; }
    public int id_cliente { get; set; }
    public int id_grupo { get; set; }
    public int id_ficha { get; set; }
    public int id_adjunto { get; set; }

    
    public object[] retorno { get; set; }
    private string sql;
    public MCon()
    {
        
    }
    public void optieneimpresion()
    {
        sql = "select * from an_solicitud_credito_encabezado where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }

   
    public void optieneimpresion1()
    {
        sql = "select * from an_solicitud_credito_detalle where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }
    public void existeFicha()
    {
        sql = "select * from AN_Ficha_Datos where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + id_cliente ;
        retorno = ejecuta.scalarToString(sql);
    }
    public void existeIntegrantesSol()
    {
        sql = "select count(*) from AN_Solicitud_Credito_Detalle where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.scalarToString(sql);
    }
    public void existeAdjuntosFicha()
    {
        sql = "select extension from AN_Adjunto_FIcha_datos where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_ficha=" + id_ficha;
        retorno = ejecuta.scalarToString(sql);
    }
    public void obtieneImagen()
    {
        sql = "select descripcion_adjunto,extension,adjunto from an_adjunto_ficha_datos where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_ficha=" + id_ficha + " and id_ficha_adjunto=" + id_adjunto;
        retorno = ejecuta.dataSet(sql);
    }
    public void tieneokAMC()
    {
        sql = "update an_solicitud_credito_detalle set amc='AUTORIZADO' where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + id_cliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void notieneokAMC()
    {
        sql = "update an_solicitud_credito_detalle set amc='NO AUTORIZADO' where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + id_cliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void tieneokAMCs()
    {
        sql = "update an_solicitud_credito_encabezado set amcs='AUTORIZADO' where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

 
    public void validarokamc()
    {
        sql = "select amc from an_solicitud_credito_detalle  where id_empresa=" + empresa + " and id_sucursal=" + sucursal;
    }
    public void tieneAMC()
    {
        sql = "select count(*) from an_solicitud_credito_detalle  where id_empresa=" + empresa + " and id_sucursal=" + sucursal +" and id_solicitud_credito="+idSolicitudEdita + " and  amc='AUTORIZADO'";
        retorno = ejecuta.scalarToInt(sql);
    }
}