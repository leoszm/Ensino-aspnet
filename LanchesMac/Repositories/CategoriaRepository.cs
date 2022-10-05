using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {//         somente leitura     tipo         Nome       
        private readonly AppDbContext _context;

        //injetou no contrutor uma instancia do contexto e atribuiu isso na variável context que é tipo appdbcontext
        public CategoriaRepository(AppDbContext context)
        {
            _context = context; 
        }

        //                            propriedade       acessar tabela/coleção categorias
        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
