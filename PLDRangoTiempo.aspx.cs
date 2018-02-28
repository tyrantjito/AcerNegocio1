using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDRangoTiempo : System.Web.UI.Page
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
        PLDrTiempo edt = new PLDrTiempo();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_rtiempo"]);
        edt.Codigo_Tiempo = codigo;
                //Solo recuperando el codigo recupera los demas campos
                //edt.Id_Escala = Convert.ToInt32(txtEscalaTiempoEdit.Text);
                //edt._Inicial = Convert.ToInt32(txtRangoInicialEdit.Text);
                //edt._Final = Convert.ToInt32(txtRangoFinalEdit.Text);
                //string t = txtTipoEdit.Text;
                //edt._tipo = t[0];
        edt.obtieneTiempo();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtEscalaTiempoEdit.Text = r1[0].ToString();
                txtCodRangoTiempoEdit.Text = r1[1].ToString();
                txtRangoInicialEdit.Text = r1[2].ToString();
                txtRangoFinalEdit.Text = r1[3].ToString();
                txtTipoEdit.Text = r1[4].ToString();
                txtCodRangoTiempoEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLDrTiempo edt = new PLDrTiempo();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_rtiempo"]);
        edt.Codigo_Tiempo = codigo;
        edt.eliminaTiempo();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDrTiempo agr = new PLDrTiempo();
        agr.Codigo_Tiempo = txtCodRangoTiempoEdit.Text;
        agr.Id_Escala = Convert.ToInt32(txtEscalaTiempoEdit.Text);
        agr._Inicial = Convert.ToInt32(txtRangoInicialEdit.Text);
        agr._Final = Convert.ToInt32(txtRangoFinalEdit.Text);
        string t = txtTipoEdit.Text;
        agr._tipo = t[0];
        agr.editaTiempo();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDrTiempo agr = new PLDrTiempo();
        agr.Codigo_Tiempo = txtCodRangoTiempoAdd.Text;
        agr.Id_Escala= Convert.ToInt32(txtIdEscalaAdd.Text);
        agr._Inicial = Convert.ToInt32(txtRInicialAdd.Text);
        agr._Final = Convert.ToInt32(txtRFinalAdd.Text);
        string tipo = txtTipoAdd.Text;
        agr._tipo = tipo[0];
        agr.agregarTiempo();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}