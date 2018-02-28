using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CatFondeo
/// </summary>
public class CatFondeo
{
    Ejecuciones ejecuta = new Ejecuciones();
    public string codigo { get; set; }
    public int monto { get; set; }
    public string fondeador { get; set; }
    public string plazo { get; set; }
    public string periodopago { get; set; }
    public string tipotasa { get; set; }
    public decimal montotasa { get; set; }
    public string descriCre { get; set; }
    public string garantia { get; set; }
    public string fecha { get; set; }
    public int Decimales { get; set; }
    public decimal Porcentaje { get; set; }

    public object[] retorno;
    private string sql;
    public CatFondeo()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public void obtieneFondeoEdit()
    {
        sql = "select * from AN_Linea_Fondeo where id_lineafondeo='" + codigo + "'";
        retorno = ejecuta.dataSet(sql);
    }

    public void eliminaLineaFondeo()
    {
        sql = "delete from AN_Linea_Fondeo where id_lineafondeo='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregarLienaFondeo()
    {
        sql = "insert into AN_Linea_Fondeo values ((select isnull ((select top 1 id_lineafondeo from AN_Linea_Fondeo order by id_lineafondeo desc),0)+1),'" + monto + "','"+fondeador+"','"+plazo+"','"+periodopago+"','"+ tipotasa + "','"+montotasa+"','"+descriCre+"','"+garantia+"','"+fecha+"')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editaLineaFondeo()
    {
        sql = "UPDATE AN_Linea_Fondeo " +
                " SET  Monto='" + monto + "', Fondeador='"+fondeador+"', Plazo='"+plazo+ "', Periodicidad_Pago='"+periodopago+"', Tipo_Tasa='"+tipotasa+"', Monto_Tasa='"+montotasa+"', Destino_Credito='"+descriCre+"', Garantia='"+garantia+"', Fecha_Fondeo='"+fecha+"' where id_lineafondeo='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
}