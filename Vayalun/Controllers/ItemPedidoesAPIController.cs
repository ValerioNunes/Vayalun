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
    public class ItemPedidoesAPIController : ApiController
    {
        private VayalunContext db = new VayalunContext();

        // GET: api/ItemPedidoesAPI
        public IQueryable<ItemPedido> GetItemPedidoes()
        {
            return db.ItemPedidoes;
        }

        // GET: api/ItemPedidoesAPI/5
        [ResponseType(typeof(List<ItemPedido>))]
        public IHttpActionResult GetItemPedido(int id)
        {
            List<ItemPedido> itemPedidos = db.ItemPedidoes.Include(s => s.ItemCardapio).Where(x => x.PedidoId == id).ToList();
            if (itemPedidos == null)
            {
                return NotFound();
            }

            return Json(itemPedidos);
        }

        // PUT: api/ItemPedidoesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItemPedido(int id, ItemPedido itemPedido)
        {
            if (itemPedido != null)
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
            return Ok();
        }

        // POST: api/ItemPedidoesAPI
        [HttpOptions, HttpPost]
        [ResponseType(typeof(ItemPedido))]
        public IHttpActionResult PostItemPedido(ItemPedido itemPedido)
        {
            if (itemPedido != null)
            {
             
                try
                {
                    if (itemPedido.Id == 0)
                    {
                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }

                        db.ItemPedidoes.Add(itemPedido);
                        db.SaveChanges();

                        return CreatedAtRoute("DefaultApi", new { id = itemPedido.Id }, itemPedido);
                    }
                    else
                    {
                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }

                        db.Entry(itemPedido).State = EntityState.Modified;

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ItemPedidoExists(itemPedido.Id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }

                        return Json("Sucessagem");
                    }
                }
                catch (Exception e)
                {
                    DebugLog.Logar(e.Message);
                    DebugLog.Logar(e.StackTrace);
                    return Json(e.Message);
                }
            }
            return Ok();

        }

        // DELETE: api/ItemPedidoesAPI/5
        [ResponseType(typeof(ItemPedido))]
        public IHttpActionResult DeleteItemPedido(int? id)
        {
            try
            {
                if (id != null)
                {
                    DebugLog.Logar(id.ToString());
                    ItemPedido itemPedido = db.ItemPedidoes.Where(x => x.Id == id).FirstOrDefault();
                    if (itemPedido == null)
                    {
                        return Ok("itemPedido == null");
                    }

                    db.ItemPedidoes.Remove(itemPedido);
                    db.SaveChanges();
                }
                return Ok();
            }
            catch (Exception e)
            {
                DebugLog.Logar(e.Message);
                DebugLog.Logar(e.StackTrace);
                return Json("Erro");
            }
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