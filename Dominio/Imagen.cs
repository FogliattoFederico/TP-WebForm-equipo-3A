using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Dominio
{
    public class Imagen
    {
        public int Id { get; set; }
        
        public int IdArticulo { get; set; }

        [DisplayName("Url Imagen")]
        public string ImagenUrl { get; set; }

        public override string ToString()
        {
            return ImagenUrl;
        }
    }
}
