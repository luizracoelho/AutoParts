using AutoParts.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AutoParts.DAL
{
    public class ProdutoDAO : ICrudable<Produto>
    {
        private const int COL_PRODUTO_ID = 0;
        private const int COL_NOME_PROD = 1;
        private const int COL_DESC_PROD = 2;
        private const int COL_VALOR_PROD = 3;

        private SqlConnection conn;

        public ProdutoDAO()
        {
            var dao = new ConnectorDAO();
            conn = ConnectorDAO.GetConnection();
        }

        public void Adicionar(Produto produto)
        {
            try
            {
                var query = "uspProdutoInserir";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@NOME_PROD", SqlDbType.VarChar).Value = produto.Nome;
                    cmd.Parameters.AddWithValue("@DESC_PROD", SqlDbType.VarChar).Value = produto.Descricao;
                    cmd.Parameters.AddWithValue("@VALOR_PROD", SqlDbType.Decimal).Value = produto.Valor;

                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Editar(Produto produto)
        {
            try
            {
                var query = "uspProdutoAlterar";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@PRODUTO_ID", SqlDbType.Int).Value = produto.ProdutoId;
                    cmd.Parameters.Add("@NOME_PROD", SqlDbType.VarChar).Value = produto.Nome;
                    cmd.Parameters.AddWithValue("@DESC_PROD", SqlDbType.VarChar).Value = produto.Descricao;
                    cmd.Parameters.AddWithValue("@VALOR_PROD", SqlDbType.Decimal).Value = produto.Valor;

                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public IList<Produto> Listar()
        {
            try
            {
                var query = "SELECT * FROM PRODUTOS";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    var dr = cmd.ExecuteReader();

                    var lista = new List<Produto>();

                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            lista.Add(new Produto
                            {
                                ProdutoId = dr.GetInt32(COL_PRODUTO_ID),
                                Nome = dr.GetString(COL_NOME_PROD),
                                Descricao = dr.GetString(COL_DESC_PROD),
                                Valor = dr.GetDecimal(COL_VALOR_PROD)
                            });
                        }

                    return lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Remover(Produto produto)
        {
            try
            {
                var query = "uspProdutoExcluir";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@PRODUTO_ID", SqlDbType.Int).Value = produto.ProdutoId;

                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
