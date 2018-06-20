using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Vayalun.DAL
{
  
        public class VayalunContext : DbContext
        {

             public  VayalunContext() : base("name=VayalunContext")
            {

            }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                base.OnModelCreating(modelBuilder);
            }

        public System.Data.Entity.DbSet<Vayalun.Models.Cargo> Cargoes { get; set; }

        public System.Data.Entity.DbSet<Vayalun.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<Vayalun.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<Vayalun.Models.Funcionario> Funcionarios { get; set; }

        public System.Data.Entity.DbSet<Vayalun.Models.ItemCardapio> ItemCardapios { get; set; }

        public System.Data.Entity.DbSet<Vayalun.Models.Mesa> Mesas { get; set; }

        public System.Data.Entity.DbSet<Vayalun.Models.Pedido> Pedidoes { get; set; }

        public System.Data.Entity.DbSet<Vayalun.Models.ItemPedido> ItemPedidoes { get; set; }
    }
    }
