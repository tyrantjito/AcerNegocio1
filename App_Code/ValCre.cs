using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ValCre
/// </summary>
public class ValCre
{
    Ejecuciones ejecuta = new Ejecuciones();
    public int id_Val { get; set; }
    public string Observacion { get; set; }
    public string estatus { get; set; }
    public string estatuscom { get; set; }
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int cliente { get; set; }
    public int credito { get; set; }
    public decimal Porcentaje { get; set; }

    public object[] retorno;
    private string sql;
    public ValCre()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public void agregarValidacion()
    {
        sql = "insert into AN_ValidacionesGrupos values (" + empresa + "," + sucursal + "," + credito + "," + cliente + "," + id_Val + ",'','','NO CUMPLE')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
   
    public void obtieneValidacion()
    {
        sql = "select estatus from AN_ValidacionesGrupos where id_credito=" + credito + " and id_VALIDACION=" + id_Val + " and id_cliente=" + cliente;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtieneintegranrtesid()
    {
        sql = "select id_cliente from AN_Solicitud_Credito_Detalle where id_solicitud_credito="+credito;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtienenvalidacion()
    {
        sql = "select count (*) from AN_Validaciones_Credito";
        retorno = ejecuta.dataSet(sql);
    }

    public void obtieneestatusvalidacion()
    {
        sql = "select estatus from AN_ValidacionesGrupos  where id_cliente="+cliente+" and Id_Validacion="+id_Val;
        retorno = ejecuta.dataSet(sql);
    }

    public void editaEsatutus()
    {
        sql = "UPDATE AN_ValidacionesGrupos " +
                " SET  estatusCompleto='CUMPLE' where id_credito=" + credito + " and id_Validacion=1 and id_cliente=" + cliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void obtieneAprovadas()
    {
        sql = "select estatusCompleto from AN_ValidacionesGrupos where id_Validacion=1 and Id_credito=" + credito;
        retorno = ejecuta.dataSet(sql);
    }
    public void ESTATUScREDITO()
    {
        sql = "UPDATE an_creditos  " +
                " SET  estatus='DES' where id_solicitud_credito=" + credito;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void obtienVal()
    {
        sql = "select count (*) from an_validacionesGrupos where Id_Validacion=1 and Id_credito=" + credito;
        retorno = ejecuta.dataSet(sql);
    }
    public void editaEsatutus2()
    {
        sql = "UPDATE AN_ValidacionesGrupos " +
                " SET  estatusCompleto='NO CUMPLE' where id_credito=" + credito + " and id_Validacion=1 and id_cliente=" + cliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void editaValidacion()
    {
        sql = "UPDATE AN_ValidacionesGrupos " +
                " SET  Observacion='" + Observacion + "', estatus='" + estatus + "',estatusCompleto='"+estatuscom+"' where id_credito=" + credito + " and id_Validacion=" + id_Val + " and id_cliente=" + cliente; 
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}