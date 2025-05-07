using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Formulario : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            lblDni.Text = "";
            btnParticipar.Enabled = false;
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
            }else
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
            Cliente cliente = new Cliente();
            ClienteNegocio negocio = new ClienteNegocio();
            try
            {
                cliente.Dni = txtDni.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.Nombre = txtNombre.Text;
                cliente.Email = txtEmail.Text;
                cliente.Ciudad = txtCiudad.Text;
                cliente.Cp = int.Parse(txtCp.Text);
                cliente.Direccion = txtDireccion.Text;

                negocio.AgregarCliente(cliente);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            
 
        }
    }
}