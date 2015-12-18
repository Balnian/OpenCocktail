using OpenCocktail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenCocktail.Controllers
{
    public class CocktailsController : Controller
    {
        //
        // GET: /Cocktails/
        public ActionResult Index()
        {
            Cocktails cock = new Cocktails(Session["DB"].ToString());
            cock.SelectAll();
            return View(cock.ToList());
        }

        //
        // GET: /Cocktails/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Cocktails/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cocktails/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Cocktails/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Cocktails/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Cocktails/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Cocktails/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
