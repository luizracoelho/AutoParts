using AutoParts.DL;
using System;
using System.Collections.Generic;

namespace AutoParts.DAL
{
    public class ProdutoDAO : ICrud<Produto>
    {
        public void Adicionar(Produto produto)
        {
            throw new NotImplementedException();
        }

        public void Editar(Produto produto)
        {
            throw new NotImplementedException();
        }

        public IList<Produto> Listar()
        {
            return new List<Produto>();
        }

        public void Remover(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}
