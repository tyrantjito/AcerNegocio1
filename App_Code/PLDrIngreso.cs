using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDrIngreso
/// </summary>
public class PLDrIngreso
{
    public PLDrIngreso()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    Ejecuciones ejecuta = new Ejecuciones();
    public int Id_Escala { get; set; }
    public string Codigo_Ingreso { get; set; }
    public decimal _Inicial { set; get; }
    public decimal _Final { set; get; }
    public char _tipo { set; get; }


    public object[] retorno;
    private string sql;

    public void obtieneTiempo()
    {
        sql = "select * from PLD_Rango_Ingreso where codigo_ringreso='" + Codigo_Ingreso + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaTiempo()
    {
        sql = "delete from PLD_Rango_Ingreso where codigo_ringreso='" + Codigo_Ingreso + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarTiempo()
    {
        sql = "insert into PLD_Rango_Ingreso (codigo_ringreso, id_escala, inicial, final, tipo)" +
              " values ((select isnull((select top 1 codigo_ringreso from PLD_Rango_Ingreso order by cast (codigo_ringreso as integer) desc),0)+1),'" + Id_Escala + "','" + _Inicial + "','" + _Final + "','" + _tipo + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void editaTiempo()
    {
        sql = "UPDATE PLD_Rango_Ingreso SET id_escala ='" + Id_Escala + "', inicial='" + _Inicial + "',final='" + _Final + "', tipo='" + _tipo + "' where codigo_ringreso='" + Codigo_Ingreso + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}