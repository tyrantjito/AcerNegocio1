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

public partial class AutComite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private int[] obtieneSesiones()
    {
        int[] sesiones = new int[5];
        sesiones[0] = Convert.ToInt32(Request.QueryString["u"]);
        sesiones[1] = Convert.ToInt32(Request.QueryString["p"]);
        sesiones[2] = Convert.ToInt32(Request.QueryString["e"]);
        sesiones[3] = Convert.ToInt32(Request.QueryString["t"]);
        sesiones[4] = Convert.ToInt32(Request.QueryString["c"]);
        return sesiones;
    }
    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        SolVis edita = new SolVis();
        edita.empresa = sesiones[2];
        edita.sucursal = sesiones[3];
        int grupo = Convert.ToInt32(RadGrid1.SelectedValues["id_solicitud_credito"]);
        edita.idSolicitudEdita = grupo;
        edita.obtieneSolicitudEnc();
        if (Convert.ToBoolean(edita.retorno[0]) == true)
        {
            DataSet ds = (DataSet)edita.retorno[1];

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txt_suc_vis.Text = r[1].ToString();
                try { txtFecha_sol_vis.SelectedDate = Convert.ToDateTime(r[4]); }
                catch (Exception) { txtFecha_sol_vis.Clear(); txt_fecha_sol.DataBind(); }
                try { txt_fecha_sol.SelectedDate = Convert.ToDateTime(r[5]); }
                catch (Exception) { txt_fecha_sol.Clear(); txt_fecha_sol.DataBind(); }
                txt_gpro_vis.Text = r[6].ToString();
                txt_numGrup_vis.Text = r[7].ToString();
                txt_moncred_vis.Text = r[8].ToString();
                txt_plazo_vis.Text = r[9].ToString();
                txt_tasa_vis.Text = r[10].ToString();
                txt_garaliq_vis.Text = r[11].ToString();
                txt_mont_max.Text = r[12].ToString();
                txt_montaut_vis.Text = r[13].ToString();
                cmbplazo.SelectedValue = r[14].ToString();
                cmbtaza.SelectedValue = r[15].ToString();
                txt_formapag_vis.Text = r[15].ToString();

            }

        }
            lblgrup.Text= Convert.ToString(RadGrid1.SelectedValues["id_solicitud_credito"]);
    }

    protected void RadGrid2_SelectedIndexChanged(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        SolVis edita = new SolVis();
        edita.empresa = sesiones[2];
        edita.sucursal = sesiones[3];
        int grupo = Convert.ToInt32(RadGrid2.SelectedValues["id_solicitud_credito"]);
        int cliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        edita.id_cliente = cliente;
        edita.idSolicitudEdita = grupo;
        edita.obtieneDet();
        if (Convert.ToBoolean(edita.retorno[0]) == true)
        {
            DataSet ds = (DataSet)edita.retorno[1];

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txt_nombre_cli.Text = Convert.ToString(r[0]);
                txt_ingreso.Text = Convert.ToString(r[1]);
                txt_destino.Text = Convert.ToString(r[2]);
                txt_cred_ant.Text = Convert.ToString(r[3]);
                txt_cred_sol.Text = Convert.ToString(r[4]);
                txt_gara_liq.Text = Convert.ToString(r[5]);
                txt_cred_aut.Text = Convert.ToString(r[6]);
                txt_tel.Text= Convert.ToString(r[7]);

                txt_nombre_cli.Visible = true;
                txt_ingreso.Visible = true;
                txt_destino.Visible = true;
                txt_cred_ant.Visible = true;
                txt_cred_sol.Visible = true;
                txt_gara_liq.Visible = true;
                txt_cred_aut.Visible = true;
                txt_tel.Visible = true;
            }

        }
    
    }

    protected void lnkagregar_Click(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        SolicitudCredito agrega = new SolicitudCredito();
        agrega.empresa = sesiones[2];
        agrega.sucursal = sesiones[3];
        int grupo = Convert.ToInt32(RadGrid1.SelectedValues["id_solicitud_credito"]);
        agrega.idSolicitudEdita = grupo;
        agrega.montoAutorizado = Convert.ToDecimal( txt_montaut_vis.Text);
        agrega.plazoRC = Convert.ToInt32(cmbplazo.SelectedValue);
        agrega.tasaRC = Convert.ToDecimal(cmbtaza.SelectedValue);
        agrega.agregaEnca();
        RadGrid1.DataBind();
       

    }

    protected void lnkdetalle_Click(object sender, EventArgs e)
    {

        int[] sesiones = obtieneSesiones();
        SolVis edita = new SolVis();
        edita.empresa = sesiones[2];
        edita.sucursal = sesiones[3];
        int grupo2 = Convert.ToInt32(RadGrid2.SelectedValues["id_solicitud_credito"]);
        int cliente2 = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        edita.id_cliente = cliente2;
        edita.idSolicitudEdita = grupo2;
        edita.obtieneDet();

        if (Convert.ToBoolean(edita.retorno[0]) == true)
        {
            DataSet ds = (DataSet)edita.retorno[1];

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                int ant  = Convert.ToInt32(r[6]);
                
                SolicitudCredito agrega2 = new SolicitudCredito();
                agrega2.empresa = sesiones[2];
                agrega2.sucursal = sesiones[3];
                int grupo1 = Convert.ToInt32(RadGrid2.SelectedValues["id_solicitud_credito"]);
                int cliente1 = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
                agrega2.idSolicitudEdita = grupo1;
                agrega2.idCliente = cliente1;
                agrega2.obtieneSolicitudEnc();

                if (Convert.ToBoolean(agrega2.retorno[0]) == true)
                {
                    DataSet ds1 = (DataSet)agrega2.retorno[1];

                    foreach (DataRow r1 in ds1.Tables[0].Rows)
                    {
                        int total = 0;
                        int res = ant;


                        int sub = Convert.ToInt32(r1[13]);
                        
                        total = sub - res;
                        agrega2.montoAutorizado = Convert.ToDecimal(total);
                        agrega2.agregaEnca();

                        agrega2.creditoAutorizado = Convert.ToDecimal(txt_cred_aut.Text);

                        agrega2.idSolicitudEdita = grupo1;
                        agrega2.obtieneSolicitudEnc();
                        agrega2.AgregaDet();
                       // RadGrid2.DataBind();

                        agrega2.obtieneSolicitudEnc();
                        if (Convert.ToBoolean(agrega2.retorno[0]) == true)
                        {
                            DataSet ds3 = (DataSet)agrega2.retorno[1];

                            foreach (DataRow r2 in ds3.Tables[0].Rows)
                            {

                                decimal total3 = 0;
                                decimal sub3 = 0;
                                decimal mon = Convert.ToDecimal(txt_cred_aut.Text);
                                sub3 = Convert.ToDecimal(r2[13]);
                                total3 = sub3 + mon;
                                txt_montaut_vis.Text = Convert.ToString(total3);
                                txt_montaut_vis.DataBind();
                            }
                        }
                        agrega2.montoAutorizado = Convert.ToDecimal(txt_montaut_vis.Text);
                        agrega2.agregaEnca();
                        RadGrid1.DataBind();
                       
                    }
                }
            }
        }
        int grupoooo = Convert.ToInt32(lblgrup.Text);
        SqlDataSource1.SelectCommand = "select * from AN_Solicitud_Credito_Detalle  where id_empresa=" + sesiones[2] + " and id_sucursal=" + sesiones[3] + " and id_solicitud_credito=" + grupoooo;
        SqlDataSource1.DataBind();
       // RadGrid2.DataBind();



    }
}