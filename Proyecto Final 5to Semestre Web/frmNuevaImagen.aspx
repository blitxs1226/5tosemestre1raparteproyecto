<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="frmNuevaImagen.aspx.cs" Inherits="Proyecto_Final_5to_Semestre_Web.frmNuevaImagen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .w-200 {
            max-width: 200px;
        }

        .estilo-cap {
            font-size: 1.5rem;
            font-style: italic;
            font-weight: bold;
            -webkit-transform: skew(15deg, 15deg);
            -moz-transform: skew(15deg, 15deg);
            -ms-transform: skew(15deg, 15deg);
            transform: skew(15deg, 15deg);
        }

    </style>
    <div class="sc">
        <h1>Subir Imagenes</h1>

        <div class="row">
            <div class="col-lg-12">
                <table class="tableCont tablafoto" runat="server" id="ingreso">
                    <tr>
                        <td colspan="2">
                            <asp:Image ID="Image2" CssClass="fotoimg w-200" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="flpDragAndDrop" runat="server" CssClass="btn btn-success" onchange="readURL(this)" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCaptcha" Enabled="false" CssClass="estilo-cap" runat="server"></asp:Label></td>

                        <td>
                            <asp:Image ID="imgCaptchaNoCon" ImageUrl="~/Imagenes/Captcha/2.jpg" CssClass="w-100 w-200" runat="server" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lbltituloCapcha" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCapchaCon" CssClass="form-control" runat="server"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtCapchaNoCon" CssClass="form-control" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnsubir" runat="server" CssClass="btn btn-info" OnClick="btnsubir_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-lg-12">
                <asp:GridView ID="gvhistorial" CssClass="table table-responsive" runat="server" AutoGenerateColumns="false" OnRowCommand="gvhistorial_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ID" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none" />
                        <asp:BoundField DataField="Registrado Por" HeaderText="Registrado Por" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                        <asp:BoundField DataField="NIT" HeaderText="NIT" />
                        <asp:BoundField DataField="Fecha1" HeaderText="Fecha 1" />
                        <asp:BoundField DataField="Articulo 1" HeaderText="Artículo 1" />
                        <asp:BoundField DataField="Cantidad 1" HeaderText="Cantidad 1" />
                        <asp:BoundField DataField="Articulo 2" HeaderText="Artículo 2" />
                        <asp:BoundField DataField="Cantidad 2" HeaderText="Cantidad 2" />
                        <asp:TemplateField HeaderText="Acción" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSeleccionar" runat="server"
                                    CommandName="cmdSelect"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    Text="Seleccionar <i class='fas fa-angle-right'></i>"
                                    CssClass="btn btn-secondary"
                                    Visible="false">
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    CommandName="cmdSelect"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    Text="Eliminar <i class='fas fa-trash-alt'></i>"
                                    CssClass="btn btn-danger" Visible="false">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Panel ID="pnlVista" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%;">
                            <asp:Image ID="Img1" CssClass="w-100" runat="server" />
                        </td>
                        <td style="width: 50%;">
                            <asp:Image ID="Img2" CssClass="w-100" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%;">
                            <asp:Image ID="Img3" CssClass="w-100" runat="server" />
                        </td>
                        <td style="width: 50%;">
                            <asp:Image ID="Img4" CssClass="w-100" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%;">
                            <asp:Image ID="Img5" CssClass="w-100" runat="server" />
                        </td>
                        <td style="width: 50%;">
                            <asp:Image ID="Img6" CssClass="w-100" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
    <script>
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $("#<%= Image2.ClientID %>").attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }
        $("#<%= flpDragAndDrop.ClientID %>").change(function () {
            readURL(this);
        });
    </script>
</asp:Content>
