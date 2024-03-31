﻿namespace Common
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

        public static class Show
        {
            public const string FailedAddShow = "Failed to create show - {0}";
            public const string FailedEditShow = "Failed to edit show - {0}";
        }
    }
}
