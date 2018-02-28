using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI; 
using iTextSharp.text;
using iTextSharp.text.pdf;
using E_Utilities;
using System.IO;
public partial class VisitaOcular : System.Web.UI.Page
{
    Recepciones recepciones = new Recepciones();
    DatosOrdenes datosOrdenes = new DatosOrdenes();
    protected void Page_Load(object sender, EventArgs e)
    {
        txt_fecha_visita.MaxDate = DateTime.Now;
        txt_clientedesde.MaxDate = DateTime.Now;
        txtFecha_cli.MaxDate = DateTime.Now;
        txtFecha_cli.MinDate = DateTime.Now.AddYears(-70);
        txt_fecha_visita.MinDate = DateTime.Now.AddYears(-70);
        txt_clientedesde.MinDate = DateTime.Now.AddYears(-70);
        txt_nac_gara.MaxDate = DateTime.Now.AddYears(-18);

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
        lblVisita.Text = "Agrega Visita";
        pnlMask.Visible = true;
        windowAutorizacion.Visible = true;

    }
    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lnkAbreEdicion.Visible = true;
        lnkImprimir.Visible = true;
    }
    protected void lnkCerrar_Click(object sender, EventArgs e)
    {
        pnlMask.Visible = false;
        windowAutorizacion.Visible = false;
        borrarCampos();
    }
    protected void RadGrid2_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void cmbGrupo_SelectedIndexChanged(object sender, EventArgs e)
    {
     
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        int grupo = Convert.ToInt32(cmbGrupo.SelectedValue);
          cmbNombre.SelectCommand = "select 0 as id_cliente,'Selecione Integrante'as nombre_completo union all select c.id_cliente,c.nombre_completo from AN_Clientes c inner join AN_Acta_Integraciondetalle d on c.id_cliente = d.id_cliente where d.id_sucursal=" + sucursal+" and d.id_empresa="+empresa+" and d.id_acta="+grupo+" and d.id_cliente  not in (select id_cliente from AN_Visita_Ocular where id_empresa="+empresa+" and id_sucursal="+sucursal+")";
          cmbNombre.DataBind();
        txtNumeroGrupo.Text = Convert.ToString(grupo);

    }
    
    protected void agregaVisita(object sender, EventArgs e)
    {
            int[] sesiones = obtieneSesiones();
            VOcu agrega = new VOcu();
            agrega.empresa = sesiones[2];
            agrega.sucursal = sesiones[3];
            DateTime fechaV = Convert.ToDateTime(txt_fecha_visita.SelectedDate);
            agrega.fecha_visita = fechaV.ToString("yyyy/MM/dd");
            agrega.id_cliente =  Convert.ToInt32( cmb_nombre.SelectedValue);

            agrega.id_grupo = Convert.ToInt32( cmbGrupo.SelectedValue);
            agrega.grupo_productivo_visita = cmbGrupo.SelectedItem.Text;
            agrega.tipo_credito_visita = txt_tipo_credito.Text;
            agrega.gerente_sucursal_visita = txt_gerente.Text;
            agrega.asesor_credito_visita = txt_asesor.Text;
            string[] nombrecompleto = cmb_nombre.SelectedItem.Text.ToString().Split(new char[] { ' ' });
            string nombre = nombrecompleto[0];
            string apaterno = nombrecompleto[1];
            string amaterno = nombrecompleto[2];
           // agrega.nombre_visita_cli = nombre;
            //agrega.a_paterno_visita_cli = apaterno;
            //agrega.a_materno_visita_cli = amaterno;
           // DateTime fechaNC = Convert.ToDateTime(txtFecha_cli.SelectedDate);
            //agrega.fecha_nacimiento_cli = fechaNC.ToString("yyyy/MM/dd");
            agrega.edad_visita_cliente = Convert.ToInt32(txt_edad_cli.Text);
            //agrega.genero_visita_cli = Convert.ToChar(cmb_gen_cli.SelectedValue);
            //agrega.personas_dep_visita_cli = Convert.ToInt32(txt_perdep_cli.Text);
            DateTime clienteFecha = Convert.ToDateTime(txt_clientedesde.SelectedDate);
            agrega.edad_visita_cliente = Convert.ToInt32(txt_edad_cli.Text);
            agrega.cliente_desde_vis = clienteFecha.ToString("yyyy/MM/dd");
            //agrega.calle_visita_cli = txt_calle_cli.Text;
            //agrega.numero_exterior_vis_cli = txt_n_ext_cli.Text;
            //agrega.numero_interior_vis_cli = txt_n_interior_c.Text;
            //agrega.colonia_visita_cli = txt_colonia_cli.Text;
            //agrega.cp_visita_cli = txt_cp_cli.Text;
            //agrega.del_mun_visita_cli = txt_mundel_cli.Text;
            //agrega.estado_visita_cli = txt_estado_cli.Text;
            agrega.entre_calles_visita_cli = txt_entrecalles_cli.Text;
           // agrega.telefono_visita_cli = Convert.ToDecimal(txt_telefono_cli.Text);
            agrega.tipo_vivienda_visita = Convert.ToString(cmb_tipo_vivienda.SelectedValue);
            char Luzz = 'N';
            if (chk_luz.Checked)
            {
                Luzz = '✓';
            }
            agrega.luz = Luzz;

            char Aguaa = 'N';
            if (chk_agua.Checked)
            {
                Aguaa = '✓';
            }
            agrega.agua = Aguaa;

            char Drenajee = 'N';
            if (chk_drenaje.Checked)
            {
                Drenajee = '✓';
            }
            agrega.drenaje = Drenajee;

            char Telefonoo = 'N';
            if (chk_tel.Checked)
            {
                Telefonoo = '✓';
            }
            agrega.telefono = Telefonoo;

            char Internett = 'N';
            if (chk_internet.Checked)
            {
                Internett = '✓';
            }
            agrega.internet = Internett;

            char Gass = 'N';
            if (chk_gas.Checked)
            {
                Gass = '✓';
            }
            agrega.gas = Gass;

            char TVP = 'N';
            if (chk_tv.Checked)
            {
                TVP = '✓';
            }
            agrega.tv_paga = TVP;

            agrega.tipo_construccion = Convert.ToString(cmb_tipoCons.SelectedValue);
            char Salaa = 'N';
            if (chk_sala.Checked)
            {
                Salaa = '✓';
            }
            agrega.sala = Salaa;

            char Comedorr = 'N';
            if (chk_comedor.Checked)
            {
                Comedorr = '✓';
            }
            agrega.comedor = Comedorr;

            char Estufaa = 'N';
            if (chk_estufa.Checked)
            {
                Estufaa = '✓';
            }
            agrega.estufa = Estufaa;

            char Refrigeradorr = 'N';
            if (chk_refri.Checked)
            {
                Refrigeradorr = '✓';
            }
            agrega.refrigerador = Refrigeradorr;

            char Lavadoraa = 'N';
            if (chk_lavadora.Checked)
            {
                Lavadoraa = '✓';
            }
            agrega.lavadora = Lavadoraa;

            char Televisionn = 'N';
            if (chk_tele.Checked)
            {
                Televisionn = '✓';
            }
            agrega.television = Televisionn;

            char Computadoraa = 'N';
            if (chk_compu.Checked)
            {
                Computadoraa = '✓';
            }
            agrega.computadora = Computadoraa;
            agrega.auto_visita = Convert.ToString(cmb_auto.SelectedValue);
            agrega.marca_visita = txt_marca.Text;
            agrega.modelo_visita = txt_modelo.Text;
            agrega.placas_visita = txt_placas.Text;
            //agrega.calle_visita_neg = txt_calle.Text;
            //agrega.num_ext_neg = txt_next_neg.Text;
            //agrega.num_int_neg = txt_nint_neg.Text;
            //agrega.colonia_neg_visita = txt_col_neg.Text;
            //agrega.cp_visita_neg = txt_cp_neg.Text;
            //agrega.mun_del_visita_neg = txt_mundel_neg.Text;
            //agrega.estado_visita_neg = txt_estado_neg.Text;
            agrega.entre_calles_neg = txt_entre_neg.Text;

            //agrega.telefono_neg_visita = Convert.ToDecimal(txt_tel_neg.Text);
            agrega.caracteristicas_local_visita = Convert.ToString(cmb_caracte.SelectedValue);
            agrega.tiempo_neg_visita = txt_tiempo_neg.Text;
            //agrega.razon_social_neg_vis = txt_razon_neg.Text;
            //agrega.giro_negocio_visita = txt_giro_neg.Text;
            agrega.principales_proveedores_neg_vis = txt_principales.Text;
            agrega.garantia_per_neg = Convert.ToString(cmb_garan.SelectedValue);
            agrega.nombre_garantia_visi = txt_nom_gara.Text;
            agrega.a_paterno_garantia_visi = txt_apat_gara.Text;
            agrega.a_materno_garantia_visi = txt_amat_gara.Text;
            DateTime fechaNg = Convert.ToDateTime(txt_nac_gara.SelectedDate);
            agrega.fecha_nac_garantia_visi = fechaNg.ToString("yyyy/MM/dd");

            try { agrega.fecha_nac_garantia_visi = fechaNg.ToString("yyyy/MM/dd"); }
            catch (Exception) { fechaNg.ToString("2000/01/01"); }

            try { agrega.edad_garantia_visi = Convert.ToInt32(txt_edad_gara.Text); }
            catch (Exception) { txt_edad_gara.Text = Convert.ToString( 0); }
            agrega.genero_garantia = Convert.ToString(cmb_gen_gara.SelectedValue);
            agrega.ocupacion_garantia = txt_ocup_gara.Text;
            agrega.cuenta_bines_garantia = Convert.ToString(cmb_bienes_gara.SelectedValue);
            try { agrega.valor_bienes_garantia = Convert.ToInt32(txt_valor_gara.Text); }
            catch (Exception) { txt_valor_gara.Text = Convert.ToString(0); }
            agrega.calle_garantia = txt_calle_gara.Text;
            agrega.num_ext_garantia = txt_num_ext_gara.Text;
            agrega.num_int_garantia = txt_num_int_gara.Text;
            agrega.colonia_garantia = txt_col_gara.Text;
            agrega.cp_garantia = txt_cp_gara.Text;
            agrega.mun_del_garantia = txt_del_gara.Text;
            agrega.estado_garantia = txt_estado_gara.Text;
            agrega.entre_calles_garantia = txt_entre_gara.Text;
            try { agrega.telefono_garantia = Convert.ToInt32(txt_tel_gara.Text); }
            catch (Exception) { txt_tel_gara.Text = Convert.ToString(0); }
            if (lblVisita.Text == "Agrega Visita")
            {
                agrega.agregarVisita(); 
                if (Convert.ToBoolean(agrega.retorno[1]) == false)
                {
                    lblErrorAgrega.Visible = true;
                    lblErrorAgrega.Text = "Error al agregar la solicitud";
                }
                else
                {
                    RadGrid1.DataBind();
                    borrarCampos();
                    pnlMask.Visible = false;
                    windowAutorizacion.Visible = false;
                    lblErrorAgrega.Text = "Se agrego exitosamente";
                }
            }
            else
            {
                agrega.id_visita = Convert.ToInt32(RadGrid1.SelectedValues["id_visita"]);
                agrega.acutulizaVisita();

                if (Convert.ToBoolean(agrega.retorno[1]) == false)
                {
                    lblErrorAgrega.Visible = true;
                    lblErrorAgrega.Text = "Error al editar la solicitud";
                }
                else
                {
                    RadGrid1.DataBind();
                    borrarCampos();
                    pnlMask.Visible = false;
                    windowAutorizacion.Visible = false;
                    lblErrorAgrega.Text = "Se edito exitosamente";
                }
            }
            

    }

    protected void cmbPersonaSelected(object sender, EventArgs e)
    {
       int id_cliente = Convert.ToInt32( cmb_nombre.SelectedItem.Value);
        int[] sesiones = obtieneSesiones();
        VOcu agregar = new VOcu();
        agregar.empresa = sesiones[2];
        agregar.sucursal = sesiones[3];
        agregar.id_cliente = id_cliente;
        agregar.obtenerinfoFicha();
        if (Convert.ToBoolean(agregar.retorno[0]))
        {
            DataSet ds = (DataSet)agregar.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txtFecha_cli.SelectedDate = Convert.ToDateTime(r[0]);
                cmb_gen_cli.SelectedValue = Convert.ToString(r[1]);
                txt_perdep_cli.Text = Convert.ToString(r[2]);
                txt_calle_cli.Text = Convert.ToString(r[3]);
                txt_n_ext_cli.Text = Convert.ToString(r[4]);
                txt_n_interior_c.Text = Convert.ToString(r[5]);
                txt_colonia_cli.Text = Convert.ToString(r[6]);
                txt_cp_cli.Text = Convert.ToString(r[7]);
                txt_mundel_cli.Text = Convert.ToString(r[8]);
                txt_estado_cli.Text = Convert.ToString(r[9]);
                txt_telefono_cli.Text = Convert.ToString(r[10]);
                txt_calle.Text = Convert.ToString(r[11]);
                txt_next_neg.Text = Convert.ToString(r[12]);
                txt_nint_neg.Text = Convert.ToString(r[13]);
                txt_col_neg.Text = Convert.ToString(r[14]);
                txt_col_neg.Text = Convert.ToString(r[15]);
                txt_mundel_neg.Text = Convert.ToString(r[16]);
                txt_estado_neg.Text = Convert.ToString(r[17]);
                txt_tel_neg.Text = Convert.ToString(r[18]);
                txt_razon_neg.Text = Convert.ToString(r[19]);
                txt_giro_neg.Text = Convert.ToString(r[20]);

                DateTime fecha = Convert.ToDateTime(txtFecha_cli.SelectedDate);
                int edad = DateTime.Today.Year - fecha.Year;
                txt_edad_cli.Text = edad.ToString();
            }

        }
        
    }

    protected void lnkAbreEdicion_Click(object sender, EventArgs e)
    {
       lblVisita.Text = "Edita Visita";
        pnlMask.Visible = true;
        windowAutorizacion.Visible = true;
        int id_cliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
        int[] sesiones = obtieneSesiones();
        VOcu agregar = new VOcu();
        agregar.empresa = sesiones[2];
        agregar.sucursal = sesiones[3];
        agregar.id_cliente = id_cliente;
        agregar.edicionVisita();
        if (Convert.ToBoolean(agregar.retorno[0]))
        {
            DataSet ds = (DataSet)agregar.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txt_fecha_visita.SelectedDate = Convert.ToDateTime(r[5]);
                cmb_nombre.SelectedValue = Convert.ToString(r[3]);
                cmbGrupo.SelectedValue = Convert.ToString(r[4]);
                txt_tipo_credito.Text = Convert.ToString(r[7]);
                txt_gerente.Text = Convert.ToString(r[8]);
                txt_asesor.Text = Convert.ToString(r[9]);
                txt_edad_cli.Text = Convert.ToString(r[10]);
                txt_clientedesde.SelectedDate = Convert.ToDateTime(r[11]);
                txt_entrecalles_cli.Text = Convert.ToString(r[12]);
                cmb_tipo_vivienda.SelectedValue = Convert.ToString(r[13]);
                if (Convert.ToChar(r[14]) == 'N')
                {
                    chk_luz.Checked = false;
                }
                else
                {
                    chk_luz.Checked = true;
                }

                if (Convert.ToChar(r[15]) == 'N')
                {
                    chk_agua.Checked = false;
                }
                else
                {
                    chk_agua.Checked = true;
                }
                if (Convert.ToChar(r[16]) == 'N')
                {
                    chk_drenaje.Checked = false;
                }
                else
                {
                    chk_drenaje.Checked = true;
                }
                if (Convert.ToChar(r[17]) == 'N')
                {
                    chk_tel.Checked = false;
                }
                else
                {
                    chk_tel.Checked = true;
                }
                if (Convert.ToChar(r[18]) == 'N')
                {
                    chk_internet.Checked = false;
                }
                else
                {
                    chk_internet.Checked = true;
                }
                if (Convert.ToChar(r[19]) == 'N')
                {
                    chk_gas.Checked = false;
                }
                else
                {
                    chk_gas.Checked = true;
                }
                if (Convert.ToChar(r[20]) == 'N')
                {
                    chk_tv.Checked = false;
                }
                else
                {
                    chk_tv.Checked = true;
                }
                cmb_tipoCons.SelectedValue = Convert.ToString(r[21]);
                if (Convert.ToChar(r[22]) == 'N')
                {
                    chk_sala.Checked = false;
                }
                else
                {
                    chk_sala.Checked = true;
                }
                if (Convert.ToChar(r[23]) == 'N')
                {
                    chk_comedor.Checked = false;
                }
                else
                {
                    chk_comedor.Checked = true;
                }
                if (Convert.ToChar(r[24]) == 'N')
                {
                    chk_estufa.Checked = false;
                }
                else
                {
                    chk_estufa.Checked = true;
                }
                if (Convert.ToChar(r[25]) == 'N')
                {
                    chk_refri.Checked = false;
                }
                else
                {
                    chk_refri.Checked = true;
                }
                if (Convert.ToChar(r[26]) == 'N')
                {
                    chk_lavadora.Checked = false;
                }
                else
                {
                    chk_lavadora.Checked = true;
                }
                if (Convert.ToChar(r[27]) == 'N')
                {
                    chk_tele.Checked = false;
                }
                else
                {
                    chk_tele.Checked = true;
                }
                if (Convert.ToChar(r[28]) == 'N')
                {
                    chk_compu.Checked = false;
                }
                else
                {
                    chk_compu.Checked = true;
                }
                cmb_auto.SelectedValue = Convert.ToString(r[29]); ;
                txt_marca.Text = Convert.ToString(r[30]);
                txt_modelo.Text = Convert.ToString(r[31]);
                txt_placas.Text = Convert.ToString(r[32]);
                txt_entre_neg.Text = Convert.ToString(r[33]);
                cmb_caracte.SelectedValue = Convert.ToString(r[34]);
                txt_tiempo_neg.Text = Convert.ToString(r[35]);
                txt_principales.Text = Convert.ToString(r[36]);
                cmb_garan.SelectedValue = Convert.ToString(r[37]);
                txt_nom_gara.Text = Convert.ToString(r[38]);
                txt_apat_gara.Text = Convert.ToString(r[39]);
                txt_amat_gara.Text = Convert.ToString(r[40]);
                txt_nac_gara.SelectedDate = Convert.ToDateTime(r[41]);
                txt_edad_gara.Text = Convert.ToString(r[42]);
                cmb_gen_gara.SelectedValue = Convert.ToString(r[43]);
                txt_ocup_gara.Text = Convert.ToString(r[44]);
                cmb_bienes_gara.SelectedValue = Convert.ToString(r[45]);
                txt_valor_gara.Text = Convert.ToString(r[46]);
                txt_calle_gara.Text = Convert.ToString(r[47]);
                txt_num_ext_gara.Text = Convert.ToString(r[48]);
                txt_num_int_gara.Text = Convert.ToString(r[49]);
                txt_col_gara.Text = Convert.ToString(r[50]);
                txt_cp_gara.Text = Convert.ToString(r[51]);
                txt_del_gara.Text = Convert.ToString(r[52]);
                txt_estado_gara.Text = Convert.ToString(r[53]);
                txt_entre_gara.Text = Convert.ToString(r[54]);
                txt_tel_gara.Text = Convert.ToString(r[55]);

            }

        }
        agregar.obtenerinfoFicha();
        if (Convert.ToBoolean(agregar.retorno[0]))
        {
            DataSet df = (DataSet)agregar.retorno[1];
            foreach (DataRow r2 in df.Tables[0].Rows)
            {
                txtFecha_cli.SelectedDate = Convert.ToDateTime(r2[0]);
                cmb_gen_cli.SelectedValue = Convert.ToString(r2[1]);
                txt_perdep_cli.Text = Convert.ToString(r2[2]);
                txt_calle_cli.Text = Convert.ToString(r2[3]);
                txt_n_ext_cli.Text = Convert.ToString(r2[4]);
                txt_n_interior_c.Text = Convert.ToString(r2[5]);
                txt_colonia_cli.Text = Convert.ToString(r2[6]);
                txt_cp_cli.Text = Convert.ToString(r2[7]);
                txt_mundel_cli.Text = Convert.ToString(r2[8]);
                txt_estado_cli.Text = Convert.ToString(r2[9]);
                txt_telefono_cli.Text = Convert.ToString(r2[10]);
                txt_calle.Text = Convert.ToString(r2[11]);
                txt_next_neg.Text = Convert.ToString(r2[12]);
                txt_nint_neg.Text = Convert.ToString(r2[13]);
                txt_col_neg.Text = Convert.ToString(r2[14]);
                txt_col_neg.Text = Convert.ToString(r2[15]);
                txt_mundel_neg.Text = Convert.ToString(r2[16]);
                txt_estado_neg.Text = Convert.ToString(r2[17]);
                txt_tel_neg.Text = Convert.ToString(r2[18]);
                txt_razon_neg.Text = Convert.ToString(r2[19]);
                txt_giro_neg.Text = Convert.ToString(r2[20]);

            }

        }
      
    
    }
    protected void cmb_autoIndexChanged(object sender, EventArgs e)
    {
       if (cmb_auto.SelectedItem.Text == "No")
        {
            txt_marca.Text = "N/A"; txt_marca.ReadOnly = true;
            txt_modelo.Text = "N/A"; txt_modelo.ReadOnly = true;
            txt_placas.Text = "N/A"; txt_placas.ReadOnly = true;
            
        }
        else
        {
            txt_marca.Text = ""; txt_marca.ReadOnly = false;
            txt_modelo.Text = ""; txt_modelo.ReadOnly = false;
            txt_placas.Text = ""; txt_placas.ReadOnly = false;
        }
    }
    protected void cmb_bienes_garaIndexChanged(object sender, EventArgs e)
    {
       if (cmb_bienes_gara.SelectedItem.Text == "No")
        {
            txt_valor_gara.Text = "0"; txt_valor_gara.ReadOnly = true;

        }
        else
        {
            txt_valor_gara.Text = ""; txt_valor_gara.ReadOnly = false;
          
        }
    }


    public void borrarCampos()
    {
       txt_fecha_visita.Clear();
        //txtgrupo_visita.Text = "";
        txt_tipo_credito.Text = "";
        txt_gerente.Text = "";
        txt_asesor.Text = "";
        
        //txt_nombre_cli.Text = "";
       // txt_a_pat_cli.Text = "";
        //txt_amat_cli.Text = "";
        txtFecha_cli.Clear();
        txt_edad_cli.Text = "";
        txt_perdep_cli.Text = "";
        txt_clientedesde.Clear();
        txt_calle_cli.Text = "";
        txt_n_ext_cli.Text = "";
        txt_n_interior_c.Text = "";
        txt_colonia_cli.Text = "";
        txt_cp_cli.Text = "";
        txt_mundel_cli.Text = "";
        txt_estado_cli.Text = "";
        txt_entrecalles_cli.Text = "";
        txt_telefono_cli.Text = "";
        chk_luz.Checked = chk_agua.Checked = chk_drenaje.Checked = chk_tel.Checked = chk_internet.Checked = chk_gas.Checked = chk_tv.Checked = chk_sala.Checked = chk_comedor.Checked = chk_estufa.Checked = chk_refri.Checked = chk_lavadora.Checked = chk_tele.Checked=chk_compu.Checked = false;
        txt_marca.Text = "";
        txt_modelo.Text = "";
        txt_placas.Text = "";
        txt_calle.Text = "";
        txt_next_neg.Text="";
        txt_nint_neg.Text="";
        txt_col_neg.Text="";
        txt_col_neg.Text="";
        txt_mundel_neg.Text="";
        txt_estado_neg.Text="";
        txt_entre_neg.Text="";
        txt_tel_neg.Text="";
        txt_tiempo_neg.Text="";
        txt_razon_neg.Text="";
        txt_giro_neg.Text="";
        txt_principales.Text="";
        txt_nom_gara.Text="";
        txt_apat_gara.Text="";
        txt_amat_gara.Text="";
        txt_nac_gara.Clear();
        txt_edad_gara.Text="";
        txt_ocup_gara.Text="";
        txt_valor_gara.Text="";
        txt_calle_gara.Text="";
        txt_num_ext_gara.Text="";
        txt_num_int_gara.Text="";
        txt_col_gara.Text="";
        txt_del_gara.Text="";
        txt_estado_gara.Text="";
        txt_entre_gara.Text="";
        txt_tel_gara.Text="";

    }

    protected void lnkImprimir_Click(object sender, EventArgs e)
    {
        LinkButton btnImprimir = (LinkButton)sender;
        // Tipo de Font que vamos utilizar
        iTextSharp.text.Font fuente1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font fuente6 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font fuente5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //variable local fechas con un objeto tipo Fechas
        Fechas fechas = new Fechas();

        //creacion de documento
        Document documento = new Document(iTextSharp.text.PageSize.LETTER);
        documento.AddTitle(" Visita Ocular ");
        documento.AddCreator("AserNegocio1");
        string ruta = HttpContext.Current.Server.MapPath("~/files");
        string archivo = ruta + "\\VisitaOcular_" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd") + ".pdf";

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
            //logo.WidthPercentage = 15f;


            //encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.SetWidths(new float[] { 2f, 8f });
            tablaEncabezado.DefaultCell.Border = 0;
            tablaEncabezado.WidthPercentage = 100f;


            PdfPCell titulo = new PdfPCell(new Phrase("APOYO Y SERVICIO PARA EL DESARROLLO RURAL Y URBANO S.A. de C.V. SOFOM ENR " + Environment.NewLine + Environment.NewLine + " VISITA OCULAR ", FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD)));
            //PdfPCell titulo = new PdfPCell(new Phrase("Gaarve S.A de C.V. " + Environment.NewLine + Environment.NewLine + " Informe de Nomina " + Environment.NewLine + Environment.NewLine + strDatosObra, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            titulo.HorizontalAlignment = 1;
            titulo.Border = 0;
            titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.AddCell(logo);
            tablaEncabezado.AddCell(titulo);

            documento.Add(tablaEncabezado);



            VOcu infor = new VOcu();
            int[] sesiones = obtieneSesiones();
            infor.empresa = sesiones[2];
            infor.sucursal = sesiones[3];
            int id_cliente = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
            infor.id_cliente = id_cliente;
            infor.optieneimpresion();


            if (Convert.ToBoolean(infor.retorno[0]))
            {
                DataSet ds = (DataSet)infor.retorno[1];
                string fechavisita = "";
                string Sucursal = "";
                string GrupoProductivo = "";
                string Numero = "";
                string TipoCredito = "";
                string AsesorCredito = ""; ;
                string entrelascalles1 = "";
                string Gerente = "";
                string edadclien = "";
                string clientdes = "";
                string Pro = "";
                string Ren = "";
                string Pre = "";
                string Otr = "";
                string Luz = "";
                string Agua = "";
                string Drenaje = "";
                string Tel = "";
                string Internet = "";
                string Gas = "";
                string Tv = "";
                string Tab = "";
                string Mad = "";
                string Otr1 = "";

                if (Tab == "Tab")
                { Tab = "X"; }
                else { Tab = " "; }

                if (Mad == "Mad")
                { Mad = ""; }
                else
                { Mad = " "; }

                if (Otr1 == "Otr1")
                { Otr1 = "X"; }
                else
                { Otr1 = " "; }

                string Sala = "";
                string Comedor = "";
                string Estufa = "";
                string Refrigerador = "";
                string Lavadora = "";
                string Television = "";
                string Compu = "";
                string Auto = "";
                string Marca = "";
                string Modelo = "";
                string Placas = "";
                string entrelascalles2 = "";
                string tiempneg = "";
                string principrov = "";
                string Pro1 = "";
                string Ren1 = "";
                string Fij1 = "";
                string Sem1 = "";

                if (Pro1 == "Pro")
                { Pro1 = "X"; }
                else { Pro1 = " "; }

                if (Ren1 == "Ren")
                { Ren1 = ""; }
                else
                { Ren1 = " "; }

                if (Fij1 == "Fij")
                { Fij1 = "X"; }
                else
                { Fij1 = " "; }

                if (Sem1 == "Sem")
                { Sem1 = "X"; }
                else
                { Sem1 = " "; }


                string OS = "";
                string aval2 = "";

                if (OS == "O.S")
                { OS = "OBLIGADO SOLIDARIO"; }
                else { OS = " "; }

                if (aval2 == "Ava")
                { aval2 = "AVAL"; }
                else
                { aval2 = " "; }

                string nombre3 = "";
                string apep = "";
                string apem = "";
                string fenac = "";
                string edad3 = "";
                string sexo3 = "";
                string sexo4 = "";

                if (sexo3 == "H")
                { sexo3 = "X"; }
                else { sexo3 = " "; }

                if (sexo4 == "M")
                { sexo4 = "X"; }
                else
                { sexo4 = " "; }

                string ocupacion = "";
                string cbinm = "";
                string cbinm1 = "";

                if (cbinm == "S")
                { cbinm = "X"; }
                else { cbinm = " "; }

                if (cbinm1 == "N")
                { cbinm1 = "X"; }
                else
                { cbinm1 = " "; }

                string valorest = "";
                string callegp = "";
                string numext = "";
                string numint = "";
                string colonia = "";
                string copostal = "";
                string munici = "";
                string estadogp = "";
                string telobli = "";

                foreach (DataRow r in ds.Tables[0].Rows)

                {
                    fechavisita = r[5].ToString();
                    Sucursal = r[1].ToString();
                    GrupoProductivo = r[6].ToString();
                    Numero = r[4].ToString();
                    TipoCredito = r[7].ToString();
                    AsesorCredito = r[9].ToString();
                    entrelascalles1 = r[12].ToString();
                    Gerente = r[8].ToString();
                    edadclien = r[10].ToString();
                    clientdes = r[11].ToString();
                    Pro = r[13].ToString();
                    Ren = r[13].ToString();
                    Pre = r[13].ToString();
                    Otr = r[13].ToString();

                    if (Pro == "Pro")
                    { Pro = "X"; }
                    else { Pro = " "; }

                    if (Ren == "Ren")
                    { Ren = "X"; }
                    else
                    { Ren = " "; }

                    if (Pre == "Pre")
                    { Pre = "X"; }
                    else
                    { Pre = " "; }

                    if (Otr == "Otr")
                    { Otr = "X"; }
                    else
                    { Otr = " "; }
                    Luz = r[14].ToString();
                    if (Luz == "N")
                    {
                        Luz = "";
                    }
                    else
                    {
                        Luz = "X";
                    }
                    Agua = r[15].ToString();
                    if (Agua == "N")
                    {
                        Agua = "";
                    }
                    else
                    {
                        Agua = "X";
                    }
                    Drenaje = r[16].ToString();
                    if (Drenaje == "N")
                    {
                        Drenaje = "";
                    }
                    else
                    {
                        Drenaje = "X";
                    }
                    Tel = r[17].ToString();
                    if (Tel == "N")
                    {
                        Tel = "";
                    }
                    else
                    {
                        Tel = "X";
                    }
                    Internet = r[18].ToString();
                    if (Internet == "N")
                    {
                        Internet = "";
                    }
                    else
                    {
                        Internet = "X";
                    }
                    Gas = r[19].ToString();
                    if (Gas == "N")
                    {
                        Gas = "";
                    }
                    else
                    {
                        Gas = "X";
                    }
                    Tv = r[20].ToString();
                    if (Tv == "N")
                    {
                        Tv = "";
                    }
                    else
                    {
                        Tv = "X";
                    }
                    Tab = r[21].ToString();
                    Mad = r[21].ToString();
                    Otr1 = r[21].ToString();

                    if (Tab == "Tab")
                    { Tab = "X"; }
                    else { Tab = " "; }

                    if (Mad == "Mad")
                    { Mad = "X"; }
                    else
                    { Mad = " "; }

                    if (Otr1 == "Otr1")
                    { Otr1 = "X"; }
                    else
                    { Otr1 = " "; }

                    Sala = r[22].ToString();
                    if (Sala == "N")
                    {
                        Sala = "";
                    }
                    else
                    {
                        Sala = "X";
                    }
                    Comedor = r[23].ToString();
                    if (Comedor == "N")
                    {
                        Comedor = "";
                    }
                    else
                    {
                        Comedor = "X";
                    }

                    Estufa = r[24].ToString();
                    if (Sala == "N")
                    {
                        Estufa = "";
                    }
                    else
                    {
                        Estufa = "X";
                    }
                    Refrigerador = r[25].ToString();
                    if (Refrigerador == "N")
                    {
                        Refrigerador = "";
                    }
                    else
                    {
                        Refrigerador = "X";
                    }
                    Lavadora = r[26].ToString();
                    if (Lavadora == "N")
                    {
                        Lavadora = "";
                    }
                    else
                    {
                        Lavadora = "X";
                    }

                    Television = r[27].ToString();
                    if (Television == "N")
                    {
                        Television = "";
                    }
                    else
                    {
                        Television = "X";
                    }

                    Compu = r[28].ToString();
                    if (Compu == "N")
                    {
                        Compu = "";
                    }
                    else
                    {
                        Compu = "X";
                    }
                    Auto = r[29].ToString();
                    if (Auto == "No")
                    {
                        Auto = "";
                    }
                    else
                    {
                        Auto = "X";
                    }
                    Marca = r[30].ToString();
                    if (Marca == "N/A")
                    {
                        Marca = "";
                    }
                    Modelo = r[31].ToString();
                    if (Modelo == "N/A")
                    {
                        Modelo = "";
                    }
                    Placas = r[32].ToString();
                    if (Placas == "N/A")
                    {
                        Placas = "";
                    }
                    entrelascalles2 = r[33].ToString();
                    tiempneg = r[35].ToString();
                    principrov = r[36].ToString();
                    Pro1 = r[34].ToString();
                    Ren1 = r[34].ToString();
                    Fij1 = r[34].ToString();
                    Sem1 = r[34].ToString();

                    if (Pro1 == "Pro")
                    { Pro1 = "X"; }
                    else { Pro1 = " "; }

                    if (Ren1 == "Ren")
                    { Ren1 = ""; }
                    else
                    { Ren1 = " "; }

                    if (Fij1 == "Fij")
                    { Fij1 = "X"; }
                    else
                    { Fij1 = " "; }

                    if (Sem1 == "Sem")
                    { Sem1 = "X"; }
                    else
                    { Sem1 = " "; }

                    OS = r[37].ToString();
                    aval2 = r[37].ToString();

                    if (OS == "O.S")
                    { OS = "OBLIGADO SOLIDARIO"; }
                    else { OS = " "; }

                    if (aval2 == "Ava")
                    { aval2 = "AVAL"; }
                    else
                    { aval2 = " "; }

                    nombre3 = r[38].ToString();
                    apep = r[39].ToString();
                    apem = r[40].ToString();
                    fenac = r[41].ToString();
                    edad3 = r[42].ToString();
                    sexo3 = r[43].ToString();
                    sexo4 = r[43].ToString();

                    if (sexo3 == "H")
                    { sexo3 = "X"; }
                    else { sexo3 = " "; }

                    if (sexo4 == "M")
                    { sexo4 = "X"; }
                    else
                    { sexo4 = " "; }

                    ocupacion = r[44].ToString();
                    cbinm = r[45].ToString();
                    cbinm1 = r[45].ToString();

                    if (cbinm == "S")
                    { cbinm = "X"; }
                    else { cbinm = " "; }

                    if (cbinm1 == "N")
                    { cbinm1 = "X"; }
                    else
                    { cbinm1 = " "; }
                    valorest = r[46].ToString();
                    callegp = r[47].ToString();
                    numext = r[48].ToString();
                    numint = r[49].ToString();
                    colonia = r[50].ToString();
                    copostal = r[51].ToString();
                    munici = r[52].ToString();
                    estadogp = r[53].ToString();
                    telobli = r[55].ToString();



                }
                //FECHA
                PdfPTable fecha1 = new PdfPTable(2);
                fecha1.SetWidths(new float[] { 15, 25 });
                fecha1.DefaultCell.Border = 0;
                fecha1.WidthPercentage = 40f;
                fecha1.HorizontalAlignment = Element.ALIGN_LEFT;



                PdfPCell fec = new PdfPCell(new Phrase("FECHA DE VISITA ", fuente6));
                fec.VerticalAlignment = Element.ALIGN_CENTER;
                fec.BackgroundColor = BaseColor.LIGHT_GRAY;
                fecha1.AddCell(fec);

                PdfPCell fec1 = new PdfPCell(new Phrase("  " + Convert.ToDateTime(fechavisita).ToString("dd/MM/yyyy"), fuente8));
                fec1.HorizontalAlignment = Element.ALIGN_CENTER;
                fecha1.AddCell(fec1);
                documento.Add(fecha1);

                //datos 
                PdfPTable dat1 = new PdfPTable(4);
                dat1.SetWidths(new float[] { 15, 45, 20, 20 });
                dat1.DefaultCell.Border = 0;
                dat1.WidthPercentage = 100f;



                PdfPCell sucursal = new PdfPCell(new Phrase("SUCURSAL", fuente6));
                sucursal.VerticalAlignment = Element.ALIGN_CENTER;
                sucursal.BackgroundColor = BaseColor.LIGHT_GRAY;
                dat1.AddCell(sucursal);

                PdfPCell grupP = new PdfPCell(new Phrase("GRUPO PRODUCTIVO", fuente6));
                grupP.VerticalAlignment = Element.ALIGN_CENTER;
                grupP.BackgroundColor = BaseColor.LIGHT_GRAY;
                dat1.AddCell(grupP);

                PdfPCell nume = new PdfPCell(new Phrase("NÚMERO", fuente6));
                nume.VerticalAlignment = Element.ALIGN_CENTER;
                nume.BackgroundColor = BaseColor.LIGHT_GRAY;
                dat1.AddCell(nume);

                PdfPCell tipCre = new PdfPCell(new Phrase("TIPO DE CRÉDITO", fuente6));
                tipCre.VerticalAlignment = Element.ALIGN_CENTER;
                tipCre.BackgroundColor = BaseColor.LIGHT_GRAY;
                dat1.AddCell(tipCre);

                PdfPCell sucursal1 = new PdfPCell(new Phrase(" " + Sucursal, fuente8));
                sucursal1.VerticalAlignment = Element.ALIGN_CENTER;
                dat1.AddCell(sucursal1);

                PdfPCell grupP1 = new PdfPCell(new Phrase(" " + GrupoProductivo, fuente8));
                grupP1.VerticalAlignment = Element.ALIGN_CENTER;
                dat1.AddCell(grupP1);

                PdfPCell nume1 = new PdfPCell(new Phrase(" " + Numero, fuente8));
                nume1.VerticalAlignment = Element.ALIGN_CENTER;
                dat1.AddCell(nume1);

                PdfPCell tipCre1 = new PdfPCell(new Phrase(" " + TipoCredito, fuente8));
                tipCre1.VerticalAlignment = Element.ALIGN_CENTER;
                dat1.AddCell(tipCre1);
                documento.Add(dat1);

                VOcu infor1 = new VOcu();
                int[] sesiones1 = obtieneSesiones();
                infor1.empresa = sesiones[2];
                infor1.sucursal = sesiones[3];
                int id_cliente1 = Convert.ToInt32(RadGrid1.SelectedValues["id_cliente"]);
                infor1.id_cliente = id_cliente;
                infor1.optieneimpresion1();

                if (Convert.ToBoolean(infor1.retorno[0]))
                {
                    DataSet ds1 = (DataSet)infor1.retorno[1];

                    string nombres = "";
                    string app = "";
                    string apm = "";
                    string fechanacim = "";
                    string generooo = "";
                    string personasdep = "";
                    string callee = "";
                    string numexte = "";
                    string numinte = "";
                    string coloniaa = "";
                    string cpos = "";
                    string municipio = "";
                    string estadoooo = "";
                    string telefonofij = "";
                    string calle_nego = "";
                    string num_ext_neg = "";
                    string num_int_neg = "";
                    string colo_neg = "";
                    string cpos_neg = "";
                    string muni_nego = "";
                    string estado_neg = "";
                    string tel_fijo_neg = "";
                    string razon_soci_neg = "";
                    string gri_prin_neg = "";

                    foreach (DataRow r1 in ds1.Tables[0].Rows)
                    {

                        nombres = r1[0].ToString();
                        app = r1[1].ToString();
                        apm = r1[2].ToString();
                        fechanacim = r1[3].ToString();
                        generooo = r1[4].ToString();
                        if (generooo == "H")
                        {
                            generooo = "H";
                        }

                        else
                        {
                            generooo = "M";
                        }
                        personasdep = r1[5].ToString();
                        callee = r1[6].ToString();
                        numexte = r1[7].ToString();
                        numinte = r1[8].ToString();
                        coloniaa = r1[9].ToString();
                        cpos = r1[10].ToString();
                        municipio = r1[11].ToString();
                        estadoooo = r1[12].ToString();
                        telefonofij = r1[13].ToString();
                        calle_nego = r1[14].ToString();
                        num_ext_neg = r1[15].ToString();
                        num_int_neg = r1[16].ToString();
                        colo_neg = r1[17].ToString();
                        cpos_neg = r1[18].ToString();
                        muni_nego = r1[19].ToString();
                        estado_neg = r1[20].ToString();
                        tel_fijo_neg = r1[21].ToString();
                        razon_soci_neg = r1[22].ToString();
                        gri_prin_neg = r1[23].ToString();


                    }


                    // TABLA A GERENTE DE SUCURSAL
                    PdfPTable tabSA = new PdfPTable(6);
                    tabSA.SetWidths(new float[] { 15, 5, 10, 5, 10, 5 });
                    tabSA.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabSA.WidthPercentage = 50f;


                    PdfPCell gerenS = (new PdfPCell(new Phrase("GERENTE OPERATIVO", fuente6)) { Colspan = 6 });
                    gerenS.HorizontalAlignment = Element.ALIGN_CENTER;
                    gerenS.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(gerenS);

                    PdfPCell gerenS1 = (new PdfPCell(new Phrase(" " + Gerente, fuente8)) { Colspan = 6 });
                    gerenS1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabSA.AddCell(gerenS1);

                    PdfPCell datCli = (new PdfPCell(new Phrase("DATOS GENERALES DEL CLIENTE", fuente8)) { Colspan = 6 });
                    datCli.HorizontalAlignment = Element.ALIGN_CENTER;
                    datCli.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(datCli);

                    PdfPCell nomb = new PdfPCell(new Phrase("NOMBRE(S)", fuente6));
                    nomb.HorizontalAlignment = Element.ALIGN_LEFT;
                    nomb.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(nomb);

                    PdfPCell nomb1 = (new PdfPCell(new Phrase(" " + nombres, fuente6)) { Colspan = 5 });
                    nomb1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabSA.AddCell(nomb1);

                    PdfPCell apeP = new PdfPCell(new Phrase("APELLIDO PATERNO", fuente6));
                    apeP.HorizontalAlignment = Element.ALIGN_LEFT;
                    apeP.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(apeP);

                    PdfPCell apeP1 = (new PdfPCell(new Phrase(" " + app, fuente6)) { Colspan = 5 });
                    apeP1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabSA.AddCell(apeP1);

                    PdfPCell apeM = new PdfPCell(new Phrase("APELLIDO MATERNO", fuente6));
                    apeM.HorizontalAlignment = Element.ALIGN_LEFT;
                    apeM.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(apeM);

                    PdfPCell apeM1 = (new PdfPCell(new Phrase(" " + apm, fuente6)) { Colspan = 5 });
                    apeM1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabSA.AddCell(apeM1);

                    PdfPCell feNa = (new PdfPCell(new Phrase("FECHA DE NACIMIENTO", fuente6)) { Colspan = 2 });
                    feNa.HorizontalAlignment = Element.ALIGN_LEFT;
                    feNa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(feNa);

                    PdfPCell feNa1 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(fechanacim).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 4 });
                    feNa1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabSA.AddCell(feNa1);

                    PdfPCell edad = new PdfPCell(new Phrase("EDAD", fuente6));
                    edad.HorizontalAlignment = Element.ALIGN_CENTER;
                    edad.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(edad);

                    PdfPCell edad1 = (new PdfPCell(new Phrase(" " + edadclien, fuente6)) { Colspan = 5 });
                    edad1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabSA.AddCell(edad1);

                    PdfPCell sexo = (new PdfPCell(new Phrase("SEXO", fuente6)));
                    sexo.HorizontalAlignment = Element.ALIGN_CENTER;
                    sexo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(sexo);

                    PdfPCell sexo1 = (new PdfPCell(new Phrase(" ", fuente6)));
                    sexo1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabSA.AddCell(sexo1);

                    PdfPCell fame = (new PdfPCell(new Phrase("F", fuente6)));
                    fame.HorizontalAlignment = Element.ALIGN_CENTER;
                    fame.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(fame);

                    PdfPCell fame1 = (new PdfPCell(new Phrase(" " + generooo, fuente6)));
                    fame1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabSA.AddCell(fame1);

                    PdfPCell masc = (new PdfPCell(new Phrase("M", fuente6)));
                    masc.HorizontalAlignment = Element.ALIGN_CENTER;
                    masc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(masc);

                    PdfPCell masc1 = (new PdfPCell(new Phrase(" " + generooo, fuente6)));
                    masc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabSA.AddCell(masc1);

                    PdfPCell depen = (new PdfPCell(new Phrase("PERSONAS QUE DEPENDEN DE USTED", fuente6)) { Colspan = 4 });
                    depen.HorizontalAlignment = Element.ALIGN_LEFT;
                    depen.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(depen);

                    PdfPCell depen1 = (new PdfPCell(new Phrase(" " + personasdep, fuente6)) { Colspan = 2 });
                    depen1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabSA.AddCell(depen1);

                    PdfPCell clieDe = new PdfPCell(new Phrase("CLIENTE DESDE", fuente6));
                    clieDe.HorizontalAlignment = Element.ALIGN_LEFT;
                    clieDe.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabSA.AddCell(clieDe);

                    PdfPCell clieDe1 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(clientdes).ToString("dd/MM/yyyy"), fuente6)) { Colspan = 5 });
                    clieDe1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabSA.AddCell(clieDe1);


                    //TABLA ASESOR DE CREDITO B
                    PdfPTable tabAC = new PdfPTable(5);
                    tabAC.SetWidths(new float[] { 10, 10, 5, 10, 15 });
                    tabAC.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabAC.WidthPercentage = 50f;



                    PdfPCell asesor = (new PdfPCell(new Phrase("ASESOR DE CREDITO", fuente6)) { Colspan = 5 });
                    asesor.HorizontalAlignment = Element.ALIGN_CENTER;
                    asesor.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(asesor);

                    PdfPCell asesor1 = (new PdfPCell(new Phrase(" " + AsesorCredito, fuente8)) { Colspan = 5 });
                    asesor1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabAC.AddCell(asesor1);

                    PdfPCell domiCliente = (new PdfPCell(new Phrase("DOMICILIO DEL CLIENTE", fuente8)) { Colspan = 5 });
                    domiCliente.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiCliente.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(domiCliente);

                    PdfPCell calle = new PdfPCell(new Phrase("CALLE", fuente6));
                    calle.HorizontalAlignment = Element.ALIGN_LEFT;
                    calle.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(calle);

                    PdfPCell calle1 = (new PdfPCell(new Phrase(" " + callee, fuente6)) { Colspan = 4 });
                    calle1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(calle1);

                    PdfPCell numExt = new PdfPCell(new Phrase("NUM EXT", fuente6));
                    numExt.HorizontalAlignment = Element.ALIGN_LEFT;
                    numExt.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(numExt);

                    PdfPCell numExt1 = (new PdfPCell(new Phrase(" " + numexte, fuente6)) { Colspan = 2 });
                    numExt1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(numExt1);

                    PdfPCell numInt = new PdfPCell(new Phrase("NUM INT", fuente6));
                    numInt.HorizontalAlignment = Element.ALIGN_LEFT;
                    numInt.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(numInt);

                    PdfPCell numInt1 = (new PdfPCell(new Phrase(" " + numinte, fuente6)));
                    numInt1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(numInt1);

                    PdfPCell colo = new PdfPCell(new Phrase("COLONIA", fuente6));
                    colo.HorizontalAlignment = Element.ALIGN_LEFT;
                    colo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(colo);

                    PdfPCell colo1 = (new PdfPCell(new Phrase(" " + coloniaa, fuente6)) { Colspan = 2 });
                    colo1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(colo1);

                    PdfPCell codPo = new PdfPCell(new Phrase("COD POSTAL", fuente6));
                    codPo.HorizontalAlignment = Element.ALIGN_LEFT;
                    codPo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(codPo);

                    PdfPCell codPo1 = (new PdfPCell(new Phrase(" " + cpos, fuente6)));
                    codPo1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(codPo1);

                    PdfPCell muni = (new PdfPCell(new Phrase("MUNICIPIO O DELEG.", fuente6)) { Colspan = 2 });
                    muni.HorizontalAlignment = Element.ALIGN_LEFT;
                    muni.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(muni);

                    PdfPCell muni1 = (new PdfPCell(new Phrase(" " + municipio, fuente6)) { Colspan = 3 });
                    muni1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(muni1);

                    PdfPCell estado = new PdfPCell(new Phrase("ESTADO", fuente6));
                    estado.HorizontalAlignment = Element.ALIGN_LEFT;
                    estado.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(estado);

                    PdfPCell estado1 = (new PdfPCell(new Phrase(" " + estadoooo, fuente6)) { Colspan = 4 });
                    estado1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(estado1);

                    PdfPCell entrCa = (new PdfPCell(new Phrase("ENTRE LAS CALLES", fuente6)) { Colspan = 2 });
                    entrCa.HorizontalAlignment = Element.ALIGN_LEFT;
                    entrCa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(entrCa);

                    PdfPCell entrCa1 = (new PdfPCell(new Phrase(" " + entrelascalles1, fuente8)) { Colspan = 3 });
                    entrCa1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(entrCa1);

                    PdfPCell entrCa2 = (new PdfPCell(new Phrase(" ", fuente6)) { Colspan = 5 });
                    entrCa2.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabAC.AddCell(entrCa2);

                    PdfPCell telefo = (new PdfPCell(new Phrase("TELÉFONO (OBLIGATORIO)", fuente6)) { Colspan = 2 });
                    telefo.HorizontalAlignment = Element.ALIGN_LEFT;
                    telefo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabAC.AddCell(telefo);

                    PdfPCell telefo1 = (new PdfPCell(new Phrase(" " + telefonofij, fuente6)) { Colspan = 3 });
                    telefo1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabAC.AddCell(telefo1);

                    //TABLA DE GERENTE Y ASESOR (UNION DE TABBLAS)
                    PdfPTable tabACSV = new PdfPTable(2);
                    tabACSV.SetWidths(new float[] { 50, 50 });
                    tabACSV.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabACSV.WidthPercentage = 100f;

                    PdfPCell SV = (new PdfPCell(tabSA));
                    SV.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabACSV.AddCell(SV);

                    PdfPCell AC = (new PdfPCell(tabAC));
                    AC.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabACSV.AddCell(AC);
                    documento.Add(tabACSV);
                    documento.Add(new Paragraph(" "));

                    //datos socioeconomicos del cliente
                    //TIPOS DE VIVIENDA
                    PdfPTable tipsVi = new PdfPTable(2);
                    tipsVi.SetWidths(new float[] { 9, 9 });
                    tipsVi.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tipsVi.WidthPercentage = 18f;




                    PdfPCell tipV = (new PdfPCell(new Phrase("TIPO DE VIVIENDA", fuente6)) { Colspan = 2 });
                    tipV.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipV.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipsVi.AddCell(tipV);

                    PdfPCell propia = (new PdfPCell(new Phrase("PROPIA", fuente6)));
                    propia.HorizontalAlignment = Element.ALIGN_CENTER;
                    propia.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipsVi.AddCell(propia);

                    PdfPCell propia1 = (new PdfPCell(new Phrase(" " + Pro, fuente8)));
                    propia1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipsVi.AddCell(propia1);

                    PdfPCell renta = (new PdfPCell(new Phrase("RENTADA", fuente6)));
                    renta.HorizontalAlignment = Element.ALIGN_CENTER;
                    renta.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipsVi.AddCell(renta);

                    PdfPCell renta1 = (new PdfPCell(new Phrase(" " + Ren, fuente8)));
                    renta1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipsVi.AddCell(renta1);

                    PdfPCell prest = (new PdfPCell(new Phrase("PRESTADA", fuente6)));
                    prest.HorizontalAlignment = Element.ALIGN_CENTER;
                    prest.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipsVi.AddCell(prest);

                    PdfPCell prest1 = (new PdfPCell(new Phrase(" " + Pre, fuente8)));
                    prest1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipsVi.AddCell(prest1);

                    PdfPCell otroV = (new PdfPCell(new Phrase("OTRO", fuente6)));
                    otroV.HorizontalAlignment = Element.ALIGN_CENTER;
                    otroV.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipsVi.AddCell(otroV);

                    PdfPCell otroV1 = (new PdfPCell(new Phrase(" " + Otr, fuente8)));
                    otroV1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipsVi.AddCell(otroV1);

                    PdfPCell otroV2 = (new PdfPCell(new Phrase(" ", fuente8)) { Colspan = 2 });
                    otroV2.HorizontalAlignment = Element.ALIGN_CENTER;
                    otroV2.BorderWidthTop = 0;
                    otroV2.BorderWidthRight = 0;
                    otroV2.BorderWidthLeft = 0;
                    otroV2.BorderWidthBottom = 0;
                    tipsVi.AddCell(otroV2);





                    //SERVICIOS
                    PdfPTable servi = new PdfPTable(2);
                    servi.SetWidths(new float[] { 9, 9 });
                    servi.HorizontalAlignment = Element.ALIGN_RIGHT;
                    servi.WidthPercentage = 18f;





                    PdfPCell servi1 = (new PdfPCell(new Phrase("SERVICIOS", fuente6)) { Colspan = 2 });
                    servi1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(servi1);

                    PdfPCell luz = (new PdfPCell(new Phrase("LUZ", fuente6)));
                    luz.HorizontalAlignment = Element.ALIGN_CENTER;
                    luz.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(luz);

                    PdfPCell luz1 = (new PdfPCell(new Phrase(" " + Luz, fuente8)));
                    luz1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi.AddCell(luz1);

                    PdfPCell agua = (new PdfPCell(new Phrase("AGUA", fuente6)));
                    agua.HorizontalAlignment = Element.ALIGN_CENTER;
                    agua.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(agua);

                    PdfPCell agua1 = (new PdfPCell(new Phrase(" " + Agua, fuente8)));
                    agua1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi.AddCell(agua1);

                    PdfPCell drena = (new PdfPCell(new Phrase("DRENAJE", fuente6)));
                    drena.HorizontalAlignment = Element.ALIGN_CENTER;
                    drena.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(drena);

                    PdfPCell drena1 = (new PdfPCell(new Phrase(" " + Drenaje, fuente8)));
                    drena1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi.AddCell(drena1);

                    PdfPCell callTel = (new PdfPCell(new Phrase("TELÉFONO", fuente6)));
                    callTel.HorizontalAlignment = Element.ALIGN_CENTER;
                    callTel.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(callTel);

                    PdfPCell callTel1 = (new PdfPCell(new Phrase(" " + Tel, fuente8)));
                    callTel1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi.AddCell(callTel1);

                    PdfPCell internet = (new PdfPCell(new Phrase("INTERNET", fuente6)));
                    internet.HorizontalAlignment = Element.ALIGN_CENTER;
                    internet.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(internet);

                    PdfPCell internet1 = (new PdfPCell(new Phrase(" " + Internet, fuente8)));
                    internet1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi.AddCell(internet1);

                    PdfPCell gas = (new PdfPCell(new Phrase("GAS", fuente6)));
                    gas.HorizontalAlignment = Element.ALIGN_CENTER;
                    gas.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(gas);

                    PdfPCell gas1 = (new PdfPCell(new Phrase(" " + Gas, fuente8)));
                    gas1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi.AddCell(gas1);

                    PdfPCell ppv = (new PdfPCell(new Phrase("TV DE PAGA", fuente6)));
                    ppv.HorizontalAlignment = Element.ALIGN_CENTER;
                    ppv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    servi.AddCell(ppv);

                    PdfPCell ppv1 = (new PdfPCell(new Phrase(" " + Tv, fuente8)));
                    ppv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    servi.AddCell(ppv1);

                    //TIPO DE CONSTRUCCIONES
                    PdfPTable tipCo = new PdfPTable(2);
                    tipCo.SetWidths(new float[] { 9, 9 });
                    tipCo.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tipCo.WidthPercentage = 18f;



                    PdfPCell tipoCo = (new PdfPCell(new Phrase("TIPO DE CONSTRUCCIÓN", fuente6)) { Colspan = 2 });
                    tipoCo.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipoCo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipCo.AddCell(tipoCo);

                    PdfPCell tabique = (new PdfPCell(new Phrase("TABIQUE", fuente6)));
                    tabique.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabique.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipCo.AddCell(tabique);

                    PdfPCell tabique1 = (new PdfPCell(new Phrase(" " + Tab, fuente8)));
                    tabique1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipCo.AddCell(tabique1);

                    PdfPCell madera = (new PdfPCell(new Phrase("MADERA", fuente6)));
                    madera.HorizontalAlignment = Element.ALIGN_CENTER;
                    madera.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipCo.AddCell(madera);

                    PdfPCell madera1 = (new PdfPCell(new Phrase(" " + Mad, fuente8)));
                    madera1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipCo.AddCell(madera1);

                    PdfPCell otroCon = (new PdfPCell(new Phrase("OTRO", fuente6)));
                    otroCon.HorizontalAlignment = Element.ALIGN_CENTER;
                    otroCon.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipCo.AddCell(otroCon);

                    PdfPCell otroCon1 = (new PdfPCell(new Phrase(" " + Otr1, fuente8)));
                    otroCon1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipCo.AddCell(otroCon1);



                    PdfPCell space = (new PdfPCell(new Phrase(" ", fuente8)) { Colspan = 2 });
                    space.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipCo.AddCell(space);

                    PdfPCell space2 = (new PdfPCell(new Phrase(" ", fuente8)) { Colspan = 2 });
                    space2.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipCo.AddCell(space2);



                    //APARATOS ELECTRODOMESTICOS
                    PdfPTable aparaEl = new PdfPTable(2);
                    aparaEl.SetWidths(new float[] { 20, 8 });
                    aparaEl.HorizontalAlignment = Element.ALIGN_RIGHT;
                    aparaEl.WidthPercentage = 28f;




                    PdfPCell aemv = (new PdfPCell(new Phrase("APARATOS ELECTRODOMÉSTICOS Y MUEBLES QUE CUENTA LA VIVIENDA", fuente6)) { Colspan = 2 });
                    aemv.HorizontalAlignment = Element.ALIGN_CENTER;
                    aemv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(aemv);

                    PdfPCell sala = (new PdfPCell(new Phrase("SALA", fuente6)));
                    sala.HorizontalAlignment = Element.ALIGN_CENTER;
                    sala.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(sala);

                    PdfPCell sala1 = (new PdfPCell(new Phrase(" " + Sala, fuente8)));
                    sala1.HorizontalAlignment = Element.ALIGN_CENTER;
                    aparaEl.AddCell(sala1);

                    PdfPCell comedor = (new PdfPCell(new Phrase("COMEDOR", fuente6)));
                    comedor.HorizontalAlignment = Element.ALIGN_CENTER;
                    comedor.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(comedor);

                    PdfPCell comedor1 = (new PdfPCell(new Phrase(" " + Comedor, fuente8)));
                    comedor1.HorizontalAlignment = Element.ALIGN_CENTER;
                    aparaEl.AddCell(comedor1);

                    PdfPCell estufa = (new PdfPCell(new Phrase("ESTUFA", fuente6)));
                    estufa.HorizontalAlignment = Element.ALIGN_CENTER;
                    estufa.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(estufa);

                    PdfPCell estufa1 = (new PdfPCell(new Phrase(" " + Estufa, fuente8)));
                    estufa1.HorizontalAlignment = Element.ALIGN_CENTER;
                    aparaEl.AddCell(estufa1);

                    PdfPCell refri = (new PdfPCell(new Phrase("REFRIGERADOR", fuente6)));
                    refri.HorizontalAlignment = Element.ALIGN_CENTER;
                    refri.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(refri);

                    PdfPCell refri1 = (new PdfPCell(new Phrase(" " + Refrigerador, fuente8)));
                    refri1.HorizontalAlignment = Element.ALIGN_CENTER;
                    aparaEl.AddCell(refri1);

                    PdfPCell lavad = (new PdfPCell(new Phrase("LAVADORA", fuente6)));
                    lavad.HorizontalAlignment = Element.ALIGN_CENTER;
                    lavad.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(lavad);

                    PdfPCell lavad1 = (new PdfPCell(new Phrase(" " + Lavadora, fuente8)));
                    lavad1.HorizontalAlignment = Element.ALIGN_CENTER;
                    aparaEl.AddCell(lavad1);

                    PdfPCell telev = (new PdfPCell(new Phrase("TELEVISIÓN", fuente6)));
                    telev.HorizontalAlignment = Element.ALIGN_CENTER;
                    telev.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(telev);

                    PdfPCell telev1 = (new PdfPCell(new Phrase(" " + Television, fuente8)));
                    telev1.HorizontalAlignment = Element.ALIGN_CENTER;
                    aparaEl.AddCell(telev1);

                    PdfPCell comput = (new PdfPCell(new Phrase("COMPUTADORA", fuente6)));
                    comput.HorizontalAlignment = Element.ALIGN_CENTER;
                    comput.BackgroundColor = BaseColor.LIGHT_GRAY;
                    aparaEl.AddCell(comput);

                    PdfPCell comput1 = (new PdfPCell(new Phrase(" " + Compu, fuente8)));
                    comput1.HorizontalAlignment = Element.ALIGN_CENTER;
                    aparaEl.AddCell(comput1);

                    //OTROS BIENES
                    PdfPTable obie = new PdfPTable(2);
                    obie.SetWidths(new float[] { 9, 9 });
                    obie.HorizontalAlignment = Element.ALIGN_RIGHT;
                    obie.WidthPercentage = 18f;



                    PdfPCell otherBie = (new PdfPCell(new Phrase("OTROS BIENES PARTICULARES", fuente6)) { Colspan = 2 });
                    otherBie.HorizontalAlignment = Element.ALIGN_CENTER;
                    otherBie.BackgroundColor = BaseColor.LIGHT_GRAY;
                    obie.AddCell(otherBie);

                    PdfPCell carro = (new PdfPCell(new Phrase("AUTO", fuente6)));
                    carro.HorizontalAlignment = Element.ALIGN_CENTER;
                    carro.BackgroundColor = BaseColor.LIGHT_GRAY;
                    obie.AddCell(carro);

                    PdfPCell carro1 = (new PdfPCell(new Phrase(" " + Auto, fuente8)));
                    carro1.HorizontalAlignment = Element.ALIGN_CENTER;
                    obie.AddCell(carro1);

                    PdfPCell marca = (new PdfPCell(new Phrase("MARCA", fuente6)));
                    marca.HorizontalAlignment = Element.ALIGN_CENTER;
                    marca.BackgroundColor = BaseColor.LIGHT_GRAY;
                    obie.AddCell(marca);

                    PdfPCell marca1 = (new PdfPCell(new Phrase(" " + Marca, fuente8)));
                    marca1.HorizontalAlignment = Element.ALIGN_CENTER;
                    obie.AddCell(marca1);

                    PdfPCell modelo = (new PdfPCell(new Phrase("MODELO", fuente6)));
                    modelo.HorizontalAlignment = Element.ALIGN_CENTER;
                    modelo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    obie.AddCell(modelo);

                    PdfPCell modelo1 = (new PdfPCell(new Phrase(" " + Modelo, fuente8)));
                    modelo1.HorizontalAlignment = Element.ALIGN_CENTER;
                    obie.AddCell(modelo1);

                    PdfPCell placas = (new PdfPCell(new Phrase("PLACAS", fuente6)));
                    placas.HorizontalAlignment = Element.ALIGN_CENTER;
                    placas.BackgroundColor = BaseColor.LIGHT_GRAY;
                    obie.AddCell(placas);

                    PdfPCell placas1 = (new PdfPCell(new Phrase(" " + Placas, fuente8)));
                    placas1.HorizontalAlignment = Element.ALIGN_CENTER;
                    obie.AddCell(placas1);

                    PdfPCell espac = (new PdfPCell(new Phrase(" ", fuente8)));
                    espac.HorizontalAlignment = Element.ALIGN_CENTER;
                    espac.BorderWidth = 0;
                    obie.AddCell(espac);

                    PdfPCell espac1 = (new PdfPCell(new Phrase(" ", fuente8)));
                    espac1.HorizontalAlignment = Element.ALIGN_CENTER;
                    espac1.BorderWidth = 0;
                    obie.AddCell(espac1);

                    //tabla principal une tablas de tip de vivienda, serviccios, tipo de const, aparatos elect, otros bieness
                    PdfPTable tipDaEco = new PdfPTable(5);
                    tipDaEco.SetWidths(new float[] { 18, 18, 18, 28, 18 });
                    tipDaEco.HorizontalAlignment = Element.ALIGN_CENTER;
                    tipDaEco.DefaultCell.Border = 0;
                    tipDaEco.WidthPercentage = 100f;

                    PdfPCell titu = (new PdfPCell(new Phrase("DATOS SOCIOECONOMICOS DEL CLIENTE (BIENES MUEBLES E INMUEBLES PROPIEDAD DEL CLIENTE) MARQUE CON UNA X", fuente5)) { Colspan = 5 });
                    titu.HorizontalAlignment = Element.ALIGN_CENTER;
                    titu.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tipDaEco.AddCell(titu);

                    PdfPCell ecoVivi = (new PdfPCell(tipsVi));
                    ecoVivi.HorizontalAlignment = Element.ALIGN_LEFT;
                    ecoVivi.Border = 0;
                    tipDaEco.AddCell(ecoVivi);

                    PdfPCell ecoServi = (new PdfPCell(servi));
                    ecoServi.HorizontalAlignment = Element.ALIGN_LEFT;
                    ecoServi.Border = 0;
                    tipDaEco.AddCell(ecoServi);

                    PdfPCell ecoConst = (new PdfPCell(tipCo));
                    ecoConst.HorizontalAlignment = Element.ALIGN_LEFT;
                    ecoConst.Border = 0;
                    tipDaEco.AddCell(ecoConst);

                    PdfPCell ecoApara = (new PdfPCell(aparaEl));
                    ecoApara.HorizontalAlignment = Element.ALIGN_LEFT;
                    ecoApara.Border = 0;
                    tipDaEco.AddCell(ecoApara);

                    PdfPCell ecoBienes = (new PdfPCell(obie));
                    ecoBienes.HorizontalAlignment = Element.ALIGN_LEFT;
                    ecoBienes.Border = 0;
                    tipDaEco.AddCell(ecoBienes);
                    documento.Add(tipDaEco);
                    documento.Add(new Paragraph(" "));

                    //domicilio del negocio

                    PdfPTable domiNeg = new PdfPTable(4);
                    domiNeg.SetWidths(new float[] { 10, 20, 10, 10 });
                    domiNeg.HorizontalAlignment = Element.ALIGN_RIGHT;
                    domiNeg.WidthPercentage = 50f;



                    PdfPCell doNego = (new PdfPCell(new Phrase("DOMICILIO DEL NEGOCIO", fuente8)) { Colspan = 4 });
                    doNego.HorizontalAlignment = Element.ALIGN_CENTER;
                    doNego.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(doNego);

                    PdfPCell calleNeg = (new PdfPCell(new Phrase("CALLE", fuente6)));
                    calleNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    calleNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(calleNeg);

                    PdfPCell calleNeg1 = (new PdfPCell(new Phrase(" " + calle_nego, fuente6)) { Colspan = 3 });
                    calleNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(calleNeg1);

                    PdfPCell nExtN = (new PdfPCell(new Phrase("NUM. EXT", fuente6)));
                    nExtN.HorizontalAlignment = Element.ALIGN_CENTER;
                    nExtN.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(nExtN);

                    PdfPCell nExtN1 = (new PdfPCell(new Phrase(" " + num_ext_neg, fuente6)));
                    nExtN1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(nExtN1);

                    PdfPCell nIntN = (new PdfPCell(new Phrase("NUM. INT", fuente6)));
                    nIntN.HorizontalAlignment = Element.ALIGN_CENTER;
                    nIntN.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(nExtN);

                    PdfPCell nIntN1 = (new PdfPCell(new Phrase(" " + num_int_neg, fuente6)));
                    nIntN1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(nIntN1);

                    PdfPCell colNeg = (new PdfPCell(new Phrase("COLONIA", fuente6)));
                    colNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    colNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(colNeg);

                    PdfPCell colNeg1 = (new PdfPCell(new Phrase(" " + colo_neg, fuente6)));
                    colNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(colNeg1);

                    PdfPCell cpNeg = (new PdfPCell(new Phrase("C.P.", fuente6)));
                    cpNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    cpNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(cpNeg);

                    PdfPCell cpNeg1 = (new PdfPCell(new Phrase(" " + cpos_neg, fuente6)));
                    cpNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(cpNeg1);

                    PdfPCell delNeg = (new PdfPCell(new Phrase("MUNICIPIO O DELEGACIÓN", fuente6)) { Colspan = 2 });
                    delNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    delNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(delNeg);

                    PdfPCell delNeg1 = (new PdfPCell(new Phrase(" " + muni_nego, fuente6)) { Colspan = 2 });
                    delNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(delNeg1);

                    PdfPCell estaNeg = (new PdfPCell(new Phrase("ESTADO", fuente6)));
                    estaNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    estaNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(estaNeg);

                    PdfPCell estaNeg1 = (new PdfPCell(new Phrase(" " + estado_neg, fuente6)) { Colspan = 3 });
                    estaNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(estaNeg1);

                    PdfPCell entreNeg = (new PdfPCell(new Phrase("ENTRE LAS CALLES", fuente6)));
                    entreNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    entreNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(entreNeg);

                    PdfPCell entreNeg1 = (new PdfPCell(new Phrase(" " + entrelascalles2, fuente8)) { Colspan = 3 });
                    entreNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(entreNeg1);

                    PdfPCell entreNeg2 = (new PdfPCell(new Phrase(" ", fuente8)) { Colspan = 4 });
                    entreNeg2.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(entreNeg2);

                    PdfPCell telNeg = (new PdfPCell(new Phrase("TELÉFONO (OBLIGATORIO)", fuente6)) { Colspan = 2 });
                    telNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    telNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    domiNeg.AddCell(telNeg);

                    PdfPCell telNeg1 = (new PdfPCell(new Phrase(" " + tel_fijo_neg, fuente6)) { Colspan = 2 });
                    telNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    domiNeg.AddCell(telNeg1);

                    //CARACTERISTICAS DEL LOCAL
                    PdfPTable CarLoc = new PdfPTable(4);
                    CarLoc.SetWidths(new float[] { 20, 5, 20, 5 });
                    CarLoc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    CarLoc.WidthPercentage = 50f;



                    PdfPCell carLo = (new PdfPCell(new Phrase("CARACTERISTICAS DEL LOCAL", fuente8)) { Colspan = 4 });
                    carLo.HorizontalAlignment = Element.ALIGN_CENTER;
                    carLo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(carLo);

                    PdfPCell propioLoc = (new PdfPCell(new Phrase("PROPIO", fuente6)));
                    propioLoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    propioLoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(propioLoc);

                    PdfPCell propioLoc1 = (new PdfPCell(new Phrase(" " + Pro1, fuente8)));
                    propioLoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(propioLoc1);

                    PdfPCell rentLoc = (new PdfPCell(new Phrase("RENTADO", fuente6)));
                    rentLoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    rentLoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(rentLoc);

                    PdfPCell rentLoc1 = (new PdfPCell(new Phrase(" " + Ren1, fuente8)));
                    rentLoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(rentLoc1);

                    PdfPCell fijoLoc = (new PdfPCell(new Phrase("FIJO", fuente6)));
                    fijoLoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    fijoLoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(fijoLoc);

                    PdfPCell fijoLoc1 = (new PdfPCell(new Phrase(" " + Fij1, fuente8)));
                    fijoLoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(fijoLoc1);

                    PdfPCell semiLoc = (new PdfPCell(new Phrase("SEMIFIJO", fuente6)));
                    semiLoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    semiLoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(semiLoc);

                    PdfPCell semiLoc1 = (new PdfPCell(new Phrase(" " + Sem1, fuente8)));
                    semiLoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(semiLoc1);

                    PdfPCell espacio = (new PdfPCell(new Phrase(" ", fuente8)) { Colspan = 4 });
                    espacio.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(espacio);

                    PdfPCell timeNeg = (new PdfPCell(new Phrase("TIEMPO CON EL NEGOCIO", fuente6)) { Colspan = 2 });
                    timeNeg.HorizontalAlignment = Element.ALIGN_CENTER;
                    timeNeg.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(timeNeg);

                    PdfPCell timeNeg1 = (new PdfPCell(new Phrase(" " + tiempneg, fuente8)) { Colspan = 2 });
                    timeNeg1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(timeNeg1);

                    PdfPCell razLoc = (new PdfPCell(new Phrase("RAZÓN SOCIAL", fuente6)) { Colspan = 2 });
                    razLoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    razLoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(razLoc);

                    PdfPCell razLoc1 = (new PdfPCell(new Phrase(" " + razon_soci_neg, fuente6)) { Colspan = 2 });
                    razLoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(razLoc1);

                    PdfPCell giroLoc = (new PdfPCell(new Phrase("GIRO DEL NEGOCIO", fuente6)) { Colspan = 2 });
                    giroLoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    giroLoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(giroLoc);

                    PdfPCell giroLoc1 = (new PdfPCell(new Phrase(" " + gri_prin_neg, fuente6)) { Colspan = 2 });
                    giroLoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(giroLoc1);

                    PdfPCell prinLoc = (new PdfPCell(new Phrase("PRINCIPALES PROVEEDORES", fuente6)) { Colspan = 2 });
                    prinLoc.HorizontalAlignment = Element.ALIGN_CENTER;
                    prinLoc.BackgroundColor = BaseColor.LIGHT_GRAY;
                    CarLoc.AddCell(prinLoc);

                    PdfPCell prinLoc1 = (new PdfPCell(new Phrase(" " + principrov, fuente8)) { Colspan = 2 });
                    prinLoc1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(prinLoc1);

                    PdfPCell prinLoc2 = (new PdfPCell(new Phrase(" ", fuente6)) { Colspan = 4 });
                    prinLoc2.HorizontalAlignment = Element.ALIGN_CENTER;
                    CarLoc.AddCell(prinLoc2);

                    //TERCERA TABLA PARA UNIR TABLAS
                    PdfPTable terTable = new PdfPTable(2);
                    terTable.SetWidths(new float[] { 50, 50 });
                    terTable.HorizontalAlignment = Element.ALIGN_RIGHT;
                    terTable.WidthPercentage = 100f;

                    PdfPCell titulo1 = (new PdfPCell(new Phrase("DATOS GENERALES DEL NEGOCIO", fuente8)) { Colspan = 2 });
                    titulo1.HorizontalAlignment = Element.ALIGN_CENTER;
                    titulo1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    terTable.AddCell(titulo1);

                    PdfPCell NegoDom = (new PdfPCell(domiNeg));
                    NegoDom.HorizontalAlignment = Element.ALIGN_LEFT;
                    terTable.AddCell(NegoDom);

                    PdfPCell NegoCara = (new PdfPCell(CarLoc));
                    NegoCara.HorizontalAlignment = Element.ALIGN_RIGHT;
                    terTable.AddCell(NegoCara);
                    documento.Add(terTable);

                    //CROQUIS DE LOCALIZACIÓN
                    PdfPTable croLoc = new PdfPTable(2);
                    croLoc.SetWidths(new float[] { 50, 50 });
                    croLoc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    croLoc.WidthPercentage = 100f;

                    PdfPCell croquis = (new PdfPCell(new Phrase("CROQUIS DE LOCALIZACIÓN", fuente6)) { Colspan = 2 });
                    croquis.HorizontalAlignment = Element.ALIGN_CENTER;
                    croquis.BackgroundColor = BaseColor.LIGHT_GRAY;
                    croLoc.AddCell(croquis);

                    PdfPCell casaCro = (new PdfPCell(new Phrase("CASA", fuente6)));
                    casaCro.HorizontalAlignment = Element.ALIGN_CENTER;
                    casaCro.BackgroundColor = BaseColor.LIGHT_GRAY;
                    croLoc.AddCell(casaCro);

                    PdfPCell negCro = (new PdfPCell(new Phrase("NEGOCIO", fuente6)));
                    negCro.HorizontalAlignment = Element.ALIGN_CENTER;
                    negCro.BackgroundColor = BaseColor.LIGHT_GRAY;
                    croLoc.AddCell(negCro);
                    documento.Add(croLoc);

                    //imagen de croquis
                    PdfPTable croquisImg = new PdfPTable(1);
                    croquisImg.WidthPercentage = 100f;

                    string imagepath2 = HttpContext.Current.Server.MapPath("img/");
                    iTextSharp.text.Image croquisIm = iTextSharp.text.Image.GetInstance(imagepath2 + "cro_visita.png");

                    croquisImg.AddCell(croquisIm);
                    documento.Add(croquisImg);
                    documento.Add(new Paragraph(" "));
                    documento.Add(new Paragraph(" "));

                    //obligado solidario
                    PdfPTable garan1 = new PdfPTable(6);
                    garan1.SetWidths(new float[] { 15, 5, 5, 10, 5, 10 });
                    garan1.HorizontalAlignment = Element.ALIGN_RIGHT;
                    garan1.WidthPercentage = 50f;

                    PdfPCell obliSol = (new PdfPCell(new Phrase("OBLIGADO SOLIDARIO", fuente6)) { Colspan = 2 });
                    obliSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    obliSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(obliSol);




                    PdfPCell obliSol1 = (new PdfPCell(new Phrase(" " + OS, fuente8)) { Colspan = 4 });
                    obliSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(obliSol1);

                    PdfPCell nameSol = (new PdfPCell(new Phrase("NOMBRE(S)", fuente6)) { Colspan = 2 });
                    nameSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    nameSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(nameSol);

                    PdfPCell nameSol1 = (new PdfPCell(new Phrase(" " + nombre3, fuente8)) { Colspan = 4 });
                    nameSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(nameSol1);

                    PdfPCell apePSol = (new PdfPCell(new Phrase("APELLIDO PATERNO", fuente6)) { Colspan = 2 });
                    apePSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    apePSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(apePSol);

                    PdfPCell apePSol1 = (new PdfPCell(new Phrase(" " + apep, fuente8)) { Colspan = 4 });
                    apePSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(apePSol1);

                    PdfPCell apeMSol = (new PdfPCell(new Phrase("APELLIDO MATERNO", fuente6)) { Colspan = 2 });
                    apeMSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    apeMSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(apeMSol);

                    PdfPCell apeMSol1 = (new PdfPCell(new Phrase(" " + apem, fuente8)) { Colspan = 4 });
                    apeMSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(apeMSol1);

                    PdfPCell fechSol = (new PdfPCell(new Phrase("FECHA DE NACIMIENTO", fuente6)) { Colspan = 2 });
                    fechSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    fechSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(fechSol);

                    PdfPCell fechSol1 = (new PdfPCell(new Phrase(" " + Convert.ToDateTime(fenac).ToString("dd/MM/yyyy"), fuente8)) { Colspan = 4 });
                    fechSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(fechSol1);

                    PdfPCell edadSol = (new PdfPCell(new Phrase("EDAD", fuente6)));
                    edadSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    edadSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(edadSol);

                    PdfPCell edadSol1 = (new PdfPCell(new Phrase(" " + edad3, fuente8)) { Colspan = 5 });
                    edadSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(edadSol1);

                    PdfPCell sexoSol = (new PdfPCell(new Phrase("SEXO", fuente6)));
                    sexoSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    sexoSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(sexoSol);

                    PdfPCell sexoSol1 = (new PdfPCell(new Phrase(" ", fuente8)));
                    sexoSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(sexoSol1);

                    PdfPCell fameSol = (new PdfPCell(new Phrase("F", fuente6)));
                    fameSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    fameSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(fameSol);

                    PdfPCell fameSol1 = (new PdfPCell(new Phrase(" " + sexo4, fuente8)));
                    fameSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(fameSol1);

                    PdfPCell maleSol = (new PdfPCell(new Phrase("M", fuente6)));
                    maleSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    maleSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(maleSol);

                    PdfPCell maleSol1 = (new PdfPCell(new Phrase(" " + sexo3, fuente8)));
                    maleSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(maleSol1);

                    PdfPCell ocupSol = (new PdfPCell(new Phrase("OCUPACIÓN", fuente6)));
                    ocupSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    ocupSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(ocupSol);

                    PdfPCell ocupSol1 = (new PdfPCell(new Phrase(" " + ocupacion, fuente8)) { Colspan = 5 });
                    ocupSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(ocupSol1);

                    PdfPCell cuentaSol = (new PdfPCell(new Phrase("CUENTA CON BIENES INMUEBLES", fuente6)));
                    cuentaSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    cuentaSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(cuentaSol);

                    PdfPCell cuentaSol1 = (new PdfPCell(new Phrase(" ", fuente8)));
                    cuentaSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(cuentaSol1);

                    PdfPCell siSol = (new PdfPCell(new Phrase("SI", fuente6)));
                    siSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    siSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(siSol);

                    PdfPCell siSol1 = (new PdfPCell(new Phrase(" " + cbinm, fuente8)));
                    siSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(siSol1);

                    PdfPCell noSol = (new PdfPCell(new Phrase("NO", fuente6)));
                    noSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    noSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(noSol);

                    PdfPCell noSol1 = (new PdfPCell(new Phrase(" " + cbinm1, fuente8)));
                    noSol1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan1.AddCell(noSol1);

                    PdfPCell valoSol = (new PdfPCell(new Phrase("VALOR ESTIMADO EN BIENES", fuente6)) { Colspan = 4 });
                    valoSol.HorizontalAlignment = Element.ALIGN_CENTER;
                    valoSol.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan1.AddCell(valoSol);
                    PdfPCell valoSol1 = (new PdfPCell(new Phrase("" + valorest, fuente8)) { Colspan = 2 });
                    valoSol1.HorizontalAlignment = Element.ALIGN_LEFT;
                    garan1.AddCell(valoSol1);

                    //tabla aval
                    PdfPTable garan2 = new PdfPTable(5);
                    garan2.SetWidths(new float[] { 10, 10, 10, 10, 10 });
                    garan2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    garan2.WidthPercentage = 50f;




                    PdfPCell aval = (new PdfPCell(new Phrase("AVAL", fuente6)));
                    aval.HorizontalAlignment = Element.ALIGN_CENTER;
                    aval.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(aval);

                    PdfPCell aval1 = (new PdfPCell(new Phrase(" " + aval2, fuente8)) { Colspan = 4 });
                    aval1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(aval1);

                    PdfPCell calleAv = (new PdfPCell(new Phrase("CALLE", fuente6)));
                    calleAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    calleAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(calleAv);

                    PdfPCell calleAv1 = (new PdfPCell(new Phrase(" " + callegp, fuente8)) { Colspan = 4 });
                    calleAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(calleAv1);

                    PdfPCell numExtAv = (new PdfPCell(new Phrase("NUM. EXT", fuente6)));
                    numExtAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    numExtAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(numExtAv);

                    PdfPCell numExtAv1 = (new PdfPCell(new Phrase(" " + numext, fuente8)));
                    numExtAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(numExtAv1);

                    PdfPCell numIntAv = (new PdfPCell(new Phrase("NUM. INT", fuente6)));
                    numIntAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    numIntAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(numIntAv);

                    PdfPCell numIntAv1 = (new PdfPCell(new Phrase(" " + numint, fuente8)) { Colspan = 2 });
                    numIntAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(numIntAv1);

                    PdfPCell colAv = (new PdfPCell(new Phrase("COLONIA", fuente6)));
                    colAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    colAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(colAv);

                    PdfPCell colAv1 = (new PdfPCell(new Phrase(" " + colonia, fuente8)) { Colspan = 2 });
                    colAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(colAv1);

                    PdfPCell cpAv = (new PdfPCell(new Phrase("C.P.", fuente6)));
                    cpAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    cpAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(cpAv);

                    PdfPCell cpAv1 = (new PdfPCell(new Phrase(" " + copostal, fuente8)));
                    cpAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(cpAv1);

                    PdfPCell munAv = (new PdfPCell(new Phrase("MUNICIPIO O DELEGACIÓN", fuente6)) { Colspan = 2 });
                    munAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    munAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(munAv);

                    PdfPCell munAv1 = (new PdfPCell(new Phrase(" " + munici, fuente8)) { Colspan = 3 });
                    munAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(munAv1);

                    PdfPCell estaAv = (new PdfPCell(new Phrase("ESTADO", fuente6)));
                    estaAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    estaAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(estaAv);

                    PdfPCell estaAv1 = (new PdfPCell(new Phrase(" " + estadogp, fuente8)) { Colspan = 4 });
                    estaAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(estaAv1);

                    PdfPCell entreAv = (new PdfPCell(new Phrase("ENTRE CALLES", fuente6)) { Colspan = 2 });
                    entreAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    entreAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(entreAv);

                    PdfPCell entreAv1 = (new PdfPCell(new Phrase(" ", fuente8)) { Colspan = 3 });
                    entreAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(entreAv1);

                    PdfPCell entreAv2 = (new PdfPCell(new Phrase(" ", fuente8)) { Colspan = 5 });
                    entreAv2.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(entreAv2);

                    PdfPCell telAv = (new PdfPCell(new Phrase("TEL (OBLIGATORIO)", fuente6)) { Colspan = 2 });
                    telAv.HorizontalAlignment = Element.ALIGN_CENTER;
                    telAv.BackgroundColor = BaseColor.LIGHT_GRAY;
                    garan2.AddCell(telAv);

                    PdfPCell telAv1 = (new PdfPCell(new Phrase(" " + telobli, fuente8)) { Colspan = 3 });
                    telAv1.HorizontalAlignment = Element.ALIGN_CENTER;
                    garan2.AddCell(telAv1);

                    PdfPCell spacer = (new PdfPCell(new Phrase(" ", fuente6)) { Colspan = 2 });
                    spacer.HorizontalAlignment = Element.ALIGN_CENTER;
                    spacer.BorderWidth = 0;
                    garan2.AddCell(spacer);

                    PdfPCell spacer1 = (new PdfPCell(new Phrase(" ", fuente8)) { Colspan = 3 });
                    spacer1.HorizontalAlignment = Element.ALIGN_CENTER;
                    spacer1.BorderWidth = 0;
                    garan2.AddCell(spacer1);


                    //UNION DE TABLAS (TRES)
                    PdfPTable tabCuat = new PdfPTable(2);
                    tabCuat.SetWidths(new float[] { 50, 50 });
                    tabCuat.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabCuat.WidthPercentage = 100f;

                    PdfPCell encaCuat = (new PdfPCell(new Phrase("GARANTIAS PERSONALES")) { Colspan = 2 });
                    encaCuat.HorizontalAlignment = Element.ALIGN_CENTER;
                    encaCuat.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabCuat.AddCell(encaCuat);

                    PdfPCell tabCuat1 = (new PdfPCell(garan1));
                    tabCuat1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabCuat.AddCell(tabCuat1);

                    PdfPCell tabCuat2 = (new PdfPCell(garan2));
                    tabCuat2.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabCuat.AddCell(tabCuat2);
                    documento.Add(tabCuat);
                    documento.Add(new Paragraph(" "));

                    //TABLA DE LA FOTO
                    PdfPTable tabfoto = new PdfPTable(1);
                    tabfoto.SetWidths(new float[] { 100 });
                    tabfoto.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabfoto.WidthPercentage = 80f;

                    PdfPCell fotoT = new PdfPCell(new Phrase(" "));
                    fotoT.HorizontalAlignment = Element.ALIGN_CENTER;
                    fotoT.FixedHeight = 200f;
                    tabfoto.AddCell(fotoT);
                    documento.Add(tabfoto);

                    //PIE DE FOTO
                    PdfPTable pieFoto = new PdfPTable(2);
                    pieFoto.SetWidths(new float[] { 5, 75 });
                    pieFoto.HorizontalAlignment = Element.ALIGN_CENTER;
                    pieFoto.WidthPercentage = 80f;

                    PdfPCell anexFo = (new PdfPCell(new Phrase("ANEXAR FOTOGRAFIAS DE CASA Y NEGOCIO")) { Colspan = 2 });
                    anexFo.HorizontalAlignment = Element.ALIGN_CENTER;
                    anexFo.BackgroundColor = BaseColor.LIGHT_GRAY;
                    pieFoto.AddCell(anexFo);

                    PdfPCell nota1 = (new PdfPCell(new Phrase("Nota 2", fuente4)));
                    nota1.HorizontalAlignment = Element.ALIGN_CENTER;
                    nota1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    pieFoto.AddCell(nota1);

                    PdfPCell notaA = (new PdfPCell(new Phrase("DOMICILIO COMPARTIDO: a) Se debe visualizar la fachada completa del domicilio; b) El cliente debe de estar dentro del domicilio", fuente4)));
                    notaA.HorizontalAlignment = Element.ALIGN_CENTER;
                    pieFoto.AddCell(notaA);

                    PdfPCell nota2 = (new PdfPCell(new Phrase("Nota 3", fuente4)));
                    nota2.HorizontalAlignment = Element.ALIGN_CENTER;
                    nota2.BackgroundColor = BaseColor.LIGHT_GRAY;
                    pieFoto.AddCell(nota2);

                    PdfPCell notaB = (new PdfPCell(new Phrase("DOMICILIO: a) Se debe visualizar toda la fachada del domicilio; b) Que se visualicen todas las viviendas y su independecia.", fuente4)));
                    notaB.HorizontalAlignment = Element.ALIGN_CENTER;
                    pieFoto.AddCell(notaB);

                    PdfPCell nota3 = (new PdfPCell(new Phrase("Nota 4", fuente4)));
                    nota3.HorizontalAlignment = Element.ALIGN_CENTER;
                    nota3.BackgroundColor = BaseColor.LIGHT_GRAY;
                    pieFoto.AddCell(nota3);

                    PdfPCell notaC = (new PdfPCell(new Phrase("NEGOCIO: Se debe visualizar toda la fachada del negocio. El negocio debe de estar operando. Se debe de observar toda la mercancia.", fuente4)));
                    notaC.HorizontalAlignment = Element.ALIGN_CENTER;
                    pieFoto.AddCell(notaC);
                    documento.Add(pieFoto);

                    documento.Add(new Paragraph(" "));


                    //Firmas
                    PdfPTable firmas1 = new PdfPTable(3);
                    firmas1.SetWidths(new float[] { 35, 30, 35 });
                    firmas1.HorizontalAlignment = Element.ALIGN_CENTER;
                    firmas1.DefaultCell.Border = 0;
                    firmas1.WidthPercentage = 95f;

                    PdfPCell raya1 = (new PdfPCell(new Phrase("\n \n  \n ______________________________________________\n \n NOMBRE Y FIRMA DEL GERENTE OPERATIVO \n \n", fuente4)));
                    raya1.HorizontalAlignment = Element.ALIGN_CENTER;
                    raya1.BorderColor = BaseColor.BLUE;
                    firmas1.AddCell(raya1);

                    PdfPCell raya5 = (new PdfPCell(new Phrase(" ", fuente4)));
                    raya5.HorizontalAlignment = Element.ALIGN_CENTER;
                    raya5.BorderWidth = 0;
                    firmas1.AddCell(raya5);

                    PdfPCell raya2 = (new PdfPCell(new Phrase("\n \n  \n ______________________________________________ \n \n NOMBRE Y FIRMA DEL CLIENTE \n \n", fuente4)));
                    raya2.HorizontalAlignment = Element.ALIGN_CENTER;
                    raya2.BorderColor = BaseColor.BLUE;
                    firmas1.AddCell(raya2);


                    documento.Add(firmas1);



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

    protected void cmb_garan_SelectedIndexChanged(object sender, EventArgs e)
    {
        string gara = Convert.ToString( cmb_garan.SelectedValue);
        if (gara == "N/A")
        {
           
            txt_nom_gara.Visible = false;
            txt_apat_gara.Visible = false;
            txt_amat_gara.Visible = false;
        
            txt_edad_gara.Visible = false;
            cmb_gen_gara.Visible = false;
            txt_ocup_gara.Visible = false;
            cmb_bienes_gara.Visible = false;
            txt_valor_gara.Visible = false;
            txt_calle_gara.Visible = false;
            txt_num_ext_gara.Visible = false;
            txt_num_int_gara.Visible = false;
            txt_col_gara.Visible = false;
            txt_cp_gara.Visible = false;
            txt_del_gara.Visible = false;
            txt_estado_gara.Visible = false;
            txt_entre_gara.Visible = false;
            txt_tel_gara.Visible = false;



        }
        else
        {
            
            txt_nom_gara.Visible = true;
            txt_apat_gara.Visible = true;
            txt_amat_gara.Visible = true;
            txt_nac_gara.Visible = true;
            txt_edad_gara.Visible = true;
            cmb_gen_gara.Visible = true;
            txt_ocup_gara.Visible = true;
            cmb_bienes_gara.Visible = true;
            txt_valor_gara.Visible = true;
            txt_calle_gara.Visible = true;
            txt_num_ext_gara.Visible = true;
            txt_num_int_gara.Visible = true;
            txt_col_gara.Visible = true;
            txt_cp_gara.Visible = true;
            txt_del_gara.Visible = true;
            txt_estado_gara.Visible = true;
            txt_entre_gara.Visible = true;
            txt_tel_gara.Visible = true;
        }
    }
}