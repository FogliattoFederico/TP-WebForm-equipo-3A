<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Confirmacion.aspx.cs" Inherits="WebApplication2.Confirmacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="confirmacion">
        <h1>Gracias <%:Session["nombre"] %> por participar!!!!</h1>
        <p>En breve recibiras un mail de confirmacion a tu correo <%:Session["correo"] %></p>
        <p>MUCHA SUERTE!!!!</p>
        <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="boton" OnClick="btnVolver_Click" />
    </div>
</asp:Content>

