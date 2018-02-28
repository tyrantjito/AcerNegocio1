<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BitacoraAsignaciones.aspx.cs" Inherits="BitacoraAsignaciones" MasterPageFile="~/AdmOrdenes.master" Culture="es-Mx" UICulture="es-Mx"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

 

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">  
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "img/menos.png";
            } else {
                div.style.display = "none";
                img.src = "img/plus.png";
            }
        }
</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="page-header">		                                
		<!-- /BREADCRUMBS -->
		<div class="clearfix">
            <h3 class="content-title pull-left">Bit&aacute;cora Asignaciones</h3>             			                                
		</div>
	</div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>  
            <div class="row">                                            
                <div class="col-lg-2 col-sm-2 text-center">
                    <asp:LinkButton ID="lnkRegresarOrdenes" runat="server" OnClick="lnkRegresarOrdenes_Click" CssClass="btn btn-info t14"><i class="fa fa-reply">&nbsp;&nbsp;</i><i class="fa fa-th-list"></i>&nbsp;<span>Órdenes</span></asp:LinkButton>
                </div>
            </div>                     
            <asp:Panel ID="Panel4" runat="server" CssClass="panelCatalogos text-center" ScrollBars="Auto" >
              <div class="table-responsive">
                    <asp:GridView ID="grdFechas" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="7"
                        CssClass="table table-bordered" AllowSorting="True" DataKeyNames="ingreso"
                        DataSourceID="SqlDataSource1" 
                        onrowdatabound="GridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField SortExpression="ingreso" HeaderText="Ingreso">
                                <ItemTemplate>
                                    <a href="JavaScript:divexpandcollapse('div<%# Eval("f_recepcion") %>');">
                                        <img id="imgdiv<%# Eval("f_recepcion") %>" src="img/menos.png" width="30px" height="30px" alt="" />
                                    </a>&nbsp;&nbsp;
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ingreso") %>'></asp:Label>
                                    <tr>
                                        <td colspan="100%">
                                            <div id="div<%# Eval("f_recepcion") %>" style="position:relative; left:0px; overflow:auto">
                                                <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass = "table table-bordered" onrowdatabound="gvOrders_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Orden" SortExpression="no_orden">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnOrden" runat="server" Text='<%# Bind("no_orden") %>' CommandArgument='<%# Bind("fase_orden") %>' OnClick="btnOrden_Click" CssClass="btn btn-info textoBold"></asp:LinkButton>                            
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="alto40px" />                        
                                                        </asp:TemplateField>                                                
                                                        <asp:BoundField DataField="descripcion" HeaderText="Vehículo" SortExpression="descripcion"/>
                                                        <asp:BoundField DataField="modelo" HeaderText="Modelo" SortExpression="modelo"/>
                                                        <asp:BoundField DataField="color_ext" HeaderText="Color" SortExpression="color_ext"/>
                                                        <asp:BoundField DataField="placas" HeaderText="Placas" SortExpression="placas"/>
                                                        <asp:BoundField DataField="perfil" HeaderText="Perfil" SortExpression="perfil"/>
                                                        <asp:BoundField DataField="localizacion" HeaderText="Localización" SortExpression="localizacion"/>
                                                        <asp:BoundField DataField="razon_social" HeaderText="Cliente" SortExpression="razon_social"/>                                                            
                                                        <asp:BoundField DataField="no_siniestro" HeaderText="Siniestro" SortExpression="no_siniestro"/>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select convert(char(10), s.f_recepcion,126) as ingreso,rtrim(ltrim(replace(convert(char(10), s.f_recepcion,126),'-',''))) as f_recepcion from ordenes_reparacion o inner join seguimiento_orden s on s.id_empresa=o.id_empresa and s.id_taller=o.id_taller and s.no_orden=o.no_orden where o.id_empresa=@id_empresa and o.id_taller=@id_taller group by s.f_recepcion order by s.f_recepcion desc">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="id_empresa" QueryStringField="e" DefaultValue="0" />
                            <asp:QueryStringParameter Name="id_taller" QueryStringField="t" DefaultValue="0" />
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