namespace Tests.Comparers.Shows
{
    using Application.DTOs.Shows;
    using Tests.Comparers.Photos;
    using Tests.Comparers.ShowTypes;
    using Tests.Extensions;

    public class ShowAddOrEditDtoComparer : IComparer<ShowAddOrEditShowDto>
    {
        public int Compare(ShowAddOrEditShowDto? x, ShowAddOrEditShowDto? y)
        {
            if (x!.Title != y!.Title)
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

            if (x.Season != y.Season)
            {
                return Nullable.Compare(x.Season, y.Season);
            }

            if (x.Duration != y.Duration)
            {
                return Nullable.Compare(x.Duration, y.Duration);
            }

            PhotoDtoComparer photoDtoComparer = new();
            int photoResult = photoDtoComparer.Compare(x.Photo, y.Photo);
            if (photoResult != 0)
            {
                return photoResult;    
            }

            TVSeriesDtoComparer tvSeriesComparer = new();
            int tvSeriesResult = tvSeriesComparer.Compare(x!.Series, y!.Series);
            if (tvSeriesResult != 0)
            {
                return tvSeriesResult;
            }

            ShowTypeDtoComparer showTypeComparer = new();
            int showTypeResult = showTypeComparer.Compare(x.ShowType, y.ShowType);
            if (showTypeResult != 0)
            {
                return showTypeResult;
            }

            int genresResult = x.Genres.Compare(y.Genres);
            if (genresResult != 0)
            {
                return genresResult;
            }

            int languagesResult = x.Languages.Compare(y.Languages);
            if (languagesResult != 0)
            {
                return languagesResult;
            }

            int filmingLocationsResult = x.FilmingLocations.Compare(y.FilmingLocations);
            if (filmingLocationsResult != 0)
            {
                return filmingLocationsResult;
            }

            int countriesOfOriginResult = x.CountriesOfOrigin.Compare(y.CountriesOfOrigin);
            if (countriesOfOriginResult != 0)
            {
                return countriesOfOriginResult;
            }

            return 0;
        }
    }
}
