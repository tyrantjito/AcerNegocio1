using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDTipoPep : System.Web.UI.Page
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
        PLDtPep edt = new PLDtPep();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_tpep"]);
        edt._CodiPep= codigo;
        edt.obtiene_Pep();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtEscalaEdit.Text = r1[0].ToString();
                txtCodPepEdit.Text = r1[1].ToString();
                txtNomPepEdit.Text = r1[2].ToString();
                txtCodPepEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLDtPep edt = new PLDtPep();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_tpep"]);
        edt._CodiPep = codigo;
        edt.elimina_Pep();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDtPep agr = new PLDtPep();
        agr._Escala = Convert.ToInt32(txtEscalaEdit.Text);
        agr._CodiPep = txtCodPepEdit.Text;
        agr._NombPep = txtNomPepEdit.Text;
        agr.edita_Pep();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDtPep agr = new PLDtPep();
        agr._Escala = Convert.ToInt32(txtEscalaAdd.Text);
        agr._CodiPep = txtCodPepAdd.Text;
        agr._NombPep = txtNomPepAdd.Text;
        agr.agregar_Pep();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}