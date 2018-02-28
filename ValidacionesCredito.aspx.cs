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

public partial class ValidacionesCredito : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        ValCre agr = new ValCre();
        agr.empresa = sesiones[2];
        agr.sucursal = sesiones[3];
        agr.credito = sesiones[4];
        agr.obtienVal();
        int nval = 0;
        if (Convert.ToBoolean(agr.retorno[0]))
        {
            DataSet ds1 = (DataSet)agr.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                nval = Convert.ToInt32(r1[0]);
            }
        }

        agr.obtieneAprovadas();
        string apro = "";
        int cont = 0;
        if (Convert.ToBoolean(agr.retorno[0]))
        {
            DataSet ds2 = (DataSet)agr.retorno[1];

            foreach (DataRow r2 in ds2.Tables[0].Rows)
            {
                apro = Convert.ToString(r2[0]);
                if (apro == "CUMPLE")
                {
                    cont++;
                }
                else { }

            }

            if (cont == nval)
            {
                agr.ESTATUScREDITO();
            }
            else
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

            var btnAprovar = r[0].ToString();
            var status = r[3].ToString();
            //img.ImageUrl = "img/fase_" + fase + ".png";
            try
            {
                if (status == "CUM")
                {
                    var btn1 = e.Item.Cells[0].Controls[0].FindControl("btnAprovar") as LinkButton;
                    var btn2 = e.Item.Cells[0].Controls[0].FindControl("btnNOAprovar") as LinkButton;
                    var btn3 = e.Item.Cells[0].Controls[0].FindControl("btnEditar") as LinkButton;

                    btn1.Visible = true;
                    btn2.Visible = false;
                    btn3.Visible = true;
                    btn3.CssClass = "btn btn-danger textoBold colorBlanco";
                }
                else if (status == "NCU")
                {
                    var btn1 = e.Item.Cells[0].Controls[0].FindControl("btnAprovar") as LinkButton;
                    var btn2 = e.Item.Cells[0].Controls[0].FindControl("btnNOAprovar") as LinkButton;
                    var btn3 = e.Item.Cells[0].Controls[0].FindControl("btnEditar") as LinkButton;

                    btn1.Visible = false;
                    btn2.Visible = true;
                    btn3.Visible = true;
                    btn3.CssClass = "btn btn-grey textoBold colorBlanco";

                }
                else
                {
                    var btn3 = e.Item.Cells[0].Controls[0].FindControl("btnEditar") as LinkButton;
                    btn3.Visible = false;
                }


            }
            catch (Exception ex)
            {

            }
        }
    }

    protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void lnkAgregarNuevo_Click(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        ValCre agr = new ValCre();
        agr.empresa = sesiones[2];
        agr.sucursal = sesiones[3];
        agr.credito = sesiones[4];
        int pol = Convert.ToInt32(txtValiId.Text);
        int cliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        agr.id_Val = pol;
        agr.cliente = cliente;
        agr.Observacion = txtObservacion.Text;
        agr.estatus = "CUM";
        agr.estatuscom = "CUMPLE";
        agr.editaValidacion();
        RadGrid1.DataBind();
        string script = "cierraNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
        SqlDataSource1.SelectCommand = " select c.id_validacion,c.Descripcion,g.Observacion,g.Estatus from AN_Validaciones_Credito c left join (select * from AN_ValidacionesGrupos where Id_cliente = " + cliente + " )  g on c.id_validacion = g.id_validacion   ";
        SqlDataSource1.DataBind();
        lblint.Text = cliente.ToString();
    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        ValCre agr = new ValCre();
        agr.empresa = sesiones[2];
        agr.sucursal = sesiones[3];
        agr.credito = sesiones[4];
        int pol = Convert.ToInt32(txtidVali.Text);
        int cliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        agr.id_Val = pol;
        agr.cliente = cliente;
        agr.Observacion = txtObservacionEDT.Text;
        agr.estatus = "NCU";
        agr.estatuscom = "NO CUMPLE";
        agr.editaValidacion();
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
        SqlDataSource1.SelectCommand = " select c.id_validacion,c.Descripcion,g.Observacion,g.Estatus from AN_Validaciones_Credito c left join (select * from AN_ValidacionesGrupos where Id_cliente = " + cliente + " )  g on c.id_validacion = g.id_validacion   ";
        SqlDataSource1.DataBind();
        lblint.Text = cliente.ToString();
    }

    protected void btnAprovar_Click(object sender, EventArgs e)
    {

    }

    protected void btnEditar_Click1(object sender, EventArgs e)
    {
        string script = "abreModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abre", script, true);
        LinkButton aut = (LinkButton)sender;
        string[] arg = aut.CommandArgument.ToString().Split(new char[] { ';' });
        int Val = Convert.ToInt16(arg[0]);
        txtidVali.Text = Val.ToString();
        BtnActualizar.Visible = false;
        btnActua.Visible = true;

    }


    protected void btnActua_Click(object sender, EventArgs e)
    {
        int[] sesiones = obtieneSesiones();
        ValCre agr = new ValCre();
        agr.credito = sesiones[4];
        int cliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
        int val = Convert.ToInt32( txtidVali.Text);
        agr.cliente = cliente;
        agr.id_Val = val;
        agr.obtieneValidacion();
        string estatus = "";
        if (Convert.ToBoolean(agr.retorno[0]))
        {
            DataSet ds1 = (DataSet)agr.retorno[1];

            foreach (DataRow r1 in ds1.Tables[0].Rows)
            {
                estatus = r1[0].ToString();

                if (estatus == "CUM")
                {
                    agr.Observacion = txtObservacionEDT.Text;
                    agr.estatus = "NCU";
                    agr.estatuscom = "NO CUMPLE";
                    agr.editaValidacion();
                }
                else
                {
                    agr.Observacion = txtObservacionEDT.Text;
                    agr.estatus = "CUM";
                    agr.estatuscom = "CUMPLE";
                    agr.editaValidacion();
                }

            }
        }
        RadGrid1.DataBind();
        string script = "cierraModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cierra", script, true);
        SqlDataSource1.SelectCommand = " select c.id_validacion,c.Descripcion,g.Observacion,g.Estatus from AN_Validaciones_Credito c left join (select * from AN_ValidacionesGrupos where Id_cliente = " + cliente + " )  g on c.id_validacion = g.id_validacion   ";
        SqlDataSource1.DataBind();
        lblint.Text = cliente.ToString();

    }

    protected void btnAprovar_Click1(object sender, EventArgs e)
    {
        string script = "abreNewEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abre", script, true);
        LinkButton aut = (LinkButton)sender;
        string[] arg = aut.CommandArgument.ToString().Split(new char[] { ';' });
        int pol = Convert.ToInt16(arg[0]);
        txtValiId.Text = pol.ToString();
    }

    protected void btnNOAprovar_Click(object sender, EventArgs e)
    {
        string script = "abreModEmi()";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abre", script, true);
        LinkButton aut = (LinkButton)sender;
        string[] arg = aut.CommandArgument.ToString().Split(new char[] { ';' });
        int pol = Convert.ToInt16(arg[0]);
        txtidVali.Text = pol.ToString();

    }

 

    protected void btnValidacion_Click(object sender, EventArgs e)
    {
        ValCre obt = new ValCre();
        int[] sesiones = obtieneSesiones();
        obt.credito = sesiones[4];
        obt.obtienenvalidacion();
         int cliente = Convert.ToInt32(RadGrid2.SelectedValues["id_cliente"]);
         obt.cliente = cliente;
         int tValidacion = 0;
         if (Convert.ToBoolean(obt.retorno[0]))
         {
             DataSet ds1 = (DataSet)obt.retorno[1];

             foreach (DataRow r1 in ds1.Tables[0].Rows)
             {
                 tValidacion = Convert.ToInt32(r1[0]);

             }

             string ok = "";
             string estatus = "";
             int i = 1;


            for (i = 1; i <= tValidacion; i++)
            {
                ValCre obt2 = new ValCre();
                obt2.credito = sesiones[4];
                obt2.id_Val = i;
                obt2.cliente = cliente;
                obt2.obtieneestatusvalidacion();
                if (Convert.ToBoolean(obt2.retorno[0]))
             {
               
                DataSet ds2 = (DataSet)obt2.retorno[1];
                 foreach (DataRow r2 in ds2.Tables[0].Rows)
                         {
               
                    estatus = Convert.ToString(r2[0]);
                    
                         if (estatus == "CUM")
                         {
                             ok = "listo";
                         }
                         else {
                            ok = "no";
                             i = tValidacion + 1;
                         }
                     }
                    
                 }
                  }
            if (ok == "listo")
            {
                obt.editaEsatutus();
            }
            else
            {
                obt.editaEsatutus2();
            }

        }

          pnlMask.Visible = true;
          windowAutorizacion.Visible = true;
         SqlDataSource1.SelectCommand = " select c.id_validacion,c.Descripcion,g.Observacion,g.Estatus from AN_Validaciones_Credito c left join (select * from AN_ValidacionesGrupos where Id_cliente = " + cliente + " )  g on c.id_validacion = g.id_validacion   ";
         SqlDataSource1.DataBind();
         lblint.Text = cliente.ToString();

       
    }

    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        pnlMask.Visible = false;
        windowAutorizacion.Visible = false;
        RadGrid2.DataBind();
    }



    protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
    {

        if (e.Item is GridDataItem)
        {
            DataRowView filas = (DataRowView)e.Item.DataItem;
            DataRow r = filas.Row;
            //string fase = DataBinder.Eval(e.DataItem, "fase_orden").ToString();
            //var img = e.Row.Cells[10].Controls[1].FindControl("imgFase") as System.Web.UI.WebControls.Image;

            var btnID = r[0].ToString();
            var status = r[3].ToString();
            //img.ImageUrl = "img/fase_" + fase + ".png";
            try
            {
                if (status == "CUMPLE")
                {
                    var btn1 = e.Item.Cells[0].Controls[0].FindControl("btnID") as LinkButton;

                    btn1.Visible = true;
                    btn1.CssClass = "btn btn-grey textoBold colorBlanco";
                }
                else
                {
                    var btn1 = e.Item.Cells[0].Controls[0].FindControl("btnID") as LinkButton;
                    btn1.Visible = true;
                    btn1.CssClass = "btn btn-danger textoBold colorBlanco";
                }


            }
            catch (Exception ex)
            {

           }
        }

    }
}