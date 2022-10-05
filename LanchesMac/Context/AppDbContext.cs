using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;
//Classe de contexto Vai carregar as informações e definir quais classes vão para quais tabelas
namespace LanchesMac.Context
{   //classe de contexto vai herdar de DbContext(classe do EFCore)
    public class AppDbContext : DbContext
    {//quais são as classes do modelo de domínio que quer mapear
        //construtor        classe          referenciar                 vai passar para o construtor a classe base que é DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //define e carrega as informnações de configurações necessárias pra carregar DbContext
        }
        //quais classes quer mapear para quais tabelas
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }


        //mapeando a classe pedido para a tabela pedido e a classe PedidoDetalhe para a tabela PedidoDetalhes
        //mapeando a classe pedido para a tabela pedido e a classe PedidoDetalhe para a tabela PedidoDetalhes
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }
    }
}
