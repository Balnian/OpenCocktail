using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenCocktail.Models
{
    public class Cocktail
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

        [Display(Name = "Image")]
        public String Image { get; set; }

        private ImageGUIDReference ImageReference;

    }

    public class Cocktails:SqlExpressUtilities.SqlExpressWrapper
    {
        public Cocktail Cocktail { get; set; }

        public Cocktails(String conn):base(conn)
        {
            Cocktail = new Cocktail();
        }

        public Cocktails()
        {
            Cocktail = new Cocktail();
        }

        public List<Cocktail> ToList()
        {
            List<object> list = this.RecordsList();
            List<Cocktail> Cocktails_list = new List<Cocktail>();
            foreach (Cocktail Cocktail in list)
            {
                Cocktails_list.Add(Cocktail);
            }
            return Cocktails_list;
        }
    }
}