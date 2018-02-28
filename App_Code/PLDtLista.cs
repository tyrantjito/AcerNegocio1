using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDtLista
/// </summary>
public class PLDtLista
{
    public PLDtLista()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public int _Escala { set; get; }
    public string _Codigo { set; get; }
    public string _Nombre { set; get; }

    Ejecuciones ejecuta = new Ejecuciones();
    public object[] retorno;
    private string sql;
    public void obtiene_Lista()
    {
        sql = "select * from PLD_Tipo_Lista where codigo_tlista='" + _Codigo + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void elimina_Lista()
    {
        sql = "delete from PLD_Tipo_Lista where codigo_tlista='" + _Codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregar_Lista()
    {
        sql = "insert into PLD_Tipo_Lista (codigo_tlista, nombre_tlista, id_escala) "+ 
            "values ((select isnull((select top 1 codigo_tlista from PLD_Tipo_Lista order by cast(codigo_tlista as int)desc),0)+1),'" + _Nombre + "','" + _Escala + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void edita_Lista()
    {
        sql = "UPDATE PLD_Tipo_Lista SET nombre_tlista='" + _Nombre + "',id_escala='" + _Escala + "' where codigo_tlista='" + _Codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}