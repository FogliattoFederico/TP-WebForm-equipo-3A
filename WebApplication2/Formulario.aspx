<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="WebApplication2.Formulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        let dniFueValido = false; // Flag para controlar si alguna vez fue válido

        function manejarCambioDni() {
            const txtDni = document.getElementById('<%= txtDni.ClientID %>');
            const lblDni = document.getElementById('<%= lblDni.ClientID %>');
            const dni = txtDni.value.trim();

            // Si el valor actual tiene 7 u 8 dígitos, marcamos como válido
            if (/^\d{7,8}$/.test(dni)) {
                dniFueValido = true;
                lblDni.textContent = "";
            }
            // Si alguna vez fue válido y ahora es menos de 7 dígitos, mostramos el error
            else if (dniFueValido && dni.length < 7) {
                lblDni.textContent = "Ingresá un DNI válido de 7 u 8 dígitos.";
            } else {
                lblDni.textContent = "";
            }

            // Siempre que cambie el DNI, reiniciamos el formulario
            reiniciarFormulario();
        }

        function reiniciarFormulario() {
            const campos = [
            '<%= txtNombre.ClientID %>',
            '<%= txtApellido.ClientID %>',
            '<%= txtEmail.ClientID %>',
            '<%= txtDireccion.ClientID %>',
            '<%= txtCiudad.ClientID %>',
            '<%= txtCp.ClientID %>'
            ];

            for (let id of campos) {
                const campo = document.getElementById(id);
                if (campo) {
                    campo.value = "";
                    campo.disabled = true;
                }
            }

            document.getElementById('<%= btnParticipar.ClientID %>').disabled = true;
    }

    function validarFormulario() {
        const campos = [
            '<%= txtDni.ClientID %>',
            '<%= txtNombre.ClientID %>',
            '<%= txtApellido.ClientID %>',
            '<%= txtEmail.ClientID %>',
            '<%= txtDireccion.ClientID %>',
            '<%= txtCiudad.ClientID %>',
            '<%= txtCp.ClientID %>'
        ];

        let completo = true;

        for (let id of campos) {
            let campo = document.getElementById(id);
            if (!campo || campo.disabled || campo.value.trim() === "") {
                completo = false;
                break;
            }
        }

        document.getElementById('<%= btnParticipar.ClientID %>').disabled = !completo;
        }
    </script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
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
                            <%--<asp:TextBox CssClass="form-control" ID="txtDni" runat="server" AutoPostBack="true" OnTextChanged="txtDni_TextChanged" TextMode="Number"></asp:TextBox>--%>
                            <%--<asp:TextBox CssClass="form-control" ID="txtDni" runat="server" onkeyup="validarFormulario()" TextMode="Number"></asp:TextBox>--%>
                            <asp:TextBox CssClass="form-control" ID="txtDni" runat="server" onkeyup="manejarCambioDni()" placeholder="Ingresá tu DNI (7 u 8 dígitos)" TextMode="SingleLine" MaxLength="8"></asp:TextBox>
                            <asp:RegularExpressionValidator 
                                ID="revDni" 
                                runat="server" 
                                ControlToValidate="txtDni" 
                                ValidationExpression="^\d{7,8}$" 
                                ForeColor="Red" 
                                Display="Dynamic" 
                                CssClass="text-danger" />
                            <asp:Label ID="lblDni" runat="server" Text="" CssClass="text-danger"></asp:Label>
                        </div>
                        <asp:Button ID="btnVerificar" CssClass="btn btn-primary mt-2 w-100" runat="server"
                            Text="Verificar DNI" OnClick="btnVerificar_Click" />
                    </div>

                    <hr />

                    <div class="mb-3">
                        <label for="txtApellido" class="form-label">Apellido</label>
                        <%--<asp:TextBox CssClass="form-control" ID="txtApellido" AutoPostBack="true" OnTextChanged="txtApellido_TextChanged" runat="server"></asp:TextBox>--%>
                        <asp:TextBox CssClass="form-control" ID="txtApellido" onkeyup="validarFormulario()" runat="server"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label for="txtNombre" class="form-label">Nombre</label>
                        <%--<asp:TextBox CssClass="form-control" OnTextChanged="txtNombre_TextChanged" AutoPostBack="true" ID="txtNombre" runat="server"></asp:TextBox>--%>
                        <asp:TextBox CssClass="form-control" ID="txtNombre" onkeyup="validarFormulario()" runat="server"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Email</label>
                        <%--<asp:TextBox CssClass="form-control" ID="txtEmail" OnTextChanged="txtEmail_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>--%>
                        <asp:TextBox CssClass="form-control" ID="txtEmail" onkeyup="validarFormulario()" runat="server"></asp:TextBox>
                        <asp:Label ID="lblemail" CssClass="text-danger" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="mb-3">
                        <label for="txtDireccion" class="form-label">Direccion</label>
                        <%--<asp:TextBox CssClass="form-control" ID="txtDireccion" OnTextChanged="txtDireccion_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>--%>
                        <asp:TextBox CssClass="form-control" ID="txtDireccion" onkeyup="validarFormulario()" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtCiudad" class="form-label">Ciudad</label>
                        <%--<asp:TextBox CssClass="form-control" ID="txtCiudad" OnTextChanged="txtCiudad_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>--%>
                        <asp:TextBox CssClass="form-control" ID="txtCiudad" onkeyup="validarFormulario()" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtCp" class="form-label">CP</label>
                        <%--<asp:TextBox CssClass="form-control" ID="txtCp" OnTextChanged="txtCp_TextChanged" AutoPostBack="true" runat="server" TextMode="Number"></asp:TextBox>--%>
                        <asp:TextBox CssClass="form-control" ID="txtCp" onkeyup="validarFormulario()" runat="server" TextMode="Number"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnParticipar" CssClass="btn btn-primary" runat="server" Text="Participar" OnClick="btnParticipar_Click" />
                    <asp:Label ID="lblResultado" CssClass="text-success" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="col-md-4"></div>
        </div>
    </div>
</asp:Content>
