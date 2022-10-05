using LanchesMac.Models;

namespace LanchesMac.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        //propriedade somente de leitura(get)
        IEnumerable<Categoria> Categorias { get; }
    }
}
