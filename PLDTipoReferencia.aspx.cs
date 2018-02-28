using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDTipoReferencia : System.Web.UI.Page
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
        PLDtReferencia edt = new PLDtReferencia();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_treferencia"]);
        edt.codigotReferencia= codigo;
        edt.obtienetReferencia();

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
        PLDtReferencia edt = new PLDtReferencia();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_treferencia"]);
        edt.codigotReferencia = codigo;
        edt.eliminaPldtReferencia();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDtReferencia agr = new PLDtReferencia();
        agr.codigotReferencia= txtCodEdoEdt.Text;
        agr.nombtReferencia = txtNameEdoEdt.Text;
        agr.editapldtReferencia();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDtReferencia agr = new PLDtReferencia();
        agr.codigotReferencia= txtPldEdoAdd.Text;
        agr.nombtReferencia = txtNameEdoAdd.Text;
        agr.agregarpldtReferencia();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}