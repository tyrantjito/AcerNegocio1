using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDOrigenFondo : System.Web.UI.Page
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
        PLDoFondo edt = new PLDoFondo();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_ofondo"]);
        edt._CodigoFondo = codigo;
        edt.obtiene_Fondo();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtIDFondoEdit.Text = r1[0].ToString();
                txtNombreFondoEdit.Text = r1[1].ToString();
                txtIDFondoEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLDoFondo edt = new PLDoFondo();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_ofondo"]);
        edt._CodigoFondo = codigo;
        edt.elimina_Fondo();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDoFondo agr = new PLDoFondo();
        agr._CodigoFondo = txtIDFondoEdit.Text;
        agr._NombreFondo = txtNombreFondoEdit.Text;
        agr.edita_Fondo();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDoFondo agr = new PLDoFondo();
        //agr._CodProducto = txtCodProductoAdd.Text;
        agr._NombreFondo = txtNombreFondoAdd.Text;
        agr._CodigoFondo = txtIDFondoAdd.Text;
        agr.agregar_Fondo();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}