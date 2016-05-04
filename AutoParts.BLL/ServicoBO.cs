using AutoParts.BL;
using AutoParts.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoParts.BLL
{
    public class ServicoBO : ICrud<Servico>
    {
        ServicoDAO dao;

        public ServicoBO()
        {
            dao = new ServicoDAO();
        }

        public void Adicionar(Servico servico)
        {
            try
            {
                Validar(servico);

                dao.Adicionar(servico);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Editar(Servico servico)
        {
            try
            {
                Validar(servico);

                dao.Editar(servico);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Servico Encontrar(int id)
        {
            try
            {
                return Listar().FirstOrDefault(x => x.ServicoId == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<Servico> Listar()
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

        public void Remover(Servico servico)
        {
            try
            {
                dao.Remover(servico);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Validar(Servico servico)
        {
            var servicoExiste = dao.Listar().FirstOrDefault(x => x.Nome.ToLower().Contains(servico.Nome.ToLower()));

            if (servicoExiste != null)
                throw new Exception("Já existe um Serviço com este Nome.");
        }
    }
}
