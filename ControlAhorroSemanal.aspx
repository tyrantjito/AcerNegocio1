<%@ Page Title="" Language="C#" MasterPageFile="~/AdmonOrden.master" AutoEventWireup="true" CodeFile="ControlAhorroSemanal.aspx.cs" Inherits="ControlAhorroSemanal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>

          <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
              <ContentTemplate>
                 <div class="page-header">
                <!-- /BREADCRUMBS -->
                <div class="clearfix">
                    <h3 class="content-title pull-left">Control Ahorro Semanal</h3>
                    <asp:Label ID="Label8" runat="server" CssClass="alert-danger"></asp:Label>
                </div>
            </div>
               <asp:Label ID="Label15" runat="server" Visible="false" ></asp:Label>
                  <div class="row marTop text-center">
                      <asp:LinkButton ID="lnkImprimirControlAhorroSemanal" runat="server" Visible="true" ToolTip="Imprimir Control" OnClick="lnkImprimirControlAhorroSemanal_Click" CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Control Ahorro Semanal</span></asp:LinkButton>
                  </div>
                     
                  <br />
                   <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br />


                 

              </ContentTemplate>
          </asp:UpdatePanel>
</asp:Content>

