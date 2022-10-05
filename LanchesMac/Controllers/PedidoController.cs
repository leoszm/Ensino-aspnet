using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace LanchesMac.Controllers
{
    public class PedidoController : Controller
    {
        //injetar as instancias no contrutor deste controller 
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository,
                                CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            //obtendo itens do carrinho de compras

            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItems = items;

            //verificando a existência de itens de pedido

            if (_carrinhoCompra.CarrinhoCompraItems.Count == 0)
            {
                /*ModelState é uma coleçao de pares(nome valor) que é submetida ao servidor durante post
                coleçao de mensagens de erro, ModelState vai estar com estado inválido, impossibilitando o prosseguimento*/
                ModelState.AddModelError("", "Seu Carrinho está vazio, retorne ao catálogo que possue uma variedade de lanches :D");
            }

            //calcular o total de itens e o total do pedido
            foreach (var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
            }
                //atribui os valores obtidos ao pedido
            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            //valida os dados do pedido de acordo com retorno do ModelState
            if(ModelState.IsValid){
            //cria o pedido e seus detalhes
            _pedidoRepository.CriarPedido(pedido);

            //ViewBag é o comandop utilizado para compartilahmento de dados entre view e controller ou vice e versa
            //define mensagens ao cliente
            ViewBag.CheckoutCompletoMensagem = "Thanks for your request, see you later :) ";
            ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

            //limpa o carrinho do cliente
            _carrinhoCompra.LimparCarrinho();

            //exibir a view com                                   dados do cliente e do pedido
            return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }
        return View(pedido);         
        }
    }
}