namespace Common
{
    public static class FailMessages
    {
        public static class User
        {
            public const string FailedDeleteUser = "Failed to delete the user {0}";
            public const string FailedEditUser = "Failed to edit the user {0}";
            public const string FailedLogin = "Invalid email or password";
            public const string FailedRegister = "User registration failed";
            public const string FailedLogout = "Failed to logout";
        }

        public static class Photo
        {
            public const string FailedUploadPhoto = "Error occured during uploading photo";
            public const string FailedDeletePhoto = "Error occured during deleting photo";
        }
    }
}
