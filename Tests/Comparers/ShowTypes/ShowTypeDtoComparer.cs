namespace Tests.Comparers.ShowTypes
{
    using Application.DTOs.ShowTypes;

    public class ShowTypeDtoComparer : IComparer<ShowTypeDto>
    {
        public int Compare(ShowTypeDto? x, ShowTypeDto? y)
        {
            if (x!.Id != y!.Id)
            {
                return x.Id.CompareTo(y.Id);
            }

            if (x.Name != y.Name)
            {
                return x.Name.CompareTo(y.Name);
            }

            return 0;
        }
    }
}
