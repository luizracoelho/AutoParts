using AutoParts.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AutoParts.DAL
{
    public class UsuarioDAO : ICrudable<Usuario>
    {
        private const int COL_USUARIO_ID = 0;
        private const int COL_NOME_USUARIO = 1;
        private const int COL_LOGIN = 2;
        private const int COL_SENHA = 3;

        private SqlConnection conn;

        public UsuarioDAO()
        {
            var dao = new ConnectorDAO();
            conn = ConnectorDAO.GetConnection();
        }

        public void Adicionar(Usuario usuario)
        {
            try
            {
                var query = "uspUsuarioInserir";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@NOME_USUARIO", SqlDbType.VarChar).Value = usuario.Nome;
                    cmd.Parameters.AddWithValue("@LOGIN", SqlDbType.VarChar).Value = usuario.Login;
                    cmd.Parameters.AddWithValue("@SENHA", SqlDbType.VarChar).Value = usuario.Senha;

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

        public void Editar(Usuario usuario)
        {
            try
            {
                var query = "uspUsuarioAlterar";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@USUARIO_ID", SqlDbType.Int).Value = usuario.UsuarioId;
                    cmd.Parameters.Add("@NOME_USUARIO", SqlDbType.VarChar).Value = usuario.Nome;
                    cmd.Parameters.AddWithValue("@LOGIN", SqlDbType.VarChar).Value = usuario.Login;
                    cmd.Parameters.AddWithValue("@SENHA", SqlDbType.VarChar).Value = usuario.Senha;

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

        public IList<Usuario> Listar()
        {
            try
            {
                var query = "SELECT * FROM USUARIOS";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    var dr = cmd.ExecuteReader();

                    var lista = new List<Usuario>();

                    if (dr.HasRows)
                        while (dr.Read())
                        {
                            lista.Add(new Usuario
                            {
                                UsuarioId = dr.GetInt32(COL_USUARIO_ID),
                                Nome = dr.GetString(COL_NOME_USUARIO),
                                Login = dr.GetString(COL_LOGIN),
                                Senha = dr.GetString(COL_SENHA)
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

        public void Remover(Usuario usuario)
        {
            try
            {
                var query = "uspUsuarioExcluir";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@USUARIO_ID", SqlDbType.Int).Value = usuario.UsuarioId;

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
