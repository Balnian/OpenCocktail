﻿using OpenCocktail.Models;
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
                //Insert cock
                Cocktails cock = new Cocktails(Session["DB"].ToString());
                cock.Cocktail.Nom = collection["Nom"];
                cock.Cocktail.Description = collection["Description"];
                cock.Cocktail.Image = collection["FU_Image"];
                cock.Cocktail.UpLoadPoster(Request);
                cock.Insert();
                cock.SelectLast();

                //Insert Ingredients
                Ingredients ing = new Ingredients(Session["DB"].ToString());
                Composants comp = new Composants(Session["DB"].ToString());
                //if (collection["Ingredients"].ToString().Split(',').Length > 0)
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
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Cocktails/Edit/5
        public ActionResult Edit(int id)
        {
            Cocktails cock = new Cocktails(Session["DB"].ToString());
            cock.SelectByID(id);

            Composants comp = new Composants(Session["DB"].ToString());

            comp.SelectByFieldName(@"Id_Cocktail", id);
            ViewData["Comp"] = comp.ToList();

            Ingredients ingrs = new Ingredients(Session["DB"].ToString());
            ingrs.SelectIngredientsByID(id);
            ingrs.SelectAll();
            ViewData["Ingredients"] = ingrs.ToList();
            return View(cock.Cocktail);
        }

        //
        // POST: /Cocktails/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                //Insert cock
                Cocktails cock = new Cocktails(Session["DB"].ToString());
                cock.Cocktail.Id = id;
                cock.Cocktail.Nom = collection["Nom"];
                cock.Cocktail.Description = collection["Description"];
                cock.Cocktail.Image = collection["FU_Image"];
                cock.Cocktail.UpLoadPoster(Request);
                cock.Update();

                //Update Ingredients
                Ingredients ing = new Ingredients(Session["DB"].ToString());
                Composants comp = new Composants(Session["DB"].ToString());
                comp.SelectByFieldName("Id_Cocktail",cock.Cocktail.Id);
                List<Composant> compList = comp.ToList();

                //if (collection["Ingredients"] != null)
                if(collection["Ingredients"].ToString().Split(',').Length>0)
                foreach (var item in collection["Ingredients"].ToString().Split(','))
                {
                    //Si  existe déjà update sinon insert
                    if (compList.Count(X => X.Id_Ingredient == long.Parse(item)) > 0)
                    {
                        comp.Composant = compList.Where(X => X.Id_Ingredient == long.Parse(item)).First();
                        comp.Composant.Qte = long.Parse(collection[item.ToString()].ToString());
                        comp.Update();
                        foreach (var todel in compList.Where(X => X.Id_Ingredient == long.Parse(item)).Reverse())
                        {
                            compList.Remove(todel);
                        }
                    }
                    else
                    {
                        ing.SelectByID(item);

                        comp.Composant.Id_Ingredient = ing.Ingredient.Id;
                        comp.Composant.Id_Cocktail = cock.Cocktail.Id;
                        comp.Composant.Qte = long.Parse(collection[ing.Ingredient.Id.ToString()].ToString());
                        comp.Insert();
                    }
                }
                //delete remaining composants
                foreach (var item in compList)
                {
                    comp.DeleteRecordByID(item.Id);
 
                }
                    
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Cocktails/Delete/5
        public ActionResult Delete(int id)
        {
            Cocktails cock = new Cocktails(Session["DB"].ToString());
            if (cock.SelectByID(id))
                return View(cock.Cocktail);
            else
                return RedirectToAction("Index");
        }

        //
        // POST: /Cocktails/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Cocktail collection)
        {
            Cocktails cock = new Cocktails(Session["DB"].ToString());

            try
            {
                if (cock.SelectByID(id))
                {

                    //Delete composant
                    Composants comp = new Composants(Session["DB"].ToString());
                    comp.SelectByFieldName("Id_Cocktail", cock.Cocktail.Id);
                    List<Composant> compList = comp.ToList();
                    if(compList.Count>0)
                    foreach (var item in compList)
                    {
                        comp.DeleteRecordByID(item.Id);
                    }

                    //Delete Cocktail
                    cock.Cocktail = collection;
                    cock.Cocktail.RemovePoster();
                    cock.DeleteRecordByID(id);

                    

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Home");
            }
            catch(Exception wat)
            {
                return View(collection);
            }   
        }
    }
}
