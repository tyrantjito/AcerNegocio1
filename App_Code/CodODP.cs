using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CodODP
/// </summary>
public class CodODP
{
    Ejecuciones ejecuta = new Ejecuciones();

    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int idcliente { get; set; }
    public int idSolicitudEdita { get; set; }
    public int grupo { get; set; }
    public object[] retorno { get; set; }
    private string sql;
	public CodODP()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    public void recuperagrupo()
    {
        sql = "select id_grupo from an_creditos where id_solicitud_credito=" + grupo;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtieneInfoDetalle()
    {
        sql = "select d.id_cliente, d.nombre_cliente, d.ciclo, e.id_banco, b.nombre, b.convenio, b.referencia, d.credito_autorizado, d.credito_autorizado from AN_Solicitud_Credito_Detalle d inner join AN_Solicitud_Credito_Encabezado e on d.id_empresa=e.id_empresa and d.id_solicitud_credito=e.id_solicitud_credito and d.id_sucursal=e.id_sucursal inner join AN_catBancos b on e.id_banco=b.clave where e.id_empresa=" + empresa + " and d.id_solicitud_credito=" + grupo + " and e.id_sucursal=" + sucursal + " and d.id_cliente= " + idcliente;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtieneInfoDeta()
    {
        sql = "select d.credito_autorizado,e.fecha_solicitud,e.fecha_entrega_credito from an_solicitud_credito_detalle d inner join AN_Solicitud_Credito_Encabezado e on d.id_solicitud_credito = e.id_solicitud_credito where d.id_empresa=" + empresa + " and d.id_sucursal=" + sucursal + " and d.id_solicitud_credito=" + idSolicitudEdita + " and d.id_cliente=" + idcliente;
        retorno = ejecuta.dataSet(sql);
    }
}