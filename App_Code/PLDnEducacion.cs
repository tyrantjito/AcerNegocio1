using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDnEducacion
/// </summary>
public class PLDnEducacion
{
    public PLDnEducacion()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    Ejecuciones ejecuta = new Ejecuciones();
    public string codigoEducacion { get; set; }
    public string nombEducacion { get; set; }


    public object[] retorno;
    private string sql;

    public void obtieneEducacion()
    {
        sql = "select * from PLD_Nivel_Escolaridad where codigo_neducacion='" + codigoEducacion + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaEducacion()
    {
        sql = "delete from PLD_Nivel_Escolaridad where codigo_neducacion='" + codigoEducacion + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarEducacion()
    {
        sql = "insert into PLD_Nivel_Escolaridad (codigo_neducacion, nombre_neducacion)" +
              " values ((select isnull((select top 1 codigo_neducacion from PLD_Nivel_Escolaridad order by codigo_neducacion desc),0)+1),'" + nombEducacion + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editaEducacion()
    {
        sql = "UPDATE PLD_Nivel_Escolaridad " +
                " SET nombre_neducacion ='" + nombEducacion + "' where codigo_neducacion='" + codigoEducacion + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}