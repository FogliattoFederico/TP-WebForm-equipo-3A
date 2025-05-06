using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VoucherNegocio
    {
        public List<Voucher> Listar()
        {
            List<Voucher> lista = new List<Voucher>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("select CodigoVoucher, IdCliente, FechaCanje, IdArticulo from Vouchers");
                datos.ejecutarLectura();


                while (datos.Lector.Read())
                {
                    Voucher aux = new Voucher();

                    aux.CodigoVoucher = (string)datos.Lector["CodigoVoucher"];

                    if (datos.Lector["IdCliente"] != DBNull.Value)
                    {
                        aux.IdCliente = (int)datos.Lector["IdCliente"];
                    }

                    if (datos.Lector["FechaCanje"] != DBNull.Value)
                    {
                        aux.FechaCanje = (DateTime)datos.Lector["FechaCanje"];
                    }

                    if (datos.Lector["IdArticulo"] != DBNull.Value)
                    {
                        aux.IdArticulo = (int)datos.Lector["IdArticulo"];
                    }

                    lista.Add(aux);
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


        public void ModificarVoucher(Voucher voucher)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                //datos.setearConsulta("update Vouchers set IdCliente = @IdCliente, FechaCanje = @FechaCanje where CodigoVoucher = @CodigoVoucher");
                datos.setearConsulta("UPDATE Vouchers set IdCliente = @IdCliente, FechaCanje = GETDATE(), IdArticulo = @IdArticulo Where CodigoVoucher = @NVoucher");
                datos.setearParametro("@IdCliente", voucher.IdCliente);
                //datos.setearParametro("@FechaCanje", voucher.FechaCanje);
                datos.setearParametro("@IdArticulo", voucher.IdArticulo);
                datos.setearParametro("@CodigoVoucher", voucher.CodigoVoucher);

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
