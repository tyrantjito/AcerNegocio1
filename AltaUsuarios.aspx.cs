using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AltaUsuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        string script = "abreModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abre", script, true);
        AlUsuarios edt = new AlUsuarios();
        int codigo = Convert.ToInt32(RadGrid1.SelectedValues["id_usuario"]);
        edt.id = codigo;
        edt.obtienePuestoEdit();

        if (Convert.ToBoolean(edt.retorno[0]))
        {
            DataSet ds1 = (DataSet)edt.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                txtUsuarioEDT.Text = r1[1].ToString();
                txtContraEDT.Text = r1[2].ToString();
                txtNombreEDT.Text = r1[3].ToString();
                SqlDataSource2.SelectCommand = "select * from an_Puestos";
                cmbPuestoEDT.DataBind();
            }
        }

    }

    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAgregar.Visible = true;
        btnEditar.Visible = true;
        btnEliminar.Visible = true;
        btnSelec.Visible = true;
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        AlUsuarios edt = new AlUsuarios();
        int codigo = Convert.ToInt32(RadGrid1.SelectedValues["id_usuario"]);
        edt.id = codigo;
        edt.eliminaPuesto();
        RadGrid1.DataBind();
    }

    private int[] obtieneSesiones()
    {
        int[] sesiones = new int[4];
        sesiones[0] = Convert.ToInt32(Request.QueryString["u"]);
        sesiones[1] = Convert.ToInt32(Request.QueryString["p"]);
        sesiones[2] = Convert.ToInt32(Request.QueryString["e"]);
        sesiones[3] = Convert.ToInt32(Request.QueryString["t"]);
        return sesiones;
    }

    protected void lnkAgregarNuevo_Click(object sender, EventArgs e)
    {
        AlUsuarios agr = new AlUsuarios();
        agr.nick = txtUsuario.Text;
        agr.contraseña = txtContra.Text;
        agr.nombre = txtNombre.Text;
        agr.puestirri = Convert.ToInt32( cmb_puesto.SelectedValue);
        string puesto = Convert.ToString(cmb_puesto.SelectedItem);
        if (puesto == "Analista")
        {
            agr.npuesto = "ANA";
        }
        else if (puesto == "Gerente")
        {
            agr.npuesto = "GRT";
        } else if(puesto == "Asesor"){
        
            agr.npuesto = "ASE";
        
        }else
        {
            agr.npuesto = "OTR";
        }
        agr.agregarPuesto();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        AlUsuarios agr = new AlUsuarios();
        agr.nick = txtUsuarioEDT.Text;
        agr.contraseña = txtContraEDT.Text;
        agr.nombre = txtNombreEDT.Text;
        agr.puestirri = Convert.ToInt32(cmbPuestoEDT.SelectedValue);
       
        string puesto = Convert.ToString(cmbPuestoEDT.SelectedItem);
        if (puesto == "Analista")
        {
            agr.npuesto = "ANA";
        }
        else if (puesto == "Gerente")
        {
            agr.npuesto = "GRT";
        }
        else if (puesto == "Asesor")
        {

            agr.npuesto = "ASE";

        }
        else
        {
            agr.npuesto = "OTR";
        }
        agr.editaPuesto();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
    }
}