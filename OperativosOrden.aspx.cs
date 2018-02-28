using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Globalization;
using E_Utilities;

public partial class OperativosOrden : System.Web.UI.Page
{
    ManoObraOrden datosMO = new ManoObraOrden();
    Fechas fechas = new Fechas();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {            
            lblError.Text = "";
            SqlDataSource1.SelectCommand = "select no_orden,id_asignacion,idEmp,Convert(char(10),fecha_ini_prog,126) as fecha_ini_prog,convert(char(8),hora_ini_prog,108) as hora_ini_prog,convert(char(10),fecha_ini,126) as fecha_ini,convert(char(8),hora_ini,108) as hora_ini,convert(char(10),fecha_fin,126) as fecha_fin,convert(char(8),hora_fin,108) as hora_fin,monto,oservaciones,estatus from Operativos_Orden where id_empresa=" + Request.QueryString["e"] + " and id_taller=" + Request.QueryString["t"] + " and IdEmp=0";
            lblOperario.Text = "Seleccione un operario para ver sus ordenes asignadas";
            lblIdOperario.Text = "0";
            grdOrdenes.EmptyDataText = "Debe seleccionar un operario para ver las ordenes a las que está asignado";
            grdOrdenes.DataBind();
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

    protected void lnkRegresarOrdenes_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ordenes.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    private bool validaFechaFin(string fechaIni, string fechaFin)//22-12-2015
    {
        try
        {
            DateTime ini = Convert.ToDateTime(fechaIni);
            DateTime fin = Convert.ToDateTime(fechaFin);
            if (fin > ini)
                return false;
            else
                return true;
        }
        catch (Exception) { return true; }
    }

    private bool tiempoMenorActual(string tiempo)
    {
        try
        {
            DateTime tiempoTxt = Convert.ToDateTime(tiempo);
            if (tiempoTxt < fechas.obtieneFechaLocal())
                return true;
            else
                return false;
        }
        catch (Exception ex) { return false; }
    }

    private bool tiempoNull(string tiempo)
    {
        try
        {
            if (tiempo != "" && tiempo != null)
                return true;
            else
                return false;
        }
        catch (Exception ex) { return false; }
    }

    private bool validaTiempo(string tiempo)
    {
        try
        {
            DateTime.ParseExact(tiempo, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            return true;
        }
        catch (Exception ex)
        {
            try
            {
                if (tiempo.Length == 7)
                    tiempo = tiempo.PadLeft(8, '0');
                DateTime.ParseExact(tiempo, "HH:mm:ss", CultureInfo.InvariantCulture);
                return true;
            }
            catch (Exception ex1) { return false; }
        }
    }
    
    protected void GridOperarios_RowDataBound(object sender, GridViewRowEventArgs e)//juan-9-12-2015
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPichonera = e.Row.FindControl("lblPichonera") as Label;
            Label lblAsigOperMO = e.Row.FindControl("lblAsigOperMO") as Label;
            LinkButton lnkAsigOperMO = e.Row.FindControl("lnkAsigOperMO") as LinkButton;
            int disponibles = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "resta").ToString());
            try
            {
                if (disponibles < 1)
                    lblAsigOperMO.ForeColor = Color.Red;
                else
                    lblAsigOperMO.ForeColor = Color.Empty;
            }
            catch (Exception ex) { }
        }
    }
    protected void lnkBuscarOpe_Click(object sender, EventArgs e)
    {
        if (txtBuscaOperario.Text != "")
            SqlDataSourceOperadoresRH.SelectCommand = "select (rtrim(ltrim(e.Nombres))+' '+rtrim(ltrim(e.Apellido_Paterno))+' '+isnull(rtrim(ltrim(e.Apellido_Materno)),'')) as nombre,e.clv_pichonera,e.IdEmp,(e.clv_pichonera - (select count(*) from Operativos_Orden oo where oo.IdEmp=e.IdEmp and oo.estatus<>'T'))as resta from Empleados e where e.status_empleado!='B' and e.tipo_empleado in ('EX','IN') AND (rtrim(ltrim(e.Nombres)) LIKE '%" + txtBuscaOperario.Text.ToUpper().Trim() + "%' or rtrim(ltrim(e.Apellido_Paterno)) like '%" + txtBuscaOperario.Text.ToUpper().Trim() + "%' or isnull(rtrim(ltrim(e.Apellido_Materno)),'') like '%" + txtBuscaOperario.Text.ToUpper().Trim() + "%') order by resta desc";
        GridOperarios.DataBind();
        SqlDataSource1.SelectCommand = "select no_orden,id_asignacion,idEmp,Convert(char(10),fecha_ini_prog,126) as fecha_ini_prog,convert(char(8),hora_ini_prog,108) as hora_ini_prog,convert(char(10),fecha_ini,126) as fecha_ini,convert(char(8),hora_ini,108) as hora_ini,convert(char(10),fecha_fin,126) as fecha_fin,convert(char(8),hora_fin,108) as hora_fin,monto,oservaciones,estatus from Operativos_Orden where id_empresa=" + Request.QueryString["e"] + " and id_taller=" + Request.QueryString["t"] + " and IdEmp=0";
        lblOperario.Text = "Seleccione un operario para ver sus ordenes asignadas";
        grdOrdenes.EmptyDataText = "Debe seleccionar un operario para ver las ordenes a las que está asignado";
        grdOrdenes.DataBind();
    }
    protected void hpkLimpiar_Click(object sender, EventArgs e)
    {
        txtBuscaOperario.Text = "";
        SqlDataSourceOperadoresRH.SelectCommand = "select (rtrim(ltrim(e.Nombres))+' '+rtrim(ltrim(e.Apellido_Paterno))+' '+isnull(rtrim(ltrim(e.Apellido_Materno)),'')) as nombre,e.clv_pichonera,e.IdEmp,(e.clv_pichonera - (select count(*) from Operativos_Orden oo where oo.IdEmp=e.IdEmp and oo.estatus<>'T'))as resta from Empleados e where e.status_empleado!='B' and e.tipo_empleado in ('EX','IN') order by resta desc";
        GridOperarios.DataBind();
        SqlDataSource1.SelectCommand = "select no_orden,id_asignacion,idEmp,Convert(char(10),fecha_ini_prog,126) as fecha_ini_prog,convert(char(8),hora_ini_prog,108) as hora_ini_prog,convert(char(10),fecha_ini,126) as fecha_ini,convert(char(8),hora_ini,108) as hora_ini,convert(char(10),fecha_fin,126) as fecha_fin,convert(char(8),hora_fin,108) as hora_fin,monto,oservaciones,estatus from Operativos_Orden where id_empresa=" + Request.QueryString["e"] + " and id_taller=" + Request.QueryString["t"] + " and IdEmp=0";
        lblOperario.Text = "Seleccione un operario para ver sus ordenes asignadas";
        grdOrdenes.EmptyDataText = "Debe seleccionar un operario para ver las ordenes a las que está asignado";
        grdOrdenes.DataBind();
    }
    protected void lnkDetalleOperario_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] argumentos=btn.CommandArgument.ToString().Split(new char[]{';'});
        lblOperario.Text = "Ordenes asignadas a " + argumentos[1];
        lblIdOperario.Text = argumentos[0];
        SqlDataSource1.SelectCommand = "select no_orden,id_asignacion,idEmp,Convert(char(10),fecha_ini_prog,126) as fecha_ini_prog,convert(char(8),hora_ini_prog,108) as hora_ini_prog,convert(char(10),fecha_ini,126) as fecha_ini,convert(char(8),hora_ini,108) as hora_ini,convert(char(10),fecha_fin,126) as fecha_fin,convert(char(8),hora_fin,108) as hora_fin,monto,oservaciones,estatus from Operativos_Orden where id_empresa=" + Request.QueryString["e"] + " and id_taller=" + Request.QueryString["t"] + " and IdEmp=" + argumentos[0]+" order by no_orden desc, fecha_ini_prog desc, hora_ini_prog desc";
        grdOrdenes.EmptyDataText = "El operario " + argumentos[1] + " no cuenta con ordenes asignadas";
        grdOrdenes.DataBind();
    }


    protected void grdOrdenes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string estatus = DataBinder.Eval(e.Row.DataItem, "estatus").ToString();
            string modo = e.Row.RowState.ToString();
            string[] valores = null;
            try { valores = modo.Split(new char[] { ',' }); }
            catch (Exception) { modo = e.Row.ToString(); }
            if (valores != null) {
                for (int i = 0; i < valores.Length; i++) {
                    if (valores[i].Trim() == "Edit")
                    {
                        modo = "Edit";
                        break;
                    }
                    else
                        modo = valores[i].Trim();
                }
            }

            if (modo == "Edit" )
            {
                TextBox txtFechaIniDes = e.Row.FindControl("txtFechaIni") as TextBox;
                TextBox txtHoraIniDes = e.Row.FindControl("txtHoraIni") as TextBox;
                TextBox txtFechaFinDes = e.Row.FindControl("txtFechaFin") as TextBox;
                TextBox txtHoraFinDes = e.Row.FindControl("txtHoraFin") as TextBox;
                TextBox txtMonto = e.Row.FindControl("txtMonto") as TextBox;
                TextBox txtObservaciones = e.Row.FindControl("txtObservaciones") as TextBox;
                Label lblIni = e.Row.FindControl("lblIniMod") as Label;
                Label lblFin = e.Row.FindControl("lblFinMod") as Label;
                LinkButton lnkCanIni = e.Row.FindControl("lnkCancelIni") as LinkButton;
                LinkButton lnkCanFin = e.Row.FindControl("lnkCancelFin") as LinkButton;
                LinkButton lnkIni = e.Row.FindControl("lnkFechaIni") as LinkButton;
                LinkButton lnkFin = e.Row.FindControl("lnkFechaFin") as LinkButton;
                LinkButton lnkAsignar = e.Row.FindControl("lnkAsignar") as LinkButton;

                txtFechaIniDes.Visible = txtHoraIniDes.Visible = txtFechaFinDes.Visible = txtHoraFinDes.Visible = lblIni.Visible = lblFin.Visible = lnkCanIni.Visible = lnkCanFin.Visible = lnkIni.Visible = lnkFin.Visible = lnkAsignar.Visible = false;

                try
                {
                    switch (estatus)
                    {
                        case "A":
                            //Asignado
                            txtFechaIniDes.Visible = txtHoraIniDes.Visible = lnkIni.Visible = true;                            
                            break;
                        case "I":
                            //Iniciado
                            lblIni.Visible = lnkCanIni.Visible = txtFechaFinDes.Visible = txtHoraFinDes.Visible = lnkFin.Visible= lnkAsignar.Visible = true;                            
                            break;
                        case "T":
                            //Terminado
                            lblIni.Visible = lblFin.Visible = lnkCanFin.Visible = true;
                            break; 
                        case "M":
                            lnkCanFin.Visible = true;
                            break;
                        default:
                            break;
                    }
                    
                }
                catch (Exception) {  }
            }
        }
    }
   
    protected void lnkActualizar_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            string[] argumentos = btn.CommandArgument.ToString().Split(new char[] { ';' });

            int noOrden = Convert.ToInt32(argumentos[3]);
            int idTaller = Convert.ToInt32(Request.QueryString["t"]);
            int idEmpresa = Convert.ToInt32(Request.QueryString["e"]);
            int idEmp = Convert.ToInt32(argumentos[2]);
            int idAsignacion = Convert.ToInt32(argumentos[0]);
            string usuario = Request.QueryString["u"];
            bool reseteado = false;
            
            switch (argumentos[1])
            {
                case "A":
                    TextBox fecha = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtFechaIni") as TextBox;
                    TextBox hora = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtHoraIni") as TextBox;
                    TextBox obs = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtObservaciones") as TextBox;
                    TextBox montoI = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtMonto") as TextBox;
                    decimal monto = 0;

                    if (fecha.Text != "")
                    {
                        if (hora.Text != "")
                        {
                            try
                            {
                                if (montoI.Text != "")
                                    monto = Convert.ToDecimal(montoI.Text);
                                else
                                    monto = 0;
                                DateTime fechaInicio = Convert.ToDateTime(fecha.Text + " " + hora.Text);
                                reseteado = datosMO.actualizaTiemposOperadorMO(noOrden, idTaller, idEmpresa, idEmp, idAsignacion, "Ini", fechaInicio, 'I', usuario, obs.Text, monto);
                                if (reseteado)
                                {
                                    GridOperarios.DataBind();
                                    grdOrdenes.DataBind();
                                }
                            }
                            catch (Exception ex)
                            {
                                lblErroresOO.Text = "Error al actualizar: " + ex.Message;
                            }
                        }
                        else
                            lblErroresOO.Text = "Debe indicar la hora de inicio";
                    }
                    else
                        lblErroresOO.Text = "Debe indicar una fecha de inicio";
                    break;
                case "I":
                    TextBox fechaF = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtFechaFin") as TextBox;
                    TextBox horaF = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtHoraFin") as TextBox;
                    TextBox obsF = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtObservaciones") as TextBox;
                    TextBox montoF = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtMonto") as TextBox;
                    decimal montoD = 0;

                    if (fechaF.Text != "")
                    {
                        if (horaF.Text != "")
                        {
                            try
                            {
                                if (montoF.Text != "")
                                    montoD = Convert.ToDecimal(montoF.Text);
                                else
                                    montoD = 0;
                                DateTime fechaInicio = Convert.ToDateTime(fechaF.Text + " " + horaF.Text);
                                reseteado = datosMO.actualizaTiemposOperadorMO(noOrden, idTaller, idEmpresa, idEmp, idAsignacion, "Fin", fechaInicio, 'T', usuario, obsF.Text, montoD);
                                if (reseteado)
                                {
                                    GridOperarios.DataBind();
                                    grdOrdenes.DataBind();
                                }  
                            }
                            catch (Exception ex)
                            {
                                lblErroresOO.Text = "Error al actualizar: " + ex.Message;
                            }
                        }
                        else
                            lblErroresOO.Text = "Debe indicar la hora de termino";
                    }
                    else
                        lblErroresOO.Text = "Debe indicar una fecha de termino";
                    break;
                case "T":
                    TextBox obsM = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtObservaciones") as TextBox;
                    TextBox montoM = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtMonto") as TextBox;
                    decimal montoMO = 0;
                    try
                    {
                        if (montoM.Text != "")
                            montoMO = Convert.ToDecimal(montoM.Text);
                        else
                            montoMO = 0;
                        reseteado = datosMO.actualizaTiemposOperadorMO(noOrden, idTaller, idEmpresa, idEmp, idAsignacion, "", fechas.obtieneFechaLocal(), 'M', usuario, obsM.Text, montoMO);
                        if (reseteado)
                        {
                            GridOperarios.DataBind();
                            grdOrdenes.DataBind();
                        } 
                    }
                    catch (Exception ex)
                    {
                        lblErroresOO.Text = "Error al actualizar: " + ex.Message;
                    }

                    break;
                case "M":
                    TextBox obsM1 = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtObservaciones") as TextBox;
                    TextBox montoM1 = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtMonto") as TextBox;
                    decimal montoMO1 = 0;
                    try
                    {
                        if (montoM1.Text != "")
                            montoMO1 = Convert.ToDecimal(montoM1.Text);
                        else
                            montoMO1 = 0;
                        reseteado = datosMO.actualizaTiemposOperadorMO(noOrden, idTaller, idEmpresa, idEmp, idAsignacion, "", fechas.obtieneFechaLocal(), 'M', usuario, obsM1.Text, montoMO1);
                        if (reseteado)
                        {
                            GridOperarios.DataBind();
                            grdOrdenes.DataBind();
                        }  
                    }
                    catch (Exception ex)
                    {
                        lblErroresOO.Text = "Error al actualizar: " + ex.Message;
                    }
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            lblErroresOO.Text = "Error al actualizar: " + ex.Message;
        }
    }
    protected void lnkCancelIni_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] argumentos = btn.CommandArgument.ToString().Split(new char[] { ';' });
        TextBox obs = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtObservaciones") as TextBox;
        TextBox mon = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtMontoM") as TextBox;
        decimal monto = 0;
        try { monto = Convert.ToDecimal(mon.Text); }
        catch (Exception) { monto = 0; }
        int noOrden = Convert.ToInt32(argumentos[3]);
        int idTaller = Convert.ToInt32(Request.QueryString["t"]);
        int idEmpresa = Convert.ToInt32(Request.QueryString["e"]);
        int idEmp = Convert.ToInt32(argumentos[2]);
        int idAsignacion = Convert.ToInt32(argumentos[0]);
        string usuario = Request.QueryString["u"];
        bool reseteado = false;        
        reseteado = datosMO.actualizaTiemposOperadorMO(noOrden, idTaller, idEmpresa, idEmp, idAsignacion, "Ini", null, 'A', usuario, obs.Text, monto);
        if (reseteado)
        {
            GridOperarios.DataBind();
            grdOrdenes.DataBind();
        }
    }

    protected void lnkCancelFin_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] argumentos = btn.CommandArgument.ToString().Split(new char[] { ';' });
        TextBox obs = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtObservaciones") as TextBox;
        TextBox mon = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtMontoM") as TextBox;
        decimal monto = 0;
        try { monto = Convert.ToDecimal(mon.Text); }
        catch (Exception) { monto = 0; }
        int noOrden = Convert.ToInt32(argumentos[3]);
        int idTaller = Convert.ToInt32(Request.QueryString["t"]);
        int idEmpresa = Convert.ToInt32(Request.QueryString["e"]);
        int idEmp = Convert.ToInt32(argumentos[2]);
        int idAsignacion = Convert.ToInt32(argumentos[0]);
        string usuario = Request.QueryString["u"];
        bool reseteado = false;
        reseteado = datosMO.actualizaTiemposOperadorMO(noOrden, idTaller, idEmpresa, idEmp, idAsignacion, "Fin", null, 'I', usuario, obs.Text, monto);
        if (reseteado)
        {
            GridOperarios.DataBind();
            grdOrdenes.DataBind();
        }
    }
    protected void lnkAsignar_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] argumentos = btn.CommandArgument.ToString().Split(new char[] { ';' });
        TextBox obs = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtObservaciones") as TextBox;
        TextBox mon = grdOrdenes.Rows[grdOrdenes.EditIndex].FindControl("txtMontoM") as TextBox;
        decimal monto = 0;
        try { monto = Convert.ToDecimal(mon.Text); }
        catch (Exception) { monto = 0; }
        int noOrden = Convert.ToInt32(argumentos[3]);
        int idTaller = Convert.ToInt32(Request.QueryString["t"]);
        int idEmpresa = Convert.ToInt32(Request.QueryString["e"]);
        int idEmp = Convert.ToInt32(argumentos[2]);
        int idAsignacion = Convert.ToInt32(argumentos[0]);
        string usuario = Request.QueryString["u"];
        bool reseteado = false;
        reseteado = datosMO.actualizaTiemposOperadorMO(noOrden, idTaller, idEmpresa, idEmp, idAsignacion, "Fin", fechas.obtieneFechaLocal(), 'T', usuario, obs.Text, monto);
        if (reseteado)
        {
            GridOperarios.DataBind();
            grdOrdenes.DataBind();
            Response.Redirect("Asignacion.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"] + "&o=" + noOrden.ToString() );
        }
    }
}