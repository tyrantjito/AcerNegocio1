<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="FichaDatos.aspx.cs" Inherits="FichaDatos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>
    <div class="page-header">
        <!-- /BREADCRUMBS -->
        <div class="clearfix">
            <h3 class="content-title pull-left">Cédula del Cliente </h3>
        </div>
    </div>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">

                <div class=" col-lg-6 col-sm-6 text-center">
                    <asp:LinkButton ID="lnkAbreWindow" runat="server" CssClass="btn btn-success t14" OnClick="lnkAbreWindow_Click"><i class="fa fa-save"></i>&nbsp;<span>Genera Cédula</span></asp:LinkButton>
                </div>
                <div class=" col-lg-6 col-sm-6 text-center">
                    <asp:LinkButton ID="lnkAbreEdicion" runat="server" Visible="false" CssClass="btn btn-warning t14" OnClick="lnkAbreEdicion_Click"><i class="fa fa-save"></i>&nbsp;<span>Editar Cédula</span></asp:LinkButton>
                </div>

                <div class=" col-lg-12 col-sm-12 text-center">
                    <asp:Label ID="lblErrorAfuera" CssClass="alert-danger" runat="server" Visible="false"></asp:Label>
                </div>

            </div>


            <div class="ancho100 marTop">
                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" EnableAJAX="true">
                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" AllowFilteringByColumn="true" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged"
                        EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource1" AllowSorting="true" GroupingEnabled="false" PageSize="50">
                        <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="false" DataKeyNames="id_ficha,id_cliente">
                            <Columns>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_completo" FilterControlAltText="Filtro Nombre" HeaderText="Nombre" SortExpression="nombre_completo" UniqueName="nombre_completo" Resizable="true" />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="rfc_cli" FilterControlAltText="Filtro RFC" HeaderText="RFC" SortExpression="rfc_cli" UniqueName="rfc_cli" Resizable="true" />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="genero_cli" FilterControlAltText="Filtro Genero" HeaderText="Genero" SortExpression="genero_cli" UniqueName="genero_cli" Resizable="true" />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nivel_escolaridad" FilterControlAltText="Filtro Nivel Escolaridad" HeaderText="Nivel Escolaridad" SortExpression="nivel_escolaridad" UniqueName="nivel_escolaridad" Resizable="true" />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="select a.id_ficha,a.id_cliente,a.rfc_cli,case a.genero_cli when 'H' then 'HOMBRE' when 'M' then 'MUJER' else '' end as genero_cli,case a.nivel_escolaridad when 'SIN' then 'SIN INSTRUCCION' when 'PRI' then 'PRIMARIA' when'SEC' then 'SECUNDARIA' when'BAC' then 'BACHILLERATO' when'LIC' then 'LICENCIATURA' when 'POS' then 'POSGRADO' else '' end as nivel_escolaridad,b.nombre_completo from AN_Ficha_Datos a inner join an_Clientes b on b.id_cliente=a.id_cliente where a.id_sucursal=@sucursal and a.id_empresa=@empresa order by a.id_ficha desc" ConnectionString="<%$ ConnectionStrings:Taller %>">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0" />
                        <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
            <div class="row marTop text-center">
                <asp:LinkButton ID="lnkImprimir" runat="server" Visible="false" ToolTip="Imprimir Solicitud" OnClick="lnkImprimir_Click " CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Cédula</span></asp:LinkButton>
            </div>


            <asp:Panel ID="Sec_Fotos" runat="server" Visible="false">

                <div class="row">
                    <div class="col-lg-12 col-sm-12 text-center alert-info">
                        <h3>
                            <i class="fa fa-instagram"></i>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label110" runat="server" Text="Documentacion"></asp:Label>
                        </h3>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12 col-sm-12 text-center">
                        <asp:Label ID="lblErrorFotos" runat="server" CssClass="errores" />
                    </div>
                </div>

                <div class="row col-lg-12 col-sm-12 text-center pad1em">
                    <div class="DropZone1">
                        <i class="fa fa-plus-square fa-3x"></i>
                        <br />
                        <span>
                            <h4>Arrastre los archivos a cargar</h4>
                        </span>
                        <br />
                        <span>Tipo Archivos Permitidos: .pdf,.jpg,.jpeg,.tiff,.bmp,.png,.doc,.docx,.xls,.xlsx,.gif</span>
                    </div>
                    <br />
                    <br />
                    <br />
                    <br />
                </div>

                <br />
                <br />
                <div class="row">
                </div>
                <div class="row col-lg-12 col-sm-12">
                    <asp:Label ID="lblError2" runat="server" CssClass="text-danger"></asp:Label>
                    <asp:Label ID="lblIdLevantamiento2" runat="server" Visible="false"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" ValidationGroup="cargar" DisplayMode="List" />
                </div>



                <div class="row pad1m">
                    <div class="col-lg-3 col-sm-3 text-center ">
                        <br />
                        <br />
                        <div class="col-lg-6 col-sm-6 text-left ">
                            <telerik:RadAsyncUpload Visible="true" RenderMode="Lightweight" runat="server" ID="rauAdjunto"
                                MultipleFileSelection="Automatic" DropZones=".DropZone1" AllowedFileExtensions=".jpeg,.jpg,.png,.gif,.bmp,.tiff,.JPEG,.JPG,.PNG,.GIF" />

                            <asp:LinkButton ID="btnAddFotoDanos" runat="server" ToolTip="Agregar Foto" OnClick="lnkAdjuntarDoctos_Click"
                                CssClass="alingMiddle btn btn-info t14"><i class="fa fa-plus-circle"></i>&nbsp;<span>Agregar Fotograf&iacute;a</span></asp:LinkButton>
                        </div>

                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />

                        <div class="col-lg-6 col-sm-6 text-left ">
                            <telerik:RadAsyncUpload Visible="true" RenderMode="Lightweight" runat="server" ID="rauAdjuntoNeg"
                                MultipleFileSelection="Automatic" DropZones=".DropZone1" AllowedFileExtensions=".jpeg,.jpg,.png,.gif,.bmp,.tiff,.JPEG,.JPG,.PNG,.GIF" />

                            <asp:LinkButton ID="LinkButton2" runat="server" ToolTip="Agregar Foto" OnClick="lnkAdjuntarDoctosNeg_Click"
                                CssClass="alingMiddle btn btn-info t14"><i class="fa fa-plus-circle"></i>&nbsp;<span>Agregar Fotograf&iacute;a Negocio</span></asp:LinkButton>
                        </div>

                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <div class="col-lg-6 col-sm-6 text-left ">
                            <telerik:RadAsyncUpload Visible="true" RenderMode="Lightweight" runat="server" ID="rauAdjuntoID"
                                MultipleFileSelection="Automatic" DropZones=".DropZone1" AllowedFileExtensions=".jpeg,.jpg,.png,.gif,.bmp,.tiff,.JPEG,.JPG,.PNG,.GIF" />

                            <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Agregar Foto" OnClick="lnkAdjuntarDoctosID_Click"
                                CssClass="alingMiddle btn btn-info t14"><i class="fa fa-plus-circle"></i>&nbsp;<span>Agregar ID</span></asp:LinkButton>
                        </div>

                    </div>

                    <div class="col-lg-3 col-sm-3 text-center ">

                        <br />
                        <br />
                        <div class="col-lg-6 col-sm-6 text-left ">
                            <div class="col-lg-6 col-sm-6 text-left ">
                                <telerik:RadAsyncUpload Visible="true" RenderMode="Lightweight" runat="server" ID="raureporte"
                                    MultipleFileSelection="Automatic" DropZones=".DropZone1" AllowedFileExtensions=".jpeg,.jpg,.png,.gif,.bmp,.tiff,.JPEG,.JPG,.PNG,.GIF" />

                                <asp:LinkButton ID="LinkButton3" runat="server" ToolTip="Agregar Foto" OnClick="lnkAdjuntarDoctosReporte_Click"
                                    CssClass="alingMiddle btn btn-info t14"><i class="fa fa-plus-circle"></i>&nbsp;<span>Agregar Reporte</span></asp:LinkButton>
                            </div>


                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />

                            <div class="col-lg-6 col-sm-6 text-left ">
                                <telerik:RadAsyncUpload Visible="true" RenderMode="Lightweight" runat="server" ID="raiIdPerm"
                                    MultipleFileSelection="Automatic" DropZones=".DropZone1" AllowedFileExtensions=".jpeg,.jpg,.png,.gif,.bmp,.tiff,.JPEG,.JPG,.PNG,.GIF" />

                                <asp:LinkButton ID="LinkButton4" runat="server" ToolTip="Agregar Foto" OnClick="lnkAdjuntarDoctosPerma_Click"
                                    CssClass="alingMiddle btn btn-info t14"><i class="fa fa-plus-circle"></i>&nbsp;<span>Agregar Id Permanente</span></asp:LinkButton>
                            </div>


                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <div class="col-lg-6 col-sm-6 text-left ">
                                <telerik:RadAsyncUpload Visible="true" RenderMode="Lightweight" runat="server" ID="raucurp"
                                    MultipleFileSelection="Automatic" DropZones=".DropZone1" AllowedFileExtensions=".jpeg,.jpg,.png,.gif,.bmp,.tiff,.JPEG,.JPG,.PNG,.GIF" />

                                <asp:LinkButton ID="LinkButton5" runat="server" ToolTip="Agregar Foto" OnClick="lnkAdjuntarDoctosCurpAct_Click"
                                    CssClass="alingMiddle btn btn-info t14"><i class="fa fa-plus-circle"></i>&nbsp;<span>Agregar CURP o Acta</span></asp:LinkButton>
                            </div>
                        </div>
                    </div>


                    <div class="col-lg-6 col-sm-6 text-center ">
                        <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" EnableAJAX="true">
                            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid2" AllowFilteringByColumn="true" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="WebBlue" OnSelectedIndexChanged="RadGrid2_SelectedIndexChanged"
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource3" AllowSorting="true" GroupingEnabled="false" PageSize="50">
                                <MasterTableView DataSourceID="SqlDataSource3" AutoGenerateColumns="False" DataKeyNames="Id_Ficha_Adjunto, descripcion_adjunto">
                                    <Columns>
                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="descripcion_adjunto" FilterControlAltText="Filtro Adjunto" HeaderStyle-Width="200px" HeaderText="Adjunto" SortExpression="descripcion_adjunto" UniqueName="desripcion_adjunto" Resizable="true" Visible="true" />
                                        <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="150px" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnZoom" runat="server" CssClass="btn btn-info" OnClick="btnZoom_Click" CommandArgument='<%# Eval("Id_Ficha_Adjunto")%>'><i class="fa fa-search-plus"></i><span>&nbsp;</span></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-danger" CommandArgument='<%# Eval("id_ficha") + ";" + Eval("Id_Ficha_Adjunto") %>' OnClick="btnEliminarAdjunto_Click" OnClientClick="return confirm('¿Está seguro de eliminar el adjunto?')"><i class="fa fa-trash"></i><span>&nbsp;Eliminar</span></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="No existen Archivos asignados a la Ficha" CssClass="text-danger"></asp:Label>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                                </ClientSettings>
                                <PagerStyle PageSizeControlType="RadDropDownList" Mode="NextPrevAndNumeric"></PagerStyle>
                            </telerik:RadGrid>
                        </telerik:RadAjaxPanel>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select id_ficha,id_ficha_adjunto,extension,adjunto,descripcion_adjunto,ruta from AN_Adjunto_Ficha_Datos where id_empresa=@empresa and id_sucursal=@sucursal and id_ficha=@id_ficha">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0" />
                                <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0" />
                                <asp:ControlParameter ControlID="RadGrid1" Name="id_ficha" DefaultValue="0" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>



                <div class="row marTop">
                    <div class="col-lg-6 col-sm-6 text-center">
                        <asp:LinkButton ID="lnkArchivo" runat="server" OnClick="lnkArchivo_Click" CssClass="btn btn-primary"><i class="fa fa-download"></i><span>&nbsp;Descargar&nbsp;</span></asp:LinkButton>
                    </div>
                </div>

            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkArchivo" />
        </Triggers>
    </asp:UpdatePanel>




    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMask" runat="server" CssClass="mask zen1" Visible="false" />
            <asp:Panel ID="windowAutorizacion" runat="server" CssClass="popUp zen2 textoCentrado ancho80" Height="80%" ScrollBars="Auto" Visible="false">


                <table class="ancho100">
                    <tr class="ancho100%">

                        <div class="page-header">
                            <div class="clearfix">
                                <div class=" col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                                    <asp:LinkButton ID="LinkButton6" runat="server" CssClass="btn btn-danger alingMiddle" OnClick="lnkCerrar_Click" ToolTip="Cerrar"><i class="fa fa-remove t18"></i></asp:LinkButton>
                                </div>
                                <h3 class="content-title center">Cédula Cliente
                                </h3>
                                <asp:Label ID="lblTitulo" runat="server" Text="Nuevo Crédito" Visible="false" CssClass="t22 colorMorado textoBold" />

                            </div>

                        </div>
                    </tr>

                </table>

                <%-- Empieza formulario --%>
                <div class="row text-center pad1m">
                    <div class="col-lg-12 col-sm-12 text-center">
                    </div>
                </div>
                <cc1:Accordion ID="acNuevaRecep" runat="server" CssClass="ancho95 centrado" FadeTransitions="true" HeaderCssClass="encabezadoAcordeonPanel" HeaderSelectedCssClass="encabezadoAcordeonPanelSelect">
                    <Panes>
                        <cc1:AccordionPane ID="acpPersonales" runat="server" CssClass="ancho95" Visible="true" Style="cursor: pointer;">
                            <Header>Datos Personales</Header>
                            <Content>
                                <div class="row textoBold pad1m">
                                    <div class="col-lg-2 col-sm-2 text-left">
                                        <asp:Label ID="Label129" runat="server" Text="Nombre Completo:" CssClass="alingMiddle textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-4 col-sm-4 text-center">
                                        <asp:TextBox ID="txt_nombre" runat="server" CssClass="alingMiddle input-large" MaxLength="100" Visible="false" ReadOnly="true" PlaceHolder=""></asp:TextBox>
                                        <asp:DropDownList ID="cmb_nombre" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                            OnSelectedIndexChanged="cmbPersonaSelected" DataSourceID="cmbNombre" DataValueField="id_cliente" DataTextField="nombre_completo">
                                            <asp:ListItem Value="0">Selecciona Cliente</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="cmbNombre" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:Taller %>" 
                                            SelectCommand="select a.id_cliente,b.nombre_completo from an_solicitud_consulta_buro a inner join an_clientes b on b.id_cliente=a.id_cliente where a.id_empresa=@empresa and a.id_sucursal=@sucursal and a.procesable='FA1'">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                                                <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>

                                <div class="row textoBold pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label111" runat="server" Text="Fecha de Nacimiento:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <telerik:RadDatePicker ID="txtFecha_cli" runat="server">
                                            <DateInput ID="DateInput3" runat="server" DateFormat="yyyy/MM/dd">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="validacionFechaCli" runat="server" ErrorMessage="Debe indicar Fecha " Text="*" ValidationGroup="crea" ControlToValidate="txtFecha_cli" CssClass="alineado errores"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Entidad_cli" runat="server" Text="Entidad de Nacimiento:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:DropDownList ID="cmbEstados" runat="server" AutoPostBack="true">
                                            <asp:ListItem Value="SUP">Seleccione Estado</asp:ListItem>
                                            <asp:ListItem Value="AGS">Aguascalientes</asp:ListItem>
                                            <asp:ListItem Value="BCN">Baja California</asp:ListItem>
                                            <asp:ListItem Value="BCS">Baja California Sur</asp:ListItem>
                                            <asp:ListItem Value="CAM">Campeche</asp:ListItem>
                                            <asp:ListItem Value="CHP">Chiapas</asp:ListItem>
                                            <asp:ListItem Value="CHI">Chihuahua</asp:ListItem>
                                            <asp:ListItem Value="CDF">Ciudad de México</asp:ListItem>
                                            <asp:ListItem Value="COA">Coahuila	</asp:ListItem>
                                            <asp:ListItem Value="COL">Colima</asp:ListItem>
                                            <asp:ListItem Value="DUR"> Durango</asp:ListItem>
                                            <asp:ListItem Value="GTO">Guanajuato</asp:ListItem>
                                            <asp:ListItem Value="GRO">Guerrero</asp:ListItem>
                                            <asp:ListItem Value="HGO">Hidalgo</asp:ListItem>
                                            <asp:ListItem Value="JAL">Jalisco</asp:ListItem>
                                            <asp:ListItem Value="MEX">México</asp:ListItem>
                                            <asp:ListItem Value="MIC"> Michoacán</asp:ListItem>
                                            <asp:ListItem Value="MOR">Morelos</asp:ListItem>
                                            <asp:ListItem Value="NAY">Nayarit</asp:ListItem>
                                            <asp:ListItem Value="NLE">Nuevo León</asp:ListItem>
                                            <asp:ListItem Value="OAX">Oaxaca</asp:ListItem>
                                            <asp:ListItem Value="PUE">Puebla</asp:ListItem>
                                            <asp:ListItem Value="QRO">Querétaro</asp:ListItem>
                                            <asp:ListItem Value="ROO">Quintana Roo</asp:ListItem>
                                            <asp:ListItem Value="SLP"> San Luis Potosí</asp:ListItem>
                                            <asp:ListItem Value="SIN">Sinaloa</asp:ListItem>
                                            <asp:ListItem Value="SON">Sonora</asp:ListItem>
                                            <asp:ListItem Value="TAB">Tabasco</asp:ListItem>
                                            <asp:ListItem Value="TAM">Tamaulipas</asp:ListItem>
                                            <asp:ListItem Value="TLX">Tlaxcala</asp:ListItem>
                                            <asp:ListItem Value="VER"> Veracruz</asp:ListItem>
                                            <asp:ListItem Value="YUC">Yucatán</asp:ListItem>
                                            <asp:ListItem Value="ZAC">Zacatecas</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Sexo" runat="server" Text="Sexo:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:DropDownList ID="cmb_gen_cli" runat="server" AutoPostBack="true" AppendDataBoundItems="true" DataSourceID="SQLGenero" DataValueField="codigo_genero" DataTextField="nombre_genero">
                                            <asp:ListItem Value="0">Selecciona Genero</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SQLGenero" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select *from PLD_Genero"></asp:SqlDataSource>
                                    </div>

                                </div>

                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="EstadoC" runat="server" Text="Estado Civil:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:DropDownList ID="cmb_ec_cli" runat="server" AutoPostBack="true" AppendDataBoundItems="true" DataSourceID="SQLeCivil" DataValueField="codigo_ecivil" DataTextField="nombre_ecivil">
                                            <asp:ListItem Value="0">Selecciona Estado Civil</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SQLeCivil" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select *from PLD_Estado_Civil"></asp:SqlDataSource>


                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label2" runat="server" Text="N°.Cred.IFE o INE:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_cre_cli" runat="server" CssClass="alingMiddle input-large" MaxLength="15"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="txtMontoWatermarkExtender1" runat="server" BehaviorID="txtMonto_TextBoxWatermarkExtender" TargetControlID="txt_cre_cli" WatermarkText="IFE/INE" WatermarkCssClass="water input-large" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_cre_cli" />
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label4" runat="server" Text="CURP:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_curp_cli" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="18"
                                            PlaceHolder="CURP"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label6" runat="server" Text="RFC:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_rfc_cli" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="13"
                                            PlaceHolder="RFC"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ValidacionRFCCli" runat="server" ErrorMessage="Debe indicar RFC " Text="*" ValidationGroup="crea" ControlToValidate="txt_rfc_cli" CssClass="alineado errores"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label5" runat="server" Text="Nivel Escolaridad:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:DropDownList ID="cmb_ne_cli" runat="server" AutoPostBack="true" AppendDataBoundItems="true" DataSourceID="SQLEscolaridad" DataValueField="codigo_neducacion" DataTextField="nombre_neducacion">
                                            <asp:ListItem Value="0">Selecciona Escolaridad</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SQLEscolaridad" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select *from PLD_Nivel_Escolaridad"></asp:SqlDataSource>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label8" runat="server" Text="Rol del Cliente:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:DropDownList ID="cmb_rol_cli" runat="server">
                                            <asp:ListItem Value="Ocupación">Ocupación</asp:ListItem>
                                            <asp:ListItem Value="Jefa(e)">Jefa(e)</asp:ListItem>
                                            <asp:ListItem Value="Pareja">Pareja</asp:ListItem>
                                            <asp:ListItem Value="Hijo(a)">Hijo(a)</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label7" runat="server" Text="N° de Hijos:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txtn_hijos_cli" runat="server" CssClass="alingMiddle input-large" MaxLength="2"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" BehaviorID="txtn_hijos_cliTextBoxWatermarkExtender" TargetControlID="txtn_hijos_cli" WatermarkText="N° Hijos" WatermarkCssClass="water input-large" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtn_hijos_cli" />
                                        <asp:RequiredFieldValidator ID="validacionHijosCli" runat="server" ErrorMessage="Debe indicar el numero de Hijos " Text="*" ValidationGroup="crea" ControlToValidate="txtn_hijos_cli" CssClass="alineado errores"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label10" runat="server" Text="Dep.Economicos:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_eco_cli" runat="server" CssClass="alingMiddle input-large" MaxLength="2"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" BehaviorID="txt_eco_cliTextBoxWatermarkExtender" TargetControlID="txt_eco_cli" WatermarkText="N° Dependientes" WatermarkCssClass="water input-large" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_eco_cli" />
                                        <asp:RequiredFieldValidator ID="validacionDepEcoCli" runat="server" ErrorMessage="Debe indicar el numero de dependientes " Text="*" ValidationGroup="crea" ControlToValidate="txt_eco_cli" CssClass="alineado errores"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label9" runat="server" Text="Teléfono(con Lada):"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_tel_cli" runat="server" CssClass="alingMiddle input-large" MaxLength="16"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" BehaviorID="txt_tel_cliTextBoxWatermarkExtender" TargetControlID="txt_tel_cli" WatermarkText="Teléfono" WatermarkCssClass="water input-large" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_cli" />
                                        <asp:RequiredFieldValidator ID="validacionTelefonoCli" runat="server" ErrorMessage="Debe indicar el numero Telefonico " Text="*" ValidationGroup="crea" ControlToValidate="txt_tel_cli" CssClass="alineado errores"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label122" runat="server" Text="Teléfono Cel:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_cel_cli" runat="server" CssClass="alingMiddle input-large" MaxLength="20"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" BehaviorID="txt_cel_cliTextBoxWatermarkExtender" TargetControlID="txt_cel_cli" WatermarkText="Teléfono Celular" WatermarkCssClass="water input-large" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_cel_cli" />
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label11" runat="server" Text="Correo Electronico:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_correo_cli" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="150"
                                            PlaceHolder="ejemplo@correo.com"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                    </div>
                                </div>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="acpDomicilio" runat="server" CssClass="ancho95" Visible="true">
                            <Header>Domicilio</Header>
                            <Content>
                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label13" runat="server" Text="Calle:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_calle_cli" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="Calle"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label14" runat="server" Text="N° Exterior:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_n_ext_cli" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="N° Exterior"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label124" runat="server" Text="N° Interior:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_nint_cli" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="N° Interior"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row pad1m">


                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label128" runat="server" Text="C.P. :"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">

                                        <asp:TextBox ID="txt_cp_cli" runat="server" OnTextChanged="EstadoCp" AutoPostBack="true"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="Cp"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label125" runat="server" Text="Delegacion o Municipio:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_del_cli" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="100" AutoPostBack="true"
                                            PlaceHolder="Delegacion o Municipio"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label15" runat="server" Text="Estado:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_estado_cli" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="100" AutoPostBack="true"
                                            PlaceHolder="Estado"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label109" runat="server" Text="Colonia:"></asp:Label>
                                    </div>

                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_colonia" runat="server" Visible="false" ReadOnly="true"
                                            CssClass="alingMiddle input-large" MaxLength="100" AutoPostBack="true"
                                            PlaceHolder=""></asp:TextBox>
                                        <asp:DropDownList ID="cmbColonia_cli" runat="server" Visible="true" DataSourceID="SqlDataSourceCombo" DataValueField="d_codigo"
                                            DataTextField="d_asenta">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSourceCombo" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"></asp:SqlDataSource>
                                    </div>

                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label12" runat="server" Text="Tipo Domicilio:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:DropDownList ID="txtTipo" runat="server" AutoPostBack="true" AppendDataBoundItems="true" DataSourceID="SQLDomicilio" DataValueField="codigo_tvivienda" DataTextField="nombre_tvivienda">
                                            <asp:ListItem Value="0">Selecciona Opcion</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SQLDomicilio" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select *from PLD_Tipo_Vivienda"></asp:SqlDataSource>

                                    </div>

                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label16" runat="server" Text="Tiempo Residencia:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_resi_cli" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="T.Residencia"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label17" runat="server" Text="N° Habitantes:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txtn_habi_cli" runat="server" CssClass="alingMiddle input-large" MaxLength="2"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" BehaviorID="txtn_habi_cliTextBoxWatermarkExtender" TargetControlID="txtn_habi_cli" WatermarkText="Numero de Habitantes" WatermarkCssClass="water input-large" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtn_habi_cli" />
                                        <asp:RequiredFieldValidator ID="validacionnohabicli" runat="server" ErrorMessage="Debe indicar el numero de habitantes " Text="*" ValidationGroup="crea" ControlToValidate="txtn_habi_cli" CssClass="alineado errores"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                    </div>
                                </div>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="acpEsposo" runat="server" CssClass="ancho95" Visible="true">
                            <Header>Datos Generales Esposo (a) Concubino (a)</Header>
                            <Content>
                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label20" runat="server" Text="Nombre(s):"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_nom_es" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="Nombre(s)"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label21" runat="server" Text="Apellidos:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_a_pat_es" runat="server" MaxLength="50"
                                            PlaceHolder="A.Paterno"></asp:TextBox>
                                        <asp:TextBox ID="txt_a_mat_es" runat="server" MaxLength="50"
                                            PlaceHolder="A.Materno"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label22" runat="server" Text="Ocupación:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_ocu_esp" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="Ocupación"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label25" runat="server" Text="Teléfono Trabajo:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_tel_trab_esp" runat="server" CssClass="alingMiddle input-large" MaxLength="16"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" BehaviorID="txt_tel_trab_espTextBoxWatermarkExtender" TargetControlID="txt_tel_trab_esp" WatermarkText="Número Teléfono" WatermarkCssClass="water input-large" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_trab_esp" />

                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label133" runat="server" Text="Teléfono Casa:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_tel_casa_esp" runat="server" CssClass="alingMiddle input-large" MaxLength="16"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" BehaviorID="txt_tel_casa_espTextBoxWatermarkExtender" TargetControlID="txt_tel_casa_esp" WatermarkText="Número Teléfono" WatermarkCssClass="water input-large" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_casa_esp" />

                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label140" runat="server" Text="Télefono Celular:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_tel_cel_esp" runat="server" CssClass="alingMiddle input-large" MaxLength="20"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender30" runat="server" BehaviorID="txt_tel_cel_espTextBoxWatermarkExtender" TargetControlID="txt_tel_cel_esp" WatermarkText="Número Teléfono Celular" WatermarkCssClass="water input-large" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender31" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_cel_esp" />

                                    </div>
                                </div>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane1" runat="server" CssClass="ancho95" Visible="true">
                            <Header>Información de Negocio</Header>
                            <Content>
                                <div class="row marTop">
                                    <div class="col-lg-3 col-sm-3 text-center">
                                        <asp:DropDownList ID="cmb_dom" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem>Ocupa mismo Domicilio</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>SI</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label27" runat="server" Text="Calle:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_calle_neg" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="Calle"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label28" runat="server" Text="N° Exterior:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_n_exterior_neg" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="N° Exterior"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label29" runat="server" Text="N° Interior:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_n_int_neg" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="N° interior"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label31" runat="server" Text="Código Postal:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_cp_neg" runat="server" OnTextChanged="EstadoCp2" AutoPostBack="true"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="C.P."></asp:TextBox>
                                    </div>


                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label33" runat="server" Text="Estado:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_estado_neg" runat="server" AutoPostBack="true"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="Estado"></asp:TextBox>
                                    </div>


                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label32" runat="server" Text="Delegación o Municipio:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_del_neg" runat="server" AutoPostBack="true"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="Delegación o Municipio"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label30" runat="server" Text="Colonia o Localidad:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_colo_neg" runat="server" Visible="false" ReadOnly="true"
                                                CssClass="alingMiddle input-large" MaxLength="100" AutoPostBack="true"
                                                PlaceHolder=""></asp:TextBox>
                                            <asp:DropDownList ID="cmb_coloNeg" runat="server" Visible="true" DataSourceID="SqlDataSourceCombo2" DataValueField="d_codigo"
                                                DataTextField="d_asenta">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSourceCombo2" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"></asp:SqlDataSource>
                                        </div>
                                    </div>

                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label34" runat="server" Text="Teléfono Fijo:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_tel_feijo_neg" runat="server" CssClass="alingMiddle input-large" MaxLength="16"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" BehaviorID="txt_tel_feijo_negTextBoxWatermarkExtender" TargetControlID="txt_tel_feijo_neg" WatermarkText="Número Teléfono Celular" WatermarkCssClass="water input-large" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_feijo_neg" />
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label35" runat="server" Text="Antigüedad:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_anti_neg" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="Antigüedad"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label36" runat="server" Text="Nombre o Razón Social:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_rz_neg" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="Nombre o Razón"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label37" runat="server" Text="Tipo de Establecimiento:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:DropDownList ID="cmbt_es_cli" runat="server">
                                            <asp:ListItem Value="Fijo">Fijo</asp:ListItem>
                                            <asp:ListItem Value="Semifijo">SemiFijo</asp:ListItem>
                                            <asp:ListItem Value="Ambulante">Ambulante</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label38" runat="server" Text="N° Empleos:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_eper_neg" runat="server" CssClass="alingMiddle input-large" MaxLength="2"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" BehaviorID="txt_eper_negTextBoxWatermarkExtender" TargetControlID="txt_eper_neg" WatermarkText="Fijos" WatermarkCssClass="water input-large" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_eper_neg" />
                                        <asp:TextBox ID="txt_eeve_neg" runat="server" CssClass="alingMiddle input-large" MaxLength="2"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender11" runat="server" BehaviorID="txt_eeve_negTextBoxWatermarkExtender" TargetControlID="txt_eeve_neg" WatermarkText="Eventuales" WatermarkCssClass="water input-large" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_eeve_neg" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Debe indicar el numero de empleados " Text="*" ValidationGroup="crea" ControlToValidate="txt_eeve_neg" CssClass="alineado errores"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label39" runat="server" Text="Giro Principal:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:DropDownList ID ="txt_gp_neg" runat="server" Width="200px" AutoPostBack="true" AppendDataBoundItems="true" DataSourceID="SQLaEconomica" DataValueField="codigo_actividad" DataTextField="nombre_actividad">
                                            <asp:ListItem Value="0">Selecciona Opcion</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SQLaEconomica" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select *from PLD_Actividad_Economica"></asp:SqlDataSource>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label40" runat="server" Text="Ingreso Mensual:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_imgp_neg" runat="server" CssClass="alingMiddle input-large" MaxLength="50"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender12" runat="server" BehaviorID="txt_imgp_negTextBoxWatermarkExtender" TargetControlID="txt_imgp_neg" WatermarkText="Ingreso" WatermarkCssClass="water input-large" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_imgp_neg" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Debe indicar el ingreso mensual " Text="*" ValidationGroup="crea" ControlToValidate="txt_imgp_neg" CssClass="alineado errores"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label41" runat="server" Text="Otras Actividades:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_oa_neg" runat="server"
                                            CssClass="alingMiddle input-large" MaxLength="100"
                                            PlaceHolder="Otras Actividades"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row pad1m">
                                    <div class="col-lg-1 col-sm-1 text-left">
                                        <asp:Label ID="Label42" runat="server" Text="Ingreso Mensual:"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:TextBox ID="txt_imoa_neg" runat="server" CssClass="alingMiddle input-large" MaxLength="50"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" BehaviorID="txt_imoa_negTextBoxWatermarkExtender" TargetControlID="txt_imoa_neg" WatermarkText="Ingreso Otras Actividades" WatermarkCssClass="water input-large" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_imoa_neg" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Debe indicar el ingreso mensual " Text="*" ValidationGroup="crea" ControlToValidate="txt_imoa_neg" CssClass="alineado errores"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                    </div>
                                    <div class="col-lg-1 col-sm-1 text-left">
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                    </div>
                                </div>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane2" runat="server" CssClass="ancho95" Visible="true">
                            <Header>Referencias del Cliente</Header>
                            <Content>
                                <div class="row pad1m">
                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label43" runat="server" Text="Nombre Completo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nom_ref" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Nombre Completo"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label44" runat="server" Text="Telefono Fijo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_tel_fijo_ref" runat="server" CssClass="alingMiddle input-large" MaxLength="16"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender14" runat="server" BehaviorID="txt_tel_fijo_refTextBoxWatermarkExtender" TargetControlID="txt_tel_fijo_ref" WatermarkText="Teléfono" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_fijo_ref" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Debe indicar el telefono ref " Text="*" ValidationGroup="crea" ControlToValidate="txt_tel_fijo_ref" CssClass="alineado errores"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label45" runat="server" Text="Telefono Celular:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_tel_cel_ref" runat="server" CssClass="alingMiddle input-large" MaxLength="20"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender15" runat="server" BehaviorID="txt_tel_cel_refTextBoxWatermarkExtender" TargetControlID="txt_tel_cel_ref" WatermarkText="Teléfono Celular" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_cel_ref" />

                                        </div>

                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label47" runat="server" Text="Parentesco o Relacion:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">

                                            <%--<asp:TextBox ID="txt_paren_ref" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Parentesco o Relacion"></asp:TextBox>--%>
                                            <asp:DropDownList ID="txt_paren_ref" runat="server" AutoPostBack="true" AppendDataBoundItems="true" DataSourceID="SQLParentesco" DataValueField="codigo_treferencia" DataTextField="nombre_treferencia">
                                            <asp:ListItem Value="0">Selecciona Opcion</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SQLParentesco" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select *from PLD_Tipo_Referencia"></asp:SqlDataSource>


                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label48" runat="server" Text="Tiempo de Conocerlo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_tiem_ref" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Tiempo de Conocerlo"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label18" runat="server" Text="Correo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_correo_ref" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Correo"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label57" runat="server" Text="Nombre Completo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nombrecompleto_ref2" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Nombre Completo"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label115" runat="server" Text="Telefono Fijo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_tel_fijo_ref2" runat="server" CssClass="alingMiddle input-large" MaxLength="16"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender28" runat="server" BehaviorID="txt_tel_fijo_refTextBoxWatermarkExtender" TargetControlID="txt_tel_fijo_ref" WatermarkText="Teléfono" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_fijo_ref" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Debe indicar el telefono ref " Text="*" ValidationGroup="crea" ControlToValidate="txt_tel_fijo_ref" CssClass="alineado errores"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label126" runat="server" Text="Telefono Celular:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_tel_cel_ref2" runat="server" CssClass="alingMiddle input-large" MaxLength="20"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender29" runat="server" BehaviorID="txt_tel_cel_refTextBoxWatermarkExtender" TargetControlID="txt_tel_cel_ref" WatermarkText="Teléfono Celular" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender30" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_cel_ref" />

                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label130" runat="server" Text="Parentesco o Relacion:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_parentesco_ref2" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Parentesco o Relacion"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label131" runat="server" Text="Tiempo de Conocerlo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_tiempo_conocerlo_ref2" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Tiempo de Conocerlo"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label19" runat="server" Text="Correo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txtCorreo_ref2" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Correo"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </Content>
                        </cc1:AccordionPane>

                        <cc1:AccordionPane ID="AccordionPane4" runat="server" CssClass="ancho95" Visible="true">
                            <Header>Tipo de Cliente</Header>
                            <Content>
                                <div class="row pad1m">
                                    <div class="col-lg-12 col-sm-12 text-center">
                                        <asp:Label ID="Label3" runat="server" Text="Usted ha ocupado cargos publicos destacados en los ultimos doce meses:"></asp:Label>
                                    </div>

                                    <div class="col-lg-12 col-sm-12 text-center">
                                        <asp:DropDownList ID="cmb_preg1_ref" OnTextChanged="cmb_preg1_refIndexChanged" AutoPostBack="true" runat="server" AppendDataBoundItems="true" DataSourceID="SQLPEP" DataValueField="codigo_tpep" DataTextField="nombre_tpep">
                                            <asp:ListItem Value="0">Selecciona Opcion</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SQLPEP" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select *from PLD_Tipo_Pep"></asp:SqlDataSource>
                                    </div>

                                    <div class="row pad1m">

                                        <div class="col-lg-2 col-sm-2 text-left">
                                            <asp:Label ID="Label113" runat="server" Text="Cargo Desempeñado:"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-2 text-center">
                                            <asp:TextBox ID="txt_carg_ref" runat="server"
                                                CssClass="alingMiddle input-small" MaxLength="100"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2 col-sm-2 text-left">
                                            <asp:Label ID="Label114" runat="server" Text="Dependencia:"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-2 text-center">
                                            <asp:TextBox ID="txt_dep_ref" runat="server"
                                                CssClass="alingMiddle input-small" MaxLength="100"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2 col-sm-2 text-left">
                                            <asp:Label ID="Label116" runat="server" Text="Periodo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-2 text-center">
                                            <asp:TextBox ID="txt_per_ref" runat="server"
                                                CssClass="alingMiddle input-small" MaxLength="100"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-12 col-sm-2 text-center">
                                            <asp:Label ID="Label117" runat="server" Text="Esposa(O), Concubinario, Madre, Padre, Abuelo(A), Hija, Nieto, Sobrino, Cuñado:"
                                                CssClass="alingMiddle textoBold"></asp:Label>
                                        </div>
                                        <div class="col-lg-12 col-sm-12 text-center">
                                            <asp:DropDownList ID="cmb_preg2_ref" OnTextChanged="cmb_preg2_ref_TextChanged" AutoPostBack="true" runat="server">
                                                <asp:ListItem Selected="True" Value="S  ">Si</asp:ListItem>
                                                <asp:ListItem Value="N  ">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-2 col-sm-1 text-left">
                                            <asp:Label ID="Label118" runat="server" Text="Nombre del Familiar:"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nomFam_ref" runat="server"
                                                CssClass="alingMiddle input-medium" MaxLength="100"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2 col-sm-1 text-left">
                                            <asp:Label ID="Label119" runat="server" Text="Parentesco:"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_parentesco_ref" runat="server"
                                                CssClass="alingMiddle input-medium" MaxLength="100"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2 col-sm-1 text-left">
                                            <asp:Label ID="Label120" runat="server" Text="Cargo Desempeñado:"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_cargo_des_ref" runat="server"
                                                CssClass="alingMiddle input-medium" MaxLength="100"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-3 col-sm-1 text-left">
                                            <asp:Label ID="Label121" runat="server" Text="Dependencia:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_depen_ref" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-3 col-sm-1 text-left">
                                            <asp:Label ID="Label123" runat="server" Text="Periodo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_periodo_ref" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                        </div>
                                    </div>
                                </div>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane8" runat="server" CssClass="ancho95" Visible="true">
                            <Header>Beneficiario Seguro</Header>
                            <Content>
                                <div class="row pad1m">

                                    <div class="row pad1m">

                                        <div class="col-lg-2 col-sm-2 text-left">
                                            <asp:Label ID="Label137" runat="server" Text="Nombre:"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-2 text-center">
                                            <asp:TextBox ID="txtNombreBene" runat="server"
                                                CssClass="alingMiddle input-medium" MaxLength="100"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2 col-sm-2 text-left">
                                            <asp:Label ID="Label138" runat="server" Text="Apellido Paterno:"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-2 text-center">
                                            <asp:TextBox ID="txtAPatBene" runat="server"
                                                CssClass="alingMiddle input-medium" MaxLength="100"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2 col-sm-2 text-left">
                                            <asp:Label ID="Label139" runat="server" Text="Apellido Materno:"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-2 text-center">
                                            <asp:TextBox ID="txtAMatBene" runat="server"
                                                CssClass="alingMiddle input-medium" MaxLength="100"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pad1m">

                                        <div class="col-lg-2 col-sm-1 text-left">
                                            <asp:Label ID="Label141" runat="server" Text="Domicilio Completo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-3 text-left">
                                            <asp:TextBox ID="txtDomComBene" runat="server"
                                                CssClass="alingMiddle input-medium" MaxLength="200"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2 col-sm-1 text-left">
                                            <asp:Label ID="Label142" runat="server" Text="Numero Exterior:"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-3 text-left">
                                            <asp:TextBox ID="txtnumExtBene" runat="server"
                                                CssClass="alingMiddle input-medium " MaxLength="100"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2 col-sm-1 text-left">
                                            <asp:Label ID="Label143" runat="server" Text="Numero Interior:"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-3 text-left">
                                            <asp:TextBox ID="txtintExtBene" runat="server"
                                                CssClass="alingMiddle input-medium" MaxLength="100"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pad1m">

                                        <div class="col-lg-3 col-sm-1 text-left">
                                            <asp:Label ID="Label144" runat="server" Text="Colonia o Localidad:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txtColLoBene" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label145" runat="server" Text="Telefono:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txtTelBene" runat="server" CssClass="alingMiddle input-large" MaxLength="16"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" BehaviorID="txt_tel_fijo_refTextBoxWatermarkExtender" TargetControlID="txtTelBene" WatermarkText="Teléfono" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtTelBene" />

                                        </div>

                                    </div>

                                </div>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane5" runat="server" CssClass="ancho95" Visible="true">
                            <Header>Información Referente al Propietario Real</Header>
                            <Content>
                                <div class="row pad1m">
                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label52" runat="server" Text="Apellido Paterno:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_ap_pr" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="A.Paterno"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label53" runat="server" Text="Apellido Materno:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_am_pr" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="A.Materno"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label54" runat="server" Text="Nombre(s):"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nom_pr" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Nombre(s)"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label55" runat="server" Text="Fecha de Nacimiento:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <telerik:RadDatePicker ID="txt_f_nac_pr" runat="server">
                                                <DateInput ID="DateInput1" runat="server" DateFormat="yyyy/MM/dd">
                                                </DateInput>

                                            </telerik:RadDatePicker>

                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label56" runat="server" Text="Entidad de Nacimiento:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_enac_pr" runat="server" MaxLength="50"
                                                PlaceHolder="E.Nacimiento"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label50" runat="server" Text="Nacionalidad:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nac_pr" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Nacionalidad"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label51" runat="server" Text="Sexo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:DropDownList ID="cmb_gen_pr" runat="server">
                                                <asp:ListItem Value="H">Hombre</asp:ListItem>
                                                <asp:ListItem Value="M">Mujer</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label58" runat="server" Text="Estado Civil:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:DropDownList ID="cmb_ec_pr" runat="server">
                                                <asp:ListItem Value="SOL">Soltera(O)</asp:ListItem>
                                                <asp:ListItem Value="CAS">Casada(O)</asp:ListItem>
                                                <asp:ListItem Value="VIU">Viuda(O)</asp:ListItem>
                                                <asp:ListItem Value="DIV">Divorciada(O)</asp:ListItem>
                                                <asp:ListItem Value="U.L">U.Libre</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label59" runat="server" Text="N° Credencial:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_no_cre_pr" runat="server" CssClass="alingMiddle input-large" MaxLength="15"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender16" runat="server" BehaviorID="txt_no_cre_prTextBoxWatermarkExtender" TargetControlID="txt_no_cre_pr" WatermarkText="IFE/INE" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_no_cre_pr" />
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label60" runat="server" Text="CURP:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_curp_pr" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="18"
                                                PlaceHolder="CURP"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label61" runat="server" Text="RFC:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_rfc_pr" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="13"
                                                PlaceHolder="RFC"></asp:TextBox>

                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label62" runat="server" Text="Nivel Escolaridad:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:DropDownList ID="cmb_ne_pr" runat="server">
                                                <asp:ListItem Value="SIN">Sin.Instruccion</asp:ListItem>
                                                <asp:ListItem Value="PRI">Primaria</asp:ListItem>
                                                <asp:ListItem Value="SEC">Secundaria</asp:ListItem>
                                                <asp:ListItem Value="BAC">Bachillerato</asp:ListItem>
                                                <asp:ListItem Value="LIC">Licenciatura</asp:ListItem>
                                                <asp:ListItem Value="POS">Posgrado</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label63" runat="server" Text="Rol del Cliente:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:DropDownList ID="cmb_rol_pr" runat="server">
                                                <asp:ListItem>Jefa(e)</asp:ListItem>
                                                <asp:ListItem>Pareja</asp:ListItem>
                                                <asp:ListItem>Hijo(a)</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label64" runat="server" Text="N° de Hijos:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nhijos_pr" runat="server" CssClass="alingMiddle input-large" MaxLength="2"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender17" runat="server" BehaviorID="txt_nhijos_prTextBoxWatermarkExtender" TargetControlID="txt_nhijos_pr" WatermarkText="N° Hijos" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_nhijos_pr" />

                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label65" runat="server" Text="N°.Dependiente:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_ndep_pr" runat="server" CssClass="alingMiddle input-large" MaxLength="2"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender18" runat="server" BehaviorID="txt_ndep_prTextBoxWatermarkExtender" TargetControlID="txt_ndep_pr" WatermarkText="N° Hijos" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_ndep_pr" />

                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label66" runat="server" Text="Ocupacion:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_ocu_pr" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Ocupacion"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label67" runat="server" Text="Telefono Fijo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_tel_fijo_pr" runat="server" CssClass="alingMiddle input-large" MaxLength="16"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender19" runat="server" BehaviorID="txt_tel_fijo_prTextBoxWatermarkExtender" TargetControlID="txt_tel_fijo_pr" WatermarkText="Teléfono" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_fijo_pr" />

                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label68" runat="server" Text="Telefono Celular:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_tel_cel_pr" runat="server" CssClass="alingMiddle input-large" MaxLength="20"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender20" runat="server" BehaviorID="txt_tel_cel_prTextBoxWatermarkExtender" TargetControlID="txt_tel_cel_pr" WatermarkText="Teléfono Celular" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_cel_pr" />

                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label69" runat="server" Text="Correo Electronico:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_correo_pr" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Correo Electronico"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                        </div>
                                    </div>
                                </div>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane6" runat="server" CssClass="ancho95" Visible="true">
                            <Header>Domicilio Propietario</Header>
                            <Content>
                                <div class="row pad1m">
                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label71" runat="server" Text="Calle:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_calle_pr" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Calle"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label72" runat="server" Text="N° Exterior:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nex_pr" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="N° Exterior"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label73" runat="server" Text="N° Interior:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nin_pr" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="N° interior"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pad1m">

                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label75" runat="server" Text="Código Postal:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_cp_pr" runat="server" OnTextChanged="EstadoCp3" AutoPostBack="true"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="C.P."></asp:TextBox>
                                        </div>



                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label76" runat="server" Text="Delegación o Municipio:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_del_pr" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100" AutoPostBack="true"
                                                PlaceHolder="Delegación o Municipio"></asp:TextBox>
                                        </div>

                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label77" runat="server" Text="Estado:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_estado_pr" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100" AutoPostBack="true"
                                                PlaceHolder="Estado"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label74" runat="server" Text="Colonia o Localidad:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_col_pr" runat="server" Visible="false" ReadOnly="true"
                                                CssClass="alingMiddle input-large" MaxLength="100" AutoPostBack="true"
                                                PlaceHolder=""></asp:TextBox>
                                            <asp:DropDownList ID="cmb_colonia_pr" runat="server" Visible="true" AutoPostBack="true" AppendDataBoundItems="true" DataSourceID="SqlDataSourceCombo3" DataValueField="d_asenta"
                                                DataTextField="d_asenta">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSourceCombo3" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"></asp:SqlDataSource>
                                        </div>

                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label78" runat="server" Text="Tiempo Recidencia:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_tre_pr" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Tiempo Recidencia"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label79" runat="server" Text="N° Habitantes:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nhab_pr" runat="server" CssClass="alingMiddle input-large" MaxLength="2"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender21" runat="server" BehaviorID="txt_nhab_prTextBoxWatermarkExtender" TargetControlID="txt_nhab_pr" WatermarkText="Numero de Habitantes" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_nhab_pr" />

                                        </div>
                                    </div>
                                </div>
                            </Content>
                        </cc1:AccordionPane>
                        <cc1:AccordionPane ID="AccordionPane3" runat="server" CssClass="ancho95" Visible="true">
                            <Header>Información Referente al Proveedor de Recursos</Header>
                            <Content>
                                <div class="row pad1m">
                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label81" runat="server" Text="Apellido Paterno:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_apat_prove" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="A.Paterno"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label82" runat="server" Text="Apellido Materno:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_amat_provee" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="A.Materno"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label83" runat="server" Text="Nombre(s):"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nom_provee" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Nombre(s)"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label84" runat="server" Text="Fecha de Nacimiento:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <telerik:RadDatePicker ID="txt_fnac_provee" runat="server">
                                                <DateInput ID="DateInput2" runat="server" DateFormat="yyyy/MM/dd">
                                                </DateInput>
                                            </telerik:RadDatePicker>

                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label85" runat="server" Text="Entidad de Nacimiento:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_enac_provee" runat="server" MaxLength="50"
                                                PlaceHolder="E.Nacimiento"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label86" runat="server" Text="Nacionalidad:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nac_provee" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Nacionalidad"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label87" runat="server" Text="Sexo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:DropDownList ID="cmb_genero_provee" runat="server">
                                                <asp:ListItem Value="H">Hombre</asp:ListItem>
                                                <asp:ListItem Value="M">Mujer</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label88" runat="server" Text="Estado Civil:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:DropDownList ID="cmb_ec_provee" runat="server">
                                                <asp:ListItem Value="SOL">Soltera(O)</asp:ListItem>
                                                <asp:ListItem Value="CAS">Casada(O)</asp:ListItem>
                                                <asp:ListItem Value="VIU">Viuda(O)</asp:ListItem>
                                                <asp:ListItem Value="DIV">Divorciada(O)</asp:ListItem>
                                                <asp:ListItem Value="U.L">U.Libre</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label89" runat="server" Text="N° Credencial:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_ncre_provee" runat="server" CssClass="alingMiddle input-large" MaxLength="15"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" BehaviorID="txt_ncre_proveeTextBoxWatermarkExtender" TargetControlID="txt_ncre_provee" WatermarkText="IFE/INE" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_ncre_provee" />
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label90" runat="server" Text="CURP:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_curp_provee" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="CURP"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label91" runat="server" Text="RFC:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_rfc_provee" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="RFC"></asp:TextBox>

                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label92" runat="server" Text="Nivel Escolaridad:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:DropDownList ID="cmb_ne_provee" runat="server">
                                                <asp:ListItem Value="SIN">Sin.Instrucción</asp:ListItem>
                                                <asp:ListItem Value="PRI">Primaria</asp:ListItem>
                                                <asp:ListItem Value="SEC">Secundaria</asp:ListItem>
                                                <asp:ListItem Value="BAC">Bachillerato</asp:ListItem>
                                                <asp:ListItem Value="LIC">Licenciatura</asp:ListItem>
                                                <asp:ListItem Value="POS">Posgrado</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label93" runat="server" Text="Rol en el Hogar:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:DropDownList ID="cmb_rol_provee" runat="server">
                                                <asp:ListItem>Jefa(e)</asp:ListItem>
                                                <asp:ListItem>Pareja</asp:ListItem>
                                                <asp:ListItem>Hijo(a)</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label94" runat="server" Text="N° de Hijos:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nohijos_provee" runat="server" CssClass="alingMiddle input-large" MaxLength="2"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender23" runat="server" BehaviorID="txt_nohijos_proveeTextBoxWatermarkExtender" TargetControlID="txt_nohijos_provee" WatermarkText="N° Hijos" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_nohijos_provee" />

                                        </div>


                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label95" runat="server" Text="N°.Dependiente:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nodep_provee" runat="server" CssClass="alingMiddle input-large" MaxLength="2"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender24" runat="server" BehaviorID="txt_nodep_proveeTextBoxWatermarkExtender" TargetControlID="txt_nodep_provee" WatermarkText="N° Dependientes" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_nohijos_provee" />

                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label96" runat="server" Text="Ocupación:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_ocu_prove" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Ocupación"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label97" runat="server" Text="Teléfono Fijo:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_tel_fijo_provee" runat="server" CssClass="alingMiddle input-large" MaxLength="16"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender25" runat="server" BehaviorID="txt_tel_fijo_proveeTextBoxWatermarkExtender" TargetControlID="txt_tel_fijo_provee" WatermarkText="Teléfono" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_fijo_provee" />

                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label99" runat="server" Text="Teléfono Celular:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_tel_cel_prove" runat="server" CssClass="alingMiddle input-large" MaxLength="20"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender26" runat="server" BehaviorID="txt_tel_fijo_proveeTextBoxWatermarkExtender" TargetControlID="txt_tel_cel_prove" WatermarkText="Teléfono Celular" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_tel_cel_prove" />

                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label100" runat="server" Text="Correo Electrónico:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_correo_prove" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Correo Electrónico"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label101" runat="server" Text="Calle:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_calle_provee" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Calle"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label102" runat="server" Text="N° Exterior:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_noext_provee" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="N° Exterior"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label103" runat="server" Text="N° Interior:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_noint_provee" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="N° interior"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label105" runat="server" Text="Código Postal:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_cp_prove" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100" OnTextChanged="EstadoCp4" AutoPostBack="true"
                                                PlaceHolder="C.P."></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label106" runat="server" Text="Delegación o Municipio:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_del_provee" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100" AutoPostBack="true"
                                                PlaceHolder="Delegación o Municipio"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="row pad1m">

                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label107" runat="server" Text="Estado:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_estado_provee" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100" AutoPostBack="true"
                                                PlaceHolder="Estado"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label104" runat="server" Text="Colonia o Localidad:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_col_prove" runat="server" Visible="false" ReadOnly="true"
                                                CssClass="alingMiddle input-large" MaxLength="100" AutoPostBack="true"
                                                PlaceHolder=""></asp:TextBox>
                                            <asp:DropDownList ID="cmb_col_prove" runat="server" Visible="true" AutoPostBack="true" AppendDataBoundItems="true" DataSourceID="SqlDataSourceCombo4" DataValueField="d_asenta"
                                                DataTextField="d_asenta">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSourceCombo4" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"></asp:SqlDataSource>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label108" runat="server" Text="Tiempo Residencia:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_tiempores_provee" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Tiempo Recidencia"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label112" runat="server" Text="N° Habitantes:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txt_nohab_provee" runat="server" CssClass="alingMiddle input-large" MaxLength="2"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender27" runat="server" BehaviorID="txt_nohab_proveeTextBoxWatermarkExtender" TargetControlID="txt_nohab_provee" WatermarkText="N° Habitantes" WatermarkCssClass="water input-large" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_nohab_provee" />

                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label23" runat="server" Text="Denominacion O Razon Social:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txtDEmoRazProvee" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Denominacion O Razon Social"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label24" runat="server" Text="Firma Electronica:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txtFirmaElec" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Firma Electronica"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </Content>
                        </cc1:AccordionPane>

                        <cc1:AccordionPane ID="AccordionPane7" runat="server" CssClass="ancho95" Visible="true">
                            <Header>Informacion Referente al Propietario Real (Persona Moral)</Header>
                            <Content>
                                <div class="row pad1m">
                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label26" runat="server" Text="Razon Social:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txtDenoPM" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Denominacion o Razon Social"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label46" runat="server" Text="Nacionalidad:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txtNacionalidadpm" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Nacionalidad"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label49" runat="server" Text="Objeto Social:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txtObjetoPM" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Objeto Social"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pad1m">
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label70" runat="server" Text="Capital Social:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txtCapitalPM" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Capital Social"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label80" runat="server" Text="Domicilio:"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 col-sm-3 text-left">
                                            <asp:TextBox ID="txtDomicilioPM" runat="server"
                                                CssClass="alingMiddle input-large" MaxLength="100"
                                                PlaceHolder="Domicilio"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label98" runat="server" Text="Numero Exterior:"></asp:Label>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:TextBox ID="txtNexteriorpm" runat="server"
                                                CssClass="alingMiddle input-mini" MaxLength="100"
                                                PlaceHolder="N°"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label127" runat="server" Text="Numero Interior:"></asp:Label>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:TextBox ID="txtninteriorpm" runat="server"
                                                CssClass="alingMiddle input-mini" MaxLength="100"
                                                PlaceHolder="N° "></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pad1m">


                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label132" runat="server" Text="C.P. :"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-2 text-left">

                                            <asp:TextBox ID="txtCpPM" runat="server" OnTextChanged="EstadoCp5" AutoPostBack="true"
                                                CssClass="alingMiddle input-medium" MaxLength="100"
                                                PlaceHolder="Cp"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label134" runat="server" Text="Delegacion o Municipio:"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-2 text-left">
                                            <asp:TextBox ID="txtDelMunPM" runat="server"
                                                CssClass="alingMiddle input-medium" MaxLength="100" AutoPostBack="true"
                                                PlaceHolder="Delegacion o Municipio"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label135" runat="server" Text="Estado:"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 col-sm-2 text-left">
                                            <asp:TextBox ID="txtEstadoPM" runat="server"
                                                CssClass="alingMiddle input-medium" MaxLength="100" AutoPostBack="true"
                                                PlaceHolder="Estado"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1 col-sm-1 text-left">
                                            <asp:Label ID="Label136" runat="server" Text="Colonia:"></asp:Label>
                                        </div>

                                        <div class="col-lg-2 col-sm-2 text-left">
                                            <asp:TextBox ID="txtColPM" runat="server" Visible="false" ReadOnly="true"
                                                CssClass="alingMiddle input-medium" MaxLength="100" AutoPostBack="true"
                                                PlaceHolder=""></asp:TextBox>
                                            <asp:DropDownList ID="ddlPM" runat="server" Visible="true" DataSourceID="SqlDataSourcePM" DataValueField="d_codigo"
                                                DataTextField="d_asenta">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSourcePM" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                    <div class="row pad1m">
                                        <div class="col-lg-12 col-sm-12 text-center">
                                            <asp:Label ID="Label146" runat="server" Text="Accionistas:"></asp:Label>
                                        </div>
                                        <div class="col-lg-12 col-sm-12 text-center">
                                            <asp:TextBox ID="txtaccionista1" runat="server"
                                                CssClass="alingMiddle input-xxlarge" MaxLength="200" AutoPostBack="true"
                                                PlaceHolder="1"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-12 col-sm-12 text-center">
                                            <asp:TextBox ID="txtaccionista2" runat="server"
                                                CssClass="alingMiddle input-xxlarge" MaxLength="200" AutoPostBack="true"
                                                PlaceHolder="2"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                            </Content>
                        </cc1:AccordionPane>

                    </Panes>


                </cc1:Accordion>
                <div class="ancho100 marTop">
                    <asp:Label ID="lblEditaAgrega" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblIdConsultaEdita" runat="server" Visible="false"></asp:Label>
                </div>

                <div class="ancho100 text-center">
                    <asp:LinkButton ID="lnkAgregaSolicitud" runat="server" ValidationGroup="crea" ToolTip="Guarda Solicitud" OnClick="lnkAgregaSolicitud_Click" CssClass="btn btn-success t14"><i class="fa fa-save"></i>&nbsp;<span>Guarda Cédula</span></asp:LinkButton>
                </div>
                <div class="ancho100 text-center">
                    <asp:Label ID="lblErrorAgrega" runat="server" Visible="false" CssClass=""></asp:Label>
                </div>
            </asp:Panel>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <asp:Panel ID="pnlMaskLoad" runat="server" CssClass="maskLoad"></asp:Panel>
                    <asp:Panel ID="pnlCargando" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoad" runat="server" ImageUrl="~/img/loading.gif" CssClass="center" />
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

