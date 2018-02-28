using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de AsigAna
/// </summary>
public class AsigAna
{
    Ejecuciones ejecuta = new Ejecuciones();

    public int Grupo { get; set; }
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int analista { get; set; }
    public int credito { get; set; }
    public string nombregrupo { get; set; }

    private string sql;
    public object[] retorno;
    public AsigAna()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public void obtienePuestoEdit()
    {
        sql = "select * from AN_Creditos where id_empresa="+empresa+" and id_sucursal="+sucursal+" and id_solicitud_credito="+Grupo;
        retorno = ejecuta.dataSet(sql);
    }

    public void editaAnalista()
    {
        sql = "UPDATE an_analista  " +
                " SET  id_grupo=" + Grupo + ", nombreGrupo='" + nombregrupo + "',id_credito="+credito+" where id_analista=" + analista;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void editaAnalistaCred()
    {
        sql = "UPDATE AN_Creditos  " +
                " SET  id_analista=" + analista + " where id_solicitud_credito=" + Grupo;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}