<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="CheckList.aspx.cs" Inherits="CheckList" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"/>
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server"></telerik:RadSkinManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <br />
               <div class="row">                    
                    <div class="col-lg-2 col-sm-1 text-left"><asp:Label ID="Label57" runat="server" Text="Fecha de Elaboración:" CssClass="textoBold alingMiddle"></asp:Label></div>
                    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:TextBox ID="txtFechaelaboracion" runat="server" CssClass="alingMiddle input-small" MaxLength="15" Enabled="false"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFechaelaboracion_CalendarExtender" runat="server" BehaviorID="txtFechaelaboracion_CalendarExtender" TargetControlID="txtFechaelaboracion" Format="yyyy-MM-dd" PopupButtonID="lnkFSIN"/>                        
                        <cc1:TextBoxWatermarkExtender ID="txtFechaelaboracionWatermarkExtender1" runat="server" BehaviorID="txtelaboracion_TextBoxWatermarkExtender" TargetControlID="txtFechaelaboracion" WatermarkText="aaaa-mm-dd" WatermarkCssClass="water input-small" />                                    
                        <asp:LinkButton ID="lnkFSIN" runat="server" CssClass="btn btn-info t14"><i class="fa fa-calendar"></i></asp:LinkButton>
                    </div>
                </div>
            <br />
           <div class="row">
                <div class="col-lg-12 col-sm-12 text-center alert-info">
                    <h3>
                        <i class="fa fa-credit-card "></i>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label112" runat="server" Text="Datos del Grupo"></asp:Label>                        
                    </h3>                    
                </div>
            </div>
 <div class="row">
                <div class="col-lg-12 col-sm-12 text-center"> 
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="orden" DisplayMode="List" CssClass="errores"/>                   
                        <asp:Label ID="lblErrorRecepcion" runat="server" CssClass="errores alert-danger"></asp:Label>                        
                    </h3>                    
                </div>
            </div>
<!--INICIO -->

  <div class="row pad1m">
     <div class="col-lg-1 col-sm-1 text-left">
                <asp:Label ID="Label1" runat="server" Text="Sucursal:" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-2 col-sm-1 text-left">
         <asp:DropDownList ID="DropDownList1" runat="server">
         <asp:ListItem>Sucursal 1</asp:ListItem>
         <asp:ListItem>Sucursal 2</asp:ListItem>
      
            </asp:DropDownList>
     </div>
     <div class="col-lg-1 col-sm-1 text-left">
              <asp:Label ID="Label3" runat="server" Text="Grupo Productivo:" CssClass="alingMiddle textoBold"></asp:Label>
              </div>
              <div class="col-lg-2 col-sm-2 text-left">
                        <asp:TextBox ID="TextBox2" runat="server" 
                            CssClass="alingMiddle input-medium" MaxLength="100" 
                            PlaceHolder="Grupo Productivo"></asp:TextBox>
                         </div>
    <div class="col-lg-1 col-sm-1 text-left">
              <asp:Label ID="Label110" runat="server" Text="Numero:" CssClass="alingMiddle textoBold"></asp:Label>
              </div>
              <div class="col-lg-2 col-sm-2 text-left">
                        <asp:TextBox ID="TextBox96" runat="server" 
                            CssClass="alingMiddle input-medium" MaxLength="80" 
                            PlaceHolder="Numero"></asp:TextBox>
                         </div>
    <div class="col-lg-1 col-sm-1 text-left">
        <asp:Label ID="Labelciclo" runat="server" Text="Ciclo:" CssClass="alingMiddle textoBold"></asp:Label>
    </div>
    <div class="col-lg-2 col-sm-2 text-left">
        <asp:TextBox ID="TexCiclo" runat="server"
            CssClass="alingMiddle input-medium" MaxLength="100"
            PlaceHolder="Ciclo"></asp:TextBox>
    </div>
</div>

 <div class="row">
    <div class="col-lg-12 col-sm-12 text-center alert-info">
    <h3>
    <i class="fa fa-credit-card "></i>&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label2" runat="server" Text="Expediente Operativo"></asp:Label>                        
    </h3>                    
    </div>
 </div>

    <div class="row pad1m">
      <div class="col-lg-5 col-sm-5 text-left colorNegro"style="background-color:silver">
        <asp:Label ID="Label10" runat="server" Text="Nombre del Documento" CssClass="alingMiddle textoBold"></asp:Label>
    </div>
    
        <div class="col-lg-5 col-sm-5 text-center colorNegro"style="background-color:silver">
        <asp:Label ID="Label29" runat="server" Text="Observaciones" CssClass="alingMiddle textoBold"></asp:Label>
    </div>
        </div>
            <br />

    <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label37" runat="server" Text="1.- Solicitud de crédito para grupos productivos" CssClass="alingMiddle textoBold"></asp:Label>
    </div>
    <div class="col-lg-2 col-sm-3 text-left">
                        <asp:RadioButtonList ID="rdlsolicitud" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            
                        </asp:RadioButtonList>

                    </div>
    
    <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>

            <div class="row pad1m">
   <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label4" runat="server" Text="2.- Control de reuniones de integración" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList21" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>                            
                        </asp:RadioButtonList>
                    </div>
                 <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="textObservaciones" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>

</div>
            <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label5" runat="server" Text="3.- Acta de integración y reglamento interno" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox1" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
            <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label6" runat="server" Text="4. Evaluación del grupo productivo" CssClass="alingMiddle textoBold"></asp:Label></div>
  <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox3" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
            <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label7" runat="server" Text="5.- Ficha de datos del cliente" CssClass="alingMiddle textoBold"></asp:Label></div>
    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                           
                        </asp:RadioButtonList>
                    </div>
                <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox4" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
            <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label8" runat="server" Text="6.- Carta de autorización para solicitar reporte de crédito persona 
        Fisíca/Moral" CssClass="alingMiddle textoBold"></asp:Label></div>
    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox5" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
            <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label9" runat="server" Text="7.- Reporte de visita ocular" CssClass="alingMiddle textoBold"></asp:Label></div>
    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox6" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
            <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label12" runat="server" Text="8.- Copia de identificación oficial vigente" CssClass="alingMiddle textoBold"></asp:Label></div>
   <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList6" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox7" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
            <div class="row pad1m">
   <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label13" runat="server" Text="9.- Copia de comprobante de domicilio vigente" CssClass="alingMiddle textoBold"></asp:Label></div>
<div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList7" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                           
                        </asp:RadioButtonList>
                    </div>
                <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox8" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
            <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label14" runat="server" Text="10.- Copia de CURP o acta de nacimiento" CssClass="alingMiddle textoBold"></asp:Label></div>
   <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList22" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                          
                        </asp:RadioButtonList>
                    </div>
                <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox9" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
            <div class="row pad1m">
   <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label15" runat="server" Text="11.- Estudio de capacidad de pago (C/A)" CssClass="alingMiddle textoBold"></asp:Label></div>
   <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList8" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                           
                        </asp:RadioButtonList>
                    </div>
                <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox10" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender11" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
            <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label16" runat="server" Text="12.- Ficha de pago de garantía liquida" CssClass="alingMiddle textoBold"></asp:Label></div>
    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList9" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox11" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender12" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
            <div class="row pad1m">
   <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label17" runat="server" Text="13.-Ficha de pago de seguro de vida" CssClass="alingMiddle textoBold"></asp:Label></div>
  <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList10" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                           
                        </asp:RadioButtonList>
                    </div>
                   <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox23" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender24" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
            <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label18" runat="server" Text="14.- Control de pagos semanales (Renovación)" CssClass="alingMiddle textoBold"></asp:Label></div>
<div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList23" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                           
                        </asp:RadioButtonList>
                    </div>
                <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox12" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
            <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label19" runat="server" Text="15.- Control de ahorro (Renovación)" CssClass="alingMiddle textoBold"></asp:Label></div>
    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList11" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox13" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender14" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
            <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label20" runat="server" Text="16.- Control de aportaciones solidarias (Renovación)" CssClass="alingMiddle textoBold"></asp:Label></div>
   <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList12" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                           
                        </asp:RadioButtonList>
                    </div>
                <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox14" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender15" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
             <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label21" runat="server" Text="17.- Solicitud de devolución de garantia liquida" CssClass="alingMiddle textoBold"></asp:Label></div>
    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList13" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                 <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox15" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender16" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
             <div class="row pad1m">
   <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label22" runat="server" Text="18.- Autorización para pago con garantía liquida (Renovación)" CssClass="alingMiddle textoBold"></asp:Label></div>
   <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList14" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                 <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox16" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender17" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
             <div class="row pad1m">
   <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label23" runat="server" Text="19.- Bitacora de cobranza (Cuando Aplique)" CssClass="alingMiddle textoBold"></asp:Label></div>
  <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList15" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                           
                        </asp:RadioButtonList>
                    </div>
                 <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox17" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender18" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
             <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label24" runat="server" Text="20.- Recibos de efectivo (Cuando Aplique)" CssClass="alingMiddle textoBold"></asp:Label></div>
<div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList16" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                 <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox18" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender19" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
             <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label25" runat="server" Text="21.-Primer aviso de cobranza (Cuando Aplique)" CssClass="alingMiddle textoBold"></asp:Label></div>
    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList17" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                           
                        </asp:RadioButtonList>
                    </div>
                 <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox19" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender20" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
             <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label26" runat="server" Text="22.- Segundo aviso de cobranza (Cuando Aplique)" CssClass="alingMiddle textoBold"></asp:Label></div>
    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList18" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                          
                        </asp:RadioButtonList>
                    </div>
                      <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox22" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender23" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
             <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label27" runat="server" Text="23.- Tercer aviso de cobranza (Renovación)" CssClass="alingMiddle textoBold"></asp:Label></div>
<div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList19" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                 <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox20" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender21" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
             <div class="row pad1m">
    <div class="col-lg-5 col-sm-8 text-left">
    <asp:Label ID="Label28" runat="server" Text="24.- Orden del día para la entrega del crédito productivo" CssClass="alingMiddle textoBold"></asp:Label></div>
    <div class="col-lg-2 col-sm-2 text-left">
                        <asp:RadioButtonList ID="RadioButtonList20" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="ancho30">
                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                          
                        </asp:RadioButtonList>
                    </div>
                 <div class="col-lg-3 col-sm-3 text-center">
    <asp:TextBox ID="TextBox21" runat="server" MaxLength="250" CssClass="input-large"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" BehaviorID="txtObservaciones_TextBoxWatermarkExtender" TargetControlID="txtObservaciones" WatermarkText="Observaciones" WatermarkCssClass="input-large water" />
    </div>
</div>
           
            <div class="row">
                <div class="col-lg-12 col-sm-12 text-center alert-info">
                    <h5>
                        <asp:Label ID="Label11" runat="server" Text="Firmas de Autorización"></asp:Label>                        
                    </h5>                    
                </div>
            </div>

            <br />
            <br />
         
 <!--</asp:Panel>-->
        </ContentTemplate>
          </asp:UpdatePanel>    
                       </asp:Content>




