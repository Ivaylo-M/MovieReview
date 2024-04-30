namespace Tests.Comparers.Shows
{
    using Application.DTOs.Shows;

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

            int genresResult = CompareGenres(x.Genres.ToList(), y.Genres.ToList());
            if (genresResult != 0)
            {
                return genresResult;
            }

            return 0;
        }

        private static int CompareGenres(IList<int> x, IList<int> y)
        {
            if (ReferenceEquals(x, y)) return 0;

            if (x.Count != y.Count) return x.Count.CompareTo(y.Count);

            for (int i = 0; i < x.Count; i++)
            {
                int elementComparison = x[i].CompareTo(y[i]);
                if (elementComparison != 0)
                {
                    return elementComparison;
                }
            }

            return 0;
        }
    }
}
