<%@ Page Title="" Language="C#" MasterPageFile="~/AdmOrdenes.master" AutoEventWireup="true" CodeFile="EvaluacionGrupal.aspx.cs" Inherits="EvaluacionGrupal" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" />
     <telerik:RadSkinManager ID="RadSkinManager1" runat="server"></telerik:RadSkinManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          
              <div class="page-header">
                            <!-- /BREADCRUMBS -->
                            <div class="clearfix">
                                <h3 class="content-title pull-left"> 
                                   Evaluación Grupal</h3>
                            </div>
                    </div>
             <br />
              <div class="row text-center marTop">
                
                   <div class="row col-lg-6 col-sm-6 text-center">
                  <asp:LinkButton ID="lnkAbreWindow" runat="server" ToolTip="Guarda Solicitud" CssClass="btn btn-success t14" OnClick="lnkAbreWindow_Click"><i class="fa fa-save"></i>&nbsp;<span>Genera Evaluación</span></asp:LinkButton>
                       </div>
                  <div class="row col-lg-6 col-sm-6 text-center">
                  <asp:LinkButton ID="lnkAbreEdicion" runat="server" Visible="false" CssClass="btn btn-warning t14" OnClick="lnkAbreEdicion_Click"><i class="fa fa-save"></i>&nbsp;<span>Editar Evaluación</span></asp:LinkButton>
                    </div>
              </div>
               <br /> 
              <div class="row">
                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" EnableAJAX="true">
                    <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" AllowFilteringByColumn="true"  runat="server" EnableHeaderContextMenu="true" Culture="es-Mx" Skin="Metro" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged"
                                EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" DataSourceID="SqlDataSource1" AllowSorting="true" GroupingEnabled="false" PageSize="50" >
                        <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="false" DataKeyNames="id_grupo,id_evalgpo">
                            <Columns>
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="id_grupo" FilterControlAltText="Filtro ID Grupo" HeaderText="Numero Grupo" SortExpression="id_grupo" UniqueName="id_grupo" Resizable="true"  />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="grupo_productivo_eval" FilterControlAltText="Filtro Grupo" HeaderText="Grupo" SortExpression="grupo_productivo_eval" UniqueName="grupo_productivo_eval" Resizable="true" />
                                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="fecha_eval" FilterControlAltText="Filtro Fecha" HeaderText="Fecha Evaluacion" SortExpression="fecha_eval" UniqueName="fecha_eval" Resizable="true" />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="select id_grupo,id_evalgpo,fecha_eval,grupo_productivo_eval from AN_Evaluacion_grupal where id_sucursal=@sucursal and id_empresa=@empresa" ConnectionString="<%$ ConnectionStrings:Taller %>">
                     <SelectParameters>
                                        <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0"/>
                                        <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0"/>
                                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
            <div class="row marTop text-center">
            <asp:LinkButton ID="lnkImprimir" runat="server" Visible="false" ToolTip="Imprimir Solicitud" OnClick="lnkImprimir_Click " CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Evaluacion</span></asp:LinkButton>                
                </div>
            
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                     <asp:Panel ID="pnlMask" runat="server" CssClass="mask zen1"  Visible="false" />
                     <asp:Panel ID="windowAutorizacion" CssClass="popUp zen2 textoCentrado ancho90" Height="90%" ScrollBars="Vertical"  Visible="false" runat="server">

                           <table class="ancho100">
                    <tr class="ancho100%">
                        
                         <div class="page-header" >
                <div class="clearfix">
                     <div class=" col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                     <asp:LinkButton ID="lnkCerrar" runat="server" CssClass="btn btn-danger alingMiddle" OnClick="lnkCerrar_Click" ToolTip="Cerrar"><i class="fa fa-remove t18"></i></asp:LinkButton>
                 </div>
                    <h3 class="content-title center">Analisis Capacidad de Pago
                    </h3>
                    <asp:Label ID="lbleval" runat="server" Visible="false" CssClass="t22 colorMorado textoBold" />
                    
                </div>
                
            </div>
                    </tr>
                     
                </table>

                       
                          <div>
<div class="row">
                <div class="col-lg-12 col-sm-12 text-center alert-info">
                    <h3>
                        <asp:Label ID="Label1" runat="server" Text="Evaluación Grupal"></asp:Label>                        
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
                           

<div class="row">
    
     <div class="col-lg-6 col-sm-6 text-right textoBold">
              <asp:Label ID="Label57" runat="server" Text="Fecha:"></asp:Label>
              </div>
    <div class="col-lg-6 col-sm-6 text-left">
        <telerik:RadDatePicker ID="txtfecha_eval" runat="server">
            <DateInput runat="server" DateFormat="yyyy/MM/dd">
            </DateInput>
        </telerik:RadDatePicker>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator6"  runat="server" ErrorMessage="Debe indicar el RFC / CURP" Text="*" ValidationGroup="crea" ControlToValidate="txtfecha_eval" CssClass="alineado errores"></asp:RequiredFieldValidator>
    </div>
    </div>
<div class="row pad1m">
    <div class="col-lg-1 col-sm-1 text-left">
              <asp:Label ID="Label43" runat="server"  Text="Sucursal:" CssClass="alingMiddle textoBold"></asp:Label>
              </div>
              <div class="col-lg-3 col-sm-2 text-left">
                        <asp:TextBox ID="txt_suc" runat="server"
            CssClass="alingMiddle input-medium" MaxLength="100"
            PlaceHolder=""></asp:TextBox>
                         </div>
     <div class="col-lg-1 col-sm-1 text-left">
              <asp:Label ID="Label44" runat="server" Text="Grupo Productivo:" CssClass="alingMiddle textoBold"></asp:Label>
              </div>
              <div class="col-lg-3 col-sm-2 text-left">
                       <telerik:RadComboBox ID="cmb_sucursal" runat="server" DataSourceID="SqlDataSourceSucrsal" OnSelectedIndexChanged="cmb_sucursal_SelectedIndexChanged1" AutoPostBack="true" 
                            DataValueField="id_grupo" DataTextField="grupo_productivo">
                       </telerik:RadComboBox>
                 <asp:SqlDataSource ID="SqlDataSourceSucrsal" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select 0 as id_grupo,'Selecione Grupo'as grupo_productivo union all Select id_grupo,grupo_productivo from an_grupos">
                 </asp:SqlDataSource>
                         </div>
    <div class="col-lg-1 col-sm-1 text-left">
              <asp:Label ID="Label45" runat="server" Text="Número:" CssClass="alingMiddle textoBold"></asp:Label>
              </div>
              <div class="col-lg-3 col-sm-2 text-left">
                         </div>
   <asp:TextBox ID="txt_num" runat="server"
            CssClass="alingMiddle input-medium" MaxLength="100"
            PlaceHolder=""></asp:TextBox>
</div>
<div class="row pad1m">
     <div class="col-lg-1 col-sm-1 text-left">
        <asp:Label ID="Labelciclo" runat="server" Text="Ciclo:" CssClass="alingMiddle textoBold"></asp:Label>
    </div>
    <div class="col-lg-3 col-sm-2 text-left">
        <asp:TextBox ID="txt_ciclo" runat="server"
            CssClass="alingMiddle input-medium" MaxLength="100" AutoPostBack="true"
            PlaceHolder="Ciclo"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ErrorMessage="Debe indicar el RFC / CURP" Text="*" ValidationGroup="crea" ControlToValidate="txt_ciclo" CssClass="alineado errores"></asp:RequiredFieldValidator>
    </div>

     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label29" runat="server" Text="Gerente:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-2 col-sm-2 text-left">
                    
                     <asp:DropDownList ID="ddlgerente" runat="server" AutoPostBack="true" DataSourceID="cmbgerente" DataValueField="id_usuario"
                           DataTextField="nombre_usuario" >
                       </asp:DropDownList>
                        </div>
                        <asp:SqlDataSource ID="cmbgerente" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select 0 as id_usuario,'Selecione Gerente'as nombre_usuario union all select id_usuario,nombre_usuario from usuarios where n_puesto='GRT'">
                          
                        </asp:SqlDataSource>
     <div class="col-lg-2 col-sm-2 text-left">
                        <asp:Label ID="Label40" runat="server" Text="Asesor:" CssClass="alingMiddle textoBold"></asp:Label>
                    </div>
                    <div class="col-lg-2 col-sm-2 text-left">
                    
                     <asp:DropDownList ID="ddlAsesor" runat="server" AutoPostBack="true" DataSourceID="cmbasesor" DataValueField="id_usuario"
                           DataTextField="nombre_usuario" >
                       </asp:DropDownList>
                        </div>
                        <asp:SqlDataSource ID="cmbasesor" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>" SelectCommand="select 0 as id_usuario,'Selecione Asesor'as nombre_usuario union all select id_usuario,nombre_usuario from usuarios where n_puesto='ASE'">
                          
                        </asp:SqlDataSource>
</div>

<div class="row">
                <div class="col-lg-12 col-sm-12 text-center alert-info">
                    <h5>
                        <asp:Label ID="Label3" runat="server" Text="La evaluación consiste en asignar valores para cada uno de los criterios considerando la siguiente escala:"></asp:Label>                        
                    </h5>                    
                </div>
            </div>
<div class="row">
                <div class="col-lg-12 col-sm-12 text-center alert-info">
                    <h5>
                        <asp:Label ID="Label4" runat="server" Text="Deficiente=0 Regular=2 Bueno=3 Excelente=5"></asp:Label>                        
                    </h5>                    
                </div>
            </div>

<div class="row pad1m">
    <div class="col-lg-8 col-sm-8 text-center colorNegro"style="background-color:silver">
        <asp:Label ID="Label10" runat="server" Text="CRITERIOS A CALIFICAR PARA GRUPOS PRODUCTIVOS NUEVOS" CssClass="alingMiddle textoBold"></asp:Label>
    </div>
     <div class="col-lg-4 col-sm-4 text-left colorNegro"style="background-color:silver">
        <asp:Label ID="Label11" runat="server" Text="CALIFICACIÓN" CssClass="alingMiddle textoBold"></asp:Label>
    </div>
    <br />
     <br />
    </div>


<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label37" runat="server" Text="1.ASISTENCIA DE LOS INTEGRANTES A LAS REUNIONES DE INTEGRACIÓN GRUPAL:" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
         <asp:TextBox ID="txt1_gn" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" 
                            PlaceHolder=""></asp:TextBox>
            
     </div>



</div>

<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label5" runat="server" Text="2.PUNTUALIDAD DE LOS INTEGRANTES A LAS REUNIONES DE INTEGRACIÓN GRUPAL:" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
         <asp:TextBox ID="txt2_gn" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" 
                            PlaceHolder=""></asp:TextBox>
     </div>
    </div>

<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label6" runat="server" Text="3.COMPLIMIENTO REQUISITOS NORMATIVOS DE LOS INTEGRANTES (SOLVENCIA MORAL,NEGOCIO PROPIO, ARRAIGO EN EL DOMICILIO, CERCANIA GEOGRÁFICA):" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
         <asp:TextBox ID="txt3_gn" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" 
                            PlaceHolder=""></asp:TextBox>
     </div>
    </div>

<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label7" runat="server" Text="4.DESEMPEÑO DEL COMITE DE ADMINISTRACIÓN (CONOCIMIENTO Y EJECUCIÓN DE FUNCIONES):" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
        <asp:TextBox ID="txt4_gn" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" 
                            PlaceHolder=""></asp:TextBox>
     </div>
    </div>

<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label8" runat="server" Text="5.APORTACIÓN DE LA GARANTÍA LÍQUIDA EN TIEMPO Y FORMA:" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
        <asp:TextBox ID="txt5_gn" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100"  OnTextChanged="txt5_gn_TextChanged" AutoPostBack="true"
                            PlaceHolder=""></asp:TextBox>
     </div>
    </div>

                              <div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label9" runat="server" Text="TOTAL:" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
        <asp:TextBox ID="txt_total" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" AutoPostBack="true" ReadOnly="true"
                            PlaceHolder=""></asp:TextBox>
     </div>
    </div>


            <br />
<div class="row pad1m">
    <div class="col-lg-8 col-sm-8 text-right">
        <asp:Label ID="Label12" runat="server" Text="CALIFICACIÓN OPTIMA" CssClass="alingMiddle textoBold"></asp:Label>
    </div>
     <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="TextBox3" runat="server" Text="25 Puntos" ReadOnly="true"
                            CssClass="alingMiddle input-small" MaxLength="100"
                             ></asp:TextBox>
                         </div>
</div>

<div class="row pad1m">
    <div class="col-lg-8 col-sm-8 text-right">
        <asp:Label ID="Label13" runat="server" Text="CALIFICACIÓN MÍNIMA ACEPTABLE PARA AUTORIZACIÓN DE CRÉDITO PRODUCTIVO" CssClass="alingMiddle textoBold"></asp:Label>
    </div>
     <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="TextBox4" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" Text="20 Puntos" ReadOnly="true"
                             ></asp:TextBox>
                         </div>
</div>
            <br />
            <br />
<div class="row pad1m">
    <div class="col-lg-8 col-sm-8 text-center colorNegro"style="background-color:silver">
        <asp:Label ID="Label14" runat="server" Text="CRITERIOS A CALIFICAR PARA GRUPOS PRODUCTIVOS DE RENOVACIÓN" CssClass="alingMiddle textoBold"></asp:Label>
    </div>
     <div class="col-lg-4 col-sm-4 text-left colorNegro"style="background-color:silver">
        <asp:Label ID="Label15" runat="server" Text="CALIFICACIÓN" CssClass="alingMiddle textoBold"></asp:Label>
    </div>
    <br />
     <br />
    </div>

<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label16" runat="server" Text="1.ASISTENCIA DE LOS INTEGRANTES A LAS REUNIONES SEMANALES:" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
         <asp:TextBox ID="txt1_ga" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" 
                            PlaceHolder=""></asp:TextBox>
     </div>
</div>

<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label17" runat="server" Text="2.PUNTUALIDAD DE LOS INTEGRANTES A LAS REUNIONES SEMANALES:" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
          <asp:TextBox ID="txt2_ga" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" 
                            PlaceHolder=""></asp:TextBox>
     </div>



</div>

<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label18" runat="server" Text="3.DESEMPEÑO DEL COMITE DE ADMINISTRACIÓN (CONOCIMIENTO Y CUMPLIMIENTO DE FUNCIONES):" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
         <asp:TextBox ID="txt3_ga" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" 
                            PlaceHolder=""></asp:TextBox>
     </div>



</div>

<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label19" runat="server" Text="4.APORTACION DE LA GARANTÍA LÍQUIDA EN TIEMPO Y FORMA:" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
         <asp:TextBox ID="txt4_ga" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" 
                            PlaceHolder=""></asp:TextBox>
     </div>



</div>

<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label21" runat="server" Text="5.APLICACIÓN DE METODOLOGÍA INSTITUCIONAL (CONOCIMIENTO Y MANEJO DE CONTROLES DE PAGO):" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
         <asp:TextBox ID="txt5_ga" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" 
                            PlaceHolder=""></asp:TextBox>
     </div>



</div>

<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label20" runat="server" Text="6.CUMPLIMIENTO DE LA OBLIGACIÓN SOLIDARIA  DE LOS INTEGRANTES DEL GRUPO PRODUCTIVO :" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
          <asp:TextBox ID="txt6_ga" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" 
                            PlaceHolder=""></asp:TextBox>
     </div>



</div>

<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label22" runat="server" Text="7.AHORRO DE LOS INTEGRANTES DEL GRUPO PRODUCTIVO:" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
          <asp:TextBox ID="txt7_ga" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" 
                            PlaceHolder=""></asp:TextBox>
            
     </div>



</div>

<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label23" runat="server" Text="8.APEGO AL REGLAMENTO INTERNO :" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
        <asp:TextBox ID="txt8_ga" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" 
                            PlaceHolder=""></asp:TextBox>
     </div>



</div>

<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label24" runat="server" Text="9.CRECIMIENTO DEL GRUPO PRODUCTIVO(INCORPORACIÓN DE NUEVOS INTEGRANTES):" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
          <asp:TextBox ID="txt9_ga" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" 
                            PlaceHolder=""></asp:TextBox>
     </div>



</div>

<div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label25" runat="server" Text="10.CUMPLIMIENTO REQUISITOS NORMATIVOS DE LOS NUEVOS INTEGRANTES (SOLVENCIA MORAL, NEGOCIO PROPIO, ARRAIGO EN EL DOMICILIO, CERCANIA GEOGRÁFICA):"  CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
         <asp:TextBox ID="txt10_ga" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" OnTextChanged="txt10_ga_TextChanged" AutoPostBack="true"
                            PlaceHolder=""></asp:TextBox>
     </div>



</div>
                                  <div class="row pad1m">
            <div class="col-lg-8 col-sm-8 text-left">
                <asp:Label ID="Label26" runat="server" Text="TOTAL:" CssClass="alingMiddle textoBold"></asp:Label></div>
     <div class="col-lg-4 col-sm-4 text-left">
        <asp:TextBox ID="txt_total2" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" AutoPostBack="true" ReadOnly="true" 
                            PlaceHolder=""></asp:TextBox>
     </div>
    </div>


            <br />
<div class="row pad1m">
    <div class="col-lg-8 col-sm-8 text-right">
        <asp:Label ID="Label27" runat="server" Text="CALIFICACIÓN OPTIMA" CssClass="alingMiddle textoBold"></asp:Label>
    </div>
     <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="TextBox6" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" Text="50 Puntos"
                            ReadOnly="true" ></asp:TextBox>
                         </div>
</div>

<div class="row pad1m">
    <div class="col-lg-8 col-sm-8 text-right ">
        <asp:Label ID="Label28" runat="server" Text="CALIFICACIÓN MÍNIMA ACEPTABLE PARA AUTORIZACIÓN DE CRÉDITO PRODUCTIVO" CssClass="alingMiddle textoBold"></asp:Label>
    </div>
     <div class="col-lg-4 col-sm-4 text-left">
                        <asp:TextBox ID="TextBox7" runat="server" 
                            CssClass="alingMiddle input-small" MaxLength="100" Text="40 Puntos"
                           ReadOnly="true" ></asp:TextBox>
                         </div>
    </div>
            </div>
                         <div class="row marTop">
                    <asp:Label ID="lblEditaAgrega" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblIdConsultaEdita" runat="server" Visible="false" ></asp:Label>
                </div>  

                <div class="row text-center">
                    <asp:LinkButton ID="lnkAgregaSolicitud" runat="server" ValidationGroup="crea" ToolTip="Guarda Solicitud" OnClick="lnkAgregaSolicitud_Click" CssClass="btn btn-success t14"  ><i class="fa fa-save"></i>&nbsp;<span>Guarda Evaluacion</span></asp:LinkButton>
                </div>
                          <div class="row text-center">
                    <asp:Label ID="lblDatos" runat="server" Text="Se requiere todos los campos marcados con *" Visible="false" CssClass="alert-danger"></asp:Label>
                </div>
                <div class="row text-center">
                    <asp:Label ID="lblErrorAgrega" runat="server" Visible="false" CssClass=""></asp:Label>
                </div>

                        
                     </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

