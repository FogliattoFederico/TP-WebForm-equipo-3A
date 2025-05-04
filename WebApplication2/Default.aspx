<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class=" voucher-container">
        <p>
            Con su compra, ud ha recibio un voucher para participar en el sorteo de increibles productos.
            Solo debe ingresar el número de voucher en el siguiente campo y hacer click en el botón "Participar".
            Debera completar el formulario de registro para que su participación sea válida.
            Recuerde que el sorteo se realizará el 30 de mayo de 2025 y los ganadores serán publicados en nuestras redes sociales.
        </p>
        <div class="row voucher">
            <div class="col-md-4"></div>
            <div class="col-md-4 mb-3 flex">
                <input type="text" class="form-control" id="N_voucher" placeholder="N° Voucher">
                <asp:Button CssClass="boton" Text="Participar" ID="btn_participar" OnClick="btn_participar_Click" runat="server" />
            </div>
            <div class="col-md-4"></div>
        </div>
        <div>
            <asp:GridView ID="gvVouchers" runat="server">
            </asp:GridView>
        </div>

    </div>



</asp:Content>
