<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="SolicitudGrupos.aspx.cs" Inherits="SolicitudGrupos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="page-header">
                <!-- /BREADCRUMBS -->
                <div class="clearfix">
                    <h3 class="content-title pull-left">Solicitud de Crédito (Grupos Productivos)</h3>
                </div>
            </div>


            <div class="col-lg-12 col-sm-12">

                <div class="col-lg-4 col-sm-4">
                    <asp:LinkButton ID="lnkAbreSolicitud" runat="server" ToolTip="Genera Solicitud" OnClick="lnkAbreSolicitud_Click" CssClass="btn btn-info t14" Visible="false"><i class="fa fa-save"></i>&nbsp;<span>Genera Solicitud</span></asp:LinkButton>
                </div>
                <div class="col-lg-4 col-sm-4">
                    <asp:LinkButton ID="lnkAbreIntegrantes" runat="server" Visible="false" ToolTip="Agrega Integrantes" OnClick="lnkAbreIntegrantes_Click" CssClass="btn btn-grey t14"><i class="fa fa-save"></i>&nbsp;<span>Agrega Integrantes</span></asp:LinkButton>
                </div>
                <div class="col-lg-4 col-sm-4">
                    <asp:LinkButton ID="lnkAbreEdicion" runat="server" Visible="false" ToolTip="Editar Solicitud" OnClick="lnkAbreEdicion_Click" CssClass="btn btn-warning t14"><i class="fa fa-edit"></i>&nbsp;<span>Edita Solicitud</span></asp:LinkButton>
                </div>
            </div>


            <div class="row text-center">
                <asp:Label ID="lblErrorAfuera" runat="server"></asp:Label>
                <asp:Label ID="lblIdEditaAgrega" runat="server" Visible="false"></asp:Label>
            </div>


            <div class=" ancho100 marTop text-center">
                <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" EnableAJAX="true">
                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" AllowFilteringByColumn="true" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged"
                        EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource2" AllowSorting="true" GroupingEnabled="false" PageSize="50">
                        <MasterTableView DataSourceID="SqlDataSource2" AutoGenerateColumns="false" DataKeyNames="id_solicitud_credito,id_acta">
                            <Columns>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="grupo_productivo" FilterControlAltText="Filtro Grupo" HeaderText="Grupo" SortExpression="grupo_productivo" UniqueName="grupo_productivo" Resizable="true" />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="fecha_solicitud" FilterControlAltText="Filtro Fecha Solicitud" HeaderText="Fecha Solicitud" SortExpression="fecha_solicitud" UniqueName="fecha_solicitud" Resizable="true" />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="monto_credito" FilterControlAltText="Filtro Monto" HeaderText="Monto" SortExpression="monto_credito" UniqueName="monto_credito" Resizable="true" DataFormatString="{0:N2}" Aggregate="Sum" FooterAggregateFormatString="{0:C2}" />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="monto_autorizado" FilterControlAltText="Filtro Autorizado" HeaderText="Monto Autorizado" SortExpression="monto_autorizado" UniqueName="monto_autorizado" Resizable="true" DataFormatString="{0:N2}" Aggregate="Sum" FooterAggregateFormatString="{0:C2}" />
                            </Columns>
                            <NoRecordsTemplate>
                                <asp:Label ID="Label1" runat="server" Text="No existen solicitudes registradas" CssClass="text-danger"></asp:Label>
                            </NoRecordsTemplate>
                        </MasterTableView>
                        <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                        </ClientSettings>

                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="select e.id_solicitud_credito,e.grupo_productivo,convert(char(10),fecha_Solicitud,120) as fecha_Solicitud,e.monto_credito,e.monto_autorizado,a.id_acta from AN_Solicitud_Credito_Encabezado e inner join an_acta_integracion a on e.id_grupo = a.id_grupo " ConnectionString="<%$ ConnectionStrings:Taller %>"></asp:SqlDataSource>
            </div>
            <div class="row marTop text-center">
                <asp:LinkButton ID="lnkImprimir" runat="server" Visible="false" ToolTip="Imprimir Solicitud" OnClick="lnkImprimir_Click " CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Solicitud</span></asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <asp:Panel ID="pnlMask" runat="server" CssClass="mask zen1" Visible="false" />
            <asp:Panel ID="windowSolicitud" CssClass="popUp zen2  ancho80" Height="80%" ScrollBars="Vertical" Visible="false" runat="server">

                <table class="ancho100">
                    <tr class="ancho100%">

                        <div class="page-header">
                            <div class="clearfix">
                                <div class=" col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                                    <asp:LinkButton ID="lnkCerrar" runat="server" CssClass="btn btn-danger alingMiddle" OnClick="lnkCerrar_Click" ToolTip="Cerrar"><i class="fa fa-remove t18"></i></asp:LinkButton>
                                </div>
                                <h3 class="content-title center">Solicitud de Crédito
                                </h3>
                                <asp:Label ID="lblTitulo" runat="server" Visible="false" CssClass="t22 colorNegro textoBold" />

                            </div>

                        </div>
                    </tr>

                </table>



                <div class="row">
                    <div class="col-lg-12 col-sm-12 text-center alert-info">
                        <h3>
                            <i class="fa fa-credit-card "></i>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label13" runat="server" Text="Datos Grupo"></asp:Label>
                        </h3>
                    </div>
                </div>

                <div class="row pad1m marTop textoBold">
                    <div class="col-lg-1 col-sm-1  text-center">
                        <asp:Label ID="Label2" runat="server" Text="Fecha Solicitud:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <telerik:RadDatePicker ID="txtFechaSolicitud" runat="server">
                            <DateInput ID="DateInput1" runat="server" DateFormat="yyyy/MM/dd">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Debe indicar la fecha de Solicitud " Text="*" ValidationGroup="crea" ControlToValidate="txtFechaSolicitud" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label9" runat="server" Text="Fecha Entrega:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <telerik:RadDatePicker ID="txtFechaEntrega" runat="server">
                            <DateInput ID="DateInput2" runat="server" DateFormat="yyyy/MM/dd">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe indicar la fecha de Entrega " Text="*" ValidationGroup="crea" ControlToValidate="txtFechaEntrega" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label3" runat="server" Text="Grupo Productivo:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <asp:DropDownList ID="cmbGrupoProductivo" runat="server" DataSourceID="SqlDataSourceCombo" DataTextField="grupo_productivo" DataValueField="id_grupo"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourceCombo" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_grupo, grupo_productivo from AN_Grupos where id_empresa=@empresa and id_sucursal=@sucursal">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0" />
                                <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>

                <div class="row pad1m textoBold">
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label10" runat="server" Text="Monto Crédito:"></asp:Label>
                    </div>
                    <div class="co2-lg-3 col-sm-3 text-center">
                        <asp:TextBox ID="txtMontoCredito" runat="server" CssClass="alingMiddle input-large" MaxLength="20" OnTextChanged="txtGarantiaLiquidaEncabezado_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" BehaviorID="txtMontoCreditoTextBoxWatermarkExtender" TargetControlID="txtMontoCredito" WatermarkText="Monto Crédito" WatermarkCssClass="water input-large" />
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtMontoCredito" />
                        <asp:RequiredFieldValidator ID="validacionTelCelCli" runat="server" ErrorMessage="Debe indicar el número Monto " Text="*" ValidationGroup="crea" ControlToValidate="txtMontoCredito" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label5" runat="server" Text="Plazo"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <asp:DropDownList ID="cmbplazo" runat="server">
                            <asp:ListItem Value="12">12 Semanas</asp:ListItem>
                            <asp:ListItem Value="16">16 Semanas</asp:ListItem>
                            <asp:ListItem Value="20">20 Semanas</asp:ListItem>
                            <asp:ListItem Value="24">24 Semanas</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label6" runat="server" Text="Tasa"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <asp:DropDownList ID="cmbtaza" runat="server">
                            <asp:ListItem Value="4">4%</asp:ListItem>
                            <asp:ListItem Value="5">5%</asp:ListItem>
                            <asp:ListItem Value="6">6%</asp:ListItem>
                            <asp:ListItem Value="7">7%</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row pad1m textoBold">
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label7" runat="server" Text="Garantía Líquida:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <asp:TextBox ID="txtGarantiaLiquidaEncabezado" runat="server" CssClass="alingMiddle input-large" MaxLength="20" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label8" runat="server" Text="Monto Máximo"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <asp:TextBox ID="txtMontoMaximo" runat="server" CssClass="alingMiddle input-large" MaxLength="20" ReadOnly="true"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderr2" runat="server" BehaviorID="txtPlazoTextBoxWatermarkExtender" TargetControlID="txtMontoMaximo" WatermarkText="Monto Crédito" WatermarkCssClass="water input-large" />
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderr2" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtMontoMaximo" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorr3" runat="server" ErrorMessage="Debe indicar el Monto Máximo " Text="*" ValidationGroup="crea" ControlToValidate="txtMontoMaximo" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label17" runat="server" Text="Monto Autorizado"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <asp:TextBox ID="txtMontoAutorizado" runat="server" CssClass="alingMiddle input-large" MaxLength="20" ReadOnly="true"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" BehaviorID="txtMontoAutorizadoBoxWatermarkExtender" TargetControlID="txtMontoAutorizado" WatermarkText="Monto Autorizado" WatermarkCssClass="water input-large" />
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtMontoAutorizado" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Debe indicar el Monto Máximo " Text="*" ValidationGroup="crea" ControlToValidate="txtMontoAutorizado" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row pad1m textoBold">
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label11" runat="server" Text="Plazo"></asp:Label>
                        (Semanas):
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <asp:TextBox ID="txtPlazoRC" runat="server" CssClass="alingMiddle input-large" MaxLength="20" ReadOnly="true"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" BehaviorID="txtMontoAutorizadoBoxWatermarkExtender" TargetControlID="txtPlazoRC" WatermarkText="Plazo" WatermarkCssClass="water input-large" />
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" TargetControlID="txtPlazoRC" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Debe indicar el Plazo " Text="*" ValidationGroup="crea" ControlToValidate="txtPlazoRC" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label12" runat="server" Text="Tasa:"></asp:Label>
                        %:
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <asp:TextBox ID="txtTasaRC" runat="server" CssClass="alingMiddle input-large" MaxLength="20" ReadOnly="true"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" BehaviorID="txtTasaRCAutorizadoBoxWatermarkExtender" TargetControlID="txtTasaRC" WatermarkText="Tasa" WatermarkCssClass="water input-large" />
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtTasaRC" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Debe indicar la Tasa " Text="*" ValidationGroup="crea" ControlToValidate="txtTasaRC" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label111" runat="server" Text="Forma de pago:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <telerik:RadComboBox RenderMode="Lightweight" ID="cmbFormaPago" runat="server" DropDownHeight="200px" DropDownWidth="200px">
                            <Items>
                                <telerik:RadComboBoxItem Value="Sn.F" Text="Seleccione Forma" />
                                <telerik:RadComboBoxItem Value="CHE" Text="Cheque" />
                                <telerik:RadComboBoxItem Value="ORD" Text="Orden de Pago" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                </div>

                <div class="row pad1m textoBold">
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label29" runat="server" Text="Ciclo"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <asp:TextBox ID="txtCiclo" runat="server" CssClass="alingMiddle input-large" MaxLength="20"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" BehaviorID="txtMontoAutorizadoBoxWatermarkExtender" TargetControlID="txtCiclo" WatermarkText="Ciclo" WatermarkCssClass="water input-large" />
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtCiclo" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Debe indicar el Ciclo " Text="*" ValidationGroup="crea" ControlToValidate="txtCiclo" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Labell1" runat="server" Text="Observaciones"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <telerik:RadTextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Height="100px" Width="200px" PlaceHolder="Observaciones"></telerik:RadTextBox>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label4" runat="server" Text="Banco"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <asp:DropDownList ID="ddlbanco" runat="server" AutoPostBack="true" AppendDataBoundItems="true" DataSourceID="SQLBanco" DataValueField="Clave" DataTextField="Nombre">
                            <asp:ListItem Value="0">Selecciona Banco</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SQLBanco" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select *from an_Catbancos"></asp:SqlDataSource>
                    </div>

                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label30" runat="server" Text="Canal"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <asp:DropDownList ID="DDLCanal" runat="server" AutoPostBack="true" AppendDataBoundItems="true" DataSourceID="SQLCanal" DataValueField="codigo_canal" DataTextField="nombre_canal">
                            <asp:ListItem Value="0">Seleccione Canal</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SQLCanal" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select *from PLD_Canal"></asp:SqlDataSource>
                    </div>

                </div>
                <div class="row pad1m textoBold">
                    <div class="col-lg-1 col-sm-1 text-center">
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                    </div>
                </div>

                <div class="row text-center">
                    <asp:Label ID="lblErrorDatosGrupo" Visible="false" runat="server" CssClass="alert-danger"></asp:Label>
                </div>

                <div class="row text-center">
                    <asp:LinkButton ID="lnkAgregaSolicitud" runat="server" ToolTip="Agrega Cliente" OnClick="lnkAgregaSolicitud_Click" CssClass="btn btn-success t14"><i class="fa fa-save"></i>&nbsp;<span>Agrega Solicitud</span></asp:LinkButton>
                </div>


            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>

            <asp:Panel ID="pnlMarsk2" runat="server" CssClass="mask zen1" Visible="false" />
            <asp:Panel ID="windowIntegrantes" CssClass="popUp zen2  ancho80" Height="80%" ScrollBars="Vertical" Visible="false" runat="server">


                <table class="ancho100">
                    <tr class="ancho100%">

                        <div class="page-header">
                            <div class="clearfix">
                                <div class=" col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-danger alingMiddle" OnClick="lnkCerrar2_Click" ToolTip="Cerrar"><i class="fa fa-remove t18"></i></asp:LinkButton>
                                </div>
                                <h3 class="content-title center">Agrega Integrante
                                </h3>
                                <asp:Label ID="lblTituloDetalle" runat="server" Visible="false" CssClass="t22 colorNegro textoBold" />

                            </div>

                        </div>
                    </tr>

                </table>





                <div class="row marTop">

                    <div class="col-lg-12 col-sm-12 text-center alert-info">
                        <h3>
                            <i class="fa fa-user "></i>&nbsp;&nbsp;&nbsp;                   
                                <asp:Label ID="Label14" runat="server" Text="Integrantes"></asp:Label>
                        </h3>
                    </div>
                </div>


                <div class="row pad1m textoBold">
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label15" runat="server" Text="Cliente"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <asp:DropDownList ID="cmb_nombre" runat="server" OnTextChanged="txtTelefono_TextChanged" AutoPostBack="true" OnSelectedIndexChanged="cmbPersonaSelected" DataSourceID="cmbNombre" DataValueField="id_cliente"
                            DataTextField="nombre_completo">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="cmbNombre" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"></asp:SqlDataSource>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label16" runat="server" Text="Ciclo:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <telerik:RadTextBox ID="txtCicloDetalle" runat="server" PlaceHolder="Ciclo"></telerik:RadTextBox>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label18" runat="server" Text="Cargo:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <telerik:RadTextBox ID="txt_cargo" AutoPostBack="true" runat="server" PlaceHolder="Cargo"></telerik:RadTextBox>
                    </div>
                </div>

                <div class="row pad1m textoBold">
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label19" runat="server" Text="Estatus"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <telerik:RadComboBox RenderMode="Lightweight" ID="cmbEstatus" runat="server" DropDownHeight="200px" DefaultMessage="Selecciona un cargo" DropDownWidth="200px">
                            <Items>
                                <telerik:RadComboBoxItem Value="SIN" Text="Seleccione Estatus" />
                                <telerik:RadComboBoxItem Value="ANT" Text="Antiguo" />
                                <telerik:RadComboBoxItem Value="NUE" Text="Nuevo" />
                                <telerik:RadComboBoxItem Value="DES" Text="Desertor" />
                                <telerik:RadComboBoxItem Value="REI" Text="Reingreso" />
                                <telerik:RadComboBoxItem Value="CDG" Text="Cambio de Grupo" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label20" runat="server" Text="Causas de Desercion:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <telerik:RadComboBox RenderMode="Lightweight" ID="cmbCausasDeser" runat="server" DropDownHeight="200px" DefaultMessage="Selecciona un cargo" DropDownWidth="200px">
                            <Items>
                                <telerik:RadComboBoxItem Value="SIN" Text="Seleccione Causa" />
                                <telerik:RadComboBoxItem Value="MOR" Text="Morosidad" />
                                <telerik:RadComboBoxItem Value="CON" Text="Conflicto" />
                                <telerik:RadComboBoxItem Value="SOB" Text="Sobreendeudamiento" />
                                <telerik:RadComboBoxItem Value="MAL" Text="Mala Fe" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label21" runat="server" Text="Giro del Negocio:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <%--<telerik:RadComboBox RenderMode="Lightweight" ID="cmbGiroNegocio" runat="server"  DropDownHeight="200px" DefaultMessage="Selecciona un cargo" DropDownWidth="200px">
                                    <Items>
                                        <telerik:RadComboBoxItem VALUE="SIN" Text="Seleccione Giro" />
                                       <telerik:RadComboBoxItem VALUE="COM" Text="Comercio" />
                                       <telerik:RadComboBoxItem VALUE="IND" Text="Industria" />
                                       <telerik:RadComboBoxItem VALUE="SER" Text="Servicios" />
                                       <telerik:RadComboBoxItem VALUE="AGR" Text="Agropecuario" />
                                       <telerik:RadComboBoxItem VALUE="FOR" Text="Forestal" />
                                       <telerik:RadComboBoxItem VALUE="PES" Text="Pesquero" />
                                       <telerik:RadComboBoxItem VALUE="MIN" Text="Mineria" />
                                    </Items> 
                                 </telerik:RadComboBox>--%>

                        <asp:DropDownList ID="cmbGiroNegocio" runat="server" Width="200px" AutoPostBack="true" AppendDataBoundItems="true" DataSourceID="SQLaEconomica" DataValueField="codigo_actividad" DataTextField="nombre_actividad">
                            <asp:ListItem Value="0">Selecciona Opcion</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SQLaEconomica" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select *from PLD_Actividad_Economica"></asp:SqlDataSource>
                    </div>
                </div>

                <div class="row pad1m textoBold">
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label22" runat="server" Text="Ingreso:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <telerik:RadNumericTextBox ID="txtIngreso" runat="server" PlaceHolder="0.00"></telerik:RadNumericTextBox>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label23" runat="server" Text="Destino del Credito:"></asp:Label>
                    </div>

                    <%--  --%>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <asp:DropDownList ID="txtDestinoCredito" runat="server" Width="200px" AutoPostBack="true" AppendDataBoundItems="true" DataSourceID="SQLPCuenta" DataValueField="codigo_pproducto" DataTextField="nombre_pproducto">
                            <asp:ListItem Value="0">Selecciona Opcion</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SQLPCuenta" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select *from pld_proposito_cuenta"></asp:SqlDataSource>
                        <%--<telerik:RadTextBox ID="txtDestinoCredito" runat="server" PlaceHolder="Destino del Credito:" ></telerik:RadTextBox>--%>
                    </div>


                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label24" runat="server" Text="Credito Anterior:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <telerik:RadNumericTextBox ID="txtCreditoAnterior" runat="server" PlaceHolder="0.00"></telerik:RadNumericTextBox>
                    </div>
                </div>

                <div class="row pad1m textoBold">
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label25" runat="server" Text="Credito Sol:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <telerik:RadNumericTextBox ID="txtCreditoSolicitado" OnTextChanged="txtCreditoSolicitado_TextChanged" AutoPostBack="true" runat="server" PlaceHolder="0.00"></telerik:RadNumericTextBox>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label26" runat="server" Text="Garantia Liquida:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <telerik:RadNumericTextBox ID="txtGarantiaLiquida" runat="server" AutoPostBack="true" PlaceHolder="0.00"></telerik:RadNumericTextBox>
                    </div>
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label27" runat="server" Text="Credito Aut:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <telerik:RadNumericTextBox ID="txtCreditoautorizado" runat="server" PlaceHolder="" ReadOnly="true"></telerik:RadNumericTextBox>
                    </div>
                </div>

                <div class="row pad1m textoBold">
                    <div class="col-lg-1 col-sm-1 text-center">
                        <asp:Label ID="Label28" runat="server" Text="Telefono:"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-center">
                        <telerik:RadTextBox ID="txtTelefono" AutoPostBack="true" runat="server" PlaceHolder="Telefono"></telerik:RadTextBox>
                    </div>
                </div>
                <div class="row text-center">
                    <asp:Label ID="lblErrorDetalle" CssClass=" alert-danger" runat="server" Visible="false"></asp:Label>
                </div>

                <div class="row text-center">
                    <asp:LinkButton ID="lnkAgregaCliente" runat="server" ToolTip="Agrega Cliente" OnClick="lnkAgregaIntegrante_Click" CssClass="btn btn-success t14"><i class="fa fa-save"></i>&nbsp;<span>Agrega Cliente</span></asp:LinkButton>
                    <asp:LinkButton ID="lnkActualizaCliente" runat="server" ToolTip="Actualiza Cliente" Visible="false" OnClick="lnkActualizaCliente_Click" CssClass="btn btn-success t14"><i class="fa fa-save"></i>&nbsp;<span>Actualiza Cliente</span></asp:LinkButton>
                </div>


                <div class="row marTop text-center">
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" EnableAJAX="true">
                        <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid2" AllowFilteringByColumn="true" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue"
                            EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource1" AllowSorting="true" GroupingEnabled="false" PageSize="50">
                            <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="false" DataKeyNames="id_detalle,id_cliente,id_solicitud_credito">
                                <Columns>
                                    <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_cliente" FilterControlAltText="Filtro Cliente" HeaderText="Cliente" SortExpression="nombre_cliente" UniqueName="nombre_cliente" Resizable="true" />
                                    <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="ciclo" FilterControlAltText="Filtro ciclo" HeaderText="Ciclo" SortExpression="ciclo" UniqueName="ciclo" Resizable="true" />
                                    <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="cargo" FilterControlAltText="Filtro Cargo" HeaderText="Cargo" SortExpression="cargo" UniqueName="cargo" Resizable="true" />
                                    <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="credito_autorizado" FilterControlAltText="Filtro Credito Autorizado" HeaderText="Credito Autorizado" SortExpression="credito_autorizado" UniqueName="credito_autorizado" Resizable="true" />
                                    <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" HeaderText="Editar" SortExpression="id_cliente" UniqueName="id_cliente" FilterControlAltText="Filtro Editar" DataField="id_cliente">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEditar" runat="server" Visible="true" CssClass="btn btn-success t14" OnClick="lnkEditar_Click"><i></i>&nbsp;<span>Editar</span></asp:LinkButton>
                                            <asp:LinkButton ID="lnkEliminar" runat="server" CssClass="btn btn-danger t14" OnClick="lnkEliminar_Click"><i ></i>&nbsp;<span>Eliminar</span></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="No existen solicitudes registradas" CssClass="text-danger"></asp:Label>
                                </NoRecordsTemplate>
                            </MasterTableView>
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                            </ClientSettings>

                        </telerik:RadGrid>
                    </telerik:RadAjaxPanel>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="select id_cliente,id_solicitud_credito,id_detalle,nombre_cliente,ciclo,cargo,credito_autorizado,telefono from AN_Solicitud_Credito_Detalle where id_empresa=@empresa and id_sucursal=@sucursal and id_solicitud_credito=@id_solicitud_credito" ConnectionString="<%$ ConnectionStrings:Taller %>">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                            <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />
                            <asp:ControlParameter ControlID="RadGrid1" Name="id_solicitud_credito" DefaultValue="0" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>



            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

