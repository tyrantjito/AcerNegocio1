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
using E_Utilities;

public partial class BitacoraRegistroPintura : System.Web.UI.Page
{
    Fechas fechas = new Fechas();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            txtFechaIni.Text = fechas.obtieneFechaLocal().AddDays(-30).ToString("yyyy-MM-dd");
            txtFechaFin.Text = fechas.obtieneFechaLocal().ToString("yyyy-MM-dd");
            rbdEstatus.SelectedValue = "S";
        }
    }
    
    protected void btnOrden_Click(object sender, EventArgs e)
    {
        LinkButton lknReferencia = (LinkButton)sender;
        int orden = Convert.ToInt32(lknReferencia.Text);
        int fase = Convert.ToInt32(lknReferencia.CommandArgument.ToString());
        Response.Redirect("BienvenidaOrdenes.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"] + "&o=" + orden + "&f=" + fase);
    }
    
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType==GridItemType.AlternatingItem)
        {
            try
            {
                DateTime fechaRecp = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "fecha_entrega").ToString());
                DateTime fechaNula = Convert.ToDateTime("1900-01-01");
                if (fechaRecp > fechaNula)
                    e.Item.CssClass = "alert-warning";
            }
            catch (Exception ex)
            {
                e.Item.CssClass = "";
            }
        }
    }

    protected void lnkBuscar_Click(object sender, EventArgs e)
    {
        RadGrid1.DataBind();
    }
}