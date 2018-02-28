﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using E_Utilities;

/// <summary>
/// Descripción breve de Cronos
/// </summary>
public class Cronos
{
    private static Ejecuciones ejecuta = new Ejecuciones();

    int _empresa;
    int _taller;
    int _orden;
    int _localizacion;
    int _perfil;
    decimal _avance;
    string[] _valores;
    object[] _actualizacion;

	public Cronos()
	{
        _empresa = _taller = _orden = _localizacion =_perfil = 0;
        _avance = 0;
        _valores = new string[26];
        _actualizacion = new object[2];
	}

    public int Empresa { set { _empresa = value; } }
    public int Taller { set { _taller = value; } }
    public int Orden { set { _orden = value; } }
    public int Localizacion { set { _localizacion = value; } }
    public int Perfil { set { _perfil = value; } }
    public decimal Avance { set { _avance = value; } }
    public string[] Fechas { set { _valores = value; } get { return _valores; } }
    public object[] Actualizacion { get { return _actualizacion; } }


    public void obtieneFechas() {
        object[] datos = new object[2];
        string sql = string.Format("select top 1 * from seguimiento_orden where id_empresa={0} and id_taller={1} and no_orden={2} order by id_registro desc", _empresa, _taller, _orden);
        datos = ejecuta.dataSet(sql);
        if (Convert.ToBoolean(datos[0]))
        {
            DataSet fechas = (DataSet)datos[1];
            foreach (DataRow fila in fechas.Tables[0].Rows)
            {
                _valores[0] = fila[4].ToString();
                _valores[1] = fila[5].ToString();
                _valores[2] = fila[6].ToString();
                _valores[3] = fila[7].ToString();
                _valores[4] = fila[8].ToString();
                _valores[5] = fila[9].ToString();
                _valores[6] = fila[10].ToString();
                _valores[7] = fila[11].ToString();
                _valores[8] = fila[12].ToString();
                _valores[9] = fila[13].ToString();
                _valores[10] = fila[14].ToString();
                _valores[11] = fila[15].ToString();
                _valores[12] = fila[16].ToString();
                _valores[13] = fila[17].ToString();
                _valores[14] = fila[18].ToString();
                _valores[15] = fila[19].ToString();
                _valores[16] = fila[20].ToString();
                _valores[17] = fila[21].ToString();
                _valores[18] = fila[22].ToString();
                _valores[19] = fila[23].ToString();
                _valores[20] = fila[24].ToString();
                _valores[21] = fila[25].ToString();
                _valores[22] = fila[26].ToString();
                _valores[23] = fila[27].ToString();
                _valores[24] = fila[28].ToString();
                _valores[25] = fila[29].ToString();
            }
        }
        else {
            for (int i = 0; i < _valores.Length; i++) {
                _valores[i] = "";
            }
        }
    }

    public DataTable obtieneDtSegOrden()
    {
        DataTable dt = new DataTable();
        object[] datos = new object[2];
        string sql = string.Format("select * from seguimiento_orden where id_empresa={0} and id_taller={1} and no_orden={2} order by id_registro desc", _empresa, _taller, _orden);
        datos = ejecuta.dataSet(sql);
        if (Convert.ToBoolean(datos[0]))
            dt = ((DataSet)datos[1]).Tables[0];
        return dt;
    }


    public void actualizaCronos() {
        for(int i =0;i<_valores.Length;i++){
            if (i == 14 || i == 16 || i==20 || i==22 || i ==24)
            {
                if (_valores[i] == "")
                    _valores[i] = "00:00:00";
            }
            else
            {
                if (_valores[i] == "")
                    _valores[i] = "1900-01-01";
            }
        }

        string sql = "update seguimiento_orden set f_retorno_transito=" + CargaValoresNulos(_valores[2]) + ",f_alta=" + CargaValoresNulos(_valores[3]) + ",f_valuacion=" + CargaValoresNulos(_valores[4]) + 
            ",f_autorizacion=" + CargaValoresNulos(_valores[5]) + ",f_asignacion=" + CargaValoresNulos(_valores[6]) + ",f_tocado=" + CargaValoresNulos(_valores[7]) + ",f_primer_llamada=" + CargaValoresNulos(_valores[8]) + 
            ",f_alta_portal=" + CargaValoresNulos(_valores[9]) + ",f_ult_entrega_ref=" + CargaValoresNulos(_valores[10]) + ",f_promesa_portal=" + CargaValoresNulos(_valores[11]) + ",f_terminado=" + CargaValoresNulos(_valores[12]) + 
            ",f_terminacion=" + CargaValoresNulos(_valores[13]) + ",h_terminacion=" + CargaValoresNulos(_valores[14]) + ",f_entrega_estimada=" + CargaValoresNulos(_valores[15]) + ",h_estrega_estimada=" + CargaValoresNulos(_valores[16]) + 
            ",f_baja_portal=" + CargaValoresNulos(_valores[17]) + ",f_entrega=" + CargaValoresNulos(_valores[18]) + ", f_pactada=" + CargaValoresNulos(_valores[19]) + ", h_pactada=" + CargaValoresNulos(_valores[20]) + 
            ", f_prog_retorno_tran=" + CargaValoresNulos(_valores[21]) + ", h_prog_retorno_tran=" + CargaValoresNulos(_valores[22]) + ", f_confirmacion=" + CargaValoresNulos(_valores[23]) + ", h_confirmacion=" + CargaValoresNulos(_valores[24]) + ",f_complemento=" + CargaValoresNulos(_valores[25]) +
            " where id_empresa=" + _empresa + " and id_taller=" + _taller + " and no_orden=" + _orden;
        _actualizacion = ejecuta.insertUpdateDelete(sql);
    }

    private string CargaValoresNulos(string valor)
    {
        if (valor == "00:00:00" || valor == "1900-01-01")
            return "null";
        else
            return "'" + valor + "'";
    }

    public void actualizaTerminado(string noOrden, string idEmpresa, string idTaller, string usuario)
    {
        Fechas fehas = new Fechas();
        string sql = "update seguimiento_orden set f_terminado='" + fehas.obtieneFechaLocal().ToString("yyyy-MM-dd") + "' where no_orden=" + noOrden + " and id_empresa=" + idEmpresa + " and id_taller=" + idTaller+
            " update ordenes_reparacion set id_localizacion=3 where no_orden=" + noOrden + " and id_empresa=" + idEmpresa + " and id_taller=" + idTaller;
        BitacorasComentarios bitacora = new BitacorasComentarios();
        bitacora.Empresa = Convert.ToInt32(idEmpresa);
        bitacora.Taller = Convert.ToInt32(idTaller);
        bitacora.Orden = Convert.ToInt32(noOrden);
        bitacora.Bitacora = 2;
        bitacora.Usuario = Convert.ToInt32(usuario);
        bitacora.Valor = "3";
        bitacora.agregaRegistro();
        _actualizacion = ejecuta.insertUpdateDelete(sql);
    }

    public void existeFechaTermino(string noOrden, string idEmpresa, string idTaller)
    {
        string sql = "select count(*) from seguimiento_orden where f_terminado>'1900-01-01' and no_orden=" + noOrden + " and id_empresa=" + idEmpresa + " and id_taller=" + idTaller;
        _actualizacion = ejecuta.scalarToInt(sql);
    }

    public void obtieneIncompletos(int noOrden, int idEmpresa, int idTaller)
    {
        string sql = "select count(tabla.p100)-sum(cast(tabla.p100 as int))as incompletos from ("
        + "select distinct mo.id_grupo_op,upper(g.descripcion_go) as descripcion,"
        + "isnull((select s.p25 from Seguimiento_Operacion s where s.id_empresa=mo.id_empresa and s.id_taller=mo.id_taller and s.no_orden=mo.no_orden and s.id_grupo_op=mo.id_grupo_op),0) as p25,"
        + "isnull((select s.p50 from Seguimiento_Operacion s where s.id_empresa=mo.id_empresa and s.id_taller=mo.id_taller and s.no_orden=mo.no_orden and s.id_grupo_op=mo.id_grupo_op),0) as p50,"
        + "isnull((select s.p75 from Seguimiento_Operacion s where s.id_empresa=mo.id_empresa and s.id_taller=mo.id_taller and s.no_orden=mo.no_orden and s.id_grupo_op=mo.id_grupo_op),0) as p75,"
        + "isnull((select s.p100 from Seguimiento_Operacion s where s.id_empresa=mo.id_empresa and s.id_taller=mo.id_taller and s.no_orden=mo.no_orden and s.id_grupo_op=mo.id_grupo_op),0) as p100,"
        + "isnull((select s.terminado from Seguimiento_Operacion s where s.id_empresa=mo.id_empresa and s.id_taller=mo.id_taller and s.no_orden=mo.no_orden and s.id_grupo_op=mo.id_grupo_op),0) as VoBo "
        + "from Mano_Obra mo "
        + "inner join Grupo_Operacion g on g.id_grupo_op=mo.id_grupo_op "
        + "where mo.id_empresa=" + idEmpresa.ToString() + " and mo.id_taller=" + idTaller.ToString() + " and mo.no_orden=" + noOrden.ToString() + ")as tabla";
        _actualizacion = ejecuta.scalarToInt(sql);
    }

    public static void addBitacora(string orden, string tipo, string col, int usuAut, int[] sesiones)
    {
        Fechas fechas = new Fechas();
        string mnsj = "";
        if (tipo == "B")
        {
            mnsj = "Elimina fecha, columna: " + (col.IndexOf('=').ToString() == "-1" ? col : col.Substring(0, col.IndexOf('='))) + ".";
        }
        else if (tipo == "A")
        {
            mnsj = "Cambia fecha: " + col;
        }
        string sqlInsBitCronos = "INSERT INTO Bitacora_Cronos VALUES(" + sesiones[2] + "," + sesiones[3] + "," + orden + ",'" + mnsj + "', CONVERT(smalldatetime,'" + fechas.obtieneFechaLocal().ToString("yyyy-MM-dd HH:mm:ss") + "',120)," + sesiones[0] + "," + usuAut + ")";
        ejecuta.insertUpdateDelete(sqlInsBitCronos);
    }
}