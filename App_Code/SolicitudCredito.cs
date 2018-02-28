using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de SolicitudCredito
/// </summary>
public class SolicitudCredito
{

    Ejecuciones ejecuta = new Ejecuciones(); 
    //Datos Encabezado
    public int empresa { get; set; } 
    public int sucursal { get; set; }

    public int grupo { get; set; }
    public int canal { set; get; }
    public int acta { get; set; }
    public int idSolicitudEdita { get; set; }
    public string fechasolicitud { get; set; }
    public string fechaEntrega { get; set; }
    public string grupoProductivo { get; set; }
    public int numeroGrupo { get; set; }
    public decimal montoCredito { get; set; }
    public int plazo { get; set; }
    public decimal tasa { get; set; }
    public decimal garantiaLiquida { get; set; }
    public decimal montoMaximo { get; set; }
    public decimal montoAutorizado {get; set;}
    public int plazoRC { get; set; }
    public decimal tasaRC { get; set; }
    public string formaPago { get; set; }
    public int ciclo { get; set; }
    public string observaciones { get; set; }

    //Datos Detalle
    public int idDetalleEdita { get; set; }
    public string cliente { get; set; }
    public int idCliente { get; set; }
    public int id_banco { get; set; }
    public int cicloIndiv { get; set; }
    public string cargo { get; set; }
    public string estatus { get; set; }
    public string causaDesercion { get; set; }
    public string giro { get; set; }
    public decimal ingreso { get; set; }
    public string destinoCredito {get; set;}
    public decimal creditoAnterior { get; set; }
    public decimal creditoSolicitado { get; set; }
    public decimal garantiaLiquidaIndiv { get; set; }
    public decimal creditoAutorizado { get; set; }
    public decimal telefono { get; set; }
    public string  amc { get; set; } //amc Autorizacion de mesa de control acepta un string que seria Autorizado o No autorizado 
    public string amcs { get; set; }  //amcs Autorizacion de mesa de control acepta un string que seria Autorizado o No autorizado 

    public object[] retorno { get; set; }
    private string sql;

	public SolicitudCredito()  
	{
        retorno = new object[] { false, "" };
        canal = 0;
	}

    public void agregaDatosGrupo()
    {
        sql = "insert into an_solicitud_credito_encabezado values ("+empresa+","+sucursal+",(select isnull((select top 1 id_solicitud_credito from an_solicitud_credito_encabezado where id_empresa="+empresa+" and id_sucursal="+sucursal+" order by id_solicitud_credito desc),0)+1),"+numeroGrupo+",'" + fechasolicitud + "','" + fechaEntrega + "','" + grupoProductivo + "'," + numeroGrupo + "," + montoCredito + "," + plazo + "," + tasa + "," + garantiaLiquida + "," + montoMaximo + "," + montoAutorizado + "," + plazoRC + "," + tasaRC + ",'" +formaPago + "'," + ciclo + ",'"+observaciones+ "','" + amcs + "',"+id_banco+","+canal+")";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregaDetalleGrupo()
    {
        sql = "insert into an_solicitud_credito_detalle values ("+empresa+","+sucursal+","+idSolicitudEdita+",(select isnull((select top 1 id_detalle from an_solicitud_credito_detalle where id_empresa="+empresa+" and id_sucursal="+sucursal+" order by id_detalle desc),0)+1),"+idCliente+",'"+cliente+"',"+ciclo+",'"+cargo+"','"+estatus+"','"+causaDesercion+"','"+giro+"',"+ingreso+",'"+destinoCredito+"',"+creditoAnterior+","+creditoSolicitado+","+garantiaLiquidaIndiv+","+creditoAutorizado+","+telefono+ ",'" + amc + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void obtieneSolicitudEnc()
    {
        sql = "select * from an_solicitud_credito_encabezado where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }
    public void actualizaDatosGrupo()
    {
        sql = "update an_solicitud_credito_encabezado set fecha_solicitud = '"+ fechasolicitud +"', fecha_entrega_credito = '" + fechaEntrega + "',grupo_productivo = '" + grupoProductivo + "', numero_grupo = " + numeroGrupo + ", monto_credito = "+ montoCredito + ",plazo = " + plazo + ", tasa = " + tasa + ", garantia_liquida = " + garantiaLiquida + ", monto_maximo = " + montoMaximo + ",monto_autorizado = " + montoAutorizado + ", plazoRC = " + plazoRC + ", tasaRC = " + tasaRC + ", forma_pago = '" + formaPago + "', ciclo = " + ciclo + ", observaciones = '" + observaciones + "', id_banco="+id_banco+", id_canal="+canal+" where id_empresa = " + empresa + " and id_sucursal = " + sucursal + " and id_solicitud_credito =" + idSolicitudEdita;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void optieneimpresion()
    {
        //sql = "select * from an_solicitud_credito_encabezado where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita;
        sql = "select A.id_empresa, S.nombre_sucursal, A.id_solicitud_credito, A.id_grupo, A.fecha_solicitud, A.fecha_entrega_credito, A.grupo_productivo, A.numero_grupo, A.monto_credito, A.plazo, A.tasa, A.garantia_liquida, A.monto_maximo, A.monto_autorizado, A.plazoRC, A.tasaRC, A.forma_pago, A.ciclo, A.observaciones, A.amcs, A.id_banco, c.nombre_canal from an_solicitud_credito_encabezado A,PLD_Sucursal S, pld_canal c where A.id_empresa=" + empresa + "and A.id_sucursal="+sucursal+ " and c.codigo_canal=A.id_canal  and S.codigo_sucursal=" + sucursal+ " and A.id_solicitud_credito="+idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }
    public void optieneimpresion1()
    {
        //sql = "select * from an_solicitud_credito_detalle where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita+"order by cargo";
        sql = "select A.id_empresa,A.id_sucursal,A.id_solicitud_credito,A.id_detalle,A.id_cliente, A.nombre_cliente,A.ciclo,A.cargo,A.estatus,A.causas_desercion,A.giro_negocio,A.ingreso,B.nombre_pproducto,credito_anterior,credito_solicitado,garantia_liquida,credito_autorizado,telefono,amc from an_solicitud_credito_detalle A, pld_proposito_cuenta B where id_empresa=" + empresa + " and id_sucursal =" + sucursal + " and id_solicitud_credito =" + idSolicitudEdita + " and B.codigo_pproducto = A.detino_credito order by cargo";
                 retorno = ejecuta.dataSet(sql);
    }

    public void recuperaIntegrantes()
    {
        sql = "select c.id_cliente,c.nombre_completo from AN_Clientes c inner join AN_Acta_IntegracionDetalle a on c.id_cliente=a.id_cliente where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_acta=" +acta ;
        retorno = ejecuta.dataSet(sql);
    }

    public void AgregaDet()
    {
        sql = "update an_solicitud_credito_detalle set credito_autorizado=" + creditoAutorizado + " where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita + " and id_cliente=" + idCliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregaEnca()
    {
        sql = "update  an_solicitud_credito_encabezado set monto_autorizado=" + montoAutorizado + ", plazorc=" + plazoRC + ",tasarc=" + tasaRC + " where id_empresa = " + empresa + " and id_sucursal = " + sucursal + " and id_solicitud_credito =" + idSolicitudEdita;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void recuperdetalleedita()
    {
        sql = "select * from an_solicitud_credito_detalle where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita+" and id_cliente="+idCliente;
        retorno = ejecuta.dataSet(sql);
    }

    public void actualizaint()
    {
        sql = "update  an_solicitud_credito_detalle set ciclo=" + ciclo + ", cargo='" + cargo + "',estatus='" + estatus + "',causas_desercion='"+causaDesercion+ "',giro_negocio='"+giro+ "',ingreso="+ingreso+ ",detino_credito='"+destinoCredito+ "',credito_anterior="+creditoAnterior+ ",credito_solicitado="+creditoSolicitado+ ",garantia_liquida="+garantiaLiquida+ ",telefono="+telefono+ "  where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita + " and id_cliente=" + idCliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void eliminaintegrante()
    {
        sql = "delete from an_solicitud_credito_detalle where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita + " and id_cliente=" + idCliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void recuperagrupo()
    {
        sql = "select id_grupo from an_creditos where id_solicitud_credito=" + grupo;
        retorno = ejecuta.dataSet(sql);
    }
}