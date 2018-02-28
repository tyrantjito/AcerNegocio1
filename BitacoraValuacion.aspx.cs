using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using E_Utilities;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;

public partial class BitacoraValuacion : System.Web.UI.Page
{
    BitacoraValuacionDatos datosBitValuacion = new BitacoraValuacionDatos();
    Fechas fechas = new Fechas();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            txtFechaFin.SelectedDate = fechas.obtieneFechaLocal();
            txtFechaIni.SelectedDate = fechas.obtieneFechaLocal().AddMonths(-1); 
            cargaIndicadores();
            aseguradoras();
            muestraDetalles(0);
        }
    }

      private void cargaIndicadores()
    {
        Indicadores indicadores = new Indicadores();
        indicadores.empresa = Convert.ToInt32(Request.QueryString["e"]);
        indicadores.taller = Convert.ToInt32(Request.QueryString["t"]);
        indicadores.fechaIni = Convert.ToDateTime(txtFechaIni.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.fechaFin = Convert.ToDateTime(txtFechaFin.SelectedDate).ToString("yyyy-MM-dd");
        indicadores.obtienePendientesValuar();
        int pendientes = indicadores.indicador;
        lblPendientes.Text = pendientes.ToString();
        indicadores.obtieneValuados();
        int valuados = indicadores.indicador;
        lblPorAutorizar.Text = valuados.ToString();
        indicadores.obtieneValuacionesAutorizadas();
        int autorizados = indicadores.indicador;
        lblAutorizados.Text = autorizados.ToString();
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

    protected void lnkSelecciona_Click(object sender, EventArgs e)
    {
        LinkButton lknReferencia = (LinkButton)sender;
        string[] argumentos = lknReferencia.CommandArgument.ToString().Split(new char[] { ';' });

        int orden = Convert.ToInt32(argumentos[0]);
        int fase = Convert.ToInt32(argumentos[1]);
        Response.Redirect("CronosOrden.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"] + "&o=" + orden + "&f=" + fase);
    }   

    protected void lnkRegresarOrdenes_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ordenes.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkBuscar_Click(object sender, EventArgs e)
    {
        cargaIndicadores();
        aseguradoras();
        muestraDetalles(0);        
    }
    protected void lnkPendientes1_Click(object sender, EventArgs e)
    {
        muestraDetalles(1);
    }
    protected void lnkPendientes2_Click(object sender, EventArgs e)
    {
        muestraDetalles(2);
    }
    protected void lnkPendientes3_Click(object sender, EventArgs e)
    {
        muestraDetalles(3);
    }

    private void muestraDetalles(int opcion)
    {
        SqlDsPorValuar.SelectParameters["fechaIni"].DefaultValue = txtFechaIni.SelectedDate.Value.ToString("yyyy-MM-dd");
        SqlDsPorValuar.SelectParameters["fechaFin"].DefaultValue = txtFechaFin.SelectedDate.Value.ToString("yyyy-MM-dd");
        if (opcion == 1) {
            lblOpcion.Text="Pendientes por Valuar";
            grdPorAut.DataSource = null;
            grdPorAut.DataBind();
            grdPorAut.Visible = false;
            grdPndtsValAutPorProv.DataSource = null;
            grdPndtsValAutPorProv.DataBind();
            grdPndtsValAutPorProv.Visible = false;
            grdPorValuar.DataBind();
            grdPorValuar.Visible = true;
            grdAutorizados.DataSource = null;
            grdAutorizados.DataBind();
            grdAutorizados.Visible = false;
        }
        else if (opcion == 2) {
            lblOpcion.Text = "Pendientes por Autorizar";       
            grdPorAut.DataSource=cargaFechas(0);
            grdPorAut.DataBind();
            grdPorAut.Visible = true;
            grdPorValuar.DataSource = null;
            grdPorValuar.DataBind();
            grdPorValuar.Visible = false;
            grdAutorizados.DataSource = null;
            grdAutorizados.DataBind();
            grdAutorizados.Visible = false;
            grdPndtsValAutPorProv.DataSource = null;
            grdPndtsValAutPorProv.DataBind();
            grdPndtsValAutPorProv.Visible = false;
        }
        else if (opcion == 3) {
            lblOpcion.Text = "Autorizados";
            grdPorAut.DataSource = null;
            grdPorAut.DataBind();
            grdPorAut.Visible = false;
            grdPorValuar.DataSource = null;
            grdPorValuar.DataBind();
            grdPorValuar.Visible = false;
            grdAutorizados.DataSource = cargaFechas(1);
            grdAutorizados.DataBind();
            grdAutorizados.Visible = true;
            grdPndtsValAutPorProv.DataSource = null;
            grdPndtsValAutPorProv.DataBind();
            grdPndtsValAutPorProv.Visible = false;
        }        
        else {
            lblOpcion.Text = "Seleccione una de las opciones para ver su información";
            grdPorAut.DataSource = null;
            grdPorAut.DataBind();
            grdPorAut.Visible = false;
            grdPorValuar.DataSource = null;
            grdPorValuar.DataBind();
            grdPorValuar.Visible = false;
            grdAutorizados.DataSource = null;
            grdAutorizados.DataBind();
            grdAutorizados.Visible = false;
            grdPndtsValAutPorProv.DataSource = null;
            grdPndtsValAutPorProv.DataBind();
            grdPndtsValAutPorProv.Visible = false;
        }
    }

    protected void grdPorAut_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
            (sender as RadGrid).DataSource = cargaFechas(0);
    }
    
    private DataTable cargaFechas(int proceso)
    {
        DataTable dt = new DataTable();
        dt = null;
        string sql ="";
        if (proceso == 0)
        {
            sql = string.Format("SELECT  orp.no_orden, orp.fase_orden, orp.placas, no_siniestro, no_poliza, (Marcas.descripcion + ' ' + tipu.descripcion + ' ' + tipv.descripcion) AS descripcion, v.modelo, v.color_ext, c.razon_social," +
                "perf.descripcion AS perfil, loc.descripcion AS localizacion, so.f_recepcion AS fecha_Ingreso, CASE Convert(char(10),so.f_alta,120) WHEN '1900-01-01' THEN '' ELSE Convert(char(10),so.f_alta,120) END AS f_alta, " +
                "CASE Convert(char(10),so.f_alta_portal,120) WHEN '1900-01-01' THEN '' ELSE Convert(char(10),so.f_alta_portal,120) END AS f_alta_portal, CASE Convert(char(10),f_entrega,120) WHEN '1900-01-01' THEN '' ELSE Convert(char(10),f_entrega,120) END AS f_recibido_expediente, " +
                "CASE Convert(char(10),so.f_valuacion,120) WHEN '1900-01-01' THEN '' ELSE Convert(char(10),so.f_valuacion,120) END AS f_valuacion, CASE Convert(char(10),so.f_autorizacion,120) WHEN '1900-01-01' THEN '' ELSE Convert(char(10),so.f_autorizacion,120) END AS f_autorizacion " +
                "FROM Ordenes_Reparacion AS orp LEFT JOIN Marcas ON orp.id_marca = Marcas.id_marca LEFT JOIN Tipo_Unidad AS tipu ON orp.id_marca = tipu.id_marca AND orp.id_tipo_vehiculo = tipu.id_tipo_vehiculo AND orp.id_tipo_unidad = tipu.id_tipo_unidad " +
                "LEFT JOIN Tipo_Vehiculo AS tipv ON orp.id_tipo_vehiculo = tipv.id_tipo_vehiculo LEFT JOIN PerfilesOrdenes AS perf ON orp.id_perfilOrden = perf.id_perfilOrden LEFT  JOIN Localizaciones AS loc ON orp.id_localizacion = loc.id_localizacion " +
                "INNER JOIN Seguimiento_Orden AS so ON orp.no_orden = so.no_orden AND orp.id_empresa = so.id_empresa AND orp.id_taller = so.id_taller LEFT JOIN Vehiculos AS v ON orp.id_marca = v.id_marca AND orp.id_tipo_vehiculo = v.id_tipo_vehiculo AND orp.id_tipo_unidad = v.id_tipo_unidad " +
                "AND orp.id_vehiculo = v.id_vehiculo INNER JOIN Cliprov c ON orp.id_cliprov = c.id_cliprov and c.tipo = 'C' " +
                "WHERE orp.tipo_cliprov = 'C' AND orp.id_empresa = {0} AND orp.id_taller = {1} AND orp.status_orden = 'A' " +
                "AND (so.f_alta IS NOT NULL OR Convert(char(10),so.f_alta,120) <> '1900-01-01') AND (f_entrega IS NOT NULL OR Convert(char(10),f_entrega,120) <> '1900-01-01') AND (f_alta_portal IS NOT NULL OR Convert(char(10),f_alta_portal,120) <> '1900-01-01') " +
                "AND (f_valuacion IS NOT NULL OR Convert(char(10),f_valuacion,120) <> '1900-01-01') AND (f_autorizacion IS NULL OR Convert(char(10),f_autorizacion,120) = '1900-01-01') AND so.f_recepcion BETWEEN '{2}' AND '{3}'" +
                "ORDER BY so.f_recepcion DESC, orp.no_orden DESC", Request.QueryString["e"], Request.QueryString["t"], txtFechaIni.SelectedDate.Value.ToString("yyyy-MM-dd"), txtFechaFin.SelectedDate.Value.ToString("yyyy-MM-dd"));
        }
        else
        {
            sql = string.Format("SELECT  orp.no_orden, orp.fase_orden, orp.placas, no_siniestro, no_poliza, (Marcas.descripcion + ' ' + tipu.descripcion + ' ' + tipv.descripcion) AS descripcion, v.modelo, v.color_ext, c.razon_social," +
                "perf.descripcion AS perfil, loc.descripcion AS localizacion, so.f_recepcion AS fecha_Ingreso, CASE Convert(char(10),so.f_alta,120) WHEN '1900-01-01' THEN '' ELSE Convert(char(10),so.f_alta,120) END AS f_alta, " +
                "CASE Convert(char(10),so.f_alta_portal,120) WHEN '1900-01-01' THEN '' ELSE Convert(char(10),so.f_alta_portal,120) END AS f_alta_portal, CASE Convert(char(10),f_entrega,120) WHEN '1900-01-01' THEN '' ELSE Convert(char(10),f_entrega,120) END AS f_recibido_expediente, " +
                "CASE Convert(char(10),so.f_valuacion,120) WHEN '1900-01-01' THEN '' ELSE Convert(char(10),so.f_valuacion,120) END AS f_valuacion, CASE Convert(char(10),so.f_autorizacion,120) WHEN '1900-01-01' THEN '' ELSE Convert(char(10),so.f_autorizacion,120) END AS f_autorizacion " +
                "FROM Ordenes_Reparacion AS orp LEFT JOIN Marcas ON orp.id_marca = Marcas.id_marca LEFT JOIN Tipo_Unidad AS tipu ON orp.id_marca = tipu.id_marca AND orp.id_tipo_vehiculo = tipu.id_tipo_vehiculo AND orp.id_tipo_unidad = tipu.id_tipo_unidad " +
                "LEFT JOIN Tipo_Vehiculo AS tipv ON orp.id_tipo_vehiculo = tipv.id_tipo_vehiculo LEFT JOIN PerfilesOrdenes AS perf ON orp.id_perfilOrden = perf.id_perfilOrden LEFT  JOIN Localizaciones AS loc ON orp.id_localizacion = loc.id_localizacion " +
                "INNER JOIN Seguimiento_Orden AS so ON orp.no_orden = so.no_orden AND orp.id_empresa = so.id_empresa AND orp.id_taller = so.id_taller LEFT JOIN Vehiculos AS v ON orp.id_marca = v.id_marca AND orp.id_tipo_vehiculo = v.id_tipo_vehiculo AND orp.id_tipo_unidad = v.id_tipo_unidad " +
                "AND orp.id_vehiculo = v.id_vehiculo INNER JOIN Cliprov c ON orp.id_cliprov = c.id_cliprov and c.tipo = 'C' " +
                "WHERE orp.tipo_cliprov = 'C' AND orp.id_empresa = {0} AND orp.id_taller = {1} AND orp.status_orden = 'A' " +
                "AND (so.f_alta IS NOT NULL OR Convert(char(10),so.f_alta,120) <> '1900-01-01') AND (f_entrega IS NOT NULL OR Convert(char(10),f_entrega,120) <> '1900-01-01') AND (f_alta_portal IS NOT NULL OR Convert(char(10),f_alta_portal,120) <> '1900-01-01') " +
                "AND (f_valuacion IS NOT NULL OR Convert(char(10),f_valuacion,120) <> '1900-01-01') AND (f_autorizacion IS NOT NULL OR Convert(char(10),f_autorizacion,120) <> '1900-01-01') AND so.f_recepcion BETWEEN '{2}' AND '{3}'" +
                "ORDER BY so.f_recepcion DESC, orp.no_orden DESC", Request.QueryString["e"], Request.QueryString["t"], txtFechaIni.SelectedDate.Value.ToString("yyyy-MM-dd"), txtFechaFin.SelectedDate.Value.ToString("yyyy-MM-dd"));
        }
        dt = GetData(sql);
        return dt;
    }

    private DataTable cargaFechasProv(string cliente)
    {
        DataTable dt = new DataTable();
        dt = null;
        string sql = string.Format("SELECT COUNT(orp.no_orden) AS ordenes, FORMAT(so.f_recepcion, 'dd-MM-yyyy') AS ingreso FROM Ordenes_Reparacion AS orp " +
            "INNER JOIN Seguimiento_Orden AS so ON orp.no_orden = so.no_orden AND orp.id_empresa = so.id_empresa AND orp.id_taller = so.id_taller " +
            "WHERE orp.id_empresa={0} AND orp.id_taller = {1} AND orp.id_cliprov= {4} AND orp.tipo_cliprov = 'C' AND orp.status_orden = 'A'  " +
            "AND (so.f_recepcion BETWEEN '{2}' AND '{3}') " +
            "GROUP BY so.f_recepcion " +
            "ORDER BY f_recepcion DESC", Request.QueryString["e"], Request.QueryString["t"], txtFechaIni.SelectedDate.Value.ToString("yyyy-MM-dd"), txtFechaFin.SelectedDate.Value.ToString("yyyy-MM-dd"), cliente);

        dt = GetData(sql);
        return dt;
    }       

    protected void grdPorAut_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        switch (e.DetailTableView.Name)
        {
            case "ingreso":
                {
                    string CustomerID = dataItem.GetDataKeyValue("ingreso").ToString();
                    e.DetailTableView.DataSource = GetData(string.Format("select t.no_orden,t.no_siniestro,t.no_poliza,t.descripcion,t.modelo,t.color_ext,t.placas,t.perfil,t.localizacion,t.razon_social,t.fecha_Ingreso,case t.f_alta when '1900-01-01' then '' else t.f_alta end as f_alta,case t.f_alta_portal when '1900-01-01' then '' else t.f_alta_portal end as f_alta_portal,case t.f_recibido_expediente when '1900-01-01' then '' else t.f_recibido_expediente end as f_recibido_expediente,case t.f_valuacion when '1900-01-01' then '' else t.f_valuacion end as f_valuacion,case t.f_autorizacion when '1900-01-01' then '' else t.f_autorizacion end as f_autorizacion,t.observacion,t.fase_orden from(" +
"select orp.no_orden,orp.no_siniestro,orp.no_poliza,m.descripcion+' '+tu.descripcion+' '+tv.descripcion as descripcion,v.modelo,upper(v.color_ext) as color_ext,orp.placas,po.descripcion as perfil,l.descripcion as localizacion,C.razon_social,convert(varchar,so.f_recepcion,126)as fecha_Ingreso," +
"convert(varchar,isnull(so.f_alta,'1900-01-01'),126)as f_alta, convert(varchar,isnull(so.f_alta_portal,'1900-01-01'),126)as f_alta_portal, convert(varchar,isnull(so.f_entrega,'1900-01-01'),126)as f_recibido_expediente, convert(varchar,isnull(so.f_valuacion,'1900-01-01'),126)as f_valuacion, convert(varchar,isnull(so.f_autorizacion,'1900-01-01'),126)as f_autorizacion," +
"(select isnull((select top 1 boc.observacion from Bitacora_Orden_Comentarios boc where boc.no_orden = orp.no_orden and boc.id_empresa = orp.id_empresa and boc.id_taller = orp.id_taller order by boc.id_observacion desc),''))as observacion,orp.fase_orden " +
"from Ordenes_Reparacion orp " +
"left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa = so.id_empresa and orp.id_taller = so.id_taller " +
"left join Vehiculos v on v.id_marca = orp.id_marca and v.id_tipo_vehiculo = orp.id_tipo_vehiculo and v.id_tipo_unidad = orp.id_tipo_unidad and v.id_vehiculo = orp.id_vehiculo " +
"left join Marcas m on m.id_marca = orp.id_marca " +
"left join Tipo_Vehiculo tv on tv.id_tipo_vehiculo = orp.id_tipo_vehiculo " +
"left join Tipo_Unidad tu on tu.id_marca = orp.id_marca and tu.id_tipo_vehiculo = orp.id_tipo_vehiculo and tu.id_tipo_unidad = orp.id_tipo_unidad " +
"left join Localizaciones l on l.id_localizacion = orp.id_localizacion " +
"left join Cliprov c on c.id_cliprov = orp.id_cliprov and c.tipo = orp.tipo_cliprov and c.tipo = 'C' " +
"left join PerfilesOrdenes po on po.id_perfilOrden = orp.id_perfilOrden " +
"where orp.id_empresa= {0}  and orp.id_taller= {1} and orp.status_orden='A' ) as t where (t.f_valuacion = '{2}') and (t.f_autorizacion='1900-01-01') order by t.no_orden desc", Request.QueryString["e"], Request.QueryString["t"], CustomerID));
                    break;
                }
        }
    }

    protected void grdPorAut_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (grdPorAut.MasterTableView.Items.Count > 0)
            {
                for (int i = 0; i < grdPorAut.MasterTableView.Items.Count; i++)
                {
                    grdPorAut.MasterTableView.Items[i].Expanded = true;
                }
            }
        }
    }

    protected void grdAutorizados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
            (sender as RadGrid).DataSource = cargaFechas(1);
    }

    protected void grdAutorizados_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        switch (e.DetailTableView.Name)
        {
            case "ingreso":
                {
                    string CustomerID = dataItem.GetDataKeyValue("ingreso").ToString();
                    e.DetailTableView.DataSource = GetData(string.Format("select t.no_orden,t.no_siniestro,t.no_poliza,t.descripcion,t.modelo,t.color_ext,t.placas,t.perfil,t.localizacion,t.razon_social,t.fecha_Ingreso,case t.f_alta when '1900-01-01' then '' else t.f_alta end as f_alta,case t.f_alta_portal when '1900-01-01' then '' else t.f_alta_portal end as f_alta_portal,case t.f_recibido_expediente when '1900-01-01' then '' else t.f_recibido_expediente end as f_recibido_expediente,case t.f_valuacion when '1900-01-01' then '' else t.f_valuacion end as f_valuacion,case t.f_autorizacion when '1900-01-01' then '' else t.f_autorizacion end as f_autorizacion,t.observacion,t.fase_orden from(" +
"select orp.no_orden,orp.no_siniestro,orp.no_poliza,m.descripcion+' '+tu.descripcion+' '+tv.descripcion as descripcion,v.modelo,upper(v.color_ext) as color_ext,orp.placas,po.descripcion as perfil,l.descripcion as localizacion,C.razon_social,convert(varchar,so.f_recepcion,126)as fecha_Ingreso," +
"convert(varchar,isnull(so.f_alta,'1900-01-01'),126)as f_alta, convert(varchar,isnull(so.f_alta_portal,'1900-01-01'),126)as f_alta_portal, convert(varchar,isnull(so.f_entrega,'1900-01-01'),126)as f_recibido_expediente, convert(varchar,isnull(so.f_valuacion,'1900-01-01'),126)as f_valuacion, convert(varchar,isnull(so.f_autorizacion,'1900-01-01'),126)as f_autorizacion," +
"(select isnull((select top 1 boc.observacion from Bitacora_Orden_Comentarios boc where boc.no_orden = orp.no_orden and boc.id_empresa = orp.id_empresa and boc.id_taller = orp.id_taller order by boc.id_observacion desc),''))as observacion,orp.fase_orden " +
"from Ordenes_Reparacion orp " +
"left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa = so.id_empresa and orp.id_taller = so.id_taller " +
"left join Vehiculos v on v.id_marca = orp.id_marca and v.id_tipo_vehiculo = orp.id_tipo_vehiculo and v.id_tipo_unidad = orp.id_tipo_unidad and v.id_vehiculo = orp.id_vehiculo " +
"left join Marcas m on m.id_marca = orp.id_marca " +
"left join Tipo_Vehiculo tv on tv.id_tipo_vehiculo = orp.id_tipo_vehiculo " +
"left join Tipo_Unidad tu on tu.id_marca = orp.id_marca and tu.id_tipo_vehiculo = orp.id_tipo_vehiculo and tu.id_tipo_unidad = orp.id_tipo_unidad " +
"left join Localizaciones l on l.id_localizacion = orp.id_localizacion " +
"left join Cliprov c on c.id_cliprov = orp.id_cliprov and c.tipo = orp.tipo_cliprov and c.tipo = 'C' " +
"left join PerfilesOrdenes po on po.id_perfilOrden = orp.id_perfilOrden " +
"where orp.id_empresa= {0}  and orp.id_taller= {1} and orp.status_orden='A' ) as t where (t.f_valuacion>'1900-01-01') and (t.f_autorizacion= '{2}') order by t.no_orden desc", Request.QueryString["e"], Request.QueryString["t"], CustomerID));
                    break;
                }
        }
    }

    protected void grdAutorizados_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (grdAutorizados.MasterTableView.Items.Count > 0)
            {
                for (int i = 0; i < grdAutorizados.MasterTableView.Items.Count; i++)
                {
                    grdAutorizados.MasterTableView.Items[i].Expanded = true;
                }
            }
        }
    }

    protected void grdPndtsValAutPorProv_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
            (sender as RadGrid).DataSource = cargaFechasProv("0");
    }

    protected void grdPndtsValAutPorProv_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        switch (e.DetailTableView.Name)
        {
            case "ingreso":
                {
                    string CustomerID = Convert.ToDateTime(dataItem.GetDataKeyValue("ingreso").ToString()).ToString("yyyy-MM-dd");
                    e.DetailTableView.DataSource = GetData(string.Format("SELECT  orp.no_orden, orp.fase_orden, orp.placas, no_siniestro, no_poliza, orp.observaciones, (Marcas.descripcion + ' ' + tipu.descripcion + ' ' + tipv.descripcion) AS descripcion, v.modelo, v.color_ext, " +
                    "perf.descripcion AS perfil, loc.descripcion AS localizacion, so.f_recepcion AS fecha_Ingreso, CASE convert(char(10),so.f_alta,120) WHEN '1900-01-01' THEN '' ELSE convert(char(10),so.f_alta,120) END AS f_alta, " +
                    "CASE convert(char(10),so.f_alta_portal,120) WHEN '1900-01-01' THEN '' ELSE convert(char(10),so.f_alta_portal,120) END AS f_alta_portal, CASE convert(char(10),f_entrega,120) WHEN '1900-01-01' THEN '' ELSE convert(char(10),f_entrega,120) END AS f_recibido_expediente, " +
                    "CASE convert(char(10),so.f_valuacion,120) WHEN '1900-01-01' THEN '' ELSE convert(char(10),so.f_valuacion,120) END AS f_valuacion, CASE convert(char(10),so.f_autorizacion,120) WHEN '1900-01-01' THEN '' ELSE convert(char(10),so.f_autorizacion,120) END AS f_autorizacion " +
                    "FROM Ordenes_Reparacion AS orp LEFT JOIN Marcas ON orp.id_marca = Marcas.id_marca " +
                    "LEFT JOIN Tipo_Unidad AS tipu ON orp.id_marca = tipu.id_marca AND orp.id_tipo_vehiculo = tipu.id_tipo_vehiculo AND orp.id_tipo_unidad = tipu.id_tipo_unidad " +
                    "LEFT JOIN Tipo_Vehiculo AS tipv ON orp.id_tipo_vehiculo = tipv.id_tipo_vehiculo LEFT JOIN PerfilesOrdenes AS perf ON orp.id_perfilOrden = perf.id_perfilOrden " +
                    "LEFT JOIN Localizaciones AS loc ON orp.id_localizacion = loc.id_localizacion INNER JOIN Seguimiento_Orden AS so ON orp.no_orden = so.no_orden AND orp.id_empresa = so.id_empresa AND orp.id_taller = so.id_taller " +
                    "LEFT JOIN  Vehiculos AS v ON orp.id_marca = v.id_marca AND orp.id_tipo_vehiculo = v.id_tipo_vehiculo AND orp.id_tipo_unidad = v.id_tipo_unidad AND orp.id_vehiculo = v.id_vehiculo " +
                    "WHERE orp.id_empresa = {0} AND orp.id_taller = {1} AND orp.id_cliprov = {2} AND(orp.tipo_cliprov = 'C')  AND orp.status_orden = 'A' AND so.f_recepcion = '{3}' " +
                    "ORDER BY so.f_recepcion DESC, orp.no_orden DESC", Request.QueryString["e"], Request.QueryString["t"], lblIdCliprov.Text, CustomerID)); 
                    break;
                }
        }
    }

    protected void grdPndtsValAutPorProv_PreRender(object sender, EventArgs e)
    {
            if (grdPndtsValAutPorProv.MasterTableView.Items.Count > 0)
            {
                for (int i = 0; i < grdPndtsValAutPorProv.MasterTableView.Items.Count; i++)
                {
                    grdPndtsValAutPorProv.MasterTableView.Items[i].Expanded = true;
                }
            }
        
    }


    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        string rojo = DataBinder.Eval(e.Item.DataItem, "rgb_r").ToString();
        string verde = DataBinder.Eval(e.Item.DataItem, "rgb_g").ToString();
        string azul = DataBinder.Eval(e.Item.DataItem, "rgb_b").ToString();
        HtmlControl indicador = (HtmlControl)e.Item.FindControl("cuadro");
        indicador.Attributes["style"] = "background-color:rgb(" + Convert.ToInt32(rojo) + "," + Convert.ToInt32(verde) + "," + Convert.ToInt32(azul) + ");";
    }

    protected void lnkProceso(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] argumentos = btn.CommandArgument.ToString().Split(new char[] { ';' });
        lblOpcion.Text = argumentos[1];
        lblIdCliprov.Text = argumentos[0];
        grdPorAut.DataSource = null;
        grdPorAut.DataBind();
        grdPorAut.Visible = false;
        grdPorValuar.DataSource = null;
        grdPorValuar.DataBind();
        grdPorValuar.Visible = false;
        grdAutorizados.DataSource = null;
        grdAutorizados.DataBind();
        grdAutorizados.Visible = false;
        grdPndtsValAutPorProv.DataSource = cargaFechasProv(argumentos[0]);
        grdPndtsValAutPorProv.DataBind();
        grdPndtsValAutPorProv.Visible = true;
        
    }

    private void aseguradoras()
    {
        try
        {
            int idEmpresa = Convert.ToInt32(Request.QueryString["e"]);
            int idTaller = Convert.ToInt32(Request.QueryString["t"]);
            string fechaIni = Convert.ToDateTime(txtFechaIni.SelectedDate.ToString()).ToString("yyyy-MM-dd");
            string fechaFin = Convert.ToDateTime(txtFechaFin.SelectedDate.ToString()).ToString("yyyy-MM-dd");
            object[] ejecutado = new object[2];
            DataTable aseguradorasData = null;
            Indicadores datos = new Indicadores();
            ejecutado = datos.optieneAseguradorasValuacion(idEmpresa, idTaller, fechaIni, fechaFin);
            if ((bool)ejecutado[0] && ((DataTable)ejecutado[1]).Rows.Count > 0)
            {
                aseguradorasData = (DataTable)ejecutado[1];
                procesaTiles(aseguradorasData);
            }
            else
                DataList1.Caption = ejecutado[1].ToString();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    private void procesaTiles(DataTable aseguradorasData)
    {
        DataList1.DataSource = aseguradorasData;
        DataList1.DataBind();
    }

    protected void grdPndtsValAutPorProv_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "ingreso")
        {
            DataRowView filas = (DataRowView)e.Item.DataItem;
            DataRow r = filas.Row;
            string proc = "P";
            bool fAlt = !string.IsNullOrEmpty(r[12].ToString());
            bool fAlP = !string.IsNullOrEmpty(r[13].ToString());
            bool fEnt = !string.IsNullOrEmpty(r[14].ToString());
            bool fVal = !string.IsNullOrEmpty(r[15].ToString());
            bool fAut = !string.IsNullOrEmpty(r[16].ToString());
            bool[] fechas = { fAlt, fAlP, fEnt, fVal, fAut };

            int p = 0;
            int v = 0;
            int a = 0;
            for(int i = 0; i < fechas.Length; i++)
            {
                if (i < 3)
                {
                    if (fechas[i])
                        p++;
                }
                else if (i == 3)
                {
                    if (fechas[i])
                        v = 1;
                }
                else {
                    if (fechas[i])
                        a = 1;
                }
            }

            if (p == 3)
                if (v == 1)
                {
                    proc = "V";
                    if (a == 1)
                        proc = "A";
                }
             
            switch (proc)
            {
                case "P":
                    e.Item.CssClass = "alert-danger";
                    break;
                case "V":
                    e.Item.CssClass = "alert-warning";
                    break;
                case "A":
                    e.Item.CssClass = "alert-success";
                    break;
                default:
                    e.Item.CssClass = "alert-info";
                    break;
            }
        }
    }
}