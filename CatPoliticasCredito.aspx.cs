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

public partial class CatPoliticasCredito : System.Web.UI.Page
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
        CatPoliCre edt = new CatPoliCre();

        string codigo = Convert.ToString(RadGrid1.SelectedValues["Id_politica"]);
        edt.codigo = codigo;
        edt.obtienePoliticasEdit();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtPoliEd.Text = r1[0].ToString();
                txtDescriEd.Text = r1[1].ToString();
                txtEstatusEd.Text = r1[2].ToString();
            }
        }
        txtPoliEd.Enabled = false;
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        CatPoliCre edt = new CatPoliCre();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["Id_politica"]);
        edt.codigo = codigo;
        edt.eliminaPolitica();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void lnkAgregarNuevo_Click(object sender, EventArgs e)
    {
        
        CatPoliCre agr = new CatPoliCre();
        agr.codigo = txtIdPoli.Text;
        agr.descripcion = txtDescrip.Text;
        agr.estatus = txtEstatus.Text;
        agr.agregarPolitica();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        CatPoliCre agr = new CatPoliCre();
        agr.codigo = txtPoliEd.Text;
        agr.descripcion = txtDescriEd.Text;
        agr.estatus = txtEstatusEd.Text;
        agr.editaLineaFondeo();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        string script = "abreNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abre", script, true);
        txtIdPoli.Visible = false;
        Label4.Visible = false;
    }
}