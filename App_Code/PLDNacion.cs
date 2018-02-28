using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDNacionalidad
/// </summary>
public class PLDNacion
{
    public PLDNacion()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    Ejecuciones ejecuta = new Ejecuciones();

    public int _Escala {set; get;}
    public string _CodigoNacion { set; get; }
    public string _Nombre { set; get; }
    public string _Nacionalidad { set; get; }

    public object[] retorno;
    private string sql;

    public void obtieneNacion()
    {
        sql = "select * from PLD_Nacionalidad where codigo_nacionalidad='" + _CodigoNacion + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaNacion()
    {
        sql = "delete from PLD_Nacionalidad where codigo_nacionalidad='" + _CodigoNacion + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarNacion()
    {
        sql = "insert into PLD_Nacionalidad (codigo_nacionalidad, id_escala, nacionalidad, nombre_pais)" +
              " values ('"+_CodigoNacion+"','" + _Escala + "','" + _Nacionalidad + "','" + _Nombre+ "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void editaNacion()
    {
        sql = "UPDATE PLD_Nacionalidad SET id_escala ='" + _Escala + "', nacionalidad='" + _Nacionalidad+ "', nombre_pais='" + _Nombre+ "' where codigo_nacionalidad='" + _CodigoNacion + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}