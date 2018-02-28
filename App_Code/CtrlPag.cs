using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CtrlPag
/// </summary>
public class CtrlPag
{
    Ejecuciones ejecuta = new Ejecuciones();
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int credito { get; set; }
    public int pago { get; set; }
    public int idcliente { get; set; }
    public int saldo_actual { get; set; }

    public decimal dev { get; set; }
    public decimal ap { get; set; }
    public decimal devt { get; set; }
    public decimal apt { get; set; }

    public object[] retorno { get; set; }
    private string sql;
    public CtrlPag()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public void obtieneInfoEnca()
    {
        sql = "select plazorc from an_solicitud_credito_encabezado where id_solicitud_credito=" + credito;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtenerelpagol()
    {
        sql = "select d.id_cliente,d.credito_autorizado,e.plazorc,e.tasarc from AN_Solicitud_Credito_Detalle d inner join AN_Solicitud_Credito_Encabezado e on d.id_solicitud_credito=e.id_solicitud_credito where d.id_empresa="+empresa+" and d.id_sucursal="+sucursal+" and d.id_solicitud_credito="+credito;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtenerSaldoActual()
    {
        sql = "select d.id_cliente,d.nombre_cliente,d.credito_autorizado,e.plazoRC,e.tasaRC from an_solicitud_credito_detalle d inner join an_solicitud_credito_encabezado e on d.id_solicitud_credito = e.id_solicitud_credito where d.id_empresa="+empresa+" and d.id_sucursal="+sucursal+" and d.id_solicitud_credito="+credito;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtenerSaldos()
    {
        sql = "select id_cliente,saldo_actual,monto_pago,monto_ahorro,ap,dev from an_operacion_credito where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_grupo="+credito+" and no_pago="+pago;
        retorno = ejecuta.dataSet(sql);
    }

    public void actualizaSaldo()
    {
        sql = "UPDATE an_operacion_credito  " +
                " SET  saldo_actual="+ saldo_actual+ " where id_empresa="+empresa+" and id_sucursal="+sucursal+" and no_pago=1 and id_cliente=" + idcliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void actualizaSaldo2()
    {
        sql = "UPDATE an_operacion_credito  " +
                " SET  saldo_actual=" + saldo_actual + " where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and no_pago="+pago+" and id_cliente=" + idcliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void actualizaAPDEV()
    {
        sql = "UPDATE an_operacion_credito  " +
                " SET  ap="+ap+",apt="+apt+" where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and no_pago=" + pago + " and id_cliente=" + idcliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void actualizaAPt()
    {
        sql = "UPDATE an_operacion_credito  " +
                " SET apt=" + apt + " where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and no_pago=" + pago + " and id_cliente=" + idcliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void actualizaAPDEVsig()
    {
        sql = "UPDATE an_operacion_credito  " +
                " SET  ap=" + ap + ",apt=" + apt + "where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and no_pago=" + pago + " and id_cliente=" + idcliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void obtenerSaldos2()
    {
        sql = "UPDATE an_operacion_credito  " +
                 " SET  referencia_pago='Abono Referencia Pago' where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and no_pago=" + pago + " and id_cliente=" + idcliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void obtenerdev()
    {
        sql = "select id_cliente,devT from an_operacion_credito where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_grupo=" + credito + " and id_cliente="+idcliente+" and no_pago=" + pago;
        retorno = ejecuta.dataSet(sql);
    }

    public void eliminadev()
    {
        sql = "UPDATE an_operacion_credito  " +
                " SET  devt=" + devt + " where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_grupo=" + credito + " and id_cliente=" + idcliente + " and no_pago=" + pago;
        retorno = ejecuta.dataSet(sql);
    }

    public void ActualizaproxapAPT()
    {
        sql = "UPDATE an_operacion_credito  " +
                " SET  ap=" + ap + ",apt=" + apt + "where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and no_pago=" + pago + " and id_cliente=" + idcliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void ActualizaGrid()
    {
        sql = "UPDATE an_operacion_credito SET monto_Pago=@monto_Pago,monto_Ahorro=@monto_Ahorro,fecha_aplicacion=@fecha_aplicacion,ap=@ap,dev=@dev,apT=@ap,devT=@dev WHERE id_empresa =" + empresa + " AND id_sucursal = " + sucursal + " and no_pago=" + pago + " and id_grupo=" + credito + " and id_cliente=" + idcliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}