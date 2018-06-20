using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vayalun.Models
{
    [Table("tbitempedido")]
    public class ItemPedido
    {
        [Column("id")]
        public int Id { set; get; }

        [Column("qtd")]
        public int Quantidade { set; get; }

        [Column("preco")]
        public Double Preco { set; get; }

       
        [Column("tbpedido_id")]
        public int PedidoId { set; get; }
        public Pedido Pedido { set; get; }

       
        [Column("tbitemcardapio_id")]
        public int ItemCardapioId { set; get; }
        public ItemCardapio ItemCardapio { set; get; }


    }
}