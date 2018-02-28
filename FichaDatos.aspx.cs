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


public partial class FichaDatos : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        validarNyL vln = new validarNyL(); 
        txtFecha_cli.MinDate = DateTime.Now.AddYears(-70);
        txtFecha_cli.MaxDate = DateTime.Now.AddYears(-18) ;
        txt_f_nac_pr.MinDate = DateTime.Now.AddYears(-70);
        txt_f_nac_pr.MaxDate = DateTime.Now.AddYears(-18);
        txt_fnac_provee.MinDate = DateTime.Now.AddYears(-70);
        txt_fnac_provee.MaxDate = DateTime.Now.AddYears(-18);
    }



    private int[] obtieneSesiones()
    {
        int[] sesiones = new int[4];
        sesiones[0] = Convert.ToInt32(Request.QueryString["u"]);
        sesiones[1] = Convert.ToInt32(Request.QueryString["p"]);
        sesiones[2] = Convert.ToInt32(Request.QueryString["e"]);
        sesiones[3] = Convert.ToInt32(Request.QueryString["t"]);
        return sesiones;
    }

    protected void lnkAbreWindow_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "Nueva Cédula";
        borrarCampos();
        pnlMask.Visible = true;
        windowAutorizacion.Visible = true;
      
    }

    protected void lnkAbreEdicion_Click(object sender, EventArgs e)
    {
        borrarCampos();
         lblTitulo.Text = "Edita Cédula";
        FDat edit = new FDat();
        int idFicha = Convert.ToInt32(RadGrid1.SelectedValues["id_ficha"]);
        int cliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
        int[] sesiones = obtieneSesiones();
        edit.empresa = sesiones[2];
        edit.sucursal = sesiones[3];
        edit.ficha = idFicha;
        edit.id_cliente =cliente;
        edit.obtieneCliente();

        txt_nombre.Visible = true;
        cmb_nombre.Visible = false;

        string nombre = "";
        if (Convert.ToBoolean(edit.retorno[0]))
        {
            DataSet ds1 = (DataSet)edit.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                nombre = r1[0].ToString();
               txt_nombre.Text = nombre;

            }
        }


                edit.obtieneFichaEdit();
        if(Convert.ToBoolean(edit.retorno[0]))
        {
            DataSet ds = (DataSet)edit.retorno[1];

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                //Seccion Principal


                try { txtFecha_cli.SelectedDate = Convert.ToDateTime(r[4]); }
                catch (Exception) { txtFecha_cli.Clear(); }
                cmbEstados.SelectedValue = r[5].ToString();
                cmb_gen_cli.SelectedValue = r[6].ToString();
                cmb_ec_cli.SelectedValue = r[7].ToString();
                txt_cre_cli.Text = r[8].ToString();
                txt_curp_cli.Text = r[9].ToString();
                txt_rfc_cli.Text = r[10].ToString();
                cmb_ne_cli.SelectedValue = r[11].ToString();
                cmb_rol_cli.SelectedValue = r[12].ToString();
                txtn_hijos_cli.Text = r[13].ToString();
                txt_eco_cli.Text = r[14].ToString();
                txt_tel_cli.Text = r[15].ToString();
                txt_cel_cli.Text = r[16].ToString();
                txt_correo_cli.Text = r[17].ToString();
                // Seccion Direccion
                txt_calle_cli.Text = r[18].ToString();
                txt_n_ext_cli.Text = r[19].ToString();
                txt_nint_cli.Text = r[20].ToString();
                // cmbColonia_cli.SelectedValue = r[21].ToString();
                //cmbColonia_cli.Visible = false;
                txt_colonia.Visible = true;
                txt_colonia.Text = r[21].ToString();
                txt_cp_cli.Text = r[22].ToString();
                txt_del_cli.Text = r[23].ToString();
                txt_estado_cli.Text = r[24].ToString();
                txt_resi_cli.Text = r[25].ToString();
                txtn_habi_cli.Text = r[26].ToString();
                //   cmb_con_cli.SelectedValue = r[27].ToString();
                //Seccion Esposo
                txt_a_pat_es.Text = r[28].ToString();
                txt_a_mat_es.Text = r[29].ToString();
                txt_nom_es.Text = r[30].ToString();
                txt_ocu_esp.Text = r[32].ToString();
                txt_tel_trab_esp.Text = r[33].ToString();
                txt_tel_casa_esp.Text = r[34].ToString();
                txt_tel_cel_esp.Text = r[35].ToString();
                // Seccion Datos Negocio
                txt_calle_neg.Text = r[36].ToString();
                txt_n_exterior_neg.Text = r[37].ToString();
                txt_n_int_neg.Text = r[38].ToString();
                //cmb_coloNeg.Visible = false;
                txt_colo_neg.Visible = true;
                txt_colo_neg.Text = r[39].ToString();
                //cmb_coloNeg.SelectedValue = r[39].ToString();
                txt_cp_neg.Text = r[40].ToString();
                txt_del_neg.Text = r[41].ToString();
                txt_estado_neg.Text = r[42].ToString();
                txt_tel_feijo_neg.Text = r[43].ToString();
                txt_anti_neg.Text = r[44].ToString();
                txt_rz_neg.Text = r[45].ToString();
                cmbt_es_cli.Text = r[46].ToString();
                txt_eper_neg.Text = r[47].ToString();
                txt_eeve_neg.Text = r[48].ToString();
                txt_gp_neg.Text = r[49].ToString();
                txt_imgp_neg.Text = r[50].ToString();
                txt_oa_neg.Text = r[51].ToString();
                txt_imoa_neg.Text = r[52].ToString();
                //Seccion Referencias
                txt_nom_ref.Text = r[53].ToString();
                txt_tel_fijo_ref.Text = r[54].ToString();
                txt_tel_cel_ref.Text = r[55].ToString();
                txt_paren_ref.Text = r[56].ToString();
                txt_tiem_ref.Text = r[57].ToString();
                //Seccion Tipo Cliente
                cmb_preg1_ref.SelectedValue = r[58].ToString();
                if (r[61].ToString() == "N  ")
                {
                    txt_carg_ref.Text = "N/A";
                    txt_dep_ref.Text = "N/A";
                    txt_per_ref.Text = "N/A";
                }
                else
                {
                    txt_carg_ref.Text = r[59].ToString();
                    txt_dep_ref.Text = r[60].ToString();
                    txt_per_ref.Text = r[61].ToString();

                }
                cmb_preg2_ref.SelectedValue = r[62].ToString();
                if (r[63].ToString() == "N  ")
                {
                    txt_nomFam_ref.Text = "N/A";
                    txt_parentesco_ref.Text = "N/A";
                    txt_cargo_des_ref.Text = "N/A";
                    txt_depen_ref.Text = "N/A";
                    txt_periodo_ref.Text = "N/A";
                }
                else
                {
                    txt_nomFam_ref.Text = "N/A";
                    txt_parentesco_ref.Text = "N/A";
                    txt_cargo_des_ref.Text = "N/A";
                    txt_depen_ref.Text = "N/A";
                    txt_periodo_ref.Text = "N/A";
                }
                //Seccion Propietario
                txt_ap_pr.Text = r[68].ToString();
                txt_am_pr.Text = r[69].ToString();
                txt_nom_pr.Text = r[70].ToString();
                try { txt_f_nac_pr.SelectedDate = Convert.ToDateTime(r[72]); }
                catch (Exception) { txt_f_nac_pr.Clear(); }
                txt_enac_pr.Text = r[73].ToString();
                txt_nac_pr.Text = r[74].ToString();
                cmb_gen_pr.SelectedValue = r[75].ToString();
                cmb_ec_pr.SelectedValue = r[76].ToString();
                txt_no_cre_pr.Text = r[77].ToString();
                txt_curp_pr.Text = r[78].ToString();
                txt_rfc_pr.Text = r[79].ToString();
                cmb_ne_pr.SelectedValue = r[80].ToString();
                cmb_rol_pr.SelectedValue = r[81].ToString();
                txt_nhijos_pr.Text = r[82].ToString();
                txt_ndep_pr.Text = r[83].ToString();
                txt_ocu_pr.Text = r[84].ToString();
                txt_tel_fijo_pr.Text = r[85].ToString();
                txt_tel_cel_pr.Text = r[86].ToString();
                txt_correo_pr.Text = r[87].ToString();
                //Seccion Domicilio Propietario
                txt_calle_pr.Text = r[88].ToString();
                txt_nex_pr.Text = r[89].ToString();
                txt_nin_pr.Text = r[90].ToString();
                // cmb_colonia_pr.Visible = false;
                txt_col_pr.Visible = true;
                txt_col_pr.Text = r[91].ToString();
                //txt_col_pr.Text = r[91].ToString();
                txt_cp_pr.Text = r[92].ToString();
                txt_del_pr.Text = r[93].ToString();
                txt_estado_pr.Text = r[94].ToString();
                txt_tre_pr.Text = r[95].ToString();
                txt_nhab_pr.Text = r[96].ToString();
                // Seccion Proveedor
                txt_apat_prove.Text = r[97].ToString();
                txt_amat_provee.Text = r[98].ToString();
                txt_nom_provee.Text = r[99].ToString();

                try { txt_fnac_provee.SelectedDate = Convert.ToDateTime(r[101]); }
                catch (Exception) { txt_fnac_provee.Clear(); }

                txt_enac_provee.Text = r[102].ToString();
                txt_nac_provee.Text = r[103].ToString();
                cmb_genero_provee.SelectedValue = r[104].ToString();
                cmb_ec_provee.SelectedValue = r[105].ToString();
                txt_ncre_provee.Text = r[106].ToString();
                txt_curp_provee.Text = r[107].ToString();
                txt_rfc_provee.Text = r[108].ToString();
                cmb_ne_provee.SelectedValue = r[109].ToString();
                cmb_rol_provee.SelectedValue = r[110].ToString();
                txt_nohijos_provee.Text = r[111].ToString();
                txt_nodep_provee.Text = r[112].ToString();
                txt_ocu_prove.Text = r[113].ToString();
                txt_tel_fijo_provee.Text = r[114].ToString();
                txt_tel_cel_prove.Text = r[115].ToString();
                txt_correo_prove.Text = r[116].ToString();
                txt_calle_provee.Text = r[117].ToString();
                txt_noext_provee.Text = r[118].ToString();
                txt_noint_provee.Text = r[119].ToString();
                txt_col_prove.Visible = true;
                txt_col_prove.Text = r[120].ToString();
                //txt_col_prove.Text = r[120].ToString();
                txt_cp_prove.Text = r[121].ToString();
                txt_del_provee.Text = r[122].ToString();
                txt_estado_provee.Text = r[123].ToString();
                txt_tiempores_provee.Text = r[124].ToString();
                txt_nohab_provee.Text = r[125].ToString();
                txt_nombrecompleto_ref2.Text = r[126].ToString();
                txt_tel_fijo_ref2.Text = r[127].ToString();
                txt_tel_cel_ref2.Text = r[128].ToString();
                txt_parentesco_ref2.Text = r[129].ToString();
                txt_tiempo_conocerlo_ref2.Text = r[130].ToString();
                txtTipo.Text = r[131].ToString();
                txtAPatBene.Text = r[132].ToString();
                txtAMatBene.Text = r[133].ToString();
                txtNombreBene.Text = r[134].ToString();
                txtDomComBene.Text = r[135].ToString();
                txtnumExtBene.Text = r[136].ToString();
                txtintExtBene.Text = r[137].ToString();
                txtColLoBene.Text = r[138].ToString();
                txtTelBene.Text = r[139].ToString();
                txtDEmoRazProvee.Text = r[140].ToString();
                txtFirmaElec.Text = r[141].ToString();
                txtDenoPM.Text = r[142].ToString();
                txtNacionalidadpm.Text = r[143].ToString();
                txtObjetoPM.Text = r[144].ToString();
                txtCapitalPM.Text = r[145].ToString();
                txtDomicilioPM.Text = r[146].ToString();
                txtNexteriorpm.Text = r[147].ToString();
                txtninteriorpm.Text = r[148].ToString();
                txtColPM.Visible = true;
                txtColPM.Text = r[149].ToString();
                txtCpPM.Text = r[150].ToString();
                txtDelMunPM.Text = r[151].ToString();
                txtEstadoPM.Text = r[152].ToString();
                txtaccionista1.Text = r[153].ToString();
                txtaccionista2.Text = r[154].ToString();
                txt_correo_ref.Text = r[155].ToString();
                txtCorreo_ref2.Text = r[156].ToString();
            }
        }
       
        pnlMask.Visible = true;
        windowAutorizacion.Visible = true;
    }

   
       
    protected void lnkAdjuntarDoctos_Click(object sender, EventArgs e)
    {
        lblErrorFotos.Text = "";
        lblErrorFotos.CssClass = "errores";
        int[] sesiones = obtieneSesiones();
        int  idFicha = Convert.ToInt32(RadGrid1.SelectedValues["id_ficha"]);
        string ruta = Server.MapPath("~/TMP");
        byte[] imagen = null;
        // Si el directorio no existe, crearlo
        if (!Directory.Exists(ruta))
            Directory.CreateDirectory(ruta);
        int archivos = 0, archivosCargado = 0;
        try
        {
            string filename = "";
            int documentos = rauAdjunto.UploadedFiles.Count;
            archivos = documentos;
            for (int i = 0; i < documentos; i++)
            {
                filename = rauAdjunto.UploadedFiles[i].FileName;
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
                            System.Drawing.Image img = System.Drawing.Image.FromStream(rauAdjunto.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "jpg":
                            img = System.Drawing.Image.FromStream(rauAdjunto.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "png":
                            img = System.Drawing.Image.FromStream(rauAdjunto.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "gif":
                            img = System.Drawing.Image.FromStream(rauAdjunto.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "bmp":
                            img = System.Drawing.Image.FromStream(rauAdjunto.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "tiff":
                            img = System.Drawing.Image.FromStream(rauAdjunto.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo);
                            break;
                        default:
                            Telerik.Web.UI.UploadedFile up = rauAdjunto.UploadedFiles[i];
                            up.SaveAs(archivo);
                            imagen = File.ReadAllBytes(archivo);
                            break;
                    }
                }
                catch (Exception) { imagen = null; }
                if (imagen != null)
                {
                    FDat docto = new FDat();
                    docto.empresa = sesiones[2];
                    docto.sucursal = sesiones[3];
                    docto.ficha = idFicha;
                    docto.adjunto = imagen;
                    docto.nombreAdjunto = segmenatado[0];
                    docto.extension = segmenatado[1].ToLower();
                    docto.agregaAdjunto();
                    object[] retorno = docto.retorno;
                    if (Convert.ToBoolean(retorno[0]))
                        archivosCargado++;
                }
            }

            lblError2.Text = "Se han guardardado " + archivosCargado + " de " + archivos + " seleccionados";
        }
        catch (Exception) { imagen = null; }
        finally
        {
            RadGrid2.DataBind();
        }
        btnAddFotoDanos.Visible = false;
    }
    protected void lnkAdjuntarDoctosID_Click(object sender, EventArgs e)
    {
        lblErrorFotos.Text = "";
        lblErrorFotos.CssClass = "errores";
        int[] sesiones = obtieneSesiones();
        int idFicha = Convert.ToInt32(RadGrid1.SelectedValues["id_ficha"]);
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
                    FDat docto = new FDat();
                    docto.empresa = sesiones[2];
                    docto.sucursal = sesiones[3];
                    docto.ficha = idFicha;
                    docto.adjunto = imagen;
                    docto.nombreAdjunto = segmenatado[0];
                    docto.extension = segmenatado[1].ToLower();
                    docto.agregaAdjunto();
                    object[] retorno = docto.retorno;
                    if (Convert.ToBoolean(retorno[0]))
                        archivosCargado++;
                }
            }

            lblError2.Text = "Se han guardardado " + archivosCargado + " de " + archivos + " seleccionados";
        }
        catch (Exception) { imagen = null; }
        finally
        {
            RadGrid2.DataBind();
        }

       // btndocID.Visible = false;
    }
    protected void lnkAdjuntarDoctosNeg_Click(object sender, EventArgs e)
    {
        lblErrorFotos.Text = "";
        lblErrorFotos.CssClass = "errores";
        int[] sesiones = obtieneSesiones();
        int idFicha = Convert.ToInt32(RadGrid1.SelectedValues["id_ficha"]);
        string ruta = Server.MapPath("~/TMP");
        byte[] imagen = null;
        // Si el directorio no existe, crearlo
        if (!Directory.Exists(ruta))
            Directory.CreateDirectory(ruta);
        int archivos = 0, archivosCargado = 0;
        try
        {
            string filename = "";
            int documentos = rauAdjuntoNeg.UploadedFiles.Count;
            archivos = documentos;
            for (int i = 0; i < documentos; i++)
            {
                filename = rauAdjuntoNeg.UploadedFiles[i].FileName;
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
                            System.Drawing.Image img = System.Drawing.Image.FromStream(rauAdjuntoNeg.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "jpg":
                            img = System.Drawing.Image.FromStream(rauAdjuntoNeg.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "png":
                            img = System.Drawing.Image.FromStream(rauAdjuntoNeg.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "gif":
                            img = System.Drawing.Image.FromStream(rauAdjuntoNeg.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "bmp":
                            img = System.Drawing.Image.FromStream(rauAdjuntoNeg.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "tiff":
                            img = System.Drawing.Image.FromStream(rauAdjuntoNeg.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo);
                            break;
                        default:
                            Telerik.Web.UI.UploadedFile up = rauAdjuntoNeg.UploadedFiles[i];
                            up.SaveAs(archivo);
                            imagen = File.ReadAllBytes(archivo);
                            break;
                    }
                }
                catch (Exception) { imagen = null; }
                if (imagen != null)
                {
                    FDat docto = new FDat();
                    docto.empresa = sesiones[2];
                    docto.sucursal = sesiones[3];
                    docto.ficha = idFicha;
                    docto.adjunto = imagen;
                    docto.nombreAdjunto = segmenatado[0];
                    docto.extension = segmenatado[1].ToLower();
                    docto.agregaAdjunto();
                    object[] retorno = docto.retorno;
                    if (Convert.ToBoolean(retorno[0]))
                        archivosCargado++;
                }
            }

            lblError2.Text = "Se han guardardado " + archivosCargado + " de " + archivos + " seleccionados";
        }
        catch (Exception) { imagen = null; }
        finally
        {
            RadGrid2.DataBind();
        }
       // btnDocneg.Visible = false;
    }
    protected void lnkAdjuntarDoctosReporte_Click(object sender, EventArgs e)
    {
        lblErrorFotos.Text = "";
        lblErrorFotos.CssClass = "errores";
        int[] sesiones = obtieneSesiones();
        int idFicha = Convert.ToInt32(RadGrid1.SelectedValues["id_ficha"]);
        string ruta = Server.MapPath("~/TMP");
        byte[] imagen = null;
        // Si el directorio no existe, crearlo
        if (!Directory.Exists(ruta))
            Directory.CreateDirectory(ruta);
        int archivos = 0, archivosCargado = 0;
        try
        {
            string filename = "";
            int documentos = raureporte.UploadedFiles.Count;
            archivos = documentos;
            for (int i = 0; i < documentos; i++)
            {
                filename = raureporte.UploadedFiles[i].FileName;
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
                            System.Drawing.Image img = System.Drawing.Image.FromStream(raureporte.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "jpg":
                            img = System.Drawing.Image.FromStream(raureporte.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "png":
                            img = System.Drawing.Image.FromStream(raureporte.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "gif":
                            img = System.Drawing.Image.FromStream(raureporte.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "bmp":
                            img = System.Drawing.Image.FromStream(raureporte.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "tiff":
                            img = System.Drawing.Image.FromStream(raureporte.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo);
                            break;
                        default:
                            Telerik.Web.UI.UploadedFile up = raureporte.UploadedFiles[i];
                            up.SaveAs(archivo);
                            imagen = File.ReadAllBytes(archivo);
                            break;
                    }
                }
                catch (Exception) { imagen = null; }
                if (imagen != null)
                {
                    FDat docto = new FDat();
                    docto.empresa = sesiones[2];
                    docto.sucursal = sesiones[3];
                    docto.ficha = idFicha;
                    docto.adjunto = imagen;
                    docto.nombreAdjunto = segmenatado[0];
                    docto.extension = segmenatado[1].ToLower();
                    docto.agregaAdjunto();
                    object[] retorno = docto.retorno;
                    if (Convert.ToBoolean(retorno[0]))
                        archivosCargado++;
                }
            }

            lblError2.Text = "Se han guardardado " + archivosCargado + " de " + archivos + " seleccionados";
        }
        catch (Exception) { imagen = null; }
        finally
        {
            RadGrid2.DataBind();
        }
       // btnDocReport.Visible = false;
    
    }
    protected void lnkAdjuntarDoctosPerma_Click(object sender, EventArgs e)
    {
        lblErrorFotos.Text = "";
        lblErrorFotos.CssClass = "errores";
        int[] sesiones = obtieneSesiones();
        int idFicha = Convert.ToInt32(RadGrid1.SelectedValues["id_ficha"]);
        string ruta = Server.MapPath("~/TMP");
        byte[] imagen = null;
        // Si el directorio no existe, crearlo
        if (!Directory.Exists(ruta))
            Directory.CreateDirectory(ruta);
        int archivos = 0, archivosCargado = 0;
        try
        {
            string filename = "";
            int documentos = raiIdPerm.UploadedFiles.Count;
            archivos = documentos;
            for (int i = 0; i < documentos; i++)
            {
                filename = raiIdPerm.UploadedFiles[i].FileName;
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
                            System.Drawing.Image img = System.Drawing.Image.FromStream(raiIdPerm.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "jpg":
                            img = System.Drawing.Image.FromStream(raiIdPerm.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "png":
                            img = System.Drawing.Image.FromStream(raiIdPerm.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "gif":
                            img = System.Drawing.Image.FromStream(raiIdPerm.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "bmp":
                            img = System.Drawing.Image.FromStream(raiIdPerm.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "tiff":
                            img = System.Drawing.Image.FromStream(raiIdPerm.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo);
                            break;
                        default:
                            Telerik.Web.UI.UploadedFile up = raiIdPerm.UploadedFiles[i];
                            up.SaveAs(archivo);
                            imagen = File.ReadAllBytes(archivo);
                            break;
                    }
                }
                catch (Exception) { imagen = null; }
                if (imagen != null)
                {
                    FDat docto = new FDat();
                    docto.empresa = sesiones[2];
                    docto.sucursal = sesiones[3];
                    docto.ficha = idFicha;
                    docto.adjunto = imagen;
                    docto.nombreAdjunto = segmenatado[0];
                    docto.extension = segmenatado[1].ToLower();
                    docto.agregaAdjunto();
                    object[] retorno = docto.retorno;
                    if (Convert.ToBoolean(retorno[0]))
                        archivosCargado++;
                }
            }

            lblError2.Text = "Se han guardardado " + archivosCargado + " de " + archivos + " seleccionados";
        }
        catch (Exception) { imagen = null; }
        finally
        {
            RadGrid2.DataBind();
        }
        //btnDocPerm.Visible = false;
    }
    protected void lnkAdjuntarDoctosCurpAct_Click(object sender, EventArgs e)
    {
        lblErrorFotos.Text = "";
        lblErrorFotos.CssClass = "errores";
        int[] sesiones = obtieneSesiones();
        int idFicha = Convert.ToInt32(RadGrid1.SelectedValues["id_ficha"]);
        string ruta = Server.MapPath("~/TMP");
        byte[] imagen = null;
        // Si el directorio no existe, crearlo
        if (!Directory.Exists(ruta))
            Directory.CreateDirectory(ruta);
        int archivos = 0, archivosCargado = 0;
        try
        {
            string filename = "";
            int documentos = raucurp.UploadedFiles.Count;
            archivos = documentos;
            for (int i = 0; i < documentos; i++)
            {
                filename = raucurp.UploadedFiles[i].FileName;
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
                            System.Drawing.Image img = System.Drawing.Image.FromStream(raucurp.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "jpg":
                            img = System.Drawing.Image.FromStream(raucurp.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "png":
                            img = System.Drawing.Image.FromStream(raucurp.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "gif":
                            img = System.Drawing.Image.FromStream(raucurp.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "bmp":
                            img = System.Drawing.Image.FromStream(raucurp.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo); break;
                        case "tiff":
                            img = System.Drawing.Image.FromStream(raucurp.UploadedFiles[i].InputStream);
                            img.Save(archivo);
                            imagen = Imagen_A_Bytes(archivo);
                            break;
                        default:
                            Telerik.Web.UI.UploadedFile up = raucurp.UploadedFiles[i];
                            up.SaveAs(archivo);
                            imagen = File.ReadAllBytes(archivo);
                            break;
                    }
                }
                catch (Exception) { imagen = null; }
                if (imagen != null)
                {
                    FDat docto = new FDat();
                    docto.empresa = sesiones[2];
                    docto.sucursal = sesiones[3];
                    docto.ficha = idFicha;
                    docto.adjunto = imagen;
                    docto.nombreAdjunto = segmenatado[0];
                    docto.extension = segmenatado[1].ToLower();
                    docto.agregaAdjunto();
                    object[] retorno = docto.retorno;
                    if (Convert.ToBoolean(retorno[0]))
                        archivosCargado++;
                }
            }

            lblError2.Text = "Se han guardardado " + archivosCargado + " de " + archivos + " seleccionados";
        }
        catch (Exception) { imagen = null; }
        finally
        {
            RadGrid2.DataBind();
        }
     //   btnfoto.Visible = false;
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

    protected void btnEliminarAdjunto_Click(object sender, EventArgs e)
    {
        LinkButton botonEliminar = (LinkButton)sender;
        string[] argumentos = botonEliminar.CommandArgument.ToString().Split(new char[] { ';' });
        int fisha = Convert.ToInt32(argumentos[0]);
        int adjunto = Convert.ToInt32(argumentos[1]);
        int[] sesiones = obtieneSesiones();
        FDat dcto = new FDat();
        dcto.empresa = sesiones[2];
        dcto.sucursal = sesiones[3];
        dcto.idAdjunto = adjunto;
        dcto.ficha = fisha;
        dcto.eliminaAdjunto();
        if (Convert.ToBoolean(dcto.retorno[1]))
        {
            lblError2.Text = "El documento se eliminó correctamente";
            RadGrid2.DataBind();
        }
        else
        {
            lblError2.Text = "No se pudo eliminar el adjunto. Detalle:" + Convert.ToString(dcto.retorno[1]);
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

    protected void btnZoom_Click(object sender, EventArgs e)
    {

    }

    protected void lnkArchivo_Click(object sender, EventArgs e)
    {
        lblError2.Text = "";
        int[] sesiones = obtieneSesiones();
        string ruta = Server.MapPath("~/TMP");
        int fichas = Convert.ToInt32(RadGrid1.SelectedValues["id_ficha"]);
        int adjuntos = Convert.ToInt32(RadGrid2.SelectedValues["Id_Ficha_Adjunto"]); 
        int renglonDocto = Convert.ToInt32(adjuntos);
        FDat docto = new  FDat();
        docto.empresa = sesiones[2];
        docto.sucursal = sesiones[3];
        docto.ficha = fichas;
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

                    descargaArchivo(archivo, extension, ruta);
                }
            }
            else
                lblError2.Text = "No existen archivos a descargar o no ha seleccionado alguno de los archivos para descargarlo";
        }

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

    private void descargaArchivo(FileInfo archivo, string extension, string ruta)
    {
        Response.Clear();
        FileInfo doc = new FileInfo(archivo.FullName);
       
            Response.AddHeader("content-disposition", "attachment;filename=" + doc.Name);
            Response.WriteFile(ruta + "\\" + doc.Name);
            Response.End();
    }

    protected void lnkAgregaSolicitud_Click(object sender, EventArgs e)
    {

        int[] sesiones = obtieneSesiones();
        FDat agrega = new FDat();
        agrega.empresa = sesiones[2];
        agrega.sucursal = sesiones[3];
        string[] nombrecompleto = cmb_nombre.SelectedItem.Text.ToString().Split(new char[] { ' ' });
        agrega.id_cliente = Convert.ToInt32(cmb_nombre.SelectedValue);
        DateTime fecha_cli = Convert.ToDateTime(txtFecha_cli.SelectedDate);
        agrega.f_nacimiento_cli = fecha_cli.ToString("yyyy/MM/dd");
        agrega.e_nacimiento_cli = Convert.ToString(cmbEstados.SelectedValue);
            agrega.genero_cli = Convert.ToChar(cmb_gen_cli.SelectedValue);
            agrega.estado_civil_cli = Convert.ToString(cmb_ec_cli.SelectedValue);
           agrega.no_credencial_ife_cli = txt_cre_cli.Text;
        string curp = txt_curp_cli.Text;
        curp = curp.ToUpper();
            agrega.curp_cli = curp;
            agrega.nivel_escolaridad = Convert.ToString(cmb_ne_cli.SelectedValue);
            string rfc = txt_rfc_cli.Text;
            rfc = rfc.ToUpper();
            agrega.rfc_cli = rfc;
            agrega.rol_cliente = Convert.ToString(cmb_rol_cli.SelectedValue);
            try { agrega.no_hijos_cli = Convert.ToInt32(txtn_hijos_cli.Text); }
            catch (Exception) { agrega.no_hijos_cli = 0; }
            try { agrega.dep_economicos_cli = Convert.ToInt32(txt_eco_cli.Text); }
            catch (Exception) { agrega.dep_economicos_cli = 0; }
            try { agrega.tel_fijo_cli = Convert.ToDecimal(txt_tel_cli.Text); }
            catch (Exception) { agrega.tel_fijo_cli = 0; }
            try { agrega.tel_cel_cli = Convert.ToDecimal(txt_cel_cli.Text); }
            catch (Exception) { agrega.tel_cel_cli = 0; }
            agrega.correo_cli = txt_correo_cli.Text;
            agrega.calle_cli = txt_calle_cli.Text;
        agrega.no_exterior_cli = txt_n_ext_cli.Text;
            agrega.no_interior_cli = txt_nint_cli.Text;
            agrega.colonia_cli = Convert.ToString(cmbColonia_cli.SelectedItem); 
            agrega.cp_cli = txt_cp_cli.Text;                                                                                            
        agrega.del_mun_cli = txt_del_cli.Text;
            agrega.estado_cli = txt_estado_cli.Text;
            agrega.tiempo_residencia_cli = txt_resi_cli.Text;
            try { agrega.no_habitantes_cli = Convert.ToInt32(txtn_habi_cli.Text); }
            catch (Exception) { agrega.no_habitantes_cli = 0; }
            //agrega.condicion_propiedad_cli = Convert.ToString(cmb_con_cli.SelectedValue);
            agrega.nombre_esp = txt_nom_es.Text;
            agrega.a_paterno_esp = txt_a_pat_es.Text;
            agrega.a_materno_esp = txt_a_mat_es.Text;
            agrega.nombre_completo_esp = txt_nom_es.Text + txt_a_pat_es.Text + txt_a_mat_es.Text;
            agrega.ocupacion_esp = txt_ocu_esp.Text;
            try { agrega.tel_trab_esp = Convert.ToDecimal(txt_tel_trab_esp.Text); }
            catch (Exception) { agrega.tel_trab_esp = 0; }
            try { agrega.tel_casa_esp = Convert.ToDecimal(txt_tel_casa_esp.Text); }
            catch (Exception) { agrega.tel_casa_esp = 0; }
            try { agrega.tel_cel_esp = Convert.ToDecimal(txt_tel_cel_esp.Text); }
            catch (Exception) { agrega.tel_cel_esp = 0; }
            agrega.calle_neg = txt_calle_neg.Text;
            agrega.no_exterior_neg = txt_n_exterior_neg.Text;
            agrega.no_interior_neg = txt_n_int_neg.Text;
            agrega.colionia_neg = Convert.ToString(cmb_coloNeg.SelectedItem); 
        try {agrega.cp_neg = Convert.ToInt32(txt_cp_neg.Text);}
            catch (Exception) { agrega.cp_neg = 0; }
            agrega.del_mun_neg = txt_del_neg.Text;
            agrega.estado_neg = txt_estado_neg.Text;
            try { agrega.tel_fijo_neg = Convert.ToDecimal(txt_tel_feijo_neg.Text); }
            catch (Exception) { agrega.tel_fijo_neg = 0; }
            agrega.antiguedad_neg = Convert.ToString(txt_anti_neg.Text);
        string razonSocial = txt_rz_neg.Text;
        razonSocial = razonSocial.ToUpper();
            agrega.razon_social_neg = razonSocial;
            agrega.tipo_neg = cmbt_es_cli.SelectedValue;
            try { agrega.empleados_perma_neg = Convert.ToInt32(txt_eper_neg.Text); }
            catch (Exception) { agrega.empleados_perma_neg = 0; }
            try { agrega.empleados_even_neg = Convert.ToInt32(txt_eeve_neg.Text); }
            catch (Exception) { agrega.empleados_even_neg = 0; }
            agrega.giro_principal_neg = txt_gp_neg.Text;
            try { agrega.ingreso_men_gp_neg = Convert.ToInt32(txt_imgp_neg.Text); }
            catch (Exception) { agrega.ingreso_men_gp_neg = 0; }
            agrega.otras_activ_neg = txt_oa_neg.Text;
            try { agrega.ingreso_men_oa_neg = Convert.ToInt32(txt_imoa_neg.Text); }
            catch (Exception) { agrega.ingreso_men_oa_neg = 0; }
            agrega.nombre_completo_ref = txt_nom_ref.Text;
            try { agrega.telefono_fijo_ref = Convert.ToDecimal(txt_tel_fijo_ref.Text); }
            catch (Exception) { agrega.telefono_fijo_ref = 0; }
        
            try { agrega.telefono_celular_ref = Convert.ToDecimal(txt_tel_cel_ref.Text); }
            catch (Exception) { agrega.telefono_celular_ref = 0; }
            
            agrega.parentesco_relacion_ref = txt_paren_ref.Text;
            agrega.tiempo_conocerlo_ref = Convert.ToString(txt_tiem_ref.Text);
            agrega.cargo_preg_ref = Convert.ToString(cmb_preg1_ref.SelectedValue);
            agrega.cargo_desempeña_ref = txt_carg_ref.Text;
            agrega.dependencia_cargo_ref = txt_dep_ref.Text;
            agrega.periodo_cargo_ref = txt_tiem_ref.Text;
            agrega.parentesco_preg_ref = Convert.ToString(cmb_preg2_ref.SelectedValue);
            agrega.nombre_parentesco_ref = txt_nomFam_ref.Text;
            agrega.parentesco_ref = txt_parentesco_ref.Text;
            agrega.cargo_parentesco_ref = txt_cargo_des_ref.Text;
            agrega.dependencia_parentesco_ref = txt_depen_ref.Text;
            agrega.periodo_parentesco_ref = txt_periodo_ref.Text;
            agrega.a_paterno_pr = txt_ap_pr.Text;
            agrega.a_materno_pr = txt_am_pr.Text;
            agrega.nombre_pr = txt_nom_pr.Text;
            agrega.nombre_completo_pr = txt_ap_pr.Text + txt_am_pr.Text + txt_nom_pr.Text;
            DateTime fecha_pr = Convert.ToDateTime(txt_f_nac_pr.SelectedDate);
            agrega.f_nacimiento_pr = fecha_pr.ToString("yyyy/MM/dd");
            agrega.e_nacimiento_pr = txt_enac_pr.Text;
            agrega.nacionalidad_pr = txt_nac_pr.Text;
            agrega.genero_pr = Convert.ToChar(cmb_gen_pr.SelectedValue);
            agrega.estado_civil_pr = Convert.ToString(cmb_ec_pr.SelectedValue);
            agrega.no_credencial_ife_pr = txt_no_cre_pr.Text;
        string curp2  = txt_curp_pr.Text;
        curp2 = curp2.ToUpper();
        agrega.curp_pr = curp2;
        string rfc2 = txt_rfc_pr.Text;
        rfc2 = rfc2.ToUpper();
            agrega.rfc_pr = rfc2;
            agrega.nivel_pr = Convert.ToString(cmb_ne_pr.SelectedValue);
            agrega.rol_propietario_real = Convert.ToString(cmb_rol_pr.SelectedValue);
            try { agrega.no_hijos_pr = Convert.ToInt32(txt_nhijos_pr.Text); }
            catch (Exception) { agrega.no_hijos_pr = 0; }
            try { agrega.dep_economicos_pr = Convert.ToInt32(txt_ndep_pr.Text); }
            catch (Exception) { agrega.dep_economicos_pr = 0; }
            agrega.ocupacion_pr = txt_ocu_pr.Text;
            try { agrega.tel_fijo_pr = Convert.ToDecimal(txt_tel_fijo_pr.Text); }
            catch (Exception) { agrega.tel_fijo_pr = 0; }
            try { agrega.tel_cel_pr = Convert.ToDecimal(txt_tel_cel_pr.Text); }
            catch (Exception) { agrega.tel_cel_pr = 0;  }
            agrega.correo_pr = txt_correo_pr.Text;
            agrega.calle_pr = txt_calle_pr.Text;
            agrega.no_exterior_pr = txt_nex_pr.Text;
            agrega.no_interior_pr = txt_nin_pr.Text;
            agrega.colonia_pr = Convert.ToString(cmb_colonia_pr.SelectedItem);
        try { agrega.cp_pr = Convert.ToInt32(txt_cp_pr.Text); }
            catch (Exception) { agrega.cp_pr = 0; }
            agrega.del_mun_pr = txt_del_pr.Text;
            agrega.estado_pr = txt_estado_pr.Text;
            agrega.tiempo_residencia_pr = Convert.ToString(txt_tre_pr.Text);
            try { agrega.no_habitantes_pr = Convert.ToInt32(txt_nhab_pr.Text); }
            catch (Exception) { agrega.no_habitantes_pr = 0; }
            agrega.a_paterno_proveedor = txt_apat_prove.Text;
            agrega.a_materno_proveedor = txt_amat_provee.Text;
            agrega.nombre_proveedor = txt_nom_provee.Text;
            agrega.nombre_completo_proveedor = txt_apat_prove.Text + txt_amat_provee.Text + txt_nom_provee.Text;
            DateTime fecha_prove = Convert.ToDateTime(txt_fnac_provee.SelectedDate);
            agrega.f_nacimiento_proveedor = fecha_prove.ToString("yyyy/MM/dd");
            agrega.e_nacimiento_proveedor = txt_enac_provee.Text;
            agrega.nacionalidad_proveedor = txt_nac_provee.Text;
            agrega.genero_proveedor = Convert.ToChar(cmb_genero_provee.SelectedValue);
            agrega.estado_civil_proveedor = Convert.ToString(cmb_ec_provee.SelectedValue);
            agrega.no_credencial_ife_proveedor = txt_ncre_provee.Text;
        string curp3 = txt_curp_provee.Text;
        curp3 = curp3.ToUpper();
            agrega.curp_proveedor = curp3;
        string rfc3 = txt_rfc_provee.Text;
        rfc3 = rfc3.ToUpper();
            agrega.rfc_proveedor = rfc3;
            agrega.nivel_proveedor = Convert.ToString(cmb_ne_provee.SelectedValue);
            agrega.rol_proveedor = Convert.ToString(cmb_rol_provee.SelectedValue);
            try { agrega.no_hijos_proveedor = Convert.ToInt32(txt_nohijos_provee.Text); }
            catch (Exception) { agrega.no_hijos_proveedor = 0; }
            try { agrega.dep_economicos_proveedor = Convert.ToInt32(txt_ndep_pr.Text); }
            catch (Exception) { agrega.dep_economicos_proveedor = 0; }
            agrega.ocupacion_proveedor = txt_ocu_prove.Text;
            try { agrega.tel_fijo_proveedor = Convert.ToDecimal(txt_tel_fijo_provee.Text); }
            catch (Exception) { agrega.tel_fijo_proveedor = 0; }
            try { agrega.tel_cel_proveedor = Convert.ToDecimal(txt_tel_cel_prove.Text); }
            catch (Exception) { agrega.tel_cel_proveedor = 0; }
            agrega.correo_proveedor = txt_correo_prove.Text;
            agrega.calle_proveedor = txt_calle_provee.Text;
            agrega.no_exterior_proveedor = txt_noext_provee.Text;
            agrega.no_interior_proveedor = txt_noint_provee.Text;
            agrega.colonia_proveedor = Convert.ToString(cmb_col_prove.SelectedItem);
        try { agrega.cp_proveedor = Convert.ToInt32(txt_cp_prove.Text); }
            catch (Exception) { agrega.cp_proveedor = 0; }
            agrega.del_mun_proveedor = txt_del_provee.Text;
            agrega.estado_proveedor = txt_estado_provee.Text;
            agrega.tiempo_residencia_proveedor = Convert.ToString(txt_tiempores_provee.Text);
            try { agrega.no_habitantes_proveedor = Convert.ToInt32(txt_nohab_provee.Text); }
            catch (Exception) { agrega.no_habitantes_proveedor = 0; }
        agrega.nombre_completo_ref2 = txt_nombrecompleto_ref2.Text;
        try { agrega.telefono_fijo_ref2 = Convert.ToInt32(txt_tel_fijo_ref2.Text); }
        catch (Exception){ agrega.telefono_fijo_ref2 = 0; }
        try { agrega.telefono_fijo_ref2 = Convert.ToInt32(txt_tel_cel_ref2.Text); }
        catch (Exception){ agrega.telefono_celular_ref2 = 0; }
        agrega.parentesco_relacion_ref2 = txt_parentesco_ref2.Text;
        agrega.tiempo_conocerlo_ref2 = txt_tiempo_conocerlo_ref2.Text;
        agrega.tipoDomicilio = txtTipo.Text;
        agrega.aPaternoBene = txtAPatBene.Text;
        agrega.aMaternoBene = txtAMatBene.Text;
        agrega.nombreBene = txtNombreBene.Text;
        agrega.domicilioBene = txtDomComBene.Text;
        agrega.NextBene = txtnumExtBene.Text;
        agrega.NintBene = txtintExtBene.Text;
        agrega.colocalBene = txtColLoBene.Text;
        try { agrega.telefonoBene = Convert.ToInt32(txtTelBene.Text); }
        catch (Exception) { agrega.telefonoBene = 0; }
        agrega.RazonSocial_provee = txtDEmoRazProvee.Text;
        agrega.FirmaElectronica_provee = txtFirmaElec.Text;
        agrega.RazonSocial_pm = txtDenoPM.Text;
        agrega.nacionalidad_pm = txtNacionalidadpm.Text;
        agrega.objetoSocial_pm = txtObjetoPM.Text;
        agrega.capitalSocial_pm = txtCapitalPM.Text;
        agrega.domicilio_pm = txtDomicilioPM.Text;
        agrega.noext_pm = txtNexteriorpm.Text;
        agrega.noint_pm = txtninteriorpm.Text;
        agrega.colonialoca_pm = Convert.ToString(ddlPM.SelectedItem);
        agrega.cp_pm = txtCpPM.Text;
        agrega.del_mun_pm = txtDelMunPM.Text;
        agrega.estado_pm = txtEstadoPM.Text;
        agrega.accionista1 = txtaccionista1.Text;
        agrega.accionista2 = txtaccionista2.Text;
        agrega.correoref1 = txt_correo_ref.Text;
        agrega.correoref2 = txtCorreo_ref2.Text;

        if (lblTitulo.Text == "Nueva Cédula")
            {
                agrega.agregarFicha();
                if (Convert.ToBoolean(agrega.retorno[1]) == false)
                {
                    lblErrorAgrega.Visible = true;
                    lblErrorAgrega.Text = "Error al agregar la Cédula:" + agrega.retorno[1].ToString();
               
                }
                else
                {
                    RadGrid1.DataBind();
                    borrarCampos();
                    lblErrorAgrega.Text = "Cédula Agregada Exitosamente";
                    pnlMask.Visible = false;
                    windowAutorizacion.Visible = false;
                }
            }
            else
            {
                agrega.IdEdita = Convert.ToInt32(RadGrid1.SelectedValues["id_ficha"]);
                agrega.id_cliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
            agrega.editaFicha();
                if (Convert.ToBoolean(agrega.retorno[1]) == false)
                {
                    lblErrorAgrega.Visible = true;
                    lblErrorAgrega.Text = "Error al editar la Cédula:" + agrega.retorno[1].ToString();
                }
                else
                {
                    RadGrid1.DataBind();
                    borrarCampos();
                    lblErrorAgrega.Text = "Cédula Editada Exitosamente";
                    pnlMask.Visible = false;
                    windowAutorizacion.Visible = false;
                }
            }
    }

    public void borrarCampos()
    {
        txtFecha_cli.Clear();
    //    txtentidad_cli.Text="";
        txt_cre_cli.Text="";
        txt_curp_cli.Text="";
        txt_rfc_cli.Text="";
        txtn_hijos_cli.Text="";
        txt_eco_cli.Text="";
        txt_tel_cli.Text="";
        txt_cel_cli.Text="";
        txt_correo_cli.Text="";
        txt_calle_cli.Text="";
        txt_n_ext_cli.Text="";
        txt_nint_cli.Text="";
        txt_del_cli.Text="";
        txt_estado_cli.Text="";
        txt_resi_cli.Text="";
        txtn_habi_cli.Text="";
        txt_nom_es.Text="";
        txt_a_pat_es.Text="";
        txt_a_mat_es.Text="";
        txt_ocu_esp.Text="";
        txt_tel_trab_esp.Text="";
        txt_tel_casa_esp.Text="";
        txt_tel_cel_esp.Text="";
        txt_calle_neg.Text="";
        txt_n_exterior_neg.Text="";
        txt_n_int_neg.Text="";
        //txt_col_neg.Text="";
        txt_cp_neg.Text="";
        txt_del_neg.Text="";
        txt_estado_neg.Text="";
        txt_tel_feijo_neg.Text="";
        txt_anti_neg.Text="";
        txt_rz_neg.Text="";
        txt_eper_neg.Text="";
        txt_eeve_neg.Text="";
        //txt_gp_neg.Text="";
        txt_imgp_neg.Text="";
        txt_oa_neg.Text="";
        txt_imoa_neg.Text="";
        txt_nom_ref.Text="";
        txt_tel_fijo_ref.Text="";
        txt_tel_cel_ref.Text="";
        //txt_paren_ref.Text="";
        txt_tiem_ref.Text="";
        txt_carg_ref.Text="";
        txt_dep_ref.Text="";
        txt_tiem_ref.Text="";
        txt_nomFam_ref.Text="";
        txt_parentesco_ref.Text="";
        txt_cargo_des_ref.Text="";
        txt_depen_ref.Text="";
        txt_periodo_ref.Text="";
        txt_ap_pr.Text="";
        txt_am_pr.Text="";
        txt_nom_pr.Text="";
        txt_f_nac_pr.Clear();
        txt_enac_pr.Text="";
        txt_nac_pr.Text="";
        txt_no_cre_pr.Text="";
        txt_curp_pr.Text="";
        txt_rfc_pr.Text="";
        txt_nhijos_pr.Text="";
        txt_ndep_pr.Text="";
        txt_ocu_pr.Text="";
        txt_tel_fijo_pr.Text="";
        txt_tel_cel_pr.Text="";
        txt_correo_pr.Text="";
        txt_calle_pr.Text="";
        txt_nex_pr.Text="";
        txt_nin_pr.Text="";
       // txt_col_pr.Text="";
        txt_cp_pr.Text="";
        txt_del_pr.Text="";
        txt_estado_pr.Text="";
        txt_tre_pr.Text="";
        txt_nhab_pr.Text="";
        txt_apat_prove.Text="";
        txt_amat_provee.Text="";
        txt_nom_provee.Text="";
        txt_fnac_provee.Clear();
        txt_enac_provee.Text="";
        txt_nac_provee.Text="";
        txt_ncre_provee.Text="";
        txt_curp_provee.Text="";
        txt_rfc_provee.Text="";
        txt_nohijos_provee.Text="";
        txt_ndep_pr.Text="";
        txt_ocu_prove.Text="";
        txt_tel_fijo_provee.Text="";
        txt_tel_cel_prove.Text="";
        txt_correo_prove.Text="";
        txt_calle_provee.Text="";
        txt_noext_provee.Text="";
        txt_noint_provee.Text="";
       // txt_col_prove.Text="";
        txt_cp_prove.Text="";
        txt_del_provee.Text="";
        txt_estado_provee.Text="";
        txt_tiempores_provee.Text="";
        txt_nohab_provee.Text="";
        txt_correo_ref.Text = "";
        txtCorreo_ref2.Text = "";
    }
    protected void cmbPersonaSelected(object sender, EventArgs e)
    {
        int id_cliente = Convert.ToInt32(cmb_nombre.SelectedItem.Value);
        int[] sesiones = obtieneSesiones();
        FDat agregar = new FDat();
        agregar.empresa = sesiones[2];
        agregar.sucursal = sesiones[3];
        agregar.id_cliente = id_cliente;
        agregar.obtieneDatosFicha();
        if (Convert.ToBoolean(agregar.retorno[0]))
        {
            DataSet ds = (DataSet)agregar.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                if (r[8].ToString() == "MOR")
                {
                    txt_rfc_cli.Text = Convert.ToString(r[0]);
                    txt_rfc_cli.Text = Convert.ToString(r[0]);
                }
                else {
                    txt_curp_cli.Text = Convert.ToString(r[0]);
                    txt_rfc_cli.Text = Convert.ToString(r[0]);
                }
                txt_calle_cli.Text= Convert.ToString(r[1]);
                txt_n_ext_cli.Text = Convert.ToString(r[2]);
               // txt_col_cli.Text = Convert.ToString(r[3]);
                txt_del_cli.Text = Convert.ToString(r[4]);
                txt_estado_cli.Text = Convert.ToString(r[5]);
                txt_colonia.Visible = true;
                cmbColonia_cli.Visible = false;
                txt_colonia.Text = r[3].ToString();
                txt_cp_cli.Text = Convert.ToString(r[6]);
                txt_tel_cli.Text = Convert.ToString(r[7]); 

            }

        }

    }

    protected void lnkCerrar_Click(object sender, EventArgs e)
    {
        pnlMask.Visible = false;
        windowAutorizacion.Visible = false;
        borrarCampos();
    }

    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Sec_Fotos.Visible = true;
        lnkAbreEdicion.Visible = true;
        lnkImprimir.Visible = true;

    }

    protected void RadGrid2_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblError2.Text = "";
    }

    protected void cmb_preg1_refIndexChanged(object sender, EventArgs e)
    {
        if (cmb_preg1_ref.Text == "SD")
        {
            txt_carg_ref.Text = "N/A"; txt_carg_ref.ReadOnly = true;
            txt_dep_ref.Text = "N/A"; txt_dep_ref.ReadOnly = true;
            txt_per_ref.Text = "N/A"; txt_per_ref.ReadOnly = true;
        }
        else 
        {
            txt_carg_ref.Text = ""; txt_carg_ref.ReadOnly = false;
            txt_dep_ref.Text = ""; txt_dep_ref.ReadOnly = false;
            txt_per_ref.Text = ""; txt_per_ref.ReadOnly = false;
        }
    }

    protected void cmb_estadoCivilIndexChanged(object sender, EventArgs e)
    {
        if (cmb_ec_cli.SelectedValue == "CAS")
        {
            txt_nom_es.Text = "";txt_nom_es.ReadOnly = false;
            txt_a_pat_es.Text = ""; txt_a_pat_es.ReadOnly = false;
            txt_a_mat_es.Text = ""; txt_a_mat_es.ReadOnly = false;
            txt_ocu_esp.Text = ""; txt_ocu_esp.ReadOnly = false;
            txt_tel_trab_esp.Text = ""; txt_tel_trab_esp.ReadOnly = false;
            txt_tel_casa_esp.Text = ""; txt_tel_casa_esp.ReadOnly = false;
            txt_tel_cel_esp.Text = ""; txt_tel_cel_esp.ReadOnly = false;


        }
        else
        {
            txt_nom_es.Text = "N/A"; txt_nom_es.ReadOnly = true;
            txt_a_pat_es.Text = "N/A"; txt_a_pat_es.ReadOnly = true;
            txt_a_mat_es.Text = "N/A"; txt_a_mat_es.ReadOnly = true;
            txt_ocu_esp.Text = "N/A"; txt_ocu_esp.ReadOnly = true;
            txt_tel_trab_esp.Text = "0"; txt_tel_trab_esp.ReadOnly = true;
            txt_tel_casa_esp.Text = "0"; txt_tel_casa_esp.ReadOnly = true;
            txt_tel_cel_esp.Text = "0"; txt_tel_cel_esp.ReadOnly = true;
        }

    }


    protected void lnkImprimir_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;

        // Tipo de Font que vamos utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();


        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" Celula del Cliente ");
        documento.AddCreator("DESARROLLARTE");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\CelulaCliente_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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


            // Creamos la imagen y le ajustamos el tamaño
            string imagepath = HttpContext.Current.Server.MapPath("img/");
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagepath + "logo_aser.png");
            logo.WidthPercentage = 15f;


            //encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.SetWidths(new float[] { 2f, 8f });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;


            PdfPCell titulo = new PdfPCell(new Phrase("APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + "CÉDULA DEL CLIENTE", FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD)));
            //PdfPCell titulo = new PdfPCell(new Phrase("Gaarve S.A de C.V. " + Environment.NewLine + Environment.NewLine + " Informe de Nomina " + Environment.NewLine + Environment.NewLine + strDatosObra, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(logo);
            tablaEncabezado.AddCell(titulo);

            documento.Add(tablaEncabezado);
            
            //documento.Add(new Paragraph(""));

            //DATOS GENERALES
            PdfPTable datGen = new PdfPTable(1);
            datGen.WidthPercentage = 100f;

            PdfPCell datG = new PdfPCell(new Phrase("DATOS GENERALES", fuente8));
            datG.BackgroundColor = BaseColor.LIGHT_GRAY;
            datG.HorizontalAlignment = Element.ALIGN_CENTER;
            datGen.AddCell(datG);
            documento.Add(datGen);

            //datos

            FDat infor = new FDat();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];            
            int idcons = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
            int id_fichaD = Convert.ToInt32(RadGrid1.SelectedValues["id_ficha"]);
            infor.id_cliente = idcons;
            infor.id_ficha = id_fichaD;            
            infor.optieneimpresion();

            

            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    infor.id_cliente = Convert.ToInt32(r[2]);
                    infor.optieneimpresion2();
                    DataSet dn = (DataSet)infor.retorno[1];
                    string nombree , apellidop, apellidom = "";
                    foreach (DataRow rn in dn.Tables[0].Rows)
                    {
                        
                        nombree = Convert.ToString( rn[0]);
                        apellidop = Convert.ToString(rn[1]);
                        apellidom = Convert.ToString(rn[2]);

                        PdfPTable nombre = new PdfPTable(3);
                        nombre.WidthPercentage = 100f;
                        int[] nombrecellwidth = { 30, 30, 40 };
                        nombre.SetWidths(nombrecellwidth);

                        string nom = nombree;

                        PdfPCell apeP = new PdfPCell(new Phrase(" "+apellidop.ToUpper(), fuente8));
                        apeP.HorizontalAlignment = Element.ALIGN_CENTER;
                        nombre.AddCell(apeP);

                        PdfPCell apeM = new PdfPCell(new Phrase(" "+apellidom.ToUpper(), fuente8));
                        apeM.HorizontalAlignment = Element.ALIGN_CENTER;
                        nombre.AddCell(apeM);

                        PdfPCell name = new PdfPCell(new Phrase(" "+nombree.ToUpper(), fuente8));
                        name.HorizontalAlignment = Element.ALIGN_CENTER;
                        nombre.AddCell(name);

                        PdfPCell apeP1 = new PdfPCell(new Phrase("APELLIDO PATERNO", fuente2));
                        apeP1.HorizontalAlignment = Element.ALIGN_CENTER;
                        nombre.AddCell(apeP1);

                        PdfPCell apeM1 = new PdfPCell(new Phrase("APELLIDO MATERNO", fuente2));
                        apeM1.HorizontalAlignment = Element.ALIGN_CENTER;
                        nombre.AddCell(apeM1);

                        PdfPCell name1 = new PdfPCell(new Phrase("NOMBRE (S)", fuente2));
                        name1.HorizontalAlignment = Element.ALIGN_CENTER;
                        nombre.AddCell(name1);
                        documento.Add(nombre);
                         
                    }
                    
                    

                    //fecha de nacimiento
                    PdfPTable fech = new PdfPTable(10);
                    fech.WidthPercentage = 100f;
                    int[] fechcellwidth = { 15, 10, 15, 5, 5, 10, 10, 10, 10, 10 };
                    fech.SetWidths(fechcellwidth);

                    string fechanacim = r[4].ToString();
                    string entidad = r[5].ToString();
                    
                    
                    //SEXO
                    string H = r[6].ToString();
                    string M = r[6].ToString();

                    iTextSharp.text.Font f = new iTextSharp.text.Font();
                    iTextSharp.text.Font f2 = new iTextSharp.text.Font();

                    if (H == "H")
                    {
                        H = "X";
                        f= new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { H = " ";
                        f = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }
                    
                    if (M == "M")
                    {
                        M = "X";
                        f2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else
                    {
                        M = " ";
                        f2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    //ESTADO CIVIL
                    string SOL = r[7].ToString();
                    string CAS = r[7].ToString();
                    string VIU = r[7].ToString();
                    string DIV = r[7].ToString();
                    string UL = r[7].ToString();

                    iTextSharp.text.Font j = new iTextSharp.text.Font();
                    iTextSharp.text.Font j2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font j3= new iTextSharp.text.Font();
                    iTextSharp.text.Font j4 = new iTextSharp.text.Font();
                    iTextSharp.text.Font j5 = new iTextSharp.text.Font();

                    if (SOL == "SOL")
                    {
                        SOL = "X";
                        j = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { SOL = " ";
                        j = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (CAS == "CAS")
                    {
                        CAS = "X";
                        j2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { CAS = " ";
                        j2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (VIU == "VIU")
                    {
                        VIU = "X";
                        j3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK); 
                    }
                    else { VIU = " ";
                        j3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (DIV == "DIV")
                    {
                        DIV = "X";
                        j4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { DIV = " ";
                        j4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (UL == "UL")
                    {
                        UL = "X";
                        j5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { UL = " ";
                        j5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }


                    PdfPCell fechN = new PdfPCell(new Phrase("FECHA DE NACIMIENTO (dd/mm/aa)", fuente2));
                    fechN.HorizontalAlignment = Element.ALIGN_CENTER;
                    fechN.BackgroundColor = BaseColor.LIGHT_GRAY;
                    fech.AddCell(fechN);

                    PdfPCell enti = new PdfPCell(new Phrase("ENTIDAD DE NACIMIENTO", fuente2));
                    enti.HorizontalAlignment = Element.ALIGN_CENTER;
                    enti.BackgroundColor = BaseColor.LIGHT_GRAY;
                    fech.AddCell(enti);

                    PdfPCell nacio = new PdfPCell(new Phrase("NACIONALIDAD", fuente2));
                    nacio.HorizontalAlignment = Element.ALIGN_CENTER;
                    nacio.BackgroundColor = BaseColor.LIGHT_GRAY;
                    fech.AddCell(nacio);

                    PdfPCell sexo = (new PdfPCell(new Phrase("SEXO (Marque con una X)", fuente2)) { Colspan = 2 });
                    sexo.HorizontalAlignment = Element.ALIGN_CENTER;
                    sexo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    fech.AddCell(sexo);

                    PdfPCell edoC = (new PdfPCell(new Phrase("ESTADO CIVIL (Marque con una X)", fuente2)) { Colspan = 5 });
                    edoC.HorizontalAlignment = Element.ALIGN_CENTER;
                    edoC.BackgroundColor = BaseColor.LIGHT_GRAY;
                    fech.AddCell(edoC);

                    PdfPCell fechN1 = new PdfPCell(new Phrase(" " + Convert.ToDateTime(fechanacim).ToString("dd/MM/yyyy"), fuente8));
                    fechN1.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(fechN1);

                    PdfPCell enti1 = new PdfPCell(new Phrase(" " + entidad.ToUpper(), fuente8));
                    enti1.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(enti1);

                    PdfPCell nacio1 = new PdfPCell(new Phrase(" " , fuente8));
                    nacio1.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(nacio1);

                    PdfPCell sexoF = new PdfPCell(new Phrase("M  \n " + H, f));
                    sexoF.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(sexoF);

                    PdfPCell sexoM = new PdfPCell(new Phrase("F  \n " + M, f2));
                    sexoM.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(sexoM);

                    PdfPCell edoCS = new PdfPCell(new Phrase("SOLTERO(A)  \n " + SOL, j));
                    edoCS.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(edoCS);

                    PdfPCell edoCC = new PdfPCell(new Phrase("CASADO(A)  \n " + CAS, j2));
                    edoCC.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(edoCC);

                    PdfPCell edoCV = new PdfPCell(new Phrase("VIUDO(A)  \n " + VIU, j3));
                    edoCV.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(edoCV);

                    PdfPCell edoCD = new PdfPCell(new Phrase("DIVORCIADO(A)  \n" + DIV, j4));
                    edoCD.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(edoCD);

                    PdfPCell edoCU = new PdfPCell(new Phrase("UNION LIBRE  \n" + UL, j5));
                    edoCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    fech.AddCell(edoCU);
                    documento.Add(fech);

                    //TABLA DE CREDENCIALES
                    PdfPTable cred = new PdfPTable(6);
                    cred.WidthPercentage = 100f;
                    int[] credcellwidth = { 15, 25, 12, 25, 5, 18 };
                    cred.SetWidths(credcellwidth);
                    string nocred = r[8].ToString();
                    string curpc = r[9].ToString();
                    string rfcc = r[10].ToString();


                    PdfPCell ine = new PdfPCell(new Phrase("No. CRED. IFE O INE", fuente2));
                    ine.HorizontalAlignment = Element.ALIGN_CENTER;
                    ine.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cred.AddCell(ine);

                    PdfPCell ine1 = new PdfPCell(new Phrase(" " + nocred, fuente8));
                    ine1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cred.AddCell(ine1);

                    PdfPCell curp = new PdfPCell(new Phrase("CURP", fuente2));
                    curp.HorizontalAlignment = Element.ALIGN_CENTER;
                    curp.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cred.AddCell(curp);

                    PdfPCell curp1 = new PdfPCell(new Phrase(" " + curpc, fuente8));
                    curp1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cred.AddCell(curp1);

                    PdfPCell rfc = new PdfPCell(new Phrase("RFC", fuente2));
                    rfc.HorizontalAlignment = Element.ALIGN_CENTER;
                    rfc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cred.AddCell(rfc);

                    PdfPCell rfc1 = new PdfPCell(new Phrase(" " + rfcc, fuente8));
                    rfc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cred.AddCell(rfc1);
                    documento.Add(cred);

                    //NIVEL DE ESCOLARIDAD
                    PdfPTable school = new PdfPTable(10);
                    school.WidthPercentage = 100f;
                    int[] schoolcellwidth = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
                    school.SetWidths(schoolcellwidth);

                    string escolaridad = r[11].ToString();
                    string escolaridad2 = r[11].ToString();
                    string escolaridad3 = r[11].ToString();
                    string escolaridad4 = r[11].ToString();
                    string escolaridad5 = r[11].ToString();
                    string escolaridad6 = r[11].ToString();

                    iTextSharp.text.Font es = new iTextSharp.text.Font();
                    iTextSharp.text.Font es2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font es3= new iTextSharp.text.Font();
                    iTextSharp.text.Font es4 = new iTextSharp.text.Font();
                    iTextSharp.text.Font es5 = new iTextSharp.text.Font();
                    iTextSharp.text.Font es6 = new iTextSharp.text.Font();

                    if (escolaridad == "SIN")
                    {
                        escolaridad = "X";
                        es = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { escolaridad = " ";
                        es = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad2 == "PRI")
                    {
                        escolaridad2 = "X";
                        es2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { escolaridad2 = " ";
                        es2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad3 == "SEC")
                    {
                        escolaridad3 = "X";
                        es3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { escolaridad3 = " ";
                        es3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }

                    if (escolaridad4 == "BAC")
                    {
                        escolaridad4 = "X";
                        es4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { escolaridad4 = " ";
                        es4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }

                    if (escolaridad5 == "LIC")
                    {
                        escolaridad5 = "X";
                        es5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { escolaridad5 = " ";
                        es5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }

                    if (escolaridad6 == "POS")
                    {
                        escolaridad6 = "X";
                            es6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK); 
                    }
                    else { escolaridad6 = " ";
                        es6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }


                    string Jefa = r[12].ToString();
                    string Pareja = r[12].ToString();
                    string Hijo = r[12].ToString();

                    iTextSharp.text.Font rol1 = new iTextSharp.text.Font();
                    iTextSharp.text.Font rol2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font rol3 = new iTextSharp.text.Font();

                    if (Jefa == "Jefa(e)")
                    {
                        Jefa = "X";
                        rol1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { Jefa = " ";
                        rol1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }

                    if (Pareja == "Pareja")
                    {
                        Pareja = "X";
                        rol2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { Pareja = " ";
                        rol2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }

                    if (Hijo == "Hijo(a)")
                    {
                        Hijo = "X";
                        rol3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { Hijo = " ";
                        rol3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }


                    PdfPCell escol = (new PdfPCell(new Phrase("NIVEL DE ESCOLARIDAD (Marque con una X) ", fuente2)) { Colspan = 6 });
                    escol.HorizontalAlignment = Element.ALIGN_CENTER;
                    escol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    school.AddCell(escol);

                    PdfPCell ocupa = (new PdfPCell(new Phrase("OCUPACIÓN", fuente2)));
                    ocupa.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocupa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    school.AddCell(ocupa);

                    PdfPCell rol = (new PdfPCell(new Phrase("ROL DEL CLIENTE EN EL HOGAR (Marque con una X)", fuente2)) { Colspan = 6 });
                    rol.HorizontalAlignment = Element.ALIGN_CENTER;
                    rol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    school.AddCell(rol);

                    PdfPCell sinIn = new PdfPCell(new Phrase("SIN INSTRUCCIÓN  \n" + escolaridad, es));
                    sinIn.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(sinIn);

                    PdfPCell prim = new PdfPCell(new Phrase("PRMARIA  \n" + escolaridad2, es2));
                    prim.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(prim);

                    PdfPCell secu = new PdfPCell(new Phrase("SECUANDARIA  \n" + escolaridad3, es3));
                    secu.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(secu);

                    PdfPCell bach = new PdfPCell(new Phrase("BACHILLERATO  \n" + escolaridad4, es4));
                    bach.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(bach);

                    PdfPCell lic = new PdfPCell(new Phrase("LICENCIATURA  \n" + escolaridad5, es5));
                    lic.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(lic);

                    PdfPCell pos = new PdfPCell(new Phrase("POSGRADO  \n" + escolaridad6, es6));
                    pos.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(pos);

                    PdfPCell ocupa1 = new PdfPCell(new Phrase(" " , es6));
                    ocupa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(ocupa1);

                    PdfPCell jef = new PdfPCell(new Phrase("JEFA  \n" + Jefa, rol1));
                    jef.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(jef);

                    PdfPCell pare = new PdfPCell(new Phrase("PAREJA  \n" + Pareja, rol2));
                    pare.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(pare);

                    PdfPCell hijo = new PdfPCell(new Phrase("HIJO(A)  \n" + Hijo, rol3));
                    hijo.HorizontalAlignment = Element.ALIGN_CENTER;
                    school.AddCell(hijo);
                    documento.Add(school);

                    //taba economica
                    PdfPTable economia = new PdfPTable(5);
                    economia.WidthPercentage = 100f;
                    int[] economiacellwidth = { 15, 15, 25, 30, 15 };
                    economia.SetWidths(economiacellwidth);
                    string hijos = r[13].ToString();
                    string depecon = r[14].ToString();
                    string telfijo = r[15].ToString();
                    string telcel = r[16].ToString();
                    string email = r[17].ToString();

                    PdfPCell noHi = new PdfPCell(new Phrase("No. DE HIJOS", fuente2));
                    noHi.HorizontalAlignment = Element.ALIGN_CENTER;
                    noHi.BackgroundColor = BaseColor.LIGHT_GRAY;
                    economia.AddCell(noHi);

                    PdfPCell depEc = new PdfPCell(new Phrase("DEP. ECONÓMICOS", fuente2));
                    depEc.HorizontalAlignment = Element.ALIGN_CENTER;
                    depEc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    economia.AddCell(depEc);

                    PdfPCell telFi = new PdfPCell(new Phrase("TELÉFONO FIJO (Incluir clave LADA)", fuente2));
                    telFi.HorizontalAlignment = Element.ALIGN_CENTER;
                    telFi.BackgroundColor = BaseColor.LIGHT_GRAY;
                    economia.AddCell(telFi);

                    PdfPCell telCel = new PdfPCell(new Phrase("TELÉFONO CELULAR", fuente2));
                    telCel.HorizontalAlignment = Element.ALIGN_CENTER;
                    telCel.BackgroundColor = BaseColor.LIGHT_GRAY;
                    economia.AddCell(telCel);

                    PdfPCell correo = new PdfPCell(new Phrase("CORREO ELECTRÓNICO", fuente2));
                    correo.HorizontalAlignment = Element.ALIGN_CENTER;
                    correo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    economia.AddCell(correo);

                    PdfPCell noHi1 = new PdfPCell(new Phrase(" " + hijos, fuente8));
                    noHi1.HorizontalAlignment = Element.ALIGN_CENTER;
                    economia.AddCell(noHi1);

                    PdfPCell depEc1 = new PdfPCell(new Phrase(" " + depecon, fuente8));
                    depEc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    economia.AddCell(depEc1);

                    PdfPCell telFi1 = new PdfPCell(new Phrase(" " + telfijo, fuente8));
                    telFi1.HorizontalAlignment = Element.ALIGN_CENTER;
                    economia.AddCell(telFi1);

                    PdfPCell telCel1 = new PdfPCell(new Phrase(" " + telcel, fuente8));
                    telCel1.HorizontalAlignment = Element.ALIGN_CENTER;
                    economia.AddCell(telCel1);

                    PdfPCell correo1 = new PdfPCell(new Phrase(" " + email, fuente8));
                    correo1.HorizontalAlignment = Element.ALIGN_CENTER;
                    economia.AddCell(correo1);
                    documento.Add(economia);

                    //

                    //domicilio
                    PdfPTable domic = new PdfPTable(1);
                    domic.WidthPercentage = 100f;
                    int[] domiccellwidth = { 100 };
                    domic.SetWidths(domiccellwidth);

                    PdfPCell domicilio = new PdfPCell(new Phrase("DOMICILIO", fuente8));
                    domicilio.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicilio.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domic.AddCell(domicilio);
                    documento.Add(domic);

                    //domicilio2.1
                    PdfPTable domici = new PdfPTable(4);
                    domici.WidthPercentage = 100f;
                    int[] domicicellwidth = { 45, 10, 10, 35 };
                    domici.SetWidths(domicicellwidth);
                    string callecl = r[18].ToString();
                    string numext = r[19].ToString();
                    string numint = r[20].ToString();
                    string colonialocal = r[21].ToString();


                    PdfPCell calle = new PdfPCell(new Phrase("CALLE, AVENIDA, ANDADOR, CERRADA, CALLEJO, ETC", fuente2));
                    calle.HorizontalAlignment = Element.ALIGN_CENTER;
                    calle.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domici.AddCell(calle);

                    PdfPCell ext = new PdfPCell(new Phrase("No. EXTERIOR", fuente2));
                    ext.HorizontalAlignment = Element.ALIGN_CENTER;
                    ext.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domici.AddCell(ext);

                    PdfPCell inte = new PdfPCell(new Phrase("No. INTERIOR", fuente2));
                    inte.HorizontalAlignment = Element.ALIGN_CENTER;
                    inte.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domici.AddCell(inte);

                    PdfPCell colo = new PdfPCell(new Phrase("COLONIA O LOCALIDAD", fuente2));
                    colo.HorizontalAlignment = Element.ALIGN_CENTER;
                    colo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domici.AddCell(colo);

                    PdfPCell calle1 = new PdfPCell(new Phrase(" " + callecl.ToUpper(), fuente8));
                    calle1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domici.AddCell(calle1);

                    PdfPCell ext1 = new PdfPCell(new Phrase(" " + numext, fuente8));
                    ext1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domici.AddCell(ext1);

                    PdfPCell inte1 = new PdfPCell(new Phrase(" " + numint, fuente8));
                    inte1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domici.AddCell(inte1);

                    PdfPCell colo1 = new PdfPCell(new Phrase(" " + colonialocal.ToUpper(), fuente8));
                    colo1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domici.AddCell(colo1);

                    documento.Add(domici);

                    //código postal
                    PdfPTable codP = new PdfPTable(6);
                    codP.WidthPercentage = 100f;
                    int[] codPcellwidth = { 10, 25, 20,15, 15, 15 };
                    codP.SetWidths(codPcellwidth);

                    string postal = r[22].ToString();
                    string delmuni = r[23].ToString();
                    string estadoc = r[24].ToString();
                    string time = r[25].ToString();
                    string numhabita = r[26].ToString();


                    PdfPCell cp = new PdfPCell(new Phrase("CÓDIGO POSTAL", fuente2));
                    cp.HorizontalAlignment = Element.ALIGN_CENTER;
                    cp.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codP.AddCell(cp);

                    PdfPCell delg = new PdfPCell(new Phrase("DELEGACIÓN O MUNICIPIO", fuente2));
                    delg.HorizontalAlignment = Element.ALIGN_CENTER;
                    delg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codP.AddCell(delg);

                    PdfPCell esta = new PdfPCell(new Phrase("ESTADO", fuente2));
                    esta.HorizontalAlignment = Element.ALIGN_CENTER;
                    esta.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codP.AddCell(esta);

                    PdfPCell tipoR = new PdfPCell(new Phrase("TIPO DE DOMICILIO", fuente2));
                    tipoR.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipoR.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codP.AddCell(tipoR);

                    PdfPCell timeR = new PdfPCell(new Phrase("TIEMPO DE RESIDENCIA", fuente2));
                    timeR.HorizontalAlignment = Element.ALIGN_CENTER;
                    timeR.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codP.AddCell(timeR);

                    PdfPCell noHA = new PdfPCell(new Phrase("PERSONAS QUE HABITAN EN EL DOMICILIO", fuente2));
                    noHA.HorizontalAlignment = Element.ALIGN_CENTER;
                    noHA.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codP.AddCell(noHA);

                    PdfPCell cp1 = new PdfPCell(new Phrase(" " + postal, fuente8));
                    cp1.HorizontalAlignment = Element.ALIGN_CENTER;
                    codP.AddCell(cp1);

                    PdfPCell delg1 = new PdfPCell(new Phrase(" " + delmuni.ToUpper(), fuente8));
                    delg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    codP.AddCell(delg1);

                    PdfPCell esta1 = new PdfPCell(new Phrase(" " + estadoc.ToUpper(), fuente8));
                    esta1.HorizontalAlignment = Element.ALIGN_CENTER;
                    codP.AddCell(esta1);

                    PdfPCell tipoR1 = new PdfPCell(new Phrase(" ", fuente8));
                    tipoR1.HorizontalAlignment = Element.ALIGN_CENTER;
                    codP.AddCell(tipoR1);

                    PdfPCell timeR1 = new PdfPCell(new Phrase(" " + time, fuente8));
                    timeR1.HorizontalAlignment = Element.ALIGN_CENTER;
                    codP.AddCell(timeR1);

                    PdfPCell noHA1 = new PdfPCell(new Phrase(" " + numhabita, fuente8));
                    noHA1.HorizontalAlignment = Element.ALIGN_CENTER;
                    codP.AddCell(noHA1);
                    documento.Add(codP);

                    //condición de propiedad
                    PdfPTable condicionP = new PdfPTable(4);
                    condicionP.WidthPercentage = 100f;
                    int[] condicionPcellwidth = { 25, 25, 25, 25 };
                    condicionP.SetWidths(condicionPcellwidth);

                    string PRO = r[27].ToString();
                    string REN = r[27].ToString();
                    string PRE = r[27].ToString();
                    string FAM = r[27].ToString();

                    iTextSharp.text.Font ps = new iTextSharp.text.Font();
                    iTextSharp.text.Font ps2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font ps3 = new iTextSharp.text.Font();
                    iTextSharp.text.Font ps4 = new iTextSharp.text.Font();
                    


                    if (PRO == "PRO")
                    {
                        PRO = "X";
                        ps = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { PRO = " ";
                        ps = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (REN == "REN")
                    {
                        REN = "X";
                        ps2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { REN = " ";
                        ps2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (PRE == "PRE")
                    {
                        PRE = "X";
                        ps3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { PRE = " ";
                        ps3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (FAM == "FAM")
                    {
                        FAM = "X";
                        ps4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { FAM = " ";
                        ps4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    PdfPCell condPro = (new PdfPCell(new Phrase("CONDICIÓN DE PROPIEDAD DEL INMUEBLE (Marque con una X)", fuente2)) { Colspan = 4 });
                    condPro.HorizontalAlignment = Element.ALIGN_CENTER;
                    condPro.BackgroundColor = BaseColor.LIGHT_GRAY;
                    condicionP.AddCell(condPro);

                    PdfPCell propio = new PdfPCell(new Phrase("PROPIO  \n" + PRO, ps));
                    propio.HorizontalAlignment = Element.ALIGN_CENTER;
                    condicionP.AddCell(propio);

                    PdfPCell rentado = new PdfPCell(new Phrase("RENTADO  \n" + REN, ps2));
                    rentado.HorizontalAlignment = Element.ALIGN_CENTER;
                    condicionP.AddCell(rentado);

                    PdfPCell prestado = new PdfPCell(new Phrase("PRESTADO  \n" + PRE, ps3));
                    prestado.HorizontalAlignment = Element.ALIGN_CENTER;
                    condicionP.AddCell(prestado);

                    PdfPCell famili = new PdfPCell(new Phrase("FAMILIAR  \n" + FAM, ps4));
                    famili.HorizontalAlignment = Element.ALIGN_CENTER;
                    condicionP.AddCell(famili);
                    //documento.Add(condicionP);

                    //datos generales
                    PdfPTable datosGen = new PdfPTable(3);
                    datosGen.WidthPercentage = 100f;
                    int[] datosGencellwidth = { 30, 30, 40 };
                    datosGen.SetWidths(datosGencellwidth);
                    string apellidopc = r[28].ToString();
                    string apellidomc = r[29].ToString();
                    string nombresc = r[30].ToString();
                    

                    PdfPCell datosGeneral = (new PdfPCell(new Phrase("DATOS GENENRALES DEL ESPOSO(A) ó CONCUBINO(A)", fuente2)) { Colspan = 3 });
                    datosGeneral.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosGeneral.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datosGen.AddCell(datosGeneral);

                    PdfPCell apelliME = new PdfPCell(new Phrase(" " + apellidopc.ToUpper(), fuente8));
                    apelliME.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosGen.AddCell(apelliME);

                    PdfPCell apelliPE = new PdfPCell(new Phrase(" " + apellidomc.ToUpper(), fuente8));
                    apelliPE.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosGen.AddCell(apelliPE);

                    PdfPCell nameE = new PdfPCell(new Phrase(" " + nombresc.ToUpper(), fuente8));
                    nameE.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosGen.AddCell(nameE);

                    PdfPCell apelliPE1 = new PdfPCell(new Phrase("APELLIDO PATERNO", fuente2));
                    apelliPE1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosGen.AddCell(apelliPE1);

                    PdfPCell apelliME1 = new PdfPCell(new Phrase("APELLIDO MATERNO", fuente2));
                    apelliME1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosGen.AddCell(apelliME1);

                    PdfPCell nameE1 = new PdfPCell(new Phrase("NOMBRE(S)", fuente2));
                    nameE1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosGen.AddCell(nameE1);
                    documento.Add(datosGen);

                    //ocupación
                    PdfPTable ocup = new PdfPTable(4);
                    ocup.WidthPercentage = 100f;
                    int[] ocupcellwidth = { 40, 20, 20, 20 };
                    ocup.SetWidths(ocupcellwidth);
                    string ocpacionc = r[32].ToString();
                    string teltrac = r[33].ToString();
                    string telcasc = r[34].ToString();
                    string telcelc = r[35].ToString();

                    PdfPCell ocupaci = new PdfPCell(new Phrase("OCUPACIÓN", fuente2));
                    ocupaci.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocupaci.BackgroundColor = BaseColor.LIGHT_GRAY;
                    ocup.AddCell(ocupaci);

                    PdfPCell telTra = new PdfPCell(new Phrase("TELÉFONO TRABAJO", fuente2));
                    telTra.HorizontalAlignment = Element.ALIGN_CENTER;
                    telTra.BackgroundColor = BaseColor.LIGHT_GRAY;
                    ocup.AddCell(telTra);

                    PdfPCell telTra1 = new PdfPCell(new Phrase("TELÉFONO CASA", fuente2));
                    telTra1.HorizontalAlignment = Element.ALIGN_CENTER;
                    telTra1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    ocup.AddCell(telTra1);

                    PdfPCell telTra2 = new PdfPCell(new Phrase("TELÉFONO CELULAR", fuente2));
                    telTra2.HorizontalAlignment = Element.ALIGN_CENTER;
                    telTra2.BackgroundColor = BaseColor.LIGHT_GRAY;
                    ocup.AddCell(telTra2);

                    PdfPCell ocupaci1 = new PdfPCell(new Phrase(" " + ocpacionc.ToUpper(), fuente8));
                    ocupaci1.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocup.AddCell(ocupaci1);

                    PdfPCell telTra11 = new PdfPCell(new Phrase(" " + teltrac, fuente8));
                    telTra11.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocup.AddCell(telTra11);

                    PdfPCell telTra12 = new PdfPCell(new Phrase(" " + telcasc, fuente8));
                    telTra12.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocup.AddCell(telTra12);

                    PdfPCell telTra21 = new PdfPCell(new Phrase(" " + telcelc, fuente8));
                    telTra21.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocup.AddCell(telTra21);
                    documento.Add(ocup);

                    //info neg.
                    PdfPTable info = new PdfPTable(1);
                    info.WidthPercentage = 100f;
                    int[] infocellwidth = { 100 };
                    info.SetWidths(infocellwidth);

                    PdfPCell infonego = new PdfPCell(new Phrase("INFORMACIÓN DEL NEGOCIO", fuente8));
                    infonego.HorizontalAlignment = Element.ALIGN_CENTER;
                    infonego.BackgroundColor = BaseColor.LIGHT_GRAY;
                    info.AddCell(infonego);
                    documento.Add(info);

                    //NEGOCIO
                    PdfPTable domicinEG = new PdfPTable(4);
                    domicinEG.WidthPercentage = 100f;
                    int[] domicinEGcellwidth = { 45, 10, 10, 35 };
                    domicinEG.SetWidths(domicinEGcellwidth);
                    string callenc = r[36].ToString();
                    string numextcl = r[37].ToString();
                    string numintecl = r[38].ToString();
                    string coloniac = r[39].ToString();

                    PdfPCell calleNeg = new PdfPCell(new Phrase("CALLE, AVENIDA, ANDADOR, CERRADA, CALLEJO, ETC", fuente2));
                    calleNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    calleNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicinEG.AddCell(calleNeg);

                    PdfPCell extNeg = new PdfPCell(new Phrase("No. EXTERIOR", fuente2));
                    extNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    extNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicinEG.AddCell(extNeg);

                    PdfPCell inteNeg = new PdfPCell(new Phrase("No. EXTERIOR", fuente2));
                    inteNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    inteNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicinEG.AddCell(inteNeg);

                    PdfPCell coloNeg = new PdfPCell(new Phrase("COLONIA O LOCALIDAD", fuente2));
                    coloNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    coloNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicinEG.AddCell(coloNeg);

                    PdfPCell calle1Neg = new PdfPCell(new Phrase(" " + callenc.ToUpper(), fuente8));
                    calle1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicinEG.AddCell(calle1Neg);

                    PdfPCell ext1Neg = new PdfPCell(new Phrase(" " + numextcl, fuente8));
                    ext1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicinEG.AddCell(ext1Neg);

                    PdfPCell inte1Neg = new PdfPCell(new Phrase(" " + numintecl, fuente8));
                    inte1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicinEG.AddCell(inte1Neg);

                    PdfPCell colo1Neg = new PdfPCell(new Phrase(" " + coloniac.ToUpper(), fuente8));
                    colo1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicinEG.AddCell(colo1Neg);
                    documento.Add(domicinEG);

                    //cod postal nego
                    //código postal
                    PdfPTable codPNeg = new PdfPTable(5);
                    codPNeg.WidthPercentage = 100f;
                    int[] codPNegcellwidth = { 15, 30, 20, 20, 15 };
                    codPNeg.SetWidths(codPNegcellwidth);
                    string codpostal = r[40].ToString();
                    string delegacimuni = r[41].ToString();
                    string estadoclie = r[42].ToString();
                    string telfijocli = r[43].ToString();
                    string antiguedadcli = r[44].ToString();

                    PdfPCell cpNeg = new PdfPCell(new Phrase("CÓDIGO POSTAL", fuente2));
                    cpNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    cpNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codPNeg.AddCell(cpNeg);

                    PdfPCell delgNeg = new PdfPCell(new Phrase("DELEGACIÓN O MUNICIPIO", fuente2));
                    delgNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    delgNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codPNeg.AddCell(delgNeg);

                    PdfPCell estaNeg = new PdfPCell(new Phrase("ESTADO", fuente2));
                    estaNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    estaNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codPNeg.AddCell(estaNeg);

                    PdfPCell timeRNeg = new PdfPCell(new Phrase("TELÉFONO FIJO", fuente2));
                    timeRNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    timeRNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codPNeg.AddCell(timeRNeg);

                    PdfPCell noHANeg = new PdfPCell(new Phrase("ANTIGÜEDAD", fuente2));
                    noHANeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    noHANeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    codPNeg.AddCell(noHANeg);

                    PdfPCell cp1Neg = new PdfPCell(new Phrase(" " + codpostal, fuente8));
                    cp1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    codPNeg.AddCell(cp1Neg);

                    PdfPCell delg1Neg = new PdfPCell(new Phrase(" " + delegacimuni.ToUpper(), fuente8));
                    delg1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    codPNeg.AddCell(delg1Neg);

                    PdfPCell esta1Neg = new PdfPCell(new Phrase(" " + estadoclie.ToUpper(), fuente8));
                    esta1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    codPNeg.AddCell(esta1Neg);

                    PdfPCell timeR1Neg = new PdfPCell(new Phrase(" " + telfijocli, fuente8));
                    timeR1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    codPNeg.AddCell(timeR1Neg);

                    PdfPCell noHA1Neg = new PdfPCell(new Phrase(" " + antiguedadcli, fuente8));
                    noHA1Neg.HorizontalAlignment = Element.ALIGN_CENTER;
                    codPNeg.AddCell(noHA1Neg);
                    documento.Add(codPNeg);

                    //razón social
                    PdfPTable razS = new PdfPTable(8);
                    razS.WidthPercentage = 100f;
                    int[] razScellwidth = { 35, 10, 10, 15, 10, 5, 10, 5 };
                    razS.SetWidths(razScellwidth);
                    string razonsocial = r[45].ToString();

                    string Fijo = r[46].ToString();
                    string Semifijo = r[46].ToString();
                    string Ambulante = r[46].ToString();

                    iTextSharp.text.Font pt = new iTextSharp.text.Font();
                    iTextSharp.text.Font pt2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font pt3 = new iTextSharp.text.Font();
                 


                    if (Fijo == "Fijo")
                    {
                        Fijo = "X";
                        pt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { Fijo = " ";
                        pt = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (Semifijo == "Semifijo")
                    {
                        Semifijo = "X";
                        pt2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { Semifijo = " ";
                        pt2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (Ambulante == "Ambulante")
                    {
                        Ambulante = "X";
                        pt3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { Ambulante = " ";
                        pt3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    string numempleP = r[47].ToString();
                    string numempleE = r[48].ToString();

                    PdfPCell nameRaz = new PdfPCell(new Phrase("NOMBRE O RAZÓN SOCIAL DEL NEGOCIO", fuente2));
                    nameRaz.HorizontalAlignment = Element.ALIGN_CENTER;
                    nameRaz.BackgroundColor = BaseColor.LIGHT_GRAY;
                    razS.AddCell(nameRaz);

                    PdfPCell tipEst = (new PdfPCell(new Phrase("TIPO DE ESTABLECIMIENTO (Marque con una X)", fuente2)) { Colspan = 3 });
                    tipEst.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipEst.BackgroundColor = BaseColor.LIGHT_GRAY;
                    razS.AddCell(tipEst);

                    PdfPCell NoEmp = (new PdfPCell(new Phrase("No. DE EMPLEOS", fuente2)) { Colspan = 4 });
                    NoEmp.HorizontalAlignment = Element.ALIGN_CENTER;
                    NoEmp.BackgroundColor = BaseColor.LIGHT_GRAY;
                    razS.AddCell(NoEmp);

                    PdfPCell nomRazS1 = new PdfPCell(new Phrase(" " + razonsocial.ToUpper(), fuente8));
                    nomRazS1.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(nomRazS1);

                    PdfPCell fijo = new PdfPCell(new Phrase("FIJO  \n" + Fijo, pt));
                    fijo.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(fijo);

                    PdfPCell semi = new PdfPCell(new Phrase("SEMIFIJO  \n" + Semifijo, pt2));
                    semi.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(semi);

                    PdfPCell ambu = new PdfPCell(new Phrase("AMBULANTE  \n" + Ambulante, pt3));
                    ambu.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(ambu);

                    PdfPCell perma = new PdfPCell(new Phrase("PERMANENTE", fuente2));
                    perma.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(perma);

                    PdfPCell perma1 = new PdfPCell(new Phrase(" " + numempleP, fuente8));
                    perma1.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(perma1);

                    PdfPCell even = new PdfPCell(new Phrase("EVENTUALES", fuente2));
                    even.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(even);

                    PdfPCell even1 = new PdfPCell(new Phrase(" " +numempleE, fuente8));
                    even1.HorizontalAlignment = Element.ALIGN_CENTER;
                    razS.AddCell(even1);
                    documento.Add(razS);

                    //grupo principa
                    PdfPTable grupP = new PdfPTable(4);
                    grupP.WidthPercentage = 100f;
                    int[] grupPcellwidth = { 15, 35, 15, 35 };
                    grupP.SetWidths(grupPcellwidth);

                    string giroprinci = r[49].ToString();
                    string ingrem = r[50].ToString();
                    string otraact = r[51].ToString();
                    string ingrems = r[52].ToString();

                    PdfPCell gruPrin = new PdfPCell(new Phrase("GIRO PRINCIPAL", fuente2));
                    gruPrin.HorizontalAlignment = Element.ALIGN_CENTER;
                    gruPrin.BackgroundColor = BaseColor.LIGHT_GRAY;
                    grupP.AddCell(gruPrin);

                    PdfPCell gruPrin1 = new PdfPCell(new Phrase(" " + giroprinci.ToUpper(), fuente8));
                    gruPrin1.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupP.AddCell(gruPrin1);

                    PdfPCell ingM = new PdfPCell(new Phrase("INGRESO MENSUAL", fuente2));
                    ingM.HorizontalAlignment = Element.ALIGN_CENTER;
                    ingM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    grupP.AddCell(ingM);

                    PdfPCell ingM1 = new PdfPCell(new Phrase(" " + ingrem, fuente8));
                    ingM1.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupP.AddCell(ingM1);

                    PdfPCell otrasA = new PdfPCell(new Phrase("OTRAS ACTIVIDADES", fuente2));
                    otrasA.HorizontalAlignment = Element.ALIGN_CENTER;
                    otrasA.BackgroundColor = BaseColor.LIGHT_GRAY;
                    grupP.AddCell(otrasA);

                    PdfPCell otrasA1 = new PdfPCell(new Phrase(" " + otraact.ToUpper(), fuente8));
                    otrasA1.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupP.AddCell(otrasA1);

                    PdfPCell ingMO = new PdfPCell(new Phrase("INGRESO MENSUAL", fuente2));
                    ingMO.HorizontalAlignment = Element.ALIGN_CENTER;
                    ingMO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    grupP.AddCell(ingMO);

                    PdfPCell ingMO1 = new PdfPCell(new Phrase(" " + ingrems, fuente8));
                    ingMO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    grupP.AddCell(ingMO1);
                    documento.Add(grupP);
                    documento.Add(new Paragraph(" "));

                    //titulo del croquis
                    PdfPTable cro = new PdfPTable(2);
                    cro.WidthPercentage = 100f;
                    int[] crocellwidth = { 50, 50 };
                    cro.SetWidths(crocellwidth);

                    PdfPCell croDC = new PdfPCell(new Phrase("CROQUIS DE UBICACIÓN DEL DOMICILIO DEL CLIENTE", fuente2));
                    croDC.HorizontalAlignment = Element.ALIGN_CENTER;
                    croDC.Border = 0;
                    cro.AddCell(croDC);

                    PdfPCell croNC = new PdfPCell(new Phrase("CROQUIS DE UBICACIÓN DEL DOMICILIO DEL CLIENTE", fuente2));
                    croNC.HorizontalAlignment = Element.ALIGN_CENTER;
                    croNC.Border = 0;
                    cro.AddCell(croNC);
                    documento.Add(cro);

                    //imagen de croquis
                    PdfPTable croquisImg = new PdfPTable(1);
                    croquisImg.WidthPercentage = 100f;

                    string imagepath1 = HttpContext.Current.Server.MapPath("img/");
                    iTextSharp.text.Image croquis = iTextSharp.text.Image.GetInstance(imagepath1 + "croquis.png");

                    croquisImg.AddCell(croquis);
                    croquis.Border = 0;
                    documento.Add(croquisImg);

                    //pie de croquis
                    PdfPTable regisName = new PdfPTable(1);
                    regisName.WidthPercentage = 80f;
                    regisName.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell reName = new PdfPCell(new Phrase("REGISTRE EL NOMBRE DE LAS PRINCIPALES CALLES, AVENIDAS, ANDADORES, CERRADAS QUE COLINDAN CON LA VIVIENDA Y EL NEGOCIO DEL CLIENTE", fuente2));
                    reName.HorizontalAlignment = Element.ALIGN_CENTER;
                    reName.Border = 0;
                    regisName.AddCell(reName);
                    documento.Add(regisName);

                    //referencias pincipales
                    PdfPTable ubiRe = new PdfPTable(2);
                    ubiRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    ubiRe.WidthPercentage = 100f;

                    PdfPCell refUbi = (new PdfPCell(new Phrase("PRINCIPALES REFERENCIAS DE UBICACIÓN", fuente2)));
                    refUbi.HorizontalAlignment = Element.ALIGN_CENTER;
                    ubiRe.AddCell(refUbi);

                    PdfPCell refUbi1 = (new PdfPCell(new Phrase("PRINCIPALES REFERENCIAS DE UBICACIÓN", fuente2)));
                    refUbi1.HorizontalAlignment = Element.ALIGN_CENTER;
                    ubiRe.AddCell(refUbi1);

                    PdfPCell refUbi2 = (new PdfPCell(new Phrase(" \n \n \n", fuente6)) { Rowspan = 5 });
                    refUbi2.HorizontalAlignment = Element.ALIGN_CENTER;
                    ubiRe.AddCell(refUbi2);

                    PdfPCell refUbi3 = (new PdfPCell(new Phrase(" \n \n \n ", fuente6)) { Rowspan = 5 });
                    refUbi3.HorizontalAlignment = Element.ALIGN_CENTER;
                    ubiRe.AddCell(refUbi3);
                    documento.Add(ubiRe);

                    

                    


                    //referencias personales (llenado de datos
                    PdfPTable refeCli = new PdfPTable(7);
                    refeCli.WidthPercentage = 100f;
                    int[] refeClicellwidth = { 3, 27, 15, 15, 10, 10, 20 };
                    refeCli.SetWidths(refeClicellwidth);
                    string nombreref = r[53].ToString();
                    string telfijoclrf = r[54].ToString();
                    string telceluref = r[55].ToString();
                    string parentesco = r[56].ToString();
                    string timeconocerl = r[57].ToString();
                    string emailRef1 = r[155].ToString();


                    string nombRef2 = r[126].ToString();
                    string telFijRef2 = r[127].ToString();
                    string celRefe2 = r[128].ToString();
                    string relaRef2 = r[129].ToString();
                    string timeConoRef2 = r[130].ToString();
                    string emailRef2 = r[156].ToString();


                    PdfPCell encaRefe = (new PdfPCell(new Phrase("REFERENCIAS DEL CLIENTE (PERSONALES Y FAMILIARES", fuente8)) { Colspan = 7 });
                    encaRefe.HorizontalAlignment = Element.ALIGN_CENTER;
                    encaRefe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    refeCli.AddCell(encaRefe);


                    PdfPCell NoRefe = new PdfPCell(new Phrase("No.", fuente6));
                    NoRefe.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(NoRefe);

                    PdfPCell NameRef = new PdfPCell(new Phrase("NOMBRE COMPLETO", fuente2));
                    NameRef.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(NameRef);

                    PdfPCell telRef = new PdfPCell(new Phrase("TELÉFONO FIJO (Incluir clave LADA)", fuente2));
                    telRef.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(telRef);

                    PdfPCell celRef = new PdfPCell(new Phrase("TELÉFONO CELULAR", fuente2));
                    celRef.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(celRef);

                    PdfPCell pareRef = new PdfPCell(new Phrase("PARENTESCO O RELACIÓN", fuente2));
                    pareRef.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(pareRef);

                    PdfPCell timeRef = new PdfPCell(new Phrase("TIEMPO DE CONOCERLO", fuente2));
                    timeRef.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(timeRef);

                    PdfPCell correoRef = new PdfPCell(new Phrase("CORREO ELECTRÓNICO", fuente2));
                    correoRef.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(correoRef);

                    PdfPCell NoRefe1 = new PdfPCell(new Phrase("1", fuente6));
                    NoRefe1.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(NoRefe1);

                    PdfPCell NameRef1 = new PdfPCell(new Phrase(" " + nombreref.ToUpper(), fuente8));
                    NameRef1.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(NameRef1);

                    PdfPCell telRef1 = new PdfPCell(new Phrase(" " + telfijoclrf, fuente8));
                    telRef1.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(telRef1);

                    PdfPCell celRef1 = new PdfPCell(new Phrase(" " + telceluref, fuente8));
                    celRef1.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(celRef1);

                    PdfPCell pareRef1 = new PdfPCell(new Phrase(" " + parentesco.ToUpper(), fuente8));
                    pareRef1.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(pareRef1);

                    PdfPCell timeRef1 = new PdfPCell(new Phrase(" " + timeconocerl, fuente8));
                    timeRef1.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(timeRef1);

                    PdfPCell correoRef1 = new PdfPCell(new Phrase(emailRef1, fuente8));
                    correoRef1.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(correoRef1);

                    PdfPCell NoRefe2 = new PdfPCell(new Phrase("2", fuente8));
                    NoRefe2.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(NoRefe2);

                    PdfPCell NameRef2 = new PdfPCell(new Phrase(nombRef2, fuente8));
                    NameRef2.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(NameRef2);

                    PdfPCell telRef2 = new PdfPCell(new Phrase(telFijRef2, fuente8));
                    telRef2.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(telRef2);

                    PdfPCell celRef2 = new PdfPCell(new Phrase(celRefe2, fuente8));
                    celRef2.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(celRef2);

                    PdfPCell pareRef2 = new PdfPCell(new Phrase(relaRef2, fuente8));
                    pareRef2.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(pareRef2);

                    PdfPCell timeRef2 = new PdfPCell(new Phrase(timeConoRef2, fuente8));
                    timeRef2.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(timeRef2);

                    PdfPCell correoRef2 = new PdfPCell(new Phrase(emailRef2, fuente8));
                    correoRef2.HorizontalAlignment = Element.ALIGN_CENTER;
                    refeCli.AddCell(correoRef2);


                    documento.Add(refeCli);

                    PdfPTable benSeg = new PdfPTable(3);
                    benSeg.WidthPercentage = 100f;
                    int[] benSegcellwidth = {30, 30, 40};
                    benSeg.SetWidths(benSegcellwidth);

                    string aPaterbenef = r[132].ToString(); ;
                    string aMaternoBenef = r[133].ToString();
                    string nombreBenef = r[134].ToString();


                    PdfPCell benefSeg = (new PdfPCell(new Phrase("BENEFICIARIO DEL SEGURO", fuente8)) { Colspan = 3 });
                    benefSeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    benefSeg.VerticalAlignment = Element.ALIGN_MIDDLE;
                    benefSeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    benSeg.AddCell(benefSeg);

                    PdfPCell apelliPSeg = new PdfPCell(new Phrase(aPaterbenef, fuente8));
                    apelliPSeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    benSeg.AddCell(apelliPSeg);

                    PdfPCell apelliMSeg = new PdfPCell(new Phrase(aMaternoBenef, fuente8));
                    apelliMSeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    benSeg.AddCell(apelliMSeg);

                    PdfPCell nomSeg = new PdfPCell(new Phrase(nombreBenef, fuente8));
                    nomSeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    benSeg.AddCell(nomSeg);


                    PdfPCell apelliPSeg1 = new PdfPCell(new Phrase("APELLIDO PATERNO", fuente2));
                    apelliPSeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    benSeg.AddCell(apelliPSeg1);

                    PdfPCell apelliMSeg1 = new PdfPCell(new Phrase("APELLIDO MATERNO", fuente2));
                    apelliMSeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    benSeg.AddCell(apelliMSeg1);

                    PdfPCell nomSeg1 = new PdfPCell(new Phrase("NOMBRE(S)", fuente2));
                    nomSeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    benSeg.AddCell(nomSeg1);
                    documento.Add(benSeg);


                    PdfPTable datBen = new PdfPTable(5);
                    datBen.WidthPercentage = 100f;
                    int[] datBencellwidth = { 30, 10, 10, 35, 15 };
                    datBen.SetWidths(datBencellwidth);

                    string calleBenef = r[135].ToString();
                    string noExtBenef = r[136].ToString();
                    string noIntBenef = r[137].ToString();
                    string colBenef = r[138].ToString();
                    string telBenef = r[139].ToString();

                    PdfPCell datosBenf = (new PdfPCell(new Phrase("CALLE, AVENIDA, ANDADOR, CERRADA, CALLEJON, ETC.", fuente2)));
                    datosBenf.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosBenf.VerticalAlignment = Element.ALIGN_MIDDLE;
                    datosBenf.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datBen.AddCell(datosBenf);

                    PdfPCell extBen = (new PdfPCell(new Phrase("No. EXTERIOR", fuente2)));
                    extBen.HorizontalAlignment = Element.ALIGN_CENTER;
                    extBen.VerticalAlignment = Element.ALIGN_MIDDLE;
                    extBen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datBen.AddCell(extBen);

                    PdfPCell intBen = (new PdfPCell(new Phrase("No. INTERIOR", fuente2)));
                    intBen.HorizontalAlignment = Element.ALIGN_CENTER;
                    intBen.VerticalAlignment = Element.ALIGN_MIDDLE;
                    intBen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datBen.AddCell(intBen);

                    PdfPCell colBen = (new PdfPCell(new Phrase("COLONIA O LOCALIDAD", fuente2)));
                    colBen.HorizontalAlignment = Element.ALIGN_CENTER;
                    colBen.VerticalAlignment = Element.ALIGN_MIDDLE;
                    colBen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datBen.AddCell(colBen);

                    PdfPCell telBen = (new PdfPCell(new Phrase("TELÉFONO", fuente2)));
                    telBen.HorizontalAlignment = Element.ALIGN_CENTER;
                    telBen.VerticalAlignment = Element.ALIGN_MIDDLE;
                    telBen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datBen.AddCell(telBen);

                    PdfPCell datosBenf1 = (new PdfPCell(new Phrase(calleBenef, fuente8)));
                    datosBenf1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datosBenf1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    datBen.AddCell(datosBenf1);

                    PdfPCell extBen1 = (new PdfPCell(new Phrase(noExtBenef, fuente8)));
                    extBen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    extBen1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    datBen.AddCell(extBen1);

                    PdfPCell intBen1 = (new PdfPCell(new Phrase(noIntBenef, fuente8)));
                    intBen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    intBen1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    datBen.AddCell(intBen1);

                    PdfPCell colBen1 = (new PdfPCell(new Phrase(colBenef, fuente8)));
                    colBen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    colBen1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    datBen.AddCell(colBen1);

                    PdfPCell telBen1 = (new PdfPCell(new Phrase(telBenef, fuente8)));
                    telBen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    telBen1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    datBen.AddCell(telBen1);
                    documento.Add(datBen);



                    documento.NewPage();

                    //tabla 2° hoja
                    PdfPTable SegHo = new PdfPTable(5);
                    SegHo.WidthPercentage = 100f;
                    int[] SegHocellwidth = { 5, 5, 35, 35, 20 };
                    SegHo.SetWidths(SegHocellwidth);


                    // string sicli = r[61].ToString();
                    // string nocli = r[61].ToString();
                    string cargo = r[59].ToString();
                    string dependencia = r[60].ToString();
                    string periodo = r[61].ToString();


                    PdfPCell ocupado = (new PdfPCell(new Phrase("¿USTED HA OCUPADO CARGOS PÚBLICOS DESTACADOS EN LOS ÚLTIMOS DOCE MESES?", fuente6)) { Colspan = 5 });
                    ocupado.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocupado.BackgroundColor = BaseColor.LIGHT_GRAY;
                    SegHo.AddCell(ocupado);

                    PdfPCell persPol = (new PdfPCell(new Phrase("(Pesona políticamente expuesta, entre otros: Jefe de estado, de Gobierno, Líder Politico, Senador, Diputado, Presidente Municipal, miembro del Partido político, Judicial o Militar de cualquier gerarquía", fuente10)) { Colspan = 5, });
                    persPol.HorizontalAlignment = Element.ALIGN_CENTER;
                    SegHo.AddCell(persPol);

                    PdfPCell no = new PdfPCell(new Phrase("NO", fuente2));
                    no.HorizontalAlignment = Element.ALIGN_CENTER;
                    no.BackgroundColor = BaseColor.LIGHT_GRAY;
                    SegHo.AddCell(no);

                    PdfPCell si = new PdfPCell(new Phrase("SI", fuente2));
                    si.HorizontalAlignment = Element.ALIGN_CENTER;
                    si.BackgroundColor = BaseColor.LIGHT_GRAY;
                    SegHo.AddCell(si);

                    PdfPCell carDes = new PdfPCell(new Phrase("CARGO DESEMPEÑADO", fuente2));
                    carDes.HorizontalAlignment = Element.ALIGN_CENTER;
                    carDes.BackgroundColor = BaseColor.LIGHT_GRAY;
                    SegHo.AddCell(carDes);

                    PdfPCell depen = new PdfPCell(new Phrase("DEPENDENCIA", fuente2));
                    depen.HorizontalAlignment = Element.ALIGN_CENTER;
                    depen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    SegHo.AddCell(depen);

                    PdfPCell perio = new PdfPCell(new Phrase("PERIODO", fuente2));
                    perio.HorizontalAlignment = Element.ALIGN_CENTER;
                    perio.BackgroundColor = BaseColor.LIGHT_GRAY;
                    SegHo.AddCell(perio);

                    PdfPCell no1 = new PdfPCell(new Phrase(" ", fuente6));
                    no1.HorizontalAlignment = Element.ALIGN_CENTER;
                    SegHo.AddCell(no1);

                    PdfPCell si1 = new PdfPCell(new Phrase(" ", fuente6));
                    si1.HorizontalAlignment = Element.ALIGN_CENTER;
                    SegHo.AddCell(si1);

                    PdfPCell carDes1 = new PdfPCell(new Phrase(" " + cargo.ToUpper(), fuente8));
                    carDes1.HorizontalAlignment = Element.ALIGN_CENTER;
                    SegHo.AddCell(carDes1);

                    PdfPCell depen1 = new PdfPCell(new Phrase(" " + dependencia, fuente8));
                    depen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    SegHo.AddCell(depen1);

                    PdfPCell perio1 = new PdfPCell(new Phrase(" " + periodo, fuente8));
                    perio1.HorizontalAlignment = Element.ALIGN_CENTER;
                    SegHo.AddCell(perio1);

                    PdfPCell sobri1 = (new PdfPCell(new Phrase("ESPOSO(A), CONCUBINO(A), MADRE, PADRE, ABUELO(A), HIJA(O), NIETO, HERMANO, SOBRINO, CUÑADO", fuente2)) { Colspan = 5 });
                    sobri1.HorizontalAlignment = Element.ALIGN_LEFT;
                    SegHo.AddCell(sobri1);
                    documento.Add(SegHo);

                    //FAMILIAR
                    PdfPTable family = new PdfPTable(4);
                    family.WidthPercentage = 100f;
                    int[] familycellwidth = { 5, 5, 50, 50 };
                    family.SetWidths(familycellwidth);

                    string nomfami = r[63].ToString();
                    string paren = r[64].ToString();

                    PdfPCell noFa = new PdfPCell(new Phrase("NO", fuente2));
                    noFa.HorizontalAlignment = Element.ALIGN_CENTER;
                    noFa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    family.AddCell(noFa);

                    PdfPCell siFa = new PdfPCell(new Phrase("SI", fuente2));
                    siFa.HorizontalAlignment = Element.ALIGN_CENTER;
                    siFa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    family.AddCell(siFa);

                    PdfPCell nameFa = new PdfPCell(new Phrase("NOMBRE FAMILIAR", fuente2));
                    nameFa.HorizontalAlignment = Element.ALIGN_CENTER;
                    nameFa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    family.AddCell(nameFa);

                    PdfPCell pareFa = new PdfPCell(new Phrase("PARENTESCO", fuente2));
                    pareFa.HorizontalAlignment = Element.ALIGN_CENTER;
                    pareFa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    family.AddCell(pareFa);

                    PdfPCell noFa1 = new PdfPCell(new Phrase(" ", fuente6));
                    noFa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    family.AddCell(noFa1);

                    PdfPCell siFa1 = new PdfPCell(new Phrase(" ", fuente6));
                    siFa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    family.AddCell(siFa1);

                    PdfPCell nameFa1 = new PdfPCell(new Phrase(" " + nomfami.ToUpper(), fuente8));
                    nameFa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    family.AddCell(nameFa1);

                    PdfPCell pareFa1 = new PdfPCell(new Phrase(" " + paren.ToUpper(), fuente8));
                    pareFa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    family.AddCell(pareFa1);
                    documento.Add(family);

                    //cargo desempeñado
                    PdfPTable carDe = new PdfPTable(3);
                    carDe.WidthPercentage = 100f;
                    int[] carDecellwidth = { 40, 40, 20 };
                    carDe.SetWidths(carDecellwidth);

                    string cargodes = r[65].ToString();
                    string dep = r[66].ToString();
                    string per = r[67].ToString();

                    PdfPCell cargoDesem = new PdfPCell(new Phrase("CARGO DEPEMPEÑADO", fuente2));
                    cargoDesem.HorizontalAlignment = Element.ALIGN_CENTER;
                    cargoDesem.BackgroundColor = BaseColor.LIGHT_GRAY;
                    carDe.AddCell(cargoDesem);

                    PdfPCell depenDen = new PdfPCell(new Phrase("DEPENDENCIA", fuente2));
                    depenDen.HorizontalAlignment = Element.ALIGN_CENTER;
                    depenDen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    carDe.AddCell(depenDen);

                    PdfPCell periodoDe = new PdfPCell(new Phrase("PERIODO", fuente2));
                    periodoDe.HorizontalAlignment = Element.ALIGN_CENTER;
                    periodoDe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    carDe.AddCell(periodoDe);

                    PdfPCell cargoDesem1 = new PdfPCell(new Phrase(" ", fuente2));
                    cargoDesem1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cargoDesem1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    carDe.AddCell(cargoDesem1);

                    PdfPCell depenDen1 = new PdfPCell(new Phrase(" ", fuente2));
                    depenDen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    depenDen1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    carDe.AddCell(depenDen1);

                    PdfPCell periodoDe1 = new PdfPCell(new Phrase(" ", fuente2));
                    periodoDe1.HorizontalAlignment = Element.ALIGN_CENTER;
                    periodoDe1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    carDe.AddCell(periodoDe1);
                    documento.Add(carDe);

                    

                    //INFORMACIÓN REFERENTE AL PROPIETARIO
                    PdfPTable infoRefP = new PdfPTable(3);
                    infoRefP.WidthPercentage = 100f;
                    int[] infoRefPcellwidth = { 20, 20, 60 };
                    infoRefP.SetWidths(infoRefPcellwidth);

                    string apellipa = r[68].ToString();
                    string apellima = r[69].ToString();
                    string nombrs = r[70].ToString();

                    PdfPCell encaRefP = (new PdfPCell(new Phrase("INFORMACIÓN REFERENTE AL PROPIETARIO REAL", fuente6)) { Colspan = 3 });
                    encaRefP.HorizontalAlignment = Element.ALIGN_CENTER;
                    encaRefP.BackgroundColor = BaseColor.LIGHT_GRAY;
                    infoRefP.AddCell(encaRefP);

                    PdfPCell apelliParR = new PdfPCell(new Phrase(" " + apellipa.ToUpper(), fuente8));
                    apelliParR.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRefP.AddCell(apelliParR);

                    PdfPCell apelliMarR = new PdfPCell(new Phrase(" " + apellima.ToUpper(), fuente8));
                    apelliMarR.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRefP.AddCell(apelliMarR);

                    PdfPCell nameRePr = new PdfPCell(new Phrase(" " + nombrs.ToUpper(), fuente8));
                    nameRePr.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRefP.AddCell(nameRePr);

                    PdfPCell apelliParR1 = new PdfPCell(new Phrase("APELLIDO PATERNO", fuente2));
                    apelliParR1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRefP.AddCell(apelliParR1);

                    PdfPCell apelliMarR1 = new PdfPCell(new Phrase("APELLIDO MATERNO", fuente2));
                    apelliMarR1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRefP.AddCell(apelliMarR1);

                    PdfPCell nameRePr1 = new PdfPCell(new Phrase("NOMBRE(S)", fuente2));
                    nameRePr1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRefP.AddCell(nameRePr1);
                    documento.Add(infoRefP);

                    //datos generales
                    PdfPTable datGeRe = new PdfPTable(10);
                    datGeRe.WidthPercentage = 100f;
                    int[] datGeRecellwidth = { 20, 18, 13, 2, 2, 9, 9, 9, 9, 9 };
                    datGeRe.SetWidths(datGeRecellwidth);

                    string fechanac = r[72].ToString();
                    string entidnacimi = r[73].ToString();
                    string nacionali = r[74].ToString();

                    string H1 = r[75].ToString();
                    string M1 = r[75].ToString();

                    iTextSharp.text.Font xb = new iTextSharp.text.Font();
                    iTextSharp.text.Font xb2 = new iTextSharp.text.Font();

                    if (H1 == "H")
                    {
                        H1 = "X";
                        xb = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { H1 = " ";
                        xb = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (M1 == "M")
                    {
                        M1 = "X";
                        xb2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else
                    {
                        M1 = " ";
                        xb2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    string SOL1 = r[76].ToString();
                    string CAS1 = r[76].ToString();
                    string VIU1 = r[76].ToString();
                    string DIV1= r[76].ToString();
                    string UL1 = r[76].ToString();

                    iTextSharp.text.Font PUT = new iTextSharp.text.Font();
                    iTextSharp.text.Font PUT2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font PUT3 = new iTextSharp.text.Font();
                    iTextSharp.text.Font PUT4 = new iTextSharp.text.Font();
                    iTextSharp.text.Font PUT5 = new iTextSharp.text.Font();
                    

                    if (SOL1 == "SOL")
                    {
                        SOL1 = "X";
                        PUT = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { SOL1 = " ";
                        PUT = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (CAS1 == "CAS")
                    {
                        CAS1 = "X";
                        PUT2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { CAS1 = " ";
                        PUT2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (VIU1 == "VIU")
                    {
                        VIU1 = "X";
                        PUT3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { VIU1 = " ";
                        PUT3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (DIV1 == "DIV")
                    {
                        DIV1 = "X";
                        PUT4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { DIV1 = " ";
                        PUT4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (UL1 == "UL")
                    {
                        UL1 = "X";
                        PUT5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { UL1 = " ";
                        PUT5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                  


                    PdfPCell fechNaR = new PdfPCell(new Phrase("FECHA DE NACIMIENTO(dd/mm/aa)", fuente2));
                    fechNaR.HorizontalAlignment = Element.ALIGN_CENTER;
                    fechNaR.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGeRe.AddCell(fechNaR);

                    PdfPCell entiNaR = new PdfPCell(new Phrase("ENTIDAD DE NACIMIENTO", fuente2));
                    entiNaR.HorizontalAlignment = Element.ALIGN_CENTER;
                    entiNaR.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGeRe.AddCell(entiNaR);

                    PdfPCell naciRe = new PdfPCell(new Phrase("NACIONALIDAD", fuente2));
                    naciRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    naciRe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGeRe.AddCell(naciRe);

                    PdfPCell sexRe = (new PdfPCell(new Phrase("SEXO", fuente2)) { Colspan = 2 });
                    sexRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    sexRe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGeRe.AddCell(sexRe);

                    PdfPCell estCiRe = (new PdfPCell(new Phrase("ESTADO CIVIL (Marque con una X)", fuente2)) { Colspan = 5 });
                    estCiRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    estCiRe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGeRe.AddCell(estCiRe);

                    PdfPCell fechNaR1 = new PdfPCell(new Phrase(" " + Convert.ToDateTime(fechanac).ToString("dd/MM/yyyy"), fuente8));
                    fechNaR1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(fechNaR1);

                    PdfPCell entiNaR1 = new PdfPCell(new Phrase(" " + entidnacimi.ToUpper(), fuente8));
                    entiNaR1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(entiNaR1);

                    PdfPCell naciRe1 = new PdfPCell(new Phrase(" " + nacionali.ToUpper(), fuente8));
                    naciRe1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(naciRe1);

                    PdfPCell sexRe1 = new PdfPCell(new Phrase("M" +M1, xb2));
                    sexRe1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(sexRe1);

                    PdfPCell sexRe2 = new PdfPCell(new Phrase("H"+H1, xb));
                    sexRe2.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(sexRe2);

                    PdfPCell estCiRe1 = new PdfPCell(new Phrase("SOLTERO(A)  \n" + SOL1, PUT));
                    estCiRe1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(estCiRe1);

                    PdfPCell estCiRe2 = new PdfPCell(new Phrase("CASADO(A)  \n" + CAS1, PUT2));
                    estCiRe2.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(estCiRe2);

                    PdfPCell estCiRe3 = new PdfPCell(new Phrase("VIUDO(A)  \n" + VIU1, PUT3));
                    estCiRe3.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(estCiRe3);

                    PdfPCell estCiRe4 = new PdfPCell(new Phrase("DIVORCIADO(A)  \n" + DIV1, PUT4));
                    estCiRe4.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(estCiRe4);

                    PdfPCell estCiRe5 = new PdfPCell(new Phrase("UNION LIBRE  \n" + UL1, PUT5));
                    estCiRe5.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGeRe.AddCell(estCiRe5);
                    documento.Add(datGeRe);

                    //DATOS GENERALES VOL 2
                    PdfPTable curpRe = new PdfPTable(6);
                    curpRe.WidthPercentage = 100f;
                    int[] curpRecellwidth = { 15, 20, 10, 25, 10, 20 };
                    curpRe.SetWidths(curpRecellwidth);

                    string ifeine = r[77].ToString();
                    string curprop = r[78].ToString();
                    string rfcpro = r[79].ToString();

                    PdfPCell ineRe = new PdfPCell(new Phrase("No. CRED. IFE o INE", fuente2));
                    ineRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    ineRe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    curpRe.AddCell(ineRe);

                    PdfPCell ineRe1 = new PdfPCell(new Phrase(" " + ifeine, fuente8));
                    ineRe1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpRe.AddCell(ineRe1);

                    PdfPCell curpReal = new PdfPCell(new Phrase("CURP", fuente2));
                    curpReal.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpReal.BackgroundColor = BaseColor.LIGHT_GRAY;
                    curpRe.AddCell(curpReal);

                    PdfPCell curpReal1 = new PdfPCell(new Phrase(" " + curprop, fuente8));
                    curpReal1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpRe.AddCell(curpReal1);

                    PdfPCell rfcReal = new PdfPCell(new Phrase("RFC", fuente2));
                    rfcReal.HorizontalAlignment = Element.ALIGN_CENTER;
                    rfcReal.BackgroundColor = BaseColor.LIGHT_GRAY;
                    curpRe.AddCell(rfcReal);

                    PdfPCell rfcReal1 = new PdfPCell(new Phrase(" " + rfcpro, fuente8));
                    rfcReal1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpRe.AddCell(rfcReal1);
                    documento.Add(curpRe);

                    //escolaridad real
                    PdfPTable schoolRe = new PdfPTable(10);
                    schoolRe.WidthPercentage = 100f;
                    int[] schoolRecellwidth = { 10, 10, 10, 10, 10, 10, 10, 10, 7, 13 };
                    schoolRe.SetWidths(schoolRecellwidth);

                    
                    string rolhogar1 = r[81].ToString();
                    string rolhogar2 = r[81].ToString();
                    string rolhogar3 = r[81].ToString();

                    iTextSharp.text.Font roll1 = new iTextSharp.text.Font();
                    iTextSharp.text.Font roll2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font roll3 = new iTextSharp.text.Font();

                    if (rolhogar1 == "Jefa(e)")
                    {
                        rolhogar1 = "X";
                        roll1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        rolhogar1 = " ";
                        roll1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (rolhogar2 == "Pareja")
                    {
                        rolhogar2 = "X";
                        roll2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        rolhogar2 = " ";
                        roll2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (rolhogar3 == "Hijo(a)")
                    {
                        rolhogar3 = "X";
                        roll3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        rolhogar3 = " ";
                        roll3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    string nohijos = r[82].ToString();
                    string noindepn = r[83].ToString();                
                    
                    string escolaridad1 = r[80].ToString();
                    string escolaridad12 = r[80].ToString();
                    string escolaridad13 = r[80].ToString();
                    string escolaridad14 = r[80].ToString();
                    string escolaridad15 = r[80].ToString();
                    string escolaridad16 = r[80].ToString();

                    iTextSharp.text.Font EST = new iTextSharp.text.Font();
                    iTextSharp.text.Font EST2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font EST3 = new iTextSharp.text.Font();
                    iTextSharp.text.Font EST4 = new iTextSharp.text.Font();
                    iTextSharp.text.Font EST5 = new iTextSharp.text.Font();
                    iTextSharp.text.Font EST6 = new iTextSharp.text.Font();

                    if (escolaridad1 == "SIN")
                    {
                        escolaridad1 = "X";
                        EST = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { escolaridad1 = " ";
                        EST = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad12 == "PRI")
                    {
                        escolaridad12 = "X";
                        EST2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { escolaridad12 = " ";
                        EST2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad13 == "SEC")
                    {
                        escolaridad13 = "X";
                        EST3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { escolaridad13 = " ";
                        EST3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad14 == "BAC")
                    {
                        escolaridad14 = "X";
                        EST4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { escolaridad14 = " ";
                        EST4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad15 == "LIC")
                    {
                        escolaridad15 = "X";
                        EST5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { escolaridad15 = " ";
                        EST5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad16 == "POS")
                    {
                        escolaridad16 = "X";
                        EST6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { escolaridad16 = " ";
                        EST6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                   


                    PdfPCell niVe = (new PdfPCell(new Phrase("NIVEL DE ESCOLARIDAD (Marque con una X)", fuente2)) { Colspan = 5 });
                    niVe.HorizontalAlignment = Element.ALIGN_CENTER;
                    niVe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolRe.AddCell(niVe);

                    PdfPCell rolRe = (new PdfPCell(new Phrase("ROL EN EL HOGAR (Marque con una X)", fuente2)) { Colspan = 3 });
                    rolRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    rolRe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolRe.AddCell(rolRe);

                    PdfPCell hijosRe = new PdfPCell(new Phrase("No. HIJOS", fuente2));
                    hijosRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    hijosRe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolRe.AddCell(hijosRe);

                    PdfPCell depenRe = new PdfPCell(new Phrase("No. DE DEPEDIENTES", fuente2));
                    depenRe.HorizontalAlignment = Element.ALIGN_CENTER;
                    depenRe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolRe.AddCell(depenRe);

                    PdfPCell nini = new PdfPCell(new Phrase("SIN INSTRUCCIÓN  \n "+escolaridad1, EST));
                    nini.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(nini);

                    PdfPCell prima = new PdfPCell(new Phrase("PRIMARIA  \n " + escolaridad12, EST2));
                    prima.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(prima);

                    PdfPCell secundaria = new PdfPCell(new Phrase("SECUNDARIA  \n " + escolaridad13, EST3));
                    secundaria.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(secundaria);

                    PdfPCell bachi = new PdfPCell(new Phrase("BACHILLERATO  \n " + escolaridad14, EST4));
                    bachi.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(bachi);

                    PdfPCell licen = new PdfPCell(new Phrase("LICENCIATURA  \n " + escolaridad15, EST5));
                    licen.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(licen);

                    PdfPCell POST = new PdfPCell(new Phrase("POSGRADO  \n " + escolaridad16, EST6));
                    POST.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(POST);

                    PdfPCell jefeFa = new PdfPCell(new Phrase("JEFE(A) \n" + rolhogar1, roll1));
                    jefeFa.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(jefeFa);

                    PdfPCell parej = new PdfPCell(new Phrase("PAREJA \n"+rolhogar2, roll2));
                    parej.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(parej);

                    PdfPCell hij = new PdfPCell(new Phrase("HIJO(A) \n"+ rolhogar3, roll3));
                    hij.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(hij);

                    PdfPCell numHij = new PdfPCell(new Phrase(" " + nohijos, fuente8));
                    numHij.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(numHij);

                    PdfPCell numD = new PdfPCell(new Phrase(" " + noindepn, fuente8));
                    numD.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolRe.AddCell(numD);
                    documento.Add(schoolRe);
                    documento.Add(new Paragraph(" "));

                    //mas tablas 
                    PdfPTable tablaDos = new PdfPTable(4);
                    tablaDos.WidthPercentage = 100f;
                    int[] tablaDoscellwidth = { 35, 15, 25, 25 };
                    tablaDos.SetWidths(tablaDoscellwidth);

                    string ocuparef = r[84].ToString();
                    string tfijoref = r[85].ToString();
                    string tcelre = r[86].ToString();
                    string emailref = r[87].ToString();


                    PdfPCell ocupDos = new PdfPCell(new Phrase("OCUPACIÓN", fuente2));
                    ocupDos.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocupDos.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDos.AddCell(ocupDos);

                    PdfPCell telFiDos = new PdfPCell(new Phrase("TELÉFONO FIJO", fuente2));
                    telFiDos.HorizontalAlignment = Element.ALIGN_CENTER;
                    telFiDos.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDos.AddCell(telFiDos);

                    PdfPCell telCelDos = new PdfPCell(new Phrase("TELÉFONO CELULAR", fuente2));
                    telCelDos.HorizontalAlignment = Element.ALIGN_CENTER;
                    telCelDos.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDos.AddCell(telCelDos);

                    PdfPCell correoDos = new PdfPCell(new Phrase("CORREO ELECTRÓNICO", fuente2));
                    correoDos.HorizontalAlignment = Element.ALIGN_CENTER;
                    correoDos.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDos.AddCell(correoDos);

                    PdfPCell ocupDos1 = new PdfPCell(new Phrase(" " + ocuparef, fuente8));
                    ocupDos1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDos.AddCell(ocupDos1);

                    PdfPCell telFiDos2 = new PdfPCell(new Phrase(" " + tfijoref, fuente8));
                    telFiDos2.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDos.AddCell(telFiDos2);

                    PdfPCell telCelDos1 = new PdfPCell(new Phrase(" " + tcelre, fuente8));
                    telCelDos1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDos.AddCell(telCelDos1);

                    PdfPCell correoDos1 = new PdfPCell(new Phrase(" " + emailref, fuente8));
                    correoDos1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDos.AddCell(correoDos1);
                    documento.Add(tablaDos);

                    //domicilio 3.0
                    PdfPTable domiTre = new PdfPTable(4);
                    domiTre.WidthPercentage = 100f;
                    int[] domiTrecellwidth = { 40, 10, 10, 40 };
                    domiTre.SetWidths(domiTrecellwidth);
                    string domic5 = r[88].ToString();
                    string numext5 = r[89].ToString();
                    string numint5 = r[90].ToString();
                    string colonia5 = r[91].ToString();

                    PdfPCell domiTr3s = (new PdfPCell(new Phrase("DOMICILIO", fuente6)) { Colspan = 4 });
                    domiTr3s.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTr3s.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTre.AddCell(domiTr3s);

                    PdfPCell aveni = new PdfPCell(new Phrase("CALLE, AVENIDA ANDADOR, CERRADA, CALLEJON, MANZANA, LOTE", fuente2));
                    aveni.HorizontalAlignment = Element.ALIGN_CENTER;
                    aveni.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTre.AddCell(aveni);

                    PdfPCell exter = new PdfPCell(new Phrase("No. EXTERIOR", fuente2));
                    exter.HorizontalAlignment = Element.ALIGN_CENTER;
                    exter.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTre.AddCell(exter);

                    PdfPCell inter = new PdfPCell(new Phrase("No. INTERIOR", fuente2));
                    inter.HorizontalAlignment = Element.ALIGN_CENTER;
                    inter.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTre.AddCell(inter);

                    PdfPCell coloniaD = new PdfPCell(new Phrase("COLONIA O LOCALIDAD", fuente2));
                    coloniaD.HorizontalAlignment = Element.ALIGN_CENTER;
                    coloniaD.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTre.AddCell(coloniaD);

                    PdfPCell aveni1 = new PdfPCell(new Phrase(" " + domic5.ToUpper(), fuente8));
                    aveni1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTre.AddCell(aveni1);

                    PdfPCell exter1 = new PdfPCell(new Phrase(" " + numext5, fuente8));
                    exter1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTre.AddCell(exter1);

                    PdfPCell inter1 = new PdfPCell(new Phrase(" " + numint5, fuente8));
                    inter1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTre.AddCell(inter1);

                    PdfPCell coloniaD1 = new PdfPCell(new Phrase(" " + colonia5.ToUpper(), fuente8));
                    coloniaD1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTre.AddCell(coloniaD1);
                    documento.Add(domiTre);

                    //mas datos generales
                    PdfPTable datGenra = new PdfPTable(5);
                    datGenra.WidthPercentage = 100f;
                    int[] datGenracellwidth = { 15, 30, 20, 15, 20 };
                    datGenra.SetWidths(datGenracellwidth);

                    string cp5 = r[92].ToString();
                    string del5 = r[93].ToString();
                    string estado5 = r[94].ToString();
                    string tiempo5 = r[95].ToString();
                    string numhabit5 = r[96].ToString();

                    PdfPCell codiPost = new PdfPCell(new Phrase("CÓDIGO POSTAL", fuente2));
                    codiPost.HorizontalAlignment = Element.ALIGN_CENTER;
                    codiPost.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenra.AddCell(codiPost);

                    PdfPCell munici = new PdfPCell(new Phrase("DELEGACIÓN MUNICIPIO", fuente2));
                    munici.HorizontalAlignment = Element.ALIGN_CENTER;
                    munici.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenra.AddCell(munici);

                    PdfPCell stado = new PdfPCell(new Phrase("ESTADO", fuente2));
                    stado.HorizontalAlignment = Element.ALIGN_CENTER;
                    stado.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenra.AddCell(stado);

                    PdfPCell timeResiden = new PdfPCell(new Phrase("TIEMPO DE RESIDENCIA", fuente2));
                    timeResiden.HorizontalAlignment = Element.ALIGN_CENTER;
                    timeResiden.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenra.AddCell(timeResiden);

                    PdfPCell habitant = new PdfPCell(new Phrase("No. DE HABITANTES", fuente2));
                    habitant.HorizontalAlignment = Element.ALIGN_CENTER;
                    habitant.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenra.AddCell(habitant);

                    PdfPCell codiPost1 = new PdfPCell(new Phrase(" " + cp5.ToUpper(), fuente8));
                    codiPost1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenra.AddCell(codiPost1);

                    PdfPCell munici1 = new PdfPCell(new Phrase(" " + del5.ToUpper(), fuente8));
                    munici1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenra.AddCell(munici1);

                    PdfPCell stado1 = new PdfPCell(new Phrase(" " + estado5.ToUpper(), fuente8));
                    stado1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenra.AddCell(stado1);

                    PdfPCell timeResiden1 = new PdfPCell(new Phrase(" " + tiempo5, fuente8));
                    timeResiden1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenra.AddCell(timeResiden1);

                    PdfPCell habitant1 = new PdfPCell(new Phrase(" " + numhabit5, fuente8));
                    habitant1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenra.AddCell(habitant1);
                    documento.Add(datGenra);
                    documento.Add(new Paragraph(" "));

                    //INFORMACION DEL PROVEEDOR DE RECURSOS
                    PdfPTable infoPRO = new PdfPTable(3);
                    infoPRO.WidthPercentage = 100f;
                    int[] infoPROcellwidth = { 20, 20, 60 };
                    infoPRO.SetWidths(infoPROcellwidth);

                    string apellido5 = r[97].ToString();
                    string materno5 = r[98].ToString();
                    string names5 = r[99].ToString();


                    PdfPCell encaRefPRO = (new PdfPCell(new Phrase("INFORMACIÓN REFERENTE PROVEEDOR DE RECURSOS", fuente6)) { Colspan = 3 });
                    encaRefPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    encaRefPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    infoPRO.AddCell(encaRefPRO);

                    PdfPCell apelliParPRO = new PdfPCell(new Phrase(" " + apellido5.ToUpper(), fuente8));
                    apelliParPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoPRO.AddCell(apelliParPRO);

                    PdfPCell apelliMarPRO = new PdfPCell(new Phrase(" " + materno5.ToUpper(), fuente8));
                    apelliMarPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoPRO.AddCell(apelliMarPRO);

                    PdfPCell namePROPr = new PdfPCell(new Phrase(" " + names5.ToUpper(), fuente8));
                    namePROPr.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoPRO.AddCell(namePROPr);

                    PdfPCell apelliParPRO1 = new PdfPCell(new Phrase("APELLIDO PATERNO", fuente2));
                    apelliParPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoPRO.AddCell(apelliParPRO1);

                    PdfPCell apelliMarPRO1 = new PdfPCell(new Phrase("APELLIDO MATERNO", fuente2));
                    apelliMarPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoPRO.AddCell(apelliMarPRO1);

                    PdfPCell namePROPr1 = new PdfPCell(new Phrase("NOMBRE(S)", fuente2));
                    namePROPr1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoPRO.AddCell(namePROPr1);
                    documento.Add(infoPRO);

                    //datos generales
                    PdfPTable datGePRO = new PdfPTable(10);
                    datGePRO.WidthPercentage = 100f;
                    int[] datGePROcellwidth = { 20, 18, 13, 2, 2, 9, 9, 9, 9, 9 };
                    datGePRO.SetWidths(datGePROcellwidth);

                    string fechanam5 = r[101].ToString();
                    string entid5 = r[102].ToString();
                    string nacional5 = r[103].ToString();

                    string H12 = r[104].ToString();
                    string M13 = r[104].ToString();

                    iTextSharp.text.Font SEX = new iTextSharp.text.Font();
                    iTextSharp.text.Font SEX2 = new iTextSharp.text.Font();


                    if (H12 == "H")
                    {
                        H12 = "X";
                        SEX = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { H12 = " ";
                        SEX = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (M13 == "M")
                    {
                        M13 = "X";
                        SEX2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else
                    {
                        M13 = " ";
                        SEX2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }


                    string SOL2 = r[105].ToString();
                    string CAS3 = r[105].ToString();
                    string VIU4 = r[105].ToString();
                    string DIV5 = r[76].ToString();
                    string UL6= r[105].ToString();

                    iTextSharp.text.Font EC = new iTextSharp.text.Font();
                    iTextSharp.text.Font EC2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font EC3 = new iTextSharp.text.Font();
                    iTextSharp.text.Font EC4 = new iTextSharp.text.Font();
                    iTextSharp.text.Font EC5 = new iTextSharp.text.Font();
                    


                    if (SOL2 == "SOL")
                    {
                        SOL2 = "X";
                        EC = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { SOL2 = " ";
                        EC = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (CAS3 == "CAS")
                    {
                        CAS3 = "X";
                        EC2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { CAS3 = " ";
                        EC2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (VIU4 == "VIU")
                    {
                        VIU4 = "X";
                        EC3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { VIU4 = " ";
                        EC3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (DIV5 == "DIV")
                    {
                        DIV5 = "X";
                        EC4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { DIV5 = " ";
                        EC4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (UL6 == "UL")
                    {
                        UL6 = "X";
                        EC5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { UL6 = " ";
                        EC5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }



                    PdfPCell fechNaPRO = new PdfPCell(new Phrase("FECHA DE NACIMIENTO(dd/mm/aa)", fuente2));
                    fechNaPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    fechNaPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGePRO.AddCell(fechNaPRO);

                    PdfPCell entiNaPRO = new PdfPCell(new Phrase("ENTIDAD DE NACIMIENTO", fuente2));
                    entiNaPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    entiNaPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGePRO.AddCell(entiNaPRO);

                    PdfPCell naciPRO = new PdfPCell(new Phrase("NACIONAIDAD", fuente2));
                    naciPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    naciPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGePRO.AddCell(naciPRO);

                    PdfPCell sexPRO = (new PdfPCell(new Phrase("SEXO", fuente2)) { Colspan = 2 });
                    sexPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    sexPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGePRO.AddCell(sexPRO);

                    PdfPCell estCiPRO = (new PdfPCell(new Phrase("ESTADO CIVIL (Marque con una X)", fuente2)) { Colspan = 5 });
                    estCiPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    estCiPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGePRO.AddCell(estCiPRO);

                    PdfPCell fechNaPRO1 = new PdfPCell(new Phrase(" " + Convert.ToDateTime(fechanam5).ToString("dd/MM/yyyy"), fuente8));
                    fechNaPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(fechNaPRO1);

                    PdfPCell entiNaPRO1 = new PdfPCell(new Phrase(" " + entid5.ToUpper(), fuente8));
                    entiNaPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(entiNaPRO1);

                    PdfPCell naciPRO1 = new PdfPCell(new Phrase(" " + nacional5.ToUpper(), fuente8));
                    naciPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(naciPRO1);

                    PdfPCell sexPRO1 = new PdfPCell(new Phrase("M  \n" + M13, SEX2));
                    sexPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(sexPRO1);

                    PdfPCell sexPRO2 = new PdfPCell(new Phrase("H  \n" + H12, SEX));
                    sexPRO2.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(sexPRO2);

                    PdfPCell estCiPRO1 = new PdfPCell(new Phrase("SOLTERO(A)  \n" + SOL2, EC));
                    estCiPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(estCiPRO1);

                    PdfPCell estCiPRO2 = new PdfPCell(new Phrase("CASADO(A) \n" + CAS3, EC2));
                    estCiPRO2.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(estCiPRO2);

                    PdfPCell estCiPRO3 = new PdfPCell(new Phrase("VIUDO(A)  \n" + VIU4, EC3));
                    estCiPRO3.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(estCiPRO3);

                    PdfPCell estCiPRO4 = new PdfPCell(new Phrase("DIVORCIADO(A)  \n" + DIV5, EC4));
                    estCiPRO4.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(estCiPRO4);

                    PdfPCell estCiPRO5 = new PdfPCell(new Phrase("UNIÓN LIBRE  \n" + UL6, EC5));
                    estCiPRO5.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGePRO.AddCell(estCiPRO5);
                    documento.Add(datGePRO);

                    //DATOS GENERALES VOL 2
                    PdfPTable curpPRO = new PdfPTable(6);
                    curpPRO.WidthPercentage = 100f;
                    int[] curpPROcellwidth = { 15, 20, 10, 25, 10, 20 };
                    curpPRO.SetWidths(curpPROcellwidth);

                    string ine5 = r[106].ToString();
                    string curp5 = r[107].ToString();
                    string rfc5 = r[108].ToString();

                    PdfPCell inePRO = new PdfPCell(new Phrase("No. CRED. IFE o INE", fuente2));
                    inePRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    inePRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    curpPRO.AddCell(inePRO);

                    PdfPCell inePRO1 = new PdfPCell(new Phrase(" " + ine5, fuente8));
                    inePRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpPRO.AddCell(inePRO1);

                    PdfPCell curpPRO1 = new PdfPCell(new Phrase("CURP", fuente2));
                    curpPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpPRO1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    curpPRO.AddCell(curpPRO1);

                    PdfPCell curpPROO1 = new PdfPCell(new Phrase(" " + curp5, fuente8));
                    curpPROO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpPRO.AddCell(curpPROO1);

                    PdfPCell rfcPRO = new PdfPCell(new Phrase("RFC", fuente2));
                    rfcPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    rfcPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    curpPRO.AddCell(rfcPRO);

                    PdfPCell rfcPRO1 = new PdfPCell(new Phrase(" " + rfc5, fuente8));
                    rfcPRO1.HorizontalAlignment = Element.ALIGN_CENTER;
                    curpPRO.AddCell(rfcPRO1);
                    documento.Add(curpPRO);

                    //escolaridad real
                    PdfPTable schoolPRO = new PdfPTable(10);
                    schoolPRO.WidthPercentage = 100f;
                    int[] schoolPROcellwidth = { 10, 10, 10, 10, 10, 10, 10, 10, 7, 13 };
                    schoolPRO.SetWidths(schoolPROcellwidth);

                    string numhijos5 = r[111].ToString();
                    string numdepe5 = r[112].ToString();

                    string escolaridad01 = r[109].ToString();
                    string escolaridad02 = r[109].ToString();
                    string escolaridad03 = r[109].ToString();
                    string escolaridad04 = r[109].ToString();
                    string escolaridad05 = r[109].ToString();
                    string escolaridad06 = r[109].ToString();

                    iTextSharp.text.Font Ees = new iTextSharp.text.Font();
                    iTextSharp.text.Font Ees2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font Ees3 = new iTextSharp.text.Font();
                    iTextSharp.text.Font Ees4 = new iTextSharp.text.Font();
                    iTextSharp.text.Font Ees5 = new iTextSharp.text.Font();
                    iTextSharp.text.Font Ees6 = new iTextSharp.text.Font();

                    if (escolaridad01 == "SIN")
                    {
                        escolaridad01 = "X";
                        Ees = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad01 = " ";
                        Ees = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad02 == "PRI")
                    {
                        escolaridad02 = "X";
                        Ees2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad02 = " ";
                        Ees2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad03 == "SEC")
                    {
                        escolaridad03 = "X";
                        Ees3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad03 = " ";
                        Ees3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad04 == "BAC")
                    {
                        escolaridad04 = "X";
                        Ees4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad04 = " ";
                        Ees4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad05 == "LIC")
                    {
                        escolaridad05 = "X";
                        Ees5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad05 = " ";
                        Ees5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (escolaridad06 == "POS")
                    {
                        escolaridad06 = "X";
                        Ees6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else {
                        escolaridad06 = " ";
                        Ees6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }


                    string rolhogar01 = r[110].ToString();
                    string rolhogar02 = r[110].ToString();
                    string rolhogar03 = r[110].ToString();

                    iTextSharp.text.Font RROLL = new iTextSharp.text.Font();
                    iTextSharp.text.Font RROLL2 = new iTextSharp.text.Font();
                    iTextSharp.text.Font RROLL3 = new iTextSharp.text.Font();

                    if (rolhogar01 == "Jefa(e)")
                    {
                        rolhogar01 = "X";
                        RROLL = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { rolhogar01 = " ";
                        RROLL = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (rolhogar02 == "Pareja")
                    {
                        rolhogar02 = "X";
                        RROLL2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { rolhogar02 = " ";
                        RROLL2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }

                    if (rolhogar03 == "Hijo(a)")
                    {
                        rolhogar03 = "X";
                        RROLL3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    }
                    else { rolhogar03 = " ";
                        RROLL3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    }


                    PdfPCell niVePRO = (new PdfPCell(new Phrase("NIVEL DE ESCOLARIDAD (Marque con una X)", fuente2)) { Colspan = 5 });
                    niVePRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    niVePRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolPRO.AddCell(niVePRO);

                    PdfPCell rolPRO = (new PdfPCell(new Phrase("ROL EN EL HOGAR (Marque con una X)", fuente2)) { Colspan = 3 });
                    rolPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    rolPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolPRO.AddCell(rolPRO);

                    PdfPCell hijosPRO = new PdfPCell(new Phrase("No. HIJOS", fuente2));
                    hijosPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    hijosPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolPRO.AddCell(hijosPRO);

                    PdfPCell depenPRO = new PdfPCell(new Phrase("No. DE DEPEDIENTES", fuente2));
                    depenPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    depenPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    schoolPRO.AddCell(depenPRO);

                    PdfPCell niniPRO = new PdfPCell(new Phrase("SIN INSTRUCCIÓN  \n" + escolaridad01, Ees));
                    niniPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(niniPRO);

                    PdfPCell primaPRO = new PdfPCell(new Phrase("PRIMARIA  \n" + escolaridad02, Ees2));
                    primaPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(primaPRO);

                    PdfPCell secundariaPRO = new PdfPCell(new Phrase("SECUNDARIA  \n" + escolaridad03, Ees3));
                    secundariaPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(secundariaPRO);

                    PdfPCell bachiPRO = new PdfPCell(new Phrase("BACHILLERATO  \n" + escolaridad04, Ees4));
                    bachiPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(bachiPRO);

                    PdfPCell licenPRO = new PdfPCell(new Phrase("LICENCIATURA  \n" + escolaridad05, Ees5));
                    licenPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(licenPRO);

                    PdfPCell jefeFaPRO = new PdfPCell(new Phrase("JEFE(A)  \n" + rolhogar01, RROLL));
                    jefeFaPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(jefeFaPRO);

                    PdfPCell parejPRO = new PdfPCell(new Phrase("PAREJA  \n" + rolhogar02, RROLL2));
                    parejPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(parejPRO);

                    PdfPCell hijPRO = new PdfPCell(new Phrase("HIJO(A)  \n" + rolhogar03, RROLL3));
                    hijPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(hijPRO);

                    PdfPCell numHijPRO = new PdfPCell(new Phrase(" " + numhijos5, fuente8));
                    numHijPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(numHijPRO);

                    PdfPCell numDPRO = new PdfPCell(new Phrase(" " + numdepe5, fuente8));
                    numDPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    schoolPRO.AddCell(numDPRO);
                    documento.Add(schoolPRO);

                    //mas tablas 
                    PdfPTable tablaDosPRO = new PdfPTable(4);
                    tablaDosPRO.WidthPercentage = 100f;
                    int[] tablaDosPROcellwidth = { 35, 15, 25, 25 };
                    tablaDosPRO.SetWidths(tablaDosPROcellwidth);

                    string ocupacion6 = r[113].ToString();
                    string fijo6 = r[114].ToString();
                    string celular5 = r[115].ToString();
                    string email6 = r[116].ToString();

                    PdfPCell ocupDosPRO = new PdfPCell(new Phrase("OCUPACIÓN", fuente2));
                    ocupDosPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocupDosPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDosPRO.AddCell(ocupDosPRO);

                    PdfPCell telFiDosPRO = new PdfPCell(new Phrase("TELÉFONO FIJO", fuente2));
                    telFiDosPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    telFiDosPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDosPRO.AddCell(telFiDosPRO);

                    PdfPCell telCelDosPRO = new PdfPCell(new Phrase("TELÉFONO CELULAR", fuente2));
                    telCelDosPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    telCelDosPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDosPRO.AddCell(telCelDosPRO);

                    PdfPCell correoDosPRO = new PdfPCell(new Phrase("CORREO ELECTRÓNICO", fuente2));
                    correoDosPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    correoDosPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tablaDosPRO.AddCell(correoDosPRO);

                    PdfPCell ocupDos1PRO = new PdfPCell(new Phrase(" " + ocupacion6, fuente8));
                    ocupDos1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDosPRO.AddCell(ocupDos1PRO);

                    PdfPCell telFiDos2PRO = new PdfPCell(new Phrase(" " + fijo6, fuente8));
                    telFiDos2PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDosPRO.AddCell(telFiDos2PRO);

                    PdfPCell telCelDos1PRO = new PdfPCell(new Phrase(" " + celular5, fuente8));
                    telCelDos1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDosPRO.AddCell(telCelDos1PRO);

                    PdfPCell correoDos1PRO = new PdfPCell(new Phrase(" " + email6, fuente8));
                    correoDos1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaDosPRO.AddCell(correoDos1PRO);
                    documento.Add(tablaDosPRO);

                    //domicilio 3.0
                    PdfPTable domiTrePRO = new PdfPTable(4);
                    domiTrePRO.WidthPercentage = 100f;
                    int[] domiTrePROcellwidth = { 40, 10, 10, 40 };
                    domiTrePRO.SetWidths(domiTrePROcellwidth);

                    string calle7 = r[117].ToString();
                    string numext7 = r[118].ToString();
                    string numint7 = r[119].ToString();
                    string coloni7 = r[120].ToString();

                    PdfPCell domiTr3sPRO = (new PdfPCell(new Phrase("DOMICILIO", fuente6)) { Colspan = 4 });
                    domiTr3sPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTr3sPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTrePRO.AddCell(domiTr3sPRO);

                    PdfPCell aveniPRO = new PdfPCell(new Phrase("CALLE, AVENIDA ANDADOR, CERRADA, CALLEJON, MANZANA, LOTE", fuente2));
                    aveniPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    aveniPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTrePRO.AddCell(aveniPRO);

                    PdfPCell exterPRO = new PdfPCell(new Phrase("No. EXTERIOR", fuente2));
                    exterPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    exterPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTrePRO.AddCell(exterPRO);

                    PdfPCell interPRO = new PdfPCell(new Phrase("No. INTERIOR", fuente2));
                    interPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    interPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTrePRO.AddCell(interPRO);

                    PdfPCell coloniaDPRO = new PdfPCell(new Phrase("COLONIA O LOCALIDAD", fuente2));
                    coloniaDPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    coloniaDPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiTrePRO.AddCell(coloniaDPRO);

                    PdfPCell aveni1PRO = new PdfPCell(new Phrase(" " + calle7.ToUpper(), fuente8));
                    aveni1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTrePRO.AddCell(aveni1PRO);

                    PdfPCell exter1PRO = new PdfPCell(new Phrase(" " + numext7, fuente8));
                    exter1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTrePRO.AddCell(exter1PRO);

                    PdfPCell inter1PRO = new PdfPCell(new Phrase(" " + numint7, fuente8));
                    inter1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTrePRO.AddCell(inter1PRO);

                    PdfPCell coloniaD1PRO = new PdfPCell(new Phrase(" " + coloni7.ToUpper(), fuente8));
                    coloniaD1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiTrePRO.AddCell(coloniaD1PRO);
                    documento.Add(domiTrePRO);

                    //mas datos generales
                    PdfPTable datGenraPRO = new PdfPTable(5);
                    datGenraPRO.WidthPercentage = 100f;
                    int[] datGenraPROcellwidth = { 15, 30, 20, 15, 20 };
                    datGenraPRO.SetWidths(datGenraPROcellwidth);

                    string cp7 = r[121].ToString();
                    string delega7 = r[122].ToString();
                    string estado7 = r[123].ToString();
                    string tiemporesi7 = r[124].ToString();
                    string numhabi7 = r[125].ToString();

                    PdfPCell codiPostPRO = new PdfPCell(new Phrase("CÓDIGO POSTAL", fuente2));
                    codiPostPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    codiPostPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenraPRO.AddCell(codiPostPRO);

                    PdfPCell municiPRO = new PdfPCell(new Phrase("DELEGACIÓN MUNICIPIO", fuente2));
                    municiPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    municiPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenraPRO.AddCell(munici);

                    PdfPCell stadoPRO = new PdfPCell(new Phrase("ESTADO", fuente2));
                    stadoPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    stadoPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenraPRO.AddCell(stadoPRO);

                    PdfPCell timeResidenPRO = new PdfPCell(new Phrase("TIEMPO DE RESIDENCIA", fuente2));
                    timeResidenPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    timeResidenPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenraPRO.AddCell(timeResidenPRO);

                    PdfPCell habitantPRO = new PdfPCell(new Phrase("No. DE HABITANTES", fuente2));
                    habitantPRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    habitantPRO.BackgroundColor = BaseColor.LIGHT_GRAY;
                    datGenraPRO.AddCell(habitantPRO);

                    PdfPCell codiPost1PRO = new PdfPCell(new Phrase(" " + cp7, fuente8));
                    codiPost1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenraPRO.AddCell(codiPost1PRO);

                    PdfPCell munici1PRO = new PdfPCell(new Phrase(" " + delega7.ToUpper(), fuente8));
                    munici1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenraPRO.AddCell(munici1PRO);

                    PdfPCell stado1PRO = new PdfPCell(new Phrase(" " + estado7.ToUpper(), fuente8));
                    stado1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenraPRO.AddCell(stado1PRO);

                    PdfPCell timeResiden1PRO = new PdfPCell(new Phrase(" " + tiemporesi7, fuente8));
                    timeResiden1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenraPRO.AddCell(timeResiden1PRO);

                    PdfPCell habitant1PRO = new PdfPCell(new Phrase(" " + numhabi7, fuente8));
                    habitant1PRO.HorizontalAlignment = Element.ALIGN_CENTER;
                    datGenraPRO.AddCell(habitant1PRO);
                    documento.Add(datGenraPRO);

                    PdfPTable firmElec = new PdfPTable(2);
                    firmElec.WidthPercentage = 65f;
                    firmElec.HorizontalAlignment = Element.ALIGN_LEFT;
                    int[] firmEleccellwidth = { 60 , 40 };
                    firmElec.SetWidths(firmEleccellwidth);

                    PdfPCell razSoc = new PdfPCell(new Phrase("DENOMINACIÓN O RAZÓN SOCIAL" , fuente8));
                    razSoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    razSoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    firmElec.AddCell(razSoc);

                    PdfPCell firmaE = new PdfPCell(new Phrase("FIRMA ELECTRÓNICA", fuente8));
                    firmaE.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmaE.BackgroundColor = BaseColor.LIGHT_GRAY;
                    firmElec.AddCell(firmaE);

                    PdfPCell razSoc1 = new PdfPCell(new Phrase(" ", fuente8));
                    razSoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmElec.AddCell(razSoc1);

                    PdfPCell firmaE1 = new PdfPCell(new Phrase(" ", fuente8));
                    firmaE1.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmElec.AddCell(firmaE1);
                    documento.Add(firmElec);

                    PdfPTable infoRef = new PdfPTable(4);
                    infoRef.WidthPercentage = 100f;
                    infoRef.HorizontalAlignment = Element.ALIGN_LEFT;
                    int[] infoRefcellwidth = { 45, 25, 15, 15 };
                    infoRef.SetWidths(infoRefcellwidth);

                    PdfPCell persMoral = (new PdfPCell(new Phrase("INFORMACIÓN REFERENTE AL PROPIETARIO REAL (PERSONA MORAL)", fuente6)) { Colspan = 4 } );
                    persMoral.HorizontalAlignment = Element.ALIGN_CENTER;
                    persMoral.BackgroundColor = BaseColor.LIGHT_GRAY;
                    infoRef.AddCell(persMoral);

                    PdfPCell denoRaz = (new PdfPCell(new Phrase("DENOMINACIÓN O RAZON SOCIAL", fuente2)));
                    denoRaz.HorizontalAlignment = Element.ALIGN_CENTER;
                    denoRaz.BackgroundColor = BaseColor.LIGHT_GRAY;
                    infoRef.AddCell(denoRaz);

                    PdfPCell nacioRaz = (new PdfPCell(new Phrase("NACIONALIDAD", fuente2)));
                    nacioRaz.HorizontalAlignment = Element.ALIGN_CENTER;
                    nacioRaz.BackgroundColor = BaseColor.LIGHT_GRAY;
                    infoRef.AddCell(nacioRaz);

                    PdfPCell objeRaz = (new PdfPCell(new Phrase("OBJETO SOCIAL", fuente2)));
                    objeRaz.HorizontalAlignment = Element.ALIGN_CENTER;
                    objeRaz.BackgroundColor = BaseColor.LIGHT_GRAY;
                    infoRef.AddCell(objeRaz);

                    PdfPCell capSoRaz = (new PdfPCell(new Phrase("CAPITAL SOCIAL", fuente2)));
                    capSoRaz.HorizontalAlignment = Element.ALIGN_CENTER;
                    capSoRaz.BackgroundColor = BaseColor.LIGHT_GRAY;
                    infoRef.AddCell(capSoRaz);

                    PdfPCell denoRaz1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    denoRaz1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRef.AddCell(denoRaz1);

                    PdfPCell nacioRaz1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    nacioRaz1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRef.AddCell(nacioRaz1);

                    PdfPCell objeRaz1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    objeRaz1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRef.AddCell(objeRaz1);

                    PdfPCell capSoRaz1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    capSoRaz1.HorizontalAlignment = Element.ALIGN_CENTER;
                    infoRef.AddCell(capSoRaz1);

                    documento.Add(infoRef);

                    PdfPTable domicRaz = new PdfPTable(7);
                    domicRaz.WidthPercentage = 100f;
                    domicRaz.HorizontalAlignment = Element.ALIGN_LEFT;
                    int[] domicRazcellwidth = { 25, 10, 10, 20, 10, 15, 10 };
                    domicRaz.SetWidths(domicRazcellwidth);

                    PdfPCell domicilioPM = (new PdfPCell(new Phrase("DOMICILIO", fuente6)) { Colspan = 7 });
                    domicilioPM.HorizontalAlignment = Element.ALIGN_CENTER;
                    domicilioPM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(domicilioPM);

                    PdfPCell callePM = (new PdfPCell(new Phrase("CALLE, AVENIDA, CERRADA, CALLEJÓN, ETC.", fuente2)));
                    callePM.HorizontalAlignment = Element.ALIGN_CENTER;
                    callePM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(callePM);

                    PdfPCell noEtxPM = (new PdfPCell(new Phrase("No. EXT", fuente2)));
                    noEtxPM.HorizontalAlignment = Element.ALIGN_CENTER;
                    noEtxPM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(noEtxPM);

                    PdfPCell noIntPM = (new PdfPCell(new Phrase("No. INT", fuente2)));
                    noIntPM.HorizontalAlignment = Element.ALIGN_CENTER;
                    noIntPM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(noIntPM);

                    PdfPCell colPM = (new PdfPCell(new Phrase("COLONIA O LOCALIDAD", fuente2)));
                    colPM.HorizontalAlignment = Element.ALIGN_CENTER;
                    colPM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(colPM);

                    PdfPCell codPsPM = (new PdfPCell(new Phrase("CÓDIGO POSTAL", fuente2)));
                    codPsPM.HorizontalAlignment = Element.ALIGN_CENTER;
                    codPsPM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(codPsPM);

                    PdfPCell delegPM = (new PdfPCell(new Phrase("DELEGACIÓN O MUNICIPIO", fuente2)));
                    delegPM.HorizontalAlignment = Element.ALIGN_CENTER;
                    delegPM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(delegPM);

                    PdfPCell edoPM = (new PdfPCell(new Phrase("ESTADO", fuente2)));
                    edoPM.HorizontalAlignment = Element.ALIGN_CENTER;
                    edoPM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domicRaz.AddCell(edoPM);

                    PdfPCell callePM1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    callePM1.HorizontalAlignment = Element.ALIGN_CENTER; 
                    domicRaz.AddCell(callePM1);

                    PdfPCell noEtxPM1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    noEtxPM1.HorizontalAlignment = Element.ALIGN_CENTER; 
                    domicRaz.AddCell(noEtxPM1);

                    PdfPCell noIntPM1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    noIntPM1.HorizontalAlignment = Element.ALIGN_CENTER; 
                    domicRaz.AddCell(noIntPM1);

                    PdfPCell colPM1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    colPM1.HorizontalAlignment = Element.ALIGN_CENTER; 
                    domicRaz.AddCell(colPM1);

                    PdfPCell codPsPM1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    codPsPM1.HorizontalAlignment = Element.ALIGN_CENTER; 
                    domicRaz.AddCell(codPsPM1);

                    PdfPCell delegPM1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    delegPM1.HorizontalAlignment = Element.ALIGN_CENTER; 
                    domicRaz.AddCell(delegPM1);

                    PdfPCell edoPM1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    edoPM1.HorizontalAlignment = Element.ALIGN_CENTER; 
                    domicRaz.AddCell(edoPM1);
                    documento.Add(domicRaz);
                    documento.Add(new Paragraph(" "));


                    PdfPTable accionistas = new PdfPTable(1);
                    accionistas.WidthPercentage = 60f;
                    accionistas.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell space = (new PdfPCell(new Phrase("ACCIONISTAS", fuente2)));
                    space.HorizontalAlignment = Element.ALIGN_CENTER;
                    space.BackgroundColor = BaseColor.LIGHT_GRAY;
                    accionistas.AddCell(space);

                    PdfPCell space1 = (new PdfPCell(new Phrase(" ", fuente2)));
                    space1.HorizontalAlignment = Element.ALIGN_CENTER;
                    accionistas.AddCell(space1);

                    PdfPCell space2 = (new PdfPCell(new Phrase(" ", fuente2)));
                    space2.HorizontalAlignment = Element.ALIGN_CENTER;
                    accionistas.AddCell(space2);

                    documento.Add(accionistas);


                    //firma de enmedio
                    PdfPTable tabFirm = new PdfPTable(3);
                    tabFirm.WidthPercentage = 100f;
                    int[] tabFirmcellwidth = { 33, 33, 34 };
                    tabFirm.SetWidths(tabFirmcellwidth);

                    PdfPCell aviso = (new PdfPCell(new Phrase(" \n \n Declaro bajo protesta de decir la verdad que la información y los documentos proporcionados para esta solicitud son verdaderos.", fuente10)) { Colspan=3 });
                    aviso.HorizontalAlignment = Element.ALIGN_CENTER;
                    aviso.VerticalAlignment = Element.ALIGN_MIDDLE;
                    aviso.Border = 0;
                    tabFirm.AddCell(aviso);

                    PdfPCell firmaTa = new PdfPCell(new Phrase("\n \n _______________________________________________ \n \n NOMBRE Y FIRMA DEL CLIENTE \n", fuente2));
                    firmaTa.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmaTa.BorderColor = BaseColor.BLUE;
                    tabFirm.AddCell(firmaTa);

                    PdfPCell firmaTa1 = new PdfPCell(new Phrase("\n \n _______________________________________________ \n \n NOMBRE Y FIRMA DEL ASESOR \n", fuente2));
                    firmaTa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmaTa1.BorderColor = BaseColor.BLUE;
                    tabFirm.AddCell(firmaTa1);

                    PdfPCell firmaTa2 = new PdfPCell(new Phrase("\n \n _______________________________________________ \n \n NOMBRE Y FIRMA GERENTE OPERATIVO \n", fuente2));
                    firmaTa2.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmaTa2.BorderColor = BaseColor.BLUE;
                    tabFirm.AddCell(firmaTa2);
                    documento.Add(tabFirm);

                    documento.Add(new Paragraph(" "));


                    //TABLA FINAL DE POLITICAS
                    PdfPTable FINAL = new PdfPTable(1);
                    FINAL.WidthPercentage = 100f;
                    int[] FINALcellwidth = { 100 };
                    FINAL.SetWidths(FINALcellwidth);

                    PdfPCell final1 = new PdfPCell(new Phrase("Estoy enterado del contenido del aviso de privacidad de APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR y sus declaraciones legales con fundamento en lo dispuesto por la Ley Federal de Protección de Datos Personales en posesión de los particulares y su reglamento, para lo cual otorgo de manera voluntaria el más amplio consentimiento y facultad a la empresa APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR a utilizar mis datos personales. APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR se reserva el derecho de cambiar, modificar, complementar y/o alterar el presente aviso, en cualquier momento, en cuyo casi se hará de su conocimiento a través de los medios que establezca la legislación en materia", fuente10));
                    final1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    final1.Border = 0;
                    FINAL.AddCell(final1);
                    documento.Add(FINAL);
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


   


    protected void EstadoCp(object sender, EventArgs e)
    {
            SqlDataSourceCombo.SelectCommand = "";
        SqlDataSourceCombo.DataBind();
        cmbColonia_cli.DataBind();
        ConsBuro obten = new ConsBuro();
        obten.codigo = Convert.ToInt32(txt_cp_cli.Text);
        obten.datosCP();
        if (Convert.ToBoolean(obten.retorno[0]))
        {
            DataSet ds = (DataSet)obten.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txt_estado_cli.Text = Convert.ToString(r[0]);
                txt_del_cli.Text = Convert.ToString(r[1]);
            }
            int cp = Convert.ToInt32(txt_cp_cli.Text);
            SqlDataSourceCombo.SelectCommand = "select 0 as d_codigo,'Selecione una Colonia'as d_asenta union all select d_codigo,d_asenta from AN_CodigoPostal  where d_codigo=" + cp;
            SqlDataSourceCombo.DataBind();
            cmbColonia_cli.DataBind();

        }
    }
    protected void EstadoCp2(object sender, EventArgs e)
    {
        SqlDataSourceCombo2.SelectCommand = "";
        SqlDataSourceCombo2.DataBind();
        cmb_coloNeg.DataBind();
        ConsBuro obten = new ConsBuro();
        obten.codigo = Convert.ToInt32(txt_cp_neg.Text);
        obten.datosCP();
        if (Convert.ToBoolean(obten.retorno[0]))
        {
            
            DataSet ds = (DataSet)obten.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txt_estado_neg.Text = Convert.ToString(r[0]);
                txt_del_neg.Text = Convert.ToString(r[1]);
            }
            int cp = Convert.ToInt32(txt_cp_neg.Text);
            SqlDataSourceCombo2.SelectCommand = "select 0 as d_codigo,'Selecione una Colonia'as d_asenta union all select d_codigo,d_asenta from AN_CodigoPostal   where d_codigo=" + cp;
            SqlDataSourceCombo2.DataBind();
            cmbColonia_cli.DataBind();
        }
    }
    protected void EstadoCp3(object sender, EventArgs e)
    {
        SqlDataSourceCombo3.SelectCommand = "";
        SqlDataSourceCombo3.DataBind();
        cmb_colonia_pr.DataBind();
        ConsBuro obten = new ConsBuro();
        obten.codigo = Convert.ToInt32(txt_cp_pr.Text);
        obten.datosCP();
        if (Convert.ToBoolean(obten.retorno[0]))
        {
            int cp = Convert.ToInt32(txt_cp_pr.Text);
            DataSet ds = (DataSet)obten.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txt_estado_pr.Text = Convert.ToString(r[0]);
                txt_del_pr.Text = Convert.ToString(r[1]);
            }
            SqlDataSourceCombo3.SelectCommand = "select 0 as d_codigo,'Selecione una Colonia'as d_asenta union all select d_codigo,d_asenta from AN_CodigoPostal   where d_codigo=" + cp;
            SqlDataSourceCombo3.DataBind();
            cmbColonia_cli.DataBind();
        }
    }
    protected void EstadoCp4(object sender, EventArgs e)
    {
        SqlDataSourceCombo4.SelectCommand = "";
        SqlDataSourceCombo4.DataBind();
        cmb_col_prove.DataBind();
        ConsBuro obten = new ConsBuro();
        obten.codigo = Convert.ToInt32(txt_cp_prove.Text);
        obten.datosCP();
        if (Convert.ToBoolean(obten.retorno[0]))
        {
            int cp = Convert.ToInt32(txt_cp_prove.Text);
            DataSet ds = (DataSet)obten.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txt_estado_provee.Text = Convert.ToString(r[0]);
                txt_del_provee.Text = Convert.ToString(r[1]);
            }
            SqlDataSourceCombo4.SelectCommand = "select d_asenta,d_codigo from an_codigopostal  where d_codigo=" + cp;
            SqlDataSourceCombo4.DataBind();
            cmbColonia_cli.DataBind();
        }
    }

    protected void EstadoCp5(object sender, EventArgs e)
    {
        SqlDataSourcePM.SelectCommand = "";
        SqlDataSourcePM.DataBind();
        cmb_col_prove.DataBind();
        ConsBuro obten = new ConsBuro();
        obten.codigo = Convert.ToInt32(txtCpPM.Text);
        obten.datosCP();
        if (Convert.ToBoolean(obten.retorno[0]))
        {
            int cp = Convert.ToInt32(txtCpPM.Text);
            DataSet ds = (DataSet)obten.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txtEstadoPM.Text = Convert.ToString(r[0]);
                txtDelMunPM.Text = Convert.ToString(r[1]);
            }
            SqlDataSourcePM.SelectCommand = "select d_asenta,d_codigo from an_codigopostal  where d_codigo=" + cp;
            SqlDataSourcePM.DataBind();
            cmbColonia_cli.DataBind();
        }
    }





    protected void cmb_preg2_ref_TextChanged(object sender, EventArgs e)
    {
        if (cmb_preg2_ref.SelectedItem.Text == "No")
        {
            txt_nomFam_ref.Text = "N/A"; txt_nomFam_ref.ReadOnly = true;
            txt_parentesco_ref.Text = "N/A"; txt_parentesco_ref.ReadOnly = true;
            txt_cargo_des_ref.Text = "N/A"; txt_cargo_des_ref.ReadOnly = true;
            txt_depen_ref.Text = "N/A"; txt_depen_ref.ReadOnly = true;
            txt_periodo_ref.Text = "N/A"; txt_periodo_ref.ReadOnly = true;
        }
        else
        {
            txt_nomFam_ref.Text = ""; txt_nomFam_ref.ReadOnly = false;
            txt_parentesco_ref.Text = ""; txt_parentesco_ref.ReadOnly = false;
            txt_cargo_des_ref.Text = ""; txt_cargo_des_ref.ReadOnly = false;
            txt_depen_ref.Text = ""; txt_depen_ref.ReadOnly = false;
            txt_periodo_ref.Text = ""; txt_periodo_ref.ReadOnly = false;
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(cmb_dom.SelectedValue == "SI")
        {
            int id_cliente = Convert.ToInt32(cmb_nombre.SelectedItem.Value);
            int[] sesiones = obtieneSesiones();
            FDat agregar = new FDat();
            agregar.empresa = sesiones[2];
            agregar.sucursal = sesiones[3];
            agregar.id_cliente = id_cliente;
            agregar.obtieneDatosFicha();
            if (Convert.ToBoolean(agregar.retorno[0]))
            {
                DataSet ds = (DataSet)agregar.retorno[1];
                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    txt_calle_neg.Text = Convert.ToString(r[1]);
                    txt_n_exterior_neg.Text = Convert.ToString(r[2]);
                    // txt_col_cli.Text = Convert.ToString(r[3]);
                    txt_del_neg.Text = Convert.ToString(r[4]);
                    txt_estado_neg.Text = Convert.ToString(r[5]);
                    txt_colo_neg.Visible = true;
                    cmb_coloNeg.Visible = false;
                    txt_colo_neg.Text = r[3].ToString();
                    txt_cp_neg.Text = Convert.ToString(r[6]);
                    txt_tel_feijo_neg.Text = Convert.ToString(r[7]);

                }

            }
        }
        else
        {
            txt_calle_neg.Text ="";
            txt_n_exterior_neg.Text = "";
            // txt_col_cli.Text = Convert.ToString(r[3]);
            txt_del_neg.Text = "";
            txt_estado_neg.Text ="";
            txt_colo_neg.Visible = false;
            cmb_coloNeg.Visible = true;
            txt_colo_neg.Text = "";
            txt_cp_neg.Text = "";
            txt_tel_feijo_neg.Text ="";
        }
    }
}

