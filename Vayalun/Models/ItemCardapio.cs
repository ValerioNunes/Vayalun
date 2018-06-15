using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vayalun.Models
{
    [Table("tbitemCardapio")]
    public class ItemCardapio
    {
        [Column("id")]
        public int Id { set; get; }
        [Column("nome")]
        public string Nome { set; get; }
        [Column("descricao")]
        public string Descricao { set; get; }
        [Column("img")]
        public string Imagem { set; get; }
        [Column("preco")]
        public Double Preco { set; get; }

        [Column("tbcategoria_id")]
        public int CategoriaId { set; get; }
        public Categoria Categoria { set; get; }

    }
}