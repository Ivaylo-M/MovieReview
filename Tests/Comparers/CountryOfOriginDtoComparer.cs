namespace Tests.Comparers
{
    using Application.DTOs.CountriesOfOrigin;

    public class CountryOfOriginDtoComparer : IComparer<CountryOfOriginDto>
    {
        public int Compare(CountryOfOriginDto? x, CountryOfOriginDto? y)
        {
            if (x!.CountryOfOriginId != y!.CountryOfOriginId)
            {
                return x.CountryOfOriginId.CompareTo(y.CountryOfOriginId);
            }

            if (x!.Name != y.Name)
            {
                return x.Name.CompareTo(y.Name);
            }

            if (x!.HasValue != y.HasValue)
            {
                return x.HasValue!.Value.CompareTo(y.HasValue!.Value);
            }

            return 0;
        }
    }
}
