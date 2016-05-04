using System.Collections.Generic;

namespace AutoParts.BLL
{
    public interface ICrud<T>
    {
        void Adicionar(T entidade);
        void Editar(T entidade);
        T Encontrar(int id);
        IList<T> Listar();
        void Remover(T entidade);
    }
}
