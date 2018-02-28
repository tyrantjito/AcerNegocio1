using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDrTiempo
/// </summary>
public class PLDrTiempo
{
    public PLDrTiempo()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    Ejecuciones ejecuta = new Ejecuciones();
    public int Id_Escala { get; set; }
    public string Codigo_Tiempo { get; set; }
    public int _Inicial { set; get; }
    public int _Final { set; get; }
    public char _tipo { set; get; }


    public object[] retorno;
    private string sql;

    public void obtieneTiempo()
    {
        sql = "select * from PLD_Rango_Tiempo where codigo_rtiempo='" + Codigo_Tiempo + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaTiempo()
    {
        sql = "delete from PLD_Rango_Tiempo where codigo_rtiempo='" + Codigo_Tiempo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarTiempo()
    {
        sql = "insert into PLD_Rango_Tiempo (codigo_rtiempo, id_escala, inicial, final, tipo)" +
              " values ((select isnull((select top 1 codigo_rtiempo from PLD_Rango_Tiempo order by cast (codigo_rtiempo as integer) desc),0)+1),'" + Id_Escala + "','"+_Inicial+ "','"+_Final+ "','"+_tipo+"')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void editaTiempo()
    {
        sql = "UPDATE PLD_Rango_Tiempo SET id_escala ='" + Id_Escala + "', inicial='"+_Inicial+"',final='"+_Final+"', tipo='"+_tipo+"' where codigo_rtiempo='" + Codigo_Tiempo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}