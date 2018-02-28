<%@ Page Title="" Language="C#" MasterPageFile="~/AdmonOrden.master" AutoEventWireup="true" CodeFile="ValidacionesCredito.aspx.cs" Inherits="ValidacionesCredito" %>

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
    <telerik:RadWindow RenderMode="Lightweight" ID="modalNuevo" Title="Observaciones" EnableShadow="true" Skin="Metro"
        Behaviors="Move,Close,Resize,Pin" VisibleOnPageLoad="false" ShowContentDuringLoad="false" DestroyOnClose="true" 
        Animation="Fade" runat="server" Modal="true" Width="900px" Height="200px" Style="z-index: 1000;">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanelEmi" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlPrincipal" runat="server" CssClass="ancho100 text-center">
                        <div class="col-lg-12 col-sm-12 text-center">
                            <asp:Label ID="lblErrorNuevo" runat="server"></asp:Label>
                           
                        </div>                 
                        

                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label5" runat="server" Text="Observacion:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtObservacion" runat="server" MaxLength="100" CssClass="input-xxlarge"></asp:TextBox>
                        </div>
                         <div class="col-lg-3 col-sm-3 text-left"></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtValiId" runat="server" Visible="false" MaxLength="100" CssClass="input-xxlarge"></asp:TextBox>
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
    <telerik:RadWindow RenderMode="Lightweight" ID="modalModifica" Title="Observaciones no cumple" EnableShadow="true" Skin="Metro"
        Behaviors="Move,Resize,Pin,Close" VisibleOnPageLoad="false" ShowContentDuringLoad="false" DestroyOnClose="true" 
        Animation="Fade" runat="server" Modal="true" Width="900px" Height="200px" Style="z-index: 1000;">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanelEmiMod" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server" CssClass="ancho100 text-center">
                        <div class="col-lg-12 col-sm-12 text-center">
                            <asp:Label ID="Label1" runat="server" CssClass="errores"></asp:Label>
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="agrega" CssClass="errores" DisplayMode="List" />
                        </div>        
                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label3" runat="server" Text="Observaciones:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtObservacionEDT" runat="server" MaxLength="100" CssClass="input-xxlarge"></asp:TextBox>
                        </div>

                         <div class="col-lg-3 col-sm-3 text-left"></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtidVali" runat="server" Visible="false" MaxLength="100" CssClass="input-xxlarge"></asp:TextBox>
                        </div>

                         <div class="col-lg-6 col-sm-6 text-center pad1m">
                            <asp:LinkButton ID="BtnActualizar" runat="server" CssClass="btn btn-success t14" OnClick="BtnActualizar_Click" ValidationGroup="agrega" ><i class="fa fa-check-circle"></i>&nbsp; Agregar</asp:LinkButton>
                             <asp:LinkButton ID="btnActua" runat="server" Visible="false" CssClass="btn btn-success t14" OnClick="btnActua_Click" ValidationGroup="agrega" ><i class="fa fa-check-circle"></i>&nbsp; Actualizar</asp:LinkButton>
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
                    <h3 class="content-title pull-left">Validaciones</h3>
                    <asp:Label ID="Label8" runat="server" CssClass="alert-danger"></asp:Label>
                </div>
            </div>

     <div class="col-lg-12 col-sm-12 text-right">
                 <div class="col-lg-6 col-sm-6 text-center">
                <asp:LinkButton ID="btnValidacion" runat="server" CssClass="btn btn-info t14" OnClick="btnValidacion_Click" ><i class="fa fa-thumbs-o-up"></i>&nbsp;<span>Validación</span></asp:LinkButton>
                     </div>
            </div>
    <br /><br />
          <br /><br />

    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" EnableAJAX="true">
                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid2" AllowFilteringByColumn="true"  runat="server" Height="350" OnItemDataBound="RadGrid2_ItemDataBound"  EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Metro" 
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource2" AllowSorting="true" GroupingEnabled="false" PageSize="50" >
                        <MasterTableView DataSourceID="SqlDataSource2"    AutoGenerateColumns="false" DataKeyNames="id_cliente">
                           <Columns>
                               <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" HeaderText="ID"  SortExpression="id_cliente" UniqueName="id_cliente" FilterControlAltText="Filtro Crédito" DataField="id_cliente">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnID" CssClass="btn btn-info" runat="server"  CommandArgument='<%# Bind("id_cliente") %>'  Text='<%# Bind("id_cliente") %>' ></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>     
                               <telerik:GridBoundColumn HeaderStyle-Width="45%" FilterCheckListEnableLoadOnDemand="true" DataField="nombre_cliente" FilterControlAltText="Filtro nombre_cliente" HeaderText="nombre_cliente" SortExpression="nombre_cliente" UniqueName="nombre_cliente"  />
                               <telerik:GridBoundColumn HeaderStyle-Width="25%" FilterCheckListEnableLoadOnDemand="true" DataField="ciclo" FilterControlAltText="Filtro ciclo" HeaderText="ciclo" SortExpression="ciclo" UniqueName="ciclo" />
                               <telerik:GridBoundColumn HeaderStyle-Width="25%" FilterCheckListEnableLoadOnDemand="true" DataField="EstatusCompleto" FilterControlAltText="Filtro EstatusCompleto" HeaderText="EstatusCompleto" SortExpression="EstatusCompleto" UniqueName="EstatusCompleto" />
                           </Columns>
                        </MasterTableView>
                        <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="select  d.id_cliente,d.nombre_cliente,d.ciclo,g.EstatusCompleto from AN_Solicitud_Credito_Detalle d left join AN_ValidacionesGrupos g on d.id_cliente = g.Id_Cliente where g.id_credito=@grupo and g.Id_Validacion=1" ConnectionString="<%$ ConnectionStrings:Taller %>">
                     <SelectParameters>
                    <asp:QueryStringParameter Name="grupo" QueryStringField="c" DefaultValue="0"/>
                    </SelectParameters>
                </asp:SqlDataSource>
                             <asp:Label ID="lblint" runat="server" Visible="false" CssClass="alert-danger"></asp:Label>
     <asp:UpdatePanel ID="UpdatePanelok" runat="server">
         <ContentTemplate>
              <asp:Panel ID="pnlMask" runat="server" CssClass="mask zen1" Visible="false" />
                <asp:Panel ID="windowAutorizacion" runat="server" CssClass="popUp zen2 textoCentrado ancho80" Height="50%" ScrollBars="Auto"  Visible="false">
                    <table class="ancho100">
                    <tr class="ancho100%">
                         <div class="page-header" >
                <div class="clearfix">
                     <div class=" col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                     <asp:LinkButton ID="LinkButton6" runat="server" CssClass="btn btn-danger alingMiddle" OnClick="LinkButton6_Click" ToolTip="Cerrar"><i class="fa fa-remove t18"></i></asp:LinkButton>
                 </div>
                    <h3 class="content-title center">Validación Cliente
                    </h3>
                </div>
            </div></tr>
                </table>
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>            
            <div class="col-lg-12 col-sm-12 text-right">
                
            </div>
            <div class="row pad1m">
                <div class="col-lg-12 col-sm-12 text-center">
                    <asp:Label ID="lblError" runat="server" CssClass="errores" />
                </div>
            </div>
            <asp:Panel ID="pnlContenido" CssClass="col-lg-12 col-sm-12" runat="server" ScrollBars="Auto">
                <asp:Panel ID="pnlCatalogos" runat="server" CssClass="col-lg-12 col-sm-12" ScrollBars="Auto">
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" EnableAJAX="true">
                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1"  runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Metro" OnItemDataBound="RadGrid1_ItemDataBound"
                                 AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource1" AllowSorting="true" GroupingEnabled="false" PageSize="50" >
                        <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="false" DataKeyNames="id_validacion">
                            <Columns>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="id_validacion" FilterControlAltText="Filtro Clave" HeaderText="Clave" SortExpression="id_validacion" UniqueName="id_validacion" HeaderStyle-Width="5%"  Resizable="true"  />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="Descripcion" FilterControlAltText="Filtro Descripción" HeaderText="Descripción" SortExpression="Descripcion" UniqueName="Descripcion" Resizable="true"  HeaderStyle-Width="15%" />
                               <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" HeaderText="Cumple"   FilterControlAltText="Filtro Crédito" HeaderStyle-Width="5%" >
                                <ItemTemplate>
                                     <asp:LinkButton ID="btnAprovar"  runat="server" CommandArgument='<%# Eval("id_validacion") %>' CssClass="btn btn-grey t14" OnClick="btnAprovar_Click1" ><i class="fa fa-check"></i>&nbsp;<span></span></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>   
                                <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" HeaderText="No Cumple"   FilterControlAltText="Filtro Crédito" HeaderStyle-Width="5%" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnNOAprovar" CssClass="btn btn-danger" runat="server"  CommandArgument='<%# Bind("id_validacion") %>'  OnClick="btnNOAprovar_Click" Text="No Cumple" ><i class="fa fa-remove"></i></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>   
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="Observacion" FilterControlAltText="Filtro Observación" HeaderText="Observación" SortExpression="Observacion" UniqueName="Observacion" Resizable="true" />
                                <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" HeaderText=""   FilterControlAltText="Filtro Editar" HeaderStyle-Width="6%" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditar" visible="false" CssClass="btn btn-warning" runat="server"  CommandArgument='<%# Bind("id_validacion") %>'   OnClick="btnEditar_Click1"  Text="Cambiar" ></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>   
                            </Columns>
                        </MasterTableView>
                        <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"  ConnectionString="<%$ ConnectionStrings:Taller %>">
                     <SelectParameters>
                        <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0"/>
                        <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0"/>
                         <asp:QueryStringParameter Name="credito" QueryStringField="c" DefaultValue="0"/>
                         <asp:ControlParameter Name="cliente" ControlID="lblint" DefaultValue="0" />
                     </SelectParameters>
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
                </asp:Panel>
         </ContentTemplate>
     </asp:UpdatePanel>


</asp:Content>

