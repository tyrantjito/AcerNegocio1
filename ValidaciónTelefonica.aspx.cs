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
public partial class ValidaciónTelefonica : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        ValTel obt = new ValTel();
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
                txtLlamo.Text = nombre;
            }
        }
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
    protected void cmb_sucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        int empresa = sesiones[2];
        int sucursal = sesiones[3];
        int grupo = Convert.ToInt32(cmb_sucursal.SelectedValue);
        cmbNombre.SelectCommand = "select 0 as id_cliente,'Selecione Cliente' as nombre_completo union all select c.id_cliente,c.nombre_completo from AN_Clientes c inner join AN_Acta_Integraciondetalle d on c.id_cliente = d.id_cliente where d.id_sucursal=" + sucursal + " and d.id_empresa=" + empresa + " and d.id_acta=" + grupo + " and d.id_cliente in (select id_cliente from an_ficha_datos where id_empresa=" + empresa + " and id_sucursal=" + sucursal + ")";
        cmbNombre.DataBind();
        SqlDataSource1.SelectCommand = "select * from an_llamadas where id_sucursal=" + sesiones[3] + " and id_empresa=" + sesiones[2] + " and id_grupo=" + grupo;
        SqlDataSource1.DataBind();
    }

    protected void cmb_nombre_SelectedIndexChanged(object sender, EventArgs e)
    {
        ValTel agregar = new ValTel();
        int[] sesiones = obtieneSesiones();
        agregar.empresa = sesiones[2];
        agregar.sucursal = sesiones[3];
        int id_cliente = Convert.ToInt32(cmb_nombre.SelectedValue);

        agregar.id_cliente = id_cliente;

        agregar.obtieneTelefonos();
        if (Convert.ToBoolean(agregar.retorno[0]))
        {
            DataSet ds = (DataSet)agregar.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txt_tel.Text = Convert.ToString(r[0]);
                txt_tel_cel.Text = Convert.ToString(r[1]);
            }
        }


    }

    protected void agregarLlamadas(object sender, EventArgs e)
    {
        ValTel agregar = new ValTel();
        int[] sesiones = obtieneSesiones();
        agregar.empresa = sesiones[2];
        agregar.sucursal = sesiones[3];
        agregar.tipollamada = rbtnTipoLlamada.SelectedValue;
        agregar.id_grupo = Convert.ToInt32(cmb_sucursal.SelectedValue);
        agregar.id_cliente = Convert.ToInt32(cmb_nombre.SelectedValue);
        agregar.nombre_com = Convert.ToString(cmb_nombre.SelectedItem);
        agregar.fecha = txtFechaLlamada.Text;
        agregar.hora = Convert.ToString(RadTimePicker1.SelectedTime);
        agregar.usuariollamada = txtLlamo.Text;
        agregar.atendio = txtContesto.Text;
        agregar.observaciones = txtComentarioCliente.Text;
        agregar.agregarllamadas();

        borrarCampos();
        RadGrid2.DataBind();

    }

    public void borrarCampos()
    {
        cmb_sucursal.DataBind();
        cmb_nombre.DataBind();
        txtFechaLlamada.Text = "";
        RadTimePicker1.DataBind();
        txtLlamo.Text = "";
        txtContesto.Text = "";
        txtComentarioCliente.Text = "";
    }


    protected void lnkcuestionario_Click(object sender, EventArgs e)
    {
        pnlMask.Visible = true;
        windowAutorizacion.Visible = true;
        ValTel agregar = new ValTel();
        int[] sesiones = obtieneSesiones();
        agregar.empresa = sesiones[2];
        agregar.sucursal = sesiones[3];
        agregar.id_grupo = Convert.ToInt32(cmb_sucursal.SelectedValue);
        agregar.obtieneDatos();
        if (Convert.ToBoolean(agregar.retorno[0]))
        {
            DataSet ds = (DataSet)agregar.retorno[1];
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                txt_sucursal.Text = Convert.ToString(r[1]);
                txtGrupo.Text = Convert.ToString(r[6]);
                txtnumero.Text = Convert.ToString(r[7]);
                txtCiclo.Text = Convert.ToString(r[17]);
                txtP1.Text = Convert.ToString(r[6]);
            }
        }
        agregar.obtienPresidente();
        if (Convert.ToBoolean(agregar.retorno[0]))
        {
            DataSet ds2 = (DataSet)agregar.retorno[1];
            foreach (DataRow r2 in ds2.Tables[0].Rows)
            {
                txtp2.Text = Convert.ToString(r2[1]);
            }
        }
        agregar.obtienTeso();
        if (Convert.ToBoolean(agregar.retorno[0]))
        {
            DataSet ds3 = (DataSet)agregar.retorno[1];
            foreach (DataRow r2 in ds3.Tables[0].Rows)
            {
                txtp3.Text = Convert.ToString(r2[1]);
            }
        }
        agregar.idllamada = Convert.ToInt32(RadGrid2.SelectedValues["id_llamada"]);
        agregar.id_cliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        agregar.obtfechaLLamada();
        if (Convert.ToBoolean(agregar.retorno[0]))
        {
            DataSet ds4 = (DataSet)agregar.retorno[1];
            foreach (DataRow r2 in ds4.Tables[0].Rows)
            {
                txtFecha.SelectedDate = Convert.ToDateTime(r2[0]);
                if (Convert.ToChar(r2[1]) == 'N')
                {
                    txtsi1.Checked = false;
                }
                else
                {
                    txtsi1.Checked = true;
                }

                if (Convert.ToChar(r2[2]) == 'N')
                {
                    txtno1.Checked = false;
                }
                else
                {
                    txtno1.Checked = true;
                }
                if (Convert.ToChar(r2[3]) == 'N')
                {
                    txtsi2.Checked = false;
                }
                else
                {
                    txtsi2.Checked = true;
                }

                if (Convert.ToChar(r2[4]) == 'N')
                {
                    txtno2.Checked = false;
                }
                else
                {
                    txtno2.Checked = true;
                }
                if (Convert.ToChar(r2[5]) == 'N')
                {
                    txtsi3.Checked = false;
                }
                else
                {
                    txtsi3.Checked = true;
                }

                if (Convert.ToChar(r2[6]) == 'N')
                {
                    txtno3.Checked = false;
                }
                else
                {
                    txtno3.Checked = true;
                }

                txtp4.Text = Convert.ToString(r2[7]);
                txtp5.Text = Convert.ToString(r2[8]);
            }
        }

    }

    protected void RadGrid2_ItemCommand(object sender, GridCommandEventArgs e)
    {
        lnkcuestionario.Visible = true;
    }

    protected void lnkCerrar_Click(object sender, EventArgs e)
    {
        pnlMask.Visible = false;
        windowAutorizacion.Visible = false;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ValTel agregar = new ValTel();
        int[] sesiones = obtieneSesiones();
        agregar.empresa = sesiones[2];
        agregar.sucursal = sesiones[3];
        agregar.tipollamada = rbtnTipoLlamada.SelectedValue;
        agregar.id_grupo = Convert.ToInt32(cmb_sucursal.SelectedValue);
        agregar.id_cliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        DateTime fecha_cli = Convert.ToDateTime(txtFecha.SelectedDate);
        agregar.fechacues = fecha_cli.ToString("yyyy/MM/dd");
        char r1 = 'N';
        if (txtsi1.Checked)
        {
            r1 = 'S';
        }
        agregar.p1 = r1.ToString();
        char res1 = 'N';
        if (txtno1.Checked)
        {
            res1 = 'S';
        }
        agregar.res1 = res1.ToString();
        char r2 = 'N';
        if (txtsi2.Checked)
        {
            r2 = 'S';
        }
        agregar.p2 = r2.ToString();
        char res2 = 'N';
        if (txtno2.Checked)
        {
            res2 = 'S';
        }
        agregar.res2 = res2.ToString();
        char r3 = 'N';
        if (txtsi3.Checked)
        {
            r3 = 'S';
        }
        agregar.p3 = r3.ToString();
        char res3 = 'N';
        if (txtno3.Checked)
        {
            res3 = 'S';
        }
        agregar.res3 = res3.ToString();
        agregar.p4 = txtp4.Text;
        agregar.p5 = txtp5.Text;
        agregar.agregaCuesr();
        pnlMask.Visible = false;
        windowAutorizacion.Visible = false;
    }
}