using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Telerik.Web.UI;
using E_Utilities;

public partial class PLDMoneda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        /*string script = "abreModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abre", script, true);
        PLDMon edt = new PLDMon();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_moneda"]);
        edt.codigo = codigo;
        edt.obtienepldMon();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtCodMonEdt.Text = r1[0].ToString();
                txtNameMonEdt.Text = r1[1].ToString();
                txtSiglEdt.Text = r1[2].ToString();
                txtCodMonEdt.Enabled = false;
            }
        }*/
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
       /* PLDMon edt = new PLDMon();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_moneda"]);
        edt.codigo = codigo;
        edt.eliminaPldMon();
        RadGrid1.DataBind();*/
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        /*PLDMon agr = new PLDMon();
        agr.codigo = txtCodMonEdt.Text;
        agr.nomMoneda = txtNameMonEdt.Text;
        agr.siglas = txtSiglEdt.Text;
        agr.editapldmoneda();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);*/
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        /*PLDMon agr = new PLDMon();
        agr.codigo = txtPldMonAdd.Text;
        agr.nomMoneda = txtNameMonAdd.Text;
        agr.siglas = txtSiglAdd.Text;
        agr.agregarpldMon();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);*/
    }

    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*.Visible = true;
        btnEditar.Visible = true;
        btnEliminar.Visible = true;
        btnSelec.Visible = true;*/
    }
}