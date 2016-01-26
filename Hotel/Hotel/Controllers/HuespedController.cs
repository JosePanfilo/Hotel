using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;
using System.Web.Script.Serialization;

namespace Hotel.Controllers
{
    public class HuespedController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult PruebaAjax()
        {
            return View();
        }

        // GET: Huesped
        public ActionResult Index()
        {
            return View(db.Huespeds.ToList());
        }

        // GET: Huesped/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Huesped huesped = db.Huespeds.Find(id);
            if (huesped == null)
            {
                return HttpNotFound();
            }
            return View(huesped);
        }
        //GET: Huesped/Details/5
        public JsonResult AjaxDetails(int? id)
        {
            Huesped huesped = db.Huespeds.Find(id);
            VMHuesped vmHuesped = new VMHuesped(huesped);
        }

        // GET: Huesped/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Huesped/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "huespedID,nombre,apellidoP,apellidoM,telefono")] Huesped huesped)
        {
            if (ModelState.IsValid)
            {
                db.Huespeds.Add(huesped);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(huesped);
        }

        // GET: Huesped/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Huesped huesped = db.Huespeds.Find(id);
            if (huesped == null)
            {
                return HttpNotFound();
            }

            return View(huesped);
        }

        // POST: Huesped/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "huespedID,nombre,apellidoP,apellidoM,telefono")] Huesped huesped)
        {
            if (ModelState.IsValid)
            {
                db.Entry(huesped).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(huesped);
        }
        public JsonResult AjaxEdit(int huespedID = 0)
        {
            /*Un objeto instanciado del modelo de datos*/
            Huesped huesped = db.Huespeds.Find(huespedID);

            /*Necesito una instancia del modelo de vista*/
            VMHuesped vmHuesped = new VMHuesped(huesped);

            return Json(vmHuesped, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AjaxEdit(int huespedID = 0)
        {
            /*Un objeto instanciado del modelo de datos*/
            Huesped huesped = db.Huespeds.Find(huespedID);

            /*Necesito una instancia del modelo de vista*/
            VMHuesped vmHuesped = new VMHuesped(huesped);

            //return Json(vmAlumno, JsonRequestBehavior.AllowGet);
            return Json(vmHuesped, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AjaxEdit(Huesped huesped)
        {
            String mensaje = String.Empty;

            try
            {
                db.Entry(huesped).State = EntityState.Modified;
                int c = db.SaveChanges();
                mensaje = "Se ha editado los datos del alumno satisfactoriamente";
            }
            catch (Exception exc)
            {
                mensaje = "Hubo un error en el servidor: " + exc.Message;
            }


            return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // GET: Huesped/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Huesped huesped = db.Huespeds.Find(id);
            if (huesped == null)
            {
                return HttpNotFound();
            }
            return View(huesped);
        }

        // POST: Huesped/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Huesped huesped = db.Huespeds.Find(id);
            db.Huespeds.Remove(huesped);
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
