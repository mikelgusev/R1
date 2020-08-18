using System;
using System.Collections.Generic;
using System.Linq;

namespace R1
{
    class Program
    {
        static void Main(string[] args)
        {
            4.Perm().DistinctAndSort().ToStr().Show();
            "aabc".Perm().DistinctAndSort().ToStr().Show();
            "dd".Perm().DistinctAndSort().ToStr().Show();
        }
    }
    public static class E
    {
        public static T It<T>(this T x) => x;
        public static void Show(this string s) => Console.WriteLine(s);
        public static IEnumerable<IEnumerable<int>> Perm(this int n) => Enumerable.Range(1, n).Perm();
        public static string ToStr<T>(this IEnumerable<IEnumerable<T>> x) => "[" + string.Join('\n', x.Select(y => string.Join(' ', y))) + "]";
        public static IEnumerable<IEnumerable<T>> DistinctAndSort<T>(this IEnumerable<IEnumerable<T>> x) => x.Distinct(new Eq<T>()).OrderByDescending(It, new Cmp<T>()).Select(It);
        public static IEnumerable<IEnumerable<T>> Perm<T>(this IEnumerable<T> ts) => ts.Count() < 2 ? new[] { ts } : ts.SelectMany((x, i) => ts.Where((y, j) => j != i).Perm().Select(t => t.Prepend(x)));

        private class Eq<T> : EqualityComparer<IEnumerable<T>>
        {
            public override int GetHashCode(IEnumerable<T> obj) => 0;
            public override bool Equals(IEnumerable<T> x, IEnumerable<T> y) => x.SequenceEqual(y);
        }

        private class Cmp<T> : Comparer<IEnumerable<T>>
        {
            public override int Compare(IEnumerable<T> x, IEnumerable<T> y)
            {
                int rb;
                bool xb, yb;
                using var xe = x.GetEnumerator();
                using var ye = y.GetEnumerator();
                while ((xb = xe.MoveNext())&(yb = ye.MoveNext())) if ((rb = Comparer<T>.Default.Compare(xe.Current, ye.Current)) != 0) return rb;
                return xb.CompareTo(yb);
            }
        }
    }

}

