using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vayalun.Models
{
    [Table("tbcategoria")]
    public class Categoria
    {
        [Column("id")]
        public int Id { set; get; }
        [Column("nome")]
        public string Nome { set; get; }
    }
}