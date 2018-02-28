using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDtReferencia
/// </summary>
public class PLDtReferencia
{
    public PLDtReferencia()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    Ejecuciones ejecuta = new Ejecuciones();
    public string codigotReferencia { get; set; }
    public string nombtReferencia { get; set; }


    public object[] retorno;
    private string sql;

    public void obtienetReferencia()
    {
        sql = "select * from PLD_Tipo_Referencia where codigo_treferencia='" + codigotReferencia + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaPldtReferencia()
    {
        sql = "delete from PLD_Tipo_Referencia where codigo_treferencia='" + codigotReferencia + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarpldtReferencia()
    {
        sql = "insert into PLD_Tipo_Referencia (codigo_treferencia, nombre_treferencia)" +
              " values ((select isnull((select top 1 codigo_treferencia from PLD_Tipo_Referencia order by codigo_treferencia desc),0)+1),'" + nombtReferencia + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editapldtReferencia()
    {
        sql = "UPDATE PLD_Tipo_Referencia " +
                " SET nombre_treferencia ='" + nombtReferencia + "' where codigo_treferencia='" + codigotReferencia + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}
