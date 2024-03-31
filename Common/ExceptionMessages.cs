namespace Common
{
    public static class ExceptionMessages
    {
        public static class Repository
        {
            public const string EntityNotFound = "Entity not found";
        }

        public static class User
        {
            public const string UserNotFound = "The user is not found";
            public const string UserResultNotSucceeded = "The user result is not succeeded";
            public const string ChangingPasswordFailed = "Changing password failed";
            public const string NotAuthenticated = "The user is not authenticated";
        }

        public static class MemoryCache
        {
            public const string NullValue = "The value of the cache is null";
        }

        public static class Show
        {
            public const string ShowNotFound = "The show is not found";
        }
    }
}
