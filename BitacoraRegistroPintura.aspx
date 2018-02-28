<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="BitacoraRegistroPintura.aspx.cs" Inherits="BitacoraRegistroPintura" Culture="es-Mx" UICulture="es-Mx" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

 

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">      
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server"></telerik:RadSkinManager>
    <div class="page-header">		                                
		<!-- /BREADCRUMBS -->
		<div class="clearfix">
            <h3 class="content-title pull-left">Bit&aacute;cora Reg. Pinturas</h3>             			                                
		</div>
	</div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>  
            <div class="row"> 
                <div class="col-lg-3 col-sm-3 text-center">
                    <asp:Label ID="Label2" runat="server" Text="Fecha Inicial:"></asp:Label>
                    <asp:TextBox ID="txtFechaIni" runat="server" CssClass="alingMiddle input-small" Enabled="false"  ></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="txtFechaIni_CalendarExtender" TargetControlID="txtFechaIni" Format="yyyy-MM-dd" PopupButtonID="lnkFechaIni" />
                    <asp:LinkButton ID="lnkFechaIni" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                </div>
                <div class="col-lg-3 col-sm-3 text-center">
                    <asp:Label ID="Label3" runat="server" Text="Fecha Inicial:"></asp:Label>
                    <asp:TextBox ID="txtFechaFin" runat="server" CssClass="alingMiddle input-small" Enabled="false"  ></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" BehaviorID="txtFechaFin_CalendarExtender" TargetControlID="txtFechaFin" Format="yyyy-MM-dd" PopupButtonID="lnkFechaFin" />
                    <asp:LinkButton ID="lnkFechaFin" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                </div>
                 <div class="col-lg-5 col-sm-5 text-center">
                     <asp:Label ID="Label4" runat="server" Text="Estatus:"></asp:Label>
                     <asp:RadioButtonList ID="rbdEstatus" runat="server" CellPadding="5" CellSpacing="5" RepeatDirection="Horizontal" RepeatColumns="3" >
                         <asp:ListItem Value="S" Text="Solicitado"></asp:ListItem>
                         <asp:ListItem Value="T" Text="Terminado"></asp:ListItem>                         
                         <asp:ListItem Value="C" Text="Cancelados"></asp:ListItem>
                     </asp:RadioButtonList>
                 </div>                                        
                <div class="col-lg-1 col-sm-1 text-center">
                    <asp:LinkButton ID="lnkBuscar" runat="server" CssClass="btn btn-info t14" OnClick="lnkBuscar_Click"><i class="fa fa-search"></i><span>&nbsp;Buscar</span></asp:LinkButton>
                </div>
            </div>                     
            <asp:Panel ID="Panel4" runat="server" CssClass="panelCatalogos text-center" ScrollBars="Auto" >
                <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" DataSourceID="SqlDataSource2" OnItemDataBound="RadGrid1_ItemDataBound" Skin="Metro"
                    runat="server" AutoGenerateColumns="False" AllowSorting="True"
                    AllowPaging="True" PageSize="50" GridLines="None">
                    <PagerStyle Mode="NumericPages"></PagerStyle>
                    <MasterTableView DataSourceID="SqlDataSource2" DataKeyNames="id_solicitud">                        
                        <Columns>
                            <telerik:GridTemplateColumn DataField="no_orden" HeaderText="Orden" UniqueName="no_orden" SortExpression="no_orden">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnOrden" runat="server" Text='<%# Bind("no_orden") %>' CommandArgument='<%# Bind("fase_orden") %>' OnClick="btnOrden_Click" CssClass="btn btn-info textoBold"></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn SortExpression="descripcion" HeaderText="Vehículo" DataField="descripcion" UniqueName="descripcion"/>
                            <telerik:GridBoundColumn SortExpression="modelo" HeaderText="Modelo" DataField="modelo" UniqueName="modelo"/>
                            <telerik:GridBoundColumn SortExpression="placas" HeaderText="Placas" DataField="placas" UniqueName="placas"/>
                            <telerik:GridBoundColumn SortExpression="color_ext" HeaderText="Color" DataField="color_ext" UniqueName="color_ext"/>
                            <telerik:GridBoundColumn SortExpression="f_recepcion" HeaderText="Ingreso" DataField="f_recepcion" UniqueName="f_recepcion" DataFormatString="{0:yyyy-MM-dd}"/>
                            <telerik:GridBoundColumn SortExpression="no_siniestro" HeaderText="Siniestro" DataField="no_siniestro" UniqueName="no_siniestro"/>
                            <telerik:GridBoundColumn SortExpression="localizacion" HeaderText="Localización" DataField="localizacion" UniqueName="localizacion"/>
                            <telerik:GridBoundColumn SortExpression="perfil" HeaderText="Perfil" DataField="perfil" UniqueName="perfil"/>
                            <telerik:GridBoundColumn SortExpression="folio_solicitud" HeaderText="Folio" DataField="folio_solicitud" UniqueName="folio_solicitud"/>
                            <telerik:GridBoundColumn SortExpression="desc_solicitud" HeaderText="Observaciones" DataField="desc_solicitud" UniqueName="desc_solicitud"/>
                            <telerik:GridBoundColumn SortExpression="detalle" HeaderText="Detalle o Identificador" DataField="detalle" UniqueName="detalle"/>
                            <telerik:GridBoundColumn SortExpression="fecha_solicitud" HeaderText="Fecha Solicitud" DataField="fecha_solicitud" UniqueName="fecha_solicitud"/>
                            <telerik:GridBoundColumn SortExpression="fecha_entrega" HeaderText="Fecha Entrega" DataField="fecha_entrega" UniqueName="fecha_entrega"/>
                            <telerik:GridBoundColumn SortExpression="dias_igualacion" HeaderText="Días Igualación" DataField="dias_igualacion" UniqueName="dias_igualacion"/>                            
                            <telerik:GridBoundColumn SortExpression="estatusSolicitud" HeaderText="Estatus" DataField="estatusSolicitud" UniqueName="estatusSolicitud"/>
                        </Columns>
                        <NoRecordsTemplate>
                            <asp:Label ID="lblEmpty" runat="server" Text="No existen registros de pintura registrados" CssClass="errores"></asp:Label>
                        </NoRecordsTemplate>
                    </MasterTableView>
                </telerik:RadGrid>

                 <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" 
                        SelectCommand="select orp.no_orden,tv.descripcion+' '+m.descripcion+' '+tu.descripcion as descripcion,upper(v.color_ext) as color_ext,orp.placas,l.descripcion as localizacion,C.razon_social,orp.fase_orden, so.f_recepcion, orp.no_siniestro,v.modelo,po.descripcion as perfil,
isnull((select ((case datos_orden when 1 then 1 else 0 end) +(case inventario when 1 then 1 else 0 end) +(case caracteristicas_vehiculo when 1 then 1 else 0 end) )  from control_procesos where id_empresa=orp.id_empresa and id_taller=orp.id_taller and no_orden=orp.no_orden),0) as procesos
,convert(varchar(10),rp.fecha_solicitud,120)as fecha_solicitud,convert(varchar(10),rp.fecha_entrega,120)as fecha_entrega,rp.dias_igualacion as dias_igualacion,case rp.estatus when 'S' then 'Solicitado' when 'C' then 'Cancelado' when 'T' then 'Terminado' else '' end as estatusSolicitud,
rp.folio_solicitud,rp.estatus,rp.detalle,rp.desc_solicitud,rp.id_solicitud,rp.ano
from Ordenes_Reparacion orp
left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa = so.id_empresa and orp.id_taller = so.id_taller
left join Vehiculos v on v.id_marca = orp.id_marca and v.id_tipo_vehiculo = orp.id_tipo_vehiculo and v.id_tipo_unidad = orp.id_tipo_unidad and v.id_vehiculo = orp.id_vehiculo
left join Marcas m on m.id_marca = orp.id_marca
left join Tipo_Vehiculo tv on tv.id_tipo_vehiculo = orp.id_tipo_vehiculo
left join Tipo_Unidad tu on tu.id_marca = orp.id_marca and tu.id_tipo_vehiculo = orp.id_tipo_vehiculo and tu.id_tipo_unidad = orp.id_tipo_unidad
left join Localizaciones l on l.id_localizacion = orp.id_localizacion
left join Cliprov c on c.id_cliprov = orp.id_cliprov and c.tipo = orp.tipo_cliprov and c.tipo = 'C'
left join PerfilesOrdenes po on po.id_perfilOrden = orp.id_perfilOrden
inner join Registro_Pinturas rp on rp.no_orden = orp.no_orden and rp.id_empresa = orp.id_empresa and rp.id_taller = orp.id_taller and rp.estatus!='C'
where orp.id_empresa= @id_empresa  and orp.id_taller= @id_taller and rp.estatus =@estatus and rp.fecha_solicitud between @fechaIni and @fechaFin order by rp.fecha_solicitud desc">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="id_empresa" QueryStringField="e" DefaultValue="0" />
                            <asp:QueryStringParameter Name="id_taller" QueryStringField="t" DefaultValue="0" />
                            <asp:ControlParameter Name="estatus" ControlID="rbdEstatus" PropertyName="SelectedValue" Type="Char" DefaultValue="S" />
                            <asp:ControlParameter Name="fechaIni" ControlID="txtFechaIni" PropertyName="Text" />
                            <asp:ControlParameter Name="fechaFin" ControlID="txtFechaFin" PropertyName="Text"  />
                        </SelectParameters>
                    </asp:SqlDataSource>
            </asp:Panel>
                
                
                 
                                                

            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <asp:Panel ID="pnlMaskLoad1" runat="server" CssClass="maskLoad"></asp:Panel>
                    <asp:Panel ID="pnlCargando1" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoad1" runat="server" ImageUrl="~/img/loading.gif" CssClass="ancho100" />                            
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>