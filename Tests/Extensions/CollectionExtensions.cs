namespace Tests.Extensions
{
    using System.Reflection;

    public static class CollectionExtensions
    {
        public static int Compare(this IEnumerable<int>? x, IEnumerable<int> y)
        {
            if (ReferenceEquals(x, y)) return 0;

            if (x!.Count() != y.Count()) return x!.Count().CompareTo(y.Count());

            using var enumerator1 = x!.GetEnumerator();
            using var enumerator2 = y.GetEnumerator();
            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                int result = enumerator1.Current.CompareTo(enumerator2.Current);
                if (result != 0)
                {
                    return result;
                }
            }

            return 0;
        }

        public static int Compare<TClass>(this IEnumerable<TClass>? x, IEnumerable<TClass>? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            if (x.Count() != y.Count())
            {
                return x.Count().CompareTo(y.Count());
            }

            IComparer<TClass> comparer = InitializeComparer<TClass>();

            using var enumerator1 = x.GetEnumerator();
            using var enumerator2 = y.GetEnumerator();
            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                int result = comparer.Compare(enumerator1.Current, enumerator2.Current);
                if (result != 0)
                {
                    return result;
                }
            }

            return 0;
        }

        private static IComparer<TClass> InitializeComparer<TClass>()
        {
            Assembly assembly = Assembly.GetCallingAssembly();

            string dtoTypeName = typeof(TClass).Name;

            Type? comparerType = assembly.GetTypes().FirstOrDefault(type => type.Name.Contains(dtoTypeName) &&
                typeof(IComparer<TClass>).IsAssignableFrom(type)) ?? throw new ArgumentNullException("The comparer type is null");

            object? comparer = Activator.CreateInstance(comparerType) ?? throw new ArgumentNullException("The comparer is null");

            IComparer<TClass> convertedComparer = (comparer as IComparer<TClass>)!;

            return convertedComparer;
        }
    }
}
