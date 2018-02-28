using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de AsigFond
/// </summary>
public class AsigFond
{
    Ejecuciones ejecuta = new Ejecuciones();

    public int Grupo { get; set; }
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int fondeo { get; set; }
    public int credito { get; set; }
    public string nombregrupo { get; set; }

    private string sql;
    public object[] retorno;
    public AsigFond()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public void editaAnalistaCred()
    {
        sql = "UPDATE AN_Creditos  " +
                " SET  id_fondeo=" + fondeo + " where id_empresa="+empresa+" and id_sucursal="+sucursal+ " and id_solicitud_credito="+ Grupo ;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void obtienePuestoEdit()
    {
        sql = "select * from AN_Creditos where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + Grupo;
        retorno = ejecuta.dataSet(sql);
    }
}