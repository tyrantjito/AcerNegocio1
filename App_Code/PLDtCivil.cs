using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDtCivil
/// </summary>
public class PLDtCivil
{
    public PLDtCivil()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    Ejecuciones ejecuta = new Ejecuciones();
    public string codigotCivil { get; set; }
    public string nombtCivil { get; set; }


    public object[] retorno;
    private string sql;

    public void obtieneECivil()
    {
        sql = "select * from PLD_Estado_Civil where codigo_ecivil='" + codigotCivil + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaPldECivil()
    {
        sql = "delete from PLD_Estado_Civil where codigo_ecivil='" + codigotCivil + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarpldECivil()
    {
        sql = "insert into PLD_Estado_Civil (codigo_ecivil, nombre_ecivil)" +
              " values ((select isnull((select top 1 codigo_ecivil from PLD_Estado_Civil order by codigo_ecivil desc),0)+1),'" + nombtCivil + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editapldECivil()
    {
        sql = "UPDATE PLD_Estado_Civil " +
                " SET nombre_ecivil ='" + nombtCivil + "' where codigo_ecivil='" + codigotCivil + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}