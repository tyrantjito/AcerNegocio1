using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PantallasPatioMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
            cargaInfo();
    }

    private void cargaInfo()
    {
        int opcion = 0;
        try
        {
            opcion = Convert.ToInt32(rbtnPantallas.SelectedValue);
            if (opcion == 2)
            {
                ddlGop.Visible = true;
                ddlGop.DataBind();
            }
            else
                ddlGop.Visible = false;
        }
        catch (Exception ex)
        {
            ddlGop.Visible = false;
        }
    }

    protected void rbtnPantallas_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargaInfo();
    }

    protected void lnkVerPantalla_Click(object sender, EventArgs e)
    {
        int idGOP = -1;
        if (rbtnPantallas.SelectedValue != "2")
            idGOP = -1;
        else
            idGOP = Convert.ToInt32(ddlGop.SelectedValue);
        try
        {
            int op = Convert.ToInt32(rbtnPantallas.SelectedValue);
            string url = ConfigurationManager.AppSettings["direccion"].ToString() + "/PantallaPatio2.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"] + "&gop=" + idGOP.ToString() + "&op=" + op.ToString();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            lblError.Text = "";
        }
        catch (Exception ex)
        {
            lblError.Text = "Debe seleccionar una opción para generar la vista a patio";
        }
    }

    protected void lnkRegresarOrdenes_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ordenes.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
}