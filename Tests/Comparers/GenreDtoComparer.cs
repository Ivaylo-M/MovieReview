namespace Tests.Comparers
{
    using Application.DTOs.Genres;
    using System.Collections;

    public class GenreDtoComparer : IComparer<GenreDto>
    {
        public int Compare(GenreDto? x, GenreDto? y)
        {
            if (x!.GenreId != y!.GenreId)
            {
                return x.GenreId.CompareTo(y.GenreId);
            }

            if (x!.Name != y.Name)
            {
                return x.Name.CompareTo(y.Name);
            }

            return 0;
        }
    }
}
