<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="ConsultaBuro.aspx.cs" Inherits="ConsultaBuro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        

     
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
                    </asp:ScriptManager>

                    <div class="page-header">
                            <!-- /BREADCRUMBS -->
                            <div class="clearfix">
                                <h3 class="content-title pull-left">
                                   Solicitud Círculo Cr&eacute;dito</h3> 
                            </div>
                    </div>



        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>

            <div class="row marTop">
                <asp:Label ID="lblErrorAfuera" runat="server" Visible="false"></asp:Label>
            </div>
            <div class="row text-center marTop">
                <div class="row col-lg-4 col-sm-4 text-center">
                    <asp:LinkButton ID="lnkAbreWindow" runat="server" ToolTip="Guarda Solicitud" CssClass="btn btn-success t14" OnClick="lnkAbreWindow_Click"  ><i class="fa fa-save"></i>&nbsp;<span>Genera Solicitud</span></asp:LinkButton>
                </div>
                 <div class="row col-lg-4 col-sm-4 text-center">
                    <asp:LinkButton ID="lnkValidarCredito" runat="server" ToolTip="Guarda Solicitud" Visible="false" CssClass="btn btn-grey t14"  ><i class="fa fa-save"></i>&nbsp;<span>Consulta Crédito</span></asp:LinkButton>
                </div>
                <div class="row col-lg-4 col-sm-4 text-center">
                    <asp:LinkButton ID="lnkAbreEdicion" runat="server" Visible="false" CssClass="btn btn-warning t14" OnClick="lnEditaSolicitud_Click" ><i class="fa fa-edit"></i><span>&nbsp;Editar Solicitud</span></asp:LinkButton>   
                </div>
            </div>
                
                <div class="ancho100 marTop">
                 <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" EnableAJAX="true">
                            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" AllowFilteringByColumn="true" runat="server" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Metro"
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource2" AllowSorting="true" GroupingEnabled="false" PageSize="50" >                        
                                <MasterTableView DataSourceID="SqlDataSource2" AutoGenerateColumns="false" DataKeyNames="id_consulta,id_cliente">
                                    <Columns>
                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="nombre_completo" FilterControlAltText="Filtro Cliente" HeaderText="Cliente" SortExpression="nombre_completo" UniqueName="nombre_completo"  Resizable="true"/>
                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="rfc_curp" FilterControlAltText="Filtro Identificador" HeaderText="Identificador" SortExpression="rfc_curp" UniqueName="rfc_curp"  Resizable="true"/>
                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="estatus" FilterControlAltText="Filtro Estatus" HeaderText="Estatus" SortExpression="estatus" UniqueName="estatus"  Resizable="true"/>
                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="tipo_persona" FilterControlAltText="Filtro Tipo Persona"  HeaderText="Tipo Persona" SortExpression="tipo_persona" UniqueName="tipo_persona" Resizable="true"/>
                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="fecha_consulta" FilterControlAltText="Filtro Fecha Consulta" HeaderText="Fecha Consulta" SortExpression="fecha_consulta" UniqueName="fecha_consulta" Resizable="true"/>                                    
                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="folio_consulta" FilterControlAltText="Filtro Folio Consulta" HeaderText="Folio Consulta" SortExpression="folio_consulta" UniqueName="folio_consulta" Resizable="true"/>                                    
                                    </Columns>
                                    <NoRecordsTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="No existen consultas registradas" CssClass="text-danger"></asp:Label>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                                <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>                                               
                            </telerik:RadGrid>
                    </telerik:RadAjaxPanel>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="select b.nombre_completo,a.id_consulta,a.id_cliente,a.rfc_curp,a.estatus,a.tipo_persona,convert(char(10),a.fecha_consulta,120) as fecha_consulta,a.folio_consulta from an_solicitud_consulta_buro a inner join 
                        an_clientes b on b.id_cliente=a.id_cliente where a.id_empresa=@empresa and a.id_sucursal=@sucursal
                        " ConnectionString="<%$ ConnectionStrings:Taller %>">
                    <SelectParameters>
                         <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />
                         <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                    </SelectParameters>
                    </asp:SqlDataSource>
                </div>
               <br />
                <div class="row marTop text-center">
                    <asp:LinkButton ID="lnkImprimir" runat="server" Visible="false" ToolTip="Imprimir Solicitud" OnClick="lnkImprimir_Click " CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Solicitud</span></asp:LinkButton>                
                </div>
                 

                <br /><br />
                
                    <%-- Seccion de adjuntos ID y Reporte de Credito --%>
                <asp:Panel ID="pnlSeccionAdjuntos" Visible="false" runat="server" >

                        <div class="row">
                            <div class="col-lg-6 col-sm-6 text-center">
                                <asp:Label ID="lblErrorFotoID" runat="server" CssClass="errores" />
                            </div>
                            <div class="col-lg-6 col-sm-6 text-center">
                                <asp:Label ID="lblErrorFotoRP" runat="server" CssClass="errores" />
                            </div>
                        </div>
          
                <div class="row">
                    <div class="col-lg-3 col-sm-3 text-right ">
                        <telerik:RadAsyncUpload Visible="false" RenderMode="Lightweight" runat="server" ID="rauAdjuntoID" MultipleFileSelection="Automatic" DropZones=".DropZone1" AllowedFileExtensions=".jpeg,.jpg,.png,.gif,.bmp,.tiff,.JPEG,.JPG,.PNG,.GIF,.BMP,.TIFF,.PDF,.pdf,.DOC,.doc,.DOCX,.docx,.xls,.XLS,.xlsx,.XLSX"  />
                        
                     </div>
                    <div class="col-lg-3 col-sm-3 text-left ">
                        <asp:LinkButton ID="lnkAdjuntarID" Visible="false" runat="server" ToolTip="Agregar Foto" OnClick="lnkAdjuntarID_Click"
                            CssClass="alingMiddle btn btn-info t14"><i class="fa fa-plus-circle"></i>&nbsp;<span>Adjuntar ID</span></asp:LinkButton>
                    </div>
                    <div class="col-lg-3 col-sm-3 text-right ">
                        <telerik:RadAsyncUpload Visible="false" RenderMode="Lightweight" runat="server" ID="rauAdjuntoRP" MultipleFileSelection="Automatic" DropZones=".DropZone1" AllowedFileExtensions=".jpeg,.jpg,.png,.gif,.bmp,.tiff,.JPEG,.JPG,.PNG,.GIF,.BMP,.TIFF,.PDF,.pdf,.DOC,.doc,.DOCX,.docx,.xls,.XLS,.xlsx,.XLSX"  />
                   </div>
                    <div class="col-lg-3 col-sm-3 text-left ">
                        <asp:LinkButton ID="lnkAdjuntarRP" Visible="false" runat="server" ToolTip="Agregar Foto" OnClick="lnkAdjuntarRP_Click"
                            CssClass="alingMiddle btn btn-info t14"><i class="fa fa-plus-circle"></i>&nbsp;<span>Adjuntar Reporte Credíto</span></asp:LinkButton>
                    </div>
                    <div class="row col-lg-12 col-sm-12">
                            <asp:Label ID="lblError2" runat="server" CssClass="text-danger"></asp:Label>
                            <asp:Label ID="lblIdLevantamiento2" runat="server" Visible="false" ></asp:Label>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" ValidationGroup="cargar" DisplayMode="List" />
                        </div>
                </div>

                    
                    <div class="ancho100 marTop">
                        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" EnableAJAX="true">
                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid2" AllowFilteringByColumn="true" runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Metro" 
                        EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource1" AllowSorting="true" GroupingEnabled="false" PageSize="50">
                        <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="id_consulta,id_cliente,id_adjunto,tipo">
                            <Columns>
                                 <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="descripcion" FilterControlAltText="Filtro Descripcion" HeaderText="Descripción" SortExpression="descripcion" UniqueName="descripcion"  Resizable="true"/>
                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="tipo" FilterControlAltText="Filtro Tipo" HeaderText="Tipo" SortExpression="tipo" UniqueName="tipo"  Resizable="true"/>
                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="validacion_digital" FilterControlAltText="Filtro Autorizacion Digital" HeaderText="Autorización Dígital" SortExpression="validacion_digital" UniqueName="validacion_digital"  Resizable="true"/>
                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="observaciones_dig" FilterControlAltText="Filtro Observaciones" HeaderText="Observaciones" SortExpression="observaciones_dig" UniqueName="observaciones_dig"  Resizable="true"/>
                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="validacion_fisica" FilterControlAltText="Filtro Autorizacion Fisica" HeaderText="Autorización Física" SortExpression="validacion_fisica" UniqueName="validacion_fisica"  Resizable="true"/>
                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="observaciones_fis" FilterControlAltText="Filtro Observaciones" HeaderText="Observaciones" SortExpression="observaciones_fis" UniqueName="observaciones_fis"  Resizable="true"/>
                                        <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="150px" AutoPostBackOnFilter="true">
                                            <ItemTemplate>
                                                <asp:LinkButton  ID="btnEliminar" runat="server" CssClass="btn btn-danger" CommandArgument='<%# Eval("id_cliente") + ";" + Eval("id_consulta") + ";" + Eval("id_adjunto") +";" + Eval("tipo") %>' OnClick="btnEliminarAdjunto_Click"  OnClientClick="return confirm('¿Está seguro de eliminar el adjunto?')"><i class="fa fa-trash"></i><span>&nbsp;Eliminar</span></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                            </Columns>
                            <NoRecordsTemplate>
                                <asp:Label ID="Label1" runat="server" Text="No existen Archivos adjuntos" CssClass="text-danger"></asp:Label>
                            </NoRecordsTemplate>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                        </ClientSettings>
                        <PagerStyle PageSizeControlType="RadDropDownList" Mode="NextPrevAndNumeric"></PagerStyle>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"  ConnectionString="<%$ ConnectionStrings:Taller %>">
                    <SelectParameters>
                         <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />
                         <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                    </SelectParameters>
                    </asp:SqlDataSource>
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
            <asp:Panel ID="windowAutorizacion" CssClass="popUp zen2 textoCentrado ancho80" Height="80%" ScrollBars="Vertical" Visible="false" runat="server">
                
                
                 
                 <table class="ancho100">
                    <tr class="ancho100%">
                        
                         <div class="page-header" >
                <div class="clearfix">
                     <div class=" col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                     <asp:LinkButton ID="lnkCerrar" runat="server" CssClass="btn btn-danger roundTopRight" OnClick="lnkCerrar_Click" ToolTip="Cerrar"><i class="fa fa-remove t18"></i></asp:LinkButton>            
                 </div>
                    <h3 class="content-title center">Consulta Buro
                    </h3>
                    <asp:Label ID="lblTitulo" visible="false" runat="server"  CssClass="t22 colorBlanco textoBold" />  
                    
                </div>
                
            </div>
                    </tr>
                     
                </table>


                <div class="row">
                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" CssClass="text-danger" ValidationGroup="crea" />  
                </div>

                <div class="row text-center marTop textoBold">
                    <asp:Label ID="Label3" runat="server" Text="Tipo Persona:"></asp:Label> 
                    <br /><br />
                    <asp:DropDownList  ID="cmbAutorizacion" runat="server" OnSelectedIndexChanged="cmbTipo_PersonaIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="SIN">Seleccione Un Tipo</asp:ListItem>
                            <asp:ListItem Value="FIS">Fisica</asp:ListItem>    
                            <asp:ListItem Value="FAE">Fisica  Con Actividad Empresarial</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <br />

               <asp:Panel ID="seccionDatos" Visible="false"  runat="server">

                <div class="row pad1m marTop textoBold">
                    <div class="col-lg-2 col-sm-2  text-left">
                        <asp:Label ID="lblNombre" runat="server"  Text="Nombre:"></asp:Label>
                        <asp:Label ID="lblRazonSocial" runat="server" Visible="false"  Text="Raz&oacute;n Social:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txtNombre" runat="server" Width="70%"  PlaceHolder="Nombre" > </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe indicar el Nombre" Text="*" ValidationGroup="crea" ControlToValidate="txtNombre" CssClass="alineado errores"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtRazonSozial" runat="server" Width="70%" Visible="false" PlaceHolder="Raz&oacute;n Social" > </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Debe indicar el Nombre" Text="*" ValidationGroup="crea" ControlToValidate="txtNombre" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="lblAPat" runat="server" Text="A. Paterno:"></asp:Label> 
                        <asp:Label ID="lblRepLeg" runat="server" Visible="false" Text="Representante legal:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txtApellidoP" runat="server" Width="70%" PlaceHolder="A. Paterno" > </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="" Text="*" ValidationGroup="crea" ControlToValidate="txtApellidoP" CssClass="alineado errores"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtRepresentanteLegal" runat="server" Visible="false" Width="70%" PlaceHolder="Representante Legal" > </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="" Text="*" ValidationGroup="crea" ControlToValidate="txtRepresentanteLegal" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row pad1m marTop textoBold">
                    <div class="col-lg-2 col-sm-2  text-left">
                        <asp:Label ID="lblAMat" runat="server"  Text="A. Materno:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txtApellidoM" runat="server" Width="70%"  PlaceHolder="A. Materno" > </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Debe indicar el Nombre" Text="*" ValidationGroup="crea" ControlToValidate="txtApellidoM" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-2 col-sm-2 text-left">
                         
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        
                    </div>
                </div>

                <div class="row pad1m textoBold">
                    <div class="col-lg-2 col-sm-2  text-left">
                        <asp:Label ID="Label4" runat="server"  Text="RFC / CURP:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txtRFCCURP" runat="server" MaxLength="18" Width="70%"  PlaceHolder="RFC / CURP " > </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe indicar el RFC / CURP" Text="*" ValidationGroup="crea" ControlToValidate="txtRFCCURP" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row pad1m textoBold">
                    <div class="col-lg-2 col-sm-2  text-left">
                        <asp:Label ID="Label5" runat="server"  Text="Calle y Número:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txtCalle" runat="server" Width="70%" MaxLength="50"  PlaceHolder="Calle" > </asp:TextBox>
                        <br />
                        <asp:TextBox ID="txtNumero" runat="server" Width="70%" MaxLength="50"  PlaceHolder="Número" > </asp:TextBox>
                    </div>
                    <div class="col-lg-2 col-sm-2  text-left">
                        <asp:Label ID="Label6" runat="server"  Text="C.P:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                         <asp:TextBox ID="txt_cp_cb" runat="server" MaxLength="50"  PlaceHolder="C.P. " Width="70%" OnTextChanged="EstadoCp" AutoPostBack="true" > </asp:TextBox>
                        
                    </div>
                    <div class="col-lg-2 col-sm-2 marTop  text-left">
                        <asp:Label ID="Label7" runat="server"  Text="Municipio:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 marTop text-left">
                        <asp:TextBox ID="txtMunicipio" runat="server" Width="70%" MaxLength="50" AutoPostBack="true" PlaceHolder="Municipio " > </asp:TextBox>
                    </div>
                    <div class="col-lg-2 col-sm-2 marTop  text-left">
                        <asp:Label ID="Label8" runat="server"   Text="Estado:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 marTop text-left">
                        <asp:TextBox ID="txtEstado" runat="server" MaxLength="50"  PlaceHolder="Estado " AutoPostBack="true" Width="70%"> </asp:TextBox>
                    </div>
                     <div class="col-lg-2 col-sm-2 marTop  text-left">
                        <asp:Label ID="Label9" runat="server"  Text="Colonia:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 marTop text-left">
                        <asp:DropDownList ID="cmbColonia" runat="server"    DataSourceID="SqlDataSourceCombo"  DataValueField="d_codigo"
                           DataTextField="d_asenta" >
                       </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourceCombo" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" >
                        </asp:SqlDataSource>
                    </div>
                    <div class="col-lg-2 col-sm-2 marTop  text-left">
                        <asp:Label ID="Label10" runat="server"  Text="Teléfono:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 marTop text-left">
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="alingMiddle input-large" MaxLength="15"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="txtMontoWatermarkExtender1" runat="server" BehaviorID="txtTelefonoBoxWatermarkExtender" TargetControlID="txtTelefono" WatermarkText="Número de Teléfono" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtTelefono" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Debe indicar el teléfono"  Text="*" ValidationGroup="crea" ControlToValidate="txtTelefono" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row pad1m textoBold">
                    <div class="col-lg-2 col-sm-2  text-left">
                        <asp:Label ID="Label11" runat="server"  Text="Lugar Autorizacion:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="txtLugar" runat="server" MaxLength="50" Width="70%" PlaceHolder="Lugar de Firma Autorización" > </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Debe indicar el RFC / CURP" Text="*" ValidationGroup="crea" ControlToValidate="txtLugar" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-2 col-sm-2  text-left">
                        <asp:Label ID="Label12" runat="server"  Text="Fecha:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <telerik:RadDatePicker ID="txtFechaFirma" Height="30px" runat="server" Width="70%" >
                            <DateInput ID="DateInput1" runat="server" DateFormat="yyyy/MM/dd">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6"  runat="server" ErrorMessage="Debe indicar el RFC / CURP" Text="*" ValidationGroup="crea" ControlToValidate="txtFechaFirma" CssClass="alineado errores"></asp:RequiredFieldValidator>
                    </div>
                    
                </div>

                <div class="row pad1m textoBold">
                    <div class="col-lg-2 col-sm-2  text-left">
                        <asp:Label ID="Label14" runat="server"  Text="Nombre Funcionario:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4  text-left">
                        <asp:TextBox ID="txtFuncionario" ReadOnly="true" runat="server" Width="70%"  PlaceHolder="Funcionario" > </asp:TextBox>
                    </div>
                </div>

                <div class="row pad1m textoBold">
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label15" runat="server"  Text="Fecha Consulta CC:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm- text-left">
                        <telerik:RadDatePicker ID="txtFechaConsulta" Width="70%" runat="server" >
                            <DateInput ID="DateInput2" runat="server" DateFormat="yyyy/MM/dd"  ReadOnly="false">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </div>
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label17" runat="server"  Text="Folio Consulta CC:"></asp:Label>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                          <asp:TextBox ID="txtFolioConsulta" runat="server" CssClass="alingMiddle input-large" MaxLength="15"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" BehaviorID="txtFolioConsultaBoxWatermarkExtender" TargetControlID="txtFolioConsulta" WatermarkText="Folio Consulta" WatermarkCssClass="water input-large" />
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtFolioConsulta" />
                    </div>
                </div>
                <div class="row pad1m textoBold text-center">
                    <asp:Label ID="Label50" runat="server" Text="Estatus:"></asp:Label>
                    <asp:Label ID="lblEstatus" runat="server" Text="Pendiente"></asp:Label>
                </div>
                <asp:Panel ID="pnlEstatus" Visible="false"  runat="server">
                    <div class="row pad1m textoBold">
                        <div class="col-lg6 col-sm-6 text-center">
                            <td class="ancho100 text-center">
                                <asp:LinkButton ID="btnAcepta" runat="server" ToolTip="Aceptar" OnClick="btnAcepta_Click" CssClass="btn btn-success alingMiddle" ><i class="fa fa-check-circle t18"></i></asp:LinkButton>
                            </td>
                        </div>
                        <div class="col-lg6 col-sm-6 text-center">
                            <asp:LinkButton ID="btnDeclina" runat="server" ToolTip="Aceptar" OnClick="btnDeclina_Click" CssClass="btn btn-danger alingMiddle" ><i class="fa fa-times t18"></i></asp:LinkButton>
                        </div>
                    </div>
               </asp:Panel>

               <div class="row marTop">
                    <asp:Label ID="lblEditaAgrega" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblIdConsultaEdita" runat="server" Visible="false" ></asp:Label>
                    <asp:Label ID="lblTipoPersonaEdita" runat="server" Visible="false" ></asp:Label>
                </div>  

                <div class="row text-center">
                    <asp:LinkButton ID="lnkAgregaSolicitud" runat="server" ValidationGroup="crea" ToolTip="Guarda Solicitud" OnClick="lnkAgregaSolicitud_Click" CssClass="btn btn-success t14"  ><i class="fa fa-save"></i>&nbsp;<span>Guarda Solicitud</span></asp:LinkButton>
                </div>
                <div class="row marTop text-center">
                    <asp:Label ID="lblErrorAgrega" runat="server" Visible="true" CssClass="alert-danger"></asp:Label>
                </div>
                </asp:Panel>
                
                
                </asp:Panel>
                            </ContentTemplate>                    
                        </asp:UpdatePanel> 
                    


</asp:Content>

