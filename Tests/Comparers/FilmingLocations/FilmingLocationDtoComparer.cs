namespace Tests.Comparers.FilmingLocations
{
    using Application.DTOs.FilmingLocations;

    public class FilmingLocationDtoComparer : IComparer<FilmingLocationDto>
    {
        public int Compare(FilmingLocationDto? x, FilmingLocationDto? y)
        {
            if (x!.FilmingLocationId != y!.FilmingLocationId)
            {
                return x.FilmingLocationId.CompareTo(y.FilmingLocationId);
            }

            if (x!.Name != y!.Name)
            {
                return x.Name.CompareTo(y.Name);
            }

            return 0;
        }
    }
}
