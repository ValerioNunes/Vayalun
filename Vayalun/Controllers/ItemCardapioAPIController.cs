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
    public class ItemCardapioAPIController : ApiController
    {
        private VayalunContext db = new VayalunContext();

        // GET: api/ItemCardapioAPI
        public IQueryable<ItemCardapio> GetItemCardapios()
        {
            return db.ItemCardapios;
        }

        // GET: api/ItemCardapioAPI/5
        [ResponseType(typeof(ItemCardapio))]
        public IHttpActionResult GetItemCardapio(int id)
        {
            ItemCardapio itemCardapio = db.ItemCardapios.Find(id);
            if (itemCardapio == null)
            {
                return NotFound();
            }

            return Ok(itemCardapio);
        }

        // PUT: api/ItemCardapioAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItemCardapio(int id, ItemCardapio itemCardapio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemCardapio.Id)
            {
                return BadRequest();
            }

            db.Entry(itemCardapio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCardapioExists(id))
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

        // POST: api/ItemCardapioAPI
        [ResponseType(typeof(ItemCardapio))]
        public IHttpActionResult PostItemCardapio(ItemCardapio itemCardapio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ItemCardapios.Add(itemCardapio);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = itemCardapio.Id }, itemCardapio);
        }

        // DELETE: api/ItemCardapioAPI/5
        [ResponseType(typeof(ItemCardapio))]
        public IHttpActionResult DeleteItemCardapio(int id)
        {
            ItemCardapio itemCardapio = db.ItemCardapios.Find(id);
            if (itemCardapio == null)
            {
                return NotFound();
            }

            db.ItemCardapios.Remove(itemCardapio);
            db.SaveChanges();

            return Ok(itemCardapio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemCardapioExists(int id)
        {
            return db.ItemCardapios.Count(e => e.Id == id) > 0;
        }
    }
}