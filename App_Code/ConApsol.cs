using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ConApsol
/// </summary>
public class ConApsol
{

    Ejecuciones ejecuta = new Ejecuciones();
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int idSolicitudEdita { get; set; }
    public int idcliente { get; set; }

    public object[] retorno { get; set; }
    private string sql;
    public ConApsol()
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
        sql = "select nombre_cliente from an_solicitud_credito_detalle  where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }
}