<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="PagosPactados.aspx.cs" Inherits="PagosPactados" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>
    
                    <div class="page-header">
                            <!-- /BREADCRUMBS -->
                            <div class="clearfix">
                                <h3 class="content-title pull-left">
                                   Pagos Pactados</h3> 
                            </div>
                    </div>




    <div class="row marTop text-center">
                    <asp:LinkButton ID="lnkImprimir" runat="server" ToolTip="Imprimir Cartera" OnClick="lnkImprimir_Click" CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Pagos Pactados</span></asp:LinkButton>                
                </div>
            
</asp:Content>

