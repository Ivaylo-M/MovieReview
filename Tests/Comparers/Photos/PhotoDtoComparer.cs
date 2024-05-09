namespace Tests.Comparers.Photos
{
    using Application.DTOs.Photos;

    public class PhotoDtoComparer : IComparer<PhotoDto>
    {
        public int Compare(PhotoDto? x, PhotoDto? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            if (x!.Id != y!.Id)
            {
                return x.Id.CompareTo(y.Id);
            }

            if (x.Url != y.Url)
            {
                return x.Url.CompareTo(y.Url);
            }

            return 0;
        }
    }
}
