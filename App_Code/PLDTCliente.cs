using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDTCliente
/// </summary>
public class PLDTCliente
{
    public PLDTCliente()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    Ejecuciones ejecuta = new Ejecuciones();
    public string codigo { get; set; }
    public string nombEdo { get; set; }


    public object[] retorno;
    private string sql;
    public void obtienepldEdos()
    {
        sql = "select * from PLD_Tipo_Cliente where codigo_tcliente='" + codigo + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaPldEdo()
    {
        sql = "delete from PLD_Tipo_Cliente where codigo_tcliente='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarpldEdo()
    {
        sql = "insert into PLD_Tipo_Cliente (codigo_tcliente, nombre_cliente)" +
              " values ((select isnull((select top 1 codigo_tcliente from PLD_Tipo_Cliente order by codigo_tcliente desc),0)+1),'" + nombEdo + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editapldedo()
    {
        sql = "UPDATE PLD_Tipo_Cliente " +
                " SET   nombre_cliente='" + nombEdo + "' where codigo_tcliente='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}