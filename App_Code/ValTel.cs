using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ValTel
/// </summary>
public class ValTel
{
    Ejecuciones ejecuta = new Ejecuciones();
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int usuario { get; set; }
    public int id_cliente { get; set; }
    public int idllamada { get; set; }
    public int id_grupo { get; set; }
    public string nombre_com { get; set; }
    public string tipollamada { get; set; }
    public string fecha { get; set; }
    public string hora { get; set; }
    public string usuariollamada { get; set; }
    public string atendio { get; set; }

    public string fechacues { get; set; }
    public string p1 { get; set; }
    public string p2 { get; set; }
    public string p3 { get; set; }
    public string res1 { get; set; }
    public string res2 { get; set; }
    public string res3 { get; set; }
    public string p4 { get; set; }
    public string p5 { get; set; }

    public string observaciones { get; set; }


    public object[] retorno;
    private string sql;
    public ValTel()
    {

    }

    public void obtieneTelefonos()
    {
        sql = "select tel_fijo_cli,tel_cel_cli from  an_ficha_datos  where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente =" + id_cliente ;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtieneDatos()
    {
        sql = "select * from AN_Solicitud_Credito_Encabezado  where id_empresa=" + empresa + " and id_sucursal=" + sucursal + "and id_solicitud_credito=" + id_grupo ;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtienPresidente()
    {
        sql =" select d.id_cliente,c.nombre_cliente from AN_Acta_IntegracionDetalle d inner join AN_Solicitud_Credito_Detalle c on c.id_cliente = d.id_cliente where d.cargo ='P' and d.id_acta ="+id_grupo;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtfechaLLamada()
    {
        sql = " select fecha_llamada,preg1,resn1,preg2,resn2,preg3,resn3,preg4,preg5 from an_llamadas where id_cliente=" + id_cliente+" and id_llamada="+idllamada;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtienTeso()
    {
        sql = " select d.id_cliente,c.nombre_cliente from AN_Acta_IntegracionDetalle d inner join AN_Solicitud_Credito_Detalle c on c.id_cliente = d.id_cliente where d.cargo ='T' and d.id_acta =" + id_grupo;
        retorno = ejecuta.dataSet(sql);
    }

    public void agregarllamadas()
    {
        sql = "insert into AN_llamadas values (" + empresa + "," + sucursal + ",(select isnull((select top 1 id_llamada from AN_llamadas where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " order by id_llamada desc),0)+1)," + id_cliente + "," + id_grupo + ",'"+ nombre_com + "','"+tipollamada+"','"+ fecha +"','"+hora+"','"+ usuariollamada + "','"+ atendio + "','"+observaciones+"','','','','','','','','','')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregaCuesr()
    {
        sql = "UPDATE AN_llamadas  " +
                  " SET  fecha_cuestionario='" + fechacues + "',preg1='"+p1+"',preg2='"+p2+"',preg3='"+p3+"',preg4='"+p4+"',preg5='"+p5+"',resn1='"+res1+"',resn2='"+res2+"',resn3='"+res3+"' where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + id_cliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void obtenusuario()
    {
        sql = " select nombre_usuario from usuarios where id_usuario=" + usuario;
        retorno = ejecuta.dataSet(sql);
    }
}