using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDEscolaridad : System.Web.UI.Page
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
        PLDnEducacion edt = new PLDnEducacion();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_neducacion"]);
        edt.codigoEducacion = codigo;
        edt.obtieneEducacion();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtCodEdoEdt.Text = r1[0].ToString();
                txtNameEdoEdt.Text = r1[1].ToString();
                txtCodEdoEdt.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLDnEducacion edt = new PLDnEducacion();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_neducacion"]);
        edt.codigoEducacion = codigo;
        edt.eliminaEducacion();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDnEducacion agr = new PLDnEducacion();
        agr.codigoEducacion = txtCodEdoEdt.Text;
        agr.nombEducacion = txtNameEdoEdt.Text;
        agr.editaEducacion();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDnEducacion agr = new PLDnEducacion();
        agr.codigoEducacion = txtPldEdoAdd.Text;
        agr.nombEducacion = txtNameEdoAdd.Text;
        agr.agregarEducacion();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}