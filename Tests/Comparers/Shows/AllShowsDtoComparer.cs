namespace Tests.Comparers.Shows
{
    using Application.DTOs.Shows;
    using Tests.Extensions;

    public class AllShowsDtoComparer : IComparer<AllShowsDto>
    {
        public int Compare(AllShowsDto? x, AllShowsDto? y)
        {
            int showIdResult = StringComparer.OrdinalIgnoreCase.Compare(x!.ShowId, y!.ShowId);
            if (showIdResult != 0)
            {
                return showIdResult;
            }

            int photoUrlResult = StringComparer.OrdinalIgnoreCase.Compare(x.PhotoUrl, y.PhotoUrl);
            if (photoUrlResult != 0)
            {
                return photoUrlResult;
            }

            if (x.Title != y.Title)
            {
                return x.Title.CompareTo(y.Title);
            }

            if (x.Description != y.Description)
            {
                return x.Description.CompareTo(y.Description);
            }

            if (x.ReleaseYear != y.ReleaseYear)
            {
                return x.ReleaseYear.CompareTo(y.ReleaseYear);
            }

            if (x.EndYear != y.EndYear)
            {
                return Nullable.Compare(x.EndYear, y.EndYear);
            }

            if (x.Duration != y.Duration)
            {
                return Nullable.Compare(y.Duration, x.Duration);
            }

            if (x.AverageRating != y.AverageRating)
            {
                return x.AverageRating.CompareTo(y.AverageRating);
            }

            if (x.NumberOfRatings != y.NumberOfRatings)
            {
                return x.NumberOfRatings.CompareTo(y.NumberOfRatings);
            }

            if (x.MyRating != y.MyRating)
            {
                return Nullable.Compare(x.MyRating, y.MyRating);
            }

            if (x.ShowType != y.ShowType)
            {
                return x.ShowType.CompareTo(y.ShowType);
            }

            int genresResult = x.Genres.Compare(y.Genres);
            if (genresResult != 0)
            {
                return genresResult;
            }

            return 0;
        }
    }
}
