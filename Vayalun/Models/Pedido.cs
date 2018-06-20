using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vayalun.Models
{
    [Table("tbpedido")]
    public class Pedido
    {
        [Column("id")]
        public int Id { set; get; }

        [Column("datapedidofeito")]
        public DateTime DataPedidoFeito { set; get; }

        [Column("datapedidopronto")]
        public DateTime DataPedidoPronto { set; get; }

        [Column("total")]
        public Double Total { set; get; }

        [Column("status")]
        public String Status { set; get; }

        [Column("tbcliente_id")]
        public int ClienteId { set; get; }
        public Cliente Cliente { set; get; }

        [Column("tbmesa_id")]
        public int MesaId { set; get; }
        public Mesa Mesa { set; get; }

        [Column("tbfuncionario_id")]
        public int FuncionarioId { set; get; }
        public Funcionario Funcionario { set; get; }


        public ICollection<ItemPedido> ItemPedidosId { set; get; }

    }
}