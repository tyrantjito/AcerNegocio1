using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDfTrabajo
/// </summary>
public class PLDfTrabajo
{
    public PLDfTrabajo()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    Ejecuciones ejecuta = new Ejecuciones();
    public string codigoftrabajo { get; set; }
    public string nombtrabajo { get; set; }


    public object[] retorno;
    private string sql;

    public void obtieneTrabajo()
    {
        sql = "select * from PLD_Forma_De_Trabajo where codigo_ftrabajo='" + codigoftrabajo + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaTrabajo()
    {
        sql = "delete from PLD_Forma_De_Trabajo where codigo_ftrabajo='" + codigoftrabajo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarTrabajo()
    {
        sql = "insert into PLD_Forma_De_Trabajo (codigo_ftrabajo, nombre_ftrabajo)" +
              " values ((select isnull((select top 1 codigo_ftrabajo from PLD_Forma_De_Trabajo order by codigo_ftrabajo desc),0)+1),'" + nombtrabajo + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editaTrabajo()
    {
        sql = "UPDATE PLD_Forma_De_Trabajo " +
                " SET nombre_ftrabajo ='" + nombtrabajo + "' where codigo_ftrabajo='" + codigoftrabajo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}