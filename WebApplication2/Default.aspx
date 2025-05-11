<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class=" fondo voucher-container">
        <p>
            Con su compra, ud ha recibio un voucher para participar en el sorteo de increibles productos.
            Solo debe ingresar el número de voucher en el siguiente campo y hacer click en el botón "Participar".
            Debera completar el formulario de registro para que su participación sea válida.
            Recuerde que el sorteo se realizará el 30 de mayo de 2025 y los ganadores serán publicados en nuestras redes sociales.
        </p>
        <div class="row voucher">
            <div class="col-md-4"></div>
            <div class="col-md-4 mb-3 flex">
                <asp:TextBox ID="txtVoucher" CssClass="form-control" runat="server" placeholder="N° Voucher"></asp:TextBox>
                <asp:Button CssClass="boton btn btn-primary" Text="Validar" ID="btn_validar" OnClick="btn_validar_Click" runat="server" />
                <asp:Label Text="" ID="lblValidado" runat="server" />
            </div>
            <div class="col-md-4"></div>
        </div>
       

    </div>



</asp:Content>
