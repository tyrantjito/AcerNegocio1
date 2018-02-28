<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true"
    CodeFile="BitacoraValuacion.aspx.cs" Inherits="BitacoraValuacion" Culture="es-Mx"
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
                Bit&aacute;cora Valuaci&oacute;n</h3>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-sm-12">
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:Label ID="lblIni" runat="server" Text="Fecha Inicial:" CssClass="textoBold"></asp:Label>
                    <telerik:RadDatePicker RenderMode="Lightweight" ID="txtFechaIni" CssClass="input-medium"
                        runat="server" SkinID="Metro">
                    </telerik:RadDatePicker>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:Label ID="Label1" runat="server" Text="Fecha Final:" CssClass="textoBold"></asp:Label>
                    <telerik:RadDatePicker RenderMode="Lightweight" ID="txtFechaFin" CssClass="toDate input-medium"
                        runat="server" SkinID="Metro">
                    </telerik:RadDatePicker>
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
                <div class="tile-wide" data-role="tile" data-effect="slideRight" style="background-color: #c83c21">
                    <div class="tile-content">
                        <div class="live-slide">
                            <center>
                                <asp:LinkButton ID="lnkPendientes1" runat="server" CssClass="link" ToolTip="Por Valuar"
                                    OnClick="lnkPendientes1_Click"><img alt="POR VALUAR" src="img/bitacoras/porValuar.png" class="pad1m"></asp:LinkButton>
                            </center>
                        </div>
                        <div class="live-slide">
                            <div class="pad1m">
                                <center>
                                    <asp:LinkButton ID="lnkPendientes11" runat="server" CssClass="link" ToolTip="Por Valuar"
                                        OnClick="lnkPendientes1_Click">
                                        <asp:Label ID="Label3" runat="server" Text="POR VALUAR" CssClass="textoBold colorBlanco pad1m"
                                            Font-Size="24pt"></asp:Label><br />
                                        <asp:Label ID="lblPendientes" runat="server" CssClass="textoBold colorBlanco pad1m"
                                            Font-Size="40pt"></asp:Label>
                                    </asp:LinkButton>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tile-wide" data-role="tile" data-effect="slideDown" style="background-color: #f37928">
                    <div class="tile-content">
                        <div class="live-slide">
                            <center>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="link" ToolTip="Por Autorizar"
                                    OnClick="lnkPendientes2_Click"><img alt="POR AUTORIZAR" src="img/bitacoras/porAutorizar.png" class="pad1m"></asp:LinkButton></center>
                        </div>
                        <div class="live-slide">
                            <div class="pad1m">
                                <center>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="link" ToolTip="Por Autorizar"
                                        OnClick="lnkPendientes2_Click">
                                        <asp:Label ID="Label5" runat="server" Text="POR AUTORIZAR" CssClass="textoBold colorBlanco pad1m"
                                            Font-Size="20pt"></asp:Label><br />
                                        <asp:Label ID="lblPorAutorizar" runat="server" CssClass="textoBold colorBlanco pad1m"
                                            Font-Size="40pt"></asp:Label></asp:LinkButton>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tile-wide" data-role="tile" data-effect="slideLeft" style="background-color: #03953f">
                    <div class="tile-content">
                        <div class="live-slide">
                            <center>
                                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="link" ToolTip="Autorizados"
                                    OnClick="lnkPendientes3_Click">
                                <img alt="AUTORIZADOS" src="img/bitacoras/autorizado.png" class="pad1m"></asp:LinkButton></center>
                        </div>
                        <div class="live-slide">
                            <div class="pad1m">
                                <center>
                                    <asp:LinkButton ID="LinkButton4" runat="server" CssClass="link" ToolTip="Autorizados"
                                        OnClick="lnkPendientes3_Click">
                                        <asp:Label ID="Label2" runat="server" Text="AUTORIZADOS" CssClass="textoBold colorBlanco pad1m"
                                            Font-Size="24pt"></asp:Label><br />
                                        <asp:Label ID="lblAutorizados" runat="server" CssClass="textoBold colorBlanco pad1m"
                                            Font-Size="40pt"></asp:Label></asp:LinkButton>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:DataList ID="DataList1" runat="server" RepeatColumns="3" OnItemDataBound="DataList1_ItemDataBound" ShowFooter="true">
                    <ItemTemplate>
                        <div class="tile-square" data-role="tile" data-effect="slideUpDown" runat="server" id="cuadro">
                            <div class="tile-content">
                                <div class="live-slide centrado">
                                    <asp:LinkButton ID="lnkSeleccion" CssClass="link" runat="server" CommandArgument='<%# Eval("id_cliprov")+";"+Eval("cliente") %>'
                                        OnClick="lnkProceso">
                                        <asp:Image ID="Image1" runat="server" CssClass="pad1m" ImageUrl='<%# "~/ImgCliente.ashx?id="+Eval("id_cliprov") %>'
                                            ToolTip='<%# Eval("cliente") %>' AlternateText='<%# Eval("cliente") %>' />
                                    </asp:LinkButton>
                                </div>
                                <div class="live-slide centrado">
                                    <div class="pad1m">
                                        <asp:LinkButton ID="LinkButton11" CssClass="link" runat="server" CommandArgument='<%# Eval("id_cliprov")+";"+Eval("cliente") %>'
                                            OnClick="lnkProceso">
                                            <asp:Label ID="lblEtiqueta" runat="server" Text='<%# Eval("cliente") %>' CssClass="textoBold colorBlanco pad1m"></asp:Label><br />
                                            <div class="center centrado" style="margin-top: 10px;">
                                                <div style="background-color: #c83c21; float: left; width: 33%; padding: 7px;" class="textoBold colorBlanco pad1m" >
                                                    <asp:Label ID="lblIndPendts" runat="server" Text='<%# Eval("totPorValuar") %>' CssClass="textoBold colorBlanco" Font-Size="22pt" />
                                                </div>
                                                <div style="background-color: #f37928; float: left; width: 33%; padding: 7px" class="textoBold colorBlanco pad1m" >
                                                    <asp:Label ID="lblIndPorAut" runat="server" Text='<%# Eval("totPorAut") %>' CssClass="textoBold colorBlanco" Font-Size="22pt" />
                                                </div>
                                                <div style="background-color: #03953f; float: left; width: 33%; padding: 7px" class="textoBold colorBlanco pad1m" >
                                                    <asp:Label ID="lblIndAut" runat="server" Text='<%# Eval("totAutorizados") %>' CssClass="textoBold colorBlanco" Font-Size="22pt" />
                                                </div>
                                            </div>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <asp:Panel ID="Panel4" runat="server" CssClass="panelCatalogos text-center" ScrollBars="Auto">
                <div class="">
                    <asp:Label ID="lblOpcion" runat="server" CssClass="t22 text-info" />
                    <asp:Label ID="lblIdCliprov" runat="server" Visible="false" />

                    <telerik:RadGrid RenderMode="Lightweight" ID="grdPorValuar" runat="server" DataSourceID="SqlDsPorValuar" AutoGenerateColumns="false" AllowSorting="True" AllowPaging="True" PageSize="20" Skin="Metro" ShowHeader="true">
                        <ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1">
                            </Scrolling>
                        </ClientSettings>
                        <MasterTableView ShowHeader="true">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Orden" SortExpression="no_orden">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnOrdenPendientes" runat="server" Text='<%# Bind("no_orden") %>'
                                            CommandArgument='<%# Bind("fase_orden") %>' OnClick="btnOrden_Click" CssClass="btn btn-info textoBold"></asp:LinkButton></ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="no_siniestro" HeaderText="Siniestro" SortExpression="no_siniestro"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="no_poliza" HeaderText="No. Poliza" SortExpression="no_poliza"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="descripcion" HeaderText="Vehículo" SortExpression="descripcion"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="modelo" HeaderText="Modelo" SortExpression="modelo"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="color_ext" HeaderText="Color" SortExpression="color_ext"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="placas" HeaderText="Placas" SortExpression="placas"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="perfil" HeaderText="Perfil" SortExpression="perfil"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="localizacion" HeaderText="Localización" SortExpression="localizacion"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="razon_social" HeaderText="Cliente" SortExpression="razon_social"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="fecha_Ingreso" HeaderText="Ingreso" SortExpression="fecha_Ingreso"
                                    ReadOnly="true" DataFormatString="{0:yyyy-MM-dd}" />
                                <telerik:GridBoundColumn DataField="f_alta" HeaderText="Alta" SortExpression="f_alta"
                                    ReadOnly="true" DataFormatString="{0:yyyy-MM-dd}" />
                                <telerik:GridBoundColumn DataField="f_alta_portal" HeaderText="Alta Portal" SortExpression="f_alta_portal"
                                    ReadOnly="true" DataFormatString="{0:yyyy-MM-dd}" />
                                <telerik:GridBoundColumn DataField="f_recibido_expediente" HeaderText="Expediente"
                                    SortExpression="f_recibido_expediente" ReadOnly="true" DataFormatString="{0:yyyy-MM-dd}" />
                                <telerik:GridBoundColumn DataField="f_valuacion" HeaderText="Valuación" SortExpression="f_valuacion"
                                    ReadOnly="true" DataFormatString="{0:yyyy-MM-dd}" />
                                <telerik:GridBoundColumn DataField="f_autorizacion" HeaderText="Autorización" SortExpression="f_autorizacion"
                                    ReadOnly="true" DataFormatString="{0:yyyy-MM-dd}" />
                                <telerik:GridTemplateColumn HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSeleccionaPendientes" CommandName="Select" runat="server"
                                            CommandArgument='<%# Eval("no_orden")+";"+Eval("fase_orden") %>' OnClick="lnkSelecciona_Click"
                                            CssClass="btn btn-success"><i class="fa fa-check"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <NoRecordsTemplate>
                                <asp:Label ID="lnlemptygridPend" runat="server" CssClass="errores" Text="No existe información"></asp:Label></NoRecordsTemplate>
                        </MasterTableView>                        
                        <PagerStyle Mode="NextPrevAndNumeric" />
                    </telerik:RadGrid>
                    <asp:SqlDataSource ID="SqlDsPorValuar" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"
                        SelectCommand= "SELECT  orp.no_orden, orp.fase_orden, orp.placas, no_siniestro, no_poliza, (Marcas.descripcion + ' ' + tipu.descripcion + ' ' + tipv.descripcion) AS descripcion, v.modelo, v.color_ext, c.razon_social, 
                        perf.descripcion AS perfil, loc.descripcion AS localizacion, so.f_recepcion AS fecha_Ingreso, CASE so.f_alta WHEN '1900-01-01' THEN '' ELSE so.f_alta END AS f_alta, 
	                    CASE Convert(char(20),so.f_alta_portal,120) WHEN '1900-01-01' THEN '' ELSE Convert(char(20),so.f_alta_portal,120) END AS f_alta_portal, CASE Convert(char(20),f_entrega,120) WHEN '1900-01-01' THEN '' ELSE Convert(char(20),f_entrega,120) END AS f_recibido_expediente, 
	                    CASE Convert(char(20),so.f_valuacion,120) WHEN '1900-01-01' THEN '' ELSE Convert(char(20),so.f_valuacion,120) END AS f_valuacion, CASE Convert(char(20),so.f_autorizacion,120) WHEN '1900-01-01' THEN '' ELSE Convert(char(20),so.f_autorizacion,120) END AS f_autorizacion
                        FROM  Ordenes_Reparacion AS orp LEFT JOIN
                        Marcas ON orp.id_marca = Marcas.id_marca LEFT JOIN
                        Tipo_Unidad AS tipu ON orp.id_marca = tipu.id_marca AND orp.id_tipo_vehiculo = tipu.id_tipo_vehiculo AND 
                        orp.id_tipo_unidad = tipu.id_tipo_unidad LEFT JOIN
                        Tipo_Vehiculo AS tipv ON orp.id_tipo_vehiculo = tipv.id_tipo_vehiculo LEFT JOIN
                        PerfilesOrdenes AS perf ON orp.id_perfilOrden = perf.id_perfilOrden LEFT  JOIN
                        Localizaciones AS loc ON orp.id_localizacion = loc.id_localizacion INNER JOIN
                        Seguimiento_Orden AS so ON orp.no_orden = so.no_orden AND orp.id_empresa = so.id_empresa AND orp.id_taller = so.id_taller LEFT JOIN
                        Vehiculos AS v ON orp.id_marca = v.id_marca AND orp.id_tipo_vehiculo = v.id_tipo_vehiculo AND orp.id_tipo_unidad = v.id_tipo_unidad AND 
                        orp.id_vehiculo = v.id_vehiculo INNER JOIN Cliprov c ON orp.id_cliprov = c.id_cliprov and c.tipo=orp.tipo_cliprov
                        WHERE orp.tipo_cliprov = 'C' AND (orp.id_taller = @id_taller) AND (orp.id_empresa = @id_empresa) AND (orp.status_orden = 'A') 
		                AND (((so.f_alta IS NULL OR so.f_alta='1900-01-01') OR (f_entrega IS NULL OR f_entrega='1900-01-01') OR (f_alta_portal IS NULL OR f_alta_portal='1900-01-01')) 
                        OR ((so.f_alta IS NOT NULL OR so.f_alta<>'1900-01-01') AND (f_entrega IS NOT NULL OR f_entrega<>'1900-01-01') AND (f_alta_portal IS NOT NULL OR f_alta_portal<>'1900-01-01') AND (f_valuacion IS NULL OR f_valuacion='1900-01-01'))) AND so.f_recepcion BETWEEN @fechaIni AND @fechaFin ORDER BY so.f_recepcion DESC, orp.no_orden DESC">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="id_empresa" QueryStringField="e" DefaultValue="0" />
                            <asp:QueryStringParameter Name="id_taller" QueryStringField="t" DefaultValue="0" />
                            <asp:Parameter Name="fechaIni" Type="String" />
                            <asp:Parameter Name="fechaFin" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <telerik:RadGrid RenderMode="Lightweight" ID="grdPorAut" runat="server" ShowStatusBar="true"
                        AutoGenerateColumns="False" AllowSorting="True" AllowMultiRowSelection="False" ShowHeader="true"
                        OnDetailTableDataBind="grdPorAut_DetailTableDataBind" OnNeedDataSource="grdPorAut_NeedDataSource"
                        OnPreRender="grdPorAut_PreRender" Skin="Metro" AllowPaging="true" PageSize="50">
                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                        <ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1">
                            </Scrolling>
                        </ClientSettings>
                        <MasterTableView DataKeyNames="no_orden" ShowHeader="true">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Orden" SortExpression="no_orden">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnOrdenValuadas" runat="server" Text='<%# Bind("no_orden") %>'
                                            CommandArgument='<%# Bind("fase_orden") %>' OnClick="btnOrden_Click" CssClass="btn btn-info textoBold"></asp:LinkButton></ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="no_siniestro" HeaderText="Siniestro" SortExpression="no_siniestro"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="no_poliza" HeaderText="No. Poliza" SortExpression="no_poliza"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="descripcion" HeaderText="Vehículo" SortExpression="descripcion"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="modelo" HeaderText="Modelo" SortExpression="modelo"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="color_ext" HeaderText="Color" SortExpression="color_ext"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="placas" HeaderText="Placas" SortExpression="placas"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="perfil" HeaderText="Perfil" SortExpression="perfil"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="localizacion" HeaderText="Localización" SortExpression="localizacion"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="razon_social" HeaderText="Cliente" SortExpression="razon_social"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="fecha_Ingreso" HeaderText="Ingreso" SortExpression="fecha_Ingreso"
                                    ReadOnly="true" DataFormatString="{0:yyyy-MM-dd}" />
                                <telerik:GridBoundColumn DataField="f_alta" HeaderText="Alta" SortExpression="f_alta"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="f_alta_portal" HeaderText="Alta Portal" SortExpression="f_alta_portal"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="f_recibido_expediente" HeaderText="Expediente"
                                    SortExpression="f_recibido_expediente" ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="f_valuacion" HeaderText="Valuación" SortExpression="f_valuacion"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="f_autorizacion" HeaderText="Autorización" SortExpression="f_autorizacion"
                                    ReadOnly="true" />
                                <telerik:GridTemplateColumn HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSeleccionaValuadas" CommandName="Select" runat="server" CommandArgument='<%# Eval("no_orden")+";"+Eval("fase_orden") %>'
                                            OnClick="lnkSelecciona_Click" CssClass="btn btn-success"><i class="fa fa-check"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <NoRecordsTemplate>
                                <asp:Label ID="lnlemptygrid" runat="server" CssClass="errores" Text="No existe información"></asp:Label>
                            </NoRecordsTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>

                    <!--Grid de Valuados original con grupos -->
                    <telerik:RadGrid RenderMode="Lightweight" ID="grdPorAut_OLD" runat="server" ShowStatusBar="true" Visible="false"
                        AutoGenerateColumns="False" AllowSorting="True" AllowMultiRowSelection="False" ShowHeader="true"
                        OnDetailTableDataBind="grdPorAut_DetailTableDataBind" OnNeedDataSource="grdPorAut_NeedDataSource"
                        OnPreRender="grdPorAut_PreRender" Skin="Metro">
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                        <MasterTableView DataKeyNames="ingreso" AllowMultiColumnSorting="True">
                            <DetailTables>
                                <telerik:GridTableView DataKeyNames="no_orden" Name="ingreso" Width="100%">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Orden" SortExpression="no_orden">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnOrdenValuadas" runat="server" Text='<%# Bind("no_orden") %>'
                                                    CommandArgument='<%# Bind("fase_orden") %>' OnClick="btnOrden_Click" CssClass="btn btn-info textoBold"></asp:LinkButton></ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="no_siniestro" HeaderText="Siniestro" SortExpression="no_siniestro"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="no_poliza" HeaderText="No. Poliza" SortExpression="no_poliza"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="descripcion" HeaderText="Vehículo" SortExpression="descripcion"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="modelo" HeaderText="Modelo" SortExpression="modelo"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="color_ext" HeaderText="Color" SortExpression="color_ext"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="placas" HeaderText="Placas" SortExpression="placas"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="perfil" HeaderText="Perfil" SortExpression="perfil"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="localizacion" HeaderText="Localización" SortExpression="localizacion"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="razon_social" HeaderText="Cliente" SortExpression="razon_social"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="fecha_Ingreso" HeaderText="Ingreso" SortExpression="fecha_Ingreso"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_alta" HeaderText="Alta" SortExpression="f_alta"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_alta_portal" HeaderText="Alta Portal" SortExpression="f_alta_portal"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_recibido_expediente" HeaderText="Expediente"
                                            SortExpression="f_recibido_expediente" ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_valuacion" HeaderText="Valuación" SortExpression="f_valuacion"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_autorizacion" HeaderText="Autorización" SortExpression="f_autorizacion"
                                            ReadOnly="true" />
                                        <telerik:GridTemplateColumn HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSeleccionaValuadas" CommandName="Select" runat="server" CommandArgument='<%# Eval("no_orden")+";"+Eval("fase_orden") %>'
                                                    OnClick="lnkSelecciona_Click" CssClass="btn btn-success"><i class="fa fa-check"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                            <Columns>
                                <telerik:GridBoundColumn SortExpression="ingreso" HeaderText="Fecha" HeaderButtonType="TextButton"
                                    DataField="ingreso">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="ordenes" HeaderText="Ordenes" HeaderButtonType="TextButton"
                                    DataField="ordenes">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <ItemStyle CssClass="text-bold" />
                            <AlternatingItemStyle CssClass="text-bold" />
                            <NoRecordsTemplate>
                                <asp:Label ID="lnlemptygrid" runat="server" CssClass="errores" Text="No existe información"></asp:Label>
                            </NoRecordsTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>

                    <telerik:RadGrid RenderMode="Lightweight" ID="grdAutorizados" runat="server" ShowStatusBar="true"
                        AutoGenerateColumns="False" AllowSorting="True" AllowMultiRowSelection="False" ShowHeader="true"
                        OnDetailTableDataBind="grdAutorizados_DetailTableDataBind" OnNeedDataSource="grdAutorizados_NeedDataSource"
                        OnPreRender="grdAutorizados_PreRender" Skin="Metro" AllowPaging="true" PageSize="50">
                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                        <ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1">
                            </Scrolling>
                        </ClientSettings>
                        <MasterTableView DataKeyNames="no_orden" ShowHeader="true">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Orden" SortExpression="no_orden">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnOrdenAut" runat="server" Text='<%# Bind("no_orden") %>' CommandArgument='<%# Bind("fase_orden") %>'
                                            OnClick="btnOrden_Click" CssClass="btn btn-info textoBold"></asp:LinkButton></ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="no_siniestro" HeaderText="Siniestro" SortExpression="no_siniestro"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="no_poliza" HeaderText="No. Poliza" SortExpression="no_poliza"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="descripcion" HeaderText="Vehículo" SortExpression="descripcion"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="modelo" HeaderText="Modelo" SortExpression="modelo"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="color_ext" HeaderText="Color" SortExpression="color_ext"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="placas" HeaderText="Placas" SortExpression="placas"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="perfil" HeaderText="Perfil" SortExpression="perfil"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="localizacion" HeaderText="Localización" SortExpression="localizacion"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="razon_social" HeaderText="Cliente" SortExpression="razon_social"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="fecha_Ingreso" HeaderText="Ingreso" SortExpression="fecha_Ingreso"
                                    ReadOnly="true" DataFormatString="{0:yyyy-MM-dd}" />
                                <telerik:GridBoundColumn DataField="f_alta" HeaderText="Alta" SortExpression="f_alta"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="f_alta_portal" HeaderText="Alta Portal" SortExpression="f_alta_portal"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="f_recibido_expediente" HeaderText="Expediente"
                                    SortExpression="f_recibido_expediente" ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="f_valuacion" HeaderText="Valuación" SortExpression="f_valuacion"
                                    ReadOnly="true" />
                                <telerik:GridBoundColumn DataField="f_autorizacion" HeaderText="Autorización" SortExpression="f_autorizacion"
                                    ReadOnly="true" />
                                <telerik:GridTemplateColumn HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSeleccionaAut" CommandName="Select" runat="server" CommandArgument='<%# Eval("no_orden")+";"+Eval("fase_orden") %>'
                                            OnClick="lnkSelecciona_Click" CssClass="btn btn-success"><i class="fa fa-check"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <NoRecordsTemplate>
                                <asp:Label ID="lnlemptygrid" runat="server" CssClass="errores" Text="No existe información"></asp:Label>
                            </NoRecordsTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>

                    <!--Grid original de Autorizados por grupos de fechas -->
                    <telerik:RadGrid RenderMode="Lightweight" ID="grdAutorizados_OLD" runat="server" ShowStatusBar="true" Visible="false"
                        AutoGenerateColumns="False" AllowSorting="True" AllowMultiRowSelection="False" ShowHeader="true"
                        OnDetailTableDataBind="grdAutorizados_DetailTableDataBind" OnNeedDataSource="grdAutorizados_NeedDataSource"
                        OnPreRender="grdAutorizados_PreRender" Skin="Metro">
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                        <MasterTableView DataKeyNames="ingreso" AllowMultiColumnSorting="True">
                            <DetailTables>
                                <telerik:GridTableView DataKeyNames="no_orden" Name="ingreso" Width="100%">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Orden" SortExpression="no_orden">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnOrdenAut" runat="server" Text='<%# Bind("no_orden") %>' CommandArgument='<%# Bind("fase_orden") %>'
                                                    OnClick="btnOrden_Click" CssClass="btn btn-info textoBold"></asp:LinkButton></ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="no_siniestro" HeaderText="Siniestro" SortExpression="no_siniestro"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="no_poliza" HeaderText="No. Poliza" SortExpression="no_poliza"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="descripcion" HeaderText="Vehículo" SortExpression="descripcion"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="modelo" HeaderText="Modelo" SortExpression="modelo"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="color_ext" HeaderText="Color" SortExpression="color_ext"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="placas" HeaderText="Placas" SortExpression="placas"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="perfil" HeaderText="Perfil" SortExpression="perfil"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="localizacion" HeaderText="Localización" SortExpression="localizacion"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="razon_social" HeaderText="Cliente" SortExpression="razon_social"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="fecha_Ingreso" HeaderText="Ingreso" SortExpression="fecha_Ingreso"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_alta" HeaderText="Alta" SortExpression="f_alta"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_alta_portal" HeaderText="Alta Portal" SortExpression="f_alta_portal"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_recibido_expediente" HeaderText="Expediente"
                                            SortExpression="f_recibido_expediente" ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_valuacion" HeaderText="Valuación" SortExpression="f_valuacion"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_autorizacion" HeaderText="Autorización" SortExpression="f_autorizacion"
                                            ReadOnly="true" />
                                        <telerik:GridTemplateColumn HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSeleccionaAut" CommandName="Select" runat="server" CommandArgument='<%# Eval("no_orden")+";"+Eval("fase_orden") %>'
                                                    OnClick="lnkSelecciona_Click" CssClass="btn btn-success"><i class="fa fa-check"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                            <Columns>
                                <telerik:GridBoundColumn SortExpression="ingreso" HeaderText="Fecha" HeaderButtonType="TextButton"
                                    DataField="ingreso">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="ordenes" HeaderText="Ordenes" HeaderButtonType="TextButton"
                                    DataField="ordenes">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <ItemStyle CssClass="text-bold" />
                            <AlternatingItemStyle CssClass="text-bold" />
                            <NoRecordsTemplate>
                                <asp:Label ID="lnlemptygrid" runat="server" CssClass="errores" Text="No existe información"></asp:Label>
                            </NoRecordsTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>

                    <telerik:RadGrid RenderMode="Lightweight" ID="grdPndtsValAutPorProv" runat="server" ShowStatusBar="true"
                        AutoGenerateColumns="False" AllowSorting="True" AllowMultiRowSelection="False" ShowHeader="true"
                        OnDetailTableDataBind="grdPndtsValAutPorProv_DetailTableDataBind" OnNeedDataSource="grdPndtsValAutPorProv_NeedDataSource"
                        OnPreRender="grdPndtsValAutPorProv_PreRender" Skin="Metro" OnItemDataBound="grdPndtsValAutPorProv_ItemDataBound">
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                        <MasterTableView DataKeyNames="ingreso" AllowMultiColumnSorting="True" GroupLoadMode="Client" GroupsDefaultExpanded="true">
                            <DetailTables>
                                <telerik:GridTableView DataKeyNames="no_orden" Name="ingreso" Width="100%">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Orden" SortExpression="no_orden">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnOrdenValuadas" runat="server" Text='<%# Bind("no_orden") %>'
                                                    CommandArgument='<%# Bind("fase_orden") %>' OnClick="btnOrden_Click" CssClass="btn btn-info textoBold"></asp:LinkButton></ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="no_siniestro" HeaderText="Siniestro" SortExpression="no_siniestro"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="no_poliza" HeaderText="No. Poliza" SortExpression="no_poliza"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="descripcion" HeaderText="Vehículo" SortExpression="descripcion"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="modelo" HeaderText="Modelo" SortExpression="modelo"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="color_ext" HeaderText="Color" SortExpression="color_ext"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="placas" HeaderText="Placas" SortExpression="placas"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="perfil" HeaderText="Perfil" SortExpression="perfil"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="localizacion" HeaderText="Localización" SortExpression="localizacion"
                                            ReadOnly="true" />                                        
                                        <telerik:GridBoundColumn DataField="fecha_Ingreso" HeaderText="Ingreso" SortExpression="fecha_Ingreso" DataFormatString="{0:yyyy-MM-dd}"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_alta" HeaderText="Alta" SortExpression="f_alta"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_alta_portal" HeaderText="Alta Portal" SortExpression="f_alta_portal"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_recibido_expediente" HeaderText="Expediente"
                                            SortExpression="f_recibido_expediente" ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_valuacion" HeaderText="Valuación" SortExpression="f_valuacion"
                                            ReadOnly="true" />
                                        <telerik:GridBoundColumn DataField="f_autorizacion" HeaderText="Autorización" SortExpression="f_autorizacion"
                                            ReadOnly="true" />
                                        <telerik:GridTemplateColumn HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSeleccionaValuadas" CommandName="Select" runat="server" CommandArgument='<%# Eval("no_orden")+";"+Eval("fase_orden") %>'
                                                    OnClick="lnkSelecciona_Click" CssClass="btn btn-success"><i class="fa fa-check"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                            <Columns>
                                <telerik:GridBoundColumn SortExpression="ingreso" HeaderText="Fecha" HeaderButtonType="TextButton"
                                    DataField="ingreso">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="ordenes" HeaderText="Ordenes" HeaderButtonType="TextButton"
                                    DataField="ordenes">
                                </telerik:GridBoundColumn>
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
</asp:Content>
