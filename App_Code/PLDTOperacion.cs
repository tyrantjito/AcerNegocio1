using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDTOperacion
/// </summary>
public class PLDTOperacion
{
    public PLDTOperacion()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string _Codigo { set; get; }
    public string _Nombre { set; get; }

    Ejecuciones ejecuta = new Ejecuciones();
    public object[] retorno;
    private string sql;
    public void obtiene_Operacion()
    {
        sql = "select * from PLD_Tipo_Operacion where codigo_toperacion='" + _Codigo + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void elimina_Operacion()
    {
        sql = "delete from PLD_Tipo_Operacion where codigo_toperacion='" + _Codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregar_Operacion()
    {
        sql = "insert into PLD_Tipo_Operacion (codigo_toperacion, nombre_toperacion) " +
            "values ('" + _Codigo + "','" + _Nombre+ "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void edita_Operacion()
    {
        sql = "UPDATE PLD_Tipo_Operacion SET nombre_toperacion='" + _Nombre + "' where codigo_toperacion='" + _Codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}