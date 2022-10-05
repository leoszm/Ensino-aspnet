using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {//vai injetar o serviço como repositório, ja foi definido os serviços em startup.cs!
        private readonly ILancheRepository _lancheRepository;
        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        //vai listar os lanches
        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                //if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
                //{
                //    lanches = _lancheRepository.Lanches
                //        .Where(l => l.Categoria.CategoriaNome.Equals("Normal"))
                //        .OrderBy(l => l.Nome);
                //}
                //else
                //{
                //    lanches = _lancheRepository.Lanches
                //       .Where(l => l.Categoria.CategoriaNome.Equals("Natural"))
                //       .OrderBy(l => l.Nome);
                //}
                lanches = _lancheRepository.Lanches
                          .Where(l => l.Categoria.CategoriaNome.Equals(categoria))
                          .OrderBy(c => c.Nome);

                categoriaAtual = categoria;
            }

            /*passagem de dados da controller para a view
            ViewData["Titulo"] = "Todos os Lanches";
            ViewData["Data"] = DateTime.Now;*/

            //lista de lanches
            //var lanches = _lancheRepository.Lanches;
            //return View(lanches);

            //instancia
            var lanchesListViewModel = new LancheListViewModel
            {//utilizar a instancia pra pegar as 2 propriedades abaixo
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesListViewModel);
        }

        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
            return View(lanche);
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(p => p.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                //if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
                //{
                //    lanches = _lancheRepository.Lanches
                //        .Where(l => l.Categoria.CategoriaNome.Equals("Normal"))
                //        .OrderBy(l => l.Nome);
                //}
                //else
                //{
                //    lanches = _lancheRepository.Lanches
                //       .Where(l => l.Categoria.CategoriaNome.Equals("Natural"))
                //       .OrderBy(l => l.Nome);
                //}
                lanches = _lancheRepository.Lanches
                          .Where(p => p.Nome.ToLower().Contains(searchString.ToLower()));
                if (lanches.Any())
                {
                    categoriaAtual = "Lanches";
                }
                else
                {
                    categoriaAtual = "Nenhum lanche foi encontrado";
                }
            }
                return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
                {
                    Lanches = lanches,
                    CategoriaAtual = categoriaAtual
                });


            /*public IActionResult Details(int lancheId)
            {
                //obter o lanche do repositório, acessar tabela e retornar a view do lanche
                var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
                return View(lanche);
            }*/
        }
    }
}
