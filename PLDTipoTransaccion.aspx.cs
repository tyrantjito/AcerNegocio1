using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDTipoTransaccion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e){ }

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
        PLDTransaccion edt = new PLDTransaccion();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_ttransaccion"]);
        edt._Codigo = codigo;
        edt.obtiene_Transaccion();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtTopeEdit.Text = r1[0].ToString();
                txtCodTransaccionEdit.Text = r1[1].ToString();
                txtNomTransaccionEdit.Text = r1[2].ToString();
                txtCodTransaccionEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLDTransaccion edt = new PLDTransaccion();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_ttransaccion"]);
        edt._Codigo = codigo;
        edt.elimina_Transaccion();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDTransaccion agr = new PLDTransaccion();
        agr._Codigo = txtCodTransaccionEdit.Text;
        agr._Nombre = txtNomTransaccionEdit.Text;
        agr._ID = Convert.ToInt32(txtTopeEdit.Text);
        agr.edita_Transaccion();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDTransaccion agr = new PLDTransaccion();
        agr._Codigo = txtCodTransaccionAdd.Text;
        agr._Nombre = txtNomTransaccionAdd.Text;
        agr._ID = Convert.ToInt32(txtTopeAdd.Text);
        agr.agregar_Transaccion();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}