<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cronologicos.aspx.cs" Inherits="Cronologicos" MasterPageFile="~/AdmOrdenes.master" Culture="es-Mx" UICulture="es-Mx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
    <style type="text/css">
        input, select, .uneditable-input{
            padding:0 !important;
        }
        .timePick {
            width: 90px;
            float: right;
            margin-top: 8px;
            MARGIN-RIGHT: 12px;
            align-self: auto;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="page-header">
        <!-- /BREADCRUMBS -->
        <div class="clearfix">
            <h3 class="content-title pull-left">Cronológico</h3>

        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-5 col-sm-5 text-center">
                    <asp:TextBox ID="txtNoOrden" runat="server" placeholder="No. Orden" CssClass="input-medium alingMiddle" />
                    <cc1:FilteredTextBoxExtender ID="txtNoOrden_FilteredTextBoxExtender"
                        runat="server" BehaviorID="txtNoOrden_FilteredTextBoxExtender"
                        TargetControlID="txtNoOrden" FilterType="Numbers" />
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnBuscarAsignaciones" runat="server" OnClick="btnBuscarAsignaciones_Click" CssClass="btn btn-info t14"><i class="fa fa-search"></i></asp:LinkButton>
                    <asp:Label ID="lblError" runat="server" CssClass="errores"></asp:Label>
                </div>
                <div class="col-lg-2 col-sm-2 text-center">
                    <asp:LinkButton ID="lnkGuardaFechas" runat="server" ToolTip="Guarda Cambios" CssClass="btn btn-success t14" OnClientClick="javascript:return valHoras();" OnClick="lnkGuardaFechas_Click" Visible="false"><i class="fa fa-save"></i><span>&nbsp;Guarda Cambios</span></asp:LinkButton>
                </div>
                 <div class="col-lg-2 col-sm-2 text-center">
                    <asp:LinkButton ID="lnkEnviarCarta" runat="server" ToolTip="Enviar Carta de Término" 
                        CssClass="btn btn-primary t14" onclick="lnkEnviarCarta_Click" Visible="false" ><i class="fa fa-paper-plane"></i><span>&nbsp;Enviar Carta T&eacute;rmino</span></asp:LinkButton>
                </div>
                <div class="col-lg-2 col-sm-2 text-right">
                    <asp:LinkButton ID="lnkRegresarOrdenes" runat="server" OnClick="lnkRegresarOrdenes_Click" CssClass="btn btn-info t14"><i class="fa fa-reply">&nbsp;&nbsp;</i><i class="fa fa-th-list"></i>&nbsp;<span>Órdenes</span></asp:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-sm-12">
                    <asp:Panel ID="pnlCronos" runat="server" CssClass="panelCatalogos text-center" ScrollBars="Auto" Visible="false">
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
                                        <asp:Label ID="Label4" runat="server" Text="Retorno Tránsito Programado:" CssClass="textoBold alingMiddle" /></div>
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
                                        <asp:Label ID="Label5" runat="server" Text="Entrega Expediente:" CssClass="textoBold alingMiddle" /></div>
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
                                        <asp:Label ID="Label7" runat="server" Text="Autorización:" CssClass="textoBold alingMiddle" /></div>
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
                                        <telerik:RadTimePicker RenderMode="Lightweight" ID="timph_estrega_estimada" Culture="es-Mx" DateInput-DateFormat="HH:mm:ss" DateInput-EmptyMessage="Hora" Skin="MetroTouch" Width="120px" runat="server"></telerik:RadTimePicker>                                    </div>
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
                                        <asp:Label ID="Label1" runat="server" Text="Entrega Pactada:" CssClass="textoBold alingMiddle" /></div>
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
                                        <asp:Label ID="Label6" runat="server" Text="Confirm. Entrega:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-5 col-sm-5 text-left">
                                        <asp:TextBox ID="txtf_confirmacion" runat="server" CssClass="alingMiddle input-small" Enabled="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender17" runat="server" BehaviorID="txtf_confirmacion_CalendarExtender" TargetControlID="txtf_confirmacion" Format="yyyy-MM-dd" PopupButtonID="lnkConfirmaEntrega" />
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
                                        <asp:Label ID="Label21" runat="server" Text="Localización:" CssClass="textoBold alingMiddle" /></div>
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
                                        <asp:Label ID="Label2" runat="server" Text="Perfil:" CssClass="textoBold alingMiddle" /></div>
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
                                        <asp:Label ID="Label3" runat="server" Text="Comentarios:" CssClass="textoBold alingMiddle" /></div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox runat="server" ID="txtComentario" CssClass="alingMiddle textNota" MaxLength="500" TextMode="MultiLine" Rows="10" />
                                        <cc1:TextBoxWatermarkExtender ID="txtComentario_TextBoxWatermarkExtender" runat="server" BehaviorID="txtComentario_TextBoxWatermarkExtender" TargetControlID="txtComentario" WatermarkCssClass="water textNota" WatermarkText="Comentario" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
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
                            <asp:Label ID="Label8" runat="server" Text="Usuario:" CssClass="textoBold" />
                        </div>
                        <div class="col-lg-8 col-sm-8 text-left">
                            <asp:TextBox ID="txtUsuarioLog" runat="server" CssClass="login input-large" MaxLength="20" placeholder="Usuario" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="*" ForeColor="Red" ValidationGroup="log" ControlToValidate="txtUsuarioLog" ErrorMessage="Debe indicar el usuario." CssClass="pull-right" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Text="*" ForeColor="Red" ValidationGroup="log" ErrorMessage="El usuario debe contener de entre 3 y 20 caracteres." CssClass="pull-right" ControlToValidate="txtUsuarioLog" ValidationExpression="[a-zA-Z0-9]{3,20}" />
                        </div>                                                
                        <div class="col-lg-4 col-sm-4 text-left padding-top-10">
                            <asp:Label ID="Label10" runat="server" Text="Contraseña:" CssClass="textoBold"/>&nbsp;
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
                    <asp:Panel ID="pnlMaskLoad1" runat="server" CssClass="maskLoad"></asp:Panel>
                    <asp:Panel ID="pnlCargando1" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoad1" runat="server" ImageUrl="~/img/loading.gif" CssClass="ancho100" />
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
