using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class PremioNegocio
    {

        public List<Premio> listar()
        {
            List<Premio> lista = new List<Premio>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT A.Id AS IdArticulo, A.Nombre, A.Descripcion, I.ImagenUrl 
                                FROM ARTICULOS A
                                LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id
                                ORDER BY A.Id";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                Premio premio = null;

                while (datos.Lector.Read())
                {
                    int idArticulo = (int)datos.Lector["IdArticulo"];

                    if (premio == null || premio.Id != idArticulo)
                    {
                        if (premio != null)
                        {
                            lista.Add(premio);  // Guardamos el premio anterior
                        }

                        premio = new Premio();
                        premio.Id = idArticulo;
                        premio.Nombre = datos.Lector["Nombre"].ToString();
                        premio.Descripcion = datos.Lector["Descripcion"].ToString();
                        premio.Imagenes = new List<string>();

                    }
                    
                    if (datos.Lector["ImagenUrl"] != DBNull.Value)
                    {
                        string imagenUrl = datos.Lector["ImagenUrl"].ToString();
                        premio.Imagenes.Add(imagenUrl);
                    }
                }

                if (premio != null)
                {
                    lista.Add(premio);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
