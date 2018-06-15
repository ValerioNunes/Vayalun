using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vayalun.Models
{
    [Table("tbfuncionario")]
    public class Funcionario
    {
        [Column("id")]
        public int Id { set; get; }

        [Column("nome")]
        public string Nome { set; get; }

        [Column("cpf")]
        public string CPF { set; get; }

        [Column("senha")]
        public string Senha { set; get; }

        [Column("tbcargo_id")]
        public int CargoId { set; get; }
        public Cargo Cargo { set; get; }
    

    }
}