namespace Tests.Comparers.Shows
{
    using Domain;
    using Tests.Extensions;

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

            int genresResult = x.Genres.Select(g => g.GenreId).Compare(y.Genres.Select(g => g.GenreId));
            if (genresResult != 0)
            {
                return genresResult;
            }

            int filmingLocationsResult = x.FilmingLocations.Select(fl => fl.FilmingLocationId).Compare(y.FilmingLocations.Select(fl => fl.FilmingLocationId));
            if (filmingLocationsResult != 0)
            {
                return filmingLocationsResult;
            }

            int languagesResult = x.Languages.Select(l => l.LanguageId).Compare(y.Languages.Select(l => l.LanguageId));
            if (languagesResult != 0)
            {
                return languagesResult;
            }

            int countriesOfOriginResult = x.CountriesOfOrigin.Select(coo => coo.CountryOfOriginId).Compare(y.CountriesOfOrigin.Select(coo => coo.CountryOfOriginId));

            if (countriesOfOriginResult != 0)
            {
                return countriesOfOriginResult;
            }

            return 0;
        }
    }
}
