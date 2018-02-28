using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDPropositoCuenta : System.Web.UI.Page
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
        PLDpCuenta edt = new PLDpCuenta();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_pproducto"]);
        edt._CodigoCuenta = codigo;
        edt.obtiene_Cuenta();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtIDProductoEdit.Text = r1[0].ToString();
                txtNombreCuentaEdit.Text = r1[1].ToString();
                txtIDProductoEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLDpCuenta edt = new PLDpCuenta();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_pproducto"]);
        edt._CodigoCuenta = codigo;
        edt.elimina_Cuenta();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDpCuenta agr = new PLDpCuenta();
        agr._CodigoCuenta = txtIDProductoEdit.Text;
        agr._NombreCuenta = txtNombreCuentaEdit.Text;
        agr.edita_Cuenta();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDpCuenta agr = new PLDpCuenta();
        //agr._CodProducto = txtCodProductoAdd.Text;
        agr._NombreCuenta = txtNombreCuentaAdd.Text;
        agr._CodigoCuenta = txtIDProductoAdd.Text;
        agr.agregar_Cuenta();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}