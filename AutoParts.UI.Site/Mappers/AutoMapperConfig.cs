using AutoMapper;
using AutoParts.DL;
using AutoParts.UI.Site.Models;

namespace AutoParts.UI.Site.Mappers
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            //Produto
            Mapper.CreateMap<Produto, ProdutoVM>();
            Mapper.CreateMap<ProdutoVM, Produto>();

            //Serviço
            Mapper.CreateMap<Servico, ServicoVM>();
            Mapper.CreateMap<ServicoVM, Servico>();
        }
    }
}
