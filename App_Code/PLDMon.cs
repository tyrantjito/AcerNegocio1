using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDMon
/// </summary>
public class PLDMon
{
    Ejecuciones ejecuta = new Ejecuciones();
    public string codigo { get; set; }
    public string nomMoneda { get; set; }
    public string siglas { get; set; }

    public object[] retorno;
    private string sql;
    public PLDMon()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public void obtienepldMon()
    {
        sql = "select * from PLD_Moneda where codigo_moneda='" + codigo + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaPldMon()
    {
        sql = "delete from PLD_Moneda where codigo_moneda='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editapldmoneda()
    {
        sql = "UPDATE PLD_Moneda " +
                " SET   nombre_moneda='" + nomMoneda + "', sigla='"+siglas+"' where codigo_moneda='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarpldMon()
    {
        sql = "insert into PLD_Moneda (codigo_moneda, nombre_moneda, sigla)" +
              " values ((select isnull((select top 1 codigo_moneda from PLD_Moneda order by codigo_moneda desc),0)+1),'" + nomMoneda + "','"+siglas+"')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}