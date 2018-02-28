using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDTransaccion
/// </summary>
public class PLDTransaccion
{
    public PLDTransaccion()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public int _ID { set; get; }
    public string _Codigo { set; get; }
    public string _Nombre { set; get; }

    Ejecuciones ejecuta = new Ejecuciones();
    public object[] retorno;
    private string sql;
    public void obtiene_Transaccion()
    {
        sql = "select * from PLD_Tipo_Transaccion where codigo_ttransaccion='" + _Codigo + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void elimina_Transaccion()
    {
        sql = "delete from PLD_Tipo_Transaccion where codigo_ttransaccion='" + _Codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregar_Transaccion()
    {
        sql = "insert into PLD_Tipo_Transaccion (codigo_ttransaccion, nombre_ttransaccion, tope_id) " +
            "values ((select isnull((select top 1 codigo_ttransaccion from PLD_Tipo_Transaccion order by cast(codigo_ttransaccion as int)desc),0)+1),'" + _Nombre + "','"+_ID+"')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void edita_Transaccion()
    {
        sql = "UPDATE PLD_Tipo_Transaccion SET nombre_ttransaccion='" + _Nombre + "',tope_id='"+_ID+"' where codigo_ttransaccion='" + _Codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}