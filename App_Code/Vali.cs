using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Vali
/// </summary>
public class Vali
{
    Ejecuciones ejecuta = new Ejecuciones();

    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int id_cliente { get; set; }

    public object[] retorno { get; set; }
    private string sql;
    public Vali()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public void obtieneFicha()
    {
        sql = "select b.nombre_completo from AN_Ficha_Datos a inner join an_solicitud_consulta_buro b on b.id_cliente a.id_cliente where a.id_sucursal="+sucursal+" and a.id_empresa="+empresa+" and a.id_cliente="+id_cliente;
        retorno = ejecuta.dataSet(sql);
    }
}