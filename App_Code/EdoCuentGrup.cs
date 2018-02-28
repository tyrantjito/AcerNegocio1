using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de EdoCuentGrup
/// </summary>
public class EdoCuentGrup
{
    Ejecuciones ejecuta = new Ejecuciones();
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int idcliente { get; set; }
    public int grupo { get; set; }
    public int cpost { get; set; }
    public object[] retorno { get; set; }

    private string sql;
	public EdoCuentGrup()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    public void obtenEmpresa()
    {
        sql = "select*from an_parametros where id_empresa=" + empresa + " and id_sucursal=" + sucursal;
        retorno = ejecuta.dataSet(sql);
    }


    public void obtenEncabezados()
    {
        sql = "select sce.grupo_productivo, ai.numero_direccion_reunion, ai.calle_direccion_reunion, ai.colonia_direccion_reunion, ai.id_sucursal, ai.municipio_direccion_reunion, ai.estado_direccion_reunion, ai.id_asesor from An_acta_integracion ai inner join AN_Solicitud_Credito_Encabezado sce on ai.id_empresa=sce.id_empresa and ai.id_sucursal=sce.id_sucursal and ai.id_grupo=sce.id_solicitud_credito where ai.id_grupo="+grupo+" and ai.id_empresa="+empresa+" and ai.id_sucursal="+sucursal;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtenerCP()
    {
        sql = "select top 1 d_asenta from AN_CodigoPostal where d_codigo=" + cpost;
        retorno = ejecuta.dataSet(sql);
    }
}