using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PLD_Regional
/// </summary>
public class PLD_Regional
{
    public PLD_Regional()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public string _IDRegional { set; get; }
    public string _NombreRegional { set; get; }

    Ejecuciones ejecuta = new Ejecuciones();
    public object[] retorno;
    private string sql;
    public void obtiene_Regional()
    {
        sql = "select * from PLD_Regional where reg_id='" + _IDRegional + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void elimina_Regional()
    {
        sql = "delete from PLD_Regional where reg_id='" + _IDRegional + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregar_Regional()
    {
        sql = "insert into PLD_Regional (reg_id, nombre_regional)" +
              " values ((select isnull((select top 1 reg_id from PLD_Regional order by cast (reg_id as int) desc),0)+1),'" + _NombreRegional + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void edita_Regional()
    {
        sql = "UPDATE PLD_Regional SET nombre_regional='" + _NombreRegional + "' where reg_id='" + _IDRegional + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}