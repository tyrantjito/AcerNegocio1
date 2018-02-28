using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDEdos
/// </summary>
public class PLDEdos
{
    Ejecuciones ejecuta = new Ejecuciones();
    public string codigo { get; set; }
    public string nombEdo { get; set; }


    public object[] retorno;
    private string sql;
    public PLDEdos()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public void obtienepldEdos()
    {
        sql = "select * from PLD_Estados where codigo_estado='" + codigo + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaPldEdo()
    {
        sql = "delete from PLD_Estados where codigo_estado='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarpldEdo()
    {
        sql = "insert into PLD_Estados (codigo_estado, nombre_estado)" +
              " values ((select isnull((select top 1 codigo_estado from PLD_Estados order by codigo_estado desc),0)+1),'" + nombEdo + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editapldedo()
    {
        sql = "UPDATE PLD_Estados " +
                " SET   nombre_estado='" + nombEdo + "' where codigo_estado='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}