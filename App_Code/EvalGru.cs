using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de EvalGru
/// </summary>
public class EvalGru
{
    Ejecuciones ejecuta = new Ejecuciones();
    public int empresa { get; set; } 
    public int sucursal { get; set; }
    public int id_grupo { get; set; }
    public int id_evalgpo { get; set; }
    public string fecha_eval { get; set; }
    public string grupo_productivo_eval { get; set; }
    public int cicli_eval { get; set;}
    public int gerente_sucursal_eval { get; set; }
    public int asesor_eval { get; set; }
    public int preg1_evalgn { get; set; }
    public int preg2_evalgn { get; set; }
    public int preg3_evalgn { get; set; }
    public int preg4_evalgn { get; set; }
    public int preg5_evalgn { get; set; }
    public int total_evalgn { get; set; }
    public int preg1_evalgr { get; set; } 
    public int preg2_evalgr { get; set; }
    public int preg3_evalgr { get; set; }
    public int preg4_evalgr { get; set; }
    public int preg5_evalgr { get; set; }
    public int preg6_evalgr { get; set; }
    public int preg7_evalgr { get; set; }
    public int preg8_evalgr { get; set; }
    public int preg9_evalgr { get; set; }
    public int preg10_evalgr { get; set; }
    public int total_evalgr { get; set; }

    public object[] retorno;
    private string sql;

    public EvalGru() 
    {

    }

    public void agregarEval()
    {
        sql = "insert into AN_Evaluacion_Grupal values (" + empresa + "," + sucursal + ","+id_grupo+",(select isnull((select top 1 id_evalgpo from AN_Evaluacion_Grupal where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " order by id_evalgpo desc),0)+1),'" + fecha_eval+"','"+grupo_productivo_eval+"',"+cicli_eval+","+gerente_sucursal_eval+","+asesor_eval+","+preg1_evalgn+ "," + preg2_evalgn + "," + preg3_evalgn + "," + preg4_evalgn + "," + preg5_evalgn + ","+total_evalgn+","+preg1_evalgr+ "," + preg2_evalgr + "," + preg3_evalgr + "," + preg4_evalgr + "," + preg5_evalgr + "," + preg6_evalgr + "," + preg7_evalgr + "," + preg8_evalgr + "," + preg9_evalgr + "," + preg10_evalgr + ","+total_evalgr+")";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void obtieneEval()
    {
        sql = "select * from an_evaluacion_grupal where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_grupo =" + id_grupo+ " and id_evalgpo=" +id_evalgpo;
        retorno = ejecuta.dataSet(sql);
    }
    public void acutulizaeval()
    {
        sql = "Update an_evaluacion_grupal set  fecha_eval='"+fecha_eval+ "',grupo_productivo_eval='"+grupo_productivo_eval+ "',cicli_eval="+cicli_eval+ ",gerente_sucursal_eval='"+gerente_sucursal_eval+ "',asesor_eval='"+ asesor_eval + "',preg1_evalgn="+preg1_evalgn+ ",preg2_evalgn=" + preg2_evalgn + ",preg3_evalgn=" + preg3_evalgn + ",preg4_evalgn=" + preg4_evalgn + ",preg5_evalgn=" + preg5_evalgn + ",total_evalgn="+total_evalgn+ ",preg1_evalgr="+preg1_evalgr+ ",preg2_evalgr=" + preg2_evalgr + ",preg3_evalgr=" + preg3_evalgr + ",preg4_evalgr=" + preg4_evalgr + ",preg5_evalgr=" + preg5_evalgr + ",preg6_evalgr=" + preg6_evalgr + ",preg7_evalgr=" + preg7_evalgr + ",preg8_evalgr=" + preg8_evalgr + ",preg9_evalgr=" + preg9_evalgr + ",preg10_evalgr=" + preg10_evalgr + ",total_evalgr="+total_evalgr+"  where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_grupo =" + id_grupo+"and id_evalgpo="+id_evalgpo;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void obtineImpri()
    {
        sql = "select * from an_evaluacion_grupal where id_empresa=" + empresa + " and id_sucursal=" + sucursal; 
 

        retorno = ejecuta.dataSet(sql);
    }
    public void obtenerciclo()
    {
        sql = "select ciclo from AN_Solicitud_Credito_Encabezado where id_empresa="+empresa+" and id_sucursal="+sucursal+" and id_grupo="+id_grupo;
        retorno = ejecuta.dataSet(sql);
    }
}