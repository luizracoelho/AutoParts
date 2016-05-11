using AutoParts.DL;
using AutoParts.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

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
                usuario.Senha = OnCrypt.Encrypt(usuario.Senha);

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

        public IList<Usuario> Listar()
        {
            try
            {
                return dao.Listar();
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
                dao.Remover(usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Validar(Usuario usuario)
        {
            var usuarioExiste = dao.Listar().FirstOrDefault(x => x.Login.ToLower().Contains(usuario.Login.ToLower()));

            if (usuarioExiste != null)
                throw new Exception("Já existe um Usuário com este Login.");
        }
    }
}
