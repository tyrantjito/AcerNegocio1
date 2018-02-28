using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de AnaPag
/// </summary>
public class AnaPag
{
    Ejecuciones ejecuta = new Ejecuciones();
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int id_apago { get; set; }
    public int id_cliente { get; set; }
    public int id_grupo { get; set; }
    public int id_solicitud { get; set; }
    public int id_ficha { get; set; }
    public string gerente_apago { get; set; }
    public string asesor_apago { get; set; }
    public string fecha_pago_apago { get; set; }
    public string grupo_productivo_apago { get; set; }
    public string nombre_cliente_apago { get; set; }
    public string girio_neg { get; set; }
    public decimal Lunes { get; set; }
    public decimal Martes { get; set; }
    public decimal Miercoles { get; set; }
    public decimal Jueves { get; set; }
    public decimal Viernes { get; set; }
    public decimal Sabado { get; set; }
    public decimal Domingo { get; set; }
    public decimal total_semanal_ap { get; set; }
    public decimal total_mensual_ap { get; set; }
    public decimal materias_primas { get; set; }
    public decimal mercancias { get; set; }
    public decimal renta { get; set; }
    public decimal luz { get; set; }
    public decimal agua { get; set; }
    public decimal gas { get; set; }
    public decimal art_papeleria { get; set; }
    public decimal telefono { get; set; }
    public decimal sueldos_sal { get; set; }
    public decimal transporte { get; set; }
    public decimal mantenimiento { get; set; }
    public decimal pago_impuestos { get; set; }
    public decimal pago_finiancieros { get; set; }
    public decimal otras_deudas { get; set; }
    public decimal total_b { get; set; }
    public decimal renta_cli { get; set; }
    public decimal luz_cli { get; set; }
    public decimal agua_cli { get; set; }
    public decimal telefono_cli { get; set; }
    public decimal alimentacion_cli { get; set; }
    public decimal vestido_cli { get; set; }
    public decimal gastos_escolares_cli { get; set; }
    public decimal gastos_medicos_cli { get; set; }
    public decimal transporte_cli { get; set; }
    public decimal deudas_cli { get; set; }
    public decimal mantenimiento_cli { get; set; }
    public decimal pago_impuestos_cli { get; set; }
    public decimal otros_gastos_cli { get; set; }
    public decimal total_c { get; set; }
    public decimal utilidad { get; set; }
    public decimal disponibilidad_semanal { get; set; }
    public decimal monto_credito { get; set; }
    public decimal plazo { get; set; }
    public decimal pago_semanal { get; set; }
    public decimal solvencia { get; set; }
    public string dictamen_final { get; set;}

    public object[] retorno;
    private string sql;

    public AnaPag() 
    { 
        
    }

    public void agregarAnaPago()
    {
        sql = "insert into AN_Analisis_Pago values (" + empresa + "," + sucursal + ",(select isnull((select top 1 id_apago from AN_Analisis_Pago where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " order by id_apago desc),0)+1),"+id_cliente+","+ id_grupo+ "," + id_ficha + ",'" + gerente_apago + "','"+fecha_pago_apago+"','" + grupo_productivo_apago + "','" + nombre_cliente_apago + "','"+girio_neg+"'," + Lunes + "," + Martes + "," + Miercoles + "," + Jueves + "," + Viernes + "," + Sabado + "," + Domingo + "," + total_semanal_ap + "," + total_mensual_ap + "," + materias_primas + "," + mercancias + "," + renta + "," + luz + "," + agua + "," + gas + "," + art_papeleria + "," + telefono + "," + sueldos_sal + "," + transporte + "," + mantenimiento + "," + pago_impuestos + "," + pago_finiancieros + "," + otras_deudas + "," + total_b + "," + renta_cli + "," + luz_cli + "," + agua_cli + "," + telefono_cli + "," + alimentacion_cli + "," + vestido_cli + "," + gastos_escolares_cli + "," + gastos_medicos_cli + "," + transporte_cli + "," + deudas_cli + "," + mantenimiento_cli + "," + pago_impuestos_cli + "," + otros_gastos_cli + "," + total_c + "," + utilidad + "," + disponibilidad_semanal + "," + monto_credito + "," + plazo + "," + pago_semanal + "," + solvencia + ",'" + dictamen_final + "','"+asesor_apago+"')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void edicionAnalisis()
    {
        sql = "select * from AN_Analisis_Pago where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente =" + id_cliente+" and id_apago="+id_apago;
        retorno = ejecuta.dataSet(sql);
    }
    public void acutulizaAnalisis()
    {
        sql = "Update AN_Analisis_Pago set  gerente_apago='" + gerente_apago + "',fecha_pago_apago='" + fecha_pago_apago + "',grupo_productivo_apago='" + grupo_productivo_apago + "',nombre_cliente_apago='" + nombre_cliente_apago + "',giro_neg='"+girio_neg+"',lunes=" + Lunes + ",martes=" + Martes + ",miercoles=" + Miercoles + ",jueves=" + Jueves + ",viernes=" + Viernes + ",sabado=" + Sabado + ",domingo=" + Domingo + ",materias_primas=" + materias_primas + ",mercancias=" + mercancias + ",renta=" + renta + ",luz=" + luz + ",agua=" + agua + ",gas=" + gas + ",art_papeleria=" + art_papeleria + ",telefono=" + telefono + ",sueldos_sal=" + sueldos_sal + ",transporte=" + transporte + ",mantenimiento=" + mantenimiento + ",pago_impuestos=" + pago_impuestos + ",pago_finiancieros=" + pago_finiancieros + ",otras_deudas=" + otras_deudas + ",renta_cli=" + renta_cli + ",luz_cli=" + luz_cli + ",agua_cli=" + agua_cli + ",telefono_cli=" + telefono_cli + ",alimentacion_cli=" + alimentacion_cli + ",vestido_cli=" + vestido_cli + ",gastos_escolares_cli=" + gastos_escolares_cli + ",gastos_medicos_cli=" + gastos_medicos_cli + ",transporte_cli=" + transporte_cli + ",deudas_cli=" + deudas_cli + ",mantenimiento_cli=" + mantenimiento_cli + ",pago_impuestos_cli=" + pago_impuestos_cli + ",otros_gastos_cli=" + otros_gastos_cli + ",monto_credito=" + monto_credito + ",plazo=" + plazo + ",pago_semanal=" + pago_semanal + ",dictamen_final='" + dictamen_final + "' where id_empresa=" + empresa + " and id_sucursal=" + sucursal + "and id_cliente=" + id_cliente + " and id_apago="+id_apago;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void optieneimpresion()
    {
        sql = "select * from AN_Analisis_Pago where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente =" + id_cliente;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtienepagosem()
    {
        sql = "select d.credito_solicitado,e.plazo from AN_Solicitud_Credito_Detalle d inner join AN_Solicitud_Credito_Encabezado e on d.id_solicitud_credito = e.id_solicitud_credito where d.id_empresa=" + empresa+" and d.id_sucursal="+sucursal+" and d.id_cliente="+id_cliente;
        retorno = ejecuta.dataSet(sql);
    }
}