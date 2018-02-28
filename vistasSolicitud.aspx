<%@ Page Title="" Language="C#" MasterPageFile="~/AdmonOrden.master" AutoEventWireup="true" CodeFile="vistasSolicitud.aspx.cs" Inherits="vistasSolicitud" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>
    
     <div class="page-header">
                <!-- /BREADCRUMBS -->
                <div class="clearfix">
                    <h3 class="content-title pull-left">Solicitud Crédito</h3>
                    <asp:Label ID="Label16" runat="server" CssClass="alert-danger"></asp:Label>
                </div>
            </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" OnLoad="lnkObtieneEncabezado_Click">
        <ContentTemplate>
              
                    <div class="row ">
                            <div class="col-lg-2 col-sm-1 text-left">
                                  <asp:Label ID="Label15" runat="server" Visible="false" ></asp:Label>

                                             <asp:Label ID="Label7" runat="server" Text="Sucursal:" ></asp:Label>
                                         </div>
                                         <div class="col-lg-4 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_suc_vis" runat="server"
                                                 CssClass="alingMiddle input-large" MaxLength="100" ReadOnly="true"
                                                 PlaceHolder="Sucursal"></asp:TextBox> 
                                         </div>
                        
                                       
                                           <div class="col-lg-2 col-sm-1 text-left">
                                             <asp:Label ID="Label10" runat="server" Text="Fecha Solicitud:" ></asp:Label>
                                         </div>
                                         <div class="col-lg-4 col-sm-3 text-left">
                                             <telerik:RadDatePicker ID="txtFecha_sol_vis" runat="server" ReadOnly="true">
                                                 <DateInput runat="server" DateFormat="yyyy/MM/dd"> 
                                                 </DateInput>
                                             </telerik:RadDatePicker>
                                         </div>
                                        
                                         
                                 </div>

                    <div class="row ">



                <div class="col-lg-2 col-sm-1 text-left">
                  

                    <asp:Label ID="Label2" runat="server" Text="Fecha Entrega del Crédito:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <telerik:RadDatePicker ID="txt_fecha_sol" runat="server" ReadOnly="true">
                        <DateInput runat="server" DateFormat="yyyy/MM/dd">
                        </DateInput>
                    </telerik:RadDatePicker>
                </div>

                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label1" runat="server" Text="Grupo Productivo:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_gpro_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                </div>

            </div>

                    <div class="row ">



                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label3" runat="server" Text="Número de Grupo:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_numGrup_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                </div>

                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label4" runat="server" Text="Monto de Crédito:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_moncred_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                </div>

            </div>

                    <div class="row ">



                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label5" runat="server" Text="Plazo:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_plazo_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                </div>

                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label6" runat="server" Text="Tasa (%):"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_tasa_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                </div>

            </div>

                    <div class="row ">



                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label8" runat="server" Text="Garantía Líquida:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_garaliq_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                </div>

                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label9" runat="server" Text="Monto máximo del Crédito:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_mont_max" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                </div>

            </div>

                    <div class="row ">



                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label11" runat="server" Text="Monto crédito Autorizado:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_montaut_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                </div>

                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label12" runat="server" Text="Plazo:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_plazo2" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                </div>

            </div>

                    <div class="row ">



                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label13" runat="server" Text="Tasa(%):"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_tasa2_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                </div>

                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label14" runat="server" Text="Forma de Pago:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_formapag_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                </div>

            </div>

            <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" EnableAJAX="true">
                            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" AllowFilteringByColumn="true" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue" 
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource2" AllowSorting="true" GroupingEnabled="false" PageSize="50" >                       
                                    <MasterTableView DataSourceID="SqlDataSource2" AutoGenerateColumns="false" DataKeyNames="id_solicitud_credito">
                                        <Columns>                                                              
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="id_cliente" FilterControlAltText="Filtro Id_Cliente" HeaderText="ID Cliente" SortExpression="id_cliente" UniqueName="id_cliente"  Resizable="true"/>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_cliente" FilterControlAltText="Filtro Nombre" HeaderText="Nombre" SortExpression="nombre_cliente" UniqueName="nombre_cliente" Resizable="true"/>                                    
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="ciclo" FilterControlAltText="Filtro Ciclo" HeaderText="Ciclo" SortExpression="ciclo" UniqueName="ciclo" Resizable="true"/>                                   
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="cargo" FilterControlAltText="Filtro Cargo" HeaderText="Cargo" SortExpression="cargo" UniqueName="cargo" Resizable="true"/> 
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="estatus" FilterControlAltText="Filtro Estatus" HeaderText="Estatus" SortExpression="estatus" UniqueName="estatus" Resizable="true"/> 
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="causas_desercion" FilterControlAltText="Filtro Causas" HeaderText="Causas Deserción" SortExpression="causas_desercion" UniqueName="causas_desercion" Resizable="true"/>                                                                                                           
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="giro_negocio" FilterControlAltText="Filtro Giro" HeaderText="Giro Negocio" SortExpression="giro_negocio" UniqueName="giro_negocio" Resizable="true"/>                                    
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="ingreso" FilterControlAltText="Filtro Ingreso" HeaderText="Ingreso" SortExpression="ingreso" UniqueName="ingreso" Resizable="true"/> 
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="detino_credito" FilterControlAltText="Filtro Destino" HeaderText="Destino Crédito" SortExpression="detino_credito" UniqueName="detino_credito" Resizable="true"/>  
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="credito_anterior" FilterControlAltText="Filtro Credito" HeaderText="Crédito Anterior" SortExpression="credito_anterior" UniqueName="credito_anterior" Resizable="true"/> 
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="credito_solicitado" FilterControlAltText="Filtro Credito" HeaderText="Crédito Solicitado" SortExpression="credito_solicitado" UniqueName="credito_solicitado" Resizable="true"/>    
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="garantia_liquida" FilterControlAltText="Filtro GL" HeaderText="Garantía Líquida" SortExpression="garantia_liquida" UniqueName="garantia_liquida" Resizable="true"/>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="credito_autorizado" FilterControlAltText="Filtro Credito" HeaderText="Crédito Autorizado" SortExpression="credito_autorizado" UniqueName="credito_autorizado" Resizable="true"/>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="telefono" FilterControlAltText="Filtro Telefono" HeaderText="Teléfono" SortExpression="telefono" UniqueName="telefono" Resizable="true"/>
                                                                                                                                                                                                                        
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
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="select * from AN_Solicitud_Credito_Detalle where id_empresa=@empresa and id_sucursal=@sucursal and id_solicitud_credito=@grupo" ConnectionString="<%$ ConnectionStrings:Taller %>">
                            <SelectParameters>
                                        <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0" />
                                        <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0" />
                                        <asp:QueryStringParameter Name="credito" QueryStringField="c" DefaultValue="0" />
                                        <asp:ControlParameter Name="grupo" ControlID="Label15" DefaultValue="0" />
                                    </SelectParameters>
                        </asp:SqlDataSource>
            <div class="row marTop text-center">
                        <asp:LinkButton ID="lnkImprimir" runat="server" Visible="true" ToolTip="Imprimir Solicitud" OnClick="lnkImprimir_Click" CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Solicitud</span></asp:LinkButton>                
                    </div>
        </ContentTemplate>

       
    </asp:UpdatePanel>

</asp:Content>

