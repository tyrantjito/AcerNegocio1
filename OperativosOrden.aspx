<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OperativosOrden.aspx.cs" Inherits="OperativosOrden" MasterPageFile="~/AdmOrdenes.master" Culture="es-Mx" UICulture="es-Mx" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">  
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>
    <div class="page-header">		                                
		<!-- /BREADCRUMBS -->
		<div class="clearfix">
            <h3 class="content-title pull-left">Operativos - Orden</h3> 
            <asp:Label ID="lblError" runat="server" CssClass="alert-danger"></asp:Label>			                                
		</div>
	</div>
    
        <div class="row">
            <div class="col-lg-12 col-sm-12 text-right">
                <asp:LinkButton ID="lnkRegresarOrdenes" runat="server" OnClick="lnkRegresarOrdenes_Click" CssClass="btn btn-info t14"><i class="fa fa-reply">&nbsp;&nbsp;</i><i class="fa fa-th-list"></i>&nbsp;<span>Órdenes</span></asp:LinkButton>                        
            </div>              
        </div>                
        <asp:Panel ID="Panel4" runat="server" CssClass="panelCatalogos col-lg-12 col-sm-12 text-center" ScrollBars="Auto">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-sm-12 text-center">
                <asp:Label ID="lblErroresOO" runat="server" CssClass="errores alert-danger"></asp:Label>
            </div>   
            <asp:Panel ID="Panel1" runat="server" CssClass="col-lg-12 col-sm-12 text-center">
                 <div class="col-lg-12 col-sm-12 text-center">
                    <asp:Label ID="Label9" runat="server" Text="Buscar:"></asp:Label>
                    <asp:TextBox ID="txtBuscaOperario" runat="server" CssClass="input-medium"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtBuscaOperario_TextBoxWatermarkExtender" runat="server" BehaviorID="txtBuscaOperario_TextBoxWatermarkExtender" TargetControlID="txtBuscaOperario" WatermarkCssClass="water input-medium" WatermarkText="Buscar . . ." />
                    <asp:LinkButton ID="lnkBuscarOpe" runat="server" CssClass="btn btn-info t14" onclick="lnkBuscarOpe_Click"  ><i class="fa fa-search"></i></asp:LinkButton>
                    <asp:LinkButton ID="hpkLimpiar" runat="server" Font-Size="8pt" onclick="hpkLimpiar_Click">Limpiar Búsqueda</asp:LinkButton>
                </div>
                <div class="col-lg-12 col-sm-12 tex-center">
                    <div class="table-responsive">
                        <asp:GridView ID="GridOperarios" runat="server" AutoGenerateColumns="False" EmptyDataText="No existen operarios registrados" EmptyDataRowStyle-CssClass="errores"
                            DataSourceID="SqlDataSourceOperadoresRH" OnRowDataBound="GridOperarios_RowDataBound" DataKeyNames="IdEmp"
                            CssClass="table table-bordered" AllowPaging="True" AllowSorting="True" PageSize="7">
                            <Columns>
                                <asp:TemplateField HeaderText="Operarios Disponibles" SortExpression="nombre">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("nombre") %>' ID="lblAsigOperMO" />
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:BoundField DataField="clv_pichonera" ReadOnly="True" SortExpression="clv_pichonera" HeaderText="Cajones"/>  
                                <asp:BoundField DataField="resta" ReadOnly="True" SortExpression="resta" HeaderText="Disponibles"/>                                                           
                                <asp:TemplateField Visible="false" HeaderText="clv_pichonera" SortExpression="clv_pichonera">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("clv_pichonera") %>' ID="lblPichonera"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDetalleOperario" runat="server" ToolTip="Asignaciones" 
                                            CommandArgument='<%# Eval("IdEmp")+";"+Eval("nombre") %>' 
                                            CssClass="btn btn-info t14" onclick="lnkDetalleOperario_Click"><i class="fa fa-plus-circle"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>                                    
                            </Columns>
                            <EditRowStyle CssClass="alert-warning" />
                            <EmptyDataRowStyle CssClass="errores" />
                        </asp:GridView>
                        <asp:SqlDataSource runat="server" ID="SqlDataSourceOperadoresRH" ConnectionString='<%$ ConnectionStrings:Taller %>'
                            SelectCommand="select (rtrim(ltrim(e.Nombres))+' '+rtrim(ltrim(e.Apellido_Paterno))+' '+isnull(rtrim(ltrim(e.Apellido_Materno)),'')) as nombre,e.clv_pichonera,e.IdEmp,(e.clv_pichonera - (select count(*) from Operativos_Orden oo where oo.IdEmp=e.IdEmp and oo.estatus<>'T'))as resta from Empleados e where e.status_empleado!='B' and e.tipo_empleado in ('EX','IN') order by resta desc">
                        </asp:SqlDataSource>
                    </div>
                </div>
            </asp:Panel>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <asp:Panel ID="pnlMaskLoad" runat="server" CssClass="maskLoad"></asp:Panel>
                <asp:Panel ID="pnlCargando" runat="server" CssClass="pnlPopUpLoad">
                    <asp:Image ID="imgLoad" runat="server" ImageUrl="~/img/loading.gif" CssClass="ancho100" />                            
                </asp:Panel>
            </ProgressTemplate>                            
        </asp:UpdateProgress>
     </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel2" runat="server" CssClass="col-lg-12 col-sm-12 text-center">
                <div class="col-lg-12 col-sm-12 text-center textoBold alert-info">
                    <asp:Label ID="lblOperario" runat="server"></asp:Label>
                    <asp:Label ID="lblIdOperario" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="col-lg-12 col-sm-12 text-center">
                    <div class="table-responsive">
                        <asp:GridView ID="grdOrdenes" runat="server" AllowPaging="True" PageSize="7" CssClass="table table-bordered"
                            AllowSorting="True" AutoGenerateColumns="False" 
                            DataSourceID="SqlDataSource1" EmptyDataRowStyle-CssClass="errores" 
                            onrowdatabound="grdOrdenes_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="no_orden" HeaderText="Orden" SortExpression="no_orden" ReadOnly="true" />
                                <asp:BoundField DataField="id_asignacion" HeaderText="id_asignacion" SortExpression="id_asignacion" Visible="false" />
                                <asp:TemplateField HeaderText="Inicio Programado" SortExpression="fecha_ini_prog">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("fecha_ini_prog")+" "+Eval("hora_ini_prog") %>'></asp:Label>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inicio" SortExpression="fecha_ini">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIni" runat="server" Text='<%# Eval("fecha_ini")+" "+Eval("hora_ini") %>'></asp:Label> 
                                    </ItemTemplate> 
                                    <EditItemTemplate>
                                        <asp:Label ID="lblIniMod" runat="server" Text='<%# Eval("fecha_ini")+" "+Eval("hora_ini") %>'></asp:Label> 
                                        <asp:TextBox ID="txtFechaIni" runat="server" CssClass="alingMiddle input-small" Enabled="false" Text='<%# Eval("fecha_ini") %>'></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFechaIni_CalendarExtender" runat="server" BehaviorID="txtFechaIni_CalendarExtender" TargetControlID="txtFechaIni" Format="yyyy-MM-dd" PopupButtonID="lnkFechaIni" />
                                        <asp:LinkButton ID="lnkFechaIni" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>&nbsp;&nbsp;                                        
                                        <asp:TextBox ID="txtHoraIni" runat="server" CssClass="alingMiddle input-small" MaxLength="8" Text='<%# Eval("hora_ini") %>'></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="txtHoraIni_TextBoxWatermarkExtender" runat="server" BehaviorID="txtHoraIni_TextBoxWatermarkExtender" TargetControlID="txtHoraIni" WatermarkText="hh:mm:ss" WatermarkCssClass="water input-small" />
                                        <cc1:FilteredTextBoxExtender ID="txtHoraIni_FilteredTextBoxExtender" runat="server" BehaviorID="txtHoraIni_FilteredTextBoxExtender" TargetControlID="txtHoraIni" FilterType="Numbers, Custom" ValidChars=":" />
                                        <asp:LinkButton ID="lnkCancelIni" runat="server" CssClass="btn btn-danger t14"  
                                            CommandArgument='<%# Eval("id_asignacion")+";"+Eval("estatus")+";"+Eval("idEmp")+";"+Eval("no_orden") %>' 
                                            onclick="lnkCancelIni_Click"><i class="fa fa-close"></i></asp:LinkButton>
                                    </EditItemTemplate>                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Termino" SortExpression="fecha_fin">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFin" runat="server" Text='<%# Eval("fecha_fin")+" "+Eval("hora_fin") %>'></asp:Label>                                                                                
                                    </ItemTemplate> 
                                    <EditItemTemplate>
                                        <asp:Label ID="lblFinMod" runat="server" Text='<%# Eval("fecha_fin")+" "+Eval("hora_fin") %>'></asp:Label>                                                                                
                                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="alingMiddle input-small" Enabled="false" Text='<%# Eval("fecha_fin") %>'></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFechaFin_CalendarExtender" runat="server" BehaviorID="txtFechaFin_CalendarExtender" TargetControlID="txtFechaFin" Format="yyyy-MM-dd" PopupButtonID="lnkFechaFin" />
                                        <asp:LinkButton ID="lnkFechaFin" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>&nbsp;&nbsp;                                        
                                        <asp:TextBox ID="txtHoraFin" runat="server" CssClass="alingMiddle input-small" MaxLength="8" Text='<%# Eval("hora_fin") %>'></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="txtHoraFin_TextBoxWatermarkExtender" runat="server" BehaviorID="txtHoraFin_TextBoxWatermarkExtender" TargetControlID="txtHoraFin" WatermarkText="hh:mm:ss" WatermarkCssClass="water input-small" />
                                        <cc1:FilteredTextBoxExtender ID="txtHoraFin_FilteredTextBoxExtender" runat="server" BehaviorID="txtHoraFin_FilteredTextBoxExtender" TargetControlID="txtHoraFin" FilterType="Numbers, Custom" ValidChars=":" />
                                        <asp:LinkButton ID="lnkCancelFin" runat="server" CssClass="btn btn-danger t14"  CommandArgument='<%# Eval("id_asignacion")+";"+Eval("estatus")+";"+Eval("idEmp")+";"+Eval("no_orden") %>' onclick="lnkCancelFin_Click"><i class="fa fa-close"></i></asp:LinkButton>
                                    </EditItemTemplate>                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Monto" SortExpression="monto">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonto" runat="server" Text='<%# Eval("monto") %>'></asp:Label>                                          
                                    </ItemTemplate>  
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtMonto" runat="server" Text='<%# Eval("monto") %>' CssClass="input-small"></asp:TextBox> 
                                        <cc1:FilteredTextBoxExtender ID="txtMonto_FilteredTextBoxExtender" runat="server" BehaviorID="txtMonto_FilteredTextBoxExtender" TargetControlID="txtMonto" FilterType="Numbers,Custom" ValidChars="." />
                                        <cc1:TextBoxWatermarkExtender ID="txtMonto_TextBoxWatermarkExtender" runat="server" BehaviorID="txtMonto_TextBoxWatermarkExtender" TargetControlID="txtMonto" WatermarkText="Monto" WatermarkCssClass="water input-small" />
                                    </EditItemTemplate>                                  
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Observaciones" SortExpression="oservaciones">
                                    <ItemTemplate>
                                        <asp:Label ID="lblObs" runat="server" Text='<%# Eval("oservaciones") %>'></asp:Label>  
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtObservaciones" runat="server" Text='<%# Eval("oservaciones") %>' TextMode="MultiLine" Rows="6" CssClass="textNota"></asp:TextBox>                                        
                                        <cc1:TextBoxWatermarkExtender ID="txtObservaciones_TextBoxWatermarkExtender" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="water textNota" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="estatus" HeaderText="estatus" SortExpression="estatus" Visible="False" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="False" CommandArgument='<%# Eval("id_asignacion") %>' CommandName="Edit" CssClass="btn btn-warning" ><i class="fa fa-edit"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnkActualizar" runat="server" CausesValidation="True" CommandArgument='<%# Eval("id_asignacion")+";"+Eval("estatus")+";"+Eval("idEmp")+";"+Eval("no_orden") %>' CommandName="Update" CssClass="btn btn-success" onclick="lnkActualizar_Click"><i class="fa fa-save"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkCancelar" runat="server" CausesValidation="False" CommandArgument='<%# Eval("id_asignacion") %>' CommandName="Cancel" CssClass="btn btn-danger"><i class="fa fa-close"></i></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnkAsignar" runat="server" CausesValidation="False" 
                                            CommandArgument='<%# Eval("id_asignacion")+";"+Eval("estatus")+";"+Eval("idEmp")+";"+Eval("no_orden") %>' 
                                            CommandName="Asig" CssClass="btn btn-info" onclick="lnkAsignar_Click" ><i class="fa fa-user-plus"></i></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="errores" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand = "select no_orden,id_asignacion,idEmp,Convert(char(10),fecha_ini_prog,126) as fecha_ini_prog,convert(char(8),hora_ini_prog,108) as hora_ini_prog,convert(char(10),fecha_ini,126) as fecha_ini,convert(char(8),hora_ini,108) as hora_ini,convert(char(10),fecha_fin,126) as fecha_fin,convert(char(8),hora_fin,108) as hora_fin,monto,oservaciones,estatus from Operativos_Orden where id_empresa=@id_empresa and id_taller=@id_taller and IdEmp=@idEmp order by no_orden desc, fecha_ini_prog desc, hora_ini_prog desc">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="id_empresa" DefaultValue="0" QueryStringField="e" />
                                <asp:QueryStringParameter Name="id_taller" DefaultValue="0" QueryStringField="t" />
                                <asp:ControlParameter Name="idEmp" DefaultValue="0" ControlID="lblIdOperario" PropertyName="Text" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
            </asp:Panel>

        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
            <ProgressTemplate>
                <asp:Panel ID="pnlMaskLoad1" runat="server" CssClass="maskLoad"></asp:Panel>
                <asp:Panel ID="pnlCargando1" runat="server" CssClass="pnlPopUpLoad">
                    <asp:Image ID="imgLoad1" runat="server" ImageUrl="~/img/loading.gif" CssClass="ancho100" />                            
                </asp:Panel>
            </ProgressTemplate>                            
        </asp:UpdateProgress>
     </ContentTemplate>
    </asp:UpdatePanel>
        </asp:Panel>



        
       
</asp:Content>