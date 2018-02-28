using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Paga
/// </summary>
public class Paga
{
    Ejecuciones ejecuta = new Ejecuciones();
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int idSolicitudEdita { get; set; }
    public int idcliente { get; set; }

    public object[] retorno { get; set; }
    private string sql;
    public Paga()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public void obtieneInfoDeta()
    {
        sql = "select d.credito_autorizado,e.fecha_solicitud,e.fecha_entrega_credito from an_solicitud_credito_detalle d inner join AN_Solicitud_Credito_Encabezado e on d.id_solicitud_credito = e.id_solicitud_credito where d.id_empresa=" + empresa + " and d.id_sucursal=" + sucursal + " and d.id_solicitud_credito=" + idSolicitudEdita+" and d.id_cliente="+idcliente;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtieneInfoDetalle()
    {
        sql = "select d.nombre_cliente,f.curp_cli,credito_autorizado from an_solicitud_credito_detalle d inner join an_ficha_datos f on d.id_cliente = f.id_cliente  where d.id_empresa=" + empresa + " and d.id_sucursal=" + sucursal + " and d.id_cliente=" + idcliente;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtieneclientes()
    {
        sql = "select id_cliente from an_solicitud_credito_detalle where id_empresa="+empresa+" and id_sucursal="+sucursal+" and id_solicitud_credito="+idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }

    public void tablaAmorti()
    {
        sql = "select e.plazo,e.tasa,e.fecha_entrega_credito,d.credito_autorizado from an_solicitud_credito_detalle d inner join an_solicitud_credito_encabezado e on d.id_solicitud_credito = e.id_solicitud_credito  where d.id_empresa=" + empresa + " and d.id_sucursal=" + sucursal + " and d.id_cliente=" + idcliente;
        retorno = ejecuta.dataSet(sql);
    }

    public void losavales()
    {

    }


    public void obtenerEmpresa()
    {
        sql = "select*from an_parametros where id_empresa=" + empresa + " and id_sucursal=" + sucursal;
        retorno = ejecuta.dataSet(sql);
    }
}