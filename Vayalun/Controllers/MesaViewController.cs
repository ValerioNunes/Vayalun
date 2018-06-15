using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Vayalun.DAL;
using Vayalun.Models;

namespace Vayalun.Controllers
{
    public class MesaViewController : ApiController
    {
        private VayalunContext db = new VayalunContext();
        // GET: api/MesaView
        [ResponseType(typeof(List<MesaView>))]
        public IHttpActionResult Get()
        {
            List<MesaView> mesaViews = new List<MesaView>();
            db.Mesas.ToList().ForEach ( x =>
            {
                MesaView vlmesaView = new MesaView();
                vlmesaView.Id = x.Id;
                vlmesaView.Nome = x.Nome;
                vlmesaView.Status = "livre";
                mesaViews.Add(vlmesaView);
            });

            return Json(mesaViews);
        }

        // GET: api/MesaView/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MesaView
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MesaView/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MesaView/5
        public void Delete(int id)
        {
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
