using OpenCocktail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenCocktail.Controllers
{
    public class IngredientsController : Controller
    {
        //
        // GET: /Ingredients/
        public ActionResult Index()
        {
            Ingredients ingredients = new Ingredients(Session["DB"].ToString());
            ingredients.SelectAll();
            return View(ingredients.ToList());
        }

        //
        // GET: /Ingredients/Details/5
        public ActionResult Details(int id)
        {
            Ingredients ingredients = new Ingredients(Session["DB"].ToString());
            ingredients.SelectByID(id);
            return View(ingredients.Ingredient);
        }

        //
        // GET: /Ingredients/Create
        public ActionResult Create()
        {
            return View(new Ingredient());
        }

        //
        // POST: /Ingredients/Create
        [HttpPost]
        public ActionResult Create(Ingredient collection)
        {
            try
            {
                Ingredients ing = new Ingredients(Session["DB"].ToString());
                ing.Ingredient = collection;
                ing.Insert();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        //
        // GET: /Ingredients/Edit/5
        public ActionResult Edit(int id)
        {
            Ingredients ingredients = new Ingredients(Session["DB"].ToString());
            if (ingredients.SelectByID(id))
                return View(ingredients.Ingredient);
            else
                return RedirectToAction("Index");
        }

        //
        // POST: /Ingredients/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Ingredient collection)
        {
            Ingredients ingredients = new Ingredients(Session["DB"].ToString());

            if (ModelState.IsValid)
            {
                if (ingredients.SelectByID(id))
                {
                    ingredients.Ingredient = collection;
                    ingredients.Update();
                    return RedirectToAction("Index");
                }
            }
            return View(collection);
        }

        //
        // GET: /Ingredients/Delete/5
        public ActionResult Delete(int id)
        {
            Ingredients ingredients = new Ingredients(Session["DB"].ToString());
            if (ingredients.SelectByID(id))
                return View(ingredients.Ingredient);
            else
                return RedirectToAction("Index");
        }

        //
        // POST: /Ingredients/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Ingredient collection)
        {
            Ingredients ingredients = new Ingredients(Session["DB"].ToString());

            try
            {
                if (ingredients.SelectByID(id))
                {
                    ingredients.Ingredient = collection;
                    ingredients.DeleteRecordByID(id);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(collection);
            }            
        }
    }
}
