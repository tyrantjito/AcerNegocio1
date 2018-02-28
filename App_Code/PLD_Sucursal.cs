using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLD_Sucursal
/// </summary>
public class PLD_Sucursal
{
    public PLD_Sucursal()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public int _Regional { set; get; }
    public int _Escala { set; get; }
    public string _CodigoSucursal { set; get; }
    public string _NombreSucursal { set; get; }

    Ejecuciones ejecuta = new Ejecuciones();
    public object[] retorno;
    private string sql;
    public void obtiene_Sucursal()
    {
        sql = "select * from PLD_Sucursal where codigo_sucursal='" + _CodigoSucursal + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void elimina_Sucursal()
    {
        sql = "delete from PLD_Sucursal where codigo_sucursal='" + _CodigoSucursal + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregar_Sucursal()
    {
        sql = "insert into PLD_Sucursal (codigo_sucursal, nombre_sucursal, reg_id, id_escala)" +
              " values ((select isnull((select top 1 codigo_sucursal from PLD_Sucursal order by cast (codigo_sucursal as int) desc),0)+1),'" + _NombreSucursal +"','"+_Regional + "','" +_Escala+ "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void edita_Sucursal()
    {
        sql = "UPDATE PLD_Sucursal SET nombre_sucursal='" + _NombreSucursal + "',reg_id='" +_Regional + "',id_escala='" +_Escala+ "' where codigo_sucursal='" + _CodigoSucursal + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}