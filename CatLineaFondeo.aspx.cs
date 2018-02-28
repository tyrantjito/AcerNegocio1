using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Telerik.Web.UI;
using E_Utilities;
public partial class CatLineaFondeo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        string script = "abreModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abre", script, true);
        CatFondeo edt = new CatFondeo();

        string codigo = Convert.ToString(RadGrid1.SelectedValues["id_lineafondeo"]);
        edt.codigo = codigo;
        edt.obtieneFondeoEdit();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtLineaFondeoEd.Text = r1[0].ToString();
                txtMontEd.Text = r1[1].ToString();
                txtFonEd.Text = r1[2].ToString();
                txtPlazoEd.Text = r1[3].ToString();
                txtPerPagoEd.Text = r1[4].ToString();
                txtTipoTasaEd.Text = r1[5].ToString();
                txtMontoTasaEd.Text = r1[6].ToString();
                txtDestCreEd.Text = r1[7].ToString();
                txtGarantiaEd.Text = r1[8].ToString();
                txtFechaFondEd.SelectedDate =Convert.ToDateTime( r1[9].ToString());


            }
        }
        txtLineaFondeoEd.Enabled = false;
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        CatFondeo edt = new CatFondeo();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["id_lineafondeo"]);
        edt.codigo = codigo;
        edt.eliminaLineaFondeo();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAgregar.Visible = true;
        btnEditar.Visible = true;
        btnEliminar.Visible = true;
        btnSelec.Visible = true;
    }

    protected void lnkAgregarNuevo_Click(object sender, EventArgs e)
    {
        CatFondeo agr = new CatFondeo();
        agr.codigo = txtLineaFonde.Text;
        agr.monto =Convert.ToInt32( txtMonto.Text);
        agr.fondeador = txtFendeador.Text;
        agr.plazo = txtPlazo.Text;
        agr.periodopago = txtPeriPago.Text;
        agr.tipotasa = txtTipTasa.Text;
        agr.montotasa = Convert.ToInt32(txtMonTasa.Text);
        agr.descriCre = txtDesCre.Text;
        agr.garantia = txtGarant.Text;
        DateTime fechaF = Convert.ToDateTime(txtFechaFon.SelectedDate);
        agr.fecha =  fechaF.ToString("yyyy/MM/dd");
        agr.agregarLienaFondeo();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        CatFondeo agr = new CatFondeo();
        agr.codigo = txtLineaFondeoEd.Text;
        agr.monto = Convert.ToInt32(txtMontEd.Text);
        agr.fondeador = txtFonEd.Text;
        agr.plazo = txtPlazoEd.Text;
        agr.periodopago = txtPerPagoEd.Text;
        agr.tipotasa = txtTipoTasaEd.Text;
        agr.montotasa = Convert.ToInt32(txtMontoTasaEd.Text);
        agr.descriCre = txtDestCreEd.Text;
        agr.garantia = txtGarantiaEd.Text;
        DateTime fechaFEd = Convert.ToDateTime(txtFechaFondEd.SelectedDate);
        agr.fecha = fechaFEd.ToString("yyyy/MM/dd");
        agr.editaLineaFondeo();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        string script = "abreNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abre", script, true);
        Label4.Visible = false;
        txtLineaFonde.Visible = false;
    }
}