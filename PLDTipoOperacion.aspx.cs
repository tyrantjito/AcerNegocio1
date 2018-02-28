using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDTipoOperacion : System.Web.UI.Page
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
        PLDTOperacion edt = new PLDTOperacion();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_toperacion"]);
        edt._Codigo = codigo;
        edt.obtiene_Operacion();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtCodOperacionEdit.Text = r1[0].ToString();
                txtNomOperacionEdit.Text = r1[1].ToString();
                txtCodOperacionEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLDTOperacion edt = new PLDTOperacion();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_toperacion"]);
        edt._Codigo = codigo;
        edt.elimina_Operacion();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDTOperacion agr = new PLDTOperacion();
        agr._Codigo = txtCodOperacionEdit.Text;
        agr._Nombre = txtNomOperacionEdit.Text;
        agr.edita_Operacion();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDTOperacion agr = new PLDTOperacion();
        agr._Codigo = txtCodOperacionAdd.Text;
        agr._Nombre = txtNomOperacionAdd.Text;
        agr.agregar_Operacion();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}