<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BitacoraLocalizacion.aspx.cs" Inherits="BitacoraLocalizacion" MasterPageFile="~/AdmOrdenes.master"  Culture="es-Mx" UICulture="es-Mx"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">  
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="page-header">		                                
		<!-- /BREADCRUMBS -->
		<div class="clearfix">
            <h3 class="content-title pull-left">Bit&aacute;cora de Localizaci&oacute;n</h3>             			                                
		</div>
	</div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>           
            <div class="row">                
                <div class="col-lg-10 col-sm-10 text-center">
                    <asp:TextBox ID="txtNoOrden" runat="server" placeholder="No. Orden" CssClass="input-medium alingMiddle" />
                    <cc1:FilteredTextBoxExtender ID="txtNoOrden_FilteredTextBoxExtender" 
                        runat="server" BehaviorID="txtNoOrden_FilteredTextBoxExtender" 
                        TargetControlID="txtNoOrden" FilterType="Numbers"/>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnBuscarAsignaciones" runat="server" OnClick="btnBuscarAsignaciones_Click" CssClass="btn btn-info t14"><i class="fa fa-search"></i></asp:LinkButton>
                    <asp:Label ID="lblError" runat="server" CssClass="errores"></asp:Label>
                </div>                
                <div class="col-lg-2 col-sm-2 text-right">
                    <asp:LinkButton ID="lnkRegresarOrdenes" runat="server" OnClick="lnkRegresarOrdenes_Click" CssClass="btn btn-info t14"><i class="fa fa-reply">&nbsp;&nbsp;</i><i class="fa fa-th-list"></i>&nbsp;<span>Órdenes</span></asp:LinkButton>
                </div>
            </div>
            <asp:Panel ID="Panel4" runat="server" CssClass="panelCatalogos text-center" ScrollBars="Auto" Visible="false">
                <div class="row">
                    <div class="col-lg-12 col-sm-12 text-center" > 
                        <asp:Label ID="Label22" runat="server" Text="Localización:" CssClass="textoBold alingMiddle" />
                        <asp:DropDownList ID="ddlLocalizacion" runat="server" DataSourceID="SqlDataSource26" DataTextField="descripcion" DataValueField="id_localizacion" CssClass="alingMiddle input-large"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource26" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_localizacion, descripcion from Localizaciones" ></asp:SqlDataSource>
                        <asp:LinkButton ID="lnkGuarda" runat="server" ToolTip="Guarda Cambios" CssClass="btn btn-success t14" onclick="lnkGuarda_Click" ><i class="fa fa-save"></i><span>&nbsp;Guarda Cambios</span></asp:LinkButton>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" AllowPaging="true" AllowSorting="true" PageSize="7" DataSourceID="SqlDataSource1">
                        <Columns>
                            <asp:BoundField DataField="descripcion" HeaderText="Localización" SortExpression="descripcion" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" />
                            <asp:BoundField DataField="hora" HeaderText="Hora" SortExpression="hora" />
                            <asp:BoundField DataField="nombre_usuario" HeaderText="Usuario" SortExpression="nombre_usuario" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select l.descripcion,convert(char(10),b.fecha,126) as fecha,convert(char(8),b.hora,126) as hora,u.nombre_usuario from bitacora_orden_localizacion b inner join usuarios u on u.id_usuario=b.id_usuario inner join localizaciones l on l.id_localizacion=b.id_localizacion where b.id_empresa=@empresa and b.id_taller=@taller and b.no_orden=@orden order by b.id_registro desc">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="empresa" QueryStringField="e" />
                            <asp:QueryStringParameter Name="taller" QueryStringField="t" />
                            <asp:ControlParameter Name="orden" ControlID="txtNoOrden" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
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