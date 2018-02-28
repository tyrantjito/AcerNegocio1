<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="BitacoraPerdidaPago.aspx.cs" Inherits="BitacoraPerdidaPago" %>

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
                Bit&aacute;cora Perfiles</h3>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                <div class="col-lg-12 col-sm-12 text-right">
                    <asp:CheckBoxList ID="chkPerfiles" RepeatColumns="5" AutoPostBack="true" RepeatDirection="Horizontal" 
                        TextAlign="Left" CellPadding="3" CellSpacing="5" OnSelectedIndexChanged="chkPerfiles_SelectedIndexChanged" runat="server" 
                        DataSourceID="SqlDataSource10" DataTextField="descripcion" DataValueField="id_perfilOrden">
                    </asp:CheckBoxList>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource10" ConnectionString='<%$ ConnectionStrings:Taller %>' 
                        SelectCommand="SELECT id_perfilOrden, descripcion+'&nbsp;'as descripcion FROM PerfilesOrdenes order by LEN(descripcion) asc"></asp:SqlDataSource>
                </div>
            </asp:Panel>
            <div class="col-lg-2 col-sm-2 text-right">
                <asp:LinkButton ID="lnkRegresarOrdenes" runat="server" OnClick="lnkRegresarOrdenes_Click"
                    CssClass="btn btn-info t14"><i class="fa fa-reply">&nbsp;&nbsp;</i><i class="fa fa-th-list"></i>&nbsp;<span>Órdenes</span></asp:LinkButton>
            </div>
            <div class="col-lg-12 col-sm-12 text-center">
                <asp:Label ID="lblError" runat="server" CssClass="errores" />
            </div>
            <div class="tile-container">
                <asp:Panel ID="PandTotales" runat="server">
                    <div class="tile-wide" data-role="tile" data-effect="slideRight" style="background-color: #000000">
                        <div class="tile-content">
                            <div class="live-slide text-center">
                                <asp:LinkButton Enabled="false" ID="lnkPendientes1" runat="server" CssClass="link" ToolTip="PERFIL(ES) SELECCIONADO(S)"
                                    OnClick="lnkPendientes1_Click"><img alt="VEHÍCULO(S) EN PERFIL(ES) SELECCIONADO(S)" src="img/bitacoras/autorizado.png" class="pad1m"></asp:LinkButton>
                            </div>
                            <div class="live-slide">
                                <div class="pad1m">
                                    <center>
                                        <asp:LinkButton ID="lnkPendientes11" runat="server" CssClass="link" ToolTip="PERFIL(ES) SELECCIONADO(S)"
                                            OnClick="lnkPendientes1_Click">
                                            <asp:Label ID="Label3" runat="server" Text="VEHÍCULO(S) EN PERFIL(ES) SELECCIONADO(S)" CssClass="textoBold colorBlanco pad1m"
                                                Font-Size="18pt"></asp:Label><br />
                                            <asp:Label ID="lblSeleccionados" runat="server" CssClass="textoBold colorBlanco pad1m"
                                                Font-Size="40pt"></asp:Label>
                                        </asp:LinkButton>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PanExtras" runat="server">
                    <div class="tile-wide" data-role="tile" data-effect="slideDown" style="background-color: #f37928">
                        <div class="tile-content">
                            <div class="live-slide">
                                <center>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="link" ToolTip="REINGRESO"
                                        OnClick="lnkPendientes2_Click"><img alt="REINGRESO" src="img/bitacoras/porAutorizar.png" class="pad1m"></asp:LinkButton></center>
                            </div>
                            <div class="live-slide">
                                <div class="pad1m">
                                    <center>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="link" ToolTip="REINGRESO"
                                            OnClick="lnkPendientes2_Click">
                                            <asp:Label ID="Label5" runat="server" Text="REINGRESO" CssClass="textoBold colorBlanco pad1m"
                                                Font-Size="20pt"></asp:Label><br />
                                            <asp:Label ID="lblPorRetornoHoy" runat="server" CssClass="textoBold colorBlanco pad1m"
                                                Font-Size="40pt"></asp:Label></asp:LinkButton>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tile-wide" data-role="tile" data-effect="slideLeft" style="background-color: #c83c21">
                        <div class="tile-content">
                            <div class="live-slide">
                                <center>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CssClass="link" ToolTip="REINGRESO VENCIDO"
                                        OnClick="lnkPendientes3_Click">
                                    <img alt="REINGRESO VENCIDO" src="img/bitacoras/porValuar.png" class="pad1m"></asp:LinkButton></center>
                            </div>
                            <div class="live-slide">
                                <div class="pad1m">
                                    <center>
                                        <asp:LinkButton ID="LinkButton4" runat="server" CssClass="link" ToolTip="REINGRESO VENCIDO"
                                            OnClick="lnkPendientes3_Click">
                                            <asp:Label ID="Label2" runat="server" Text="REINGRESO VENCIDO" CssClass="textoBold colorBlanco pad1m"
                                                Font-Size="24pt"></asp:Label><br />
                                            <asp:Label ID="lblRetProgVenc" runat="server" CssClass="textoBold colorBlanco pad1m"
                                                Font-Size="40pt"></asp:Label></asp:LinkButton>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:DataList ID="DataList1" runat="server" RepeatColumns="1" DataKeyField="id_perfilOrden,vehiculos" OnItemDataBound="DataList1_ItemDataBound">
                    <ItemTemplate>
                        <asp:Label ID="lblPerfilId" runat="server" Text='<%# Eval("id_perfilOrden") %>' ></asp:Label>
                        <asp:Label ID="lblVehiculosInt" runat="server" Text='<%# Eval("vehiculos") %>' CssClass="textoBold colorBlanco pad1m" Font-Size="22pt"></asp:Label>
                        <div class="tile-square" data-role="tile" data-effect="slideUpDown" runat="server" id="cuadro">
                            <div class="tile-content">
                                <div class="live-slide">
                                    <div class="pad1m text-center">
                                        <asp:LinkButton ID="LinkButton5" CssClass="link" runat="server" CommandArgument='<%# Eval("id_perfilOrden") %>'
                                            OnClick="lnkProceso">
                                            <asp:Label ID="lblPerfil" runat="server" Text='<%# Eval("perfil") %>' CssClass="textoBold colorBlanco pad1m"></asp:Label><br />
                                            <asp:Label ID="lblVehiculos" runat="server" Text='<%# Eval("vehiculos") %>' CssClass="textoBold colorBlanco pad1m"
                                                Font-Size="22pt"></asp:Label>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:DataList ID="DataLAsg" runat="server" RepeatColumns="3" OnItemDataBound="DataLAsg_ItemDataBound">
                            <ItemTemplate>
                                <div class="tile-square" data-role="tile" data-effect="slideUpDown" runat="server" id="cuadro">
                                    <div class="tile-content">
                                        <div class="live-slide">
                                            <center>
                                                <asp:LinkButton ID="lnkSeleccion" CssClass="link" runat="server" CommandArgument='<%# Eval("id_cliprov")+";"+Eval("cliente") %>'
                                                    OnClick="lnkProceso">
                                                    <asp:Image ID="Image1" runat="server" CssClass="pad1m" ImageUrl='<%# "~/ImgCliente.ashx?id="+Eval("id_cliprov") %>'
                                                        ToolTip='<%# Eval("cliente") %>' AlternateText='<%# Eval("cliente") %>' />
                                                </asp:LinkButton>
                                            </center>
                                        </div>
                                        <div class="live-slide">
                                            <div class="pad1m">
                                                <center>
                                                    <asp:LinkButton ID="LinkButton11" CssClass="link" runat="server" CommandArgument='<%# Eval("id_cliprov")+";"+Eval("cliente") %>'
                                                        OnClick="lnkProceso">
                                                        <asp:Label ID="lblEtiqueta" runat="server" Text='<%# Eval("cliente") %>' CssClass="textoBold colorBlanco pad1m"></asp:Label><br />
                                                        <asp:Label ID="lblIndicadores" runat="server" Text='<%# Eval("total") %>' CssClass="textoBold colorBlanco pad1m"
                                                            Font-Size="22pt"></asp:Label>
                                                    </asp:LinkButton>
                                                </center>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <asp:DataList ID="DataPerfiles" runat="server" RepeatColumns="1" OnItemDataBound="DataPerfiles_ItemDataBound">
                <ItemTemplate>
                    <div id="DivPerfil" runat="server" class="col-lg-6 col-sm-6 text-center">
                        <asp:Label ID="lblPerfilId2" runat="server" Text='<%# Eval("id_perfilOrden") %>' Visible="false" ></asp:Label>
                        <asp:Label ID="lblPerfilDesc2" runat="server" Text='<%# Eval("perfil") %>' CssClass="textoBold colorBlanco pad1m" /><br />
                        <asp:Label ID="lblVehiculosInt2" runat="server" Text='<%# Eval("vehiculos") %>' CssClass="textoBold colorBlanco pad1m" />
                    </div>
                    <div class="col-lg-6 col-sm-6 text-left">
                        <asp:DataList ID="DataLAsg2" runat="server" RepeatColumns="6" OnItemDataBound="DataLAsg2_ItemDataBound" >
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblClienteAsg" Text='<%# Eval("cliente") %>' Visible="false" />
                                <asp:Label runat="server" ID="lblTotalAsg" Text='<%# Eval("total") %>' Visible="false" />
                                <div class="tile-square" data-role="tile" data-effect="slideUpDown" runat="server" id="cuadro">
                                    <div class="tile-content">
                                        <div class="live-slide">
                                            <center>
                                                <asp:LinkButton ID="lnkSeleccion" CssClass="link" runat="server" CommandArgument='<%# Eval("id_cliprov")+";"+Eval("cliente")+";"+Eval("id_perfilOrden") %>'
                                                    OnClick="lnkProceso">
                                                    <asp:Image ID="Image1" runat="server" CssClass="pad1m" ImageUrl='<%# "~/ImgCliente.ashx?id="+Eval("id_cliprov") %>'
                                                        ToolTip='<%# Eval("cliente") %>' AlternateText='<%# Eval("cliente") %>' />
                                                </asp:LinkButton>
                                            </center>
                                        </div>
                                        <div class="live-slide">
                                            <div class="pad1m">
                                                <center>
                                                    <asp:LinkButton ID="LinkButton11" CssClass="link" runat="server" CommandArgument='<%# Eval("id_cliprov")+";"+Eval("cliente")+";"+Eval("id_perfilOrden") %>'
                                                        OnClick="lnkProceso">
                                                        <asp:Label ID="lblEtiqueta" runat="server" Text='<%# Eval("cliente") %>' CssClass="textoBold colorBlanco pad1m"></asp:Label><br />
                                                        <asp:Label ID="lblIndicadores" runat="server" Text='<%# Eval("total") %>' CssClass="textoBold colorBlanco pad1m"
                                                            Font-Size="22pt"></asp:Label>
                                                    </asp:LinkButton>
                                                </center>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            <asp:Panel ID="Panel4" runat="server" CssClass="panelCatalogos text-center" ScrollBars="Auto">
                <div class="table-responsive">
                    <div class="col-lg-12 col-sm-12 text-center pad1m">
                        <asp:Label ID="lblOpcion" runat="server" CssClass="t22 text-info" />
                        <asp:Label ID="lblIdCliprov" runat="server" Visible="false" />
                        <asp:Label ID="lblidPerfilSelClip" runat="server" Visible="false" />
                    </div>
                    <asp:GridView ID="GridDatos" runat="server" AutoGenerateColumns="false" GridLines="None" PageSize="20"
                        OnRowDataBound="GridDatos_RowDataBound" EmptyDataText="No cuenta con informaicón para mostrar."
                        EmptyDataRowStyle-CssClass="errores alert-danger">
                        <Columns>
                            <asp:TemplateField HeaderText="Orden" SortExpression="no_orden">
                                <ItemTemplate>
                                        <asp:LinkButton ID="btnOrdenPendientes" runat="server" Text='<%# Bind("no_orden") %>'
                                            CommandArgument='<%# Bind("fase_orden") %>' OnClick="btnOrden_Click" CssClass="btn btn-info textoBold"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:BoundField DataField="no_siniestro" HeaderText="Siniestro" SortExpression="no_siniestro"
                                    ReadOnly="true" />
                                <asp:BoundField DataField="no_poliza" HeaderText="No. Poliza" SortExpression="no_poliza"
                                    ReadOnly="true" />
                                <asp:BoundField DataField="descripcion" HeaderText="Vehículo" SortExpression="descripcion"
                                    ReadOnly="true" />
                                <asp:BoundField DataField="modelo" HeaderText="Modelo" SortExpression="modelo"
                                    ReadOnly="true" />
                                <asp:BoundField DataField="color_ext" HeaderText="Color" SortExpression="color_ext"
                                    ReadOnly="true" />
                                <asp:BoundField DataField="placas" HeaderText="Placas" SortExpression="placas"
                                    ReadOnly="true" />
                            <asp:TemplateField HeaderText="Perfil" SortExpression="perfil">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("perfil") %>' ID="Label1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Localizaci&#243;n" SortExpression="localizacion">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("localizacion") %>' ID="Label2"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="razon_social" HeaderText="Cliente" SortExpression="razon_social"
                                    ReadOnly="true" />
                                <asp:BoundField DataField="fecha_Ingreso" HeaderText="Ingreso" SortExpression="fecha_Ingreso"
                                    ReadOnly="true" />
                            <asp:TemplateField HeaderText="Prog. Retorno Transito" SortExpression="f_prog_retorno_tran" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFechProgRetTrans" Text='<%# Eval("f_prog_retorno_tran") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Retorno Transito" SortExpression="f_retorno_transito" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFechRetTrans" Text='<%# Eval("f_retorno_transito") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:BoundField DataField="observacion" HeaderText="Observaciones"
                                    SortExpression="observacion" ReadOnly="true" />
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSeleccionaPendientes" CommandName="Select" runat="server"
                                            CommandArgument='<%# Eval("no_orden")+";"+Eval("fase_orden") %>' OnClick="lnkSelecciona_Click"
                                            CssClass="btn btn-success"><i class="fa fa-check"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"
                        SelectCommand="select orp.id_perfilOrden,p.descripcion as perfil,count(orp.id_perfilOrden)as vehiculos
                            from Ordenes_Reparacion orp
                            inner join perfilesordenes p on p.id_perfilOrden=orp.id_perfilOrden
                            where orp.status_orden='A' and orp.id_empresa = @id_empresa and orp.id_taller = @id_taller
                            group by p.descripcion,orp.id_perfilOrden">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"
                        SelectCommand="select orp.id_perfilOrden,p.descripcion as perfil,count(orp.id_perfilOrden)as vehiculos--,orp.id_cliprov as cliente
                            from Ordenes_Reparacion orp
                            inner join perfilesordenes p on p.id_perfilOrden=orp.id_perfilOrden
                            where orp.id_perfilOrden = @id_perfilOrden and orp.status_orden='A' and orp.id_empresa = @id_empresa and orp.id_taller = @id_taller
                            group by p.descripcion,orp.id_perfilOrden,orp.id_cliprov
                            order by orp.id_perfilOrden">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"
                        SelectCommand="select cast(orp.no_orden as varchar) as no_orden,orp.no_siniestro,orp.no_poliza,m.descripcion+' '+tu.descripcion+' '+tv.descripcion as descripcion,cast(v.modelo as varchar) as modelo,upper(v.color_ext) as color_ext,orp.placas,po.descripcion as perfil,l.descripcion as localizacion,C.razon_social,convert(varchar,so.f_recepcion,126)as fecha_Ingreso
                            ,convert(varchar, isnull(so.f_prog_retorno_tran, '1900-01-01'), 126) as f_prog_retorno_tran,
                            convert(varchar, isnull(so.f_retorno_transito, '1900-01-01'), 126) as f_retorno_transito,
                            (select isnull((select top 1 boc.observacion from Bitacora_Orden_Comentarios boc where boc.no_orden = orp.no_orden and boc.id_empresa = orp.id_empresa and boc.id_taller = orp.id_taller order by boc.id_observacion desc), ''))as observacion,cast(orp.fase_orden as varchar)as fase_orden,cast(c.id_cliprov as varchar)as id_cliprov
                            from Ordenes_Reparacion orp
                            left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa = so.id_empresa and orp.id_taller = so.id_taller
                            left join Vehiculos v on v.id_marca = orp.id_marca and v.id_tipo_vehiculo = orp.id_tipo_vehiculo and v.id_tipo_unidad = orp.id_tipo_unidad and v.id_vehiculo = orp.id_vehiculo
                            left join Marcas m on m.id_marca = orp.id_marca
                            left join Tipo_Vehiculo tv on tv.id_tipo_vehiculo = orp.id_tipo_vehiculo
                            left join Tipo_Unidad tu on tu.id_marca = orp.id_marca and tu.id_tipo_vehiculo = orp.id_tipo_vehiculo and tu.id_tipo_unidad = orp.id_tipo_unidad
                            left join Localizaciones l on l.id_localizacion = orp.id_localizacion
                            left join Cliprov c on c.id_cliprov = orp.id_cliprov and c.tipo = orp.tipo_cliprov and c.tipo = 'C'
                            left join PerfilesOrdenes po on po.id_perfilOrden = orp.id_perfilOrden
                            where orp.id_perfilOrden = 2 and orp.id_empresa = @id_empresa and orp.id_taller = @id_taller and orp.status_orden = 'A'">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"
                        SelectCommand="select cast(orp.no_orden as varchar) as no_orden,orp.no_siniestro,orp.no_poliza,m.descripcion+' '+tu.descripcion+' '+tv.descripcion as descripcion,cast(v.modelo as varchar) as modelo,upper(v.color_ext) as color_ext,orp.placas,po.descripcion as perfil,l.descripcion as localizacion,C.razon_social,convert(varchar,so.f_recepcion,126)as fecha_Ingreso
                            ,convert(varchar, isnull(so.f_prog_retorno_tran, '1900-01-01'), 126) as f_prog_retorno_tran,
                            convert(varchar, isnull(so.f_retorno_transito, '1900-01-01'), 126) as f_retorno_transito,
                            (select isnull((select top 1 boc.observacion from Bitacora_Orden_Comentarios boc where boc.no_orden = orp.no_orden and boc.id_empresa = orp.id_empresa and boc.id_taller = orp.id_taller order by boc.id_observacion desc), ''))as observacion,cast(orp.fase_orden as varchar)as fase_orden,cast(c.id_cliprov as varchar)as id_cliprov
                            from Ordenes_Reparacion orp
                            left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa = so.id_empresa and orp.id_taller = so.id_taller
                            left join Vehiculos v on v.id_marca = orp.id_marca and v.id_tipo_vehiculo = orp.id_tipo_vehiculo and v.id_tipo_unidad = orp.id_tipo_unidad and v.id_vehiculo = orp.id_vehiculo
                            left join Marcas m on m.id_marca = orp.id_marca
                            left join Tipo_Vehiculo tv on tv.id_tipo_vehiculo = orp.id_tipo_vehiculo
                            left join Tipo_Unidad tu on tu.id_marca = orp.id_marca and tu.id_tipo_vehiculo = orp.id_tipo_vehiculo and tu.id_tipo_unidad = orp.id_tipo_unidad
                            left join Localizaciones l on l.id_localizacion = orp.id_localizacion
                            left join Cliprov c on c.id_cliprov = orp.id_cliprov and c.tipo = orp.tipo_cliprov and c.tipo = 'C'
                            left join PerfilesOrdenes po on po.id_perfilOrden = orp.id_perfilOrden
                            where orp.id_perfilOrden = 2 and so.f_prog_retorno_tran =@fecha and orp.id_empresa = @id_empresa and orp.id_taller = @id_taller and orp.status_orden = 'A'">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"
                        SelectCommand="select cast(orp.no_orden as varchar) as no_orden,orp.no_siniestro,orp.no_poliza,m.descripcion+' '+tu.descripcion+' '+tv.descripcion as descripcion,cast(v.modelo as varchar) as modelo,upper(v.color_ext) as color_ext,orp.placas,po.descripcion as perfil,l.descripcion as localizacion,C.razon_social,convert(varchar,so.f_recepcion,126)as fecha_Ingreso,
                            convert(varchar, isnull(so.f_prog_retorno_tran, '1900-01-01'), 126) as f_prog_retorno_tran,
                            convert(varchar, isnull(so.f_retorno_transito, '1900-01-01'), 126) as f_retorno_transito,
                            (select isnull((select top 1 boc.observacion from Bitacora_Orden_Comentarios boc where boc.no_orden = orp.no_orden and boc.id_empresa = orp.id_empresa and boc.id_taller = orp.id_taller order by boc.id_observacion desc), ''))as observacion,cast(orp.fase_orden as varchar)as fase_orden,cast(c.id_cliprov as varchar)as id_cliprov
                            from Ordenes_Reparacion orp
                            left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa = so.id_empresa and orp.id_taller = so.id_taller
                            left join Vehiculos v on v.id_marca = orp.id_marca and v.id_tipo_vehiculo = orp.id_tipo_vehiculo and v.id_tipo_unidad = orp.id_tipo_unidad and v.id_vehiculo = orp.id_vehiculo
                            left join Marcas m on m.id_marca = orp.id_marca
                            left join Tipo_Vehiculo tv on tv.id_tipo_vehiculo = orp.id_tipo_vehiculo
                            left join Tipo_Unidad tu on tu.id_marca = orp.id_marca and tu.id_tipo_vehiculo = orp.id_tipo_vehiculo and tu.id_tipo_unidad = orp.id_tipo_unidad
                            left join Localizaciones l on l.id_localizacion = orp.id_localizacion
                            left join Cliprov c on c.id_cliprov = orp.id_cliprov and c.tipo = orp.tipo_cliprov and c.tipo = 'C'
                            left join PerfilesOrdenes po on po.id_perfilOrden = orp.id_perfilOrden
                            where orp.id_perfilOrden = 2 and so.f_prog_retorno_tran <@fecha and orp.id_empresa = @id_empresa and orp.id_taller = @id_taller and orp.status_orden = 'A'">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"
                        SelectCommand="select cast(orp.no_orden as varchar) as no_orden,orp.no_siniestro,orp.no_poliza,m.descripcion+' '+tu.descripcion+' '+tv.descripcion as descripcion,cast(v.modelo as varchar) as modelo,upper(v.color_ext) as color_ext,orp.placas,po.descripcion as perfil,l.descripcion as localizacion,C.razon_social,convert(varchar,so.f_recepcion,126)as fecha_Ingreso,
                            convert(varchar, isnull(so.f_prog_retorno_tran, '1900-01-01'), 126) as f_prog_retorno_tran,
                            convert(varchar, isnull(so.f_retorno_transito, '1900-01-01'), 126) as f_retorno_transito,
                            (select isnull((select top 1 boc.observacion from Bitacora_Orden_Comentarios boc where boc.no_orden = orp.no_orden and boc.id_empresa = orp.id_empresa and boc.id_taller = orp.id_taller order by boc.id_observacion desc), ''))as observacion,cast(orp.fase_orden as varchar)as fase_orden,cast(c.id_cliprov as varchar)as id_cliprov
                            from Ordenes_Reparacion orp
                            left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa = so.id_empresa and orp.id_taller = so.id_taller
                            left join Vehiculos v on v.id_marca = orp.id_marca and v.id_tipo_vehiculo = orp.id_tipo_vehiculo and v.id_tipo_unidad = orp.id_tipo_unidad and v.id_vehiculo = orp.id_vehiculo
                            left join Marcas m on m.id_marca = orp.id_marca
                            left join Tipo_Vehiculo tv on tv.id_tipo_vehiculo = orp.id_tipo_vehiculo
                            left join Tipo_Unidad tu on tu.id_marca = orp.id_marca and tu.id_tipo_vehiculo = orp.id_tipo_vehiculo and tu.id_tipo_unidad = orp.id_tipo_unidad
                            left join Localizaciones l on l.id_localizacion = orp.id_localizacion
                            left join Cliprov c on c.id_cliprov = orp.id_cliprov and c.tipo = orp.tipo_cliprov and c.tipo = 'C'
                            left join PerfilesOrdenes po on po.id_perfilOrden = orp.id_perfilOrden
                            where orp.id_perfilOrden = @id_perfilOrden and orp.id_empresa = @id_empresa and orp.id_taller = @id_taller and orp.status_orden = 'A' and orp.id_cliprov=@id_cliprov">
                    </asp:SqlDataSource>
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
