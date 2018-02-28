using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ActaIntDetalle
/// </summary>
public class ActaIntDetalle
{
    Ejecuciones ejecuta = new Ejecuciones();
    private string sql;
    public object[] retorno;

    //lista de campos| propiedades 
    public int id_empresa { get; set; }
    public int id_sucursal { get; set; }
    public int id_acta { get; set; }
    public int id_actadetalle { get; set; }

    public int id_integrante { get; set; }
    public int id_cliente { get; set; }

    public string cargo { get; set; }

    public ActaIntDetalle() 
    {
        retorno = new object[] { false, "" };
    }

    public void agregaDetalleActaIntegracion()
    {
        sql = "insert into an_acta_integraciondetalle values (" + id_empresa + "," + id_sucursal + "," + id_acta +
               ",(select isnull((select top 1 id_actadetalle from an_acta_integraciondetalle where id_empresa=" + id_empresa +
               " and id_sucursal=" + id_sucursal + " and id_acta=" + id_acta + "   order by id_actadetalle desc ),0)+1),'" +

              id_cliente + "','" + cargo 

               + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }


}