using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using E_Utilities;

public partial class Cronologicos : System.Web.UI.Page
{
    ManoObraOrden datosMO = new ManoObraOrden();
    Recepciones recepciones = new Recepciones();
    Permisos permisos = new Permisos();
    string correo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtNoOrden.Text = "";
            lblError.Text = "";
            txtAvance.Text = "";
            txtComentario.Text = "";
            revisaPermiso();
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

    protected void btnBuscarAsignaciones_Click(object sender, EventArgs e)//juan-14-12-15
    {

        pnlCronos.Visible = false;
        lnkGuardaFechas.Visible = false;
        if (txtNoOrden.Text.Trim() != "")
        {
            lblError.Text = "";
            txtAvance.Text = "";
            txtComentario.Text = "";
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
                        pnlCronos.Visible = true;
                        lnkGuardaFechas.Visible = true;
                        cargaDatosPie();
                        cargaCronos();
                    }
                    else
                    {
                        lblError.Text = "El número de orden no existe.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message; ;
            }
        }
        else
        {

            lblError.Text = "Necesita colocar un número de orden.";
        }
    }

    private void cargaDatosPie()
    {
        int empresa = Convert.ToInt32(Request.QueryString["e"]);
        int taller = Convert.ToInt32(Request.QueryString["t"]);
        int orden = Convert.ToInt32(txtNoOrden.Text);
        object[] datosOrden = recepciones.obtieneInfoOrdenPie(orden, empresa, taller);
        if (Convert.ToBoolean(datosOrden[0]))
        {
            DataSet ordenDatos = (DataSet)datosOrden[1];
            foreach (DataRow filaOrd in ordenDatos.Tables[0].Rows)
            {
                lblFaseIni.Text = filaOrd[6].ToString();
                lblLocalizacionIni.Text = filaOrd[7].ToString();
                lblAvanceIni.Text = filaOrd[8].ToString();
                lblPerfilIni.Text = filaOrd[12].ToString();
                correo = filaOrd[15].ToString();
            }
        }
    }

    private void cargaCronos()
    {
        int[] sesiones = obtieneSesiones();
        Cronos cronologico = new Cronos();
        cronologico.Empresa = sesiones[2];
        cronologico.Taller = sesiones[3];
        cronologico.Orden = Convert.ToInt32(txtNoOrden.Text);
        DataTable dtSegOrd = cronologico.obtieneDtSegOrden();

        foreach (DataRow dr in dtSegOrd.Rows)
        {
            Label[] ingreso = { lblFRecepcion, lblHoraRecepcion };
            TextBox[] datosTxtBox = { txtf_retorno_transito, txtf_alta, txtf_valuacion, txtf_autorizacion, txtf_asignacion, txtf_tocado, txtf_primer_llamada, txtf_alta_portal, txtf_ult_entrega_ref, txtf_promesa_portal,
                            txtf_terminado, txtf_terminacion, txtf_entrega_estimada, txtf_baja_portal, txtf_entrega, txtf_pactada,  txtf_prog_retorno_tran,  txtf_confirmacion,  txtf_complemento };
            Telerik.Web.UI.RadTimePicker[] picks = { timph_terminacion, timph_estrega_estimada, timph_pactada, timph_prog_retorno_tran, timph_confirmacion };

            lblFRecepcion.Text = convierteFecha(Convert.ToString(dr[4]), 0);
            lblHoraRecepcion.Text = convierteFecha(Convert.ToString(dr[5]), 1);
            datosTxtBox[0].Text = convierteFecha(Convert.ToString(dr[6]), 2);
            datosTxtBox[1].Text = convierteFecha(Convert.ToString(dr[7]), 3);
            datosTxtBox[2].Text = convierteFecha(Convert.ToString(dr[8]), 4);
            datosTxtBox[3].Text = convierteFecha(Convert.ToString(dr[9]), 5);
            datosTxtBox[4].Text = convierteFecha(Convert.ToString(dr[10]), 6);
            datosTxtBox[5].Text = convierteFecha(Convert.ToString(dr[11]), 7);
            datosTxtBox[6].Text = convierteFecha(Convert.ToString(dr[12]), 8);
            datosTxtBox[7].Text = convierteFecha(Convert.ToString(dr[13]), 9);
            datosTxtBox[8].Text = convierteFecha(Convert.ToString(dr[14]), 10);
            datosTxtBox[9].Text = convierteFecha(Convert.ToString(dr[15]), 11);
            datosTxtBox[10].Text = convierteFecha(Convert.ToString(dr[16]), 12);
            datosTxtBox[11].Text = convierteFecha(Convert.ToString(dr[17]), 13);
            try { picks[0].SelectedDate = Convert.ToDateTime(dr[18].ToString()); } catch (Exception ex) { picks[0].Clear(); }
            datosTxtBox[12].Text = convierteFecha(Convert.ToString(dr[19]), 15);
            try { picks[1].SelectedDate = Convert.ToDateTime(dr[20].ToString()); } catch (Exception ex) { picks[1].Clear(); }
            datosTxtBox[13].Text = convierteFecha(Convert.ToString(dr[21]), 17);
            datosTxtBox[14].Text = convierteFecha(Convert.ToString(dr[22]), 18);
            datosTxtBox[15].Text = convierteFecha(Convert.ToString(dr[23]), 19);
            try { picks[2].SelectedDate = Convert.ToDateTime(dr[24].ToString()); } catch (Exception ex) { picks[2].Clear(); }
            datosTxtBox[16].Text = convierteFecha(Convert.ToString(dr[25]), 21);
            try { picks[3].SelectedDate = Convert.ToDateTime(dr[26].ToString()); } catch (Exception ex) { picks[3].Clear(); }
            datosTxtBox[17].Text = convierteFecha(Convert.ToString(dr[27]), 23);
            try { picks[4].SelectedDate = Convert.ToDateTime(dr[28].ToString()); } catch (Exception ex) { picks[4].Clear(); }
            datosTxtBox[18].Text = convierteFecha(Convert.ToString(dr[29]), 25);
        }


        ddlLocalizacion.SelectedValue = lblLocalizacionIni.Text;
        ddlPerfil.SelectedValue = lblPerfilIni.Text;
        txtAvance.Text = lblAvanceIni.Text;
        if (txtf_terminado.Text != "")
            lnkEnviarCarta.Visible = true;
        else
            lnkEnviarCarta.Visible = false;

    }

    private string convierteFecha(string valor, int dato)
    {
        string retorno = ""; ;
        DateTime fecha = Convert.ToDateTime("1900-01-01");
        try
        {
            fecha = Convert.ToDateTime(valor);
            if (dato == 1 || dato == 14 || dato == 16 || dato == 20 || dato == 22 || dato == 24)
            {
                if (fecha.ToString("HH:mm:ss") == "00:00:00")
                {
                    retorno = "";
                }
                else
                {
                    retorno = fecha.ToString("HH:mm:ss");
                }
            }
            else
            {
                if (fecha.ToString("yyyy-MM-dd") == "1900-01-01")
                    retorno = "";
                else
                    retorno = fecha.ToString("yyyy-MM-dd");
            }
        }
        catch (Exception) { retorno = ""; }
        return retorno;
    }

    protected void lnkGuardaFechas_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        decimal avance = -1;
        try { avance = Convert.ToDecimal(txtAvance.Text.Trim()); }
        catch (Exception) { avance = -1; }
        if (avance != -1)
        {
            bool esTerm_Ent = (ddlLocalizacion.SelectedValue == "3" || ddlLocalizacion.SelectedValue == "4");
            if (esTerm_Ent && !revisaVoBo())
                lblError.Text = "Para colocar la orden como Terminada o Entregada, todas las operaciones deben estar con VoBo.";
            else
            {
                if (esTerm_Ent && avance != 100)
                    lblError.Text = "Para colocar la orden como Terminada o Entregada, debe tener una avance del 100 %";
                else
                {
                    string mnsg = string.Empty;
                    string colFecha = string.Empty;
                    int[] sesiones = obtieneSesiones();
                    Cronos cronologico = new Cronos();
                    cronologico.Empresa = sesiones[2];
                    cronologico.Taller = sesiones[3];
                    cronologico.Orden = Convert.ToInt32(txtNoOrden.Text);
                    cronologico.obtieneFechas();
                    string[] fechOld = cronologico.Fechas;
                    string[] datosArmados = new string[26];

                    TextBox[] datosTxtBox = { txtf_retorno_transito, txtf_alta, txtf_valuacion, txtf_autorizacion, txtf_asignacion, txtf_tocado, txtf_primer_llamada, txtf_alta_portal, txtf_ult_entrega_ref, txtf_promesa_portal,
                            txtf_terminado, txtf_terminacion, null, txtf_entrega_estimada, null, txtf_baja_portal, txtf_entrega, txtf_pactada, null, txtf_prog_retorno_tran, null, txtf_confirmacion, null, txtf_complemento };
                    //              10                 11      12            13             14              15             16                   18                            20                       22

                    int j = 0;
                    for (int i = 2; i < datosArmados.Length; i++)
                    {
                        if (esTerm_Ent && datosTxtBox[j] != null && string.IsNullOrEmpty(datosTxtBox[j].Text))
                            switch (j)
                            {
                                case 1:
                                    mnsg += "Alta, ";
                                    break;
                                /*case 2:
                                    mnsg += "Valuación, ";
                                    break;
                                case 3:
                                    mnsg += "Autorización, ";
                                    break;*/
                                case 4:
                                    mnsg += "Asignado, ";
                                    break;
                                case 5:
                                    mnsg += "Tocado, ";
                                    break;
                                case 6:
                                    mnsg += "Primer llamada, ";
                                    break;
                                case 7:
                                    mnsg += "Alta Portal, ";
                                    break;
                                case 8:
                                    mnsg += "Última Entrega de Refacción, ";
                                    break;
                                case 9:
                                    mnsg += "Promesa Portal, ";
                                    break;
                                case 13:
                                    mnsg += "Promesa, ";
                                    break;
                                case 16:
                                    mnsg += "Entrega Expediente.";
                                    break;
                            }
                        else
                        {
                            switch (i)
                            {
                                case 14:
                                    try { datosArmados[14] = timph_terminacion.SelectedTime.Value.ToString(); } catch (Exception ex) { datosArmados[14] = null; }// timph_terminacion.Hour == 0 ? "" : (timph_terminacion.Hour.ToString() + ":" + timph_terminacion.Minute.ToString() +":" + timph_terminacion.Second.ToString());
                                    break;
                                case 16:
                                    try
                                    {
                                        datosArmados[16] = timph_estrega_estimada.SelectedTime.Value.ToString();
                                    }
                                    catch (Exception ex) { datosArmados[16] = null; }//timph_estrega_estimada.Hour == 0 ? "" : (timph_estrega_estimada.Hour.ToString() + ":" + timph_estrega_estimada.Minute.ToString() + ":" + timph_estrega_estimada.Second.ToString());
                                    break;
                                case 20:
                                    try
                                    {
                                        datosArmados[20] = timph_pactada.SelectedTime.Value.ToString();
                                    }
                                    catch (Exception ex) { datosArmados[20] = null; }//timph_pactada.Hour == 0 ? "" : timph_pactada.Hour.ToString() + ":" + timph_pactada.Minute.ToString() + ":" + timph_pactada.Second.ToString();
                                    break;
                                case 22:
                                    try
                                    {
                                        datosArmados[22] = timph_prog_retorno_tran.SelectedTime.Value.ToString();
                                    }
                                    catch (Exception ex) { datosArmados[22] = null; }//timph_prog_retorno_tran.Hour == 0 ? "" : timph_prog_retorno_tran.Hour.ToString() + ":" + timph_prog_retorno_tran.Minute.ToString() + ":" + timph_prog_retorno_tran.Second.ToString();
                                    break;
                                case 24:
                                    try
                                    { datosArmados[24] = timph_confirmacion.SelectedTime.Value.ToString(); }
                                    catch (Exception ex) { datosArmados[24] = null; }//.Hour == 0 ? "" : timph_confirmacion.Hour.ToString() + ":" + timph_confirmacion.Minute.ToString() + ":" + timph_confirmacion.Second.ToString();
                                    break;
                                default:
                                    {
                                        datosArmados[i] = datosTxtBox[j].Text;
                                        break;
                                    }
                            }
                            //Datos para la bitacora, se usa el ID del textbox para saber el campo que cambia
                            if (datosTxtBox[j] != null && convierteFecha(fechOld[i], i) != (datosArmados[i] == "1900-01-01" ? "" : datosArmados[i] == "00:00:00" ? "" : datosArmados[i]))
                                colFecha += datosTxtBox[j].ID.Substring(3) + " de " + (convierteFecha(fechOld[i], i) == "" ? "NULL" : convierteFecha(fechOld[i], i)) + " a " + datosArmados[i] + ", ";
                        }
                        j++;
                    }

                    if (string.IsNullOrEmpty(mnsg))
                    {
                        cronologico.Fechas = datosArmados;
                        cronologico.actualizaCronos();
                        object[] actualizados = cronologico.Actualizacion;
                        if (Convert.ToBoolean(actualizados[0]))
                        {
                            if(!string.IsNullOrEmpty(colFecha))
                                Cronos.addBitacora(txtNoOrden.Text, "A", colFecha, sesiones[0], sesiones);
                            Recepciones recepciones = new Recepciones();
                            actualizados = recepciones.actualizaOrdenDatos(sesiones[2], sesiones[3], Convert.ToInt32(txtNoOrden.Text), ddlLocalizacion.SelectedValue, txtAvance.Text, ddlPerfil.SelectedValue);
                            if (Convert.ToBoolean(actualizados[0]))
                            {
                                BitacorasComentarios bitacora = new BitacorasComentarios();
                                bitacora.Empresa = sesiones[2];
                                bitacora.Taller = sesiones[3];
                                bitacora.Orden = Convert.ToInt32(txtNoOrden.Text);
                                bitacora.Usuario = sesiones[0];

                                if (ddlLocalizacion.SelectedValue != lblLocalizacionIni.Text)
                                {
                                    bitacora.Bitacora = 2;
                                    bitacora.Valor = ddlLocalizacion.SelectedValue;
                                    bitacora.agregaRegistro();
                                    actualizados = bitacora.Afectado;
                                    if (Convert.ToBoolean(actualizados[0]))
                                    {
                                        lblError.Text = "El cronológico se actualizó Correctamente";
                                    }
                                    else
                                        lblError.Text = "Error al actualizar bitacora de localizaciones: " + Convert.ToString(actualizados[1]);
                                }

                                if (ddlPerfil.SelectedValue != lblPerfilIni.Text)
                                {
                                    bitacora.Bitacora = 4;
                                    bitacora.Valor = ddlPerfil.SelectedValue;
                                    bitacora.agregaRegistro();
                                    actualizados = bitacora.Afectado;
                                    if (Convert.ToBoolean(actualizados[0]))
                                    {
                                        if (ddlPerfil.SelectedValue == "2")
                                        {
                                            bool actualizaRetTrans = bitacora.actualizaRetornoTransitoProg();
                                            if (actualizaRetTrans)
                                                lblError.Text = "El cronológico se actualizó Correctamente";
                                            else
                                                lblError.Text = "Error al actualizar bitacora de perfiles: " + Convert.ToString(actualizados[1]);
                                        }
                                        else
                                            lblError.Text = "El cronológico se actualizó Correctamente";
                                    }
                                    else
                                        lblError.Text = "Error al actualizar bitacora de perfiles: " + Convert.ToString(actualizados[1]);
                                }

                                if (txtAvance.Text != lblAvanceIni.Text)
                                {
                                    bitacora.Bitacora = 1;
                                    bitacora.Valor = txtAvance.Text;
                                    bitacora.agregaRegistro();
                                    actualizados = bitacora.Afectado;
                                    if (Convert.ToBoolean(actualizados[0]))
                                        lblError.Text = "El cronológico se actualizó Correctamente";
                                    else
                                        lblError.Text = "Error al actualizar bitacora de avances: " + Convert.ToString(actualizados[1]);
                                }


                                if (txtComentario.Text != "")
                                {

                                    bitacora.Bitacora = 3;
                                    bitacora.Valor = txtComentario.Text;
                                    bitacora.agregaRegistro();
                                    actualizados = bitacora.Afectado;
                                    if (Convert.ToBoolean(actualizados[0]))
                                        lblError.Text = "El cronológico se actualizó Correctamente";
                                    else
                                        lblError.Text = "Error al agregar el comentario: " + Convert.ToString(actualizados[1]);
                                }

                                if (txtf_terminado.Text != "")
                                    lnkEnviarCarta.Visible = true;
                                else
                                    lnkEnviarCarta.Visible = false;
                                lblError.Text = "Cronologico actualizado correctamente";
                            }
                            else
                                lblError.Text = "Error al actualizar los datos de la Orden: " + Convert.ToString(actualizados[1]);
                        }
                        else
                            lblError.Text = "Error al actualizar el cronológico de la orden: " + Convert.ToString(actualizados[1]);

                        //acutalizaFase();
                    }
                    else
                        lblError.Text = string.Format("Para colocar la orden como Terminada o Entregada, se debe especificar fecha de: {0} ",
                            mnsg.EndsWith(", ") ? mnsg.Substring(0, mnsg.Length - 2) + '.' : mnsg);
                }
            }
        }
        else
            lblError.Text = "Debe indicar un avance correcto";
    }

    private void acutalizaFase()
    {
        int faseSActual = Convert.ToInt32(lblFaseIni.Text);
        int empresa = Convert.ToInt32(Request.QueryString["e"]);
        int taller = Convert.ToInt32(Request.QueryString["t"]);
        int orden = Convert.ToInt32(txtNoOrden.Text);
        try
        {
            object[] datos = recepciones.obtieneInfoOrden(orden, empresa, taller);
            if (Convert.ToBoolean(datos[0]))
            {
                DataSet valores = (DataSet)datos[1];
                foreach (DataRow fila in valores.Tables[0].Rows)
                {
                    faseSActual = Convert.ToInt32(fila[53].ToString());
                }

                if (faseSActual < 4)
                {
                    recepciones.actualizaFaseOrden(orden, taller, empresa, 4);
                }
            }
        }
        catch (Exception) { }
    }

    protected void lnkEnviarCarta_Click(object sender, EventArgs e)
    {
        cargaDatosPie();
        int[] sesiones = obtieneSesiones();
        Envio_Mail enviar = new Envio_Mail();
        DatosVehiculos vehiculos = new DatosVehiculos();
        string marca, tipo, modelo, orden, placas, prefijo;
        marca = tipo = modelo = orden = placas = prefijo = "";

        object[] vehiculo = vehiculos.obtieneDatosBasicosVehiculoCorreo(Convert.ToInt32(txtNoOrden.Text), sesiones[2], sesiones[3]);
        if (Convert.ToBoolean(vehiculo[0]))
        {
            DataSet valores = (DataSet)vehiculo[1];
            foreach (DataRow fila in valores.Tables[0].Rows)
            {
                orden = fila[0].ToString();
                marca = fila[1].ToString();
                tipo = fila[2].ToString();
                modelo = fila[3].ToString();
                placas = fila[4].ToString();
                prefijo = fila[5].ToString();
            }
        }

        string mensaje = string.Format("<table width='553' border='0' align='center' cellpadding='0' cellspacing='0'>" +
       "<tr><td><p>&nbsp;</p><p>&nbsp;</p></td></tr>" +
       "<tr><td><img src='http://www.formulasistemas.com/empresas/logoMoncar.png' widht='200' height='100'></td></tr><tr><td><p>&nbsp;</p></td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr>" +
       "<tr><td><p align='justify'>Le informamos que la reparaci&oacute;n de su veh&iacute;culo ({0} {1}, modelo {2}, placas {5} y OT {3}{4}) ha sido concluida y le recordamos que puede acudir a nuestras instalaciones por su unidad a partir del d&iacute;a de hoy en nuestros horarios habituales de Lunes a Viernes de 9:00 a 14:00 hrs. y de 15:00 a 18:00 hrs., S&aacute;bado de 9:00 a 14:00 hrs.</p></td></tr>" +
       "<tr><td>&nbsp;</td></tr>" +
       "<tr><td><p align='justify'>No olvide presentar su identificaci&oacute;n oficial as&iacute; como su comprobante de pago de deducible en caso de aplicar, recuerde que seguimos a sus &oacute;rdenes brind&aacute;ndole atenci&oacute;n telef&oacute;nica si cuenta con alguna duda en particular.</p></td></tr>" +
       "<tr><td>&nbsp;</td></tr><tr><td><p>&nbsp;</p></td></tr><tr><td><b>&nbsp;</b></td></tr>" +
       "<tr><td><p>Para Moncar Aztahuacan, S.A. de C.V. ha sido un placer atenderle.<br>&nbsp;</p></td></tr><tr><td>Gracias por su preferencia</td></tr><tr><td>&nbsp;<br>&nbsp;</td></tr></table>", marca, tipo, modelo, prefijo, orden, placas);
        object[] enviado = enviar.obtieneDatosServidor("", correo.ToLower().Trim(), mensaje, "", "Término de Reparación", null, sesiones[2], "", "");
        if (Convert.ToBoolean(enviado[0]))
            lblError.Text = "Se ha enviado la información de la orden vía correo electrónico";
        else
            lblError.Text = "No pudo enviar el correo electrónico, intente de nuevo. Detalle: " + Convert.ToString(enviado[1]);
    }

    private void revisaPermiso()
    {
        int[] sesiones = obtieneSesiones();
        permisos.idUsuario = sesiones[0];
        //permisos.obtienePermisos();
        //bool[] permisosUsuario = permisos.permisos;

        LinkButton[] botones = { lnkEnviarCarta };

        int[] codigos = { 92 };

        //permisos.permisos = permisosUsuario;
        for (int i = 0; i < codigos.Length; i++)
        {
            permisos.permiso = codigos[i];
            permisos.tienePermisoIndicado();
            if (!permisos.tienePermiso)
                botones[i].Visible = false;
            else
                botones[i].Visible = true;
        }
    }

    private bool revisaVoBo()
    {
        bool tieneVoBo = true;
        string sqlSegOp = "select terminado from seguimiento_operacion mo where mo.no_orden = " + txtNoOrden.Text.Trim() + " and mo.id_empresa = " + Request.QueryString["e"] + " and mo.id_taller = " + Request.QueryString["t"] + " " +
 "and mo.id_grupo_op in (select tabla.grupo from(select distinct case when CHARINDEX('-', oo.idgops) = 0 then oo.idgops else substring(oo.idgops, 1, CHARINDEX('-', oo.idgops) - 1) end as grupo " +
 "from operativos_orden oo where oo.no_orden = mo.no_orden and oo.id_empresa = mo.id_empresa and oo.id_taller = mo.id_taller) as tabla) ";
        Ejecuciones ejec = new Ejecuciones();
        DataSet ds = (DataSet)ejec.dataSet(sqlSegOp)[1];
        if (ds.Tables[0].Rows.Count > 0)
            foreach (DataRow rw in ds.Tables[0].Rows)
            {
                if (!(bool)rw["terminado"])
                {
                    tieneVoBo = false;
                    break;
                }
            }
        return tieneVoBo;
    }

    protected void btnEliminaFecha_Click(object sender, EventArgs e)
    {
        LinkButton btnElimina = (LinkButton)sender;
        btnAutoriza.CommandArgument = btnElimina.CommandArgument;
        txtContraseñaLog.Text = txtUsuarioLog.Text = lblErrorLog.Text = "";
        PanelMask.Visible = PanelPopUpPermiso.Visible = true;
    }

    protected void btnCancelAut_Click(object sender, EventArgs e)
    {
        PanelMask.Visible = PanelPopUpPermiso.Visible = false;
    }

    protected void btnAutoriza_Click(object sender, EventArgs e)
    {
        LinkButton btnAut = (LinkButton)sender;
        lblErrorLog.Text = "";
        Autorizaciones autoriza = new Autorizaciones();
        autoriza.nick = txtUsuarioLog.Text;
        autoriza.contrasena = txtContraseñaLog.Text;
        autoriza.permiso = 1;
        autoriza.validaUsuario();
        if (autoriza.Valido)
            borraFecha(Int16.Parse(btnAut.CommandArgument), autoriza.IdUsuario);
        else
            lblErrorLog.Text = "Error: " + autoriza.Error;
    }

    private void borraFecha(Int16 opFecha, int usuAut)
    {
        int[] sesiones = obtieneSesiones();
        string colFecha = "";
        switch (opFecha)
        {
            case 1:
                colFecha = "f_prog_retorno_tran=NULL, h_prog_retorno_tran";
                break;
            case 2:
                colFecha = "f_retorno_transito";
                break;
            case 3:
                colFecha = "f_alta";
                break;
            case 4:
                colFecha = "f_entrega";
                break;
            case 5:
                colFecha = "f_alta_portal";
                break;
            case 6:
                colFecha = "f_valuacion";
                break;
            case 7:
                colFecha = "f_autorizacion";
                break;
            case 8:
                colFecha = "f_primer_llamada";
                break;
            case 9:
                colFecha = "f_entrega_estimada=NULL, h_estrega_estimada";
                break;
            case 10:
                colFecha = "f_asignacion";
                break;
            case 11:
                colFecha = "f_tocado";
                break;
            case 12:
                colFecha = "f_promesa_portal";
                break;
            case 13:
                colFecha = "f_ult_entrega_ref";
                break;
            case 14:
                colFecha = "f_terminado";
                break;
            case 15:
                colFecha = "f_baja_portal";
                break;
            case 16:
                colFecha = "f_pactada=NULL, h_pactada";
                break;
            case 17:
                colFecha = "f_confirmacion=NULL, h_confirmacion";
                break;
            case 18:
                colFecha = "f_terminacion=NULL, h_terminacion";
                break;
            case 19:
                colFecha = "f_complemento";
                break;
        }
        string sqlBorraFecha = string.Format("UPDATE Seguimiento_Orden SET {0}=NULL WHERE no_orden={1} and id_empresa={2} and id_taller={3}",colFecha, txtNoOrden.Text, sesiones[2], sesiones[3]);
        Ejecuciones ejec = new Ejecuciones();
        ejec.scalarToInt(sqlBorraFecha);
        Cronos.addBitacora(txtNoOrden.Text, "B", colFecha, usuAut, sesiones);
        PanelMask.Visible = PanelPopUpPermiso.Visible = false;
        cargaCronos();
    }
    
}
