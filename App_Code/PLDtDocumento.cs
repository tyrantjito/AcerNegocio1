using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDtDocumento
/// </summary>
public class PLDtDocumento
{
    Ejecuciones ejecuta = new Ejecuciones();
    public string codigotDocumento { get; set; }
    public string nombtDocumento { get; set; }


    public object[] retorno;
    private string sql;

    public PLDtDocumento()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public void obtienetDocumento()
    {
        sql = "select * from PLD_Tipo_Documento where codigo_tdocumento='" + codigotDocumento + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaPldtDocumento()
    {
        sql = "delete from PLD_Tipo_Documento where codigo_tdocumento='" + codigotDocumento + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarpldtDocumento()
    {
        sql = "insert into PLD_Tipo_Documento (codigo_tdocumento, nombre_tdocumento)" +
              " values ((select isnull((select top 1 codigo_tdocumento from PLD_Tipo_Documento order by codigo_tdocumento desc),0)+1),'" + nombtDocumento + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editapldtDocumento()
    {
        sql = "UPDATE PLD_Tipo_Documento " +
                " SET nombre_tdocumento ='" + nombtDocumento + "' where codigo_tdocumento='" + codigotDocumento + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}