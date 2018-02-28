using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDrEconomico
/// </summary>
public class PLDrEconomico
{
    public PLDrEconomico()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    Ejecuciones ejecuta = new Ejecuciones();
    public string codigoEconomico { get; set; }
    public string nombEconomico { get; set; }


    public object[] retorno;
    private string sql;

    public void obtieneEconomico()
    {
        sql = "select * from PLD_Regimen_Economico where codigo_reconomico='" + codigoEconomico + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaEconomico()
    {
        sql = "delete from PLD_Regimen_Economico where codigo_reconomico='" + codigoEconomico + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarEconomico()
    {
        sql = "insert into PLD_Regimen_Economico (codigo_reconomico, nombre_reconomico)" +
              " values ((select isnull((select top 1 codigo_reconomico from PLD_Regimen_Economico order by codigo_reconomico desc),0)+1),'" + nombEconomico + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editaEconomico()
    {
        sql = "UPDATE PLD_Regimen_Economico " +
                " SET nombre_reconomico ='" + nombEconomico + "' where codigo_reconomico='" + codigoEconomico + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}