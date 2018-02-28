<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="Parametros.aspx.cs" Inherits="Parametros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        

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

    <telerik:RadWindow RenderMode="Lightweight" ID="modalModifica" Title="Edita Moneda" EnableShadow="true" Skin="Metro"
        Behaviors="Move,Resize,Pin,Close" VisibleOnPageLoad="false" ShowContentDuringLoad="false" DestroyOnClose="true" 
        Animation="Fade" runat="server" Modal="true" Width="900px" Height="550px" Style="z-index: 1000;">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanelEmiMod" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server" CssClass="ancho100 text-center">
                        <div class="col-lg-12 col-sm-12 text-center">
                            <asp:Label ID="Label1" runat="server" CssClass="errores"></asp:Label>
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="agrega" CssClass="errores" DisplayMode="List" />
                        </div>                 
                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label2" runat="server" Text="Nombre Corto:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtNameCortEd" runat="server" CssClass="input-xxlarge" MaxLength="128" />   </div>

                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label3" runat="server" Text="Nombre Empresa" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtEmpreEd" runat="server" MaxLength="300" CssClass="input-xxlarge"></asp:TextBox>                                                      
                        </div>

                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label15" runat="server" Text="Dirección:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtDirEd" runat="server" CssClass="input-xxlarge" MaxLength="1000" Height="100px" Width="400px" TextMode="MultiLine" />         </div>

                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label16" runat="server" Text="Correo:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtCorreoEd" runat="server" MaxLength="100" CssClass="input-xxlarge"></asp:TextBox>                                                      
                        </div>

                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label17" runat="server" Text="Teléfono Pago:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtTelEd" runat="server" CssClass="input-xxlarge" MaxLength="128" />         </div>

                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label18" runat="server" Text="RFC:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtRfcEd" runat="server" MaxLength="100" CssClass="input-xxlarge"></asp:TextBox>                                                      
                        </div>

                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label19" runat="server" Text="Página Web:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtPagWebEd" runat="server" CssClass="input-xxlarge" MaxLength="128" />   </div>

                        <div class="col-lg-3 col-sm-3 text-left"><asp:Label ID="Label20" runat="server" Text="Representante:" CssClass="textoBold"></asp:Label></div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtRepreEd" runat="server" MaxLength="100" CssClass="input-xxlarge"></asp:TextBox>                                                      
                        </div>

                        

                        
                         <div class="col-lg-6 col-sm-6 text-center pad1m">
                            <asp:LinkButton ID="BtnActualizar" runat="server" CssClass="btn btn-success t14" OnClick="BtnActualizar_Click" ValidationGroup="agrega" ><i class="fa fa-check-circle"></i>&nbsp; Agregar</asp:LinkButton>
                        </div>
                        <div class="col-lg-6 col-sm-6 text-center pad1m">
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="cierraModEmi()" CssClass="btn btn-danger t14" ><i class="fa fa-remove"></i>&nbsp; Cancelar</asp:LinkButton>
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
                    <h3 class="content-title pull-left">Parámetros</h3>
                    <asp:Label ID="Label8" runat="server" CssClass="alert-danger"></asp:Label>
                </div>
            </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>            
            <div class="col-lg-12 col-sm-12 text-right">
                 
                <div class="col-lg-3 col-sm-3 text-center">
                <asp:LinkButton ID="btnEditar" runat="server" Visible="false" ToolTip="Editar" CssClass="btn btn-warning t14" OnClick="btnEditar_Click"><i class="fa fa-edit"></i>&nbsp;<span>Editar</span></asp:LinkButton>
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
                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" AllowFilteringByColumn="true" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Metro" 
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource1" AllowSorting="true" GroupingEnabled="false" PageSize="50" >
                        <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="false" DataKeyNames="id_empresa">
                            <Columns>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombreCortoEmp" FilterControlAltText="Filtro nombreCortoEmp" HeaderText="Nombre Corto" SortExpression="nombreCortoEmp" UniqueName="nombreCortoEmp" Resizable="true"  />

                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="empresa" FilterControlAltText="Filtro empresa" HeaderText="Empresa" SortExpression="empresa" UniqueName="empresa" Resizable="true" />

                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="direccion" FilterControlAltText="Filtro direccion" HeaderText="Dirección" SortExpression="direccion" UniqueName="direccion" Resizable="true" />

                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="correo" FilterControlAltText="Filtro correo" HeaderText="Correo" SortExpression="correo" UniqueName="correo" Resizable="true" />

                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="telefono" FilterControlAltText="Filtro telefono" HeaderText="Teléfono" SortExpression="telefono" UniqueName="telefono" Resizable="true" />

                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="rfc" FilterControlAltText="Filtro rfc" HeaderText="RFC" SortExpression="rfc" UniqueName="rfc" Resizable="true" />

                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="pagweb" FilterControlAltText="Filtro pagweb" HeaderText="Página Web" SortExpression="pagweb" UniqueName="pagweb" Resizable="true" />

                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="representante" FilterControlAltText="Filtro representante" HeaderText="Representante" SortExpression="representante" UniqueName="representante" Resizable="true" />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="select*from an_parametros where id_empresa=@empresa and id_sucursal=@sucursal" ConnectionString="<%$ ConnectionStrings:Taller %>">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0"/>
                        <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0"/>
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

</asp:Content>

