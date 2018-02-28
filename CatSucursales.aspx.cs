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

public partial class CatSucursales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        string script = "abreModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abre", script, true);
        CatSuc edt = new CatSuc();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["id_sucursal"]);
        edt.codigo = codigo;
        edt.obtieneservEdit();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtIdSucEdt.Text = r1[0].ToString();
                txtNamSucEdt.Text = r1[1].ToString();
                txtIdentiEdt.Text = r1[2].ToString();
                txtRegEdt.Text = r1[3].ToString();
                txtIdSucEdt.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        CatSuc edt = new CatSuc();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["id_sucursal"]);
        edt.codigo = codigo;
        edt.eliminaSucursal();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAgregar.Visible = true;
        btnEditar.Visible = true;
        btnEliminar.Visible = true;
        btnSelec.Visible = true;
    }



    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        CatSuc agr = new CatSuc();
        agr.codigo = txtIdSucAdd.Text;

        agr.nomSuc = txtNameSucAdd.Text;
        agr.identificador = txtIdentAdd.Text;
        agr.regional = txtRegAdd.Text;
        agr.agregarServicio();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        CatSuc agr = new CatSuc();
        agr.codigo = txtIdSucEdt.Text;
        agr.nomSuc = txtNamSucEdt.Text;
        agr.identificador = txtIdentiEdt.Text;
        agr.regional = txtRegEdt.Text;
        agr.editaSucursal();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}