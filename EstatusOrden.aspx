<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="EstatusOrden.aspx.cs" Inherits="EstatusOrden" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-header">
                <!-- /BREADCRUMBS -->
                <div class="clearfix">
                    <h3 class="content-title pull-left">Cambio de Estatus Orden</h3>                    
                </div>
            </div>
            <div class="row pad1m">
                <div class="col-lg-5 col-sm-5 text-center">
                    <asp:Label ID="Label1" runat="server" Text="Orden:"></asp:Label>
                    <asp:TextBox ID="txtNoOrden" runat="server" placeholder="No. Orden" CssClass="input-medium alingMiddle" OnTextChanged="txtNoOrden_TextChanged" AutoPostBack="true" />
                    <cc1:FilteredTextBoxExtender ID="txtNoOrden_FilteredTextBoxExtender" runat="server" BehaviorID="txtNoOrden_FilteredTextBoxExtender" TargetControlID="txtNoOrden" FilterType="Numbers" />                    
                </div>
                <div class="col-lg-5 col-sm-5 text-center">
                    <asp:Label ID="lblEstatusInicial" runat="server" Visible="false"></asp:Label>
                   <asp:Label ID="Label2" runat="server" Text="Estatus a Indicar:"></asp:Label>
                    <asp:DropDownList ID="ddlEstatus" runat="server" AppendDataBoundItems="True" >                        
                        <asp:ListItem Selected="True" Value="A">Abierta</asp:ListItem>
                        <asp:ListItem Value="T">Completada</asp:ListItem>
                        <asp:ListItem Value="R">Remisionada</asp:ListItem>
                        <asp:ListItem Value="F">Facturada</asp:ListItem>
                        <asp:ListItem Value="C">Cerrada</asp:ListItem>
                        <asp:ListItem Value="S">Salida Sin Cargo</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-lg-2 col-sm-2 text-center">
                    <asp:LinkButton ID="lnkActualizarEstatus" runat="server" OnClick="lnkActualizarEstatus_Click" OnClientClick="return confirm('¿Está seguro de cambiar el estatus de la orden?')" CssClass="btn btn-info t14"><i class="fa fa-save"></i>&nbsp;<span>Actualizar estatus</span></asp:LinkButton>
                </div>
            </div>
            <div class="row text-center pad1m">
                <div class="col-lg-12 col-sm-12">
                    <asp:Label ID="lblError" runat="server" CssClass="errores"></asp:Label>
                </div>
            </div>

            <%--<asp:Panel ID="PanelMask" runat="server" CssClass="mask" Visible="false"></asp:Panel>
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
                            <asp:Label ID="Label14" runat="server" Text="Usuario:" CssClass="textoBold" />
                        </div>
                        <div class="col-lg-8 col-sm-8 text-left">
                            <asp:TextBox ID="txtUsuarioLog" runat="server" CssClass="login input-large" MaxLength="20" placeholder="Usuario" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="*" ForeColor="Red" ValidationGroup="log" ControlToValidate="txtUsuarioLog" ErrorMessage="Debe indicar el usuario." CssClass="pull-right" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Text="*" ForeColor="Red" ValidationGroup="log" ErrorMessage="El usuario debe contener de entre 3 y 20 caracteres." CssClass="pull-right" ControlToValidate="txtUsuarioLog" ValidationExpression="[a-zA-Z0-9]{3,20}" />
                        </div>                                                
                        <div class="col-lg-4 col-sm-4 text-left padding-top-10">
                            <asp:Label ID="Label15" runat="server" Text="Contraseña:" CssClass="textoBold"/>&nbsp;
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
                                <asp:LinkButton ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" CssClass="btn btn-success" ValidationGroup="log"><i class="fa fa-check"></i><span>&nbsp;Autorizar</span></asp:LinkButton>
                            </div>
                            <div class="col-lg-6 col-sm-6 text-center">
                                <asp:LinkButton ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" CssClass="btn btn-danger"><i class="fa fa-ban"></i><span>&nbsp;Cancelar</span></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                 </asp:Panel>
            </asp:Panel>--%>


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

