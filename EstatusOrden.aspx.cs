using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EstatusOrden : System.Web.UI.Page
{
    Recepciones recepciones = new Recepciones();
    ManoObraOrden datos = new ManoObraOrden();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtNoOrden.Text = "";
            lblError.Text = "";
            lblEstatusInicial.Text = "";
            //PanelMask.Visible = PanelPopUpPermiso.Visible = false;               
            lnkActualizarEstatus.Visible = false;
        }
    }

    private int[] obtieneSesiones()
    {
        int[] sesiones = new int[4] { 0, 0, 0, 0 };
        try
        {
            sesiones[0] = Convert.ToInt32(Request.QueryString["u"]);
            sesiones[1] = Convert.ToInt32(Request.QueryString["p"]);
            sesiones[2] = Convert.ToInt32(Request.QueryString["e"]);
            sesiones[3] = Convert.ToInt32(Request.QueryString["t"]);
        }
        catch (Exception x)
        {
            sesiones = new int[4] { 0, 0, 0, 0 };
            Session["paginaOrigen"] = "EstatusOrden.aspx";
            Session["errores"] = "Su sesión a expirado vuelva a iniciar Sesión";
            Response.Redirect("AppErrorLog.aspx");
        }
        return sesiones;
    }

    protected void lnkActualizarEstatus_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        if (txtNoOrden.Text.Trim() != "")
        {
            int noOrden, idTaller, idEmpresa;
            noOrden = idTaller = idEmpresa = 0;
            bool error = false;
            try
            {
                noOrden = Convert.ToInt32(txtNoOrden.Text);
                try
                {
                    idTaller = Convert.ToInt32(Request.QueryString["t"]);
                    idEmpresa = Convert.ToInt32(Request.QueryString["e"]);
                }
                catch (Exception) { error = true; }
                if (!error)
                {
                    bool existe = datos.existeNoOrden(noOrden, idTaller, idEmpresa);
                    if (existe)
                    {
                        if (lblEstatusInicial.Text != ddlEstatus.SelectedValue)
                        {
                            //txtUsuarioLog.Text = txtContraseñaLog.Text = lblErrorLog.Text = "";
                            //PanelMask.Visible = PanelPopUpPermiso.Visible = true;
                            noOrden = Convert.ToInt32(txtNoOrden.Text);
                            try
                            {
                                idTaller = Convert.ToInt32(Request.QueryString["t"]);
                                idEmpresa = Convert.ToInt32(Request.QueryString["e"]);
                                string estatus = ddlEstatus.SelectedValue;
                                string usuario = Request.QueryString["u"];
                                string usuarioAuto = recepciones.obtieneNombreUsuario(Request.QueryString["u"]);
                                if (lblEstatusInicial.Text != "")
                                {
                                    object[] actualizado = recepciones.actualizaEstatusOrdenIndicado(idEmpresa, idTaller, noOrden, estatus, usuario, usuarioAuto, lblEstatusInicial.Text);
                                    if (Convert.ToBoolean(actualizado[0]))
                                    {
                                        //txtUsuarioLog.Text = txtContraseñaLog.Text = lblErrorLog.Text = "";
                                        //PanelMask.Visible = PanelPopUpPermiso.Visible = false;
                                        lblError.Text = "La orden " + noOrden.ToString() + " se a colocado en estatus " + ddlEstatus.SelectedItem.Text;
                                        if (ddlEstatus.SelectedValue != "A") {
                                            Operarios operarios = new Operarios();
                                            int[] sessiones = obtieneSesiones();
                                            operarios.empresa = sessiones[2];
                                            operarios.taller = sessiones[3];
                                            operarios.orden = noOrden;
                                            operarios.liberarCajones();
                                            object[] op = operarios.retorno;
                                            if (Convert.ToBoolean(op[0]))
                                            {
                                                DataSet info = (DataSet)op[1];
                                                foreach (DataRow r in info.Tables[0].Rows)
                                                {
                                                    string[] valoresOperarios = new string[5] { Convert.ToString(r[0]), Convert.ToString(r[1]), Convert.ToString(r[2]), Convert.ToString(r[3]), Convert.ToString(r[4]) };
                                                    operarios.liberar(sessiones, valoresOperarios);
                                                }
                                            }
                                        }
                                        txtNoOrden.Text = "";

                                    }
                                    else
                                        lblError.Text = "Error al actualizar la orden. " + actualizado[1].ToString();
                                }
                                else
                                    lblError.Text = "La orden indicada no tiene un estatus valido. Cierre esta ventana y verifique los datos ingresados";
                            }
                            catch (Exception ex)
                            {
                                lblError.Text = "Error: " + ex.Message;
                            }
                        }
                        else
                            lblError.Text = "No es posible actualizar la orden al estatus indicado ya que la misma se encuentra en dicho estatus";
                    }
                    else
                        lblError.Text = "El número de orden no existe.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
            }
        }
        else
            lblError.Text = "Necesita colocar un número de orden.";
    }

    
    /*protected void btnAceptar_Click(object sender, EventArgs e)
    {
        lblErrorLog.Text = "";
        Autorizaciones autoriza = new Autorizaciones();
        autoriza.nick = txtUsuarioLog.Text;
        autoriza.contrasena = txtContraseñaLog.Text;
        autoriza.permiso = 1;
        autoriza.validaUsuario();
        int noOrden, idTaller, idEmpresa;
        noOrden = idTaller = idEmpresa = 0;
        if (autoriza.Valido)
        {
            
            
        }
        else
            lblErrorLog.Text = "Error: " + autoriza.Error;
    }
protected void btnCancelar_Click(object sender, EventArgs e)
    {
        txtUsuarioLog.Text = txtContraseñaLog.Text = lblErrorLog.Text = "";
        PanelMask.Visible = PanelPopUpPermiso.Visible = false;
    }*/

    protected void txtNoOrden_TextChanged(object sender, EventArgs e)
    {
        lnkActualizarEstatus.Visible = false;
        lblError.Text = "";
        if (txtNoOrden.Text.Trim() != "")
        {
            int noOrden, idTaller, idEmpresa;
            noOrden = idTaller = idEmpresa = 0;
            bool error = false;
            try
            {
                noOrden = Convert.ToInt32(txtNoOrden.Text);
                try
                {
                    idTaller = Convert.ToInt32(Request.QueryString["t"]);
                    idEmpresa = Convert.ToInt32(Request.QueryString["e"]);
                }
                catch (Exception) { error = true; }
                if (!error)
                {
                    bool existe = datos.existeNoOrden(noOrden, idTaller, idEmpresa);
                    if (existe)
                    {
                        object[] info = recepciones.obtieneEstatusOrden(idEmpresa.ToString(), idTaller.ToString(), noOrden.ToString());
                        if (Convert.ToBoolean(info[0]))
                        {
                            lblError.Text = "Orden encontrada";
                            lnkActualizarEstatus.Visible = true;
                            ddlEstatus.SelectedValue = lblEstatusInicial.Text = Convert.ToString(info[1]);
                        }
                        else
                            lblError.Text = "No es posible cargar el estatus actual de la orden.";
                    }
                    else
                        lblError.Text = "El número de orden no existe.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
            }
        }
        else
            lblError.Text = "Necesita colocar un número de orden.";
    }
}