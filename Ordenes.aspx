﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ordenes.aspx.cs" Inherits="Ordenes" EnableEventValidation="false" MasterPageFile="~/AdmOrdenes.master" Culture="es-Mx" UICulture="es-Mx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
      <script type="text/javascript">
          function OnClientSelectedM(sender, eventArgs) {
            var cboMarca = sender;
            var cboVehic = $find("<%= ddlNuevoTv.ClientID %>");
              if (cboVehic.get_value() != "")
                  __doPostBack("ddlNuevaMarca", '{\"Command\" : \"Select\"}');
          }

          function OnClientSelectedV(sender, eventArgs) {
            var cboVehic = sender;
            var cboMarca = $find("<%= ddlNuevaMarca.ClientID %>");
              if (cboMarca.get_value() != "")
                  __doPostBack("ddlNuevoTv", '{\"Command\" : \"Select\"}');
        }
    </script>  
    <telerik:RadScriptManager ID="RadScriptManajer1" runat="server" EnableScriptGlobalization="true"></telerik:RadScriptManager>
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server"></telerik:RadSkinManager>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="page-header">
                <!-- /BREADCRUMBS -->
                <div class="clearfix">
                    <h3 class="content-title pull-left">Créditos</h3>
                    <asp:Label ID="lblError" runat="server" CssClass="alert-danger"></asp:Label>
                </div>
            </div>

             <div class="col-lg-6 col-sm-6">
                    <asp:LinkButton ID="lnkAbrirWindow"  Visible="false" runat="server" OnClick="lnkAbrirWindow_Click" CssClass="btn btn-info t14"  ><i class="fa fa-retweet"></i>&nbsp;<span>Regresa a Solicitud</span></asp:LinkButton>
                </div>

            <asp:Panel ID="Panel4" runat="server" CssClass="panelCatalogos text-center" ScrollBars="Auto">
                <div class="ancho100 textoDerecha">
                    <asp:Label ID="Label13" runat="server" Text="Estatus" CssClass="textoBold"></asp:Label>
                    <asp:DropDownList ID="ddlEstatus" runat="server" AppendDataBoundItems="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlEstatus_SelectedIndexChanged">
                        <asp:ListItem Value="0">Todos</asp:ListItem>
                        <asp:ListItem Selected="True" Value="A">Ciclo 1</asp:ListItem>
                        <asp:ListItem Value="T">Ciclo 2</asp:ListItem>
                        <asp:ListItem Value="R">Ciclo 3</asp:ListItem>
                        <asp:ListItem Value="F">Ciclo 4</asp:ListItem>
                        <asp:ListItem Value="C">Ciclo 5</asp:ListItem>
                        <asp:ListItem Value="S">Cerrado</asp:ListItem>
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkNuevaRecepcion" runat="server" Visible="false" ToolTip="Agrega Crédito" CssClass="btn btn-info alingMiddle" OnClick="lnkNuevaRecepcion_Click"><i class="fa fa-plus-circle t18"></i>&nbsp;<span>Nuevo Cr&eacute;dito</span></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkRefressh" runat="server" ToolTip="Refrescar Vista" CssClass="btn btn-info alingMiddle" OnClick="lnkRefressh_Click"><i class="fa fa-refresh t18"></i>&nbsp;<span>Actualizar</span></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                    
                    <asp:TextBox ID="txtFiltro" runat="server" CssClass="input-medium alingMiddle" Visible="false"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtFiltro_TextBoxWatermarkExtender" runat="server" BehaviorID="txtFiltro_TextBoxWatermarkExtender" TargetControlID="txtFiltro" WatermarkCssClass="water input-medium" WatermarkText="Buscar..." />
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lnkBuscar" Visible="false" runat="server" ToolTip="Buscar" CssClass="btn btn-info alingMiddle"><i class="fa fa-search t18"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkLimpiar" runat="server" Visible="false" ToolTip="Limpiar Búsqueda" CssClass="btn btn-info alingMiddle" OnClick="lnkLimpiar_Click"><i class="fa fa-eraser t18"></i>&nbsp;<span>Limpiar B&uacute;squeda</span></asp:LinkButton>
                </div>
                <br />

                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" EnableAJAX="true">
                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" AllowFilteringByColumn="true" runat="server"  EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue" OnItemDataBound="RadGrid1_ItemDataBound"
                        EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource1" AllowSorting="true" GroupingEnabled="false" PageSize="50" >                        
                        <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="id_solicitud_credito">
                            <Columns>
                            <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" HeaderText="Crédito"  SortExpression="id_solicitud_credito" UniqueName="id_solicitud_credito" FilterControlAltText="Filtro Crédito" DataField="id_solicitud_credito">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnOrden" CssClass="btn btn-info" runat="server"  CommandArgument='<%# Bind("id_solicitud_credito") %>' OnClick="btnOrden_Click" Text='<%# Bind("id_solicitud_credito") %>' ></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>                                
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="grupo_productivo" FilterControlAltText="Filtro Grupo" HeaderText="Grupo Productivo" SortExpression="grupo_productivo" UniqueName="grupo_productivo"  Resizable="true"/>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="id_grupo" FilterControlAltText="Filtro Grupo" HeaderText="Grupo" SortExpression="id_grupo" UniqueName="id_grupo" Resizable="true"/>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="monto_autorizado" FilterControlAltText="Filtro Monto" HeaderText="Monto"  SortExpression="monto_autorizado" UniqueName="monto_autorizado" Resizable="true" DataFormatString="{0:C}" />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="plazo" FilterControlAltText="Filtro Plazo" HeaderText="Plazo" SortExpression="plazo" UniqueName="plazo" Resizable="true"/>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="ciclo" FilterControlAltText="Filtro ciclo" HeaderText="Ciclo" SortExpression="ciclo" UniqueName="ciclo" Resizable="true"/>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="fecha_autorizacion" FilterControlAltText="Filtro Fecha" HeaderText="Fecha Autorizacion" SortExpression="fecha_autorizacion" visible="true" UniqueName="fecha_autorizacion" Resizable="true" DataFormatString="{0:yyyy-MM-dd}" />
                              
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                        </ClientSettings>                                                
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
                <div class="ancho100 text-center marTop">
                   <div class="page-header">
                <!-- /BREADCRUMBS -->
                
                    <h3 class="content-title pull-left">Estatus</h3>
                    <asp:Label ID="Label14" runat="server" CssClass="alert-danger"></asp:Label>
                
            </div>
                </div>
                <div class="ancho100 text-center marTop">    
                     <div class="col-lg-2 col-sm-2"><asp:LinkButton ID="LinkButton1"  Visible="true" runat="server" OnClick="PresolicitadoClick" CssClass="btn btn-group-justified btn-success"  ><i></i>&nbsp;<span>Presolicitado</span></asp:LinkButton> </div>
                    <div class="col-lg-2 col-sm-2"><asp:LinkButton ID="LinkButton2"  Visible="true" runat="server" OnClick="SolicitadoClick" CssClass="btn btn-group-justified btn-warning"  ><i></i>&nbsp;<span>Solicitado</span></asp:LinkButton> </div>        
                    <div class="col-lg-3 col-sm-3"><asp:LinkButton ID="LinkButton3"  Visible="true" runat="server" OnClick="AprobadoClick" CssClass="btn btn-group-justified btn-grey"  ><i></i>&nbsp;<span>Aprobado</span></asp:LinkButton> </div>       
                    <div class="col-lg-2 col-sm-2"><asp:LinkButton ID="LinkButton4"  Visible="true" runat="server" OnClick="DesembolsadoClick" CssClass="btn btn-group-justified btn-info"  ><i></i>&nbsp;<span>Desembolsado</span></asp:LinkButton> </div>        
                    <div class="col-lg-3 col-sm-3"><asp:LinkButton ID="LinkButton5"  Visible="true" runat="server" OnClick="CanceladoClick" CssClass=" btn btn-group-justified btn-danger"  ><i></i>&nbsp;<span>No Aprobado</span></asp:LinkButton> </div>       
              </div>  
              <br /><br />
                <br />
                <br /><br />

                  <br /><br />
                <br />
                <br /><br />  <br /><br />
                <br />
                <br /><br /><br />
                <br /><br />
                <asp:SqlDataSource ID="SqlDataSource1" SelectCommand="select * from an_creditos " runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"/>                
            </asp:Panel>
            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                <ProgressTemplate>
                    <asp:Panel ID="pnlMaskLoad1" runat="server" CssClass="maskLoad"></asp:Panel>
                    <asp:Panel ID="pnlCargando1" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoad1" runat="server" ImageUrl="~/img/loading.gif" CssClass="center" />
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMask" runat="server" CssClass="mask zen1" Visible="false" />
            <asp:Panel ID="pnlNuevaRecepcion" runat="server" CssClass="popUp zen2 textoCentrado ancho80" Visible="false">
                <table class="ancho100">
                    <tr class="ancho100 centrado  ">
                        <td class="ancho95 text-center encabezadoTabla roundTopLeft">
                            <asp:Label ID="Label98" runat="server" Text="Nuevo Crédito" CssClass="t22 colorMorado textoBold" />
                        </td>
                        <td class="ancho5 text-right encabezadoTabla roundTopRight">
                            <asp:LinkButton ID="lnkCerrar" runat="server" CssClass="btn btn-danger alingMiddle" OnClick="lnkCerrar_Click" ToolTip="Cerrar"><i class="fa fa-remove t18"></i></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <div class="row text-center pad1m">
                    <div class="col-lg-12 col-sm-12 text-center">
                        <asp:Label ID="Label1" runat="server" Text="Número:" CssClass="textoBold marTop9px alingMiddle"></asp:Label>
                        <asp:TextBox ID="txtPlacaNueva" runat="server" MaxLength="12" CssClass="input-medium" OnTextChanged="txtPlacaNueva_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtPlacaNueva_TextBoxWatermarkExtender" runat="server" BehaviorID="txtPlacaNueva_TextBoxWatermarkExtender" TargetControlID="txtPlacaNueva" WatermarkText="Número" WatermarkCssClass="water input-medium" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe indicar el número del crédito" ControlToValidate="txtPlacaNueva" ValidationGroup="placa" CssClass="alert-danger errores alingMiddle"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblErrorRecepcion" runat="server" CssClass="alert-danger errores alingMiddle"></asp:Label>
                    </div>
                </div>
                <cc1:Accordion ID="acNuevaRecep" runat="server" CssClass="ancho95 centrado" FadeTransitions="true" HeaderCssClass="encabezadoAcordeonPanel" HeaderSelectedCssClass="encabezadoAcordeonPanelSelect">
                    <Panes>
                        <cc1:AccordionPane ID="acpVehiculo" runat="server" CssClass="ancho95" Visible="false" Style="cursor: pointer;">
                            <Header>Datos del Grupo</Header>
                            <Content>
                                <div class="row pad1m">
                                    <div class="col-lg-2 col-sm-2 text-left">
                                        <asp:Label ID="Label2" runat="server" Text="Número de Grupo:" CssClass="alingMiddle textoBold"></asp:Label></div>
                                    <div class="col-lg-4 col-sm-4 text-left">
                                        <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="ddlNuevaMarca" AllowCustomText="true" CssClass="input-xlarge" Width="300px" MaxHeight="300px" DataSourceID="SqlDataSource2" Skin="MetroTouch" DataTextField="marca" DataValueField="id_marca" EmptyMessage="Seleccione Marca" Filter="Contains" AutoPostBack="false" OnSelectedIndexChanged="ddlNuevaMarca_SelectedIndexChanged" OnClientSelectedIndexChanged="OnClientSelectedM"></telerik:RadComboBox>                                        
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_marca, descripcion as marca from marcas"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe seleccionar una marca" Text="*" CssClass="errores alingMiddle " ValidationGroup="altav" ControlToValidate="ddlNuevaMarca"></asp:RequiredFieldValidator>
                                        <asp:LinkButton ID="lnkAgregaMarca" runat="server" CssClass="btn btn-info t14 colorBlanco" ToolTip="Agregar Marca" onclick="lnkAgregaMarca_Click"><i class="fa fa-plus t18"></i></asp:LinkButton>
                                    </div>
                                    <div class="col-lg-2 col-sm-2 text-left">
                                        <asp:Label ID="Label3" runat="server" Text="Grupo Productivo:" CssClass="textoBold alingMiddle"></asp:Label></div>
                                    <div class="col-lg-4 col-sm-4 text-left">
                                        <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="ddlNuevoTv" AllowCustomText="true" CssClass="input-xlarge" Width="300px" MaxHeight="300px" DataSourceID="SqlDataSource3" DataTextField="descripcion" DataValueField="id_tipo_vehiculo" Skin="MetroTouch" EmptyMessage="Seleccione Vehículo" Filter="Contains" AutoPostBack="false" OnSelectedIndexChanged="ddlNuevoTv_SelectedIndexChanged" OnClientSelectedIndexChanged="OnClientSelectedV"></telerik:RadComboBox>                                        
                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_tipo_vehiculo, UPPER(descripcion) AS descripcion from Tipo_Vehiculo"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Debe seleccionar un tipo de vehículo" Text="*" CssClass="errores alingMiddle" ValidationGroup="altav" ControlToValidate="ddlNuevoTv"></asp:RequiredFieldValidator>
                                        <asp:LinkButton ID="lnkAgregaTv" runat="server" CssClass="btn btn-info t14 colorBlanco" ToolTip="Agregar Tipo de Vehículo" onclick="lnkAgregaTv_Click"><i class="fa fa-plus t18"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row pad1m">
                                    <div class="col-lg-2 col-sm-2 text-left">
                                        <asp:Label ID="Label4" runat="server" Text="Sucursal:" CssClass="alingMiddle textoBold"></asp:Label></div>
                                    <div class="col-lg-4 col-sm-4 text-left">
                                        <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="ddlNuevaUnidad" AllowCustomText="true" CssClass="input-xlarge" Width="300px" MaxHeight="300px" DataSourceID="SqlDataSource4" DataTextField="descripcion" DataValueField="id_tipo_unidad" Skin="MetroTouch" EmptyMessage="Seleccione Línea" Filter="Contains" ></telerik:RadComboBox>                                                                                
                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_tipo_unidad, upper(descripcion) as descripcion from Tipo_Unidad where id_marca=@id_marca and id_tipo_vehiculo=@id_tipo_vehiculo">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlNuevaMarca" DefaultValue="0" Type="Int32" Name="id_marca" PropertyName="SelectedValue" />
                                                <asp:ControlParameter ControlID="ddlNuevoTv" DefaultValue="0" Type="Int32" Name="id_tipo_vehiculo" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Debe seleccionar una línea" Text="*" CssClass="errores alingMiddle" ValidationGroup="altav" ControlToValidate="ddlNuevaUnidad"></asp:RequiredFieldValidator>
                                        <asp:LinkButton ID="lnkAgregaUnidad" runat="server" CssClass="btn btn-info t14 colorBlanco" ToolTip="Agregar Línea" onclick="lnkAgregaUnidad_Click"><i class="fa fa-plus t18"></i></asp:LinkButton>
                                    </div>
                                    <div class="col-lg-2 col-sm-2 text-left">
                                        <asp:Label ID="Label5" runat="server" Text="Monto:" CssClass="alingMiddle textoBold"></asp:Label></div>
                                    <div class="col-lg-4 col-sm-4 text-left">
                                        <asp:TextBox ID="txtNuevoMod" runat="server" MaxLength="4" CssClass="input-small alingMiddle" SkinID="MetroTouch"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="txtNuevoModWatermarkExtender" runat="server" BehaviorID="txtNuevoMod_TextBoxWatermarkExtender" TargetControlID="txtNuevoMod" WatermarkText="Modelo" WatermarkCssClass="water input-small" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtNuevoMod" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Debe indicar el monto" Text="*" CssClass="errores alingMiddle" ValidationGroup="altav" ControlToValidate="txtNuevoMod"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row pad1m">
                                    <div class="col-lg-2 col-sm-2 text-left">
                                        <asp:Label ID="Label6" runat="server" Text="Fecha de Solicitud:" CssClass="alingMiddle textoBold"></asp:Label></div>
                                    <div class="col-lg-4 col-sm-4 text-left">
                                        <asp:TextBox ID="txtColorNuevo" runat="server" MaxLength="20" CssClass="input-medium alingMiddle" SkinID="MetroTouch"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="txtColorNuevoWatermarkExtender1" runat="server" BehaviorID="txtColorNuevo_TextBoxWatermarkExtender" TargetControlID="txtColorNuevo" WatermarkText="Color Exterior" WatermarkCssClass="water input-medium" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Debe indicar la fecha de solicitud" Text="*" CssClass="errores alingMiddle " ValidationGroup="altav" ControlToValidate="txtColorNuevo"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="acpOrden" runat="server" CssClass="ancho95" Visible="false">
                            <Header>Datos del Cr&eacute;dito</Header>
                            <Content>
                                <div class="row pad1m">
                                    <div class="col-lg-2 col-sm-2 text-left">
                                        <asp:Label ID="Label7" runat="server" Text="Monto Máximo:" CssClass="alingMiddle textoBold"></asp:Label></div>
                                    <div class="col-lg-4 col-sm-4 text-left">
                                        <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="ddlTipoOrdenNuevo" AllowCustomText="true" CssClass="input-large" Width="300px" MaxHeight="300px" DataSourceID="SqlDataSource5" DataTextField="descripcion" DataValueField="id_tipo_orden" Skin="MetroTouch" EmptyMessage="Seleccione Tipo Orden" Filter="Contains" ></telerik:RadComboBox>                                        
                                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_tipo_orden,descripcion from Tipo_Orden"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Debe seleccionar el monto máximo" Text="*" CssClass="errores alingMiddle" ValidationGroup="altav" ControlToValidate="ddlTipoOrdenNuevo"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-2 col-sm-2 text-left">
                                        <asp:Label ID="Label8" runat="server" Text="Monto Autorizado:" CssClass="alingMiddle textoBold"></asp:Label></div>
                                    <div class="col-lg-4 col-sm-4 text-left">
                                        <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="ddlClienteNuevo" AllowCustomText="true" CssClass="input-large" Width="300px" MaxHeight="300px" DataSourceID="SqlDataSource6" DataTextField="razon_social" DataValueField="id_cliprov" Skin="MetroTouch" EmptyMessage="Seleccione Cliente" Filter="Contains" ></telerik:RadComboBox>                                        
                                        <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_cliprov,razon_social from Cliprov where tipo='C' and estatus='A'"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Debe seleccionar el monto autorizado" Text="*" CssClass="errores alingMiddle" ValidationGroup="altav" ControlToValidate="ddlClienteNuevo"></asp:RequiredFieldValidator>
                                        <asp:LinkButton ID="lnkClienteNuevo" runat="server" CssClass="btn btn-info t14 colorBlanco" ToolTip="Agregar Cliente" onclick="lnkClienteNuevo_Click"><i class="fa fa-plus t18"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row pad1m">
                                    <div class="col-lg-2 col-sm-2 text-left">
                                        <asp:Label ID="Label9" runat="server" Text="Plazo:" CssClass="alingMiddle textoBold"></asp:Label></div>
                                    <div class="col-lg-4 col-sm-4 text-left">
                                        <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="ddlTipoServNuevo" AllowCustomText="true" CssClass="input-large" Width="300px" MaxHeight="300px" DataSourceID="SqlDataSource7" DataTextField="descripcion" DataValueField="id_tipo_servicio" Skin="MetroTouch" EmptyMessage="Seleccione Tipo Servicio" Filter="Contains" ></telerik:RadComboBox>                                        
                                        <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_tipo_servicio, descripcion from Tipo_Servicios"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Debe seleccionar el tipo de servicio" Text="*" CssClass="errores alingMiddle" ValidationGroup="altav" ControlToValidate="ddlTipoServNuevo"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-2 col-sm-2 text-left">
                                        <asp:Label ID="Label10" runat="server" Text="Tasa:" CssClass="alingMiddle textoBold"></asp:Label></div>
                                    <div class="col-lg-4 col-sm-4 text-left">
                                        <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="ddlTipoValNuevo" AllowCustomText="true" CssClass="input-large" Width="300px" MaxHeight="300px" DataSourceID="SqlDataSource8" DataTextField="descripcion" DataValueField="id_tipo_valuacion" Skin="MetroTouch" EmptyMessage="Seleccione Tipo Valuación" Filter="Contains" ></telerik:RadComboBox>                                                                                
                                        <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_tipo_valuacion, descripcion from Tipo_Valuacion"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Debe seleccionar el tipo de valuación" Text="*" CssClass="errores alingMiddle" ValidationGroup="altav" ControlToValidate="ddlTipoValNuevo"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row pad1m">
                                    <div class="col-lg-2 col-sm-2 text-left">
                                        <asp:Label ID="Label11" runat="server" Text="Garantía Líquida:" CssClass="alingMiddle textoBold"></asp:Label></div>
                                    <div class="col-lg-4 col-sm-4 text-left">
                                        <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="ddlTipoAseguradoNuevo" AllowCustomText="true" CssClass="input-large" Width="300px" MaxHeight="300px" DataSourceID="SqlDataSource9" DataTextField="descripcion" DataValueField="id_tipo_asegurado" Skin="MetroTouch" EmptyMessage="Seleccione Tipo Asegurado" Filter="Contains" ></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_tipo_asegurado,descripcion from Tipo_Asegurados"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Debe seleccionar el tipo de asegurado" Text="*" CssClass="errores alingMiddle" ValidationGroup="altav" ControlToValidate="ddlTipoAseguradoNuevo"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-2 col-sm-2 text-left">
                                        <asp:Label ID="Label15" runat="server" Text="Fecha de Entrega:" CssClass="alingMiddle textoBold"></asp:Label></div>
                                    <div class="col-lg-4 col-sm-4 text-left">
                                        <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="ddlPerfilOrdenNuevo" AllowCustomText="true" CssClass="input-large" Width="300px" MaxHeight="300px" DataSourceID="SqlDataSource13" DataTextField="descripcion" DataValueField="id_perfilOrden" Skin="MetroTouch" EmptyMessage="Seleccione Perfil" Filter="Contains" ></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlDataSource13" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_perfilOrden, descripcion from PerfilesOrdenes"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Debe seleccionar un perfil" Text="*" CssClass="errores alingMiddle" ValidationGroup="altav" ControlToValidate="ddlPerfilOrdenNuevo"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row pad1m">
                                    <div class="col-lg-2 col-sm-2 text-left">
                                        <asp:Label ID="Label12" runat="server" Text="Ciclo:" CssClass="alingMiddle textoBold"></asp:Label></div>
                                    <div class="col-lg-4 col-sm-4 text-left">
                                        <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="ddlLocalizacionNuevo" AllowCustomText="true" CssClass="input-large" Width="300px" MaxHeight="300px" DataSourceID="SqlDataSource10" DataTextField="descripcion" DataValueField="id_localizacion" Skin="MetroTouch" EmptyMessage="Seleccione Localización" Filter="Contains" ></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="SqlDataSource10" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_localizacion, descripcion from Localizaciones"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Debe seleccionar una localizacion" Text="*" CssClass="errores alingMiddle" ValidationGroup="altav" ControlToValidate="ddlLocalizacionNuevo"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="acpOrdPrevias" runat="server" CssClass="ancho95" Visible="false">
                            <Header>Ordenes Previas</Header>
                            <Content>
                                <div class="row pad1m">
                                    <div class="col-lg-6 col-sm-6 text-center">
                                        <asp:Panel ID="pnlOrdPrevi" runat="server" CssClass="col-lg-12 col-sm-12 table-responsive">
                                            <asp:GridView ID="grvOrdPrev" runat="server" CssClass="table table-bordered" PageSize="6" EmptyDataRowStyle-CssClass="errores"
                                                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="no_orden"
                                                DataSourceID="SqlDataSource11" EmptyDataText="No existen órdenes previas registradas">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Orden" SortExpression="no_orden">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkOrdenPrevia" runat="server" Text='<%# Bind("no_orden") %>' CssClass="btn btn-info" OnClick="lnkOrdenPrevia_Click" CommandArgument='<%# Eval("no_orden") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="f_recepcion" HeaderText="Fecha Recepción" SortExpression="f_recepcion" />
                                                    <asp:BoundField DataField="h_recepcion" HeaderText="Hora Recepción" SortExpression="h_recepcion" />
                                                    <asp:BoundField DataField="f_entrega" HeaderText="Fecha Entrega" SortExpression="f_entrega" />
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="errores" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource11" runat="server" SelectCommand="select ord.no_orden,convert(char(10),so.f_recepcion,103) as f_recepcion, convert(char(8),so.h_recepcion,108) as h_recepcion,convert(char(10),so.f_entrega,103) as f_entrega from Ordenes_Reparacion ord left join Seguimiento_Orden so on so.id_empresa=ord.id_empresa and so.id_taller=ord.id_taller and so.no_orden=ord.no_orden where ord.placas=@placas and ord.id_empresa=@id_empresa and ord.id_taller=@id_taller order by ord.no_orden desc" ConnectionString="<%$ ConnectionStrings:Taller %>">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="txtPlacaNueva" Name="placas" PropertyName="Text" Type="String" />
                                                    <asp:QueryStringParameter QueryStringField="e" Name="id_empresa" Type="Int32" />
                                                    <asp:QueryStringParameter QueryStringField="t" Name="id_taller" Type="Int32" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-lg-6 col-sm-6 text-center">
                                        <asp:Panel ID="Panel2" runat="server" CssClass="col-lg-12 col-sm-12 table-responsive panelesOrdn" ScrollBars="Auto">
                                            <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered"
                                                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="id_grupo_op" EmptyDataRowStyle-CssClass="errores"
                                                EmptyDataText="No existen operaciones registradas"
                                                DataSourceID="SqlDataSource12">
                                                <Columns>
                                                    <asp:BoundField DataField="id_grupo_op" HeaderText="id_grupo_op" SortExpression="id_grupo_op" Visible="False" />
                                                    <asp:TemplateField HeaderText="Grupo" SortExpression="Grupo">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkGrupo" runat="server" Text='<%# Bind("Grupo") %>' CssClass="btn btn-info" CommandArgument='<%# Eval("no_orden")+";"+Eval("id_grupo_op")+";"+Eval("Grupo") %>' OnClick="lnkGrupo_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource12" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"></asp:SqlDataSource>
                                        </asp:Panel>
                                        <br />
                                        <asp:Panel ID="Panel3" runat="server" CssClass="col-lg-12 col-sm-12 table-responsive marTop panelesOrdn" ScrollBars="Auto">
                                            <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered"
                                                AllowSorting="True" AutoGenerateColumns="False" EmptyDataRowStyle-CssClass="errores"
                                                EmptyDataText="No existe mano de obra registrada"
                                                DataSourceID="SqlDataSource14">
                                                <Columns>
                                                    <asp:BoundField DataField="mano" HeaderText="Mano" SortExpression="mano" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource14" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"></asp:SqlDataSource>
                                        </asp:Panel>
                                    </div>
                                </div>

                            </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>
                <div class="row text-center pad1m">
                    <div class="col-lg-12 col-sm-12 text-center">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="altav" CssClass="errores" DisplayMode="List" />
                        <asp:Label ID="lblErrorNuevaOrden" runat="server" CssClass="errores"></asp:Label>
                        <asp:Label ID="lblNota" runat="server" CssClass="errores" Text="Importante: el número de crédito será único por cada ciclo con el que cuente el grupo"></asp:Label><br />
                        <asp:LinkButton ID="btnGuardarOrden" runat="server" ToolTip="Registrar Orden" ValidationGroup="altav" Visible="false" CssClass="btn btn-success alingMiddle" OnClick="btnGuardarOrden_Click"><i class="fa fa-check-circle t18"></i></asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlOrden" runat="server" CssClass="popUp zen2 textoCentrado ancho30" Visible="false">
                <table class="ancho100">
                    <tr class="ancho100 centrado">
                        <td class="ancho100 text-center encabezadoTabla roundTopLeft roundTopRight">
                            <asp:Label ID="Label99" runat="server" Text="Orden Generada" CssClass="t22 colorMorado textoBold" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ancho100 text-center">
                            <asp:Label ID="lblOrdenGen" runat="server" CssClass="textoBold t22 text-info"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="ancho100 text-center">
                            <asp:LinkButton ID="btnAceptarOrden" runat="server" ToolTip="Aceptar" CssClass="btn btn-success alingMiddle" OnClick="btnAceptarOrden_Click"><i class="fa fa-check-circle t18"></i></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPregunta" runat="server" CssClass="popUp zen2 textoCentrado ancho30" Visible="false">
                <table class="ancho100">
                    <tr class="ancho100 centrado" >
                        <td class="ancho100 text-center encabezadoTabla roundTopLeft roundTopRight" colspan="2">
                            <asp:Label ID="Label21" runat="server" Text="Orden Existente" CssClass="t22 colorMorado textoBold" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ancho100 text-center" colspan="2">
                            <asp:Label ID="Label22" runat="server" CssClass="textoBold t14 text-info" Text="Ya existe una orden generada previamente ¿Está seguro de crear una nueva orden?"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="ancho50 text-center">
                            <asp:LinkButton ID="lnkAceptarNuevaOrden" runat="server" ToolTip="Aceptar" CssClass="btn btn-success alingMiddle" OnClick="lnkAceptarNuevaOrden_Click"><i class="fa fa-check-circle t18"></i></asp:LinkButton>
                        </td>
                        <td class="ancho50 text-center">
                            <asp:LinkButton ID="lnkCancelarNuevaOrden" runat="server" ToolTip="Cancelar" CssClass="btn btn-danger alingMiddle" OnClick="lnkCancelarNuevaOrden_Click"><i class="fa fa-remove t18"></i></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <asp:Panel ID="pnlMaskLoad" runat="server" CssClass="maskLoad"></asp:Panel>
                    <asp:Panel ID="pnlCargando" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoad" runat="server" ImageUrl="~/img/loading.gif" CssClass="center"/>
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

    
</asp:Content>
