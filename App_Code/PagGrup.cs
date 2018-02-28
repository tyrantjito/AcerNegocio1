using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PagGrup
/// </summary>
public class PagGrup
{
    Ejecuciones ejecuta = new Ejecuciones();
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int idSolicitudEdita { get; set; }

    public object[] retorno { get; set; }
    private string sql;
    public PagGrup()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    } 

    public void obtieneInfoEncabezado()
    {
        sql = "select * from an_solicitud_credito_encabezado where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtieneInfoDetalle()
    {
        sql = "select d.nombre_cliente,f.curp_cli from an_solicitud_credito_detalle d inner join an_ficha_datos f on d.id_cliente = f.id_cliente  where d.id_empresa=" + empresa + " and d.id_sucursal=" + sucursal + " and d.id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }
    public void tablaAmor()
    {
        sql = "select fecha_entrega_credito,monto_autorizado,plazoRC,tasaRC from AN_Solicitud_Credito_Encabezado  where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtenerEmpresa()
    {
        sql = "select*from an_parametros where id_empresa=" + empresa + " and id_sucursal=" + sucursal;
        retorno = ejecuta.dataSet(sql);
    }
}