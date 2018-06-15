using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vayalun.Models
{
    [Table("tbcliente")]
    public class Cliente
    {
        [Column("id")]
        public int Id { set; get; }
        [Column("nome")]
        public string Nome { set; get; }
        [Column("cpf")]
        public string CPF { set; get; }
    }
}