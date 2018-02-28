using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using E_Utilities;

public partial class CalendarioPersonal : System.Web.UI.Page
{
    Fechas fechas = new Fechas();
    private Hashtable informacion;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            lblOrdAsig.Text = "Seleccione un operario para mostrar su información";
            Panel3.Visible = false;
            Panel5.Visible = false;
        }
    }
    protected void lnkBuscarOpe_Click(object sender, EventArgs e)
    {
        if (txtBuscaOperario.Text != "")
            SqlDataSource1.SelectCommand = "SELECT Empleados.IdEmp, Empleados.LLave_nombre_empleado, Empleados.Puesto, Puestos.descripcion FROM Empleados LEFT OUTER JOIN Puestos ON Puestos.id_puesto = Empleados.Puesto where Empleados.status_empleado <>'B' and Empleados.tipo_empleado in ('EX','IN') AND Empleados.LLave_nombre_empleado like '%" + txtBuscaOperario.Text + "%'";
        GridView1.SelectedIndex = -1;
        GridView1.DataBind();
        lblOrdAsig.Text = "Seleccione un operario para mostrar su información";
        Panel3.Visible = false;
        Panel5.Visible = false;
        
    }
    protected void hpkLimpiar_Click(object sender, EventArgs e)
    {
        txtBuscaOperario.Text = "";
        SqlDataSource1.SelectCommand = "SELECT Empleados.IdEmp, Empleados.LLave_nombre_empleado, Empleados.Puesto, Puestos.descripcion FROM Empleados LEFT OUTER JOIN Puestos ON Puestos.id_puesto = Empleados.Puesto where Empleados.status_empleado <>'B' and Empleados.tipo_empleado in ('EX','IN')";
        GridView1.SelectedIndex = -1;
        GridView1.DataBind();
        lblOrdAsig.Text = "Seleccione un operario para mostrar su información";
        Panel3.Visible = false;
        Panel5.Visible = false;
       
    }
    protected void lnkSeleccionar_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] valores = btn.CommandArgument.ToString().Split(new char[] { ';' });
        lblOrdAsig.Text = "Ordenes de " + valores[1];
        lblIdOper.Text = valores[0];
        Panel3.Visible = true;
        DateTime fechaLocal = fechas.obtieneFechaLocal();
        DateTime inicio = Convert.ToDateTime("01/" + fechaLocal.Month.ToString().PadLeft(2, '0') + "/" + fechaLocal.Year.ToString().PadLeft(4, '0'));
        DateTime fin = inicio.AddMonths(1);
        fin = fin.AddDays(-1);

        informacion = obtieneOrdenes(valores[0], inicio, fin);
        
        Calendar1.SelectedDate = DateTime.Today;
        Calendar1.FirstDayOfWeek = FirstDayOfWeek.Sunday;
        Calendar1.NextPrevFormat = NextPrevFormat.ShortMonth;
        Calendar1.TitleFormat = TitleFormat.MonthYear;
        Calendar1.ShowGridLines = true;
        Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Left;
        Calendar1.DayStyle.VerticalAlign = VerticalAlign.Top;
        Calendar1.DayStyle.Height = new Unit(75);
        Calendar1.DayStyle.Width = new Unit(100);
        Calendar1.OtherMonthDayStyle.BackColor = Color.LightGray;
        Calendar1.OtherMonthDayStyle.ForeColor = Color.Gray;
        Calendar1.SelectedDayStyle.BackColor = Color.Cyan;
        Calendar1.SelectedDayStyle.ForeColor = Color.Blue;
        Panel5.Visible = false;
    }

    private Hashtable obtieneOrdenes(string empleado, DateTime ini, DateTime fin) {
        Hashtable _ordenes = new Hashtable();
        Calendarizacion calendario = new Calendarizacion();
        calendario.Empresa = Convert.ToInt32(Request.QueryString["e"]);
        calendario.Empleado = Convert.ToInt32(empleado);
        calendario.Taller = Convert.ToInt32(Request.QueryString["t"]);
        calendario.FIni = ini.ToString("yyyy-MM-dd");
        calendario.FFin = fin.ToString("yyyy-MM-dd");
        calendario.obtieneOrdenesEmpleado();
        string arreglo = "";
        string fecha_ant = "";
        int cont = 0;
        int iteracion = 0;
        object[] info = calendario.Datos;
        if (Convert.ToBoolean(info[0])) {
            DataSet datos = (DataSet)info[1];
            foreach (DataRow row in datos.Tables[0].Rows) {
                if (row[1].ToString() != "") {
                    if (row[1].ToString() != fecha_ant)
                    {
                        if (cont != 0)
                            _ordenes[fecha_ant] = arreglo;
                        
                            arreglo = row[0].ToString();
                            _ordenes[row[1]] = row[0];
                            fecha_ant = row[1].ToString();
                            cont = 0;
                        
                    }
                    else
                    {
                        if (cont == 0)
                        {
                            arreglo = arreglo.Trim() + "; " + row[0].ToString();
                            cont++;
                        }
                        else
                        {
                            arreglo = arreglo.Trim() + "; " + row[0].ToString();
                            cont++;
                        }
                    }
                    
                }
                iteracion++;
            }
            if (iteracion == datos.Tables[0].Rows.Count)
                _ordenes[fecha_ant] = arreglo;
        }
        return _ordenes;
    }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        try
        {
            if (informacion[e.Day.Date.ToString("yyyy-MM-dd")] != null)
            {
                Literal lit = new Literal();
                lit.Text = "<br/>";
                e.Cell.Controls.Add(lit);

                Label lbl = new Label();
                lbl.Text = (string)informacion[e.Day.Date.ToString("yyyy-MM-dd")];
                lbl.Font.Size = new FontUnit(FontSize.XSmall);
                lbl.ForeColor = Color.DarkBlue;
                e.Cell.Controls.Add(lbl);
            }
        }
        catch (Exception ex) { }
    }

    protected void lnkRegresarOrdenes_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ordenes.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"]);
    }
    protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        DateTime inicio = Convert.ToDateTime("01/" + e.NewDate.Month.ToString().PadLeft(2, '0') + "/" + e.NewDate.Year.ToString().PadLeft(4, '0'));
        DateTime fin = inicio.AddMonths(1);
        fin = fin.AddDays(-1);
        informacion = obtieneOrdenes(lblIdOper.Text, inicio, fin);
        Panel5.Visible = false;
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        DateTime inicio = Convert.ToDateTime("01/" + Calendar1.SelectedDate.Month.ToString().PadLeft(2, '0') + "/" + Calendar1.SelectedDate.Year.ToString().PadLeft(4, '0'));
        DateTime fin = inicio.AddMonths(1);
        fin = fin.AddDays(-1);
        informacion = obtieneOrdenes(lblIdOper.Text, inicio, fin);
        string secuencia = obtieneSecuencia(lblIdOper.Text, inicio, fin, Calendar1.SelectedDate);
        lblFecha.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
        if (secuencia == "")
            secuencia = "000000";
        cargaOrdenes(secuencia);
        Panel5.Visible = true;
    }

    private string obtieneSecuencia(string empleado, DateTime ini, DateTime fin, DateTime fecha)
    {
        Calendarizacion calendario = new Calendarizacion();
        calendario.Empresa = Convert.ToInt32(Request.QueryString["e"]);
        calendario.Empleado = Convert.ToInt32(empleado);
        calendario.Taller = Convert.ToInt32(Request.QueryString["t"]);
        calendario.FIni = ini.ToString("yyyy-MM-dd");
        calendario.FFin = fin.ToString("yyyy-MM-dd");
        calendario.obtieneOrdenesEmpleado();
        string arreglo = "";
        string fecha_ant = fecha.ToString("yyyy-MM-dd");
        int cont = 0;
        object[] info = calendario.Datos;
        if (Convert.ToBoolean(info[0]))
        {
            DataSet datos = (DataSet)info[1];
            foreach (DataRow row in datos.Tables[0].Rows)
            {
                if (row[1].ToString() != "")
                {
                    if (row[1].ToString() == fecha_ant)
                    {                       
                        if (cont == 0)
                        {
                            arreglo = row[0].ToString();
                            cont++;
                        }
                        else
                        {
                            arreglo = arreglo.Trim() + ", " + row[0].ToString();
                            cont++;
                        }
                    }

                }
            }
        }
        return arreglo;
    }

    private void cargaOrdenes(string secuencia)
    {
        if (secuencia != "")
            secuencia = "and orp.no_orden in (" + secuencia + ")";
        gvOrders.DataSource = GetData(string.Format("select orp.no_orden,tv.descripcion+' '+m.descripcion+' '+tu.descripcion as descripcion,upper(v.color_ext) as color_ext,orp.placas,l.descripcion as localizacion,C.razon_social,orp.fase_orden, so.f_recepcion, orp.no_siniestro,v.modelo,po.descripcion as perfil,"
                + " isnull((select count(*) from operativos_orden where id_empresa=orp.id_empresa and id_taller=orp.id_taller and no_orden=orp.no_orden),0) as procesos"
                + " from Ordenes_Reparacion orp"
                + " left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa=so.id_empresa and orp.id_taller=so.id_taller"
                + " left join Vehiculos v on v.id_marca=orp.id_marca and v.id_tipo_vehiculo=orp.id_tipo_vehiculo and v.id_tipo_unidad=orp.id_tipo_unidad and v.id_vehiculo=orp.id_vehiculo"
                + " left join Marcas m on m.id_marca=orp.id_marca"
                + " left join Tipo_Vehiculo tv on tv.id_tipo_vehiculo=orp.id_tipo_vehiculo"
                + " left join Tipo_Unidad tu on tu.id_marca=orp.id_marca and tu.id_tipo_vehiculo=orp.id_tipo_vehiculo and tu.id_tipo_unidad=orp.id_tipo_unidad"
                + " left join Localizaciones l on l.id_localizacion=orp.id_localizacion"
                + " left join Cliprov c on c.id_cliprov=orp.id_cliprov and c.tipo=orp.tipo_cliprov and c.tipo='C'"
                + " left join PerfilesOrdenes po on po.id_perfilOrden=orp.id_perfilOrden"
                + " where orp.id_empresa= {0}  and orp.id_taller= {1} {2} order by orp.no_orden desc", Request.QueryString["e"], Request.QueryString["t"], secuencia));
        gvOrders.DataBind();
    }

    private static DataTable GetData(string query)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["Taller"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    protected void btnOrden_Click(object sender, EventArgs e)
    {
        LinkButton lknReferencia = (LinkButton)sender;
        int orden = Convert.ToInt32(lknReferencia.Text);
        int fase = Convert.ToInt32(lknReferencia.CommandArgument.ToString());
        Response.Redirect("BienvenidaOrdenes.aspx?u=" + Request.QueryString["u"] + "&p=" + Request.QueryString["p"] + "&e=" + Request.QueryString["e"] + "&t=" + Request.QueryString["t"] + "&o=" + orden + "&f=" + fase);
    }
}