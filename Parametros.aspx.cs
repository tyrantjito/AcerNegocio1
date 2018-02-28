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

public partial class Parametros : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
    protected void btnEditar_Click(object sender, EventArgs e)
    {
        string script = "abreModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abre", script, true);


        Parame edt = new Parame();
        int[] sesiones = obtieneSesiones();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["id_empresa"]);
        edt.codigo = codigo;
        edt.sucursal = sesiones[3];
        edt.empresa = sesiones[2];
        edt.obtieneParaEdit();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtNameCortEd.Text= r1[2].ToString();
                txtEmpreEd.Text = r1[3].ToString();
                txtDirEd.Text = r1[4].ToString();
                txtCorreoEd.Text = r1[5].ToString();
                txtTelEd.Text = r1[6].ToString();
                txtRfcEd.Text = r1[7].ToString();
                txtPagWebEd.Text = r1[8].ToString();
                txtRepreEd.Text = r1[9].ToString();
            }
        }

    }
    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        Parame agr = new Parame();
        int[] sesiones = obtieneSesiones();
        agr.nombreCorto = txtNameCortEd.Text;
        agr.nombreCompleto = txtEmpreEd.Text;
        agr.direccionEmp = txtDirEd.Text;
        agr.correoEmp = txtCorreoEd.Text;
        agr.teleEmp = txtTelEd.Text;
        agr.rfcEmp = txtRfcEd.Text;
        agr.pagWeb = txtPagWebEd.Text;
        agr.represen = txtRepreEd.Text;
        agr.sucursal = sesiones[3];
        agr.empresa = sesiones[2];
        agr.editaParamentro();

        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnEditar.Visible = true;
    }
}