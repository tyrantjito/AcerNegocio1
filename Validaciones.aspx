<%@ Page Title="" Language="C#" MasterPageFile="~/AdmonOrden.master" AutoEventWireup="true" CodeFile="Validaciones.aspx.cs" Inherits="Validaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>

          <asp:UpdatePanel ID="UpdatePanel1" runat="server" > 
              <ContentTemplate>
                  <div class="page-header">
                      <!-- /BREADCRUMBS -->
                      <div class="page-header">
                      <div class="clearfix">
                          <asp:Label ID="lblDetalelObra" runat="server" ></asp:Label></h3>
                         
                      </div>
                  </div>
                       <div class="page-header">
                      <div class="clearfix">
                          <h3 class="content-title pull-left">Validación</h3>
                         
                      </div>
                  </div>
               
                  <div class="row pad1m">
                         <div class="col-lg-6 col-sm-6 text-center">
                    <div class="clearfix">
                         <asp:Label ID="lblErrorAfuera" Text="Consulta Buro" CssClass="content-title" runat="server" ></asp:Label>
                      </div>
                 <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" EnableAJAX="true">
                            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid2" AllowFilteringByColumn="true" runat="server"  EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue" OnSelectedIndexChanged="RadGrid2_SelectedIndexChanged"
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource2" AllowSorting="true" GroupingEnabled="false" PageSize="50" >                        
                                <MasterTableView DataSourceID="SqlDataSource2" AutoGenerateColumns="false" DataKeyNames="id_consulta,id_cliente">
                                    <Columns>
                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_completo" FilterControlAltText="Filtro Cliente" HeaderText="Cliente" SortExpression="nombre_completo" UniqueName="nombre_completo"  Resizable="true"/>  
                                    </Columns>
                                    <NoRecordsTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="No existen consultas registradas" CssClass="text-danger"></asp:Label>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                                <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>                                               
                            </telerik:RadGrid>
                    </telerik:RadAjaxPanel>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="select b.id_consulta,b.id_cliente,c.nombre_completo,b.rfc_curp , case b.tipo_persona when 'FIS' then 'Fisica' when 'FAE' then 'Fisica Con Actividad Empresarial' when 'MOR' then 'Moral' else  '' end as tipo_persona, case b.fecha_consulta when '1900-01-01' then 'Sin Consultar' else 
                    convert(char(10),fecha_consulta,120) end as fecha_consulta,case b.folio_consulta when '' then 'Sin Consultar' else folio_consulta end as folio_consulta,
					case b.estatus when 'APR' then 'APROBADO' when 'DEC' then 'DECLINADO' else 'PENDIENTE' end as estatus from AN_Solicitud_Consulta_Buro b inner join AN_Clientes c on b.id_cliente = c.id_cliente where b.id_empresa=@empresa and b.id_sucursal=@sucursal order by id_consulta desc" ConnectionString="<%$ ConnectionStrings:Taller %>">
                    <SelectParameters>
                         <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />
                         <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                    </SelectParameters>
                    </asp:SqlDataSource>

                              <div class="row marTop text-center">
                    <asp:LinkButton ID="lnkImprimir" runat="server" Visible="false" OnClick="lnkImprimir_Click " ToolTip="Imprimir Solicitud" CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Consulta</span></asp:LinkButton>                
                </div>
                </div>


                       <div class="col-lg-6 col-sm-6 text-center">
                    <div class="clearfix">
                          <asp:Label ID="Label2" Text="Acta Integración" CssClass="content-title" runat="server" ></asp:Label>
                      </div>
                 <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server" EnableAJAX="true">
                            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid3" AllowFilteringByColumn="true" runat="server"  EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue" 
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource3" AllowSorting="true" GroupingEnabled="false" PageSize="50" >                        
                                <MasterTableView DataSourceID="SqlDataSource3" AutoGenerateColumns="false" DataKeyNames="id_acta">
                                    <Columns>
                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="grupo_productivo" FilterControlAltText="Filtro Grupo" HeaderText="Grupo" SortExpression="grupo_productivo" UniqueName="grupo_productivo"  Resizable="true"/>  
                                    </Columns>
                                    <NoRecordsTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="No existen consultas registradas" CssClass="text-danger"></asp:Label>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                                <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>                                               
                            </telerik:RadGrid>
                    </telerik:RadAjaxPanel>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="select convert(char(10),i.fecha_integracion_acta,120) as 
                                        fecha_integracion_acta,g.grupo_productivo,i.colonia_grupo_productivo, i.id_empresa, i.id_sucursal, id_acta from an_acta_integracion i inner join AN_Grupos g on i.id_grupo = g.id_grupo" ConnectionString="<%$ ConnectionStrings:Taller %>">
                    <SelectParameters>
                         <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />
                         <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                    </SelectParameters>
                    </asp:SqlDataSource>
                </div>
                      </div>

                      <div class="row pad1m">
                            <div class="col-lg-4 col-sm-4 text-center">
                     <div class="clearfix">
                          <h2 class="content-title pull-left">Ficha de Datos</h2>
                      </div>
                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" EnableAJAX="true">
                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGridFD" AllowFilteringByColumn="true"  runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue" Visible="false"
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource1" AllowSorting="true" GroupingEnabled="false" PageSize="50" >
                        <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="false" DataKeyNames="id_ficha,id_cliente">
                            <Columns>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_completo" FilterControlAltText="Filtro Nombre" HeaderText="Nombre" SortExpression="nombre_completo" UniqueName="nombre_completo" Resizable="true"  />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>">
                     <SelectParameters>
                        <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0"/>
                        <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0"/>
                     </SelectParameters>
                </asp:SqlDataSource>

                                 <div class="row marTop text-center">
            <asp:LinkButton ID="lnkFicha" runat="server" Visible="false" ToolTip="Imprimir Solicitud" OnClick="lnkImprimirFicha_Click " CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Ficha</span></asp:LinkButton>                
                </div> 
            </div>
                
                       <div class="col-lg-4 col-sm-4 text-center">
                    <div class="clearfix">
                          <h2 class="content-title pull-left">Visita Ocular</h2>
                      </div>
                 <telerik:RadAjaxPanel ID="RadAjaxPanel4" runat="server" EnableAJAX="true">
                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGridVO" AllowFilteringByColumn="true"  runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue" Visible="false"
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource4" AllowSorting="true" GroupingEnabled="false" PageSize="50" >
                        <MasterTableView DataSourceID="SqlDataSource4" AutoGenerateColumns="false" DataKeyNames="id_cliente,id_visita">
                            <Columns>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_completo" FilterControlAltText="Filtro Cliente" HeaderText="Cliente" SortExpression="nombre_completo" UniqueName="nombre_completo" Resizable="true"  />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="select a.id_visita,b.nombre_completo,a.id_cliente,convert(char(10),a.fecha_visita,120) as 
                                        fecha_visita,a.gerente_sucursal_visita from AN_visita_ocular a inner join an_clientes b on b.id_cliente=a.id_cliente where a.id_sucursal=@sucursal and a.id_empresa=@empresa order by a.id_visita desc" ConnectionString="<%$ ConnectionStrings:Taller %>">
                     <SelectParameters>
                                        <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0"/>
                                        <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0"/>
                                    </SelectParameters>
                </asp:SqlDataSource>

                           <div class="row marTop text-center">
                    <asp:LinkButton ID="lnkVisita" runat ="server" Visible ="false" OnClick="lnkVisita_Click" CssClass="btn btn-info"> <i class="fa fa-print"><span>&nbsp;Imprimir Visita</span></i></asp:LinkButton>
                </div> 
                </div>

                      <div class="col-lg-4 col-sm-4 text-center">
                    <div class="clearfix">
                          <h2 class="content-title pull-left">Capacidad de Pago</h2>
                      </div>
                  <telerik:RadAjaxPanel ID="RadAjaxPanel5" runat="server" EnableAJAX="true">
                      <telerik:RadGrid RenderMode="Lightweight" ID="RadGridCP" AllowFilteringByColumn="true" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue" Visible="false" 
                          EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource5" AllowSorting="true" GroupingEnabled="false" PageSize="50">
                          <MasterTableView DataSourceID="SqlDataSource5" AutoGenerateColumns="false" DataKeyNames="nombre_cliente_apago">
                              <Columns>
                                  <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_cliente_apago" FilterControlAltText="Filtro Nombre" HeaderText="Nombre" SortExpression="nombre_cliente_apago" UniqueName="nombre_cliente_apago" Resizable="true" />
                              </Columns>
                          </MasterTableView>
                          <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                              <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                              <Selecting AllowRowSelect="true" />
                          </ClientSettings>
                      </telerik:RadGrid>
                  </telerik:RadAjaxPanel>
                  <asp:SqlDataSource ID="SqlDataSource5" runat="server"  ConnectionString="<%$ ConnectionStrings:Taller %>">
                      <SelectParameters>
                          <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0" />
                          <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0" />
                      </SelectParameters>
                  </asp:SqlDataSource>

                          <div class="row marTop text-center">
                    <asp:LinkButton ID="lnkCapacidadPago" runat="server" Visible="false" ToolTip="Imprimir Solicitud" OnClick="lnkCapacidadPago_Click" CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Análisis</span></asp:LinkButton>                
                </div>
                </div>
                           <div class="col-lg-4 col-sm-4 text-center">

                           </div>

                      </div>
                      </div>
                  <br />
                   <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br />


                 

              </ContentTemplate>
          </asp:UpdatePanel>

</asp:Content>

