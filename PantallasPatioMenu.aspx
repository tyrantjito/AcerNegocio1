<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="PantallasPatioMenu.aspx.cs" Inherits="PantallasPatioMenu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="page-header">
                <!-- /BREADCRUMBS -->
                <div class="clearfix">
                    <h3 class="content-title pull-left">Pantallas Patio</h3>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-sm-12 text-center">
                    <asp:RadioButtonList ID="rbtnPantallas" runat="server" CellPadding="10" RepeatDirection="Horizontal"
                        CssClass="textoBold text-center" AutoPostBack="true" OnSelectedIndexChanged="rbtnPantallas_SelectedIndexChanged">
                        <asp:ListItem Text="Pantalla de Patio" Value="1" />
                        <asp:ListItem Text="Grupo Operación" Value="2" />
                        <asp:ListItem Text="Terminado" Value="3" />
                        <asp:ListItem Text="Entrega" Value="4" />
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-sm-12 text-left">
                    <asp:DropDownList ID="ddlGop" runat="server" CssClass="input-medium" DataSourceID="SqlDataSource1" DataTextField="descripcion_go" DataValueField="id_grupo_op"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:Taller %>' SelectCommand="SELECT [id_grupo_op], [descripcion_go] FROM [Grupo_Operacion]"></asp:SqlDataSource>
                    <asp:LinkButton ID="lnkVerPantalla" runat="server" CssClass="btn btn-success" OnClick="lnkVerPantalla_Click">Vista Preliminar</asp:LinkButton>&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkRegresarOrdenes" runat="server" OnClick="lnkRegresarOrdenes_Click" CssClass="btn btn-info t14"><i class="fa fa-reply">&nbsp;&nbsp;</i><i class="fa fa-th-list"></i>&nbsp;<span>Órdenes</span></asp:LinkButton>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12 col-sm-12 text-left">
                    <asp:Label ID="lblError" runat="server" CssClass="errores alert-danger"></asp:Label>
                </div>
            </div>

            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
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
