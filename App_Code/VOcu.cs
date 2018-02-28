using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de VOcu
/// </summary>
public class VOcu
{
    Ejecuciones ejecuta = new Ejecuciones();
    public int empresa { get; set; }
    public int sucursal { get; set; }
    public int ficha { get; set; } 
    public int idAdjunto { get; set; }
    public int id_grupo { get; set; }
    public int id_cliente { get; set; }
    public int id_visita { get; set; }
    public string fecha_visita { get; set; }
    public string grupo_productivo_visita { get; set; }
    public string tipo_credito_visita { get; set; }
    public string gerente_sucursal_visita { get; set; }
    public string asesor_credito_visita { get; set; }
    public int edad_visita_cliente { get; set; }
    public string cliente_desde_vis { get; set; } 
    public string entre_calles_visita_cli { get; set; }  
    public string tipo_vivienda_visita { get; set; }
    public char luz { get; set; }
    public char agua { get; set; }
    public char drenaje { get; set; }
    public char telefono { get; set; }
    public char internet { get; set; }
    public char gas { get; set; }
    public char tv_paga { get; set; }
    public string tipo_construccion { get; set; }
    public char sala { get; set; }
    public char comedor { get; set; }
    public char estufa { get; set; }
    public char refrigerador { get; set; }
    public char lavadora { get; set; }
    public char television { get; set; }
    public char computadora { get; set; }
    public string auto_visita { get; set; }
    public string marca_visita { get; set; }
    public string modelo_visita { get; set; }
    public string placas_visita { get; set; } 
    public string entre_calles_neg { get; set; }  
    public string caracteristicas_local_visita { get; set; }
    public string tiempo_neg_visita { get; set; }
    public string razon_social_neg_vis { get; set; }
    public string giro_negocio_visita { get; set; }
    public string principales_proveedores_neg_vis { get; set; }
    public string garantia_per_neg { get; set; }
    public string nombre_garantia_visi { get; set; }
    public string a_paterno_garantia_visi { get; set; }
    public string a_materno_garantia_visi { get; set; }
    public string fecha_nac_garantia_visi { get; set; }
    public int edad_garantia_visi { get; set; }
    public string genero_garantia { get; set; }
    public string ocupacion_garantia { get; set; }
    public string cuenta_bines_garantia { get; set; }
    public int valor_bienes_garantia { get; set; }
    public string calle_garantia { get; set; }
    public string num_ext_garantia { get; set; }
    public string num_int_garantia { get; set; }
    public string colonia_garantia { get; set; }
    public string cp_garantia { get; set; }
    public string mun_del_garantia { get; set; }
    public string estado_garantia { get; set; }
    public string entre_calles_garantia { get; set; }
    public decimal telefono_garantia { get; set; }



    public object[] retorno;
    private string sql;
    public VOcu()
    { 
         
    }

    public void agregarVisita()
    {
        sql = "insert into an_visita_ocular values (" + empresa + "," + sucursal + ",(select isnull((select top 1 id_visita from an_visita_ocular where id_empresa=" + empresa + " and id_sucursal=" + sucursal + "  order by id_visita desc),0)+1),"+id_cliente+","+id_grupo+",'" + fecha_visita+ "','"+grupo_productivo_visita+"','"+tipo_credito_visita+"','"+gerente_sucursal_visita+"','"+asesor_credito_visita+"',"+edad_visita_cliente+",'"+cliente_desde_vis+"','"+entre_calles_visita_cli+"','"+tipo_vivienda_visita+"','"+luz+"','"+agua+"','"+drenaje+"','"+telefono+"','"+internet+"','"+gas+"','"+tv_paga+"','"+tipo_construccion+"','"+sala+"','"+comedor+"','"+estufa+"','"+refrigerador+"','"+lavadora+"','"+television+"','"+computadora+"','"+auto_visita+"','"+marca_visita+"','"+modelo_visita+"','"+placas_visita+"','"+entre_calles_neg+"','"+caracteristicas_local_visita+"','"+tiempo_neg_visita+"','"+principales_proveedores_neg_vis+"','"+garantia_per_neg+"','"+nombre_garantia_visi+"','"+a_paterno_garantia_visi+"','"+a_materno_garantia_visi+"','"+fecha_nac_garantia_visi+"',"+edad_garantia_visi+",'"+genero_garantia+"','"+ocupacion_garantia+"','"+cuenta_bines_garantia+"',"+valor_bienes_garantia+",'"+calle_garantia+"','"+num_ext_garantia+"','"+num_int_garantia+"','"+colonia_garantia+"','"+cp_garantia+"','"+mun_del_garantia+"','"+estado_garantia+"','"+entre_calles_garantia+"',"+telefono_garantia+")";
        retorno = ejecuta.insertUpdateDelete(sql);
    }

    public void obtenerinfoFicha()
    {
        sql = "select f_nacimiento_cli,genero_cli,dep_economicos_cli,calle_cli,no_exterior_cli,no_interior_cli,colonia_cli,cp_cli,del_mun_cli,estado_cli,tel_fijo_cli,calle_neg,no_exterior_neg,no_interior_neg,colonia_neg,cp_neg,del_mun_neg,estado_neg,tel_fijo_neg,razon_social_neg,giro_principal_neg from an_ficha_datos where id_empresa=" + empresa + " and id_sucursal=" + sucursal + " and id_cliente=" + id_cliente ;
        retorno = ejecuta.dataSet(sql);
    }

    public void edicionVisita()
    {
        sql = "select * from an_visita_ocular where id_empresa=" + empresa + "and id_sucursal=" + sucursal + " and id_cliente="+id_cliente;
        retorno = ejecuta.dataSet(sql);
    }

    public void acutulizaVisita()
    {
        sql = "Update an_visita_ocular set fecha_visita='"+fecha_visita+ "',grupo_productivo_visita='"+grupo_productivo_visita+ "',tipo_credito_visita='"+tipo_credito_visita+"',gerente_sucursal_visita='"+ gerente_sucursal_visita + "',asesor_credito_visita='"+ asesor_credito_visita + "',edad_cli="+edad_visita_cliente+ ",cliente_desde_vis='"+cliente_desde_vis+ "',entre_calles_visita_cli='"+ entre_calles_visita_cli + "',tipo_vivienda_visita='"+ tipo_vivienda_visita + "',luz='"+luz+"',agua='"+agua+"',drenaje='"+drenaje+"',telefono='"+telefono+"',internet='"+internet+"',gas='"+gas+ "',tv_paga='"+tv_paga+ "',tipo_construccion='"+tipo_construccion+"',sala='"+sala+"',comedor='"+comedor+"',estufa='"+estufa+"',refrigerador='"+refrigerador+"',lavadora='"+lavadora+"',television='"+television+"',computadora='"+computadora+ "',auto_visita='"+ auto_visita + "',marca_visita='"+ marca_visita + "',modelo_visita='"+ placas_visita + "',entre_calles_neg='"+ entre_calles_neg + "',caracteristicas_local_visita='"+ caracteristicas_local_visita + "',tiempo_neg_visita='"+ tiempo_neg_visita + "',principales_proveedores_neg_vis='"+ principales_proveedores_neg_vis + "',garantia_per_neg='"+ garantia_per_neg + "',nombre_garantia_visi='"+ nombre_garantia_visi + "',a_paterno_garantia_visi='"+ a_paterno_garantia_visi + "',a_materno_garantia_visi='"+ a_materno_garantia_visi + "',fecha_nac_garantia_visi='"+fecha_nac_garantia_visi+ "',edad_garantia_visi="+edad_garantia_visi+ ",genero_garantia='"+ genero_garantia + "',ocupacion_garantia='"+ ocupacion_garantia + "',cuenta_bines_garantia='"+ cuenta_bines_garantia + "',valor_bienes_garantia="+ valor_bienes_garantia + ",calle_garantia='"+ calle_garantia + "',num_ext_garantia='"+ num_ext_garantia + "',num_int_garantia='"+ num_int_garantia + "',colonia_garantia='"+ colonia_garantia + "',cp_garantia='"+ cp_garantia + "',mun_del_garantia='"+ mun_del_garantia + "',estado_garantia='"+ estado_garantia + "',entre_calles_garantia='"+ entre_calles_garantia + "',telefono_garantia="+telefono_garantia+ " WHERE id_empresa=" + empresa + "and id_sucursal=" + sucursal + " and id_cliente=" + id_cliente;
        retorno = ejecuta.insertUpdateDelete(sql);
    }


    public void optieneimpresion()
    {
        sql = "select * from an_visita_ocular where id_empresa=" + empresa + "and id_sucursal=" + sucursal + " and id_cliente=" + id_cliente;
        retorno = ejecuta.dataSet(sql);
    }

    public void optieneimpresion1()
    {
        sql = "select b.nombre,b.apellido_p,b.apellido_m, d.f_nacimiento_cli,d.genero_cli,d.dep_economicos_cli,d.calle_cli,d.no_exterior_cli,d.no_interior_cli,d.colonia_cli,d.cp_cli,d.del_mun_cli,d.estado_cli,d.tel_cel_cli,d.calle_neg,d.no_exterior_neg,d.no_interior_neg,d.colonia_neg,d.cp_neg,d.del_mun_neg,d.estado_neg,d.tel_fijo_neg,d.razon_social_neg,d.giro_principal_neg from AN_Ficha_Datos D INNER JOIN AN_Clientes B on b.id_cliente = d.id_cliente  where d.id_empresa = " + empresa + "and d.id_sucursal = " + sucursal + " and d.id_cliente = " + id_cliente;
       retorno = ejecuta.dataSet(sql);
    }

}