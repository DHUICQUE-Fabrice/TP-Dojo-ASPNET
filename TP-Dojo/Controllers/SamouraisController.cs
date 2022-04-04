using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using TP_Dojo.Models;

namespace TP_Dojo.Controllers
{
    public class SamouraisController : Controller
    {
        private Context db = new Context();
        private List<Samourai> samouraiList = new List<Samourai>();
        private List<Arme> armes = new List<Arme>();

        private ArmesSamouraisVM GetArmesSamouraisVM(int? id)
        {

            ArmesSamouraisVM armesSamouraisVM = new ArmesSamouraisVM();
            armesSamouraisVM.Samourai = db.Samourais.FirstOrDefault(s=>s.Id == id);
            armesSamouraisVM.Samourai.Arme = db.Armes.FirstOrDefault(s=>s.Id == armesSamouraisVM.IdSelectedArme);
            return armesSamouraisVM;
        }
        // GET: Samourais
        public ActionResult Index()
        {
            ArmesSamouraisVM armesSamouraisVM = new ArmesSamouraisVM();
            List<ArmesSamouraisVM> armesSamouraisVMList = new List<ArmesSamouraisVM>();
            foreach (Samourai samourai in db.Samourais)
            {
                armesSamouraisVMList.Add(GetArmesSamouraisVM(samourai.Id));
            }
            return View(armesSamouraisVMList);
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int id)
        {            
            var vm = GetArmesSamouraisVM(id);

            if (vm == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            ArmesSamouraisVM armesSamouraisVM = new ArmesSamouraisVM();
            armesSamouraisVM.Armes = db.Armes.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() }).ToList();
            return View(armesSamouraisVM);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArmesSamouraisVM armesSamouraisVM)
        {

            if (ModelState.IsValid)
            {
                Samourai samourai = armesSamouraisVM.Samourai;
                samourai.Arme = db.Armes.FirstOrDefault(a => a.Id == armesSamouraisVM.IdSelectedArme);
                db.Samourais.Add(samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(armesSamouraisVM);
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id)
        {
            ArmesSamouraisVM armesSamouraisVM = new ArmesSamouraisVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            armesSamouraisVM.Samourai = db.Samourais.FirstOrDefault(i=> i.Id == id);
            
            if (armesSamouraisVM.Samourai == null)
            {
                return HttpNotFound();
            }
            armesSamouraisVM.Armes = db.Armes.Select(i=> new SelectListItem { Text = i.Nom, Value = i.Id.ToString() }).ToList();   
            if(armesSamouraisVM.Samourai.Arme != null)
            {
                armesSamouraisVM.IdSelectedArme = armesSamouraisVM.Samourai.Arme.Id;
            }

            return View(armesSamouraisVM);
        }

        // POST: Samourais/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArmesSamouraisVM armesSamouraisVM)
        {
            if (ModelState.IsValid)
            {
                Samourai samourai = db.Samourais.FirstOrDefault(s=> s.Id == armesSamouraisVM.Samourai.Id);
                samourai.Nom = armesSamouraisVM.Samourai.Nom;
                samourai.Force = armesSamouraisVM.Samourai.Force;
                samourai.Arme = (armesSamouraisVM.IdSelectedArme == null)
                    ? null
                    : db.Armes.FirstOrDefault(a => a.Id == armesSamouraisVM.IdSelectedArme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(armesSamouraisVM.Samourai.Id);
        }

        // GET: Samourais/Delete/5
        public ActionResult Delete(int? id)
        {
            ArmesSamouraisVM armesSamouraisVM = GetArmesSamouraisVM(id);
            if (armesSamouraisVM == null)
            {
                return HttpNotFound();
            }
            return View(armesSamouraisVM);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
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
