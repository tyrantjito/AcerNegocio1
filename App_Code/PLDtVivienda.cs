using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDtVivienda
/// </summary>
public class PLDtVivienda
{
    Ejecuciones ejecuta = new Ejecuciones();
    public string codigotVivienda { get; set; }
    public string nombtVivienda { get; set; }


    public object[] retorno;
    private string sql;
    public PLDtVivienda()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public void obtienetVivienda()
    {
        sql = "select * from PLD_Tipo_Vivienda where codigo_tvivienda='" + codigotVivienda + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaPldtVivienda()
    {
        sql = "delete from PLD_Tipo_Vivienda where codigo_tvivienda='" + codigotVivienda + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarpldtVivienda()
    {
        sql = "insert into PLD_Tipo_Vivienda (codigo_tvivienda, nombre_tvivienda)" +
              " values ((select isnull((select top 1 codigo_tvivienda from PLD_Tipo_Vivienda order by codigo_tvivienda desc),0)+1),'" + nombtVivienda + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editapldtVivienda()
    {
        sql = "UPDATE PLD_Tipo_Vivienda " +
                " SET nombre_tvivienda ='" + nombtVivienda + "' where codigo_tvivienda='" + codigotVivienda + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}