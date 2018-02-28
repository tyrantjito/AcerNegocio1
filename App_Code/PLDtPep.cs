using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDtPep
/// </summary>
public class PLDtPep
{
    public PLDtPep()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public int _Escala { set; get; }
    public string _CodiPep { set; get; }
    public string _NombPep { set; get; }

    Ejecuciones ejecuta = new Ejecuciones();
    public object[] retorno;
    private string sql;
    public void obtiene_Pep()
    {
        sql = "select * from PLD_Tipo_Pep where codigo_tpep='" + _CodiPep + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void elimina_Pep()
    {
        sql = "delete from PLD_Tipo_Pep where codigo_tpep='" + _CodiPep + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregar_Pep()
    {
        sql = "insert into PLD_Tipo_Pep (codigo_tpep, nombre_tpep, id_escala) values ('"+_CodiPep+"','" + _NombPep + "','" +_Escala + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void edita_Pep()
    {
        sql = "UPDATE PLD_Tipo_Pep SET nombre_tpep='" + _NombPep + "',id_escala='" + _Escala + "' where codigo_tpep='" + _CodiPep + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}