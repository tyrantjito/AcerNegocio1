using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ConsBuro
/// </summary>
public class ConsBuro 
{
    Ejecuciones ejecuta = new Ejecuciones();
    public string  estatus {get;set;}
    public char rp { get; set; }
    public char id { get; set; }
    public int idConsultaEdita { get; set; }
    public int idCliente { get; set; }
    public int codigo { get; set; }
    public string nombre_completo { get; set; }
    public string identificador { get; set; }
    public string calle { get; set; }
    public string municipio { get; set; }
    public string CP { get; set; }
    public string lugarAutorizacion { get; set; }
    public string nombreFuncionario { get; set; }
    public char Rp { get; set; }
    public char Id { get; set; }

    public string fechaConsulta { get; set; } 
    public string folioConsulta { get; set; }
    public string representante { get; set; }
    public string colonia {get;set;}
    public string estado {get;set;}
    public string tipo { get; set; }
    public decimal telefono { get; set; }
    public string fechaAutorizacion { get; set; }
    public object[] retorno;
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public string nombre { get; set; }
    public string aPaterno { get; set; }
    public string aMaterno { get; set; }
    public string numero { get; set; }
    public int idClienteEdita { get; set; }
    public byte[] adjunto;
    public int idAdjunto { get; set; }
    public string extension { get; set; }
    public string nombreAdjunto { get; set; }
    public int nuevoCliente { get; set; }
    public int usuario { get; set; }

    private string sql;



	public ConsBuro()
	{
        retorno = new object[] { false, "" };
	}

    public void agregaSolicitud() 
    {
        sql = "declare  @cliente int set @cliente=(select isnull((select top 1 id_cliente from AN_Clientes where id_empresa="+ empresa + " and id_sucursal=" + sucursal + "  order by id_cliente desc),0)+1) " +
              "declare @consulta int set @consulta = (select isnull((select top 1 id_consulta from AN_Solicitud_Consulta_Buro where id_empresa = " +empresa + " and id_sucursal = " + sucursal + " order by id_consulta desc),0)+1) " +
              "insert into an_solicitud_consulta_buro " +
              "values ("+empresa+","+sucursal+ ", @consulta,@cliente,'" + identificador.ToUpper() + "','"+tipo+"','"+representante.ToUpper() + "','"+calle.ToUpper() + "','"+numero.ToUpper() + "','"+colonia.ToUpper() + "','"+municipio.ToUpper() + "','"+estado.ToUpper() + "',"+CP+","+telefono+",'"+lugarAutorizacion.ToUpper() + "','"+fechaAutorizacion+"','"+nombreFuncionario.ToUpper() + "','"+fechaConsulta+"','"+folioConsulta+"','"+estatus+"','PEN','PEN','ENP','"+rp+"','"+id+"') select @cliente";
        retorno = ejecuta.scalarToInt(sql);
        if(Convert.ToInt32(retorno[1]) != 0)
        {
            nuevoCliente = Convert.ToInt32(retorno[1]);
        }
    }

    public void existeCliente()
    {
        sql = "select count(*) from an_clientes  where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and rfc_curp='" + identificador + "'";
        retorno = ejecuta.scalarToBool(sql);
    }

    public void registraClienteNuevo()
    {
        sql = "insert into an_clientes values ("+empresa+","+sucursal+","+nuevoCliente+",'"+nombre.ToUpper() + "','"+aPaterno.ToUpper() + "','"+aMaterno.ToUpper() + "','"+nombre_completo.ToUpper() + "','"+identificador.ToUpper() + "','A')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void editaConsulta()
    {
        sql = "select a.id_cliente,b.nombre,b.apellido_p,b.apellido_m,b.rfc_curp,b.nombre_completo,a.tipo_persona,a.representante_legal,a.numero,a.calle,a.colonia,a.municipio,a.estado,a.cp,a.telefono,a.lugar_autorizacion,a.fecha_autorizacion,a.nombre_funcionario,a.fecha_consulta,a.estatus,a.folio_consulta from an_solicitud_consulta_buro a  inner join an_clientes b on b.id_cliente=a.id_cliente where a.id_empresa=" + empresa + " and a.id_sucursal=" + sucursal + " and a.id_cliente="+idClienteEdita + " and a.id_consulta =" + idConsultaEdita;
        retorno = ejecuta.dataSet(sql);
    }
    public void editarSolicitud()
    {
        sql = "update an_solicitud_consulta_buro set rfc_curp='"+identificador.ToUpper()+"', tipo_persona='"+tipo.ToUpper()+"', representante_legal='"+representante.ToUpper() + "', calle='"+calle.ToUpper() + "', numero='"+numero.ToUpper() + "', colonia='"+colonia.ToUpper() + "', municipio='"+municipio.ToUpper() + "', estado='"+estado.ToUpper() + "', cp="+CP+", telefono="+telefono+", lugar_autorizacion='"+lugarAutorizacion.ToUpper() + "', fecha_autorizacion='"+fechaAutorizacion+"', nombre_funcionario='"+nombreFuncionario.ToUpper() + "', fecha_consulta='"+fechaConsulta+"', folio_consulta='"+folioConsulta+"', estatus='"+estatus+"' where id_empresa="+empresa+" and id_sucursal="+sucursal+" and id_cliente="+idClienteEdita+" and id_consulta="+ idConsultaEdita
            + "update an_clientes set nombre='"+nombre.ToUpper() + "', apellido_p='"+aPaterno.ToUpper() + "',apellido_m='"+aMaterno.ToUpper() + "',nombre_completo='"+nombre_completo.ToUpper() + "', rfc_curp='"+identificador.ToUpper() + "' where id_empresa="+empresa+" and id_sucursal="+sucursal+" and id_cliente="+idClienteEdita;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void optieneimoresion()
    {
        sql = "select upper(c.nombre_completo),upper(s.rfc_curp),tipo_persona,case representante_legal when 'N/A' then '' else representante_legal end as representante_legal,upper(calle),upper(numero),upper(colonia),upper(municipio),upper(estado),cp,telefono,upper(lugar_autorizacion),convert(char(10),fecha_autorizacion,120) as fecha,nombre_funcionario,convert(char(10),fecha_consulta,120) as fechaC,case folio_consulta when '' then 'SIN CONSULTAR' else folio_consulta end as folio,s.estatus from an_solicitud_consulta_buro s inner join an_clientes c on s.id_cliente= c.id_cliente  where s.id_empresa=" + empresa + " and s.id_sucursal=" + sucursal + " and s.id_consulta =" + idConsultaEdita + "and s.id_cliente=" + idClienteEdita;
        retorno = ejecuta.dataSet(sql);
    }
     
    public void existeIdAdjunto()
    {
        sql = "select descripcion from AN_Adjuntos_Consulta_Buro where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + idClienteEdita + " and id_consulta=" + idConsultaEdita + " and tipo='ID'";
        retorno = ejecuta.scalarToString(sql);
    }

    public void existeRpAdjunto()
    {
        sql = "select descripcion from AN_Adjuntos_Consulta_Buro where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + idClienteEdita + " and id_consulta=" + idConsultaEdita + " and tipo='RP'";
        retorno = ejecuta.scalarToString(sql);
    }

    public void agregaAdjuntoID()
    {
        sql = "insert into an_adjuntos_consulta_buro (id_empresa,id_sucursal,id_cliente,id_consulta,id_adjunto,extension,adjunto,descripcion,tipo) values (" + empresa + "," + sucursal + "," + idClienteEdita + ","+idConsultaEdita+ ",(select isnull((select top 1 id_adjunto from an_adjuntos_consulta_buro where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + idClienteEdita + " and id_consulta=" + idConsultaEdita + " order by id_adjunto desc),0)+1),'" + extension + "',@imagen,'" + nombreAdjunto + "','ID')";
        retorno = ejecuta.insertUpdateDeleteImagenes2(sql, adjunto);
    }
    public void obtieneImagen()
    {
        sql = "select descripcion,extension,adjunto from an_adjuntos_consulta_buro where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + idClienteEdita + " and id_adjunto=" + idAdjunto + "and id_consulta=" + idConsultaEdita;
        retorno = ejecuta.dataSet(sql);
    }

    public void agregaAdjuntoRP()
    {
        sql = "insert into an_adjuntos_consulta_buro (id_empresa,id_sucursal,id_cliente,id_consulta,id_adjunto,extension,adjunto,descripcion,tipo) values (" + empresa + "," + sucursal + "," + idClienteEdita + "," + idConsultaEdita + ",(select isnull((select top 1 id_adjunto from an_adjuntos_consulta_buro where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + idClienteEdita + " and id_consulta=" + idConsultaEdita + " order by id_adjunto desc),0)+1),'" + extension + "',@imagen,'" + nombreAdjunto + "','RP')";
        retorno = ejecuta.insertUpdateDeleteImagenes2(sql, adjunto);
    }
    public void ActualizatieneRp()
    {
        sql = "update an_solicitud_consulta_buro set rp='S' where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + idClienteEdita + " and id_consulta=" + idConsultaEdita;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void ActualizatieneId()
    {
        sql = "update an_solicitud_consulta_buro set id='S' where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + idClienteEdita + " and id_consulta=" + idConsultaEdita;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void ActualizanotieneRP()
    {
        sql = "update an_solicitud_consulta_buro set rp='N' where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + idClienteEdita + " and id_consulta=" + idConsultaEdita;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void Actualizanotieneid()
    {
        sql = "update an_solicitud_consulta_buro set id='N' where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + idClienteEdita + " and id_consulta=" + idConsultaEdita;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void eliminaAdjunto()
    {
        sql = "delete from an_adjuntos_consulta_buro where id_empresa=" + empresa + " and id_sucursal=" + sucursal + "and id_cliente=" + idClienteEdita + " and id_adjunto=" + idAdjunto + "and id_consulta=" + idConsultaEdita;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void obtenusuario()
    {
        sql = " select nombre_usuario from usuarios where id_usuario=" + usuario;
        retorno = ejecuta.dataSet(sql);
    }

    public void datosCP()
    {
        sql = "SELECT d_estado,d_mnpio from an_codigopostal where  d_codigo=" + codigo;
        retorno = ejecuta.dataSet(sql);
    }

    
}