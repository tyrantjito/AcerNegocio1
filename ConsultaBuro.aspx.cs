using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Telerik.Web.UI;
using E_Utilities;


public partial class ConsultaBuro : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtFechaFirma.MaxDate = DateTime.Now;
        txtFechaConsulta.MaxDate = DateTime.Now;
        int[] sesiones = obtieneSesiones();
        ConsBuro obt = new ConsBuro();
        int usuario = sesiones[0];
        obt.usuario = usuario;
        obt.obtenusuario();
        string nombre = "";
        if (Convert.ToBoolean(obt.retorno[0]))
        {
            DataSet ds = (DataSet)obt.retorno[1];

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                nombre = Convert.ToString(r[0]);
                txtFuncionario.Text = nombre;
            }
        }

    }

    protected void btnEliminarAdjunto_Click(object sender, EventArgs e)
    {
        LinkButton botonEliminar = (LinkButton)sender;
        int idCliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        int idConsulta = Convert.ToInt32(RadGrid2.SelectedValues["id_consulta"]);
        int adjunto = Convert.ToInt32(RadGrid2.SelectedValues["id_adjunto"]);
        char tipo =Convert.ToChar (RadGrid1.SelectedValues["tipo"]);
        int[] sesiones = obtieneSesiones();
        ConsBuro dcto = new ConsBuro();
        dcto.empresa = sesiones[2];
        dcto.sucursal = sesiones[3];
        dcto.idClienteEdita = idCliente;
        dcto.idConsultaEdita = idConsulta;
        dcto.idAdjunto = adjunto;
        dcto.eliminaAdjunto();
        if (Convert.ToBoolean(dcto.retorno[1]))
        {
            lblErrorFotoID.Text = "El documento se eliminó correctamente";

            SqlDataSource1.SelectCommand = "select descripcion,case tipo when 'ID' then 'IDENTIFICACION' else 'REPORTE CREDITO' end as tipo,id_cliente,id_consulta,id_adjunto,case when validacion_digital is null  then 'PENDIENTE' when validacion_digital='AUT' then 'AUTORIZADA' when validacion_digital='NEG' then 'NEGADA' else 'PENDIENTE' end as validacion_digital,case when validacion_fisica is null  then 'PENDIENTE' when validacion_fisica='AUT' then 'AUTORIZADA' when validacion_fisica='NEG' then 'NEGADA' else 'PENDIENTE' end as validacion_fisica,case when observaciones_dig is null then 'PENDIENTE' when observaciones_dig='' then 'PENDIENTE' else observaciones_dig end as observaciones_dig,case when observaciones_fis is null then 'PENDIENTE' when observaciones_fis='' then 'PENDIENTE' else observaciones_fis end as observaciones_fis from AN_Adjuntos_Consulta_Buro  where id_empresa=" + sesiones[2] + " and id_sucursal=" + sesiones[3] + " and id_cliente=" + idCliente + " and id_consulta=" + idConsulta;
            RadGrid2.Rebind();
        }
        else
        {
            lblErrorFotoID.Text = "No se pudo eliminar el adjunto. Detalle:" + Convert.ToString(dcto.retorno[1]);
        }

    }
    protected void lnkAbreWindow_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "Agrega Solicitud";
        borrarCampos();
        cmbAutorizacion.SelectedValue = "SIN" ;
        seccionDatos.Visible = false;
        pnlMask.Visible = true;
        windowAutorizacion.Visible = true;
        lblEstatus.Text = "Pendiente";
        btnAcepta.Visible = false;
        btnDeclina.Visible = false;
        
       

    }
    protected void lnkAgregaSolicitud_Click(object sender, EventArgs e)
    {
        lblErrorAgrega.Text = "";
        lblErrorAfuera.Text = "";
        int[] sesiones = obtieneSesiones();
        ConsBuro existe = new ConsBuro();
        existe.empresa = sesiones[2];
        existe.sucursal = sesiones[3];
        existe.identificador = txtRFCCURP.Text;
        
        if (lblTitulo.Text == "Agrega Solicitud")
        {
            existe.existeCliente();
            if (Convert.ToBoolean(existe.retorno[1]) == false)
            {
                ConsBuro agrega = new ConsBuro();
                agrega.empresa = sesiones[2];
                agrega.sucursal = sesiones[3];

                string tipo = cmbAutorizacion.SelectedItem.Value;
                string nombreF = txtNombre.Text;
                string apellidoPF = txtApellidoP.Text;
                string apellidoMF = txtApellidoM.Text;
                string nombreCompletoF = nombreF + " " + apellidoPF + " " + apellidoMF;
                agrega.nombre = nombreF;
                agrega.aPaterno = apellidoPF;
                agrega.aMaterno = apellidoMF;
                agrega.nombre_completo = nombreCompletoF;
                string txtRFC = txtRFCCURP.Text;
                txtRFC = txtRFC.ToUpper();
                agrega.identificador = txtRFC;
                agrega.tipo = cmbAutorizacion.SelectedItem.Value;
                agrega.representante = txtRepresentanteLegal.Text;
                agrega.calle = txtCalle.Text;
                agrega.numero = txtNumero.Text;
                agrega.colonia = Convert.ToString(cmbColonia.SelectedItem);
                agrega.municipio = txtMunicipio.Text;
                agrega.estado = txtEstado.Text;
                agrega.CP = txt_cp_cb.Text;
                agrega.telefono = Convert.ToDecimal(txtTelefono.Text);
                agrega.lugarAutorizacion = txtLugar.Text;
                DateTime fechaA = Convert.ToDateTime(txtFechaFirma.SelectedDate);
                agrega.fechaAutorizacion = fechaA.ToString("yyyy/MM/dd");
                agrega.nombreFuncionario = txtFuncionario.Text;
                DateTime fechaC = Convert.ToDateTime(txtFechaConsulta.SelectedDate);
                if (fechaC != Convert.ToDateTime("0001-01-01"))
                {
                    string feshi = fechaC.ToString("yyyy-MM-dd");
                    agrega.fechaConsulta = feshi;
                }
                else
                {
                    string feshirri = "";
                    agrega.fechaConsulta = feshirri;
                }
                agrega.folioConsulta = txtFolioConsulta.Text;
                agrega.estatus = "PEN";
                agrega.agregaSolicitud();
                if (Convert.ToInt32(agrega.retorno[1]) == 0)
                {
                    lblErrorAgrega.Visible = true;
                    lblErrorAgrega.Text = "Error al Agregar la Solicitud";
                }
                else
                {
                    agrega.registraClienteNuevo();
                    RadGrid1.DataBind();
                    borrarCampos();
                    lblErrorAfuera.Text = "Solicitud Agregada Correctamente";
                    pnlMask.Visible = false;
                    windowAutorizacion.Visible = false;
                    btnAcepta.Visible = true;
                    btnDeclina.Visible = true;

                }
            }
            lblErrorAgrega.Text = "El cliente ya ha sido dado de alta anteriormente";

        }
        else
        {
            ConsBuro agrega = new ConsBuro();
            agrega.empresa = sesiones[2];
            agrega.sucursal = sesiones[3];
            if (cmbAutorizacion.SelectedValue != "S")
            {
                agrega.tipo = cmbAutorizacion.SelectedItem.Value;
            }
            else
            {
                agrega.tipo = lblTipoPersonaEdita.Text;
            }
            agrega.identificador = txtRFCCURP.Text;
            agrega.nombre = txtNombre.Text;
            agrega.aPaterno = txtApellidoP.Text;
            agrega.aMaterno = txtApellidoM.Text;
            string nombreF = txtNombre.Text;
            string apellidoPF = txtApellidoP.Text;
            string apellidoMF = txtApellidoM.Text;
            string nombreCompletoF = nombreF + " " + apellidoPF + " " + apellidoMF;
            agrega.nombre = nombreF;
            agrega.aPaterno = apellidoPF;
            agrega.aMaterno = apellidoMF;
            agrega.nombre_completo = nombreCompletoF;
            string txtRFC = txtRFCCURP.Text;
            txtRFC = txtRFC.ToUpper();
            agrega.identificador = txtRFC;
            agrega.representante = txtRepresentanteLegal.Text;
            agrega.calle = txtCalle.Text;
            agrega.numero = txtNumero.Text;
            agrega.colonia = Convert.ToString(cmbColonia.SelectedValue);
            agrega.municipio = txtMunicipio.Text;
            agrega.estado = txtEstado.Text;
            agrega.CP = txt_cp_cb.Text;
            agrega.telefono = Convert.ToDecimal(txtTelefono.Text);
            agrega.lugarAutorizacion = txtLugar.Text;
            DateTime fechaA = Convert.ToDateTime(txtFechaFirma.SelectedDate);
            agrega.fechaAutorizacion = fechaA.ToString("yyyy/MM/dd");
            agrega.nombreFuncionario = txtFuncionario.Text;
            DateTime fechaC = Convert.ToDateTime(txtFechaConsulta.SelectedDate);
            if (fechaC != Convert.ToDateTime("0001-01-01"))
            {
                string feshi = fechaC.ToString("yyyy-MM-dd");
                agrega.fechaConsulta = feshi;
            }
            else
            {
                string feshirri = "";
                agrega.fechaConsulta = feshirri;
            }
            agrega.folioConsulta = txtFolioConsulta.Text;
            string est = lblEstatus.Text;
            if (est == "Pendiente")
            {
                est = "PEN";
            }
            if (est == "Declinado")
            {
                est = "DEC";
            }
            if (est == "Aprobado")
            {
                est = "APR";
            }
            agrega.estatus = est;
            agrega.idConsultaEdita = Convert.ToInt32(RadGrid1.SelectedValues["id_consulta"]);
            agrega.idClienteEdita = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
            agrega.editarSolicitud();
            if (Convert.ToBoolean(agrega.retorno[1]) == false)
            {
                lblErrorAgrega.Visible = true;
                lblErrorAgrega.Text = "Error al Actualizar la Solicitud";
            }
            else
            {
                RadGrid1.DataBind();
                borrarCampos();
                lblErrorAfuera.Text = "Solicitud Actualizada Correctamente";
                pnlMask.Visible = false;
                windowAutorizacion.Visible = false;
                btnAcepta.Visible = true;
                btnDeclina.Visible = true;
            }
        }
        
    }

    protected void cmbTipo_PersonaIndexChanged(object sender, EventArgs e)
    {
        string persona = cmbAutorizacion.SelectedItem.Value.ToString();
       
        if (persona == "SIN")
        {
            seccionDatos.Visible = false;
            txtRepresentanteLegal.Text = "";
            txtRepresentanteLegal.ReadOnly = false;
            lnkAgregaSolicitud.Visible = false;
        }
        if (persona == "MOR")
        {
            seccionDatos.Visible = true;
            txtRepresentanteLegal.Text = "";
            txtRepresentanteLegal.ReadOnly = false;
            lnkAgregaSolicitud.Visible = true;
            txtApellidoM.Visible = false;
            txtNombre.Visible = false;
            txtApellidoP.Visible = false;
            txtRazonSozial.Visible = true;
            txtRepresentanteLegal.Visible = true;
            lblNombre.Visible = false;
            lblAMat.Visible = false;
            lblAPat.Visible = false;
            lblRazonSocial.Visible = true;
            lblRepLeg.Visible = true;
            
        }
        if (persona == "FIS")
        {
            seccionDatos.Visible = true;
            txtRepresentanteLegal.Text = "N/A";
            txtRepresentanteLegal.ReadOnly=true;
            lnkAgregaSolicitud.Visible = true;
            txtApellidoM.Visible = true;
            txtNombre.Visible = true;
            txtApellidoP.Visible = true;
            txtRepresentanteLegal.Visible = false;
            lblNombre.Visible = true;
            txtRazonSozial.Visible = false;
            lblRazonSocial.Visible = false;
            lblRepLeg.Visible = false;
            lblAPat.Visible = true;
            lblAMat.Visible = true;
        }
        if(persona=="FAE")
        {
            seccionDatos.Visible = true;
            txtRepresentanteLegal.Text = "N/A";
            txtRepresentanteLegal.ReadOnly = true;
            lnkAgregaSolicitud.Visible = true;
            txtApellidoM.Visible = true;
            txtNombre.Visible = true;
            txtApellidoP.Visible = true;
            txtRepresentanteLegal.Visible = false;
            lblNombre.Visible = true;
            txtRazonSozial.Visible = false;
            lblRazonSocial.Visible = false;
            lblRepLeg.Visible = false;
            lblAPat.Visible = true;
            lblAMat.Visible = true;
        }
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


    public void borrarCampos()
    {
        txtRFCCURP.Text = txt_cp_cb.Text = txtNumero.Text = txtEstado.Text = txtNombre.Text = txtApellidoM.Text = txtApellidoP.Text = txtMunicipio.Text = txtNombre.Text = txtRepresentanteLegal.Text = lblEditaAgrega.Text = txtCalle.Text = txtTelefono.Text = txtLugar.Text = txtFuncionario.Text = txtFolioConsulta.Text = "";
        txtFechaConsulta.Clear();
        txtFechaFirma.Clear();
        txtFechaFirma.DataBind();
        txtFechaConsulta.DataBind();
        
    }

    protected void lnEditaSolicitud_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "Edita Solicitud";
        borrarCampos();

        LinkButton btn = (LinkButton) sender;
        int idConsulta = Convert.ToInt32(RadGrid1.SelectedValues["id_consulta"]);
        int idCliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
        int[] sesiones = obtieneSesiones();
        ConsBuro edita = new ConsBuro();
        edita.empresa = sesiones[2];
        edita.sucursal = sesiones[3];
        edita.idConsultaEdita = idConsulta;
        edita.idClienteEdita = idCliente;
        edita.editaConsulta();

        if(Convert.ToBoolean(edita.retorno[0]))
        {
            DataSet ds = (DataSet)edita.retorno[1];

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                string tipo = r[6].ToString();
                string nombre = r[1].ToString();
                string apellidoP = r[2].ToString(); 
                string apellidoM = r[3].ToString();
                if (tipo == "MOR")
                {
                    nombre = r[5].ToString();
                    txtRazonSozial.Text = nombre;
                    txtApellidoM.Visible = false;
                    txtNombre.Visible = false;
                    txtApellidoP.Visible = false;
                    txtRazonSozial.Visible = true;
                    txtRepresentanteLegal.Visible = true;
                    lblNombre.Visible = false;
                    lblAMat.Visible = false;
                    lblAPat.Visible = false;
                    lblRazonSocial.Visible = true;
                    lblRepLeg.Visible = true;
                }
                else
                {
                    txtNombre.Text = nombre;
                    txtApellidoP.Text = apellidoP;
                    txtApellidoM.Text = apellidoM;
                    lnkAgregaSolicitud.Visible = true;
                    txtApellidoM.Visible = true;
                    txtNombre.Visible = true;
                    txtApellidoP.Visible = true;
                    txtRepresentanteLegal.Visible = false;
                    lblNombre.Visible = true;
                    txtRazonSozial.Visible = false;
                    lblRazonSocial.Visible = false;
                    lblRepLeg.Visible = false;
                    lblAPat.Visible = true;
                    lblAMat.Visible = true;
                }
                txtRFCCURP.Text = Convert.ToString(r[4]);
                cmbAutorizacion.SelectedValue = Convert.ToString(r[6]);
                //txtRepresentanteLegal.Text = Convert.ToString(r[16]);
                txtCalle.Text = Convert.ToString(r[9]);
                txtNumero.Text = r[8].ToString();
                SqlDataSourceCombo.SelectCommand = "select d_asenta,d_codigo from an_codigopostal  where d_codigo=" + r[13].ToString();
                SqlDataSourceCombo.DataBind();
                txtMunicipio.Text = Convert.ToString(r[11]);
                txtEstado.Text = Convert.ToString(r[12]);
                txt_cp_cb.Text = Convert.ToString(r[13]);
                txtTelefono.Text = Convert.ToString(r[14]);
                txtLugar.Text = Convert.ToString(r[15]);
                txtFechaFirma.SelectedDate = Convert.ToDateTime(r[16]);
                txtFuncionario.Text = Convert.ToString(r[17]);
                DateTime feshirriconsulta = Convert.ToDateTime(r[18]);
                if (feshirriconsulta != Convert.ToDateTime("1900-01-01"))
                {
                    txtFechaConsulta.SelectedDate = Convert.ToDateTime(r[18]);
                }
                else
                {
                    txtFechaConsulta.Clear();
                    txtFechaConsulta.DataBind();
                }

                txtFolioConsulta.Text= Convert.ToString(r[20]);
                if (r[19].ToString() == "APR")
                {
                    lblEstatus.Text = "Aprobado";
                    btnAcepta.Visible = false;
                    btnDeclina.Visible = true;
                }
                if (r[19].ToString() == "DEC")
                {
                    lblEstatus.Text = "Declinado";
                    btnAcepta.Visible = true;
                    btnDeclina.Visible = false;
                }
                if (r[19].ToString() == "PEN")
                {
                    lblEstatus.Text = "Pendiente";
                    btnAcepta.Visible = true;
                    btnDeclina.Visible = true;
                }

            }

        }
        pnlEstatus.Visible = true;
        seccionDatos.Visible = true;
        pnlMask.Visible = true;
        windowAutorizacion.Visible = true;
    }

    protected void lnkCerrar_Click(object sender, EventArgs e)
    {
        pnlMask.Visible = false;
        windowAutorizacion.Visible = false;
        borrarCampos();
    }

    protected void btnAcepta_Click(object sender, EventArgs e)
    {
        if(btnDeclina.Visible == false)
        {
            btnDeclina.Visible = true;
        }
        btnAcepta.Visible = false;
        lblEstatus.Text= "Aprobado";
    }

    protected void btnDeclina_Click(object sender, EventArgs e)
    {
        if (btnAcepta.Visible == false)
        {
            btnAcepta.Visible = true;
        }
        btnDeclina.Visible = false;
        lblEstatus.Text = "Declinado";

    }

    protected void lnkAdjuntarID_Click(object sender, EventArgs e)
    {
        lblErrorFotoID.Text = "";
        lblErrorFotoID.CssClass = "errores";
        int[] sesiones = obtieneSesiones();
        int idCliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
        int idConsulta = Convert.ToInt32(RadGrid1.SelectedValues["id_consulta"]);
        string ruta = Server.MapPath("~/TMP");
        byte[] imagen = null;
        // Si el directorio no existe, crearlo
        if (!Directory.Exists(ruta))
            Directory.CreateDirectory(ruta);
        int archivos = 0, archivosCargado = 0;
        try
        {
            string filename = "";
            int documentos = rauAdjuntoID.UploadedFiles.Count;
            archivos = documentos;
            for (int i = 0; i < documentos; i++)
            {
                filename = rauAdjuntoID.UploadedFiles[i].FileName;
                string[] segmenatado = filename.Split(new char[] { '.' });
                bool archivoValido = validaArchivo(segmenatado[1]);
                string extension = segmenatado[1];
                string archivo = String.Format("{0}\\{1}", ruta, filename);
                try
                {
                    FileInfo file = new FileInfo(archivo);
                    // Verificar que el archivo no exista
                    if (File.Exists(archivo))
                        file.Delete();


                    switch (extension.ToLower())
                    {
                        case "jpeg":
                            System.Drawing.Image img = System.Drawing.Image.FromStream(rauAdjuntoID.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "jpg":
                            img = System.Drawing.Image.FromStream(rauAdjuntoID.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "png":
                            img = System.Drawing.Image.FromStream(rauAdjuntoID.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "gif":
                            img = System.Drawing.Image.FromStream(rauAdjuntoID.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "bmp":
                            img = System.Drawing.Image.FromStream(rauAdjuntoID.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "tiff":
                            img = System.Drawing.Image.FromStream(rauAdjuntoID.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo);
                            break;
                        default:
                            Telerik.Web.UI.UploadedFile up = rauAdjuntoID.UploadedFiles[i];
                            up.SaveAs(archivo);
                            imagen = File.ReadAllBytes(archivo);
                            break;
                    }
                }
                catch (Exception) { imagen = null; }
                if (imagen != null)
                {
                    ConsBuro docto = new ConsBuro();
                    docto.empresa = sesiones[2];
                    docto.sucursal = sesiones[3];
                    docto.idClienteEdita = idCliente;
                    docto.idConsultaEdita = idConsulta;
                    docto.adjunto = imagen;
                    docto.nombreAdjunto = segmenatado[0];
                    docto.extension = segmenatado[1].ToLower();
                    docto.agregaAdjuntoID();
                    if (Convert.ToBoolean(docto.retorno[0]))
                    {
                        docto.ActualizatieneId();
                        lblErrorFotoID.Text = "Se han guardardado " + archivosCargado + " de " + archivos + " seleccionados";
                        rauAdjuntoID.Visible = false;
                        lnkAdjuntarID.Visible = false;
                    }
                    else
                    {
                        lblErrorFotoID.Text = "Error al guardar " + archivosCargado + " de " + archivos + " seleccionados";
                    }
                    object[] retorno = docto.retorno;
                    if (Convert.ToBoolean(retorno[0]))
                        archivosCargado++;
                }
            }
        }
        catch (Exception) { imagen = null;
            lblErrorFotoID.Text = "Favor de cargar algun adjunto";
        }
        finally
        {
            SqlDataSource1.SelectCommand = "select descripcion,case tipo when 'ID' then 'IDENTIFICACION' else 'REPORTE CREDITO' end as tipo,id_cliente,id_consulta,id_adjunto,case when validacion_digital is null  then 'PENDIENTE' when validacion_digital='AUT' then 'AUTORIZADA' when validacion_digital='NEG' then 'NEGADA' else 'PENDIENTE' end as validacion_digital,case when validacion_fisica is null  then 'PENDIENTE' when validacion_fisica='AUT' then 'AUTORIZADA' when validacion_fisica='NEG' then 'NEGADA' else 'PENDIENTE' end as validacion_fisica,case when observaciones_dig is null then 'PENDIENTE' when observaciones_dig='' then 'PENDIENTE' else observaciones_dig end as observaciones_dig,case when observaciones_fis is null then 'PENDIENTE' when observaciones_fis='' then 'PENDIENTE' else observaciones_fis end as observaciones_fis from AN_Adjuntos_Consulta_Buro  where id_empresa=" + sesiones[2] + " and id_sucursal=" + sesiones[3] + " and id_cliente=" + idCliente + " and id_consulta=" + idConsulta;
            RadGrid2.DataBind();
        }
    }

    private Byte[] Imagen_A_Bytes(String ruta)
    {
        try
        {
            FileStream foto = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Byte[] adjunto = new Byte[foto.Length];
            BinaryReader reader = new BinaryReader(foto);
            adjunto = reader.ReadBytes(Convert.ToInt32(foto.Length));
            return adjunto;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private bool validaArchivo(string extencion)
    {
        string[] extenciones = { "jpeg", "jpg", "png", "gif", "bmp", "tiff" };
        bool valido = false;
        for (int i = 0; i < extenciones.Length; i++)
        {
            if (extencion.ToUpper() == extenciones[i].ToUpper())
            {
                valido = true;
                break;
            }
        }
        return valido;
    }

    protected void lnkAdjuntarRP_Click(object sender, EventArgs e)
    {
        lblErrorFotoRP.Text = "";
        lblErrorFotoRP.CssClass = "errores";
        int[] sesiones = obtieneSesiones();
        int idCliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
        int idConsulta = Convert.ToInt32(RadGrid1.SelectedValues["id_consulta"]);
        string ruta = Server.MapPath("~/TMP");
        byte[] imagen = null;
        // Si el directorio no existe, crearlo
        if (!Directory.Exists(ruta))
            Directory.CreateDirectory(ruta);
        int archivos = 0, archivosCargado = 0;
        try
        {
            string filename = "";
            int documentos = rauAdjuntoRP.UploadedFiles.Count;
            archivos = documentos;
            for (int i = 0; i < documentos; i++)
            {
                filename = rauAdjuntoRP.UploadedFiles[i].FileName;
                string[] segmenatado = filename.Split(new char[] { '.' });
                bool archivoValido = validaArchivo(segmenatado[1]);
                string extension = segmenatado[1];
                string archivo = String.Format("{0}\\{1}", ruta, filename);
                try
                {
                    FileInfo file = new FileInfo(archivo);
                    // Verificar que el archivo no exista
                    if (File.Exists(archivo))
                        file.Delete();


                    switch (extension.ToLower())
                    {
                        case "jpeg":
                            System.Drawing.Image img = System.Drawing.Image.FromStream(rauAdjuntoRP.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "jpg":
                            img = System.Drawing.Image.FromStream(rauAdjuntoRP.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "png":
                            img = System.Drawing.Image.FromStream(rauAdjuntoRP.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "gif":
                            img = System.Drawing.Image.FromStream(rauAdjuntoRP.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "bmp":
                            img = System.Drawing.Image.FromStream(rauAdjuntoRP.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "tiff":
                            img = System.Drawing.Image.FromStream(rauAdjuntoRP.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo);
                            break;
                        default:
                            Telerik.Web.UI.UploadedFile up = rauAdjuntoRP.UploadedFiles[i];
                            up.SaveAs(archivo);
                            imagen = File.ReadAllBytes(archivo);
                            break;
                    }
                }
                catch (Exception) { imagen = null; }
                if (imagen != null)
                {
                    ConsBuro docto = new ConsBuro();
                    docto.empresa = sesiones[2];
                    docto.sucursal = sesiones[3];
                    docto.idClienteEdita = idCliente;
                    docto.idConsultaEdita = idConsulta;
                    docto.adjunto = imagen;
                    docto.nombreAdjunto = segmenatado[0];
                    docto.extension = segmenatado[1].ToLower();
                    docto.agregaAdjuntoRP();
                    if (Convert.ToBoolean(docto.retorno[0]))
                    {
                        docto.ActualizatieneRp();
                        lblErrorFotoRP.Text = "Se han guardardado " + archivosCargado + " de " + archivos + " seleccionados";
                        rauAdjuntoRP.Visible = false;
                        lnkAdjuntarRP.Visible = false;

                    }
                    else
                    {
                        lblErrorFotoRP.Text = "Error al guardar " + archivosCargado + " de " + archivos + " seleccionados";
                    }
                    object[] retorno = docto.retorno;
                    if (Convert.ToBoolean(retorno[0]))
                        archivosCargado++;
                }
            }

            
        }
        catch (Exception) { imagen = null;
            lblErrorFotoID.Text = "Favor de cargar algun adjunto";
        }
        finally
        {
            SqlDataSource1.SelectCommand = "select descripcion,case tipo when 'ID' then 'IDENTIFICACION' else 'REPORTE CREDITO' end as tipo,id_cliente,id_consulta,id_adjunto,case when validacion_digital is null  then 'PENDIENTE' when validacion_digital='AUT' then 'AUTORIZADA' when validacion_digital='NEG' then 'NEGADA' else 'PENDIENTE' end as validacion_digital,case when validacion_fisica is null  then 'PENDIENTE' when validacion_fisica='AUT' then 'AUTORIZADA' when validacion_fisica='NEG' then 'NEGADA' else 'PENDIENTE' end as validacion_fisica,case when observaciones_dig is null then 'PENDIENTE' when observaciones_dig='' then 'PENDIENTE' else observaciones_dig end as observaciones_dig,case when observaciones_fis is null then 'PENDIENTE' when observaciones_fis='' then 'PENDIENTE' else observaciones_fis end as observaciones_fis from AN_Adjuntos_Consulta_Buro  where id_empresa=" + sesiones[2] + " and id_sucursal=" + sesiones[3] + " and id_cliente=" + idCliente + " and id_consulta=" + idConsulta;
            RadGrid2.DataBind();
        }
    }

    protected void lnkImprimir_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;

        //tipos de font a utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de un nuevo documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" Consulta Crédito ");
        documento.AddCreator("AserNegocio1");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\ConsultaBuro_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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
            //el encabezado
            PdfPTable tablaEncabezado = new PdfPTable(1);
            tablaEncabezado.SetWidths(new float[] { 10f });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;
            


            PdfPCell titulo = new PdfPCell(new Phrase("Autorización para solicitar reportes de crédito" + Environment.NewLine + " Persona Física / Persona Moral ", fuente3));

            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(titulo);
            documento.Add(tablaEncabezado);
            documento.Add(new Paragraph(" "));

            //texto 
            PdfPTable cuerpotext = new PdfPTable(1);
            cuerpotext.DefaultCell.Border = 0;
            cuerpotext.WidthPercentage = 100f;

            PdfPCell txt = new PdfPCell(new Phrase("Por este conduto autoriza expresamente a APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR, para que por conducto de sus funcionarios facultados lleve a cabo investigaciones sobre mi comportamiento crediticio o el de la empresa que represento en CIRCULO DE CREDITO, S.A. de C.V.", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
            txt.Border = 0;
            txt.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cuerpotext.AddCell(txt);
            documento.Add(cuerpotext);
            documento.Add(new Paragraph(""));

            PdfPTable cuerpotext2 = new PdfPTable(1);
            cuerpotext2.DefaultCell.Border = 0;
            cuerpotext2.WidthPercentage = 100f;

            PdfPCell txt2 = new PdfPCell(new Phrase("Así mismo, declaro que conozco la naturaleza y alcance de las Sociedades de Información Crediticia y de la información contenida en los reportes de crédito y reporte de crédito especial, declaro que conozco la naturaleza y alcance de la información que se solicitará, del uso que APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR hará de la infomación, de que ésta podra realizar consultas periódicas sobre mi historial o el de la empresa que represento, consintiendo que esta autorización se encuentre vigente por un periodo de un año contado a partir de su expedición y en todo caso durante el tiempo que se mantenga la relación juridica.", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
            txt2.Border = 0;
            txt2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cuerpotext2.AddCell(txt2);
            documento.Add(cuerpotext2);
            documento.Add(new Paragraph(""));

            PdfPTable cuerpotext3 = new PdfPTable(1);
            cuerpotext3.DefaultCell.Border = 0;
            cuerpotext3.WidthPercentage = 100f;

            PdfPCell txt3 = new PdfPCell(new Phrase("En caso de que el solicitante sea una Persona Moral, declaro bajo protesta de decir la verdad ser Representante Legal de la Empresa mencionada en esta autorización; manifestando que a la fecha de firma de la presente autorización de los poderes no me han sido revocados, limitados, ni modificados en forma alguna.", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
            txt3.Border = 0;
            txt3.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cuerpotext3.AddCell(txt3);
            documento.Add(cuerpotext3);
            documento.Add(new Paragraph(" "));

            //autorización encabezado.

            ConsBuro info = new ConsBuro();
            int[] sesiones = obtieneSesiones();
            info.empresa = sesiones[2];
            info.sucursal = sesiones[3];
            int idConsulta = Convert.ToInt32(RadGrid1.SelectedValues["id_consulta"]);
            int idCliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
            info.idConsultaEdita = idConsulta;
            info.idClienteEdita = idCliente;
            info.optieneimoresion();

            if (Convert.ToBoolean(info.retorno[0]))
            {
                DataSet ds = (DataSet)info.retorno[1];

                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    PdfPTable AutoEnca = new PdfPTable(1);
                    AutoEnca.WidthPercentage = 100f;

                    string personaFIS = r[2].ToString();
                    string personaFISC = r[2].ToString();
                    string personaMOR = r[2].ToString();
                    if (personaFIS == "FIS")
                    {
                        personaFIS = "X";
                    }
                    else
                    {
                        personaFIS = "";
                    }
                    if (personaFISC == "MOR")
                    {
                        personaFISC = "X";
                    }
                    else
                    {
                        personaFISC = "";
                    }
                    if (personaMOR == "FAE")
                    {
                        personaMOR = "X";
                    }
                    else
                    {
                        personaMOR = "";
                    }


                    PdfPCell encaAuto = new PdfPCell(new Phrase("Autorización para : \n \n ", fuente10));
                    encaAuto.Border = 0;
                    encaAuto.HorizontalAlignment = 1;
                    encaAuto.VerticalAlignment = Element.ALIGN_MIDDLE;
                    AutoEnca.AddCell(encaAuto);
                    documento.Add(AutoEnca);

                    //personas
                    PdfPTable person = new PdfPTable(6);
                    person.DefaultCell.Border = 0;
                    person.WidthPercentage = 100f;
                    person.SetWidths(new float[] { 20, 3, 41, 3, 30, 3 });


                    PdfPCell per1 = new PdfPCell(new Phrase("Persona física ", fuente10));
                    per1.Border = 0;
                    per1.HorizontalAlignment = Element.ALIGN_CENTER;
                    per1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    person.AddCell(per1);

                    PdfPCell per11 = new PdfPCell(new Phrase( personaFIS, fuente10));
                    per11.Border = 1;
                    per11.BorderWidthLeft = 1;
                    per11.BorderWidthRight = 1;
                    per11.BorderWidthTop = 1;
                    per11.BorderWidthBottom = 1;
                    per11.HorizontalAlignment = Element.ALIGN_CENTER;
                    per11.VerticalAlignment = Element.ALIGN_MIDDLE;
                    person.AddCell(per11);


                    PdfPCell per2 = new PdfPCell(new Phrase("Persona física con actividad empresarial ", fuente10));
                    per2.Border = 0;
                    per2.HorizontalAlignment = 1;
                    per2.VerticalAlignment = Element.ALIGN_MIDDLE;
                    person.AddCell(per2);

                    PdfPCell per22 = new PdfPCell(new Phrase(personaFISC, fuente10));
                    per22.Border = 1;
                    per22.BorderWidth = 1;
                    per22.BorderWidthBottom = 1;
                    per22.BorderWidthRight = 1;
                    per22.BorderWidthLeft = 1;
                    per22.BorderWidthTop = 1;
                    per22.HorizontalAlignment = Element.ALIGN_CENTER;
                    per22.VerticalAlignment = Element.ALIGN_MIDDLE;
                    person.AddCell(per22);

                    PdfPCell per3 = new PdfPCell(new Phrase("Persona moral ", fuente10));
                    per3.Border = 0;
                    per3.HorizontalAlignment = Element.ALIGN_CENTER;
                    per3.VerticalAlignment = Element.ALIGN_MIDDLE;
                    person.AddCell(per3);

                    PdfPCell per33 = new PdfPCell(new Phrase(personaMOR, fuente10));
                    per33.Border = 1;
                    per33.BorderWidth = 1;
                    per33.BorderWidthTop = 1;
                    per33.BorderWidthRight = 1;
                    per33.BorderWidthLeft = 1;
                    per33.BorderWidthBottom = 1;
                    per33.HorizontalAlignment = Element.ALIGN_CENTER;
                    per33.VerticalAlignment = Element.ALIGN_MIDDLE;
                    person.AddCell(per33);

                    documento.Add(person);
                    documento.Add(new Paragraph(" "));

                    //campos a llenar con bases de datos

                    //nombre del solicitante
                    PdfPTable nameSol = new PdfPTable(2);
                    nameSol.WidthPercentage = 100f;
                    int[] nameSolcellwidth = { 50, 50 };
                    nameSol.SetWidths(nameSolcellwidth);
                    string nombre = r[0].ToString();

                    PdfPCell name1 = (new PdfPCell(new Phrase("Nombre de solicitante (Persona física o razón social de la persona moral : ", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL))) { Colspan=2 });
                    name1.Border = 0;
                    name1.HorizontalAlignment = Element.ALIGN_LEFT;
                    nameSol.AddCell(name1);

                    PdfPCell name = (new PdfPCell(new Phrase(nombre, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL))) { Colspan=2 });
                    name.Border = 1;
                    name.BorderWidthBottom = 1;
                    name.BorderWidthLeft = 0;
                    name.BorderWidthRight = 0;
                    name.BorderWidthTop = 0;
                    name.VerticalAlignment = Element.ALIGN_LEFT;
                    name.HorizontalAlignment = Element.ALIGN_LEFT;
                    nameSol.AddCell(name);
                    documento.Add(nameSol);

                    //nombre del representante legal
                    PdfPTable nameRe = new PdfPTable(2);
                    nameRe.WidthPercentage = 100f;
                    string nombrerep = r[3].ToString();
                    int[] nameRecellwidth = { 45, 55 };
                    nameRe.SetWidths(nameRecellwidth);

                    PdfPCell nameRep1 = new PdfPCell(new Phrase("Para el caso de Persona Moral, nombre de Representante Legal :", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    nameRep1.Border = 0;
                    nameRep1.HorizontalAlignment = Element.ALIGN_CENTER;
                    nameRe.AddCell(nameRep1);

                    PdfPCell nameRep = new PdfPCell(new Phrase(nombrerep, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    nameRep.Border = 1;
                    nameRep.BorderWidthBottom = 1;
                    nameRep.BorderWidthLeft = 0;
                    nameRep.BorderWidthRight = 0;
                    nameRep.BorderWidthTop = 0;
                    nameRep.VerticalAlignment = Element.ALIGN_MIDDLE;
                    nameRep.HorizontalAlignment = Element.ALIGN_LEFT;
                    nameRe.AddCell(nameRep);
                    documento.Add(nameRe);

                    //RFC/CURP

                    PdfPTable curp = new PdfPTable(2);
                    curp.WidthPercentage = 100f;
                    int[] curpcellwidth = { 10, 90 };
                    curp.SetWidths(curpcellwidth);

                    string Rfcurp = r[1].ToString();

                    PdfPCell rfc1 = new PdfPCell(new Phrase("RFC/CURP :", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    rfc1.Border = 0;
                    rfc1.BorderWidthBottom = 0;
                    rfc1.BorderWidthTop = 0;
                    rfc1.BorderWidthRight = 0;
                    rfc1.BorderWidthLeft = 0;
                    rfc1.HorizontalAlignment = Element.ALIGN_LEFT;
                    curp.AddCell(rfc1);

                    PdfPCell rfc = new PdfPCell(new Phrase(Rfcurp, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    rfc.Border = 1;
                    rfc.BorderWidthBottom = 1;
                    rfc.BorderWidthTop = 0;
                    rfc.BorderWidthRight = 0;
                    rfc.BorderWidthLeft = 0;
                    rfc.VerticalAlignment = Element.ALIGN_MIDDLE;
                    rfc.HorizontalAlignment = Element.ALIGN_LEFT;
                    curp.AddCell(rfc);
                    documento.Add(curp);

                    //Domicilio

                    PdfPTable dom = new PdfPTable(2);
                    dom.WidthPercentage = 100f;
                    int[] domcellwidth = { 19, 81 };
                    dom.SetWidths(domcellwidth);
                    dom.DefaultCell.Border = 0;

                    string domicilio = r[4].ToString();

                    PdfPCell direc1 = (new PdfPCell(new Phrase("Domicilio Calle y Número : ", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL))) { Rowspan=2 });
                    direc1.Border = 1;
                    direc1.HorizontalAlignment = Element.ALIGN_LEFT;
                    direc1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    dom.AddCell(direc1);

                    PdfPCell direc = (new PdfPCell(new Phrase(domicilio, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.UNDERLINE))) { Rowspan = 2 });
                    direc.Border = 0;
                    direc.BorderWidthBottom = 1;
                    direc.BorderWidthLeft = 0;
                    direc.BorderWidthRight = 0;
                    direc.BorderWidthTop = 0;
                    direc.VerticalAlignment = Element.ALIGN_MIDDLE;
                    direc.HorizontalAlignment = Element.ALIGN_LEFT;
                    dom.AddCell(direc);
                    documento.Add(dom);

                    //Colonia / municipio / estado
                    PdfPTable reside = new PdfPTable(6);
                    reside.WidthPercentage = 100f;
                    int[] residecellwidth = { 7,7, 12, 34,10,34 };
                    reside.SetWidths(residecellwidth);
                    string colon = r[6].ToString();
                    string municip = r[7].ToString();
                    string estad = r[8].ToString();

                    PdfPCell col1 = new PdfPCell(new Phrase("Colonia : ", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    col1.Border = 0;
                    col1.HorizontalAlignment = Element.ALIGN_LEFT;
                    reside.AddCell(col1);

                    PdfPCell col = new PdfPCell(new Phrase(colon, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    col.Border = 1;
                    col.BorderWidthBottom = 1;
                    col.BorderWidthTop = 0;
                    col.BorderWidthRight = 0;
                    col.BorderWidthLeft = 0;
                    col.VerticalAlignment = Element.ALIGN_LEFT;
                    col.HorizontalAlignment = Element.ALIGN_CENTER;
                    reside.AddCell(col);

                    PdfPCell muni1 = new PdfPCell(new Phrase("Municipio :", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    muni1.Border = 0;
                    muni1.HorizontalAlignment = Element.ALIGN_CENTER;
                    reside.AddCell(muni1);

                    PdfPCell muni = new PdfPCell(new Phrase(municip, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    muni.Border = 1;
                    muni.BorderWidthBottom = 1;
                    muni.BorderWidthLeft = 0;
                    muni.BorderWidthRight = 0;
                    muni.BorderWidthTop = 0;
                    muni.VerticalAlignment = Element.ALIGN_LEFT;
                    muni.HorizontalAlignment = Element.ALIGN_LEFT;
                    reside.AddCell(muni);

                    PdfPCell est1 = new PdfPCell(new Phrase("Estado : ", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    est1.Border = 0;
                    est1.HorizontalAlignment = Element.ALIGN_CENTER;
                    reside.AddCell(est1);

                    PdfPCell est = new PdfPCell(new Phrase(estad, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    est.Border = 1;
                    est.BorderWidthBottom = 1;
                    est.BorderWidthTop = 0;
                    est.BorderWidthLeft = 0;
                    est.BorderWidthRight = 0;
                    est.VerticalAlignment = Element.ALIGN_LEFT;
                    est.HorizontalAlignment = Element.ALIGN_LEFT;
                    reside.AddCell(est);
                    documento.Add(reside);



                    //Codigo postal / telefonos
                    PdfPTable codp = new PdfPTable(4);
                    codp.WidthPercentage = 100f;
                    int[] codpcellwidth = { 11, 7, 10, 72 };
                    codp.SetWidths(codpcellwidth);
                    string codipos = r[9].ToString();
                    string telefon = r[10].ToString();

                    PdfPCell cp1 = new PdfPCell(new Phrase("Código Postal : ", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    cp1.Border = 0;
                    cp1.HorizontalAlignment = Element.ALIGN_LEFT;
                    codp.AddCell(cp1);

                    PdfPCell cp = new PdfPCell(new Phrase(codipos, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    cp.Border = 1;
                    cp.BorderWidthBottom = 1;
                    cp.BorderWidthRight = 0;
                    cp.BorderWidthLeft = 0;
                    cp.BorderWidthTop = 0;
                    cp.VerticalAlignment = Element.ALIGN_LEFT;
                    cp.HorizontalAlignment = Element.ALIGN_LEFT;
                    codp.AddCell(cp);

                    PdfPCell tel1 = new PdfPCell(new Phrase("Teléfono(s): ", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    tel1.Border = 0;
                    tel1.HorizontalAlignment = Element.ALIGN_CENTER;
                    codp.AddCell(tel1);

                    PdfPCell tel = new PdfPCell(new Phrase(telefon, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    tel.Border = 1;
                    tel.BorderWidthBottom = 1;
                    tel.BorderWidthTop = 0;
                    tel.BorderWidthRight = 0;
                    tel.BorderWidthLeft = 0;
                    tel.VerticalAlignment = Element.ALIGN_LEFT;
                    tel.HorizontalAlignment = Element.ALIGN_LEFT;
                    codp.AddCell(tel);
                    documento.Add(codp);

                    //lugar y fecha de firma autorizada
                    PdfPTable firau = new PdfPTable(2);                    
                    firau.WidthPercentage = 100f;
                    int[] firaucellwidth = { 31,69 };
                    firau.SetWidths(firaucellwidth);
                    string lugaryfecha = r[11].ToString();
                    string fechaAuFu = r[12].ToString();

                    PdfPCell lug1 = new PdfPCell(new Phrase("Lugar y fecha en que se firma a autorización : ", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    lug1.Border = 0;
                    lug1.HorizontalAlignment = Element.ALIGN_LEFT;
                    firau.AddCell(lug1);

                    PdfPCell lug = new PdfPCell(new Phrase(lugaryfecha + " " + fechaAuFu, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    lug.Border = 1;
                    lug.BorderWidthBottom = 1;
                    lug.BorderWidthTop = 0;
                    lug.BorderWidthRight = 0;
                    lug.BorderWidthLeft = 0;
                    lug.HorizontalAlignment = 1;
                    lug.HorizontalAlignment = Element.ALIGN_LEFT;
                    firau.AddCell(lug);
                    documento.Add(firau);

                    //Nombre del funcionario
                    PdfPTable funci = new PdfPTable(2);
                    funci.WidthPercentage = 100f;
                    int[] funcicellwidth = { 35,65 };
                    funci.SetWidths(funcicellwidth);
                    string nomfunci = r[13].ToString();

                    PdfPCell nomfu1 = new PdfPCell(new Phrase("Nombre del funcionario que recaba la autorización : ", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    nomfu1.Border = 0;
                    nomfu1.HorizontalAlignment = Element.ALIGN_LEFT;
                    funci.AddCell(nomfu1);

                    PdfPCell nomfu = new PdfPCell(new Phrase(nomfunci, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    nomfu.Border = 1;
                    nomfu.BorderWidthBottom = 1;
                    nomfu.BorderWidthTop = 0;
                    nomfu.BorderWidthRight = 0;
                    nomfu.BorderWidthLeft = 0;
                    nomfu.HorizontalAlignment = 1;
                    nomfu.HorizontalAlignment = Element.ALIGN_LEFT;
                    funci.AddCell(nomfu);
                    documento.Add(funci);
                    documento.Add(new Paragraph(" "));

                    //texto  faltante
                    PdfPTable textoSeg = new PdfPTable(1);
                    textoSeg.WidthPercentage = 100f;


                    PdfPCell segun = new PdfPCell(new Phrase("Estoy consciente y acepto que este documento quede bajo custodia de APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR y/o la Sociedad de Información Crediticia consultada para efectos de control y cumplimiento del artículo 28 de la Ley para Regular las Sociedades de Información Crediticia; mismo que señala que las Sociedades solo podrán preporcionar información a un usuario cuando este cuente con la autorización expresa del Cliente mediante su firma autógrafa", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    segun.Border = 0;
                    segun.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    textoSeg.AddCell(segun);
                    documento.Add(textoSeg);
                    documento.Add(new Paragraph(" "));

                    //encabezado de la firma
                    PdfPTable pfae = new PdfPTable(1);
                    pfae.WidthPercentage = 100f;

                    PdfPCell firmapf = new PdfPCell(new Phrase("Nombre y firma del PF, PFAE o Representante legal de la Empresa \n \n \n \n ________________________________________________ ", fuente3));
                    firmapf.Border = 0;
                    firmapf.HorizontalAlignment = 1;
                    firmapf.VerticalAlignment = Element.ALIGN_MIDDLE;
                    pfae.AddCell(firmapf);
                    documento.Add(pfae);

                    //guion bajo para la firma
                    
                    //texto abajo de la firma
                    PdfPTable txtfir = new PdfPTable(1);
                    txtfir.DefaultCell.Border = 0;
                    txtfir.WidthPercentage = 100f;

                    PdfPCell firtxt = new PdfPCell(new Phrase("Para uso exclusivo de la Empresa que efectúa la consulta, APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR", fuente3));
                    firtxt.Border = 0;
                    firtxt.HorizontalAlignment = Element.ALIGN_CENTER;
                    firtxt.VerticalAlignment = Element.ALIGN_MIDDLE;
                    txtfir.AddCell(firtxt);
                    //documento.Add(txtfir);

                    PdfPTable txtfir2 = new PdfPTable(1);
                    txtfir2.WidthPercentage = 100f;

                    
                    //documento.Add(txtfir2);
                    documento.Add(new Paragraph(" "));

                    // FECHA DE CONSUKTA Y FOLIO DE LA CONSULTA
                    PdfPTable fecfol = new PdfPTable(4);
                    fecfol.WidthPercentage = 100f;
                    string fechaconsul = r[14].ToString();
                    string folioconsul = r[15].ToString();

                    PdfPCell fechaC1 = new PdfPCell(new Phrase ("Fecha de consulta CC :", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    fechaC1.Border = 0;
                    fechaC1.HorizontalAlignment = Element.ALIGN_CENTER;
                    fecfol.AddCell(fechaC1);

                    PdfPCell fechaC = new PdfPCell(new Phrase(fechaconsul, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    fechaC.BorderWidthBottom = 1;
                    fechaC.BorderWidthLeft = 0;
                    fechaC.BorderWidthRight = 0;
                    fechaC.BorderWidthTop = 0;
                    fechaC.VerticalAlignment = Element.ALIGN_MIDDLE;
                    fechaC.HorizontalAlignment = Element.ALIGN_CENTER;
                    fecfol.AddCell(fechaC);
                    documento.Add(fecfol);

                    PdfPCell folioC1 = new PdfPCell(new Phrase("Fecha de consulta CC :", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    folioC1.Border = 0;
                    folioC1.HorizontalAlignment = Element.ALIGN_CENTER;
                    fecfol.AddCell(folioC1);

                    PdfPCell folioC = new PdfPCell(new Phrase(folioconsul, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    folioC.BorderWidthBottom = 1;
                    folioC.BorderWidthLeft = 0;
                    folioC.BorderWidthRight = 0;
                    folioC.BorderWidthTop = 0;
                    folioC.VerticalAlignment = Element.ALIGN_MIDDLE;
                    folioC.HorizontalAlignment = Element.ALIGN_CENTER;
                    fecfol.AddCell(folioC);
                    //documento.Add(fecfol);
                    documento.Add(new Paragraph(" "));


                    //Aviso importante
                    PdfPTable aviso = new PdfPTable(1);
                    aviso.DefaultCell.Border = 0;
                    aviso.WidthPercentage = 100f;



                    PdfPCell txtA = new PdfPCell(new Phrase("IMPORTANTE. Este formato debe ser llenado de forma individual para una sola persona física o para una sola empresa. En caso de requerir el historial crediticio del representante legal, favor de llenar un formato adicional. \n \n", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    txtA.Border = 0;
                    txtA.HorizontalAlignment = 1;
                    txtA.HorizontalAlignment = Element.ALIGN_LEFT;
                    aviso.AddCell(txtA);
                    //documento.Add(aviso);




                    PdfPTable borde = new PdfPTable(1);
                    borde.WidthPercentage = 100f;
                    
                    PdfPCell borBlue = new PdfPCell(txtfir);
                    borBlue.Border = 0;
                    borBlue.BorderColorLeft = BaseColor.BLUE;
                    borBlue.BorderColorRight = BaseColor.BLUE;
                    borBlue.BorderColorTop = BaseColor.BLUE;
                    borBlue.BorderWidthLeft = 2;
                    borBlue.BorderWidthRight = 2;
                    borBlue.BorderWidthTop = 2;
                    borde.AddCell(borBlue);

                    PdfPCell borBlue1 = new PdfPCell( txtfir2 );
                    borBlue1.Border = 0;
                    borBlue1.BorderColorLeft = BaseColor.BLUE;
                    borBlue1.BorderColorRight = BaseColor.BLUE;
                    borBlue1.BorderWidthLeft = 2;
                    borBlue1.BorderWidthRight = 2;
                    borBlue1.BorderWidthBottom = 0;
                    borde.AddCell(borBlue1);

                    PdfPCell space = new PdfPCell(new Phrase(" ") );
                    space.Border = 0;
                    space.BorderColorLeft = BaseColor.BLUE;
                    space.BorderColorRight = BaseColor.BLUE;
                    space.BorderWidthLeft = 2;
                    space.BorderWidthRight = 2;
                    space.BorderWidthBottom = 0;
                    borde.AddCell(space);
                    borde.AddCell(space);
                    borde.AddCell(space);


                    PdfPCell borBlue2 = new PdfPCell(fecfol);
                    borBlue2.Border = 0;
                    borBlue2.BorderColorLeft = BaseColor.BLUE;
                    borBlue2.BorderColorRight = BaseColor.BLUE;
                    borBlue2.BorderWidthLeft = 2;
                    borBlue2.BorderWidthRight = 2;
                    borBlue2.BorderWidthBottom = 0;
                    borde.AddCell(borBlue2);
                    borde.AddCell(space);
                    borde.AddCell(space);


                    PdfPCell borBlue3 = new PdfPCell(aviso);
                    borBlue3.Border = 0;
                    borBlue3.VerticalAlignment = Element.ALIGN_MIDDLE;
                    borBlue3.BorderWidthTop = 0;
                    borBlue3.BorderColorBottom = BaseColor.BLUE;
                    borBlue3.BorderColorLeft = BaseColor.BLUE;
                    borBlue3.BorderColorRight = BaseColor.BLUE;
                    borBlue3.BorderWidthLeft = 2;
                    borBlue3.BorderWidthRight = 2;
                    borBlue3.BorderWidthBottom = 2;
                    borde.AddCell(borBlue3);



                    documento.Add(borde);

                    //para abrirlo en navegador el documento

                    //documento.Add(new Paragraph(""));
                    documento.Close();
                }
            }

            //
            FileInfo filename = new FileInfo(archivo);
            if (filename.Exists)
            {
                string url = "Descargas.aspx?filename=" + filename.Name;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "pdfs", "window.open('" + url + "', 'nuevo', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, titlebar=no, fullscreen=yes,resizable=yes');", true);
            }
        }

    }

    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsBuro obtiene = new ConsBuro();
        int[] sesiones = obtieneSesiones();
        int id_cliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
        int id_consulta = Convert.ToInt32(RadGrid1.SelectedValues["id_consulta"]);
        obtiene.empresa = sesiones[2];
        obtiene.sucursal = sesiones[3];
        obtiene.idClienteEdita = id_cliente;
        obtiene.idConsultaEdita = id_consulta;
        obtiene.existeIdAdjunto();
        if (Convert.ToString(obtiene.retorno[1]) != "")
        {
            rauAdjuntoID.Visible = false;
            lnkAdjuntarID.Visible = false;
        }
        else
        {
            rauAdjuntoID.Visible = true;
            lnkAdjuntarID.Visible = true;
        }
        obtiene.existeRpAdjunto();
        if (Convert.ToString(obtiene.retorno[1]) != "")
        {
            rauAdjuntoRP.Visible = false;
            rauAdjuntoRP.Visible = false;
        }
        else
        {
            rauAdjuntoRP.Visible = true;
            lnkAdjuntarRP.Visible = true;
        }
        SqlDataSource1.SelectCommand = "select descripcion,case tipo when 'ID' then 'IDENTIFICACION' else 'REPORTE CREDITO' end as tipo,id_cliente,id_consulta,id_adjunto,case when validacion_digital is null  then 'PENDIENTE' when validacion_digital='AUT' then 'AUTORIZADA' when validacion_digital='NEG' then 'NEGADA' else 'PENDIENTE' end as validacion_digital,case when validacion_fisica is null  then 'PENDIENTE' when validacion_fisica='AUT' then 'AUTORIZADA' when validacion_fisica='NEG' then 'NEGADA' else 'PENDIENTE' end as validacion_fisica,case when observaciones_dig is null then 'PENDIENTE' when observaciones_dig='' then 'PENDIENTE' else observaciones_dig end as observaciones_dig,case when observaciones_fis is null then 'PENDIENTE' when observaciones_fis='' then 'PENDIENTE' else observaciones_fis end as observaciones_fis from AN_Adjuntos_Consulta_Buro  where id_empresa=" + sesiones[2] + " and id_sucursal=" + sesiones[3] + " and id_cliente=" + id_cliente + " and id_consulta=" + id_consulta;
        RadGrid2.DataBind();
        pnlSeccionAdjuntos.Visible = true;
        lnkValidarCredito.Visible = true;
        lnkAbreEdicion.Visible = true;
        lnkImprimir.Visible =true;
    }

    protected void lnkArchivo_Click(object sender, EventArgs e)
    {
        lblError2.Text = "";

        int[] sesiones = obtieneSesiones();
        string ruta = Server.MapPath("~/TMP");
        int consulta = Convert.ToInt32(RadGrid1.SelectedValues["id_consulta"]);
        int adjuntos = Convert.ToInt32(RadGrid2.SelectedValues["id_adjunto"]);
        int idCliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        int renglonDocto = Convert.ToInt32(adjuntos);
        ConsBuro docto = new ConsBuro();
        docto.empresa = sesiones[2];
        docto.sucursal = sesiones[3];
        docto.idConsultaEdita = consulta;
        docto.idClienteEdita = idCliente;
        docto.idAdjunto = renglonDocto;
        docto.obtieneImagen();
        object[] retorno = docto.retorno;
        if (Convert.ToBoolean(retorno[0]))
        {
            DataSet docuemntos = (DataSet)retorno[1];

            if (docuemntos.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in docuemntos.Tables[0].Rows)
                {
                    string nombreFoto = r[0].ToString();
                    string extension = r[1].ToString().Trim();
                    byte[] imagenBuffer = (byte[])r[2];

                    string rutaArchivo = ruta + "\\" + nombreFoto.Trim() + "." + extension.ToLower().Trim();
                    FileInfo archivo = new FileInfo(rutaArchivo);
                    if (archivo.Exists)
                        archivo.Delete();

                    switch (extension.ToLower())
                    {
                        case "jpeg":
                            System.IO.MemoryStream st = new System.IO.MemoryStream(imagenBuffer);
                            System.Drawing.Image foto = System.Drawing.Image.FromStream(st);
                            System.Drawing.Imaging.ImageFormat formato = obtieneFormato(extension.ToLower());
                            foto.Save(rutaArchivo, formato);
                            break;
                        case "jpg":
                            st = new System.IO.MemoryStream(imagenBuffer);
                            foto = System.Drawing.Image.FromStream(st);
                            formato = obtieneFormato(extension.ToLower());
                            foto.Save(rutaArchivo, formato); break;
                        case "png":
                            st = new System.IO.MemoryStream(imagenBuffer);
                            foto = System.Drawing.Image.FromStream(st);
                            formato = obtieneFormato(extension.ToLower());
                            foto.Save(rutaArchivo, formato); break;
                        case "gif":
                            st = new System.IO.MemoryStream(imagenBuffer);
                            foto = System.Drawing.Image.FromStream(st);
                            formato = obtieneFormato(extension.ToLower());
                            foto.Save(rutaArchivo, formato); break;
                        case "bmp":
                            st = new System.IO.MemoryStream(imagenBuffer);
                            foto = System.Drawing.Image.FromStream(st);
                            formato = obtieneFormato(extension.ToLower());
                            foto.Save(rutaArchivo, formato); break;
                        case "tiff":
                            st = new System.IO.MemoryStream(imagenBuffer);
                            foto = System.Drawing.Image.FromStream(st);
                            formato = obtieneFormato(extension.ToLower());
                            foto.Save(rutaArchivo, formato);
                            break;
                        default:
                            File.WriteAllBytes(rutaArchivo, imagenBuffer);
                            break;
                    }

                    lnkDescarga_Click(archivo, extension, ruta);
                }
            }
            else
            {
                lblError2.Text = "No existen archivos a descargar o no ha seleccionado alguno de los archivos para descargarlo";
            }
               
        }

    }

    protected void lnkDescarga_Click(FileInfo archivo, string extension, string ruta)
    {
        Response.Clear();
        FileInfo doc = new FileInfo(archivo.FullName);

        Response.AddHeader("content-disposition", "attachment;filename=" + doc.Name);
        Response.WriteFile(ruta + "\\" + doc.Name);
        Response.End();
    }
   
    private ImageFormat obtieneFormato(string extension)
    {
        System.Drawing.Imaging.ImageFormat formato;
        switch (extension.ToLower())
        {
            case "jpg":
                formato = System.Drawing.Imaging.ImageFormat.Jpeg;
                break;
            case "png":
                formato = System.Drawing.Imaging.ImageFormat.Png;
                break;
            case "jpeg":
                formato = System.Drawing.Imaging.ImageFormat.Jpeg;
                break;
            case "gif":
                formato = System.Drawing.Imaging.ImageFormat.Gif;
                break;
            case "bmp":
                formato = System.Drawing.Imaging.ImageFormat.Bmp;
                break;
            case "tiff":
                formato = System.Drawing.Imaging.ImageFormat.Tiff;
                break;
            default:
                formato = System.Drawing.Imaging.ImageFormat.Jpeg;
                break;
        }
        return formato;
    }

    protected void EstadoCp(object sender, EventArgs e)
    {
        ConsBuro obten = new ConsBuro();
        obten.codigo = Convert.ToInt32(txt_cp_cb.Text);
        int cp = Convert.ToInt32(txt_cp_cb.Text);
        obten.datosCP();
        if (Convert.ToBoolean(obten.retorno[0]))
        {
            DataSet ds = (DataSet)obten.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txtEstado.Text = Convert.ToString(r[0]);
                txtMunicipio.Text = Convert.ToString(r[1]);
            }
            SqlDataSourceCombo.SelectCommand = "select d_asenta,d_codigo from an_codigopostal  where d_codigo=" + cp;
            SqlDataSourceCombo.DataBind();
        }
    }

}