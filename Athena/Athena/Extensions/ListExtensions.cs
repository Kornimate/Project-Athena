using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> elements)
        {
            Random rndGen = new Random();
            int n = elements.Count;
            while (n > 1)
            {
                n--;
                
                int randPlace = rndGen.Next(n + 1);

                (elements[n], elements[randPlace]) = (elements[randPlace], elements[n]);
            }
        }
    }
}
