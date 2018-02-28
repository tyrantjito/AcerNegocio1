using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Globalization;
using System.Drawing;
using E_Utilities;
using Telerik.Web.UI;

public partial class Ordenes : System.Web.UI.Page 
{
    Recepciones recepciones = new Recepciones();
    DatosOrdenes datosOrdenes = new DatosOrdenes();
    Fechas fechas = new Fechas();
    Permisos permisos = new Permisos();

    protected void Page_Load(object sender, EventArgs e)
    {
        cargaInfo();
        controlAccesos();
    }


    private void limpiaCombo(RadComboBox combo)
    {
        combo.Items.Clear();
        //combo.Items.Add(new ListItem("", ""));
        combo.DataBind();
        //combo.SelectedValue = "";
        combo.SelectedIndex = -1;
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

    private void cargaInfo()
    {

        int[] sesiones = obtieneSesiones();
        if (sesiones[0] == 0 || sesiones[1] == 0 || sesiones[2] == 0 || sesiones[3] == 0)
            Response.Redirect("Default.aspx");
        try
        {
            string estatus = ddlEstatus.SelectedValue;
            if (estatus == "0")
                estatus = "";
            else
                estatus = " and orp.status_orden='" + ddlEstatus.SelectedValue + "'";
            SqlDataSource1.SelectParameters.Clear();

            if (txtFiltro.Text == "")
            {
               /* SqlDataSource1.SelectCommand = "select orp.no_orden,tv.descripcion+' '+m.descripcion+' '+tu.descripcion as descripcion,upper(v.color_ext) as color_ext,orp.placas,l.descripcion as localizacion,C.razon_social,orp.fase_orden, so.f_recepcion, orp.no_siniestro,v.modelo,po.descripcion as perfil,orp.status_orden"
                                                        + " from Ordenes_Reparacion orp"
                                                        + " left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa=so.id_empresa and orp.id_taller=so.id_taller"
                                                        + " left join Vehiculos v on v.id_marca=orp.id_marca and v.id_tipo_vehiculo=orp.id_tipo_vehiculo and v.id_tipo_unidad=orp.id_tipo_unidad and v.id_vehiculo=orp.id_vehiculo"
                                                        + " left join Marcas m on m.id_marca=orp.id_marca"
                                                        + " left join Tipo_Vehiculo tv on tv.id_tipo_vehiculo=orp.id_tipo_vehiculo"
                                                        + " left join Tipo_Unidad tu on tu.id_marca=orp.id_marca and tu.id_tipo_vehiculo=orp.id_tipo_vehiculo and tu.id_tipo_unidad=orp.id_tipo_unidad"
                                                        + " left join Localizaciones l on l.id_localizacion=orp.id_localizacion"
                                                        + " left join Cliprov c on c.id_cliprov=orp.id_cliprov and c.tipo=orp.tipo_cliprov and c.tipo='C'"
                                                        + " left join PerfilesOrdenes po on po.id_perfilOrden=orp.id_perfilOrden"
                                                        + " where orp.id_empresa=" + sesiones[2].ToString() + " and orp.id_taller=" + sesiones[3].ToString() + estatus + "  order by orp.no_orden desc";*/
            }
            else
            {
               /* SqlDataSource1.SelectCommand = "select orp.no_orden,tv.descripcion+' '+m.descripcion+' '+tu.descripcion as descripcion,upper(v.color_ext) as color_ext,orp.placas,l.descripcion as localizacion,C.razon_social,orp.fase_orden, so.f_recepcion, orp.no_siniestro,v.modelo,po.descripcion as perfil,orp.status_orden"
                                                        + " from Ordenes_Reparacion orp"
                                                        + " left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa=so.id_empresa and orp.id_taller=so.id_taller"
                                                        + " left join Vehiculos v on v.id_marca=orp.id_marca and v.id_tipo_vehiculo=orp.id_tipo_vehiculo and v.id_tipo_unidad=orp.id_tipo_unidad and v.id_vehiculo=orp.id_vehiculo"
                                                        + " left join Marcas m on m.id_marca=orp.id_marca"
                                                        + " left join Tipo_Vehiculo tv on tv.id_tipo_vehiculo=orp.id_tipo_vehiculo"
                                                        + " left join Tipo_Unidad tu on tu.id_marca=orp.id_marca and tu.id_tipo_vehiculo=orp.id_tipo_vehiculo and tu.id_tipo_unidad=orp.id_tipo_unidad"
                                                        + " left join Localizaciones l on l.id_localizacion=orp.id_localizacion"
                                                        + " left join Cliprov c on c.id_cliprov=orp.id_cliprov and c.tipo=orp.tipo_cliprov and c.tipo='C'"
                                                        + " left join PerfilesOrdenes po on po.id_perfilOrden=orp.id_perfilOrden "
                                                        + " where orp.id_empresa=" + sesiones[2].ToString() + " and orp.id_taller=" + sesiones[3].ToString() + estatus + " and (orp.no_orden like '%" + txtFiltro.Text + "%' or c.razon_social like '%" + txtFiltro.Text + "%' or orp.placas like '%" + txtFiltro.Text + "%' or m.descripcion like '%" + txtFiltro.Text + "%' or tv.descripcion like '%" + txtFiltro.Text + "%' or tu.descripcion like '%" + txtFiltro.Text + "%' or v.color_ext like '%" + txtFiltro.Text + "%' or orp.no_siniestro like '%" + txtFiltro.Text + "%' or v.modelo like '%" + txtFiltro.Text + "%' or l.descripcion like '%" + txtFiltro.Text + "%' or po.descripcion like '%" + txtFiltro.Text + "%') order by orp.no_orden desc";
                                                        */
            }
        }
        catch (Exception ex)
        {
            Session["errores"] = ex.Message;
            Session["paginaOrigen"] = "Ordenes.aspx";
            Response.Redirect("AppErrorLog.aspx");
        }
    }

    protected void btnGuardarOrden_Click(object sender, EventArgs e)
    {
        try
        {
            object[] existePlaca = recepciones.existePlaca(txtPlacaNueva.Text);
            if (Convert.ToBoolean(existePlaca[0]))
            {
                int[] datosVehiculo = new int[4] { 0, 0, 0, 0 };
                int[] sesiones = obtieneSesiones();
                object[] existePrevia = recepciones.ordenPreviaMismoDia(txtPlacaNueva.Text, sesiones[2], sesiones[3]);
                bool existeOrdenMismoDia = false;

                if (Convert.ToBoolean(existePrevia[0]))
                    existeOrdenMismoDia = Convert.ToBoolean(existePrevia[1]);
                else
                    existeOrdenMismoDia = true;

                if (!existeOrdenMismoDia)
                {
                    bool validos = validaCampos();

                    if (validos)
                    {
                        if (!Convert.ToBoolean(existePlaca[1]))
                        {
                            object[] vehiculoAgregado = recepciones.agregaVehiculo(ddlNuevaMarca.SelectedValue, ddlNuevoTv.SelectedValue, ddlNuevaUnidad.SelectedValue, txtNuevoMod.Text, txtPlacaNueva.Text, txtColorNuevo.Text);
                            if (Convert.ToBoolean(vehiculoAgregado[0]))
                            {
                                object vehiculo = vehiculoAgregado[1];
                                datosVehiculo = obtieneVehiculo(vehiculo);
                                object[] orden = generaOrden(datosVehiculo);
                                if (Convert.ToBoolean(orden[0]))
                                {
                                    int ordenGenerada = 0;
                                    try { ordenGenerada = Convert.ToInt32(orden[1]); }
                                    catch (Exception x)
                                    {
                                        ordenGenerada = 0;
                                    }
                                    if (ordenGenerada != 0)
                                    {
                                        pnlNuevaRecepcion.Visible = false;
                                        lblOrdenGen.Text = ordenGenerada.ToString();
                                        pnlOrden.Visible = true;
                                    }
                                    else
                                        lblErrorRecepcion.Text = "Error: " + orden[1].ToString();
                                }
                                else
                                {
                                    lblErrorRecepcion.Text = "Error: " + orden[1].ToString();
                                }
                            }
                            else
                                lblErrorRecepcion.Text = "Error: " + vehiculoAgregado[1].ToString();
                        }
                        else
                        {
                            object[] obtieneInfoVehiculo = recepciones.obtieneInfoVehiculo(txtPlacaNueva.Text);
                            if (Convert.ToBoolean(obtieneInfoVehiculo[0]))
                            {
                                DataSet datos = new DataSet();
                                datos = (DataSet)obtieneInfoVehiculo[1];
                                datosVehiculo = new int[4] { 0, 0, 0, 0 };
                                foreach (DataRow fila in datos.Tables[0].Rows)
                                {
                                    datosVehiculo[0] = Convert.ToInt32(fila[0].ToString());
                                    datosVehiculo[1] = Convert.ToInt32(fila[1].ToString());
                                    datosVehiculo[2] = Convert.ToInt32(fila[2].ToString());
                                    datosVehiculo[3] = Convert.ToInt32(fila[3].ToString());
                                }
                                string mensajeActualizacion = actualizaVehiculo(datosVehiculo);
                                if (mensajeActualizacion == "")
                                {
                                    object[] orden = generaOrden(datosVehiculo);
                                    if (Convert.ToBoolean(orden[0]))
                                    {
                                        int ordenGenerada = 0;
                                        try { ordenGenerada = Convert.ToInt32(orden[1]); }
                                        catch (Exception x)
                                        {
                                            ordenGenerada = 0;
                                        }
                                        if (ordenGenerada != 0)
                                        {
                                            pnlNuevaRecepcion.Visible = false;
                                            lblOrdenGen.Text = ordenGenerada.ToString();
                                            pnlOrden.Visible = true;
                                        }
                                        else
                                            lblErrorRecepcion.Text = "Error: " + orden[1].ToString();
                                    }
                                    else
                                    {
                                        lblErrorRecepcion.Text = "Error: " + orden[1].ToString();
                                    }
                                }
                                else
                                {
                                    lblErrorRecepcion.Text = "Error: " + mensajeActualizacion;
                                }
                            }
                            else
                            {
                                lblErrorRecepcion.Text = "Error: " + obtieneInfoVehiculo[1].ToString();
                            }
                        }
                    }
                }
                else {
                    pnlNuevaRecepcion.Visible = false;
                    pnlPregunta.Visible = true;
                }
            }
            else
                lblErrorRecepcion.Text = "Error: " + existePlaca[1].ToString();
        }
        catch (Exception ex)
        {
            Session["errores"] = ex.Message;
            Session["paginaOrigen"] = "Ordenes.aspx";
            Response.Redirect("AppErrorLog.aspx");
        }
        finally {
            RadGrid1.DataBind();
        }
    }

    private bool validaCampos()
    {
        bool validos = false;
        Ejecuciones eje = new Ejecuciones();
        object[] obj = new object[2];
        string strSql;
        string err = "";

        if (!string.IsNullOrEmpty(ddlNuevaMarca.Text.Trim()) || (ddlNuevaMarca.SelectedValue != "" && ddlNuevaMarca.SelectedValue != "0"))
        {
            strSql = "SELECT COUNT(*) FROM Marcas WHERE descripcion='" + ddlNuevaMarca.Text.Trim() +"'";
            obj = eje.scalarToInt(strSql);
            if (int.Parse(obj[1].ToString()) > 0)
                validos = true;
            else
                err = "Debe indicar una marca o bien si esta indicada debe presionar el boton aun costado del valor Marca para agregar la misma<br>";
        }
        if(!string.IsNullOrEmpty(ddlNuevoTv.Text.Trim()) || (ddlNuevoTv.SelectedValue != "" && ddlNuevoTv.SelectedValue != "0"))
        {
            strSql = "SELECT COUNT(*) FROM Tipo_Vehiculo WHERE descripcion='" + ddlNuevoTv.Text.Trim() + "'";
            obj = eje.scalarToInt(strSql);
            if (int.Parse(obj[1].ToString()) > 0)
                validos = true;
            else 
                err += "Debe indicar un vehículo o bien si esta indicado debe presionar el boton aun costado del valor Vehículo para agregar el mismo<br>";
        }
        if(!string.IsNullOrEmpty(ddlNuevaUnidad.Text.Trim()) || (ddlNuevaUnidad.SelectedValue != "" && ddlNuevaUnidad.SelectedValue != "0"))
        {
            strSql = "SELECT COUNT(*) FROM Tipo_Unidad WHERE UPPER(descripcion)='" + ddlNuevaUnidad.Text.Trim() + "'";
            obj = eje.scalarToInt(strSql);
            if (int.Parse(obj[1].ToString()) > 0)
                validos = true;
            else
                err += "Debe seleccionar una linea o si va agregar una nueva debe presionar el boton aun costado del valor Linea para agregar la misma<br>";
        }
        if(!string.IsNullOrEmpty(ddlTipoOrdenNuevo.Text.Trim()) || (ddlTipoOrdenNuevo.SelectedValue != "" && ddlTipoOrdenNuevo.SelectedValue != "0"))
        {
            strSql = "SELECT COUNT(*) FROM Tipo_Orden WHERE descripcion='" + ddlTipoOrdenNuevo.Text.Trim() + "'";
            obj = eje.scalarToInt(strSql);
            if (int.Parse(obj[1].ToString()) > 0)
                validos = true;
            else
                err += "Debe indicar un Tipo Orden<br>";
        }
        if(!string.IsNullOrEmpty(ddlClienteNuevo.Text.Trim()) || (ddlClienteNuevo.SelectedValue != "" && ddlClienteNuevo.SelectedValue != "0"))
        {
            strSql = "SELECT COUNT(*) FROM Cliprov WHERE razon_social='" + ddlClienteNuevo.Text + "' AND tipo='C'";
            obj = eje.scalarToInt(strSql);
            if (int.Parse(obj[1].ToString()) > 0)
                validos = true;
            else
                err += "Debe indicar un Cliente o bien si esta indicado debe presionar el boton aun costado del valor Cliente para agregar el mismo<br>";
        }
        if(!string.IsNullOrEmpty(ddlTipoServNuevo.Text) || (ddlTipoServNuevo.SelectedValue != "" && ddlTipoServNuevo.SelectedValue != "0"))
        {
            strSql = "SELECT COUNT(*) FROM Tipo_Servicios WHERE descripcion='" + ddlTipoServNuevo.Text + "'";
            obj = eje.scalarToInt(strSql);
            if (int.Parse(obj[1].ToString()) > 0)
                validos = true;
            else
                err += "Debe indicar un Tipo Servicio<br>";
        }
        if(!string.IsNullOrEmpty(ddlTipoValNuevo.Text) || (ddlTipoValNuevo.SelectedValue != "" && ddlTipoValNuevo.SelectedValue != "0"))
        {
            strSql = "SELECT COUNT(*) FROM Tipo_Valuacion WHERE descripcion='" + ddlTipoValNuevo.Text + "'";
            obj = eje.scalarToInt(strSql);
            if (int.Parse(obj[1].ToString()) > 0)
                validos = true;
            else
                err += "Debe indicar un Tipo Valuación<br>";
        }
        if(!string.IsNullOrEmpty(ddlTipoAseguradoNuevo.Text) || (ddlTipoAseguradoNuevo.SelectedValue != "" && ddlTipoAseguradoNuevo.SelectedValue != "0"))
        {
            strSql = "SELECT COUNT(*) FROM Tipo_Asegurados WHERE descripcion='" + ddlTipoAseguradoNuevo.Text + "'";
            obj = eje.scalarToInt(strSql);
            if (int.Parse(obj[1].ToString()) > 0)
                validos = true;
            else
                err += "Debe indicar un Tipo Asegurado<br>";
        }
        if(!string.IsNullOrEmpty(ddlPerfilOrdenNuevo.Text) || (ddlPerfilOrdenNuevo.SelectedValue != "" && ddlPerfilOrdenNuevo.SelectedValue != "0"))
        {
            strSql = "SELECT COUNT(*) FROM PerfilesOrdenes WHERE descripcion='" + ddlPerfilOrdenNuevo.Text + "'";
            obj = eje.scalarToInt(strSql);
            if (int.Parse(obj[1].ToString()) > 0)
                validos = true;
            else
                err += "Debe indicar un Perfil<br>";
        }
        if(!string.IsNullOrEmpty(ddlLocalizacionNuevo.Text) || (ddlLocalizacionNuevo.SelectedValue != "" && ddlLocalizacionNuevo.SelectedValue != "0"))
        {
            strSql = "SELECT COUNT(*) FROM Localizaciones WHERE descripcion='" + ddlLocalizacionNuevo.Text + "'";
            obj = eje.scalarToInt(strSql);
            if (int.Parse(obj[1].ToString()) > 0)
                validos = true;
            else
                err += "Debe indicar una Localización";
        }

        if (string.IsNullOrEmpty(err))
            validos = true;
        else
        {
            validos = false;
            lblErrorRecepcion.Text = err;
        }

        
        //Validación anterior
        /*
        if (ddlNuevaMarca.SelectedValue == "" || ddlNuevaMarca.SelectedValue == "0")
        {
            lblErrorRecepcion.Text = "Debe indicar una marca o bien si esta indicada debe presionar el boton aun costado del valor Marca para agregar la misma";
            validos = false;
        }
        else
        {
            if (ddlNuevoTv.SelectedValue == "" || ddlNuevoTv.SelectedValue == "0")
            {
                lblErrorRecepcion.Text = "Debe indicar un vehículo o bien si esta indicado debe presionar el boton aun costado del valor Vehículo para agregar el mismo";
                validos = false;
            }
            else {
                if (ddlNuevaUnidad.SelectedValue == "" || ddlNuevaUnidad.SelectedValue == "0")
                {
                    lblErrorRecepcion.Text = "Debe indicar una linea o bien si esta indicada debe presionar el boton aun costado del valor Linea para agregar la misma";
                    validos = false;
                }
                else {
                    if (ddlTipoOrdenNuevo.SelectedValue == "" || ddlTipoOrdenNuevo.SelectedValue == "0")
                    {
                        lblErrorRecepcion.Text = "Debe indicar un Tipo Orden";
                        validos = false;
                    }
                    else {
                        if (ddlClienteNuevo.SelectedValue == "" || ddlClienteNuevo.SelectedValue == "0")
                        {
                            lblErrorRecepcion.Text = "Debe indicar un Cliente o bien si esta indicado debe presionar el boton aun costado del valor Cliente para agregar el mismo";
                            validos = false;
                        }
                        else {
                            if (ddlTipoServNuevo.SelectedValue == "" || ddlTipoServNuevo.SelectedValue == "0") {
                                lblErrorRecepcion.Text = "Debe indicar un Tipo Servicio";
                                validos = false;
                            }
                            else
                            {
                                if (ddlTipoValNuevo.SelectedValue == "" || ddlTipoValNuevo.SelectedValue == "0")
                                {
                                    lblErrorRecepcion.Text = "Debe indicar un Tipo Valuación";
                                    validos = false;
                                }
                                else
                                {
                                    if (ddlTipoAseguradoNuevo.SelectedValue == "" || ddlTipoAseguradoNuevo.SelectedValue == "0")
                                    {
                                        lblErrorRecepcion.Text = "Debe indicar un Tipo Asegurado";
                                        validos = false;
                                    }
                                    else
                                    {
                                        if (ddlPerfilOrdenNuevo.SelectedValue == "" || ddlPerfilOrdenNuevo.SelectedValue == "0")
                                        {
                                            lblErrorRecepcion.Text = "Debe indicar un Perfil";
                                            validos = false;
                                        }
                                        else
                                        {
                                            if (ddlLocalizacionNuevo.SelectedValue == "" || ddlLocalizacionNuevo.SelectedValue == "0")
                                            {
                                                lblErrorRecepcion.Text = "Debe indicar una Localización";
                                                validos = false;
                                            }
                                            else
                                                validos = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }*/
        return validos;
    }

    private string actualizaVehiculo(int[] datosVehiculo)
    {
        string mensaje = "";
        try
        {
            object[] actualizoVeh = recepciones.actualizaDatosBasicosVehiculo(datosVehiculo, txtColorNuevo.Text);
            if (!Convert.ToBoolean(actualizoVeh[0]))
            {
                mensaje = actualizoVeh[1].ToString();
            }
        }
        catch (Exception ex)
        {
            mensaje = ex.Message.ToString();
        }
        return mensaje;
    }

    private object[] generaOrden(int[] datosVehiculo)
    {
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int taller = sesiones[3];
        int usuario = sesiones[0];
        int asesor = sesiones[0];
        object[] datosOrden = new object[16];
        datosOrden[0] = empresa;
        datosOrden[1] = taller;
        datosOrden[2] = txtPlacaNueva.Text.ToUpper();
        datosOrden[3] = ddlTipoOrdenNuevo.SelectedValue;
        datosOrden[4] = ddlClienteNuevo.SelectedValue;
        datosOrden[5] = ddlTipoServNuevo.SelectedValue;
        datosOrden[6] = ddlTipoValNuevo.SelectedValue;
        datosOrden[7] = ddlTipoAseguradoNuevo.SelectedValue;
        datosOrden[8] = asesor;
        datosOrden[9] = datosVehiculo[0];
        datosOrden[10] = datosVehiculo[1];
        datosOrden[11] = datosVehiculo[2];
        datosOrden[12] = datosVehiculo[3];
        datosOrden[13] = usuario;
        datosOrden[14] = ddlLocalizacionNuevo.SelectedValue;
        datosOrden[15] = ddlPerfilOrdenNuevo.SelectedValue;
        return recepciones.generaOrdenNueva(datosOrden);

    }

    private int[] obtieneVehiculo(object obj)
    {
        int[] valores = new int[4] { 0, 0, 0, 0 };
        if (obj != null)
        {
            IEnumerable<object> coleccion = (IEnumerable<object>)obj;
            int iDom = 0;
            foreach (object item in coleccion)
            {
                valores[iDom] = Convert.ToInt32(item);
                iDom++;
            }
        }
        return valores;
    }

    protected void btnAceptarOrden_Click(object sender, EventArgs e)
    {
        pnlOrden.Visible = false;
        pnlMask.Visible = false;
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        pnlMask.Visible = false;
        cargaInfo();
    }

    protected void btnOrden_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string credito = btn.CommandArgument.ToString();
        Response.Redirect("BienvenidoCredito.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"] + "&c=" + credito);
    }

    private string convierteStringtoInt(string valor)
    {
        string retorno = "0";
        try
        {
            if (valor == "")
            {
                retorno = "0";
            }
            else
            {
                retorno = valor;
            }
        }
        catch (Exception x)
        {
            retorno = "0";
        }
        return retorno;
    }

    private bool convierteStringtoBool(string valor)
    {
        bool retorno = false;
        try
        {
            if (valor == "null")
            {
                retorno = false;
            }
            else
            {
                if (valor == "False")
                    retorno = false;
                else
                    retorno = true;
            }
        }
        catch (Exception x)
        {
            retorno = false;
        }
        return retorno;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string fase = DataBinder.Eval(e.Row.DataItem, "fase_orden").ToString();
            var img = e.Row.Cells[10].Controls[1].FindControl("imgFase") as System.Web.UI.WebControls.Image;
            var lblFechaIni = e.Row.Cells[1].Controls[1].FindControl("lblFechaRecp") as Label;
            var lbtnNoOrden = e.Row.Cells[0].Controls[1].FindControl("no_orden") as LinkButton;
            img.ImageUrl = "img/fase_" + fase + ".png";
            try
            {
                string fechaIni = lblFechaIni.Text;
                int noOrden = Convert.ToInt32(lbtnNoOrden.Text);
                bool llamadaPendiente = checaEstatusLlamada(noOrden);
                if (!llamadaPendiente)
                {
                    DateTime fechaActual = fechas.obtieneFechaLocal();
                    DateTime fechaIngreso = Convert.ToDateTime(fechaIni);
                    TimeSpan hrsDif = fechaActual - fechaIngreso;
                    int hrsDiferencia = Convert.ToInt32(hrsDif.TotalHours);
                    if (hrsDiferencia < 49)
                    { }
                    else if (hrsDiferencia < 72 && hrsDiferencia > 48)
                    {
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.Yellow;
                    }
                    else if (hrsDiferencia > 72)
                    {
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {

            }
        }
    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            DataRowView filas = (DataRowView)e.Item.DataItem;
            DataRow r = filas.Row;
            //string fase = DataBinder.Eval(e.DataItem, "fase_orden").ToString();
            //var img = e.Row.Cells[10].Controls[1].FindControl("imgFase") as System.Web.UI.WebControls.Image;
            var lblFechaIni = r[8].ToString();
            var btnOrden = r[0].ToString();
            var status = r[10].ToString();
            //img.ImageUrl = "img/fase_" + fase + ".png";
            try
            {
                var btn = e.Item.Cells[0].Controls[0].FindControl("btnOrden") as LinkButton;
                if (Convert.ToString(status) == "PRE")
                    btn.CssClass = "btn btn-success textoBold colorBlanco";
                else if (Convert.ToString(status) == "SOL")
                    btn.CssClass = "btn btn-warning textoBold colorBlanco";
                else if (Convert.ToString(status) == "APR")
                    btn.CssClass = "btn btn-grey textoBold colorBlanco";
                else if (Convert.ToString(status) == "DES")
                    btn.CssClass = "btn btn-info textoBold colorBlanco";
                else if (Convert.ToString(status) == "CAN")
                    btn.CssClass = "btn btn-danger textoBold colorBlanco";
            }
            catch (Exception ex)
            {

            }
        }
    }

    private bool checaEstatusLlamada(int noOrden)
    {
        bool pendiente = false;
        try
        {
            int[] sesiones = obtieneSesiones();
            int idEmpresa = sesiones[2];
            int idTaller = sesiones[3];
            bool estatus = datosOrdenes.obtieneEstatusLlamada(noOrden, idEmpresa, idTaller);
            if (estatus)
                pendiente = true;
            else
                pendiente = false;
        }
        catch (Exception ex)
        {
            pendiente = false;
        }
        return pendiente;
    }

    private int convierteBooltoInt(string valor)
    {
        int retorno = 0;
        try
        {
            bool dato = Convert.ToBoolean(valor);
            if (dato)
                retorno = 1;
            else
                retorno = 0;
        }
        catch (Exception x)
        {
            retorno = 0;
        }
        return retorno;
    }
    
    protected void ddlNuevaMarca_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ddlNuevaUnidad.Items.Clear();
        ddlNuevaUnidad.Text = string.Empty;
        ddlNuevaUnidad.DataBind();
    }

    protected void ddlNuevoTv_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ddlNuevaUnidad.Items.Clear();
        ddlNuevaUnidad.Text = string.Empty;
        ddlNuevaUnidad.DataBind();
    }

    protected void lnkNuevaRecepcion_Click(object sender, EventArgs e)
    {
        pnlMask.Visible = true;
        pnlNuevaRecepcion.Visible = true;
        lblErrorRecepcion.Text = "";
        txtPlacaNueva.Text = "";
        txtPlacaNueva.Enabled = true;
        //        lnkVerificaPlaca.Visible = true;
        btnGuardarOrden.Visible = false;

        ddlNuevaMarca.Text = ddlNuevoTv.Text = ddlNuevaUnidad.Text = ddlClienteNuevo.Text = "" ;

        limpiaCombo(ddlNuevaMarca);
        ddlNuevaMarca.Enabled = true;

        limpiaCombo(ddlNuevoTv);
        ddlNuevoTv.Enabled = true;

        limpiaCombo(ddlNuevaUnidad);
        ddlNuevaUnidad.Enabled = true;

        txtColorNuevo.Text = "";

        txtNuevoMod.Text = "";
        txtNuevoMod.Enabled = true;


        limpiaCombo(ddlTipoOrdenNuevo);
        limpiaCombo(ddlClienteNuevo);
        limpiaCombo(ddlTipoServNuevo);
        limpiaCombo(ddlTipoValNuevo);
        limpiaCombo(ddlTipoAseguradoNuevo);
        limpiaCombo(ddlLocalizacionNuevo);


        acpVehiculo.Visible = false;
        acpOrden.Visible = false;
        acpOrdPrevias.Visible = false;

        GridView2.DataSource = GridView3.DataSource = null;
        GridView2.DataBind();
        GridView3.DataBind();
    }
    protected void lnkRefressh_Click(object sender, EventArgs e)
    {
        cargaInfo();
    }
    protected void lnkBuscar_Click(object sender, EventArgs e)
    {
        cargaInfo();
    }
    protected void lnkLimpiar_Click(object sender, EventArgs e)
    {
        txtFiltro.Text = "";
        cargaInfo();
    }
    protected void lnkCerrar_Click(object sender, EventArgs e)
    {
        pnlMask.Visible = false;
        pnlNuevaRecepcion.Visible = false;
    }
    protected void lnkVerificaPlaca_Click(object sender, EventArgs e)
    {

    }
    protected void txtPlacaNueva_TextChanged(object sender, EventArgs e)
    {
        /* if (Page.IsValid)
         {*/
        lblErrorRecepcion.Text = "";
        GridView2.DataSource = GridView3.DataSource = null;
        GridView2.DataBind();
        GridView3.DataBind();
        try
        {
            btnGuardarOrden.Visible = false;
            txtColorNuevo.Text = "";
            txtNuevoMod.Text = "";
            txtNuevoMod.Enabled = true;
            acpVehiculo.Visible = false;
            acpOrdPrevias.Visible = false;
            object[] existePlaca = recepciones.existePlaca(txtPlacaNueva.Text);
            if (Convert.ToBoolean(existePlaca[0]))
            {
                if (!Convert.ToBoolean(existePlaca[1]))
                {
                    ddlNuevaMarca.Items.Clear();
                    ddlNuevaMarca.DataBind();
                    ddlNuevoTv.Items.Clear();
                    ddlNuevoTv.DataBind();
                    ddlNuevaUnidad.Items.Clear();
                    ddlNuevaUnidad.DataBind();
                    acpVehiculo.Visible = true;
                    acpOrden.Visible = true;
                    btnGuardarOrden.Visible = true;
                    ddlNuevaMarca.Enabled = true;
                    ddlNuevoTv.Enabled = true;
                    ddlNuevaUnidad.Enabled = true;
                    txtNuevoMod.Enabled = true;

                    lnkAgregaMarca.Visible = lnkAgregaTv.Visible = lnkAgregaUnidad.Visible = true;

                }
                else
                {
                    object[] obtieneInfoVehiculo = recepciones.obtieneInfoVehiculo(txtPlacaNueva.Text);
                    if (Convert.ToBoolean(obtieneInfoVehiculo[0]))
                    {
                        DataSet datos = new DataSet();
                        datos = (DataSet)obtieneInfoVehiculo[1];
                        ddlNuevaMarca.Items.Clear();
                        ddlNuevaMarca.DataBind();
                        ddlNuevoTv.Items.Clear();
                        ddlNuevoTv.DataBind();
                        ddlNuevaUnidad.Items.Clear();
                        ddlNuevaUnidad.DataBind();

                        acpVehiculo.Visible = true;
                        acpOrden.Visible = true;
                        btnGuardarOrden.Visible = true;

                        foreach (DataRow fila in datos.Tables[0].Rows)
                        {
                            ddlNuevaMarca.SelectedValue = fila[0].ToString();
                            ddlNuevoTv.SelectedValue = fila[1].ToString();
                            llenaDdlLinea(fila[0].ToString(), fila[1].ToString(), fila[2].ToString());

                            txtNuevoMod.Text = fila[4].ToString();
                            ddlNuevaMarca.Enabled = false;
                            ddlNuevoTv.Enabled = false;
                            ddlNuevaUnidad.Enabled = false;
                            txtNuevoMod.Enabled = false;
                            txtColorNuevo.Text = fila[5].ToString();
                        }
                        lnkAgregaMarca.Visible = lnkAgregaTv.Visible = lnkAgregaUnidad.Visible = false;


                        int[] sesiones = obtieneSesiones();
                        object[] existenOrdPrev = recepciones.existeOrdenesPrevias(txtPlacaNueva.Text, sesiones[2], sesiones[3]);
                        if (Convert.ToBoolean(existenOrdPrev[0]))
                        {
                            if (!Convert.ToBoolean(existenOrdPrev[1]))
                                acpOrdPrevias.Visible = false;
                            else
                                acpOrdPrevias.Visible = true;
                        }
                    }
                }
            }
            else
            {
                lblErrorRecepcion.Text = "Error: " + existePlaca[1].ToString();
            }
        }
        catch (Exception ex)
        {
            Session["errores"] = ex.Message;
            Session["paginaOrigen"] = "Ordenes.aspx";
            Response.Redirect("AppErrorLog.aspx");
        }
        /*}*/
    }

    private void llenaDdlLinea(string marca, string vehiculo, string linea)
    {
        ddlNuevaUnidad.Items.Clear();
        SqlDataSource4.SelectCommand = "select id_tipo_unidad, UPPER(descripcion) AS descripcion from Tipo_Unidad where id_marca=" + marca + " and id_tipo_vehiculo=" + vehiculo + " and id_tipo_unidad=" + linea;
        try { ddlNuevaUnidad.SelectedIndex = 0; } catch (Exception) { }
        ddlNuevaUnidad.DataBind();
    }

    protected void lnkOrdenPrevia_Click(object sender, EventArgs e)
    {
        GridView2.DataSource = GridView3.DataSource = null;
        GridView2.DataBind();
        GridView3.DataBind();
        LinkButton btn = (LinkButton)sender;
        string orden = btn.CommandArgument;
        SqlDataSource12.SelectCommand = "select distinct mo.no_orden, mo.id_grupo_op, upper(g.descripcion_go) as Grupo from mano_obra mo inner join grupo_operacion g on g.id_grupo_op=mo.id_grupo_op where mo.id_empresa=" + Request.QueryString["e"] + " and mo.id_taller=" + Request.QueryString["t"] + " and mo.no_orden=" + orden + " order by mo.no_orden, mo.id_grupo_op asc";
        GridView2.DataBind();
    }

    protected void lnkGrupo_Click(object sender, EventArgs e)
    {
        GridView3.DataSource = null;
        GridView3.DataBind();
        LinkButton btn = (LinkButton)sender;
        string[] datos = btn.CommandArgument.Split(new char[] { ';' });
        SqlDataSource14.SelectCommand = "select upper(o.descripcion_op+' '+mo.id_refaccion) as mano from mano_obra mo inner join operaciones o on o.id_operacion=mo.id_operacion where mo.id_empresa=" + Request.QueryString["e"] + " and mo.id_taller=" + Request.QueryString["t"] + " and mo.no_orden=" + datos[0] + " and mo.id_grupo_op=" + datos[1];
        GridView3.Columns[0].HeaderText = datos[2];
        GridView3.DataBind();
    }
    protected void ddlEstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargaInfo();
    }

    private void controlAccesos()
    {
        int[] sesiones = obtieneSesiones();
        permisos.idUsuario = sesiones[0];
        /*permisos.obtienePermisos();
        bool[] permisosUsuario = permisos.permisos;
        permisos.permisos = permisosUsuario;*/
        permisos.permiso = 60;
        permisos.tienePermisoIndicado();
        if (!permisos.tienePermiso)
            lnkNuevaRecepcion.Visible = false;
        else
            lnkNuevaRecepcion.Visible = false;

    }
    protected void lnkAgregaMarca_Click(object sender, EventArgs e)
    {
        lblErrorRecepcion.Text = "";
        if (ddlNuevaMarca.Text != "")
        {
            Catalogos cat = new Catalogos();
            object[] existeMarca = cat.existeMarca(ddlNuevaMarca.Text.ToUpper());
            if (Convert.ToBoolean(existeMarca[0]))
            {
                if (Convert.ToBoolean(existeMarca[1]))
                    lblErrorRecepcion.Text = "La marca que intenta dar de alta ya esta registrada";
                else
                {
                    object[] ingresado = cat.agregaMarca(ddlNuevaMarca.Text.ToUpper());
                    if (Convert.ToBoolean(ingresado[0]))
                    {
                        limpiaCombo(ddlNuevaMarca);
                        ddlNuevaMarca.SelectedValue = ingresado[1].ToString();
                    }
                    else
                        lblErrorRecepcion.Text = "No se pudo agregar la marca indicada. Detalle: " + ingresado[1].ToString();
                }
            }
            else
                lblErrorRecepcion.Text = "La marca que intenta dar de alta ya esta registrada";
        }
        else
            lblErrorRecepcion.Text = "Debe indicar una marca para poder darla de alta";
    }

    protected void lnkAgregaTv_Click(object sender, EventArgs e)
    {
        lblErrorRecepcion.Text = "";
        if (ddlNuevoTv.Text != "")
        {
            Catalogos cat = new Catalogos();
            object[] existeMarca = cat.existeTipoVehiculo(ddlNuevoTv.Text.ToUpper());
            if (Convert.ToBoolean(existeMarca[0]))
            {
                if (Convert.ToBoolean(existeMarca[1]))
                    lblErrorRecepcion.Text = "El tipo de Vehículo que intenta dar de alta ya esta registrado";
                else
                {
                    object[] ingresado = cat.agregaTipoVehiculo(ddlNuevoTv.Text.ToUpper());
                    if (Convert.ToBoolean(ingresado[0]))
                    {
                        limpiaCombo(ddlNuevoTv);
                        ddlNuevoTv.SelectedValue = ingresado[1].ToString();
                    }
                    else
                        lblErrorRecepcion.Text = "No se pudo agregar el tipo de vehículo indicado. Detalle: " + ingresado[1].ToString();
                }
            }
            else
                lblErrorRecepcion.Text = "El tipo de Vehículo que intenta dar de alta ya esta registrado";
        }
        else
            lblErrorRecepcion.Text = "Debe indicar un tipo de vehículo para poder darlo de alta";
    }

    protected void lnkAgregaUnidad_Click(object sender, EventArgs e)
    {
        lblErrorRecepcion.Text = "";
        if (ddlNuevaUnidad.Text != "")
        {
            Catalogos cat = new Catalogos();
            object[] existeMarca = cat.existeTipoUnidad(ddlNuevaUnidad.Text.ToUpper());
            if (Convert.ToBoolean(existeMarca[0]))
            {
                int existe = -1;
                try { existe = Convert.ToInt32(existeMarca[1]); }
                catch (Exception) { existe = -1; }


                if (existe == -1)
                    lblErrorRecepcion.Text = "La línea que intenta dar de alta ya esta registrado, con la siguiente calsificación: " + existeMarca[1].ToString();
                else
                {
                    object[] ingresado = cat.agregaTipoUnidad(ddlNuevaMarca.SelectedValue, ddlNuevoTv.SelectedValue, ddlNuevaUnidad.Text.ToUpper());
                    if (Convert.ToBoolean(ingresado[0]))
                    {
                        limpiaCombo(ddlNuevaUnidad);
                        ddlNuevaUnidad.SelectedValue = ingresado[1].ToString();
                    }
                    else
                        lblErrorRecepcion.Text = "No se pudo agregar la línea indicada. Detalle: " + ingresado[1].ToString();
                }
            }
            else
                lblErrorRecepcion.Text = "La línea que intenta dar de alta ya esta registrado o no ingreso los datos correctos";
        }
        else
            lblErrorRecepcion.Text = "Debe indicar una línea para darse de alta";
    }

    protected void lnkClienteNuevo_Click(object sender, EventArgs e)
    {
        lblErrorRecepcion.Text = "";
        if (ddlClienteNuevo.Text != "")
        {
            Catalogos cat = new Catalogos();
            object[] existenCliente = cat.existeCliente(ddlClienteNuevo.Text.ToUpper());

            if (Convert.ToBoolean(existenCliente[0]))
            {
                if (Convert.ToBoolean(existenCliente[1]))
                    lblErrorRecepcion.Text = "El cliente que intenda dar de alta ya esta registrado";
                else
                {
                    object[] ingresado = cat.agregaCliente(ddlClienteNuevo.Text.ToUpper());
                    if (Convert.ToBoolean(ingresado[0]))
                    {
                        limpiaCombo(ddlClienteNuevo);
                        ddlClienteNuevo.SelectedValue = ingresado[1].ToString();
                    }
                    else
                        lblErrorRecepcion.Text = "No se pudo agregar el cliente indicado. Detalle: " + ingresado[1].ToString();
                }
            }
            else
                lblErrorRecepcion.Text = "No se puede obetener acceso al cliente";
        }
        else
            lblErrorRecepcion.Text = "Debe indicar un cliente para darlo de alta";
    }

   /* protected void Page_Init(object sender, System.EventArgs e)
    {
        RadGrid1.FilterMenu.Items.Clear();
        GridFilterMenu filterMenu = RadGrid1.FilterMenu;
        RadMenuItem menuItem = new RadMenuItem();
        menuItem.Text = "Iguial a";
        menuItem.Value = "EqualTo";
        RadGrid1.FilterMenu.Items.Add(menuItem);

        RadMenuItem menuItem2 = new RadMenuItem();
        menuItem2.Text = "Contiene";
        menuItem2.Value = "Contains";
        RadGrid1.FilterMenu.Items.Add(menuItem2);
        RadMenuItem menuItem3 = new RadMenuItem();
        menuItem3.Text = "Menor que";
        menuItem3.Value = "LessThan";
        RadGrid1.FilterMenu.Items.Add(menuItem3);
        RadMenuItem menuItem4 = new RadMenuItem();
        menuItem4.Text = "Mayor que";
        menuItem4.Value = "GreaterThan";
        RadGrid1.FilterMenu.Items.Add(menuItem4);
        RadMenuItem menuItem5 = new RadMenuItem();
        menuItem5.Text = "Quitar Filtro";
        menuItem5.Value = "NoFilter";
        RadGrid1.FilterMenu.Items.Add(menuItem5);
        filterMenu.ItemClick += new RadMenuEventHandler(filterMenu_ItemClick);
    }*/

    protected void filterMenu_ItemClick(object sender, RadMenuEventArgs e)
    {
        GridFilteringItem filterItem = RadGrid1.MasterTableView.GetItems(GridItemType.FilteringItem)[0] as GridFilteringItem;
        filterItem.FireCommandEvent("Filter", new Pair(e.Item.Value, e.Item.Attributes["columnUniqueName"]));
    }

    protected void lnkAceptarNuevaOrden_Click(object sender, EventArgs e)
    {
        try
        {
            object[] existePlaca = recepciones.existePlaca(txtPlacaNueva.Text);
            if (Convert.ToBoolean(existePlaca[0]))
            {
                int[] datosVehiculo = new int[4] { 0, 0, 0, 0 };

                bool validos = validaCampos();

                if (validos)
                {
                    if (!Convert.ToBoolean(existePlaca[1]))
                    {
                        object[] vehiculoAgregado = recepciones.agregaVehiculo(ddlNuevaMarca.SelectedValue, ddlNuevoTv.SelectedValue, ddlNuevaUnidad.SelectedValue, txtNuevoMod.Text, txtPlacaNueva.Text, txtColorNuevo.Text);
                        if (Convert.ToBoolean(vehiculoAgregado[0]))
                        {
                            object vehiculo = vehiculoAgregado[1];
                            datosVehiculo = obtieneVehiculo(vehiculo);
                            object[] orden = generaOrden(datosVehiculo);
                            if (Convert.ToBoolean(orden[0]))
                            {
                                int ordenGenerada = 0;
                                try { ordenGenerada = Convert.ToInt32(orden[1]); }
                                catch (Exception x)
                                {
                                    ordenGenerada = 0;
                                }
                                if (ordenGenerada != 0)
                                {
                                    pnlNuevaRecepcion.Visible = pnlPregunta.Visible = false;
                                    lblOrdenGen.Text = ordenGenerada.ToString();
                                    pnlOrden.Visible = true;
                                }
                                else
                                    lblErrorRecepcion.Text = "Error: " + orden[1].ToString();
                            }
                            else
                            {
                                lblErrorRecepcion.Text = "Error: " + orden[1].ToString();
                            }
                        }
                        else
                            lblErrorRecepcion.Text = "Error: " + vehiculoAgregado[1].ToString();
                    }
                    else
                    {
                        object[] obtieneInfoVehiculo = recepciones.obtieneInfoVehiculo(txtPlacaNueva.Text);
                        if (Convert.ToBoolean(obtieneInfoVehiculo[0]))
                        {
                            DataSet datos = new DataSet();
                            datos = (DataSet)obtieneInfoVehiculo[1];
                            datosVehiculo = new int[4] { 0, 0, 0, 0 };
                            foreach (DataRow fila in datos.Tables[0].Rows)
                            {
                                datosVehiculo[0] = Convert.ToInt32(fila[0].ToString());
                                datosVehiculo[1] = Convert.ToInt32(fila[1].ToString());
                                datosVehiculo[2] = Convert.ToInt32(fila[2].ToString());
                                datosVehiculo[3] = Convert.ToInt32(fila[3].ToString());
                            }
                            string mensajeActualizacion = actualizaVehiculo(datosVehiculo);
                            if (mensajeActualizacion == "")
                            {
                                object[] orden = generaOrden(datosVehiculo);
                                if (Convert.ToBoolean(orden[0]))
                                {
                                    int ordenGenerada = 0;
                                    try { ordenGenerada = Convert.ToInt32(orden[1]); }
                                    catch (Exception x)
                                    {
                                        ordenGenerada = 0;
                                    }
                                    if (ordenGenerada != 0)
                                    {
                                        pnlNuevaRecepcion.Visible = pnlPregunta.Visible = false;
                                        lblOrdenGen.Text = ordenGenerada.ToString();
                                        pnlOrden.Visible = true;
                                    }
                                    else
                                        lblErrorRecepcion.Text = "Error: " + orden[1].ToString();
                                }
                                else
                                {
                                    lblErrorRecepcion.Text = "Error: " + orden[1].ToString();
                                }
                            }
                            else
                            {
                                lblErrorRecepcion.Text = "Error: " + mensajeActualizacion;
                            }
                        }
                        else
                        {
                            lblErrorRecepcion.Text = "Error: " + obtieneInfoVehiculo[1].ToString();
                        }
                    }
                }                
            }
            else
            {
                lblErrorRecepcion.Text = "Error: " + existePlaca[1].ToString();
            }
        }
        catch (Exception ex)
        {
            Session["errores"] = ex.Message;
            Session["paginaOrigen"] = "Ordenes.aspx";
            Response.Redirect("AppErrorLog.aspx");
        }
        finally
        {
            RadGrid1.DataBind();
        }
    }

    protected void lnkCancelarNuevaOrden_Click(object sender, EventArgs e)
    {
        pnlNuevaRecepcion.Visible = pnlPregunta.Visible = pnlMask.Visible = false;
        lblOrdenGen.Text = "";
        pnlOrden.Visible = false;
        RadGrid1.DataBind();
    }

    protected void lnkAbrirWindow_Click(object sender, EventArgs e)
    {
        Response.Redirect("SolicitudGrupos.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
        RadGrid1.DataBind();
    }

    protected void PresolicitadoClick(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        string estatus = "PRE";
        SqlDataSource1.SelectCommand = "select * from an_creditos  where id_empresa=" + sesiones[2] + " and id_sucursal=" + sesiones[3] + " and estatus='" + estatus+"'";
        RadGrid1.DataBind();
    }
    protected void SolicitadoClick(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        string estatus = "SOL";
        SqlDataSource1.SelectCommand = "select * from an_creditos  where id_empresa=" + sesiones[2] + " and id_sucursal=" + sesiones[3] + " and estatus='" + estatus + "'";
    }
    protected void AprobadoClick(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        string estatus = "APR";
        SqlDataSource1.SelectCommand = "select * from an_creditos  where id_empresa=" + sesiones[2] + " and id_sucursal=" + sesiones[3] + " and estatus='" + estatus + "'";
    }
    protected void DesembolsadoClick(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        string estatus = "DES";
        SqlDataSource1.SelectCommand = "select * from an_creditos  where id_empresa=" + sesiones[2] + " and id_sucursal=" + sesiones[3] + " and estatus='" + estatus + "'";
    }

    protected void CanceladoClick(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        string estatus = "CAN";
        SqlDataSource1.SelectCommand = "select * from an_creditos  where id_empresa=" + sesiones[2] + " and id_sucursal=" + sesiones[3] + " and estatus='" + estatus + "'";
    }

}