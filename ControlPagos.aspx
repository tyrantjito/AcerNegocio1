<%@ Page Title="" Language="C#" MasterPageFile="~/AdmonOrden.master" AutoEventWireup="true" CodeFile="ControlPagos.aspx.cs" Inherits="ControlPagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
         <ContentTemplate>
              <div class="page-header">
                <!-- /BREADCRUMBS -->
                <div class="clearfix">
                    <h3 class="content-title pull-left">Control de Pagos</h3>
                    <asp:Label ID="Label8" runat="server" CssClass="alert-danger"></asp:Label>
                </div>
            </div>


    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="MetroTouch"></telerik:RadAjaxLoadingPanel>
               <div class="col-lg-12 col-sm-12">
              <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label4" runat="server" Text="Semana:" CssClass="textoBold"></asp:Label>
                        </div>
                   <div class="col-lg-3 col-sm-3 text-left">
                       <asp:DropDownList ID="cmbplazo" runat="server" AutoPostBack="true" Visible="false" 
                            >
                                                <asp:ListItem Value="0">Seleccione Plazo</asp:ListItem>
                                                 <asp:ListItem Value="1">16 semanas</asp:ListItem>
                                                 <asp:ListItem Value="2">20 semanas</asp:ListItem>
                                                 <asp:ListItem Value="3">32 semanas</asp:ListItem>
                                                 <asp:ListItem Value="4">64 semanas</asp:ListItem>
                                                
                                             </asp:DropDownList>
                   </div>
                        <div class="col-lg-6 col-sm-6 text-left">
                                <asp:DropDownList ID="cmbSemanas" runat="server" Visible="false" AutoPostBack="true" OnTextChanged="cmbSemanas_TextChanged" >
                                                <asp:ListItem Value="0">Seleccione Semana</asp:ListItem>
                                                 <asp:ListItem Value="1">Semana 1</asp:ListItem>
                                                 <asp:ListItem Value="2">Semana 2</asp:ListItem>
                                                 <asp:ListItem Value="3">Semana 3</asp:ListItem>
                                                 <asp:ListItem Value="4">Semana 4</asp:ListItem>
                                                 <asp:ListItem Value="5">Semana 5</asp:ListItem>
                                                 <asp:ListItem Value="6">Semana 6</asp:ListItem>
                                                 <asp:ListItem Value="7">Semana 7</asp:ListItem>
                                                 <asp:ListItem Value="8">Semana 8</asp:ListItem>
                                                 <asp:ListItem Value="9">Semana 9</asp:ListItem>
                                                 <asp:ListItem Value="10"> Semana 10</asp:ListItem>
                                                 <asp:ListItem Value="11">Semana 11</asp:ListItem>
                                                 <asp:ListItem Value="12">Semana 12</asp:ListItem>
                                                 <asp:ListItem Value="13">Semana 13</asp:ListItem>
                                                 <asp:ListItem Value="14">Semana 14</asp:ListItem>
                                                 <asp:ListItem Value="15">Semana 15</asp:ListItem>
                                                 <asp:ListItem Value="16">Semana 16</asp:ListItem>
                                             </asp:DropDownList>
                            <asp:DropDownList ID="cmbSemanas20" runat="server" Visible="false" AutoPostBack="true" OnTextChanged="cmbSemanas20_TextChanged" >
                                                  <asp:ListItem Value="0">Seleccione Semana</asp:ListItem>
                                                 <asp:ListItem Value="1">Semana 1</asp:ListItem>
                                                 <asp:ListItem Value="2">Semana 2</asp:ListItem>
                                                 <asp:ListItem Value="3">Semana 3</asp:ListItem>
                                                 <asp:ListItem Value="4">Semana 4</asp:ListItem>
                                                 <asp:ListItem Value="5">Semana 5</asp:ListItem>
                                                 <asp:ListItem Value="6">Semana 6</asp:ListItem>
                                                 <asp:ListItem Value="7">Semana 7</asp:ListItem>
                                                 <asp:ListItem Value="8">Semana 8</asp:ListItem>
                                                 <asp:ListItem Value="9">Semana 9</asp:ListItem>
                                                 <asp:ListItem Value="10"> Semana 10</asp:ListItem>
                                                 <asp:ListItem Value="11">Semana 11</asp:ListItem>
                                                 <asp:ListItem Value="12">Semana 12</asp:ListItem>
                                                 <asp:ListItem Value="13">Semana 13</asp:ListItem>
                                                 <asp:ListItem Value="14">Semana 14</asp:ListItem>
                                                 <asp:ListItem Value="15">Semana 15</asp:ListItem>
                                                 <asp:ListItem Value="16">Semana 16</asp:ListItem>
                                                 <asp:ListItem Value="17">Semana 17</asp:ListItem>
                                                 <asp:ListItem Value="18">Semana 18</asp:ListItem>
                                                 <asp:ListItem Value="19">Semana 19</asp:ListItem>
                                                 <asp:ListItem Value="20">Semana 20</asp:ListItem>
                                             </asp:DropDownList>
                             <asp:DropDownList ID="cmbsemanas32" runat="server" Visible="false" AutoPostBack="true" OnTextChanged="cmbSemanas_TextChanged" >
                                                  <asp:ListItem Value="0">Seleccione Semana</asp:ListItem>
                                                 <asp:ListItem Value="1">Semana 1</asp:ListItem>
                                                 <asp:ListItem Value="2">Semana 2</asp:ListItem>
                                                 <asp:ListItem Value="3">Semana 3</asp:ListItem>
                                                 <asp:ListItem Value="4">Semana 4</asp:ListItem>
                                                 <asp:ListItem Value="5">Semana 5</asp:ListItem>
                                                 <asp:ListItem Value="6">Semana 6</asp:ListItem>
                                                 <asp:ListItem Value="7">Semana 7</asp:ListItem>
                                                 <asp:ListItem Value="8">Semana 8</asp:ListItem>
                                                 <asp:ListItem Value="9">Semana 9</asp:ListItem>
                                                 <asp:ListItem Value="10"> Semana 10</asp:ListItem>
                                                 <asp:ListItem Value="11">Semana 11</asp:ListItem>
                                                 <asp:ListItem Value="12">Semana 12</asp:ListItem>
                                                 <asp:ListItem Value="13">Semana 13</asp:ListItem>
                                                 <asp:ListItem Value="14">Semana 14</asp:ListItem>
                                                 <asp:ListItem Value="15">Semana 15</asp:ListItem>
                                                 <asp:ListItem Value="16">Semana 16</asp:ListItem>
                                                 <asp:ListItem Value="17">Semana 17</asp:ListItem>
                                                 <asp:ListItem Value="18">Semana 18</asp:ListItem>
                                                 <asp:ListItem Value="19">Semana 19</asp:ListItem>
                                                 <asp:ListItem Value="20">Semana 20</asp:ListItem>
                                                 <asp:ListItem Value="21">Semana 21</asp:ListItem>
                                                 <asp:ListItem Value="22">Semana 22</asp:ListItem>
                                                 <asp:ListItem Value="23">Semana 23</asp:ListItem>
                                                 <asp:ListItem Value="24">Semana 24</asp:ListItem>
                                                 <asp:ListItem Value="25">Semana 25</asp:ListItem>
                                                 <asp:ListItem Value="26"> Semana 26</asp:ListItem>
                                                 <asp:ListItem Value="27">Semana 27</asp:ListItem>
                                                 <asp:ListItem Value="28">Semana 28</asp:ListItem>
                                                 <asp:ListItem Value="29">Semana 29</asp:ListItem>
                                                 <asp:ListItem Value="30">Semana 30</asp:ListItem>
                                                 <asp:ListItem Value="31">Semana 31</asp:ListItem>
                                                 <asp:ListItem Value="32">Semana 32</asp:ListItem>
                                             </asp:DropDownList>
                             <asp:DropDownList ID="cmbsemanas64" runat="server" Visible="false" AutoPostBack="true" OnTextChanged="cmbSemanas_TextChanged" >
                                                         <asp:ListItem Value="0">Seleccione Semana</asp:ListItem>
                                                 <asp:ListItem Value="1">Semana 1</asp:ListItem>
                                                 <asp:ListItem Value="2">Semana 2</asp:ListItem>
                                                 <asp:ListItem Value="3">Semana 3</asp:ListItem>
                                                 <asp:ListItem Value="4">Semana 4</asp:ListItem>
                                                 <asp:ListItem Value="5">Semana 5</asp:ListItem>
                                                 <asp:ListItem Value="6">Semana 6</asp:ListItem>
                                                 <asp:ListItem Value="7">Semana 7</asp:ListItem>
                                                 <asp:ListItem Value="8">Semana 8</asp:ListItem>
                                                 <asp:ListItem Value="9">Semana 9</asp:ListItem>
                                                 <asp:ListItem Value="10"> Semana 10</asp:ListItem>
                                                 <asp:ListItem Value="11">Semana 11</asp:ListItem>
                                                 <asp:ListItem Value="12">Semana 12</asp:ListItem>
                                                 <asp:ListItem Value="13">Semana 13</asp:ListItem>
                                                 <asp:ListItem Value="14">Semana 14</asp:ListItem>
                                                 <asp:ListItem Value="15">Semana 15</asp:ListItem>
                                                 <asp:ListItem Value="16">Semana 16</asp:ListItem>
                                                 <asp:ListItem Value="17">Semana 17</asp:ListItem>
                                                 <asp:ListItem Value="18">Semana 18</asp:ListItem>
                                                 <asp:ListItem Value="19">Semana 19</asp:ListItem>
                                                 <asp:ListItem Value="20">Semana 20</asp:ListItem>
                                                 <asp:ListItem Value="21">Semana 21</asp:ListItem>
                                                 <asp:ListItem Value="22">Semana 22</asp:ListItem>
                                                 <asp:ListItem Value="23">Semana 23</asp:ListItem>
                                                 <asp:ListItem Value="24">Semana 24</asp:ListItem>
                                                 <asp:ListItem Value="25">Semana 25</asp:ListItem>
                                                 <asp:ListItem Value="26"> Semana 26</asp:ListItem>
                                                 <asp:ListItem Value="27">Semana 27</asp:ListItem>
                                                 <asp:ListItem Value="28">Semana 28</asp:ListItem>
                                                 <asp:ListItem Value="29">Semana 29</asp:ListItem>
                                                 <asp:ListItem Value="30">Semana 30</asp:ListItem>
                                                 <asp:ListItem Value="31">Semana 31</asp:ListItem>
                                                 <asp:ListItem Value="32">Semana 32</asp:ListItem>
                                                 <asp:ListItem Value="33">Semana 33</asp:ListItem>
                                                 <asp:ListItem Value="34">Semana 34</asp:ListItem>
                                                 <asp:ListItem Value="35">Semana 35</asp:ListItem>
                                                 <asp:ListItem Value="36">Semana 36</asp:ListItem>
                                                 <asp:ListItem Value="37">Semana 37</asp:ListItem>
                                                 <asp:ListItem Value="38">Semana 38</asp:ListItem>
                                                 <asp:ListItem Value="39">Semana 39</asp:ListItem>
                                                 <asp:ListItem Value="40">Semana 40</asp:ListItem>
                                                 <asp:ListItem Value="41">Semana 41</asp:ListItem>
                                                 <asp:ListItem Value="42"> Semana 42</asp:ListItem>
                                                 <asp:ListItem Value="43">Semana 43</asp:ListItem>
                                                 <asp:ListItem Value="44">Semana 44</asp:ListItem>
                                                 <asp:ListItem Value="45">Semana 45</asp:ListItem>
                                                 <asp:ListItem Value="46">Semana 46</asp:ListItem>
                                                 <asp:ListItem Value="47">Semana 47</asp:ListItem>
                                                 <asp:ListItem Value="48">Semana 48</asp:ListItem>
                                                 <asp:ListItem Value="49">Semana 49</asp:ListItem>
                                                 <asp:ListItem Value="50">Semana 50</asp:ListItem>
                                                 <asp:ListItem Value="51">Semana 51</asp:ListItem>
                                                 <asp:ListItem Value="52">Semana 52</asp:ListItem>
                                                 <asp:ListItem Value="53">Semana 53</asp:ListItem>
                                                 <asp:ListItem Value="54">Semana 54</asp:ListItem>
                                                 <asp:ListItem Value="55">Semana 55</asp:ListItem>
                                                 <asp:ListItem Value="56">Semana 56</asp:ListItem>
                                                 <asp:ListItem Value="57">Semana 57</asp:ListItem>
                                                 <asp:ListItem Value="58"> Semana 57</asp:ListItem>
                                                 <asp:ListItem Value="59">Semana 59</asp:ListItem>
                                                 <asp:ListItem Value="60">Semana 60</asp:ListItem>
                                                 <asp:ListItem Value="61">Semana 61</asp:ListItem>
                                                 <asp:ListItem Value="62">Semana 62</asp:ListItem>
                                                 <asp:ListItem Value="63">Semana 63</asp:ListItem>
                                                 <asp:ListItem Value="64">Semana 64</asp:ListItem>
                                             </asp:DropDownList>
                             <asp:TextBox ID="txtplazo" AutoPostBack="true" runat="server" Visible="false"
                                                 CssClass="alingMiddle input-large" MaxLength="18"
                                               ></asp:TextBox>
                             <asp:TextBox ID="txt_sem" AutoPostBack="true" runat="server" Visible="false"
                                                 CssClass="alingMiddle input-large" MaxLength="18"
                                               ></asp:TextBox>
                                 <br /><br />
                            </div>
                   <div class="col-lg-12 col-sm-12 text-center">
                      <asp:LinkButton ID="lnkImprimirControlPagoSemanal" runat="server"  ToolTip="Imprimir Control" OnClick="lnkImprimirControlPagoSemanal_Click" CssClass="btn btn-info"><i class="fa fa-print"></i>&nbsp;<span>Imprimir Control Pago Semanal</span></asp:LinkButton>
                       <br /><br />
                  </div>
                   <br /><br /><br /><br />
                   </div>
             <br /><br />
            

               <div class="col-lg-12 col-sm-12">

                   <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid2" GridLines="None" Visible="true" runat="server" PageSize="50" AllowAutomaticUpdates="True"   OnItemUpdated="RadGrid1_ItemUpdated"
                                        AllowPaging="false" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="false"  EnableHeaderContextMenu="true"   DataKeyNames="id_cliente"
                                         AllowSorting="true" DataSourceID="SqlDataSource2" Skin="Metro" ShowFooter="true" AllowAutomaticInserts="false" AllowAutomaticDeletes="false" >
                                        <MasterTableView CommandItemDisplay="Bottom" DataKeyNames="id_cliente" HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="False" DataSourceID="SqlDataSource2">                                    
                                            <BatchEditingSettings EditType="Row" />
                                            <CommandItemStyle CssClass="text-right" />
                                            <CommandItemSettings SaveChangesText="Guardar Cambios" ShowAddNewRecordButton="false"  ShowRefreshButton="false" ShowSaveChangesButton="true" CancelChangesText="Cancelar Cambios"/>
                                            <Columns>
                                                <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" Resizable="true" DataField="id_cliente" HeaderText="ID Cliente" SortExpression="id_cliente" UniqueName="id_cliente" ReadOnly="true" >
                                                    <ItemTemplate><%# Eval("id_cliente") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" Skin="Metro" ID="id_cliente" Width="100px" ShowSpinButtons="true" ></telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>                                   
                                                 <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" Resizable="true" DataField="nombre_cliente" HeaderText="Nombre" SortExpression="nombre_cliente" UniqueName="nombre_cliente" ReadOnly="true" HeaderStyle-Width="40%" >
                                                    <ItemTemplate><%# Eval("nombre_cliente") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" Skin="Metro" ID="nombreCli" Width="100px" ShowSpinButtons="true" ></telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>                                   
                                                <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" Resizable="true" DataField="GL" HeaderText="GL" SortExpression="GL" UniqueName="GL" HeaderStyle-Width="10%"  ReadOnly="true" >
                                                    <ItemTemplate><%# Eval("GL","{0:C2}") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox runat="server" RenderMode="Lightweight" Skin="Metro" ID="GL" Width="100px" ShowSpinButtons="true" MaxValue="9999999999.99" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-DecimalSeparator="."></telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>  
                                                 <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" Resizable="true" DataField="credito_autorizado" HeaderText="Credito Autorizado" SortExpression="credito_autorizado" UniqueName="credito_autorizado" HeaderStyle-Width="10%"  ReadOnly="true" >
                                                    <ItemTemplate><%# Eval("credito_autorizado","{0:C2}") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox runat="server" RenderMode="Lightweight" Skin="Metro" ID="creditoautorizado" Width="100px" ShowSpinButtons="true" MaxValue="9999999999.99" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-DecimalSeparator="."></telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>                                   
                                                <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" Resizable="true" DataField="pagosemanal" HeaderText="Pago Semanal" SortExpression="pagosemanal" UniqueName="pagosemanal" HeaderStyle-Width="10%"  ReadOnly="true">
                                                    <ItemTemplate><%# Eval("pagosemanal","{0:C2}") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox runat="server" RenderMode="Lightweight" Skin="Metro" ID="pagosemanal" Width="100px" ShowSpinButtons="true" MaxValue="9999999999.99" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-DecimalSeparator="."></telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                 <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" Resizable="true" DataField="saldo_actual" HeaderText="Saldo Actual" SortExpression="saldo_actual" UniqueName="saldo_actual" HeaderStyle-Width="10%"  ReadOnly="true">
                                                    <ItemTemplate><%# Eval("saldo_actual","{0:C2}") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox runat="server" RenderMode="Lightweight" Skin="Metro" ID="saldo_actual" Width="100px" ShowSpinButtons="true" MaxValue="9999999999.99" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-DecimalSeparator="."></telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" Resizable="true" DataField="Fecha_Pago" HeaderText="Fecha Pago" SortExpression="Fecha_Pago" UniqueName="Fecha_Pago" HeaderStyle-Width="10%"  ReadOnly="true">
                                                    <ItemTemplate><%# Eval("Fecha_Pago","{0:dd/MM/yyyy}") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" Skin="Metro" ID="fechapago" Width="100px" ShowSpinButtons="true"  NumberFormat-DecimalDigits="2" NumberFormat-DecimalSeparator="."></telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                 <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" Resizable="true" DataField="fecha_aplicacion" HeaderText="Fecha Aplicacion" SortExpression="fecha_aplicacion" UniqueName="fecha_aplicacion" HeaderStyle-Width="10%">
                                                    <ItemTemplate><%# Eval("fecha_aplicacion","{0:dd/MM/yyyy}") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" Skin="Metro" ID="fechaaplicacion" Width="100px" ShowSpinButtons="true"  NumberFormat-DecimalDigits="2" NumberFormat-DecimalSeparator="."></telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" Resizable="true" DataField="no_pago" HeaderText="Semana" Visible="true" SortExpression="no_pago" UniqueName="no_pago" HeaderStyle-Width="10%"  ReadOnly="true" >
                                                    <ItemTemplate><%# Eval("no_pago") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox runat="server" RenderMode="Lightweight" Skin="Metro" ID="nopago" Width="100px" ShowSpinButtons="true" MaxValue="9999999999.99" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-DecimalSeparator="."></telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                 <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" Resizable="true" DataField="monto_Pago" HeaderText="Pago" SortExpression="monto_Pago" UniqueName="monto_Pago" HeaderStyle-Width="10%"  ReadOnly="false">
                                                    <ItemTemplate><%# Eval("monto_Pago","{0:C2}") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox runat="server" RenderMode="Lightweight" Skin="Metro" ID="montoPago" Width="100px" ShowSpinButtons="true" MaxValue="9999999999.99" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-DecimalSeparator="."></telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>  
                                                 <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" Resizable="true" DataField="monto_Ahorro" HeaderText="Ahorro" SortExpression="monto_Ahorro" UniqueName="monto_Ahorro" HeaderStyle-Width="10%"  ReadOnly="false">
                                                    <ItemTemplate><%# Eval("monto_Ahorro","{0:C2}") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox runat="server" RenderMode="Lightweight" Skin="Metro" ID="montmonto_AhorrooPago" Width="100px" ShowSpinButtons="true" MaxValue="9999999999.99" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-DecimalSeparator="."></telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                 <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" Resizable="true" DataField="ap" HeaderText="AP" SortExpression="ap" UniqueName="ap" HeaderStyle-Width="10%"  ReadOnly="false">
                                                    <ItemTemplate><%# Eval("ap","{0:C2}") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox runat="server" RenderMode="Lightweight" Skin="Metro" ID="apor" Width="100px" ShowSpinButtons="true" MaxValue="9999999999.99" MinValue="0" NumberFormat-DecimalDigits="2"  NumberFormat-DecimalSeparator="."></telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" Resizable="true" DataField="dev" HeaderText="DEV" SortExpression="dev" UniqueName="dev" HeaderStyle-Width="10%"  ReadOnly="false">
                                                    <ItemTemplate><%# Eval("dev","{0:C2}") %></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox runat="server" RenderMode="Lightweight" Skin="Metro" ID="devo" Width="100px" ShowSpinButtons="true" MaxValue="9999999999.99" MinValue="0" NumberFormat-DecimalDigits="2"  NumberFormat-DecimalSeparator="."></telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>   
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <asp:Label ID="lblnoReecMo" runat="server" Text="No existen Pagos registrados" CssClass="errores"></asp:Label>
                                            </NoRecordsTemplate>
                                        </MasterTableView>                                
                                        <ClientSettings AllowKeyboardNavigation="true">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" ></Scrolling>
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <PagerStyle  PageSizeControlType="RadDropDownList" Mode="NextPrevAndNumeric"></PagerStyle>
                                    </telerik:RadGrid>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Taller %>"                                
                                        SelectCommand="select d.id_cliente,d.nombre_cliente,d.credito_autorizado*.10 as Gl,d.credito_autorizado,o.pagosemanal,o.saldo_actual,o.fecha_pago,o.fecha_aplicacion,o.no_pago,o.monto_Pago,o.monto_Ahorro,o.ap,o.dev  from AN_Solicitud_Credito_Detalle d left join AN_Operacion_Credito o on d.id_cliente = o.id_cliente where no_pago=1 and id_grupo=@credito "
                                        UpdateCommand="UPDATE an_operacion_credito SET monto_Pago=@monto_Pago,monto_Ahorro=@monto_Ahorro,fecha_aplicacion=@fecha_aplicacion,ap=@ap,dev=@dev WHERE id_empresa =@empresa AND id_sucursal = @sucursal and no_pago=@semana and id_grupo=@credito and id_cliente=@id_cliente ">
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0" />
                                             <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0" />
                                            <asp:QueryStringParameter Name="credito" QueryStringField="c" DefaultValue="0" />
                                            <asp:ControlParameter Name="pago" ControlID="txtplazo"  DefaultValue="0" />
                                            <asp:ControlParameter ControlID="txt_sem" Name="Semana" DefaultValue="0" />
                                        </SelectParameters>                                
                                        <UpdateParameters>   
                                            <asp:Parameter Name="no_pago" Type="Int32"></asp:Parameter>
                                             <asp:Parameter Name="id_cliente" Type="Int32"></asp:Parameter>
                                            <asp:Parameter Name="monto_Pago" Type="Decimal"></asp:Parameter>
                                            <asp:Parameter Name="monto_Ahorro" Type="Decimal" ></asp:Parameter>
                                            <asp:Parameter Name="fecha_aplicacion" Type="DateTime" ></asp:Parameter>  
                                            <asp:Parameter Name="ap" Type="Decimal"></asp:Parameter>
                                            <asp:Parameter Name="dev" Type="Decimal" ></asp:Parameter>                                     
                                            <asp:QueryStringParameter Name="empresa" QueryStringField="e" DefaultValue="0" />
                                             <asp:QueryStringParameter Name="sucursal" QueryStringField="t" DefaultValue="0" />
                                            <asp:QueryStringParameter Name="credito" QueryStringField="c" DefaultValue="0" />
                                            <asp:ControlParameter ControlID="txt_sem" Name="Semana" DefaultValue="0" />
                                            <asp:ControlParameter Name="pago" ControlID="txtplazo"  DefaultValue="0" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>

               </div>

         </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

