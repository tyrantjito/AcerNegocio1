using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Clase para los datos venta hechos desde punto de Venta
/// </summary>
public class VentaDet
{
    private string _clave;
    private string _producto;
    private string _precio;
    private string _cantidad;
    private string _total;
    private string _renglon;
    private float _porc_descuento;
    private float _descuento;

    public VentaDet()
    {
        _clave = string.Empty;
        _producto = string.Empty;
        _precio = string.Empty;
        _cantidad = string.Empty;
        _total = string.Empty;
        _renglon = string.Empty;
        _porc_descuento = 0.00f;
        _descuento = 0.00f;
    }

    public string clave
    {
        get { return _clave; }
        set { _clave = value; }
    }
    public string producto
    {
        get { return _producto; }
        set { _producto = value; }
    }
    public string precio
    {
        get { return _precio; }
        set { _precio = value; }
    }
    public string cantidad
    {
        get { return _cantidad; }
        set { _cantidad = value; }
    }
    public string total
    {
        get { return _total; }
        set { _total = value; }
    }

    public string renglon
    {
        get { return _renglon; }
        set { _renglon = value; }
    }

    public float descuento
    {
        get { return _descuento; }
        set { _descuento = value; }
    }

    public float porc_descuento
    {
        get { return _porc_descuento; }
        set { _porc_descuento = value; }
    }

    public static object[] datosVenta(int ticket, int idPunto, int idAlmacen)
    {
        Ejecuciones ejecuta = new Ejecuciones();
        string sqlVenta = "SELECT renglon, id_refaccion, descripcion, cantidad, venta_unitaria, importe, vd.porc_descuento, valor_descuento, ve.porc_descuento,ve.desglosado " +
            "FROM venta_det vd LEFT JOIN venta_enc ve ON vd.ticket = ve.ticket WHERE ve.ticket=" + ticket + " AND ve.id_punto=" + idPunto;
        object[] result = ejecuta.dataSet(sqlVenta);
        return result;
    }

    public static object[] actualizaFacturado(int ticket, string isla,int cfd)
    {
        Ejecuciones ejecuta = new Ejecuciones();
        string sql = "update venta_enc set facturado=1,estatus='F',idcfd=" + cfd + " where ticket=" + ticket + " and id_punto=" + isla;
        return ejecuta.insertUpdateDelete(sql);
    }

    public static object[] obtieneFechaTicket(int ticket, string isla)
    {
        Ejecuciones ejecuta = new Ejecuciones();
        string sql = "select convert(char(10),fecha_venta,120) as fecha from venta_enc where ticket=" + ticket + " and id_punto=" + isla;
        return ejecuta.scalarToString(sql);
    }

    public static object[] datosVentaM(string[] valoresTicket, int idPunto, int desglosado)
    {
        string tickets = "";
        for (int i = 0; i < valoresTicket.Length; i++) {
            tickets = tickets + valoresTicket[i] + ",";
        }
        tickets = tickets.Substring(0, tickets.Length - 1);
        Ejecuciones ejecuta = new Ejecuciones();
        string sqlVenta = "SELECT renglon, id_refaccion, descripcion, cantidad, venta_unitaria, importe, vd.porc_descuento, valor_descuento, ve.porc_descuento,ve.desglosado,ve.ticket " +
            "FROM venta_det vd LEFT JOIN venta_enc ve ON vd.ticket = ve.ticket WHERE ve.ticket in (" + tickets + ") AND ve.id_punto=" + idPunto+ " order by ve.ticket,renglon";
        object[] result = ejecuta.dataSet(sqlVenta);
        return result;
    }
}