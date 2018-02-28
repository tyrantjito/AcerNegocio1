using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDdFondo
/// </summary>
public class PLDdFondo
{
    public PLDdFondo()
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
        sql = "select * from PLD_Destino_Fondo where codigo_dfondo='" + _CodigoFondo + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void elimina_Fondo()
    {
        sql = "delete from PLD_Destino_Fondo where codigo_dfondo='" + _CodigoFondo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregar_Fondo()
    {
        sql = "insert into PLD_Destino_Fondo (codigo_dfondo, nombre_dfondo)" +
              " values ((select isnull((select top 1 codigo_dfondo from PLD_Destino_Fondo order by cast (codigo_dfondo as int) desc),0)+1),'" + _NombreFondo + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void edita_Fondo()
    {
        sql = "UPDATE PLD_Destino_Fondo SET nombre_dfondo='" + _NombreFondo + "' where codigo_dfondo='" + _CodigoFondo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}