<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="VisitaOcular.aspx.cs" Inherits="VisitaOcular" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"/>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <!-- Letrero Acta de Integración y regulamiento interno-->
               
                    <div class="page-header">
                            <!-- /BREADCRUMBS -->
                            <div class="clearfix">
                                <h3 class="content-title pull-left">
                                   Visita Ocular</h3> 
                            </div>
                    </div>
            
             
             

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                <ContentTemplate>
                    <div class="row pad1m">
                       
                           
                        <div class="row col-lg-6 col-sm-6 text-center">
                           
                                <asp:LinkButton ID="lnkAbreWindow" runat="server" ToolTip="Guarda Solicitud" CssClass="btn btn-success t14" OnClick="lnkAbreWindow_Click"><i class="fa fa-save"></i>&nbsp;<span>Genera Visita</span></asp:LinkButton>
                            </div>
                         <div class="row col-lg-6 col-sm-6 text-center">
                            <asp:LinkButton ID="lnkAbreEdicion" runat="server" Visible="false" CssClass="btn btn-warning t14" OnClick="lnkAbreEdicion_Click"><i class="fa fa-save"></i>&nbsp;<span>Editar Visita</span></asp:LinkButton>
                                
                        </div>

                        </div>

                    
                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" EnableAJAX="true">
                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" AllowFilteringByColumn="true"  runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Metro" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged"
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource1" AllowSorting="true" GroupingEnabled="false" PageSize="50" >
                        <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="false" DataKeyNames="id_cliente,id_visita">
                            <Columns>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_completo" FilterControlAltText="Filtro Cliente" HeaderText="Cliente" SortExpression="nombre_completo" UniqueName="nombre_completo" Resizable="true"  />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="fecha_visita" FilterControlAltText="Filtro Fecha Visita" HeaderText="Fecha Visita" SortExpression="fecha_visita" UniqueName="fecha_visita" Resizable="true" />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="gerente_sucursal_visita" FilterControlAltText="Filtro Gerente Sucursal" HeaderText="Gerente Sucursal" SortExpression="gerente_sucursal_visita" UniqueName="gerente_sucursal_visita" Resizable="true" />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="select a.id_visita,b.nombre_completo,a.id_cliente,convert(char(10),a.fecha_visita,120) as 
                                        fecha_visita,a.gerente_sucursal_visita from AN_visita_ocular a inner join an_Clientes b on b.id_cliente=a.id_cliente where a.id_sucursal=@sucursal and a.id_empresa=@empresa order by a.id_visita desc" ConnectionString="<%$ ConnectionStrings:Taller %>">
                     <SelectParameters>
                                        <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0"/>
                                        <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0"/>
                                    </SelectParameters>
                </asp:SqlDataSource>
          
                    <div class="row marTop text-center">
                    <asp:LinkButton ID="lnkImprimir" runat ="server" Visible ="false" OnClick="lnkImprimir_Click" CssClass="btn btn-info"> <i class="fa fa-print"><span>&nbsp;Imprimir Visita</span></i></asp:LinkButton>
                </div> 
                </ContentTemplate>
            </asp:UpdatePanel>

               <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlMask" runat="server" CssClass="mask zen1" Visible="false" />
            <asp:Panel ID="windowAutorizacion" runat="server" CssClass="popUp zen2 textoCentrado ancho80" Height="80%" ScrollBars="Auto"  Visible="false">

                  <table class="ancho100">
                    <tr class="ancho100%">
                        
                         <div class="page-header" >
                <div class="clearfix">
                     <div class=" col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                     <asp:LinkButton ID="lnkCerrar" runat="server" CssClass="btn btn-danger alingMiddle" OnClick="lnkCerrar_Click" ToolTip="Cerrar"><i class="fa fa-remove t18"></i></asp:LinkButton>
                 </div>
                    <h3 class="content-title center">Visita Ocular
                    </h3>
                    <asp:Label ID="lblVisita" runat="server" Visible="false" CssClass="t22 colorMorado textoBold" />
                    
                </div>
                
            </div>
                    </tr>
                     
                </table>

           
                 <div class="row text-center pad1m">
                    <div class="col-lg-12 col-sm-12 text-center">
                    </div>
                </div>

                 <cc1:Accordion ID="acNuevaRecep" runat="server" CssClass="ancho95 centrado" FadeTransitions="true" HeaderCssClass="encabezadoAcordeonPanel" HeaderSelectedCssClass="encabezadoAcordeonPanelSelect">
                      <Panes>
                           <cc1:AccordionPane ID="acpPersonales" runat="server" CssClass="ancho95" Visible="true" Style="cursor: pointer;">
                                <Header>Sucursal</Header>
                            <Content>
                                 <div>
                <div class="row pad1m">
                    <div class="col-lg-2 col-sm-2 text-left">
                        <label id="lblFechaVisita" class="text-left textoBold col-lg-6 col-md-6 col-sm-12 col-xs-12">Fecha de visita</label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadDatePicker ID="txt_fecha_visita" runat="server">
                            <DateInput ID="DateInput1" runat="server" DateFormat="yyyy/MM/dd">
                            </DateInput>
                        </telerik:RadDatePicker>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6"  runat="server" ErrorMessage="Debe indicar la fecha" Text="*" ValidationGroup="crea" ControlToValidate="txt_fecha_visita" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>

                    <!-- GrupoProductivo-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label4" runat="server" Text="Grupo Productivo:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                    
                     <asp:DropDownList ID="cmbGrupo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbGrupo_SelectedIndexChanged" DataSourceID="cmbSucursal" DataValueField="id_grupo"
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

                    <!-- Numero-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label5" runat="server" Text="Número:" CssClass="alingMiddle textoBold" ></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txtNumeroGrupo" ReadOnly="true" runat="server"  AutoPostBack="true"></asp:TextBox>
                    </div>

                    <!-- TipoDeCredito-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label6" runat="server" Text="Tipo de Crédito :" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_tipo_credito" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Tipo Crédito "></asp:TextBox>
                    </div>
                </div>

                <div class="row pad1m">

                    <!-- GerenteDeSucursal-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label7" runat="server" Text="Gerente Sucursal :" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_gerente" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Gerente "></asp:TextBox>
                    </div>

                    <!-- AsesorDeCredito-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label8" runat="server" Text="Asesor de Crédito :" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_asesor" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Asesor Crédito "></asp:TextBox>
                    </div>
                </div>
            </div>

      
                            </Content>
                           </cc1:AccordionPane>
                            <cc1:AccordionPane ID="acpDomicilio" runat="server" CssClass="ancho95" Visible="true">
                                 <Header>Datos del Cliente</Header>
                            <Content>
                                <div class="row pad1m">


                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label129" runat="server" Text="Nombre Completo:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                       <asp:DropDownList ID="cmb_nombre" runat="server"   AutoPostBack="true" OnSelectedIndexChanged="cmbPersonaSelected" DataSourceID="cmbNombre" DataValueField="id_cliente"
                           DataTextField="nombre_completo" >
                           
                       </asp:DropDownList>
                        <asp:SqlDataSource ID="cmbNombre" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" >
                            <SelectParameters>
                                <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                                <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />

                            </SelectParameters>
                        </asp:SqlDataSource>
                            </div>
                            
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label9" runat="server" Text="Fecha Nacimiento :" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadDatePicker ID="txtFecha_cli" runat="server" >
                            <DateInput runat="server" ReadOnly="true" DateFormat="yyyy/MM/dd">
                            </DateInput>
                        </telerik:RadDatePicker>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ErrorMessage="Debe indicar la fecha" Text="*" ValidationGroup="crea" ControlToValidate="txtFecha_cli" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                    </div>

                <div class="row pad1m">
                    <!-- EdadCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label13" runat="server" Text="Edad:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_edad_cli" runat="server" CssClass="alingMiddle input-large" MaxLength="2"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" BehaviorID="txt_edad_cliTextBoxWatermarkExtender" TargetControlID="txt_edad_cli" WatermarkText="Edad" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_edad_cli" />
                                              <asp:RequiredFieldValidator ID="validacionHijosCli" runat="server" ErrorMessage="Debe indicar la edad " Text="*" ValidationGroup="crea" ControlToValidate="txt_edad_cli" CssClass="alineado errores"></asp:RequiredFieldValidator>
                                       
                    </div>

                    <!-- SexoCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Sexo" runat="server" Text="Sexo:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:DropDownList ID="cmb_gen_cli" runat="server" ReadOnly="true">
                            <asp:ListItem Value="H">Hombre</asp:ListItem>
                            <asp:ListItem Value="M">Mujer</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row pad1m">
                    <!-- PersonasDependientesCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label14" runat="server" Text="Personas que dependen de usted:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_perdep_cli" runat="server" CssClass="alingMiddle input-large" MaxLength="2"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" BehaviorID="txt_perdep_cliTextBoxWatermarkExtender" TargetControlID="txt_perdep_cli" WatermarkText="Dependientes" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_perdep_cli" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  runat="server" ErrorMessage="Debe indicar la fecha" Text="*" ValidationGroup="crea" ControlToValidate="txt_perdep_cli" CssClass="alineado errores"></asp:RequiredFieldValidator>

                    </div>


                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label111" runat="server" Text="Cliente Desde:" CssClass="alingMiddle 
                                                 textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadDatePicker ID="txt_clientedesde" runat="server">
                            <DateInput runat="server" DateFormat="yyyy/MM/dd">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  runat="server" ErrorMessage="Debe indicar la fecha" Text="*" ValidationGroup="crea" ControlToValidate="txt_clientedesde" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                </div>

          
                            </Content>
                            </cc1:AccordionPane>
                          <cc1:AccordionPane ID="AccordionPane1" runat="server" CssClass="ancho95" Visible="true">
                               <Header>Domicilio</Header>
                            <Content>
                                 <div class="row pad1m">


                    <!--CalleDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label15" runat="server" Text="Calle:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_calle_cli" runat="server" ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Calle"></asp:TextBox>
                    </div>

                    <!-- NumExtDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label16" runat="server" Text="Número Exterior:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_n_ext_cli" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Número Exterior"></asp:TextBox>
                    </div>

                </div>

                <div class="row pad1m">
                    <!-- NumIntDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label17" runat="server" Text="Número Interior:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_n_interior_c" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Número Interior"></asp:TextBox>
                    </div>

                    <!-- ColoniaDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label18" runat="server" Text="Colonia:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_colonia_cli" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Colonia"></asp:TextBox>
                    </div>

                </div>

                <div class="row pad1m">
                    <!-- CodigoPostalDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label19" runat="server" Text="Código Postal:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_cp_cli" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="C.P."></asp:TextBox>
                    </div>

                    <!-- MunicipioDelegacionDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label20" runat="server" Text="Municipio o Delegación:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_mundel_cli" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Municipio o Delegación"></asp:TextBox>
                    </div>
                </div>

                <div class="row pad1m">
                    <!-- EstadoDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label21" runat="server" Text="Estado:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_estado_cli" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Estado"></asp:TextBox>
                    </div>

                    <!-- EntreCallesDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label22" runat="server" Text="Entre Calles:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txt_entrecalles_cli" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Entre Calles"></asp:TextBox>
                    </div>
                </div>

                <div class="row pad1m">
                    <!-- TelefonoDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label23" runat="server" Text="Teléfono:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                         <asp:TextBox ID="txt_telefono_cli" runat="server" CssClass="alingMiddle input-large" MaxLength="18"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" BehaviorID="txt_telefono_cliTextBoxWatermarkExtender" TargetControlID="txt_telefono_cli" WatermarkText="Teléfono" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_telefono_cli" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10"  runat="server" ErrorMessage="Debe indicar el numero Telefónico" Text="*" ValidationGroup="crea" ControlToValidate="txt_telefono_cli" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                </div>


                            </Content>
                          </cc1:AccordionPane>
                           <cc1:AccordionPane ID="AccordionPane2" runat="server" CssClass="ancho95" Visible="true">
                                 <Header>Datos Socioeconómicos del Cliente</Header>
                            <Content>
                                 <div class="row pad1m">


                    <!--CalleDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label1" runat="server" Text="Calle:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Calle"></asp:TextBox>
                    </div>

                    <!-- NumExtDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label2" runat="server" Text="Número Exterior:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="TextBox2" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Número Exterior"></asp:TextBox>
                    </div>

                </div>

                <div class="row pad1m">
                    <!-- NumIntDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label3" runat="server" Text="Número Interior:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="TextBox3" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Número Interior"></asp:TextBox>
                    </div>

                    <!-- ColoniaDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label10" runat="server" Text="Colonia:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="TextBox4" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Colonia"></asp:TextBox>
                    </div>

                </div>

                <div class="row pad1m">
                    <!-- CodigoPostalDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label11" runat="server" Text="Código Postal:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="TextBox5" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="C.P."></asp:TextBox>
                    </div>

                    <!-- MunicipioDelegacionDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label12" runat="server" Text="Municipio o Delegación:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="TextBox6" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Municipio o Delegación"></asp:TextBox>
                    </div>
                </div>

                <div class="row pad1m">
                    <!-- EstadoDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label24" runat="server" Text="Estado:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="TextBox7" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Estado"></asp:TextBox>
                    </div>

                    <!-- EntreCallesDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label25" runat="server" Text="Entre Calles:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="TextBox8" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Entre Calles"></asp:TextBox>
                    </div>
                </div>

                <div class="row pad1m">
                    <!-- TelefonoDomicilioCliente-->
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label26" runat="server" Text="Teléfono:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                         <asp:TextBox ID="TextBox9" runat="server" CssClass="alingMiddle input-large" MaxLength="18"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" BehaviorID="txt_telefono_cliTextBoxWatermarkExtender" TargetControlID="TextBox9" WatermarkText="Teléfono" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="TextBox9" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  runat="server" ErrorMessage="Debe indicar el numero Telefónico" Text="*" ValidationGroup="crea" ControlToValidate="TextBox9" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                </div>
            <!-- Letrero Datos Socioeconomicos del Cliente-->
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center alert-info">
                    <!--<span>Acta de Integración y regulamiento interno</span>-->
                    <h3>Datos Socioeconómicos del Cliente
                    </h3>
                </div>
            </div>

            <!-- Datos Socioeconómicos del Cliente -->


            <!--Tipos de Vivienda-->
            <div class="row pad1m">
                <div class="row">

                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="tV" runat="server" Text="Tipo de Vivienda:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:DropDownList ID="cmb_tipo_vivienda" runat="server">
                            <asp:ListItem Value="Pro">Propia</asp:ListItem>
                            <asp:ListItem Value="Ren">Rentada</asp:ListItem>
                            <asp:ListItem Value="Pre">Prestada</asp:ListItem>
                            <asp:ListItem Value="Otr">Otro</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            </div>

            <!--Servicios-->
            <div class="row pad1m">

                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <label id="lblServicios" class="text-left textoBold col-lg-12 col-md-12 col-sm-12 col-xs-12">Servicios</label>
                    </div>
                </div>
                <br />

                <div class="row">

                    <!--ServiciosLuz-->
                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">

                        <asp:CheckBox ID="chk_luz" runat="server" Text="Luz" />
                    </div>

                    <!-- ServiciosAgua-->
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <asp:CheckBox ID="chk_agua" runat="server" Text="Agua" />
                    </div>

                    <!-- ServiciosDrenaje-->
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <asp:CheckBox ID="chk_drenaje" runat="server" Text="Drenaje" />
                    </div>

                    <!-- ServiciosTelefono-->
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <asp:CheckBox ID="chk_tel" runat="server" Text="Teléfono" />
                    </div>

                    <!--ServiciosInternet-->
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <asp:CheckBox ID="chk_internet" runat="server" Text="Internet" />
                    </div>

                    <!--ServiciosGas-->
                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                        <asp:CheckBox ID="chk_gas" runat="server" Text="Gas" />
                    </div>

                    <!--ServiciosTVPaga-->
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <asp:CheckBox ID="chk_tv" runat="server" Text="TV Paga" />
                    </div>
                </div>
            </div>


            <!--Tipo de construcción-->

            <div class="row pad1m">
                <div class="row">

                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label27" runat="server" Text="Tipo de Construcción:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:DropDownList ID="cmb_tipoCons" runat="server">
                            <asp:ListItem Value="Con">Concreto</asp:ListItem>
                            <asp:ListItem Value="Mad">Madera</asp:ListItem>
                            <asp:ListItem Value="Lam">Lamina</asp:ListItem>
                            <asp:ListItem Value="AoB">Adobe/Barro</asp:ListItem>
                            <asp:ListItem Value="Otr">Otro</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            </div>



            <!--AparatosElectrodomesticos-->
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <label id="lblaparatos" class="text-left textoBold col-lg-12 col-md-12 col-sm-12 col-xs-12">Aparatos electrodoméstico y muebles de vivienda</label>
                </div>
            </div>

            <div class="row pad1m">
                <div class="row">
                    <!--AparatosElectrodomesticosSala-->
                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                        <asp:CheckBox ID="chk_sala" runat="server" Text="Sala" />
                    </div>

                    <!-- AparatosElectrodomesticosComedor-->
                    <div class="col-lg-2 col-md-1 col-sm-12 col-xs-12">
                        <asp:CheckBox ID="chk_comedor" runat="server" Text="Comedor" />
                    </div>

                    <!-- AparatosElectrodomesticosEstufa-->
                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                        <asp:CheckBox ID="chk_estufa" runat="server" Text="Estufa" />
                    </div>

                    <!-- AparatosElectrodomesticosRefrigerador-->
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <asp:CheckBox ID="chk_refri" runat="server" Text="Refrigerador" />
                    </div>

                    <!--AparatosElectrodomesticosLavadora-->
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <asp:CheckBox ID="chk_lavadora" runat="server" Text="Lavadora" />
                    </div>

                    <!--AparatosElectrodomesticosTelevision-->
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <asp:CheckBox ID="chk_tele" runat="server" Text="Televisión" />
                    </div>

                    <!--AparatosElectrodomesticosComputadora-->
                    <div class="col-lg-2 col-md-1 col-sm-12 col-xs-12">
                        <asp:CheckBox ID="chk_compu" runat="server" Text="Computadora" />
                    </div>

                </div>
            </div>

            <div>
                <!--OtrosBienes-->

                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <label id="lblOtrosBienes" class="text-left textoBold col-lg-12 col-md-12 col-sm-12 col-xs-12">Otros Bienes</label>
                    </div>
                </div>


                <!--OtrosBienesAuto-->
                <div class="row pad1m">


                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label28" runat="server" Text="Auto:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:DropDownList ID="cmb_auto" runat="server" OnSelectedIndexChanged="cmb_autoIndexChanged" AutoPostBack="true">
                            <asp:ListItem>Si</asp:ListItem>
                            <asp:ListItem>No</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>


                <div class="row pad1m">
                    <!-- OtrosBienesAutoMarca-->
                    <div class="col-lg-1 col-sm-1 text-left">
                        <asp:Label ID="Label29" runat="server" Text="Marca:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-left">
                        <asp:TextBox ID="txt_marca" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Marca"></asp:TextBox>
                    </div>


                    <!-- OtrosBienesAutoModelo-->
                    <div class="col-lg-1 col-sm-1 text-left">
                        <asp:Label ID="Label30" runat="server" Text="Modelo:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-left">
                        <asp:TextBox ID="txt_modelo" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Modelo"></asp:TextBox>
                    </div>

                    <!-- OtrosBienesAutoPlacas-->
                    <div class="col-lg-1 col-sm-1 text-left">
                        <asp:Label ID="Label31" runat="server" Text="Placas:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-left">
                        <asp:TextBox ID="txt_placas" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Placas"></asp:TextBox>
                    </div>

                </div>

            </div>

                            </Content>
                           </cc1:AccordionPane>
                          <cc1:AccordionPane ID="AccordionPane3" runat="server" CssClass="ancho95" Visible="true">
                                 <Header>Datos generales del negocio</Header>
                            <Content>
                                <div class="row pad1m">
                <!-- DomicilioDelNegocio-->
               
                    <!--CalleDomicilioDelNegocio--> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label32" runat="server" Text="Calle:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_calle" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Calle"></asp:TextBox>
                    </div>
                   
                    <!-- NumExtDomicilioDelNegocio--> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label33" runat="server" Text="Número Exterior:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_next_neg" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Número Exterior"></asp:TextBox>
                    </div>

                    </div>

                <div class="row pad1m">
                    <!-- NumIntDomicilioDelNegocio--> 
                     <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label34" runat="server" Text="Número Interior:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_nint_neg" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Número Interior"></asp:TextBox>
                    </div>

                    <!-- ColoniaDomicilioDelNegocio--> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label35" runat="server" Text="Colonia:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_col_neg" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Colonia"></asp:TextBox>
                    </div>

                    </div>

                <div class="row pad1m">

                    <!-- CodigoPostalDomicilioDelNegocio--> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label36" runat="server" Text="Código Postal:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_cp_neg" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="C.P."></asp:TextBox>
                    </div>


                     <!-- MunicipioDelegacionDomicilioDelNegocio--> 
                     <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label37" runat="server" Text="Municipio o Delegación:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_mundel_neg" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Municipio o Delegación"></asp:TextBox>
                    </div>

                </div>

                <div class="row pad1m">

                     <!-- EstadoDomicilioDelNegocio--> 
                   <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label38" runat="server" Text="Estado:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_estado_neg" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Estado"></asp:TextBox>
                    </div>

                     <!-- EntreCallesDomicilioDelNegocio--> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label39" runat="server" Text="Entre Calles:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_entre_neg" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Entre Calles"></asp:TextBox>
                    </div>


              </div>

                <div class="row pad1m">
                     <!-- TelefonoDomicilioDelNegocio--> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label40" runat="server" Text="Teléfono:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_tel_neg" runat="server" CssClass="alingMiddle input-large" MaxLength="18"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" BehaviorID="txt_tel_negTextBoxWatermarkExtender" TargetControlID="txt_tel_neg" WatermarkText="Teléfono" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_neg" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  runat="server" ErrorMessage="Debe indicar la fecha" Text="*" ValidationGroup="crea" ControlToValidate="txt_tel_neg" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>

                <!--CaracterísticasDelLocal-->
               
                     
                    <div class="col-lg-2 col-sm-2 text-left">
                                             <asp:Label ID="Label41" runat="server" Text="Caracteristicas del Local:" CssClass="alingMiddle textoBold"></asp:Label>
                                         </div>
                                         <div class="col-lg-4 col-sm-4 text-left">
                                             <asp:DropDownList ID="cmb_caracte" runat="server">
                                                 <asp:ListItem value="Pro">Propio</asp:ListItem>
                                                 <asp:ListItem value="Ren">Rentado</asp:ListItem>
                                                 <asp:ListItem value="Fij">Fijo</asp:ListItem>
                                                 <asp:ListItem value="Sem">Semifijo</asp:ListItem> 
                                             </asp:DropDownList>
                                         </div>
                                     
                </div> 
           
                <div class="row pad1m">
                    <!-- CaracteristicasDelLocaltiempoConElNegocio--> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label42" runat="server" Text="Tiempo con el Negocio:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_tiempo_neg" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Tiempo Negocio"></asp:TextBox>
                    </div>

                    <!-- CaracteristicasDelLocalRazonSocial--> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label43" runat="server" Text="Razón Social:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_razon_neg" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Razón Social"></asp:TextBox>
                    </div>
                </div>

                <div class="row pad1m">
                    <!-- CaracteristicasDelLocalGiroDelNegocio--> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label44" runat="server" Text="Giro del Negocio:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_giro_neg" runat="server"  ReadOnly="true"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Giro del Negocio"></asp:TextBox>
                    </div>

                    <!-- CaracteristicasDelLocalPrincipalesProveedores--> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label45" runat="server" Text="Principales Proveedores:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_principales" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Principales Proveedores"></asp:TextBox>
                    </div>

                </div>

               
                            </Content>
                          </cc1:AccordionPane>
                          <cc1:AccordionPane ID="AccordionPane4" runat="server" CssClass="ancho95" Visible="true">
                               <Header>Garantías Personales</Header>
                            <Content>
                                <div class="row pad1m">
                <!-- ObligadoSolidario-->
                <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label46" runat="server" Text="Garantía:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:DropDownList ID="cmb_garan" runat="server" OnSelectedIndexChanged="cmb_garan_SelectedIndexChanged" AutoPostBack="true"> 
                            <asp:ListItem Value="Sin">Seleccione Garantia</asp:ListItem>
                            <asp:ListItem Value="O.S">Obligado Solidario</asp:ListItem>
                            <asp:ListItem Value="Ava">Aval</asp:ListItem>
                             <asp:ListItem Value="N/A">No Aplica</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            <div class="row pad1m">
               
                    <!--NombreObligadoSolidario--> 
                   <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label47" runat="server" Text="Nombre:" CssClass="alingMiddle textoBold" AutoPostBack="true"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_nom_gara" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Nombre"></asp:TextBox>
                    </div>
                    
                   
                    <!-- ApellidoPaternoObligadoSolidario--> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label48" runat="server" Text="Apellido Paterno:" CssClass="alingMiddle textoBold" AutoPostBack="true"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_apat_gara" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Apellido Paterno"></asp:TextBox>
                    </div>
                </div>

            <div class="row pad1m">
                    <!-- ApellidoMaternoObligadoSolidario--> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label49" runat="server" Text="Apellido Materno:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_amat_gara" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Apellido Materno"></asp:TextBox>
                    </div>

                    <!--FechaDeNacimientoObligadoSolidario--> 
                   <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label50" runat="server" Text="Fecha Nacimiento :" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadDatePicker ID="txt_nac_gara" runat="server">
                            <DateInput runat="server" DateFormat="yyyy/MM/dd">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7"  runat="server" ErrorMessage="Debe indicar la fecha" Text="*" ValidationGroup="crea" ControlToValidate="txt_nac_gara" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                    </div>

            <div class="row pad1m">

                    <!-- EdadObligadoSolidario--> 
                     <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label51" runat="server" Text="Edad:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_edad_gara" runat="server" CssClass="alingMiddle input-large" MaxLength="2"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" BehaviorID="txt_edad_garaTextBoxWatermarkExtender" TargetControlID="txt_edad_gara" WatermarkText="Edad" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_edad_gara" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8"  runat="server" ErrorMessage="Debe indicar la fecha" Text="*" ValidationGroup="crea" ControlToValidate="txt_edad_gara" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>

                    <!-- SexoObligadoSolidario--> 
                     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label52" runat="server" Text="Sexo:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:DropDownList ID="cmb_gen_gara" runat="server">
                            <asp:ListItem Value="H">Hombre</asp:ListItem>
                            <asp:ListItem Value="M">Mujer</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    </div>

            <div class="row pad1m">

                    <!-- OcupacionObligadoSolidario--> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label53" runat="server" Text="Ocupación:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_ocup_gara" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Ocupación"></asp:TextBox>
                    </div>

                    <!-- CuentaConBienesInmueblesObligadoSolidario--> 
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label54" runat="server" Text="¿Cuenta con bienes inmuebles?:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:DropDownList ID="cmb_bienes_gara" runat="server" OnSelectedIndexChanged="cmb_bienes_garaIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="S">Si</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    </div>

            <div class="row pad1m">

                    <!--Aval --> 
                     <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label55" runat="server" Text="Valor estimado de los Bienes" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                         <asp:TextBox ID="txt_valor_gara" runat="server" CssClass="alingMiddle input-large" MaxLength="15"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" BehaviorID="txt_valor_garaTextBoxWatermarkExtender" TargetControlID="txt_valor_gara" WatermarkText="Valor " WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_valor_gara" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9"  runat="server" ErrorMessage="Debe indicar la fecha" Text="*" ValidationGroup="crea" ControlToValidate="txt_valor_gara" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>

                     <!--CalleAval --> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label56" runat="server" Text="Calle:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_calle_gara" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Calle"></asp:TextBox>
                    </div>

            </div>

            <div class="row pad1m">

                     <!--NumExtAval --> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label57" runat="server" Text="Número Exterior:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_num_ext_gara" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Número Exterior"></asp:TextBox>
                    </div>

                     <!--NumIntAval --> 
                   <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label58" runat="server" Text="Número Interior:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_num_int_gara" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Número Interior"></asp:TextBox>
                    </div>

            </div>

            <div class="row pad1m">

                     <!--ColoniaAval --> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label59" runat="server" Text="Colonia:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_col_gara" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Colonia"></asp:TextBox>
                    </div>
                 <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label60" runat="server" Text="Código Postal:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_cp_gara" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="C.P."></asp:TextBox>
                    </div>

                    

            </div>

            <div class="row pad1m">

                 <!--DelegacionMunicipioAval --> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label61" runat="server" Text="Delegación o Municipio:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_del_gara" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Delegación o Municipio"></asp:TextBox>
                    </div>

                     <!--EstadoAval --> 
                   <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label62" runat="server" Text="Estado:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_estado_gara" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Estado"></asp:TextBox>
                    </div>

                    
                    
                </div>

            <div class="row pad1m">
                <!--EntreLasCallesAval --> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label63" runat="server" Text="Entre Calles:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_entre_gara" runat="server"
                            CssClass="alingMiddle input-large" MaxLength="100"
                            PlaceHolder="Entre Calles"></asp:TextBox>
                    </div>

                    <!--TelefonoAval --> 
                    <div class="col-lg-2 col-sm-1 text-left">
                        <asp:Label ID="Label64" runat="server" Text="Teléfono:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-3 text-left">
                        <asp:TextBox ID="txt_tel_gara" runat="server" CssClass="alingMiddle input-large" MaxLength="18"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" BehaviorID="txt_tel_garaTextBoxWatermarkExtender" TargetControlID="txt_tel_gara" WatermarkText="Telefono" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_gara" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11"  runat="server" ErrorMessage="Debe indicar el Teléfono" Text="*" ValidationGroup="crea" ControlToValidate="txt_tel_gara" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                 <div class="row text-center">
                    <asp:LinkButton ID="lnkAgregaSolicitud" runat="server" ValidationGroup="crea" ToolTip="Guarda Solicitud" OnClick="agregaVisita" CssClass="btn btn-success t14"  ><i class="fa fa-save"></i>&nbsp;<span>Guarda Visita</span></asp:LinkButton>
                </div>
                            </Content>
                          </cc1:AccordionPane>
                      </Panes>
                 </cc1:Accordion>

                  <div class="row marTop">
                    <asp:Label ID="lblEditaAgrega" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblIdConsultaEdita" runat="server" Visible="false" ></asp:Label>
                </div>  
                <br />
               
                <div class="row text-center">
                    <asp:Label ID="lblErrorAgrega" runat="server" Visible="false" CssClass=""></asp:Label>
                </div>
                </div>
                
</div>   

            </asp:Panel>
                    </ContentTemplate>

               </asp:UpdatePanel>
</asp:Content>

