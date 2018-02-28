using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDActividadEconomica : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAgregar.Visible = true;
        btnEditar.Visible = true;
        btnEliminar.Visible = true;
        btnSelec.Visible = true;
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        string script = "abreModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abre", script, true);
        PLDEconomico edt = new PLDEconomico();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_actividad"]);
        edt._CodigoEconomico = codigo;
        edt.obtieneEconomico();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtEscalaEdit.Text = r1[0].ToString();
                txtCodEconomicoEdit.Text = r1[1].ToString();
                txtNomEconomicodEdit.Text = r1[2].ToString();
                txtCodEconomicoEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLDEconomico edt = new PLDEconomico();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_actividad"]);
        edt._CodigoEconomico = codigo;
        edt.eliminaEconomico();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDEconomico agr = new PLDEconomico();
        agr._CodigoEconomico = txtCodEconomicoEdit.Text;
        agr._Escala = Convert.ToInt32(txtEscalaEdit.Text);
        agr._Nombre = txtNomEconomicodEdit.Text;
        agr.editaEconomico();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDEconomico agr = new PLDEconomico();
        agr._CodigoEconomico = txtCodEconomicodAdd.Text;
        agr._Escala = Convert.ToInt32(txtIdEscalaAdd.Text);
        agr._Nombre= txtNomEconomicoAdd.Text;
        agr.agregarEconomico();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}