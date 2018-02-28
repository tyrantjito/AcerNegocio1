using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLD_Productos : System.Web.UI.Page
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
        PLD_Producto edt = new PLD_Producto();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_producto"]);
        edt._CodProducto = codigo;
        edt.obtiene_Producto();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtIDProductoEdit.Text = r1[0].ToString();
                txtEscalaEdit.Text = r1[1].ToString();
                txtCodProductoEdit.Text= r1[2].ToString();
                txtNomProductoEdit.Text = r1[3].ToString();
                txtCodProductoEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLD_Producto edt = new PLD_Producto();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_producto"]);
        edt._CodProducto = codigo;
        edt.elimina_Producto();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLD_Producto agr = new PLD_Producto();
        agr._CodProducto = txtCodProductoEdit.Text;
        agr._Escala = Convert.ToInt32(txtEscalaEdit.Text);
        agr._NomProducto = txtNomProductoEdit.Text;
        agr._ID = Convert.ToInt32(txtIDProductoEdit.Text);
         agr.edita_Producto();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLD_Producto agr = new PLD_Producto();
        //agr._CodProducto = txtCodProductoAdd.Text;
        agr._Escala = Convert.ToInt32(txtEscalaAdd.Text);
        agr._NomProducto = txtNomProductoAdd.Text;
        agr._ID = Convert.ToInt32(txtIDProductoAdd.Text);
        agr.agregar_Producto();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}