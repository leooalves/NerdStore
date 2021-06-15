

using NerdStore.Catalogo.Application.ViewModel;
using NerdStore.Catalogo.Domain.Entidades;
using AutoMapper;

namespace NerdStore.Catalogo.Application.AutoMapper
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<ProdutoViewModel, Produto>()
                .ConstructUsing(p =>
                    new Produto(p.Nome, p.Descricao, p.Ativo,
                        p.Valor, p.DataCadastro, p.CategoriaId,
                        p.Imagem, new Dimensoes(p.Altura, p.Largura, p.Profundidade)));

            CreateMap<CategoriaViewModel, Categoria>()
                .ConstructUsing(c => new Categoria(c.Nome, c.Codigo));
        }
    }
}
