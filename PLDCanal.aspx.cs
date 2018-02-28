using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDCanal : System.Web.UI.Page
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
        PLDtCanal edt = new PLDtCanal();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_canal"]);
        edt._CodigoCanal = codigo;
        edt.obtieneCanal();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtEscalaEdit.Text = r1[0].ToString();
                txtCodCanalEdit.Text = r1[1].ToString();
                txtNomCanalEdit.Text = r1[2].ToString();
                txtCodCanalEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLDtCanal edt = new PLDtCanal();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_canal"]);
        edt._CodigoCanal = codigo;
        edt.eliminaCanal();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDtCanal agr = new PLDtCanal();
        agr._CodigoCanal = txtCodCanalEdit.Text;
        agr._Escala = Convert.ToInt32(txtEscalaEdit.Text);
        agr._Nombre = txtNomCanalEdit.Text;
        agr.editaCanal();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDtCanal agr = new PLDtCanal();
        agr._CodigoCanal = txtCodCanaldAdd.Text;
        agr._Escala = Convert.ToInt32(txtIdEscalaAdd.Text);
        agr._Nombre = txtNomCanalAdd.Text;
        agr.agregarCanal();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}