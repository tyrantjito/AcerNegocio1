using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using E_Utilities;
using System.Web.UI.HtmlControls;
using System.Configuration;

public partial class AdmOrdenes : System.Web.UI.MasterPage
{
    Recepciones recepciones = new Recepciones();
    Fechas fechas = new Fechas();
    Permisos permisos = new Permisos();
    protected void Page_Load(object sender, EventArgs e)
    {
        cargaNotificaciones();
        if (!IsPostBack)
        {
            cargaInfoEnc();
            controlAccesos();
            ddlTallerCambio.DataBind();
            ddlTallerCambio.SelectedValue = Request.QueryString["t"]; ;
            if (ddlTallerCambio.Items.Count > 1)
            {
                ddlTallerCambio.Visible = true;
                lblTaller.Visible = true;
            }
            else
            {
                ddlTallerCambio.Visible = false;
                lblTaller.Visible = false;
            }
            lblversion.Text = ConfigurationManager.AppSettings["version"].ToString();
        }
        
    }

    private void cargaInfoEnc()
    {
        int[] sesiones = obtieneSesiones();
        if (sesiones[0] == 0 || sesiones[1] == 0 || sesiones[2] == 0 || sesiones[3] == 0 )
            Response.Redirect("Default.aspx");
        try
        {
            lblEmpresa.Text = recepciones.obtieneNombreEmpresa(Request.QueryString["e"]);
            lblUser.Text = recepciones.obtieneNombreUsuario(Request.QueryString["u"]);
            lblTallerSesion.Text = recepciones.obtieneNombreTaller(Request.QueryString["t"]);
        }
        catch (Exception) { 

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
            Session["paginaOrigen"] = "Ordenes.aspx";
            Session["errores"] = "Su sesión a expirado vuelva a iniciar Sesión";
            Response.Redirect("AppErrorLog.aspx");
        }
        return sesiones;
    }

    protected void lnkRecarga_Click(object sender, EventArgs e)
    {
        cargaNotificaciones();
        DataList2.DataBind();
    }
    private void cargaNotificaciones()
    {
        int[] sesiones = obtieneSesiones();
        if (sesiones[0] == 0 || sesiones[1] == 0 || sesiones[2] == 0 || sesiones[3] == 0)
            Response.Redirect("Default.aspx");
        fechas.fecha = fechas.obtieneFechaLocal();
        fechas.tipoFormato = 4;
        lblFechaActual.Text = fechas.obtieneFechaConFormato();
        Notificaciones noti = new Notificaciones();
        noti.Empresa = sesiones[2];
        noti.Taller = sesiones[3];
        noti.Fecha = Convert.ToDateTime(lblFechaActual.Text);
        noti.Estatus = "P";
        noti.obtieneNotificacionesPendientes();
        object[] pendientes = noti.Retorno;
        if (Convert.ToBoolean(pendientes[0]))
            lblNotifi.Text = Convert.ToString(pendientes[1]);
        else
            lblNotifi.Text = "0";
    }
    protected void lnkNotificacion_Click(object sender, EventArgs e)
    {
        LinkButton btnNot = (LinkButton)sender;
        int alerta = Convert.ToInt32(btnNot.CommandArgument.ToString());
        Notificaciones noti = new Notificaciones();
        noti.Fecha = Convert.ToDateTime(lblFechaActual.Text);
        noti.Estatus = "V";
        noti.Entrada = alerta;
        noti.Empresa = Convert.ToInt32(Request.QueryString["e"]);
        noti.Taller = Convert.ToInt32(Request.QueryString["t"]);
        noti.actualizaEstado();
        DataList2.DataBind();
        noti.Estatus = "P";
        noti.obtieneNotificacionesPendientes();
        object[] pendientes = noti.Retorno;
        if (Convert.ToBoolean(pendientes[0]))
            lblNotifi.Text = Convert.ToString(pendientes[1]);
        else
            lblNotifi.Text = "0";
    }
    protected void lnkTodas_Click(object sender, EventArgs e)
    {
        Response.Redirect("ConsultaNotificaciones.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"] + "&pag=Ordenes");
    }
    protected void lnkAsignaciones_Click(object sender, EventArgs e)
    {
        Response.Redirect("OperativosOrden.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);        
    }
    protected void lnkCronos_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cronologicos.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkBitAvances_Click(object sender, EventArgs e)
    {
        Response.Redirect("BitacoraAvance.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkBitComentarios_Click(object sender, EventArgs e)
    {
        Response.Redirect("BitacoraDeComentarios.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkMesaControl_Click(object sender, EventArgs e)
    {
        Response.Redirect("MesaControl.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkBitLocalizacion_Click(object sender, EventArgs e)
    {
        Response.Redirect("BitacoraLocalizacion.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkBitIngresos_Click(object sender, EventArgs e)
    {
        Response.Redirect("BitacoraIngresos.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkBitAsignaciones_Click(object sender, EventArgs e)
    {
        Response.Redirect("BitacoraAsignaciones.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkCalendario_Click(object sender, EventArgs e)
    {
        Response.Redirect("CalendarioPersonal.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkBitValuacion_Click(object sender, EventArgs e)
    {
        Response.Redirect("BitacoraValuacion.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkBitLlamadas_Click(object sender, EventArgs e)
    {
        Response.Redirect("BitacoraLlamadas.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkPantallasPatioMenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("PantallasPatioMenu.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("RepProgSemanal.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkPinturas_Click(object sender, EventArgs e)
    {
        Response.Redirect("BitacoraRegistroPintura.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"] + "&c=" + 4);
    }
   
    
    protected void lnkCuentas_Click(object sender, EventArgs e)
    {
        Response.Redirect("BienvenidaCuentas.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    private void controlAccesos()
    {
        int[] sesiones = obtieneSesiones();
        permisos.idUsuario = sesiones[0];
        //permisos.obtienePermisos();
        //bool[] permisosUsuario = permisos.permisos;


        HtmlControl[] controles = {
        (HtmlControl)this.FindControl("madmon"),
        (HtmlControl)this.FindControl("mcrono"),
        (HtmlControl)this.FindControl("mBitacoras"),
        (HtmlControl)this.FindControl("mConsultas"),
        (HtmlControl)this.FindControl("mreportes"),
        (HtmlControl)this.FindControl("actNot"),
        (HtmlControl)this.FindControl("mnot"),
        (HtmlControl)this.FindControl("mpantallaspatiomenu"),
            (HtmlControl)this.FindControl("subPersonal"),
            (HtmlControl)this.FindControl("subCalendar"),
                (HtmlControl)this.FindControl("subIngresos"),
                (HtmlControl)this.FindControl("subBitAsig"),
                (HtmlControl)this.FindControl("subBitVal"),
                (HtmlControl)this.FindControl("subBitLlam"),
                (HtmlControl)this.FindControl("subPint"),
                    (HtmlControl)this.FindControl("subConsTrans"),
                    (HtmlControl)this.FindControl("subConsGar"),
                    (HtmlControl)this.FindControl("subConsTerm"),
                    (HtmlControl)this.FindControl("subConsEntregas"),
                        (HtmlControl)this.FindControl("progSem"),
        (HtmlControl)this.FindControl("mFactura"),
        (HtmlControl)this.FindControl("refaccionesPendientes"),
        (HtmlControl)this.FindControl("prePendiente"),
        (HtmlControl)this.FindControl("mCuentas"),
        (HtmlControl)this.FindControl("sunOrden"),
        (HtmlControl)this.FindControl("subTran"),
        (HtmlControl)this.FindControl("subPerfil")
        };

        int[] codigos = { 41, 44, 45, 51, 56, 58, 58, 59, 42, 43, 46, 47, 48, 49, 50, 52, 53, 54, 55, 57, 83, 96, 97, 98, 103, 104, 105 };

        //permisos.permisos = permisosUsuario;
        for (int i = 0; i < codigos.Length; i++)
        {   /*         
            permisos.permiso = codigos[i];
            permisos.tienePermisoIndicado();            
            if (!permisos.tienePermiso)
                controles[i].Attributes["style"] = "display:none;";
            else
                controles[i].Attributes["style"] = "";
                */
        }
    }



    protected void lnkCerrarSesion_Click(object sender, EventArgs e)
    {
        CatUsuarios usuarios = new CatUsuarios();
        object[] actualizaAcceso = usuarios.actualizaBitacoraAcceso(Convert.ToInt32(Request.QueryString["u"]), "I");
        if (Convert.ToBoolean(actualizaAcceso[0]))
        {
            Response.Redirect("Default.aspx");
        }
    }


    protected void ddlTallerCambio_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string usuario = Request.QueryString["u"];
            string perfil = Request.QueryString["p"];
            string empresa = Request.QueryString["e"];
            string taller = ddlTallerCambio.SelectedValue;
            Response.Redirect("Ordenes.aspx?u=" + usuario + "&p=" + perfil + "&e=" + empresa + "&t=" + taller, false);
        }
        catch (Exception ex)
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void lnkTransito_Click(object sender, EventArgs e)
    {
        Response.Redirect("BitacoraTransito.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void lnkPerfiles_Click(object sender, EventArgs e)
    {
        Response.Redirect("BitacoraPerdidaPago.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    

    protected void lnkAbrirOrden_Click(object sender, EventArgs e)
    {
        Response.Redirect("EstatusOrden.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void lnkOrdenes_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ordenes.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

  

   

    protected void lnkSolicitudGrupo_Click(object sender, EventArgs e)
    {
        Response.Redirect("SolicitudGrupos.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkConsultaBuro_Click(object sender, EventArgs e)
    {
        Response.Redirect("ConsultaBuro.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkAnalisisPago_Click(object sender, EventArgs e)
    {
        Response.Redirect("AnalisisCapacidadPago.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkFichaDatos_Click(object sender, EventArgs e)
    {
        Response.Redirect("FichaDatos.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkEvaluacionGrupal_Click(object sender, EventArgs e)
    {
        Response.Redirect("EvaluacionGrupal.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkActaIntegracion_Click(object sender, EventArgs e)
    {
        Response.Redirect("ActaIntegracion.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkVisitaOcular_Click(object sender, EventArgs e)
    {
        Response.Redirect("VisitaOcular.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkCheckList_Click(object sender, EventArgs e)
    {
        Response.Redirect("CheckList.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkAutorizacionBuro_Click(object sender, EventArgs e)
    {
        Response.Redirect("ValidacionCirculoaCredito.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }


    protected void lnkAbreAltaPuestos_Click(object sender, EventArgs e)
    {
        Response.Redirect("AltaPuestos.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void lnkAbreAltaUsuarios_Click(object sender, EventArgs e)
    {
        Response.Redirect("AltaUsuarios.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void lnkCatalogoBancos_Click(object sender, EventArgs e)
    {
        Response.Redirect("catBancos.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void lnkValidacionTelefonica_Click(object sender, EventArgs e)
    {
        Response.Redirect("ValidacionTelefonica.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkAutComite_Click(object sender, EventArgs e)
    {
        Response.Redirect("AutComite.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void lnkCatLineaFondeo_Click(object sender, EventArgs e)
    {
        Response.Redirect("CatLineaFondeo.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void lnkCatPoliticas_Click(object sender, EventArgs e)
    {
        Response.Redirect("CatPoliticasCredito.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void lnkCatValidaciones_Click(object sender, EventArgs e)
    {
        Response.Redirect("catValidaciones.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void Parametros_Click(object sender, EventArgs e)
    {
        Response.Redirect("Parametros.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_Cartera_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cartera.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_CarteraMora_Click(object sender, EventArgs e)
    {
        Response.Redirect("CarteraMora.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PagosPac_Click(object sender, EventArgs e)
    {
        Response.Redirect("PagosPactados.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void btn_CompMora_Click1(object sender, EventArgs e)
    {
        Response.Redirect("ComparativoMora.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_catSucur_Click(object sender, EventArgs e)
    {
        Response.Redirect("CatSucursales.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDEstados_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDEstados.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDMoneda_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDMoneda.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_tipClien_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDTipCliente.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDTipoGrupo_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDTipoGrupo.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void btn_PLDTipoDocumento_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDTipoDocumento.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void btn_PLDTipoVivienda_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDTipoVivienda.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDTipoReferencia_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDTipoReferencia.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDEstadoCivil_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDEstadoCivil.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDGenero_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDGenero.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDTrabajo_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDTrabajo.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDEscolaridad_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDEscolaridad.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDEconomico_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDRegimenEconomico.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDRangoTiempo_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDRangoTiempo.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDRangoIngreso_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDrIngresos.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDNacionalidad_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDNacionalidad.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void btn_PLDActividadEconomica_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDActividadEconomica.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDCanal_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDCanal.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void btn_PLDTipoProducto_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDTipoProducto.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDProductos_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLD_Productos.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDPropositoCuenta_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDPropositoCuenta.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDDestinoFondo_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDDestinoFondo.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDOrigenFondo_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDOrigenFondo.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void btn_PLDRegional_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDRegional.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void btn_PLDSucursal_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDSucursal.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void btn_PLDTipoPep_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDTipoPep.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    //
    protected void bbtn_PLDTLista_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDTipoLista.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void btn_PLDTOperacion_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDTipoOperacion.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void btn_PLDTransaccion_Click(object sender, EventArgs e)
    {
        Response.Redirect("PLDTipoTransaccion.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
}
