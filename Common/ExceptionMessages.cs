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

        public static class Photo
        {
            public const string EmptyPhoto = "File is not selected or empty";
            public const string ShowAlreadyHavePhoto = "This show already have photo";
            public const string PhotoNotFound = "Photo does not exist";
            public const string NoPhotoYet = "This show doesn't have a photo yet";
        }

        public static class Show
        {
            public const string ShowNotFound = "This show does not exist! Please select an existing one";
        }
    }
}
