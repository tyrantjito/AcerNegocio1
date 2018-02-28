<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalendarioPersonal.aspx.cs" Inherits="CalendarioPersonal" MasterPageFile="~/AdmOrdenes.master" Culture="es-Mx" UICulture="es-Mx"  %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">  

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="page-header">		                                
		<!-- /BREADCRUMBS -->
		<div class="clearfix">
            <h3 class="content-title pull-left">Calendarizaci&oacute;n</h3>             			                                
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
                <asp:Panel ID="Panel1" runat="server" CssClass="col-lg-4 col-sm-4">
                    <div class="col-lg-12 col-sm-12 text-center">
                        <asp:Label ID="Label9" runat="server" Text="Buscar:"></asp:Label>
                        <asp:TextBox ID="txtBuscaOperario" runat="server" CssClass="input-medium"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtBuscaOperario_TextBoxWatermarkExtender" runat="server" BehaviorID="txtBuscaOperario_TextBoxWatermarkExtender" TargetControlID="txtBuscaOperario" WatermarkCssClass="water input-medium" WatermarkText="Buscar . . ." />
                        <asp:LinkButton ID="lnkBuscarOpe" runat="server" CssClass="btn btn-info t14" onclick="lnkBuscarOpe_Click"  ><i class="fa fa-search"></i></asp:LinkButton>
                        <asp:LinkButton ID="hpkLimpiar" runat="server" Font-Size="8pt" onclick="hpkLimpiar_Click">Limpiar Búsqueda</asp:LinkButton>
                    </div>
                    <div class="col-lg-12 col-sm-12">
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                CssClass="table table-bordered" DataKeyNames="IdEmp" AllowPaging="True" 
                                AllowSorting="True" PageSize="7"
                                DataSourceID="SqlDataSource1">
                                <Columns>
                                    <asp:BoundField DataField="IdEmp" HeaderText="IdEmp" ReadOnly="True" SortExpression="IdEmp" Visible="false" />
                                    <asp:BoundField DataField="LLave_nombre_empleado" HeaderText="Operario" SortExpression="LLave_nombre_empleado" />
                                    <asp:BoundField DataField="Puesto" HeaderText="Puesto" SortExpression="Puesto" Visible="false" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Puesto" SortExpression="descripcion" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSeleccionar" runat="server" CausesValidation="False" 
                                                CommandName="Select" ToolTip="Seleccionar" CssClass="btn btn-success t14" 
                                                CommandArgument='<%# Eval("IdEmp")+";"+Eval("LLave_nombre_empleado") %>' 
                                                onclick="lnkSeleccionar_Click"><i class="fa fa-chevron-right"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle CssClass="alert-success" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:Taller %>" 
                                SelectCommand="SELECT Empleados.IdEmp, Empleados.LLave_nombre_empleado, Empleados.Puesto, Puestos.descripcion FROM Empleados LEFT OUTER JOIN Puestos ON Puestos.id_puesto = Empleados.Puesto where Empleados.status_empleado <>'B' and Empleados.tipo_empleado in ('EX','IN')">
                            </asp:SqlDataSource>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server" CssClass="col-lg-8 col-sm-8">
                    <div class="col-lg-12 col-sm-12 text-center t14 textoBold alert-info">
                        <asp:Label ID="lblOrdAsig" runat="server" ></asp:Label>
                        <asp:Label ID="lblIdOper" runat="server" Visible="false"></asp:Label>
                    </div>
                    <asp:Panel ID="Panel3" runat="server" CssClass="col-lg-12 col-sm-12" Visible="false">
                        <asp:Calendar ID="Calendar1" runat="server" ondayrender="Calendar1_DayRender" 
                            onvisiblemonthchanged="Calendar1_VisibleMonthChanged" 
                            onselectionchanged="Calendar1_SelectionChanged"></asp:Calendar>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="Panel5" runat="server" CssClass="col-lg-12 col-sm-12" Visible="true">
                    <div class="col-lg-12 col-sm-12 text-center t14 textoBold alert-info">
                        <asp:Label ID="lblFecha" runat="server" ></asp:Label>                        
                    </div>
                    <div class="col-lg-12 col-sm-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass = "table table-bordered" EmptyDataRowStyle-CssClass="errores" EmptyDataText="No existen ordenes asignadas en la fecha indicada">
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
                    </div>
                </asp:Panel>
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
