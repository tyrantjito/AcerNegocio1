﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="catBancos.aspx.cs" Inherits="catBancos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <script type="text/javascript">
        function abreNewEmi() {
            var oWnd = $find("<%=modalNuevo.ClientID%>");
            oWnd.setUrl('');
            oWnd.show();
        }

        function cierraNewEmi() {
            var oWnd = $find("<%=modalNuevo.ClientID%>");
            oWnd.close();
        }

        function abreModEmi() {
            var oWnd = $find("<%=modalModifica.ClientID%>");
            oWnd.setUrl('');
            oWnd.show();
        }

        function cierraModEmi() {
            var oWnd = $find("<%=modalModifica.ClientID%>");
            oWnd.close();
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" />    
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" />
    <telerik:RadFormDecorator ID="RadFormDecorator1" RenderMode="Lightweight" runat="server" DecoratedControls="Buttons" />

     <%-- Nueva Moneda --%>
    <telerik:RadWindow RenderMode="Lightweight" ID="modalNuevo" Title="Nueva Moneda" EnableShadow="true" Skin="Metro"
        Behaviors="Move,Close,Resize,Pin" VisibleOnPageLoad="false" ShowContentDuringLoad="false" DestroyOnClose="true" 
        Animation="Fade" runat="server" Modal="true" Width="900px" Height="250px" Style="z-index: 1000;">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanelEmi" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlPrincipal" runat="server" CssClass="ancho100 text-center">
                        <div class="col-lg-12 col-sm-12 text-center">
                            <asp:Label ID="lblErrorNuevo" runat="server"></asp:Label>
                           
                        </div>                 
                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label4" runat="server" Text="Clave Banco:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtClaveMon" runat="server" CssClass="input-xxlarge" MaxLength="128" />
                            
                                                     
                        </div>

                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label5" runat="server" Text="Nombre:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtDes" runat="server" MaxLength="100" CssClass="input-xxlarge"></asp:TextBox>
                            
                           
                        </div>

                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label6" runat="server" Text="Convenio:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtConv" runat="server" MaxLength="100" CssClass="input-xxlarge"></asp:TextBox>
                            
                           
                        </div>

                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label7" runat="server" Text="Referencia:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtRef" runat="server" MaxLength="100" CssClass="input-xxlarge"></asp:TextBox>
                            
                           
                        </div>

                       
                         <div class="col-lg-6 col-sm-6 text-center pad1m">
                            <asp:LinkButton ID="lnkAgregarNuevo" runat="server" CssClass="btn btn-success t14" ValidationGroup="agrega" OnClick="lnkAgregarNuevo_Click" ><i class="fa fa-check-circle"></i>&nbsp; Agregar</asp:LinkButton>
                        </div>
                        <div class="col-lg-6 col-sm-6 text-center pad1m">
                            <asp:LinkButton ID="lnkCancelarNuevo" runat="server" CssClass="btn btn-danger t14" ><i class="fa fa-remove"></i>&nbsp; Cancelar</asp:LinkButton>
                        </div>
                    </asp:Panel>
                    <asp:UpdateProgress ID="updProgEmi" runat="server" AssociatedUpdatePanelID="UpdatePanelEmi">
                        <ProgressTemplate>
                            <asp:Panel ID="pnlMaskLoadEmi" runat="server" CssClass="maskLoad" />
                            <asp:Panel ID="pnlCargandoEmi" runat="server" CssClass="pnlPopUpLoad">
                                <asp:Image ID="imgLoadEmi" runat="server" ImageUrl="~/img/loading.gif" CssClass="center" />
                            </asp:Panel>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>

     <%-- Modifica Empresa --%>
    <telerik:RadWindow RenderMode="Lightweight" ID="modalModifica" Title="Edita Moneda" EnableShadow="true" Skin="Metro"
        Behaviors="Move,Resize,Pin,Close" VisibleOnPageLoad="false" ShowContentDuringLoad="false" DestroyOnClose="true" 
        Animation="Fade" runat="server" Modal="true" Width="900px" Height="250px" Style="z-index: 1000;">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanelEmiMod" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server" CssClass="ancho100 text-center">
                        <div class="col-lg-12 col-sm-12 text-center">
                            <asp:Label ID="Label1" runat="server" CssClass="errores"></asp:Label>
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="agrega" CssClass="errores" DisplayMode="List" />
                        </div>                 
                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label2" runat="server" Text="Clave Banco:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtClaveMonEdt" runat="server" CssClass="input-xxlarge" MaxLength="128" />
                           
                        </div>

                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label3" runat="server" Text="Nombre:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtDesEdt" runat="server" MaxLength="100" CssClass="input-xxlarge"></asp:TextBox>
                           
                        </div>

                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label9" runat="server" Text="Convenio:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtConvEd" runat="server" MaxLength="100" CssClass="input-xxlarge"></asp:TextBox>
                           
                        </div>

                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label10" runat="server" Text="Referencia:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtRefEd" runat="server" MaxLength="100" CssClass="input-xxlarge"></asp:TextBox>
                           
                        </div>

                        
                         <div class="col-lg-6 col-sm-6 text-center pad1m">
                            <asp:LinkButton ID="BtnActualizar" runat="server" CssClass="btn btn-success t14" OnClick="BtnActualizar_Click" ValidationGroup="agrega" ><i class="fa fa-check-circle"></i>&nbsp; Agregar</asp:LinkButton>
                        </div>
                        <div class="col-lg-6 col-sm-6 text-center pad1m">
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-danger t14" ><i class="fa fa-remove"></i>&nbsp; Cancelar</asp:LinkButton>
                        </div>
                    </asp:Panel>
                    <asp:UpdateProgress ID="updProgEmiMod" runat="server" AssociatedUpdatePanelID="UpdatePanelEmiMod">
                        <ProgressTemplate>
                            <asp:Panel ID="pnlMaskLoadEmiMod" runat="server" CssClass="maskLoad" />
                            <asp:Panel ID="pnlCargandoEmiMod" runat="server" CssClass="pnlPopUpLoad">
                                <asp:Image ID="imgLoadEmiMod" runat="server" ImageUrl="~/img/loading.gif" CssClass="center" />
                            </asp:Panel>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>

   <div class="page-header">
                <!-- /BREADCRUMBS -->
                <div class="clearfix">
                    <h3 class="content-title pull-left">Bancos</h3>
                    <asp:Label ID="Label8" runat="server" CssClass="alert-danger"></asp:Label>
                </div>
            </div>


     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>            
            <div class="col-lg-12 col-sm-12 text-right">
                 <div class="col-lg-3 col-sm-3 text-center">
                <asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-info t14" ToolTip="Agregar" OnClientClick="abreNewEmi()"><i class="fa fa-plus-circle"></i>&nbsp;<span>Agregar</span></asp:LinkButton>
                     </div>
                <div class="col-lg-3 col-sm-3 text-center">
                <asp:LinkButton ID="btnEditar" runat="server" Visible="false" ToolTip="Editar" CssClass="btn btn-warning t14" onclick="btnEditar_Click"><i class="fa fa-edit"></i>&nbsp;<span>Editar</span></asp:LinkButton>
                </div>
                 <div class="col-lg-3 col-sm-3 text-center">
                <asp:LinkButton ID="btnEliminar" runat="server" Visible="false" ToolTip="Eliminar" CssClass="btn btn-danger" onclick="btnEliminar_Click"><i class="fa fa-times"></i>&nbsp;<span>Eliminar</span></asp:LinkButton>
                     </div>
                <div class="col-lg-3 col-sm-3 text-center">
                <asp:LinkButton ID="btnSelec" runat="server" Visible="false" ToolTip="Seleccionar" CssClass="btn btn-primary" onclick="btnEliminar_Click"><i class="fa fa-check"></i>&nbsp;<span>Seleccionar</span></asp:LinkButton>
                    </div>
            </div>
            <div class="row pad1m">
                <div class="col-lg-12 col-sm-12 text-center">
                    <asp:Label ID="lblError" runat="server" CssClass="errores" />
                </div>
            </div>
            <asp:Panel ID="pnlContenido" CssClass="col-lg-12 col-sm-12" runat="server" ScrollBars="Auto">
                <asp:Panel ID="pnlCatalogos" runat="server" CssClass="col-lg-12 col-sm-12" ScrollBars="Auto">
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" EnableAJAX="true">
                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" AllowFilteringByColumn="true"  OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Metro" 
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource1" AllowSorting="true" GroupingEnabled="false" PageSize="50" >
                        <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="false" DataKeyNames="Clave">
                            <Columns>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="Clave" FilterControlAltText="Filtro Clave" HeaderText="Clave" SortExpression="Clave" UniqueName="Clave" Resizable="true"  />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="Nombre" FilterControlAltText="Filtro Nombre" HeaderText="Nombre" SortExpression="Nombre" UniqueName="Nombre" Resizable="true" />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="Convenio" FilterControlAltText="Filtro Convenio" HeaderText="Convenio" SortExpression="Convenio" UniqueName="Convenio" Resizable="true" />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="Referencia" FilterControlAltText="Filtro Referencia" HeaderText="Referencia" SortExpression="Referencia" UniqueName="Referencia" Resizable="true" />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="select * from an_catBancos " ConnectionString="<%$ ConnectionStrings:Taller %>">
                </asp:SqlDataSource>
                </asp:Panel>
            </asp:Panel>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <asp:Panel ID="pnlMaskLoad" runat="server" CssClass="maskLoad">
                    </asp:Panel>
                    <asp:Panel ID="pnlCargando" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoad" runat="server" ImageUrl="~/img/loading.gif" CssClass="center" />
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

