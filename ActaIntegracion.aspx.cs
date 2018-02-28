using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI; 
using iTextSharp.text;
using iTextSharp.text.pdf;
using E_Utilities;

public partial class ActaIntegracion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtFechaIntegracionActa.MaxDate = DateTime.Now;

    }
    protected void lnkAbreWindow_Click(object sender, EventArgs e)
    {
        //string script1 = "abreWin1()";
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "Abre Captura", script1, true);
        //this.pnlMaskCapturaActa
        pnlMaskCapturaActa.Visible = true;
        windowActaIntegracion.Visible = true;
        cmbIdPresidente.DataSourceID = "SqlDataSourcePresidente";
        cmbIdPresidente.DataBind();
    }
    protected void lnkEditaIntegrantesWindow_Click(object sender, EventArgs e)
    {
        pnlMaskCapturaActa.Visible = true;
        windowActaIntegracion.Visible = true;
        pnlEncabezadoActaIntegracion.Visible = false;
        pnlIntegrantesActaIntegracion.Visible = true;
        lblidActa.Text = gridActaIntegracion.SelectedValues["id_acta"].ToString();
    }
    protected void cierrascreen_click(object sender, EventArgs e)
    {
        //string script1 = "abreWin1()";
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "Abre Captura", script1, true);
        //this.pnlMaskCapturaActa
        pnlMaskCapturaActa.Visible = false;
        windowActaIntegracion.Visible = false;

        //seteamos los paneles internos a visibles y listos para funcionar
        
        pnlEncabezadoActaIntegracion.Visible = true;
        //el panel internio de los integrantes sera visible solo al terminar  la  captura del encabezado.
        pnlIntegrantesActaIntegracion.Visible = false;
    }
    private int[] obtieneSesiones()
    {
        int[] sesiones = new int[5];
        sesiones[0] = Convert.ToInt32(Request.QueryString["u"]);
        sesiones[1] = Convert.ToInt32(Request.QueryString["p"]);
        sesiones[2] = Convert.ToInt32(Request.QueryString["e"]);
        sesiones[3] = Convert.ToInt32(Request.QueryString["t"]);
        return sesiones;
    }

    protected void lnkAgregaActaIntegracion_Click(object sender, EventArgs e)
    {
        //lblErrorAgregaActa.Text = "";
        //lblErrorAfuera.Text = "";

        int[] sesiones = obtieneSesiones(); 
        ActInt registro = new ActInt();
        registro.id_empresa = sesiones[2];
        registro.id_sucursal = sesiones[3];
        registro.nombre_grupo_productivo = txtNombreGrupoProductivo.Text;
        registro.estatus = "A";
        registro.hora_acta_integracion = Convert.ToString(txtHoraActaIntegracion.SelectedTime);
        DateTime fecha_integ = Convert.ToDateTime(txtFechaIntegracionActa.SelectedDate);
        registro.fecha_integracion_acta = fecha_integ.ToString("yyyy-MM-dd");
        registro.calle_direccion_de_integracion_acta = txtCalleDireccionDeIntegracionActa.Text;
        registro.numero_direccion_de_integracion_acta = txtNumeroDireccionDeIntegracionActa.Text;
        registro.colonia_direccion_de_integracion_acta = txtColoniaDireccionDeIntegracionActa.Text;
        registro.municipio_direccion_de_integracion_acta = txtMunicipioDireccionDeIntegracionActa.Text;
        registro.estado_direccion_de_integracion_acta = txtEstadoDireccionDeIntegracionActa.Text;
       
        //registro.nombre_presidente = txtNombrePresidente.Text;
        //registro.nombre_secretario = txtNombreSecretario.Text;
        //registro.nombre_tesorero = txtNombreTesorero.Text;
        //registro.nombre_supervisor = txtNombreSupervisor.Text;
        registro.colonia_grupo_productivo = txtColoniaGrupoProductivo.Text;
        registro.colonias_circunvecinas = txtColoniasCircunvecinas.Text;
        registro.monto_ahorro_minimo_semanal = Convert.ToDecimal( txtMontoAhorroMinimoSemanal.Text);
        registro.numero_reparto_ahorro = txtNumeroRepartoAhorro.Text;
        registro.dia_reunion_semanal = txtDiaReunionSemanal.Text;
        registro.hora_reunion_samanal = txtHoraReunionSamanal.Text;
        //registro.nombre_integrante_para_reuniones = txtNombreIntegranteParaReuniones.Text;
        registro.cargo_integrante_para_reuniones = txtCargoIntegranteParaReuniones.Text;
        registro.calle_direccion_reunion = txtCalleDireccionReunion.Text;
        registro.numero_direccion_reunion = txtNumeroDireccionReunion.Text;
        registro.colonia_direccion_reunion = txtColoniaDireccionReunion.Text;
        registro.estado_direccion_reunion = txtEstadoDireccionReunion.Text;
        registro.municipio_direccion_reunion = txtMunicipioDireccionReunion.Text;

        registro.tiempo_tolerancia_reunion = txtTiempoToleranciaReunion.Text;

        registro.multa_retardo_reunion = Convert.ToDecimal(txtMultaRetardoReunion.Text);
        registro.multaFalta_conEnvio_completo = Convert.ToDecimal(txtMultaFaltaConEnvioCompleto.Text);
        registro.multaFalta_conEnvio_incompleto = Convert.ToDecimal(txtMultaFaltaConEnvioIncompleto.Text);
        registro.multaFalta = Convert.ToDecimal(txtMultaFalta.Text);

        registro.hora_termina_reunion = txtHoraTerminaReunion.Text;
        
        registro.id_presidente = Convert.ToInt32(cmbIdPresidente.Text) ;
        registro.id_secretario = Convert.ToInt32(cmbIdSecretario.Text);
        registro.id_tesorero = Convert.ToInt32(cmbIdTesorero.Text);
        registro.id_V1 = Convert.ToInt32(cmbIdSupervisor.Text);
        registro.id_V2 = Convert.ToInt32(cmbIdVocal2.Text);

        registro.agregaActa();

            int numActa = Convert.ToInt32(registro.retorno[1]);
            //guardamos el num ero de acta en la etiqueta
            lblidActa.Text = numActa.ToString();
         
        
        if ( Convert.ToBoolean(registro.retorno[0]))
        {
            // en este punto se ha creado el encabezado
            // se agregran de forma automatica los registros del detalle, al nos los que figuren en los 
            // puestos de presidente, secretario, tesorero, supervidor solo si hay datos en estos campos.


            //Agregando al presidente al detalle de acta (si lo hay)
            if (registro.id_presidente > 0)
            {
                // ok, hay que registrar en el detalle a un integrante con cargo de presidente
                // esto es en el campo "cargo" = "P" => presidente ||| 
                // valores admitidos (S= Secretario, T = tesorero, SP = supervisor, I = interante
                ActaIntDetalle registroPresidente = new ActaIntDetalle();
                registroPresidente.id_empresa = registro.id_empresa;
                registroPresidente.id_sucursal = registro.id_sucursal;
                registroPresidente.id_acta = numActa;
                registroPresidente.id_cliente = registro.id_presidente;
                registroPresidente.cargo = Convert.ToString("P");
                registroPresidente.agregaDetalleActaIntegracion();
                

            }

            //Agregando al secretario al detalle de acta (si lo hay)
            if (registro.id_secretario > 0)
            {
                // ok, hay que registrar en el detalle a un integrante con cargo de secretario
                // esto es en el campo "cargo" = "P" => presidente ||| 
                // valores admitidos (S= Secretario, T = tesorero, SP = supervisor, I = interante
                ActaIntDetalle registroSecretario = new ActaIntDetalle();
                registroSecretario.id_empresa = registro.id_empresa;
                registroSecretario.id_sucursal = registro.id_sucursal;
                registroSecretario.id_acta = numActa;
                registroSecretario.id_cliente = registro.id_secretario;
                registroSecretario.cargo = Convert.ToString("T");
                registroSecretario.agregaDetalleActaIntegracion();


            }
            //Agregando al tesorero al detalle de acta (si lo hay)
            if (registro.id_tesorero > 0)
            {
                // ok, hay que registrar en el detalle a un integrante con cargo de secretario
                // esto es en el campo "cargo" = "P" => presidente ||| 
                // valores admitidos (S= Secretario, T = tesorero, SP = supervisor, I = interante
                ActaIntDetalle registroTesorero = new ActaIntDetalle();
                registroTesorero.id_empresa = registro.id_empresa;
                registroTesorero.id_sucursal = registro.id_sucursal;
                registroTesorero.id_acta = numActa;
                registroTesorero.id_cliente = registro.id_tesorero;
                registroTesorero.cargo = Convert.ToString("S");
                registroTesorero.agregaDetalleActaIntegracion();


            }

            //Agregando al supervisor al detalle de acta (si lo hay)
            if (registro.id_V1 > 0)
            {
                // ok, hay que registrar en el detalle a un integrante con cargo de secretario
                // esto es en el campo "cargo" = "P" => presidente ||| 
                // valores admitidos (S= Secretario, T = tesorero, SP = supervisor, I = interante
                ActaIntDetalle registroSupervisor = new ActaIntDetalle();
                registroSupervisor.id_empresa = registro.id_empresa;
                registroSupervisor.id_sucursal = registro.id_sucursal;
                registroSupervisor.id_acta = numActa;
                registroSupervisor.id_cliente = registro.id_V1;
                registroSupervisor.cargo = Convert.ToString("V1");
                registroSupervisor.agregaDetalleActaIntegracion();


            }

            if (registro.id_V2 > 0)
            {
                // ok, hay que registrar en el detalle a un integrante con cargo de secretario
                // esto es en el campo "cargo" = "P" => presidente ||| 
                // valores admitidos (S= Secretario, T = tesorero, SP = supervisor, I = interante
                ActaIntDetalle registroSupervisor = new ActaIntDetalle();
                registroSupervisor.id_empresa = registro.id_empresa;
                registroSupervisor.id_sucursal = registro.id_sucursal;
                registroSupervisor.id_acta = numActa;
                registroSupervisor.id_cliente = registro.id_V2;
                registroSupervisor.cargo = Convert.ToString("V2");
                registroSupervisor.agregaDetalleActaIntegracion();


            }


            lblidEmpresa.Text = Convert.ToString(registro.id_empresa);
            lblidSucursal.Text = Convert.ToString(registro.id_sucursal);
            lblidActa.Text = Convert.ToString(numActa);
            

            //registro agregado exitosamente
            pnlEncabezadoActaIntegracion.Visible = false;
            //activamos panel para captura de integrantes
            pnlIntegrantesActaIntegracion.Visible = true;
            //
            lblErrorAfuera.Text = "Acta agregada correctamente";
            lblErrorAfuera.Visible = true;
            gridActaIntegracion.DataBind();
            //    ////////////////////////////////////////////
            //pnlMaskCapturaActa.Visible = false;
            //windowActaIntegracion.Visible = false;

            //nuevo 
            //lblidEmpresa.Text = "";
            //lblidSucursal.Text = "";
            //lblidActa.Text = "";

        }
        else
        {
            lblErrorAgregaActa.Text = "Error al agregar el acta";   
        }
    }

    protected void lnkagregaIntegrantes_Click(object sender, EventArgs e)
    {
        lblIntegrantesError.Text = "";
        int[] sesiones = obtieneSesiones();
        ActaIntDetalle registroIntegrante = new ActaIntDetalle();
        registroIntegrante.id_empresa = sesiones[2];
        registroIntegrante.id_sucursal = sesiones[3];
        registroIntegrante.id_acta = Convert.ToInt32( gridActaIntegracion.SelectedValues["id_acta"]);

        registroIntegrante.id_cliente = Convert.ToInt32(cmbIdIntegrante.Text);
        registroIntegrante.cargo = 'I'.ToString(); //se asigna con el cargo (I)ntegrante a la tabla de detalle
        registroIntegrante.agregaDetalleActaIntegracion();
        if (Convert.ToBoolean(registroIntegrante.retorno[0]))
        {
            lblIntegrantesError.Text = "Integrante agregado exitosamente";
            gridIntegrntesActaIntegracion.DataBind();
            cmbIdIntegrante.DataBind();
        }
        else
        {
            lblIntegrantesError.Text = "Error al agregar integrante" + registroIntegrante.retorno[1].ToString();
        }
        SqlDataSource2.DataBind();

    }
    protected void lnkcierraagregaIntegrantes_Click(object sender, EventArgs e)
    {
        //nuevo 
        lblidEmpresa.Text = "";
        lblidSucursal.Text = "";
        lblidActa.Text = "";

        //dejamos paneles listos 
        pnlEncabezadoActaIntegracion.Visible = true;
        //activamos panel para captura de integrantes
        pnlIntegrantesActaIntegracion.Visible = false;

        pnlMaskCapturaActa.Visible = false;
        windowActaIntegracion.Visible = false;
        

    }
    protected void lnkEditaActaWindow_Click(object sender, EventArgs e)
    {

        //obtenemos el id del acta que proviene del grid
        lblidActa.Text = Convert.ToString( gridActaIntegracion.SelectedValues["id_acta"]);

        int[] sesiones = obtieneSesiones();
        ActInt registroEditar = new ActInt();
        registroEditar.id_empresa = sesiones[2];
        registroEditar.id_sucursal = sesiones[3];
        registroEditar.idactaEdita = Convert.ToInt32(lblidActa.Text) ;
        // obtenemos un dataset con el metodo obtieneDatosActa
        registroEditar.obtieneDatosActa();

        //seteamos a los combos normales a visibles = false
        // para activar los combos que usaremos en la edicion
        cmbIdPresidente.Visible = false;
        cmbIdSecretario.Visible = false;
        cmbIdTesorero.Visible = false;
        cmbIdSupervisor.Visible = false;
        cmbIdVocal2.Visible = false;
        cmb_Casa.Visible = false;


        //apagamos el boton de agregar en el panel
        lnkAgregaActaIntegracion.Visible = false;

        // ahora a poner visibles ¿los combos de edicion
        cmbIdPresidenteEditar.Visible = true;
        cmbIdSecretarioEditar.Visible = true;
        cmbIdTesoreroEditar.Visible = true;
        cmbIdSupervisorEditar.Visible = true;
        cmbIdVocal2Editar.Visible = true;
        cmb_CasaEdt.Visible = true;
        

        //prendemos el boton de guardar cambios
        lnkCambiosActaIntegracion.Visible = true;


        if ( Convert.ToBoolean(registroEditar.retorno[0]) )
        {
            // ya tenemos un data set
            // bajamos los datos a los controles
            DataSet ds = (DataSet)registroEditar.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {

                txtHoraActaIntegracion.SelectedDate = Convert.ToDateTime(r[4]);
                txtFechaIntegracionActa.SelectedDate = Convert.ToDateTime(r[5]);
                txtCalleDireccionDeIntegracionActa.Text = Convert.ToString( r[6] );
                txtNumeroDireccionDeIntegracionActa.Text = Convert.ToString(r[7]);
                txtColoniaDireccionDeIntegracionActa.Text = Convert.ToString(r[8]);
                txtMunicipioDireccionDeIntegracionActa.Text = Convert.ToString(r[9]);
                txtEstadoDireccionDeIntegracionActa.Text = Convert.ToString(r[10]);
                txtNombreGrupoProductivo.Text = Convert.ToString(r[34]);
                txtColoniaGrupoProductivo.Text = Convert.ToString(r[11]);
                txtColoniasCircunvecinas.Text = Convert.ToString(r[12]);
                decimal MontoAhorroMinimoSemanal = Convert.ToDecimal(r[13]);
                txtMontoAhorroMinimoSemanal.Text = MontoAhorroMinimoSemanal.ToString("N2");
                txtNumeroRepartoAhorro.Text = Convert.ToString(r[14]);
                txtDiaReunionSemanal.Text = Convert.ToString(r[15]);
                txtHoraReunionSamanal.Text = Convert.ToString(r[16]);
              //  cmb_CasaEdt.SelectedValue = Convert.ToString(r[17]);
                txtCargoIntegranteParaReuniones.Text = Convert.ToString(r[18]);
                txtCalleDireccionReunion.Text = Convert.ToString(r[19]);
                txtNumeroDireccionReunion.Text = Convert.ToString(r[20]);
                txtColoniaDireccionReunion.Text = Convert.ToString(r[21]);

                txtMunicipioDireccionReunion.Text = Convert.ToString(r[22]);
                txtEstadoDireccionReunion.Text = Convert.ToString(r[23]);

                txtTiempoToleranciaReunion.Text = Convert.ToString(r[24]);
                txtMultaRetardoReunion.Text = Convert.ToString(r[25]);
                txtMultaFaltaConEnvioCompleto.Text = Convert.ToString(r[26]);
                txtMultaFaltaConEnvioIncompleto.Text = Convert.ToString(r[27]);
                txtMultaFalta.Text = Convert.ToString(r[28]);
                txtHoraTerminaReunion.Text = Convert.ToString(r[29]);

                cmbIdPresidenteEditar.SelectedValue = Convert.ToString(r[30]);
                cmbIdPresidenteEditar.DataBind();
                

                cmbIdSecretarioEditar.SelectedValue = Convert.ToString(r[31]);
                cmbIdSecretarioEditar.DataBind();

                 cmbIdTesoreroEditar.SelectedValue = Convert.ToString(r[32]);
                 cmbIdTesoreroEditar.DataBind();

                 cmbIdSupervisorEditar.SelectedValue = Convert.ToString(r[33]);
                 cmbIdSupervisorEditar.DataBind();

                cmbIdVocal2Editar.SelectedValue = Convert.ToString(r[34]);
                cmbIdVocal2Editar.DataBind();

            }
          


                }


        //seteamos a visible la ventana | panel para mostrar los datos ya obtenidos
        pnlMaskCapturaActa.Visible = true;
        windowActaIntegracion.Visible = true;
        // seteamos la etiqueta que nos sirve como auxiliar a cero
        //lblidActa.Text = "0";
    }

    protected void gridActaIntegracion_SelectedIndexChanged(object sender, EventArgs e)
    {
        lnkEditaWindow.Visible = true;
        lnkImprimir.Visible = true;
        lnkAgregaIntegrante.Visible = true;

    }

    protected void lnkCambiosActaIntegracion_Click(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        ActInt registroActualiza = new ActInt();
        registroActualiza.id_empresa = sesiones[2];
        registroActualiza.id_sucursal = sesiones[3];
        registroActualiza.id_acta = Convert.ToInt32(lblidActa.Text);
        registroActualiza.hora_acta_integracion = Convert.ToString(txtHoraActaIntegracion.SelectedTime);
        DateTime fecha_integ = Convert.ToDateTime(txtFechaIntegracionActa.SelectedDate);
        registroActualiza.fecha_integracion_acta = fecha_integ.ToString("yyyy-MM-dd");
        registroActualiza.calle_direccion_de_integracion_acta = txtCalleDireccionDeIntegracionActa.Text;
        registroActualiza.numero_direccion_de_integracion_acta = txtNumeroDireccionDeIntegracionActa.Text;
        registroActualiza.colonia_direccion_de_integracion_acta = txtColoniaDireccionDeIntegracionActa.Text;
        registroActualiza.municipio_direccion_de_integracion_acta = txtMunicipioDireccionDeIntegracionActa.Text;
        registroActualiza.estado_direccion_de_integracion_acta = txtEstadoDireccionDeIntegracionActa.Text;
        registroActualiza.nombre_grupo_productivo = txtNombreGrupoProductivo.Text;

        registroActualiza.colonia_grupo_productivo = txtColoniaGrupoProductivo.Text;

        registroActualiza.colonias_circunvecinas = txtColoniasCircunvecinas.Text;

        registroActualiza.monto_ahorro_minimo_semanal = Convert.ToDecimal(txtMontoAhorroMinimoSemanal.Text);

        registroActualiza.numero_reparto_ahorro = txtNumeroRepartoAhorro.Text;
        registroActualiza.dia_reunion_semanal = txtDiaReunionSemanal.Text;
        registroActualiza.hora_reunion_samanal = txtHoraReunionSamanal.Text;
       // registroActualiza.nombre_integrante_para_reuniones = txtNombreIntegranteParaReuniones.Text;
        registroActualiza.cargo_integrante_para_reuniones = txtCargoIntegranteParaReuniones.Text;
        registroActualiza.calle_direccion_reunion = txtCalleDireccionReunion.Text;
        registroActualiza.numero_direccion_reunion = txtNumeroDireccionReunion.Text;
        registroActualiza.colonia_direccion_reunion = txtColoniaDireccionReunion.Text;
        registroActualiza.estado_direccion_reunion = txtEstadoDireccionReunion.Text;
        registroActualiza.municipio_direccion_reunion = txtMunicipioDireccionReunion.Text;
        registroActualiza.tiempo_tolerancia_reunion = txtTiempoToleranciaReunion.Text;

        registroActualiza.multa_retardo_reunion = Convert.ToDecimal(txtMultaRetardoReunion.Text);

        registroActualiza.multaFalta_conEnvio_completo = Convert.ToDecimal(txtMultaFaltaConEnvioCompleto.Text);

        registroActualiza.multaFalta_conEnvio_incompleto = Convert.ToDecimal(txtMultaFaltaConEnvioIncompleto.Text);
        registroActualiza.multaFalta = Convert.ToDecimal(txtMultaFalta.Text);


        registroActualiza.hora_termina_reunion = txtHoraTerminaReunion.Text;



        registroActualiza.id_presidente = Convert.ToInt32(cmbIdPresidenteEditar.Text);
        registroActualiza.id_secretario = Convert.ToInt32(cmbIdSecretarioEditar.Text);
        registroActualiza.id_tesorero = Convert.ToInt32(cmbIdTesoreroEditar.Text);
        registroActualiza.id_V1 = Convert.ToInt32(cmbIdSupervisorEditar.Text);
        registroActualiza.id_V2 = Convert.ToInt32(cmbIdVocal2.Text);


        //lblidSucursal.Text = Convert.ToString(registroActualiza.id_sucursal);
        //lblidActa.Text = Convert.ToString(numActa);
        //aplicamos el Update en la tabla
        registroActualiza.actualizaDatosActa();

        if(Convert.ToBoolean(registroActualiza.retorno[0]))
        {
            //registro Editado exitosamente
            lblErrorAfuera.Text = "Acta editada correctamente";
            lblErrorAfuera.Visible = true;

            //panelesinternos los dejamos activados paneles
            pnlEncabezadoActaIntegracion.Visible = true;
            pnlIntegrantesActaIntegracion.Visible = false;

            //apagamos paneles
            pnlMaskCapturaActa.Visible = false;
            windowActaIntegracion.Visible = false;

            //refrescamos el grid inicial
            gridActaIntegracion.DataBind(); 
        }
        else
        {
            lblErrorAgregaActa.Text = "Error al actualizar el acta" + registroActualiza.retorno[1];
        }
    }

    protected void lnkImprimir_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;
        // Tipo de Font que vamos utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" Acta Integracion ");
        documento.AddCreator("DESARROLLARTE");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\ActaIntegracion_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

        //si no existe la carpeta temporal la creamos 
        if (!(Directory.Exists(ruta)))
            Directory.CreateDirectory(ruta);

        FileInfo docto = new FileInfo(archivo);
        if (docto.Exists)
            docto.Delete();
        if (archivo.Trim() != "")
        {

            FileStream file = new FileStream(archivo,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            FileShare.ReadWrite);
            PdfWriter.GetInstance(documento, file);

            // Abrir documento.
            documento.Open();

            //Insertar logo o imagen  

            string imagepath = HttpContext.Current.Server.MapPath("img/");
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagepath + "logo_aser.png");
            logo.WidthPercentage = 15f;

            //encabezado
            PdfPTable tablaEncabezado = new PdfPTable(1);
            tablaEncabezado.SetWidths(new float[] { 100 });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;
            tablaEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;


            PdfPCell titulo = new PdfPCell(new Phrase("ACTA DE INTEGRACIÓN DE GRUPO PRODUCTIVO", fuente1));

            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_CENTER;
            tablaEncabezado.AddCell(titulo);
            documento.Add(tablaEncabezado);
            documento.Add(new Paragraph(" "));


            ActInt info = new ActInt();

            int[] sesiones = obtieneSesiones();
            info.id_empresa = sesiones[2];
            info.id_sucursal = sesiones[3];
            int ActaIntegracion;
            try { ActaIntegracion = Convert.ToInt32(gridActaIntegracion.SelectedValues["id_acta"]); }
            catch (Exception)
            {
                ActaIntegracion = 0;
            }
            info.idactaEdita = ActaIntegracion;
            info.optieneimoresion();

            if (Convert.ToBoolean(info.retorno[0]))
            {
                DataSet ds = (DataSet)info.retorno[1];

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DateTime feC = Convert.ToDateTime(r[5].ToString());
                    string[] fechacompleta = feC.ToString("yyyy-MM-dd").Split(new char[] { '-' });
                    //TABLA IZQUIERDA
                    PdfPTable tableIzq = new PdfPTable(2);
                    tableIzq.SetWidths(new float[] { 4, 96 });
                    tableIzq.DefaultCell.Border = 0;
                    tableIzq.WidthPercentage = 40f;
                    tableIzq.HorizontalAlignment = Element.ALIGN_LEFT;
                    string horaAct = r[4].ToString().ToUpper();
                    string diaAct = fechacompleta[2];
                    string mesAct = fechacompleta[1];
                    string añoAct = fechacompleta[0];
                    string calleAct = r[6].ToString().ToUpper();
                    string calleNoAct = r[7].ToString().ToUpper();
                    string colAct = r[8].ToString().ToUpper();
                    string delArt = r[9].ToString().ToUpper();
                    string edoArt = r[10].ToString().ToUpper();
                    string GPnameArt = r[11].ToString().ToUpper();

                    if (mesAct == "01")
                    {
                        mesAct = "Enero";
                    }
                    else if (mesAct == "02")
                    {
                        mesAct = "Febrero";
                    }
                    else if (mesAct == "03")
                    {
                        mesAct = "Marzo";
                    }
                    else if (mesAct == "04")
                    {
                        mesAct = "Abril";
                    }
                    else if (mesAct == "05")
                    {
                        mesAct = "Mayo";
                    }
                    else if (mesAct == "06")
                    {
                        mesAct = "Junio";
                    }
                    else if (mesAct == "07")
                    {
                        mesAct = "Julio";
                    }
                    else if (mesAct == "08")
                    {
                        mesAct = "Agosto";
                    }
                    else if (mesAct == "09")
                    {
                        mesAct = "Septiembre";
                    }
                    else if (mesAct == "10")
                    {
                        mesAct = "Octubre";
                    }
                    else if (mesAct == "11")
                    {
                        mesAct = "Noviembre";
                    }
                    else
                    {
                        mesAct = "Diciembre";
                    }

                    PdfPCell prime = (new PdfPCell(new Phrase("Siendo las " + horaAct + " horas del día " + diaAct + " del mes de " + mesAct + " del " + añoAct + " se reunieron las personas abajo citadas en la calle " + calleAct.ToUpper() + " Número " + calleNoAct.ToUpper() + " de la Colonia o Comunidad " + colAct.ToUpper() + " del Municipio o Delegación " + delArt.ToUpper() + " del Estado de " + edoArt.ToUpper() + " con la finalidad de integrar un Grupo Productivo cuyo propósito es solicitar a APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO, S.A. de C.V. SOFOM ENR microcréditos para mejorar sus negocios, elegir a los integrantes de la mesa directiva y elaborar el reglamento interno del Grupo.", fuente6)) { Colspan = 2 });
                    prime.Border = 0;
                    prime.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    tableIzq.AddCell(prime);

                    PdfPCell prime1 = (new PdfPCell(new Phrase("ACUERDOS", fuente6)) { Colspan = 2 });
                    prime1.Border = 0;
                    prime1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableIzq.AddCell(prime1);

                    PdfPCell uno = new PdfPCell(new Phrase("1. ", fuente6));
                    uno.Border = 0;
                    uno.HorizontalAlignment = Element.ALIGN_LEFT;
                    tableIzq.AddCell(uno);

                    PdfPCell uno1 = new PdfPCell(new Phrase("Por unanimidad los presentes convienen en integrar un Grupo Productivo que será identificado con el siguiente nombre  " + GPnameArt.ToUpper() + ".", fuente6));
                    uno1.Border = 0;
                    uno1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    tableIzq.AddCell(uno1);

                    PdfPCell dos = new PdfPCell(new Phrase("2. ", fuente6));
                    dos.Border = 0;
                    dos.HorizontalAlignment = Element.ALIGN_LEFT;
                    tableIzq.AddCell(dos);

                    PdfPCell dos2 = new PdfPCell(new Phrase("La máxima Autoridad será la Asamblea General que estará conformada por todos los integrantes del  Grupo Productivo y tendrá las siguientes facultades: Nombrar y remover a los integrantes del Comité de Administración, avalar el ingreso de nuevos integrantes al Grupo Productivo, pre autorizar los montos de crédito, autorizar la baja de integrantes y resolver sobre asuntos relacionados con el funcinamiento del grupo.", fuente6));
                    dos2.Border = 0;
                    dos2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    tableIzq.AddCell(dos2);

                    PdfPCell tres = new PdfPCell(new Phrase("3. ", fuente6));
                    tres.Border = 0;
                    tres.HorizontalAlignment = Element.ALIGN_LEFT;
                    tableIzq.AddCell(tres);

                    PdfPCell tres3 = new PdfPCell(new Phrase("Se eligieron a las siguientes personas para ocupar los cargos de representación del Comité de Administración \n \n \n \n ", fuente6));
                    tres3.Border = 0;
                    tres3.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    tableIzq.AddCell(tres3);




                    //tablas de firmas
                    ActInt info1 = new ActInt();

                    int[] sesiones1 = obtieneSesiones();
                    info1.id_empresa = sesiones1[2];
                    info1.id_sucursal = sesiones1[3];
                    int ActaIntegracion1 = Convert.ToInt32(gridActaIntegracion.SelectedValues["id_acta"]);
                    info1.id_acta = ActaIntegracion;
                    info1.optieneimoresion1();
                    int presidente = 0;
                    int secretaria = 0;
                    int tesorera = 0;
                    int voval1 = 0;
                    int voval2 = 0;
                    string nomPresi = "";
                    string nomSec = "";
                    string nomTes = "";
                    string nomVoc1 = "";
                    string nomVoc2 = "";

                    if (Convert.ToBoolean(info1.retorno[0]))
                    {
                        DataSet ds1 = (DataSet)info1.retorno[1];

                        foreach (DataRow r1 in ds1.Tables[0].Rows)
                        {
                            presidente = Convert.ToInt32(r1[0]);
                            info1.id_cliente = presidente;
                            info1.obtieneNombres();
                            nomPresi = info1.retorno[1].ToString();
                            secretaria = Convert.ToInt32(r1[1]);
                            info1.id_cliente = secretaria;
                            info1.obtieneNombres();
                            nomSec = info1.retorno[1].ToString();
                            tesorera = Convert.ToInt32(r1[2]);
                            info1.id_cliente = tesorera;
                            info1.obtieneNombres();
                            nomTes = info1.retorno[1].ToString();
                            voval1 = Convert.ToInt32(r1[3]);
                            info1.id_cliente = voval1;
                            info1.obtieneNombres();
                            nomVoc1 = info1.retorno[1].ToString();
                            voval2 = Convert.ToInt32(r1[4]);
                            info1.id_cliente = voval2;
                            info1.obtieneNombres();
                            nomVoc2 = info1.retorno[1].ToString();
                        }

                    }



                    PdfPTable tableFir = new PdfPTable(1);
                    tableFir.SetWidths(new float[] { 100 });
                    tableFir.DefaultCell.Border = 0;
                    tableFir.WidthPercentage = 40f;
                    tableFir.HorizontalAlignment = Element.ALIGN_LEFT;

                    PdfPCell firma1 = (new PdfPCell(new Phrase("____________________________________\n\n" + nomPresi.ToUpper() + " \n PRESIDENTA \n \n \n ", fuente6)));
                    firma1.Border = 0;
                    firma1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableFir.AddCell(firma1);


                    PdfPCell firmaA = new PdfPCell(new Phrase("____________________________________\n\n" + nomSec.ToUpper() + "\n SECRETARIA \n \n \n ", fuente6));
                    firmaA.Border = 0;
                    firmaA.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableFir.AddCell(firmaA);

                    PdfPCell firmaX = new PdfPCell(new Phrase("____________________________________\n\n" + nomTes.ToUpper() + " \n TESORERA \n \n \n ", fuente6));
                    firmaX.Border = 0;
                    firmaX.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableFir.AddCell(firmaX);



                    PdfPCell firma10 = new PdfPCell(new Phrase("____________________________________\n\n" + nomVoc1.ToUpper() + "\n VOCAL DE VIGILANCIA \n \n \n \n ", fuente6));
                    firma10.Border = 0;
                    firma10.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableFir.AddCell(firma10);

                    PdfPCell firma11 = new PdfPCell(new Phrase("____________________________________\n\n" + nomVoc2.ToUpper() + "\n VOCAL DE VIGILANCIA 2 \n \n \n \n ", fuente6));
                    firma11.Border = 0;
                    firma11.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableFir.AddCell(firma11);



                    //PARTE B DE LA TABLA DIVIDIDA
                    PdfPTable partB = new PdfPTable(2);
                    partB.SetWidths(new float[] { 4, 96 });
                    partB.DefaultCell.Border = 0;
                    partB.WidthPercentage = 40f;
                    partB.HorizontalAlignment = Element.ALIGN_LEFT;
                    string reuArt = r[15].ToString();
                    string reuHorArt = r[16].ToString();
                    string jefeReuArt = r[17].ToString();

                    ActInt nombreCliente = new ActInt();
                    nombreCliente.nombClien = jefeReuArt;
                    nombreCliente.obtienenombrecliente();
                    string jefeCliente = "";
                    if (Convert.ToBoolean(nombreCliente.retorno[0]))
                    {
                        DataSet clien = (DataSet)nombreCliente.retorno[1];

                        foreach (DataRow ret1 in clien.Tables[0].Rows)
                        {
                            jefeCliente = Convert.ToString(ret1[0]);
                        }
                    }

                        string desemArt = r[18].ToString();
                        string calleReuArt = r[19].ToString();
                        string NoCalleArt = r[20].ToString();
                        string ColReuArt = r[21].ToString();
                        string delegaArt = r[22].ToString();
                        string EstReArt = r[23].ToString();
                        string toleArt = r[24].ToString();
                        string mul1Art = r[25].ToString();
                        string mul2Art = r[26].ToString();
                        string mul3Art = r[27].ToString();
                        string mul4Art = r[28].ToString();
                        string FinalReArt = r[29].ToString();


                        PdfPCell acuerC = new PdfPCell(new Phrase("c. ", fuente6));
                        acuerC.Border = 0;
                        acuerC.HorizontalAlignment = Element.ALIGN_LEFT;
                        partB.AddCell(acuerC);

                        PdfPCell acuerC1 = new PdfPCell(new Phrase("El Grupo Productivo se reunirá los dias " + reuArt.ToUpper() + " de cada semana a las " + reuHorArt.ToUpper() + "  horas, en el domicilio de la Sra. " + jefeCliente.ToUpper() + ", quien se desempeña como " + desemArt.ToUpper() + ", ubicado en la calle " + calleReuArt.ToUpper() + " numero  " + NoCalleArt.ToUpper() + "  Colonia o Comunidad " + ColReuArt.ToUpper() + " del Municipio o Delegación  " + delegaArt.ToUpper() + " , Estado de " + EstReArt.ToUpper() + ".", fuente6));
                        acuerC1.Border = 0;
                        acuerC1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        partB.AddCell(acuerC1);

                        PdfPCell acuerD = new PdfPCell(new Phrase("d. ", fuente6));
                        acuerD.Border = 0;
                        acuerD.HorizontalAlignment = Element.ALIGN_LEFT;
                        partB.AddCell(acuerD);

                        PdfPCell acuerD1 = new PdfPCell(new Phrase("Las personas que lleguen despues de " + toleArt.ToUpper() + " minutos de la hora de reunión deberán pagar una multa de $ " + mul1Art.ToUpper() + ", en caso de inasistencia, el monto de la sanción será establecido de acuerdo a lo siguiente:", fuente6));
                        acuerD1.Border = 0;
                        acuerD1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        partB.AddCell(acuerD1);

                        PdfPCell acuerD3 = new PdfPCell(new Phrase(" ", fuente6));
                        acuerD3.Border = 0;
                        acuerD3.HorizontalAlignment = Element.ALIGN_LEFT;
                        partB.AddCell(acuerD3);

                        PdfPCell acuerD2 = new PdfPCell(new Phrase("Inasistencia, pero envía pago completo: $  " + mul2Art.ToUpper() + "  ; inasistencia, pero envia pago incompleto: $  " + mul3Art.ToUpper() + "  ; inasistencia y no envia pago : $  " + mul4Art.ToUpper() + ".", fuente6));
                        acuerD2.Border = 0;
                        acuerD2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        partB.AddCell(acuerD2);


                        PdfPCell acuerE = new PdfPCell(new Phrase("e. ", fuente6));
                        acuerE.Border = 0;
                        acuerE.HorizontalAlignment = Element.ALIGN_LEFT;
                        partB.AddCell(acuerE);

                        PdfPCell acuerE1 = new PdfPCell(new Phrase("Cuando algún integrante del Grupo Productivo incumple con el pago semanal, los demás integrantes deberán obligatoriamente hacer una aportación solidaria para cubrir el monto faltante y cerrar el pago comprometido", fuente6));
                        acuerE1.Border = 0;
                        acuerE1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        partB.AddCell(acuerE1);

                        PdfPCell acuerF = new PdfPCell(new Phrase("f. ", fuente6));
                        acuerF.Border = 0;
                        acuerF.HorizontalAlignment = Element.ALIGN_LEFT;
                        partB.AddCell(acuerF);

                        PdfPCell acuerF1 = new PdfPCell(new Phrase("En caso de que un integrante del Grupo Productivo acumule más de dos atrasos, deberá entregar a la mesa directiva una prenda en garantía cuyo valor sea equivalente al doble del monto de su adeudo y firmar un acuerdo de pago, en caso de incumplimiento se rematará el bien y se liquidará el adeudo", fuente6));
                        acuerF1.Border = 0;
                        acuerF1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        partB.AddCell(acuerF1);

                        PdfPCell acuerG = new PdfPCell(new Phrase("g. ", fuente6));
                        acuerG.Border = 0;
                        acuerG.HorizontalAlignment = Element.ALIGN_LEFT;
                        partB.AddCell(acuerG);

                        PdfPCell acuerG1 = new PdfPCell(new Phrase("Los integrantes que acumulen más de tres atrasos serán dados de baja en el momento que liquiden su adeudo", fuente6));
                        acuerG1.Border = 0;
                        acuerG1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        partB.AddCell(acuerG1);

                        PdfPCell acuerH = new PdfPCell(new Phrase("h. ", fuente6));
                        acuerH.Border = 0;
                        acuerH.HorizontalAlignment = Element.ALIGN_LEFT;
                        partB.AddCell(acuerH);

                        PdfPCell acuerH1 = new PdfPCell(new Phrase("Los integrantes de la mesa directiva podrán ser removidos de su cargo por incumplir con las responsabilidades que les fueron conferidas, por malos manejos de los pagos, ahorros, aportaciones solidarias o multas y por depositar extemporáneamente el pago semanal y los ahorros.", fuente6));
                        acuerH1.Border = 0;
                        acuerH1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        partB.AddCell(acuerH1);

                        PdfPCell acuerI = new PdfPCell(new Phrase("i. ", fuente6));
                        acuerI.Border = 0;
                        acuerI.HorizontalAlignment = Element.ALIGN_LEFT;
                        partB.AddCell(acuerI);

                        PdfPCell acuerI1 = new PdfPCell(new Phrase("Podrá ser motivo de baja definitiva del Grupo Productivo el incumplimiento del Reglamento Interno en cuaquiera de sus puntos.", fuente6));
                        acuerI1.Border = 0;
                        acuerI1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        partB.AddCell(acuerI1);

                        PdfPCell acuerFin = (new PdfPCell(new Phrase("No habiedo otro asunto que tratar, se da por terminada la presente reunion siendo las  " + FinalReArt.ToUpper() + "  horas del mismo día, firmando las personas que en ella participaron:", fuente6)) { Colspan = 2 });
                        acuerFin.Border = 0;
                        acuerFin.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        partB.AddCell(acuerFin);

                        //nombre y firma





                        //DONDE VAN LAS FRIMAS


                        // tabla de la derecha
                        PdfPTable tableDer = new PdfPTable(2);
                        tableDer.SetWidths(new float[] { 5, 95 });
                        tableDer.DefaultCell.Border = 0;
                        tableDer.WidthPercentage = 40f;
                        tableDer.HorizontalAlignment = Element.ALIGN_RIGHT;

                        PdfPCell respo = (new PdfPCell(new Phrase("Responsabilidades de la mesa Directiva. \n ", fuente6)) { Colspan = 2 });
                        respo.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        respo.Border = 0;
                        tableDer.AddCell(respo);

                        PdfPCell presi = (new PdfPCell(new Phrase("PRESIDENTA: Dirigir las reuniones semanales del Grupo Productivo; Coordinar el trabajo de la mesa Directiva; Vigilar el cumplimiento del Reglamento Interno; Garantizar el depósito en tiempo y forma del pago del Grupo Productivo y Convocar a reuniones extraordinarias. \n ", fuente6)) { Colspan = 2 });
                        presi.Border = 0;
                        presi.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        tableDer.AddCell(presi);



                        PdfPCell secre = (new PdfPCell(new Phrase("SECRETARIA: Registrar la asistencia, retardos, faltas y permisos de los Integrantes del Grupo Productivo a las reuniones semanales; Apoyar a la Tesorera en el llenado de los controles semanales y libretas de pago; Levantar actas de los acuerdos tomados por la Asamblea Genera y en su caso, suplir en funciones a la Presidenta en casos de ausencia  por causas de fuerza mayor \n ", fuente6)) { Colspan = 2 });
                        secre.Border = 0;
                        secre.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        tableDer.AddCell(secre);



                        PdfPCell tesore = (new PdfPCell(new Phrase("TESORERA: Recibir y registrar semanalmente en los controles y libretas de pagos, multas y aportaciones solidarias hechas por los integrantes del Grupo Productivo; Realizar y registrar el arqueo de los pagos, ahorros, multas y aportaciones solidarias; Coordinar el depósito del pago semanal del Grupo Productivo; Resguardar las fichas de depósito del pago semanal y/o recibos de efectivo; Administrar los ahorros, multas y otras aportaciones realizas por los integrantes del Grupo Productivo y Presentar informes de resultado, devolver ahorros y distribuir, según instrucciones de la Asamblea General, las multas acumuladas al cierre de cada ciclo de crédito. \n ", fuente6)) { Colspan = 2 });
                        tesore.Border = 0;
                        tesore.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        tableDer.AddCell(tesore);

                        PdfPCell superv = (new PdfPCell(new Phrase("VOCAL DE VIGILANCIA: Supervisar semanalmente el correcto registro de pagos, ahorros, aportaciones solidarias y multas de los integrantes del Grupo Productivo en los controles y libretas de pagos; Corroborar que el crédito fue utilizado para los fines manifestados en la solicitud respectiva y Visitar a los integrantes del Grupo Productivo que incumplan con sus obligación de pago y ahorro semanal. \n \n \n \n ", fuente6)) { Colspan = 2 });
                        superv.Border = 0;
                        superv.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        tableDer.AddCell(superv);

                        //paarte 2 acuerdos


                        string asaArt = r[11].ToString();
                        string asa1Art = r[12].ToString();
                        string ahorroArt = r[13].ToString();
                        string gananArt = r[14].ToString();

                        PdfPCell cuat = new PdfPCell(new Phrase("4. ", fuente6));
                        cuat.Border = 0;
                        cuat.HorizontalAlignment = Element.ALIGN_LEFT;
                        tableDer.AddCell(cuat);

                        PdfPCell cuat1 = new PdfPCell(new Phrase("El Reglamento interno del Grupo de Trabajo quedó redactado de a siguiente manera:", fuente6));
                        cuat1.Border = 0;
                        cuat1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        tableDer.AddCell(cuat1);

                        PdfPCell cuatA = new PdfPCell(new Phrase("a. ", fuente6));
                        cuatA.Border = 0;
                        cuatA.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        tableDer.AddCell(cuatA);

                        PdfPCell cuatA1 = new PdfPCell(new Phrase("Podrán ser integrantes del Grupo Productivo los microempresarios con negocio activo que cuenten con el visto bueno de la Asamblea General y que tengan su domicilio en la Colonia  " + asaArt.ToUpper() + "  o colonias circunvecinas ubicadas a no más de 15 minutos caminando al lugar de reunión designado por el Grupo Productivo, las cuales son: " + asa1Art.ToUpper(), fuente6));
                        cuatA1.Border = 0;
                        cuatA1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        tableDer.AddCell(cuatA1);

                        PdfPCell cuatB = new PdfPCell(new Phrase("b. ", fuente6));
                        cuatB.Border = 0;
                        cuatB.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        tableDer.AddCell(cuatB);

                        PdfPCell cuatB1 = new PdfPCell(new Phrase("Los integrantes del Grupo Productivo deberán realizar un ahorro mínimo semanal de $ " + ahorroArt.ToUpper() + ". El ahorrose depositará en la Institución Bancaria de su elección y será  administrado por la Presidenta y la Tesorera. Ningún integrante podrá retirar o pagar con ahorro antes del vencimiento del ciclo. Las ganancias se repartirán al final del ciclo " + "____ (En partes iguales o en proporción al ahorro de cada integrante del Grupo Productivo)", fuente6));
                        cuatB1.Border = 0;
                        cuatB1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        tableDer.AddCell(cuatB1);


                        //nombre y firma 2
                        PdfPTable rayFi2 = new PdfPTable(1);
                        rayFi2.SetWidths(new float[] { 100 });
                        rayFi2.DefaultCell.Border = 0;
                        rayFi2.WidthPercentage = 40f;
                        rayFi2.HorizontalAlignment = Element.ALIGN_RIGHT;

                        PdfPCell nombrFir = new PdfPCell(new Phrase("NOMBRE Y FIRMA \n ", fuente6));
                        nombrFir.Border = 0;
                        nombrFir.HorizontalAlignment = Element.ALIGN_CENTER;
                        rayFi2.AddCell(nombrFir);

                        info1.obtieneNombres2();

                        DataSet nepe = (DataSet)info1.retorno[1];
                        int numeritoBebe = 1;

                        foreach (DataRow kk in nepe.Tables[0].Rows)
                        {
                            PdfPCell sal2 = new PdfPCell(new Phrase(" ", fuente6));
                            sal2.Border = 0;
                            sal2.HorizontalAlignment = Element.ALIGN_LEFT;
                            rayFi2.AddCell(sal2);

                            PdfPCell raya1 = new PdfPCell(new Phrase("____________________________________\n\n" + kk[0].ToString() + " \n \n ", fuente6));
                            raya1.Border = 0;
                            raya1.HorizontalAlignment = Element.ALIGN_CENTER;
                            rayFi2.AddCell(raya1);



                            numeritoBebe = numeritoBebe + 1;
                        }



                        //tabla principal que junta las tablas
                        PdfPTable junta = new PdfPTable(2);
                        junta.SetWidths(new float[] { 50, 50 });
                        junta.DefaultCell.Border = 0;
                        junta.WidthPercentage = 100F;
                        junta.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell tablitaA = new PdfPCell(tableIzq);
                        tablitaA.Border = 0;
                        tablitaA.HorizontalAlignment = Element.ALIGN_LEFT;
                        junta.AddCell(tablitaA);

                        PdfPCell tablitaB = (new PdfPCell(tableDer) { Rowspan = 4 });
                        tablitaB.Border = 0;
                        tablitaB.HorizontalAlignment = Element.ALIGN_LEFT;
                        junta.AddCell(tablitaB);

                        PdfPCell tablitaC = new PdfPCell(tableFir);
                        tablitaC.Border = 0;
                        tablitaC.HorizontalAlignment = Element.ALIGN_LEFT;
                        junta.AddCell(tablitaC);


                        PdfPTable junta2 = new PdfPTable(2);
                        junta2.SetWidths(new float[] { 50, 50 });
                        junta2.DefaultCell.Border = 0;
                        junta2.WidthPercentage = 100F;
                        junta2.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell tablitaH = new PdfPCell(partB);
                        tablitaH.Border = 0;
                        tablitaH.HorizontalAlignment = Element.ALIGN_LEFT;
                        junta2.AddCell(tablitaH);

                        PdfPCell tablitaI = (new PdfPCell(rayFi2));
                        tablitaI.Border = 0;
                        tablitaI.HorizontalAlignment = Element.ALIGN_LEFT;
                        junta2.AddCell(tablitaI);




                        PdfPCell tablitaK1 = new PdfPCell();
                        tablitaK1.Border = 0;
                        tablitaK1.HorizontalAlignment = Element.ALIGN_LEFT;
                        junta.AddCell(tablitaK1);


                        documento.Add(junta);

                        documento.NewPage();

                        documento.Add(junta2);


                        documento.Close();



                        FileInfo filename = new FileInfo(archivo);
                        if (filename.Exists)
                        {
                            string url = "Descargas.aspx?filename=" + filename.Name;
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "pdfs", "window.open('" + url + "', 'nuevo', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, titlebar=no, fullscreen=yes,resizable=yes');", true);


                        }

                    }
                }
            }
        
    }

    protected void cmbIdPresidente_SelectedIndexChanged(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];

       

        int idPresidente = Convert.ToInt32(cmbIdPresidente.SelectedValue);

        SqlDataSourceSecretario.SelectCommand = "select 0 as id_cliente,'Selecione  Tesorero'as nombre_completo union all  select id_cliente,nombre_completo from an_clientes where id_cliente not in (select id_cliente from AN_Acta_IntegracionDetalle) and id_empresa="+ empresa + " and id_sucursal="+ sucursal +" and id_cliente not in ("+ idPresidente +")";
        SqlDataSourceSecretario.DataBind();
        //cmbIdSecretario.DataBind();
       
       // cmbIdTesorero.DataBind();
       // cmbIdSupervisor.DataBind();

    }

    protected void cmbIdSecretario_SelectedIndexChanged(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        int idPresidente = Convert.ToInt32(cmbIdPresidente.SelectedValue);
        int idTesorero = Convert.ToInt32(cmbIdSecretario.SelectedValue);

        SqlDataSourceTesorero.SelectCommand = "select 0 as id_cliente,'Selecione Secretario'as nombre_completo union all  select id_cliente,nombre_completo from an_clientes where id_cliente not in (select id_cliente from AN_Acta_IntegracionDetalle) and id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente not in (" + idPresidente + ","+ idTesorero + ")";
        SqlDataSourceTesorero.DataBind();
       // cmbIdTesorero.DataBind();
       // cmbIdPresidente.DataBind();
       // cmbIdSecretario.DataBind();
       // cmbIdSupervisor.DataBind();

    }

    protected void cmbIdTesorero_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        int idPresidente = Convert.ToInt32(cmbIdPresidente.SelectedValue);
        int idTesorero = Convert.ToInt32(cmbIdSecretario.SelectedValue);
        int idSecretario = Convert.ToInt32(cmbIdTesorero.SelectedValue);
        

        SqlDataSourceSupervisor.SelectCommand = "select 0 as id_cliente,'Selecione Vocal 1'as nombre_completo union all  select id_cliente,nombre_completo from an_clientes where id_cliente not in (select id_cliente from AN_Acta_IntegracionDetalle) and id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente not in (" + idPresidente + "," + idTesorero + ","+idSecretario+")";
        SqlDataSourceSupervisor.DataBind();
        //cmbIdSupervisor.DataBind();
       // cmbIdTesorero.DataBind();
       // cmbIdPresidente.DataBind();
       // cmbIdSecretario.DataBind();
    }

    protected void cmbIdSupervisor_SelectedIndexChanged(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        int idPresidente = Convert.ToInt32(cmbIdPresidente.SelectedValue);
        int idTesorero = Convert.ToInt32(cmbIdSecretario.SelectedValue);
        int idSecretario = Convert.ToInt32(cmbIdTesorero.SelectedValue);
        int idVocal1 = Convert.ToInt32(cmbIdSupervisor.SelectedValue);
        SqlDataSourceVocalV2.SelectCommand = "select 0 as id_cliente,'Selecione Vocal 2'as nombre_completo union all  select id_cliente,nombre_completo from an_clientes where id_cliente not in (select id_cliente from AN_Acta_IntegracionDetalle) and id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente not in (" + idPresidente + "," + idTesorero + "," + idSecretario + ","+idVocal1+")";
        SqlDataSourceVocalV2.DataBind();
    }

    protected void cmbIdVocalV2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void elPresi_TextChanged(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];


        SqlDataSourcePresidente.SelectCommand = "select 0 as id_cliente,'Selecione Presidente'as nombre_completo union all  select id_cliente,nombre_completo from an_clientes where id_cliente not in (select id_cliente from AN_Acta_IntegracionDetalle) and id_empresa="+empresa+" and id_sucursal="+sucursal;
        SqlDataSourcePresidente.DataBind();

    }

    protected void PrestacasasParty(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        int idPresidente = Convert.ToInt32(cmbIdPresidente.SelectedValue);
        int idSecretario = Convert.ToInt32(cmbIdSecretario.SelectedValue);
        int idTesorero = Convert.ToInt32(cmbIdTesorero.SelectedValue);
        int idVocal1 = Convert.ToInt32(cmbIdSupervisor.SelectedValue);
        int idVocal2 = Convert.ToInt32(cmbIdVocal2.SelectedValue);

        SqlDataSourceCasa.SelectCommand = "select 0 as id_cliente,'Otro'as nombre_completo union all  select id_cliente,nombre_completo from an_clientes where id_cliente not in (select id_cliente from AN_Acta_IntegracionDetalle) and id_empresa="+empresa+" and id_sucursal="+sucursal+" and id_cliente in ("+idPresidente+","+idSecretario+","+idTesorero+","+idVocal1+","+idVocal2+")";
        SqlDataSourceCasa.DataBind();
    }

    protected void cmb_Casa_SelectedIndexChanged(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        ActInt agrega = new ActInt();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        int cliente = Convert.ToInt32(cmb_Casa.SelectedValue);
        agrega.id_empresa = empresa;
        agrega.id_sucursal = sucursal;
        agrega.id_cliente = cliente;
        agrega.obtinedatoscasa();
        if (Convert.ToBoolean(agrega.retorno[0]))
        {
            DataSet ds = (DataSet)agrega.retorno[1];

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txtCalleDireccionReunion.Text = Convert.ToString(r[1]);
                txtNumeroDireccionReunion.Text = Convert.ToString(r[2]);
                txtColoniaDireccionReunion.Text = Convert.ToString(r[3]);
                txtMunicipioDireccionReunion.Text = Convert.ToString(r[4]);
                txtEstadoDireccionReunion.Text = Convert.ToString(r[5]);
            }
        }


    }


    protected void lnkEliminar_Click(object sender, EventArgs e)
    {
        lblIntegrantesError.Text = "";
        int[] sesiones = obtieneSesiones();
        ActInt edita = new ActInt();
        edita.id_empresa = sesiones[2];
        edita.id_sucursal = sesiones[3];
        int acta = Convert.ToInt32(gridActaIntegracion.SelectedValues["id_acta"]);
        int cliente = Convert.ToInt32(gridIntegrntesActaIntegracion.SelectedValues["id_cliente"]);
        edita.id_acta = acta;
        edita.id_cliente = cliente;
        edita.eliminaintegrante();
        lblIntegrantesError.Text = "Cliente Eliminado Exitosamente";
        gridActaIntegracion.DataBind();
        gridIntegrntesActaIntegracion.DataBind();

     
    }


    
}