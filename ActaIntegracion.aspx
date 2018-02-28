<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="ActaIntegracion.aspx.cs" Inherits="ActaIntegracion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%-- 
        Aqui inserto las funciones de JavaScript 
    
        Las siguientes funciones  abren y cierran el control  de "RadWindows" de telerik 
        con el nombre de "windowActaIntegracion"
        
        <script type="text/javascript">
            function abreWin1() {
              
                    var oWnd = $find("<%=windowActaIntegracion.ClientID%>");
                oWnd.setUrl('');
                oWnd.show();
            
            }
            function cierraWin1() {
                var oWnd = $find("<%=windowActaIntegracion.ClientID%>");
                oWnd.close();
            }
        </script>
    --%>



    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- Letrero de encabezado --%>
            <div class="page-header">
                <div class="clearfix">
                    <h3 class="content-title pull-left">Acta de Integración y regulamiento interno
                    </h3>
                </div>
            </div>

            <%-- Panel para grid  --%>
            <asp:UpdatePanel ID="panelTablaInicial" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-center">
                            <%-- boton editar --%>
                            <asp:LinkButton ID="lnkAbreWindow" runat="server" ToolTip="Guarda Acta" CssClass="btn btn-success t14"
                                OnClick="lnkAbreWindow_Click">
                                <i class="fa fa-save"></i>&nbsp;
                                <span>
                                    Genera Acta
                                </span>
                            </asp:LinkButton>
                        </div>
                          <%-- boton agrega integrantes --%>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-center">
                            
                            <asp:LinkButton ID="lnkAgregaIntegrante" runat="server" ToolTip="Edita Integrantes" CssClass="btn btn-grey t14"
                                OnClick="lnkEditaIntegrantesWindow_Click" Visible="false">
                                <i class="fa fa-users"></i>&nbsp;
                                <span>
                                    Agrega Integrantes
                                </span>
                            </asp:LinkButton>
                            
                        </div>
                            <%-- boton agrega --%>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-center">
                            
                            <asp:LinkButton ID="lnkEditaWindow" runat="server" ToolTip="Edita Acta" CssClass="btn btn-warning t14"
                                OnClick="lnkEditaActaWindow_Click" Visible="false">
                                <i class="fa fa-save"></i>&nbsp;
                                <span>
                                    Edita Acta
                                </span>
                            </asp:LinkButton>
                            
                        </div>
                    </div>
                    
                    <%--Dibujamos la tabla inicial --%>
                    <div class="row marTop">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <telerik:RadAjaxPanel ID="RadPanAjaxTablaInicial" runat="server" EnableAJAX="false"> 
                                <%--Aqui va el grid --%>
                                <telerik:RadGrid ID="gridActaIntegracion" RenderMode="Lightweight" runat="server" Skin="Office2010Blue"
                                    AllowFilteringByColumn="true" EnableHeaderContextMenu="true" Culture="es-Mx"
                                    AllowPaging="true" PageSize="50" AllowSorting="true" AutoGenerateColumns="false"
                                    DataSourceID="SqlDataSource1" OnSelectedIndexChanged="gridActaIntegracion_SelectedIndexChanged">
                                    <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="false" DataKeyNames="id_acta">
                                        <Columns>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="fecha_integracion_acta" HeaderText="Fecha Integración"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="grupo_productivo" HeaderText="Nombre del Grupo"></telerik:GridBoundColumn>
                                            <%-- <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_presidente" HeaderText="Presidente"></telerik:GridBoundColumn> --%>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="colonia_grupo_productivo" HeaderText="Colonia"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1">
                                           
                                        </Scrolling>
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>    
                                </telerik:RadGrid>

                            </telerik:RadAjaxPanel>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="select convert(char(10),a.fecha_integracion_acta,120) as 
                                        fecha_integracion_acta, b.grupo_productivo, a.colonia_grupo_productivo,a.id_acta from an_acta_integracion a inner join an_grupos b on b.id_grupo=a.id_grupo "
                                ConnectionString="<%$ ConnectionStrings:Taller %>"></asp:SqlDataSource>
                        </div>
                   </div>
                        <div class="row marTop  text-center">
                            <asp:LinkButton ID="lnkImprimir" runat="server" Visible="false" ToolTip="Imprimir Solicitud" OnClick="lnkImprimir_Click " CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Acta</span></asp:LinkButton>                
                        </div> 
                    
                    <div class="row ">
                        <div class="marTop">
                            <asp:Label ID="lblErrorAfuera" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMaskCapturaActa" runat="server" CssClass="mask zen1" Visible="false" />
            <asp:Panel ID="windowActaIntegracion" CssClass="popUp zen2 textoCentrado ancho80" Visible="false" runat="server" Width="90%" Height="90%" ScrollBars="Both">
                <%--barra de titulo --%>

                 <table class="ancho100">
                    <tr class="ancho100%">
                        
                         <div class="page-header" >
                <div class="clearfix">
                     <div class=" col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                      <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-danger alingMiddle  "
                            OnClick="cierrascreen_click" ToolTip="Cerrar"><i class="fa fa-remove t18"></i></asp:LinkButton>
                 </div>
                    <h3 class="content-title center">Acta de Integración y regulamiento interno
                    </h3>
                   
                    
                </div>
                
            </div>
                    </tr>
                     
                </table>

                

                <%--
                    Encerramos la captura del encabezado en un panel y la captura de los integrantes que conforman el acta en otro panel
                    acontinuacion, el panel de encabezado del acta de integracion
                --%>

                <asp:Panel ID="pnlEncabezadoActaIntegracion" runat="server">

                   
                        <!--Hora en que se integra el acta-->
                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblHoraActaIntegracion">Hora de Integración:</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <telerik:RadTimePicker RenderMode="Lightweight" ID="txtHoraActaIntegracion" Culture="es-Mx" DateInput-DateFormat="HH:mm:ss" DateInput-EmptyMessage="Hora" Skin="MetroTouch" Width="120px" runat="server"></telerik:RadTimePicker>                 
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                <label id="lblfechaIntegracionActa">Fecha de integración:</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <telerik:RadDatePicker ID="txtFechaIntegracionActa" runat="server" Height="35px" Width="250px">
                                    <DateInput ID="DateInput1" runat="server" DateFormat="yyyy/MM/dd">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6"  runat="server" ErrorMessage="Debe indicar Fecha" Text="*" ValidationGroup="crea" ControlToValidate="txtFechaIntegracionActa" CssClass="alineado errores"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        

                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblCalleDireccionDeIntegracionActa">Calle:</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtCalleDireccionDeIntegracionActa" runat="server" PlaceHolder="Calle" Height="35px" Width="250px"> </asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                <label id="lblNumeroDireccionDeIntegracionActa">Número:</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtNumeroDireccionDeIntegracionActa" runat="server" PlaceHolder="Número" Height="35px" Width="250px"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblColoniaDireccionDeIntegracionActa">Colonia:</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtColoniaDireccionDeIntegracionActa" runat="server" PlaceHolder="Colonia" Height="35px" Width="250px"> </asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                <label id="lblMunicipioDireccionDeIntegracionActa">Municipio:</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtMunicipioDireccionDeIntegracionActa" runat="server" PlaceHolder="Municipio" Height="35px" Width="250px"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblEstadoDireccionDeIntegracionActa">Estado:</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtEstadoDireccionDeIntegracionActa" runat="server" PlaceHolder="Estado" Height="35px" Width="250px"> </asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                
                            </div>
                        </div>

                    <!-- Letrero Acuerdos-->
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center alert-info">
                            <!--<span>Acta de Integración y regulamiento interno</span>-->
                            <h3>
                                <!--<span>Acta de Integración y regulamiento interno</span>-->
                                Acuerdos
                            </h3>
                        </div>
                    </div>

                    
                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblNombreGrupoProductivo">Nombre del Grupo Productivo</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                 <asp:TextBox ID="txtNombreGrupoProductivo" runat="server" PlaceHolder="Digita el nombre del grupo" OnTextChanged="elPresi_TextChanged" AutoPostBack="true"
                                Height="35px" Width="250px" CssClass="col-lg-6 col-md-6 col-sm-12 col-xs-12"> </asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                <label id="lblNombrePresidente">Nombre del presidente(a)</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:DropDownList ID="cmbIdPresidente" AutoPostBack="true" OnSelectedIndexChanged="cmbIdPresidente_SelectedIndexChanged" runat="server"
                                DataSourceID="SqlDataSourcePresidente" DataTextField="nombre_completo"
                                DataValueField="id_cliente">
                                    
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourcePresidente" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" >
                                
                            </asp:SqlDataSource>
                              
                            <asp:SqlDataSource ID="SqlDataSourceTesorero" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"/>
                            <asp:SqlDataSource ID="SqlDataSourceSecretario" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"/>
                            <asp:SqlDataSource ID="SqlDataSourceSupervisor" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"/>
                            <asp:SqlDataSource ID="SqlDataSourceVocalV2" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"/>

                            <asp:DropDownList ID="cmbIdPresidenteEditar" runat="server"
                                DataSourceID="SqlDSCombosEditar" DataTextField="nombre_completo"
                                DataValueField="id_cliente" Visible="false">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDSCombosEditar" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Taller %>"
                                SelectCommand="select 0 as id_cliente,'Selecione  Presidente'as nombre_completo union all select a.id_cliente,b.nombre_completo from an_solicitud_consulta_buro a inner join an_clientes b on b.id_cliente=a.id_cliente where a.id_empresa=@empresa and a.id_sucursal=@sucursal and a.procesable='FA1'">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="cliente" QueryStringField="e"
                                        DefaultValue="0" />
                                    <asp:QueryStringParameter Name="empresa" QueryStringField="e"
                                        DefaultValue="0" />
                                    <asp:QueryStringParameter Name="sucursal"
                                        QueryStringField="t" DefaultValue="0" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            </div>
                        </div>

                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblNombreSecretario">Nombre del Tesorero(a)</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:DropDownList ID="cmbIdSecretario" AutoPostBack="true" OnSelectedIndexChanged="cmbIdSecretario_SelectedIndexChanged"  runat="server"
                                DataSourceID="SqlDataSourceSecretario" DataTextField="nombre_completo"
                                DataValueField="id_cliente">
                                    <asp:ListItem Value="0">Selecciona Cliente</asp:ListItem>
                            </asp:DropDownList>
                            <%--combo para editar --%>
                            <asp:DropDownList ID="cmbIdSecretarioEditar" runat="server"
                                DataSourceID="SqlDSCombosEditar" DataTextField="nombre_completo"
                                DataValueField="id_cliente" Visible="false">
                            </asp:DropDownList>
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                <label id="lblNombreTesorero">Nombre del Secretario(a)</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:DropDownList ID="cmbIdTesorero"  AutoPostBack="true" runat="server"
                                DataSourceID="SqlDataSourceTesorero" OnSelectedIndexChanged="cmbIdTesorero_SelectedIndexChanged" DataTextField="nombre_completo"
                                DataValueField="id_cliente">
                                    <asp:ListItem Value="0">Selecciona Cliente</asp:ListItem>
                            </asp:DropDownList>
                             <%--combo para editar --%>
                            <asp:DropDownList ID="cmbIdTesoreroEditar" runat="server"
                                DataSourceID="SqlDSCombosEditar" DataTextField="nombre_completo"
                                DataValueField="id_cliente"  Visible="false" >
                            </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblNombreSupervisor">Nombre del Vocal de Vigilancia 1(a)</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:DropDownList ID="cmbIdSupervisor"   runat="server" AutoPostBack="true"
                                DataSourceID="SqlDataSourceSupervisor" OnSelectedIndexChanged="cmbIdSupervisor_SelectedIndexChanged" DataTextField="nombre_completo"
                                DataValueField="id_cliente">
                                    <asp:ListItem Value="0">Selecciona Cliente</asp:ListItem>
                            </asp:DropDownList>
                             <%--combo para editar --%>
                            <asp:DropDownList ID="cmbIdSupervisorEditar" runat="server"
                                DataSourceID="SqlDSCombosEditar" DataTextField="nombre_completo"
                                DataValueField="id_cliente"  Visible="false">
                            </asp:DropDownList>
                            </div>

                            
                             
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblNombreVocalV2">Nombre del Vocal de Vigilancia 2(a)</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:DropDownList ID="cmbIdVocal2"   runat="server"
                                DataSourceID="SqlDataSourceVocalV2"  DataTextField="nombre_completo"
                                DataValueField="id_cliente">
                                    <asp:ListItem Value="0">Selecciona Cliente</asp:ListItem>
                            </asp:DropDownList>
                             <%--combo para editar --%>
                                 <asp:DropDownList ID="cmbIdVocal2Editar" runat="server"
                                DataSourceID="SqlDSCombosEditar" DataTextField="nombre_completo"
                                DataValueField="id_cliente"  Visible="false">
                            </asp:DropDownList>
                            </div>
                                 
                        </div>

                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblColoniasCircunvecinas">Colonias circunvecinas</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtColoniasCircunvecinas" runat="server" PlaceHolder="Nombre de las colonias vecinas"
                                Height="35px" Width="250px"> </asp:TextBox>
                            </div>
                             <div class="col-lg-2 col-sm-2 text-left">
                                <label id="lblColoniaGrupoProductivo">Colonia del Gpo. Productivo</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtColoniaGrupoProductivo" runat="server" PlaceHolder="Colonia principal del Gpo"
                                Height="35px" Width="250px"> </asp:TextBox>
                            </div>
                        </div>
                     
                     <div class="row pad1m marTop textoBold">
                     <div class="col-lg-2 col-sm-2 text-left">
                                <label id="lblMontoAhorroMinimoSemanal">Monto de ahorro mínimo semanal</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtMontoAhorroMinimoSemanal" runat="server" CssClass="alingMiddle input-large" MaxLength="20"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" BehaviorID="txtMontoAhorroMinimoSemanalTextBoxWatermarkExtender" TargetControlID="txtMontoAhorroMinimoSemanal" WatermarkText="Monto minimo Semanal" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtMontoAhorroMinimoSemanal" />
                                             <asp:RequiredFieldValidator ID="validacionTelCelCli" runat="server" ErrorMessage="Debe indicar el monto minimo " Text="*" ValidationGroup="crea" ControlToValidate="txtMontoAhorroMinimoSemanal" CssClass="alineado errores"></asp:RequiredFieldValidator>
                            </div>
                    </div>

                        <div class="row pad1m marTop textoBold">
                         
                            <div class="col-lg-4 col-sm-4 text-left">
                                 <asp:TextBox ID="txtNumeroRepartoAhorro" runat="server" Visible="false" CssClass="alingMiddle input-large" MaxLength="20"></asp:TextBox>
                                            
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                
                            </div>
                        </div>


                    <!-- Letrero reunión-->
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center alert-info">
                            <!--<span>Acta de Integración y regulamiento interno</span>-->
                            <h3>
                                <!--<span>Acta de Integración y regulamiento interno</span>-->
                                Reuniones
                            </h3>
                        </div>
                    </div>

                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblDiaReunionSemanal">Día reunión semanal</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtDiaReunionSemanal" runat="server" PlaceHolder="Día Reunión"
                            Height="35px" Width="250px"> </asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                <label id="lblHoraReunionSamanal">Hora reunión semanal</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtHoraReunionSamanal" runat="server" PlaceHolder="Hora de la reunión semanal" OnTextChanged="PrestacasasParty" AutoPostBack="true"
                            Height="35px" Width="250px"> </asp:TextBox>
                            </div>
                        </div>

                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblNombreIntegranteParaReuniones">Nombre del integrante donde se realizarán las reuniones</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                     <asp:DropDownList ID="cmb_Casa" AutoPostBack="true" runat="server" OnSelectedIndexChanged="cmb_Casa_SelectedIndexChanged"
                                DataSourceID="SqlDataSourceCasa" DataTextField="nombre_completo"
                                DataValueField="id_cliente">
                            </asp:DropDownList>
                                <asp:DropDownList ID="cmb_CasaEdt" Visible="false" AutoPostBack="true" runat="server" OnSelectedIndexChanged="cmb_Casa_SelectedIndexChanged"
                                DataSourceID="SqlDataSourceCasa" DataTextField="nombre_completo"
                                DataValueField="id_cliente">
                            </asp:DropDownList>
                            <asp:SqlDataSource  ID="SqlDataSourceCasa" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" >
                                
                            </asp:SqlDataSource>
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                <label id="lblCargoIntegranteParaReuniones">
                                    Cargo del integrante donde se realizarán las reuniones
                                </label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                 <asp:TextBox ID="txtCargoIntegranteParaReuniones" runat="server" PlaceHolder="Cargo del Integrante"
                            Height="35px" Width="250px"> </asp:TextBox>
                            </div>
                        </div>


                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblCalleDireccionReunion">Calle:</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                 <asp:TextBox ID="txtCalleDireccionReunion" runat="server" PlaceHolder="Calle de la reunión" AutoPostBack="true"
                            Height="35px" Width="250px"> </asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                <label id="lblNumeroDireccionReunion" >Número:</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtNumeroDireccionReunion" runat="server" PlaceHolder="Digita el número" AutoPostBack="true"
                            Height="35px" Width="250px"> </asp:TextBox>
                            </div>
                        </div>


                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblColoniaDireccionReunion">Colonia:</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtColoniaDireccionReunion" runat="server" PlaceHolder="Colonia de la reunión" AutoPostBack="true"
                            Height="35px" Width="250px"> </asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                <label id="lblMunicipioDireccionReunion">Municipio / Delegación:</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtMunicipioDireccionReunion" runat="server" PlaceHolder="Municipio o Delegación" AutoPostBack="true"
                            Height="35px" Width="250px"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblEstadoDireccionReunion">
                                        Estado:
                                </label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtEstadoDireccionReunion" runat="server" PlaceHolder="Estado" AutoPostBack="true"
                            Height="35px" Width="250px"> </asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                <label id="lblTiempoToleranciaReunion">
                                    Tiempo de tolerancia (en min.):
                                </label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                 <asp:TextBox ID="txtTiempoToleranciaReunion" runat="server" CssClass="alingMiddle input-large"   MaxLength="20"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" BehaviorID="txtTiempoToleranciaReunionMinimoSemanalTextBoxWatermarkExtender" TargetControlID="txtTiempoToleranciaReunion" WatermarkText="Digita el número en minutos" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtTiempoToleranciaReunion" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe indicar la Tolerancia " Text="*" ValidationGroup="crea" ControlToValidate="txtTiempoToleranciaReunion" CssClass="alineado errores"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblMultaRetardoReunion">Multa por llegar tarde:</label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtMultaRetardoReunion" runat="server" CssClass="alingMiddle input-large" MaxLength="20"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" BehaviorID="txtMultaRetardoReunionTextBoxWatermarkExtender" TargetControlID="txtMultaRetardoReunion" WatermarkText="Multa Retardo" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtMultaRetardoReunion" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Debe indicar la multa por retardo " Text="*" ValidationGroup="crea" ControlToValidate="txtMultaRetardoReunion" CssClass="alineado errores"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                <label id="lblMultaFaltaConEnvioCompleto" >
                                    Multa por inasistencia con envio completo:
                                </label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                 <asp:TextBox ID="txtMultaFaltaConEnvioCompleto" runat="server" CssClass="alingMiddle input-large" MaxLength="20"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" BehaviorID="txtMultaFaltaConEnvioCompletoTextBoxWatermarkExtender" TargetControlID="txtMultaFaltaConEnvioCompleto" WatermarkText="Multa Retardo" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtMultaFaltaConEnvioCompleto" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Debe indicar la multa por falta " Text="*" ValidationGroup="crea" ControlToValidate="txtMultaFaltaConEnvioCompleto" CssClass="alineado errores"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblMultaFaltaConEnvioIncompleto">
                                    Multa por inasistencia con envio incompleto:
                                </label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtMultaFaltaConEnvioIncompleto" runat="server" CssClass="alingMiddle input-large" MaxLength="20"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" BehaviorID="txtMultaFaltaConEnvioIncompletoTextBoxWatermarkExtender" TargetControlID="txtMultaFaltaConEnvioIncompleto" WatermarkText="Multa Retardo" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtMultaFaltaConEnvioIncompleto" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Debe indicar la multa por falta " Text="*" ValidationGroup="crea" ControlToValidate="txtMultaFaltaConEnvioCompleto" CssClass="alineado errores"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                <label id="lblMultaFalta">
                                    Multa por inasistencia sin envió de pago:
                                </label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                 <asp:TextBox ID="txtMultaFalta" runat="server" CssClass="alingMiddle input-large" MaxLength="20"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" BehaviorID="txtMultaFaltaTextBoxWatermarkExtender" TargetControlID="txtMultaFalta" WatermarkText="Multa por Falta" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtMultaFalta" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Debe indicar la multa por falta " Text="*" ValidationGroup="crea" ControlToValidate="txtMultaFalta" CssClass="alineado errores"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row pad1m marTop textoBold">
                            <div class="col-lg-2 col-sm-2  text-left">
                                <label id="lblHoraTerminaReunion" >
                                    Hora de finalización de la reunión:
                                </label>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtHoraTerminaReunion" runat="server" PlaceHolder="Hora de finalización de la reunión"
                            Height="35px" Width="250px"> </asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-sm-2 text-left">
                                
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                
                            </div>
                        </div>

               

                    <div class="row marTop">
                        <div class="text-center">
                            <asp:LinkButton ID="lnkAgregaActaIntegracion" runat="server" ToolTip="Guarda Solicitud" OnClick="lnkAgregaActaIntegracion_Click"
                                CssClass="btn btn-success t14">
						            <i class="fa fa-save"></i>&nbsp;
						                <span>
							                Guarda Acta de Integración
						                </span>
                            </asp:LinkButton>
                            <%-- boton de salbar los cambios en enacezado ... de inicio estara no visible --%>
                            <asp:LinkButton ID="lnkCambiosActaIntegracion" runat="server" ToolTip="Guarda Cacambios" OnClick="lnkCambiosActaIntegracion_Click"
                                CssClass="btn btn-success t14" Visible="false" >
						            <i class="fa fa-save"></i>&nbsp;
						                <span>
							                Guarda cambios
						                </span>
                            </asp:LinkButton>
                        </div>
                    </div>



                </asp:Panel>

                <%-- este label me servira como bandera para saber si se agrego correctamente el acta --%>
                <div class="row">
                    <asp:Label ID="lblErrorAgregaActa" runat="server" Visible="false" CssClass=""></asp:Label>
                </div>

                <div class="row">
                    <asp:Label ID="lblErrorAgregaActaDetalle" runat="server" Visible="false" CssClass=""></asp:Label>
                </div>

                <%-- 
                    acontinuacion el panel de captura de integrantes...
                --%>

                <asp:Panel ID="pnlIntegrantesActaIntegracion" runat="server" Visible="false">
                    <%-- - Letrero Integrantes- --%>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center alert-info">
                            <h3>Integrantes
                            </h3>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <!--Presidente-->
                        <label id="lblNombreIntegrante" class="text-left textoBold col-lg-6 col-md-6 col-sm-12 col-xs-12">Nombre del integrante</label>
                        <%-- <input class="col-lg-6 col-md-6 col-sm-12 col-xs-12" type="text" placeholder="Nombre del presidente" /> --%>
                        <%--  <telerik:RadTextBox ID="txtNombrePresidente" runat="server"  PlaceHolder="Nombre del presidente"
                                                                                Height="35px" Width="250px" CssClass="col-lg-6 col-md-6 col-sm-12 col-xs-12" > 
                                                            </telerik:RadTextBox>--%>
                        
                        <asp:DropDownList ID="cmbIdIntegrante" runat="server"
                            DataSourceID="SqlDataSource2" DataTextField="nombre_completo"
                            DataValueField="id_cliente" ValidationGroup="crea">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                            ConnectionString="<%$ ConnectionStrings:Taller %>"
                            SelectCommand="select 0 as id_cliente,'Selecione Integrante'as nombre_completo union all  select id_cliente,nombre_completo from an_clientes where id_cliente not in (select id_cliente from AN_Acta_IntegracionDetalle) and id_empresa=@empresa and id_sucursal=@sucursal">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="empresa" QueryStringField="e"
                                    DefaultValue="0" />
                                <asp:QueryStringParameter Name="sucursal"
                                    QueryStringField="t" DefaultValue="0" />
                                <asp:ControlParameter ControlID="lblIdActa" DefaultValue="0" Name="acta" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>

                    <%--Boton agrega integrantes --%>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <asp:LinkButton ID="LinkButton2" runat="server" ToolTip="Guarda Acta" CssClass="btn btn-success t14"
                                OnClick="lnkagregaIntegrantes_Click" ValidationGroup="crea">
                                    <i class="fa fa-save"></i>&nbsp;
                                    <span>
                                        Agrega integrantes
                                    </span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <asp:Label ID="lblIntegrantesError" Text="" runat="server" CssClass="alert-danger"></asp:Label>
                    </div>
                    <%-- 
                            Definicion de etiquetas no visibles como auxiliares para guardar 
                            informacion:  id_empresa, id_sucursal, id_acta
                    --%>

                    <asp:Label ID="lblidEmpresa" runat="server" Visible="false" Text="0" ></asp:Label>
                    <asp:Label ID="lblidSucursal" runat="server" Visible="false" Text="0"></asp:Label>
                    <asp:Label ID="lblidActa" runat="server" Visible="false" Text="0"></asp:Label>

                    <%--div contenedor de datos captura integrantes --%>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <telerik:RadAjaxPanel ID="RadPanAjaxTablaIntegrantes" runat="server" EnableAJAX="false">
                                <%--Aqui va el grid --%>
                                    <telerik:RadGrid ID="gridIntegrntesActaIntegracion" RenderMode="Lightweight" runat="server" Skin="Office2010Blue"
                                    AllowFilteringByColumn="true" EnableHeaderContextMenu="true" Culture="es-Mx"
                                    AllowPaging="true" PageSize="50" AllowSorting="true" AutoGenerateColumns="false"
                                    DataSourceID="SqlDataSource3" >

                                    <MasterTableView DataSourceID="SqlDataSource3" AutoGenerateColumns="false" DataKeyNames="id_cliente">
                                        <Columns>

                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="cargo" 
                                                 HeaderText="Cargo del integrante"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_completo" 
                                                 HeaderText="Nombre del integrante"></telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" HeaderText="Editar" SortExpression="id_cliente" UniqueName="id_cliente" FilterControlAltText="Filtro Editar" DataField="id_cliente">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEliminar" runat="server"  CssClass="btn btn-danger t14" OnClick="lnkEliminar_Click"><i ></i>&nbsp;<span>Eliminar</span></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>   
                                        </Columns>
                                    </MasterTableView>
                                         <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1">
                                           
                                        </Scrolling>
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>    
                                </telerik:RadGrid>

                            </telerik:RadAjaxPanel>
                           
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                                SelectCommand="select a.id_cliente,b.nombre_completo,case a.cargo when 'P' then 'PRESIDENTE' when 'S' then 'SECRETARIA' when 'T' then 'TESORERO' when 'V1' then 'VOCAL 1' when 'V2' then 'VOCAL 2' else 'INTEGRANTE' end as cargo from AN_Acta_IntegracionDetalle a inner 
                                                        join AN_Clientes b on  b.id_cliente = a.id_cliente where a.id_acta = @acta order by a.id_actadetalle"
                                ConnectionString="<%$ ConnectionStrings:Taller %>">
                                <SelectParameters>
                                    <asp:ControlParameter Name="acta" ControlID="lblidActa" />

                                </SelectParameters>

                            </asp:SqlDataSource>
                            <%-- 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ErrorMessage="Debe seleccionar un integrante" Text="*" ValidationGroup="crea"
                                ControlToValidate="cmbIdIntegrante" CssClass="alineado errores">
                            </asp:RequiredFieldValidator>
                            --%>
                        </div>
                        <%--Boton Salir de agregar integrantes --%>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                                <asp:LinkButton ID="LinkButton3" runat="server" ToolTip="Salir" CssClass="btn btn-success t14"
                                    OnClick="lnkcierraagregaIntegrantes_Click" ValidationGroup="crea">
                                        <i class="fa fa-save"></i>&nbsp;
                                        <span>
                                            Salir 
                                        </span>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <%--FIN div contenedor de datos captura integrantes --%>
                </asp:Panel>









            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

