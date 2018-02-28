<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BitacoraIngresos.aspx.cs"
    Inherits="BitacoraIngresos" MasterPageFile="~/AdmOrdenes.master" Culture="es-Mx"
    UICulture="es-Mx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="css/metro/metro.css" rel="stylesheet">    
    <script src="js/metro/metro.js"></script>    

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    
    <div class="page-header">
        <!-- /BREADCRUMBS -->
        <div class="clearfix">
            <h3 class="content-title pull-left">
                Bit&aacute;cora Ingresos</h3>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-sm-12">
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:Label ID="lblIni" runat="server" Text="Fecha Inicial:" CssClass="textoBold"></asp:Label>
                    <telerik:RadDatePicker RenderMode="Lightweight" ID="txtFechaIni" CssClass="input-medium"  runat="server" SkinID="Metro"></telerik:RadDatePicker>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:Label ID="Label1" runat="server" Text="Fecha Final:" CssClass="textoBold"></asp:Label>
                    <telerik:RadDatePicker RenderMode="Lightweight" ID="txtFechaFin" CssClass="toDate input-medium" runat="server" SkinID="Metro"></telerik:RadDatePicker>
                </div>
                <div class="col-lg-2 col-sm-2 text-center">
                    <asp:LinkButton ID="lnkBuscar" runat="server" CssClass="btn btn-info" ToolTip="Generar"
                        OnClick="lnkBuscar_Click"><i class="fa fa-cog"></i><span>&nbsp;Generar</span></asp:LinkButton>
                </div>
                <div class="col-lg-2 col-sm-2 text-center">
                    <asp:LinkButton ID="lnkRegresarOrdenes" runat="server" OnClick="lnkRegresarOrdenes_Click"
                        CssClass="btn btn-info t14"><i class="fa fa-reply">&nbsp;&nbsp;</i><i class="fa fa-th-list"></i>&nbsp;<span>Órdenes</span></asp:LinkButton>
                </div>
            </div>
            <div class="col-lg-12 col-sm-12 text-center">
                <asp:Label ID="lblError" runat="server" CssClass="errores" />
            </div>
            <div class="tile-container">
                <div class="tile-large" data-role="tile" data-effect="slideUpDown" style="background-color: #007ac0">
                    <div class="tile-content">
                        <div class="live-slide">
                            <center>
                                <img alt="Ingresos" src="img/bitacoras/ingresos.png" class="pad1m"></center>
                        </div>
                        <div class="live-slide">
                            <div class="pad1m">
                                <center>
                                    <asp:Label ID="lblEtiquetaIngreso" runat="server" Text="INGRESOS" CssClass="textoBold colorBlanco pad1m"
                                        Font-Size="30pt"></asp:Label><br />                                    
                                    <asp:Label ID="lblIngresosTotales" runat="server" Text="5" CssClass="textoBold colorBlanco pad1m"
                                        Font-Size="60pt"></asp:Label>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tile-large" data-role="tile" data-effect="slideRight" style="background-color: #c83c21">
                    <div class="tile-content">
                        <div class="live-slide">
                            <center>
                                <img alt="Clientes" src="img/bitacoras/clientes.png" class="pad1m"></center>
                        </div>
                        <div class="live-slide">
                            <center>
                                <asp:Label ID="Label8" runat="server" Text="CLIENTES" CssClass="t14 textoBold colorBlanco pad1m"></asp:Label><br />
                                <div class="pad1m">
                                    <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGridClientes" SkinID="Metro"
                                        ShowHeader="false" CssClass="textoBold" OnNeedDataSource="RadGridClientes_NeedDataSource" Culture="es-Mx">                                        
                                    </telerik:RadGrid>
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
                <div class="tile-large" data-role="tile" data-effect="slideDown" style="background-color: #f37928">
                    <div class="tile-content">
                        <div class="live-slide">
                            <center>
                                <img alt="Localizaciones" src="img/bitacoras/localizacion.png" class="pad1m"></center>
                        </div>
                        <div class="live-slide">
                            <center>
                                <asp:Label ID="Label5" runat="server" Text="LOCALIZACION" CssClass="t14 textoBold colorBlanco pad1m"></asp:Label><br />
                                <div class="pad1m">
                                    <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGridLocalizaciones"
                                        SkinID="Metro" ShowHeader="false" CssClass="textoBold" OnNeedDataSource="RadGridLocalizaciones_NeedDataSource">
                                    </telerik:RadGrid>
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
                <div class="tile-group.four">
                    <div class="tile-wide" data-role="tile" data-effect="slideLeft" style="background-color: #03953f">
                        <div class="tile-content">
                            <div class="live-slide">
                                <center>
                                    <img alt="Tipo Servicios" src="img/bitacoras/servicios.png" class="pad1m"></center>
                            </div>
                            <div class="live-slide">
                                <center>
                                    <asp:Label ID="Label2" runat="server" Text="TIPO SERVICIO" CssClass="t14 textoBold colorBlanco pad1m"></asp:Label><br />
                                    <div class="pad1m">
                                        <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGridTipoSeervicio"
                                            SkinID="Metro" ShowHeader="false" OnNeedDataSource="RadGridTipoServicio_NeedDataSource"
                                            CssClass="textoBold">
                                        </telerik:RadGrid>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>
                    <div class="tile-wide" data-role="tile" data-effect="slideUp" style="background-color: #f8b617">
                        <div class="tile-content">
                            <div class="live-slide">
                                <center>
                                    <img alt="Perfiles" src="img/bitacoras/perfiles.png" class="pad1m"></center>
                            </div>
                            <div class="live-slide">
                                <center>
                                    <asp:Label ID="Label4" runat="server" Text="PERFILES" CssClass="t14 textoBold colorBlanco pad1m"></asp:Label><br />
                                    <div class="pad1m">
                                        <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGridPerfiles" SkinID="Metro"
                                            ShowHeader="false" CssClass="textoBold" OnNeedDataSource="RadGridPerfiles_NeedDataSource">
                                        </telerik:RadGrid>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>
                    <div class="tile-wide" data-role="tile" data-effect="slideLeftRight" style="background-color: #a2316e">
                        <div class="tile-content">
                            <div class="live-slide">
                                <center>
                                    <img alt="Tipo Valuación" src="img/bitacoras/valuacion.png" class="pad1m"></center>
                            </div>
                            <div class="live-slide">
                                <center>
                                    <asp:Label ID="Label6" runat="server" Text="TIPO VALUACION" CssClass="t14 textoBold colorBlanco pad1m"></asp:Label><br />
                                    <div class="pad1m">
                                        <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGridValuacion" SkinID="Metro"
                                            ShowHeader="false" CssClass="textoBold" OnNeedDataSource="RadGridValuacion_NeedDataSource">
                                        </telerik:RadGrid>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>
                    <div class="tile-wide" data-role="tile" data-effect="slideUpDown" style="background-color: #82c92f">
                        <div class="tile-content">
                            <div class="live-slide">
                                <center>
                                    <img alt="Etapas Recepción" src="img/bitacoras/etapas.png" class="pad1m"></center>
                            </div>
                            <div class="live-slide">
                                <center>
                                    <asp:Label ID="Label7" runat="server" Text="ETAPAS COMPLETADAS RECEPCIÓN" CssClass="t14 textoBold colorBlanco pad1m"></asp:Label><br />
                                    <div class="pad1m">
                                        <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGridEtapas" SkinID="Metro"
                                            ShowHeader="false" CssClass="textoBold" OnNeedDataSource="RadGridEtapas_NeedDataSource">
                                        </telerik:RadGrid>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="Panel1" runat="server" CssClass="panelCatalogos text-center" ScrollBars="Auto">
                <div class="table-responsive">
                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" runat="server" ShowStatusBar="true"
                        AutoGenerateColumns="False" AllowSorting="True" AllowMultiRowSelection="False" 
                        OnDetailTableDataBind="RadGrid1_DetailTableDataBind" OnNeedDataSource="RadGrid1_NeedDataSource"
                        OnPreRender="RadGrid1_PreRender" Skin="Metro" OnItemDataBound="RadGrid1_ItemDataBound">
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                        <MasterTableView DataKeyNames="ingreso" AllowMultiColumnSorting="True" GroupsDefaultExpanded="true" HierarchyDefaultExpanded="true">
                            <DetailTables>
                                <telerik:GridTableView DataKeyNames="no_orden" Name="ingreso" Width="100%" >                                    
                                    <Columns>                                      
                                        <telerik:GridTemplateColumn SortExpression="no_orden" HeaderText="Orden">
                                            <ItemTemplate>
                                                 <asp:LinkButton ID="btnOrden" runat="server" Text='<%# Eval("no_orden") %>' CommandArgument='<%# Eval("fase_orden") %>' OnClick="btnOrden_Click" CssClass="btn btn-info textoBold"></asp:LinkButton>               
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn SortExpression="descripcion" HeaderText="Vehículo" HeaderButtonType="TextButton" DataField="descripcion"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="modelo" HeaderText="Modelo" HeaderButtonType="TextButton" DataField="modelo"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="color_ext" HeaderText="Color" SortExpression="color_ext"  HeaderButtonType="TextButton"/>
                                        <telerik:GridBoundColumn DataField="placas" HeaderText="Placas" SortExpression="placas"  HeaderButtonType="TextButton"/>
                                        <telerik:GridBoundColumn DataField="perfil" HeaderText="Perfil" SortExpression="perfil"  HeaderButtonType="TextButton"/>
                                        <telerik:GridBoundColumn DataField="localizacion" HeaderText="Localización" SortExpression="localizacion"  HeaderButtonType="TextButton"/>
                                        <telerik:GridBoundColumn DataField="razon_social" HeaderText="Cliente" SortExpression="razon_social"  HeaderButtonType="TextButton"/>
                                        <telerik:GridBoundColumn DataField="no_siniestro" HeaderText="Siniestro" SortExpression="no_siniestro"  HeaderButtonType="TextButton"/>
                                        <telerik:GridTemplateColumn HeaderText="Datos Orden" SortExpression="datos_orden">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkDatOrd" Checked='<%# Eval("datos_orden") %>' runat="server" Enabled="false" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Inventario" SortExpression="inventario">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkInvent" Checked='<%# Eval("inventario") %>' runat="server" Enabled="false" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Características Vehículo" SortExpression="caracteristicas_vehiculo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCaractVeh" Checked='<%# Eval("caracteristicas_vehiculo") %>' runat="server" Enabled="false" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="usuario" HeaderText="Asesor" SortExpression="usuario"  HeaderButtonType="TextButton"/>
                                    </Columns>                                    
                                </telerik:GridTableView>
                            </DetailTables>
                            <Columns>
                                <telerik:GridBoundColumn SortExpression="ingreso" HeaderText="Fecha" HeaderButtonType="TextButton" DataField="ingreso"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="ordenes" HeaderText="Ordenes" HeaderButtonType="TextButton" DataField="ordenes"></telerik:GridBoundColumn>
                            </Columns>
                            <ItemStyle CssClass="text-bold" />     
                            <AlternatingItemStyle CssClass="text-bold" />                       
                            <NoRecordsTemplate>
                                <asp:Label ID="lnlemptygrid" runat="server" CssClass="errores" Text="No existe información"></asp:Label>
                            </NoRecordsTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </asp:Panel>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <asp:Panel ID="pnlMaskLoad1" runat="server" CssClass="maskLoad">
                    </asp:Panel>
                    <asp:Panel ID="pnlCargando1" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoad1" runat="server" ImageUrl="~/img/loading.gif" CssClass="ancho100" />
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
