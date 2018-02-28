using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDTipoLista : System.Web.UI.Page
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
        PLDtLista edt = new PLDtLista();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_tlista"]);
        edt._Codigo = codigo;
        edt.obtiene_Lista();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtEscalaEdit.Text = r1[0].ToString();
                txtCodListaEdit.Text = r1[1].ToString();
                txtNomListaEdit.Text = r1[2].ToString();
                txtCodListaEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLDtLista edt = new PLDtLista();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_tlista"]);
        edt._Codigo = codigo;
        edt.elimina_Lista();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDtLista agr = new PLDtLista();
        agr._Escala = Convert.ToInt32(txtEscalaEdit.Text);
        agr._Codigo = txtCodListaEdit.Text;
        agr._Nombre = txtNomListaEdit.Text;
        agr.edita_Lista();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDtLista agr = new PLDtLista();
        agr._Escala = Convert.ToInt32(txtEscalaAdd.Text);
        agr._Codigo = txtCodListaAdd.Text;
        agr._Nombre = txtNomListaAdd.Text;
        agr.agregar_Lista();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}