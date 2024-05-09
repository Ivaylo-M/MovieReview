using Application.DTOs.Shows;

namespace Tests.Comparers.Shows
{
    public class TVSeriesDtoComparer : IComparer<TVSeriesDto>
    {
        public int Compare(TVSeriesDto? x, TVSeriesDto? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            if (!x!.Id.Equals(y!.Id, StringComparison.OrdinalIgnoreCase))
            {
                return StringComparer.OrdinalIgnoreCase.Compare(x.Id, y.Id);
            }

            if (x!.Title != y.Title)
            {
                return x.Title.CompareTo(y.Title);
            }

            return 0;
        }
    }
}