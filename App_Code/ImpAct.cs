using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ImpAct
/// </summary>
public class ImpAct
{
    Ejecuciones ejecuta = new Ejecuciones();
    private string sql;
    public object[] retorno;

    public int empresa { get; set; } 
    public int sucursal { get; set; }
    public int idActa { get; set; }
    public int idActaDetalle { get; set; }
    


    public ImpAct()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public void obtieneInfoEncabezado()
    {
        sql = " select * from an_acta_integracion where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_acta=" + idActa;
        retorno = ejecuta.dataSet(sql);
    }
}