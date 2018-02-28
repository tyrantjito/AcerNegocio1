using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using E_Utilities;
using System.Data;

public partial class PantallaPatio : System.Web.UI.Page
{
    Recepciones recepciones = new Recepciones();
    DatosPantallasPatio datosPantallas = new DatosPantallasPatio();
    DatosOrdenes datosOrdenes = new DatosOrdenes();
    Fechas fechas = new Fechas();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargaInfoEnc();
        }
    }

    private void cargaInfoEnc()
    {
        int[] sesiones = obtieneSesiones();
        if (sesiones[0] == 0 || sesiones[1] == 0 || sesiones[2] == 0 || sesiones[3] == 0 || sesiones[4] == 0 || sesiones[5] == 0)
            Response.Redirect("Default.aspx");
        try
        {
            lblEmpresa.Text = recepciones.obtieneNombreEmpresa(Request.QueryString["e"]);
            lblTallerSesion.Text = recepciones.obtieneNombreTaller(Request.QueryString["t"]);
            string gopStr = sesiones[4].ToString();
            string gopTitulo = "";
            string opcion = "";

            fechas.fecha = fechas.obtieneFechaLocal();
            fechas.tipoFormato = 4;
            string fechaRetorno = fechas.obtieneFechaConFormato();            

            if (Convert.ToInt32(sesiones[5]) == 1)
            {
                opcion = " and orp.no_orden in(select mo.no_orden from mano_obra mo where mo.id_empresa = orp.id_empresa and mo.id_taller = orp.id_taller) order by orp.no_orden desc";
                gopTitulo = "Por Iniciar Operación";
                GridView1.EmptyDataText = "Actualmente no hay ordenes con operaciones asignadas";
            }
            else if (Convert.ToInt32(sesiones[5]) == 2)
            {
                opcion = " and orp.no_orden in(select mo.no_orden from mano_obra mo where mo.id_empresa = orp.id_empresa and mo.id_taller = orp.id_taller and mo.id_grupo_op =" + sesiones[4].ToString() + " and " +
                    "mo.id_grupo_op in (select bv.id_grupo_op from bitacora_vista_patio bv where bv.no_orden = orp.no_orden and bv.id_empresa = orp.id_empresa and bv.id_taller = orp.id_taller and bv.consecutivo_bit_patio in (select top 1 bivi.consecutivo_bit_patio from Bitacora_Vista_Patio bivi where bivi.no_orden = orp.no_orden and bivi.id_empresa = orp.id_empresa and bivi.id_taller = orp.id_taller order by bivi.consecutivo_bit_patio desc))) and " +
                    "orp.no_orden not in(select s.no_orden from seguimiento_operacion s where s.id_empresa = orp.id_empresa and s.id_taller = orp.id_taller and s.id_grupo_op = " + sesiones[4].ToString() + " and s.terminado = 1) " +
                    "order by orp.no_orden desc";
                gopTitulo = datosPantallas.obtieneDescGOP(Convert.ToInt32(sesiones[4]));
                GridView1.Columns[3].Visible = GridView1.Columns[6].Visible = GridView1.Columns[7].Visible = GridView1.Columns[9].Visible = GridView1.Columns[10].Visible = GridView1.Columns[12].Visible = false;
                GridView1.EmptyDataText = "Actualmente no hay ordenes con "+gopTitulo;
            }
            else if (Convert.ToInt32(sesiones[5]) == 3)
            {
                opcion = " and orp.no_orden in(select tabla.no_orden "+
                    "from(select no_orden, (select count(tabla.gpos) from(" +
                    "select distinct id_grupo_op as gpos from Mano_Obra where no_orden = mo.no_orden and id_empresa = mo.id_empresa and id_taller = mo.id_taller" +
                    ") as tabla) as grupos," +
                    "(select count(*) from seguimiento_operacion where no_orden = mo.no_orden and terminado = 1 and id_empresa = mo.id_empresa and id_taller = mo.id_taller) as gpos_terminados " +
                    "from mano_obra mo where mo.id_empresa = " + sesiones[2].ToString() + " and mo.id_taller = " + sesiones[3].ToString() +
                    " group by mo.no_orden,mo.id_taller,mo.id_empresa) as tabla " +
                    "where tabla.gpos_terminados = tabla.grupos) order by orp.no_orden desc";
                gopTitulo = "Terminados";
                GridView1.EmptyDataText = "Actualmente no hay ordenes Terminadas";
            }
            else if (Convert.ToInt32(sesiones[5]) == 4)
            {
                opcion = " and orp.no_orden in(select tabla.no_orden " +
                    "from(select no_orden, (select count(tabla.gpos) from(" +
                    "select distinct id_grupo_op as gpos from Mano_Obra where no_orden = mo.no_orden and id_empresa = mo.id_empresa and id_taller = mo.id_taller" +
                    ") as tabla) as grupos," +
                    "(select count(*) from seguimiento_operacion where no_orden = mo.no_orden and terminado = 1 and id_empresa = mo.id_empresa and id_taller = mo.id_taller) as gpos_terminados " +
                    "from mano_obra mo where mo.id_empresa = " + sesiones[2].ToString() + " and mo.id_taller = " + sesiones[3].ToString() +
                    " group by mo.no_orden,mo.id_taller,mo.id_empresa) as tabla " +
                    "where tabla.gpos_terminados = tabla.grupos) and so.f_pactada='"+fechaRetorno+"' or "+
                    "orp.id_empresa=" + sesiones[2].ToString() + " and orp.id_taller=" + sesiones[3].ToString() +
                    " and orp.no_orden in(select tabla.no_orden " +
                    "from(select no_orden, (select count(tabla.gpos) from(" +
                    "select distinct id_grupo_op as gpos from Mano_Obra where no_orden = mo.no_orden and id_empresa = mo.id_empresa and id_taller = mo.id_taller" +
                    ") as tabla) as grupos," +
                    "(select count(*) from seguimiento_operacion where no_orden = mo.no_orden and terminado = 1 and id_empresa = mo.id_empresa and id_taller = mo.id_taller) as gpos_terminados " +
                    "from mano_obra mo where mo.id_empresa = " + sesiones[2].ToString() + " and mo.id_taller = " + sesiones[3].ToString() +
                    " group by mo.no_orden,mo.id_taller,mo.id_empresa) as tabla " +
                    "where tabla.gpos_terminados = tabla.grupos) and so.f_entrega_estimada='" + fechaRetorno + "' and so.f_entrega=null" +
                    " order by orp.no_orden desc";
                gopTitulo = "Por Entregar";
                GridView1.EmptyDataText = "Actualmente no hay ordenes terminadas para entrega el dia " + fechaRetorno;
            }            
            lblGopTitulo.Text = gopTitulo;
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectCommand = "select orp.id_empresa,orp.id_taller,orp.no_orden,tv.descripcion+' '+m.descripcion+' '+tu.descripcion as descripcion,upper(v.color_ext) as color_ext,orp.placas,l.descripcion as localizacion,C.razon_social,orp.fase_orden, so.f_recepcion, orp.no_siniestro,v.modelo,po.descripcion as perfil, isnull((case convert(varchar,so.f_pactada,120) when '1900-01-01' then '' end),'')as f_pactada,isnull((case convert(varchar,so.f_entrega_estimada,120) when '1900-01-01' then '' end),'')as f_entrega_estimada " +
                "from Ordenes_Reparacion orp " +
                "left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa = so.id_empresa and orp.id_taller = so.id_taller " +
                "left join Vehiculos v on v.id_marca = orp.id_marca and v.id_tipo_vehiculo = orp.id_tipo_vehiculo and v.id_tipo_unidad = orp.id_tipo_unidad and v.id_vehiculo = orp.id_vehiculo " +
                "left join Marcas m on m.id_marca = orp.id_marca " +
                "left join Tipo_Vehiculo tv on tv.id_tipo_vehiculo = orp.id_tipo_vehiculo " +
                "left join Tipo_Unidad tu on tu.id_marca = orp.id_marca and tu.id_tipo_vehiculo = orp.id_tipo_vehiculo and tu.id_tipo_unidad = orp.id_tipo_unidad " +
                "left join Localizaciones l on l.id_localizacion = orp.id_localizacion " +
                "left join Cliprov c on c.id_cliprov = orp.id_cliprov and c.tipo = orp.tipo_cliprov and c.tipo = 'C' " +
                "left join PerfilesOrdenes po on po.id_perfilOrden = orp.id_perfilOrden " +
                "where orp.id_empresa=" + sesiones[2].ToString() + " and orp.id_taller=" + sesiones[3].ToString() + 
                opcion;
            GridView1.DataBind();
        }
        catch (Exception)
        {
            GridView1.DataSource = null;
            GridView1.EmptyDataRowStyle.CssClass = "errores";
            GridView1.DataBind();
        }        
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
            sesiones[4] = Convert.ToInt32(Request.QueryString["gop"]);
            sesiones[5] = Convert.ToInt32(Request.QueryString["op"]);
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string fase = DataBinder.Eval(e.Row.DataItem, "fase_orden").ToString();
            var img = e.Row.Cells[10].Controls[1].FindControl("imgFase") as System.Web.UI.WebControls.Image;
            var lblFechaIni = e.Row.Cells[1].Controls[1].FindControl("lblFechaRecp") as Label;
            var lbtnNoOrden = e.Row.Cells[0].Controls[1].FindControl("lblNoOrdenGrid") as Label;
            var lblEmpresa = e.Row.Cells[13].Controls[1].FindControl("lblEmpresa") as Label;
            var lblTaller = e.Row.Cells[14].Controls[1].FindControl("lblTaller") as Label;
            var lblOperarios = e.Row.Cells[15].Controls[1].FindControl("lblOperarios") as Label;
            
            img.ImageUrl = "img/fase_" + fase + ".png";
            try
            {
                int idGops = Convert.ToInt32(Request.QueryString["gop"]);
                string empleados = datosPantallas.obtieneNombresEmpleados(lbtnNoOrden.Text, lblEmpresa.Text, lblTaller.Text, idGops);
                string[] empleadosArg = empleados.Split('/');
                decimal porcentaje = 0;
                int calificacionesTot = 0;
                string textoLabel = "";
                DataSet calificTotEmp = null;
                //string idEmpleados = datosPantallas.obtieneIdEmps(lbtnNoOrden.Text, lblEmpresa.Text, lblTaller.Text);
                string[] idEmpleadosArr = null; //idEmpleados.Split(';');
                try
                {
                    calificacionesTot = Convert.ToInt32(datosPantallas.obtieneCalificaciones());
                    int[] calificaciones = new int[calificacionesTot];
                    int[] calificacion = new int[calificacionesTot];
                    int[] idEmps = new int[idEmpleadosArr.Length];
                    int contaForeach = 0;
                    for (int conta = 0; conta < idEmps.Length; conta++)
                    {
                        calificTotEmp = datosPantallas.obtieneTotalCalEmp(lbtnNoOrden.Text, lblEmpresa.Text, lblTaller.Text, Convert.ToInt32(idEmps[conta]));
                        if (calificTotEmp != null)
                        {
                            foreach(DataRow row in calificTotEmp.Tables[0].Rows)
                            {
                                calificacion[contaForeach] = Convert.ToInt32(row[0]);
                                calificaciones[contaForeach] = Convert.ToInt32(row[1]);
                                contaForeach++;
                            }
                            porcentaje = procesaPorcCal(calificacion, calificaciones, calificacionesTot);
                            if (conta == idEmps.Length - 1)
                                textoLabel = empleadosArg + "-" + porcentaje.ToString() + "%";
                            else
                                textoLabel = empleadosArg + "-" + porcentaje.ToString() + "%" + " / ";
                        }
                    }
                }
                catch (Exception ex)
                { lblOperarios.Text = empleados; }

                lblOperarios.Text = empleados;

                string fechaIni = lblFechaIni.Text;
                int noOrden = Convert.ToInt32(lbtnNoOrden.Text);
                bool llamadaPendiente = checaEstatusLlamada(noOrden);
                if (!llamadaPendiente)
                {
                    DateTime fechaActual = fechas.obtieneFechaLocal();
                    DateTime fechaIngreso = Convert.ToDateTime(fechaIni);
                    TimeSpan hrsDif = fechaActual - fechaIngreso;
                    int hrsDiferencia = Convert.ToInt32(hrsDif.TotalHours);
                    if (hrsDiferencia < 49)
                    { }
                    else if (hrsDiferencia < 72 && hrsDiferencia > 48)
                    {
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.Yellow;
                    }
                    else if (hrsDiferencia > 72)
                    {
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {

            }
        }
    }

    private decimal procesaPorcCal(int[] calificacion, int[] calificaciones, int calificacionesTot)
    {
        decimal porcentaje = 0;
        decimal promedioTotales = 100 / calificacionesTot;
        decimal promedioDeCalificacion = 0;
        for (int cont = 0; cont < calificacionesTot; cont++)
        {
            promedioDeCalificacion = (calificacionesTot - cont) * promedioTotales;
            porcentaje = porcentaje + ((promedioDeCalificacion * calificaciones[cont]) / calificacionesTot);
        }
        return porcentaje;
    }

    private bool checaEstatusLlamada(int noOrden)
    {
        bool pendiente = false;
        try
        {
            int[] sesiones = obtieneSesiones();
            int idEmpresa = sesiones[2];
            int idTaller = sesiones[3];
            bool estatus = datosOrdenes.obtieneEstatusLlamada(noOrden, idEmpresa, idTaller);
            if (estatus)
                pendiente = true;
            else
                pendiente = false;
        }
        catch (Exception ex)
        {
            pendiente = false;
        }
        return pendiente;
    }

    protected void btnOrden_Click(object sender, EventArgs e)
    {
        LinkButton lknReferencia = (LinkButton)sender;
        int orden = Convert.ToInt32(lknReferencia.Text);
        int fase = Convert.ToInt32(lknReferencia.CommandArgument.ToString());
        Response.Redirect("BienvenidaOrdenes.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"] + "&o=" + orden + "&f=" + fase);
    }

    protected void lnkRegresarOrdenes_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ordenes.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        cargaInfoEnc();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        cargaInfoEnc();
    }
}