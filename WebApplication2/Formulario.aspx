<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="WebApplication2.Formulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="fondo-formulario">

        <h1 class="tituloFormulario">Datos del Cliente</h1>
        <div class="row mb-3">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <div class="card p-3 mt-3">
                    <div class="mb-3">
                        <div class="form-group">
                            <label for="txtDni" class="form-label">DNI</label>
                            <asp:TextBox CssClass="form-control" ID="txtDni" runat="server" AutoPostBack="true" OnTextChanged="txtDni_TextChanged" TextMode="Number"></asp:TextBox>
                            <asp:Label ID="lblDni" runat="server" Text="" CssClass="text-danger"></asp:Label>
                        </div>
                        <asp:Button ID="btnVerificar" CssClass="btn btn-primary mt-2 w-100" runat="server"
                            Text="Verificar DNI" OnClick="btnVerificar_Click" />
                    </div>

                    <hr />

                    <div class="mb-3">
                        <label for="txtApellido" class="form-label">Apellido</label>
                        <asp:TextBox CssClass="form-control" ID="txtApellido" AutoPostBack="true" OnTextChanged="txtApellido_TextChanged" runat="server"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label for="txtNombre" class="form-label">Nombre</label>
                        <asp:TextBox CssClass="form-control" OnTextChanged="txtNombre_TextChanged" AutoPostBack="true" ID="txtNombre" runat="server"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Email</label>
                        <asp:TextBox CssClass="form-control" ID="txtEmail" OnTextChanged="txtEmail_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                        <asp:Label ID="lblemail" CssClass="text-danger" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="mb-3">
                        <label for="txtDireccion" class="form-label">Direccion</label>
                        <asp:TextBox CssClass="form-control" ID="txtDireccion" OnTextChanged="txtDireccion_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtCiudad" class="form-label">Ciudad</label>
                        <asp:TextBox CssClass="form-control" ID="txtCiudad" OnTextChanged="txtCiudad_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtCp" class="form-label">CP</label>
                        <asp:TextBox CssClass="form-control" ID="txtCp" OnTextChanged="txtCp_TextChanged" AutoPostBack="true" runat="server" TextMode="Number"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnParticipar" CssClass="btn btn-primary" runat="server" Text="Participar" OnClick="btnParticipar_Click" />
                    <asp:Label ID="lblResultado" CssClass="text-success" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="col-md-4"></div>
        </div>
    </div>
</asp:Content>
