using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Credit
/// </summary>
public class Credit
{
    Ejecuciones ejecuta = new Ejecuciones();
    private string sql;
    public object[] retorno;

    public int id_empresa { get; set; }
    public int npago { get; set; }
    public int id_solicitud_credito { get; set; }
    public int id_sucursal { get; set; }
    public int id_cliente { get; set; }
    public string grupo_productivo { get; set; }
    public string fecha_auto { get; set; }
    public string fecha_pago { get; set; }
    public string fecha_aplicacion { get; set; }
    public string nombre { get; set; }
    public int numero_grupo { get; set; }
    public decimal monto_autorizado { get; set; }
    public decimal pagosem { get; set; }
    public int plazo { get; set; }
    public int  ciclo { get; set; }
   

    public Credit() 
    {
        retorno = new object[] { false, "" };
    }
     
    public void obtieneInfoSolicitud()
    {
        sql="select * from an_solicitud_credito_encabezado where id_empresa="+id_empresa+" and id_sucursal="+id_sucursal+" and id_solicitud_credito="+id_solicitud_credito;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtieneDetalle()
    {
        sql = "select d.id_cliente,d.nombre_cliente,d.credito_autorizado,e.fecha_entrega_credito from AN_Solicitud_Credito_Detalle d left join AN_Solicitud_Credito_Encabezado e on d.id_solicitud_credito= e.id_solicitud_credito where d.id_solicitud_credito="+id_solicitud_credito;
        retorno = ejecuta.dataSet(sql);
    }
    public void generaCredito()
    {
        sql = "UPDATE an_creditos  " +
                " SET  estatus='SOL' where id_solicitud_credito=" + id_solicitud_credito;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void generaCreditoSol()
    {
        sql = "insert into an_creditos values (" + id_empresa + "," + id_sucursal + ",(select isnull((select top 1 id_solicitud_credito from an_creditos where id_empresa=" + id_empresa + " and id_sucursal=" + id_sucursal + " order by id_solicitud_credito desc),0)+1),'" + grupo_productivo + "'," + numero_grupo + "," + monto_autorizado + "," + plazo + "," + ciclo + ",'" + fecha_auto + "',0,'PRE',0,0,0)";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void validarCredito()
    {
        sql = "select * from an_creditos where id_empresa="+id_empresa+" and id_sucursal="+id_sucursal+" and id_solicitud_credito="+id_solicitud_credito+", and ciclo="+ciclo;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void recupéraCargo()
    {
        sql = "select cargo  from AN_Acta_IntegracionDetalle where id_empresa=" + id_empresa + " and id_sucursal=" + id_sucursal + " and id_cliente=" + id_cliente;
        retorno = ejecuta.dataSet(sql);
    }
    public void recupéraTelefono()
    {
        sql = "select telefono from AN_Solicitud_Consulta_Buro where id_empresa="+id_empresa+" and id_sucursal="+id_sucursal+" and id_cliente="+id_cliente;
        retorno = ejecuta.dataSet(sql);
    }

    public void recupéraCiclo()
    {
        sql = "select ciclo from an_solicitud_credito_encabezado where id_empresa=" + id_empresa + " and id_sucursal=" + id_sucursal + " and id_solicitud_credito=" + id_solicitud_credito;
        retorno = ejecuta.dataSet(sql);
    }

    public void insertarClienteCredito()
    {
        sql = "insert into AN_Operacion_Credito values("+id_empresa+","+id_sucursal+",(select isnull((select top 1 id_operacion from AN_Operacion_Credito  order by id_operacion desc),0)+1),"+id_cliente+","+id_solicitud_credito+","+monto_autorizado+","+npago+",'"+fecha_pago+"','"+fecha_aplicacion+"','',0,0,0,0,0,"+pagosem+",0,0)";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}