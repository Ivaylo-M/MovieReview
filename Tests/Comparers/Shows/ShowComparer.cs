namespace Tests.Comparers.Shows
{
    using Domain;

    public class ShowComparer : IComparer<Show>
    {
        public int Compare(Show? x, Show? y)
        {
            int showIdResult = StringComparer.OrdinalIgnoreCase.Compare(x!.ShowId.ToString(), y!.ShowId.ToString());
            if (showIdResult != 0)
            {
                return showIdResult;
            }

            if (x.Title != y.Title)
            {
                return x.Title.CompareTo(y.Title);
            }

            if (x.Description != y.Description)
            {
                return x.Description.CompareTo(y.Description);
            }

            if (x.ReleaseDate != y.ReleaseDate)
            {
                return x.ReleaseDate.CompareTo(y.ReleaseDate);
            }

            if (x.EndDate != y.EndDate)
            {
                return Nullable.Compare(x.EndDate, y.EndDate);
            }

            if (x.Duration != y.Duration)
            {
                return Nullable.Compare(y.Duration, x.Duration);
            }

            if (x.ShowType != y.ShowType)
            {
                return x.ShowType.CompareTo(y.ShowType);
            }

            if (x.Season != y.Season)
            {
                return Nullable.Compare(x.Season, y.Season);
            }

            int seriesIdResult = StringComparer.OrdinalIgnoreCase.Compare(x.SeriesId.ToString(), y.SeriesId.ToString());
            if (x.SeriesId != y.SeriesId)
            {
                return seriesIdResult;
            }

            int genresResult = CompareCollections(x.Genres.Select(g => g.GenreId).ToList(), y.Genres.Select(g => g.GenreId).ToList());
            if (genresResult != 0)
            {
                return genresResult;
            }

            int filmingLocationsResult = CompareCollections(x.FilmingLocations.Select(fl => fl.FilmingLocationId).ToList(), y.FilmingLocations.Select(fl => fl.FilmingLocationId).ToList());
            if (filmingLocationsResult != 0)
            {
                return filmingLocationsResult;
            }

            int languagesResult = CompareCollections(x.Languages.Select(l => l.LanguageId).ToList(), y.Languages.Select(l => l.LanguageId).ToList());
            if (languagesResult != 0)
            {
                return languagesResult;
            }

            int countriesOfOriginResult = CompareCollections(x.CountriesOfOrigin.Select(coo => coo.CountryOfOriginId).ToList(), y.CountriesOfOrigin.Select(coo => coo.CountryOfOriginId).ToList());

            if (countriesOfOriginResult != 0)
            {
                return countriesOfOriginResult;
            }

            return 0;
        }

        private static int CompareCollections(IList<int>? x, IList<int>? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            if (x.Count != y.Count)
            {
                return x.Count.CompareTo(y.Count);
            }

            for (int i = 0; i < x.Count; i++)
            {
                if (x[i] != y[i])
                {
                    return x[i].CompareTo(y[i]);
                }
            }

            return 0;
        }
    }
}
