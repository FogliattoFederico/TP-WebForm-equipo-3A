using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;


            try
            {
                conexion.ConnectionString = "server = .\\SQLEXPRESS; database= CATALOGO_P3_DB; integrated security = true";
                comando.CommandType = System.Data.CommandType.Text;
                //comando.CommandText = "select\r\n\tA.Id,\r\n\tA.nombre,\r\n\tA.codigo,\r\n\tA.descripcion,\r\n\tM.Descripcion as Marca,\r\n\tC.Descripcion as Categoria,\r\n\tA.Precio,\r\n\tI.ImagenUrl\r\nFROM\t\r\n\tARTICULOS A\r\ninner JOIN\r\n\tMARCAS M ON A.IdMarca = M.Id\r\nleft JOIN\r\n\tCATEGORIAS C on A.IdCategoria = C.Id\r\ninner join\r\n\tIMAGENES I on A.Id = I.IdArticulo";
                comando.CommandText = "select A.Id,A.nombre, A.codigo, A.descripcion, A.IdCategoria, A.IdMarca, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio, I.ImagenUrl FROM ARTICULOS A INNER JOIN MARCAS M ON A.IdMarca = M.Id  LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id INNER JOIN IMAGENES I ON A.Id = I.IdArticulo order by Nombre ASC";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)lector["Id"];
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.Precio = Convert.ToDecimal(lector["Precio"]);
                   

                    aux.UrlImagen = new Imagen();
                    aux.UrlImagen.ImagenUrl = (string)lector["ImagenUrl"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)lector["IdMarca"];
                    aux.Marca.Descripcion = (string)lector["Marca"];

                    //Manejo de moto g sin categoria
                    aux.Categoria = !(lector["Categoria"] is DBNull)
                    ? new Categoria { Descripcion = (string)lector["Categoria"], Id = (int)lector["IdCategoria"] }
                    : null;
                   
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void AgregarArticulo(Articulo nuevoArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Primera consulta: Insertar el artículo
                datos.setearConsulta(@"insert into ARTICULOS (Nombre, Codigo, Descripcion, IdMarca, IdCategoria, Precio) 
                            values (@Nombre, @Codigo, @Descripcion, @Marca, @Categoria, @Precio);
                            SELECT SCOPE_IDENTITY();"); // Aquí capturamos el ID generado

                // Parámetros del artículo
                datos.setearParametro("@Nombre", nuevoArticulo.Nombre);
                datos.setearParametro("@Codigo", nuevoArticulo.Codigo);
                datos.setearParametro("@Descripcion", nuevoArticulo.Descripcion);
                datos.setearParametro("@Marca", nuevoArticulo.Marca.Id);
                datos.setearParametro("@Categoria", nuevoArticulo.Categoria.Id);
                datos.setearParametro("@Precio", nuevoArticulo.Precio);

                // Ejecutar y obtener el ID generado
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    // Asignamos el ID generado al artículo
                    nuevoArticulo.Id = Convert.ToInt32(datos.Lector[0]);
                }
                datos.cerrarConexion(); // Cerrar la primera conexión

                // Segunda consulta: Insertar la imagen usando el ID obtenido
                if (!string.IsNullOrEmpty(nuevoArticulo.UrlImagen.ImagenUrl))
                {
                    datos = new AccesoDatos(); // Nueva instancia o reabrir conexión
                    datos.setearConsulta(@"insert into IMAGENES (IdArticulo, ImagenUrl) values (@IdArticulo, @UrlImagen)");
                    datos.setearParametro("@IdArticulo", nuevoArticulo.Id);
                    datos.setearParametro("@UrlImagen", nuevoArticulo.UrlImagen.ImagenUrl);
                    datos.ejecutarAccion();
                }
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

        public void modificarArticulo(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();  
            try
            {
                datos.setearConsulta("UPDATE articulos \r\nSET codigo = @Codigo, \r\n    nombre = @Nombre, \r\n    Descripcion = @Descripcion, \r\n    IdMarca = @IdMarca,  -- Nombre más descriptivo\r\n    IdCategoria = @IdCategoria,  -- Nombre más descriptivo\r\n    Precio = @Precio \r\nWHERE id = @Id;");
                datos.setearParametro("@codigo", art.Codigo);
                datos.setearParametro("@Nombre", art.Nombre);
                datos.setearParametro("@Descripcion", art.Descripcion);
                datos.setearParametro("@IdMarca", art.Marca.Id);
                datos.setearParametro("@IdCategoria", art.Categoria.Id);
                datos.setearParametro("@Precio", art.Precio);
                datos.setearParametro("@Id", art.Id);

                datos.ejecutarAccion();
              
                if (!string.IsNullOrEmpty(art.UrlImagen.ImagenUrl))
                {
                    datos = new AccesoDatos(); // Nueva instancia o reabrir conexión
                    datos.setearConsulta("update IMAGENES set ImagenUrl = @UrlImagen  where IdArticulo = @IdArticulo;");
                    datos.setearParametro("@IdArticulo", art.Id);
                    datos.setearParametro("@UrlImagen", art.UrlImagen.ImagenUrl);
                    datos.ejecutarAccion();
                    
                }


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


        public void eliminarArticulo(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try { 
                
                datos.setearConsulta("delete from imagenes where IdArticulo = @IdArticulo");
                datos.setearParametro("@IdArticulo", id);
                datos.ejecutarAccion();
                datos.cerrarConexion();
            
                datos.setearConsulta("delete from ARTICULOS where id = @id");
                datos.setearParametro("@id",id);
                datos.ejecutarAccion();
                


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


        public List<Articulo> filtrar(string columna, string criterio, string buscar)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select A.Id,A.nombre, A.codigo, A.descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio, I.ImagenUrl FROM ARTICULOS A INNER JOIN MARCAS M ON A.IdMarca = M.Id  LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id INNER JOIN IMAGENES I ON A.Id = I.IdArticulo WHERE ";

                string opcion = "";

                switch (columna)
                {
                    case "Nombre":
                        opcion = "A.nombre";
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += opcion + " LIKE '" + buscar + "%'";
                                break;
                            case "Termina con":
                                consulta += opcion + " LIKE '%" + buscar + "'";
                                break;
                            case "Contiene":
                                consulta += opcion + " LIKE '%" + buscar + "%'";
                                break;
                        }
                        break;
                    case "Marca":
                        opcion = "M.Descripcion";
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += opcion + " LIKE '" + buscar + "%'";
                                break;
                            case "Termina con":
                                consulta += opcion + " LIKE '%" + buscar + "'";
                                break;
                            case "Contiene":
                                consulta += opcion + " LIKE '%" + buscar + "%'";
                                break;
                        }
                        break;
                    case "Categoria":
                        opcion = "C.Descripcion";
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += opcion + " LIKE '" + buscar + "%'";
                                break;
                            case "Termina con":
                                consulta += opcion + " LIKE '%" + buscar + "'";
                                break;
                            case "Contiene":
                                consulta += opcion + " LIKE '%" + buscar + "%'";
                                break;
                        }
                        break;
                    case "Precio":
                        opcion = "A.Precio";
                        switch (criterio)
                        {
                            case "Igual a":
                                consulta += opcion + " = '" + buscar + "'";
                                break;
                            case "Menor a":
                                consulta += opcion + " < '" + buscar + "'";
                                break;
                            case "Mayor a":
                                consulta += opcion + " > '" + buscar + "'";
                                break;
                        }
                        break;

                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = Convert.ToDecimal(datos.Lector["Precio"]);

                    aux.UrlImagen = new Imagen();
                    aux.UrlImagen.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    //Manejo de moto g sin categoria
                    aux.Categoria = !(datos.Lector["Categoria"] is DBNull)
                    ? new Categoria { Descripcion = (string)datos.Lector["Categoria"] }
                    : null;

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

    }
}

