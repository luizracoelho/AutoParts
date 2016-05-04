﻿using AutoParts.BL;
using AutoParts.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoParts.BLL
{
    public class ProdutoBO : ICrud<Produto>
    {
        ProdutoDAO dao;

        public ProdutoBO()
        {
            dao = new ProdutoDAO();
        }

        public void Adicionar(Produto produto)
        {
            try
            {
                Validar(produto);

                dao.Adicionar(produto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Editar(Produto produto)
        {
            try
            {
                Validar(produto);

                dao.Editar(produto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Produto Encontrar(int id)
        {
            try
            {
                return Listar().FirstOrDefault(x => x.ProdutoId == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<Produto> Listar()
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

        public void Remover(Produto produto)
        {
            try
            {
                dao.Remover(produto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Validar(Produto produto)
        {
            var produtoExiste = dao.Listar().FirstOrDefault(x => x.Nome.ToLower().Contains(produto.Nome.ToLower()));

            if (produtoExiste != null)
                throw new Exception("Já existe um Produto com este Nome.");
        }
    }
}
