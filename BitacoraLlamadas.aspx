<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="BitacoraLlamadas.aspx.cs" Inherits="BitacoraLlamadas" Culture="es-Mx" UICulture="es-Mx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script type="text/javascript">
        $(function(){
            var txtFeIni = $get('<%= txtFechaIni.ClientID %>');
            var txtFeFin = $get('<%= txtFechaFin.ClientID %>');
            var calExtFeFin = $find('calExtFeFin');
            txtFeIni.contentEditable = false;
            txtFeFin.contentEditable = false;
            txtFeIni.disabled = true;
            txtFeFin.disabled = true;
        });
        
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

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>
    <div class="page-header">
        <!-- /BREADCRUMBS -->
        <div class="clearfix">
            <h3 class="content-title pull-left">Bit&aacute;cora Llamadas</h3>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
            <div class="row marTop">                
                <div class="col-lg-2 col-sm-2 text-center text-right">
                    <span>Tipo Llamada:</span>
                </div>
                <div class="col-lg-4 col-sm-4 text-left">
                    <asp:RadioButtonList ID="rbtnTipoLlamada" runat="server" CellPadding="5" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnTipoLlamada_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Pendientes" Selected="True" Value="P" />
                        <asp:ListItem Text="Entrantes" Value="E" />
                        <asp:ListItem Text="Salientes" Value="S" />
                    </asp:RadioButtonList>
                    <table class="center-block"><tbody><tr><td>
                        <asp:Label ID="lblLlamMnsj" runat="server" CssClass="text-danger"></asp:Label>
                        </td></tr></tbody>
                    </table>
                </div>
            
                <div class="col-lg-2 col-sm-2 text-left" >
                    <span>Fecha Inicial:&nbsp;</span>
                    <asp:TextBox ID="txtFechaIni" runat="server" CssClass="input-small" Enabled="false" />
                    <cc1:CalendarExtender ID="calext_txtFechaIni" runat="server" TargetControlID="txtFechaIni" Format="yyyy-MM-dd" PopupButtonID="lnkFechaIni" ClearTime="true" />
                    <asp:LinkButton ID="lnkFechaIni" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                </div>
                <div class="col-lg-2 col-sm-2 text-left">
                    <span>Fecha Final:&nbsp;</span>
                    <asp:TextBox ID="txtFechaFin" runat="server" CssClass="input-small" Enabled="false" />
                    <cc1:CalendarExtender ID="calExttxtFechaFin" runat="server" TargetControlID="txtFechaFin" EnabledOnClient="true" Format="yyyy-MM-dd" PopupButtonID="lnkFechaFin" ClearTime="true"  />
                    <asp:LinkButton ID="lnkFechaFin" runat="server" CssClass="btn btn-info t14" ><i class="fa fa-calendar"></i></asp:LinkButton> 
                                                
                </div>
                <div class="col-lg-2 col-sm-2 text-left">
                    <asp:LinkButton ID="lnkBuscaFechas" runat="server" OnClick="lnkBuscaFechas_Click" CssClass="btn btn-primary t14"><i class="fa fa-search"></i><span>&nbsp;Buscar</span></asp:LinkButton>
                </div>
            </div>

            <asp:Panel ID="Panel4" runat="server" CssClass="panelCatalogos text-center" ScrollBars="Auto">
                <div class="col-lg-12 col-sm-12">

                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" runat="server" ShowStatusBar="true" AutoGenerateColumns="False" 
                        AllowMultiRowSelection="False" OnItemDataBound="RadGrid1_ItemDataBound" Skin="Metro" ShowFooter="true"
                        OnDetailTableDataBind="RadGrid1_DetailTableDataBind" OnNeedDataSource="RadGrid1_NeedDataSource"
                        OnPreRender="RadGrid1_PreRender" CssClass="col-lg-12 col-sm-12">
                        <MasterTableView DataKeyNames="no_orden" AllowFilteringByColumn="true">
                            <DetailTables>
                                <telerik:GridTableView DataKeyNames="consecutivo" Name="Llamadas" CssClass="col-lg-12 col-sm-12" SkinID="Metro" >
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="tipo_llamada" HeaderText="Tipo" ReadOnly="True" SortExpression="tipo_llamada"></telerik:GridBoundColumn>                                        
                                        <telerik:GridBoundColumn DataField="fecha_llamada" HeaderText="Fecha Llamada" SortExpression="fecha_llamada" ReadOnly="True" DataFormatString="{0:yyy-MM-dd}"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="hora" HeaderText="Hora Llamada" SortExpression="hora" ReadOnly="True"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="cliente_llamo" HeaderText="Llamo" SortExpression="cliente_llamo" ReadOnly="True"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="contesto" HeaderText="Contesto" SortExpression="contesto" ReadOnly="True"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="atendio" HeaderText="Atendio" SortExpression="atendio" ReadOnly="True"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="responsable" HeaderText="Responsable" SortExpression="responsable" ReadOnly="True"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="telefonos" HeaderText="Telefonos" SortExpression="telefonos" ReadOnly="True"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="comentario_cliente" HeaderText="Comentario Cliente" SortExpression="comentario_cliente" ReadOnly="True"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="observaciones" HeaderText="Observaciones" SortExpression="observaciones" ReadOnly="True"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="f_promesa" HeaderText="Fecha Promesa" SortExpression="f_promesa" ReadOnly="True" DataFormatString="{0:yyy-MM-dd}"></telerik:GridBoundColumn>                                        
                                        <telerik:GridCheckBoxColumn DataField="atendida" HeaderText="Atendida, Realizada o Regresada" SortExpression="atendida" UniqueName="atendida"/>   
                                        <telerik:GridBoundColumn DataField="quienatendio" HeaderText="Atendida, Realizada o Regresada por" SortExpression="quienatendio" ReadOnly="True"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="fechaatendio" HeaderText="Fecha"  SortExpression="fechaatendio" UniqueName="fechaatendio" ReadOnly="true"  DataFormatString="{0:yyyy-MM-dd}" />  
                                        <telerik:GridBoundColumn DataField="horaatendio" HeaderText="Hora"  SortExpression="horaatendio" UniqueName="horaatendio" ReadOnly="true" />                                                                           
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <asp:Label runat="server" ID="lblvacio" Text="No existen llamadas registradas" CssClass="errores"></asp:Label>
                                    </NoRecordsTemplate>
                                </telerik:GridTableView>
                            </DetailTables>
                            <Columns>
                                <telerik:GridTemplateColumn SortExpression="no_orden" HeaderText="Orden" DataField="no_orden" AllowFiltering="true" UniqueName="no_orden" DataType="System.String">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnOrden" runat="server" Text='<%# Bind("no_orden") %>' CommandArgument='<%# Bind("fase_orden") %>' OnClick="btnOrden_Click" CssClass="btn btn-info textoBold"></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>                                                          
                                <telerik:GridBoundColumn DataField="f_recepcion" HeaderText="Ingreso" SortExpression="f_recepcion" UniqueName="f_recepcion" DataFormatString="{0:yyyy-MM-dd}" Resizable="true"/>
                                <telerik:GridBoundColumn DataField="descripcion" HeaderText="Vehículo" SortExpression="descripcion" UniqueName="descripcion" Resizable="true"/>
                                <telerik:GridBoundColumn DataField="modelo" HeaderText="Modelo" SortExpression="modelo" UniqueName="modelo" Resizable="true"/>
                                <telerik:GridBoundColumn DataField="color_ext" HeaderText="Color" SortExpression="color_ext" UniqueName="color_ext" Resizable="true"/>
                                <telerik:GridBoundColumn DataField="placas" HeaderText="Placas" SortExpression="placas" UniqueName="placas" Resizable="true"/>
                                <telerik:GridBoundColumn DataField="perfil" HeaderText="Perfil" SortExpression="perfil" UniqueName="perfil" Resizable="true"/>
                                <telerik:GridBoundColumn DataField="localizacion" HeaderText="Localización" SortExpression="localizacion" UniqueName="localizacion" Resizable="true"/>
                                <telerik:GridBoundColumn DataField="razon_social" HeaderText="Cliente" SortExpression="razon_social" UniqueName="razon_social" Resizable="true"/>
                                <telerik:GridBoundColumn DataField="no_siniestro" HeaderText="Siniestro" SortExpression="no_siniestro" UniqueName="no_siniestro" Resizable="true"/>
                            </Columns>
                            <NoRecordsTemplate>
                                <asp:Label runat="server" ID="lblvacio" Text="No existen llamadas registradas" CssClass="errores"></asp:Label>
                            </NoRecordsTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
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
