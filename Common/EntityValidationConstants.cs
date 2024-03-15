namespace Common
{
    public static class EntityValidationConstants
    {
        public static class Show
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 100;
            public const int DescriptionMaxLength = 500;
        }

        public static class User
        {
            public const int BioMaxLength = 1200;
            public const int PasswordMinLength = 5;
            public const int NameMinLength = 2;
            public const int NameMaxLength = 100;
        }

        public static class ShowType
        {
            public const int NameMaxLength = 20;
        }

        public static class Review
        {
            public const int HeadingMaxLength = 100;
            public const int ContentMaxLength = 1200;
        }

        public static class RegionOfResidence
        {
            public const int NameMaxLength = 100;
        }

        public static class Language
        {
            public const int NameMaxLength = 30;
        }

        public static class Genre
        {
            public const int NameMaxLength = 50;
        }

        public static class FilmingLocation
        {
            public const int NameMaxLength = 50;
        }

        public static class CountryOfOrigin
        {
            public const int NameMaxLength = 50;
        }
    }
}
