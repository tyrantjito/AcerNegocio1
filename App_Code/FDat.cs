using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de FDat
/// </summary>
public class FDat
{ 
    Ejecuciones ejecuta = new Ejecuciones();
    public int id_cliente { get; set; }
    public int id_ficha { get; set; }
    public string  d_codigo { get; set; }
    public int IdEdita { get; set; }
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public string f_nacimiento_cli { get; set;}
    public string e_nacimiento_cli { get; set; } 
    public char genero_cli { get; set; }
    public string estado_civil_cli { get; set; }
    public string tipoDomicilio { get; set; }
    public string no_credencial_ife_cli { get; set; }
    public string curp_cli { get; set; }
    public string rfc_cli { get; set; }
    public string nivel_escolaridad { get; set; }
    public string rol_cliente { get; set; }
    public int no_hijos_cli { get; set; }
    public int dep_economicos_cli { get; set; }
    public decimal tel_fijo_cli { get; set; }
    public decimal tel_cel_cli { get; set; }
    public string correo_cli { get; set; }
    public string calle_cli { get; set; }
    public string no_exterior_cli { get; set; }
    public string no_interior_cli { get; set; }
    public string colonia_cli { get; set; }
    public string cp_cli { get; set; }
    public string del_mun_cli { get; set; }
    public string estado_cli { get; set; }
    public string tiempo_residencia_cli { get; set; }
    public int no_habitantes_cli { get; set; }
    public string condicion_propiedad_cli { get; set; }
    public string nombre_esp { get; set; }
    public string a_paterno_esp { get; set; }
    public string a_materno_esp { get; set; }
    public string nombre_completo_esp { get; set; }
    public string ocupacion_esp { get; set; }
    public decimal tel_trab_esp { get; set; }
    public decimal tel_casa_esp { get; set; }
    public decimal tel_cel_esp { get; set; }
    public string calle_neg { get; set; }
    public string no_exterior_neg { get; set; }
    public string no_interior_neg { get; set; }
    public string colionia_neg { get; set; }
    public int cp_neg { get; set; }
    public string del_mun_neg { get; set; }
    public string estado_neg { get; set; }
    public decimal tel_fijo_neg { get; set; }
    public string antiguedad_neg { get; set; }
    public string razon_social_neg { get; set; }
    public string tipo_neg { get; set; }
    public int empleados_perma_neg { get; set; }
    public int empleados_even_neg { get; set; }
    public string giro_principal_neg { get; set; }
    public int ingreso_men_gp_neg { get; set; }
    public string otras_activ_neg { get; set; }
    public decimal ingreso_men_oa_neg { get; set; }
    public string nombre_completo_ref { get; set; }
    public decimal telefono_fijo_ref { get; set; }
    public decimal telefono_celular_ref { get; set; }
    public string parentesco_relacion_ref { get; set; }
    public string tiempo_conocerlo_ref { get; set; }
    public string cargo_preg_ref { get; set; }
    public string cargo_desempeña_ref { get; set; }
    public string dependencia_cargo_ref { get; set; }
    public string periodo_cargo_ref { get; set; }
    public string parentesco_preg_ref { get; set; }
    public string nombre_parentesco_ref { get; set; }
    public string parentesco_ref { get; set; }
    public string cargo_parentesco_ref { get; set; }
    public string dependencia_parentesco_ref { get; set; }
    public string periodo_parentesco_ref { get; set; }
    public string nombre_pr { get; set; }
    public string a_paterno_pr { get; set; }
    public string a_materno_pr { get; set; }
    public string nombre_completo_pr { get; set; }
    public string f_nacimiento_pr { get; set; }
    public string e_nacimiento_pr { get; set; }
    public string nacionalidad_pr { get; set; }
    public char genero_pr { get; set; }
    public string estado_civil_pr { get; set; }
    public string no_credencial_ife_pr { get; set; }
    public string curp_pr { get; set; }
    public string rfc_pr { get; set; }
    public string nivel_pr { get; set; }
    public string rol_propietario_real { get; set; }
    public int no_hijos_pr { get; set; }
    public int dep_economicos_pr { get; set; }
    public string ocupacion_pr { get; set; }
    public decimal tel_fijo_pr{ get; set; }
    public decimal tel_cel_pr { get; set; }
    public string correo_pr { get; set; }
    public string calle_pr { get; set; }
    public string no_exterior_pr { get; set; }
    public string no_interior_pr { get; set; }
    public string colonia_pr { get; set; }
    public int cp_pr { get; set; }
    public string del_mun_pr { get; set; }
    public string estado_pr { get; set; }
    public string tiempo_residencia_pr { get; set; }
    public int no_habitantes_pr { get; set; }
    public string a_paterno_proveedor { get; set; }
    public string a_materno_proveedor { get; set; }
    public string nombre_proveedor { get; set; }
    public string nombre_completo_proveedor { get; set;}
    public string f_nacimiento_proveedor { get; set; }
    public string e_nacimiento_proveedor { get; set; }
    public string nacionalidad_proveedor { get; set; }
    public char genero_proveedor { get; set; }
    public string estado_civil_proveedor { get; set; }
    public string no_credencial_ife_proveedor { get; set; } 
    public string curp_proveedor { get; set; }
    public string rfc_proveedor { get; set; }
    public string nivel_proveedor { get; set; }
    public string rol_proveedor { get; set; }
    public int no_hijos_proveedor { get; set; }
    public int dep_economicos_proveedor { get; set; }
    public string ocupacion_proveedor { get; set; }
    public decimal tel_fijo_proveedor { get; set; }
    public decimal tel_cel_proveedor { get; set; }
    public string correo_proveedor { get; set; }
    public string calle_proveedor { get; set; }
    public string no_exterior_proveedor { get; set; }
    public string no_interior_proveedor { get; set; }
    public string colonia_proveedor { get; set; }
    public int cp_proveedor { get; set; }
    public string del_mun_proveedor { get; set; }
    public string estado_proveedor { get; set; }
    public string tiempo_residencia_proveedor { get; set; }
    public int no_habitantes_proveedor { get; set; }
    public string nombre_completo_ref2{ get; set; }
    public decimal telefono_fijo_ref2 { get; set; }
    public decimal telefono_celular_ref2 { get; set; }
    public string parentesco_relacion_ref2 { get; set; }
    public string tiempo_conocerlo_ref2 { get; set; }
    public string aPaternoBene { get; set; }
    public string aMaternoBene { get; set; }
    public string nombreBene { get; set; }
    public decimal telefonoBene { get; set; }
    public string domicilioBene { get; set; }
    public string colocalBene { get; set; }
    public string NextBene { get; set; }
    public string NintBene { get; set; }
    public string RazonSocial_provee { get; set; }
    public string FirmaElectronica_provee { get; set; }
    public string RazonSocial_pm { get; set; }
    public string nacionalidad_pm { get; set; }
    public string objetoSocial_pm { get; set; }
    public string capitalSocial_pm { get; set; }
    public string domicilio_pm { get; set; }
    public string noext_pm { get; set; }
    public string noint_pm { get; set; }
    public string colonialoca_pm { get; set; }
    public string cp_pm { get; set; }
    public string del_mun_pm { get; set; }
    public string estado_pm { get; set; }
    public string accionista1 { get; set; }
    public string accionista2 { get; set; }
    public string correoref1 { get; set; }
    public string correoref2 { get; set; }



    public object[] retorno;
    private string sql;
    public int ficha { get; set; }
    public byte[] adjunto;
    public int idAdjunto { get; set; }
    public string extension { get; set; }
    public string nombreAdjunto { get; set; }

    public FDat()
    {  
        retorno = new object[] { false, "" };
    }
    public void agregarFicha()
    {
        sql = "insert into an_ficha_datos values (" + empresa + "," + sucursal + ","+id_cliente+",(select isnull((select top 1 id_ficha from an_ficha_datos where id_empresa=" + empresa + " and id_sucursal=" + sucursal + "  order by id_ficha desc),0)+1),'" + f_nacimiento_cli + "','" + e_nacimiento_cli + "','" + genero_cli + "','" + estado_civil_cli + "','" + no_credencial_ife_cli + "','" + curp_cli + "','" + rfc_cli + "','" + nivel_escolaridad + "','" + rol_cliente + "'," + no_hijos_cli + "," + dep_economicos_cli + "," + tel_fijo_cli + "," + tel_cel_cli + ",'" + correo_cli + "','" + calle_cli + "','" + no_exterior_cli + "','" + no_interior_cli + "','" + colonia_cli + "','" + cp_cli + "','" + del_mun_cli + "','" + estado_cli + "','" + tiempo_residencia_cli + "'," + no_habitantes_cli + ",'" + condicion_propiedad_cli + "','" + a_paterno_esp+"','"+a_materno_esp+"','"+nombre_esp+"','"+nombre_completo_esp+"','"+ocupacion_esp+"',"+tel_trab_esp+","+tel_casa_esp+","+tel_cel_esp+",'"+calle_neg+"','"+no_exterior_neg+"','"+no_interior_neg+"','"+colionia_neg+"','"+cp_neg+"','"+del_mun_neg+"','"+estado_neg+"',"+tel_fijo_neg+",'"+antiguedad_neg+"','"+razon_social_neg+"','"+tipo_neg+"',"+empleados_perma_neg+","+empleados_even_neg+",'"+giro_principal_neg+"',"+ingreso_men_gp_neg+",'"+otras_activ_neg+"',"+ingreso_men_oa_neg+",'"+nombre_completo_ref+"',"+telefono_fijo_ref+","+telefono_celular_ref+",'"+parentesco_ref+"','"+tiempo_conocerlo_ref+"','"+cargo_preg_ref+"','"+cargo_desempeña_ref+"','"+dependencia_cargo_ref+"','"+periodo_cargo_ref+"','"+parentesco_preg_ref+"','"+nombre_parentesco_ref+"','"+parentesco_ref+"','"+cargo_parentesco_ref+"','"+dependencia_parentesco_ref+"','"+periodo_parentesco_ref+"','"+a_paterno_pr+"','"+a_materno_pr+"','"+nombre_pr+"','"+nombre_completo_pr+"','"+f_nacimiento_pr+"','"+e_nacimiento_pr+"','"+nacionalidad_pr+"','"+genero_pr+"','"+estado_civil_pr+"','"+no_credencial_ife_pr+"','"+curp_pr+"','"+rfc_pr+"','"+nivel_escolaridad+"','"+rol_propietario_real+"',"+no_hijos_pr+","+dep_economicos_pr+",'"+ocupacion_pr+"',"+tel_fijo_pr+","+tel_cel_pr+",'"+correo_pr+"','"+calle_pr+"','"+no_exterior_pr+"','"+no_interior_pr+"','"+colonia_pr+"','"+cp_pr+"','"+del_mun_pr+"','"+estado_pr+"','"+tiempo_residencia_pr+"',"+no_habitantes_pr+",'"+a_paterno_proveedor+"','"+a_materno_proveedor+"','"+nombre_proveedor+"','"+nombre_completo_proveedor+"','"+f_nacimiento_proveedor+"','"+e_nacimiento_proveedor+"','"+nacionalidad_proveedor+"','"+genero_proveedor+"','"+estado_civil_proveedor+"','"+no_credencial_ife_proveedor+"','"+curp_proveedor+"','"+rfc_proveedor+"','"+nivel_proveedor+"','"+rol_proveedor+"',"+no_hijos_proveedor+","+dep_economicos_proveedor+",'"+ocupacion_proveedor+"',"+tel_fijo_proveedor+","+tel_cel_proveedor+",'"+correo_proveedor+"','"+calle_proveedor+"','"+no_exterior_proveedor+"','"+no_interior_proveedor+"','"+colonia_proveedor+"','"+cp_proveedor+"','"+del_mun_proveedor+"','"+estado_proveedor+"','"+tiempo_residencia_proveedor+"',"+no_habitantes_proveedor+",'"+nombre_completo_ref2+"',"+ telefono_fijo_ref2 + ","+telefono_celular_ref2+",'"+parentesco_relacion_ref2+"','"+tiempo_conocerlo_ref2+"','"+tipoDomicilio+"','"+aPaternoBene+"','"+aMaternoBene+"','"+nombreBene+"','"+domicilioBene+"','"+NextBene+"','"+NintBene+"','"+colocalBene+"',"+telefonoBene+",'"+RazonSocial_provee+"','"+FirmaElectronica_provee+"','"+RazonSocial_pm+"','"+nacionalidad_pm+"','"+objetoSocial_pm+"','"+capitalSocial_pm+"','"+domicilio_pm+"','"+noext_pm+"','"+noint_pm+"','"+colonialoca_pm+"','"+cp_pm+"','"+del_mun_pm+"','"+estado_pm+"','"+accionista1+"','"+accionista2+"','"+correoref1+"','"+correoref2+"')";
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void agregaAdjunto()
    {
        sql = "insert into an_adjunto_ficha_datos (id_empresa,id_sucursal,id_ficha,id_ficha_adjunto,extension,adjunto,descripcion_adjunto) values ("+empresa+","+sucursal+","+ficha+",(select isnull((select top 1 id_ficha_adjunto from an_adjunto_ficha_datos where id_empresa="+empresa+" and id_sucursal="+ sucursal + " and id_ficha=" + ficha + "  order by id_ficha_adjunto desc),0)+1),'"+extension+"',@imagen,'"+nombreAdjunto+"')";
        retorno = ejecuta.insertUpdateDeleteImagenes2(sql,adjunto);
    }
    public void eliminaAdjunto()
    {
        sql = "delete from an_adjunto_ficha_datos where id_empresa=" + empresa +" and id_sucursal=" + sucursal + "and id_ficha=" + ficha + " and id_ficha_adjunto=" + idAdjunto ;
        retorno = ejecuta.insertUpdateDelete(sql);
    }
    public void obtieneFichaEdit()
    {
        sql = "select * from an_ficha_datos where id_empresa=" + empresa +"and id_sucursal=" + sucursal + " and id_ficha="+ ficha;
        retorno = ejecuta.dataSet(sql);
    }

    public void obtieneCliente()
    {
        sql = "select nombre_completo from AN_Clientes where id_empresa=" + empresa + "and id_sucursal=" + sucursal + " and id_cliente=" + id_cliente;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtieneDatosFicha()
    {
        sql= "select rfc_curp,calle,numero,colonia,municipio,estado,cp,telefono,tipo_persona from AN_Solicitud_Consulta_Buro where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente =" + id_cliente;
        retorno = ejecuta.dataSet(sql);
    }
    public void obtieneImagen()
    {
        sql = "select descripcion_adjunto,extension,adjunto from an_adjunto_ficha_datos where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_ficha="+ ficha + " and id_ficha_adjunto=" + idAdjunto;
        retorno = ejecuta.dataSet(sql);
    }
    public void editaFicha()
    {
        sql = "UPDATE AN_Ficha_Datos " +
                " SET  f_nacimiento_cli='" + f_nacimiento_cli + "', e_nacimiento_cli='" + e_nacimiento_cli + "', genero_cli='" + genero_cli +"', estado_civil_cli='" + estado_civil_cli + "', no_credencial_ife_cli='" + no_credencial_ife_cli + "',curp_cli='" + curp_cli + "',rfc_cli='" + rfc_cli + "',"+
                " nivel_escolaridad='" + nivel_escolaridad +"', rol_cliente='" + rol_cliente +"',no_hijos_cli=" + no_hijos_cli +", dep_economicos_cli=" + dep_economicos_cli +", tel_fijo_cli=" + tel_fijo_cli +", tel_cel_cli=" + tel_cel_cli +", correo_cli='" + correo_cli + "', calle_cli='" + calle_cli + "', no_exterior_cli='" + no_exterior_cli + "', no_interior_cli='" + no_interior_cli +"',colonia_cli='" + colonia_cli + "',cp_cli='" + cp_cli + "',"+
                " del_mun_cli='" + del_mun_cli + "',estado_cli='" + estado_cli +"',tiempo_residencia_cli='" + tiempo_residencia_cli +"',no_habitantes_cli=" + no_habitantes_cli +", condicion_propiedad_cli='" + condicion_propiedad_cli +"', a_paterno_esp='" + a_paterno_esp +"',a_materno_esp='" + a_materno_esp + "', nombre_esp='" + nombre_esp + "', nombre_completo_esp='" + nombre_completo_esp  + "',ocupacion_esp='" + ocupacion_esp + "',tel_trab_esp=" + tel_trab_esp +","+
                " tel_cas_esp=" + tel_casa_esp + ", tel_cel_esp=" + tel_cel_esp + ", calle_neg='" + calle_neg +"', no_exterior_neg='" + no_exterior_neg +"', no_interior_neg='" + no_interior_neg + "', colonia_neg='" + colionia_neg + "', cp_neg=" + cp_neg + ", del_mun_neg='" + del_mun_neg +"', estado_neg='" + estado_neg + "', tel_fijo_neg=" + tel_fijo_neg + ", antiguedad_neg='" + antiguedad_neg + "',razon_social_neg='" + razon_social_neg +"',"+
                " tipo_neg='" + tipo_neg + "', empleados_perma_neg=" + empleados_perma_neg + ", empleados_event_neg=" + empleados_even_neg + ", giro_principal_neg='" + giro_principal_neg + "', ingreso_men_gp_neg=" + ingreso_men_gp_neg + ", otras_activ_neg='" + otras_activ_neg + "', ingreso_men_oa_neg=" + ingreso_men_oa_neg + ", nombre_completo_ref='" + nombre_completo_ref + "', telefono_fijo_ref=" + telefono_fijo_ref +","+
                " telefono_celular_ref=" + telefono_celular_ref + ", parentesco_relacion_ref='" + parentesco_ref + "', tiempo_conocerlo_ref='" + tiempo_conocerlo_ref + "', cargo_preg_ref='" + cargo_preg_ref + "', cargo_desempeña_ref='" + cargo_desempeña_ref + "', dependencia_cargo_ref='" + dependencia_cargo_ref + "', periodo_cargo_ref='" + periodo_cargo_ref + "', prentesco_preg_ref='" + parentesco_preg_ref + "', nombre_parentesco_ref='" + nombre_parentesco_ref + "'," +
                " parentesco_ref='" + parentesco_ref +"', cargo_parentesco_ref='" + cargo_parentesco_ref +"', dependencia_parentesco_ref='" + dependencia_parentesco_ref +"', periodo_parentesco_ref='" +periodo_parentesco_ref + "', a_paterno_pr='" + a_paterno_pr +"', a_materno_pr='" + a_materno_pr +"', nombre_pr='" + nombre_pr + "', nombre_completo_pr='" + nombre_completo_pr + "', f_nacimiento_pr='" + f_nacimiento_pr + "',"+
                " e_nacimiento_pr='" + e_nacimiento_pr + "', nacionalidad_pr='" + nacionalidad_pr + "', genero_pr='" + genero_pr +"', estado_civil_pr='" + estado_civil_pr +"', no_credencial_ife_pr='" + no_credencial_ife_pr + "', curp_pr='" + curp_pr + "', rfc_pr='" + rfc_pr + "',nivel_escolaridad_pr='" + nivel_escolaridad + "',rol_propietario_real='" + rol_propietario_real + "',no_hijos_pr=" + no_hijos_pr + " ,dep_economicos_pr=" +dep_economicos_pr + " ,"+
                " ocupacion_pr='" + ocupacion_esp + "', tel_fijo_pr=" + tel_fijo_pr + ", tel_cel_pr=" + tel_cel_pr + ", correo_pr='" + correo_pr + "', callle_pr='" + calle_pr + "', no_exterior_pr='" + no_exterior_pr +"',colonia_pr='" + colonia_pr + "',cp_pr=" + cp_pr + ", del_mun_pr='"+ del_mun_pr + "', estado_pr='" + estado_pr + "', tiempo_residencia_pr='" + tiempo_residencia_pr + "', no_habitantes_pr=" + no_habitantes_pr +", a_paterno_prove='" + a_paterno_proveedor +"',"+
                " a_materno_prove='" + a_materno_proveedor + "', nombre_prove='" + nombre_proveedor +"', nombre_completo_prove='" + nombre_completo_proveedor + "', f_nacimiento_prove='" + f_nacimiento_proveedor + "', entidad_prove='" + e_nacimiento_proveedor + "', nacionalidad_prove='" + nacionalidad_proveedor + "', genero_prove='" + genero_proveedor + "', estado_civil_prove='" + estado_civil_proveedor + "', no_credencial_ife_prove='" + no_credencial_ife_proveedor + "',"+
                " curp_prove='" + curp_proveedor + "', rfc_prove='" + rfc_proveedor + "', nivel_escolaridad_prove='" + nivel_proveedor + "', rol_proveedor='" + rol_proveedor + "', no_hijos_prove=" + no_hijos_proveedor + " ,dep_economicos_prove=" + dep_economicos_proveedor + ", ocupacion_prove='" + ocupacion_proveedor + "', tel_fijo_prove=" + tel_fijo_proveedor + ", tel_cel_provee=" + tel_cel_proveedor + ", correo_prove='" + correo_proveedor + "',"+
                " calle_prove='" + calle_proveedor +"', no_exterior_prove='" + no_exterior_proveedor +"', no_interior_prove='" + no_interior_proveedor +"', colonia_prove='" + colonia_proveedor + "', cp_prove=" + cp_proveedor + ", estado_prove='" + estado_proveedor + "', tiempo_recidencia_prove='" + tiempo_residencia_proveedor + "', no_habitantes_prove=" + no_habitantes_proveedor + " correoRef1= "+ correoref1 + " correoRef2="+ correoref2 +
                "  WHERE id_empresa=" + empresa +" and id_sucursal=" + sucursal + " and id_ficha=" + IdEdita+" and id_cliente="+id_cliente ;
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void optieneimpresion() 
    {
        sql = "select * from an_ficha_datos where id_empresa=" + empresa + "and id_sucursal=" + sucursal + " and id_cliente=" + id_cliente+" and id_ficha="+id_ficha;
        retorno = ejecuta.dataSet(sql);
    }

    public void optieneimpresion2()
    {
        sql = "select nombre,apellido_p,apellido_m from an_clientes where id_empresa=" + empresa + "and id_sucursal=" + sucursal + " and id_cliente=" + id_cliente;
        retorno = ejecuta.dataSet(sql);
    }

    public void datoEstadoCp()
    {
        sql = "select d_estado from an_cp where d_codigo='"+d_codigo;
        retorno = ejecuta.dataSet(sql);
    }

}