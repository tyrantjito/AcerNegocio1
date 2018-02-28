<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="ValidacionCirculoaCredito.aspx.cs" Inherits="ValidacionCirculoaCredito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"/>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <!-- Letrero Acta de Integración y regulamiento interno-->
                <div class="page-header">
                            <!-- /BREADCRUMBS -->
                            <div class="clearfix">  
                                <h3 class="content-title pull-left">
                                    Autorizaci&oacute;n Circulo de Cr&eacute;dito</h3>
                            </div>
                    </div>

            <br />
            <div class="row text-center">
                <div class="col-lg-6 col-sm-6">
                     <asp:DropDownList ID="cmb_nombre_cli" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="cmb_nombre_cli_SelectedIndexChanged"
                          DataSourceID="cmbNombre_CLi" DataValueField="id_cliente"
                           DataTextField="nombre_completo" >
                         <asp:ListItem Value="0">Selecciona Cliente</asp:ListItem>
                       </asp:DropDownList>
                        <asp:SqlDataSource ID="cmbNombre_Cli" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select b.id_cliente, a.nombre_completo from AN_Clientes a inner join AN_Solicitud_Consulta_Buro b on b.id_cliente=a.id_cliente and b.id_empresa=@empresa and b.id_sucursal=@sucursal and b.rp='S' and b.id='S' and b.procesable not in ('FA1')">
                            <%--cualquier cosa!! con nancy "select distinct id_cliente,nombre_completo from an_solicitud_consulta_buro where rp='S' and id='S' and id_empresa=@empresa and id_sucursal=@sucursal and procesable not in ('FA1')"--%>
                            <SelectParameters>
                                <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                                <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                </div>
                 <div class="col-lg-6 col-sm-6">
                     <asp:DropDownList ID="cmb_Consulta" runat="server"  AutoPostBack="true" DataSourceID="SqlDataSourceConsulta" DataValueField="id_consulta"
                           DataTextField="fecha_autorizacion" >
                         <asp:ListItem Value="0">Selecciona una Consulta</asp:ListItem>
                       </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourceConsulta" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" >
                        </asp:SqlDataSource> 
                </div>
            </div>
            <br />
            <div class="row marTop">
                 <div class="col-lg-6 col-sm-6 text-center">
                <asp:LinkButton ID="lnkAbreWindow" runat="server" ToolTip="Valuda Documento" Visible="false" CssClass="btn btn-success t14" OnClick="lnkAbreWindow1_Click"><i class="fa fa-save"></i>&nbsp;<span>Valida Documentos</span></asp:LinkButton>
                 </div>
                <div class="col-lg-6 col-sm-6 text-center">
                     <asp:LinkButton ID="lnkAbreWindow2" runat="server" ToolTip="Recepcion Documento" Visible="false"  CssClass="btn btn-success t14" OnClick="lnkAbreWindow2_Click"><i class="fa fa-save"></i>&nbsp;<span>Valida Documentos Fisicos</span></asp:LinkButton>
                </div>
            </div>
            <div class="row marTop">
                 <div class="col-lg-12 col-sm-12 text-center">
                    <asp:Label ID="lblErrorAfuera" runat="server" CssClass="alert-danger" Visible="false"></asp:Label>
                 </div>
            </div>
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
             <asp:Panel ID="pnlMask" runat="server" CssClass="mask zen1" Visible="false" />
            <asp:Panel ID="windowAutorizacion" CssClass="popUp zen2 textoCentrado ancho80" Height="80%" ScrollBars="Vertical" Visible="false" runat="server">

                   <table class="ancho100">
                       <tr class="ancho100 centrado  ">
                           <td class="ancho95 text-center encabezadoTabla roundTopLeft">
                               <asp:Label ID="lblValida" runat="server" CssClass="t22 colorMorado textoBold" />
                           </td>
                           <td class="ancho5 text-right encabezadoTabla roundTopRight">
                               <asp:LinkButton ID="lnkCerrar" runat="server" CssClass="btn btn-danger alingMiddle" OnClick="lnkCerrar_Click" ToolTip="Cerrar"><i class="fa fa-remove t18"></i></asp:LinkButton>
                           </td>
                       </tr>
                   </table>

                <div class="row marTop text-center">
                    <div class="col-lg-6 col-sm-6 text-center">
                        
                    </div>
                    <div class="col-lg-6 col-sm-6 text-center">
                        <asp:Label ID="lblAdjunto" runat="server"></asp:Label> 
                        <br /><br />
                        <asp:TextBox ID="txtObservaciones" runat="server" Visible="false" PlaceHolder="Motivo" ></asp:TextBox>
                        <br /><br />
                        <asp:LinkButton ID="lnkAutorizaDIG" runat="server" Visible="false"   CssClass="btn btn-success t14" OnClick="lnkAutorizaDIG_Click"><i class="fa fa-check"></i>&nbsp;<span></span></asp:LinkButton>
                        <asp:LinkButton ID="lnkNiegaDIG" runat="server" Visible="false"   CssClass="btn btn-danger t14" OnClick="lnkNiegaDIG_Click"><i class="fa fa-remove"></i>&nbsp;<span></span></asp:LinkButton>
                    </div>
                </div>

                <div class="row marTop text-center">
                    <asp:Label ID="lblErrorDigital" runat="server" CssClass="alert-danger"></asp:Label>
                </div>


                <br /><br /><br />
                   <div class="ancho100 text-center">
                       <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" EnableAJAX="true">
                        <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" AllowFilteringByColumn="true" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged"  runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue"
                                    EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true"  AllowSorting="true" GroupingEnabled="false" PageSize="50" >
                            <MasterTableView  AutoGenerateColumns="false" DataSourceID="SqlDataSource1" DataKeyNames="id_consulta,id_cliente,id_adjunto">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="validacion_digital" HeaderText="Numero de Cliente"  Resizable="true"  />
                                    <telerik:GridBoundColumn DataField="id_cliente" HeaderText="Numero de Cliente"  Resizable="true"  />
                                    <telerik:GridBoundColumn DataField="nombre_completo" HeaderText="Nombre"  Resizable="true" />
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Adjunto"   Resizable="true" />
                                    <telerik:GridBoundColumn DataField="tipo" HeaderText="Tipo"  Resizable="true" />
                                </Columns>
                            </MasterTableView>
                            <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadAjaxPanel>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="">
                         <SelectParameters>
                             <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                             <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />
                         </SelectParameters>
                    </asp:SqlDataSource>
                 </div>
                 <div class="row marTop">
                        <div class="col-lg-6 col-sm-6 text-center">
                            <asp:LinkButton ID="lnkArchivo" runat="server" OnClick="lnkArchivo_Click"
                                 CssClass="btn btn-primary"><i class="fa fa-download"></i><span>&nbsp;Descargar&nbsp;</span></asp:LinkButton>
                        </div>
                    </div> 
               </asp:Panel>
        </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="lnkArchivo" />   
        </Triggers>
    </asp:UpdatePanel>

     
     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
             <asp:Panel ID="pnlMask1" runat="server" CssClass="mask zen1" Visible="false" />
            <asp:Panel ID="windowAutorizacion1" CssClass="popUp zen2 textoCentrado ancho80" Height="80%" ScrollBars="Vertical" Visible="false" runat="server">

                   <table class="ancho100">
                       <tr class="ancho100 centrado  ">
                           <td class="ancho95 text-center encabezadoTabla roundTopLeft">
                               <asp:Label ID="lblRec" runat="server" CssClass="t22 colorMorado textoBold" />
                           </td>
                           <td class="ancho5 text-right encabezadoTabla roundTopRight">
                               <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-danger alingMiddle" OnClick="lnkCerrar_Click1" ToolTip="Cerrar"><i class="fa fa-remove t18"></i></asp:LinkButton>
                           </td>
                       </tr>
                   </table>

                <div class="col-lg-6 col-sm-6 text-center">
                        <asp:Label ID="lblTipo2" runat="server"></asp:Label> 
                        <br /><br />
                        <asp:TextBox ID="txtObservacionesFIS" runat="server" Visible="false" PlaceHolder="Motivo" ></asp:TextBox>
                        <br /><br />
                        <asp:LinkButton ID="lnkAutorizaFis" runat="server" Visible="false"   CssClass="btn btn-success t14" OnClick="lnkAutorizaFIS_Click"><i class="fa fa-check"></i>&nbsp;<span></span></asp:LinkButton>
                        <asp:LinkButton ID="lnkNiegaFis" runat="server" Visible="false"   CssClass="btn btn-danger t14" OnClick="lnkNiegaFIS_Click"><i class="fa fa-remove"></i>&nbsp;<span></span></asp:LinkButton>
                    </div>
              

                <div class="row marTop text-center">
                    <asp:Label ID="lblErrorFisica" runat="server" CssClass="alert-danger"></asp:Label>
                </div>

                <br /><br /><br />
                  <div class="ancho100 text-center">
                      <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" EnableAJAX="true">
                        <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid2" AllowFilteringByColumn="true" OnSelectedIndexChanged="RadGrid2_SelectedIndexChanged" DataSourceID="SqlDataSource2"  runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Office2010Blue"
                                    EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true"  AllowSorting="true" GroupingEnabled="false" PageSize="50" >
                            <MasterTableView  AutoGenerateColumns="false" DataSourceID="SqlDataSource2" DataKeyNames="id_adjunto,id_cliente,id_consulta">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="validacion_fisica" HeaderText="Numero de Cliente"  Resizable="true"  />
                                    <telerik:GridBoundColumn DataField="id_cliente" HeaderText="Numero de Cliente"  Resizable="true"  />
                                    <telerik:GridBoundColumn DataField="nombre_completo" HeaderText="Nombre"  Resizable="true" />
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Adjunto"   Resizable="true" />
                                    <telerik:GridBoundColumn DataField="tipo" HeaderText="Tipo"  Resizable="true" />
                                </Columns>
                            </MasterTableView>
                            <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadAjaxPanel>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="">
                         <SelectParameters>
                             <asp:QueryStringParameter Name="empresa" DefaultValue="0" QueryStringField="e" />
                             <asp:QueryStringParameter Name="sucursal" DefaultValue="0" QueryStringField="t" />
                         </SelectParameters>
                    </asp:SqlDataSource>
                 </div>

               </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

