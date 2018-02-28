using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDtCanal
/// </summary>
public class PLDtCanal
{
    public PLDtCanal()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    Ejecuciones ejecuta = new Ejecuciones();
    public object[] retorno;
    private string sql;

    public string _CodigoCanal { set; get; }
    public int _Escala { set; get; }
    public string _Nombre { set; get; }

    public void obtieneCanal()
    {
        sql = "select * from PLD_Canal where codigo_canal='" + _CodigoCanal + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaCanal()
    {
        sql = "delete from PLD_Canal where codigo_canal='" + _CodigoCanal + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarCanal()
    {
        sql = "insert into PLD_Canal (codigo_canal, id_escala, nombre_canal)" +
              " values ((select isnull((select top 1 codigo_canal from PLD_Canal order by cast (codigo_canal as integer) desc),0)+1),'" + _Escala + "','" + _Nombre + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void editaCanal()
    {
        sql = "UPDATE PLD_Canal SET id_escala ='" + _Escala + "', nombre_canal='" + _Nombre + "' where codigo_canal='" + _CodigoCanal + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

}