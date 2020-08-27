using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace R1
{
    class Program
    {
        static void Main(string[] args)
        {
            "abbc".Do();
            new[] { "a", "bb", "b", "c" }.Do();
            "ab".Do();
            "a".Do();
            "".Do();
            Console.ReadLine();
        }
    }
    public static class EAlg
    {
        public static IEnumerable<IEnumerable<T>> Perm<T>(this IEnumerable<T> ts)
        {
            if (ts.Any())
            {
                var was = new HashSet<T>();
                foreach (var (t,i) in ts.Select((t,i)=>(t,i)))
                    if (was.Add(t))
                        foreach (var s in ts.Take(i).Concat(ts.Skip(i + 1)).Perm())
                            yield return s.Prepend(t);
            }
            else yield return ts;
        }
    }

    public static class ECommon
    {
        public static void Do<T>(this IEnumerable<T> ts) => ts.MyOrder().Perm().ToStr().Show();
        public static void Show(this string s) => Console.WriteLine(s);
        public static string ToStr<T>(this IEnumerable<IEnumerable<T>> x) => "(" + string.Join('\n', x.Select(y => string.Join(' ', y))) + ")";
        public static T It<T>(T x) => x;
        public static IEnumerable<T> MyOrder<T>(this IEnumerable<T> ts) => ts.OrderByDescending(It).Select(It);
    }
}

