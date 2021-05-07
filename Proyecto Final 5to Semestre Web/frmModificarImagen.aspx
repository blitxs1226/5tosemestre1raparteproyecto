<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="frmModificarImagen.aspx.cs" Inherits="Proyecto_Final_5to_Semestre_Web.frmModificarImagen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:LinkButton runat="server" ID="lnkNuevo" CssClass="btn btn-success" OnClick="lnkNuevo_Click"><i class="fas fa-eye"></i> Generar Reporte</asp:LinkButton>
    <asp:Panel runat="server" ID="pnl"></asp:Panel>
</asp:Content>
