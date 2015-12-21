using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenCocktail.Models
{
    public class Composant
    {
        public long Id { get; set; }

        [Display(Name = "Id_Cocktail")]
        public long Id_Cocktail { get; set; }

        [Display(Name = "Id_Ingredient")]
        public long Id_Ingredient { get; set; }

        [Display(Name = "Qte")]
        public long Qte { get; set; }

        public Composant()
        {
            Id_Cocktail = 0;
            Id_Ingredient = 0;
            Qte = 1;
        }
    }
     public class Composants:SqlExpressUtilities.SqlExpressWrapper
    {
        public Composant Composant { get; set; }

        public Composants(String conn):base(conn)
        {
            Composant = new Composant();
        }

        public Composants()
        {
            Composant = new Composant();
        }

        public List<Composant> ToList()
        {
            List<object> list = this.RecordsList();
            List<Composant> Cocktails_list = new List<Composant>();
            foreach (Composant Cocktail in list)
            {
                Cocktails_list.Add(Cocktail);
            }
            return Cocktails_list;
        }
    }
}