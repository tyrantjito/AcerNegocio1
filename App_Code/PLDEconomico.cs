using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDEconomico
/// </summary>
public class PLDEconomico
{
    public PLDEconomico()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    Ejecuciones ejecuta = new Ejecuciones();

    public int _Escala { set; get; }
    public string _CodigoEconomico { set; get; }
    public string _Nombre { set; get; }

    public object[] retorno;
    private string sql;

    public void obtieneEconomico()
    {
        sql = "select * from PLD_Actividad_Economica where codigo_actividad='" + _CodigoEconomico + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaEconomico()
    {
        sql = "delete from PLD_Actividad_Economica where codigo_actividad='" + _CodigoEconomico + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarEconomico()
    {
        sql = "insert into PLD_Actividad_Economica (codigo_actividad, id_escala, nombre_actividad)" +
              " values ((select isnull((select top 1 codigo_actividad from PLD_Actividad_Economica order by cast (codigo_actividad as integer) desc),0)+1),'" + _Escala + "','" + _Nombre + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void editaEconomico()
    {
        sql = "UPDATE PLD_Actividad_Economica SET id_escala ='" + _Escala + "', nombre_actividad='" + _Nombre + "' where codigo_actividad='" + _CodigoEconomico + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}