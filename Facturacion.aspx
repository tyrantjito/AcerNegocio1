﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdmonOrden.master" AutoEventWireup="true"
    CodeFile="Facturacion.aspx.cs" Inherits="Facturacion_Facturacion" Culture="es-Mx"
    UICulture="es-Mx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function abreWinEmi() {
            var oWnd = $find("<%=modalEmisores.ClientID%>");
            oWnd.setUrl('');
            oWnd.show();
        }

        function cierraWinEmi() {
            var oWnd = $find("<%=modalEmisores.ClientID%>");
            oWnd.close();
        }

        function abreWinRec() {
            var oWnd = $find("<%=modalReceptores.ClientID%>");
            oWnd.setUrl('');
            oWnd.show();
        }

        function cierraWinRec() {
            var oWnd = $find("<%=modalReceptores.ClientID%>");
            oWnd.close();
        }

         function abreWinCatRec() {
            var oWnd = $find("<%=RadCatReceptores.ClientID%>");
            oWnd.setUrl('');
            oWnd.show();
        }

        function cierraWinCatRec() {
            var oWnd = $find("<%=RadCatReceptores.ClientID%>");
            oWnd.close();
        }

        function demo() {
            alert("JavaScript Called");
            var data = [{ "Concepto": "", "Importe": "", "SubTotal": "", "Imp. Tras.": "", "Imp. Ret.": "", "Total": "", "Select": "" }, { "Concepto": "", "Importe": "", "SubTotal": "", "Imp. Tras.": "", "Imp. Ret.": "", "Total": "", "Select": "" }];
            var mtv = $find("<%=grdDocu.ClientID%>").get_masterTableView();
            var grdDocu = $find("<%=grdDocu.ClientID%>");
            var batchEditingManager = grdDocu.get_batchEditingManager();
            batchEditingManager.addNewRecord(mtv);
            //mtv.set_dataSource(data); 
            //mtv.dataBind();
        }

        function muestraFila() {
            var masterTable = $find("<%=grdDocu.ClientID%>").get_masterTableView();
            var count = masterTable.get_dataItems().length;
            var checkbox;
            var item;
            for (var i = 0; i < count; i++) {
                item = masterTable.get_dataItems()[i];
                if (item.get_visible() == false) {
                    item.set_visible(true);
                    return false;
                }
                //checkbox = item.findElement("chkProduct");
                //if (checkbox.checked) {
                //    var dataKeyValue = item.getDataKeyValue("ContactName");
                //    alert(dataKeyValue);
                //}
            }
        }

        function leeConcpto() {
            var masterTable = $find("<%=grdDocu.ClientID%>").get_masterTableView();
            var count = masterTable.get_dataItems().length;
            var txtIdent;
            var txtConcepto;
            var item;
            for (var i = 0; i < count; i++) {
                item = masterTable.get_dataItems()[i];
                if (i == count - 1) {
                    txtIdent = item.findElement("txtIdent");
                    txtConcepto = item.findElement("txtConcepto");
                    if (txtConcepto.value.trim() == "" || txtIdent.value.trim() == "") {

                        alert("El Identificador y la descripción del Concepto deben ser capturados.");
                        return false;
                    }
                    else {
                        var lblTotal = item.findElement("lblTotal");
                        //masterTable.fireCommand("InitInsert", "");
                    }
                }
            }
        }

        var nodo;
        function ddlImp_ItemSelected(sender, eventArgs) {
            var impDescrip = eventArgs.get_item().get_text();
            var impId = eventArgs.get_item().get_value();
            var grdDocu = $find("<%=grdDocu.ClientID%>");
            var mtv = grdDocu.get_masterTableView();
            var ivaValor;

            //Para buscar control
            nodo = getElement(sender.get_element());
            /*--NOTA: Si se cambia el orden o valores en la tabla "ImpTrasladado",
            actualizar también estos valores que corresponden al IVA*/
            switch (impId) {
                case "1", "4", "7":
                    ivaValor = 0;
                    break;
                case "2":
                    ivaValor = 0.16;
                    break;
                case "3":
                    ivaValor = 0.11;
                    break;
                default:
                    ivaValor = 0;
            }

            var lblIvaTras = buscaElemento(nodo, "lblIvaTras");
            var lblSubTot = buscaElemento(nodo, "lblSubTotal");
            var lblTotal = buscaElemento(nodo, "lblTotal");

            var subTot = lblSubTot.get_value(); //.innerHTML;
            var ImpTot = subTot * ivaValor;

            if (lblIvaTras) {
                lblIvaTras.set_value(ImpTot.toFixed(2));
                lblTotal.set_value((Number(subTot) + Number(ImpTot)).toFixed(2));
                //alert((Number(subTot) + Number(ImpTot)));
                //grdDocu.set_activeRow(nodo);
            }
            var itemIndex = 0;
            //mtv.updateItem(itemIndex);
        }

        function buscaElemento(node, control) {
            var foundElement = null;
            while (node) {
                //foundElement = $telerik.findElement(node, control);
                foundElement = $telerik.findControl(node, control);
                if (foundElement) {
                    nodo = node;
                    break;
                }
                node = getElement(node.parentNode);
            }
            return foundElement;
        }

        function getElement(element) {
            return Telerik.Web.UI.Grid.GetFirstParentByTagName(element, "tr");
        }




    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" />
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" />
    <telerik:RadFormDecorator ID="RadFormDecorator1" RenderMode="Lightweight" runat="server"
        DecoratedControls="Buttons" />

    <%-- Catalogo de Emisores --%>
    <telerik:RadWindow RenderMode="Lightweight" ID="modalEmisores" Title="Emisores" EnableShadow="true"
        Behaviors="Default" VisibleOnPageLoad="false" ShowContentDuringLoad="false" DestroyOnClose="true"
        Animation="Fade" runat="server" Modal="true" Width="790px" Height="590px" Style="z-index: 1000;">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanelEmi" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlPrincipal" runat="server" CssClass="ancho100 text-center">
                        <div class="col-lg-6 col-sm-6 pad1m text-left">
                            <asp:LinkButton ID="lnkSeleccionarEmisor" runat="server" CssClass="btn btn-success t14"
                                OnClick="lnkSeleccionarEmisor_Click"><i class="fa fa-check-circle"></i><span>&nbsp;Seleccionar</span></asp:LinkButton>
                            <asp:LinkButton ID="lnkAgregarEmisor" runat="server" CssClass="btn btn-info t14"><i class="fa fa-plus-circle"></i><span>&nbsp;Agregar</span></asp:LinkButton>
                            <asp:LinkButton ID="lnkEditaEmisor" runat="server" CssClass="btn btn-warning t14"><i class="fa fa-edit"></i><span>&nbsp;Editar</span></asp:LinkButton>
                            <asp:LinkButton ID="lnkEliminaEmisor" runat="server" CssClass="btn btn-danger t14"><i class="fa fa-trash"></i><span>&nbsp;Eliminar</span></asp:LinkButton>

                        </div>
                        <div class="col-lg-6 col-sm-6 pad1m text-right">
                            <asp:Label ID="Label51" runat="server" Text="Buscar:" CssClass="textoBold"></asp:Label>&nbsp;
                            <asp:TextBox ID="txtBusqueda" runat="server" CssClass="input-medium" placeHolder="Buscar..."></asp:TextBox>&nbsp;
                            <asp:LinkButton ID="lnkBusqueda" runat="server" CssClass="btn btn-info t14" OnClick="lnkBusqueda_Click"><i class="fa fa-search"></i></asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="lnkLimpiar" runat="server" CssClass="link t8" OnClick="lnkLimpiar_Click">Limpiar B&uacute;squeda</asp:LinkButton>
                        </div>
                        <div class="col-lg-12 col-sm-12 text-center">
                            <asp:Label ID="lblErrorEmisores" runat="server" CssClass="errores"></asp:Label>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="grdEmisores" runat="server" CssClass="table table-bordered" AllowPaging="True"
                                AllowSorting="True" AutoGenerateColumns="False" EmptyDataText="No existen Emisores Registrados"
                                DataKeyNames="IdEmisor" DataSourceID="SqlDataSource1" PageSize="5" OnPageIndexChanged="grdEmisores_PageIndexChanged"
                                OnSelectedIndexChanged="grdEmisores_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="IdEmisor" HeaderText="IdEmisor" InsertVisible="False"
                                        ReadOnly="True" SortExpression="IdEmisor" Visible="False" />
                                    <asp:BoundField DataField="EmRFC" HeaderText="R.F.C." SortExpression="EmRFC" />
                                    <asp:BoundField DataField="EmNombre" HeaderText="Nombre / Razón Social" SortExpression="EmNombre" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSeleccionaeEmi" runat="server" CausesValidation="False" CommandName="Select"
                                                ToolTip="Seleccionar" CssClass="btn btn-primary t14"><i class="fa fa-check"></i><span>&nbsp;Seleccionar</span></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle CssClass="alert-info" />
                                <EmptyDataRowStyle CssClass="errores" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:eFactura %>"
                                SelectCommand="select IdEmisor,EmRFC,EmNombre from emisores"></asp:SqlDataSource>
                        </div>
                        <div class="col-lg-12 col-sm-12">
                            <asp:Label ID="Label52" runat="server" Text="Nombre Corto:" CssClass="textoBold"></asp:Label>&nbsp;
                            <asp:Label ID="lblEmiNomCto" runat="server"></asp:Label>
                            <asp:Label ID="lblIdEmisor" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="col-lg-12 col-sm-12 marTop9px">
                            <telerik:RadTabStrip ID="tabInfoEmi" RenderMode="Lightweight" runat="server" MultiPageID="multiPaginaEmi"
                                SelectedIndex="0" Skin="MetroTouch">
                                <Tabs>
                                    <telerik:RadTab Text="Domicilio Fiscal" />
                                    <telerik:RadTab Text="Lugar de Expedición de Documentos" />
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage runat="server" ID="multiPaginaEmi" SelectedIndex="0" SkinID="MetroTouch">
                                <telerik:RadPageView runat="server" ID="vDomicilioEmi">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label7" runat="server" Text="Calle:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblCalleEmi" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label9" runat="server" Text="Número Exterior:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblNoExtEmi" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label11" runat="server" Text="Número Interior:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblNoIntEmi" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label13" runat="server" Text="Colonia:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblColoniaEmi" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label14" runat="server" Text="Delegación/Municipio:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblDelMunEmi" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label15" runat="server" Text="Estado:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblEdoEmi" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label16" runat="server" Text="País:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblPaisEmi" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label17" runat="server" Text="Código Postal:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblCpEmi" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label18" runat="server" Text="Ciudad/Localidad:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblLocalidadEmi" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label19" runat="server" Text="Referencia:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblReferenciaEmi" runat="server"></asp:Label>
                                    </div>
                                </telerik:RadPageView>
                                <telerik:RadPageView runat="server" ID="vExpedicionEmi">
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label20" runat="server" Text="Calle:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblCalleEx" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label21" runat="server" Text="Número Exterior:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblNoExtEx" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label22" runat="server" Text="Número Interior:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblNoIntEx" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label23" runat="server" Text="Colonia:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblColoniaEx" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label24" runat="server" Text="Delegación/Municipio:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblDelMunEx" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label25" runat="server" Text="Estado:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblEdoEx" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label26" runat="server" Text="País:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblPaisEx" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label27" runat="server" Text="Código Postal:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblCpEx" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label29" runat="server" Text="Ciudad/Localidad:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblLocalidadEx" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="Label31" runat="server" Text="Referencia:" CssClass="textoBold"></asp:Label>
                                    </div>
                                    <div class="col-lg-3 col-sm-3 text-left">
                                        <asp:Label ID="lblReferenciaEx" runat="server"></asp:Label>
                                    </div>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </div>
                    </asp:Panel>
                    <asp:UpdateProgress ID="updProgEmi" runat="server" AssociatedUpdatePanelID="UpdatePanelEmi">
                        <ProgressTemplate>
                            <asp:Panel ID="pnlMaskLoadEmi" runat="server" CssClass="maskLoad" />
                            <asp:Panel ID="pnlCargandoEmi" runat="server" CssClass="pnlPopUpLoad">
                                <asp:Image ID="imgLoadEmi" runat="server" ImageUrl="~/img/loading.gif" CssClass="ancho100" />
                            </asp:Panel>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>


    <%-- Catalogo de Receptores --%>
    <telerik:RadWindow RenderMode="Lightweight" ID="modalReceptores" Title="Receptores" EnableShadow="true"
        Behaviors="Default" VisibleOnPageLoad="false" ShowContentDuringLoad="false" DestroyOnClose="true"
        Animation="Fade" runat="server" Modal="true" Width="790px" Height="590px" Style="z-index: 1000;">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanelRec" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlPrincipalReceptores" runat="server" CssClass="ancho100 text-center">
                        <div class="col-lg-6 col-sm-6 pad1m text-left">
                            <asp:LinkButton ID="lnkSeleccionarReceptor" runat="server" CssClass="btn btn-success t14"
                                OnClick="lnkSeleccionarReceptor_Click"><i class="fa fa-check-circle"></i><span>&nbsp;Seleccionar</span></asp:LinkButton>
                            <asp:LinkButton ID="lnkAgregaRec" runat="server" CssClass="btn btn-info t14" OnClick="lnkAgregaRec_Click"><i class="fa fa-plus-circle"></i><span>&nbsp;Agregar</span></asp:LinkButton>
                            <asp:LinkButton ID="lnkEditaRec" runat="server" CssClass="btn btn-warning t14" OnClick="lnkEditaRec_Click"><i class="fa fa-edit"></i><span>&nbsp;Editar</span></asp:LinkButton>
                            <asp:LinkButton ID="lnkEliminaRec" runat="server" CssClass="btn btn-danger t14" OnClick="lnkEliminaRec_Click"><i class="fa fa-trash"></i><span>&nbsp;Eliminar</span></asp:LinkButton>

                        </div>
                        <div class="col-lg-6 col-sm-6 pad1m text-right">
                            <asp:Label ID="Label32" runat="server" Text="Buscar:" CssClass="textoBold"></asp:Label>&nbsp;
                            <asp:TextBox ID="txtBuscarRec" runat="server" CssClass="input-medium" placeHolder="Buscar..."></asp:TextBox>&nbsp;
                            <asp:LinkButton ID="lnkBusquedaRec" runat="server" CssClass="btn btn-info t14" OnClick="lnkBusquedaRec_Click"><i class="fa fa-search"></i></asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="lnkLimpiarRec" runat="server" CssClass="link t8" OnClick="lnkLimpiarRec_Click">Limpiar B&uacute;squeda</asp:LinkButton>
                        </div>
                        <div class="col-lg-12 col-sm-12 text-center">
                            <asp:Label ID="lblErrorRec" runat="server" CssClass="errores"></asp:Label></div>
                        
                        <div class="table-responsive">
                            <asp:GridView ID="grdReceptores" runat="server" CssClass="table table-bordered" AllowPaging="True"
                                AllowSorting="True" AutoGenerateColumns="False" EmptyDataText="No existen Receptores Registrados"
                                DataKeyNames="IdRecep" PageSize="5" OnPageIndexChanged="grdReceptores_PageIndexChanged"
                                OnSelectedIndexChanged="grdReceptores_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="IdRecep" HeaderText="IdRecep" InsertVisible="False" ReadOnly="True" SortExpression="IdRecep" Visible="False" />
                                    <asp:BoundField DataField="ReRFC" HeaderText="R.F.C." SortExpression="ReRFC" />
                                    <asp:BoundField DataField="ReNombre" HeaderText="Nombre / Razón Social" SortExpression="ReNombre" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSeleccionaRec" runat="server" CausesValidation="False" CommandName="Select"
                                                ToolTip="Seleccionar" CssClass="btn btn-primary t14"><i class="fa fa-check"></i><span>&nbsp;Seleccionar</span></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle CssClass="alert-info" />
                                <EmptyDataRowStyle CssClass="errores" />
                            </asp:GridView>                            
                        </div>
                        <div class="col-lg-12 col-sm-12 marTop9px">
                            <asp:Label ID="lblIdReceptor" runat="server" Visible="false"></asp:Label>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="Label48" runat="server" Text="Calle:" CssClass="textoBold"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="lblCalleRec" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="Label54" runat="server" Text="Número Exterior:" CssClass="textoBold"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="lblNoExtRec" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="Label58" runat="server" Text="Número Interior:" CssClass="textoBold"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="lblNoIntRec" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="Label62" runat="server" Text="Colonia:" CssClass="textoBold"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="lblColoniaRec" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="Label65" runat="server" Text="Delegación/Municipio:" CssClass="textoBold"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="lblDelMunRec" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="Label67" runat="server" Text="Estado:" CssClass="textoBold"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="lblEdoRec" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="Label69" runat="server" Text="País:" CssClass="textoBold"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="lblPaisRec" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="Label71" runat="server" Text="Código Postal:" CssClass="textoBold"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="lblCpRec" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="Label73" runat="server" Text="Ciudad/Localidad:" CssClass="textoBold"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="lblLocalidadRec" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="Label75" runat="server" Text="Referencia:" CssClass="textoBold"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="lblReferenciaRec" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="Label34" runat="server" Text="Correo Electrónico:" CssClass="textoBold"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="lblCorreoRec" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="Label40" runat="server" Text="Correo CC:" CssClass="textoBold"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="lblCorreoCCRec" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="Label50" runat="server" Text="Correo CCO:" CssClass="textoBold"></asp:Label>
                            </div>
                            <div class="col-lg-3 col-sm-3 text-left">
                                <asp:Label ID="lblCorreoCCORec" runat="server"></asp:Label>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:UpdateProgress ID="updProgRec" runat="server" AssociatedUpdatePanelID="UpdatePanelRec">
                        <ProgressTemplate>
                            <asp:Panel ID="pnlMaskLoadRec" runat="server" CssClass="maskLoad" />
                            <asp:Panel ID="pnlCargandoRec" runat="server" CssClass="pnlPopUpLoad">
                                <asp:Image ID="imgLoadRec" runat="server" ImageUrl="~/img/loading.gif" CssClass="ancho100" />
                            </asp:Panel>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>


    <telerik:RadWindow RenderMode="Lightweight" ID="RadCatReceptores" Title="Crea o Modifica Receptor" EnableShadow="true"
        Behaviors="Default" VisibleOnPageLoad="false" ShowContentDuringLoad="false" DestroyOnClose="true"
        Animation="Fade" runat="server" Modal="true" Width="790px" Height="590px" Style="z-index: 2000;">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PanelAdd" runat="server" CssClass="col-lg-12 col-sm-12 text-center">
                        <asp:Label ID="lblModo" runat="server" Visible="false"/>
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label87" runat="server" Text="Persona: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:RadioButtonList ID="rbtnPersona" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtnPersona_SelectedIndexChanged" CssClass="centrado">
                                    <asp:ListItem Value="F" Selected="True" Text="Fisica" />
                                    <asp:ListItem Value="M" Text="Moral" />
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">&nbsp;</div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label90" runat="server" Text="R.F.C.: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left"><asp:TextBox ID="txtRfc" runat="server" CssClass="input-medium" placeholder="R.F.C." /></div>
                            <div class="col-lg-4 col-sm-4 text-left"><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Debe indicar el R.F.C." Text="*" ValidationGroup="crea" ControlToValidate="txtRfc" CssClass="alineado errores"></asp:RequiredFieldValidator></div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label92" runat="server" Text="Razón Social: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtRazonNew" runat="server" CssClass="input-medium" MaxLength="200" placeholder="Razón Social" />                                
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Debe indicar la Razón Social" Text="*" ValidationGroup="crea" ControlToValidate="txtRazonNew" CssClass="alineado errores"></asp:RequiredFieldValidator>                               
                            </div>
                        </div>                        
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label97" runat="server" Text="Calle: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtCalle" runat="server" MaxLength="200" CssClass="input-medium" placeholder="Calle"></asp:TextBox>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator71" runat="server" ErrorMessage="Debe indicar la Calle." Text="*" CssClass="alineado errores" ControlToValidate="txtCalle" ValidationGroup="crea"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label98" runat="server" Text="No. Ext.: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtNoExt" runat="server" MaxLength="20" CssClass="input-small" placeholder="No. Ext."></asp:TextBox>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator72" runat="server" ErrorMessage="Debe indicar el No. Exterior." Text="*" CssClass="alineado errores" ControlToValidate="txtNoExt" ValidationGroup="crea"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label99" runat="server" Text="No. Int.: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left"><asp:TextBox ID="txtNoIntMod" runat="server" MaxLength="20" CssClass="input-small" placeholder="No. Int."></asp:TextBox></div>
                            <div class="col-lg-4 col-sm-4 text-left">&nbsp;</div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label100" runat="server" Text="País: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <telerik:RadComboBox RenderMode="Lightweight" ID="ddlPais" runat="server" Width="200" Height="200px" DataValueField="cve_pais" AutoPostBack="true" MarkFirstMatch="true" AllowCustomText="false" EnableLoadOnDemand="false" AutoCompleteSeparator="None" Filter="Contains" 
                                    EmptyMessage="Seleccione País..." DataSourceID="SqlDataSource10" DataTextField="desc_pais" Skin="Silk" OnDataBinding="PreventErrorOnbinding" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged">
                                </telerik:RadComboBox>
                                <asp:SqlDataSource ID="SqlDataSource10" runat="server" ConnectionString="<%$ ConnectionStrings:eFactura %>" SelectCommand="select cve_pais,desc_pais from Paises"></asp:SqlDataSource>                                
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator73" runat="server" ErrorMessage="Debe indicar el País." Text="*" CssClass="alineado errores" ControlToValidate="ddlPais" ValidationGroup="crea"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label101" runat="server" Text="Estado: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left">                                
                                <telerik:RadComboBox RenderMode="Lightweight" ID="ddlEstado" runat="server" Width="200" Height="200px" DataValueField="cve_edo" AutoPostBack="true" MarkFirstMatch="true" AllowCustomText="false" EnableLoadOnDemand="false" AutoCompleteSeparator="None" Filter="Contains"
                                    EmptyMessage="Seleccione Estado..." DataSourceID="SqlDataSource11" DataTextField="nom_edo" Skin="Silk" OnDataBinding="PreventErrorOnbinding" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                                </telerik:RadComboBox>
                                <asp:SqlDataSource ID="SqlDataSource11" runat="server" ConnectionString="<%$ ConnectionStrings:eFactura %>" SelectCommand="select cve_edo,nom_edo from Estados where cve_pais=@pais">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlPais" Name="pais" PropertyName="SelectedValue" DefaultValue="0" />
                                    </SelectParameters>
                                </asp:SqlDataSource>                                
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator74" runat="server" ErrorMessage="Debe indicar el Estado." Text="*" CssClass="alineado errores" ControlToValidate="ddlEstado" ValidationGroup="crea"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label102" runat="server" Text="Municip. o Deleg.: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <telerik:RadComboBox RenderMode="Lightweight" ID="ddlMunicipio" runat="server" Width="200" Height="200px" OnDataBinding="PreventErrorOnbinding" DataValueField="ID_Del_Mun" AutoPostBack="true" MarkFirstMatch="true" AllowCustomText="false" EnableLoadOnDemand="false" AutoCompleteSeparator="None" Filter="Contains"
                                    EmptyMessage="Seleccione Deleg./Municip. ..." DataSourceID="SqlDataSource12" DataTextField="Desc_Del_Mun" Skin="Silk" OnSelectedIndexChanged="ddlMunicipio_SelectedIndexChanged">
                                </telerik:RadComboBox>
                                <asp:SqlDataSource ID="SqlDataSource12" runat="server" ConnectionString="<%$ ConnectionStrings:eFactura %>" SelectCommand="select ID_Del_Mun,Desc_Del_Mun from DelegacionMunicipio where cve_pais=@pais and ID_Estado=@estado">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlPais" Name="pais" PropertyName="SelectedValue" DefaultValue="0"/>
                                        <asp:ControlParameter ControlID="ddlEstado" Name="estado" PropertyName="SelectedValue" DefaultValue="0"/>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator75" runat="server" ErrorMessage="Debe indicar el Municipio." Text="*" CssClass="alineado errores" ControlToValidate="ddlMunicipio" ValidationGroup="crea"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label103" runat="server" Text="Colonia: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <telerik:RadComboBox RenderMode="Lightweight" ID="ddlColonia" runat="server" Width="200" Height="200px" OnDataBinding="PreventErrorOnbinding" DataValueField="ID_Colonia" AutoPostBack="true" MarkFirstMatch="true" AllowCustomText="false" EnableLoadOnDemand="false" AutoCompleteSeparator="None" Filter="Contains"
                                    EmptyMessage="Seleccione Colonia ..." DataSourceID="SqlDataSource13" DataTextField="Desc_Colonia" Skin="Silk" OnSelectedIndexChanged="ddlColonia_SelectedIndexChanged">                                    
                                </telerik:RadComboBox>
                                <asp:SqlDataSource ID="SqlDataSource13" runat="server" ConnectionString="<%$ ConnectionStrings:eFactura %>" SelectCommand="select ID_Colonia,Desc_Colonia from Colonias where cve_pais=@pais and ID_Estado=@estado and ID_Del_Mun=@municipio">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlPais" Name="pais" PropertyName="SelectedValue" DefaultValue="0"/>
                                        <asp:ControlParameter ControlID="ddlEstado" Name="estado" PropertyName="SelectedValue" DefaultValue="0"/>
                                        <asp:ControlParameter ControlID="ddlMunicipio" Name="municipio" PropertyName="SelectedValue" DefaultValue="0"/>
                                    </SelectParameters>
                                </asp:SqlDataSource>                                
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator76" runat="server" ErrorMessage="Debe indicar la Colonia." Text="*" CssClass="alineado errores" ControlToValidate="ddlColonia" ValidationGroup="crea"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label104" runat="server" Text="C.P.: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <telerik:RadComboBox RenderMode="Lightweight" ID="ddlCodigo" runat="server" Width="100" Height="100px" OnDataBinding="PreventErrorOnbinding" DataValueField="ID_Cod_Pos" AutoPostBack="true" MarkFirstMatch="true" AllowCustomText="false" EnableLoadOnDemand="false" AutoCompleteSeparator="None" Filter="Contains"
                                    EmptyMessage="Seleccione Código Postal ..." DataSourceID="SqlDataSource14" DataTextField="ID_Cod_Pos" Skin="Silk">
                                </telerik:RadComboBox>
                                <asp:SqlDataSource ID="SqlDataSource14" runat="server" ConnectionString="<%$ ConnectionStrings:eFactura %>" SelectCommand="select case len(id_cod_pos) when 4 then '0'+id_cod_pos else id_cod_pos end as ID_Cod_Pos from Colonias where cve_pais=@pais and ID_Estado=@estado and ID_Del_Mun=@municipio and ID_Colonia=@colonia">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlPais" Name="pais" PropertyName="SelectedValue" DefaultValue="0"/>
                                        <asp:ControlParameter ControlID="ddlEstado" Name="estado" PropertyName="SelectedValue" DefaultValue="0"/>
                                        <asp:ControlParameter ControlID="ddlMunicipio" Name="municipio" PropertyName="SelectedValue" DefaultValue="0"/>
                                        <asp:ControlParameter ControlID="ddlColonia" Name="colonia" PropertyName="SelectedValue" DefaultValue="0"/>
                                    </SelectParameters>
                                </asp:SqlDataSource>                                
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left"><asp:RequiredFieldValidator ID="RequiredFieldValidator77" runat="server" ErrorMessage="Debe indicar el Código Postal." Text="*" CssClass="alineado errores" ControlToValidate="ddlCodigo" ValidationGroup="crea"></asp:RequiredFieldValidator></div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label105" runat="server" Text="Localidad: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left"><asp:TextBox ID="txtLocalidad" runat="server" MaxLength="50" CssClass="input-large" placeholder="Localidad"></asp:TextBox></div>
                            <div class="col-lg-4 col-sm-4 text-left">&nbsp;</div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label106" runat="server" Text="Referencia: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left"><asp:TextBox ID="txtReferenciaMod" runat="server" MaxLength="50" CssClass="input-large" placeholder="Referencia"></asp:TextBox></div>
                            <div class="col-lg-4 col-sm-4 text-left"></div>
                        </div>                        
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label115" runat="server" Text="Correo: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtCorreo" runat="server"  MaxLength="250" CssClass="input-large"></asp:TextBox>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator78" runat="server" ErrorMessage="Debe indicar el Correo." Text="*" CssClass="alineado errores" ControlToValidate="txtCorreo" ValidationGroup="crea"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label94" runat="server" Text="Correo CC: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtCorreoCC" runat="server"  MaxLength="250" CssClass="input-large"></asp:TextBox>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Debe indicar el Correo CC." Text="*" CssClass="alineado errores" ControlToValidate="txtCorreoCC" ValidationGroup="crea"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-sm-4 text-right"><asp:Label ID="Label96" runat="server" Text="Correo CCO: " /></div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:TextBox ID="txtCorreoCCO" runat="server"  MaxLength="250" CssClass="input-large"></asp:TextBox>
                            </div>
                            <div class="col-lg-4 col-sm-4 text-left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Debe indicar el Correo CCO." Text="*" CssClass="alineado errores" ControlToValidate="txtCorreoCCO" ValidationGroup="crea"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 col-sm-12 text-center">
                                <asp:Label ID="lblErrorActuraliza" runat="server" CssClass="errores"></asp:Label><br />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="crea" CssClass="errores" DisplayMode="List" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-sm-6 text-center">
                                <asp:LinkButton ID="lnkAceptarModificacion" runat="server" CssClass="btn btn-success t14" OnClick="lnkAceptarModificacion_Click" ValidationGroup="crea"><i class="fa fa-check"></i><span>&nbsp;Aceptar</span></asp:LinkButton>
                            </div>
                            <div class="col-lg-6 col-sm-6 text-center">
                                <asp:LinkButton ID="lnkCancelaMOd" runat="server" CssClass="btn btn-danger t14" OnClientClick="cierraWinCatRec()"><i class="fa fa-remove"></i><span>&nbsp;Cerrar</span></asp:LinkButton>
                            </div>
                        </div>

                        
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>



    <div class="row">
        <div class="col-lg-12 col-sm-12 text-center alert-info">
            <h3>
                <i class="fa fa-qrcode"></i>&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Text="Facturación"></asp:Label>
            </h3>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="updPanelGeneralesFact">
        <ContentTemplate>

            <asp:Panel runat="server" ID="pnlOperacionesFact" CssClass="col-lg-12 col-ms-12 text-center pad1m">
                <div class="col-lg-12 col-ms-12">
                    <asp:Label ID="Label85" runat="server" Text="Opciones De Facturación: " CssClass="textoBold"></asp:Label>
                    <asp:DropDownList ID="rdlOpcionesFactura" runat="server"
                        CssClass="input-medium" AutoPostBack="True"
                        OnSelectedIndexChanged="rdlOpcionesFactura_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="1">Global</asp:ListItem>
                        <asp:ListItem Value="2">Mano Obra</asp:ListItem>
                        <asp:ListItem Value="3">Refacciones</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlDesglose" runat="server" CssClass="input-medium"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlDesglose_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="1">Desglosada</asp:ListItem>
                        <asp:ListItem Value="2">Global</asp:ListItem>
                        <asp:ListItem Value="3">Agrupada</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtGlobal1" runat="server" CssClass="textNota" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtGlobal2" runat="server" CssClass="textNota" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkCargarInfo" runat="server" CssClass="btn btn-info t14"
                        OnClick="lnkCargarInfo_Click"><i class="fa fa-cogs"></i><span>&nbsp;Cargar Informaci&oacute;n</span></asp:LinkButton>
                </div>
            </asp:Panel>


            <asp:UpdateProgress ID="UpdateProgressgral" runat="server" AssociatedUpdatePanelID="updPanelGeneralesFact">
                <ProgressTemplate>
                    <asp:Panel ID="pnlMaskLoadgral" runat="server" CssClass="maskLoad" />
                    <asp:Panel ID="pnlCargandogral" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoadgral" runat="server" ImageUrl="~/img/loading.gif" CssClass="ancho100" />
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>



    <%-- Captura de Factura --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanelFact">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-sm-12 text-center">
                    <asp:Label ID="lblError" runat="server" CssClass="errores" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="facturas" />
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlDaños" CssClass="col-lg-12 col-sm-12">
                <telerik:RadTabStrip ID="tabFactura" RenderMode="Lightweight" runat="server" MultiPageID="multiPagina"
                    SelectedIndex="0" Skin="MetroTouch">
                    <Tabs>
                        <telerik:RadTab Text="Emisor" />
                        <telerik:RadTab Text="Lugar de Expedición" />
                        <telerik:RadTab Text="Receptor" />
                        <telerik:RadTab Text="Detalles" />
                        <telerik:RadTab Text="Información Adicional" />
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage runat="server" ID="multiPagina" SelectedIndex="0" SkinID="MetroTouch">
                    <telerik:RadPageView runat="server" ID="vEmisor">
                        <div class="col-lg-12 col-sm-12 text-left">
                            <asp:Label ID="Label3" runat="server" Text="Emisor: " CssClass="textoBold"></asp:Label>
                            <asp:Label ID="lblEmisorFacturas" runat="server" Visible="false" />
                            <asp:Label ID="lblRfcEmisor" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblRazonEmisor" runat="server"></asp:Label>
                            <asp:LinkButton ID="lnkBuscar" runat="server" CssClass="btn btn-info t14" OnClientClick="abreWinEmi()"><i class="fa fa-search"></i></asp:LinkButton>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label5" runat="server" Text="Calle:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblCalleEmiFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label30" runat="server" Text="Número Exterior:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblNoExtEmiFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label33" runat="server" Text="Número Interior:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblNoIntEmiFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label35" runat="server" Text="Colonia:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblColoniaEmiFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label37" runat="server" Text="Delegación/Municipio:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblDelMunEmiFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label39" runat="server" Text="Estado:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblEdoEmiFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label41" runat="server" Text="País:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblPaisEmiFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label43" runat="server" Text="Código Postal:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblCpEmiFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label45" runat="server" Text="Ciudad/Localidad:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblLocalidadEmiFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label47" runat="server" Text="Referencia:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblReferenciaEmiFac" runat="server"></asp:Label>
                        </div>

                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="vExpedicion">
                        <div class="col-lg-12 col-sm-12 text-left">
                            <asp:Label ID="Label28" runat="server" Text="Emisor: " CssClass="textoBold"></asp:Label>
                            <asp:Label ID="lblRFCEmiExFac" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblNombreEmiExFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label38" runat="server" Text="Calle:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblCalleEmiExFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label42" runat="server" Text="Número Exterior:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblNoExtEmiExFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label46" runat="server" Text="Número Interior:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblNoIntEmiExFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label49" runat="server" Text="Colonia:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblColoniaEmiExFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label53" runat="server" Text="Delegación/Municipio:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblDelMunEmiExFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label55" runat="server" Text="Estado:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblEdoEmiExFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label57" runat="server" Text="País:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblPaisEmiExFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label59" runat="server" Text="Código Postal:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblCpEmiExFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label61" runat="server" Text="Ciudad/Localidad:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblLocalidadEmiExFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label63" runat="server" Text="Referencia:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblReferenciaEmiExFac" runat="server"></asp:Label>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="vReceptor">
                        <div class="col-lg-12 col-sm-12 text-left">
                            <asp:Label ID="Label36" runat="server" Text="Receptor: " CssClass="textoBold"></asp:Label>
                            <asp:Label ID="lblReceptorFactura" runat="server" Visible="false" />
                            <asp:Label ID="lblRfcReceptor" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblNombreReceptor" runat="server"></asp:Label>
                            <asp:LinkButton ID="lnkBuscaRec" runat="server" CssClass="btn btn-info t14" OnClientClick="abreWinRec()"><i class="fa fa-search"></i></asp:LinkButton>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label64" runat="server" Text="Calle:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblCalleRecFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label68" runat="server" Text="Número Exterior:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblNoExtRecFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label72" runat="server" Text="Número Interior:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblNoIntRecFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label76" runat="server" Text="Colonia:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblColoniaRecFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label78" runat="server" Text="Delegación/Municipio:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblDelMunRecFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label80" runat="server" Text="Estado:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblEdoRecFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label82" runat="server" Text="País:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblPaisRecFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label84" runat="server" Text="Código Postal:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblCpRecFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label86" runat="server" Text="Ciudad/Localidad:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblLocalidadRecFac" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label88" runat="server" Text="Referencia:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="lblReferenciaRecFac" runat="server"></asp:Label>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="vDetalles">
                        <div class="col-lg-2 col-sm-2 text-left">
                            <asp:Label ID="Label44" runat="server" Text="Forma de Pago: " CssClass="textoBold"></asp:Label>
                            <asp:Label ID="Label56" runat="server" Text="*" CssClass="errores"></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-left">
                            <asp:TextBox ID="txtFormaPago" runat="server" MaxLength="30" CssClass="input-large"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="txtFormaPago_TextBoxWatermarkExtender" runat="server" BehaviorID="txtFormaPago_TextBoxWatermarkExtender" TargetControlID="txtFormaPago" WatermarkCssClass="input-large water" WatermarkText="Forma de Pago" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe indicar la Forma de Pago" Text="*" CssClass="errores" ControlToValidate="txtFormaPago" ValidationGroup="factura"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-lg-2 col-sm-2 text-left">
                            <asp:Label ID="Label60" runat="server" Text="Condiciones de Pago: " CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-left">
                            <asp:TextBox ID="txtCondicionesPago" runat="server" MaxLength="30" CssClass="input-large"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" BehaviorID="txtCondicionesPago_TextBoxWatermarkExtender" TargetControlID="txtCondicionesPago" WatermarkCssClass="input-large water" WatermarkText="Condiciones de Pago" />
                        </div>
                        <div class="col-lg-2 col-sm-2 text-left">
                            <asp:Label ID="Label66" runat="server" Text="Método de Pago: " CssClass="textoBold"></asp:Label>
                            <asp:Label ID="Label70" runat="server" Text="*" CssClass="errores"></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-left">
                            <asp:TextBox ID="txtMetodoPago" runat="server" MaxLength="30" CssClass="input-large"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" BehaviorID="txtMetodoPago_TextBoxWatermarkExtender" TargetControlID="txtMetodoPago" WatermarkCssClass="input-large water" WatermarkText="Método de Pago" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe indicar el Método de Pago" Text="*" CssClass="errores" ControlToValidate="txtMetodoPago" ValidationGroup="factura"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-lg-2 col-sm-2 text-left">
                            <asp:Label ID="Label74" runat="server" Text="Régimen: " CssClass="textoBold"></asp:Label>
                            <asp:Label ID="Label77" runat="server" Text="*" CssClass="errores"></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-left">
                            <asp:TextBox ID="txtRegimenFac" runat="server" MaxLength="128" CssClass="input-large"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" BehaviorID="txtRegimenFac_TextBoxWatermarkExtender" TargetControlID="txtRegimenFac" WatermarkCssClass="input-large water" WatermarkText="Régimen" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Debe indicar el Régimen" Text="*" CssClass="errores" ControlToValidate="txtRegimenFac" ValidationGroup="factura"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-lg-2 col-sm-2 text-left">
                            <asp:Label ID="Label79" runat="server" Text="Cuenta de Pago: " CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-left">
                            <asp:TextBox ID="txtCtaPago" runat="server" MaxLength="30" CssClass="input-large"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" BehaviorID="txtCtaPago_TextBoxWatermarkExtender" TargetControlID="txtCtaPago" WatermarkCssClass="input-large water" WatermarkText="Cuenta de Pago" />
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="vAdicional">
                        <div class="col-lg-12 col-sm-12 text-left">
                            <asp:Label ID="Label81" runat="server" Text="Moneda: " CssClass="textoBold"></asp:Label>
                            <asp:Label ID="lblIdMonedaFac" runat="server" Visible="false" />
                            <asp:Label ID="lblDescripcionMoneda" runat="server"></asp:Label>
                            <asp:LinkButton ID="lnkBuscaMonedas" runat="server" CssClass="btn btn-info t14"><i class="fa fa-search"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label83" runat="server" Text="Abreviatura: " CssClass="textoBold"></asp:Label>
                            <asp:Label ID="lblAbreviatura" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label89" runat="server" Text="Tipo de Cambio:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <telerik:RadNumericTextBox RenderMode="Lightweight" ShowSpinButtons="true" IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="txtTipoCambio" MinValue="1" MaxValue="100" CssClass="input-medium">
                                <NumberFormat DecimalDigits="2" DecimalSeparator="." GroupSeparator="," GroupSizes="3" AllowRounding="false" />
                            </telerik:RadNumericTextBox>

                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label91" runat="server" Text="Nota:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <telerik:RadTextBox RenderMode="Lightweight" CssClass="textNota" ID="txtNotaFac" runat="server" TextMode="MultiLine" Resize="Both" EmptyMessage="Nota"></telerik:RadTextBox>
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label93" runat="server" Text="Referencia Externa/Documento de Referencia:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtReferenciasFac" runat="server" MaxLength="30" CssClass="input-large"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="txtReferenciasFac_TextBoxWatermarkExtender"
                                runat="server" BehaviorID="txtReferenciasFac_TextBoxWatermarkExtender"
                                TargetControlID="txtReferenciasFac" WatermarkText="Referencia Externa/Documento de Referencia" WatermarkCssClass="input-large water" />
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label95" runat="server" Text="Folio de Impresión:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:TextBox ID="txtFolioImp" runat="server" MaxLength="30" CssClass="input-medium"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5"
                                runat="server" BehaviorID="txtFolioImp_TextBoxWatermarkExtender"
                                TargetControlID="txtFolioImp" WatermarkText="Folio de Impresión" WatermarkCssClass="input-medium water" />
                        </div>
                        <div class="col-lg-3 col-sm-3 text-left">
                            <asp:Label ID="Label107" runat="server" Text="Tipo Facturación:" CssClass="textoBold"></asp:Label>
                        </div>
                        <div class="col-lg-9 col-sm-9 text-left">
                            <asp:DropDownList ID="ddlTipoFactura" runat="server" CssClass="input-medium">
                                <asp:ListItem Selected="True" Value="GL" Text="GLOBAL"></asp:ListItem>
                                <asp:ListItem Value="MO" Text="MANO OBRA"></asp:ListItem>
                                <asp:ListItem Value="RE" Text="REFACCIONES"></asp:ListItem>
                                <asp:ListItem Value="SO" Text="SIN ORDEN"></asp:ListItem>
                                <asp:ListItem Value="PV" Text="PUNTO VENTA"></asp:ListItem>
                                <asp:ListItem Value="PA" Text="PARTICULAR"></asp:ListItem>
                                <asp:ListItem Value="NC" Text="NOTA DE CREDITO"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
                <asp:Panel runat="server" ID="pnlConceptos" CssClass="marTop9px col-lg-12 col-sm-12">
                    <div>
                        <asp:Panel ID="pnlDocumento" runat="server" HorizontalAlign="Center" ScrollBars="Auto" BorderStyle="Solid">
                            <telerik:RadGrid ID="grdDocu" AutoGenerateColumns="False" runat="server" RenderMode="Lightweight" OnPreRender="grdDocu_PreRender" OnItemCommand="grdDocu_ItemCommand" OnItemCreated="grdDocu_ItemCreated" Culture="es-Mx" OnNeedDataSource="grdDocu_NeedDataSource" GroupPanelPosition="Top" OnItemDataBound="grdDocu_ItemDataBound" >
                                <MasterTableView CommandItemDisplay="Bottom" AllowAutomaticInserts="false" CommandItemSettings-ShowAddNewRecordButton="True" SkinID="Metro" DataKeyNames="IdFila">
                                    <CommandItemSettings ShowRefreshButton="false" ShowSaveChangesButton="true" ShowCancelChangesButton="false" AddNewRecordText="Agregar Concepto" SaveChangesText="Guardar Documento" />                                    
                                    <Columns>                                        
                                        <telerik:GridTemplateColumn HeaderText="Concepto" UniqueName="Concepto">
                                            <ItemTemplate>
                                                <table style="text-align: center;">
                                                    <tr>
                                                        <td>ID:
                                                            <asp:TextBox ID="txtIdent" runat="server" CssClass="input-small dan" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Concepto:<br />
                                                            <asp:TextBox runat="server" ID="txtConcepto" TextMode="MultiLine" Columns="15" Rows="5" CssClass="textNota" /></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="Importe" HeaderText="Importe">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="text-left">Cantidad:</td>
                                                        <td class="text-right">
                                                            <telerik:RadNumericTextBox ID="radnumCantidad" runat="server" Font-Size="Small" MaxValue="10000" MinValue="1" Value="1" CssClass="input-mini" Width="80" NumberFormat-AllowRounding="False" ShowButton="False" AutoPostBack="true" ShowSpinButtons="True" OnTextChanged="radnumCantidad_TextChanged"></telerik:RadNumericTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-left">Unidad:</td>
                                                        <td class="text-right">
                                                            <asp:DropDownList ID="ddlUnidad" runat="server" DataSourceID="SqlDsUnidad" DataTextField="UnidDesc" DataValueField="IdUnid" CssClass="input-small"></asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-left">Valor Unitario:</td>
                                                        <td class="text-right">
                                                            <asp:TextBox ID="txtValUnit" runat="server" Columns="2" Text="0.00" AutoPostBack="true" OnTextChanged="txtValUnit_TextChanged"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="filtxtValUnit" runat="server" TargetControlID="txtValUnit" FilterType="Custom, Numbers" ValidChars="." />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-left">Importe:</td>
                                                        <td class="text-right">
                                                            <asp:Label ID="lblImporte" runat="server" Text="0.00"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="SubTotal" HeaderText="SubTotal">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="text-left">% Descuento:</td>
                                                        <td class="text-right">
                                                            <asp:TextBox ID="txtPtjeDscto" runat="server" Text="0" Columns="1" MaxLength="5" AutoPostBack="true" OnTextChanged="txtPtjeDscto_TextChanged"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-left">Descuento: </td>
                                                        <td class="text-right">
                                                            <asp:TextBox ID="txtDscto" runat="server" Text="0.00" Columns="2" AutoPostBack="true" OnTextChanged="txtDscto_TextChanged"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="filtxtPtheDscto" runat="server" TargetControlID="txtPtjeDscto" FilterType="Custom, Numbers" ValidChars="." />
                                                            <cc1:FilteredTextBoxExtender ID="filtxtDscto" runat="server" TargetControlID="txtDscto" FilterType="Custom, Numbers" ValidChars="." />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-left">Dto. Global: </td>
                                                        <td class="text-right">
                                                            <asp:Label ID="lblDtoGlobalConcepto" runat="server" Text="0.00"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-left">SubTotal:</td>
                                                        <td class="text-right">
                                                            <asp:Label ID="lblSubTotal" runat="server" Text="0.00"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="Imp_Tras" HeaderText="Impuestos Trasladados">
                                            <ItemTemplate>
                                                <table class="text-left">
                                                    <tr>
                                                        <td class="text-left">
                                                            <telerik:RadDropDownList runat="server" ID="ddlIvaTras" DataSourceID="SqlDSIva" DataTextField="TrasDescrip" DataValueField="Id_Tras" Width="100" DefaultMessage="Elija IVA" OnClientItemSelected="" AutoPostBack="true" OnSelectedIndexChanged="ddlIvaTras_SelectedIndexChanged"></telerik:RadDropDownList>
                                                        </td>
                                                        <td class="text-right">
                                                            <asp:Label runat="server" ID="lblIvaTras" Text="0.00"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-left">
                                                            <telerik:RadDropDownList ID="ddlIeps" runat="server" DataSourceID="SqlDSIeps" DataTextField="TrasDescrip" DataValueField="Id_Tras" DefaultMessage="Elija IEPS" Width="100" OnClientItemSelected="" AutoPostBack="true" OnSelectedIndexChanged="ddlIeps_SelectedIndexChanged"></telerik:RadDropDownList>
                                                        </td>
                                                        <td class="text-right">
                                                            <asp:Label runat="server" ID="lblIeps" Text="0.00"></asp:Label></td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="Imp_Ret" HeaderText="Impuestos Retenidos">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="text-left">
                                                            <telerik:RadDropDownList runat="server" ID="ddlIvaRet" DataSourceID="SqlDsIvaRet" DataTextField="RetDescrip" DataValueField="Id_Ret" Width="100" AutoPostBack="true"
                                                                DefaultMessage="Elija IVA" OnClientItemSelected="" OnSelectedIndexChanged="ddlIvaRet_SelectedIndexChanged">
                                                            </telerik:RadDropDownList>
                                                        </td>
                                                        <td class="text-right">
                                                            <asp:Label runat="server" ID="lblIvaRet" Text="0.00"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-left">
                                                            <telerik:RadDropDownList ID="ddlIsrRet" runat="server" DataSourceID="SqlDsIsrRet" DataTextField="RetDescrip" DataValueField="Id_Ret" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlIsrRet_SelectedIndexChanged" DefaultMessage="Elija ISR" Width="100">
                                                            </telerik:RadDropDownList>
                                                        </td>
                                                        <td class="text-right">
                                                            <asp:Label runat="server" ID="lblIsrRet" Text="0.00"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="Total" HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalCpto" runat="server" Text="0"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn  HeaderText="Elimina">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkElimina" runat="server" CssClass="btn btn-danger" OnClick="lnkElimina_Click" OnClientClick="return confirm('¿Está seguro de eliminar el concepto de la factura?')" CommandArgument='<%# Eval("idFila") %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings AllowKeyboardNavigation="false" Selecting-AllowRowSelect="true" Scrolling-AllowScroll="true" Scrolling-SaveScrollPosition="true" Scrolling-UseStaticHeaders="true"></ClientSettings>
                                <HeaderContextMenu RenderMode="Lightweight"></HeaderContextMenu>
                            </telerik:RadGrid>

                            <asp:SqlDataSource runat="server" ID="SqlDsUnidad" ConnectionString='<%$ ConnectionStrings:eFactura %>' SelectCommand="SELECT [IdUnid], [UnidDesc] FROM [Unidades]"></asp:SqlDataSource>
                            <asp:SqlDataSource runat="server" ID="SqlDSIeps" ConnectionString='<%$ ConnectionStrings:eFactura %>' SelectCommand="SELECT [Id_Tras], [TrasDescrip] FROM [ImpTrasladado] WHERE ([TrasNombre] = @TrasNombre)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="IEPS" Name="TrasNombre" Type="String"></asp:Parameter>
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource runat="server" ID="SqlDSIva" ConnectionString='<%$ ConnectionStrings:eFactura %>' SelectCommand="SELECT Id_Tras, TrasDescrip FROM [ImpTrasladado] WHERE ([TrasNombre] = @TrasNombre)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="IVA" Name="TrasNombre" Type="String"></asp:Parameter>
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource runat="server" ID="SqlDsIvaRet" ConnectionString='<%$ ConnectionStrings:eFactura %>' SelectCommand="SELECT Id_Ret, RetDescrip FROM [ImpRetenidos] WHERE (RetNombre LIKE @RetNombre)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="IVA%" Name="RetNombre" Type="String"></asp:Parameter>
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource runat="server" ID="SqlDsIsrRet" ConnectionString='<%$ ConnectionStrings:eFactura %>' SelectCommand="SELECT Id_Ret, RetDescrip FROM [ImpRetenidos] WHERE (RetNombre LIKE @RetNombre)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="ISR%" Name="RetNombre" Type="String"></asp:Parameter>
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </asp:Panel>
                        <div>
                            <asp:Label runat="server" ID="lblMnsjs"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
            </asp:Panel>
            <asp:FormView ID="fvwResumen" runat="server" DataKeyNames="IdCfd" DefaultMode="Edit" DataSourceID="SqlDSEncDet" EmptyDataText="Sin Datos" RowStyle-HorizontalAlign="Center" HorizontalAlign="Center" CssClass="pad1m">
                <EmptyDataTemplate>
                    <table class="pad1m">
                        <tr>
                            <td>Subtotal Bruto:</td>
                            <td>
                                <asp:Label ID="lblSubTotBru" runat="server" Text="0.00"></asp:Label></td>
                            <td style="width: 100px;">&nbsp;</td>
                            <td>Impuesto Trasladado:</td>
                            <td>
                                <asp:Label ID="lblImpTras" runat="server" Text="0.00"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMODesGlob" runat="server" Text="Descuento Mano de Obra:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtMODesGlob" OnTextChanged="txtMODesGlob_TextChanged" AutoPostBack="true" CssClass="input-small" runat="server" Text="0.00" Columns="1"></asp:TextBox></td>
                            <td style="width: 100px;">&nbsp;</td>
                            <td>Subtotal despues de traslados:</td>
                            <td>
                                <asp:Label ID="lblSubTotTras" runat="server" Text="0.00"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRefDesGlob" runat="server" Text="Descuento Refacciones:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtRefDesGlob" OnTextChanged="txtRefDesGlob_TextChanged" AutoPostBack="true" CssClass="input-small" runat="server" Text="0.00" Columns="1"></asp:TextBox></td>
                            <td style="width: 100px;">&nbsp;</td>
                            <td>Impuesto Retenido:</td>
                            <td>
                                <asp:Label ID="lblImpRet" runat="server" Text="0.00"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Descuento Global:</td>
                            <td>
                                <asp:TextBox ID="txtPctjeDsctoGlb" runat="server" Text="0.00" Columns="1" OnTextChanged="txtPctjeDsctoGlb_TextChanged" AutoPostBack="true" CssClass="input-small"></asp:TextBox>%&nbsp;
                                        <asp:Label ID="lblDsctoGlb" runat="server" Text="0.00"></asp:Label>
                                <cc1:FilteredTextBoxExtender ID="filtxtPctjeDsctoGlb" runat="server" TargetControlID="txtPctjeDsctoGlb" FilterType="Custom, Numbers" ValidChars="." />
                            </td>
                            <td style="width: 100px;">&nbsp;</td>
                            <td>Subtotal despu&eacute;s de retenciones:</td>
                            <td>
                                <asp:Label ID="lblSubTotRet" runat="server" Text="0.00"></asp:Label></td>
                            
                        </tr>
                        <tr>
                            <td>Descuento:</td>
                            <td>
                                <asp:Label ID="lblTotDscto" runat="server" Text="0.00"></asp:Label></td>
                            <td style="width: 100px;">&nbsp;</td>
                            <td>Traslados y retenciones adicionales:</td>
                            <td>
                                <asp:Label ID="lblTrasRetAd" runat="server" Text="0.00"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Subtotal Neto:</td>
                            <td>
                                <asp:Label ID="lblSubTotNeto" runat="server" Text="0.00"></asp:Label></td>
                            <td style="width: 100px;">&nbsp;</td>
                            <td>Total:</td>
                            <td>
                                <asp:Label ID="lblTotalGral" runat="server" Text="0.00"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblMotDscto" Text="Motivo del Descuento:" ></asp:Label></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtMotivoDscto" Columns="10" ></asp:TextBox></td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <table class="pad1m">
                        <tr>
                            <td>Subtotal Bruto:</td>
                            <td>
                                <asp:Label ID="lblSubTotBru" runat="server" Text='<%# string.IsNullOrEmpty(Eval("EncSubTotal").ToString()) ? "0.00" : Eval("EncSubTotal") %>'></asp:Label>
                            </td>
                            <td style="width: 100px;">&nbsp;</td>
                            <td>Impuesto Trasladado:</td>
                            <td>
                                <asp:Label ID="lblImpTras" runat="server" Text='<%# string.IsNullOrEmpty(Eval("EncImpTras").ToString()) ? "0.00" : Eval("EncImpTras") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Descuento Global:</td>
                            <td>
                                <asp:TextBox ID="txtPctjeDsctoGlb" runat="server" Text='<%# string.IsNullOrEmpty(Eval("EncDescGlob").ToString()) ? "0.00" : Eval("EncDescGlob") %>' Columns="1" CssClass="input-small" OnTextChanged="txtPctjeDsctoGlb_TextChanged" AutoPostBack="true"></asp:TextBox>
                                %&nbsp;
                                        <asp:Label ID="lblDsctoGlb" runat="server" Text='<%# string.IsNullOrEmpty(Eval("EncDescGlobImp").ToString()) ? "0.00" : Eval("EncDescGlobImp") %>'></asp:Label></td>
                            <cc1:FilteredTextBoxExtender ID="filtxtPctjeDsctoGlb" runat="server" TargetControlID="txtPctjeDsctoGlb" FilterType="Custom, Numbers" ValidChars="." />
                            <td style="width: 100px;">&nbsp;</td>
                            <td>Subtotal despues de traslados:</td>
                            <td>
                                <asp:Label ID="lblSubTotTras" runat="server" Text="0.00"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Descuento:</td>
                            <td>
                                <asp:Label ID="lblTotDscto" runat="server" Text='<%# string.IsNullOrEmpty(Eval("EncDesc").ToString()) ? "0.00" : Eval("EncDesc") %>'></asp:Label></td>
                            <td style="width: 100px;">&nbsp;</td>
                            <td>Impuesto Retenido:</td>
                            <td>
                                <asp:Label ID="lblImpRet" runat="server" Text='<%# string.IsNullOrEmpty(Eval("EncImpRet").ToString()) ? "0.00" : Eval("EncImpRet") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Subtotal Neto:</td>
                            <td>
                                <asp:Label ID="lblSubTotNeto" runat="server" Text="0.00"></asp:Label></td>
                            <td style="width: 100px;">&nbsp;</td>
                            <td>Subtotal despu&eacute;s de retenciones:</td>
                            <td>
                                <asp:Label ID="lblSubTotRet" runat="server" Text="0.00"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblMotDscto" Text="Motivo del Descuento:" Visible="false"></asp:Label></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtMotivoDscto" Text='<%# Eval("EncMotivoDescuento") %>' Columns="10" Visible="false"></asp:TextBox></td>
                            <td style="width: 100px;">&nbsp;</td>
                            <td>Traslados y retenciones adicionales:
                            </td>
                            <td>
                                <asp:Label ID="lblTrasRetAd" runat="server" Text="0.00"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="width: 100px;">&nbsp;</td>
                            <td>Total:
                            </td>
                            <td>
                                <asp:Label ID="lblTotalGral" runat="server" Text='<%# string.IsNullOrEmpty(Eval("EncTotal").ToString()) ? "0.00" : Eval("EncTotal") %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Editar" />
                    &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Eliminar" />
                    &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="Nuevo" />
                </ItemTemplate>
            </asp:FormView>
            

            <asp:SqlDataSource ID="SqlDSEncDet" runat="server" ConnectionString="<%$ ConnectionStrings:eFactura %>"
                DeleteCommand="DELETE FROM [EncCFD] WHERE [IdCfd] = @IdCfd"
                InsertCommand="INSERT INTO [EncCFD] ([IdTipoDoc], [IdEmisor], [IdRecep], [IdMoneda], [EncFolioUUID], [EncFecha], [EncHora], [EncImpRet], [EncFormaPago], [EncCondicionesPago], [EncMetodoPago], [EncDescGlob], [EncDescGlobImp], [EncSubTotal], [EncDesc], [EncImpTras], [EncTotal], [EncMotivoDescuento], [EncEstatus], [EncEmNombre], [EncEmCalle], [EncEmNoExt], [EncEmNoInt], [EncEmPais], [EncEmEstado], [EncEmDelMun], [EncEmColonia], [EncEmLocalidad], [EncEmReferenc], [EncEmCP], [EncEmExReferenc], [EncEmExCP], [EncReNombre], [EncReCalle], [EncReNoExt], [EncReNoInt], [EncRePais], [EncReEstado], [EncReDelMun], [EncReColonia], [EncReLocalidad], [EncReCP], [EncReReferenc], [EncReRFC], [EncEmRFC], [EndIdBilling], [EncReferencia], [IdCfd_]) VALUES (@IdTipoDoc, @IdEmisor, @IdRecep, @IdMoneda, @EncFolioUUID, @EncFecha, @EncHora, @EncImpRet, @EncFormaPago, @EncCondicionesPago, @EncMetodoPago, @EncDescGlob, @EncDescGlobImp, @EncSubTotal, @EncDesc, @EncImpTras, @EncTotal, @EncMotivoDescuento, @EncEstatus, @EncEmNombre, @EncEmCalle, @EncEmNoExt, @EncEmNoInt, @EncEmPais, @EncEmEstado, @EncEmDelMun, @EncEmColonia, @EncEmLocalidad, @EncEmReferenc, @EncEmCP, @EncEmExReferenc, @EncEmExCP, @EncReNombre, @EncReCalle, @EncReNoExt, @EncReNoInt, @EncRePais, @EncReEstado, @EncReDelMun, @EncReColonia, @EncReLocalidad, @EncReCP, @EncReReferenc, @EncReRFC, @EncEmRFC, @EndIdBilling, @EncReferencia, @IdCfd_)"
                SelectCommand="SELECT [IdCfd], [IdTipoDoc], [IdEmisor], [IdRecep], [IdMoneda], [IdMatrizRecep], [EncFolioUUID], [EncFecha], [EncHora], [EncImpRet], [EncFormaPago], [EncCondicionesPago], [EncMetodoPago], [EncDescGlob], [EncDescGlobImp], [EncSubTotal], [EncDesc], [EncImpTras], [EncTotal], [EncMotivoDescuento], [EncEstatus], [EncEmNombre], [EncEmCalle], [EncEmNoExt], [EncEmNoInt], [EncEmPais], [EncEmEstado], [EncEmDelMun], [EncEmColonia], [EncEmLocalidad], [EncEmReferenc], [EncEmCP], [EncEmExReferenc], [EncEmExCP], [EncReNombre], [EncReCalle], [EncReNoExt], [EncReNoInt], [EncRePais], [EncReEstado], [EncReDelMun], [EncReColonia], [EncReLocalidad], [EncReCP], [EncReReferenc], [EncReRFC], [EncEmRFC], [EndIdBilling], [EncReferencia], [IdCfd_] FROM [EncCFD] WHERE (([IdCfd] = @IdCfd) AND ([IdTipoDoc] = @IdTipoDoc) AND ([IdEmisor] = @IdEmisor) AND ([IdRecep] = @IdRecep))"
                UpdateCommand="UPDATE [EncCFD] SET [IdTipoDoc] = @IdTipoDoc, [IdEmisor] = @IdEmisor, [IdRecep] = @IdRecep, [IdMoneda] = @IdMoneda, [IdMatrizRecep] = @IdMatrizRecep, [EncFolioUUID] = @EncFolioUUID, [EncFecha] = @EncFecha, [EncHora] = @EncHora, [EncImpRet] = @EncImpRet, [EncFormaPago] = @EncFormaPago, [EncCondicionesPago] = @EncCondicionesPago, [EncMetodoPago] = @EncMetodoPago, [EncDescGlob] = @EncDescGlob, [EncDescGlobImp] = @EncDescGlobImp, [EncSubTotal] = @EncSubTotal, [EncDesc] = @EncDesc, [EncImpTras] = @EncImpTras, [EncTotal] = @EncTotal, [EncMotivoDescuento] = @EncMotivoDescuento, [EncEstatus] = @EncEstatus, [EncEmNombre] = @EncEmNombre, [EncEmCalle] = @EncEmCalle, [EncEmNoExt] = @EncEmNoExt, [EncEmNoInt] = @EncEmNoInt, [EncEmPais] = @EncEmPais, [EncEmEstado] = @EncEmEstado, [EncEmDelMun] = @EncEmDelMun, [EncEmColonia] = @EncEmColonia, [EncEmLocalidad] = @EncEmLocalidad, [EncEmReferenc] = @EncEmReferenc, [EncEmCP] = @EncEmCP, [EncEmExReferenc] = @EncEmExReferenc, [EncEmExCP] = @EncEmExCP, [EncReNombre] = @EncReNombre, [EncReCalle] = @EncReCalle, [EncReNoExt] = @EncReNoExt, [EncReNoInt] = @EncReNoInt, [EncRePais] = @EncRePais, [EncReEstado] = @EncReEstado, [EncReDelMun] = @EncReDelMun, [EncReColonia] = @EncReColonia, [EncReLocalidad] = @EncReLocalidad, [EncReCP] = @EncReCP, [EncReReferenc] = @EncReReferenc, [EncReRFC] = @EncReRFC, [EncEmRFC] = @EncEmRFC, [EndIdBilling] = @EndIdBilling, [EncReferencia] = @EncReferencia, [IdCfd_] = @IdCfd_ WHERE [IdCfd] = @IdCfd">
                <DeleteParameters>
                    <asp:Parameter Name="IdCfd" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="IdTipoDoc" Type="Int16" DefaultValue="1" />
                    <asp:QueryStringParameter Name="IdEmisor" QueryStringField="em" Type="Int32" />
                    <asp:Parameter Name="IdRecep" Type="Int32" />
                    <asp:Parameter Name="IdMoneda" Type="Int32" DefaultValue="1" />
                    <asp:Parameter Name="EncReRFC" Type="String" />
                    <asp:Parameter Name="EncEmRFC" Type="String" />
                    <asp:Parameter Name="EncFolioUUID" Type="String" />
                    <asp:Parameter DbType="Date" Name="EncFecha" />
                    <asp:Parameter Name="EncHora" Type="String" />
                    <asp:Parameter Name="EncImpRet" Type="Double" />
                    <asp:Parameter Name="EncFormaPago" Type="String" />
                    <asp:Parameter Name="EncCondicionesPago" Type="String" />
                    <asp:Parameter Name="EncMetodoPago" Type="String" />
                    <asp:Parameter Name="EncDescGlob" Type="Single" />
                    <asp:Parameter Name="EncDescGlobImp" Type="Double" />
                    <asp:Parameter Name="EncSubTotal" Type="Double" />
                    <asp:Parameter Name="EncDesc" Type="Double" />
                    <asp:Parameter Name="EncImpTras" Type="Double" />
                    <asp:Parameter Name="EncTotal" Type="Double" />
                    <asp:Parameter Name="EncMotivoDescuento" Type="String" />
                    <asp:Parameter Name="EncEstatus" Type="String" />
                    <asp:Parameter Name="EncEmNombre" Type="String" />
                    <asp:Parameter Name="EncEmCalle" Type="String" />
                    <asp:Parameter Name="EncEmNoExt" Type="String" />
                    <asp:Parameter Name="EncEmNoInt" Type="String" />
                    <asp:Parameter Name="EncEmPais" Type="String" />
                    <asp:Parameter Name="EncEmEstado" Type="String" />
                    <asp:Parameter Name="EncEmDelMun" Type="String" />
                    <asp:Parameter Name="EncEmColonia" Type="String" />
                    <asp:Parameter Name="EncEmLocalidad" Type="String" />
                    <asp:Parameter Name="EncEmReferenc" Type="String" />
                    <asp:Parameter Name="EncEmCP" Type="Int32" />
                    <asp:Parameter Name="EncEmExReferenc" Type="String" />
                    <asp:Parameter Name="EncEmExCP" Type="Int32" />
                    <asp:Parameter Name="EncReNombre" Type="String" />
                    <asp:Parameter Name="EncReCalle" Type="String" />
                    <asp:Parameter Name="EncReNoExt" Type="String" />
                    <asp:Parameter Name="EncReNoInt" Type="String" />
                    <asp:Parameter Name="EncRePais" Type="String" />
                    <asp:Parameter Name="EncReEstado" Type="String" />
                    <asp:Parameter Name="EncReDelMun" Type="String" />
                    <asp:Parameter Name="EncReColonia" Type="String" />
                    <asp:Parameter Name="EncReLocalidad" Type="String" />
                    <asp:Parameter Name="EncReCP" Type="Int32" />
                    <asp:Parameter Name="EncReReferenc" Type="String" />
                    <asp:Parameter Name="EndIdBilling" Type="Double" />
                    <asp:Parameter Name="EncReferencia" Type="String" />
                    <asp:Parameter Name="IdCfd_" Type="Int32" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="IdCfd" Type="Int32" />
                    <asp:Parameter Name="IdTipoDoc" Type="Int32" DefaultValue="1" />
                    <asp:QueryStringParameter Name="IdEmisor" QueryStringField="em" Type="Int32" />
                    <asp:Parameter Name="IdRecep" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="IdTipoDoc" Type="Int32" />
                    <asp:Parameter Name="IdEmisor" Type="Int32" />
                    <asp:Parameter Name="IdRecep" Type="Int32" />
                    <asp:Parameter Name="IdMoneda" Type="Int32" />
                    <asp:Parameter Name="IdMatrizRecep" Type="Int32" />
                    <asp:Parameter Name="EncFolioUUID" Type="String" />
                    <asp:Parameter DbType="Date" Name="EncFecha" />
                    <asp:Parameter Name="EncHora" Type="String" />
                    <asp:Parameter Name="EncImpRet" Type="Double" />
                    <asp:Parameter Name="EncFormaPago" Type="String" />
                    <asp:Parameter Name="EncCondicionesPago" Type="String" />
                    <asp:Parameter Name="EncMetodoPago" Type="String" />
                    <asp:Parameter Name="EncDescGlob" Type="Single" />
                    <asp:Parameter Name="EncDescGlobImp" Type="Double" />
                    <asp:Parameter Name="EncSubTotal" Type="Double" />
                    <asp:Parameter Name="EncDesc" Type="Double" />
                    <asp:Parameter Name="EncImpTras" Type="Double" />
                    <asp:Parameter Name="EncTotal" Type="Double" />
                    <asp:Parameter Name="EncMotivoDescuento" Type="String" />
                    <asp:Parameter Name="EncEstatus" Type="String" />
                    <asp:Parameter Name="EncEmNombre" Type="String" />
                    <asp:Parameter Name="EncEmCalle" Type="String" />
                    <asp:Parameter Name="EncEmNoExt" Type="String" />
                    <asp:Parameter Name="EncEmNoInt" Type="String" />
                    <asp:Parameter Name="EncEmPais" Type="String" />
                    <asp:Parameter Name="EncEmEstado" Type="String" />
                    <asp:Parameter Name="EncEmDelMun" Type="String" />
                    <asp:Parameter Name="EncEmColonia" Type="String" />
                    <asp:Parameter Name="EncEmLocalidad" Type="String" />
                    <asp:Parameter Name="EncEmReferenc" Type="String" />
                    <asp:Parameter Name="EncEmCP" Type="Int32" />
                    <asp:Parameter Name="EncEmExReferenc" Type="String" />
                    <asp:Parameter Name="EncEmExCP" Type="Int32" />
                    <asp:Parameter Name="EncReNombre" Type="String" />
                    <asp:Parameter Name="EncReCalle" Type="String" />
                    <asp:Parameter Name="EncReNoExt" Type="String" />
                    <asp:Parameter Name="EncReNoInt" Type="String" />
                    <asp:Parameter Name="EncRePais" Type="String" />
                    <asp:Parameter Name="EncReEstado" Type="String" />
                    <asp:Parameter Name="EncReDelMun" Type="String" />
                    <asp:Parameter Name="EncReColonia" Type="String" />
                    <asp:Parameter Name="EncReLocalidad" Type="String" />
                    <asp:Parameter Name="EncReCP" Type="Int32" />
                    <asp:Parameter Name="EncReReferenc" Type="String" />
                    <asp:Parameter Name="EncReRFC" Type="String" />
                    <asp:Parameter Name="EncEmRFC" Type="String" />
                    <asp:Parameter Name="EndIdBilling" Type="Double" />
                    <asp:Parameter Name="EncReferencia" Type="String" />
                    <asp:Parameter Name="IdCfd_" Type="Int32" />
                    <asp:Parameter Name="IdCfd" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString='<%$ ConnectionStrings:eFactura %>'
                DeleteCommand="DELETE FROM [DetCFD] WHERE [IdDetCfd] = @IdDetCfd" InsertCommand="INSERT INTO [DetCFD] ([IdCfd], [IdConcepto], [IdUnid], [DetCantidad], [DetValorUnit], [IdTras1], [DetImpTras1], [IdTras2], [DetImpTras2], [IdRet1], [DetImpRet1], [IdRet2], [DetImpRet2], [DetPorcDesc], [DetImpDesc], [Subtotal], [Total], [IdEmisor], [DetDesc], [CoCuentaPredial], [IdTras3], [DetImpTras3]) VALUES (@IdCfd, @IdConcepto, @IdUnid, @DetCantidad, @DetValorUnit, @IdTras1, @DetImpTras1, @IdTras2, @DetImpTras2, @IdRet1, @DetImpRet1, @IdRet2, @DetImpRet2, @DetPorcDesc, @DetImpDesc, @Subtotal, @Total, @IdEmisor, @DetDesc, @CoCuentaPredial, @IdTras3, @DetImpTras3)" SelectCommand="SELECT [IdDetCfd], [IdCfd], [IdConcepto], [IdUnid], [DetCantidad], [DetValorUnit], [IdTras1], [DetImpTras1], [IdTras2], [DetImpTras2], [IdRet1], [DetImpRet1], [IdRet2], [DetImpRet2], [DetPorcDesc], [DetImpDesc], [Subtotal], [Total], [IdEmisor], [DetDesc], [CoCuentaPredial], [IdTras3], [DetImpTras3] FROM [DetCFD]" UpdateCommand="UPDATE [DetCFD] SET [IdCfd] = @IdCfd, [IdConcepto] = @IdConcepto, [IdUnid] = @IdUnid, [DetCantidad] = @DetCantidad, [DetValorUnit] = @DetValorUnit, [IdTras1] = @IdTras1, [DetImpTras1] = @DetImpTras1, [IdTras2] = @IdTras2, [DetImpTras2] = @DetImpTras2, [IdRet1] = @IdRet1, [DetImpRet1] = @DetImpRet1, [IdRet2] = @IdRet2, [DetImpRet2] = @DetImpRet2, [DetPorcDesc] = @DetPorcDesc, [DetImpDesc] = @DetImpDesc, [Subtotal] = @Subtotal, [Total] = @Total, [IdEmisor] = @IdEmisor, [DetDesc] = @DetDesc, [CoCuentaPredial] = @CoCuentaPredial, [IdTras3] = @IdTras3, [DetImpTras3] = @DetImpTras3 WHERE [IdDetCfd] = @IdDetCfd">
                <DeleteParameters>
                    <asp:Parameter Name="IdDetCfd" Type="Int32"></asp:Parameter>
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="IdCfd" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="IdConcepto" Type="String"></asp:Parameter>
                    <asp:Parameter Name="IdUnid" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="DetCantidad" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="DetValorUnit" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="IdTras1" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="DetImpTras1" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="IdTras2" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="DetImpTras2" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="IdRet1" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="DetImpRet1" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="IdRet2" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="DetImpRet2" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="DetPorcDesc" Type="Single"></asp:Parameter>
                    <asp:Parameter Name="DetImpDesc" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="Subtotal" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="Total" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="IdEmisor" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="DetDesc" Type="String"></asp:Parameter>
                    <asp:Parameter Name="CoCuentaPredial" Type="String"></asp:Parameter>
                    <asp:Parameter Name="IdTras3" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="DetImpTras3" Type="Double"></asp:Parameter>
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="IdCfd" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="IdConcepto" Type="String"></asp:Parameter>
                    <asp:Parameter Name="IdUnid" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="DetCantidad" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="DetValorUnit" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="IdTras1" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="DetImpTras1" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="IdTras2" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="DetImpTras2" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="IdRet1" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="DetImpRet1" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="IdRet2" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="DetImpRet2" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="DetPorcDesc" Type="Single"></asp:Parameter>
                    <asp:Parameter Name="DetImpDesc" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="Subtotal" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="Total" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="IdEmisor" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="DetDesc" Type="String"></asp:Parameter>
                    <asp:Parameter Name="CoCuentaPredial" Type="String"></asp:Parameter>
                    <asp:Parameter Name="IdTras3" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="DetImpTras3" Type="Double"></asp:Parameter>
                    <asp:Parameter Name="IdDetCfd" Type="Int32"></asp:Parameter>
                </UpdateParameters>
            </asp:SqlDataSource>

            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanelFact">
                <ProgressTemplate>
                    <asp:Panel ID="pnlMaskLoadgralf" runat="server" CssClass="maskLoad" />
                    <asp:Panel ID="pnlCargandografN" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoadgralf" runat="server" ImageUrl="~/img/loading.gif" CssClass="ancho100" />
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>


    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>

            <asp:Panel runat="server" ID="Panel1" CssClass="col-lg-12 col-ms-12 text-center pad1m">
                <div class="col-lg-6 col-ms-6 text-center">
                    <asp:LinkButton ID="lnkTimbrar" runat="server" CssClass="btn btn-primary t14"
                        OnClick="lnkTimbrar_Click"><i class="fa fa-rocket"></i><span>&nbsp;Timbrar Documento</span></asp:LinkButton>
                </div>
                <div class="col-lg-6 col-ms-6 text-center">
                    <asp:LinkButton ID="lnkImprimir" runat="server" CssClass="btn btn-info t14"
                        OnClick="lnkImprimir_Click"><i class="fa fa-print"></i><span>&nbsp;Imprimir Documento</span></asp:LinkButton>
                </div>
            </asp:Panel>


            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <asp:Panel ID="pnlMaskLoadgralBTN" runat="server" CssClass="maskLoad" />
                    <asp:Panel ID="pnlCargandogralBTN" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoadgralBTN" runat="server" ImageUrl="~/img/loading.gif" CssClass="ancho100" />
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="updPanelFacturacion" runat="server">
        <ContentTemplate>
            <div class="pie pad1m">
                <div class="clearfix">
                    <div class="row colorBlanco textoBold">
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label2" runat="server" Text="Tipo Orden:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlToOrden" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label4" runat="server" Text="Cliente:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlClienteOrden" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label6" runat="server" Text="Tipo Servicio:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlTsOrden" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row colorBlanco textoBold">
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label8" runat="server" Text="Tipo Valuación:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlValOrden" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label10" runat="server" Text="Tipo Asegurado:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlTaOrden" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label12" runat="server" Text="Localización:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlLocOrden" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row colorBlanco textoBold">
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label108" runat="server" Text="Perfil:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="ddlPerfil" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label109" runat="server" Text="Siniestro:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblSiniestro" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label110" runat="server" Text="Deducible:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblDeducible" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row colorBlanco textoBold">
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label111" runat="server" Text="Total Orden:" CssClass="colorEtiqueta"
                                Visible="false"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblTotOrden" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="Label112" runat="server" Text="Promesa:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblEntregaEstimada" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-4 col-sm-4 text-center">
                            <asp:Label ID="lblPorcDeduEti" runat="server" Text="% Deducible:" CssClass="colorEtiqueta"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblPorcDedu" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <asp:UpdateProgress ID="UpdateProgressFac" runat="server" AssociatedUpdatePanelID="updPanelFacturacion">
                <ProgressTemplate>
                    <asp:Panel ID="pnlMaskLoad21" runat="server" CssClass="maskLoad" />
                    <asp:Panel ID="pnlCargando21" runat="server" CssClass="pnlPopUpLoad">
                        <asp:Image ID="imgLoad21" runat="server" ImageUrl="~/img/loading.gif" CssClass="ancho100" />
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
