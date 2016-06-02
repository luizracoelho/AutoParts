using AutoMapper;
using AutoParts.DL;
using AutoParts.UI.Web.Areas.Painel.Models;

namespace AutoParts.UI.Web.Mappers
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            //Usuário
            Mapper.CreateMap<Usuario, UsuarioVM>();
            Mapper.CreateMap<UsuarioVM, Usuario>();

            Mapper.CreateMap<Usuario, UsuarioEntrarVM>();
            Mapper.CreateMap<UsuarioEntrarVM, Usuario>();

            //Produto
            Mapper.CreateMap<Produto, ProdutoVM>();
            Mapper.CreateMap<ProdutoVM, Produto>();

            //Serviço
            Mapper.CreateMap<Servico, ServicoVM>();
            Mapper.CreateMap<ServicoVM, Servico>();
        }
    }
}
