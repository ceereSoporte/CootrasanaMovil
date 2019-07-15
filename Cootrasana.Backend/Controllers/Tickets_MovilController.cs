using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cootrasana.Backend.Models;
using Cootrasana.Common.Models;

namespace Cootrasana.Backend.Controllers
{
    public class Tickets_MovilController : Controller
    {
        private LocalContext db = new LocalContext();

        // GET: Tickets_Movil
        public async Task<ActionResult> Index()
        {
            return View(await db.Tickets_Movil.ToListAsync());
        }

        // GET: Tickets_Movil/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets_Movil tickets_Movil = await db.Tickets_Movil.FindAsync(id);
            if (tickets_Movil == null)
            {
                return HttpNotFound();
            }
            return View(tickets_Movil);
        }

        // GET: Tickets_Movil/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tickets_Movil/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idTicket,Origen,Destino,NoPersonas,ValTickets,Encomienda")] Tickets_Movil tickets_Movil)
        {
            if (ModelState.IsValid)
            {
                tickets_Movil.Fecha = DateTime.Now;
                db.Tickets_Movil.Add(tickets_Movil);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tickets_Movil);
        }

        // GET: Tickets_Movil/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets_Movil tickets_Movil = await db.Tickets_Movil.FindAsync(id);
            if (tickets_Movil == null)
            {
                return HttpNotFound();
            }
            return View(tickets_Movil);
        }

        // POST: Tickets_Movil/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idTicket,Origen,Destino,NoPersonas,ValTicket,Encomienda,Fecha")] Tickets_Movil tickets_Movil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tickets_Movil).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tickets_Movil);
        }

        // GET: Tickets_Movil/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets_Movil tickets_Movil = await db.Tickets_Movil.FindAsync(id);
            if (tickets_Movil == null)
            {
                return HttpNotFound();
            }
            return View(tickets_Movil);
        }

        // POST: Tickets_Movil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tickets_Movil tickets_Movil = await db.Tickets_Movil.FindAsync(id);
            db.Tickets_Movil.Remove(tickets_Movil);
            await db.SaveChangesAsync();
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
