using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CatPoliCre
/// </summary>
public class CatPoliCre
{
    Ejecuciones ejecuta = new Ejecuciones();
    public string codigo { get; set; }
    public string descripcion { get; set; }
    public string estatus { get; set; } 

    public object[] retorno;
    private string sql;
    public CatPoliCre()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public void eliminaPolitica()
    {
        sql = "delete from AN_Politicas_Credito where Id_politica='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void obtienePoliticasEdit()
    {
        sql = "select * from AN_Politicas_Credito where Id_politica='" + codigo + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void agregarPolitica()
    {
        sql = "insert into AN_Politicas_Credito values ((select isnull ((select top 1 Id_politica from AN_Politicas_Credito order by Id_politica desc),0)+1),'" + descripcion + "','" + estatus + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editaLineaFondeo()
    {
        sql = "UPDATE AN_Politicas_Credito " +
                " SET  Descripcion='" + descripcion + "', Estatus='" + estatus + "' where Id_politica='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}