﻿using System.Collections.Generic;

namespace AutoParts.DAL
{
    public interface ICrudable<T>
    {
        void Adicionar(T entidade);
        void Editar(T entidade);
        IList<T> Listar();
        void Remover(T entidade);
    }
}
