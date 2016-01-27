using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hotel.DAL;
using Hotel.Models;

namespace Hotel.Controllers
{
    public class ReservacionController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Reservacion
        public ActionResult Index()
        {
            var reservacion = db.reservacion.Include(r => r.habitacion).Include(r => r.huesped);
            return View(reservacion.ToList());
        }

        // GET: Reservacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // GET: Reservacion/Create
        public ActionResult Create()
        {
            ViewBag.habitacionID = new SelectList(db.habitacion, "habitacionID", "tamañoHabitacion");
            ViewBag.huespedID = new SelectList(db.huesped, "huespedID", "nombre");
            return View();
        }

        // POST: Reservacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "reservacionID,fechaDeIngreso,fechaDeSalida,numeroDeHabitacion,habitacionID,huespedID")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.reservacion.Add(reservacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.habitacionID = new SelectList(db.habitacion, "habitacionID", "tamañoHabitacion", reservacion.habitacionID);
            ViewBag.huespedID = new SelectList(db.huesped, "huespedID", "nombre", reservacion.huespedID);
            return View(reservacion);
        }

        // GET: Reservacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.habitacionID = new SelectList(db.habitacion, "habitacionID", "tamañoHabitacion", reservacion.habitacionID);
            ViewBag.huespedID = new SelectList(db.huesped, "huespedID", "nombre", reservacion.huespedID);
            return View(reservacion);
        }

        // POST: Reservacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "reservacionID,fechaDeIngreso,fechaDeSalida,numeroDeHabitacion,habitacionID,huespedID")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.habitacionID = new SelectList(db.habitacion, "habitacionID", "tamañoHabitacion", reservacion.habitacionID);
            ViewBag.huespedID = new SelectList(db.huesped, "huespedID", "nombre", reservacion.huespedID);
            return View(reservacion);
        }

        // GET: Reservacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // POST: Reservacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservacion reservacion = db.reservacion.Find(id);
            db.reservacion.Remove(reservacion);
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
