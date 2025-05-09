using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebApplication2
{
    public partial class Default : System.Web.UI.Page
    {
       protected void Page_Load(object sender, EventArgs e)
        {
           lblValidado.Text = "";

        }

        protected void btn_validar_Click(object sender, EventArgs e)
        {
            try
            {
                var negocio = new VoucherNegocio();
                var lista = negocio.Listar();
                var voucher = lista.FirstOrDefault(x => x.CodigoVoucher == txtVoucher.Text.Trim());

                if (voucher == null)
                {
                    lblValidado.Text = "El voucher no es válido";
                    lblValidado.ForeColor = System.Drawing.Color.Red;
                    lblValidado.CssClass = "text-bg-warning p-2";
                }
                else if (voucher.IdCliente != 0)
                {
                    lblValidado.Text = "El voucher ya fue canjeado";
                    lblValidado.ForeColor = System.Drawing.Color.Orange;
                    lblValidado.CssClass = "text-bg-danger p-2";
                }
                else
                {
                    Session["voucher"] = txtVoucher;
                    Response.Redirect("Premios.aspx", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
               
            }
        }

       
    }
}