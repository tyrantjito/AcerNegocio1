<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="Cartera.aspx.cs" Inherits="Cartera" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>

    <div class="page-header">
                            <!-- /BREADCRUMBS -->
                            <div class="clearfix">
                                <h3 class="content-title center">
                                   Cartera</h3> 
                            </div>
                    </div>


    <div class="row marTop text-center">
                    <asp:LinkButton ID="lnkImprimir" runat="server" ToolTip="Imprimir Cartera" OnClick="lnkImprimir_Click" CssClass="btn btn-info">&nbsp;<span><i class="fa fa-file-excel-o" aria-hidden="true"> Descargar Cartera</i></span></asp:LinkButton>                
                </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                            <div class="col-lg-12 col-sm-12 alert-danger negritas center" >
                                    <asp:Label ID="lblErrores" runat="server" Text="" CssClass="errores"></asp:Label>
                                </div>
                                </div>
                        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

