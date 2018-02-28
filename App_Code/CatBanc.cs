using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CatBanc
/// </summary>
public class CatBanc
{
    Ejecuciones ejecuta = new Ejecuciones();
    public string codigo { get; set; }
    public string Descripcion { get; set; }
    public string Convenio { get; set; }
    public string Referencia { get; set; }
    public int Decimales { get; set; }
    public decimal Porcentaje { get; set; }

    public object[] retorno;
    private string sql;
    public CatBanc()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public void agregarMoneda()
    {
        sql = "insert into an_catBancos values ((select isnull ((select top 1 Clave from an_catBancos order by Clave desc),0)+1),'" + Descripcion + "', '" + Convenio + "', '" + Referencia + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }


    public void obtieneMonedaEdit()
    {
        sql = "select * from an_catBancos where Clave='" + codigo + "'";
        retorno = ejecuta.dataSet(sql);
    }

    public void editaMoneda()
    {
        sql = "UPDATE an_catBancos " +
                " SET   Nombre='" + Descripcion + "', Convenio='"+Convenio+"', Referencia='"+Referencia+"' where Clave='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void eliminaMoneda()
    {
        sql = "delete from an_catBancos where Clave='" + codigo + "'";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

}