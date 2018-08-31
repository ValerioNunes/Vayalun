using HelperDinamico.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Vayalun.DAL;
using Vayalun.Models;
using System.Configuration;

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
                List<Pedido> lvpedidos = db.Pedidoes.Where(y => y.MesaId == x.Id && y.Status.Equals("AGUARDANDO") || y.Status.Equals("ENTREGUE") || y.Status.Equals("CONSUMO_FINALIZADO")).ToList();


                vlmesaView.Status = (lvpedidos.Count <= 0) ? ConfigurationManager.AppSettings["LIVRE"].ToString() : (lvpedidos.FirstOrDefault().Status == "CONSUMO_FINALIZADO") ? "CONSUMO_FINALIZADO" : ConfigurationManager.AppSettings["OCUPADO"].ToString();


                mesaViews.Add(vlmesaView);
            });
            
            return Json(mesaViews);
        }

        // GET: api/MesaView/5
        [ResponseType(typeof(List<Pedido>))]
        public IHttpActionResult Get(int id)
        {
            var lvpedidos = db.Pedidoes.Include(i => i.Cliente)
                           .Include(i => i.Funcionario)
                           .Include(i => i.Mesa)

                           .Where( x => x.MesaId == id && !x.Status.Equals("PAGO") && !x.Status.Equals("CANCELADO")).ToList();
            return Json(lvpedidos);
        }

        // POST: api/MesaView
        [HttpOptions, HttpPost]
        [ResponseType(typeof(RegisterCredentials))]
        public IHttpActionResult Post(RegisterCredentials registerCredentials)
        {
            if (registerCredentials != null)
            {
                try
                {

                    DebugLog.Logar(registerCredentials.Id.ToString());
                    Funcionario funcionario = db.Funcionarios.Where(x => x.Id == registerCredentials.Id && x.Senha == registerCredentials.Senha).Include(f => f.Cargo).FirstOrDefault();

                    if (funcionario == null)
                    {
                        return Json("invalido");
                    }

                        return Json(funcionario);
                    
                }
                catch (Exception e)
                {
                    DebugLog.Logar(e.Message);
                    DebugLog.Logar(e.StackTrace);
                    return Json("Erro ao cadastrar solicitação!");
                }
            }
                return Ok("registerCredentials Null");
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
