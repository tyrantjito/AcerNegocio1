using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de DepGar
/// </summary>
public class DepGar
{
    Ejecuciones ejecuta = new Ejecuciones();
    //Datos Encabezado
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int idSolicitudEdita { get; set; }

    public object[] retorno { get; set; }
    private string sql;
    public DepGar()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public void obtieneimpresionEnc()
    {
        sql = "select * from an_solicitud_credito_encabezado where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }
}