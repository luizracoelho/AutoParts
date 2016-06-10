using AutoParts.DL;
using AutoParts.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoParts.Tools.BLL;

namespace AutoParts.BLL
{
    public class UsuarioBO
    {
        UsuarioDAO dao;

        public UsuarioBO()
        {
            dao = new UsuarioDAO();
        }

        private void Adicionar(Usuario usuario)
        {
            try
            {
                Validar(usuario);

                dao.Adicionar(usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Editar(Usuario usuario)
        {
            try
            {
                Validar(usuario);

                dao.Editar(usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Salvar(Usuario usuario)
        {
            try
            {
                usuario.Login = usuario.Login.ToLower();
                usuario.Senha = Crypt.Encrypt(usuario.Senha);

                if (usuario.UsuarioId == 0)
                    Adicionar(usuario);
                else
                    Editar(usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuario Encontrar(int id)
        {
            try
            {
                return Listar().FirstOrDefault(x => x.UsuarioId == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuario Encontrar(string login)
        {
            try
            {
                return Listar().FirstOrDefault(x => x.Login.Equals(login.ToLower()));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<Usuario> Listar()
        {
            try
            {
                return dao.Listar().OrderBy(x => x.Nome).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remover(Usuario usuario)
        {
            try
            {
                if (usuario.UsuarioId == 1)
                    throw new Exception("Não é possível remover o Administrador");

                dao.Remover(usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Validar(Usuario usuario)
        {
            var usuarioExiste = dao.Listar().FirstOrDefault(x => x.Login.Contains(usuario.Login) && x.UsuarioId != usuario.UsuarioId);

            if (usuarioExiste != null)
                throw new Exception("Já existe um Usuário com este Login.");
        }

        public Usuario Login(Usuario usuario)
        {
            try
            {
                var usuarioBanco = Encontrar(usuario.Login);

                if (usuarioBanco == null)
                    throw new Exception();

                var senhaEncriptada = Crypt.Encrypt(usuario.Senha);

                if (!usuarioBanco.Senha.Equals(senhaEncriptada))
                    throw new Exception();

                return usuarioBanco;
            }
            catch (Exception)
            {
                throw new Exception("Login ou Senha incorretos.");
            }
        }
    }
}
