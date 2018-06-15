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
    public class CargoesController : ApiController
    {
        private VayalunContext db = new VayalunContext();

        // GET: api/Cargoes
        public IQueryable<Cargo> GetCargoes()
        {
            return db.Cargoes;
        }

        // GET: api/Cargoes/5
        [ResponseType(typeof(Cargo))]
        public IHttpActionResult GetCargo(int id)
        {
            Cargo cargo = db.Cargoes.Find(id);
            if (cargo == null)
            {
                return NotFound();
            }

            return Ok(cargo);
        }

        // PUT: api/Cargoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCargo(int id, Cargo cargo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cargo.Id)
            {
                return BadRequest();
            }

            db.Entry(cargo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CargoExists(id))
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

        // POST: api/Cargoes
        [ResponseType(typeof(Cargo))]
        public IHttpActionResult PostCargo(Cargo cargo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cargoes.Add(cargo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cargo.Id }, cargo);
        }

        // DELETE: api/Cargoes/5
        [ResponseType(typeof(Cargo))]
        public IHttpActionResult DeleteCargo(int id)
        {
            Cargo cargo = db.Cargoes.Find(id);
            if (cargo == null)
            {
                return NotFound();
            }

            db.Cargoes.Remove(cargo);
            db.SaveChanges();

            return Ok(cargo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CargoExists(int id)
        {
            return db.Cargoes.Count(e => e.Id == id) > 0;
        }
    }
}