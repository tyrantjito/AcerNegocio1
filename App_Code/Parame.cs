using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Parame
/// </summary>
public class Parame
{
    Ejecuciones ejecuta = new Ejecuciones();
    public string codigo { get; set; }
    public int sucursal { get; set; }
    public int empresa { get; set; }
    public string nombreCorto { get; set; }
    public string nombreCompleto { get; set; }
    public string direccionEmp { get; set; }
    public string correoEmp { get; set; }
    public string teleEmp { get; set; }
    public string rfcEmp { get; set; }
    public string pagWeb { get; set; }
    public string represen { get; set; }



    public object[] retorno;
    private string sql;

	public Parame()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    public void obtieneParaEdit()
    {
        sql = "select*from an_parametros where id_empresa="+empresa+" and id_sucursal="+sucursal;
        retorno = ejecuta.dataSet(sql);
    }

    public void editaParamentro()
    {

        sql = "update an_parametros set nombreCortoEmp='"+nombreCorto+"', empresa='"+nombreCompleto+"', direccion='"+direccionEmp+"' , correo= '"+correoEmp+"', telefono='"+teleEmp+"' , rfc='"+rfcEmp+"' , pagweb='"+pagWeb+"' , representante='"+represen+"'  where id_empresa= '"+empresa+"' and id_sucursal="+sucursal;
        retorno = ejecuta.insertUpdateDelete(sql);
        
    }
}