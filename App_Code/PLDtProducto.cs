using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDtProducto
/// </summary>
public class PLDtProducto
{
    public PLDtProducto()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string _ID { set; get; }
    public int _Clasificacion { set; get; }
    public string _Nombre { set; get; }


    Ejecuciones ejecuta = new Ejecuciones();
    public object[] retorno;
    private string sql;

    public void obtienetProducto()
    {
        sql = "select * from PLD_Tipo_Producto where tpro_id='" + _ID + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminatProducto()
    {
        sql = "delete from PLD_Tipo_Producto where tpro_id='" + _ID + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregartProducto()
    {
        sql = "insert into PLD_Tipo_Producto (tpro_id, id_clasificacion, nombre_tproducto)" +
              " values ((select isnull((select top 1 tpro_id from PLD_Tipo_Producto order by cast (tpro_id as int) desc),0)+1),'" + _Clasificacion + "','" + _Nombre + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void editatProducto()
    {
        sql = "UPDATE PLD_Tipo_Producto SET id_clasificacion ='" + _Clasificacion + "', nombre_tproducto='" + _Nombre + "' where tpro_id='" + _ID + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

}