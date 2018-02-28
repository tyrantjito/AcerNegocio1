using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PLDrIngresos : System.Web.UI.Page
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
        PLDrIngreso edt = new PLDrIngreso();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_ringreso"]);
        edt.Codigo_Ingreso = codigo;
        edt.obtieneTiempo();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtEscalaIngresoEdit.Text = r1[0].ToString();
                txtCodRangoIngresoEdit.Text = r1[1].ToString();
                txtRangoInicialEdit.Text = r1[2].ToString();
                txtRangoFinalEdit.Text = r1[3].ToString();
                txtTipoEdit.Text = r1[4].ToString();
                txtCodRangoIngresoEdit.Enabled = false;
            }
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PLDrIngreso edt = new PLDrIngreso();
        string codigo = Convert.ToString(RadGrid1.SelectedValues["codigo_ringreso"]);
        edt.Codigo_Ingreso = codigo;
        edt.eliminaTiempo();
        RadGrid1.DataBind();
    }

    protected void btnSelec_Click(object sender, EventArgs e)
    {

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        PLDrIngreso agr = new PLDrIngreso();
        agr.Codigo_Ingreso = txtCodRangoIngresoEdit.Text;
        agr.Id_Escala = Convert.ToInt32(txtEscalaIngresoEdit.Text);
        agr._Inicial = Convert.ToDecimal(txtRangoInicialEdit.Text);   //es decimal
        agr._Final = Convert.ToDecimal(txtRangoFinalEdit.Text);       //es decimal
        //string t = txtTipoEdit.Text;
        agr._tipo = (txtTipoEdit.Text)[0];
        agr.editaTiempo();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        PLDrIngreso agr = new PLDrIngreso();
        agr.Codigo_Ingreso = txtCodRangoIngresoAdd.Text;
        agr.Id_Escala = Convert.ToInt32(txtIdEscalaAdd.Text);
        agr._Inicial = Convert.ToDecimal(txtRInicialAdd.Text);    //es decimal
        agr._Final = Convert.ToDecimal(txtRFinalAdd.Text);        //es decimal
        //string tipo = txtTipoAdd.Text;
        agr._tipo = txtTipoAdd.Text[0];
        agr.agregarTiempo();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}