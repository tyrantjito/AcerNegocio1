<%@ Page Title="" Language="C#" MasterPageFile="~/AdmonOrden.master" AutoEventWireup="true" CodeFile="ValidaciónTelefonica.aspx.cs" Inherits="ValidaciónTelefonica" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
                    </asp:ScriptManager>

                 <div class="page-header">
                         <!-- /BREADCRUMBS -->
                         <div class="clearfix">
                             <h3 class="content-title pull-left">
                              Validación Telefónica</h3> 
                         </div>
                    </div>



        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>
                 <div class="row pad1m">
                <div class="col-lg-12 col-sm-12 text-center">
                    <div class="col-lg-6 col-sm-6 text-right pad1m">
                        <asp:Label ID="Label11" runat="server" Text="Tipo Llamada:" />&nbsp;
                    </div>
                    <div class="col-lg-6 col-sm-6 text-left pad1m">
                        <asp:RadioButtonList ID="rbtnTipoLlamada" AutoPostBack="true" runat="server"
                             RepeatDirection="Horizontal">
                            <asp:ListItem Text="Pendiente&nbsp;" Value="P"  Selected="True" />
                            <asp:ListItem Text="Validada;" Value="V"/>    
                        </asp:RadioButtonList>
                        <asp:Label ID="lblConsecutivo" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>


            </div>


                  <asp:Panel runat="server" ID="pnlCronos" CssClass="col-lg-12 col-sm-12" ScrollBars="Auto" Style="margin-bottom: 10px;">
                     <br />
                      <div class="row pad1m">
                           <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label1" runat="server" Text="Grupo Productivo:" CssClass="alingMiddle textoBold"></asp:Label>
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

                           <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label129" runat="server" Text="Nombre Completo:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                       <asp:DropDownList ID="cmb_nombre" runat="server" AutoPostBack="true"  DataSourceID="cmbNombre" DataValueField="id_cliente" OnSelectedIndexChanged="cmb_nombre_SelectedIndexChanged" 
                           DataTextField="nombre_completo" >
                       </asp:DropDownList>
                        <asp:SqlDataSource ID="cmbNombre" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" >
                            <SelectParameters>
                                <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                                <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />

                            </SelectParameters>
                        </asp:SqlDataSource>
                            </div>
                      </div>

                      <div class="row pad1m">
                           <div class="col-lg-8 col-sm-8 text-left">
                        <asp:Label ID="Label3" runat="server" Text="Numeros Telefonicos Asignados:   " CssClass="alingMiddle textoBold"></asp:Label>        
                        <asp:TextBox ID="txt_tel" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100" AutoPostBack="true" ReadOnly="true"
                            PlaceHolder="Teléfono"></asp:TextBox>
                               <asp:TextBox ID="txt_tel_cel" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100" AutoPostBack="true" ReadOnly="true"
                            PlaceHolder="Celular"></asp:TextBox>
                    </div>

                      </div>
                      <br />
                <div class="row pad1m">
                       <div class="col-lg-4 col-sm-4 text-center">
                        <asp:Label ID="Label39" runat="server" Text="Llamó:" CssClass="alingMiddle textoBold"></asp:Label>        
                        <asp:TextBox ID="txtLlamo" runat="server" ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Llamó"></asp:TextBox>
                        
                    </div>
                   
                     <div class="col-lg-4 col-sm-4 text-center">
                        <asp:Label ID="Label2" runat="server" Text="Contestó:" CssClass="alingMiddle textoBold"></asp:Label>
                        <asp:TextBox ID="txtContesto" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Contestó"></asp:TextBox>
                     
                    </div>


                    
                </div>


                    <div class="row pad1m">
                         <div class="col-lg-4 col-sm-4 text-center">
                        <asp:Label ID="Label5" runat="server" Text="Fecha Llamada:" CssClass="alingMiddle textoBold"></asp:Label>
                        <asp:TextBox ID="txtFechaLlamada" runat="server"
                            CssClass="alingMiddle input-small" MaxLength="100" Enabled="false" 
                            PlaceHolder="Fecha"></asp:TextBox>
                              <cc1:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="txtFechaLlamada_CalendarExtender" TargetControlID="txtFechaLlamada" Format="yyyy-MM-dd" PopupButtonID="lnkFechaLlamada" />
                    <asp:LinkButton ID="lnkFechaLlamada" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                    
                    <telerik:RadTimePicker RenderMode="Lightweight" ID="RadTimePicker1" Culture="es-Mx" DateInput-DateFormat="HH:mm:ss" DateInput-EmptyMessage="Hora" Skin="MetroTouch" Width="120px" runat="server"></telerik:RadTimePicker>                 
                    </div>

                        
                     <div class="col-lg-8 col-sm-8 text-center">
                        <asp:Label ID="Label6" runat="server" Text="Comentario Cliente:" CssClass="alingMiddle textoBold"></asp:Label>
                        <asp:TextBox ID="txtComentarioCliente" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Comentario Cliente"></asp:TextBox>
                   
                    </div>                            
                </div>

                     

                       

                      <div class="row marTop">
                    <div class="col-lg-12 col-sm-12 text-center">
                        <asp:Label ID="lblFechaActual" runat="server" Visible="false"/>
                        <asp:Label ID="lblHoraActual" runat="server" Visible="false"/>
                       <div class="col-lg-6 col-sm-6 text-center">
                        <asp:LinkButton ID="lnkGuarda" runat="server" ToolTip="Guarda Llamada" CssClass="btn btn-success t14" OnClick="agregarLlamadas" ValidationGroup="agrega" ><i class="fa fa-save"></i><span>&nbsp;Guarda Llamada</span></asp:LinkButton>
                            </div>
                         <div class="col-lg-6 col-sm-6 text-center">
                        <asp:LinkButton ID="lnkcuestionario" runat="server" ToolTip="Cuestionario" Visible="false" CssClass="btn btn-info t14" OnClick="lnkcuestionario_Click" ValidationGroup="agrega" ><i class="fa fa-save"></i><span>&nbsp;Cuestionario</span></asp:LinkButton>
                            </div>
                    </div>
                </div>
                      <br /><br />

                      <div class="row">
                            <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" EnableAJAX="true">
                            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid2" AllowFilteringByColumn="true" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Metro"  
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" OnItemCommand="RadGrid2_ItemCommand" DataSourceID="SqlDataSource1" AllowSorting="true" GroupingEnabled="false" PageSize="50" >                       
                                    <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="false" DataKeyNames="id_llamada,id_cliente">
                                        <Columns>
                                  <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_completo" FilterControlAltText="Filtro Nombre" HeaderText="Nombre" SortExpression="nombre_completo" UniqueName="nombre_completo" Resizable="true" HeaderStyle-Width="20%" />
                                  <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="tipo_llamada" FilterControlAltText="Filtro Estatus" HeaderText="Estatus Llamada" SortExpression="tipo_llamada" UniqueName="tipo_llamada" Resizable="true" HeaderStyle-Width="15%" />
                                  <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="fecha_llamada" FilterControlAltText="Filtro Fecha" HeaderText="Fecha" SortExpression="fecha_llamada" UniqueName="fecha_llamada" Resizable="true" HeaderStyle-Width="15%" />
                                  <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="observaciones" FilterControlAltText="Filtro Observaciones" HeaderText="Observaciones" SortExpression="observaciones" UniqueName="observaciones" Resizable="true"  HeaderStyle-Width="50%" />
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="No existen llamadas registradas" CssClass="text-danger"></asp:Label>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                   <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>   
                                                                             
                                </telerik:RadGrid>
                        </telerik:RadAjaxPanel>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server"   ConnectionString="<%$ ConnectionStrings:Taller %>">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                                <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />
                            </SelectParameters>
                        </asp:SqlDataSource>


                  

              </div>
                      
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
              <ContentTemplate>
                   <asp:Panel ID="pnlMask" runat="server" CssClass="mask zen1"  Visible="false" />
               <asp:Panel ID="windowAutorizacion" CssClass="popUp zen2 textoCentrado ancho90" Height="70%" ScrollBars="Vertical"  Visible="false" runat="server">

                      <table class="ancho100">
                    <tr class="ancho100%">
                        
                         <div class="page-header" >
                <div class="clearfix">
                     <div class=" col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                     <asp:LinkButton ID="lnkCerrar" runat="server" CssClass="btn btn-danger alingMiddle" OnClick="lnkCerrar_Click" ToolTip="Cerrar"><i class="fa fa-remove t18"></i></asp:LinkButton>
                 </div>
                    <h3 class="content-title center">Cuestionario Para Verficación de Grupos Nuevos
                    </h3>
                    <asp:Label ID="lblAnalisis" runat="server" Visible="false" CssClass="t22 colorMorado textoBold" />
                    
                </div>
                
            </div>
                    </tr>
                     
                </table>

                  
                
              <div>
                       <div class="row pad1m">
                   <div class="col-lg-2 col-sm-2 text-left">
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                    </div>
                   <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label7" runat="server" Text="Fecha :" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadDatePicker ID="txtFecha"  runat="server"> 
                            <DateInput runat="server" ReadOnly="true" DateFormat="yyyy/MM/dd">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </div>
                </div>
                  <div class="row pad1m">
                   <div class="col-lg-1 col-sm-1 text-left">
                           <asp:Label ID="Label74" runat="server" Text="Sucursal:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:TextBox ID="txt_sucursal" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100" ReadOnly="true"
                            PlaceHolder="Sucursal "></asp:TextBox>
                    </div>
                       <div class="col-lg-1 col-sm-1 text-left">
                           <asp:Label ID="Label8" runat="server" Text="Grupo Productivo:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-2 col-sm-4 text-left">
                         <asp:TextBox ID="txtGrupo" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100" ReadOnly="true"
                            PlaceHolder="Grupo "></asp:TextBox>
                    </div>
                   <div class="col-lg-1 col-sm-1 text-left">
                        <asp:Label ID="Label4" runat="server" Text="Numero:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-2 col-sm-4 text-left">
                        <asp:TextBox ID="txtnumero" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100" ReadOnly="true"
                            PlaceHolder="Numero "></asp:TextBox>
                    </div>
                       <div class="col-lg-1 col-sm-1 text-left">
                        <asp:Label ID="Label9" runat="server" Text="Ciclo:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-2 col-sm-4 text-left">
                        <asp:TextBox ID="txtCiclo" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100" ReadOnly="true"
                            PlaceHolder="Ciclo "></asp:TextBox>
                    </div>

                </div>
                  
                  <div class="row pad1m">
                      <div class="col-lg-6 col-sm-6 text-center ">
                           <asp:Label ID="Label10" runat="server" Text="Preguntas"  CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                      <div class="col-lg-6 col-sm-6 text-center">
                           <asp:Label ID="Label12" runat="server"  Text="Respuesta" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>

                  </div>
                   
                  <div class="row pad1m">
                      <div class="col-lg-6 col-sm-6 text-center ">
                           <asp:Label ID="Label13" runat="server" Text="1 ¿A QUE GRUPO PERTENECE USTED?"  CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                      <div class="col-lg-2 col-sm-2 text-center">
                           <asp:TextBox ID="txtP1" runat="server" ReadOnly="true"
                            CssClass="alingMiddle input-medium" MaxLength="100" 
                           ></asp:TextBox>
                          </div>
                       <div class="col-lg-2 col-sm-2 text-right">
                           <asp:CheckBox ID="txtsi1" runat="server" Text="SI" />
                           </div>
                       <div class="col-lg-2 col-sm-2 text-left">
                          <asp:CheckBox ID="txtno1" runat="server" Text="NO" />
                    </div>
                       <div class="col-lg-6 col-sm-6 text-center ">
                           <asp:Label ID="Label15" runat="server" Text="2 NOMBRE DE LA PRESIDENTA DEL GRUPO"  CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                      <div class="col-lg-2 col-sm-2 text-center">
                           <asp:TextBox ID="txtp2" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100" ReadOnly="true"
                            ></asp:TextBox>
                    </div>
                   <div class="col-lg-2 col-sm-2 text-right">
                           <asp:CheckBox ID="txtsi2" runat="server" Text="SI" />
                           </div>
                       <div class="col-lg-2 col-sm-2 text-left">
                          <asp:CheckBox ID="txtno2" runat="server" Text="NO" />
                    </div>
                      <div class="col-lg-6 col-sm-6 text-center ">
                           <asp:Label ID="Label17" runat="server" Text="3 NOMBRE DE LA TESORERA DEL GRUPO"  CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                      <div class="col-lg-2 col-sm-2 text-center">
                          <asp:TextBox ID="txtp3" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100" ReadOnly="true"
                            ></asp:TextBox>
                    </div>
                        <div class="col-lg-2 col-sm-2 text-right">
                           <asp:CheckBox ID="txtsi3" runat="server" Text="SI" />
                           </div>
                       <div class="col-lg-2 col-sm-2 text-left">
                          <asp:CheckBox ID="txtno3" runat="server" Text="NO" />
                    </div>
                       <div class="col-lg-6 col-sm-6 text-center ">
                           <asp:Label ID="Label14" runat="server" Text="4 ¿A CUANTAS REUNIONES HA ASISTIDO?"  CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                      <div class="col-lg-2 col-sm-2 text-center">
                          <asp:TextBox ID="txtp4" runat="server"
                            CssClass="alingMiddle input-xxlarge" MaxLength="100" 
                            ></asp:TextBox>
                    </div>
                       <div class="col-lg-6 col-sm-6 text-center ">
                           <asp:Label ID="Label16" runat="server" Text="5 MENCIONAR EL NOMBRE DEL GERENTE QUE ASISTIO A LA VISITA DE NEGOCIO"  CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                      <div class="col-lg-2 col-sm-2 text-center">
                          <asp:TextBox ID="txtp5" runat="server"
                            CssClass="alingMiddle input-xxlarge" MaxLength="100" 
                            ></asp:TextBox>
                    </div>

                   </div>
               <div class="col-lg-6 col-sm-6 text-center">
                        <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Guarda Llamada" CssClass="btn btn-success t14" OnClick="LinkButton1_Click" ValidationGroup="agrega" ><i class="fa fa-save"></i><span>&nbsp;Guarda Cuestionario</span></asp:LinkButton>
                            </div>

                    
               </asp:Panel>

              </ContentTemplate>
          </asp:UpdatePanel>

                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


</asp:Content>

