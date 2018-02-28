<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="AnalisisCapacidadPago.aspx.cs" Inherits="AnalisisCapacidadPago" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>



<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">  
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"/>
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server"></telerik:RadSkinManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             
            <div class="page-header">
                            <!-- /BREADCRUMBS -->
                            <div class="clearfix"> 
                                <h3 class="content-title pull-left">
                                   Análisis Capacidad de Pago</h3>
                            </div>
                    </div>
            
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <asp:Panel ID="pnlMaskLoad" runat="server" CssClass="maskLoad"></asp:Panel>
                    <asp:Panel ID="pnlCargando" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoad" runat="server" ImageUrl="~/img/loading.gif" CssClass="center" />                            
                    </asp:Panel>
                </ProgressTemplate>                            
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
     

     <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
          <ContentTemplate>

               <div class="row text-center marTop">
                    <div class="row col-lg-6 col-sm-6 text-center">
                  <asp:LinkButton ID="lnkAbreWindow" runat="server" ToolTip="Guarda Solicitud" CssClass="btn btn-success t14" OnClick="lnkAbreWindow_Click"><i class="fa fa-save"></i>&nbsp;<span>Genera Análisis</span></asp:LinkButton>
                       </div>
                <div class="row col-lg-6 col-sm-6 text-center">
                  <asp:LinkButton ID="lnkAbreEdicion" runat="server" Visible="false" CssClass="btn btn-warning t14" OnClick="lnkAbreEdicion_Click"><i class="fa fa-save"></i>&nbsp;<span>Editar Ficha</span></asp:LinkButton>
                    </div>
                  
              </div>
              <br />
              <div class="row">
                  <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" EnableAJAX="true">
                      <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" AllowFilteringByColumn="true" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Metro" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged"
                          EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource1" AllowSorting="true" GroupingEnabled="false" PageSize="50">
                          <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="false" DataKeyNames="id_cliente,id_apago,id_solicitud_credito">
                              <Columns>
                                  <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" Visible="false" DataField="id_cliente" FilterControlAltText="Filtro Analisis" HeaderText="Análisis Pago" SortExpression="id_apago" UniqueName="id_apago" Resizable="true" />
                                  <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="grupo_productivo_apago" FilterControlAltText="Filtro Grupo" HeaderText="Grupo" SortExpression="grupo_productivo_apago" UniqueName="grupo_productivo_apago" Resizable="true" />
                                  <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_cliente_apago" FilterControlAltText="Filtro Nombre" HeaderText="Nombre" SortExpression="nombre_cliente_apago" UniqueName="nombre_cliente_apago" Resizable="true" />
                              </Columns>
                          </MasterTableView>
                          <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                              <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                              <Selecting AllowRowSelect="true" />
                          </ClientSettings>
                      </telerik:RadGrid>
                  </telerik:RadAjaxPanel>
                  <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="select a.id_apago,a.id_cliente,a.grupo_productivo_apago,a.nombre_cliente_apago,e.id_solicitud_credito from an_analisis_pago a inner join AN_Solicitud_Credito_Encabezado e on a.id_grupo=e.id_grupo where a.id_sucursal=@sucursal and a.id_empresa=@empresa order by id_apago desc" ConnectionString="<%$ ConnectionStrings:Taller %>">
                      <SelectParameters>
                          <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0" />
                          <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0" />
                      </SelectParameters>
                  </asp:SqlDataSource>

              </div>
               <div class="row marTop text-center">
                    <asp:LinkButton ID="lnkImprimir" runat="server" Visible="false" ToolTip="Imprimir Solicitud" OnClick="lnkImprimir_Click " CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Análisis</span></asp:LinkButton>                
                </div>
              <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                <ProgressTemplate>
                    <asp:Panel ID="pnlMaskLoad2" runat="server" CssClass="maskLoad"></asp:Panel>
                    <asp:Panel ID="pnlCargando2" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoad2" runat="server" ImageUrl="~/img/loading.gif" CssClass="center" />                            
                    </asp:Panel>
                </ProgressTemplate>                            
            </asp:UpdateProgress>

          </ContentTemplate>
     </asp:UpdatePanel>


     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
          <ContentTemplate>
              <asp:Panel ID="pnlMask" runat="server" CssClass="mask zen1"  Visible="false" />
               <asp:Panel ID="windowAutorizacion" CssClass="popUp zen2 textoCentrado ancho90" Height="90%" ScrollBars="Vertical"  Visible="false" runat="server">

                      <table class="ancho100">
                    <tr class="ancho100%">
                        
                         <div class="page-header" >
                <div class="clearfix">
                     <div class=" col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                     <asp:LinkButton ID="lnkCerrar" runat="server" CssClass="btn btn-danger alingMiddle" OnClick="lnkCerrar_Click" ToolTip="Cerrar"><i class="fa fa-remove t18"></i></asp:LinkButton>
                 </div>
                    <h3 class="content-title center">Analisis Capacidad de Pago
                    </h3>
                    <asp:Label ID="lblAnalisis" runat="server" Visible="false" CssClass="t22 colorMorado textoBold" />
                    
                </div>
                
            </div>
                    </tr>
                     
                </table>

                  
                
              <div>
                       <div class="row pad1m">
                   <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label74" runat="server" Text="Sucursal:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_sucursal" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100" ReadOnly="true"
                            PlaceHolder="Sucursal "></asp:TextBox>
                    </div>

                   <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label111" runat="server" Text="Fecha de Elaboracion:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadDatePicker ID="txt_fecha_ela" runat="server">
                            <DateInput runat="server" DateFormat="yyyy/MM/dd">
                            </DateInput>
                        </telerik:RadDatePicker>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6"  runat="server" ErrorMessage="Debe indicar el RFC / CURP" Text="*" ValidationGroup="crea" ControlToValidate="txt_fecha_ela" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                </div>

                    <div class="row pad1m">
                
                   <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label1" runat="server" Text="Gerente:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                    
                     <asp:DropDownList ID="ddlgerente" runat="server" AutoPostBack="true" DataSourceID="cmbgerente" DataValueField="id_usuario"
                           DataTextField="nombre_usuario" >
                       </asp:DropDownList>
                        </div>
                        <asp:SqlDataSource ID="cmbgerente" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select 0 as id_usuario,'Selecione Gerente'as nombre_usuario union all select id_usuario,nombre_usuario from usuarios where n_puesto='GRT'">
                          
                        </asp:SqlDataSource>

                         <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label40" runat="server" Text="Asesor:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                    
                     <asp:DropDownList ID="ddlAsesor" runat="server" AutoPostBack="true" DataSourceID="cmbasesor" DataValueField="id_usuario"
                           DataTextField="nombre_usuario" >
                       </asp:DropDownList>
                        </div>
                        <asp:SqlDataSource ID="cmbasesor" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select 0 as id_usuario,'Selecione Asesor'as nombre_usuario union all select id_usuario,nombre_usuario from usuarios where n_puesto='ASE'">
                          
                        </asp:SqlDataSource>
                  
                </div>

                    <div class="row pad1m">

                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label78" runat="server" Text="Número grupo:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_numero" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100" ReadOnly="true"
                            PlaceHolder="Número"></asp:TextBox>
                    </div>
                   
                       <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label9" runat="server" Text="Grupo Productivo:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                    
                     <asp:DropDownList ID="cmb_sucursal" runat="server" AutoPostBack="true" DataSourceID="cmbSucursal" DataValueField="id_grupo" OnSelectedIndexChanged="cmb_sucursal_SelectedIndexChanged"
                           DataTextField="grupo_productivo" >
                       </asp:DropDownList>
                        </div>
                        <asp:SqlDataSource ID="cmbSucursal" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select 0 as id_grupo,'Selecione Grupo'as grupo_productivo union all select id_grupo,grupo_productivo  from an_grupos where id_empresa=@empresa and id_sucursal=@sucursal">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                                <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />

                            </SelectParameters>
                        </asp:SqlDataSource>
                    

                        </div>
                        
                   <div class="row pad1m">

                       <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label129" runat="server" Text="Nombre Completo:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                       <asp:DropDownList ID="cmb_nombre" runat="server" AutoPostBack="true"  DataSourceID="cmbNombre" DataValueField="id_cliente" OnSelectedIndexChanged="cmb_nombre_SelectedIndexChanged" 
                           DataTextField="nombre_completo" >
                       </asp:DropDownList>
                        </div>
                        <asp:SqlDataSource ID="cmbNombre" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" >
                            <SelectParameters>
                                <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                                <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />

                            </SelectParameters>
                        </asp:SqlDataSource>
                            
                       
                              <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label39" runat="server" Text="Giro del Negocio:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_giro" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Giro"></asp:TextBox>
                    </div>

                <div class="row">
                    <div class="col-lg-12 col-sm-12 text-center pad1m alert-info textoBold t14"><asp:Label ID="Label2" runat="server" Text="Ventas Mensuales (A)"></asp:Label></div>                    
                </div>    

                        <div class="row pad1m">                   
                   <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label33" runat="server" Text="Lunes:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                            <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_l" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                       
                    </div>
                              <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label24" runat="server" Text="Martes:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_martes" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>      
                </div>   

                                <div class="row pad1m">
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label35" runat="server" Text="Miércoles:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                         <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_miercoles" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                       <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label23" runat="server" Text="Jueves:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_jueves" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>              
                </div>  
                                      
                        <div class="row pad1m">
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label3" runat="server" Text="Viernes:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_viernes" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                       <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label22" runat="server" Text="Sábado:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                         <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_sabado" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>         
                </div>

                        <div class="row pad1m">
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label4" runat="server" Text="Domingo:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_domingo" OnTextChanged="txt_domingo_TextChanged" AutoPostBack="true" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                       <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label21" runat="server" Text="Total Semanal:" CssClass="alingMiddle textoBold" ></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_totals" runat="server" ReadOnly="true" AutoPostBack="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder=""></asp:TextBox>
                    </div>            
                </div>

                        <div class="row pad1m">
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label11" runat="server" Text="Total Mensual:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_totalm" runat="server" ReadOnly="true" AutoPostBack="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder=""></asp:TextBox>
                    </div>
                            
                </div>

                <div class="row">
                    <div class="col-lg-12 col-sm-12 text-center pad1m alert-info textoBold t14"><asp:Label ID="Label20" runat="server" Text="Costos y Gastos Mensuales del Negocio (B)"></asp:Label></div>                    
                </div>    
                      
                         <div class="row pad1m">
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label12" runat="server" Text="Materias Primas:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_matp" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label19" runat="server" Text="Mercancías:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_mercancias" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>             
                </div>

                         <div class="row pad1m">
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label13" runat="server" Text="Renta:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                         <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_renta" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label18" runat="server" Text="Luz:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_luz" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>               
                </div>

                         <div class="row pad1m">
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label14" runat="server" Text="Agua:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_agua" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label17" runat="server" Text="Gas:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_gas" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>         
                </div>
                        
                         <div class="row pad1m">
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label15" runat="server" Text="Papelería:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_artpape" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                    
                      <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label16" runat="server" Text="Teléfono:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_telefono" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>          
                </div>     
                                  
                         <div class="row pad1m">
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label5" runat="server" Text="Sueldos y Salarios:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_sueldos" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                        <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label6" runat="server" Text="Transporte/Fletes:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_tranfle" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>             
                </div>  
                                      
                         <div class="row pad1m">
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label7" runat="server" Text="Mantenimiento:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_mantenimiento" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99" NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                        <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label25" runat="server" Text="Pago Impuestos:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_pagimp" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>    
                </div>

                         <div class="row pad1m">
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label26" runat="server" Text="Pago Finiancieras:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_pagfin" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label27" runat="server" Text="Otras Deudas:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_odeu" AutoPostBack="true" OnTextChanged="txt_odeu_TextChanged" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>         
                </div>
 
                        <div class="row pad1m">
                   
                  
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label29" runat="server" Text="Total(B):" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_totalb" runat="server" ReadOnly="true" AutoPostBack="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder=""></asp:TextBox>
                    </div>
                    </div>     
                         
                <div class="row">
                    <div class="col-lg-12 col-sm-12 text-center pad1m alert-info textoBold t14"><asp:Label ID="Label8" runat="server" Text="Gastos Mensuales del Hogar (Recursos Aportados Directamente por el Cliente)"></asp:Label></div>                    
                </div>    

                                
                        <div class="row pad1m">
                   <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label30" runat="server" Text="Renta :" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_ren_cli" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                 
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label41" runat="server" Text="Luz:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_luz_cli" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                    </div>

                         <div class="row pad1m">
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label42" runat="server" Text="Agua:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_agua_cli" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99" NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>  
                  
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label43" runat="server" Text="Teléfono:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_tel_cli" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                    </div>

                        <div class="row pad1m">
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label44" runat="server" Text="Alimentación:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_alimentacion" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>   
                  
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label45" runat="server" Text="Vestido:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_vestido" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                    </div>

                        <div class="row pad1m">
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label47" runat="server" Text="Gastos Escolares:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_gastos_esc" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                   
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label46" runat="server" Text="Gastos Médicos:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_gastos_med" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                    </div>

                        <div class="row pad1m">
                   <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label48" runat="server" Text="Transporte:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_trasnporte_cli" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                  
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label49" runat="server" Text="Deudas:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_deudas" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                    </div>

                        <div class="row pad1m">
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label50" runat="server" Text="Mantenimiento:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_mante" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                  
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label51" runat="server" Text="Pago Impuestos:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_pag_imp" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                    </div>
                    
                        <div class="row pad1m">
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label52" runat="server" Text="Otros Gastos:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txt_otrosg" OnTextChanged="txt_otrosg_TextChanged" AutoPostBack="true" CssClass="input-mini" Value="0" EmptyMessage="Precio" MinValue="0" MaxValue="9999999.99"  NumberFormat-DecimalDigits="2" Skin="MetroTouch"></telerik:RadNumericTextBox>                                
                    </div>
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label53" runat="server" Text="Total (C):" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_totalc" runat="server" ReadOnly="true" AutoPostBack="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder=""></asp:TextBox>
                    </div>
                    </div>

                        <div class="row">
                    <div class="col-lg-12 col-sm-12 text-center pad1m alert-info textoBold t14"><asp:Label ID="Label10" runat="server" Text="Disponibilidad Semanal Negocio"></asp:Label></div>                    
                </div> 

                      <div class="row pad1m">
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label28" runat="server" Text="Utilidad:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_utilidad" runat="server" ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder=""></asp:TextBox>
                    </div>
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label31" runat="server" Text="Disponibilidad Semanal:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_dissem" runat="server" ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder=""></asp:TextBox>
                    </div>
                    </div>

                          <div class="row">
                    <div class="col-lg-12 col-sm-12 text-center pad1m alert-info textoBold t14"><asp:Label ID="Label32" runat="server" Text="Solvencia para el pago de Crédito"></asp:Label></div>                    
                </div> 

                    <div class="row pad1m">
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label34" runat="server" Text="Monto del Crédito:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_mon_cred" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100" AutoPostBack="true"
                            PlaceHolder=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36"  runat="server" ErrorMessage="Debe indicar el RFC / CURP" Text="*" ValidationGroup="crea" ControlToValidate="txt_mon_cred" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label36" runat="server" Text="Plazo:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_plazo" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"  AutoPostBack="true"
                            PlaceHolder=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator37"  runat="server" ErrorMessage="Debe indicar el RFC / CURP" Text="*" ValidationGroup="crea" ControlToValidate="txt_plazo" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                    </div>

                    <div class="row pad1m">
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label37" runat="server" Text="Pago Semanal:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_pagsem" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100" ReadOnly="true" AutoPostBack="true"
                            PlaceHolder=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator38"  runat="server" ErrorMessage="Debe indicar el RFC / CURP" Text="*" ValidationGroup="crea" ControlToValidate="txt_pagsem" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label38" runat="server" Text="Solvencia:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_solvencia" runat="server" ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder=""></asp:TextBox>
                    </div>
                    </div>

                    <div class="row pad1m">
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Sexo" runat="server" Text="Dictamen Final:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:DropDownList ID="cmb_dic_fin" runat="server">
                            <asp:ListItem Value="Pro">Procedente</asp:ListItem>
                            <asp:ListItem Value="Imp">Improcedente</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
                    </div>

                   </div> 
                      </div>
               <div class="row marTop">
                    <asp:Label ID="lblEditaAgrega" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblIdConsultaEdita" runat="server" Visible="false" ></asp:Label>
                </div>  

                <div class="row text-center">
                    <asp:LinkButton ID="lnkAgregaAnalisis" runat="server" ValidationGroup="crea" ToolTip="Guarda Solicitud" OnClick="agregaAnalisisP" CssClass="btn btn-success t14"  ><i class="fa fa-save"></i>&nbsp;<span>Guarda Análisis de Pago</span></asp:LinkButton>
                </div>
                <div class="row text-center">
                    <asp:Label ID="lblErrorAgrega" runat="server" Visible="false" CssClass=""></asp:Label>
                </div>
                   <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                <ProgressTemplate>
                    <asp:Panel ID="pnlMaskLoad3" runat="server" CssClass="maskLoad"></asp:Panel>
                    <asp:Panel ID="pnlCargando3" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoad3" runat="server" ImageUrl="~/img/loading.gif" CssClass="center" />                            
                    </asp:Panel>
                </ProgressTemplate>                            
            </asp:UpdateProgress>
               
               </asp:Panel>
          </ContentTemplate>
     </asp:UpdatePanel>

 </asp:Content>
