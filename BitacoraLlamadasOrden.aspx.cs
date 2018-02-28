using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using E_Utilities;

public partial class BitacoraLlamadasOrden : System.Web.UI.Page
{
    Recepciones recepciones = new Recepciones();    
    BitacoraLlamadasDatos bitacoraDatos = new BitacoraLlamadasDatos();
    protected void Page_Load(object sender, EventArgs e)
    {
        obtieneSesiones();
        if (!IsPostBack)
        {
            lblFechaActual.Text = new Fechas().obtieneFechaLocal().ToString("yyyy-MM-dd");
            lblHoraActual.Text = new Fechas().obtieneFechaLocal().ToString("HH:mm:ss");
            cargaDatosPie();
            cargaFormatoLlamada();
            limpiaCampos();
        }
    }

    private void cargaFormatoLlamada()
    {
        string tipoLlamada = rbtnTipoLlamada.SelectedValue;        
        limpiaCampos();
        if (tipoLlamada == "E" || tipoLlamada == "P")
        {
            Label13.Text = "Nombre del cliente que llamó:";
            Label14.Text = "Quién contestó llamada:";
            RequiredFieldValidator9.Enabled = false;
            Label15.Visible = true;
            txtAtendio.Visible = true;
            Label7.Visible = PanelLlamadaSaliente.Visible = ddlRespuesta.Visible = Label19.Visible = txtHoraPactada.Visible = txtFechaPactada.Visible = lnkcalendario.Visible = false;
            if (tipoLlamada == "E")
            {
                pnlPendientes.Visible = false;
                PanelLlamadaEntrante.Visible = pnlCronos.Visible = true;
                GridEntrante.DataBind();
            }
            else
            {
                pnlCronos.Visible = false;
                PanelLlamadaEntrante.Visible = false;
                pnlPendientes.Visible = true;
                GridPendiente.DataBind();
            }
        }
        else
        {
            Label13.Text = "Quién realiza la llamada:";
            Label14.Text = "Nombre del cliente que contestó:";            
            Label15.Visible = PanelLlamadaEntrante.Visible = pnlPendientes.Visible = false;
            RequiredFieldValidator9.Enabled = txtAtendio.Visible = false;
            PanelLlamadaSaliente.Visible = pnlCronos.Visible = ddlRespuesta.Visible = Label19.Visible = true;
            Label7.Visible = txtHoraPactada.Visible = txtFechaPactada.Visible = lnkcalendario.Visible = false;
            GridSaliente.DataBind();
        }
    }

    private int[] obtieneSesiones()
    {
        int[] sesiones = new int[6] { 0, 0, 0, 0, 0, 0 };
        try
        {
            sesiones[0] = Convert.ToInt32(Request.QueryString["u"]);
            sesiones[1] = Convert.ToInt32(Request.QueryString["p"]);
            sesiones[2] = Convert.ToInt32(Request.QueryString["e"]);
            sesiones[3] = Convert.ToInt32(Request.QueryString["t"]);
            sesiones[4] = Convert.ToInt32(Request.QueryString["o"]);
            sesiones[5] = Convert.ToInt32(Request.QueryString["f"]);
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

    private void cargaDatosPie()
    {
        int empresa = Convert.ToInt32(Request.QueryString["e"]);
        int taller = Convert.ToInt32(Request.QueryString["t"]);
        int orden = Convert.ToInt32(Request.QueryString["o"]);
        object[] datosOrden = recepciones.obtieneInfoOrdenPie(orden, empresa, taller);
        if (Convert.ToBoolean(datosOrden[0]))
        {
            DataSet ordenDatos = (DataSet)datosOrden[1];
            foreach (DataRow filaOrd in ordenDatos.Tables[0].Rows)
            {
                ddlToOrden.Text = filaOrd[0].ToString();
                ddlClienteOrden.Text = filaOrd[1].ToString();
                ddlTsOrden.Text = filaOrd[2].ToString();
                ddlValOrden.Text = filaOrd[3].ToString();
                ddlTaOrden.Text = filaOrd[4].ToString();
                ddlLocOrden.Text = filaOrd[5].ToString();
                ddlPerfil.Text = filaOrd[13].ToString();
                lblSiniestro.Text = filaOrd[9].ToString();
                lblDeducible.Text = Convert.ToDecimal(filaOrd[10].ToString()).ToString("C2");
                lblTotOrden.Text = Convert.ToDecimal(filaOrd[11].ToString()).ToString("C2");
                try
                {
                    DateTime fechaEntrega = Convert.ToDateTime(filaOrd[14].ToString());
                    if (fechaEntrega.ToString("yyyy-MM-dd") == "1900-01-01")
                        lblEntregaEstimada.Text = "No establecida";
                    else
                        lblEntregaEstimada.Text = filaOrd[14].ToString();
                }
                catch (Exception)
                {
                    lblEntregaEstimada.Text = "No establecida";
                }
                lblPorcDedu.Text = filaOrd[16].ToString() + "%";
            }
        }
    }

    protected void lnkGuarda_Click(object sender, EventArgs e)
    {
        string observaciones = txtComentarioCliente.Text;
        string comentarioCliente = txtComentarioCliente.Text;
        string fechaPactada = txtFechaPactada.Text;
        string horaPactada = txtHoraPactada.Text;
        string fechaLlamada = txtFechaLlamada.Text;
        string horaLlamada = "00:00:00";
        if (!timpHoraLlamada.IsEmpty)
            horaLlamada = timpHoraLlamada.SelectedTime.Value.ToString(); //timpHoraLlamada.Hour.ToString() + ":" + timpHoraLlamada.Minute.ToString() + ":" + timpHoraLlamada.Second.ToString();
        int noOrden = Convert.ToInt32(Request.QueryString["o"]);
        int idEmpresa = Convert.ToInt32(Request.QueryString["e"]);
        int idTaller = Convert.ToInt32(Request.QueryString["t"]);
        int idUsuario = Convert.ToInt32(Request.QueryString["u"]);
        int llamadaRespuesta = 0;
        try { llamadaRespuesta = Convert.ToInt32(ddlRespuesta.SelectedValue); } catch (Exception) { llamadaRespuesta = 0; }
        string atendio = "";
        string responsable = txtResponsable.Text;
        string tipoLlamada = rbtnTipoLlamada.SelectedValue;
        string contesto = "";
        string llamo = "";
        string id = lblConsecutivo.Text;
        int atendida = valorIntToBool(chkAtendida.Checked);
        string quienAtendio = txtQuienAtendio.Text;
        DateTime fechaAtencion = new Fechas().obtieneFechaLocal();

        if (tipoLlamada == "E")
        {
            atendio = txtAtendio.Text;
            contesto = txtContesto.Text;
            llamo = txtLlamo.Text;
        }
        else
        {
            atendio = "";
            llamo = txtContesto.Text;
            contesto = txtLlamo.Text;
        }
        bool insertar = false;
        if (id == "1")
            insertar = bitacoraDatos.actualizaLlamada(noOrden, idEmpresa, idTaller, tipoLlamada, fechaLlamada, horaLlamada, llamo, observaciones, comentarioCliente, llamo, contesto, atendio, responsable, llamadaRespuesta, idUsuario, id,atendida,quienAtendio,fechaAtencion);
        else
            insertar = bitacoraDatos.insertaLlamada(noOrden, idEmpresa, idTaller, tipoLlamada, fechaLlamada, horaLlamada, llamo, observaciones, comentarioCliente, llamo, contesto, atendio, responsable, llamadaRespuesta, idUsuario, atendida, quienAtendio, fechaAtencion);
        if (!insertar)
            lblError.Text = "La llamada no se logro registrar, verifique su conexión e intentelo nuevamente.";
        else
        {
            lblError.Text = "";
            limpiaCampos();
            if (!chkAtendida.Checked)
            {
                notificar();
                /*Alx: Actualiza las notificaciones de la master. Para que actualice debe haber un updatepanel en la master y un scripmanager, sin éste en cada página*/
                // Cast the loosely-typed Page.Master property and then set the GridMessageText property 
                AdmonOrden masterPage = Page.Master as AdmonOrden;
                //masterPage.cargaInfo(); //método que debe ser public en master
                // Use the strongly-typed Master property 
                //Master.cargaInfo();
            }
        }
        cargaFormatoLlamada();
    }

    private int valorIntToBool(bool valor)
    {
        if (valor)
            return 1;
        else
            return 0;
    }

    private void limpiaCampos()
    {
        txtContesto.Text = txtLlamo.Text = txtAtendio.Text = txtResponsable.Text = "";
        Fechas fechas = new Fechas();
        DateTime fechLoc = fechas.obtieneFechaLocal();
        txtFechaLlamada.Text = txtFechaPactada.Text = fechLoc.ToString("yyyy-MM-dd");
        txtHoraPactada.Text = fechLoc.ToString("HH:mm:ss");
        timpHoraLlamada.Clear();
        //timpHoraLlamada.SetTime(fechLoc.Hour, fechLoc.Minute, fechLoc.Second, fechLoc.ToString("tt") == "a.m." ? MKB.TimePicker.TimeSelector.AmPmSpec.AM : MKB.TimePicker.TimeSelector.AmPmSpec.PM);
        txtComentarioCliente.Text = lblError.Text = "";
        lblConsecutivo.Text = "0";
    }

    protected void rbtnTipoLlamada_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblConsecutivo.Text = "0";
        cargaFormatoLlamada();
    }

    protected void lnkSeleciona_Click(object sender, EventArgs e)
    {
        LinkButton bnt = (LinkButton)sender;
        lblConsecutivo.Text = bnt.CommandArgument.ToString();
    }
    
    protected void GridSaliente_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem) {
            LinkButton btn = e.Item.FindControl("lnkSeleciona") as LinkButton;
            string estatusRef = DataBinder.Eval(e.Item.DataItem, "estatus").ToString();
            bool atendida = false;
            try { atendida = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "atendida")); } catch (Exception ex) { atendida = false; }
            if (estatusRef == "P")
            {
                if (atendida)
                    btn.Visible = false;
                else
                    btn.Visible = true;
            }
            else {
                if (atendida)
                    btn.Visible = false;
                else
                    btn.Visible = true;
            }                
        }
    }

    private void notificar()
    {
        int[] ses = obtieneSesiones();
        Fechas fechas = new Fechas();
        Notificaciones notifi = new Notificaciones();
        notifi.Articulo = ses[4].ToString();
        notifi.Empresa = ses[2];
        notifi.Taller = ses[3];
        notifi.Usuario = ses[0].ToString();
        notifi.Fecha = fechas.obtieneFechaLocal();
        notifi.Estatus = "P";
        notifi.Extra = rbtnTipoLlamada.SelectedItem.Text;
        notifi.Clasificacion = 17;
        notifi.Origen = "O";
        notifi.armaNotificacion();
        notifi.agregaNotificacion();
    }
}