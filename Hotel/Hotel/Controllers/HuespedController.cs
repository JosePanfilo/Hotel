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
    public class HuespedController : Controller
    {
        private Contexto db = new Contexto();


        public JsonResult AjaxIndex(String strBuscado)
        {
            //var alumnos = db.alumnos.ToList();

            var huespedes = from Huesped in db.Huespeds
                            where Huesped.nombre.Contains(strBuscado)
                         select new
                         {
                             huespedID = Huesped.huespedID,
                             nombre = Huesped.nombre,
                             apellidoP = Huesped.apellidoP,
                             apellidoM = Huesped.apellidoM,
                             telefono = Huesped.telefono,
                            
                         };

            return Json(huespedes, JsonRequestBehavior.AllowGet);
        }
        // GET: Libro
        public ActionResult Index()
        {
            return View(db.Huespeds.ToList());
        }

        // GET: Huesped/Details/5
        public JsonResult Details(int? id)
        {
            /*Un objeto instanciado del modelo de datos*/
            Huesped huesped = db.Huespeds.Find(id);

            /*Necesito una instancia del modelo de vista*/
            //VMAlumno vmAlumno = new VMAlumno(alumno);

            //return Json(vmAlumno, JsonRequestBehavior.AllowGet);
            return Json(huesped, JsonRequestBehavior.AllowGet);
        }

        // GET: Huesped/Create
        public ActionResult Create()
        {
            return View();
        }
         //GET: Huesped/Create
        public ActionResult Create()
          {
                    return View();
                }

                // POST: Huesped/Create
                // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
                // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
                //[ValidateAntiForgeryToken]
           public JsonResult Create(Huesped huesped)
              {
                    if (ModelState.IsValid)
                    {
                        db.huesped.Add(huesped);
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
        [HttpGet]
        public JsonResult AjaxEdit(int libroId = 0)
        {
            /*Un objeto instanciado del modelo de datos*/
            Huesped huesped = db.Huespeds.Find(libroId);

            
            return Json(huesped, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult AjaxEdit(Huesped huesped)
        {
            String mensaje = String.Empty;

            try
            {
                db.Entry(huesped).State = EntityState.Modified;
                int c = db.SaveChanges();
                mensaje = "Se ha editado los datos del libro satisfactoriamente";
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
        [HttpGet]
        public JsonResult DeleteConfirmed(int huespedID = 0)
        {
            String mensaje = String.Empty;
            try
            {
                Huesped huesped = db.Huespeds.Find(huespedID);
                db.Huespeds.Remove(huesped);
                db.SaveChanges();
                mensaje = "Se ha eliminado el libro satisfactoriamente";
            }
            catch (Exception exc)
            {
                mensaje = "Hubo un error en el servidor: " + exc.Message;
            }
            return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AjaxDelete(int huespedID = 0)
        {
            /*Un objeto instanciado del modelo de datos*/
            Huesped huesped = db.Huespeds.Find(huespedID);

           
            return Json(huesped, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AjaxDelete(Huesped huesped)
        {
            String mensaje = String.Empty;


            try
            {
                db.Entry(huesped).State = EntityState.Deleted;
                int c = db.SaveChanges();
                mensaje = "Se ha eliminado libro correctamente";
            }
            catch (Exception exc)
            {
                mensaje = "Hubo un error en el servidor: " + exc.Message;


            }


            //return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);

            return Json("Response from Delete", JsonRequestBehavior.AllowGet);


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


//namespace Hotel.Controllers
//{
//    public class HuespedController : Controller
//    {
//        private Contexto db = new Contexto();
       
//        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
//        public JsonResult AjaxIndex(String strBuscado)
//        {
//            //var huespedes = db.Huespeds.ToList();

//            var huespedes = from Huesped in db.Huespeds
//                            where huesped.nombre.Contains(strBuscado)
//                          select new
//                          {
//                              huespedID = huesped.huespedID,
//                              nombre = huesped.nombre,
//                              apellidoP = huesped.apellidoP,
//                              apellidoM = huesped.apellidoM,
//                              telefono = huesped.telefono
//                          };

//            return Json(huespedes, JsonRequestBehavior.AllowGet);
//        }

//        //public JsonResult EntregarDatos()
//        //{
//        //    JavaScriptSerializer jss = new JavaScriptSerializer();
//        //    String dato = "Esto viene del server";
//        //    return Json(jss.Serialize(dato), JsonRequestBehavior.AllowGet);
//        //}
//         public ActionResult Index()
//        {
//            return View(db.Huespeds.ToList());
//        }
//        // GET: Huesped
//        //Valor 
//        //public ActionResult JsonIndex(String strBuscado = "")
//        //{
//        //    //Se declara una lista de alumnos
//        //    IEnumerable<Huesped> huespedes;

//        //    //Se busca una cadena de caracteres por nombre
//        //    huespedes = db.Huespeds.Where(algo => algo.nombre.Contains(strBuscado));

//        //    IEnumerable<VMHuesped> vmHuesped = from huesped in db.Huespeds
//        //                                       where huesped.nombre.Contains(strBuscado)
//        //                                       select new VMHuesped(huesped);

//        //    //Se envia datos principales a vista
//        //    return View(huespedes.ToList());
//        //}

//        // GET: Huesped
//        //public ActionResult Index(String strBuscado = "")
//        //{
//        //    //Se declara una lista de Huespedes
//        //    IEnumerable<Huesped> huespedes;

//        //    //Se busca una cadena de caracteres por nombre
//        //    huespedes = db.Huespeds.Where(algo => algo.nombre.Contains(strBuscado));

//        //    return View(huespedes.ToList());
//        //}

//        // GET: Huesped/Details/5
//        //public JsonResult Details(int? id)
//        //    {
//        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//        //    }
//        //    Huesped huesped = db.Huespeds.Find(id);
//        //    if (huesped == null)
//        //    {
//        //        return HttpNotFound();
//        //    }
//        //    return View(huesped);
//        //}

//        // GET: Alumno/Details/5
//        public JsonResult Details(int? id)
//        {
//            Huesped huesped = db.Huespeds.Find(id);
//            //VMHuesped vmHuesped = new VMHuesped(huesped);

//            return Json(huesped, JsonRequestBehavior.AllowGet);
//        }

//        // GET: Huesped/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Huesped/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        //[ValidateAntiForgeryToken]
//        public JsonResult Create(Huesped huesped)
//        {
//            if (ModelState.IsValid)
//            {
//                db.huesped.Add(huesped);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(huesped);
//        }

//        // GET: Huesped/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Huesped huesped = db.huesped.Find(id);
//            if (huesped == null)
//            {
//                return HttpNotFound();
//            }
//            return View(huesped);
//        }

//        // POST: Huesped/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        public ActionResult Edit(int? id/*[Bind(Include = "huespedID,nombre,apellidoP,apellidoM,telefono")] Huesped huesped*/)
//        {

//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Huesped huesped = db.Huespeds.Find(id);
//            if (huesped == null)
//            {
//                return HttpNotFound();
//            }
//            return View(huesped);

//            //if (ModelState.IsValid)
//            //{
//            //    db.Entry(huesped).State = EntityState.Modified;
//            //    db.SaveChanges();
//            //    return RedirectToAction("Index");
//            //}
//            //return View(huesped);
//        }

//        // POST: Libro/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "huespedID,nombre,isbn,autor,editorial,descripcion,año,noEjemplares")] Huesped huesped)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(huesped).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(huesped);
//        }

//        [HttpGet]
//        public JsonResult AjaxEdit(int huespedID = 0)
//        {
//            /*Un objeto instanciado del modelo de datos*/
//            Huesped huesped = db.Huespeds.Find(huespedID);

//            /*Necesito una instancia del modelo de vista*/
//            VMHuesped vmHuesped = new VMHuesped(huesped);

//            //return Json(vmAlumno, JsonRequestBehavior.AllowGet);
//            return Json(vmHuesped, JsonRequestBehavior.AllowGet);
//        }

//        [HttpPost]
//        public JsonResult AjaxEdit(Huesped huesped)
//        {
//            String mensaje = String.Empty;

//            try
//            {
//                db.Entry(huesped).State = EntityState.Modified;
//                int c = db.SaveChanges();
//                mensaje = "Se han editado los datos del huesped satisfactoriamente";
//            }
//            catch (Exception exc)
//            {
//                mensaje = "Hubo un error en el servidor: " + exc.Message;
//            }


//            return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
//        }

//        // GET: Huesped/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Huesped huesped = db.huesped.Find(id);
//            if (huesped == null)
//            {
//                return HttpNotFound();
//            }
//            return View(huesped);
//        }

//        // POST: Huesped/Delete/5
//        //[HttpPost, ActionName("Delete")]
//        //[ValidateAntiForgeryToken]
//        [HttpGet]
//        public JsonResult DeleteConfirmed(int libroId = 0)
//        {
//            Huesped huesped = db.Huespeds.Find(id);
//            db.Huespeds.Remove(huesped);
//            db.SaveChanges();
//                mensaje = "Se ha eliminado el libro satisfactoriamente";
//            }
//            catch (Exception exc)
//            {
//                mensaje = "Hubo un error en el servidor: " + exc.Message;
//            }
//            return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
//        }

//        [HttpGet]
//        public JsonResult AjaxDelete(int huespedID = 0)
//        {
//            /*Un objeto instanciado del modelo de datos*/
//            Huesped huesped = db.Huespeds.Find(huespedID);

//            /*Necesito una instancia del modelo de vista*/
//            //VMAlumno vmAlumno = new VMAlumno(alumno);

//            //return Json(vmAlumno, JsonRequestBehavior.AllowGet);
//            return Json(huesped, JsonRequestBehavior.AllowGet);
//        }

//        // GET: Libro/Delete/5
//        //public ActionResult Delete(int? id)
//        //{
//        //    if (id == null)
//        //    {
//        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//        //    }
//        //    Huesped huesped = db.Huespeds.Find(id);
//        //    if (huesped == null)
//        //    {
//        //        return HttpNotFound();
//        //    }
//        //    return View(huesped);
//        //}

//        [HttpPost]
//        public JsonResult AjaxDelete(Huesped huesped)
//        {
//            String mensaje = String.Empty;


//            try
//            {
//                db.Entry(huesped).State = EntityState.Deleted;
//                int c = db.SaveChanges();
//                mensaje = "Se ha eliminado libro correctamente";
//            }
//            catch (Exception exc)
//            {
//                mensaje = "Hubo un error en el servidor: " + exc.Message;


//            }


//            //return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);

//            return Json("Response from Delete", JsonRequestBehavior.AllowGet);

//        }

//        //public ActionResult DeleteConfirmed(int id)
//        //{
//        //    Huesped huesped = db.Huespeds.Find(id);
//        //    db.Huespeds.Remove(huesped);
//        //    db.SaveChanges();
//        //    return RedirectToAction("Index");
//        //}

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }


