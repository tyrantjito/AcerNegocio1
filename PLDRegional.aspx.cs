using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDRegional : System.Web.UI.Page
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
        PLD_Regional edt = new PLD_Regional();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["reg_id"]);
        edt._IDRegional = codigo;
        edt.obtiene_Regional();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtIDRegionEdit.Text = r1[0].ToString();
                txtNombreRegionEdit.Text = r1[1].ToString();
                txtIDRegionEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLD_Regional edt = new PLD_Regional();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["reg_id"]);
        edt._IDRegional = codigo;
        edt.elimina_Regional();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLD_Regional agr = new PLD_Regional();
        agr._IDRegional = txtIDRegionEdit.Text;
        agr._NombreRegional = txtNombreRegionEdit.Text;
        agr.edita_Regional();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLD_Regional agr = new PLD_Regional();
        //agr._CodProducto = txtCodProductoAdd.Text;
        agr._NombreRegional = txtNombreRegionAdd.Text;
        agr._IDRegional = txtIDRegionAdd.Text;
        agr.agregar_Regional();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}