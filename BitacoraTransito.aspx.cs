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

public partial class BitacoraTransito : System.Web.UI.Page
{
    BitacoraValuacionDatos datosBitValuacion = new BitacoraValuacionDatos();
    Fechas fechas = new Fechas();
    int contadorBound;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargaIndicadores();
            aseguradoras();
            muestraDetalles(0);
            grdPorValuar.Visible = false;            
        }
    }

    private void cargaIndicadores()
    {
        BitTransito bitTransito = new BitTransito();
        bitTransito.empresa = Convert.ToInt32(Request.QueryString["e"]);
        bitTransito.taller = Convert.ToInt32(Request.QueryString["t"]);
        bitTransito.obtieneTransito();
        lblPendientes.Text = bitTransito.retorno.ToString();
        bitTransito.obtieneRetornoProgHoy();
        lblPorRetornoHoy.Text = bitTransito.retorno.ToString();
        bitTransito.obtieneRetornoProgVenc();
        lblRetProgVenc.Text = bitTransito.retorno.ToString();
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
        if (opcion == 1)
        {
            lblOpcion.Text = "EN TRANSITO";
            grdPorValuar.Visible = true;
            GridDatos.Visible = false;
            SqlDataSource2.SelectParameters.Clear();
            SqlDataSource2.SelectParameters.Add("id_empresa", Request.QueryString["e"].ToString());
            SqlDataSource2.SelectParameters.Add("id_taller", Request.QueryString["t"]);
            grdPorValuar.DataSource = SqlDataSource2;
            grdPorValuar.DataBind();

            GridDatos.DataSource = SqlDataSource2;
            GridDatos.DataBind();

        }
        else if (opcion == 2)
        {
            lblOpcion.Text = "REINGRESO";
            grdPorValuar.Visible = true;
            GridDatos.Visible = false;
            string fechaHoy = fechas.obtieneFechaLocal().ToString("yyyy-MM-dd");
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("id_empresa", Request.QueryString["e"].ToString());
            SqlDataSource1.SelectParameters.Add("id_taller", Request.QueryString["t"]);
            SqlDataSource1.SelectParameters.Add("fecha", fechaHoy);
            grdPorValuar.DataSource = SqlDataSource1;
            grdPorValuar.DataBind();
            GridDatos.DataSource = SqlDataSource1;
            GridDatos.DataBind();
        }
        else if (opcion == 3)
        {
            lblOpcion.Text = "REINGRESO VENCIDO";
            grdPorValuar.Visible = true;
            GridDatos.Visible = false;
            string fechaHoy = fechas.obtieneFechaLocal().ToString("yyyy-MM-dd");
            SqlDataSource3.SelectParameters.Clear();
            SqlDataSource3.SelectParameters.Add("id_empresa", Request.QueryString["e"].ToString());
            SqlDataSource3.SelectParameters.Add("id_taller", Request.QueryString["t"]);
            SqlDataSource3.SelectParameters.Add("fecha", fechaHoy);
            grdPorValuar.DataSource = SqlDataSource3;
            grdPorValuar.DataBind();
            GridDatos.DataSource = SqlDataSource3;
            GridDatos.DataBind();
        }
        else {
            lblOpcion.Text = "Seleccione una de las opciones para ver su información";
            grdPorValuar.Visible = false;
            GridDatos.Visible = false;
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
        lblOpcion.Text = argumentos[1].ToUpper();
        //grdPorValuar.Visible = true;
        GridDatos.Visible = true;
        string fechaHoy = fechas.obtieneFechaLocal().ToString("yyyy-MM-dd");
        SqlDataSource4.SelectParameters.Clear();
        SqlDataSource4.SelectParameters.Add("id_empresa", Request.QueryString["e"]);
        SqlDataSource4.SelectParameters.Add("id_taller", Request.QueryString["t"]);
        SqlDataSource4.SelectParameters.Add("fecha", fechaHoy);
        SqlDataSource4.SelectParameters.Add("id_cliprov", argumentos[0]);
        //grdPorValuar.DataSource = SqlDataSource4;
        //grdPorValuar.DataBind();
        GridDatos.DataSource = SqlDataSource4;
        GridDatos.DataBind();
    }

    private void aseguradoras()
    {
        try
        {
            object[] ejecutado = new object[2];
            DataSet aseguradorasData = null;
            BitTransito bitTransito = new BitTransito();
            bitTransito.empresa = Convert.ToInt32(Request.QueryString["e"]);
            bitTransito.taller = Convert.ToInt32(Request.QueryString["t"]);
            ejecutado = bitTransito.optieneAseguradorasTransitoVencido();
            if ((bool)ejecutado[0])
                aseguradorasData = (DataSet)ejecutado[1];
            if (aseguradorasData != null)
                procesaTiles(aseguradorasData);
        }
        catch (Exception ex)
        {

        }
    }

    private void procesaTiles(DataSet aseguradorasData)
    {
        DataList1.DataSource = aseguradorasData;
        DataList1.DataBind();
    }

    protected void grdPorValuar_PreRender(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            if (grdPorValuar.MasterTableView.Items.Count > 0)
            {
                for (int i = 0; i < grdPorValuar.MasterTableView.Items.Count; i++)
                {
                    grdPorValuar.MasterTableView.Items[i].Expanded = true;
                }
            }
        }
    }

    protected void grdPorValuar_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            contadorBound++;
            Label lblHeadGrid = e.Item.FindControl("lblHeadGrid") as Label;
            LinkButton btnOrdenPendientes = e.Item.FindControl("btnOrdenPendientes") as LinkButton;
            if (contadorBound == 1)
            {
                lblHeadGrid.Visible = true;
                btnOrdenPendientes.Visible = false;
            }
            else
            {
                lblHeadGrid.Visible = false;
                btnOrdenPendientes.Visible = true;
            }
        }
    }

    protected void GridDatos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblFechProgRetTrans = e.Row.FindControl("lblFechProgRetTrans") as Label;
            Label lblFechRetTrans = e.Row.FindControl("lblFechRetTrans") as Label;
            try
            {
                DateTime fechProg = Convert.ToDateTime(lblFechProgRetTrans.Text);
                DateTime fechRet = Convert.ToDateTime(lblFechRetTrans.Text);
                DateTime fechNull = Convert.ToDateTime("1900-01-01");

                if (fechProg == fechNull)
                    lblFechProgRetTrans.Visible = false;
                if (fechRet == fechNull)
                    lblFechRetTrans.Visible = false;

            }
            catch(Exception)
            {

            }
        }
    }
}