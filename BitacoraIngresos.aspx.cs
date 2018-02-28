using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using E_Utilities;
using Telerik.Web.UI;
using System.Globalization;

public partial class BitacoraIngresos : System.Web.UI.Page
{
    Fechas fechas = new Fechas();
    ValidaFechas validaciones = new ValidaFechas();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            txtFechaIni.SelectedDate = txtFechaFin.SelectedDate = fechas.obtieneFechaLocal();                    
            cargaIndicadores();            
        }
    }

    private static DataTable GetData(string query)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["Taller"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    protected void btnOrden_Click(object sender, EventArgs e)
    {
        LinkButton lknReferencia = (LinkButton)sender;
        int orden = Convert.ToInt32(lknReferencia.Text);
        int fase = Convert.ToInt32(lknReferencia.CommandArgument.ToString());
        Response.Redirect("BienvenidaOrdenes.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"] + "&o=" + orden + "&f=" + fase);
    }

    protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int procesosTerminados = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "procesos").ToString());
            switch (procesosTerminados)
            {
                case 1:
                    e.Row.CssClass = "alert-danger";
                    break;
                case 2:
                    e.Row.CssClass = "alert-warning";
                    break;
                case 3:
                    e.Row.CssClass = "alert-success";
                    break;
                default:
                    e.Row.CssClass = "alert-info";
                    break;
            }
        }
    }

    protected void lnkRegresarOrdenes_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ordenes.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkBuscar_Click(object sender, EventArgs e)
    {
        cargaDatos();
        cargaIndicadores();
    }

    private void cargaDatos()
    {
        DateTime fechaIni, fechaFin;
        try
        {
            fechaIni = Convert.ToDateTime(txtFechaIni.SelectedDate);
            fechaFin = Convert.ToDateTime(txtFechaFin.SelectedDate);
            validaciones.fechaInicial = fechaIni;
            validaciones.fechaFinal = fechaFin;
            int valido = validaciones.validaFechas();
            if (valido == 1)
                lblError.Text = "La fecha inicial no puede ser mayor que la final";
            else if (valido == 2 || valido == 3)
            {                
                RadGrid1.DataSource = cargaFechas();
                RadGrid1.DataBind();
            }
            else
                lblError.Text = "No es posible validar las fechas, por favor verifique";
        }
        catch (Exception)
        {            
            RadGrid1.DataBind();
        }
    }

    private void cargaIndicadores()
    {
        Indicadores indicadores = new Indicadores();
        indicadores.empresa = Convert.ToInt32(Request.QueryString["e"]);
        indicadores.taller = Convert.ToInt32(Request.QueryString["t"]);
        indicadores.fechaIni = Convert.ToDateTime(txtFechaIni.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.fechaFin = Convert.ToDateTime(txtFechaFin.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.obtieneIngresos();
        int ingresos = indicadores.indicador;
        lblIngresosTotales.Text = ingresos.ToString();
        RadGridTipoSeervicio.DataSource = cargaTipoServicios();
        RadGridTipoSeervicio.DataBind();
        RadGridClientes.DataSource = cargaClientes();
        RadGridClientes.DataBind();
        RadGridPerfiles.DataSource = cargaPerfiles();
        RadGridPerfiles.DataBind();
        RadGridLocalizaciones.DataSource = cargaLocalizaciones();
        RadGridLocalizaciones.DataBind();
        RadGridValuacion.DataSource = cargaValuacion();
        RadGridValuacion.DataBind();
        RadGridEtapas.DataSource = cargaEtapas();
        RadGridEtapas.DataBind();
    }
    private DataTable cargaTipoServicios()
    {
        Indicadores indicadores = new Indicadores();
        indicadores.empresa = Convert.ToInt32(Request.QueryString["e"]);
        indicadores.taller = Convert.ToInt32(Request.QueryString["t"]);
        indicadores.fechaIni = Convert.ToDateTime(txtFechaIni.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.fechaFin = Convert.ToDateTime(txtFechaFin.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.obtieneTipoServicioIngresos();
        return indicadores.contadores;
    }
    private DataTable cargaClientes()
    {
        Indicadores indicadores = new Indicadores();
        indicadores.empresa = Convert.ToInt32(Request.QueryString["e"]);
        indicadores.taller = Convert.ToInt32(Request.QueryString["t"]);
        indicadores.fechaIni = Convert.ToDateTime(txtFechaIni.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.fechaFin = Convert.ToDateTime(txtFechaFin.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.obtieneClientesIngresos();
        return indicadores.contadores;
    }
    private object cargaPerfiles()
    {
        Indicadores indicadores = new Indicadores();
        indicadores.empresa = Convert.ToInt32(Request.QueryString["e"]);
        indicadores.taller = Convert.ToInt32(Request.QueryString["t"]);
        indicadores.fechaIni = Convert.ToDateTime(txtFechaIni.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.fechaFin = Convert.ToDateTime(txtFechaFin.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.obtienePerfilesIngresos();
        return indicadores.contadores;
    }
    private object cargaLocalizaciones()
    {
        Indicadores indicadores = new Indicadores();
        indicadores.empresa = Convert.ToInt32(Request.QueryString["e"]);
        indicadores.taller = Convert.ToInt32(Request.QueryString["t"]);
        indicadores.fechaIni = Convert.ToDateTime(txtFechaIni.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.fechaFin = Convert.ToDateTime(txtFechaFin.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.obtieneLocalizacionesIngresos();
        return indicadores.contadores;
    }
    private object cargaValuacion()
    {
        Indicadores indicadores = new Indicadores();
        indicadores.empresa = Convert.ToInt32(Request.QueryString["e"]);
        indicadores.taller = Convert.ToInt32(Request.QueryString["t"]);
        indicadores.fechaIni = Convert.ToDateTime(txtFechaIni.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.fechaFin = Convert.ToDateTime(txtFechaFin.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.obtieneValuacionIngresos();
        return indicadores.contadores;
    }
    private object cargaEtapas()
    {
        Indicadores indicadores = new Indicadores();
        indicadores.empresa = Convert.ToInt32(Request.QueryString["e"]);
        indicadores.taller = Convert.ToInt32(Request.QueryString["t"]);
        indicadores.fechaIni = Convert.ToDateTime(txtFechaIni.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.fechaFin = Convert.ToDateTime(txtFechaFin.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.obtieneEtapasIngresos();
        return indicadores.contadores;
    }

    protected void RadGridTipoServicio_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        (sender as RadGrid).DataSource = cargaTipoServicios();
    }
    protected void RadGridClientes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        (sender as RadGrid).DataSource = cargaClientes();
    }
    protected void RadGridPerfiles_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        (sender as RadGrid).DataSource = cargaPerfiles();
    }
    protected void RadGridLocalizaciones_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        (sender as RadGrid).DataSource = cargaLocalizaciones();
    }
    protected void RadGridValuacion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        (sender as RadGrid).DataSource = cargaValuacion();
    }
    protected void RadGridEtapas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        (sender as RadGrid).DataSource = cargaEtapas();
    }
    protected void lnkIngresos_Click(object sender, EventArgs e)
    {
        cargaDatos();
        cargaIndicadores();
    }


    protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
            (sender as RadGrid).DataSource = cargaFechas();
    }

    private DataTable cargaFechas()
    {
        DataTable dt = new DataTable();
        dt = null;
        string sql = string.Format("select convert(char(10), s.f_recepcion,126) as ingreso,rtrim(ltrim(replace(convert(char(10), s.f_recepcion,126),'-',''))) as f_recepcion,(select count(*) from Ordenes_Reparacion orp left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa=so.id_empresa and orp.id_taller=so.id_taller where orp.id_empresa= o.id_empresa  and orp.id_taller= o.id_taller and so.f_recepcion=s.f_recepcion) as ordenes from ordenes_reparacion o inner join seguimiento_orden s on s.id_empresa=o.id_empresa and s.id_taller=o.id_taller and s.no_orden=o.no_orden where o.id_empresa={0} and o.id_taller={1} and s.f_recepcion between '{2}' AND '{3}' group by o.id_empresa,o.id_taller,s.f_recepcion order by s.f_recepcion desc", Request.QueryString["e"], Request.QueryString["t"], txtFechaIni.SelectedDate.Value.ToString("yyyy-MM-dd"), txtFechaFin.SelectedDate.Value.ToString("yyyy-MM-dd"));
        dt = GetDataTable(sql);
        return dt;
    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "ingreso")
        {
            DataRowView filas = (DataRowView)e.Item.DataItem;
            DataRow r = filas.Row;
            int proceso = convierteEntero(Convert.ToBoolean(r[12])) + convierteEntero(Convert.ToBoolean(r[13])) + convierteEntero(Convert.ToBoolean(r[14]));
            switch (proceso)
            {
                case 1:
                    e.Item.CssClass = "alert-danger";
                    break;
                case 2:
                    e.Item.CssClass = "alert-warning";
                    break;
                case 3:
                    e.Item.CssClass = "alert-success";
                    break;
                default:
                    e.Item.CssClass = "alert-info";
                    break;
            }
        }
    }

    private int convierteEntero(bool valor)
    {
        if (valor)
            return 1;
        else
            return 0;
    }

    protected void RadGrid1_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        switch (e.DetailTableView.Name)
        {
            case "ingreso":
                {
                    string CustomerID = dataItem.GetDataKeyValue("ingreso").ToString();
                    e.DetailTableView.DataSource = GetDataTable(string.Format("select orp.no_orden,tv.descripcion+' '+m.descripcion+' '+tu.descripcion as descripcion,upper(v.color_ext) as color_ext,orp.placas,l.descripcion as localizacion,C.razon_social,orp.fase_orden, so.f_recepcion, orp.no_siniestro,v.modelo,po.descripcion as perfil,"
            + " isnull((select ((case datos_orden when 1 then 1 else 0 end) +(case inventario when 1 then 1 else 0 end) +(case caracteristicas_vehiculo when 1 then 1 else 0 end) )  from control_procesos where id_empresa=orp.id_empresa and id_taller=orp.id_taller and no_orden=orp.no_orden),0) as procesos,"
            + " cp.datos_orden, cp.inventario, cp.caracteristicas_vehiculo, (SELECT nombre_usuario FROM usuarios WHERE id_usuario=boa.id_usuario) AS usuario"
            + " from Ordenes_Reparacion orp"
            + " left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa=so.id_empresa and orp.id_taller=so.id_taller"
            + " left join Vehiculos v on v.id_marca=orp.id_marca and v.id_tipo_vehiculo=orp.id_tipo_vehiculo and v.id_tipo_unidad=orp.id_tipo_unidad and v.id_vehiculo=orp.id_vehiculo"
            + " left join Marcas m on m.id_marca=orp.id_marca"
            + " left join Tipo_Vehiculo tv on tv.id_tipo_vehiculo=orp.id_tipo_vehiculo"
            + " left join Tipo_Unidad tu on tu.id_marca=orp.id_marca and tu.id_tipo_vehiculo=orp.id_tipo_vehiculo and tu.id_tipo_unidad=orp.id_tipo_unidad"
            + " left join Localizaciones l on l.id_localizacion=orp.id_localizacion"
            + " left join Cliprov c on c.id_cliprov=orp.id_cliprov and c.tipo=orp.tipo_cliprov and c.tipo='C'"
            + " left join PerfilesOrdenes po on po.id_perfilOrden=orp.id_perfilOrden"
            + " LEFT JOIN Control_Procesos cp ON cp.id_empresa=orp.id_empresa and cp.id_taller=orp.id_taller and cp.no_orden=orp.no_orden"
            + " LEFT JOIN Bitacora_Orden_Avance boa ON boa.id_empresa=orp.id_empresa and boa.id_taller=orp.id_taller and boa.no_orden=orp.no_orden and boa.id_avance=1"
            + " where orp.id_empresa= {0}  and orp.id_taller= {1} and so.f_recepcion='{2}' order by orp.no_orden desc", Request.QueryString["e"], Request.QueryString["t"], CustomerID));
                    break;
                }
        }
    }

    public DataTable GetDataTable(string query)
    {
        String ConnString = ConfigurationManager.ConnectionStrings["Taller"].ConnectionString;
        SqlConnection conn = new SqlConnection(ConnString);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = new SqlCommand(query, conn);

        DataTable myDataTable = new DataTable();

        conn.Open();
        try
        {
            adapter.Fill(myDataTable);
        }
        finally
        {
            conn.Close();
        }

        return myDataTable;
    }

    protected void RadGrid1_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (RadGrid1.MasterTableView.Items.Count > 0)
            {
                for (int i = 0; i < RadGrid1.MasterTableView.Items.Count; i++)
                {
                    RadGrid1.MasterTableView.Items[i].Expanded = true;
                }
            }
        }
    }
}