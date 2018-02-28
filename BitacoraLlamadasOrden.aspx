<%@ Page Title="" Language="C#" MasterPageFile="~/AdmonOrden.master" AutoEventWireup="true" CodeFile="BitacoraLlamadasOrden.aspx.cs" Inherits="BitacoraLlamadasOrden" %>
<%@ MasterType VirtualPath="~/AdmonOrden.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script type="text/javascript">
    </script>
    <style type="text/css">
        input, select, .uneditable-input{
            padding:0 !important;
        }
        .timePick {
            width: 100px;
            float: right;
            margin-top: 8px;
            MARGIN-RIGHT: 8px;
            align-self: auto;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-sm-12 text-center alert-info">
                    <h3>
                        <i class="fa fa-book"></i>&nbsp;
                        <i class="fa fa-phone"></i>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label1" runat="server" Text="Bitácora de Llamadas"></asp:Label>
                    </h3>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-sm-12 text-center alert-danger">
                    <asp:Label ID="lblError" runat="server" CssClass="errores alert-danger" />
                </div>
                <div class="col-lg-12 col-sm-12 text-center">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="agrega" CssClass="errores alert-danger" DisplayMode="List" />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-sm-12 text-center">
                    <div class="col-lg-6 col-sm-6 text-right pad1m">
                        <asp:Label ID="Label11" runat="server" Text="Tipo Llamada:" />&nbsp;
                    </div>
                    <div class="col-lg-6 col-sm-6 text-left pad1m">
                        <asp:RadioButtonList ID="rbtnTipoLlamada" AutoPostBack="true" runat="server"
                            OnSelectedIndexChanged="rbtnTipoLlamada_SelectedIndexChanged" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Pendiente&nbsp;" Value="P"  Selected="True" />
                            <asp:ListItem Text="Saliente&nbsp;" Value="S"/>
                            <asp:ListItem Text="Entrante&nbsp;" Value="E" />
                        </asp:RadioButtonList>
                        <asp:Label ID="lblConsecutivo" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlCronos" CssClass="col-lg-12 col-sm-12" ScrollBars="Auto" Style="margin-bottom: 10px;">
                <div class="row">
                    <div class="col-lg-4 col-sm-4 text-left">
                    <asp:Label ID="Label13" runat="server" Text="Llamó:" />&nbsp;
                    <asp:TextBox ID="txtLlamo" runat="server" placeholder="Llamó" MaxLength="50" CssClass="input-medium" />&nbsp;
                    <asp:RequiredFieldValidator Text="*" CssClass="errores" ValidationGroup="agrega" ControlToValidate="txtLlamo" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Necesita colocar el nombre de quien llamó"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                    <asp:Label ID="Label14" runat="server" Text="Contestó:" />&nbsp;
                    <asp:TextBox ID="txtContesto" placeholder="Contestó" runat="server" MaxLength="50" CssClass="input-medium" />&nbsp;
                    <asp:RequiredFieldValidator Text="*" CssClass="errores" ValidationGroup="agrega" ControlToValidate="txtContesto" ID="RequiredFieldValidator8" runat="server" ErrorMessage="Necesita colocar el nombre de quien contestó"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                    <asp:Label ID="Label15" runat="server" Text="Atendió:" />&nbsp;
                    <asp:TextBox ID="txtAtendio" placeholder="Atendió" runat="server" MaxLength="50" CssClass="input-medium" />&nbsp;
                    <asp:RequiredFieldValidator Text="*" CssClass="errores" ValidationGroup="agrega" ControlToValidate="txtAtendio" ID="RequiredFieldValidator9" runat="server" ErrorMessage="Necesita colocar el nombre de quien atendió"></asp:RequiredFieldValidator>
                    <asp:Label ID="Label16" runat="server" Text="Responsable:" />&nbsp;
                    <asp:TextBox ID="txtResponsable" placeholder="Responsable" runat="server" MaxLength="50" CssClass="input-medium" />&nbsp;
                    <asp:RequiredFieldValidator Text="*" CssClass="errores" ValidationGroup="agrega" ControlToValidate="txtResponsable" ID="RequiredFieldValidator10" runat="server" ErrorMessage="Necesita colocar el nombre del responsable"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-4 col-sm-4 text-left">
                    <asp:Label ID="Label17" runat="server" Text="Fecha Llamada:" />&nbsp;
                    <asp:TextBox ID="txtFechaLlamada" runat="server" CssClass="input-small" Enabled="false" />
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="txtFechaLlamada_CalendarExtender" TargetControlID="txtFechaLlamada" Format="yyyy-MM-dd" PopupButtonID="lnkFechaLlamada" />
                    <asp:LinkButton ID="lnkFechaLlamada" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                    <asp:RequiredFieldValidator ControlToValidate="txtFechaLlamada" CssClass="errores" Text="*" ValidationGroup="agrega" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Necesita colocar el comentario del cliente" />&nbsp;&nbsp;
                    <telerik:RadTimePicker RenderMode="Lightweight" ID="timpHoraLlamada" Culture="es-Mx" DateInput-DateFormat="HH:mm:ss" DateInput-EmptyMessage="Hora" Skin="MetroTouch" Width="120px" runat="server"></telerik:RadTimePicker>                        
                    </div>
                    <div class="col-lg-8 col-sm-8 text-left">
                    <asp:Label ID="Label3" runat="server" Text="Comentario del Cliente:" />&nbsp;
                    <asp:TextBox ID="txtComentarioCliente" placeholder="Comentario Cliente" runat="server" CssClass="input-xxlarge" />
                    <asp:RequiredFieldValidator ControlToValidate="txtComentarioCliente" CssClass="errores" Text="*" ValidationGroup="agrega" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Necesita colocar el comentario del cliente" />&nbsp;&nbsp;
                    </div>                            
                </div>

                <div class="row text-center">
                    <asp:Label ID="Label7" runat="server" Text="Fecha Pactada:" Visible="false" />&nbsp;
                    <asp:TextBox ID="txtFechaPactada" runat="server" CssClass="input-small" Enabled="false" Visible="false" />
                    <cc1:CalendarExtender ID="CalendarExtender51" runat="server" BehaviorID="txtFechaPactada_CalendarExtender" TargetControlID="txtFechaPactada" Format="yyyy-MM-dd" PopupButtonID="lnkcalendario" />
                    <asp:LinkButton ID="lnkcalendario" runat="server" CssClass="btn btn-info t14" Visible="false"><i class="fa fa-calendar"></i></asp:LinkButton>                            
                    <asp:TextBox ID="txtHoraPactada" runat="server" CssClass="input-small" placeholder="HH:mm:ss" MaxLength="8" Visible="false" />                            
                    <asp:RegularExpressionValidator ControlToValidate="txtHoraPactada" Text="*" CssClass="errores" ValidationGroup="agrega" ID="RegularExpressionValidator1" runat="server" ErrorMessage="El formato de la hora es incorrecto; HH:mm:ss" ValidationExpression="(([0-1][0-9])|([2][0-3])):([0-5][0-9]):([0-5][0-9])" />
                            
                </div>
                <div class="row">
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:Label ID="Label19" runat="server" Text="Seguimiento a la llamada:" />&nbsp;
                    <asp:DropDownList ID="ddlRespuesta" runat="server" DataSourceID="SqlDataSource2" DataTextField="observaciones" DataValueField="consecutivo" AppendDataBoundItems="true" CssClass="input-medium">
                        <asp:ListItem Text="" Value="0" />
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:Taller %>'
                        SelectCommand="select consecutivo,observaciones from llamadas_orden where no_orden=@no_orden and id_empresa=@id_empresa and id_taller=@id_taller and tipo_llamada='E' order by consecutivo desc">
                        <SelectParameters>
                            <asp:QueryStringParameter QueryStringField="o" Name="no_orden"></asp:QueryStringParameter>
                            <asp:QueryStringParameter QueryStringField="e" Name="id_empresa"></asp:QueryStringParameter>
                            <asp:QueryStringParameter QueryStringField="t" Name="id_taller"></asp:QueryStringParameter>
                        </SelectParameters>
                    </asp:SqlDataSource>
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:CheckBox ID="chkAtendida" runat="server" Text="Atendida, Realizada o Regresada" TextAlign="Right" />
                    </div>
                    <div class="col-lg-4 col-sm-4 text-left">
                        <asp:Label ID="Label9" runat="server" Text="Atendida, Realizada o Regresada por:" />&nbsp;
                        <asp:TextBox ID="txtQuienAtendio" runat="server" CssClass="input-large" MaxLength="200" placeholder="Nombre de quien Atendió, Realizó o Regresó llamada"/>
                    </div>
                </div>
                <div class="row marTop">
                    <div class="col-lg-12 col-sm-12 text-center">
                        <asp:Label ID="lblFechaActual" runat="server" Visible="false"/>
                        <asp:Label ID="lblHoraActual" runat="server" Visible="false"/>
                        <asp:LinkButton ID="lnkGuarda" runat="server" ToolTip="Guarda Llamada" CssClass="btn btn-success t14" ValidationGroup="agrega" OnClick="lnkGuarda_Click"><i class="fa fa-save"></i><span>&nbsp;Guarda Llamada</span></asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>
            
            <asp:Panel ID="PanelLlamadaSaliente" runat="server" CssClass="panelCatalogos text-center" ScrollBars="None" Style="margin-bottom:10px;">
                <div class="row">
                    <div class="col-lg-12 col-sm-12">
                        <div class="table-responsive">
                            <telerik:RadGrid RenderMode="Lightweight" ID="GridSaliente" GridLines="None" runat="server" PageSize="50" CssClass="col-lg-12 col-sm-12" AllowAutomaticUpdates="True" AllowPaging="True" AutoGenerateColumns="False" 
                                PagerStyle-AlwaysVisible="true" AllowSorting="true" DataSourceID="SqlDataSource1" Skin="Metro" AllowAutomaticInserts="false" AllowAutomaticDeletes="false" OnItemDataBound="GridSaliente_ItemDataBound" >
                            <MasterTableView CommandItemDisplay="Bottom" DataKeyNames="consecutivo" HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">                                    
                                <BatchEditingSettings EditType="Row" />
                                <CommandItemStyle CssClass="text-right" />
                                <CommandItemSettings SaveChangesText="Guardar Cambios" ShowAddNewRecordButton="false"  ShowRefreshButton="false" ShowSaveChangesButton="true" CancelChangesText="Cancelar Cambios"/>
                                <Columns>                                        
                                    <telerik:GridBoundColumn DataField="aseguradora" HeaderText="Aseguradora" SortExpression="aseguradora" UniqueName="aseguradora" ReadOnly="true"/>
                                    <telerik:GridBoundColumn DataField="comentario_cliente" HeaderText="Comentario Cliente"  SortExpression="comentario_cliente" UniqueName="comentario_cliente" ReadOnly="true" />  
                                    <telerik:GridBoundColumn DataField="telefonos" HeaderText="Teléfonos"  SortExpression="telefonos" UniqueName="telefonos" ReadOnly="true" />  
                                    <telerik:GridBoundColumn DataField="observaciones" HeaderText="Observaciones"  SortExpression="observaciones" UniqueName="observaciones" ReadOnly="true" /> 
                                    <telerik:GridCheckBoxColumn DataField="atendida" HeaderText="Atendida, Realizada o Regresada" SortExpression="atendida" UniqueName="atendida"/>   
                                    <telerik:GridTemplateColumn HeaderText="Atendida, Realizada o Regresada por" DefaultInsertValue="Beverages" UniqueName="quienatendio" DataField="quienatendio">
                                        <ItemTemplate>
                                            <%# Eval("quienatendio") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadTextBox runat="server" ID="txtquien" Text='<%# Eval("quienatendio") %>' CssClass="input-large" MaxLength="200" EmptyMessage="Atendida, Realizada o Regresada por"></telerik:RadTextBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>                                           
                                    <telerik:GridBoundColumn DataField="fechaatendio" HeaderText="Fecha"  SortExpression="fechaatendio" UniqueName="fechaatendio" ReadOnly="true"  DataFormatString="{0:yyyy-MM-dd}" />  
                                    <telerik:GridBoundColumn DataField="horaatendio" HeaderText="Hora"  SortExpression="horaatendio" UniqueName="horaatendio" ReadOnly="true" />  
                                    <telerik:GridTemplateColumn HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSeleciona" runat="server" CausesValidation="False" CommandArgument='<%# Eval("consecutivo") %>'
                                                CommandName="Select" ToolTip="Seleccionar" CssClass="btn btn-success t14" 
                                                onclick="lnkSeleciona_Click"><i class="fa fa-check"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>                                   
                                </Columns>
                                <NoRecordsTemplate>
                                    <asp:Label ID="lblnoReecMo" runat="server" Text="No llamadas salientes registradas" CssClass="errores"></asp:Label>
                                </NoRecordsTemplate>
                            </MasterTableView>                                
                            <ClientSettings AllowKeyboardNavigation="true">
                                <Selecting AllowRowSelect="true" />                                    
                            </ClientSettings>
                        </telerik:RadGrid>
                               
                            <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:Taller %>'
                                        SelectCommand="select orp.no_orden, llo.consecutivo,llo.estatus,
                                            isnull((select cl.razon_social from cliprov cl where cl.id_cliprov=orp.id_cliprov_aseguradora and cl.tipo='C'),'')as aseguradora
                                            ,m.descripcion+' '+tu.descripcion+' '+upper(v.color_ext) as descripcion,orp.placas,l.descripcion as localizacion,po.descripcion as perfil,C.razon_social,
                                            isnull(llo.comentarios_cliente,'')as comentario_cliente,
                                        rtrim(isnull(case orp.tel_part_propietario  when 'N/A' then '' else orp.tel_part_propietario end,''))+'/'+rtrim(isnull(case orp.tel_cel_propietario  when 'N/A' then '' else orp.tel_cel_propietario end,''))+'/'+rtrim(isnull(case orp.tel_ofi_propietario when 'N/A' then '' else orp.tel_ofi_propietario end ,'')) as telefonos,
                                        isnull(llo.observaciones,'')as observaciones,case so.f_promesa_portal when '1900-01-01' then '' else so.f_promesa_portal end as f_promesa,case so.f_pactada  when '1900-01-01' then '' else so.f_pactada end as f_pactada, case convert(varchar,isnull(so.h_pactada,''),108) when '00:00:00' then '' else convert(varchar,isnull(so.h_pactada,''),108) end as h_pactada,
                                            so.f_recepcion as fecha_Ingreso,orp.fase_orden,
                                            isnull((select count(*) from operativos_orden where id_empresa=orp.id_empresa and id_taller=orp.id_taller and no_orden=orp.no_orden),0) as procesos,
                                        llo.atendida,llo.quienatendio,llo.fechaatendio,llo.horaatendio
                                            from Ordenes_Reparacion orp
                                            left join Seguimiento_Orden so on so.no_orden = orp.no_orden and orp.id_empresa=so.id_empresa and orp.id_taller=so.id_taller
                                            left join Vehiculos v on v.id_marca=orp.id_marca and v.id_tipo_vehiculo=orp.id_tipo_vehiculo and v.id_tipo_unidad=orp.id_tipo_unidad and v.id_vehiculo=orp.id_vehiculo
                                            left join Marcas m on m.id_marca=orp.id_marca
                                            left join Tipo_Vehiculo tv on tv.id_tipo_vehiculo=orp.id_tipo_vehiculo
                                            left join Tipo_Unidad tu on tu.id_marca=orp.id_marca and tu.id_tipo_vehiculo=orp.id_tipo_vehiculo and tu.id_tipo_unidad=orp.id_tipo_unidad
                                            left join Localizaciones l on l.id_localizacion=orp.id_localizacion
                                            left join Cliprov c on c.id_cliprov=orp.id_cliprov and c.tipo=orp.tipo_cliprov and c.tipo='C'
                                            left join PerfilesOrdenes po on po.id_perfilOrden=orp.id_perfilOrden
                                            left join Llamadas_Orden llo on llo.no_orden=orp.no_orden and llo.id_empresa= orp.id_empresa and llo.id_taller=orp.id_taller and llo.tipo_llamada='S'
                                            where orp.no_orden=@no_orden and orp.id_empresa=@id_empresa and orp.id_taller=@id_taller"
                                        UpdateCommand="update llamadas_orden set atendida=@atendida, quienatendio=@quienatendio, fechaatendio=@fechaatendio,horaatendio=@horaatendio where id_empresa=@id_empresa and id_taller=@id_taller and no_orden=@no_orden and consecutivo=@consecutivo if(@consecutivo=1) begin  update seguimiento_orden set f_primer_llamada=@fechaatendio where id_empresa=@id_empresa and id_taller=@id_taller and no_orden=@no_orden end">
                                        <SelectParameters>
                                            <asp:QueryStringParameter QueryStringField="o" Name="no_orden"></asp:QueryStringParameter>
                                            <asp:QueryStringParameter QueryStringField="e" Name="id_empresa"></asp:QueryStringParameter>
                                            <asp:QueryStringParameter QueryStringField="t" Name="id_taller"></asp:QueryStringParameter>
                                        </SelectParameters>
                                        <UpdateParameters>
                                            <asp:QueryStringParameter QueryStringField="o" Name="no_orden"></asp:QueryStringParameter>
                                            <asp:QueryStringParameter QueryStringField="e" Name="id_empresa"></asp:QueryStringParameter>
                                            <asp:QueryStringParameter QueryStringField="t" Name="id_taller"></asp:QueryStringParameter>
                                            <asp:Parameter Name="consecutivo" type="Int32" />
                                            <asp:Parameter Name="atedida" Type="Boolean" />
                                            <asp:Parameter Name="quienatendio" type="String" />                                        
                                            <asp:ControlParameter Name="fechaatendio" type="String" ControlID="lblFechaActual"  />
                                            <asp:ControlParameter Name="horaatendio" type="String" ControlID="lblHoraActual" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="PanelLlamadaEntrante" runat="server" CssClass="panelCatalogos text-center"  ScrollBars="None" Style="margin-bottom:10px;">
                <div class="row">
                    <div class="col-lg-12 col-sm-12">
                        <telerik:RadGrid RenderMode="Lightweight" ID="GridEntrante" GridLines="None" runat="server" PageSize="50" CssClass="col-lg-12 col-sm-12" AllowAutomaticUpdates="True" AllowPaging="True" AutoGenerateColumns="False" 
                                PagerStyle-AlwaysVisible="true" AllowSorting="true" DataSourceID="SqlDataSource3" Skin="Metro" AllowAutomaticInserts="false" AllowAutomaticDeletes="false" >
                            <MasterTableView CommandItemDisplay="Bottom" DataKeyNames="consecutivo" HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="False" DataSourceID="SqlDataSource3">                                    
                                <BatchEditingSettings EditType="Row" />
                                <CommandItemStyle CssClass="text-right" />
                                <CommandItemSettings SaveChangesText="Guardar Cambios" ShowAddNewRecordButton="false"  ShowRefreshButton="false" ShowSaveChangesButton="true" CancelChangesText="Cancelar Cambios"/>
                                <Columns>                                        
                                    <telerik:GridBoundColumn DataField="fecha_llamada" HeaderText="Fecha Llamada" SortExpression="fecha_llamada" UniqueName="fecha_llamada" DataFormatString="{0:yyyy-MM-dd}" ReadOnly="true"/>
                                    <telerik:GridBoundColumn DataField="hora" HeaderText="Hora Llamada"  SortExpression="hora" UniqueName="hora" ReadOnly="true" />  
                                    <telerik:GridBoundColumn DataField="cliente_llamo" HeaderText="Llamó"  SortExpression="cliente_llamo" UniqueName="cliente_llamo" ReadOnly="true" />  
                                    <telerik:GridBoundColumn DataField="contesto" HeaderText="Contestó"  SortExpression="contesto" UniqueName="contesto" ReadOnly="true" /> 
                                    <telerik:GridBoundColumn DataField="atendio" HeaderText="Atendió"  SortExpression="atendio" UniqueName="atendio" ReadOnly="true" /> 
                                    <telerik:GridBoundColumn DataField="responsable" HeaderText="Responsable"  SortExpression="responsable" UniqueName="responsable" ReadOnly="true" /> 
                                    <telerik:GridBoundColumn DataField="comentarios_cliente" HeaderText="Asunto"  SortExpression="comentarios_cliente" UniqueName="comentarios_cliente" ReadOnly="true" /> 
                                    <telerik:GridBoundColumn DataField="observaciones" HeaderText="Respuesta"  SortExpression="observaciones" UniqueName="observaciones" ReadOnly="true" /> 
                                    <telerik:GridBoundColumn DataField="telefonos" HeaderText="Teléfonos"  SortExpression="telefonos" UniqueName="telefonos" ReadOnly="true" /> 
                                    <telerik:GridCheckBoxColumn DataField="atendida" HeaderText="Atendida, Realizada o Regresada" SortExpression="atendida" UniqueName="atendida"/>   
                                    <telerik:GridTemplateColumn HeaderText="Atendida, Realizada o Regresada por" DefaultInsertValue="Beverages" UniqueName="quienatendio" DataField="quienatendio">
                                        <ItemTemplate>
                                            <%# Eval("quienatendio") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadTextBox runat="server" ID="txtquien" Text='<%# Eval("quienatendio") %>' CssClass="input-large" MaxLength="200" EmptyMessage="Atendida, Realizada o Regresada por"></telerik:RadTextBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>                                           
                                    <telerik:GridBoundColumn DataField="fechaatendio" HeaderText="Fecha"  SortExpression="fechaatendio" UniqueName="fechaatendio" ReadOnly="true"  DataFormatString="{0:yyyy-MM-dd}" />  
                                    <telerik:GridBoundColumn DataField="horaatendio" HeaderText="Hora"  SortExpression="horaatendio" UniqueName="horaatendio" ReadOnly="true" />                                                                           
                                </Columns>
                                <NoRecordsTemplate>
                                    <asp:Label ID="lblnoReecMo" runat="server" Text="No llamadas entrantes registradas" CssClass="errores"></asp:Label>
                                </NoRecordsTemplate>
                            </MasterTableView>                                
                            <ClientSettings AllowKeyboardNavigation="true">
                                <Selecting AllowRowSelect="true" />                                    
                            </ClientSettings>
                        </telerik:RadGrid>
                        
                        <asp:SqlDataSource runat="server" ID="SqlDataSource3" ConnectionString='<%$ ConnectionStrings:Taller %>'
                                SelectCommand="select convert(varchar,llo.fecha_llamada,126)as fecha_llamada,convert(varchar,llo.hora,108)as hora,
                                    llo.cliente_llamo,llo.contesto,llo.atendio,llo.responsable,
                                    isnull(llo.comentarios_cliente,'')as comentarios_cliente,
                                rtrim(isnull(case orp.tel_part_propietario  when 'N/A' then '' else orp.tel_part_propietario end,''))+'/'+rtrim(isnull(case orp.tel_cel_propietario  when 'N/A' then '' else orp.tel_cel_propietario end,''))+'/'+rtrim(isnull(case orp.tel_ofi_propietario when 'N/A' then '' else orp.tel_ofi_propietario end ,'')) as telefonos,
                                    isnull(llo.observaciones,'')as observaciones,llo.consecutivo,llo.atendida,llo.quienatendio,llo.fechaatendio,llo.horaatendio
                                    from Ordenes_Reparacion orp
                                    left join Cliprov c on c.id_cliprov=orp.id_cliprov and c.tipo=orp.tipo_cliprov and c.tipo='C'
                                    left join PerfilesOrdenes po on po.id_perfilOrden=orp.id_perfilOrden
                                    inner join Llamadas_Orden llo on llo.no_orden=orp.no_orden and llo.id_empresa= orp.id_empresa and llo.id_taller=orp.id_taller and llo.tipo_llamada=@tipo_llamada
                                    where orp.no_orden=@no_orden and orp.id_empresa=@id_empresa and orp.id_taller=@id_taller order by llo.fecha_llamada desc"
                                UpdateCommand="update llamadas_orden set atendida=@atendida, quienatendio=@quienatendio, fechaatendio=@fechaatendio,horaatendio=@horaatendio where id_empresa=@id_empresa and id_taller=@id_taller and no_orden=@no_orden and consecutivo=@consecutivo">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="rbtnTipoLlamada" PropertyName="SelectedValue" Name="tipo_llamada"></asp:ControlParameter>
                                    <asp:QueryStringParameter QueryStringField="o" Name="no_orden"></asp:QueryStringParameter>
                                    <asp:QueryStringParameter QueryStringField="e" Name="id_empresa"></asp:QueryStringParameter>
                                    <asp:QueryStringParameter QueryStringField="t" Name="id_taller"></asp:QueryStringParameter>
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:QueryStringParameter QueryStringField="o" Name="no_orden"></asp:QueryStringParameter>
                                    <asp:QueryStringParameter QueryStringField="e" Name="id_empresa"></asp:QueryStringParameter>
                                    <asp:QueryStringParameter QueryStringField="t" Name="id_taller"></asp:QueryStringParameter>
                                    <asp:Parameter Name="consecutivo" type="Int32" />
                                    <asp:Parameter Name="atedida" Type="Boolean" />
                                    <asp:Parameter Name="quienatendio" type="String" />                                        
                                    <asp:ControlParameter Name="fechaatendio" type="String" ControlID="lblFechaActual"  />
                                    <asp:ControlParameter Name="horaatendio" type="String" ControlID="lblHoraActual" />
                                    </UpdateParameters>
                            </asp:SqlDataSource>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlPendientes" runat="server" CssClass="panelCatalogos text-center" ScrollBars="Auto" Style="margin-bottom:180px;">
                <telerik:RadGrid RenderMode="Lightweight" ID="GridPendiente" GridLines="None" runat="server" PageSize="50" CssClass="col-lg-12 col-sm-12" AllowAutomaticUpdates="True" AllowPaging="True" AutoGenerateColumns="False" 
                        PagerStyle-AlwaysVisible="true" AllowSorting="true" DataSourceID="SqlDsPendts" Skin="Metro" AllowAutomaticInserts="false" AllowAutomaticDeletes="false" >
                    <MasterTableView CommandItemDisplay="Bottom" DataKeyNames="consecutivo" HorizontalAlign="Center" EditMode="Batch" AutoGenerateColumns="False" DataSourceID="SqlDsPendts">                                    
                        <BatchEditingSettings EditType="Row" />
                        <CommandItemStyle CssClass="text-right" />
                        <CommandItemSettings SaveChangesText="Guardar Cambios" ShowAddNewRecordButton="false"  ShowRefreshButton="false" ShowSaveChangesButton="true" CancelChangesText="Cancelar Cambios" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="tipo_llamada" HeaderText="Tipo" SortExpression="tipo_llamada" UniqueName="tipo_llamada" ReadOnly="true" HeaderStyle-Width="64px"/>
                            <telerik:GridBoundColumn DataField="fecha_llamada" HeaderText="Fecha Llamada" SortExpression="fecha_llamada" UniqueName="fecha_llamada" DataFormatString="{0:yyyy-MM-dd}" ReadOnly="true" HeaderStyle-Width="96px" />
                            <telerik:GridBoundColumn DataField="hora" HeaderText="Hora Llamada"  SortExpression="hora" UniqueName="hora" ReadOnly="true"  HeaderStyle-Width="92px" />  
                            <telerik:GridBoundColumn DataField="cliente_llamo" HeaderText="Llamó"  SortExpression="cliente_llamo" UniqueName="cliente_llamo" ReadOnly="true" />  
                            <telerik:GridBoundColumn DataField="contesto" HeaderText="Contestó"  SortExpression="contesto" UniqueName="contesto" ReadOnly="true" /> 
                            <telerik:GridBoundColumn DataField="atendio" HeaderText="Atendió"  SortExpression="atendio" UniqueName="atendio" ReadOnly="true" /> 
                            <telerik:GridBoundColumn DataField="responsable" HeaderText="Responsable"  SortExpression="responsable" UniqueName="responsable" ReadOnly="true" /> 
                            <telerik:GridBoundColumn DataField="comentarios_cliente" HeaderText="Asunto"  SortExpression="comentarios_cliente" UniqueName="comentarios_cliente" ReadOnly="true" /> 
                            <telerik:GridBoundColumn DataField="observaciones" HeaderText="Respuesta"  SortExpression="observaciones" UniqueName="observaciones" ReadOnly="true" /> 
                            <telerik:GridBoundColumn DataField="telefonos" HeaderText="Teléfonos"  SortExpression="telefonos" UniqueName="telefonos" ReadOnly="true" /> 
                            <telerik:GridCheckBoxColumn DataField="atendida" HeaderText="Atendida, Realizada o Regresada" SortExpression="atendida" UniqueName="atendida" Visible="false"/>   
                        </Columns>
                        <NoRecordsTemplate>
                            <asp:Label ID="lblnoReecMo" runat="server" Text="No llamadas pendientes registradas" CssClass="errores"></asp:Label>
                        </NoRecordsTemplate>
                    </MasterTableView>                                
                    <ClientSettings AllowKeyboardNavigation="true">
                        <Selecting AllowRowSelect="true" />                                    
                    </ClientSettings>
                </telerik:RadGrid>
                <asp:SqlDataSource ID="SqlDsPendts" runat="server" ConnectionString='<%$ ConnectionStrings:Taller %>' 
                    SelectCommand="SELECT llo.consecutivo, llo.fecha_llamada, llo.hora, CASE llo.tipo_llamada WHEN 'E' THEN 'Entrante' ELSE 'Saliente' END AS tipo_llamada, llo.cliente_llamo, llo.contesto, llo.responsable, llo.comentarios_cliente, llo.observaciones, llo.atendio, llo.atendida, 
                    llo.quienatendio, RTRIM(ISNULL(CASE orp.tel_part_propietario WHEN 'N/A' THEN '' ELSE orp.tel_part_propietario END, '')) 
                    + '/' + RTRIM(ISNULL(CASE orp.tel_cel_propietario WHEN 'N/A' THEN '' ELSE orp.tel_cel_propietario END, '')) 
                    + '/' + RTRIM(ISNULL(CASE orp.tel_ofi_propietario WHEN 'N/A' THEN '' ELSE orp.tel_ofi_propietario END, '')) AS telefonos
                    FROM Llamadas_Orden AS llo LEFT JOIN
                    Ordenes_Reparacion AS orp ON llo.no_orden = orp.no_orden AND llo.id_empresa = orp.id_empresa AND llo.id_taller = orp.id_taller
                    WHERE (llo.no_orden = @noOrden) AND (llo.id_empresa = @idEmpresa) AND (llo.id_taller = @idTaller) AND (llo.atendida=0)
                    ORDER BY llo.fecha_llamada, llo.tipo_llamada DESC">
                    <SelectParameters>
                        <asp:QueryStringParameter QueryStringField="o" Name="noOrden"></asp:QueryStringParameter>
                        <asp:QueryStringParameter QueryStringField="e" Name="idEmpresa"></asp:QueryStringParameter>
                        <asp:QueryStringParameter QueryStringField="t" Name="idTaller"></asp:QueryStringParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
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
    <!-- PIE -->
    <div class="pie pad1m">
        <div class="clearfix">
            <div class="row colorBlanco textoBold">
                <div class="col-lg-4 col-sm-4 text-center">
                    <asp:Label ID="Label2" runat="server" Text="Tipo Orden:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="ddlToOrden" runat="server"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-center">
                    <asp:Label ID="Label4" runat="server" Text="Cliente:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="ddlClienteOrden" runat="server"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-center">
                    <asp:Label ID="Label6" runat="server" Text="Tipo Servicio:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="ddlTsOrden" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row colorBlanco textoBold">
                <div class="col-lg-4 col-sm-4 text-center">
                    <asp:Label ID="Label8" runat="server" Text="Tipo Valuación:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="ddlValOrden" runat="server"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-center">
                    <asp:Label ID="Label10" runat="server" Text="Tipo Asegurado:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="ddlTaOrden" runat="server"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-center">
                    <asp:Label ID="Label12" runat="server" Text="Localización:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="ddlLocOrden" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row colorBlanco textoBold">
                <div class="col-lg-4 col-sm-4 text-center">
                    <asp:Label ID="Label108" runat="server" Text="Perfil:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="ddlPerfil" runat="server"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-center">
                    <asp:Label ID="Label109" runat="server" Text="Siniestro:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblSiniestro" runat="server"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-center">
                    <asp:Label ID="Label110" runat="server" Text="Deducible:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblDeducible" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row colorBlanco textoBold">
                <div class="col-lg-4 col-sm-4 text-center">
                    <asp:Label ID="Label111" runat="server" Text="Total Orden:" CssClass="colorEtiqueta" Visible="false"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblTotOrden" Visible="false" runat="server"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-center">
                    <asp:Label ID="Label112" runat="server" Text="Entrega Estimada:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblEntregaEstimada" runat="server"></asp:Label>
                </div>
                <div class="col-lg-4 col-sm-4 text-center">
                    <asp:Label ID="lblPorcDeduEti" runat="server" Text="% Deducible:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblPorcDedu" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
