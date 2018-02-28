/**
 * Created by alfredo on 5/07/17.
 */

////////////////////////////////////////////////////////////////////////////////////////////
//Funciones Utilitarias
////////////////////////////////////////////////////////////////////////////////////////////
// funcion para la validacion los valores del control: txtTasaMensual de tipo: input
// en el momento que pierda en foco (evento OnBlur
// Autor: Alfredo Gonzalez Davia
// fecha: 14 Ago 2017
// proyecto: Simulador de calculo de montos
// Cliente: Aser Negocio Juntos
////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////
// funcion de inicializacion de la aplicacion
////////////////////////////////////////////////////////////////////////////////////////////
function inicializa(){
    // construccion de evento click del boton calcular
    var x;
    x=$('#btncalcular');
    x.click(function(event) {
        event.preventDefault();
        calcular();
    });

}


////////////////////////////////////////////////////////////////////////////////////////////
// Funcion para dar formato al campo txtMontoPrestamo
////////////////////////////////////////////////////////////////////////////////////////////
function validaTasaMensual(){

    var ndatosEnTxTTasaMensual = 0.00;
    ndatosEnTxTTasaMensual =  $("#txtTasaMensual").val();
    if (ndatosEnTxTTasaMensual > 0){
        // s
        $("#txtTasaAnual").val( (((ndatosEnTxTTasaMensual/1.16)*360)/28));

    }
    $("#txtTasaAnual").mask("99.99");


}
////////////////////////////////////////////////////////////////////////////////////////////
// Funcion para boton calcular
////////////////////////////////////////////////////////////////////////////////////////////
function calcular(){
    //calculando tasa semanal
    var ndatosEnTasaAnual, ndatosEnTasaSemanal, ndatosEnTasaPorDia,  ndatosEnCapital, ndatosEnInteres, ndatosCapitalMasInteres,
        ndatoEnCapitalCuota, ndatoEnInteresCouta, ndatoEnCuota, nplazo;

    ndatosEnTasaAnual =0.00;
    ndatosEnTasaSemanal = 0.00;
    ndatosEnTasaPorDia = 0.00;
    ndatosEnCapital = 0.00;
    ndatosEnInteres = 0.00;
    ndatosCapitalMasInteres = 0.00;
    ndatoEnCapitalCuota = 0.00;
    ndatoEnInteresCouta = 0.00;
    ndatoEnCuota = 0.00;
    nplazo = 0.00;

    nplazo = parseFloat( $("#txtPlazo").val() ) ;

    ndatosEnTasaAnual = parseFloat( $("#txtTasaAnual").val() ) ;

    ndatosEnTasaSemanal = (ndatosEnTasaAnual/360)*7*(1.16);
    $("#txtTasaSemanal").val( ndatosEnTasaSemanal );
    //$("#txtTasaSemanal").mask("99.99");

    ndatosEnTasaPorDia = ndatosEnTasaSemanal/7;
    $("#txtTasaPorDia").val( ndatosEnTasaPorDia );
    //$("#txtTasaPorDia").mask("99.99");

    ndatosEnCapital = parseFloat( $("#txtMontoPrestamo").val() ) ;
    $("#txtCapital").val( ndatosEnCapital);
    //$("#txtCapital").mask("99,999,999.99");

    ndatosEnInteres = (ndatosEnCapital * nplazo * ndatosEnTasaSemanal)/100 ;
    $("#txtInteres").val( ndatosEnInteres );
    //$("#txtInteres").mask("99,999,999.99");

    ndatosCapitalMasInteres = ndatosEnCapital + ndatosEnInteres;
    $("#txtCapitalmasInteres").val( ndatosCapitalMasInteres );
    //$("#txtCapitalmasInteres").mask("99,999,999.99");

    ndatoEnCapitalCuota = ndatosEnCapital / ( nplazo );
    $("#txtCapitalCuota").val( ndatoEnCapitalCuota  );
    //$("#txtCapitalCuota").mask("99,999,999.99");

    ndatoEnInteresCouta = ndatosEnInteres / ( nplazo );
    $("#txtInteresCuota").val( ndatoEnInteresCouta );
    //$("#txtInteresCuota").mask("99,999,999.99");

    ndatoEnCuota = ndatoEnCapitalCuota +  ndatoEnInteresCouta;
    $("#txtCapitalmasInteresCuota").val( ndatoEnCuota );
    //$("#txtCapitalmasInteresCuota").mask("99,999,999.99");

    $("#txtCuotaRedondeada").val( Math.round( ndatoEnCuota ));
    //$("#txtCuotaRedondeada").mask("99,999,999");

}