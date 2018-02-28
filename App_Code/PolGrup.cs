using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PolGrup
/// </summary>
public class PolGrup
{
    Ejecuciones ejecuta = new Ejecuciones();
    public int id_POL { get; set; }
    public string Observacion { get; set; }
    public string estatus { get; set; }
    public int empresa { get; set; }
    public string estatuscom { get; set; }
    public int sucursal { get; set; }
    public int credito { get; set; }
    public int cliente { get; set; }
    public decimal Porcentaje { get; set; }

    public object[] retorno;
    private string sql;
    public PolGrup()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public void obtienepOLITICAS()
    {
        sql = "select count (*) from AN_POLITICAS_credito";
        retorno = ejecuta.dataSet(sql);
    }
    public void obtienVal()
    {
        sql = "select count (*) from AN_PoliticasGrupos where Id_Politica=1 and Id_credito="+credito;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtieneAprovadas()
    {
        sql = "select estatusCompleto from AN_PoliticasGrupos where Id_Politica=1 and Id_credito="+credito;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtieneestatuspolitica()
    {
        sql = "select estatus from AN_POLITICASGRUPOS  where id_cliente=" + cliente + " and id_politica=" + id_POL;
        retorno = ejecuta.dataSet(sql);
    }
    public void agregarPolitica()
    {
        sql = "insert into AN_POLITICASGRUPOS values (" + empresa + "," + sucursal + "," + credito + "," + cliente + "," + id_POL + ",'','','NO CUMPLE')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }


    public void obtieneintegranrtesid()
    {
        sql = "select id_cliente from AN_Solicitud_Credito_Detalle where id_solicitud_credito=" + credito;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtienePliticas()
    {
        sql = "select estatus from an_PoliticasGrupos where id_credito=" + credito + " and id_politica=" + id_POL + " and id_cliente=" + cliente;
        retorno = ejecuta.dataSet(sql);
    }
    public void editaEsatutus2()
    {
        sql = "UPDATE AN_POLITICASGRUPOS " +
                " SET  estatusCompleto='NO CUMPLE' where id_credito=" + credito + " and id_politica=1 and id_cliente=" + cliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void editaEsatutus()
    {
        sql = "UPDATE AN_POLITICASGRUPOS " +
                " SET  estatusCompleto='CUMPLE' where id_credito=" + credito + " and id_politica=1 and id_cliente=" + cliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void ESTATUScREDITO()
    {
        sql = "UPDATE an_creditos  " +
                " SET  estatus='APR' where id_solicitud_credito=" + credito;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editaPoliticas()
    {
        sql = "UPDATE AN_POLITICASGRUPOS " +
                 " SET  Observacion='" + Observacion + "', estatus='" + estatus + "',estatusCompleto='" + estatuscom + "' where id_credito=" + credito + " and id_politica=" + id_POL + " and id_cliente=" + cliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

}