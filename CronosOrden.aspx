<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CronosOrden.aspx.cs" Inherits="CronosOrden" MasterPageFile="~/AdmonOrden.master" Culture="es-Mx" UICulture="es-Mx" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">  
    <%-- %><script type="text/javascript">
        $(function(){
            var txtf_prog_retorno_tran = $get('<%=txtf_prog_retorno_tran.ClientID%>');
            var txtf_entrega_estimada = $get('<%=txtf_entrega_estimada.ClientID%>')
            var txtf_pactada = $get('<%=txtf_pactada.ClientID%>');
            var txtf_confirmacion = $get('<%=txtf_confirmacion.ClientID%>');
            var txtf_terminacion = $get('<%=txtf_terminacion.ClientID%>');

            if (txtf_prog_retorno_tran.value.trim() == "")
                $('#<%=timph_prog_retorno_tran.ClientID %>').find('input:text[id$="timph_prog_retorno_tran_txtHour"]').val('00');
                
            if (txtf_entrega_estimada.value.trim() == "")
                $('#<%=timph_estrega_estimada.ClientID %>').find('input:text[id$="timph_estrega_estimada_txtHour"]').val('00');

            if (txtf_pactada.value.trim() == "")
                $('#<%=timph_pactada.ClientID %>').find('input:text[id$="timph_pactada_txtHour"]').val('00');

            if (txtf_confirmacion.value.trim() == "")
                $('#<%=timph_confirmacion.ClientID %>').find('input:text[id$="timph_confirmacion_txtHour"]').val('00');

            if (txtf_terminacion.value.trim() == "")
                $('#<%=timph_terminacion.ClientID %>').find('input:text[id$="timph_terminacion_txtHour"]').val('00');
        
            return true;
        });

        function valHoras() {
            var txtf_prog_retorno_tran = $get('<%=txtf_prog_retorno_tran.ClientID%>');
            var txtf_entrega_estimada = $get('<%=txtf_entrega_estimada.ClientID%>')
            var txtf_pactada = $get('<%=txtf_pactada.ClientID%>');
            var txtf_confirmacion = $get('<%=txtf_confirmacion.ClientID%>');
            var txtf_terminacion = $get('<%=txtf_terminacion.ClientID%>');
            
            var hraProgRetTran = $('#<%=timph_prog_retorno_tran.ClientID %>').find('input:text[id$="timph_prog_retorno_tran_txtHour"]').val();
            var hraEntEst = $('#<%=timph_estrega_estimada.ClientID %>').find('input:text[id$="timph_estrega_estimada_txtHour"]').val();
            var hraPactada = $('#<%=timph_pactada.ClientID %>').find('input:text[id$="timph_pactada_txtHour"]').val();
            var hraConfirm = $('#<%=timph_confirmacion.ClientID %>').find('input:text[id$="timph_confirmacion_txtHour"]').val();
            var hraTermina = $('#<%=timph_terminacion.ClientID %>').find('input:text[id$="timph_terminacion_txtHour"]').val();
            var retorno = true;
            var mnsj = "Debes poner la hora para: \n";

            if (txtf_prog_retorno_tran.value.trim() != "" && hraProgRetTran == 00) {
                mnsj += "- Retorno Tránsito Programado.\n"
                retorno = false;
            }
                
            if (txtf_entrega_estimada.value.trim() != "" && hraEntEst == 00) {
                mnsj += "- Promesa.\n";
                retorno = false;
            }
                
            if (txtf_pactada.value.trim() != "" && hraPactada == 00) {
                mnsj += "- Entrega Pactada.\n";
                retorno = false;
            }
            if (txtf_confirmacion.value.trim() != "" && hraConfirm == 00) {
                mnsj += "- Confirmación de Entrega.\n";
                retorno = false;
            }
            if (txtf_terminacion.value.trim() != "" && hraTermina == 00) {
                mnsj += "- Entrega.";
                retorno = false;
            }

            if (retorno == false)
                alert(mnsj);

            return retorno;
        }

        function formatTime(txtFech) {
            txtLength = txtFech.value.length;
            var val = txtFech.value;
            var regexp1 = new RegExp(/^([0-2])$/);
            var regexp2 = new RegExp(/^([0-3])$/);
            
            if (txtLength == 1)
                if (val > 2)
                    txtFech.value = "0" + val + ":";

           if(txtLength==2 || txtLength == 2){
                txtFech.value = txtFech.value + ":";
                 return false;
            }
            else if (txtLength == 5)
                txtFech.value = txtFech.value + ":00";
            return false;
        }

    </script>--%>
    <style type="text/css">
        input, select, .uneditable-input{
            padding:0 !important;
        }
        .timePick {
            width: 100px;
            float: right;
            margin-top: 8px;
            MARGIN-RIGHT: 42px;
            align-self: auto;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"/>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-sm-12 text-center alert-info">
                    <h3>
                        <i class="fa fa-clock-o"></i>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label1" runat="server" Text="Cronológico"></asp:Label>                        
                    </h3>                    
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-sm-12 text-center">                     
                    <asp:Label ID="lblError" runat="server" CssClass="errores" />      
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlCronos" CssClass="paneles col-lg-12 col-sm-12" ScrollBars="Auto">
               <div class="row">
                            <div class="col-lg-8 col-sm-8">                                
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label87" runat="server" Text="Recepción:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:Label ID="lblFRecepcion" runat="server" CssClass="textoBold alingMiddle text-primary" />&nbsp;&nbsp;
                                            <asp:Label ID="lblHoraRecepcion" runat="server" CssClass="textoBold alingMiddle text-primary"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label2" runat="server" Text="Retorno Tránsito Programado:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_prog_retorno_tran" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender16" runat="server" TargetControlID="txtf_prog_retorno_tran" Format="yyyy-MM-dd" PopupButtonID="lnkFProgTra" />
                                        <asp:LinkButton ID="lnkFProgTra" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>                                        
                                        <telerik:RadTimePicker RenderMode="Lightweight" ID="timph_prog_retorno_tran" Culture="es-Mx" DateInput-DateFormat="HH:mm:ss" DateInput-EmptyMessage="Hora" Skin="MetroTouch" Width="120px" runat="server"></telerik:RadTimePicker>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina1" runat="server" CausesValidation="False" CommandArgument="1" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label88" runat="server" Text="Retorno Tránsito:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_retorno_transito" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtf_retorno_transito_CalendarExtender" runat="server" TargetControlID="txtf_retorno_transito" Format="yyyy-MM-dd" PopupButtonID="lnkFRTransito" />
                                        <asp:LinkButton ID="lnkFRTransito" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina2" runat="server" CausesValidation="False" CommandArgument="2" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label89" runat="server" Text="Alta:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_alta" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtf_alta" Format="yyyy-MM-dd" PopupButtonID="lnkAlta" />
                                        <asp:LinkButton ID="lnkAlta" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina3" runat="server" CausesValidation="False" CommandArgument="3" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label51" runat="server" Text="Entrega Expediente:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_entrega" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender14" runat="server" TargetControlID="txtf_entrega" Format="yyyy-MM-dd" PopupButtonID="lnkEntregaVehiculo" />
                                        <asp:LinkButton ID="lnkEntregaVehiculo" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina4" runat="server" CausesValidation="False" CommandArgument="4" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label14" runat="server" Text="Alta Portal:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_alta_portal" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtf_alta_portal" Format="yyyy-MM-dd" PopupButtonID="lnkAltaPortal" />
                                        <asp:LinkButton ID="lnkAltaPortal" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina5" runat="server" CausesValidation="False" CommandArgument="5" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label90" runat="server" Text="Valuación:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_valuacion" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtf_valuacion" Format="yyyy-MM-dd" PopupButtonID="lnkValuacion" />
                                        <asp:LinkButton ID="lnkValuacion" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina6" runat="server" CausesValidation="False" CommandArgument="6" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label71" runat="server" Text="Autorización:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_autorizacion" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtf_autorizacion" Format="yyyy-MM-dd" PopupButtonID="lnkAutorizado" />
                                        <asp:LinkButton ID="lnkAutorizado" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina7" runat="server" CausesValidation="False" CommandArgument="7" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label26" runat="server" Text="Complemento:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_complemento" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calext_txtf_complemento" runat="server" TargetControlID="txtf_complemento" Format="yyyy-MM-dd" PopupButtonID="lnkComplemento" />
                                        <asp:LinkButton ID="lnkComplemento" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina19" runat="server" CausesValidation="False" CommandArgument="19" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label13" runat="server" Text="Primer Llamada:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_primer_llamada" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtf_primer_llamada" Format="yyyy-MM-dd" PopupButtonID="lnkLlamada" />
                                        <asp:LinkButton ID="lnkLlamada" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina8" runat="server" CausesValidation="False" CommandArgument="8" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label17" runat="server" Text="Promesa:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_entrega_estimada" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="txtf_entrega_estimada" Format="yyyy-MM-dd" PopupButtonID="lnkEntregaEst" />
                                        <asp:LinkButton ID="lnkEntregaEst" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                        <telerik:RadTimePicker RenderMode="Lightweight" ID="timph_estrega_estimada" Culture="es-Mx" DateInput-DateFormat="HH:mm:ss" DateInput-EmptyMessage="Hora" Skin="MetroTouch" Width="120px" runat="server"></telerik:RadTimePicker>                                        
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina9" runat="server" CausesValidation="False" CommandArgument="9" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label9" runat="server" Text="Asignado:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_asignacion" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtf_asignacion" Format="yyyy-MM-dd" PopupButtonID="lnkAsignado" />
                                        <asp:LinkButton ID="lnkAsignado" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina10" runat="server" CausesValidation="False" CommandArgument="10" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label11" runat="server" Text="Tocado:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_tocado" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtf_tocado" Format="yyyy-MM-dd" PopupButtonID="lnkTocado" />
                                        <asp:LinkButton ID="lnkTocado" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina11" runat="server" CausesValidation="False" CommandArgument="11" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label16" runat="server" Text="Promesa Portal:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_promesa_portal" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txtf_promesa_portal" Format="yyyy-MM-dd" PopupButtonID="lnkPromesaPortal" />
                                        <asp:LinkButton ID="lnkPromesaPortal" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina12" runat="server" CausesValidation="False" CommandArgument="12" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label15" runat="server" Text="Última Entrega Ref.:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_ult_entrega_ref" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtf_ult_entrega_ref" Format="yyyy-MM-dd" PopupButtonID="lnkUltRef" />
                                        <asp:LinkButton ID="lnkUltRef" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina13" runat="server" CausesValidation="False" CommandArgument="13" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label18" runat="server" Text="Terminado:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_terminado" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender11" runat="server" TargetControlID="txtf_terminado" Format="yyyy-MM-dd" PopupButtonID="lnkTerminado" />
                                        <asp:LinkButton ID="lnkTerminado" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina14" runat="server" CausesValidation="False" CommandArgument="14" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label19" runat="server" Text="Baja Portal:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_baja_portal" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender12" runat="server" TargetControlID="txtf_baja_portal" Format="yyyy-MM-dd" PopupButtonID="lnkBajaPortal" />
                                        <asp:LinkButton ID="lnkBajaPortal" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina15" runat="server" CausesValidation="False" CommandArgument="15" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label3" runat="server" Text="Entrega Pactada:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_pactada" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender15" runat="server" TargetControlID="txtf_pactada" Format="yyyy-MM-dd" PopupButtonID="lnkEntregaPac" />
                                        <asp:LinkButton ID="lnkEntregaPac" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                        <telerik:RadTimePicker RenderMode="Lightweight" ID="timph_pactada" Culture="es-Mx" DateInput-DateFormat="HH:mm:ss" DateInput-EmptyMessage="Hora" Skin="MetroTouch" Width="120px" runat="server"></telerik:RadTimePicker>                                        
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina16" runat="server" CausesValidation="False" CommandArgument="16" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label23" runat="server" Text="Confirmación Entrega:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_confirmacion" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender17" runat="server" TargetControlID="txtf_confirmacion" Format="yyyy-MM-dd" PopupButtonID="lnkConfirmaEntrega" />
                                        <asp:LinkButton ID="lnkConfirmaEntrega" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                        <telerik:RadTimePicker RenderMode="Lightweight" ID="timph_confirmacion" Culture="es-Mx" DateInput-DateFormat="HH:mm:ss" DateInput-EmptyMessage="Hora" Skin="MetroTouch" Width="120px" runat="server"></telerik:RadTimePicker>                                        
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina17" runat="server" CausesValidation="False" CommandArgument="17" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label20" runat="server" Text="Entrega:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_terminacion" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender13" runat="server" TargetControlID="txtf_terminacion" Format="yyyy-MM-dd" PopupButtonID="lnkEntregaReal" />
                                        <asp:LinkButton ID="lnkEntregaReal" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                                        <telerik:RadTimePicker RenderMode="Lightweight" ID="timph_terminacion" Culture="es-Mx" DateInput-DateFormat="HH:mm:ss" DateInput-EmptyMessage="Hora" Skin="MetroTouch" Width="120px" runat="server"></telerik:RadTimePicker>                                        
                                    </div>
                                    <div style="float:left">
                                        <asp:LinkButton ID="btnElimina18" runat="server" CausesValidation="False" CommandArgument="18" ToolTip="Eliminar" CssClass="btn btn-danger t14" OnClick="btnEliminaFecha_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 col-sm-4">                                
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label211" runat="server" Text="Localización:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblLocalizacionIni" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblAvanceIni" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblFaseIni" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPerfilIni" runat="server" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlLocalizacion" runat="server" DataSourceID="SqlDataSource26" DataTextField="descripcion" DataValueField="id_localizacion" CssClass="alingMiddle input-large"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource26" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_localizacion, descripcion from Localizaciones"></asp:SqlDataSource>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label5" runat="server" Text="Perfil:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-3 col-sm-3 text-left">                                        
                                        <asp:DropDownList ID="ddlPerfil" runat="server" DataSourceID="SqlDataSource1" DataTextField="descripcion" DataValueField="id_perfilOrden" CssClass="alingMiddle input-large"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_perfilOrden, descripcion from perfilesOrdenes"></asp:SqlDataSource>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label22" runat="server" Text="% Avance:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txtAvance" runat="server" MaxLength="5" CssClass="alingMiddle input-mini" Enabled="false"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" BehaviorID="txtAvance_FilteredTextBoxExtender" TargetControlID="txtAvance" FilterType="Numbers, Custom" ValidChars="." />

                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label7" runat="server" Text="Comentarios:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox runat="server" ID="txtComentario" CssClass="alingMiddle textNota" MaxLength="500" TextMode="MultiLine" Rows="10" />
                                        <cc1:TextBoxWatermarkExtender ID="txtComentario_TextBoxWatermarkExtender" runat="server" BehaviorID="txtComentario_TextBoxWatermarkExtender" TargetControlID="txtComentario" WatermarkCssClass="water textNota" WatermarkText="Comentario" />
                                    </div>
                                </div>
                            </div>
                        </div>
            </asp:Panel>
            <div class="row pad1m">                                        
                <div class="col-lg-4 col-sm-4 text-center">
                    <asp:LinkButton ID="lnkGuardaFechas" runat="server" ToolTip="Guarda Cambios" OnClientClick="javascript:return valHoras();"
                        CssClass="btn btn-success t14" onclick="lnkGuardaFechas_Click" ><i class="fa fa-save"></i><span>&nbsp;Guarda Cambios</span></asp:LinkButton>
                </div>                                                     
                <div class="col-lg-2 col-sm-2 text-center">
                    <asp:LinkButton ID="lnkEnviarCarta" runat="server" ToolTip="Enviar Carta de Término" 
                        CssClass="btn btn-primary t14" onclick="lnkEnviarCarta_Click" ><i class="fa fa-paper-plane"></i><span>&nbsp;Enviar Carta T&eacute;rmino</span></asp:LinkButton>
                </div>
                <div class="col-lg-2 col-sm-2 text-center">
                    <asp:LinkButton ID="lnkNotificar" runat="server" ToolTip="Notificar" 
                        CssClass="btn btn-warning t14" onclick="lnkNotificar_Click" ><i class="fa fa-bell"></i><span>&nbsp;Notificar Localizaci&oacute;n</span></asp:LinkButton>
                </div>
                 
                <div class="col-lg-4 col-sm-4 text-center">
                    <asp:LinkButton ID="lnkNotificarSalida" runat="server" ToolTip="Notificar" 
                        CssClass="btn btn-warning t14" onclick="lnkNotificarSalida_Click" ><i class="fa fa-bell"></i><span>&nbsp;Notificar Salida</span></asp:LinkButton>
                </div>               
            </div>

            <div class="pie pad1m">		                                		                                
		        <div class="clearfix">
			        <div class="row colorBlanco textoBold">
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label21" runat="server" Text="Tipo Orden:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlToOrden" runat="server" ></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label4" runat="server" Text="Cliente:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlClienteOrden" runat="server" ></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label6" runat="server" Text="Tipo Servicio:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlTsOrden" runat="server" ></asp:Label>
                        </div>
                    </div>                                              
                    <div class="row colorBlanco textoBold">
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label8" runat="server" Text="Tipo Valuación:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlValOrden" runat="server" ></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label10" runat="server" Text="Tipo Asegurado:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlTaOrden" runat="server" ></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label12" runat="server" Text="Localización:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlLocOrden" runat="server" ></asp:Label>
                        </div>
                    </div>    
                    <div class="row colorBlanco textoBold">
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label108" runat="server" Text="Perfil:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblPerfilPie" runat="server" ></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label109" runat="server" Text="Siniestro:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblSiniestro" runat="server" ></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label110" runat="server" Text="Deducible:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblDeducible" runat="server" ></asp:Label>
                        </div>
                    </div>  
                    <div class="row colorBlanco textoBold">
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label111" runat="server" Text="Total Orden:" CssClass="colorEtiqueta" Visible="false"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblTotOrden" runat="server" Visible="false" ></asp:Label>
                        </div>  
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label112" runat="server" Text="Promesa:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblEntregaEstimada" runat="server" ></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="lblPorcDeduEti" runat="server" Text="% Deducible:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblPorcDedu" runat="server" ></asp:Label>
                        </div>                      
                    </div>
		        </div>
            </div>

            <asp:Panel ID="PanelMask" runat="server" CssClass="mask" Visible="false"></asp:Panel>
            <asp:Panel ID="PanelPopUpPermiso" runat="server" CssClass="popUp zen2  textoCentrado ancho40 centrado" Visible="false">                
                <table class="ancho100">
                    <tr class="ancho100 centrado">
                        <td class="ancho100 text-center encabezadoTabla roundTopLeft ">
                            <asp:Label ID="lblPop" runat="server" Text="Autorización" CssClass="t22 colorMorado textoBold" />                              
                        </td>                        
                    </tr>                        
                </table>
                <asp:Panel ID="prov" runat="server" CssClass="ancho80 centrado">
                    <div class="row">
                        <div class="col-lg-4 col-sm-4 text-left">
                            <asp:Label ID="Label24" runat="server" Text="Usuario:" CssClass="textoBold" />
                        </div>
                        <div class="col-lg-8 col-sm-8 text-left">
                            <asp:TextBox ID="txtUsuarioLog" runat="server" CssClass="login input-large" MaxLength="20" placeholder="Usuario" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="*" ForeColor="Red" ValidationGroup="log" ControlToValidate="txtUsuarioLog" ErrorMessage="Debe indicar el usuario." CssClass="pull-right" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Text="*" ForeColor="Red" ValidationGroup="log" ErrorMessage="El usuario debe contener de entre 3 y 20 caracteres." CssClass="pull-right" ControlToValidate="txtUsuarioLog" ValidationExpression="[a-zA-Z0-9]{3,20}" />
                        </div>                                                
                        <div class="col-lg-4 col-sm-4 text-left padding-top-10">
                            <asp:Label ID="Label25" runat="server" Text="Contraseña:" CssClass="textoBold"/>&nbsp;
                        </div>
                        <div class="col-lg-8 col-sm-8 text-left padding-top-10">
                            <asp:TextBox ID="txtContraseñaLog" runat="server" CssClass="login input-large" TextMode="Password" MaxLength="20" placeholder="Contraseña" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Text="*" ForeColor="Red" ValidationGroup="log" ControlToValidate="txtContraseñaLog" ErrorMessage="Debe indicar la contraseña." CssClass="pull-right" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Text="*" ForeColor="Red" ValidationGroup="log" ErrorMessage="La contraseña debe contener de entre 5 y 20 caracteres." CssClass="pull-right" ControlToValidate="txtContraseñaLog" ValidationExpression="[a-zA-Z0-9]{5,20}" />
                        </div>                        
                        <div class="field col-lg-12 col-sm-12 textoCentrado textoBold padding-top-10">
                            <div class="col-lg-12 col-sm-12 text-center">
                                <asp:Label ID="lblErrorLog" runat="server" CssClass="errores" />
                            </div>
                            <div class="col-lg-12 col-sm-12 text-center">
                                <asp:ValidationSummary ID="ValidationSummary4" ValidationGroup="log" CssClass="errores" runat="server" DisplayMode="List" />
                            </div>
                        </div>
                        <div class="col-lg-12 col-sm-12 text-center pad1m">
                            <div class="col-lg-6 col-sm-6 text-center">
                                <asp:LinkButton ID="btnAutoriza" runat="server" OnClick="btnAutoriza_Click" CssClass="btn btn-success" ValidationGroup="log"><i class="fa fa-check"></i><span>&nbsp;Autorizar</span></asp:LinkButton>
                            </div>
                            <div class="col-lg-6 col-sm-6 text-center">
                                <asp:LinkButton ID="btnCancelAut" OnClick="btnCancelAut_Click" runat="server" CssClass="btn btn-danger"><i class="fa fa-ban"></i><span>&nbsp;Cancelar</span></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                 </asp:Panel>
            </asp:Panel>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <asp:Panel ID="pnlMaskLoad" runat="server" CssClass="maskLoad"></asp:Panel>
                    <asp:Panel ID="pnlCargando" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoad" runat="server" ImageUrl="~/img/loading.gif" CssClass="ancho100" />                            
                    </asp:Panel>
                </ProgressTemplate>                            
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

 </asp:Content>
