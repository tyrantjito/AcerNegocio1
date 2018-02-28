using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

public partial class OrdenesCompras : System.Web.UI.Page
{
    Recepciones recepciones = new Recepciones();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargaDatosPie();
            lblEntrada.Text = lblDetalle.Text = "0";
            lnkAceptar.Visible = false;
            lnkAceptar.CommandArgument = "";
            Permisos permiso = new Permisos();
            permiso.idUsuario = Convert.ToInt32(Request.QueryString["u"]);
            permiso.permiso = 113;
            permiso.tienePermisoIndicado();
            lnkCompraNoRegistrada.Visible = permiso.tienePermiso;

            string estatus = obtieneEstatus();
            if (estatus != "")
            {
                if (estatus == "R" || estatus == "T" || estatus == "F" || estatus == "S" || estatus == "C")
                    lnkCompraNoRegistrada.Visible = false;
            }
            else
                Response.Redirect("BienvenidaOrdenes.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"] + "&o=" + Request.QueryString["o"] + "&f=" + Request.QueryString["f"]);

        }
    }

    private string obtieneEstatus()
    {
        string estatus = "";
        try
        {
            int[] sesiones = obtieneSesiones();
            object[] infoEstatus = recepciones.obtieneEstatusOrden(sesiones[2].ToString(), sesiones[3].ToString(), sesiones[4].ToString());
            if (Convert.ToBoolean(infoEstatus[0]))
                estatus = Convert.ToString(infoEstatus[1]);
            else
                estatus = "";
        }
        catch (Exception) { estatus = ""; }
        return estatus;
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
        int[] sesiones = obtieneSesiones();
        object[] datosOrden = recepciones.obtieneInfoOrdenPie(sesiones[4], sesiones[2], sesiones[3]);
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
    protected void lnkSeleccionar_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] argumentos = btn.CommandArgument.ToString().Split(new char[] { ';' });
        Response.Redirect("DetalleOrdenCompra.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"] + "&o=" + Request.QueryString["o"] + "&f=" + Request.QueryString["f"] + "&c=" + argumentos[0] + "&fc=" + argumentos[1] + "&pr=" + argumentos[3] + "&s=" + argumentos[2]);
    }

    protected void lnkCancelar_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string argumentos = btn.CommandArgument.ToString();
        OrdenCompra ordenCompra = new OrdenCompra();
        int[] sesiones = obtieneSesiones();
        object[] actualiza = ordenCompra.actualizaCancelacion(argumentos, sesiones);
        if (Convert.ToBoolean(actualiza[0]))
        {
            GridView1.DataBind();
            actualizaFase();
        }
        else
            lblError.Text = "Error: " + Convert.ToString(actualiza[1]);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnSelecciona = e.Row.FindControl("lnkCancelar") as LinkButton;
            string estatus = DataBinder.Eval(e.Row.DataItem, "estatus").ToString();
            if (estatus == "CAN")
                btnSelecciona.Visible = false;
        }
    }
    private void actualizaFase()
    {
        int faseSActual = 1;
        int empresa = Convert.ToInt32(Request.QueryString["e"]);
        int taller = Convert.ToInt32(Request.QueryString["t"]);
        int orden = Convert.ToInt32(Request.QueryString["o"]);
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

    protected void lnkCompraNoRegistrada_Click(object sender, EventArgs e)
    {
        lblEntrada.Text = lblDetalle.Text = "0";
        limpiaCamposDetalle();
        limpiaCamposEncabezado();
        lnkAceptar.Visible = false;
        string script = "abreWinCtrl()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "modales", script, true);
    }
    
    protected void RadGrid1_ItemDeleted(object source, GridDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            e.ExceptionHandled = true;
            lblErrorNew.Text = "No se pudo eliminar la refacción de la orden de compra indicada. Detalle: " + e.Exception.Message;
        }
        else
        {
            lblErrorNew.Text = "Refacción Eliminada Correctamente";
            OrdenCompra orden = new OrdenCompra();
            int[] sesiones = obtieneSesiones();
            orden.actualizaTotalEncabezado(sesiones, lblEntrada.Text);
        }
    }
      

    protected void txtCostoUnitarioMod_TextChanged(object sender, EventArgs e)
    {
        decimal cu = 0, porc = 0, cant = 0;
        try
        {
            try { cant = Convert.ToDecimal(TxtCantidadMod.Text); } catch (Exception) { cant = 0; }
            try { cu = Convert.ToDecimal(txtCostoUnitarioMod.Text); } catch (Exception) { cu = 0; }
            try { porc = Convert.ToDecimal(txtPorcDesc.Text); } catch (Exception) { porc = 0; }
            lblImporteDescMod.Text = ((cant * cu) * (porc / 100)).ToString("F2");
            lblImporteMod.Text = ((cant * cu) - Convert.ToDecimal(lblImporteDescMod.Text)).ToString("F2");
        }
        catch (Exception) { lblImporteDescMod.Text = lblImporteMod.Text = "0.00"; }
        finally { txtPorcDesc.Focus(); }
    }

    protected void txtPorcDesc_TextChanged(object sender, EventArgs e)
    {
        decimal cu = 0, porc = 0, cant = 0;
        try
        {
            try { cant = Convert.ToDecimal(TxtCantidadMod.Text); } catch (Exception) { cant = 0; }
            try { cu = Convert.ToDecimal(txtCostoUnitarioMod.Text); } catch (Exception) { cu = 0; }
            try { porc = Convert.ToDecimal(txtPorcDesc.Text); } catch (Exception) { porc = 0; }
            lblImporteDescMod.Text = ((cant * cu) * (porc / 100)).ToString("F2");
            lblImporteMod.Text = ((cant * cu) - Convert.ToDecimal(lblImporteDescMod.Text)).ToString("F2");
        }
        catch (Exception) { lblImporteDescMod.Text = lblImporteMod.Text = "0.00"; }
        finally { ddlProcedenciaMod.Focus(); }
    }
    
    protected void lnkAceptarEntrada_Click(object sender, EventArgs e)
    {
        lblErrorNew.Text = "";
        try {
            OrdenCompra orden = new OrdenCompra();
            int[] sesiones = obtieneSesiones();
            object[] agregado = orden.agregaOrdenIndependiente(sesiones, txtFolioCompraNew.Text, txtFacturaNew.Text, txtFechaRecepcion.Text, RadTimeFechaRecepcion.SelectedTime.ToString(), txtNombreNew.Text, ddlProveedorNew.SelectedValue);
            if (Convert.ToBoolean(agregado[0]))
            {
                lblEntrada.Text = Convert.ToString(agregado[1]);
                lnkAceptarEntrada.Visible = false;
                lnkAceptar.Visible = true;
                lblErrorNew.Text = "Orden generada proceda a registrar las refacciones";
                ddlRefaccionMod.Items.Clear();
                ddlRefaccionMod.DataBind();
                RadGrid1.DataBind();
            }
            else
                lblErrorNew.Text = "Error al agregar la entrada. Detalle: " + Convert.ToString(agregado[1]);
        } catch (Exception ex) { }
    }

    protected void TxtCantidadMod_TextChanged(object sender, EventArgs e)
    {
        decimal cu = 0, porc = 0, cant = 0;
        try {
            try { cant = Convert.ToDecimal(TxtCantidadMod.Text); } catch (Exception) { cant = 0; }
            try { cu = Convert.ToDecimal(txtCostoUnitarioMod.Text); } catch (Exception) { cu = 0; }
            try { porc = Convert.ToDecimal(txtPorcDesc.Text); } catch (Exception) { porc = 0; }
            lblImporteDescMod.Text = ((cant * cu) * (porc / 100)).ToString("F2");
            lblImporteMod.Text = ((cant * cu) - Convert.ToDecimal(lblImporteDescMod.Text)).ToString("F2");
        } catch (Exception) { lblImporteDescMod.Text = lblImporteMod.Text = "0.00"; }
        finally { txtCostoUnitarioMod.Focus(); }      
    }

    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblDetalle.Text = RadGrid1.SelectedItems.ToString();
        lnkAceptar.Visible = false;
        limpiaCamposDetalle();
        OrdenCompra orden = new OrdenCompra();
        int[] sesiones = obtieneSesiones();
        object[] infoDetalle= orden.obtieneInfoOrdenDetalle(sesiones, lblEntrada.Text, lblDetalle.Text);
        if (Convert.ToBoolean(infoDetalle[0])) {
            DataSet detalles = (DataSet)infoDetalle[1];
            foreach (DataRow r in detalles.Tables[0].Rows) {
                ddlRefaccionMod.SelectedValue = r[5].ToString();
                TxtCantidadMod.Text = r[6].ToString();
                txtCostoUnitarioMod.Text = Convert.ToDecimal(r[7]).ToString("F2");
                txtPorcDesc.Text = Convert.ToDecimal(r[8]).ToString("F2");
                lblImporteDescMod.Text= Convert.ToDecimal(r[9]).ToString("F2");
                lblImporteMod.Text= Convert.ToDecimal(r[10]).ToString("F2");
                try { ddlProcedenciaMod.SelectedValue = r[11].ToString(); } catch (Exception) { ddlProcedenciaMod.SelectedValue = "0"; }
                ddlRefaccionMod.Enabled = false;
                lnkAceptar.Visible = true;
            }
        }
        else {
            lblErrorRef.Text = "No se puede obtener la información de la refacción. Detalle: " + Convert.ToString(infoDetalle[1]);
            limpiaCamposDetalle();
        }
    }

    protected void lnkCancelar_Click1(object sender, EventArgs e)
    {
        lblErrorRef.Text = "";
        limpiaCamposDetalle();                
    }

    protected void lnkAceptar_Click(object sender, EventArgs e)
    {
        OrdenCompra orden = new OrdenCompra();
        int[] sesiones = obtieneSesiones();
        lblErrorRef.Text = lblErrorNew.Text = "";
        string argumento = "";
        try
        {
            bool correcto = false;
            try { argumento = lnkAceptar.CommandArgument; correcto = true; } catch (Exception) { correcto = false; argumento = ""; }
            if (argumento == "" && correcto)
            {
                bool valido = false;
                try { string valor = ddlRefaccionMod.SelectedValue; valido = true; } catch (Exception) { valido = false; }
                object[] existe = new object[] { false, "" };
                if (!valido)
                    existe = new object[] { false, "" };
                else
                    existe = orden.existeRefaccionOrden(sesiones, ddlRefaccionMod.SelectedValue);
                if (Convert.ToBoolean(existe[0]))
                {
                    if (Convert.ToBoolean(existe[1]))
                    {

                        object[] actualizado = orden.agregaDetalleOrdenIndependiente(sesiones, lblEntrada.Text, lblDetalle.Text, ddlRefaccionMod.SelectedValue, TxtCantidadMod.Text, txtCostoUnitarioMod.Text, txtPorcDesc.Text, lblImporteDescMod.Text, lblImporteMod.Text, ddlProcedenciaMod.SelectedValue, ddlRefaccionMod.SelectedItem.Text);
                        if (Convert.ToBoolean(actualizado[0]))
                        {
                            orden.actualizaTotalEncabezado(sesiones, lblEntrada.Text);
                            GridView1.DataBind();
                            Facturas facturas = new Facturas();
                            facturas.folio = Convert.ToInt32(Request.QueryString["o"]);
                            facturas.tipoCuenta = "PA";
                            facturas.factura = txtFacturaNew.Text;
                            CatClientes clientes = new CatClientes();
                            string politica = clientes.obtieneClavePolitica(Convert.ToDecimal(ddlProveedorNew.SelectedValue));
                            facturas.id_cliprov = Convert.ToInt32(ddlProveedorNew.SelectedValue);
                            facturas.politica = politica;
                            facturas.estatus = "PEN";
                            facturas.empresa = Convert.ToInt32(Request.QueryString["e"]);
                            facturas.taller = Convert.ToInt32(Request.QueryString["t"]);
                            facturas.tipoCargo = "C";
                            decimal importeVal = Convert.ToDecimal(lblImporteMod.Text);
                            importeVal = importeVal + (importeVal * Convert.ToDecimal(0.16));
                            facturas.Importe = importeVal;
                            facturas.montoPagar = importeVal;
                            facturas.orden = Convert.ToInt32(Request.QueryString["o"]);
                            facturas.razon_social = ddlProveedorNew.Text;
                            facturas.monto = recepciones.obtieneMonto(Convert.ToInt32(Request.QueryString["e"]), Convert.ToInt32(Request.QueryString["t"]), Convert.ToInt32(Request.QueryString["o"]), txtFacturaNew.Text, ddlProveedorNew.SelectedValue);
                            facturas.fechaRevision = new E_Utilities.Fechas().obtieneFechaLocal();
                            facturas.indicaFechas = true;
                            try
                            {
                                Ejecuciones ejecuta = new Ejecuciones();
                                string idPolitica = politica;
                                string sql = "select dias_plazo from politica_pago where id_politica=" + idPolitica;
                                object[] ejecutado = ejecuta.scalarToString(sql);
                                int diasPlazo = 0;
                                if ((bool)ejecutado[0])
                                    diasPlazo = Convert.ToInt32(ejecutado[1].ToString());

                                DateTime fechaRevison = new E_Utilities.Fechas().obtieneFechaLocal();
                                DateTime fechaProgPago = fechaRevison.AddDays(diasPlazo);
                                facturas.fechaProgPago = fechaProgPago;

                            }
                            catch (Exception) { }


                            facturas.existeFactura();
                            if (Convert.ToBoolean(facturas.retorno[0]))
                            {
                                if (Convert.ToInt32(facturas.retorno[1]) == 0)
                                    facturas.generaFactura();
                                else
                                {
                                    decimal total = orden.obtieneTotalOrden(sesiones, lblEntrada.Text);
                                    importeVal = total;
                                    importeVal = importeVal + (importeVal * Convert.ToDecimal(0.16));
                                    facturas.Importe = importeVal;
                                    facturas.montoPagar = importeVal;
                                    facturas.actualizaTotalFactura();
                                }
                            }
                            lblErrorNew.Text = "Refacción actualizada";
                            lnkAceptar.Visible = true;
                            limpiaCamposDetalle();
                            RadGrid1.DataBind();
                        }
                        else
                            lblErrorNew.Text = "Error al actualizar la información. Detalle: " + Convert.ToString(actualizado[1]);

                    }
                    else
                    {
                        object[] agregado = orden.agregaRefaccion(sesiones, ddlRefaccionMod.Text, TxtCantidadMod.Text, txtCostoUnitarioMod.Text, txtPorcDesc.Text, lblImporteMod.Text, ddlProveedorNew.SelectedValue);
                        if (Convert.ToBoolean(agregado[0]))
                        {
                            object[] actualizado = orden.agregaDetalleOrdenIndependiente(sesiones, lblEntrada.Text, lblDetalle.Text, Convert.ToString(agregado[1]), TxtCantidadMod.Text, txtCostoUnitarioMod.Text, txtPorcDesc.Text, lblImporteDescMod.Text, lblImporteMod.Text, ddlProcedenciaMod.SelectedValue, ddlRefaccionMod.Text);
                            if (Convert.ToBoolean(actualizado[0]))
                            {
                                orden.actualizaTotalEncabezado(sesiones, lblEntrada.Text);
                                GridView1.DataBind();
                                Facturas facturas = new Facturas();
                                facturas.folio = Convert.ToInt32(Request.QueryString["o"]);
                                facturas.tipoCuenta = "PA";
                                facturas.factura = txtFacturaNew.Text;
                                CatClientes clientes = new CatClientes();
                                string politica = clientes.obtieneClavePolitica(Convert.ToDecimal(ddlProveedorNew.SelectedValue));
                                facturas.id_cliprov = Convert.ToInt32(ddlProveedorNew.SelectedValue);
                                facturas.politica = politica;
                                facturas.estatus = "PEN";
                                facturas.empresa = Convert.ToInt32(Request.QueryString["e"]);
                                facturas.taller = Convert.ToInt32(Request.QueryString["t"]);
                                facturas.tipoCargo = "C";
                                decimal importeVal = Convert.ToDecimal(lblImporteMod.Text);
                                importeVal = importeVal + (importeVal * Convert.ToDecimal(0.16));
                                facturas.Importe = importeVal;
                                facturas.montoPagar = importeVal;
                                facturas.orden = Convert.ToInt32(Request.QueryString["o"]);
                                facturas.razon_social = ddlProveedorNew.Text;
                                facturas.monto = recepciones.obtieneMonto(Convert.ToInt32(Request.QueryString["e"]), Convert.ToInt32(Request.QueryString["t"]), Convert.ToInt32(Request.QueryString["o"]), txtFacturaNew.Text, ddlProveedorNew.SelectedValue);
                                facturas.indicaFechas = true;
                                facturas.fechaRevision = new E_Utilities.Fechas().obtieneFechaLocal();

                                try
                                {
                                    Ejecuciones ejecuta = new Ejecuciones();
                                    string idPolitica = politica;
                                    string sql = "select dias_plazo from politica_pago where id_politica=" + idPolitica;
                                    object[] ejecutado = ejecuta.scalarToString(sql);
                                    int diasPlazo = 0;
                                    if ((bool)ejecutado[0])
                                        diasPlazo = Convert.ToInt32(ejecutado[1].ToString());

                                    DateTime fechaRevison = new E_Utilities.Fechas().obtieneFechaLocal();
                                    DateTime fechaProgPago = fechaRevison.AddDays(diasPlazo);
                                    facturas.fechaProgPago = fechaProgPago;

                                }
                                catch (Exception) { }

                                facturas.existeFactura();
                                if (Convert.ToBoolean(facturas.retorno[0]))
                                {
                                    if (Convert.ToInt32(facturas.retorno[1]) == 0)
                                        facturas.generaFactura();
                                    else
                                    {
                                        decimal total = orden.obtieneTotalOrden(sesiones, lblEntrada.Text);
                                        importeVal = total;
                                        importeVal = importeVal + (importeVal * Convert.ToDecimal(0.16));
                                        facturas.Importe = importeVal;
                                        facturas.montoPagar = importeVal;
                                        facturas.actualizaTotalFactura();
                                    }
                                }
                                lblErrorNew.Text = "Refacción actualizada";
                                lnkAceptar.Visible = true;
                                limpiaCamposDetalle();
                                RadGrid1.DataBind();
                            }
                            else
                                lblErrorNew.Text = "Error al actualizar la información. Detalle: " + Convert.ToString(actualizado[1]);
                        }
                        else
                            lblErrorNew.Text = "Error al agregar la información. Detalle: " + Convert.ToString(agregado[1]);
                    }

                }
                else
                {
                    object[] agregado = orden.agregaRefaccion(sesiones, ddlRefaccionMod.Text, TxtCantidadMod.Text, txtCostoUnitarioMod.Text, txtPorcDesc.Text, lblImporteMod.Text, ddlProveedorNew.SelectedValue);
                    if (Convert.ToBoolean(agregado[0]))
                    {
                        object[] actualizado = orden.agregaDetalleOrdenIndependiente(sesiones, lblEntrada.Text, lblDetalle.Text, Convert.ToString(agregado[1]), TxtCantidadMod.Text, txtCostoUnitarioMod.Text, txtPorcDesc.Text, lblImporteDescMod.Text, lblImporteMod.Text, ddlProcedenciaMod.SelectedValue, ddlRefaccionMod.Text);
                        if (Convert.ToBoolean(actualizado[0]))
                        {
                            orden.actualizaTotalEncabezado(sesiones, lblEntrada.Text);
                            GridView1.DataBind();
                            Facturas facturas = new Facturas();
                            facturas.folio = Convert.ToInt32(Request.QueryString["o"]);
                            facturas.tipoCuenta = "PA";
                            facturas.factura = txtFacturaNew.Text;
                            CatClientes clientes = new CatClientes();
                            string politica = clientes.obtieneClavePolitica(Convert.ToDecimal(ddlProveedorNew.SelectedValue));
                            facturas.id_cliprov = Convert.ToInt32(ddlProveedorNew.SelectedValue);
                            facturas.politica = politica;
                            facturas.estatus = "PEN";
                            facturas.empresa = Convert.ToInt32(Request.QueryString["e"]);
                            facturas.taller = Convert.ToInt32(Request.QueryString["t"]);
                            facturas.tipoCargo = "C";
                            decimal importeVal = Convert.ToDecimal(lblImporteMod.Text);
                            importeVal = importeVal + (importeVal * Convert.ToDecimal(0.16));
                            facturas.Importe = importeVal;
                            facturas.montoPagar = importeVal;
                            facturas.orden = Convert.ToInt32(Request.QueryString["o"]);
                            facturas.razon_social = ddlProveedorNew.Text;
                            facturas.monto = recepciones.obtieneMonto(Convert.ToInt32(Request.QueryString["e"]), Convert.ToInt32(Request.QueryString["t"]), Convert.ToInt32(Request.QueryString["o"]), txtFacturaNew.Text, ddlProveedorNew.SelectedValue);
                            facturas.indicaFechas = true;
                            facturas.fechaRevision = new E_Utilities.Fechas().obtieneFechaLocal();                            
                            try
                            {
                                Ejecuciones ejecuta = new Ejecuciones();
                                string idPolitica = politica;
                                string sql = "select dias_plazo from politica_pago where id_politica=" + idPolitica;
                                object[] ejecutado = ejecuta.scalarToString(sql);
                                int diasPlazo = 0;
                                if ((bool)ejecutado[0])
                                    diasPlazo = Convert.ToInt32(ejecutado[1].ToString());

                                DateTime fechaRevison = new E_Utilities.Fechas().obtieneFechaLocal();
                                DateTime fechaProgPago = fechaRevison.AddDays(diasPlazo);
                                facturas.fechaProgPago = fechaProgPago;

                            }
                            catch (Exception) { }

                            facturas.existeFactura();
                            if (Convert.ToBoolean(facturas.retorno[0]))
                            {
                                if (Convert.ToInt32(facturas.retorno[1]) == 0)
                                    facturas.generaFactura();
                                else
                                {
                                    decimal total = orden.obtieneTotalOrden(sesiones, lblEntrada.Text);
                                    importeVal = total;
                                    importeVal = importeVal + (importeVal * Convert.ToDecimal(0.16));
                                    facturas.Importe = importeVal;
                                    facturas.montoPagar = importeVal;
                                    facturas.actualizaTotalFactura();
                                }
                            }
                            lblErrorNew.Text = "Refacción actualizada";
                            lnkAceptar.Visible = true;
                            limpiaCamposDetalle();
                            RadGrid1.DataBind();
                        }
                        else
                            lblErrorRef.Text = "Error al actualizar la información. Detalle: " + Convert.ToString(actualizado[1]);
                        //lblErrorRef.Text = "No se puede agregar la refacción a la orden. Detalle: " + Convert.ToString(existe[1]);
                    }
                }
            }
            else if (argumento != "" && correcto)
            {
                string[] argumentos = argumento.Split(new char[] { ';' });
                int idDetalle = 0, idRefaccion = 0;
                try { idDetalle = Convert.ToInt32(argumentos[0]); } catch (Exception) { idDetalle = 0; }
                try { idRefaccion = Convert.ToInt32(argumentos[1]); } catch (Exception) { idRefaccion = 0; }
                if (idDetalle != 0)
                {
                    object[] actualizado = orden.actualizaDetalleOrdenIndependiente(sesiones, lblEntrada.Text, idDetalle.ToString(), idRefaccion.ToString(), TxtCantidadMod.Text, txtCostoUnitarioMod.Text, txtPorcDesc.Text, lblImporteDescMod.Text, lblImporteMod.Text, ddlProcedenciaMod.SelectedValue, ddlRefaccionMod.Text);
                    if (Convert.ToBoolean(actualizado[0]))
                    {
                        orden.actualizaTotalEncabezado(sesiones, lblEntrada.Text);
                        GridView1.DataBind();
                        Facturas facturas = new Facturas();
                        facturas.folio = Convert.ToInt32(Request.QueryString["o"]);
                        facturas.tipoCuenta = "PA";
                        facturas.factura = txtFacturaNew.Text;
                        CatClientes clientes = new CatClientes();
                        string politica = clientes.obtieneClavePolitica(Convert.ToDecimal(ddlProveedorNew.SelectedValue));
                        facturas.id_cliprov = Convert.ToInt32(ddlProveedorNew.SelectedValue);
                        facturas.politica = politica;
                        facturas.estatus = "PEN";
                        facturas.empresa = Convert.ToInt32(Request.QueryString["e"]);
                        facturas.taller = Convert.ToInt32(Request.QueryString["t"]);
                        facturas.tipoCargo = "C";
                        decimal importeVal = Convert.ToDecimal(lblImporteMod.Text);
                        importeVal = importeVal + (importeVal * Convert.ToDecimal(0.16));
                        facturas.Importe = importeVal;
                        facturas.montoPagar = importeVal;
                        facturas.orden = Convert.ToInt32(Request.QueryString["o"]);
                        facturas.razon_social = ddlProveedorNew.Text;
                        facturas.monto = recepciones.obtieneMonto(Convert.ToInt32(Request.QueryString["e"]), Convert.ToInt32(Request.QueryString["t"]), Convert.ToInt32(Request.QueryString["o"]), txtFacturaNew.Text, ddlProveedorNew.SelectedValue);
                        facturas.indicaFechas = true;
                        facturas.fechaRevision = new E_Utilities.Fechas().obtieneFechaLocal();
                        try
                        {
                            Ejecuciones ejecuta = new Ejecuciones();
                            string idPolitica = politica;
                            string sql = "select dias_plazo from politica_pago where id_politica=" + idPolitica;
                            object[] ejecutado = ejecuta.scalarToString(sql);
                            int diasPlazo = 0;
                            if ((bool)ejecutado[0])
                                diasPlazo = Convert.ToInt32(ejecutado[1].ToString());

                            DateTime fechaRevison = new E_Utilities.Fechas().obtieneFechaLocal();
                            DateTime fechaProgPago = fechaRevison.AddDays(diasPlazo);
                            facturas.fechaProgPago = fechaProgPago;

                        }
                        catch (Exception) { }
                        facturas.existeFactura();
                        if (Convert.ToBoolean(facturas.retorno[0]))
                        {
                            if (Convert.ToInt32(facturas.retorno[1]) == 0)
                                facturas.generaFactura();
                            else
                            {
                                decimal total = orden.obtieneTotalOrden(sesiones, lblEntrada.Text);
                                importeVal = total;
                                importeVal = importeVal + (importeVal * Convert.ToDecimal(0.16));
                                facturas.Importe = importeVal;
                                facturas.montoPagar = importeVal;
                                facturas.actualizaTotalFactura();
                            }
                        }
                        lblErrorNew.Text = "Refacción Actualizada Correctamente";
                        lnkAceptar.Visible = true;
                        limpiaCamposDetalle();
                        RadGrid1.DataBind();
                    }
                }
            }
            else
                lblErrorNew.Text = "Error al actualizar y/o agregar la refacción.";
        }
        catch (Exception ex)
        {
            lblErrorNew.Text = "Error al agregar refacción. Detalle: " + ex.Message;
        }
        finally
        {
            orden.actualizaTotalEncabezado(sesiones, lblEntrada.Text);
            lnkAceptar.CommandArgument = "";
        }        
        
    }

    protected void ddlRefaccionMod_SelectedIndexChanged1(object sender, EventArgs e)
    {
        Refacciones refaccion = new Refacciones();
        int[] sesiones = obtieneSesiones();
        refaccion._empresa = sesiones[2];
        refaccion._taller = sesiones[3];
        refaccion._orden = sesiones[4];
        refaccion._refaccion = Convert.ToInt32(ddlRefaccionMod.SelectedValue);
        object[] cantidad = refaccion.obtieneCantidad();
        if (Convert.ToBoolean(cantidad[0]))
        {
            TxtCantidadMod.Text = Convert.ToString(cantidad[1]);
            txtCostoUnitarioMod.Text = "";
            txtPorcDesc.Text = "";
        }
        else
        {
            TxtCantidadMod.Text = "0";
            txtCostoUnitarioMod.Text = "";
            txtPorcDesc.Text = "";
        }
    }

    private void limpiaCamposDetalle() {
        ddlRefaccionMod.Items.Clear();
        ddlRefaccionMod.Text = "";
        ddlRefaccionMod.DataBind();        
        try
        {
            Refacciones refaccion = new Refacciones();
            int[] sesiones = obtieneSesiones();
            refaccion._empresa = sesiones[2];
            refaccion._taller = sesiones[3];
            refaccion._orden = sesiones[4];
            refaccion._refaccion = Convert.ToInt32(ddlRefaccionMod.SelectedValue);
            object[] cantidad = refaccion.obtieneCantidad();
            if (Convert.ToBoolean(cantidad[0]))
                TxtCantidadMod.Text = Convert.ToString(cantidad[1]);
            else
                TxtCantidadMod.Text = "0";
        }
        catch (Exception ex) { TxtCantidadMod.Text = "0"; }
        txtCostoUnitarioMod.Text = "";
        txtPorcDesc.Text = "";
        ddlProcedenciaMod.Items.Clear();
        ddlProcedenciaMod.DataBind();
        lblImporteDescMod.Text = lblImporteMod.Text = "0.00";
    }
    private void limpiaCamposEncabezado() {
        txtFolioCompraNew.Text = txtFacturaNew.Text = ddlProveedorNew.SelectedValue = ddlProveedorNew.Text = txtFechaRecepcion.Text = txtNombreNew.Text = "";
        RadTimeFechaRecepcion.Clear();
    }

    protected void ddlRefaccionMod_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        try
        {
            Refacciones refaccion = new Refacciones();
            int[] sesiones = obtieneSesiones();
            refaccion._empresa = sesiones[2];
            refaccion._taller = sesiones[3];
            refaccion._orden = sesiones[4];
            refaccion._refaccion = Convert.ToInt32(ddlRefaccionMod.SelectedValue);
            object[] cantidad = refaccion.obtieneCantidad();
            if (Convert.ToBoolean(cantidad[0]))
            {
                TxtCantidadMod.Text = Convert.ToString(cantidad[1]);
                txtCostoUnitarioMod.Text = "";
                txtPorcDesc.Text = "";
            }
            else
            {
                TxtCantidadMod.Text = "1";
                txtCostoUnitarioMod.Text = "";
                txtPorcDesc.Text = "";
            }
        }
        catch (Exception ex)
        {
            TxtCantidadMod.Text = "1";
            txtCostoUnitarioMod.Text = "0";
            txtPorcDesc.Text = "0";
            TxtCantidadMod.Focus();
        }
        finally { TxtCantidadMod.Focus(); }
    }

    protected void lnkEliminar_Click(object sender, EventArgs e)
    {
        OrdenCompra orden = new OrdenCompra();
        int[] sesiones = obtieneSesiones();
        LinkButton btn = (LinkButton)sender;
        string[] argumentos = btn.CommandArgument.ToString().Split(new char[] { ';' });
        int idDetalle = Convert.ToInt32(argumentos[0]);
        int idRefaccion = Convert.ToInt32(argumentos[1]);
        object[] actualizado = orden.eliminaDetalleOrdenIndependiente(sesiones, lblEntrada.Text, idDetalle, idRefaccion);
        if (Convert.ToBoolean(actualizado[0]))
        {
            orden.actualizaTotalEncabezado(sesiones, lblEntrada.Text);            
            GridView1.DataBind();
            Facturas facturas = new Facturas();
            facturas.folio = Convert.ToInt32(Request.QueryString["o"]);
            facturas.tipoCuenta = "PA";
            facturas.factura = txtFacturaNew.Text;
            CatClientes clientes = new CatClientes();
            string politica = clientes.obtieneClavePolitica(Convert.ToDecimal(ddlProveedorNew.SelectedValue));
            facturas.id_cliprov = Convert.ToInt32(ddlProveedorNew.SelectedValue);
            facturas.politica = politica;
            facturas.estatus = "PEN";
            facturas.empresa = Convert.ToInt32(Request.QueryString["e"]);
            facturas.taller = Convert.ToInt32(Request.QueryString["t"]);
            facturas.tipoCargo = "C";
            decimal importeVal = Convert.ToDecimal(lblImporteMod.Text);
            importeVal = importeVal + (importeVal * Convert.ToDecimal(0.16));
            facturas.Importe = importeVal;
            facturas.montoPagar = importeVal;
            facturas.orden = Convert.ToInt32(Request.QueryString["o"]);
            facturas.razon_social = ddlProveedorNew.Text;
            facturas.monto = recepciones.obtieneMonto(Convert.ToInt32(Request.QueryString["e"]), Convert.ToInt32(Request.QueryString["t"]), Convert.ToInt32(Request.QueryString["o"]), txtFacturaNew.Text, ddlProveedorNew.SelectedValue);


            facturas.existeFactura();
            if (Convert.ToBoolean(facturas.retorno[0]))
            {
                if (Convert.ToInt32(facturas.retorno[1]) == 0)
                    facturas.generaFactura();
                else
                {
                    decimal total = orden.obtieneTotalOrden(sesiones, lblEntrada.Text);
                    importeVal = total;
                    importeVal = importeVal + (importeVal * Convert.ToDecimal(0.16));
                    facturas.Importe = importeVal;
                    facturas.montoPagar = importeVal;
                    facturas.actualizaTotalFactura();
                }
            }
            lblErrorNew.Text = "Refacción Eliminada Correctamente";
            lnkAceptar.Visible = true;
            limpiaCamposDetalle();
            RadGrid1.DataBind();
        }
    }

    protected void ddlRefaccionMod_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Refacciones refaccion = new Refacciones();
            int[] sesiones = obtieneSesiones();
            refaccion._empresa = sesiones[2];
            refaccion._taller = sesiones[3];
            refaccion._orden = sesiones[4];
            refaccion._refaccion = Convert.ToInt32(ddlRefaccionMod.SelectedValue);
            object[] cantidad = refaccion.obtieneCantidad();
            if (Convert.ToBoolean(cantidad[0]))
            {
                TxtCantidadMod.Text = Convert.ToString(cantidad[1]);
                txtCostoUnitarioMod.Text = "";
                txtPorcDesc.Text = "";
            }
            else
            {
                TxtCantidadMod.Text = "1";
                txtCostoUnitarioMod.Text = "";
                txtPorcDesc.Text = "";
            }
        }
        catch (Exception ex)
        {
            TxtCantidadMod.Text = "1";
            txtCostoUnitarioMod.Text = "0";
            txtPorcDesc.Text = "0";
            TxtCantidadMod.Focus();
        }
        finally { TxtCantidadMod.Focus(); }
    }

    protected void lnkEditar_Click(object sender, EventArgs e)
    {
        OrdenCompra orden = new OrdenCompra();
        int[] sesiones = obtieneSesiones();
        LinkButton btn = (LinkButton)sender;
        lnkAceptar.CommandArgument = btn.CommandArgument;
        string[] argumentos = btn.CommandArgument.ToString().Split(new char[] { ';' });
        int idDetalle = Convert.ToInt32(argumentos[0]);
        int idRefaccion = Convert.ToInt32(argumentos[1]);
        object[] datos = orden.obtieneInfoOrdenDetalle(sesiones, lblEntrada.Text, idDetalle.ToString());
        if (Convert.ToBoolean(datos[0]))
        {
            DataSet infoRef = (DataSet)datos[1];
            foreach (DataRow r in infoRef.Tables[0].Rows) {
                //ddlRefaccionMod,TxtCantidadMod,txtCostoUnitarioMod,txtPorcDesc,lblImporteDescMod,lblImporteMod,ddlProcedenciaMod
                ddlRefaccionMod.Text = r[6].ToString();
                TxtCantidadMod.Text = Convert.ToDecimal(r[7]).ToString("F2");
                txtCostoUnitarioMod.Text = Convert.ToDecimal(r[8]).ToString("F2");
                txtPorcDesc.Text= Convert.ToDecimal(r[9]).ToString("F2");
                lblImporteDescMod.Text= Convert.ToDecimal(r[10]).ToString("F2");
                lblImporteMod.Text= Convert.ToDecimal(r[11]).ToString("F2");
                ddlProcedenciaMod.Items.Clear();
                try { ddlProcedenciaMod.SelectedValue = r[12].ToString(); } catch (Exception) { }
                ddlProcedenciaMod.DataBind();
                break;
            }
        }

    }
}