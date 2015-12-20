using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenCocktail.Models
{

    public class Ingredient
    {

        public long Id { get; set; }

        [Display(Name = "Nom")]
        [StringLength(50), Required]
        [RegularExpression(@"^((?!^Name$)[-a-zA-Z0-9 àâäçèêëéìîïòôöùûüÿñÀÂÄÇÈÊËÉÌÎÏÒÔÖÙÛÜ_'])+$", ErrorMessage = "Caractères illégaux.")]
        public String Nom { get; set; }

        [Display(Name = "Description")]
        [StringLength(50), Required]
        [RegularExpression(@"^((?!^Name$)[-a-zA-Z0-9 àâäçèêëéìîïòôöùûüÿñÀÂÄÇÈÊËÉÌÎÏÒÔÖÙÛÜ_'])+$", ErrorMessage = "Caractères illégaux.")]
        public String Description { get; set; }



        public Ingredient()
        {
            Nom = "";
            Description = "";
        }

    }

    public class Ingredients : SqlExpressUtilities.SqlExpressWrapper
    {
        public Ingredient Ingredient { get; set; }

        public Ingredients(String conn)
            : base(conn)
        {
            Ingredient = new Ingredient();
        }

        public Ingredients()
        {
            Ingredient = new Ingredient();
        }

        public List<Ingredient> ToList()
        {
            List<object> list = this.RecordsList();
            List<Ingredient> Ingredients_list = new List<Ingredient>();
            foreach (Ingredient Ingredient in list)
            {
                Ingredients_list.Add(Ingredient);
            }
            return Ingredients_list;
        }
    }

}