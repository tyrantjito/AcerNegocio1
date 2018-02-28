using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CatSuc
/// </summary>
public class CatSuc
{
    Ejecuciones ejecuta = new Ejecuciones();
    public string codigo { get; set; }
    public string nomSuc { get; set; }
    public string identificador { get; set; }
    public string regional { get; set; }

    public object[] retorno;
    private string sql;
    public CatSuc()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public void obtieneservEdit()
    {
        sql = "select * from AN_Sucursales where id_sucursal='" + codigo + "'";
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaSucursal()
    {
        sql = "delete from AN_Sucursales where id_sucursal='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarServicio()
    {
        sql = "insert into AN_Sucursales (id_sucursal,nombre_sucursal, identificador,regional)" +
              " values ((select isnull((select top 1 id_sucursal from AN_Sucursales order by id_sucursal desc),0)+1),'" + nomSuc + "','" + identificador + "','" + regional + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editaSucursal()
    {
        sql = "UPDATE AN_Sucursales " +
                " SET   nombre_sucursal='" + nomSuc + "', identificador='" + identificador + "',regional='" + regional + "' where id_sucursal='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}