using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Utilities;

/// <summary>
/// Descripción breve de ValidCirculoCred
/// </summary>
public class ValidCirculoCred
{
    Fechas fecha = new Fechas();
    Ejecuciones ejecuta = new Ejecuciones();
    private string sql;
    public object[] retorno;

    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int idcliente { get; set; }
    public int idconsulta { get; set; }
    public int idadjunto { get; set; }
    public string observaciones { get; set; }
    public int usuario { get; set; }
    public string fechaLocal { get; set; }

    public ValidCirculoCred()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public void tieneAdjuntos()
    {
        sql = "select Count(descripcion) from  AN_Adjuntos_Consulta_Buro where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + idcliente;
        retorno = ejecuta.scalarToInt(sql);
    }

    public void tienePrimeraValidacion()
    {
        sql = "select Count(descripcion) from  AN_Adjuntos_Consulta_Buro where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + idcliente + " and validacion_digital='AUT'";
        retorno = ejecuta.scalarToInt(sql);
    }

    public void tieneValidacionDigital()
    {
        sql = "select count(*) from an_adjuntos_consulta_buro where id_empresa="+empresa+" and id_sucursal="+sucursal+" and id_consulta="+idconsulta+" and id_cliente="+idcliente+" and validacion_digital='AUT'";
        retorno = ejecuta.scalarToInt(sql);
    }

    public void tieneValidacionFisica()
    {
        sql = "select count(*) from an_adjuntos_consulta_buro where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_consulta=" + idconsulta + " and id_cliente=" + idcliente + " and validacion_fisica='AUT'";
        retorno = ejecuta.scalarToInt(sql);
    }
    public void tieneNegacionDigital()
    {
        sql = "select count(*) from an_adjuntos_consulta_buro where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_consulta=" + idconsulta + " and id_cliente=" + idcliente + " and validacion_fisica='NEG'";
        retorno = ejecuta.scalarToInt(sql);
    }

    public void tieneNegacionFisica()
    {
        sql = "select count(*) from an_adjuntos_consulta_buro where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_consulta=" + idconsulta + " and id_cliente=" + idcliente + " and validacion_fisica='NEG'";
        retorno = ejecuta.scalarToInt(sql);
    }

    public void obtieneAdjuntoCliente()
    {
        sql = "select * from an_adjuntos_consulta_buro where id_empresa = " + empresa + " and id_sucursal =" + sucursal + " and id_cliente=" + idcliente + " and id_consulta=" + idconsulta + " and id_adjunto=" + idadjunto;
        retorno = ejecuta.dataSet(sql);
    }
    public void autorizaDigital()
    {
        sql = "update an_adjuntos_consulta_buro set  validacion_digital='AUT', observaciones_dig='" + observaciones + "', usuario_valido_dig=" + usuario + ", fecha_valido_dig='" + fechaLocal + "' where id_empresa = " + empresa + " and id_sucursal =" + sucursal + " and id_cliente=" + idcliente + " and id_consulta=" + idconsulta + " and id_adjunto=" + idadjunto;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void niegaDigital()
    {
        sql = "update an_adjuntos_consulta_buro set  validacion_digital='NEG', observaciones_dig='" + observaciones + "', usuario_valido_dig=" + usuario + ", fecha_valido_dig='" + fechaLocal + "' where id_empresa = " + empresa + " and id_sucursal =" + sucursal + " and id_cliente=" + idcliente + " and id_consulta=" + idconsulta + " and id_adjunto=" + idadjunto;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void actualizaConsultaBuroDIGAUT()
    {
        sql = "update an_solicitud_consulta_buro set  AutDocsDig='AUT' where id_empresa = " + empresa + " and id_sucursal =" + sucursal + " and id_cliente=" + idcliente + " and id_consulta=" + idconsulta;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void actualizaConsultaBuroDIGNIEG()
    {
        sql = "update an_solicitud_consulta_buro set  AutDocsDig='NEG' where id_empresa = " + empresa + " and id_sucursal =" + sucursal + " and id_cliente=" + idcliente + " and id_consulta=" + idconsulta;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void actualizaConsultaBuroDIGPEN()
    {
        sql = "update an_solicitud_consulta_buro set  AutDocsDig='PEN' where id_empresa = " + empresa + " and id_sucursal =" + sucursal + " and id_cliente=" + idcliente + " and id_consulta=" + idconsulta;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void actualizaConsultaBuroProcesableAUT()
    {
        sql = "update an_solicitud_consulta_buro set  procesable='FA1' where id_empresa = " + empresa + " and id_sucursal =" + sucursal + " and id_cliente=" + idcliente + " and id_consulta=" + idconsulta;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void actualizaConsultaBuroProcesableNEG()
    {
        sql = "update an_solicitud_consulta_buro set  procesable='NEG' where id_empresa = " + empresa + " and id_sucursal =" + sucursal + " and id_cliente=" + idcliente + " and id_consulta=" + idconsulta;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void actualizaConsultaBuroFISNEG()
    {
        sql = "update an_solicitud_consulta_buro set  AutDocsFis='NEG' where id_empresa = " + empresa + " and id_sucursal =" + sucursal + " and id_cliente=" + idcliente + " and id_consulta=" + idconsulta;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    

    public void actualizaConsultaBuroFISAUT()
    {
        sql = "update an_solicitud_consulta_buro set  AutDocsFis='AUT' where id_empresa = " + empresa + " and id_sucursal =" + sucursal + " and id_cliente=" + idcliente + " and id_consulta=" + idconsulta;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void actualizaConsultaBuroFISPEN()
    {
        sql = "update an_solicitud_consulta_buro set  AutDocsFis='PEN' where id_empresa = " + empresa + " and id_sucursal =" + sucursal + " and id_cliente=" + idcliente + " and id_consulta=" + idconsulta;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void autorizaFisica()
    {
        sql = "update an_adjuntos_consulta_buro set  validacion_fisica='AUT', observaciones_fis='" + observaciones + "', usuario_valido_fis=" + usuario + ", fecha_valido_fis='" + fechaLocal + "' where id_empresa = " + empresa + " and id_sucursal =" + sucursal + " and id_cliente=" + idcliente + " and id_consulta=" + idconsulta + " and id_adjunto=" + idadjunto;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void niegaFisica()
    {
        sql = "update an_adjuntos_consulta_buro set  validacion_fisica='NEG', observaciones_fis='" + observaciones + "', usuario_valido_fis=" + usuario + ", fecha_valido_fis='" + fechaLocal + "' where id_empresa = " + empresa + " and id_sucursal =" + sucursal + " and id_cliente=" + idcliente + " and id_consulta=" + idconsulta + " and id_adjunto=" + idadjunto;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

}