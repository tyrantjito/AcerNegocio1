using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class BitacoraAvance : System.Web.UI.Page
{
    Recepciones recepciones = new Recepciones();
    ManoObraOrden datosMO = new ManoObraOrden();
    protected void Page_Load(object sender, EventArgs e)
    {
        obtieneSesiones();
        if (!IsPostBack)
        {           
            txtAvance.Text = "";
            txtNoOrden.Text = "";
        }        
    }


    protected void btnBuscarAsignaciones_Click(object sender, EventArgs e)//juan-14-12-15
    {
        Panel4.Visible = false;
        if (txtNoOrden.Text.Trim() != "")
        {
            lblError.Text = "";
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
                    bool existe = datosMO.existeNoOrden(noOrden, idTaller, idEmpresa);
                    if (existe)
                    {
                        Panel4.Visible = true;
                        GridView1.DataBind();
                        txtAvance.Text = "";
                    }
                    else
                    {                        
                        lblError.Text = "El número de orden no existe.";
                    }
                }
            }
            catch (Exception ex)
            {

                lblError.Text = "Errores: " + ex.Message;
            }
        }
        else
        {            
            lblError.Text = "Necesita colocar un número de orden.";
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
        catch (Exception)
        {
            sesiones = new int[4] { 0, 0, 0, 0 };
            Session["paginaOrigen"] = "Ordenes.aspx";
            Session["errores"] = "Su sesión a expirado vuelva a iniciar Sesión";
            Response.Redirect("AppErrorLog.aspx");
        }
        return sesiones;
    }

    
    protected void lnkGuarda_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        int[] sesiones = obtieneSesiones();
        decimal avance = 0;
        try { avance = Convert.ToDecimal(txtAvance.Text); }
        catch (Exception) { avance = -1; }
        if (avance != -1)
        {
            try
            {
                BitacorasComentarios bitacora = new BitacorasComentarios();
                bitacora.Empresa = sesiones[2];
                bitacora.Taller = sesiones[3];
                bitacora.Orden = Convert.ToInt32(txtNoOrden.Text);
                bitacora.Bitacora = 1;
                bitacora.Usuario = sesiones[0];
                bitacora.Valor = avance.ToString();
                bitacora.agregaRegistro();
                object[] afectado = bitacora.Afectado;
                if (Convert.ToBoolean(afectado[0]))
                {
                    txtAvance.Text = "";
                    GridView1.DataBind();
                }
                else
                    lblError.Text = "Error al agregar el avance: " + afectado[1].ToString();
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
            }
        }
        else
            lblError.Text = "Debe indicar un valor de porcentaje avance válido";
    }

    protected void lnkRegresarOrdenes_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ordenes.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
}