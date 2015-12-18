using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCocktail.Models
{
    public class Cocktail
    {
        public long Id { get; set; }
        public String Nom { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }

    }

    public class Cocktails:SqlExpressUtilities.SqlExpressWrapper
    {
        public Cocktail Cocktail { get; set; }

        public Cocktails(String conn):base(conn)
        {

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