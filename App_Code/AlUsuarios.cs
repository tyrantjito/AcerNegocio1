using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de AlUsuarios
/// </summary>
public class AlUsuarios
{
    public string nombre { get; set; }
    public string nick { get; set; }
    public string contraseña { get; set; }
    public string npuesto { get; set; }
    public int id { get; set; }

    public int empresa { get; set; }
    public int sucursal { get; set; }

    private string sql;
    public object[] retorno;
    public int puestirri { get; set; }

    Ejecuciones ejecuta = new Ejecuciones();



    public AlUsuarios()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    //agregar a un usuario 
    public void agregarPuesto()
    {
        sql = "insert into usuarios  values ((select isnull((select top 1 id_usuario from Usuarios order by id_usuario desc),0)+1),'"+nick+"','"+contraseña+"','"+nombre+"','A','A',"+puestirri+",'"+npuesto+"')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void agregarAnalista()
    {
        sql = "insert into AN_Analista values (" + empresa + "," + sucursal + ",(select isnull((select top 1 id_analista from AN_Analista where id_empresa="+empresa+" and id_sucursal="+sucursal+" order by id_analista desc),0)+1),0,0,'NULL','"+nombre+"','NULL')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }


    public void obtienePuestoEdit()
    {
        sql = "select * from usuarios  where id_usuario=" + id ;
        retorno = ejecuta.dataSet(sql);
    }

    public void editaPuesto()
    {
        sql = "UPDATE usuarios  " +
                " SET  nick='" + nick + "', contrasena='" + contraseña + "',nombre_usuario='"+nombre+"',puesto="+puestirri+",n_puesto='"+npuesto+"' where nick='"+nick+"' " ;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void eliminaPuesto()
    {
        sql = "delete from usuarios  where id_usuario=" + id ;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

}