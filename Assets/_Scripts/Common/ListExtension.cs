using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NOOD
{
    public static class Enumerable
    {
        public static T GetRandom<T>(this IEnumerable<T> source)
        {
            return source.ToList().GetRandom();
        }
        public static T GetRandom<T>(this IList<T> source)
        {
            Random r = new Random();
            int index = r.Next(0, source.Count);
            return source[index];
        }
        public static T GetRandom<T>(this T[] source)
        {
            Random r = new Random();
            int index = r.Next(0, source.Length);
            return source[index];
        }
    }
}
