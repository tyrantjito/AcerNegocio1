using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLD_Producto
/// </summary>
public class PLD_Producto
{
    public PLD_Producto()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public int _ID { set; get; }
    public int _Escala { set; get; }
    public string _NomProducto { set; get; }
    public string _CodProducto { set; get; }

    Ejecuciones ejecuta = new Ejecuciones();
    public object[] retorno;
    private string sql;

    public void obtiene_Producto()
    {
        sql = "select * from PLD_Producto where codigo_producto='" + _CodProducto + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void elimina_Producto()
    {
        sql = "delete from PLD_Producto where codigo_producto='" + _CodProducto + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregar_Producto()
    {
        sql = "insert into PLD_Producto (codigo_producto, id_escala, tpro_id,nombre_producto)" +
              " values ((select isnull((select top 1 codigo_producto from PLD_Producto order by cast (codigo_producto as int) desc),0)+1),'" + _Escala + "','" + _ID+ "','"+_NomProducto+"')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void edita_Producto()
    {
        sql = "UPDATE PLD_Producto SET id_escala ='" + _Escala + "', nombre_producto='" + _NomProducto + "', tpro_id='"+_ID+"' where codigo_producto='" + _CodProducto + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

}