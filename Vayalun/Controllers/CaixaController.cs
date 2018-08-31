using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vayalun.DAL;
using Vayalun.Models;
using System.Data.Entity;
using System.Collections;
using System.Web.Http.Description;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using HelperDinamico.Extension;

namespace Vayalun.Controllers
{
    public class CaixaController : ApiController
    {
        private VayalunContext db = new VayalunContext();
        [ResponseType(typeof(List<MesaView>))]
        public IHttpActionResult Get()
        {
            List<MesaView> mesaViews = new List<MesaView>();
            db.Mesas.ToList().ForEach(x =>
            {
                MesaView vlmesaView = new MesaView();
                vlmesaView.Id = x.Id;
                vlmesaView.Nome = x.Nome;
                List<Pedido> lvpedidos = db.Pedidoes.Where(y => y.MesaId == x.Id && y.Status.Equals("CONSUMO_FINALIZADO")).ToList();

                vlmesaView.Status = "CONSUMO_FINALIZADO";// (lvpedidos.Count <= 0) ? ConfigurationManager.AppSettings["LIVRE"].ToString() : (lvpedidos.FirstOrDefault().Status == "CONSUMO_FINALIZADO")? "CONSUMO_FINALIZADO" : ConfigurationManager.AppSettings["OCUPADO"].ToString();

                mesaViews.Add(vlmesaView);
            });

            return Json(mesaViews);
        }
        [AcceptVerbs("GET")]
        [ResponseType(typeof(List<ItemPedido>))]
        public IHttpActionResult Get(int id)
        {
            if (id != 0)
            {
                List<ItemPedido> itemPedidos = new List<ItemPedido>();


                List<Pedido> lvpedidos = db.Pedidoes.Include(i => i.Cliente)
                   .Include(i => i.Funcionario)
                   .Include(i => i.Mesa)
                   .Where(x => x.MesaId == id && (x.Status.Equals("ENTREGUE") || x.Status.Equals("CONSUMO_FINALIZADO"))).ToList();
                try
                {
                    lvpedidos.ForEach(X => {
                    X.Status = "CONSUMO_FINALIZADO";
                    db.Entry(X).State = EntityState.Modified;
                    List<ItemPedido> vlitemPedidos = db.ItemPedidoes.Include(i => i.ItemCardapio).Where(y => y.PedidoId == X.Id).ToList();
                    vlitemPedidos.ForEach(j =>
                    {
                        itemPedidos.Add(j);
                    });

                    db.Entry(X).State = EntityState.Modified;
                });


                  
                    db.SaveChanges();
                    return Json(itemPedidos);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    DebugLog.Logar(e.Message);
                    DebugLog.Logar(e.StackTrace);
                    return Json(e.Message);
                }
                
            }

            return null;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
