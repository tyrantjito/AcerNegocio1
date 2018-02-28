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


public partial class catValidaciones : System.Web.UI.Page
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
        CatVal edt = new CatVal();

        int codigo = Convert.ToInt32(RadGrid1.SelectedValues["Id_validacion"]);
        edt.codigo = codigo;
        edt.obtieneValidacionEdit();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtValEd.Text = r1[0].ToString();
                txtDescriEd.Text = r1[1].ToString();
                txtEstatusEd.Text = r1[2].ToString();
            }
        }
        txtValEd.Enabled = false;
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        CatVal edt = new CatVal();
        int codigo = Convert.ToInt32(RadGrid1.SelectedValues["Id_validacion"]);
        edt.codigo = codigo;
        edt.eliminaValidaciones();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void lnkAgregarNuevo_Click(object sender, EventArgs e)
    {

        CatVal agr = new CatVal();
      //  agr.codigo = Convert.ToInt32( txtIdVal.Text);
        agr.descripcion = txtDescri.Text;
        agr.estatus = txtEstatus.Text;
        agr.agregarValidacion();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        CatVal agr = new CatVal();
        int codigo = Convert.ToInt32(RadGrid1.SelectedValues["Id_validacion"]);
        agr.codigo = codigo;
        agr.descripcion = txtDescriEd.Text;
        agr.estatus = txtEstatusEd.Text;
        agr.editaValidacion();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        string script = "abreNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abre", script, true);
        txtIdVal.Visible = false;
        Label4.Visible = false;
    }
}