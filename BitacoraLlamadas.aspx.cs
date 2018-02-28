using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Telerik.Web.UI;

public partial class BitacoraLlamadas : System.Web.UI.Page
{
    int[] sesiones;
    string fechIni, fechFin, tipoLlamada;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            calext_txtFechaIni.SelectedDate = new E_Utilities.Fechas().obtieneFechaLocal().AddDays(-1.00);
            calExttxtFechaFin.SelectedDate = new E_Utilities.Fechas().obtieneFechaLocal();
            txtFechaIni.Text = calext_txtFechaIni.SelectedDate.Value.ToString("yyyy-MM-dd");
            txtFechaFin.Text = calExttxtFechaFin.SelectedDate.Value.ToString("yyyy-MM-dd");
            calext_txtFechaIni.EndDate = new E_Utilities.Fechas().obtieneFechaLocal();
        }
    }

    private int[] obtieneSesiones()
    {
        int[] sesiones = new int[4] { 0, 0, 0, 0 };
        try
        {
            sesiones[0] = Convert.ToInt32(Request.QueryString["u"]);
            sesiones[1] = Convert.ToInt32(Request.QueryString["p"]);
            sesiones[2] = Convert.ToInt32(Request.QueryString["e"]);
            sesiones[3] = Convert.ToInt32(Request.QueryString["t"]);
        }
        catch (Exception x)
        {
            sesiones = new int[4] { 0, 0, 0, 0 };
            Session["paginaOrigen"] = "Ordenes.aspx";
            Session["errores"] = "Su sesión a expirado vuelva a iniciar Sesión";
            Response.Redirect("AppErrorLog.aspx");
        }
        return sesiones;
    }
    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            sesiones = obtieneSesiones();
            fechIni =txtFechaIni.Text;
            fechFin = txtFechaFin.Text;
            tipoLlamada = rbtnTipoLlamada.SelectedValue;
            RadGrid1.DataSource = GetData("select llo.no_orden, tv.descripcion + ' ' + m.descripcion + ' ' + tu.descripcion as descripcion, upper(v.color_ext) as color_ext, orp.placas, l.descripcion as localizacion, C.razon_social, orp.fase_orden, so.f_recepcion, orp.no_siniestro, v.modelo, po.descripcion as perfil, orp.status_orden, "
                + "(select count(*) from llamadas_orden lo where lo.id_empresa=orp.id_empresa and lo.id_taller=orp.id_taller and lo.no_orden=orp.no_orden and lo.tipo_llamada='" + tipoLlamada + "' and lo.atendida=0) as llamadas"
                + " from Ordenes_Reparacion orp"
                + " left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa=so.id_empresa and orp.id_taller=so.id_taller"
                + " left join Vehiculos v on v.id_marca=orp.id_marca and v.id_tipo_vehiculo=orp.id_tipo_vehiculo and v.id_tipo_unidad=orp.id_tipo_unidad and v.id_vehiculo=orp.id_vehiculo"
                + " left join Marcas m on m.id_marca=orp.id_marca"
                + " left join Tipo_Vehiculo tv on tv.id_tipo_vehiculo=orp.id_tipo_vehiculo"
                + " left join Tipo_Unidad tu on tu.id_marca=orp.id_marca and tu.id_tipo_vehiculo=orp.id_tipo_vehiculo and tu.id_tipo_unidad=orp.id_tipo_unidad"
                + " left join Localizaciones l on l.id_localizacion=orp.id_localizacion"
                + " left join Cliprov c on c.id_cliprov=orp.id_cliprov and c.tipo=orp.tipo_cliprov and c.tipo='C'"
                + " left join PerfilesOrdenes po on po.id_perfilOrden=orp.id_perfilOrden"
                + " inner join llamadas_orden llo ON llo.no_orden = orp.no_orden and llo.id_empresa = orp.id_empresa and llo.id_taller = orp.id_taller"
                + " where orp.id_empresa=" + sesiones[2].ToString() + " and orp.id_taller=" + sesiones[3].ToString() 
                + (tipoLlamada == "P" ? " AND llo.atendida=0" : " AND llo.tipo_llamada='" + tipoLlamada + "' AND (llo.fecha_llamada BETWEEN '" + fechIni + "' AND '" + fechFin + "'" + (tipoLlamada == "S" ? " OR llo.fecha_llamada IS NULL)" : ")")) +
                " order by orp.no_orden desc, llo.fecha_llamada desc");
            lblLlamMnsj.Text = "&nbsp;&nbsp;" + rbtnTipoLlamada.SelectedItem.Text + " del " + fechIni + " al " + fechFin;
        }
    }

    protected void RadGrid1_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = e.DetailTableView.ParentItem;

        //switch (e.DetailTableView.Name)
        switch (rbtnTipoLlamada.SelectedValue)
        {
            //case "Llamadas":
            case "P":
                {
                    e.DetailTableView.Columns[11].Visible = false;
                    e.DetailTableView.DataSource = GetData(string.Format("SELECT c.razon_social AS aseguradora, llo.consecutivo, llo.fecha_llamada, llo.hora, CASE llo.tipo_llamada WHEN 'E' THEN 'Entrante' ELSE 'Saliente' END AS tipo_llamada, llo.cliente_llamo, llo.contesto, llo.responsable, llo.comentarios_cliente, llo.observaciones, llo.atendio, llo.atendida, " +
                    "llo.quienatendio,  CASE so.f_promesa_portal WHEN '1900-01-01' THEN '' ELSE so.f_promesa_portal END AS f_promesa, " +
                    "RTRIM(ISNULL(CASE orp.tel_part_propietario WHEN 'N/A' THEN '' ELSE orp.tel_part_propietario END, '')) " +
                    "+ '/' + RTRIM(ISNULL(CASE orp.tel_cel_propietario WHEN 'N/A' THEN '' ELSE orp.tel_cel_propietario END, '')) " +
                    "+ '/' + RTRIM(ISNULL(CASE orp.tel_ofi_propietario WHEN 'N/A' THEN '' ELSE orp.tel_ofi_propietario END, '')) AS telefonos " +
                    "FROM Llamadas_Orden AS llo LEFT JOIN Ordenes_Reparacion AS orp ON llo.no_orden = orp.no_orden AND llo.id_empresa = orp.id_empresa AND llo.id_taller = orp.id_taller " +
                    "LEFT JOIN Seguimiento_Orden so ON so.no_orden = orp.no_orden and orp.id_empresa = so.id_empresa and orp.id_taller = so.id_taller " +
                    "LEFT JOIN Cliprov c ON c.id_cliprov = orp.id_cliprov and c.tipo = orp.tipo_cliprov and c.tipo = 'C' " +
                    "WHERE llo.no_orden = {0} AND llo.id_empresa = {1} AND llo.id_taller = {2} AND llo.atendida = 0 " +
                    "ORDER BY llo.fecha_llamada, llo.tipo_llamada DESC", dataItem.GetDataKeyValue("no_orden").ToString(), sesiones[2], sesiones[3]));
                    break;
                }
            default:
                {
                    e.DetailTableView.DataSource = GetData(string.Format("select convert(varchar,llo.fecha_llamada,126)as fecha_llamada,convert(varchar,llo.hora,108)as hora," +
                    "llo.cliente_llamo, llo.contesto, llo.atendio, llo.responsable, orp.no_orden, isnull(llo.estatus, 'P') as estatus, isnull(c.razon_social, '') as aseguradora, " +
                    "isnull(llo.comentarios_cliente, '') as comentario_cliente, rtrim(isnull(case orp.tel_part_propietario  when 'N/A' then '' else orp.tel_part_propietario end,''))+'/'+rtrim(isnull(case orp.tel_cel_propietario  when 'N/A' then '' else orp.tel_cel_propietario end,''))+'/'+rtrim(isnull(case orp.tel_ofi_propietario when 'N/A' then '' else orp.tel_ofi_propietario end ,'')) as telefonos, "+
                    "isnull(llo.observaciones, '') as observaciones, case so.f_promesa_portal when '1900-01-01' then '' else so.f_promesa_portal end as f_promesa, " +
                    "f_recepcion as fecha_Ingreso, orp.fase_orden, llo.atendida,llo.quienatendio,llo.fechaatendio,llo.horaatendio,llo.consecutivo " +
                    "from Llamadas_Orden llo left join Ordenes_Reparacion orp on llo.no_orden = orp.no_orden and llo.id_empresa = orp.id_empresa and llo.id_taller = orp.id_taller " +
                    "left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa = so.id_empresa and orp.id_taller = so.id_taller " +
                    "left join Cliprov c on c.id_cliprov = orp.id_cliprov and c.tipo = orp.tipo_cliprov and c.tipo = 'C' " +
                    "where orp.id_empresa= {0}  and orp.id_taller= {1} and orp.no_orden={2} AND llo.tipo_llamada ='{3}' AND (llo.fecha_llamada BETWEEN '" + fechIni + "' AND '" + fechFin + "'" +
                    (tipoLlamada == "S" ? " OR llo.fecha_llamada IS NULL)" : ")") +
                    "order by orp.no_orden desc,llo.consecutivo desc", sesiones[2], sesiones[3], dataItem.GetDataKeyValue("no_orden").ToString(), tipoLlamada));
                    break;
                }
        }
    }
   
    protected void RadGrid1_PreRender(object sender, EventArgs e)
    {       
            for (int i = 0; i < RadGrid1.MasterTableView.Items.Count; i++) {
                RadGrid1.MasterTableView.Items[i].Expanded = true;
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
        Response.Redirect("BitacoraLlamadasOrden.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"] + "&o=" + orden + "&f=" + fase);
    }

    protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string procesosTerminados = DataBinder.Eval(e.Row.DataItem, "estatus").ToString();
            switch (procesosTerminados)
            {
                case "P":
                    e.Row.CssClass = "alert-danger";
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

    protected void rbtnTipoLlamada_SelectedIndexChanged(object sender, EventArgs e)
    {
        calExtFechas();
        RadGrid1.Rebind();
    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            DataRowView filas = (DataRowView)e.Item.DataItem;
            DataRow r = filas.Row;
            var btnOrden = r[0].ToString();
            var status = r[11].ToString();
            if (!tipoLlamada.Equals("P"))
            {
                try
                {
                    var llamadas = Convert.ToInt32(r[12]);
                    if (llamadas == 0)
                        e.Item.CssClass = "alert-info";
                    else
                        e.Item.CssClass = "alert-danger";
                }
                catch (Exception) { }
            }
            try
            {                
                int noOrden = Convert.ToInt32(btnOrden);               
                var btn = e.Item.Cells[0].Controls[0].FindControl("btnOrden") as LinkButton;
                if (Convert.ToString(status) == "A")
                    btn.CssClass = "btn btn-primary textoBold colorBlanco";
                else if (Convert.ToString(status) == "T")
                    btn.CssClass = "btn btn-info textoBold colorBlanco";
                else if (Convert.ToString(status) == "C")
                    btn.CssClass = "btn btn-default textoBold colorNegro";
                else if (Convert.ToString(status) == "R")
                    btn.CssClass = "btn btn-success textoBold colorBlanco";
                else if (Convert.ToString(status) == "F")
                    btn.CssClass = "btn btn-warning textoBold colorBlanco";
                else if (Convert.ToString(status) == "S")
                    btn.CssClass = "btn btn-danger textoBold colorBlanco";

                if (rbtnTipoLlamada.SelectedValue == "E" || rbtnTipoLlamada.SelectedValue == "P")
                {
                    if(rbtnTipoLlamada.SelectedValue=="P")
                        RadGrid1.MasterTableView.DetailTables[0].Columns[0].Visible = true;
                    else
                        RadGrid1.MasterTableView.DetailTables[0].Columns[0].Visible = false;
                    RadGrid1.MasterTableView.DetailTables[0].Columns[1].Visible = true;
                    RadGrid1.MasterTableView.DetailTables[0].Columns[2].Visible = true;
                    RadGrid1.MasterTableView.DetailTables[0].Columns[3].Visible = true;
                    RadGrid1.MasterTableView.DetailTables[0].Columns[4].Visible = true;
                    RadGrid1.MasterTableView.DetailTables[0].Columns[5].Visible = true;
                    RadGrid1.MasterTableView.DetailTables[0].Columns[6].Visible = true;
                }
                else
                {
                    RadGrid1.MasterTableView.DetailTables[0].Columns[0].Visible = false;
                    RadGrid1.MasterTableView.DetailTables[0].Columns[1].Visible = false;
                    RadGrid1.MasterTableView.DetailTables[0].Columns[2].Visible = false;
                    RadGrid1.MasterTableView.DetailTables[0].Columns[3].Visible = false;
                    RadGrid1.MasterTableView.DetailTables[0].Columns[4].Visible = false;
                    RadGrid1.MasterTableView.DetailTables[0].Columns[5].Visible = false;
                    RadGrid1.MasterTableView.DetailTables[0].Columns[6].Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

   /* protected void Page_Init(object sender, System.EventArgs e)
    {
        RadGrid1.FilterMenu.Items.Clear();
        GridFilterMenu filterMenu = RadGrid1.FilterMenu;
        RadMenuItem menuItem = new RadMenuItem();
        menuItem.Text = "Iguial a";
        menuItem.Value = "EqualTo";
        RadGrid1.FilterMenu.Items.Add(menuItem);

        RadMenuItem menuItem2 = new RadMenuItem();
        menuItem2.Text = "Contiene";
        menuItem2.Value = "Contains";
        RadGrid1.FilterMenu.Items.Add(menuItem2);
        RadMenuItem menuItem3 = new RadMenuItem();
        menuItem3.Text = "Quitar Filtro";
        menuItem3.Value = "NoFilter";
        RadGrid1.FilterMenu.Items.Add(menuItem3);
        filterMenu.ItemClick += new RadMenuEventHandler(filterMenu_ItemClick);
    }*/

    protected void filterMenu_ItemClick(object sender, RadMenuEventArgs e)
    {
        GridFilteringItem filterItem = RadGrid1.MasterTableView.GetItems(GridItemType.FilteringItem)[0] as GridFilteringItem;
        filterItem.FireCommandEvent("Filter", new Pair(e.Item.Value, e.Item.Attributes["columnUniqueName"]));
    }

    private void calExtFechas()
    {
        calext_txtFechaIni.SelectedDate = DateTime.ParseExact(txtFechaIni.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        calExttxtFechaFin.SelectedDate = DateTime.ParseExact(txtFechaFin.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
    }

    protected void lnkBuscaFechas_Click(object sender, EventArgs e)
    {
        calExtFechas();
        RadGrid1.Rebind();
    }
}
