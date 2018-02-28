using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ActInt
/// </summary>
public class ActInt
{
    Ejecuciones ejecuta = new Ejecuciones();
    private string sql; 
    public object[] retorno;

    public int id_empresa { get; set; }

    public int id_cliente { get; set; }
    public int id_sucursal { get; set; }
    public int id_acta { get; set; }
    // propiedad para editar 
    public int idactaEdita { get; set; }
    public int id_grupo { get; set; }
    public string hora_acta_integracion { get; set; }
    public string fecha_integracion_acta { get; set; }
    public string calle_direccion_de_integracion_acta { get; set; }
    public string numero_direccion_de_integracion_acta { get; set; }
    public string colonia_direccion_de_integracion_acta { get; set; }
    public string municipio_direccion_de_integracion_acta { get; set; }
    public string estado_direccion_de_integracion_acta { get; set; }
    public string nombre_grupo_productivo { get; set; }
    public string estatus { get; set; }
    public string colonia_grupo_productivo { get; set; }
    public string colonias_circunvecinas { get; set; }
    public decimal monto_ahorro_minimo_semanal { get; set; }
    public string numero_reparto_ahorro { get; set; }
    public string dia_reunion_semanal { get; set; }
    public string hora_reunion_samanal { get; set; }
    public string nombre_integrante_para_reuniones { get; set; }
    public string cargo_integrante_para_reuniones { get; set; }
    public string calle_direccion_reunion { get; set; }
    public string numero_direccion_reunion { get; set; }
    public string colonia_direccion_reunion { get; set; }
    public string municipio_direccion_reunion { get; set; }
    public string estado_direccion_reunion { get; set; }
    public string tiempo_tolerancia_reunion { get; set; }
    public decimal multa_retardo_reunion { get; set; }
    public decimal multaFalta_conEnvio_completo { get; set; }
    public decimal multaFalta_conEnvio_incompleto { get; set; }
    public decimal multaFalta { get; set; }
    public string hora_termina_reunion { get; set; }
    //
    public int id_presidente { get; set; }
    public int id_secretario { get; set; }
    public int id_tesorero { get; set; }
    public int id_V1 { get; set; }
    public int id_V2 { get; set; }
    public int id_asesor { get; set; }

    public string nombClien { get; set; }

    // usado para recuperar nombres de la tabla de consulta de buro
    public int idBuscar { get; set; }

    public ActInt() 
    {
        retorno = new object[] { false, "" };
    }
    public void insertarGrupo()
    {
        sql = "insert into an_grupos values(" + id_empresa + ", " + id_sucursal + ", (select isnull((select top 1 id_grupo from an_grupos where id_empresa = " + id_empresa + " and id_sucursal = " + id_sucursal + "  order by id_grupo desc), 0) + 1),'" + nombre_grupo_productivo + "','" + estatus + "')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregaActa()
    {
        sql = "declare @idActa int set @idActa=(select isnull((select top 1 id_acta from an_acta_integracion where id_empresa=" + id_empresa + " and id_sucursal=" + id_sucursal + "   order by id_acta desc),0)+1) " +
              "declare @idGrupo int set @idGrupo=(select isnull((select top 1 id_grupo from an_grupos where id_empresa=" + id_empresa + " and id_sucursal=" + id_sucursal + "   order by id_grupo desc),0)+1) " +
                "insert into an_acta_integracion values (" + id_empresa + "," + id_sucursal +",@idActa ,@idGrupo,'" +

                hora_acta_integracion + "','" + fecha_integracion_acta + "','" + calle_direccion_de_integracion_acta + "','" + numero_direccion_de_integracion_acta + "','" +
                colonia_direccion_de_integracion_acta + "','" + municipio_direccion_de_integracion_acta + "','" + estado_direccion_de_integracion_acta + "','" +
                colonia_grupo_productivo + "','" + colonias_circunvecinas + "','" + monto_ahorro_minimo_semanal + "','" + numero_reparto_ahorro + "','" +
                dia_reunion_semanal + "','" + hora_reunion_samanal + "','" + nombre_integrante_para_reuniones + "','" + cargo_integrante_para_reuniones + "','" +
                calle_direccion_reunion + "','" + numero_direccion_reunion + "','" + colonia_direccion_reunion + "','" + municipio_direccion_reunion + "','" +
                estado_direccion_reunion + "','" + tiempo_tolerancia_reunion + "','" + multa_retardo_reunion + "','" + multaFalta_conEnvio_completo + "','" +
                multaFalta_conEnvio_incompleto + "','" + multaFalta + "','" + hora_termina_reunion + "'," + id_presidente + "," + id_secretario + "," +
                id_tesorero + "," + id_V1 + ","+id_V2+",0) " +
                " insert into an_grupos values (" + id_empresa + "," + id_sucursal + ",@idGrupo,'" + nombre_grupo_productivo + "','A') select @idActa ";
        retorno = ejecuta.scalarToInt(sql);
    }

    public void obtieneDatosActa()
    {
        sql = "select a.id_empresa,a.id_sucursal,a.id_acta,a.id_grupo,a.hora_acta_integracion,a.fecha_integracion_acta,a.calle_direccion_de_integracion_acta, " +
		"a.numero_direccion_de_integracion_acta,a.colonia_direccion_de_integracion_acta,a.municipio_direccion_de_integracion_acta, " +
		" a.estado_direccion_de_integracion_acta,a.colonia_grupo_productivo,a.colonias_circunvecinas,a.monto_ahorro_minimo_semanal, " +
		" a.numero_reparto_ahorro,a.dia_reunion_semanal,a.hora_reunion_samanal,a.nombre_integrante_para_reuniones,a.cargo_integrante_para_reuniones, " +
		" a.calle_direccion_reunion,a.numero_direccion_reunion,a.colonia_direccion_reunion,a.municipio_direccion_reunion,a.estado_direccion_reunion, " +
		" a.tiempo_tolerancia_reunion,a.multa_retardo_reunion,a.multaFalta_conEnvio_completo,a.multaFalta_conEnvio_incompleto,a.multaFalta,a.hora_termina_reunion, " +
        " a.id_presidente,a.id_secretario,a.id_tesorero,a.id_VocalV1,a.id_VocalV2,b.grupo_productivo  " +
		" from an_acta_integracion a  " +
		" inner join AN_Grupos b on b.id_grupo=a.id_grupo " +
		" where a.id_empresa="+id_empresa+" and a.id_sucursal="+id_sucursal+" and a.id_acta =" + idactaEdita;
        retorno = ejecuta.dataSet(sql);
    }
    
    public void actualizaDatosActa()
    {
        sql = "update an_acta_integracion set hora_acta_integracion ='" + hora_acta_integracion + "',fecha_integracion_acta='" + fecha_integracion_acta +
               "',calle_direccion_de_integracion_acta='" + calle_direccion_de_integracion_acta + "',numero_direccion_de_integracion_acta='" + numero_direccion_de_integracion_acta +
               "',colonia_direccion_de_integracion_acta='" + colonia_direccion_de_integracion_acta +
               "',municipio_direccion_de_integracion_acta='" + municipio_direccion_de_integracion_acta +
               "',estado_direccion_de_integracion_acta='" + estado_direccion_de_integracion_acta +
               "',colonia_grupo_productivo='" + colonia_grupo_productivo +
               "',colonias_circunvecinas='" + colonias_circunvecinas + "'" +
               " , monto_ahorro_minimo_semanal=" + monto_ahorro_minimo_semanal +
               " , numero_reparto_ahorro= '" + numero_reparto_ahorro +
               " ', dia_reunion_semanal= '" + dia_reunion_semanal +
               "', hora_reunion_samanal= '" + hora_reunion_samanal + "'" +
               " , nombre_integrante_para_reuniones= '" + nombre_integrante_para_reuniones +
               "', cargo_integrante_para_reuniones= '" + cargo_integrante_para_reuniones +
               "', calle_direccion_reunion= '" + calle_direccion_reunion + "'" +
               " , numero_direccion_reunion= '" + numero_direccion_reunion +
               "', colonia_direccion_reunion= '" + colonia_direccion_reunion +
               "', municipio_direccion_reunion= '" + municipio_direccion_reunion +
               "', estado_direccion_reunion= '" + estado_direccion_reunion + "'" +
               " , tiempo_tolerancia_reunion= '" + tiempo_tolerancia_reunion +
               "', multa_retardo_reunion= " + multa_retardo_reunion +
               " , multaFalta_conEnvio_completo= " + multaFalta_conEnvio_completo +
               " , multaFalta_conEnvio_incompleto= " + multaFalta_conEnvio_incompleto +
               " , multaFalta= " + multaFalta +
               " , hora_termina_reunion= '" + hora_termina_reunion +
               "', id_presidente= " + id_presidente +
               " , id_secretario= " + id_secretario +
               " , id_tesorero= " + id_tesorero +
               " , id_VocalV1= " + id_V1 +
               " , id_VocalV2= " + id_V2 +
               " where id_empresa=" + id_empresa + " and id_sucursal=" + id_sucursal + " and id_acta=" + id_acta;

        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void optieneimoresion()
    {
        sql = "select * from an_acta_integracion where id_empresa=" + id_empresa + " and id_sucursal=" + id_sucursal + " and id_acta =" + idactaEdita;
        retorno = ejecuta.dataSet(sql);
    }
    public void optieneimoresion1()
    {
        sql = "select id_presidente,id_secretario,id_tesorero,id_VocalV1,id_VocalV2 from AN_Acta_Integracion where id_empresa=" + id_empresa + " and id_sucursal=" + id_sucursal + " and id_acta =" + id_acta;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtieneNombres()
    {
        sql = "select nombre_completo from AN_clientes where id_empresa=" + id_empresa + " and id_sucursal=" + id_sucursal + " and id_cliente =" + id_cliente;
        retorno = ejecuta.scalarToString(sql);
    }
    public void obtieneNombres2()
    {
        sql = "select b.nombre_completo from an_acta_integraciondetalle A INNER JOIN AN_clientes B on b.id_cliente=a.id_cliente where a.id_empresa=" + id_empresa + " and a.id_sucursal=" + id_sucursal + "and a.id_acta=" + id_acta;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtienePresidente()
    {
        sql = "select c.id_cliente from an_clientes c inner join AN_Acta_Integracion a on c.id_cliente=a.id_presidente where a.id_empresa=" + id_empresa + " and a.id_sucursal=" + id_sucursal + "and a.id_presidente=" + id_presidente;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtinedatoscasa()
    {
        sql = "select c.id_cliente,b.calle,b.numero,b.colonia,b.municipio,b.estado from AN_Clientes c inner join AN_Solicitud_Consulta_Buro b on  c.id_cliente=b.id_cliente where b.id_empresa="+id_empresa+" and b.id_sucursal="+id_sucursal+" and b.id_cliente="+id_cliente;
        retorno = ejecuta.dataSet(sql);
    }
    public void eliminaintegrante()
    {
        sql = "delete from AN_Acta_IntegracionDetalle where id_empresa=" + id_empresa + " and id_sucursal=" + id_sucursal + " and id_acta=" + id_acta + " and id_cliente=" + id_cliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void obtienenombrecliente()
    {
        sql = "select nombre_completo from an_clientes where id_cliente="+nombClien;
        retorno = ejecuta.dataSet(sql);
    }
}