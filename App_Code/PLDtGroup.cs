using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLDtGroup
/// </summary>
public class PLDtGroup
{
    Ejecuciones ejecuta = new Ejecuciones();
    public string codigo { get; set; }
    public string nombtgrupo { get; set; }


    public object[] retorno;
    private string sql;
    public PLDtGroup()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public void obtienetGgrupo()
    {
        sql = "select * from PLD_Tipo_Grupo where codigo_tgrupo='" + codigo + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaPldtGrupo()
    {
        sql = "delete from PLD_Tipo_Grupo where codigo_tgrupo='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarpldtgrupo()
    {
        sql = "insert into PLD_Tipo_Grupo (codigo_tgrupo, nombre_tgrupo)" +
              " values ((select isnull((select top 1 codigo_tgrupo from PLD_tipo_grupo order by codigo_tgrupo desc),0)+1),'" + nombtgrupo + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editapldtgrupo()
    {
        sql = "UPDATE PLD_Tipo_Grupo " +
                " SET nombre_tgrupo ='" + nombtgrupo + "' where codigo_tgrupo='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}