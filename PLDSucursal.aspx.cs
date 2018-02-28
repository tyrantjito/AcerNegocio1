using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDSucursal : System.Web.UI.Page
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
        PLD_Sucursal edt = new PLD_Sucursal();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_sucursal"]);
        edt._CodigoSucursal = codigo;
        edt.obtiene_Sucursal();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtRegionEdit.Text = r1[0].ToString();
                txtEscalaEdit.Text = r1[1].ToString();
                txtCodSucursalEdit.Text = r1[2].ToString();
                txtNomSucursalEdit.Text = r1[3].ToString();
                txtCodSucursalEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLD_Sucursal edt = new PLD_Sucursal();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_sucursal"]);
        edt._CodigoSucursal = codigo;
        edt.elimina_Sucursal();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLD_Sucursal agr = new PLD_Sucursal();
        agr._Regional = Convert.ToInt32(txtRegionEdit.Text);
        agr._Escala = Convert.ToInt32(txtEscalaEdit.Text);
        agr._CodigoSucursal = txtCodSucursalEdit.Text;
        agr._NombreSucursal = txtNomSucursalEdit.Text;
        agr.edita_Sucursal();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLD_Sucursal agr = new PLD_Sucursal();
        //agr._CodProducto = txtCodProductoAdd.Text;
        agr._Regional = Convert.ToInt32(txtRegionAdd.Text);
        agr._Escala = Convert.ToInt32(txtEscalaAdd.Text);
        agr._CodigoSucursal = txtCodSucursalAdd.Text;
        agr._NombreSucursal = txtNomSucursalAdd.Text;
        agr.agregar_Sucursal();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}