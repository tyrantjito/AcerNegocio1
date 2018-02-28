using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ConPS
/// </summary>
public class ConPS
{

    Ejecuciones ejecuta = new Ejecuciones();
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int idSolicitudEdita { get; set; }
    public int idcliente { get; set; }
    public int npago { get; set; }
    public object[] retorno { get; set; }
    private string sql;
    public ConPS()
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
        sql = "select d.nombre_cliente,d.credito_autorizado*.10 as GL,d.credito_autorizado, d.credito_autorizado/e.plazo as pagosemanal,o.no_pago,o.monto_Pago,o.monto_Ahorro from an_solicitud_credito_detalle d left join an_solicitud_credito_encabezado e  on d.id_solicitud_credito= e.id_solicitud_credito left join an_operacion_credito o on d.id_cliente=o.id_cliente  where e.id_solicitud_credito=1 and d.id_empresa=1 and d.id_sucursal=1 and no_pago="+npago;
        //sql = "select nombre_cliente from an_solicitud_credito_detalle  where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtieneInfoEnca()
    {
        sql = "select plazorc from an_solicitud_credito_encabezado where id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }

}