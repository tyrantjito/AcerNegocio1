<%@ Page Title="" Language="C#" MasterPageFile="~/AdmonOrden.master" AutoEventWireup="true" CodeFile="ControlPagoSemanal.aspx.cs" Inherits="ControlPagoSemanal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"> </asp:ScriptManager>

     <div class="page-header">
                <!-- /BREADCRUMBS -->
                <div class="clearfix">
                    <h3 class="content-title pull-left">Control Pago Semanal</h3>
                    <asp:Label ID="Label1" runat="server" CssClass="alert-danger"></asp:Label>
                </div>
            </div>

          <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
              <ContentTemplate>
               
                   <asp:Label ID="Label15" runat="server" Visible="false" ></asp:Label>
               
                  <div class="row marTop text-center">
                      <asp:LinkButton ID="lnkImprimirControlPagoSemanal" runat="server" Visible="true" ToolTip="Imprimir Control" OnClick="lnkImprimirControlPagoSemanal_Click" CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Control Pago Semanal</span></asp:LinkButton>
                  </div>
                     
                  <br />
                   <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br />


                 

              </ContentTemplate>
          </asp:UpdatePanel>
</asp:Content>

