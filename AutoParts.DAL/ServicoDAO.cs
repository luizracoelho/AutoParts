using AutoParts.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AutoParts.DAL
{
    public class ServicoDAO : ICrudable<Servico>
    {
        private const int COL_SERVICO_ID = 0;
        private const int COL_NOME_SERV = 1;
        private const int COL_DESC_SERV = 2;
        private const int COL_VALOR_SERV = 3;

        private SqlConnection conn;

        public ServicoDAO()
        {
            var dao = new ConnectorDAO();
            conn = dao.GetConnection();
        }

        public void Adicionar(Servico servico)
        {
            try
            {
                var query = "uspServicoInserir";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@NOME_SERV", SqlDbType.VarChar).Value = servico.Nome;
                    cmd.Parameters.AddWithValue("@DESC_SERV", SqlDbType.VarChar).Value = servico.Descricao;
                    cmd.Parameters.AddWithValue("@VALOR_SERV", SqlDbType.Decimal).Value = servico.Valor;

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

        public void Editar(Servico servico)
        {
            try
            {
                var query = "uspServicoAlterar";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@SERVICO_ID", SqlDbType.Int).Value = servico.ServicoId;
                    cmd.Parameters.Add("@NOME_SERV", SqlDbType.VarChar).Value = servico.Nome;
                    cmd.Parameters.AddWithValue("@DESC_SERV", SqlDbType.VarChar).Value = servico.Descricao;
                    cmd.Parameters.AddWithValue("@VALOR_SERV", SqlDbType.Decimal).Value = servico.Valor;

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

        public IList<Servico> Listar()
        {
            try
            {
                var query = "SELECT * FROM SERVICOS";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    var dr = cmd.ExecuteReader();

                    var lista = new List<Servico>();

                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            lista.Add(new Servico
                            {
                                ServicoId = dr.GetInt32(COL_SERVICO_ID),
                                Nome = dr.GetString(COL_NOME_SERV),
                                Descricao = dr.GetString(COL_DESC_SERV),
                                Valor = dr.GetDecimal(COL_VALOR_SERV)
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

        public void Remover(Servico servico)
        {
            try
            {
                var query = "uspServicoExcluir";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@SERVICO_ID", SqlDbType.Int).Value = servico.ServicoId;

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
