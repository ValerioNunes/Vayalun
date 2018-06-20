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
    public class ItemPedidoesAPIController : ApiController
    {
        private VayalunContext db = new VayalunContext();

        // GET: api/ItemPedidoesAPI
        public IQueryable<ItemPedido> GetItemPedidoes()
        {
            return db.ItemPedidoes;
        }

        // GET: api/ItemPedidoesAPI/5
        [ResponseType(typeof(ItemPedido))]
        public IHttpActionResult GetItemPedido(int id)
        {
            ItemPedido itemPedido = db.ItemPedidoes.Find(id);
            if (itemPedido == null)
            {
                return NotFound();
            }

            return Ok(itemPedido);
        }

        // PUT: api/ItemPedidoesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItemPedido(int id, ItemPedido itemPedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemPedido.Id)
            {
                return BadRequest();
            }

            db.Entry(itemPedido).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemPedidoExists(id))
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

        // POST: api/ItemPedidoesAPI
        [ResponseType(typeof(ItemPedido))]
        public IHttpActionResult PostItemPedido(ItemPedido itemPedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ItemPedidoes.Add(itemPedido);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = itemPedido.Id }, itemPedido);
        }

        // DELETE: api/ItemPedidoesAPI/5
        [ResponseType(typeof(ItemPedido))]
        public IHttpActionResult DeleteItemPedido(int id)
        {
            ItemPedido itemPedido = db.ItemPedidoes.Find(id);
            if (itemPedido == null)
            {
                return NotFound();
            }

            db.ItemPedidoes.Remove(itemPedido);
            db.SaveChanges();

            return Ok(itemPedido);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemPedidoExists(int id)
        {
            return db.ItemPedidoes.Count(e => e.Id == id) > 0;
        }
    }
}