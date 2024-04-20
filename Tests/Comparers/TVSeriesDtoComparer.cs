using Application.DTOs.Shows;

namespace Tests.Comparers
{
    public class TVSeriesDtoComparer : IComparer<TVSeriesDto>
    {
        public int Compare(TVSeriesDto? x, TVSeriesDto? y)
        {
            if (x!.Id.ToLower() != y!.Id.ToLower()) 
            {
                return x.Id.CompareTo(y.Id);
            }

            if (x!.Title != y.Title)
            {
                return x.Title.CompareTo(y.Title);
            }

            return 0;
        }
    }
}