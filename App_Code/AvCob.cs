using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de AvisoCobranza
/// </summary>
public class AvCob
{
    Ejecuciones ejecuta = new Ejecuciones();
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int idSolicitudEdita { get; set; }
    public int idcliente { get; set; }

    public object[] retorno { get; set; }
    private string sql;
    public AvCob()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public void obtieneInfoEnca()
    {
        sql = "select p.gerente_apago,e.grupo_productivo,e.id_grupo,e.monto_autorizado from AN_Solicitud_Credito_Encabezado e left join AN_Analisis_Pago p on e.id_grupo=p.id_grupo where e.id_solicitud_credito=" + idSolicitudEdita;
        retorno = ejecuta.dataSet(sql);
    }
}