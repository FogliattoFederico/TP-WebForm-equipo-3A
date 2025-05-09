using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;


namespace WebApplication2
{
    public partial class Premios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPremios();
            }
        }

        private void CargarPremios()
        {
            PremioNegocio negocio = new PremioNegocio();
            List<Premio> listaPremios = negocio.listar();
            repPremios.DataSource = listaPremios;
            repPremios.DataBind();
        }

        // Evento para añadir el HTML de las imagenes de cada premio dentro del ItemTemplate de Repeater.
        protected void repPremios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Premio premio = (Premio)e.Item.DataItem;
                var packImagenes = (HtmlGenericControl)e.Item.FindControl("packImagenes");

                for (int i = 0; i < premio.Imagenes.Count; i++)
                {
                    string imagen1 = (i == 0) ? "active" : "";
                    string htmlImagenes = $@"
                <div class='carousel-item {imagen1}'>
                    <img src='{premio.Imagenes[i]}' class='d-block w-100 img-fluid object-fit-contain bg-light' style='height: 200px;' alt='Imagen Premio'>
                </div>";

                    packImagenes.InnerHtml += htmlImagenes;
                }
            }
        }

        // Evento al presionar el boton PARTICIPAR del premio elegido.
        protected void repPremios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "participar")
            {
                int idPremio = int.Parse(e.CommandArgument.ToString());

                Session["PremioCanjeado"] = idPremio; // Guardo id del premio canjeado en session;
                //Session.Add("PremioCanjeado", idPremio);



                Response.Redirect("Formulario.aspx", false); // Redireccion.
                //Response.Redirect("Default.aspx?PremioCanjeado=" + idPremio, false); // Redireccion con paso idPremio por URL(OPCIONAL).
            }
        }


    }
}
