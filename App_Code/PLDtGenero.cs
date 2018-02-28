using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDGenero
/// </summary>
public class PLDtGenero
{
    public PLDtGenero()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    Ejecuciones ejecuta = new Ejecuciones();
    public string codigoGenero { get; set; }
    public string nombGenero { get; set; }


    public object[] retorno;
    private string sql;

    public void obtieneGenero()
    {
        sql = "select * from PLD_Genero where codigo_genero='" + codigoGenero + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaGenero()
    {
        sql = "delete from PLD_Genero where codigo_genero='" + codigoGenero + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarGenero()
    {
        sql = "insert into PLD_Genero (codigo_genero, nombre_genero)" +
              " values ((select isnull((select top 1 codigo_genero from PLD_Genero order by codigo_genero desc),0)+1),'" + nombGenero + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editaGenero()
    {
        sql = "UPDATE PLD_Genero " +
                " SET nombre_genero ='" + nombGenero + "' where codigo_genero='" + codigoGenero + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}