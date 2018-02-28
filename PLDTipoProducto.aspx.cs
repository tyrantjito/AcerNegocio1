using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDTipoProducto : System.Web.UI.Page
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
        PLDtProducto edt = new PLDtProducto();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["tpro_id"]);
        edt._ID = codigo;
        edt.obtienetProducto();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows){
                txtCodProductoEdit.Text = r1[0].ToString();
                txtClasificacionEdit.Text = r1[1].ToString();
                txtNomProductoEdit.Text = r1[2].ToString();
                txtCodProductoEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLDtProducto edt = new PLDtProducto();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["tpro_id"]);
        edt._ID = codigo;
        edt.eliminatProducto();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDtProducto agr = new PLDtProducto();
        agr._ID = txtCodProductoEdit.Text;
        agr._Clasificacion = Convert.ToInt32(txtClasificacionEdit.Text);
        agr._Nombre = txtNomProductoEdit.Text;
        agr.editatProducto();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDtProducto agr = new PLDtProducto();
        agr._ID = txtCodProductoAdd.Text;
        agr._Clasificacion = Convert.ToInt32(txtClasificacionAdd.Text);
        agr._Nombre = txtNomProductoAdd.Text;
        agr.agregartProducto();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}