using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

namespace WebApplication2
{
    public partial class Formulario : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            lblDni.Text = "";
            lblemail.Text = "";
            lblResultado.Text = "";
            if (!IsPostBack)
            {
                btnParticipar.Enabled = false;

            }
        }

        protected void btnVerificar_Click(object sender, EventArgs e)
        {
            List<Cliente> lista = new List<Cliente>();
            ClienteNegocio negocio = new ClienteNegocio();
            lista = negocio.Listar();


            Cliente cliente = lista.FirstOrDefault(x => x.Dni == txtDni.Text.Trim());
            if (cliente != null)
            {
                txtApellido.Text = cliente.Apellido;
                txtNombre.Text = cliente.Nombre;
                txtEmail.Text = cliente.Email;
                txtCiudad.Text = cliente.Ciudad;
                txtCp.Text = cliente.Cp.ToString();
                txtDireccion.Text = cliente.Direccion;

                btnParticipar.Enabled = true;
            }
            else
            {
                lblDni.Text = "El DNI no se encuentra registrado";
                txtApellido.Text = "";
                txtNombre.Text = "";
                txtEmail.Text = "";
                txtCiudad.Text = "";
                txtCp.Text = "";
                txtDireccion.Text = "";
                return;
            }


        }

        protected void btnParticipar_Click(object sender, EventArgs e)
        {

            ClienteNegocio negocio = new ClienteNegocio();
            List<Cliente> lista = new List<Cliente>();
            Voucher voucher = new Voucher();
            VoucherNegocio voucherNegocio = new VoucherNegocio();



            try
            {
                lista = negocio.Listar();

                Cliente clienteExistente = lista.FirstOrDefault(x => x.Dni == txtDni.Text.Trim());

                // Crear objeto cliente (nuevo o existente)
                Cliente cliente = clienteExistente ?? new Cliente();

                cliente.Dni = txtDni.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.Nombre = txtNombre.Text;
                cliente.Email = txtEmail.Text;
                cliente.Ciudad = txtCiudad.Text;
                cliente.Cp = int.Parse(txtCp.Text);
                cliente.Direccion = txtDireccion.Text;

                if (!System.Text.RegularExpressions.Regex.IsMatch(
                txtEmail.Text,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    lblemail.Text = "Email no válido";
                    return;
                }

                if (clienteExistente != null)
                {
                    negocio.ModificarCliente(cliente);
                    lblResultado.Text = "Cliente modificado correctamente";

                }
                else
                {
                    voucher.IdCliente = negocio.AgregarCliente(cliente);
                    lblResultado.Text = "Cliente agregado correctamente";
                }

                //voucher.IdCliente = cliente.Id;
                voucher.CodigoVoucher = Session["voucher"].ToString();
                voucher.IdArticulo = (int)Session["PremioCanjeado"];


                voucherNegocio.ModificarVoucher(voucher);

                //enviar email
                EnviarCorreoConfirmacion(txtEmail.Text, txtNombre.Text);
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public static void EnviarCorreoConfirmacion(string destinatario, string nombre)
        {

            var remitente = "programacionlina@gmail.com";
            var contraseña = "dpzs yqbz lkuz dick";

            var mensaje = new MailMessage();
            mensaje.From = new MailAddress(remitente);
            mensaje.To.Add(destinatario);
            mensaje.Subject = "Confirmación de registro en la promoción";
            mensaje.Body = $"Hola {nombre},\n\n¡Tu registro en la promoción ha sido exitoso!\n¡¡Ya estas Participando!!\n\n\nSuscribete a nuestro NewsLetter para conocer mas novedades.\n\n";

            var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(remitente, contraseña);
            smtp.EnableSsl = true;

            smtp.Send(mensaje);
        }


        private void controlAceptar()
        {
            // Lista de todos los TextBox a validar
            TextBox[] camposRequeridos =
            {
            txtApellido, txtNombre, txtCiudad,
            txtDireccion, txtDni, txtCp, txtEmail
            };

            // Verifica si todos los campos tienen texto válido
            bool todosCompletos = camposRequeridos.All(txt =>
                !string.IsNullOrWhiteSpace(txt.Text));

            btnParticipar.Enabled = todosCompletos;

        }

        protected void txtDni_TextChanged(object sender, EventArgs e)
        {

            controlAceptar();
        }

        protected void txtApellido_TextChanged(object sender, EventArgs e)
        {
            controlAceptar();
        }

        protected void txtNombre_TextChanged(object sender, EventArgs e)
        {
            controlAceptar();
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            controlAceptar();
        }

        protected void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            controlAceptar();
        }

        protected void txtCiudad_TextChanged(object sender, EventArgs e)
        {
            controlAceptar();
        }

        protected void txtCp_TextChanged(object sender, EventArgs e)
        {
            controlAceptar();
        }
    }
}