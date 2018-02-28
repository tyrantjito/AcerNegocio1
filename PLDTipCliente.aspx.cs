using System;
using System.Web.UI;
using System.Data;

public partial class PLDTipCliente : System.Web.UI.Page
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
        try
        {
            string script = "abreModEmi()";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abre", script, true);
            PLDTCliente edt = new PLDTCliente();
            string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_tcliente"]);
            edt.codigo = codigo;
            edt.obtienepldEdos();

            if (Convert.ToBoolean(edt.retorno[0]))
            {
                DataSet ds1 = (DataSet)edt.retorno[1];

                foreach (DataRow r1 in ds1.Tables[0].Rows)
                {
                    txtCodEdoEdt.Text = r1[0].ToString();
                    txtNameEdoEdt.Text = r1[1].ToString();
                    txtCodEdoEdt.Enabled = false;
                }
            }
        }
        catch (Exception) { }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLDTCliente edt = new PLDTCliente();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_tcliente"]);
        edt.codigo = codigo;
        edt.eliminaPldEdo();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDTCliente agr = new PLDTCliente();
        agr.codigo = txtCodEdoEdt.Text;
        agr.nombEdo = txtNameEdoEdt.Text;
        agr.editapldedo();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDTCliente agr = new PLDTCliente();
        //agr.codigo = txtPldEdoAdd.Text;
        agr.nombEdo = txtNameEdoAdd.Text;
        agr.agregarpldEdo();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}