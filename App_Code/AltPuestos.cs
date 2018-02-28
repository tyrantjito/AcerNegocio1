using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de AltProcesos
/// </summary>
public class AltPuestos
{
    Ejecuciones ejecuta = new Ejecuciones();
    public string codigo { get; set; }
    public string Descripcion { get; set; }

    public object[] retorno;
    private string sql;
    public AltPuestos() 
    { 
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public void agregarPuesto()
    {
        sql = "insert into an_puestos  values ('" + codigo + "','" + Descripcion + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }


    public void obtienePuestoEdit()
    {
        sql = "select * from an_puestos  where id_puesto='" + codigo + "'";
        retorno = ejecuta.dataSet(sql);
    }

    public void editaPuesto()
    {
        sql = "UPDATE an_puestos  " +
                " SET  id_puesto='" + codigo + "', descripcion='" + Descripcion + "' where id_puesto='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void eliminaPuesto()
    {
        sql = "delete from an_puestos  where id_puesto='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

}