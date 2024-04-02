namespace Tests.Comparers
{
    using Application.DTOs.Languages;

    public class LanguageDtoComparer : IComparer<LanguageDto>
    {
        public int Compare(LanguageDto? x, LanguageDto? y)
        {
            if (x!.LanguageId != y!.LanguageId)
            {
                return x.LanguageId.CompareTo(y.LanguageId);
            }

            if (x!.Name != y.Name)
            {
                return x.Name.CompareTo(y.Name);
            }

            return 0;
        }
    }
}
