using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vayalun.DAL;
using Vayalun.Models;

namespace Vayalun.Controllers
{
    public class ItemCardapiosController : Controller
    {
        private VayalunContext db = new VayalunContext();

        // GET: ItemCardapios
        public ActionResult Index()
        {
            var itemCardapios = db.ItemCardapios.Include(i => i.Categoria);
            return View(itemCardapios.ToList());
        }

        // GET: ItemCardapios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCardapio itemCardapio = db.ItemCardapios.Find(id);
            if (itemCardapio == null)
            {
                return HttpNotFound();
            }
            return View(itemCardapio);
        }

        // GET: ItemCardapios/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome");
            return View();
        }

        // POST: ItemCardapios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Descricao,Imagem,Preco,CategoriaId")] ItemCardapio itemCardapio)
        {
            if (ModelState.IsValid)
            {
                db.ItemCardapios.Add(itemCardapio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", itemCardapio.CategoriaId);
            return View(itemCardapio);
        }

        // GET: ItemCardapios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCardapio itemCardapio = db.ItemCardapios.Find(id);
            if (itemCardapio == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", itemCardapio.CategoriaId);
            return View(itemCardapio);
        }

        // POST: ItemCardapios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao,Imagem,Preco,CategoriaId")] ItemCardapio itemCardapio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemCardapio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", itemCardapio.CategoriaId);
            return View(itemCardapio);
        }

        // GET: ItemCardapios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCardapio itemCardapio = db.ItemCardapios.Find(id);
            if (itemCardapio == null)
            {
                return HttpNotFound();
            }
            return View(itemCardapio);
        }

        // POST: ItemCardapios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemCardapio itemCardapio = db.ItemCardapios.Find(id);
            db.ItemCardapios.Remove(itemCardapio);
            db.SaveChanges();
            return RedirectToAction("Index");
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
