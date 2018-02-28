<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="AutComite.aspx.cs" Inherits="AutComite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>
    
    <div class="page-header">
                            <!-- /BREADCRUMBS -->
                            <div class="clearfix">
                                <h3 class="content-title pull-left"> 
                                    Autorización Comite </h3>
                            </div>
                    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
              
                    
            <div class="row pad1m">
                 <asp:Label ID="lblgrup" runat="server" Visible="true" ></asp:Label>
                <div class="col-lg-6 col-sm-6 text-center" >
             <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" EnableAJAX="true">
                            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" AllowFilteringByColumn="true" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged"
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource2" AllowSorting="true" GroupingEnabled="false" PageSize="50" >                       
                                    <MasterTableView DataSourceID="SqlDataSource2" AutoGenerateColumns="false" DataKeyNames="id_solicitud_credito">
                                        <Columns>                                                              
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="grupo_productivo" FilterControlAltText="Filtro Grupo" HeaderText="Grupo" SortExpression="grupo_productivo" UniqueName="grupo_productivo"  Resizable="true"/>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="monto_credito" FilterControlAltText="Filtro Monto" HeaderText="Monto" SortExpression="monto_credito" DataFormatString="{0:N2}" UniqueName="monto_credito" Resizable="true"/>                                    
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="monto_autorizado" FilterControlAltText="Filtro Autorizado" HeaderText="Monto Autorizado" DataFormatString="{0:N2}" SortExpression="monto_autorizado" UniqueName="monto_autorizado" Resizable="true"/>                                    
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
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="select id_solicitud_credito,grupo_productivo,convert(char(10),fecha_Solicitud,120) as fecha_Solicitud,monto_credito,monto_autorizado from AN_Solicitud_Credito_Encabezado" ConnectionString="<%$ ConnectionStrings:Taller %>">
                        </asp:SqlDataSource>
            </div>
             <div class="col-lg-6 col-sm-6 text-center">

                 <div class="row ">
                            <div class="col-lg-2 col-sm-2 text-left">
                                             <asp:Label ID="Label7" runat="server" Text="Sucursal:" ></asp:Label>
                                         </div>
                                         <div class="col-lg-4 col-sm-4 text-left">
                                            <asp:TextBox ID="txt_suc_vis" runat="server"
                                                 CssClass="alingMiddle input-small" MaxLength="100" ReadOnly="true"
                                                 PlaceHolder="Sucursal"></asp:TextBox> 
                                         </div>
                        
                                           <div class="col-lg-2 col-sm-2 text-left">
                                             <asp:Label ID="Label10" runat="server" Text="Fecha Solicitud:" ></asp:Label>
                                         </div>
                                         <div class="col-lg-4 col-sm-4 text-left">
                                             <telerik:RadDatePicker ID="txtFecha_sol_vis" runat="server" ReadOnly="true">
                                                 <DateInput runat="server" DateFormat="yyyy/MM/dd"> 
                                                 </DateInput>
                                             </telerik:RadDatePicker>
                                         </div>
                                        
                                         
                                 </div>
               
                
                  

                   <div class="row ">

                <div class="col-lg-2 col-sm-2 text-left">
                    <asp:Label ID="Label2" runat="server" Text="Fecha Entrega del Crédito:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                    <telerik:RadDatePicker ID="txt_fecha_sol" runat="server" ReadOnly="true">
                        <DateInput runat="server" DateFormat="yyyy/MM/dd">
                        </DateInput>
                    </telerik:RadDatePicker>
                </div>

                <div class="col-lg-2 col-sm-2 text-left">
                    <asp:Label ID="Label1" runat="server" Text="Grupo Productivo:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:TextBox ID="txt_gpro_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                </div>
</div>
      

                    <div class="row ">



                <div class="col-lg-2 col-sm-2 text-left">
                    <asp:Label ID="Label3" runat="server" Text="Número de Grupo:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:TextBox ID="txt_numGrup_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-small" MaxLength="100"></asp:TextBox>
                </div>

                <div class="col-lg-2 col-sm-2 text-left">
                    <asp:Label ID="Label4" runat="server" Text="Monto de Crédito:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:TextBox ID="txt_moncred_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-small" MaxLength="100"></asp:TextBox>
                </div>

            </div>

                    <div class="row ">



                <div class="col-lg-2 col-sm-2 text-left">
                    <asp:Label ID="Label5" runat="server" Text="Plazo:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:TextBox ID="txt_plazo_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-small" MaxLength="100"></asp:TextBox>
                </div>

                <div class="col-lg-2 col-sm-2 text-left">
                    <asp:Label ID="Label6" runat="server" Text="Tasa (%):"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:TextBox ID="txt_tasa_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-small" MaxLength="100"></asp:TextBox>
                </div>

            </div>

                    <div class="row ">



                <div class="col-lg-2 col-sm-2 text-left">
                    <asp:Label ID="Label8" runat="server" Text="Garantía Líquida:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:TextBox ID="txt_garaliq_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-small" MaxLength="100"></asp:TextBox>
                </div>

                <div class="col-lg-2 col-sm-2 text-left">
                    <asp:Label ID="Label9" runat="server" Text="Monto máximo del Crédito:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:TextBox ID="txt_mont_max" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-small" MaxLength="100"></asp:TextBox>
                </div>

            </div>

                    <div class="row ">



                <div class="col-lg-2 col-sm-2 text-left">
                    <asp:Label ID="Label11" runat="server" Text="Monto crédito Autorizado:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:TextBox ID="txt_montaut_vis" runat="server" ReadOnly="true"
                        CssClass="alingMiddle input-small" MaxLength="100"></asp:TextBox>
                </div>

                <div class="col-lg-2 col-sm-2 text-left">
                    <asp:Label ID="Label12" runat="server" Text="Plazo:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                   <asp:DropDownList ID="cmbplazo" runat="server">
                                                 <asp:ListItem Value="12">12 Semanas</asp:ListItem>
                                                 <asp:ListItem Value="16">16 Semanas</asp:ListItem>
                                                 <asp:ListItem Value="20">20 Semanas</asp:ListItem>
                                                 <asp:ListItem Value="24">24 Semanas</asp:ListItem>
                                             </asp:DropDownList>     
                </div>

            </div>

                    <div class="row ">



                <div class="col-lg-2 col-sm-2 text-left">
                    <asp:Label ID="Label13" runat="server" Text="Tasa(%):"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:DropDownList ID="cmbtaza" runat="server">
                                                 <asp:ListItem Value="4">4%</asp:ListItem>
                                                 <asp:ListItem Value="5">5%</asp:ListItem>
                                                 <asp:ListItem Value="6">6%</asp:ListItem>
                                                 <asp:ListItem Value="7">7%</asp:ListItem>
                                             </asp:DropDownList>     
                </div>

                <div class="col-lg-2 col-sm-2 text-left">
                    <asp:Label ID="Label14" runat="server" Text="Forma de Pago:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:TextBox ID="txt_formapag_vis" runat="server" ReadOnly="true" Visible="false"
                        CssClass="alingMiddle input-small" MaxLength="100"></asp:TextBox>
                </div>

            </div>
                  <br />    <br />
             </div>
             <br />    <br />
                     
                 <div class="row text-center">
                    <asp:LinkButton ID="lnkagregar" runat="server" ValidationGroup="crea" ToolTip="Guarda Encabezado" OnClick="lnkagregar_Click" CssClass="btn btn-success t14"  ><i class="fa fa-save"></i>&nbsp;<span>Guarda Encabezado</span></asp:LinkButton>
                </div>
                
            </div>

            <div class="row pad1m">
                            <div class="col-lg-6 col-sm-6 text-center" >
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" EnableAJAX="true">
                            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid2" AllowFilteringByColumn="true" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue"  OnSelectedIndexChanged="RadGrid2_SelectedIndexChanged"
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource1" AllowSorting="true" GroupingEnabled="false" PageSize="50" >                       
                                    <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="false" DataKeyNames="id_solicitud_credito,id_cliente">
                                        <Columns>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_cliente" FilterControlAltText="Filtro Cliente" HeaderText="Cliente" SortExpression="nombre_cliente" UniqueName="nombre_cliente" HeaderStyle-Width="70%" Resizable="true"/>                                            
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="credito_autorizado" FilterControlAltText="Filtro Credito Autorizado" DataFormatString="{0:N2}" HeaderText="Credito Autorizado" SortExpression="credito_autorizado" UniqueName="credito_autorizado" HeaderStyle-Width="30%" Resizable="true"/>                                    
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
                                <asp:ControlParameter ControlID="lblgrup" Name="idCredito" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                                </div>
                 <div class="col-lg-6 col-sm-6 text-center">

                 <div class="row ">
                            <div class="col-lg-2 col-sm-1 text-left">
                                             <asp:Label ID="Label15" runat="server" Text="Nombre Cliente:" ></asp:Label>
                                         </div>
                                         <div class="col-lg-4 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nombre_cli" runat="server"
                                                 CssClass="alingMiddle input-medium" MaxLength="100" ReadOnly="true" Visible="false"
                                                 PlaceHolder="Sucursal"></asp:TextBox> 
                                         </div>
                        
                                       
                                           <div class="col-lg-2 col-sm-1 text-left">
                                             <asp:Label ID="Label16" runat="server" Text="Ingreso:" ></asp:Label>
                                         </div>
                                         <div class="col-lg-4 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_ingreso" runat="server"
                                                 CssClass="alingMiddle input-medium" MaxLength="100" ReadOnly="true" Visible="false"
                                                 PlaceHolder="Ingreso"></asp:TextBox> 
                                         </div>
                                        
                                         
                                 </div>

                    <div class="row ">



                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label17" runat="server" Text="Destino Crédito:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_destino" runat="server"
                                                 CssClass="alingMiddle input-medium" MaxLength="100" ReadOnly="true" Visible="false"
                                                 PlaceHolder="Destino Credito"></asp:TextBox> 
                                         </div>

                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label18" runat="server" Text="Crédito Anterior:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_cred_ant" runat="server" ReadOnly="true" Visible="false"
                        CssClass="alingMiddle input-medium" MaxLength="100"></asp:TextBox>
                </div>

            </div>

                    <div class="row ">



                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label19" runat="server" Text="Crédito Solicitado:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_cred_sol" runat="server" ReadOnly="true" Visible="false"
                        CssClass="alingMiddle input-medium" MaxLength="100"></asp:TextBox>
                </div>

                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label20" runat="server" Text="Garantía Líquida:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_gara_liq" runat="server" ReadOnly="true" Visible="false"
                        CssClass="alingMiddle input-medium" MaxLength="100"></asp:TextBox>
                </div>

            </div>

                    <div class="row ">



                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label21" runat="server" Text="Crédito Autorizado:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_cred_aut" runat="server" ReadOnly="false" Visible="false"
                        CssClass="alingMiddle input-medium" MaxLength="100"></asp:TextBox>
                </div>

                <div class="col-lg-2 col-sm-1 text-left">
                    <asp:Label ID="Label22" runat="server" Text="Teléfono:"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-3 text-left">
                    <asp:TextBox ID="txt_tel" runat="server" ReadOnly="true" Visible="false"
                        CssClass="alingMiddle input-medium" MaxLength="100"></asp:TextBox>
                </div>

            </div>
                      <br />    <br />
                  
                      
                 <div class="row text-center">
                    <asp:LinkButton ID="lnkdetalle" runat="server" ValidationGroup="crea" ToolTip="Guarda Detalle" OnClick="lnkdetalle_Click" CssClass="btn btn-success t14"  ><i class="fa fa-save"></i>&nbsp;<span>Guarda Detalle</span></asp:LinkButton>
                </div>
                 

            </div>
             
                                </div>
        </ContentTemplate>

       
    </asp:UpdatePanel>
</asp:Content>

