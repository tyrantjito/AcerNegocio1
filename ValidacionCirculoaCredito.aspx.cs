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

public partial class ValidacionCirculoaCredito : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lnkAbreWindow1_Click(object sender, EventArgs e)
    {
        lblErrorAfuera.Text = "";
        lblValida.Text = "Valida Documentos";
        int[] sesiones = obtieneSesiones();
        ValidCirculoCred info = new ValidCirculoCred();
        info.empresa = sesiones[2];
        info.sucursal = sesiones[3];
        info.idcliente = Convert.ToInt32(cmb_nombre_cli.SelectedValue);
        info.tieneAdjuntos();

        if (Convert.ToInt32(info.retorno[1]) != 0)
        {
            int idCliente = Convert.ToInt32(cmb_nombre_cli.SelectedValue);
            int idConsulta = Convert.ToInt32(cmb_Consulta.SelectedValue);
            SqlDataSource1.SelectCommand = "select  case when ad.validacion_digital='AUT' then 'AUTORIZADO' when validacion_digital='NEG' then 'NEGADO' when validacion_digital is null then'PENDIENTE' else '' end as validacion_digital,ad.id_adjunto,cb.nombre_completo,ad.id_cliente,ad.id_consulta,ad.descripcion, case ad.tipo when 'ID' then 'IDENTIFICACION' else 'REPORTE' end as tipo from an_adjuntos_consulta_buro ad inner join an_Clientes cb on cb.id_cliente = ad.id_cliente  where ad.id_empresa = " + sesiones[2] + " and ad.id_sucursal =" + sesiones[3] + " and ad.id_cliente=" + idCliente + " and ad.id_consulta=" + idConsulta;
            RadGrid1.DataBind();
            pnlMask.Visible = true;
            windowAutorizacion.Visible = true;
        }
        else
        { 
        lblErrorAfuera.Visible = true;
        lblErrorAfuera.Text = "El cliente no cuenta con ningun documento adjunto";
        }

    }

    protected void lnkAbreWindow2_Click(object sender, EventArgs e)
    {
        lblErrorAfuera.Text = "";
        lblRec.Text = "Recepcion Documentos";
        int[] sesiones = obtieneSesiones();
        ValidCirculoCred info = new ValidCirculoCred();
        info.empresa = sesiones[2];
        info.sucursal = sesiones[3];
        info.idcliente = Convert.ToInt32(cmb_nombre_cli.SelectedValue);
        info.tieneAdjuntos();

        if (Convert.ToInt32(info.retorno[1]) != 0)
        {
            int idCliente = Convert.ToInt32(cmb_nombre_cli.SelectedValue);
            int idConsulta = Convert.ToInt32(cmb_Consulta.SelectedValue);
            SqlDataSource2.SelectCommand = "select  case when ad.validacion_fisica='AUT' then 'AUTORIZADO' when validacion_fisica='NEG' then 'NEGADO' when validacion_fisica is null then'PENDIENTE' else '' end as validacion_fisica,ad.id_adjunto,cb.nombre_completo,ad.id_cliente,ad.id_consulta,ad.descripcion, case ad.tipo when 'ID' then 'IDENTIFICACION' else 'REPORTE' end as tipo from an_adjuntos_consulta_buro ad inner join an_clientes cb on cb.id_cliente = ad.id_cliente where ad.id_empresa = " + sesiones[2] + " and ad.id_sucursal =" + sesiones[3] + " and ad.id_cliente=" + idCliente + " and ad.id_consulta=" + idConsulta; 
            RadGrid2.DataBind();
            pnlMask1.Visible = true;
            windowAutorizacion1.Visible = true;
        }
        else
        {
            lblErrorAfuera.Visible = true;
            lblErrorAfuera.Text = "El cliente no cuenta con ningun documento adjunto";
        }
    }

    protected void lnkCerrar_Click(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        ValidCirculoCred actu = new ValidCirculoCred();
        actu.empresa = sesiones[2];
        actu.sucursal = sesiones[3];
        actu.idcliente = Convert.ToInt32(cmb_nombre_cli.SelectedValue);
        actu.idconsulta = Convert.ToInt32(cmb_Consulta.SelectedValue);
        actu.tieneValidacionDigital();

        if (Convert.ToInt32(actu.retorno[1]) == 2)
        {
            actu.actualizaConsultaBuroDIGAUT();

        }
        else 
        {
            actu.tieneNegacionDigital();
            if(Convert.ToInt32(actu.retorno[1]) == 2)
            {
                actu.actualizaConsultaBuroDIGNIEG();
            }
            else
            {
                actu.actualizaConsultaBuroDIGPEN();
            }
        }
        cmb_nombre_cli.SelectedValue = 0.ToString();
        pnlMask.Visible = false;
        windowAutorizacion.Visible = false;
        lnkAbreWindow.Visible = false;
        lnkAbreWindow2.Visible = false;
        


    }

    protected void lnkCerrar_Click1(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        ValidCirculoCred actu = new ValidCirculoCred();
        actu.empresa = sesiones[2];
        actu.sucursal = sesiones[3];
        actu.idcliente = Convert.ToInt32(cmb_nombre_cli.SelectedValue);
        actu.idconsulta = Convert.ToInt32(cmb_Consulta.SelectedValue);
        actu.tieneValidacionFisica();

        if (Convert.ToInt32(actu.retorno[1]) == 2)
        {
            actu.actualizaConsultaBuroFISAUT();

            actu.tieneValidacionDigital();

            if(Convert.ToInt32(actu.retorno[1]) == 2)
            {
                actu.actualizaConsultaBuroProcesableAUT();
            }

        }
        else 
        {
            actu.tieneNegacionFisica();

            if (Convert.ToInt32(actu.retorno[1]) == 2)
            {
                actu.actualizaConsultaBuroFISNEG();

                actu.tieneNegacionDigital();
                if(Convert.ToInt32(actu.retorno[1]) == 2)
                {
                    actu.actualizaConsultaBuroProcesableNEG();
                }
            }
            else
            {
                actu.actualizaConsultaBuroFISPEN();
            }
        }
        cmb_nombre_cli.SelectedValue = 0.ToString();
        
        


        pnlMask1.Visible = false;
        lnkAbreWindow.Visible = false;
        lnkAbreWindow2.Visible = false;
        windowAutorizacion1.Visible = false;
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



    protected void cmb_nombre_cli_SelectedIndexChanged(object sender, EventArgs e)
    {
        int valor;
        try { valor = Convert.ToInt32(cmb_nombre_cli.SelectedValue); }
        catch (Exception) { valor = 0; }
        if (valor != 0)
        {
            SqlDataSourceConsulta.SelectCommand = "";
            SqlDataSourceConsulta.DataBind();
            int[] sesiones = obtieneSesiones();
            int idCliente = Convert.ToInt32(cmb_nombre_cli.SelectedValue);
            SqlDataSourceConsulta.SelectCommand = "select convert(char(10),fecha_autorizacion,120) as fecha_autorizacion,id_consulta  from an_solicitud_consulta_buro where id_empresa=" + sesiones[2] + " and id_sucursal=" + sesiones[3] + " and id_cliente=" + idCliente;
            SqlDataSourceConsulta.DataBind();

            ValidCirculoCred tiene = new ValidCirculoCred();
            tiene.empresa = sesiones[2];
            tiene.sucursal = sesiones[3];
            tiene.idcliente = Convert.ToInt32(cmb_nombre_cli.SelectedValue);
            tiene.idconsulta = Convert.ToInt32(cmb_Consulta.SelectedValue);
            tiene.tienePrimeraValidacion();
            if (Convert.ToInt32(tiene.retorno[1]) == 2)
            {
                lnkAbreWindow2.Visible = true;
            }
            else
            {
                lnkAbreWindow2.Visible = false;
            }

            lnkAbreWindow.Visible = true;
        }
        else
        {
            lnkAbreWindow.Visible = false;
            lnkAbreWindow2.Visible = false;
        }
        
    }


    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        ValidCirculoCred trae = new ValidCirculoCred();
        trae.empresa = sesiones[2];
        trae.sucursal = sesiones[3];
        trae.idcliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
        trae.idconsulta = Convert.ToInt32(RadGrid1.SelectedValues["id_consulta"]);
        trae.idadjunto = Convert.ToInt32(RadGrid1.SelectedValues["id_adjunto"]);
        trae.obtieneAdjuntoCliente();
        if (Convert.ToBoolean(trae.retorno[0]))
        {
            DataSet ds = (DataSet)trae.retorno[1];

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                string identificador = r[9].ToString();
                if(identificador == "ID")
                {
                    identificador = "IDENTIFICACION";
                }
                else
                {
                    identificador= "REPORTE";
                }
                txtObservaciones.Visible = true;
                txtObservaciones.Text = r[11].ToString();
                if (r[10].ToString() == "AUT")
                {
                    lnkAutorizaDIG.Visible = false;
                    lblAdjunto.Text = r[9].ToString();
                    lnkNiegaDIG.Visible = true;
                }
                else
                {
                    if (r[10].ToString() == "NEG")
                    {
                        lnkAutorizaDIG.Visible = true;
                        lblAdjunto.Text = r[9].ToString();
                        lnkNiegaDIG.Visible = false;
                    }
                    else
                    {
                        lblAdjunto.Text = r[9].ToString();
                        lnkAutorizaDIG.Visible = true;
                        lnkNiegaDIG.Visible = true;
                    }
                }
            }
        }
    }

    protected void lnkAutorizaDIG_Click(object sender, EventArgs e)
    {
        Fechas fecha = new Fechas();
        int[] sesiones = obtieneSesiones();
        ValidCirculoCred autoriza = new ValidCirculoCred();
        autoriza.usuario = sesiones[0];
        autoriza.empresa = sesiones[2];
        autoriza.sucursal = sesiones[3];
        autoriza.observaciones = txtObservaciones.Text;
        autoriza.fechaLocal = fecha.obtieneFechaLocal().ToString("yyyy-MM-dd");
        autoriza.idcliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
        autoriza.idconsulta = Convert.ToInt32(RadGrid1.SelectedValues["id_consulta"]);
        autoriza.idadjunto = Convert.ToInt32(RadGrid1.SelectedValues["id_adjunto"]);
        autoriza.autorizaDigital();

        if (Convert.ToBoolean(autoriza.retorno[0]))
        {
            lblErrorDigital.Text = "Documento Autorizado";
            txtObservaciones.Visible = false;
            lblAdjunto.Text = "";
            lnkAutorizaDIG.Visible = false;
            lnkNiegaDIG.Visible = false;
            int idCliente = Convert.ToInt32(cmb_nombre_cli.SelectedValue);
            int idConsulta = Convert.ToInt32(cmb_Consulta.SelectedValue);
            SqlDataSource1.SelectCommand = "select  case when ad.validacion_digital='AUT' then 'AUTORIZADO' when validacion_digital='NEG' then 'NEGADO' when validacion_digital is null then'PENDIENTE' else '' end as validacion_digital,ad.id_adjunto,cb.nombre_completo,ad.id_cliente,ad.id_consulta,ad.descripcion, case ad.tipo when 'ID' then 'IDENTIFICACION' else 'REPORTE' end as tipo from an_adjuntos_consulta_buro ad inner join an_Clientes cb on cb.id_cliente = ad.id_cliente  where ad.id_empresa = " + sesiones[2] + " and ad.id_sucursal =" + sesiones[3] + " and ad.id_cliente=" + idCliente + " and ad.id_consulta=" + idConsulta;
            RadGrid1.DataBind();
        }
        else
        {
            lblErrorDigital.Text = "Error al Autorizar el documento ";
        }
    }

    protected void lnkNiegaDIG_Click(object sender, EventArgs e)
    {
        Fechas fecha = new Fechas();
        int[] sesiones = obtieneSesiones();
        ValidCirculoCred autoriza = new ValidCirculoCred();
        autoriza.usuario = sesiones[0];
        autoriza.empresa = sesiones[2];
        autoriza.sucursal = sesiones[3];
        autoriza.observaciones = txtObservaciones.Text;
        autoriza.fechaLocal = fecha.obtieneFechaLocal().ToString("yyyy-MM-dd");
        autoriza.idcliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
        autoriza.idconsulta = Convert.ToInt32(RadGrid1.SelectedValues["id_consulta"]);
        autoriza.idadjunto = Convert.ToInt32(RadGrid1.SelectedValues["id_adjunto"]);
        autoriza.niegaDigital();

        if (Convert.ToBoolean(autoriza.retorno[0]))
        {
            lblErrorDigital.Text = "Documento Declinado";
            txtObservaciones.Visible = false;
            lblAdjunto.Text = "";
            lnkAutorizaDIG.Visible = false;
            lnkNiegaDIG.Visible = false;
            int idCliente = Convert.ToInt32(cmb_nombre_cli.SelectedValue);
            int idConsulta = Convert.ToInt32(cmb_Consulta.SelectedValue);
            SqlDataSource1.SelectCommand = "select  case when ad.validacion_digital='AUT' then 'AUTORIZADO' when validacion_digital='NEG' then 'NEGADO' when validacion_digital is null then'PENDIENTE' else '' end as validacion_digital,ad.id_adjunto,cb.nombre_completo,ad.id_cliente,ad.id_consulta,ad.descripcion, case ad.tipo when 'ID' then 'IDENTIFICACION' else 'REPORTE' end as tipo from an_adjuntos_consulta_buro ad inner join an_Clientes cb on cb.id_cliente = ad.id_cliente  where ad.id_empresa = " + sesiones[2] + " and ad.id_sucursal =" + sesiones[3] + " and ad.id_cliente=" + idCliente + " and ad.id_consulta=" + idConsulta;
            RadGrid1.DataBind();
        }
        else
        {
            lblErrorDigital.Text = "Error al Autorizar el documento ";
        }
    }


    protected void lnkAutorizaFIS_Click(object sender, EventArgs e)
    {
        Fechas fecha = new Fechas();
        int[] sesiones = obtieneSesiones();
        ValidCirculoCred autoriza = new ValidCirculoCred();
        autoriza.usuario = sesiones[0];
        autoriza.empresa = sesiones[2];
        autoriza.sucursal = sesiones[3];
        autoriza.observaciones = txtObservacionesFIS.Text;
        autoriza.fechaLocal = fecha.obtieneFechaLocal().ToString("yyyy-MM-dd");
        autoriza.idcliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        autoriza.idconsulta = Convert.ToInt32(RadGrid2.SelectedValues["id_consulta"]);
        autoriza.idadjunto = Convert.ToInt32(RadGrid2.SelectedValues["id_adjunto"]);
        autoriza.autorizaFisica();

        if (Convert.ToBoolean(autoriza.retorno[0]))
        {
            lblErrorDigital.Text = "Documento Autorizado";
            txtObservacionesFIS.Visible = false;
            txtObservacionesFIS.Text = "";
            lblTipo2.Text = "";
            lnkAutorizaFis.Visible = false;
            lnkNiegaFis.Visible = false;
            int idCliente = Convert.ToInt32(cmb_nombre_cli.SelectedValue);
            int idConsulta = Convert.ToInt32(cmb_Consulta.SelectedValue);
            SqlDataSource2.SelectCommand = "select  case when ad.validacion_fisica='AUT' then 'AUTORIZADO' when validacion_fisica='NEG' then 'NEGADO' when validacion_fisica is null then'PENDIENTE' else '' end as validacion_fisica,ad.id_adjunto,cb.nombre_completo,ad.id_cliente,ad.id_consulta,ad.descripcion, case ad.tipo when 'ID' then 'IDENTIFICACION' else 'REPORTE' end as tipo from an_adjuntos_consulta_buro ad inner join an_clientes cb on cb.id_cliente = ad.id_cliente where ad.id_empresa = " + sesiones[2] + " and ad.id_sucursal =" + sesiones[3] + " and ad.id_cliente=" + idCliente + " and ad.id_consulta=" + idConsulta;
            RadGrid2.DataBind();
        }
        else
        {
            lblErrorFisica.Text = "Error al Autorizar el documento ";
        }
    }

    protected void lnkNiegaFIS_Click(object sender, EventArgs e)
    {
        Fechas fecha = new Fechas();
        int[] sesiones = obtieneSesiones();
        ValidCirculoCred autoriza = new ValidCirculoCred();
        autoriza.usuario = sesiones[0];
        autoriza.empresa = sesiones[2];
        autoriza.sucursal = sesiones[3];
        autoriza.observaciones = txtObservacionesFIS.Text;
        autoriza.fechaLocal = fecha.obtieneFechaLocal().ToString("yyyy-MM-dd");
        autoriza.idcliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        autoriza.idconsulta = Convert.ToInt32(RadGrid2.SelectedValues["id_consulta"]);
        autoriza.idadjunto = Convert.ToInt32(RadGrid2.SelectedValues["id_adjunto"]);
        autoriza.niegaFisica();

        if (Convert.ToBoolean(autoriza.retorno[0]))
        {
            lblErrorDigital.Text = "Documento Declinado";
            txtObservacionesFIS.Visible = false;
            txtObservacionesFIS.Text = "";
            lblTipo2.Text = "";
            lnkAutorizaFis.Visible = false;
            lnkNiegaFis.Visible = false;
            int idCliente = Convert.ToInt32(cmb_nombre_cli.SelectedValue);
            int idConsulta = Convert.ToInt32(cmb_Consulta.SelectedValue);
            SqlDataSource2.SelectCommand = "select  case when ad.validacion_fisica='AUT' then 'AUTORIZADO' when validacion_fisica='NEG' then 'NEGADO' when validacion_fisica is null then'PENDIENTE' else '' end as validacion_fisica,ad.id_adjunto,cb.nombre_completo,ad.id_cliente,ad.id_consulta,ad.descripcion, case ad.tipo when 'ID' then 'IDENTIFICACION' else 'REPORTE' end as tipo from an_adjuntos_consulta_buro ad inner join an_solicitud_consulta_buro cb on cb.id_cliente = ad.id_cliente and cb.id_consulta = ad.id_consulta where ad.id_empresa = " + sesiones[2] + " and ad.id_sucursal =" + sesiones[3] + " and ad.id_cliente=" + idCliente + " and ad.id_consulta=" + idConsulta; 
            RadGrid2.DataBind();
        }
        else
        {
            lblErrorFisica.Text = "Error al Autorizar el documento ";
        }
    }



    protected void RadGrid2_SelectedIndexChanged(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        ValidCirculoCred trae = new ValidCirculoCred();
        trae.empresa = sesiones[2];
        trae.sucursal = sesiones[3];
        trae.idcliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        trae.idconsulta = Convert.ToInt32(RadGrid2.SelectedValues["id_consulta"]);
        trae.idadjunto = Convert.ToInt32(RadGrid2.SelectedValues["id_adjunto"]);
        trae.obtieneAdjuntoCliente();
        if (Convert.ToBoolean(trae.retorno[0]))
        {
            DataSet ds = (DataSet)trae.retorno[1];

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                string identificador = r[9].ToString();
                if (identificador == "ID")
                {
                    identificador = "IDENTIFICACION";
                }
                else
                {
                    identificador = "REPORTE";
                }
                lblTipo2.Text = identificador;
                txtObservacionesFIS.Visible = true;
                txtObservacionesFIS.Text = r[15].ToString();
                if (r[14].ToString() == "AUT")
                {
                    lnkAutorizaFis.Visible = false;
                    
                    lnkNiegaFis.Visible = true;
                }
                else
                {
                    if (r[14].ToString() == "NEG")
                    {
                        lnkAutorizaFis.Visible = true;
                        lnkNiegaFis.Visible = false;
                    }
                    else
                    {
                        lnkAutorizaFis.Visible = true;
                        lnkNiegaFis.Visible = true;
                    }
                }
            }
        }
    }

    protected void lnkArchivo_Click(object sender, EventArgs e)
    {
        //lblError2.Text = "";

        int[] sesiones = obtieneSesiones();
        string ruta = Server.MapPath("~/TMP");
        int consulta = Convert.ToInt32(RadGrid1.SelectedValues["id_consulta"]);
        int adjuntos = Convert.ToInt32(RadGrid1.SelectedValues["id_adjunto"]);
        int idCliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
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
              //  lblError2.Text = "No existen archivos a descargar o no ha seleccionado alguno de los archivos para descargarlo";
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

}