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
public partial class asignaAnalista : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
   

    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAgregar.Visible = true;

    }

  

    protected void lnkAgregarNuevo_Click(object sender, EventArgs e)
    {
        CatBanc agr = new CatBanc();
       // agr.codigo = txtClaveMon.Text;
       // agr.Descripcion = txtDes.Text;
        agr.agregarMoneda();
   
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        AsigAna agr = new AsigAna();
        agr.empresa = sesiones[2];
        agr.sucursal = sesiones[3];
        agr.Grupo = sesiones[4];
        agr.analista = Convert.ToInt32( cmb_analista.SelectedValue);
        agr.nombregrupo = txtGrupo.Text;
        //agr.Grupo = Convert.ToInt32( txtnumeroGrupo.Text);
        agr.credito= sesiones[4];
        agr.editaAnalistaCred();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
    private int[] obtieneSesiones()
    {
        int[] sesiones = new int[5];
        sesiones[0] = Convert.ToInt32(Request.QueryString["u"]);
        sesiones[1] = Convert.ToInt32(Request.QueryString["p"]);
        sesiones[2] = Convert.ToInt32(Request.QueryString["e"]);
        sesiones[3] = Convert.ToInt32(Request.QueryString["t"]);
        sesiones[4] = Convert.ToInt32(Request.QueryString["c"]);
        return sesiones;
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        AsigAna obt = new AsigAna();
        obt.empresa = sesiones[2];
        obt.sucursal = sesiones[3];
        obt.Grupo = sesiones[4];
        obt.obtienePuestoEdit();
        if (Convert.ToBoolean(obt.retorno[0]) == true)
        {
            DataSet ds = (DataSet)obt.retorno[1];

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txtGrupo.Text = r[3].ToString();
                txtnumeroGrupo.Text = r[4].ToString();
            }
        }


   }
}