using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BitacoraDeComentarios : System.Web.UI.Page
{
    Recepciones recepciones = new Recepciones();
    ManoObraOrden datosMO = new ManoObraOrden();
    protected void Page_Load(object sender, EventArgs e)
    {
        obtieneSesiones();
        if (!IsPostBack)
        {
            txtNoOrden.Text = "";
            txtComentario.Text = "";
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
                        txtComentario.Text = "";
                        GridView1.DataBind();
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

        try
        {
            BitacorasComentarios bitacora = new BitacorasComentarios();
            bitacora.Empresa = sesiones[2];
            bitacora.Taller = sesiones[3];
            bitacora.Orden = Convert.ToInt32(txtNoOrden.Text);
            bitacora.Bitacora = 2;
            bitacora.Usuario = sesiones[0];
            bitacora.Valor = txtComentario.Text;
            bitacora.agregaRegistro();
            object[] afectado = bitacora.Afectado;
            if (Convert.ToBoolean(afectado[0]))
            {
                txtComentario.Text = "";
                GridView1.DataBind();
            }
            else
                lblError.Text = "Error al agregar el comentario: " + afectado[1].ToString();
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }

    }

    protected void lnkRegresarOrdenes_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ordenes.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
}