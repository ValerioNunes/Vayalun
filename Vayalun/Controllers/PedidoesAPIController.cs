using HelperDinamico.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Vayalun.DAL;
using Vayalun.Models;

namespace Vayalun.Controllers
{
    public class PedidoesAPIController : ApiController
    {
        private VayalunContext db = new VayalunContext();

        // GET: api/PedidoesAPI
        public IQueryable<Pedido> GetPedidoes()
        {
            return db.Pedidoes.Include(i => i.Cliente)
                              .Include(i => i.Funcionario)
                              .Include(i => i.Mesa);

        }

        // GET: api/PedidoesAPI/5
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult GetPedido(int id)
        {
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            return Json(pedido);
        }

        // PUT: api/PedidoesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPedido(int id, Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pedido.Id)
            {
                return BadRequest();
            }

            db.Entry(pedido).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PedidoesAPI
        [HttpOptions, HttpPost]
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult PostPedido(Pedido pedido)
        {
            if (pedido != null)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    db.Pedidoes.Add(pedido);
                    db.SaveChanges();

                    return CreatedAtRoute("DefaultApi", new { id = pedido.Id }, pedido);
                }
                catch (Exception e)
                {
                    DebugLog.Logar(e.Message);
                    DebugLog.Logar(e.StackTrace);
                    return Json("Erro ao cadastrar solicitação!");
                }
            }

            return Ok();
        }

        // DELETE: api/PedidoesAPI/5
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult DeletePedido(int id)
        {
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            db.Pedidoes.Remove(pedido);
            db.SaveChanges();

            return Ok(pedido);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PedidoExists(int id)
        {
            return db.Pedidoes.Count(e => e.Id == id) > 0;
        }

       
        
    }
}