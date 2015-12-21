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
            Cocktails cock = new Cocktails(Session["DB"].ToString());
            cock.SelectByID(id);

            Ingredients ingrs = new Ingredients(Session["DB"].ToString());
            ingrs.SelectIngredientsByID(id);
            List<Ingredient> ingr = new List<Ingredient>(ingrs.ToList());

            //////////////////////////////////////////////////////////TEMPORAIRE
            /*<string> ListeIngredients = new List<string>();
            ListeIngredients.Add("5oz - chien");
            ListeIngredients.Add("1/4cup - chat");
            ListeIngredients.Add("6g - Moufette");
            ListeIngredients.Add("6tps - Rats");
            ListeIngredients.Add("7oz - Mouette");*/
            //////////////////////////////////////////////////////////

            ViewData["ingrlist"] = ingr;
            return View(cock.Cocktail);
        }

        //
        // GET: /Cocktails/Create
        public ActionResult Create()
        {
            Ingredients ing = new Ingredients(Session["DB"].ToString());
            ing.SelectAll();
            ViewData["Ingredients"] = ing.ToList();
            return View(new Cocktail());
        }

        //
        // POST: /Cocktails/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Cocktails cock = new Cocktails(Session["DB"].ToString());
                cock.Cocktail.Nom = collection["Nom"];
                cock.Cocktail.Description = collection["Description"];
                cock.Cocktail.Image = collection["FU_Image"];
                cock.Cocktail.UpLoadPoster(Request);
                cock.Insert();
                cock.SelectLast();
                Ingredients ing = new Ingredients(Session["DB"].ToString());
                Composants comp = new Composants(Session["DB"].ToString());
                foreach (var item in collection["Ingredients"].ToString().Split(','))
	            {
                    ing.SelectByID(item);

                    comp.Composant.Id_Ingredient = ing.Ingredient.Id;
                    comp.Composant.Id_Cocktail = cock.Cocktail.Id;
                    comp.Composant.Qte = long.Parse(collection[ing.Ingredient.Id.ToString()].ToString());
                    comp.Insert();
	            }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
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
