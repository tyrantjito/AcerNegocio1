using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de EdoCuent
/// </summary>
public class EdoCuent
{

    Ejecuciones ejecuta = new Ejecuciones();
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int idcliente { get; set; }
    public int grupo { get; set; }

    public int pago { get; set; }
    public int CodPost { get; set; }
    public int periodoCliTo { get; set; }
    public object[] retorno { get; set; }

    private string sql;
	public EdoCuent()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    public void obtenEncabezados()
    {
        sql = "select cli.nombre_completo, g.grupo_productivo, fd.no_interior_cli, fd.calle_cli, fd.no_exterior_cli, fd.cp_cli, fd.id_sucursal, fd.del_mun_cli, fd.estado_neg from an_clientes cli inner join an_grupos g on cli.id_empresa=g.id_empresa and cli.id_sucursal=g.id_sucursal inner join AN_Ficha_Datos fd on g.id_empresa=fd.id_empresa and cli.id_cliente=fd.id_cliente and g.id_sucursal=fd.id_sucursal where g.id_grupo="+grupo+" and fd.id_cliente="+idcliente+" and g.id_empresa="+empresa+" and fd.id_sucursal="+sucursal;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtenGrid()
    {
        sql = "select*from AN_Operacion_Credito where id_cliente=" + idcliente + " and id_sucursal=" + sucursal + " and id_empresa=" + empresa + " and id_grupo="+grupo+" order by no_pago";
        retorno = ejecuta.dataSet(sql);
    }

    public void obtenEmpresa()
    {
        sql = "select*from an_parametros where id_empresa="+empresa+" and id_sucursal="+sucursal;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtenerDetaCredito()
    {
        sql = "select d.ciclo, d.id_solicitud_credito, e.fecha_entrega_credito, e.plazo, e.tasa*12 as tasaAnual, oc.pagosemanal, d.credito_autorizado, d.credito_autorizado * .10 as glrequerdida,  (e.tasa/100)*d.credito_autorizado as Interes, ((e.tasa/100)*d.credito_autorizado)+d.credito_autorizado as totalPagar, (select top 1 fecha_aplicacion from an_Operacion_Credito where id_empresa="+empresa+" and id_sucursal="+sucursal+" and id_grupo="+grupo+" and id_cliente=" + idcliente + " order by fecha_aplicacion desc ) as fecha_fin,(select count (fecha_aplicacion) from AN_Operacion_credito where monto_pago <> 0 and id_cliente=" + idcliente + " and id_grupo="+grupo+" and id_empresa="+empresa+" and id_sucursal="+sucursal+") as periodo, (select sum(dev) from AN_Operacion_Credito where id_empresa="+empresa+" and id_sucursal="+sucursal+" and id_cliente="+idcliente+" and id_grupo="+grupo+") as devTotCli from AN_Solicitud_Credito_Detalle d inner join AN_Solicitud_Credito_Encabezado e on d.id_empresa=e.id_empresa and d.id_sucursal=e.id_sucursal and d.id_solicitud_credito=e.id_solicitud_credito inner join AN_Operacion_Credito oc on e.id_empresa=oc.id_empresa and e.id_sucursal=oc.id_sucursal and e.id_grupo=oc.id_grupo and d.id_cliente=oc.id_cliente where d.id_cliente=" + idcliente + " and e.id_grupo= " + grupo + " and d.id_empresa=" + empresa + " and d.id_sucursal=" + sucursal;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtenCodigoPostal()
    {
        sql = "select top 1 d_asenta from AN_CodigoPostal where d_codigo=" + CodPost;
        retorno = ejecuta.dataSet(sql);

    }

    public void obtenanalista()
    {
        sql = " select c.id_grupo,c.grupo_productivo,u.nombre_usuario from AN_Creditos c left join Usuarios u on c.id_Analista = u.id_usuario where c.id_solicitud_credito="+grupo;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtenerDev()
    {
        sql = "select dev, saldo_actual from AN_Operacion_Credito where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + idcliente + " and no_pago=" + periodoCliTo;
        retorno = ejecuta.dataSet(sql);
    }
}