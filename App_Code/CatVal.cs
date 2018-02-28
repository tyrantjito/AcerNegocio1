using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CatVal
/// </summary>
public class CatVal
{
    Ejecuciones ejecuta = new Ejecuciones();
    public int codigo { get; set; }
    public string descripcion { get; set; }
    public string estatus { get; set; }

    public object[] retorno;
    private string sql;
    public CatVal()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
  
    public void eliminaValidaciones()
    {
        sql = "delete from an_validaciones_credito where Id_validacion='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void obtieneValidacionEdit()
    {
        sql = "select * from an_validaciones_credito where Id_validacion='" + codigo + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void agregarValidacion()
    {
        sql = "insert into an_validaciones_credito values ((select isnull ((select top 1 Id_validacion from an_validaciones_credito order by Id_validacion desc),0)+1),'" + descripcion + "','" + estatus + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editaValidacion()
    {
        sql = "UPDATE an_validaciones_credito " +
                " SET  Descripcion='" + descripcion + "', Estatus='" + estatus + "' where Id_validacion='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}