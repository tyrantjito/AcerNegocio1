using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDNacionalidad : System.Web.UI.Page
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
        PLDNacion edt = new PLDNacion();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_nacionalidad"]);
        edt._CodigoNacion = codigo;
        edt.obtieneNacion();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtEscalaEdit.Text = r1[0].ToString();
                txtCodNacionalidadEdit.Text = r1[1].ToString();
                txtNacionalidadEdit.Text = r1[2].ToString();
                txtNombrePaisEdit.Text = r1[3].ToString();
                txtCodNacionalidadEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLDNacion edt = new PLDNacion();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_nacionalidad"]);
        edt._CodigoNacion = codigo;
        edt.eliminaNacion();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDNacion agr = new PLDNacion();
        agr._CodigoNacion = txtCodNacionalidadEdit.Text;
        agr._Escala = Convert.ToInt32(txtEscalaEdit.Text);
        agr._Nacionalidad = txtNacionalidadEdit.Text;
        agr._Nombre = txtNombrePaisEdit.Text;
        agr.editaNacion();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDNacion agr = new PLDNacion();
        agr._CodigoNacion = txtCodNacionalidadAdd.Text;
        agr._Escala = Convert.ToInt32(txtIdEscalaAdd.Text);
        agr._Nacionalidad = txtNacionalidadAdd.Text;
        agr._Nombre = txtNombrePaisAdd.Text;
        agr.agregarNacion();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}