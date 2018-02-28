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

public partial class BitacoraPerdidaPago : System.Web.UI.Page
{
    BitacoraValuacionDatos datosBitValuacion = new BitacoraValuacionDatos();
    Fechas fechas = new Fechas();
    BitPerfiles bitPerfiles = new BitPerfiles();
    int contadorBound;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridDatos.Visible = false;
            PandTotales.Visible = false;
            PanExtras.Visible = false;
        }
    }

    private void cargaIndicadores()
    {
        BitTransito bitTransito = new BitTransito();
        bitTransito.empresa = Convert.ToInt32(Request.QueryString["e"]);
        bitTransito.taller = Convert.ToInt32(Request.QueryString["t"]);
        bitTransito.obtieneTransito();
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
        //muestraDetalles(1);
    }

    protected void lnkPendientes2_Click(object sender, EventArgs e)
    {
        //muestraDetalles(2);
    }

    protected void lnkPendientes3_Click(object sender, EventArgs e)
    {
        //muestraDetalles(3);
    }

    private void muestraDetalles(int opcion)
    {
        if (opcion == 1)
        {
            lblOpcion.Text = "EN TRANSITO";
            GridDatos.Visible = true;
            SqlDataSource2.SelectParameters.Clear();
            SqlDataSource2.SelectParameters.Add("id_empresa", Request.QueryString["e"].ToString());
            SqlDataSource2.SelectParameters.Add("id_taller", Request.QueryString["t"]);
            GridDatos.DataSource = SqlDataSource2;
            GridDatos.DataBind();

        }
        else if (opcion == 2)
        {
            lblOpcion.Text = "REINGRESO";
            GridDatos.Visible = true;
            string fechaHoy = fechas.obtieneFechaLocal().ToString("yyyy-MM-dd");
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("id_empresa", Request.QueryString["e"].ToString());
            SqlDataSource1.SelectParameters.Add("id_taller", Request.QueryString["t"]);
            SqlDataSource1.SelectParameters.Add("fecha", fechaHoy);
            GridDatos.DataSource = SqlDataSource1;
            GridDatos.DataBind();
        }
        else if (opcion == 3)
        {
            lblOpcion.Text = "REINGRESO VENCIDO";
            GridDatos.Visible = true;
            string fechaHoy = fechas.obtieneFechaLocal().ToString("yyyy-MM-dd");
            SqlDataSource3.SelectParameters.Clear();
            SqlDataSource3.SelectParameters.Add("id_empresa", Request.QueryString["e"].ToString());
            SqlDataSource3.SelectParameters.Add("id_taller", Request.QueryString["t"]);
            SqlDataSource3.SelectParameters.Add("fecha", fechaHoy);
            GridDatos.DataSource = SqlDataSource3;
            GridDatos.DataBind();
        }
        else
        {
            lblOpcion.Text = "Seleccione una de las opciones para ver su información";
            GridDatos.DataSource = null;
            GridDatos.Visible = false;
        }
    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataListItem lbl = e.Item;
        Label lblVehiculos = lbl.FindControl("lblPerfilId") as Label;
        Label lblPerfil = DataList1.FindControl("lblVehiculosInt") as Label;
        int vehiculos = 0;
        int perfil = 0;
        try
        {
            vehiculos = Convert.ToInt32(lblVehiculos.Text);
            perfil = Convert.ToInt32(lblPerfil.Text);
            HtmlControl indicador = (HtmlControl)e.Item.FindControl("cuadro");
            int[] rgbsPerfilR = { 51, 51, 51, 51, 51, 51, 102, 102, 102, 102, 102, 102, 255, 255, 255, 255, 204 };
            int[] rgbsPerfilV = { 0, 51, 102, 153, 204, 255, 255, 204, 153, 102, 51, 0, 0, 51, 102, 153, 204 };
            int[] rgbsPerfilA = { 204, 204, 204, 204, 204, 204, 204, 204, 204, 204, 204, 204, 204, 204, 204, 204, 204 };
            indicador.Attributes["style"] = "background-color:rgb(" + rgbsPerfilR[perfil] + "," + rgbsPerfilV[perfil] + "," + rgbsPerfilA[perfil] + ");";
        }
        catch (Exception) { }
    }

    protected void lnkProceso(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] argumentos = btn.CommandArgument.ToString().Split(new char[] { ';' });
        lblOpcion.Text = argumentos[1];
        lblIdCliprov.Text = argumentos[0];
        lblOpcion.Text = argumentos[1].ToUpper();
        GridDatos.Visible = true;
        string fechaHoy = fechas.obtieneFechaLocal().ToString("yyyy-MM-dd");
        SqlDataSource4.SelectParameters.Clear();
        SqlDataSource4.SelectParameters.Add("id_perfilOrden", argumentos[2]);
        SqlDataSource4.SelectParameters.Add("id_empresa", Request.QueryString["e"]);
        SqlDataSource4.SelectParameters.Add("id_taller", Request.QueryString["t"]);
        SqlDataSource4.SelectParameters.Add("id_cliprov", argumentos[0]);
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
        catch (Exception) { }
    }

    private void procesaTiles(DataSet aseguradorasData)
    {
        DataList1.DataSource = aseguradorasData;
        DataList1.DataBind();
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
            catch (Exception) { }
        }
    }

    protected void chkPerfiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSeleccionados.Text = "0";
        muestraDetalles(0);
        int idEmpresa = Convert.ToInt32(Request.QueryString["e"]);
        int idTaller = Convert.ToInt32(Request.QueryString["t"]);
        CheckBoxList chkPerfilesA = chkPerfiles;
        int marcados = revisaTrue();
        bool[] checados = new bool[14];
        int[] perfiles = new int[14];
        checados = llenaChecadosArr();
        perfiles = perfilesTrue(checados);
        if (marcados > 0)
        {
            PandTotales.Visible = true;
            DataList1.Visible = true;
            DataSet dataSource5 = null;
            dataSource5 = bitPerfiles.llenaPerfilesList(idEmpresa, idTaller, perfiles);
            DataPerfiles.Visible = true;
            DataPerfiles.DataSource = dataSource5;
            DataPerfiles.DataBind();
        }
        else
        {
            PandTotales.Visible = false;
            DataList1.Visible = false;
            DataPerfiles.Visible = false;
        }
    }

    private int[] perfilesTrue(bool[] checados)
    {
        int[] perfiles= new int[checados.Length];
        for(int conta=0;conta<checados.Length;conta++)
        {
            if (checados[conta])
                perfiles[conta] = conta + 1;
        }
        perfiles = perfiles.Where(x => x != 0).ToArray();
        return perfiles;
    }

    private bool[] llenaChecadosArr()
    {
        bool[] checks = new bool[chkPerfiles.Items.Count];
        for (int conta = 0; conta < chkPerfiles.Items.Count; conta++)
        {
            checks[conta] = chkPerfiles.Items[conta].Selected;
        }
        return checks;
    }

    private int revisaTrue()
    {
        int checados = 0;
        for (int conta = 0; conta < chkPerfiles.Items.Count; conta++)
        {
            if (chkPerfiles.Items[conta].Selected)
                checados++;
        }
        return checados;
    }

    protected void DataLAsg_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        string rojo = DataBinder.Eval(e.Item.DataItem, "rgb_r").ToString();
        string verde = DataBinder.Eval(e.Item.DataItem, "rgb_g").ToString();
        string azul = DataBinder.Eval(e.Item.DataItem, "rgb_b").ToString();
        HtmlControl indicador = (HtmlControl)e.Item.FindControl("cuadro");
        indicador.Attributes["style"] = "background-color:rgb(" + Convert.ToInt32(rojo) + "," + Convert.ToInt32(verde) + "," + Convert.ToInt32(azul) + ");";
    }

    protected void DataPerfiles_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataListItem lbl = e.Item;
        Label lblPerfil = lbl.FindControl("lblPerfilId2") as Label;
        Label lblVehiculos = lbl.FindControl("lblVehiculosInt2") as Label;
        DataList DataLAsg2 = lbl.FindControl("DataLAsg2") as DataList;
                
        int carrosTotales = 0;
        int carrosPerfil = 0;
        try { carrosTotales = Convert.ToInt32(lblSeleccionados.Text); }
        catch (Exception) { carrosTotales = 0; }
        try { carrosPerfil = Convert.ToInt32(lblVehiculos.Text); }
        catch (Exception) { carrosPerfil = 0; }
        lblSeleccionados.Text = (carrosTotales + carrosPerfil).ToString();

        int vehiculos = 0;
        int perfil = 0;
        try
        {
            vehiculos = Convert.ToInt32(lblVehiculos.Text);
            perfil = Convert.ToInt32(lblPerfil.Text);
            HtmlControl indicador = (HtmlControl)e.Item.FindControl("DivPerfil");
            int[] rgbsPerfilR = { 51, 51, 51, 51, 51, 51, 102, 102, 102, 102, 102, 102, 255, 255, 255, 255, 204 };
            int[] rgbsPerfilV = { 0, 51, 102, 153, 204, 255, 255, 204, 153, 102, 51, 0, 0, 51, 102, 153, 204 };
            int[] rgbsPerfilA = { 204, 204, 204, 204, 204, 204, 204, 204, 204, 204, 204, 204, 204, 204, 204, 204, 204 };
            indicador.Attributes["style"] = "background-color:rgb(" + rgbsPerfilR[perfil] + "," + rgbsPerfilV[perfil] + "," + rgbsPerfilA[perfil] + ");";

            if (vehiculos > 0)
            {
                int idEmpresa = Convert.ToInt32(Request.QueryString["e"]);
                int idTaller = Convert.ToInt32(Request.QueryString["t"]);
                DataSet dataSource6 = null;
                dataSource6 = bitPerfiles.obtieneAseguradorasPerfil(idEmpresa, idTaller, perfil);
                DataLAsg2.DataSource = dataSource6;
                DataLAsg2.DataBind();
            }
            else
                DataLAsg2.Visible = false;
        }
        catch (Exception) { }
    }

    protected void DataLAsg2_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        string rojo = DataBinder.Eval(e.Item.DataItem, "rgb_r").ToString();
        string verde = DataBinder.Eval(e.Item.DataItem, "rgb_g").ToString();
        string azul = DataBinder.Eval(e.Item.DataItem, "rgb_b").ToString();
        HtmlControl indicador = (HtmlControl)e.Item.FindControl("cuadro");
        indicador.Attributes["style"] = "background-color:rgb(" + Convert.ToInt32(rojo) + "," + Convert.ToInt32(verde) + "," + Convert.ToInt32(azul) + ");";
    }
}