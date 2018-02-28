﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Cntra
/// </summary>
public class Cntra
{
    Ejecuciones ejecuta = new Ejecuciones();
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int idSolicitudEdita { get; set; }
    public int idcliente { get; set; }

    public object[] retorno { get; set; }
    private string sql;
    public Cntra()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public void obtieneInfoEncabezado()
    {
        sql = "select * from an_solicitud_credito_encabezado where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtieneNombres()
    {
        sql = "select nombre_cliente from an_solicitud_credito_detalle where id_sucursal="+sucursal+" and id_empresa="+empresa+" and id_solicitud_credito="+idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }
    public void datosClientes()
    {
        sql = "select d.nombre_cliente,f.nacionalidad_pr,f.estado_civil_cli,f.giro_principal_neg,f.calle_cli,f.no_exterior_cli,f.colonia_cli,f.cp_cli,f.del_mun_cli,f.estado_cli,f.no_credencial_ife_cli from an_solicitud_credito_detalle d inner join an_ficha_datos f on d.id_cliente = f.id_cliente where d.id_empresa=" + empresa + " and d.id_sucursal=" + sucursal + " and d.id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtenerEmpresa()
    {
        sql = "select*from an_parametros where id_empresa="+empresa+" and id_sucursal="+sucursal;
        retorno=ejecuta.dataSet(sql);
    }

    public void obtenpagSemanal()
    {
        sql = "select pagosemanal from an_operacion_credito where id_empresa='" + empresa + "' and id_sucursal='" + sucursal + "' and id_grupo=" + idSolicitudEdita + " and no_pago=1";
        retorno = ejecuta.dataSet(sql);
    }
}