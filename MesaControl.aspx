<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="MesaControl.aspx.cs" Inherits="MesaControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>

          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
                  
                  <div class="page-header">
                      <!-- /BREADCRUMBS -->
                      <div class="clearfix">
                          <h3 class="content-title pull-left">Mesa de Control</h3>
                      </div>
                  </div>
                  

                  <div class="row pad1m">
                      
                 <div class="col-lg-9 col-sm-9 text-center">
                                      
                        <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" EnableAJAX="true">
                            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" AllowFilteringByColumn="true" runat="server" OnItemDataBound="RadGrid1_ItemDataBound" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue" OnSelectedIndexChanged="RadGrid1_SelectedCellChanged"
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource2" AllowSorting="true" GroupingEnabled="false" PageSize="50" >                       
                                    <MasterTableView DataSourceID="SqlDataSource2" AutoGenerateColumns="false" DataKeyNames="id_solicitud_credito">
                                        <Columns>  
                                           <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" HeaderText="Crédito" SortExpression="id_solicitud_credito" UniqueName="id_solicitud_credito" HeaderStyle-Width="20%" FilterControlAltText="Filtro Crédito" DataField="id_solicitud_credito">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkAutorizaGrup"  runat="server" CommandArgument='<%# Eval("id_solicitud_credito") %>' CssClass="btn btn-grey t14" OnClientClick="return confirm('¿Está seguro de aceptar la solicitud?')" OnClick="lnkAutorizaGrup_Click"><i class="fa fa-check"></i>&nbsp;<span></span></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkNegaGrup" runat="server" CommandArgument='<%# Eval("id_solicitud_credito ") %>' CssClass="btn btn-danger t14" OnClick="lnkNegaGrup_Click"><i class="fa fa-remove"></i>&nbsp;<span></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="amcs" FilterControlAltText="Filtro Autorizado" HeaderText="Autorización" SortExpression="amcs" HeaderStyle-Width="20%" UniqueName="amcs" AutoPostBackOnFilter="true" Resizable="true"/>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="grupo_productivo" FilterControlAltText="Filtro Grupo" HeaderText="Grupo" SortExpression="grupo_productivo" HeaderStyle-Width="15%" UniqueName="grupo_productivo"  Resizable="true"/>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="fecha_solicitud" FilterControlAltText="Filtro Fecha Solicitud"  HeaderText="Fecha Solicitud" SortExpression="fecha_solicitud" HeaderStyle-Width="15%" UniqueName="fecha_solicitud" Resizable="true"/>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="monto_credito" FilterControlAltText="Filtro Monto" HeaderText="Monto" SortExpression="monto_credito" UniqueName="monto_credito" HeaderStyle-Width="15%" Resizable="true"/>                                    
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="monto_autorizado" FilterControlAltText="Filtro Autorizado" HeaderText="Monto Autorizado" SortExpression="monto_autorizado" HeaderStyle-Width="15%" UniqueName="monto_autorizado" Resizable="true"/>                                    
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="No existen solicitudes registradas" CssClass="text-danger"></asp:Label>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                    <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                                    </ClientSettings>  
                                                                             
                                </telerik:RadGrid>
                        </telerik:RadAjaxPanel>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="select id_solicitud_credito,amcs,grupo_productivo,convert(char(10),fecha_Solicitud,120) as fecha_Solicitud,monto_credito,monto_autorizado from AN_Solicitud_Credito_Encabezado " ConnectionString="<%$ ConnectionStrings:Taller %>">
                        </asp:SqlDataSource>
                   
                     </div>
                       <div class="row marTop text-center">
                    <asp:Label ID="lblErrorDigital" runat="server" CssClass="alert-danger"></asp:Label>
                           <asp:Label ID="lbltienamc" Visible="false" Text="0" runat="server" CssClass="alert-danger"></asp:Label>
                           <asp:Label ID="lblTotalIntegrantes" Visible="false" Text="0" runat="server" CssClass="alert-danger"></asp:Label>
                </div>
                  <div class="col-lg-3 col-sm-3 text-center">
                      <br /><br />
                       <asp:LinkButton ID="lnkImprimirSolicitud" runat="server" Visible="false" ToolTip="Imprimir Solicitud" OnClick="lnkImprimirSolicitud_Click" CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Solicitud</span></asp:LinkButton>                
                      
                       </div>
                      </div>

                      <br /><br />

                        <div class="row pad1m">
                            <div class="col-lg-6 col-sm-6 text-center" >
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" EnableAJAX="true">
                            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid2" AllowFilteringByColumn="true" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue"  OnSelectedIndexChanged="RadGrid2_SelectedIndexChanged"
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource1" AllowSorting="true" GroupingEnabled="false" PageSize="50" >                       
                                    <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="false" DataKeyNames="id_solicitud_credito,id_cliente">
                                        <Columns>
                                            <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" HeaderText="Crédito" SortExpression="id_solicitud_credito" UniqueName="id_solicitud_credito" HeaderStyle-Width="20%" FilterControlAltText="Filtro Crédito" DataField="id_solicitud_credito">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkAutorizaDIG"  runat="server" CommandArgument='<%# Eval ("id_cliente")+ ";" + Eval("id_solicitud_credito") %>' CssClass="btn btn-grey t14" OnClientClick="return confirm('¿Está seguro de aceptar la solicitud?')" OnClick="CheckBox2_CheckedChanged"><i class="fa fa-check"></i>&nbsp;<span></span></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkNegaDIG" runat="server" CommandArgument='<%# Eval ("id_cliente")+ ";" + Eval("id_solicitud_credito ") %>' CssClass="btn btn-danger t14" OnClick="CheckBox2a_CheckedChanged"><i class="fa fa-remove"></i>&nbsp;<span></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="amc" FilterControlAltText="Filtro Autorizado" HeaderText="Autorización" SortExpression="amc" UniqueName="amc" HeaderStyle-Width="20%" Resizable="true"/>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_cliente" FilterControlAltText="Filtro Cliente" HeaderText="Cliente" SortExpression="nombre_cliente" HeaderStyle-Width="20%" UniqueName="nombre_cliente"  Resizable="true"/>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="ciclo" FilterControlAltText="Filtro ciclo"  HeaderText="Ciclo" SortExpression="ciclo" UniqueName="ciclo" HeaderStyle-Width="10%" Resizable="true"/>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="cargo" FilterControlAltText="Filtro Cargo" HeaderText="Cargo" SortExpression="cargo" UniqueName="cargo" HeaderStyle-Width="20%" Resizable="true"/>                                    
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="credito_autorizado" FilterControlAltText="Filtro Crédito Autorizado" HeaderText="Crédito Autorizado" HeaderStyle-Width="10%" SortExpression="credito_autorizado" UniqueName="credito_autorizado" Resizable="true"/>                                    
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="No existen solicitudes registradas" CssClass="text-danger"></asp:Label>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                   <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>   
                                                                             
                                </telerik:RadGrid>
                        </telerik:RadAjaxPanel>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="select * from AN_Solicitud_Credito_Detalle  where id_empresa=@empresa and id_sucursal=@sucursal and id_solicitud_credito=@idCredito"  ConnectionString="<%$ ConnectionStrings:Taller %>">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                                <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />
                                <asp:ControlParameter ControlID="RadGrid1" Name="idCredito" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                       
                      
                   </div>
                             
                             <div class="col-lg-6 col-sm-6 text-center"  >
                                 
                                 <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server" EnableAJAX="true">
                    <telerik:RadGrid RenderMode="Lightweight" ID="radFichas" AllowFilteringByColumn="true" Visible="false" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue" OnSelectedIndexChanged="radFichas_SelectedIndexChanged" 
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true"  DataSourceID="SqlDataSource3" AllowSorting="true" GroupingEnabled="false" PageSize="50" >
                        <MasterTableView DataSourceID="SqlDataSource3" AutoGenerateColumns="false" DataKeyNames="id_ficha,id_cliente">
                            <Columns>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="rfc_cli" FilterControlAltText="Filtro RFC" HeaderText="RFC" SortExpression="rfc_cli" UniqueName="rfc_cli" Resizable="true" />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="genero_cli" FilterControlAltText="Filtro Genero" HeaderText="Genero" SortExpression="genero_cli" UniqueName="genero_cli" Resizable="true" />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nivel_escolaridad" FilterControlAltText="Filtro Nivel Escolaridad" HeaderText="Nivel Escolaridad" SortExpression="nivel_escolaridad" UniqueName="nivel_escolaridad" Resizable="true" />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>">
                     <SelectParameters>
                        <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0"/>
                        <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0"/>
                     </SelectParameters>
                </asp:SqlDataSource>
                                 <br /><br />
                                  <asp:LinkButton ID="lnkImprimirFicha" runat="server" Visible="false" ToolTip="Imprimir Solicitud" OnClick="lnkImprimirFicha_Click" CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Cédula</span></asp:LinkButton> 
                       

                             </div>
                             <div class="col-lg-6 col-sm-6 text-center">

                        <telerik:RadAjaxPanel ID="RadAjaxPanel4" runat="server" EnableAJAX="true">
                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid3" AllowFilteringByColumn="true" Visible="false" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue"  OnSelectedIndexChanged="RadGrid3_SelectedIndexChanged1"
                        EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource4" AllowSorting="true" GroupingEnabled="false" PageSize="50">
                        <MasterTableView DataSourceID="SqlDataSource4" AutoGenerateColumns="False" DataKeyNames="id_ficha,Id_Ficha_Adjunto">
                            <Columns>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="descripcion_adjunto" FilterControlAltText="Filtro Adjunto" HeaderStyle-Width="200px" HeaderText="Adjunto" SortExpression="descripcion_adjunto" UniqueName="desripcion_adjunto" Resizable="true" Visible="true" />
                                
                            </Columns>
                            <NoRecordsTemplate>
                                <asp:Label ID="Label1" runat="server" Text="No existen Archivos asignados a la Ficha" CssClass="text-danger"></asp:Label>
                            </NoRecordsTemplate>
                        </MasterTableView>
                        <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle PageSizeControlType="RadDropDownList" Mode="NextPrevAndNumeric"></PagerStyle>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" >
                    <SelectParameters>
                    <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0"/>
                        <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0"/>
                        <asp:ControlParameter ControlID="RadGrid1"  Name="id_ficha" DefaultValue="0" />
                    </SelectParameters>
                </asp:SqlDataSource>
                                 <br /><br />
                      <asp:LinkButton ID="lnkArchivoDescarga" visible="false" runat="server" OnClick="lnkArchivoDescarga_Click" CssClass="btn btn-primary"><i class="fa fa-download"></i><span>&nbsp;Descargar&nbsp;</span></asp:LinkButton>
                
                  </div>
                     
                            </div>

                  

                    
              </ContentTemplate>
                <Triggers>
            <asp:PostBackTrigger ControlID="lnkArchivoDescarga" />   
        </Triggers>

          </asp:UpdatePanel>
    
</asp:Content>

