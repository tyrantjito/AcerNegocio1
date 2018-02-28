<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PantallaPatio.aspx.cs" Inherits="PantallaPatio" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vista Patio</title>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="css/cloud-admin.css" rel="stylesheet" type="text/css" />
    <link href="css/menus.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/responsive.css" />
    <link href="css/4.4.0/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="css/generales.css" />
    <link href="css/dashboard.css" rel="stylesheet" type="text/css" />
    <!-- FONTS -->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700' rel='stylesheet' type='text/css' />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="60000">
                </asp:Timer>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
            <ContentTemplate>
                <div class="encabezado">
                    <table class="ancho100 colorBlanco alineado alto100">
                        <tr>
                            <td class="marginLeft top0">
                                <asp:Label ID="lbltitulo" runat="server" Text="MoncarWeb" CssClass="margin-left-50 titulo negritas colorMoncarAzul alineado margenLeft" Visible="false" />&nbsp;
                        <asp:Label ID="lblEmpresa" runat="server" CssClass="colorMoncarAzul alineado" Visible="false"></asp:Label>
                                <asp:Image ID="imgEmpresa" runat="server" CssClass="img-responsive imagenLogo" ImageUrl="~/img/moncar.png" />
                                <asp:Label ID="lblFechaActual" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td class="textoBold text-center">
                                <asp:Label ID="lblTallerSesion" runat="server" CssClass="colorMorado"></asp:Label><br />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="clearfix">
                    <h3 class="content-title text-center">
                        <asp:Label ID="lblGopTitulo" runat="server" /></h3>
                </div>                
                <asp:Panel ID="Panel1" runat="server" CssClass="paneles col-lg-12 col-sm-12" ScrollBars="Auto" >
                    <div class="col-lg-12 col-sm-12 text-center">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" GridLines="None"
                            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="no_orden"
                            DataSourceID="SqlDataSource1" EmptyDataText="No existen órdenes registradas" EmptyDataRowStyle-CssClass="errores"
                            OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Orden" SortExpression="no_orden">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOrdenGrid" runat="server" Text='<%# Bind("no_orden") %>' CssClass="textoBold" />
                                        <asp:LinkButton Visible="false" ID="btnOrden" runat="server" Text='<%# Bind("no_orden") %>' CommandArgument='<%# Bind("fase_orden") %>' OnClick="btnOrden_Click" CssClass="btn btn-info textoBold"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="alto40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ingreso" SortExpression="f_recepcion" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaRecp" runat="server" Text='<%# Bind("f_recepcion", "{0:yyyy-MM-dd}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="descripcion" HeaderText="Vehículo" SortExpression="descripcion" />
                                <asp:BoundField DataField="modelo" HeaderText="Modelo" SortExpression="modelo" />
                                <asp:BoundField DataField="color_ext" HeaderText="Color" SortExpression="color_ext" />
                                <asp:BoundField DataField="placas" HeaderText="Placas" SortExpression="placas" />
                                <asp:BoundField DataField="perfil" HeaderText="Perfil" SortExpression="perfil" />
                                <asp:BoundField DataField="localizacion" HeaderText="Localización" SortExpression="localizacion" />
                                <asp:BoundField DataField="razon_social" HeaderText="Cliente" SortExpression="razon_social" />
                                <asp:BoundField DataField="no_siniestro" HeaderText="Siniestro" SortExpression="no_siniestro" />
                                <asp:TemplateField HeaderText="Fase" SortExpression="fase_orden" Visible="false">
                                    <ItemTemplate>
                                        <asp:Image ID="imgFase" runat="server" CssClass="alto40px" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="alto40px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="f_entrega_estimada" HeaderText="Entrega Promesa" SortExpression="f_entrega_estimada" />
                                <asp:BoundField DataField="f_pactada" HeaderText="Entrega Pactada" SortExpression="f_pactada" />
                                <asp:TemplateField Visible ="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpresa" runat="server" Text='<%# Eval("id_empresa") %>' Visible ="false"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible ="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaller" runat="server" Text='<%# Eval("id_taller") %>' Visible ="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>     
                                <asp:TemplateField HeaderText="Operarios">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOperarios" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle CssClass="alert-warning" />
                            <EmptyDataRowStyle CssClass="alert-danger" />
                            <AlternatingRowStyle CssClass="alterTable" />
                            <SelectedRowStyle CssClass="alert-success" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"></asp:SqlDataSource>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
