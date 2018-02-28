using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDoFondo
/// </summary>
public class PLDoFondo
{
    public PLDoFondo()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string _CodigoFondo { set; get; }
    public string _NombreFondo { set; get; }

    Ejecuciones ejecuta = new Ejecuciones();
    public object[] retorno;
    private string sql;
    public void obtiene_Fondo()
    {
        sql = "select * from PLD_Origen_Fondo where codigo_ofondo='" + _CodigoFondo + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void elimina_Fondo()
    {
        sql = "delete from PLD_Origen_Fondo where codigo_ofondo='" + _CodigoFondo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregar_Fondo()
    {
        sql = "insert into PLD_Origen_Fondo (codigo_ofondo, nombre_ofondo)" +
              " values ((select isnull((select top 1 codigo_ofondo from PLD_Origen_Fondo order by cast (codigo_ofondo as int) desc),0)+1),'" + _NombreFondo + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void edita_Fondo()
    {
        sql = "UPDATE PLD_Origen_Fondo SET nombre_ofondo='" + _NombreFondo + "' where codigo_ofondo='" + _CodigoFondo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}