using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDpCuenta
/// </summary>
public class PLDpCuenta
{
    public PLDpCuenta()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string _CodigoCuenta { set; get; }
    public string _NombreCuenta { set; get; }

    Ejecuciones ejecuta = new Ejecuciones();
    public object[] retorno;
    private string sql;
    public void obtiene_Cuenta()
    {
        sql = "select * from PLD_Proposito_Cuenta where codigo_pproducto='" + _CodigoCuenta + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void elimina_Cuenta()
    {
        sql = "delete from PLD_Proposito_Cuenta where codigo_pproducto='" + _CodigoCuenta + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregar_Cuenta()
    {
        sql = "insert into PLD_Proposito_Cuenta (codigo_pproducto, nombre_pproducto)" +
              " values ((select isnull((select top 1 codigo_pproducto from PLD_Proposito_Cuenta order by cast (codigo_pproducto as int) desc),0)+1),'" + _NombreCuenta + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void edita_Cuenta()
    {
        sql = "UPDATE PLD_Proposito_Cuenta SET nombre_pproducto='" + _NombreCuenta + "' where codigo_pproducto='" + _CodigoCuenta + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}