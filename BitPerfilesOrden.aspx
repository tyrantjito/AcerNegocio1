<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BitPerfilesOrden.aspx.cs" Inherits="BitPerfilesOrden" MasterPageFile="~/AdmonOrden.master" Culture="es-Mx" UICulture="es-Mx" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">  
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"/>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-sm-12 text-center alert-info">
                    <h3>
                        <i class="fa fa-book"></i>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label1" runat="server" Text="Bitácora de Perfiles"></asp:Label>                        
                    </h3>                    
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-sm-12 text-center">                     
                    <asp:Label ID="lblError" runat="server" CssClass="errores" />      
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlCronos" CssClass="paneles col-lg-12 col-sm-12" ScrollBars="Auto">                
                <div class="row">
                    <div class="col-lg-12 col-sm-12">                                            
                        <asp:Panel ID="Panel1" runat="server" CssClass="panelCatalogos text-center" ScrollBars="Auto">
                           <div class="table-responsive">
                               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" AllowPaging="true" AllowSorting="true" PageSize="7" DataSourceID="SqlDataSource1">
                                   <Columns>
                                       <asp:BoundField DataField="descripcion" HeaderText="Perfil" SortExpression="descripcion" />
                                       <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" />
                                       <asp:BoundField DataField="hora" HeaderText="Hora" SortExpression="hora" />
                                       <asp:BoundField DataField="nombre_usuario" HeaderText="Usuario" SortExpression="nombre_usuario" />
                                   </Columns>
                               </asp:GridView>
                               <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" 
                               SelectCommand="select l.descripcion,convert(char(10),b.fecha,126) as fecha,convert(char(8),b.hora,126) as hora,u.nombre_usuario from bitacora_orden_perfiles b inner join usuarios u on u.id_usuario=b.id_usuario inner join perfilesordenes l on l.id_perfilOrden=b.id_perfilOrden where b.id_empresa=@empresa and b.id_taller=@taller and b.no_orden=@orden order by b.id_registro desc">
                                   <SelectParameters>
                                       <asp:QueryStringParameter Name="empresa" QueryStringField="e" />
                                       <asp:QueryStringParameter Name="taller" QueryStringField="t" />
                                       <asp:QueryStringParameter Name="orden" QueryStringField="o" />
                                   </SelectParameters>
                               </asp:SqlDataSource>
                           </div>
                        </asp:Panel>                        
                    </div>                    
                </div>
            </asp:Panel>
            

            <div class="pie pad1m">		                                		                                
		        <div class="clearfix">
			        <div class="row colorBlanco textoBold">
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label2" runat="server" Text="Tipo Orden:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlToOrden" runat="server" ></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label4" runat="server" Text="Cliente:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlClienteOrden" runat="server" ></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label6" runat="server" Text="Tipo Servicio:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlTsOrden" runat="server" ></asp:Label>
                        </div>
                    </div>                                              
                    <div class="row colorBlanco textoBold">
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label8" runat="server" Text="Tipo Valuación:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlValOrden" runat="server" ></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label10" runat="server" Text="Tipo Asegurado:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlTaOrden" runat="server" ></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label12" runat="server" Text="Localización:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlLocOrden" runat="server" ></asp:Label>
                        </div>
                    </div>    
                    <div class="row colorBlanco textoBold">
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label108" runat="server" Text="Perfil:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlPerfil" runat="server" ></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label109" runat="server" Text="Siniestro:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblSiniestro" runat="server" ></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label110" runat="server" Text="Deducible:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblDeducible" runat="server" ></asp:Label>
                        </div>
                    </div>  
                    <div class="row colorBlanco textoBold">
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label111" runat="server" Text="Total Orden:" CssClass="colorEtiqueta" Visible="false"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblTotOrden" runat="server" Visible="false" ></asp:Label>
                        </div>  
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label112" runat="server" Text="Promesa:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblEntregaEstimada" runat="server" ></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="lblPorcDeduEti" runat="server" Text="% Deducible:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblPorcDedu" runat="server" ></asp:Label>
                        </div>                      
                    </div>
		        </div>
            </div>


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

 </asp:Content>