<%@ Page Title="" Language="C#" MasterPageFile="~/AdmonOrden.master" AutoEventWireup="true" CodeFile="ODP.aspx.cs" Inherits="ODP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
              <ContentTemplate>
                  <div class="page-header">
                      <!-- /BREADCRUMBS -->
                      <div class="clearfix text-center">
                          <h3 class="content-title pull-center">ODP</h3>
                      </div>
                  </div>
                 <asp:Label ID="Label15" runat="server" Visible="false" ></asp:Label>
                  <div class="row marTop text-center">
                      <asp:LinkButton ID="lnkImprimir" runat="server" Visible="false" ToolTip="Imprimir Pagare" OnClick="lnkImprimir_Click" CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir ODP</span></asp:LinkButton>
                      <asp:LinkButton ID="lnkimprimirTodo" runat="server" Visible="false" ToolTip="Imprimir Pagare" OnClick="lnkimprimirTodo_Click" CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Todos los ODP</span></asp:LinkButton>
                  </div>
                  
                     
                  <br />
                   <br /> <br />

                    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" EnableAJAX="true">
                            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged" AllowFilteringByColumn="true" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue" 
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource2" AllowSorting="true" GroupingEnabled="false" PageSize="50" >                       
                                    <MasterTableView DataSourceID="SqlDataSource2" AutoGenerateColumns="false" DataKeyNames="id_cliente">
                                        <Columns>                                                              
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="id_cliente" FilterControlAltText="Filtro Id_Cliente" HeaderText="ID Cliente" SortExpression="id_cliente" UniqueName="id_cliente"  Resizable="true"/>


                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_cliente" FilterControlAltText="Filtro Nombre" HeaderText="Nombre" SortExpression="nombre_cliente" UniqueName="nombre_cliente" Resizable="true"/>                                    
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="ciclo" FilterControlAltText="Filtro Ciclo" HeaderText="Ciclo" SortExpression="ciclo" UniqueName="ciclo" Resizable="true" HeaderStyle-Width="5%"/>                                   
                                            

                                             <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" Resizable="true" DataField="credito_autorizado" HeaderText="CRÉDITO AUTORIZADO" SortExpression="credito_autorizado" UniqueName="credito_autorizado"  ReadOnly="true" HeaderStyle-Width="10%" >
                                                    <ItemTemplate><%# Eval("credito_autorizado","{0:C2}") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox runat="server" RenderMode="Lightweight" Skin="Metro" ID="credito_autorizado" Width="100px" ShowSpinButtons="true" MaxValue="9999999999.99" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-DecimalSeparator="."></telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>  

                                             
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre" FilterControlAltText="Filtro nombre" HeaderText="Nombre Banco" SortExpression="nombre" UniqueName="nombre" Resizable="true"/> 

                                                                                                                                             
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="convenio" FilterControlAltText="Filtro convenio" HeaderText="Convenio" SortExpression="convenio" UniqueName="convenio" Resizable="true"/>                                    
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="referencia" FilterControlAltText="Filtro referencia" HeaderText="Referencia" SortExpression="referencia" UniqueName="referencia" Resizable="true"/> 
                                                                        
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
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="select d.id_cliente, d.nombre_cliente, d.ciclo, d.credito_autorizado, b.nombre, b.convenio, b.referencia from AN_Solicitud_Credito_Detalle d inner join AN_Solicitud_Credito_Encabezado e on d.id_empresa=e.id_empresa and d.id_solicitud_credito=e.id_solicitud_credito and d.id_sucursal=e.id_sucursal inner join AN_catBancos b on e.id_banco=b.clave where e.id_empresa=@empresa and d.id_solicitud_credito=@grupo and e.id_sucursal=@sucursal" ConnectionString="<%$ ConnectionStrings:Taller %>">
                            <SelectParameters>
                                        <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0" />
                                        <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0" />
                                        <asp:QueryStringParameter Name="credito" QueryStringField="c" DefaultValue="0" />
                                 <asp:ControlParameter Name="grupo" ControlID="Label15" DefaultValue="0" />
                                    </SelectParameters>
                        </asp:SqlDataSource>
                 

              </ContentTemplate>
          </asp:UpdatePanel>

</asp:Content>

