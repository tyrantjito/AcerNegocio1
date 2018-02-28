<%@ Page Title="" Language="C#" MasterPageFile="~/AdmonOrden.master" AutoEventWireup="true" CodeFile="EstadoCuentaInd.aspx.cs" Inherits="EstadoCuentaInd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>

          <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
              <ContentTemplate>
                 <div class="page-header">
                <!-- /BREADCRUMBS -->
                <div class="clearfix">
                    <h3 class="content-title pull-left">Estado Cuenta Individual</h3>
                    <asp:Label ID="Label8" runat="server" CssClass="alert-danger"></asp:Label>
                </div>
            </div>
                 <asp:Label ID="Label15" runat="server" Visible="false" ></asp:Label>
                   <div class="row marTop text-center">
                      <asp:LinkButton ID="lnkImprimir" runat="server" Visible="false" ToolTip="Imprimir Pagare" OnClick="lnkImprimir_Click" CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Estado de Cuenta</span></asp:LinkButton>
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
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="Gl" FilterControlAltText="Filtro GL" HeaderText="GL" SortExpression="Gl" UniqueName="Gl" Resizable="true"/>                                   
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="credito_autorizado" FilterControlAltText="Filtro credito_autorizado" HeaderText="credito_autorizado" SortExpression="credito_autorizado" UniqueName="credito_autorizado" Resizable="true"/> 
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="pagosemanal" FilterControlAltText="Filtro pagosemanal" HeaderText="pagosemanal" SortExpression="pagosemanal" UniqueName="pagosemanal" Resizable="true"/> 
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="saldo_actual" FilterControlAltText="Filtro saldo_actual" HeaderText="saldo_actual" SortExpression="saldo_actual" UniqueName="saldo_actual" Resizable="true"/> 
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
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="select  distinct d.id_cliente,d.nombre_cliente,d.credito_autorizado*.10 as Gl,d.credito_autorizado,d.credito_autorizado / 16 as pagosemanal,o.saldo_actual,o.no_pago,o.monto_Pago,o.monto_Ahorro  from AN_Solicitud_Credito_Detalle d left join AN_Operacion_Credito o on d.id_cliente = o.id_cliente where no_pago=1 and id_grupo=@grupo" ConnectionString="<%$ ConnectionStrings:Taller %>">
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

